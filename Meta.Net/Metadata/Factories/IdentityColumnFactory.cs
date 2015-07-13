using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Meta.Net.Objects;

namespace Meta.Net.Metadata.Factories
{
    internal class IdentityColumnFactory
    {
        private int SchemaNameOrdinal { get; set; }
        private int TableNameOrdinal { get; set; }
        private int ObjectNameOrdinal { get; set; }
        private int SeedValueOrdinal { get; set; }
        private int IncrementValueOrdinal { get; set; }
        private int IsNotForReplicationOrdinal { get; set; }

        public IdentityColumnFactory(IDataRecord reader)
        {
            SchemaNameOrdinal = reader.GetOrdinal("SchemaName");
            TableNameOrdinal = reader.GetOrdinal("TableName");
            ObjectNameOrdinal = reader.GetOrdinal("ObjectName");
            SeedValueOrdinal = reader.GetOrdinal("SeedValue");
            IncrementValueOrdinal = reader.GetOrdinal("IncrementValue");
            IsNotForReplicationOrdinal = reader.GetOrdinal("IsNotForReplication");
        }

        public void CreateIdentityColumn(
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

            var identityColumn = new IdentityColumn
            {
                UserTable = userTable,
                ObjectName = Convert.ToString(reader[ObjectNameOrdinal]),
                SeedValue = Convert.ToInt32(reader[SeedValueOrdinal]),
                IncrementValue = Convert.ToInt32(reader[IncrementValueOrdinal]),
                IsNotForReplication = Convert.ToBoolean(reader[IsNotForReplicationOrdinal])
            };

            userTable.IdentityColumns.Add(identityColumn);
        }
    }
}
