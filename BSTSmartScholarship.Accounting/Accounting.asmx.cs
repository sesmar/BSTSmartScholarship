using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace BSTSmartScholarship.Accounting
{
	/// <summary>
	/// Summary description for RequestRefund
	/// </summary>
	[WebService(Namespace = "http://accounting.bstsmartscholarship.com/")]
	[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
	[System.ComponentModel.ToolboxItem(false)]
	// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
	// [System.Web.Script.Services.ScriptService]
	public class Accounting
		: System.Web.Services.WebService
	{

		[WebMethod]
		public string RequestRefund(String StudentNumber, Double reimbursementAmount)
		{
			return "Request Processed";
		}
	}
}
