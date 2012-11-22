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
	#endregion

	public class AdminController : Controller
	{
		//
		// GET: /Admin/
		[Authorize]
		public ActionResult Index()
		{
			return View(ApplicantList.GetList(a => !a.IsVerified.GetValueOrDefault(false)));
		}

		[Authorize]
		public ActionResult VerifyWithRegistrar(String sn)
		{
			Applicant applicant = Applicant.GetApplicant(sn);
			
			if (applicant != null)
			{
				BSTSmartScholarshipSerializer<Applicant> applicantSerializer = new BSTSmartScholarshipSerializer<Applicant>();
				XmlDocument applicantDoc = applicantSerializer.Serialize(applicant);
				Registrar registrar = new Registrar();
				XmlDocument studentDoc = registrar.VerifyApplicant(applicantDoc);
				BSTSmartScholarshipSerializer<Student> studentSerializer = new BSTSmartScholarshipSerializer<Student>();
				Student student = studentSerializer.Deserialize(studentDoc);

				return PartialView(applicant);
			}
			
			return Content("<div>Invalid Applicant</div>", "text/html");
		}
	}
}
