﻿namespace BSTSmartScholarship.Business
{
	#region "Using Statements"

	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;
	using BSTSmartScholarship.Business.AccountingService;

	#endregion

	public class Award
	{
		[Key]
		public String StudentNumber { get; set; }
		public String FirstName { get; set; }
		public String LastName { get; set; }
		public Double AwardAmount { get; set; }

		#region "Factory Methods"

		public static Award GetAward(String studentNumber)
		{
			Award award = null;

			using (ISmartScholarshipContext sdx = SmartScholarshipContext.Current)
			{
				award = sdx.Awards.FirstOrDefault(a => a.StudentNumber.Equals(studentNumber, StringComparison.OrdinalIgnoreCase));
			}

			return award;
		}

		public static Applicant GetAwarded(String year, String semester)
		{
			Award award = null;
			Applicant applicant = null;

			using (ISmartScholarshipContext sdx = SmartScholarshipContext.Current)
			{
				award = sdx.Awards.FirstOrDefault();

				if (award != null)
				{
					applicant = Applicant.GetApplicant(award.StudentNumber);
				}
			}

			return applicant;
		}

		public static Award AddAward(String studentNumber, String firstName, String lastName, Double awardAmount)
		{
			Award award = null;

			using (ISmartScholarshipContext sdx = SmartScholarshipContext.Current)
			{
				award = sdx.Awards.FirstOrDefault(a => a.StudentNumber.Equals(studentNumber, StringComparison.OrdinalIgnoreCase));

				if (award == null)
				{
					award = new Award();
					sdx.Awards.Add(award);
				}

				award.StudentNumber = studentNumber;
				award.FirstName = firstName;
				award.LastName = lastName;
				award.AwardAmount = awardAmount;

				sdx.SaveChanges();
			}

			return award;
		}

		#endregion

		public static Boolean Awarded(String year, String semester)
		{
			using (ISmartScholarshipContext sdx = SmartScholarshipContext.Current)
			{
				return (sdx.Awards.Count() > 0);
			}
		}

		public static String NotifyAccounting(String studentNumber, Double amount)
		{
			AccountingSoapClient client = new AccountingSoapClient();
			String response = client.RequestRefund(studentNumber, amount);

			return response;
		}
	}
}
