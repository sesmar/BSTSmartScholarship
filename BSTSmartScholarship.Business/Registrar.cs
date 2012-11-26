namespace BSTSmartScholarship.Business
{
	#region "Using Statements"

	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;
	using System.Xml;

	#endregion

	public class Registrar
	{
		internal class TuitionAmount
		{
			public String StudentNumber { get; set; }
			public Double Amount { get; set; }
		}

		internal List<TuitionAmount> TuitionAmounts = new List<TuitionAmount>()
		{
			new TuitionAmount() { StudentNumber = "88634351", Amount = 4500.00 },
			new TuitionAmount() { StudentNumber = "88634352", Amount = 5500.00 },
			new TuitionAmount() { StudentNumber = "88634353", Amount = 4561.00 },
			new TuitionAmount() { StudentNumber = "88634354", Amount = 8000.00 },
			new TuitionAmount() { StudentNumber = "88634355", Amount = 10000.00 },
			new TuitionAmount() { StudentNumber = "88634356", Amount = 9500.00 },
			new TuitionAmount() { StudentNumber = "88634357", Amount = 3500.00 },
		};

		public XmlDocument VerifyApplicant(XmlDocument applicantDoc)
		{
			BSTSmartScholarshipSerializer<Applicant> applicantSerializer = new BSTSmartScholarshipSerializer<Applicant>();
			Applicant applicant = applicantSerializer.Deserialize(applicantDoc);

			Student student = Student.GetStudent(applicant.StudentNumber);
			BSTSmartScholarshipSerializer<Student> studentSerializer = new BSTSmartScholarshipSerializer<Student>();
			XmlDocument studentDoc = studentSerializer.Serialize(student);

			return studentDoc;
		}

		public XmlDocument RequestTuitionAmount(XmlDocument request)
		{
			XmlDocument response = new XmlDocument();
			TuitionAmountRequest tuitionRequest = (new BSTSmartScholarshipSerializer<TuitionAmountRequest>()).Deserialize(request);

			TuitionAmount amount = TuitionAmounts.FirstOrDefault(ta => ta.StudentNumber.Equals(tuitionRequest.StudentNumber, StringComparison.OrdinalIgnoreCase));
			TuitionAmountResponse tuitionResponse = new TuitionAmountResponse() { StudentNumber = amount.StudentNumber, TuitionAmount = amount.Amount };

			response = (new BSTSmartScholarshipSerializer<TuitionAmountResponse>()).Serialize(tuitionResponse);

			return response;
		}
	}

	public class TuitionAmountRequest
	{
		public String StudentNumber { get; set; }

		public TuitionAmountRequest() { }

		public TuitionAmountRequest(String sn)
		{
			this.StudentNumber = sn;
		}

		public override string ToString()
		{
			return "TuitionAmountRequest";
		}
	}

	public class TuitionAmountResponse
	{
		public String StudentNumber { get; set; }
		public Double TuitionAmount { get; set; }

		public TuitionAmountResponse() { }

		public TuitionAmountResponse(String sn, Double amount)
		{
			this.StudentNumber = sn;
			this.TuitionAmount = amount;
		}

		public override string ToString()
		{
			return "TuitionAmountResponse";
		}
	}
}
