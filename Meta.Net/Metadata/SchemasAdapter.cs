using System;
using System.Data;
using System.Data.Common;
using System.Threading;
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
        
        public static async Task GetAsync(Catalog catalog, DbConnection connection, IMetadataScriptFactory metadataScriptFactory, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return;

            using (var command = connection.CreateCommand())
            {
                command.CommandText = metadataScriptFactory.Schemas(catalog.ObjectName);
                using (var reader = await command.ExecuteReaderAsync(cancellationToken))
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
        
        public static async Task BuildAsync(Catalog catalog, DbConnection connection, IMetadataScriptFactory metadataScriptFactory)
        {
            await GetAsync(catalog, connection, metadataScriptFactory);
            if (catalog.Schemas.Count == 0)
                return;

            await UserDefinedDataTypesAdapter.GetAsync(catalog, connection, metadataScriptFactory);
            await UserTablesAdapter.BuildAsync(catalog, connection, metadataScriptFactory);
            await ModulesAdapter.GetAsync(catalog, connection, metadataScriptFactory);
        }
        
        public static async Task BuildAsync(Catalog catalog, DbConnection connection, IMetadataScriptFactory metadataScriptFactory, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return;

            await GetAsync(catalog, connection, metadataScriptFactory, cancellationToken);
            if (catalog.Schemas.Count == 0)
                return;

            if (cancellationToken.IsCancellationRequested)
                return;

            await UserDefinedDataTypesAdapter.GetAsync(catalog, connection, metadataScriptFactory, cancellationToken);
            if (cancellationToken.IsCancellationRequested)
                return;

            await UserTablesAdapter.BuildAsync(catalog, connection, metadataScriptFactory, cancellationToken);
            if (cancellationToken.IsCancellationRequested)
                return;

            await ModulesAdapter.GetAsync(catalog, connection, metadataScriptFactory, cancellationToken);
        }
    }
}
