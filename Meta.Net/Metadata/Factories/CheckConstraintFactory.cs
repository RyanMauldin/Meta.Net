using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Meta.Net.Objects;

namespace Meta.Net.Metadata.Factories
{
    internal class CheckConstraintFactory
    {
        private int SchemaNameOrdinal { get; set; }
        private int TableNameOrdinal { get; set; }
        private int ColumnNameOrdinal { get; set; }
        private int ObjectNameOrdinal { get; set; }
        private int DefinitionOrdinal { get; set; }
        private int IsTableConstraintOrdinal { get; set; }
        private int IsDisabledOrdinal { get; set; }
        private int IsNotForReplicationOrdinal { get; set; }
        private int IsNotTrustedOrdinal { get; set; }
        private int IsSystemNamedOrdinal { get; set; }

        public CheckConstraintFactory(IDataRecord reader)
        {
            SchemaNameOrdinal = reader.GetOrdinal("SchemaName");
            TableNameOrdinal = reader.GetOrdinal("TableName");
            ColumnNameOrdinal = reader.GetOrdinal("ColumnName");
            ObjectNameOrdinal = reader.GetOrdinal("ObjectName");
            DefinitionOrdinal = reader.GetOrdinal("Definition");
            IsTableConstraintOrdinal = reader.GetOrdinal("IsTableConstraint");
            IsDisabledOrdinal = reader.GetOrdinal("IsDisabled");
            IsNotForReplicationOrdinal = reader.GetOrdinal("IsNotForReplication");
            IsNotTrustedOrdinal = reader.GetOrdinal("IsNotTrusted");
            IsSystemNamedOrdinal = reader.GetOrdinal("IsSystemNamed");
        }

        public void CreateCheckConstraint(
            Dictionary<string, UserTable> userTables,
            IDataReader reader)
        {
            var schemaName = Convert.ToString(reader[SchemaNameOrdinal]);
            var tableName = Convert.ToString(reader[TableNameOrdinal]);

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

            var checkConstraint = new CheckConstraint
            {
                UserTable = userTable,
                ColumnName = Convert.ToString(reader[ColumnNameOrdinal]),
                ObjectName = Convert.ToString(reader[ObjectNameOrdinal]),
                Definition = Convert.ToString(reader[DefinitionOrdinal]),
                IsTableConstraint = Convert.ToBoolean(reader[IsTableConstraintOrdinal]),
                IsDisabled = Convert.ToBoolean(reader[IsDisabledOrdinal]),
                IsNotForReplication = Convert.ToBoolean(reader[IsNotForReplicationOrdinal]),
                IsNotTrusted = Convert.ToBoolean(reader[IsNotTrustedOrdinal]),
                IsSystemNamed = Convert.ToBoolean(reader[IsSystemNamedOrdinal])
            };

            userTable.CheckConstraints.Add(checkConstraint);
        }
    }
}
