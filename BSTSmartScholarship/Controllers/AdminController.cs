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
		public ActionResult VerifyWithRegistrar()
		{
			return Content("<div>Hello world</div>", "text/html");
		}
	}
}
