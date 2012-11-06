namespace BSTSmartScholarship.Business
{
	#region "Using Statements"

	using BSTSmartScholarship.Business.Schemas;
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;
	using System.Xml;
	using System.Xml.Schema;
	using System.Xml.Serialization;

	#endregion

	public class BSTSmartScholarshipSerializer<T> where T : new()
	{
		public XmlDocument Serialize(T serializable)
		{
			XmlDocument applicantXml = new XmlDocument();
			XmlSerializerNamespaces xmlns = new XmlSerializerNamespaces();
			xmlns.Add(string.Empty, "http://bstsmartscholarship.sesmar.net/applicant");
			xmlns.Add("xsi", "http://www.w3.org/2001/XMLSchema-instance");

			XmlSerializer serializer = new XmlSerializer(typeof(T), "http://bstsmartscholarship.sesmar.net/applicant");
			MemoryStream stream = new MemoryStream();

			serializer.Serialize(stream, serializable, xmlns);
			stream.Seek(0, SeekOrigin.Begin);

			using (StreamReader reader = new StreamReader(stream))
			{
				String contents = reader.ReadToEnd();
				applicantXml.LoadXml(contents);
			}
			
			return applicantXml;
		}

		public T Deserialize(XmlDocument deserializable)
		{
			T deserialized = new T();
			
			XmlSerializer serializer = new XmlSerializer(typeof(T), "http://bstsmartscholarship.sesmar.net/applicant");
			MemoryStream stream = new MemoryStream();
			
			using (StreamWriter writer = new StreamWriter(stream))
			{
				writer.Write(deserializable.InnerXml);
				writer.Flush();
				stream.Seek(0, SeekOrigin.Begin);
				deserialized = (T)serializer.Deserialize(stream);
			}

			return deserialized;
		}
	}
}
