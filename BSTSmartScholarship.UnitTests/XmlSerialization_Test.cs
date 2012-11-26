namespace BSTSmartScholarship.UnitTests
{
	#region "Using Statements"

	using System;
	using System.IO;
	using System.Text;
	using System.Xml;
	using System.Xml.Serialization;
	using BSTSmartScholarship.Business;
	using BSTSmartScholarship.Business.Schemas;
	using Microsoft.VisualStudio.TestTools.UnitTesting;
	using System.Xml.Schema;

	#endregion

	[TestClass]
	public class XmlSerialization_Test
	{
		public Applicant TestApplicant
		{
			get
			{
				return Applicant.NewApplicant(
						"12345678",
						"Christopher",
						"Sims",
						"1232343456",
						"test@test.com",
						1,
						new DateTime(1982, 7, 13),
						4,
						3.81,
						12);
			}
		}

		public Student TestStudent
		{
			get
			{
				return new Student()
				{
					StudentNumber = "12345678",
					FirstName = "Christopher",
					LastName = "Sims",
					Gender = 1,
					DateOfBirth = new DateTime(1982, 7, 13),
					Status = 4,
					CumulativeGPA = 3.81,
					CreditHours = 12
				};
			}
		}

		[TestMethod]
		public void BSTSmartScholarship_Serialize_Applicant()
		{
			String expectedXmlString = "<?xml version=\"1.0\"?><Applicant xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns=\"http://bstsmartscholarship.sesmar.net/applicant\"><StudentNumber>12345678</StudentNumber><FirstName>Christopher</FirstName><LastName>Sims</LastName><PhoneNumber>(123) 234-3456</PhoneNumber><EmailAddress>test@test.com</EmailAddress><Gender>1</Gender><DateOfBirth>1982-07-13T00:00:00</DateOfBirth><Status>4</Status><CumulativeGPA>3.81</CumulativeGPA><CreditHours>12</CreditHours><IsEligible xsi:nil=\"true\" /><IsVerified xsi:nil=\"true\" /></Applicant>";
			XmlDocument expected = new XmlDocument();
			XmlDocument actual = (new BSTSmartScholarshipSerializer<Applicant>()).Serialize(this.TestApplicant);

			expected.LoadXml(expectedXmlString);

			Assert.AreEqual(expected.InnerXml, actual.InnerXml);
		}

		[TestMethod]
		public void Serialize_Applicant_ValidateSchema()
		{
			XmlDocument actual = (new BSTSmartScholarshipSerializer<Applicant>()).Serialize(this.TestApplicant);
			XmlSchemaProvider provider = new XmlSchemaProvider();

			actual.Schemas.Add(provider.GetSchemaFromResource("BSTSmartScholarship.Business.Schemas.Applicant.xsd"));
			actual.Validate(null);
		}

		[TestMethod]
		[ExpectedException(typeof(XmlSchemaValidationException))]
		public void ValidateSchema_Applicant_Invalid_StudentNumber()
		{
			Applicant applicant = this.TestApplicant;
			applicant.StudentNumber = "F";

			XmlDocument actual = (new BSTSmartScholarshipSerializer<Applicant>()).Serialize(applicant);
			XmlSchemaProvider provider = new XmlSchemaProvider();
			actual.Schemas.Add(provider.GetSchemaFromResource("BSTSmartScholarship.Business.Schemas.Applicant.xsd"));

			try
			{
				actual.Validate(null);
			}
			catch (XmlSchemaValidationException e)
			{
				Assert.AreEqual(@"The 'http://bstsmartscholarship.sesmar.net/applicant:StudentNumber' element is invalid - The value 'F' is invalid according to its datatype 'http://bstsmartscholarship.sesmar.net/applicant:StudentNumberType' - The Pattern constraint failed.", e.Message);
				throw e;
			}
		}

		[TestMethod]
		[ExpectedException(typeof(XmlSchemaValidationException))]
		public void ValidateSchema_Applicant_Invalid_EmailAddress()
		{
			Applicant applicant = this.TestApplicant;
			applicant.EmailAddress = "F";

			XmlDocument actual = (new BSTSmartScholarshipSerializer<Applicant>()).Serialize(applicant);
			XmlSchemaProvider provider = new XmlSchemaProvider();
			actual.Schemas.Add(provider.GetSchemaFromResource("BSTSmartScholarship.Business.Schemas.Applicant.xsd"));

			try
			{
				actual.Validate(null);
			}
			catch (XmlSchemaValidationException e)
			{
				Assert.AreEqual(@"The 'http://bstsmartscholarship.sesmar.net/applicant:EmailAddress' element is invalid - The value 'F' is invalid according to its datatype 'http://bstsmartscholarship.sesmar.net/applicant:EmailAddressType' - The Pattern constraint failed.", e.Message);
				throw e;
			}
		}

		[TestMethod]
		public void BSTSmartScholarship_Deserialize_Applicant()
		{
			String applicantXmlString = "<?xml version=\"1.0\"?><Applicant xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns=\"http://bstsmartscholarship.sesmar.net/applicant\"><StudentNumber>12345678</StudentNumber><FirstName>Christopher</FirstName><LastName>Sims</LastName><PhoneNumber>(123) 234-3456</PhoneNumber><EmailAddress>test@test.com</EmailAddress><Gender>1</Gender><DateOfBirth>1982-07-13T00:00:00</DateOfBirth><Status>4</Status><CumulativeGPA>3.81</CumulativeGPA><CreditHours>12</CreditHours><IsEligible xsi:nil=\"true\" /><IsVerified xsi:nil=\"true\" /></Applicant>";
			XmlDocument applicantXml = new XmlDocument();
			applicantXml.LoadXml(applicantXmlString);

			Applicant applicant = (new BSTSmartScholarshipSerializer<Applicant>()).Deserialize(applicantXml);

			Assert.AreEqual(applicant.FirstName, TestApplicant.FirstName);
		}

		[TestMethod]
		public void Serialize_Student_ValidateSchema()
		{
			XmlDocument actual = (new BSTSmartScholarshipSerializer<Student>()).Serialize(this.TestStudent);
			XmlSchemaProvider provider = new XmlSchemaProvider();

			actual.Schemas.Add(provider.GetSchemaFromResource("BSTSmartScholarship.Business.Schemas.Student.xsd"));
			actual.Validate(null);
		}

		[TestMethod]
		[ExpectedException(typeof(XmlSchemaValidationException))]
		public void Serialize_Student_ValidateSchema_Invalid_StudentNumber()
		{
			Student student = this.TestStudent;
			student.StudentNumber = "F";
			XmlDocument actual = (new BSTSmartScholarshipSerializer<Student>()).Serialize(student);
			XmlSchemaProvider provider = new XmlSchemaProvider();

			actual.Schemas.Add(provider.GetSchemaFromResource("BSTSmartScholarship.Business.Schemas.Student.xsd"));

			try
			{
				actual.Validate(null);
			}
			catch (XmlSchemaValidationException e)
			{
				Assert.AreEqual(@"The 'http://bstsmartscholarship.sesmar.net/student:StudentNumber' element is invalid - The value 'F' is invalid according to its datatype 'http://bstsmartscholarship.sesmar.net/student:StudentNumberType' - The Pattern constraint failed.", e.Message);
				throw e;
			}
		}

		[TestMethod]
		public void Serialize_TuitionAmountRequest_ValidateSchema()
		{
			TuitionAmountRequest request = new TuitionAmountRequest() { StudentNumber = "88634351" };
			XmlDocument actual = (new BSTSmartScholarshipSerializer<TuitionAmountRequest>()).Serialize(request);
			XmlSchemaProvider provider = new XmlSchemaProvider();

			actual.Schemas.Add(provider.GetSchemaFromResource("BSTSmartScholarship.Business.Schemas.TuitionAmountRequest.xsd"));
			actual.Validate(null);
		}

		[TestMethod]
		[ExpectedException(typeof(XmlSchemaValidationException))]
		public void Serialize_TuitionAmountRequest_ValidateSchema_Invalid_StudentNumber()
		{
			TuitionAmountRequest request = new TuitionAmountRequest() { StudentNumber = "88634351" };
			request.StudentNumber = "F";

			XmlDocument actual = (new BSTSmartScholarshipSerializer<TuitionAmountRequest>()).Serialize(request);
			XmlSchemaProvider provider = new XmlSchemaProvider();

			actual.Schemas.Add(provider.GetSchemaFromResource("BSTSmartScholarship.Business.Schemas.TuitionAmountRequest.xsd"));
			actual.Validate(null);
		}

		[TestMethod]
		public void Serialize_TuitionAmountResponse_ValidateSchema()
		{
			TuitionAmountResponse response = new TuitionAmountResponse() { StudentNumber = "88634351", TuitionAmount = 4500.00 };
			XmlDocument actual = (new BSTSmartScholarshipSerializer<TuitionAmountResponse>()).Serialize(response);
			XmlSchemaProvider provider = new XmlSchemaProvider();

			actual.Schemas.Add(provider.GetSchemaFromResource("BSTSmartScholarship.Business.Schemas.TuitionAmountResponse.xsd"));
			actual.Validate(null);
		}

		[TestMethod]
		[ExpectedException(typeof(XmlSchemaValidationException))]
		public void Serialize_TuitionAmountResponse_ValidateSchema_Invalid_StudentNumber()
		{
			TuitionAmountResponse response = new TuitionAmountResponse() { StudentNumber = "88634351", TuitionAmount = 4500.00 };
			response.StudentNumber = "F";

			XmlDocument actual = (new BSTSmartScholarshipSerializer<TuitionAmountResponse>()).Serialize(response);
			XmlSchemaProvider provider = new XmlSchemaProvider();

			actual.Schemas.Add(provider.GetSchemaFromResource("BSTSmartScholarship.Business.Schemas.TuitionAmountResponse.xsd"));
			actual.Validate(null);
		}
	}
}
