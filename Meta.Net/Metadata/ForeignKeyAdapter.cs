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
    public static class ForeignKeyAdapter
    {
        public static void Read(
            Dictionary<string, UserTable> userTables,
            Dictionary<string, PrimaryKeyColumn> primaryKeyColumns,
            Dictionary<string, UniqueConstraintColumn> uniqueConstraintColumns,
            IDataReader reader)
        {
            var factory = new ForeignKeyFactory(reader);

            while (reader.Read())
                factory.CreateForeignKey(userTables, primaryKeyColumns, uniqueConstraintColumns, reader);
        }
        
        public static async Task ReadAsync(
            Dictionary<string, UserTable> userTables,
            Dictionary<string, PrimaryKeyColumn> primaryKeyColumns,
            Dictionary<string, UniqueConstraintColumn> uniqueConstraintColumns,
            DbDataReader reader,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            var factory = new ForeignKeyFactory(reader);

            while (await reader.ReadAsync(cancellationToken))
            {
                cancellationToken.ThrowIfCancellationRequested();
                factory.CreateForeignKey(userTables, primaryKeyColumns, uniqueConstraintColumns, reader);
            }
        }

        public static void Get(
            Catalog catalog,
            Dictionary<string, UserTable> userTables,
            Dictionary<string, PrimaryKeyColumn> primaryKeyColumns,
            Dictionary<string, UniqueConstraintColumn> uniqueConstraintColumns,
            DbConnection connection,
            IMetadataScriptFactory metadataScriptFactory)
        {
            using (var command = connection.CreateCommand())
            {
                command.CommandText = metadataScriptFactory.ForeignKeys(catalog.ObjectName);
                using (var reader = command.ExecuteReader())
                {
                    if (!reader.HasRows)
                    {
                        reader.Close();
                        return;
                    }

                    Read(userTables, primaryKeyColumns, uniqueConstraintColumns, reader);

                    reader.Close();
                }
            }
        }
        
        public static async Task GetAsync(
            Catalog catalog, Dictionary<string, UserTable> userTables,
            Dictionary<string, PrimaryKeyColumn> primaryKeyColumns,
            Dictionary<string, UniqueConstraintColumn> uniqueConstraintColumns,
            DbConnection connection,
            IMetadataScriptFactory metadataScriptFactory,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = metadataScriptFactory.ForeignKeys(catalog.ObjectName);
                using (var reader = await command.ExecuteReaderAsync(cancellationToken))
                {
                    if (!reader.HasRows)
                    {
                        reader.Close();
                        return;
                    }

                    await ReadAsync(userTables, primaryKeyColumns, uniqueConstraintColumns, reader, cancellationToken);

                    reader.Close();
                }
            }
        }
    }
}
