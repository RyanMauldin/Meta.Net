using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Meta.Net.Interfaces;
using Meta.Net.Objects;

namespace Meta.Net.Metadata
{
    public static class ComputedColumnsAdapter
    {
        public const string Dot = ".";

        public static void Read(Dictionary<string, UserTable> userTables, IDataReader reader)
        {
            var schemaNameOrdinal = reader.GetOrdinal("SchemaName");
            var tableNameOrdinal = reader.GetOrdinal("TableName");
            var objectNameOrdinal = reader.GetOrdinal("ObjectName");
            var definitionOrdinal = reader.GetOrdinal("Definition");
            var isPersistedOrdinal = reader.GetOrdinal("IsPersisted");
            var isNullableOrdinal = reader.GetOrdinal("IsNullable");

            while (reader.Read())
            {
                var schemaName = Convert.ToString(reader[schemaNameOrdinal]);
                var tableName = Convert.ToString(reader[tableNameOrdinal]);
                var objectName = Convert.ToString(reader[objectNameOrdinal]);
                var definition = Convert.ToString(reader[definitionOrdinal]);
                var isPersisted = Convert.ToBoolean(reader[isPersistedOrdinal]);
                var isNullable = Convert.ToBoolean(reader[isNullableOrdinal]);

                var userTableNamespaceBuilder = new StringBuilder(schemaName.Length + tableName.Length + 1);
                userTableNamespaceBuilder.Append(schemaName).Append(Dot).Append(tableName);

                var userTableNamespace = userTableNamespaceBuilder.ToString();
                if (!userTables.ContainsKey(userTableNamespace))
                    continue;

                var userTable = userTables[userTableNamespace];
                if (userTable == null)
                    continue;

                var computedColumn = new ComputedColumn
                {
                    UserTable = userTable,
                    ObjectName = objectName,
                    Definition = definition,
                    IsPersisted = isPersisted,
                    IsNullable = isNullable
                };

                userTable.ComputedColumns.Add(computedColumn);
            }
        }

        public static void Get(Catalog catalog, Dictionary<string, UserTable> userTables, DbConnection connection, IMetadataScriptFactory metadataScriptFactory)
        {
            using (var command = connection.CreateCommand())
            {
                command.CommandText = metadataScriptFactory.ComputedColumns(catalog.ObjectName);
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
        
        public static async Task GetAsync(Catalog catalog, Dictionary<string, UserTable> userTables, DbConnection connection, IMetadataScriptFactory metadataScriptFactory)
        {
            using (var command = connection.CreateCommand())
            {
                command.CommandText = metadataScriptFactory.ComputedColumns(catalog.ObjectName);
                using (var reader = await command.ExecuteReaderAsync())
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
        
        public static async Task GetAsync(Catalog catalog, Dictionary<string, UserTable> userTables, DbConnection connection, IMetadataScriptFactory metadataScriptFactory, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return;

            using (var command = connection.CreateCommand())
            {
                command.CommandText = metadataScriptFactory.ComputedColumns(catalog.ObjectName);
                using (var reader = await command.ExecuteReaderAsync(cancellationToken))
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
    }
}
