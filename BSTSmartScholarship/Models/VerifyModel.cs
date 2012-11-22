namespace BSTSmartScholarship.Models
{
	#region "Using Statements"

	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Web;
	using BSTSmartScholarship.Business;

	#endregion

	public class VerifyModel
	{
		public Applicant Applicant { get; set; }
		public Student Student { get; set; }
	}
}