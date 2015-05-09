using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Meta.Net.Interfaces;
using Meta.Net.Objects;
using UniqueConstraint = Meta.Net.Objects.UniqueConstraint;

namespace Meta.Net.Metadata
{
    public static class UniqueConstraintsAdapter
    {
        public const string Dot = ".";

        public static void Read(Dictionary<string, UserTable> userTables, IDataReader reader, Dictionary<string, UniqueConstraintColumn> uniqueConstraintColumns)
        {
            var schemaNameOrdinal = reader.GetOrdinal("SchemaName");
            var tableNameOrdinal = reader.GetOrdinal("TableName");
            var columnNameOrdinal = reader.GetOrdinal("ColumnName");
            var objectNameOrdinal = reader.GetOrdinal("ObjectName");
            var fileGroupOrdinal = reader.GetOrdinal("FileGroup");
            var keyOrdinalOrdinal = reader.GetOrdinal("KeyOrdinal");
            var partitionOrdinalOrdinal = reader.GetOrdinal("PartitionOrdinal");
            var isClusteredOrdinal = reader.GetOrdinal("IsClustered");
            var isDescendingKeyOrdinal = reader.GetOrdinal("IsDescendingKey");
            var ignoreDupKeyOrdinal = reader.GetOrdinal("IgnoreDupKey");
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
                var isClustered = Convert.ToBoolean(reader[isClusteredOrdinal]);
                var isDescendingKey = Convert.ToBoolean(reader[isDescendingKeyOrdinal]);
                var ignoreDupKey = Convert.ToBoolean(reader[ignoreDupKeyOrdinal]);
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

                var uniqueConstraintNamespaceBuilder = new StringBuilder(userTableNamespace.Length + objectName.Length + 1);
                uniqueConstraintNamespaceBuilder.Append(userTableNamespace).Append(Dot).Append(objectName);
                var uniqueConstraintNamespace = uniqueConstraintNamespaceBuilder.ToString();
                var uniqueConstraint = userTable.UniqueConstraints[uniqueConstraintNamespace];
                if (uniqueConstraint == null)
                {
                    uniqueConstraint = new UniqueConstraint
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
                        IndexType = indexType // TODO: Remove this if possible... check other index code logic for usage (Mysql has BTREE, FULLTEXT, etc..., SQL Server doesn't)
                    };

                    userTable.UniqueConstraints.Add(uniqueConstraint);
                }

                var uniqueConstraintColumn = new UniqueConstraintColumn
                {
                    UniqueConstraint = uniqueConstraint,
                    ObjectName = columnName,
                    IsDescendingKey = isDescendingKey,
                    KeyOrdinal = keyOrdinal,
                    PartitionOrdinal = partitionOrdinal
                };

                uniqueConstraint.UniqueConstraintColumns.Add(uniqueConstraintColumn);
                uniqueConstraintColumns.Add(uniqueConstraintColumn.Namespace, uniqueConstraintColumn);
            }
        }

        public static Dictionary<string, UniqueConstraintColumn> Get(Catalog catalog, Dictionary<string, UserTable> userTables, DbConnection connection, IMetadataScriptFactory metadataScriptFactory)
        {
            var uniqueConstraintColumns = new Dictionary<string, UniqueConstraintColumn>(StringComparer.OrdinalIgnoreCase);
            using (var command = connection.CreateCommand())
            {
                command.CommandText = metadataScriptFactory.UniqueConstraints(catalog.ObjectName);
                using (var reader = command.ExecuteReader())
                {
                    if (!reader.HasRows)
                    {
                        reader.Close();
                        return uniqueConstraintColumns;
                    }

                    Read(userTables, reader, uniqueConstraintColumns);

                    reader.Close();
                }
            }

            return uniqueConstraintColumns;
        }
        
        public static async Task<Dictionary<string, UniqueConstraintColumn>> GetAsync(Catalog catalog, Dictionary<string, UserTable> userTables, DbConnection connection, IMetadataScriptFactory metadataScriptFactory)
        {
            var uniqueConstraintColumns = new Dictionary<string, UniqueConstraintColumn>(StringComparer.OrdinalIgnoreCase);

            using (var command = connection.CreateCommand())
            {
                command.CommandText = metadataScriptFactory.UniqueConstraints(catalog.ObjectName);
                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (!reader.HasRows)
                    {
                        reader.Close();
                        return uniqueConstraintColumns;
                    }

                    Read(userTables, reader, uniqueConstraintColumns);

                    reader.Close();
                }
            }

            return uniqueConstraintColumns;
        }
        
        public static async Task<Dictionary<string, UniqueConstraintColumn>> GetAsync(Catalog catalog, Dictionary<string, UserTable> userTables, DbConnection connection, IMetadataScriptFactory metadataScriptFactory, CancellationToken cancellationToken)
        {
            var uniqueConstraintColumns = new Dictionary<string, UniqueConstraintColumn>(StringComparer.OrdinalIgnoreCase);
            if (cancellationToken.IsCancellationRequested)
                return uniqueConstraintColumns;

            using (var command = connection.CreateCommand())
            {
                command.CommandText = metadataScriptFactory.UniqueConstraints(catalog.ObjectName);
                using (var reader = await command.ExecuteReaderAsync(cancellationToken))
                {
                    if (!reader.HasRows)
                    {
                        reader.Close();
                        return uniqueConstraintColumns;
                    }

                    Read(userTables, reader, uniqueConstraintColumns);

                    reader.Close();
                }
            }

            return uniqueConstraintColumns;
        }
    }
}
