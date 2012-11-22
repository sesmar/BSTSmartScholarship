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
		private static List<Student> StudentList= new List<Student>()
			{
				new Student { StudentNumber = "88634351", FirstName = "Sebastian", LastName = "Sims", Status = 1, CreditHours = 12, CumulativeGPA = 3.86, DateOfBirth = new DateTime(2010, 2, 15)  },
				new Student { StudentNumber = "88634352", FirstName = "Christopher", LastName = "Sims", Status = 4, CreditHours = 6, CumulativeGPA = 3.6, DateOfBirth = new DateTime(1982, 7, 13)  },
			};

		#region "Public Properties"

		public String StudentNumber { get; set; }
		public String FirstName { get; set; }
		public String LastName { get; set; }
		public Int32 Status { get; set; }
		public Double CumulativeGPA { get; set; }
		public Int32 CreditHours { get; set; }
		public DateTime DateOfBirth { get; set; }

		#endregion

		#region "Factory Methods"

		public static Student GetStudent(String studentNumber)
		{
			return StudentList.FirstOrDefault(s => s.StudentNumber.Equals(studentNumber, StringComparison.OrdinalIgnoreCase));
		}

		#endregion
	}
}
