using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Meta.Net.Objects;

namespace Meta.Net.Metadata.Factories
{
    internal class ComputedColumnFactory
    {
        private int SchemaNameOrdinal { get; set; }
        private int TableNameOrdinal { get; set; }
        private int ObjectNameOrdinal { get; set; }
        private int DefinitionOrdinal { get; set; }
        private int IsPersistedOrdinal { get; set; }
        private int IsNullableOrdinal { get; set; }

        public ComputedColumnFactory(IDataRecord reader)
        {
            SchemaNameOrdinal = reader.GetOrdinal("SchemaName");
            TableNameOrdinal = reader.GetOrdinal("TableName");
            ObjectNameOrdinal = reader.GetOrdinal("ObjectName");
            DefinitionOrdinal = reader.GetOrdinal("Definition");
            IsPersistedOrdinal = reader.GetOrdinal("IsPersisted");
            IsNullableOrdinal = reader.GetOrdinal("IsNullable");
        }

        public void CreateComputedColumn(
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

            var computedColumn = new ComputedColumn
            {
                UserTable = userTable,
                ObjectName = Convert.ToString(reader[ObjectNameOrdinal]),
                Definition = Convert.ToString(reader[DefinitionOrdinal]),
                IsPersisted = Convert.ToBoolean(reader[IsPersistedOrdinal]),
                IsNullable = Convert.ToBoolean(reader[IsNullableOrdinal])
            };

            userTable.ComputedColumns.Add(computedColumn);
        }
    }
}
