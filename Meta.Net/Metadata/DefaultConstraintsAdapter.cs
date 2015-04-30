using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Threading.Tasks;
using Meta.Net.Interfaces;
using Meta.Net.Objects;

namespace Meta.Net.Metadata
{
    public static class DefaultConstraintsAdapter
    {
        public const string Dot = ".";

        public static void Read(Dictionary<string, UserTable> userTables, IDataReader reader)
        {
            var schemaNameOrdinal = reader.GetOrdinal("SchemaName");
            var tableNameOrdinal = reader.GetOrdinal("TableName");
            var columnNameOrdinal = reader.GetOrdinal("ColumnName");
            var objectNameOrdinal = reader.GetOrdinal("ObjectName");
            var definitionOrdinal = reader.GetOrdinal("Definition");
            var isSystemNamedOrdinal = reader.GetOrdinal("IsSystemNamed");

            while (reader.Read())
            {
                var schemaName = Convert.ToString(reader[schemaNameOrdinal]);
                var tableName = Convert.ToString(reader[tableNameOrdinal]);
                var columnName = Convert.ToString(reader[columnNameOrdinal]);
                var objectName = Convert.ToString(reader[objectNameOrdinal]);
                var definition = Convert.ToString(reader[definitionOrdinal]);
                var isSystemNamed = Convert.ToBoolean(reader[isSystemNamedOrdinal]);

                var userTableNamespaceBuilder = new StringBuilder(schemaName.Length + tableName.Length + 1);
                userTableNamespaceBuilder.Append(schemaName).Append(Dot).Append(tableName);

                var userTableNamespace = userTableNamespaceBuilder.ToString();
                if (!userTables.ContainsKey(userTableNamespace))
                    continue;

                var userTable = userTables[userTableNamespace];
                if (userTable == null)
                    continue;

                var defaultConstraint = new DefaultConstraint
                {
                    UserTable = userTable,
                    ColumnName = columnName,
                    ObjectName = objectName,
                    Definition = definition,
                    IsSystemNamed = isSystemNamed
                };

                userTable.DefaultConstraints.Add(defaultConstraint);
            }
        }

        public static async Task GetAsync(Catalog catalog, Dictionary<string, UserTable> userTables, DbConnection connection, IMetadataScriptFactory metadataScriptFactory)
        {
            using (var command = connection.CreateCommand())
            {
                command.CommandText = metadataScriptFactory.DefaultConstraints(catalog.ObjectName);
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
    }
}
