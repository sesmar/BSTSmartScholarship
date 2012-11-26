using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BSTSmartScholarship.Business;
using System.Xml;
using BSTSmartScholarship.Business.Schemas;

namespace BSTSmartScholarship.UnitTests
{
	/// <summary>
	/// Summary description for Registrar_Tests
	/// </summary>
	[TestClass]
	public class Registrar_Tests
	{
		public Applicant TestApplicant
		{
			get
			{
				return Applicant.NewApplicant(
						"88634352",
						"Christopher",
						"Sims",
						"1232343456",
						"test@test.com",
						1,
						new DateTime(1982, 7, 13),
						4,
						3.7,
						6);
			}
		}

		public Registrar_Tests()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		private TestContext testContextInstance;

		/// <summary>
		///Gets or sets the test context which provides
		///information about and functionality for the current test run.
		///</summary>
		public TestContext TestContext
		{
			get
			{
				return testContextInstance;
			}
			set
			{
				testContextInstance = value;
			}
		}

		#region Additional test attributes
		//
		// You can use the following additional attributes as you write your tests:
		//
		// Use ClassInitialize to run code before running the first test in the class
		// [ClassInitialize()]
		// public static void MyClassInitialize(TestContext testContext) { }
		//
		// Use ClassCleanup to run code after all tests in a class have run
		// [ClassCleanup()]
		// public static void MyClassCleanup() { }
		//
		// Use TestInitialize to run code before running each test 
		// [TestInitialize()]
		// public void MyTestInitialize() { }
		//
		// Use TestCleanup to run code after each test has run
		// [TestCleanup()]
		// public void MyTestCleanup() { }
		//
		#endregion

		[TestMethod]
		public void Registrar_VerifyApplicant_Valid_StudentNumber()
		{
			Registrar registrar = new Registrar();
			BSTSmartScholarshipSerializer<Applicant> serializer = new BSTSmartScholarshipSerializer<Applicant>();
			XmlDocument applicantDoc = serializer.Serialize(this.TestApplicant);
			ISchemaProvider provider = new XmlSchemaProvider();
			applicantDoc.Schemas.Add(provider.GetSchemaFromResource("BSTSmartScholarship.Business.Schemas.Applicant.xsd"));
			applicantDoc.Validate(null);

			XmlDocument studentDoc = registrar.VerifyApplicant(applicantDoc);
			studentDoc.Schemas.Add(provider.GetSchemaFromResource("BSTSmartScholarship.Business.Schemas.Student.xsd"));
			studentDoc.Validate(null);

			BSTSmartScholarshipSerializer<Student> deserializer = new BSTSmartScholarshipSerializer<Student>();
			Student student = deserializer.Deserialize(studentDoc);

			Assert.AreEqual(student.StudentNumber, this.TestApplicant.StudentNumber);
		}

		[TestMethod]
		public void Registrar_RequestTutionAmount_Valid_StudentNumber()
		{
			Registrar registrar = new Registrar();
			TuitionAmountRequest request = new TuitionAmountRequest() { StudentNumber = "88634352" };
			BSTSmartScholarshipSerializer<TuitionAmountRequest> serializer = new BSTSmartScholarshipSerializer<TuitionAmountRequest>();
			XmlDocument requestDoc = serializer.Serialize(request);
			ISchemaProvider provider = new XmlSchemaProvider();
			requestDoc.Schemas.Add(provider.GetSchemaFromResource("BSTSmartScholarship.Business.Schemas.TuitionAmountRequest.xsd"));
			requestDoc.Validate(null);

			XmlDocument responseDoc = registrar.RequestTuitionAmount(requestDoc);
			responseDoc.Schemas.Add(provider.GetSchemaFromResource("BSTSmartScholarship.Business.Schemas.TuitionAmountResponse.xsd"));
			responseDoc.Validate(null);

			BSTSmartScholarshipSerializer<TuitionAmountResponse> deserilaizer = new BSTSmartScholarshipSerializer<TuitionAmountResponse>();
			TuitionAmountResponse response = deserilaizer.Deserialize(responseDoc);

			Assert.AreEqual(response.StudentNumber, request.StudentNumber);
			Assert.AreEqual(response.TuitionAmount, 5500.00);
		}
	}
}
