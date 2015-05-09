using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Meta.Net.Interfaces;
using Meta.Net.Objects;

namespace Meta.Net.Metadata
{
    public static class IndexesAdapter
    {
        public const string Dot = ".";

        public static void Read(Dictionary<string, UserTable> userTables, IDataReader reader)
        {
            var schemaNameOrdinal = reader.GetOrdinal("SchemaName");
            var tableNameOrdinal = reader.GetOrdinal("TableName");
            var columnNameOrdinal = reader.GetOrdinal("ColumnName");
            var objectNameOrdinal = reader.GetOrdinal("ObjectName");
            var fileGroupOrdinal = reader.GetOrdinal("FileGroup");
            var keyOrdinalOrdinal = reader.GetOrdinal("KeyOrdinal");
            var partitionOrdinalOrdinal = reader.GetOrdinal("PartitionOrdinal");
            var isDescendingKeyOrdinal = reader.GetOrdinal("IsDescendingKey");
            var isIncludedColumnOrdinal = reader.GetOrdinal("IsIncludedColumn");
            var ignoreDupKeyOrdinal = reader.GetOrdinal("IgnoreDupKey");
            var isClusteredOrdinal = reader.GetOrdinal("IsClustered");
            var isUniqueOrdinal = reader.GetOrdinal("IsUnique");
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
                var isIncludedColumn = Convert.ToBoolean(reader[isIncludedColumnOrdinal]);
                var ignoreDupKey = Convert.ToBoolean(reader[ignoreDupKeyOrdinal]);
                var isClustered = Convert.ToBoolean(reader[isClusteredOrdinal]);
                var isUnique = Convert.ToBoolean(reader[isUniqueOrdinal]);
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

                var indexNamespaceBuilder = new StringBuilder(userTableNamespace.Length + objectName.Length + 1);
                indexNamespaceBuilder.Append(userTableNamespace).Append(Dot).Append(objectName);
                var indexNamespace = indexNamespaceBuilder.ToString();
                var index = userTable.Indexes[indexNamespace];
                if (index == null)
                {
                    index = new Index
                    {
                        UserTable = userTable,
                        ObjectName = objectName,
                        FileGroup = fileGroup,
                        IgnoreDupKey = ignoreDupKey,
                        IsClustered = isClustered,
                        IsUnique = isUnique, // TODO: See usages: refactor...
                        FillFactor = fillFactor,
                        IsPadded = isPadded,
                        IsDisabled = isDisabled,
                        AllowRowLocks = allowRowLocks,
                        AllowPageLocks = allowPageLocks,
                        IndexType = indexType
                    };

                    userTable.Indexes.Add(index);
                }

                var indexColumn = new IndexColumn
                {
                    Index = index,
                    ObjectName = columnName,
                    IsDescendingKey = isDescendingKey,
                    KeyOrdinal = keyOrdinal,
                    PartitionOrdinal = partitionOrdinal,
                    IsIncludedColumn = isIncludedColumn
                };

                index.IndexColumns.Add(indexColumn);
            }
        }

        public static void Get(Catalog catalog, Dictionary<string, UserTable> userTables, DbConnection connection, IMetadataScriptFactory metadataScriptFactory)
        {
            using (var command = connection.CreateCommand())
            {
                command.CommandText = metadataScriptFactory.Indexes(catalog.ObjectName);
                using (var reader = command.ExecuteReader())
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
        
        public static async Task GetAsync(Catalog catalog, Dictionary<string, UserTable> userTables, DbConnection connection, IMetadataScriptFactory metadataScriptFactory)
        {

            using (var command = connection.CreateCommand())
            {
                command.CommandText = metadataScriptFactory.Indexes(catalog.ObjectName);
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
        
        public static async Task GetAsync(Catalog catalog, Dictionary<string, UserTable> userTables, DbConnection connection, IMetadataScriptFactory metadataScriptFactory, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return;

            using (var command = connection.CreateCommand())
            {
                command.CommandText = metadataScriptFactory.Indexes(catalog.ObjectName);
                using (var reader = await command.ExecuteReaderAsync(cancellationToken))
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
