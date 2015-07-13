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
    public static class UniqueConstraintAdapter
    {
        public static void Read(
            Dictionary<string, UserTable> userTables,
            Dictionary<string, UniqueConstraintColumn> uniqueConstraintColumns,
            IDataReader reader)
        {
            var factory = new UniqueConstraintFactory(reader);
            
            while (reader.Read())
                factory.CreateUniqueConstraint(userTables, uniqueConstraintColumns, reader);
        }
        
        public static async Task ReadAsync(
            Dictionary<string, UserTable> userTables,
            Dictionary<string, UniqueConstraintColumn> uniqueConstraintColumns,
            DbDataReader reader,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            var factory = new UniqueConstraintFactory(reader);

            while (await reader.ReadAsync(cancellationToken))
            {
                cancellationToken.ThrowIfCancellationRequested();
                factory.CreateUniqueConstraint(userTables, uniqueConstraintColumns, reader);   
            }
        }

        public static Dictionary<string, UniqueConstraintColumn> Get(
            Catalog catalog,
            Dictionary<string, UserTable> userTables,
            DbConnection connection,
            IMetadataScriptFactory metadataScriptFactory)
        {
            var uniqueConstraintColumns = new Dictionary<string, UniqueConstraintColumn>(StringComparer.OrdinalIgnoreCase);
            using (var command = connection.CreateCommand())
            {
                command.CommandText = metadataScriptFactory.UniqueConstraints(catalog.ObjectName);
                using (var reader = command.ExecuteReader())
                {
                    if (!reader.HasRows)
                    {
                        reader.Close();
                        return uniqueConstraintColumns;
                    }

                    Read(userTables, uniqueConstraintColumns, reader);

                    reader.Close();
                }
            }

            return uniqueConstraintColumns;
        }
        
        public static async Task<Dictionary<string, UniqueConstraintColumn>> GetAsync(
            Catalog catalog,
            Dictionary<string, UserTable> userTables,
            DbConnection connection,
            IMetadataScriptFactory metadataScriptFactory,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            var uniqueConstraintColumns = new Dictionary<string, UniqueConstraintColumn>(StringComparer.OrdinalIgnoreCase);

            using (var command = connection.CreateCommand())
            {
                command.CommandText = metadataScriptFactory.UniqueConstraints(catalog.ObjectName);
                using (var reader = await command.ExecuteReaderAsync(cancellationToken))
                {
                    if (!reader.HasRows)
                    {
                        reader.Close();
                        return uniqueConstraintColumns;
                    }

                    await ReadAsync(userTables, uniqueConstraintColumns, reader, cancellationToken);

                    reader.Close();
                }
            }

            return uniqueConstraintColumns;
        }
    }
}
