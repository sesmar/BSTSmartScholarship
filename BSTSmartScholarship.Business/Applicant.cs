namespace BSTSmartScholarship.Business
{
	#region "Using Statements"

	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;
	using System.ComponentModel.DataAnnotations;
	using System.Xml.Serialization;

	#endregion

	[Serializable]
	public class Applicant
	{
		private Boolean IsNew { get; set; }

		#region "Public Properties"

		[Key]
		[Required(ErrorMessage="Student number is requried")]
		[StringLength(10, ErrorMessage = "Student number cannot be longer than 10 characters")]
		[Display(Name = "Student Number")]
		public String StudentNumber { get; set; }

		[Required(ErrorMessage = "First name is requried")]
		[StringLength(128, ErrorMessage="First name cannot be longer than 128 characters")]
		[Display(Name = "First Name")]
		public String FirstName { get; set; }

		[Required(ErrorMessage = "Last name is requried")]
		[StringLength(128, ErrorMessage = "Last name cannot be longer than 128 characters")]
		[Display(Name = "Last Name")]
		public String LastName { get; set; }

		[Required(ErrorMessage = "Phone number is requried")]
		[StringLength(14, ErrorMessage = "Phone number cannot be longer than 14 characters")]
		[RegularExpression(RegexUtility.PhonePattern, ErrorMessage = "Invalid phone number format.")]
		[Display(Name = "Phone Number")]
		public String PhoneNumber { get; set; }

		[Required(ErrorMessage = "Email address is requried")]
		[StringLength(128, ErrorMessage="Email address cannot be longer than 128 characters")]
		[Display(Name = "Email Address")]
		public String EmailAddress { get; set; }

		[Required(ErrorMessage = "Gender is requried")]
		[Range(1,2, ErrorMessage="Gender must be \"Male\" or \"Female\"")]
		public Int32 Gender { get; set; }

		[Required(ErrorMessage = "Date of Birth is requried")]
		[Display(Name = "Date of Birth")]
		public DateTime DateOfBirth { get; set; }

		[Required(ErrorMessage = "Status is requried")]
		public Int32 Status { get; set; }

		[Required(ErrorMessage = "Cumulative G.P.A. is requried")]
		[Range(0, 4.0,ErrorMessage="Cumulative G.P.A. must be between 0 and 4.0")]
		[Display(Name = "Cummulative G.P.A.")]
		public Double CumulativeGPA { get; set; }

		[Required(ErrorMessage = "Current number of credit hours is requried")]
		[Range(0, 21, ErrorMessage = "The current number of credits hours must be between 0 and 21")]
		[Display(Name = "Credit Hours")]
		public Int32 CreditHours { get; set; }

		public Boolean? IsEligible { get; set; }

		public Boolean? IsVerified { get; set; }

		[XmlIgnore]
		private List<Vote> _votes = null;
		public List<Vote> Votes
		{
			get
			{
				if (_votes == null)
				{
					using (ISmartScholarshipContext sdx = SmartScholarshipContext.Current)
					{
						_votes = sdx.Votes.Where(v => v.StudentNumber.Equals(StudentNumber, StringComparison.OrdinalIgnoreCase)).ToList();
					}
				}

				return _votes;
			}
		}

		#endregion

		#region "Factory Methods"

		public static Applicant GetApplicant(String studentNumber)
		{
			Applicant applicant = SmartScholarshipContext.Current.Applicants.FirstOrDefault(a => a.StudentNumber.Equals(studentNumber, StringComparison.OrdinalIgnoreCase));
			applicant.IsNew = false;

			return applicant;
		}

		public Applicant NewApplicant()
		{
			return new Applicant();
		}

		public static Applicant NewApplicant(String studentNumber, String firstName, String lastName, String phoneNumber, String emailAddress, Int32 gender, DateTime dateOfBirth, Int32 status, Double cumulativeGPA, Int32 creditHours)
		{
			Applicant applicant = new Applicant();

			applicant.StudentNumber = studentNumber;
			applicant.FirstName = firstName;
			applicant.LastName = lastName;
			applicant.PhoneNumber = RegexUtility.FormatPhone(phoneNumber);
			applicant.EmailAddress = emailAddress;
			applicant.Gender = gender;
			applicant.DateOfBirth = dateOfBirth;
			applicant.Status = status;
			applicant.CumulativeGPA = cumulativeGPA;
			applicant.CreditHours = creditHours;

			return applicant;
		}

		public static void VerifyApplicant(String studentNumber)
		{
			using (ISmartScholarshipContext sdx = SmartScholarshipContext.Current)
			{
				Applicant applicant = Applicant.GetApplicant(studentNumber);
				applicant.IsVerified = true;
				applicant.IsEligible = true;

				applicant.Save();
			}
		}

		public static void DeclineApplicant(String studentNumber)
		{
			using (ISmartScholarshipContext sdx = SmartScholarshipContext.Current)
			{
				Applicant applicant = Applicant.GetApplicant(studentNumber);
				applicant.IsVerified = true;

				applicant.Save();
			}
		}

		public static void RejectApplicant(String studentNumber)
		{
			using (ISmartScholarshipContext sdx = SmartScholarshipContext.Current)
			{
				Applicant applicant = Applicant.GetApplicant(studentNumber);
				applicant.IsEligible = false;

				applicant.Save();
			}
		}

		public static void MarkAsIneligible(String studentNumber)
		{
			using (ISmartScholarshipContext sdx = SmartScholarshipContext.Current)
			{
				Applicant applicant = Applicant.GetApplicant(studentNumber);
				applicant.IsEligible = false;

				applicant.Save();
			}
		}

		public Applicant()
		{
			this.IsNew = true;
		}

		#endregion

		public override string ToString()
		{
			return "Applicant";
		}

		#region "Save (Insert/Update) Methods"

		public Applicant Save()
		{
			if (this.IsNew)
			{
				return this.Insert();
			}
			else
			{
				return this.Update();
			}
		}

		private Applicant Update()
		{
			using (ISmartScholarshipContext cdx = SmartScholarshipContext.Current)
			{
				Applicant applicant = cdx.Applicants.FirstOrDefault(a => a.StudentNumber.Equals(this.StudentNumber, StringComparison.OrdinalIgnoreCase));

				if (applicant != null)
				{
					applicant.StudentNumber = this.StudentNumber;
					applicant.FirstName = this.FirstName;
					applicant.LastName = this.LastName;
					applicant.PhoneNumber = RegexUtility.UnFormatPhone(this.PhoneNumber);
					applicant.EmailAddress = this.EmailAddress;
					applicant.Gender = this.Gender;
					applicant.DateOfBirth = this.DateOfBirth;
					applicant.Status = this.Status;
					applicant.CumulativeGPA = this.CumulativeGPA;
					applicant.CreditHours = this.CreditHours;
					applicant.IsEligible = this.IsEligible;
					applicant.IsVerified = this.IsVerified;

					List<Vote> newVotes = this.Votes.Where(v => applicant.Votes.Count(vi => vi.UserId == v.UserId) == 0).ToList();
					applicant.Votes.Clear();

					foreach (Vote vote in newVotes)
					{
						applicant.Votes.Add(vote);
					}

					cdx.SaveChanges();
					applicant._votes = null;
				}

				return applicant;
			}
		}

		private Applicant Insert()
		{
			using (ISmartScholarshipContext cdx = SmartScholarshipContext.Current)
			{
				Applicant applicant = cdx.Applicants.Create();

				if (applicant != null)
				{
					applicant.StudentNumber = this.StudentNumber;
					applicant.FirstName = this.FirstName;
					applicant.LastName = this.LastName;
					applicant.PhoneNumber = RegexUtility.UnFormatPhone(this.PhoneNumber);
					applicant.EmailAddress = this.EmailAddress;
					applicant.Gender = this.Gender;
					applicant.DateOfBirth = this.DateOfBirth;
					applicant.Status = this.Status;
					applicant.CumulativeGPA = this.CumulativeGPA;
					applicant.CreditHours = this.CreditHours;
					applicant.IsEligible = this.IsEligible;
					applicant.IsVerified = this.IsVerified;

					List<Vote> newVotes = this.Votes.Where(v => applicant.Votes.Count(vi => vi.UserId == v.UserId) == 0).ToList();
					applicant.Votes.Clear();

					foreach (Vote vote in newVotes)
					{
						applicant.Votes.Add(vote);
					}

					cdx.Applicants.Add(applicant);

					cdx.SaveChanges();
					applicant.IsNew = false;
					applicant._votes = null;
				}

				return applicant;
			}
		}

		#endregion

		public static void VoteForApplicant(string sn, String userId)
		{
			using (ISmartScholarshipContext sdx = SmartScholarshipContext.Current)
			{
				Vote vote = sdx.Votes.FirstOrDefault(v => v.StudentNumber.Equals(sn, StringComparison.OrdinalIgnoreCase) && v.UserId == userId);

				if (vote == null)
				{
					vote = new Vote() { StudentNumber = sn, UserId = userId };
					sdx.Votes.Add(vote);
				}

				sdx.SaveChanges();
			}
		}
	}
}