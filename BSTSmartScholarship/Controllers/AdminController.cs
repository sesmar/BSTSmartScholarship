namespace BSTSmartScholarship.Controllers
{
	#region "Using Statements"

	using System;
	using System.Collections.Generic;
	using System.Linq;
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
			return View(ApplicantList.GetList(a => a.IsVerified.GetValueOrDefault(false) && a.IsEligible == null));
		}

		[Authorize]
		public ActionResult DeclineApplicant(String sn)
		{
			Applicant.DeclineApplicant(sn);
			return RedirectToAction("Index", "Admin");
		}

		[Authorize]
		public ActionResult VerifyApplicant(String sn)
		{
			Applicant.VerifyApplicant(sn);
			return RedirectToAction("Index", "Admin");
		}
	}
}
