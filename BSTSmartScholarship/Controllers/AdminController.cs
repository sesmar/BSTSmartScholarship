namespace BSTSmartScholarship.Controllers
{
	#region "Using Statements"

	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Web;
	using System.Web.Mvc;

	#endregion

	public class AdminController : Controller
	{
		//
		// GET: /Admin/
		[Authorize]
		public ActionResult Index()
		{
			return View();
		}
	}
}
