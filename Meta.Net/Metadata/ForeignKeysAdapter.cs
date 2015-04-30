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
    public static class ForeignKeysAdapter
    {
        public const string Dot = ".";

        public static void Read(Dictionary<string, UserTable> userTables, Dictionary<string, PrimaryKeyColumn> primaryKeyColumns,
            Dictionary<string, UniqueConstraintColumn> uniqueConstraintColumns, IDataReader reader)
        {
            var schemaNameOrdinal = reader.GetOrdinal("SchemaName");
            var tableNameOrdinal = reader.GetOrdinal("TableName");
            var columnNameOrdinal = reader.GetOrdinal("ColumnName");
            var objectNameOrdinal = reader.GetOrdinal("ObjectName");
            var keyOrdinalOrdinal = reader.GetOrdinal("KeyOrdinal");
            var isDisabledOrdinal = reader.GetOrdinal("IsDisabled");
            var isNotForReplicationOrdinal = reader.GetOrdinal("IsNotForReplication");
            var isNotTrustedOrdinal = reader.GetOrdinal("IsNotTrusted");
            var deleteActionOrdinal = reader.GetOrdinal("DeleteAction");
            var deleteActionDescriptionOrdinal = reader.GetOrdinal("DeleteActionDescription");
            var updateActionOrdinal = reader.GetOrdinal("UpdateAction");
            var updateActionDescriptionOrdinal = reader.GetOrdinal("UpdateActionDescription");
            var isSystemNamedOrdinal = reader.GetOrdinal("IsSystemNamed");
            var referencedSchemaNameOrdinal = reader.GetOrdinal("ReferencedSchemaName");
            var referencedTableNameOrdinal = reader.GetOrdinal("ReferencedTableName");
            var referencedColumnNameOrdinal = reader.GetOrdinal("ReferencedColumnName");
            var referencedObjectNameOrdinal = reader.GetOrdinal("ReferencedObjectName");

            while (reader.Read())
            {
                var schemaName = Convert.ToString(reader[schemaNameOrdinal]);
                var tableName = Convert.ToString(reader[tableNameOrdinal]);
                var columnName = Convert.ToString(reader[columnNameOrdinal]);
                var objectName = Convert.ToString(reader[objectNameOrdinal]);
                var keyOrdinal = Convert.ToInt32(reader[keyOrdinalOrdinal]);
                var isDisabled = Convert.ToBoolean(reader[isDisabledOrdinal]);
                var isNotForReplication = Convert.ToBoolean(reader[isNotForReplicationOrdinal]);
                var isNotTrusted = Convert.ToBoolean(reader[isNotTrustedOrdinal]);
                var deleteAction = Convert.ToInt32(reader[deleteActionOrdinal]);
                var deleteActionDescription = Convert.ToString(reader[deleteActionDescriptionOrdinal]);
                var updateAction = Convert.ToInt32(reader[updateActionOrdinal]);
                var updateActionDescription = Convert.ToString(reader[updateActionDescriptionOrdinal]);
                var isSystemNamed = Convert.ToBoolean(reader[isSystemNamedOrdinal]);
                var referencedSchemaName = Convert.ToString(reader[referencedSchemaNameOrdinal]);
                var referencedTableName = Convert.ToString(reader[referencedTableNameOrdinal]);
                var referencedColumnName = Convert.ToString(reader[referencedColumnNameOrdinal]);
                var referencedObjectName = Convert.ToString(reader[referencedObjectNameOrdinal]);

                var userTableNamespaceBuilder = new StringBuilder(schemaName.Length + tableName.Length + 1);
                userTableNamespaceBuilder.Append(schemaName).Append(Dot).Append(tableName);

                var userTableNamespace = userTableNamespaceBuilder.ToString();
                if (!userTables.ContainsKey(userTableNamespace))
                    continue;

                var userTable = userTables[userTableNamespace];
                if (userTable == null)
                    continue;

                var referencedUserTableNamespaceBuilder = new StringBuilder(referencedSchemaName.Length + referencedTableName.Length + 1);
                referencedUserTableNamespaceBuilder.Append(referencedSchemaName).Append(Dot).Append(referencedTableName);
                var referencedUserTableNamespace = referencedUserTableNamespaceBuilder.ToString();
                if (!userTables.ContainsKey(referencedUserTableNamespace))
                    continue;

                var referencedUserTable = userTables[referencedUserTableNamespace];
                if (referencedUserTable == null)
                    continue;

                var referencedUserTableColumnNamespaceBuilder = new StringBuilder(referencedUserTableNamespace.Length + referencedObjectName.Length + referencedColumnName.Length + 2);
                referencedUserTableColumnNamespaceBuilder.Append(referencedUserTableNamespace).Append(Dot).Append(referencedObjectName).Append(Dot).Append(referencedColumnName);
                var referencedUserTableColumnNamespace = referencedUserTableColumnNamespaceBuilder.ToString();
                PrimaryKeyColumn referencedPrimaryKeyColumn;
                primaryKeyColumns.TryGetValue(referencedUserTableColumnNamespace, out referencedPrimaryKeyColumn);
                UniqueConstraintColumn referencedUniqueConstraintColumn = null;
                if (referencedPrimaryKeyColumn == null)
                    uniqueConstraintColumns.TryGetValue(referencedUserTableColumnNamespace, out referencedUniqueConstraintColumn);

                if (referencedPrimaryKeyColumn == null && referencedUniqueConstraintColumn == null)
                    throw new Exception(
                        string.Format(
                            "The foreign key {0}.{1} column has no associated primary key or unique constraint column.",
                            objectName, columnName));

                var foreignKeyNamespaceBuilder = new StringBuilder(userTableNamespace.Length + objectName + 1);
                foreignKeyNamespaceBuilder.Append(userTableNamespace).Append(Dot).Append(objectName);
                var foreignKeyNamespace = foreignKeyNamespaceBuilder.ToString();
                var foreignKey = userTable.ForeignKeys[foreignKeyNamespace];
                if (foreignKey == null)
                {
                    foreignKey = new ForeignKey
                    {
                        UserTable = userTable,
                        ObjectName = objectName,
                        IsDisabled = isDisabled,
                        IsNotForReplication = isNotForReplication,
                        IsNotTrusted = isNotTrusted,
                        IsSystemNamed = isSystemNamed,
                        DeleteAction = deleteAction,
                        DeleteActionDescription = deleteActionDescription,
                        UpdateAction = updateAction,
                        UpdateActionDescription = updateActionDescription
                    };

                    userTable.ForeignKeys.Add(foreignKey);
                }

                var foreignKeyColumn = new ForeignKeyColumn
                {
                    ReferencedUserTable = referencedUserTable,
                    ForeignKey = foreignKey,
                    ObjectName = columnName,
                    KeyOrdinal = keyOrdinal,
                    ReferencedColumn = referencedPrimaryKeyColumn as IIndexColumn ?? referencedUniqueConstraintColumn
                };

                foreignKey.ForeignKeyColumns.Add(foreignKeyColumn);
            }
        }

        public static async Task GetAsync(Catalog catalog, Dictionary<string, UserTable> userTables, Dictionary<string, PrimaryKeyColumn> primaryKeyColumns,
            Dictionary<string, UniqueConstraintColumn> uniqueConstraintColumns, DbConnection connection, IMetadataScriptFactory metadataScriptFactory)
        {
            using (var command = connection.CreateCommand())
            {
                command.CommandText = metadataScriptFactory.ForeignKeys(catalog.ObjectName);
                using (var reader = await command.ExecuteReaderAsync())
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
    }
}
