namespace BSTSmartScholarship.Business
{
	#region "Using Statements"

	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;

	#endregion

	public class AwardScholarshipHandler
	{
		protected AwardScholarshipHandler _successor;

		public AwardScholarshipHandler(AwardScholarshipHandler successor)
		{
			_successor = successor;
		}

		public virtual List<Applicant> AwardScholarship(List<Applicant> applicants)
		{
			if (applicants.Count > 1 && _successor != null)
			{
				applicants = _successor.AwardScholarship(applicants);
			}

			return applicants;
		}
	}

	public class AwardScholarshipCumulativeGPAHandler : AwardScholarshipHandler
	{
		public AwardScholarshipCumulativeGPAHandler(AwardScholarshipHandler successor) : base(successor) { }

		public override List<Applicant> AwardScholarship(List<Applicant> applicants)
		{
			applicants = applicants.Where(a => a.CumulativeGPA == applicants.Max(ap => ap.CumulativeGPA)).ToList();

			if (applicants.Count > 1 && _successor != null)
			{
				applicants = _successor.AwardScholarship(applicants);
			}

			return applicants;
		}
	}

	public class AwardScholarshipCurrentGPAHandler : AwardScholarshipHandler
	{
		public AwardScholarshipCurrentGPAHandler(AwardScholarshipHandler successor) : base(successor) { }

		public override List<Applicant> AwardScholarship(List<Applicant> applicants)
		{
			List<String> studentNumbers = (from a in applicants select a.StudentNumber).ToList();

			Double maxGPA = Student.StudentList.Where(s => studentNumbers.Contains(s.StudentNumber)).Max(s => s.CurrentGPA);
			studentNumbers = (from s in Student.StudentList where s.CurrentGPA == maxGPA select s.StudentNumber).ToList();
			applicants = applicants.Where(a => studentNumbers.Contains(a.StudentNumber)).ToList();

			if (applicants.Count > 1 && _successor != null)
			{
				applicants = _successor.AwardScholarship(applicants);
			}

			return applicants;
		}
	}

	public class AwardScholarshipStatusHandler : AwardScholarshipHandler
	{
		public AwardScholarshipStatusHandler(AwardScholarshipHandler successor) : base(successor) { }

		public override List<Applicant> AwardScholarship(List<Applicant> applicants)
		{
			if (applicants.Where(a => a.Status == (Int32)Status.Junior).Count() > 0)
			{
				applicants = applicants.Where(a => a.Status == (Int32)Status.Junior).ToList();
			}

			if (applicants.Count > 1 && _successor != null)
			{
				applicants = _successor.AwardScholarship(applicants);
			}

			return applicants;
		}
	}

	public class AwardScholarshipGenderHandler : AwardScholarshipHandler
	{
		public AwardScholarshipGenderHandler(AwardScholarshipHandler successor) : base(successor) { }

		public override List<Applicant> AwardScholarship(List<Applicant> applicants)
		{
			if (applicants.Where(a => a.Gender == (Int32)Gender.Female).Count() > 0)
			{
				applicants = applicants.Where(a => a.Gender == (Int32)Gender.Female).ToList();
			}

			if (applicants.Count > 1 && _successor != null)
			{
				applicants = _successor.AwardScholarship(applicants);
			}

			return applicants;
		}
	}

	public class AwardScholarshipAgeHandler : AwardScholarshipHandler
	{
		public AwardScholarshipAgeHandler(AwardScholarshipHandler successor) : base(successor) { }

		public override List<Applicant> AwardScholarship(List<Applicant> applicants)
		{
			if (applicants.Count > 2)
			{
				List<Applicant> twoYoungest = new List<Applicant>();
				Applicant youngest = GetYongest(applicants);

				applicants.Remove(youngest);
				twoYoungest.Add(youngest);

				//Repeat to get second youngest.
				youngest = GetYongest(applicants);

				applicants.Remove(youngest);
				twoYoungest.Add(youngest);

				applicants = twoYoungest;
			}

			return applicants;
		}

		private Applicant GetYongest(List<Applicant> applicants)
		{
			Applicant youngest;

			DateTime minDate = applicants.Max(a => a.DateOfBirth);
			youngest = applicants.Where(a => a.DateOfBirth.Equals(minDate)).FirstOrDefault();

			return youngest;
		}
	}
}
