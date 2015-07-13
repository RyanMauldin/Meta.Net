using System;
using System.Data;
using Meta.Net.Objects;

namespace Meta.Net.Metadata.Factories
{
    internal class SchemaFactory
    {
        private int ObjectNameOrdinal { get; set; }

        public SchemaFactory(IDataRecord reader)
        {
            ObjectNameOrdinal = reader.GetOrdinal("ObjectName");
        }

        public void CreateSchema(
            Catalog catalog,
            IDataRecord reader)
        {
            var schema = new Schema
            {
                Catalog = catalog,
                ObjectName = Convert.ToString(reader[ObjectNameOrdinal])
            };

            catalog.Schemas.Add(schema);
        }
    }
}
