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
    public static class PrimaryKeysAdapter
    {
        public const string Dot = ".";

        public static void Read(Dictionary<string, UserTable> userTables, IDataReader reader, Dictionary<string, PrimaryKeyColumn> primaryKeyColumns)
        {
            var schemaNameOrdinal = reader.GetOrdinal("SchemaName");
            var tableNameOrdinal = reader.GetOrdinal("TableName");
            var columnNameOrdinal = reader.GetOrdinal("ColumnName");
            var objectNameOrdinal = reader.GetOrdinal("ObjectName");
            var fileGroupOrdinal = reader.GetOrdinal("FileGroup");
            var keyOrdinalOrdinal = reader.GetOrdinal("KeyOrdinal");
            var partitionOrdinalOrdinal = reader.GetOrdinal("PartitionOrdinal");
            var isDescendingKeyOrdinal = reader.GetOrdinal("IsDescendingKey");
            var ignoreDupKeyOrdinal = reader.GetOrdinal("IgnoreDupKey");
            var isClusteredOrdinal = reader.GetOrdinal("IsClustered");
            var fillFactorOrdinal = reader.GetOrdinal("FillFactor");
            var isPaddedOrdinal = reader.GetOrdinal("IsPadded");
            var isDisabledOrdinal = reader.GetOrdinal("IsDisabled");
            var allowRowLocksOrdinal = reader.GetOrdinal("AllowRowLocks");
            var allowPageLocksOrdinal = reader.GetOrdinal("AllowPageLocks");
            var indexTypeOrdinal = reader.GetOrdinal("IndexType");

            while (reader.Read())
            {
                var schemaName = Convert.ToString(reader[schemaNameOrdinal]);
                var tableName = Convert.ToString(reader[tableNameOrdinal]);
                var columnName = Convert.ToString(reader[columnNameOrdinal]);
                var objectName = Convert.ToString(reader[objectNameOrdinal]);
                var fileGroup = Convert.ToString(reader[fileGroupOrdinal]);
                var keyOrdinal = Convert.ToInt32(reader[keyOrdinalOrdinal]);
                var partitionOrdinal = Convert.ToInt32(reader[partitionOrdinalOrdinal]);
                var isDescendingKey = Convert.ToBoolean(reader[isDescendingKeyOrdinal]);
                var ignoreDupKey = Convert.ToBoolean(reader[ignoreDupKeyOrdinal]);
                var isClustered = Convert.ToBoolean(reader[isClusteredOrdinal]);
                var fillFactor = Convert.ToInt32(reader[fillFactorOrdinal]);
                var isPadded = Convert.ToBoolean(reader[isPaddedOrdinal]);
                var isDisabled = Convert.ToBoolean(reader[isDisabledOrdinal]);
                var allowRowLocks = Convert.ToBoolean(reader[allowRowLocksOrdinal]);
                var allowPageLocks = Convert.ToBoolean(reader[allowPageLocksOrdinal]);
                var indexType = Convert.ToString(reader[indexTypeOrdinal]);

                var userTableNamespaceBuilder = new StringBuilder(schemaName.Length + tableName.Length + 1);
                userTableNamespaceBuilder.Append(schemaName).Append(Dot).Append(tableName);

                var userTableNamespace = userTableNamespaceBuilder.ToString();
                if (!userTables.ContainsKey(userTableNamespace))
                    continue;

                var userTable = userTables[userTableNamespace];
                if (userTable == null)
                    continue;

                var primaryKeyNamespaceBuilder = new StringBuilder(userTableNamespace.Length + objectName.Length + 1);
                primaryKeyNamespaceBuilder.Append(userTableNamespace).Append(Dot).Append(objectName);
                var primaryKeyNamespace = primaryKeyNamespaceBuilder.ToString();
                var primaryKey = userTable.PrimaryKeys[primaryKeyNamespace];
                if (primaryKey == null)
                {
                    primaryKey = new PrimaryKey
                    {
                        UserTable = userTable,
                        ObjectName = objectName,
                        FileGroup = fileGroup,
                        IgnoreDupKey = ignoreDupKey,
                        IsClustered = isClustered,
                        FillFactor = fillFactor,
                        IsPadded = isPadded,
                        IsDisabled = isDisabled,
                        AllowRowLocks = allowRowLocks,
                        AllowPageLocks = allowPageLocks,
                        IndexType = indexType
                    };

                    userTable.PrimaryKeys.Add(primaryKey);
                }

                // IsIncludedColumn should always be false for a primary key.
                var primaryKeyColumn = new PrimaryKeyColumn
                {
                    PrimaryKey = primaryKey,
                    ObjectName = columnName,
                    IsDescendingKey = isDescendingKey,
                    KeyOrdinal = keyOrdinal,
                    PartitionOrdinal = partitionOrdinal
                };

                primaryKey.PrimaryKeyColumns.Add(primaryKeyColumn);
                primaryKeyColumns.Add(primaryKeyColumn.Namespace, primaryKeyColumn);
            }
        }

        public static async Task<Dictionary<string, PrimaryKeyColumn>> GetAsync(Catalog catalog, Dictionary<string, UserTable> userTables, DbConnection connection, IMetadataScriptFactory metadataScriptFactory)
        {
            var primaryKeyColumns = new Dictionary<string, PrimaryKeyColumn>(StringComparer.OrdinalIgnoreCase);
            using (var command = connection.CreateCommand())
            {
                command.CommandText = metadataScriptFactory.PrimaryKeys(catalog.ObjectName);
                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (!reader.HasRows)
                    {
                        reader.Close();
                        return primaryKeyColumns;
                    }

                    Read(userTables, reader, primaryKeyColumns);

                    reader.Close();
                }
            }

            return primaryKeyColumns;
        }
    }
}
