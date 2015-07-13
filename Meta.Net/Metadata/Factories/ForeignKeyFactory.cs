using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Meta.Net.Interfaces;
using Meta.Net.Objects;

namespace Meta.Net.Metadata.Factories
{
    internal class ForeignKeyFactory
    {
        private int SchemaNameOrdinal { get; set; }
        private int TableNameOrdinal { get; set; }
        private int ColumnNameOrdinal { get; set; }
        private int ObjectNameOrdinal { get; set; }
        private int KeyOrdinalOrdinal { get; set; }
        private int IsDisabledOrdinal { get; set; }
        private int IsNotForReplicationOrdinal { get; set; }
        private int IsNotTrustedOrdinal { get; set; }
        private int DeleteActionOrdinal { get; set; }
        private int DeleteActionDescriptionOrdinal { get; set; }
        private int UpdateActionOrdinal { get; set; }
        private int UpdateActionDescriptionOrdinal { get; set; }
        private int IsSystemNamedOrdinal { get; set; }
        private int ReferencedSchemaNameOrdinal { get; set; }
        private int ReferencedTableNameOrdinal { get; set; }
        private int ReferencedColumnNameOrdinal { get; set; }
        private int ReferencedObjectNameOrdinal { get; set; }

        public ForeignKeyFactory(IDataRecord reader)
        {
            SchemaNameOrdinal = reader.GetOrdinal("SchemaName");
            TableNameOrdinal = reader.GetOrdinal("TableName");
            ColumnNameOrdinal = reader.GetOrdinal("ColumnName");
            ObjectNameOrdinal = reader.GetOrdinal("ObjectName");
            KeyOrdinalOrdinal = reader.GetOrdinal("KeyOrdinal");
            IsDisabledOrdinal = reader.GetOrdinal("IsDisabled");
            IsNotForReplicationOrdinal = reader.GetOrdinal("IsNotForReplication");
            IsNotTrustedOrdinal = reader.GetOrdinal("IsNotTrusted");
            DeleteActionOrdinal = reader.GetOrdinal("DeleteAction");
            DeleteActionDescriptionOrdinal = reader.GetOrdinal("DeleteActionDescription");
            UpdateActionOrdinal = reader.GetOrdinal("UpdateAction");
            UpdateActionDescriptionOrdinal = reader.GetOrdinal("UpdateActionDescription");
            IsSystemNamedOrdinal = reader.GetOrdinal("IsSystemNamed");
            ReferencedSchemaNameOrdinal = reader.GetOrdinal("ReferencedSchemaName");
            ReferencedTableNameOrdinal = reader.GetOrdinal("ReferencedTableName");
            ReferencedColumnNameOrdinal = reader.GetOrdinal("ReferencedColumnName");
            ReferencedObjectNameOrdinal = reader.GetOrdinal("ReferencedObjectName");
        }

        public void CreateForeignKey(
            Dictionary<string, UserTable> userTables,
            Dictionary<string, PrimaryKeyColumn> primaryKeyColumns,
            Dictionary<string, UniqueConstraintColumn> uniqueConstraintColumns,
            IDataRecord reader)
        {
            var schemaName = Convert.ToString(reader[SchemaNameOrdinal]);
            var tableName = Convert.ToString(reader[TableNameOrdinal]);
            var columnName = Convert.ToString(reader[ColumnNameOrdinal]);
            var objectName = Convert.ToString(reader[ObjectNameOrdinal]);
            var referencedSchemaName = Convert.ToString(reader[ReferencedSchemaNameOrdinal]);
            var referencedTableName = Convert.ToString(reader[ReferencedTableNameOrdinal]);
            var referencedColumnName = Convert.ToString(reader[ReferencedColumnNameOrdinal]);
            var referencedObjectName = Convert.ToString(reader[ReferencedObjectNameOrdinal]);

            var userTableNamespaceBuilder = new StringBuilder(schemaName.Length + tableName.Length + 1);
            userTableNamespaceBuilder.Append(schemaName).
                Append(Constants.Dot).
                Append(tableName);

            var userTableNamespace = userTableNamespaceBuilder.ToString();
            if (!userTables.ContainsKey(userTableNamespace))
                return;

            var userTable = userTables[userTableNamespace];
            if (userTable == null)
                return;

            var referencedUserTableNamespaceBuilder = new StringBuilder(referencedSchemaName.Length + referencedTableName.Length + 1);
            referencedUserTableNamespaceBuilder.Append(referencedSchemaName).
                Append(Constants.Dot).
                Append(referencedTableName);
            var referencedUserTableNamespace = referencedUserTableNamespaceBuilder.ToString();
            if (!userTables.ContainsKey(referencedUserTableNamespace))
                return;

            var referencedUserTable = userTables[referencedUserTableNamespace];
            if (referencedUserTable == null)
                return;

            var referencedUserTableColumnNamespaceBuilder = new StringBuilder(referencedUserTableNamespace.Length + referencedObjectName.Length + referencedColumnName.Length + 2);
            referencedUserTableColumnNamespaceBuilder.Append(referencedUserTableNamespace).
                Append(Constants.Dot).
                Append(referencedObjectName).
                Append(Constants.Dot).
                Append(referencedColumnName);
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
            foreignKeyNamespaceBuilder.Append(userTableNamespace).
                Append(Constants.Dot).
                Append(objectName);
            var foreignKeyNamespace = foreignKeyNamespaceBuilder.ToString();
            var foreignKey = userTable.ForeignKeys[foreignKeyNamespace];
            if (foreignKey == null)
            {
                foreignKey = new ForeignKey
                {
                    UserTable = userTable,
                    ObjectName = objectName,
                    IsDisabled = Convert.ToBoolean(reader[IsDisabledOrdinal]),
                    IsNotForReplication = Convert.ToBoolean(reader[IsNotForReplicationOrdinal]),
                    IsNotTrusted = Convert.ToBoolean(reader[IsNotTrustedOrdinal]),
                    IsSystemNamed = Convert.ToBoolean(reader[IsSystemNamedOrdinal]),
                    DeleteAction = Convert.ToInt32(reader[DeleteActionOrdinal]),
                    DeleteActionDescription = Convert.ToString(reader[DeleteActionDescriptionOrdinal]),
                    UpdateAction = Convert.ToInt32(reader[UpdateActionOrdinal]),
                    UpdateActionDescription = Convert.ToString(reader[UpdateActionDescriptionOrdinal])
                };

                userTable.ForeignKeys.Add(foreignKey);
            }

            var foreignKeyColumn = new ForeignKeyColumn
            {
                ReferencedUserTable = referencedUserTable,
                ForeignKey = foreignKey,
                ObjectName = columnName,
                KeyOrdinal = Convert.ToInt32(reader[KeyOrdinalOrdinal]),
                ReferencedColumn = referencedPrimaryKeyColumn as IIndexColumn ?? referencedUniqueConstraintColumn
            };

            foreignKey.ForeignKeyColumns.Add(foreignKeyColumn);
        }
    }
}
