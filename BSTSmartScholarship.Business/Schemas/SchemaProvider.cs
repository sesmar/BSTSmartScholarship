namespace BSTSmartScholarship.Business.Schemas
{
	#region "Using Statements"

	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Linq;
	using System.Reflection;
	using System.Text;
	using System.Threading.Tasks;
	using System.Xml.Schema;

	#endregion

	public class XmlSchemaProvider : ISchemaProvider
	{
		public XmlSchema GetSchemaFromResource(String schemaName)
		{
			Assembly assembly = this.GetType().Assembly;

			using (Stream stream = assembly.GetManifestResourceStream(schemaName))
			{
				return XmlSchema.Read(stream, null);
			}
		}
	}
}
