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
    public static class UserTableAdapter
    {
        public static void Read(
            Catalog catalog,
            Dictionary<string, UserTable> userTables,
            IDataReader reader)
        {
            var factory = new UserTableFactory(reader);

            while (reader.Read())
                factory.CreateUserTable(catalog, userTables, reader);
        }
        
        public static async Task ReadAsync(
            Catalog catalog,
            Dictionary<string, UserTable> userTables,
            DbDataReader reader,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            var factory = new UserTableFactory(reader);

            while (await reader.ReadAsync(cancellationToken))
            {
                cancellationToken.ThrowIfCancellationRequested();
                factory.CreateUserTable(catalog, userTables, reader);
            }
        }

        public static Dictionary<string, UserTable> Get(
            Catalog catalog,
            DbConnection connection,
            IMetadataScriptFactory metadataScriptFactory)
        {
            var userTables = new Dictionary<string, UserTable>(StringComparer.OrdinalIgnoreCase);
            using (var command = connection.CreateCommand())
            {
                command.CommandText = metadataScriptFactory.UserTables(catalog.ObjectName);
                using (var reader = command.ExecuteReader())
                {
                    if (!reader.HasRows)
                    {
                        reader.Close();
                        return userTables;
                    }

                    Read(catalog, userTables, reader);

                    reader.Close();
                }
            }

            return userTables;
        }

        public static async Task<Dictionary<string, UserTable>> GetAsync(
            Catalog catalog,
            DbConnection connection,
            IMetadataScriptFactory metadataScriptFactory,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            var userTables = new Dictionary<string, UserTable>(StringComparer.OrdinalIgnoreCase);
            using (var command = connection.CreateCommand())
            {
                command.CommandText = metadataScriptFactory.UserTables(catalog.ObjectName);
                using (var reader = await command.ExecuteReaderAsync(cancellationToken))
                {
                    if (!reader.HasRows)
                    {
                        reader.Close();
                        return userTables;
                    }

                    await ReadAsync(catalog, userTables, reader, cancellationToken);

                    reader.Close();
                }
            }

            return userTables;
        }

        public static void Build(
            Catalog catalog,
            DbConnection connection,
            IMetadataScriptFactory metadataScriptFactory)
        {
            var userTables = Get(catalog, connection, metadataScriptFactory);
            if (userTables.Count == 0)
                return;

            UserTableColumnAdapter.Get(catalog, userTables, connection, metadataScriptFactory);
            var primaryKeyColumns = PrimaryKeyAdapter.Get(catalog, userTables, connection, metadataScriptFactory);
            var uniqueConstraintColumns = UniqueConstraintAdapter.Get(catalog, userTables, connection, metadataScriptFactory);
            ForeignKeyAdapter.Get(catalog, userTables, primaryKeyColumns, uniqueConstraintColumns, connection, metadataScriptFactory);
            ComputedColumnsAdapter.Get(catalog, userTables, connection, metadataScriptFactory);
            DefaultConstraintAdapter.Get(catalog, userTables, connection, metadataScriptFactory);
            CheckConstraintAdapter.Get(catalog, userTables, connection, metadataScriptFactory);
            IdentityColumnAdapter.Get(catalog, userTables, connection, metadataScriptFactory);
            IndexAdapter.Get(catalog, userTables, connection, metadataScriptFactory);
        }

        public static async Task BuildAsync(
            Catalog catalog,
            DbConnection connection,
            IMetadataScriptFactory metadataScriptFactory,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            var userTables = await GetAsync(catalog, connection, metadataScriptFactory, cancellationToken);
            if (userTables.Count == 0)
                return;

            await UserTableColumnAdapter.GetAsync(catalog, userTables, connection, metadataScriptFactory, cancellationToken);
            var primaryKeyColumns = await PrimaryKeyAdapter.GetAsync(catalog, userTables, connection, metadataScriptFactory, cancellationToken);
            var uniqueConstraintColumns = await UniqueConstraintAdapter.GetAsync(catalog, userTables, connection, metadataScriptFactory, cancellationToken);
            await ForeignKeyAdapter.GetAsync(catalog, userTables, primaryKeyColumns, uniqueConstraintColumns, connection, metadataScriptFactory, cancellationToken);
            await ComputedColumnsAdapter.GetAsync(catalog, userTables, connection, metadataScriptFactory, cancellationToken);
            await DefaultConstraintAdapter.GetAsync(catalog, userTables, connection, metadataScriptFactory, cancellationToken);
            await CheckConstraintAdapter.GetAsync(catalog, userTables, connection, metadataScriptFactory, cancellationToken);
            await IdentityColumnAdapter.GetAsync(catalog, userTables, connection, metadataScriptFactory, cancellationToken);
            await IndexAdapter.GetAsync(catalog, userTables, connection, metadataScriptFactory, cancellationToken);
        }
    }
}
