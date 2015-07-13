using System.Data;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;
using Meta.Net.Interfaces;
using Meta.Net.Metadata.Factories;
using Meta.Net.Objects;

namespace Meta.Net.Metadata
{
    public class UserDefinedDataTypeAdapter
    {
        public static void Read(
            Catalog catalog,
            IDataReader reader)
        {
            var factory = new UserDefinedDataTypeFactory(reader);

            while (reader.Read())
                factory.CreateUserDefinedDataType(catalog, reader);
        }
        
        public static async Task ReadAsync(
            Catalog catalog,
            DbDataReader reader,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            var factory = new UserDefinedDataTypeFactory(reader);

            while (await reader.ReadAsync(cancellationToken))
            {
                cancellationToken.ThrowIfCancellationRequested();
                factory.CreateUserDefinedDataType(catalog, reader);
            }
        }

        public static void Get(
            Catalog catalog,
            DbConnection connection,
            IMetadataScriptFactory metadataScriptFactory)
        {
            using (var command = connection.CreateCommand())
            {
                command.CommandText = metadataScriptFactory.UserDefinedDataTypes(catalog.ObjectName);
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
                command.CommandText = metadataScriptFactory.UserDefinedDataTypes(catalog.ObjectName);
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
    }
}
