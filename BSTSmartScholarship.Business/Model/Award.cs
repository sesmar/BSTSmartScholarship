namespace BSTSmartScholarship.Business.Model
{
	#region "Using Statements"

	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;

	#endregion

	public class Award
	{
		public String StudentNumber { get; set; }
		public String FirstName { get; set; }
		public String LastName { get; set; }
		public String Year { get; set; }
		public String Semester { get; set; }
	}
}
