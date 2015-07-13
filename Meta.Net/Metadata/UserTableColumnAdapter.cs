using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;
using Meta.Net.Interfaces;
using Meta.Net.Metadata.Factories;
using Meta.Net.Objects;

namespace Meta.Net.Metadata
{
    public static class UserTableColumnAdapter
    {
        public static void Read(
            Dictionary<string, UserTable> userTables,
            IDataReader reader)
        {
            var factory = new UserTableColumnFactory(reader);

            while (reader.Read())
                factory.CreateUserTableColumn(userTables, reader);
        }
        
        public static async Task ReadAsync(
            Dictionary<string, UserTable> userTables,
            DbDataReader reader,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            var factory = new UserTableColumnFactory(reader);

            while (await reader.ReadAsync(cancellationToken))
            {
                cancellationToken.ThrowIfCancellationRequested();
                factory.CreateUserTableColumn(userTables, reader);
            }
        }

        public static void Get(
            Catalog catalog,
            Dictionary<string, UserTable> userTables,
            DbConnection connection,
            IMetadataScriptFactory metadataScriptFactory)
        {
            using (var command = connection.CreateCommand())
            {
                command.CommandText = metadataScriptFactory.UserTableColumns(catalog.ObjectName);
                using (var reader = command.ExecuteReader())
                {
                    if (!reader.HasRows)
                    {
                        reader.Close();
                        return;
                    }

                    Read(userTables, reader);

                    reader.Close();
                }
            }
        }

        public static async Task GetAsync(
            Catalog catalog,
            Dictionary<string, UserTable> userTables,
            DbConnection connection,
            IMetadataScriptFactory metadataScriptFactory,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = metadataScriptFactory.UserTableColumns(catalog.ObjectName);
                using (var reader = await command.ExecuteReaderAsync(cancellationToken))
                {
                    if (!reader.HasRows)
                    {
                        reader.Close();
                        return;
                    }

                    await ReadAsync(userTables, reader, cancellationToken);

                    reader.Close();
                }
            }
        }
    }
}
