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
		internal static List<Student> StudentList= new List<Student>()
			{
				new Student { StudentNumber = "88634351", FirstName = "Sebastian", LastName = "Sims", Status = 1, CreditHours = 12, CumulativeGPA = 3.89, DateOfBirth = new DateTime(2010, 2, 15), Gender = 1, CurrentGPA = 3.9 },
				new Student { StudentNumber = "88634352", FirstName = "Christopher", LastName = "Sims", Status = 4, CreditHours = 6, CumulativeGPA = 3.6, DateOfBirth = new DateTime(1982, 7, 13), Gender = 1, CurrentGPA = 3.8 },
				new Student { StudentNumber = "88634353", FirstName = "Cassandra", LastName = "Sims", Status = 3, CreditHours = 15, CumulativeGPA = 3.9, DateOfBirth = new DateTime(1982, 4, 1), Gender = 2, CurrentGPA = 4.0 },
				new Student { StudentNumber = "88634354", FirstName = "Chloe", LastName = "Sims", Status = 1, CreditHours = 12, CumulativeGPA = 3.89, DateOfBirth = new DateTime(2012, 3, 2), Gender = 2, CurrentGPA = 3.9 },
				new Student { StudentNumber = "88634355", FirstName = "Bob", LastName = "Golembiewski", Status = 2, CreditHours = 12, CumulativeGPA = 3.0, DateOfBirth = new DateTime(1992, 5, 23), Gender = 1, CurrentGPA = 3.5 },
				new Student { StudentNumber = "88634356", FirstName = "Denise", LastName = "Golembiewski", Status = 1, CreditHours = 15, CumulativeGPA = 3.34, DateOfBirth = new DateTime(1993, 4, 16), Gender = 2, CurrentGPA = 3.6 },
				new Student { StudentNumber = "88634357", FirstName = "Zoey", LastName = "Kashich", Status = 1, CreditHours = 12, CumulativeGPA = 3.89, DateOfBirth = new DateTime(2010, 1, 18), Gender = 2, CurrentGPA = 3.9 },
			};

		#region "Public Properties"

		public String StudentNumber { get; set; }
		public String FirstName { get; set; }
		public String LastName { get; set; }
		public Int32 Status { get; set; }
		public Double CumulativeGPA { get; set; }
		public Int32 CreditHours { get; set; }
		public DateTime DateOfBirth { get; set; }
		public Int32 Gender { get; set; }
		public Double CurrentGPA { get; set; }

		#endregion

		#region "Factory Methods"

		public static Student GetStudent(String studentNumber)
		{
			return StudentList.FirstOrDefault(s => s.StudentNumber.Equals(studentNumber, StringComparison.OrdinalIgnoreCase));
		}

		#endregion

		public override string ToString()
		{
			return "Student";
		}
	}
}
