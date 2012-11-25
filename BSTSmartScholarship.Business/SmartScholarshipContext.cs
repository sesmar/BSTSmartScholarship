namespace BSTSmartScholarship.Business
{
	#region "Using Statements"

	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;
	using System.Data.Entity;

	#endregion

	public class SmartScholarshipContext : DbContext, ISmartScholarshipContext
	{
		public static SmartScholarshipContext _current;
		public static SmartScholarshipContext Current
		{
			get
			{
				if (_current == null)
					return new SmartScholarshipContext();
				
				return _current;
			}
		}

		public IDbSet<Applicant> Applicants { get; set; }
		//public IDbSet<Award> Awards { get; set; }
		public IDbSet<Vote> Votes { get; set; }
	}
}
