namespace BSTSmartScholarship.Business
{
	#region "Using Statements"

	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;

	#endregion

	public class EmailUtility
	{
		private IEmailService _emailService;

		public EmailUtility(IEmailService emailService)
		{
			_emailService = emailService;
		}

		public void SendDeclinedEmail(String sn)
		{
			Applicant applicant = Applicant.GetApplicant(sn);

			SendEmail(applicant.EmailAddress, "BST Smart Scholarship, Declined Notification.", "You have been declined due to mismatching information from your application with the registrar's office");
		}

		public void SendIneligibleEmail(String sn)
		{
			Applicant applicant = Applicant.GetApplicant(sn);

			SendEmail(applicant.EmailAddress, "BST Smart Scholarship, Ineligible Notification.", "We are sorry to inform you that you are not eligible for the scholarship.");
		}

		public void SendAwardedEmail(String sn, Double amount)
		{
			Applicant applicant = Applicant.GetApplicant(sn);
			String message = String.Format(
				"Congratulation, you are this years winner of the BST Smart Scholarship. You will receive a reimbursement in the amount of {0:C}",
				amount);
			SendEmail(applicant.EmailAddress, "BST Smart Scholarship, Awarded Notification.", message);
		}

		public void SendNotAwardedEmail(String sn)
		{
			Applicant applicant = Applicant.GetApplicant(sn);

			SendEmail(applicant.EmailAddress, "BST Smart Scholarship, Better Luck Next Time Notification.", "We regret to inform you that you were not awarded the BST Smart Scholarship this year, we hope you will apply again next year.");
		}

		public void SendEmail(String to, String subject, String body)
		{
			_emailService.SendEmail(to, subject, body);
		}
	}
}
