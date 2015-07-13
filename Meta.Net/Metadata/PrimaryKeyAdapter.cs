using System;
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
    public static class PrimaryKeyAdapter
    {
        public static void Read(
            Dictionary<string, UserTable> userTables,
            Dictionary<string, PrimaryKeyColumn> primaryKeyColumns,
            IDataReader reader)
        {
            var factory = new PrimaryKeyFactory(reader);

            while (reader.Read())
                factory.CreatePrimaryKey(userTables, primaryKeyColumns, reader);
        }
        
        public static async Task ReadAsync(
            Dictionary<string, UserTable> userTables,
            Dictionary<string, PrimaryKeyColumn> primaryKeyColumns,
            DbDataReader reader,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            var factory = new PrimaryKeyFactory(reader);

            while (await reader.ReadAsync(cancellationToken))
            {
                cancellationToken.ThrowIfCancellationRequested();
                factory.CreatePrimaryKey(userTables, primaryKeyColumns, reader);
            }
        }

        public static Dictionary<string, PrimaryKeyColumn> Get(
            Catalog catalog,
            Dictionary<string, UserTable> userTables,
            DbConnection connection,
            IMetadataScriptFactory metadataScriptFactory)
        {
            var primaryKeyColumns = new Dictionary<string, PrimaryKeyColumn>(StringComparer.OrdinalIgnoreCase);
            using (var command = connection.CreateCommand())
            {
                command.CommandText = metadataScriptFactory.PrimaryKeys(catalog.ObjectName);
                using (var reader = command.ExecuteReader())
                {
                    if (!reader.HasRows)
                    {
                        reader.Close();
                        return primaryKeyColumns;
                    }

                    Read(userTables, primaryKeyColumns, reader);

                    reader.Close();
                }
            }

            return primaryKeyColumns;
        }
        
        public static async Task<Dictionary<string, PrimaryKeyColumn>> GetAsync(
            Catalog catalog,
            Dictionary<string, UserTable> userTables,
            DbConnection connection,
            IMetadataScriptFactory metadataScriptFactory,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            var primaryKeyColumns = new Dictionary<string, PrimaryKeyColumn>(StringComparer.OrdinalIgnoreCase);

            using (var command = connection.CreateCommand())
            {
                command.CommandText = metadataScriptFactory.PrimaryKeys(catalog.ObjectName);
                using (var reader = await command.ExecuteReaderAsync(cancellationToken))
                {
                    if (!reader.HasRows)
                    {
                        reader.Close();
                        return primaryKeyColumns;
                    }

                    await ReadAsync(userTables, primaryKeyColumns, reader, cancellationToken);

                    reader.Close();
                }
            }

            return primaryKeyColumns;
        }
    }
}
