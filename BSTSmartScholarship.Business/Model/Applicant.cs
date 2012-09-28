namespace BSTSmartScholarship.Business.Model
{
	#region "Using Statements"

	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;
	using System.ComponentModel.DataAnnotations;

	#endregion

	public class Applicant
	{
		#region "Public Properties"

		[Required(ErrorMessage="Student number is requried")]
		[StringLength(10, ErrorMessage = "Student number cannot be longer than 10 characters")]
		public String StudentNumber { get; set; }

		[Required(ErrorMessage = "First name is requried")]
		[StringLength(128, ErrorMessage="First name cannot be longer than 128 characters")]
		public String FirstName { get; set; }

		[Required(ErrorMessage = "Last name is requried")]
		[StringLength(128, ErrorMessage = "Last name cannot be longer than 128 characters")]
		public String LastName { get; set; }

		[Required(ErrorMessage = "Email address is requried")]
		[StringLength(10, ErrorMessage = "Phone numbercannot be longer than 10 characters")]
		public String PhoneNumber { get; set; }

		[Required(ErrorMessage = "Email address is requried")]
		[StringLength(128, ErrorMessage="Email address cannot be longer than 128 characters")]
		public String EmailAddress { get; set; }

		[Required(ErrorMessage = "Gender is requried")]
		[Range(1,2, ErrorMessage="Gender must be \"Male\" or \"Female\"")]
		public Int32 Gender { get; set; }

		[Required(ErrorMessage = "Date of Birth is requried")]
		public DateTime DateOfBirth { get; set; }

		[Required(ErrorMessage = "Status is requried")]
		public Int32 Status { get; set; }

		[Required(ErrorMessage = "Cummulative G.P.A. is requried")]
		[Range(0, 4.0,ErrorMessage="Cummulative G.P.A. must be between 0 and 4.0")]
		public Double CummulativeGPA { get; set; }

		[Required(ErrorMessage = "Current number of credit hours is requried")]
		[Range(0, 21, ErrorMessage = "The current number of credits hours must be between 0 and 21")]
		public Int32 CreditHours { get; set; }

		public Boolean? IsEligible { get; set; }

		#endregion

		#region "Factory Methods"

		public Applicant GetApplicant(String studentNumber)
		{
			Applicant applicant = SmartScholarshipContext.Current.Applicants.FirstOrDefault(a => a.StudentNumber.Equals(studentNumber, StringComparison.OrdinalIgnoreCase));

			return applicant;
		}

		public Applicant NewApplicant()
		{
			return new Applicant();
		}

		public Applicant NewApplicant(String studentNumber, String firstName, String lastName, String phoneNumber, String emailAddress, Int32 gender, DateTime dateOfBirth, Int32 status, Double cummulativeGPA, Int32 creditHours)
		{
			Applicant applicant = new Applicant();

			applicant.StudentNumber = studentNumber;
			applicant.FirstName = firstName;
			applicant.LastName = lastName;
			applicant.PhoneNumber = phoneNumber;
			applicant.EmailAddress = emailAddress;
			applicant.Gender = gender;
			applicant.DateOfBirth = dateOfBirth;
			applicant.Status = status;
			applicant.CummulativeGPA = cummulativeGPA;
			applicant.CreditHours = creditHours;

			return applicant;
		}

		public Applicant()
		{
		}

		#endregion
	}
}
