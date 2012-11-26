namespace BSTSmartScholarship.Business
{
	#region "Using Statements"

	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;

	#endregion

	public class Award
	{
		[Key]
		public String StudentNumber { get; set; }
		public String FirstName { get; set; }
		public String LastName { get; set; }

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

		public static Award AddAward(String studentNumber, String firstName, String lastName)
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

				sdx.SaveChanges();
			}

			return award;
		}

		#endregion

	}
}
