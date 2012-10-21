namespace BSTSmartScholarship.Business
{
	#region "Using Statements"

	using System;
	using System.Collections.Generic;
	using System.Data.Entity;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;

	#endregion

	public interface ISmartScholarshipContext : IDisposable
	{
		IDbSet<Applicant> Applicants { get; }
		//IDbSet<Award> Awards { get; }

		Int32 SaveChanges();
	}
}
