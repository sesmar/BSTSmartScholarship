namespace BSTSmartScholarship.Business.Schemas
{
    #region "Using Statements"

    using System;
    using System.Xml.Schema;

    #endregion

    public interface ISchemaProvider
    {
        XmlSchema GetSchemaFromResource(String schemaName);
    }
}
