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
		public void Serialize_Applicant_ToStream()
		{
			String expectedXmlString = "<?xml version=\"1.0\"?><Applicant xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"><StudentNumber>1234567890</StudentNumber><FirstName>Christopher</FirstName><LastName>Sims</LastName><PhoneNumber>(123) 234-3456</PhoneNumber><EmailAddress>test@test.com</EmailAddress><Gender>1</Gender><DateOfBirth>1982-07-13T00:00:00</DateOfBirth><Status>4</Status><CumulativeGPA>3.81</CumulativeGPA><CreditHours>12</CreditHours><IsEligible xsi:nil=\"true\" /></Applicant>";
			XmlDocument expected = new XmlDocument();
			XmlDocument actual;
			XmlSerializer serializar = new XmlSerializer(typeof(Applicant));
			Applicant applicant = this.TestApplicant;
			MemoryStream stream = new MemoryStream();

			expected.LoadXml(expectedXmlString);

			serializar.Serialize(stream, applicant);
			stream.Seek(0, SeekOrigin.Begin);
			StreamReader reader = new StreamReader(stream);
			String contents = reader.ReadToEnd();
			actual = new XmlDocument();
			actual.LoadXml(contents);

			Assert.AreEqual(expected.InnerXml, actual.InnerXml);
		}

		[TestMethod]
		public void Serialize_Applicant_ValidateSchema()
		{
			XmlDocument actual = new XmlDocument();
			XmlSerializer serializar = new XmlSerializer(typeof(Applicant));
			Applicant applicant = this.TestApplicant;
			MemoryStream stream = new MemoryStream();

			serializar.Serialize(stream, applicant);
			stream.Seek(0, SeekOrigin.Begin);
			StreamReader reader = new StreamReader(stream);
			String contents = reader.ReadToEnd();

			actual.LoadXml(contents);

			XmlSchemaProvider provider = new XmlSchemaProvider();
			
			actual.Schemas.Add(provider.GetSchemaFromResource("BSTSmartScholarship.Business.Schemas.Applicant.xsd"));
			actual.Validate(null);
		}
	}
}
