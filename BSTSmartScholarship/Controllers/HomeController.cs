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

	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			ViewBag.Message = "What we are about...";

			return View();
		}

		public ActionResult Apply()
		{
			ViewBag.Message = "Your app description page.";

			return View();
		}

		[HttpPost]
		public ActionResult Apply(Applicant applicant)
		{
			applicant.Save();

			return RedirectToAction("Index");
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}
	}
}
