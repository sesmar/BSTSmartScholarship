namespace BSTSmartScholarship.Models
{
	#region "Using Statemets"

	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Web;
using System.Web.Mvc;

	using BSTSmartScholarship.Business;

	#endregion

	public class SelectLists
	{
		public static List<SelectListItem> GetGenders()
		{
			List<SelectListItem> items = new List<SelectListItem>();

			foreach (var gender in Enum.GetValues(typeof(Gender)).Cast<Gender>())
			{
				SelectListItem item = new SelectListItem();
				item.Text = gender.ToString();
				item.Value = ((Int32)gender).ToString();

				items.Add(item);
			}

			return items;
		}

		public static List<SelectListItem> GetStatus()
		{
			List<SelectListItem> items = new List<SelectListItem>();

			foreach (var status in Enum.GetValues(typeof(Status)).Cast<Status>())
			{
				SelectListItem item = new SelectListItem();
				item.Text = status.ToString();
				item.Value = ((Int32)status).ToString();

				items.Add(item);
			}

			return items;
		}
	}
}