using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Meta.Net.Objects;

namespace Meta.Net.Metadata.Factories
{
    internal class DefaultConstraintFactory
    {
        private int SchemaNameOrdinal { get; set; }
        private int TableNameOrdinal { get; set; }
        private int ColumnNameOrdinal { get; set; }
        private int ObjectNameOrdinal { get; set; }
        private int DefinitionOrdinal { get; set; }
        private int IsSystemNamedOrdinal { get; set; }

        public DefaultConstraintFactory(IDataRecord reader)
        {
            SchemaNameOrdinal = reader.GetOrdinal("SchemaName");
            TableNameOrdinal = reader.GetOrdinal("TableName");
            ColumnNameOrdinal = reader.GetOrdinal("ColumnName");
            ObjectNameOrdinal = reader.GetOrdinal("ObjectName");
            DefinitionOrdinal = reader.GetOrdinal("Definition");
            IsSystemNamedOrdinal = reader.GetOrdinal("IsSystemNamed");
        }

        public void CreateDefaultConstraint(
            Dictionary<string, UserTable> userTables,
            IDataRecord reader)
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

            var defaultConstraint = new DefaultConstraint
            {
                UserTable = userTable,
                ColumnName = Convert.ToString(reader[ColumnNameOrdinal]),
                ObjectName = Convert.ToString(reader[ObjectNameOrdinal]),
                Definition = Convert.ToString(reader[DefinitionOrdinal]),
                IsSystemNamed = Convert.ToBoolean(reader[IsSystemNamedOrdinal])
            };

            userTable.DefaultConstraints.Add(defaultConstraint);
        }
    }
}
