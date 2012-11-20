namespace BSTSmartScholarship.Controllers
{
	#region "Using Statements"

	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Web;
	using System.Web.Mvc;

	using BSTSmartScholarship.Business;
	#endregion

	public class AdminController : Controller
	{
		//
		// GET: /Admin/
		[Authorize]
		public ActionResult Index()
		{
			return View(ApplicantList.GetList());
		}

		[Authorize]
		public ActionResult VerifyWithRegistrar(String sn)
		{
			Applicant applicant = Applicant.GetApplicant(sn);
			return PartialView(applicant);
			//			return Content(String.Format("<div>{0}</div>", applicant.FirstName), "text/html");
		}
	}
}
