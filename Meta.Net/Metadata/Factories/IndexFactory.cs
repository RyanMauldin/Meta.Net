using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Meta.Net.Objects;

namespace Meta.Net.Metadata.Factories
{
    internal class IndexFactory
    {
        private int SchemaNameOrdinal { get; set; }
        private int TableNameOrdinal { get; set; }
        private int ColumnNameOrdinal { get; set; }
        private int ObjectNameOrdinal { get; set; }
        private int FileGroupOrdinal { get; set; }
        private int KeyOrdinalOrdinal { get; set; }
        private int PartitionOrdinalOrdinal { get; set; }
        private int IsDescendingKeyOrdinal { get; set; }
        private int IsIncludedColumnOrdinal { get; set; }
        private int IgnoreDupKeyOrdinal { get; set; }
        private int IsClusteredOrdinal { get; set; }
        private int IsUniqueOrdinal { get; set; }
        private int FillFactorOrdinal { get; set; }
        private int IsPaddedOrdinal { get; set; }
        private int IsDisabledOrdinal { get; set; }
        private int AllowRowLocksOrdinal { get; set; }
        private int AllowPageLocksOrdinal { get; set; }
        private int IndexTypeOrdinal { get; set; }

        public IndexFactory(IDataRecord reader)
        {
            SchemaNameOrdinal = reader.GetOrdinal("SchemaName");
            TableNameOrdinal = reader.GetOrdinal("TableName");
            ColumnNameOrdinal = reader.GetOrdinal("ColumnName");
            ObjectNameOrdinal = reader.GetOrdinal("ObjectName");
            FileGroupOrdinal = reader.GetOrdinal("FileGroup");
            KeyOrdinalOrdinal = reader.GetOrdinal("KeyOrdinal");
            PartitionOrdinalOrdinal = reader.GetOrdinal("PartitionOrdinal");
            IsDescendingKeyOrdinal = reader.GetOrdinal("IsDescendingKey");
            IsIncludedColumnOrdinal = reader.GetOrdinal("IsIncludedColumn");
            IgnoreDupKeyOrdinal = reader.GetOrdinal("IgnoreDupKey");
            IsClusteredOrdinal = reader.GetOrdinal("IsClustered");
            IsUniqueOrdinal = reader.GetOrdinal("IsUnique");
            FillFactorOrdinal = reader.GetOrdinal("FillFactor");
            IsPaddedOrdinal = reader.GetOrdinal("IsPadded");
            IsDisabledOrdinal = reader.GetOrdinal("IsDisabled");
            AllowRowLocksOrdinal = reader.GetOrdinal("AllowRowLocks");
            AllowPageLocksOrdinal = reader.GetOrdinal("AllowPageLocks");
            IndexTypeOrdinal = reader.GetOrdinal("IndexType");
        }

        public void CreateIndex(
            Dictionary<string, UserTable> userTables,
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

            var indexNamespaceBuilder = new StringBuilder(userTableNamespace.Length + objectName.Length + 1);
            indexNamespaceBuilder.Append(userTableNamespace).
                Append(Constants.Dot).
                Append(objectName);
            var indexNamespace = indexNamespaceBuilder.ToString();
            var index = userTable.Indexes[indexNamespace];
            if (index == null)
            {
                index = new Index
                {
                    UserTable = userTable,
                    ObjectName = objectName,
                    FileGroup = Convert.ToString(reader[FileGroupOrdinal]),
                    IgnoreDupKey = Convert.ToBoolean(reader[IgnoreDupKeyOrdinal]),
                    IsClustered = Convert.ToBoolean(reader[IsClusteredOrdinal]),
                    IsUnique = Convert.ToBoolean(reader[IsUniqueOrdinal]),
                    FillFactor = Convert.ToInt32(reader[FillFactorOrdinal]),
                    IsPadded = Convert.ToBoolean(reader[IsPaddedOrdinal]),
                    IsDisabled = Convert.ToBoolean(reader[IsDisabledOrdinal]),
                    AllowRowLocks = Convert.ToBoolean(reader[AllowRowLocksOrdinal]),
                    AllowPageLocks = Convert.ToBoolean(reader[AllowPageLocksOrdinal]),
                    IndexType = Convert.ToString(reader[IndexTypeOrdinal]) // TODO: Remove this if possible... check other index code logic for usage (Mysql has BTREE, FULLTEXT, etc..., SQL Server doesn't)
                };

                userTable.Indexes.Add(index);
            }

            var indexColumn = new IndexColumn
            {
                Index = index,
                ObjectName = Convert.ToString(reader[ColumnNameOrdinal]),
                IsDescendingKey = Convert.ToBoolean(reader[IsDescendingKeyOrdinal]),
                KeyOrdinal = Convert.ToInt32(reader[KeyOrdinalOrdinal]),
                PartitionOrdinal = Convert.ToInt32(reader[PartitionOrdinalOrdinal]),
                IsIncludedColumn = Convert.ToBoolean(reader[IsIncludedColumnOrdinal])
            };

            index.IndexColumns.Add(indexColumn);
        }
    }
}
