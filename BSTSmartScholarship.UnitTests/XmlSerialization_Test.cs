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
						"1234567890",
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

		[TestMethod]
		public void BSTSmartScholarship_Serialize_Applicant()
		{
			String expectedXmlString = "<?xml version=\"1.0\"?><Applicant xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns=\"http://bstsmartscholarship.sesmar.net/applicant\"><StudentNumber>1234567890</StudentNumber><FirstName>Christopher</FirstName><LastName>Sims</LastName><PhoneNumber>(123) 234-3456</PhoneNumber><EmailAddress>test@test.com</EmailAddress><Gender>1</Gender><DateOfBirth>1982-07-13T00:00:00</DateOfBirth><Status>4</Status><CumulativeGPA>3.81</CumulativeGPA><CreditHours>12</CreditHours><IsEligible xsi:nil=\"true\" /></Applicant>";
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
			String applicantXmlString = "<?xml version=\"1.0\"?><Applicant xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns=\"http://bstsmartscholarship.sesmar.net/applicant\"><StudentNumber>1234567890</StudentNumber><FirstName>Christopher</FirstName><LastName>Sims</LastName><PhoneNumber>(123) 234-3456</PhoneNumber><EmailAddress>test@test.com</EmailAddress><Gender>1</Gender><DateOfBirth>1982-07-13T00:00:00</DateOfBirth><Status>4</Status><CumulativeGPA>3.81</CumulativeGPA><CreditHours>12</CreditHours><IsEligible xsi:nil=\"true\" /></Applicant>";
			XmlDocument applicantXml = new XmlDocument();
			applicantXml.LoadXml(applicantXmlString);

			Applicant applicant = (new BSTSmartScholarshipSerializer<Applicant>()).Deserialize(applicantXml);

			Assert.AreEqual(applicant.FirstName, TestApplicant.FirstName);
		}
	}
}
