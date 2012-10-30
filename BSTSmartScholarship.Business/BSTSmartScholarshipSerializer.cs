namespace BSTSmartScholarship.Business
{
	#region "Using Statements"

	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;
	using System.Xml;
	using System.Xml.Serialization;

	#endregion

	public class BSTSmartScholarshipSerializer<T> where T : new()
	{
		public XmlDocument Serialize(T serializable)
		{
			XmlDocument applicantXml = new XmlDocument();
			XmlSerializer serializer = new XmlSerializer(typeof(T));
			MemoryStream stream = new MemoryStream();

			serializer.Serialize(stream, serializable);
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

			XmlSerializer serializer = new XmlSerializer(typeof(T));
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
