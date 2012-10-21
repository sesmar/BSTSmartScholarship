namespace BSTSmartScholarship.Business
{
	#region "Using Statements"

	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;

	#endregion 

	public class ApplicantList
	{
		public static IEnumerable<Applicant> GetList()
		{
			using (ISmartScholarshipContext cdx = SmartScholarshipContext.Current)
			{
				return cdx.Applicants.ToList();
			}
		}
	}
}
