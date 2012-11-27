﻿namespace BSTSmartScholarship.Controllers
{
	#region "Using Statements"

	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Security.Principal;
	using System.Web;
	using System.Web.Mvc;
	using System.Xml;
	using BSTSmartScholarship.Business;
	using BSTSmartScholarship.Models;

	#endregion

	public class AdminController : Controller
	{
		//
		// GET: /Admin/
		[Authorize]
		public ActionResult Index()
		{
			if (Award.Awarded(null, null))
			{
				return RedirectToAction("Closed", "Admin");
			}

			return View(ApplicantList.GetList(a => !a.IsVerified == null));
		}

		[Authorize]
		public ActionResult VerifyWithRegistrar(String sn)
		{
			VerifyModel model = new VerifyModel();
			model.Applicant = Applicant.GetApplicant(sn);

			if (model.Applicant != null)
			{
				BSTSmartScholarshipSerializer<Applicant> applicantSerializer = new BSTSmartScholarshipSerializer<Applicant>();
				XmlDocument applicantDoc = applicantSerializer.Serialize(model.Applicant);
				Registrar registrar = new Registrar();
				XmlDocument studentDoc = registrar.VerifyApplicant(applicantDoc);
				BSTSmartScholarshipSerializer<Student> studentSerializer = new BSTSmartScholarshipSerializer<Student>();
				model.Student = studentSerializer.Deserialize(studentDoc);

				return PartialView(model);
			}

			return Content("<div>Invalid Applicant</div>", "text/html");
		}

		[Authorize]
		public ActionResult PendingReview()
		{
			if (Award.Awarded(null, null))
			{
				return RedirectToAction("Closed", "Admin");
			}

			return View(ApplicantList.GetList(a => a.IsVerified.GetValueOrDefault(false) && a.IsEligible.GetValueOrDefault(false)));
		}

		[Authorize]
		public ActionResult Awarded()
		{
			if (Award.Awarded(null, null))
			{
				return RedirectToAction("Closed", "Admin");
			}

			Boolean showVoteButton = false;
			List<Applicant> applicants = new List<Applicant>(ApplicantList.GetList(a => a.IsVerified.GetValueOrDefault(false) && a.IsEligible.GetValueOrDefault(false)));

			ViewBag.HasUserVoted = false;
			ViewBag.MostVotes = 0;

			if (applicants.Count > 0)
			{
				//Set up chain
				AwardScholarshipAgeHandler ageHandler = new AwardScholarshipAgeHandler(null);
				AwardScholarshipGenderHandler genderHandler = new AwardScholarshipGenderHandler(ageHandler);
				AwardScholarshipStatusHandler statusHandler = new AwardScholarshipStatusHandler(genderHandler);
				AwardScholarshipCurrentGPAHandler currentHandler = new AwardScholarshipCurrentGPAHandler(statusHandler);
				AwardScholarshipCumulativeGPAHandler cumulativeHandler = new AwardScholarshipCumulativeGPAHandler(currentHandler);
				AwardScholarshipHandler handler = new AwardScholarshipHandler(cumulativeHandler);

				applicants = handler.AwardScholarship(applicants);
				ViewBag.MostVotes = applicants.Max(a => a.Votes.Count);

				if (applicants.Count > 1)
				{
					using (ISmartScholarshipContext sdx = SmartScholarshipContext.Current)
					{
						if (sdx.Votes.Count(v => v.UserId.Equals(this.User.Identity.Name, StringComparison.OrdinalIgnoreCase)) == 0)
						{
							showVoteButton = true;
						}
					}

					if (applicants.Count(a => a.Votes.Count == ViewBag.MostVotes) == 1)
					{
						ViewBag.AwardTo = applicants.First(a => a.Votes.Count == ViewBag.MostVotes).StudentNumber;
					}
				}
				else
				{
					ViewBag.AwardTo = applicants.First().StudentNumber;
				}

				ViewBag.HasUserVoted = Vote.HasVoted(this.User.Identity.Name);	
			}

			ViewBag.ShowVoteButton = showVoteButton;
			return View(applicants);
		}

		[Authorize]
		public ActionResult Closed()
		{
			return View(Award.GetAwarded(null, null));
		}

		[Authorize]
		public ActionResult DeclineApplicant(String sn)
		{
			Applicant.DeclineApplicant(sn);
			EmailUtility email = new EmailUtility(new EmailService());
			email.SendDeclinedEmail(sn);

			return RedirectToAction("Index", "Admin");
		}

		[Authorize]
		public ActionResult RejectApplicant(String sn)
		{
			Applicant.RejectApplicant(sn);
			EmailUtility email = new EmailUtility(new EmailService());
			email.SendIneligibleEmail(sn);

			return RedirectToAction("PendingReview", "Admin");
		}

		[Authorize]
		public ActionResult VerifyApplicant(String sn)
		{
			Applicant.VerifyApplicant(sn);

			return RedirectToAction("Index", "Admin");
		}

		[Authorize]
		public ActionResult VoteForApplicant(String sn)
		{
			IIdentity identity = this.User.Identity;

			if (identity.IsAuthenticated)
			{
				Applicant applicant = Applicant.GetApplicant(sn);
				applicant.Votes.Add(new Vote() { StudentNumber = sn, UserId = identity.Name });
				applicant.Save();
			}

			return RedirectToAction("Awarded", "Admin");
		}

		public ActionResult AwardApplicant(String sn)
		{
			List<Applicant> applicants = new List<Applicant>(ApplicantList.GetList(a => a.IsVerified.GetValueOrDefault(false) && a.IsEligible.GetValueOrDefault(false)));
			IEmailService emailService = new EmailService();
			EmailUtility email = new EmailUtility(emailService);

			Applicant applicant = applicants.FirstOrDefault(a => a.StudentNumber.Equals(sn, StringComparison.OrdinalIgnoreCase));

			foreach (String studentNumber in applicants.Where(a=> !a.StudentNumber.Equals(sn, StringComparison.OrdinalIgnoreCase)).Select(a => a.StudentNumber))
			{	
				email.SendNotAwardedEmail(studentNumber);
			}

			Award.AddAward(applicant.StudentNumber, applicant.FirstName, applicant.LastName);
			email.SendAwardedEmail(sn);

			Registrar registrar = new Registrar();
			TuitionAmountRequest request = new TuitionAmountRequest() { StudentNumber = sn };
			XmlDocument requestDoc = (new BSTSmartScholarshipSerializer<TuitionAmountRequest>()).Serialize(request);

			XmlDocument responseDoc = registrar.RequestTuitionAmount(requestDoc);
			TuitionAmountResponse response = (new BSTSmartScholarshipSerializer<TuitionAmountResponse>()).Deserialize(responseDoc);

			//TODO: Send the amount to Accounting.

			return RedirectToAction("Awarded", "Admin");
		}
	}
}
