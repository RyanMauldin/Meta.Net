using System;
using System.Data;
using System.Data.Common;
using Meta.Net.Interfaces;
using Meta.Net.Objects;

namespace Meta.Net.Metadata
{
    public static class SchemasAdapter
    {
        public static void Read(Catalog catalog, IDataReader reader)
        {
            var objectNameOrdinal = reader.GetOrdinal("ObjectName");

            while (reader.Read())
            {
                var objectName = Convert.ToString(reader[objectNameOrdinal]);
                var schema = new Schema
                {
                    Catalog = catalog,
                    ObjectName = objectName
                };

                catalog.Schemas.Add(schema);
            }
        }

        public static void Get(Catalog catalog, DbConnection connection, IMetadataScriptFactory metadataScriptFactory)
        {
            using (var command = connection.CreateCommand())
            {
                command.CommandText = metadataScriptFactory.Schemas(catalog.ObjectName);
                using (var reader = command.ExecuteReader())
                {
                    if (!reader.HasRows)
                    {
                        reader.Close();
                        return;
                    }

                    Read(catalog, reader);

                    reader.Close();
                }
            }
        }

        public static void Build(Catalog catalog, DbConnection connection, IMetadataScriptFactory metadataScriptFactory)
        {
            Get(catalog, connection, metadataScriptFactory);
            if (catalog.Schemas.Count == 0)
                return;

            UserDefinedDataTypesAdapter.Get(catalog, connection, metadataScriptFactory);
            UserTablesAdapter.Build(catalog, connection, metadataScriptFactory);
            ModulesAdapter.Get(catalog, connection, metadataScriptFactory);
        }
    }
}
