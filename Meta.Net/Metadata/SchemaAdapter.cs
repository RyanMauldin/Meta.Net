using System.Data;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;
using Meta.Net.Interfaces;
using Meta.Net.Metadata.Factories;
using Meta.Net.Objects;

namespace Meta.Net.Metadata
{
    public static class SchemaAdapter
    {
        public static void Read(
            Catalog catalog,
            IDataReader reader)
        {
            var factory = new SchemaFactory(reader);

            while (reader.Read())
                factory.CreateSchema(catalog, reader);
        }

        public static async Task ReadAsync(
            Catalog catalog,
            DbDataReader reader,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            var factory = new SchemaFactory(reader);

            while (await reader.ReadAsync(cancellationToken))
            {
                cancellationToken.ThrowIfCancellationRequested();
                factory.CreateSchema(catalog, reader);
            }
        }

        public static void Get(
            Catalog catalog,
            DbConnection connection,
            IMetadataScriptFactory metadataScriptFactory)
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

        public static async Task GetAsync(
            Catalog catalog,
            DbConnection connection,
            IMetadataScriptFactory metadataScriptFactory,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
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

                    await ReadAsync(catalog, reader, cancellationToken);

                    reader.Close();
                }
            }
        }

        public static void Build(
            Catalog catalog,
            DbConnection connection,
            IMetadataScriptFactory metadataScriptFactory)
        {
            Get(catalog, connection, metadataScriptFactory);
            if (catalog.Schemas.Count == 0)
                return;

            UserDefinedDataTypeAdapter.Get(catalog, connection, metadataScriptFactory);
            UserTableAdapter.Build(catalog, connection, metadataScriptFactory);
            ModuleAdapter.Get(catalog, connection, metadataScriptFactory);
        }

        public static async Task BuildAsync(
            Catalog catalog,
            DbConnection connection,
            IMetadataScriptFactory metadataScriptFactory,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            await GetAsync(catalog, connection, metadataScriptFactory, cancellationToken);
            if (catalog.Schemas.Count == 0)
                return;

            await UserDefinedDataTypeAdapter.GetAsync(catalog, connection, metadataScriptFactory, cancellationToken);
            await UserTableAdapter.BuildAsync(catalog, connection, metadataScriptFactory, cancellationToken);
            await ModuleAdapter.GetAsync(catalog, connection, metadataScriptFactory, cancellationToken);
        }
    }
}
