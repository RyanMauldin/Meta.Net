using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Meta.Net.Objects;
using UniqueConstraint = Meta.Net.Objects.UniqueConstraint;

namespace Meta.Net.Metadata.Factories
{
    internal class UniqueConstraintFactory
    {
        private int SchemaNameOrdinal { get; set; }
        private int TableNameOrdinal { get; set; }
        private int ColumnNameOrdinal { get; set; }
        private int ObjectNameOrdinal { get; set; }
        private int FileGroupOrdinal { get; set; }
        private int KeyOrdinalOrdinal { get; set; }
        private int PartitionOrdinalOrdinal { get; set; }
        private int IsClusteredOrdinal { get; set; }
        private int IsDescendingKeyOrdinal { get; set; }
        private int IgnoreDupKeyOrdinal { get; set; }
        private int FillFactorOrdinal { get; set; }
        private int IsPaddedOrdinal { get; set; }
        private int IsDisabledOrdinal { get; set; }
        private int AllowRowLocksOrdinal { get; set; }
        private int AllowPageLocksOrdinal { get; set; }
        private int IndexTypeOrdinal { get; set; }

        public UniqueConstraintFactory(IDataRecord reader)
        {
            SchemaNameOrdinal = reader.GetOrdinal("SchemaName");
            TableNameOrdinal = reader.GetOrdinal("TableName");
            ColumnNameOrdinal = reader.GetOrdinal("ColumnName");
            ObjectNameOrdinal = reader.GetOrdinal("ObjectName");
            FileGroupOrdinal = reader.GetOrdinal("FileGroup");
            KeyOrdinalOrdinal = reader.GetOrdinal("KeyOrdinal");
            PartitionOrdinalOrdinal = reader.GetOrdinal("PartitionOrdinal");
            IsClusteredOrdinal = reader.GetOrdinal("IsClustered");
            IsDescendingKeyOrdinal = reader.GetOrdinal("IsDescendingKey");
            IgnoreDupKeyOrdinal = reader.GetOrdinal("IgnoreDupKey");
            FillFactorOrdinal = reader.GetOrdinal("FillFactor");
            IsPaddedOrdinal = reader.GetOrdinal("IsPadded");
            IsDisabledOrdinal = reader.GetOrdinal("IsDisabled");
            AllowRowLocksOrdinal = reader.GetOrdinal("AllowRowLocks");
            AllowPageLocksOrdinal = reader.GetOrdinal("AllowPageLocks");
            IndexTypeOrdinal = reader.GetOrdinal("IndexType");
        }

        public void CreateUniqueConstraint(
            Dictionary<string, UserTable> userTables,
            Dictionary<string, UniqueConstraintColumn> uniqueConstraintColumns,
            IDataRecord reader)
        {
            var schemaName = Convert.ToString(reader[SchemaNameOrdinal]);
            var tableName = Convert.ToString(reader[TableNameOrdinal]);
            var objectName = Convert.ToString(reader[ObjectNameOrdinal]);

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

            var uniqueConstraintNamespaceBuilder = new StringBuilder(userTableNamespace.Length + objectName.Length + 1);
            uniqueConstraintNamespaceBuilder.
                Append(userTableNamespace).
                Append(Constants.Dot).
                Append(objectName);
            var uniqueConstraintNamespace = uniqueConstraintNamespaceBuilder.ToString();
            var uniqueConstraint = userTable.UniqueConstraints[uniqueConstraintNamespace];
            if (uniqueConstraint == null)
            {
                uniqueConstraint = new UniqueConstraint
                {
                    UserTable = userTable,
                    ObjectName = objectName,
                    FileGroup = Convert.ToString(reader[FileGroupOrdinal]),
                    IgnoreDupKey = Convert.ToBoolean(reader[IgnoreDupKeyOrdinal]),
                    IsClustered = Convert.ToBoolean(reader[IsClusteredOrdinal]),
                    FillFactor = Convert.ToInt32(reader[FillFactorOrdinal]),
                    IsPadded = Convert.ToBoolean(reader[IsPaddedOrdinal]),
                    IsDisabled = Convert.ToBoolean(reader[IsDisabledOrdinal]),
                    AllowRowLocks = Convert.ToBoolean(reader[AllowRowLocksOrdinal]),
                    AllowPageLocks = Convert.ToBoolean(reader[AllowPageLocksOrdinal]),
                    IndexType = Convert.ToString(reader[IndexTypeOrdinal]) // TODO: Remove this if possible... check other index code logic for usage (Mysql has BTREE, FULLTEXT, etc..., SQL Server doesn't)
                };

                userTable.UniqueConstraints.Add(uniqueConstraint);
            }

            var uniqueConstraintColumn = new UniqueConstraintColumn
            {
                UniqueConstraint = uniqueConstraint,
                ObjectName = Convert.ToString(reader[ColumnNameOrdinal]),
                IsDescendingKey = Convert.ToBoolean(reader[IsDescendingKeyOrdinal]),
                KeyOrdinal = Convert.ToInt32(reader[KeyOrdinalOrdinal]),
                PartitionOrdinal = Convert.ToInt32(reader[PartitionOrdinalOrdinal])
            };

            uniqueConstraint.UniqueConstraintColumns.Add(uniqueConstraintColumn);
            uniqueConstraintColumns.Add(uniqueConstraintColumn.Namespace, uniqueConstraintColumn);
        }
    }
}
