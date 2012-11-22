namespace BSTSmartScholarship.Business
{
	#region "Using Statements"

	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;

	#endregion

	public class Student
	{
		#region "Public Properties"

		String StudentNumber;
		String FirstName;
		String LastName;
		Int32 Status;
		Double CumulativeGPA;
		Int32 CreditHours;
		DateTime DateOfBirth;

		#endregion

		#region "Factory Methods"

		public Student GetStudent(String studentNumber)
		{
			return new Student();
		}

		#endregion
	}
}
