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
    public static class IdentityColumnsAdapter
    {
        public const string Dot = ".";

        public static void Read(Dictionary<string, UserTable> userTables, IDataReader reader)
        {
            var schemaNameOrdinal = reader.GetOrdinal("SchemaName");
            var tableNameOrdinal = reader.GetOrdinal("TableName");
            var objectNameOrdinal = reader.GetOrdinal("ObjectName");
            var seedValueOrdinal = reader.GetOrdinal("SeedValue");
            var incrementValueOrdinal = reader.GetOrdinal("IncrementValue");
            var isNotForReplicationOrdinal = reader.GetOrdinal("IsNotForReplication");

            while (reader.Read())
            {
                var schemaName = Convert.ToString(reader[schemaNameOrdinal]);
                var tableName = Convert.ToString(reader[tableNameOrdinal]);
                var objectName = Convert.ToString(reader[objectNameOrdinal]);
                var seedValue = Convert.ToInt32(reader[seedValueOrdinal]);
                var incrementValue = Convert.ToInt32(reader[incrementValueOrdinal]);
                var isNotForReplication = Convert.ToBoolean(reader[isNotForReplicationOrdinal]);

                var userTableNamespaceBuilder = new StringBuilder(schemaName.Length + tableName.Length + 1);
                userTableNamespaceBuilder.Append(schemaName).Append(Dot).Append(tableName);

                var userTableNamespace = userTableNamespaceBuilder.ToString();
                if (!userTables.ContainsKey(userTableNamespace))
                    continue;

                var userTable = userTables[userTableNamespace];
                if (userTable == null)
                    continue;

                var identityColumn = new IdentityColumn
                {
                    UserTable = userTable,
                    ObjectName = objectName,
                    SeedValue = seedValue,
                    IncrementValue = incrementValue,
                    IsNotForReplication = isNotForReplication
                };

                userTable.IdentityColumns.Add(identityColumn);
            }
        }

        public static async Task GetAsync(Catalog catalog, Dictionary<string, UserTable> userTables, DbConnection connection, IMetadataScriptFactory metadataScriptFactory)
        {
            using (var command = connection.CreateCommand())
            {
                command.CommandText = metadataScriptFactory.IdentityColumns(catalog.ObjectName);
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
