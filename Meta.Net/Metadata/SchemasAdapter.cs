using System;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
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

        public static async Task GetAsync(Catalog catalog, DbConnection connection, IMetadataScriptFactory metadataScriptFactory)
        {
            using (var command = connection.CreateCommand())
            {
                command.CommandText = metadataScriptFactory.Schemas(catalog.ObjectName);
                using (var reader = await command.ExecuteReaderAsync())
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

        public static async Task BuildAsync(Catalog catalog, DbConnection connection, IMetadataScriptFactory metadataScriptFactory)
        {
            await GetAsync(catalog, connection, metadataScriptFactory);
            if (catalog.Schemas.Count == 0)
                return;

            await UserDefinedDataTypesAdapter.GetAsync(catalog, connection, metadataScriptFactory);
            await UserTablesAdapter.BuildAsync(catalog, connection, metadataScriptFactory);
            await ModulesAdapter.GetAsync(catalog, connection, metadataScriptFactory);
        }
    }
}
