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
		
		public XmlDocument VerifyApplicant(XmlDocument applicantDoc)
		{
			BSTSmartScholarshipSerializer<Applicant> applicantSerializer = new BSTSmartScholarshipSerializer<Applicant>();
			Applicant applicant = applicantSerializer.Deserialize(applicantDoc);

			Student student = Student.GetStudent(applicant.StudentNumber);
			BSTSmartScholarshipSerializer<Student> studentSerializer = new BSTSmartScholarshipSerializer<Student>();
			XmlDocument studentDoc = studentSerializer.Serialize(student);

			return studentDoc;
		}
	}
}
