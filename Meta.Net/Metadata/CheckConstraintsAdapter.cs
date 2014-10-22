using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using Meta.Net.Interfaces;
using Meta.Net.Objects;

namespace Meta.Net.Metadata
{
    public static class CheckConstraintsAdapter
    {
        public const string Dot = ".";

        public static void Read(Dictionary<string, UserTable> userTables, IDataReader reader)
        {
            var schemaNameOrdinal = reader.GetOrdinal("SchemaName");
            var tableNameOrdinal = reader.GetOrdinal("TableName");
            var columnNameOrdinal = reader.GetOrdinal("ColumnName");
            var objectNameOrdinal = reader.GetOrdinal("ObjectName");
            var definitionOrdinal = reader.GetOrdinal("Definition");
            var isTableConstraintOrdinal = reader.GetOrdinal("IsTableConstraint");
            var isDisabledOrdinal = reader.GetOrdinal("IsDisabled");
            var isNotForReplicationOrdinal = reader.GetOrdinal("IsNotForReplication");
            var isNotTrustedOrdinal = reader.GetOrdinal("IsNotTrusted");
            var isSystemNamedOrdinal = reader.GetOrdinal("IsSystemNamed");

            while (reader.Read())
            {
                var schemaName = Convert.ToString(reader[schemaNameOrdinal]);
                var tableName = Convert.ToString(reader[tableNameOrdinal]);
                var columnName = Convert.ToString(reader[columnNameOrdinal]);
                var objectName = Convert.ToString(reader[objectNameOrdinal]);
                var definition = Convert.ToString(reader[definitionOrdinal]);
                var isTableConstraint = Convert.ToBoolean(reader[isTableConstraintOrdinal]);
                var isDisabled = Convert.ToBoolean(reader[isDisabledOrdinal]);
                var isNotForReplication = Convert.ToBoolean(reader[isNotForReplicationOrdinal]);
                var isNotTrusted = Convert.ToBoolean(reader[isNotTrustedOrdinal]);
                var isSystemNamed = Convert.ToBoolean(reader[isSystemNamedOrdinal]);

                var userTableNamespaceBuilder = new StringBuilder(schemaName.Length + tableName.Length + 1);
                userTableNamespaceBuilder.Append(schemaName).Append(Dot).Append(tableName);

                var userTableNamespace = userTableNamespaceBuilder.ToString();
                if (!userTables.ContainsKey(userTableNamespace))
                    continue;

                var userTable = userTables[userTableNamespace];
                if (userTable == null)
                    continue;

                var checkConstraint = new CheckConstraint
                {
                    UserTable = userTable,
                    ColumnName = columnName,
                    ObjectName = objectName,
                    Definition = definition,
                    IsTableConstraint = isTableConstraint,
                    IsDisabled = isDisabled,
                    IsNotForReplication = isNotForReplication,
                    IsNotTrusted = isNotTrusted,
                    IsSystemNamed = isSystemNamed
                };

                userTable.CheckConstraints.Add(checkConstraint);
            }
        }

        public static void Get(Catalog catalog, Dictionary<string, UserTable> userTables, DbConnection connection, IMetadataScriptFactory metadataScriptFactory)
        {
            using (var command = connection.CreateCommand())
            {
                command.CommandText = metadataScriptFactory.CheckConstraints(catalog.ObjectName);
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
    }
}
