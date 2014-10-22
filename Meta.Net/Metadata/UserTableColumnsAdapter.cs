using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using Meta.Net.Interfaces;
using Meta.Net.Objects;

namespace Meta.Net.Metadata
{
    public static class UserTableColumnsAdapter
    {
        public const string Dot = ".";

        public static void Read(Dictionary<string, UserTable> userTables, IDataReader reader)
        {
            var schemaNameOrdinal = reader.GetOrdinal("SchemaName");
            var tableNameOrdinal = reader.GetOrdinal("TableName");
            var objectNameOrdinal = reader.GetOrdinal("ObjectName");
            var columnOrdinalOrdinal = reader.GetOrdinal("ColumnOrdinal");
            var dataTypeOrdinal = reader.GetOrdinal("DataType");
            var maxLengthOrdinal = reader.GetOrdinal("MaxLength");
            var precisionOrdinal = reader.GetOrdinal("Precision");
            var scaleOrdinal = reader.GetOrdinal("Scale");
            var collationOrdinal = reader.GetOrdinal("Collation");
            var hasDefaultOrdinal = reader.GetOrdinal("HasDefault");
            var hasForeignKeyOrdinal = reader.GetOrdinal("HasForeignKey");
            var hasXmlCollectionOrdinal = reader.GetOrdinal("HasXmlCollection");
            var isUserDefinedOrdinal = reader.GetOrdinal("IsUserDefined");
            var isAssemblyTypeOrdinal = reader.GetOrdinal("IsAssemblyType");
            var isNullableOrdinal = reader.GetOrdinal("IsNullable");
            var isAnsiPaddedOrdinal = reader.GetOrdinal("IsAnsiPadded");
            var isRowGuidColumnOrdinal = reader.GetOrdinal("IsRowGuidColumn");
            var isIdentityOrdinal = reader.GetOrdinal("IsIdentity");
            var isComputedOrdinal = reader.GetOrdinal("IsComputed");
            var isFileStreamOrdinal = reader.GetOrdinal("IsFileStream");
            var isXmlDocumentOrdinal = reader.GetOrdinal("IsXmlDocument");

            while (reader.Read())
            {
                var schemaName = Convert.ToString(reader[schemaNameOrdinal]);
                var tableName = Convert.ToString(reader[tableNameOrdinal]);
                var objectName = Convert.ToString(reader[objectNameOrdinal]);
                var columnOrdinal = Convert.ToInt32(reader[columnOrdinalOrdinal]);
                var dataType = Convert.ToString(reader[dataTypeOrdinal]);
                var maxLength = Convert.ToInt64(reader[maxLengthOrdinal]);
                var precision = Convert.ToInt32(reader[precisionOrdinal]);
                var scale = Convert.ToInt32(reader[scaleOrdinal]);
                var collation = Convert.ToString(reader[collationOrdinal]);
                var hasDefault = Convert.ToBoolean(reader[hasDefaultOrdinal]);
                var hasForeignKey = Convert.ToBoolean(reader[hasForeignKeyOrdinal]);
                var hasXmlCollection = Convert.ToBoolean(reader[hasXmlCollectionOrdinal]);
                var isUserDefined = Convert.ToBoolean(reader[isUserDefinedOrdinal]);
                var isAssemblyType = Convert.ToBoolean(reader[isAssemblyTypeOrdinal]);
                var isNullable = Convert.ToBoolean(reader[isNullableOrdinal]);
                var isAnsiPadded = Convert.ToBoolean(reader[isAnsiPaddedOrdinal]);
                var isRowGuidColumn = Convert.ToBoolean(reader[isRowGuidColumnOrdinal]);
                var isIdentity = Convert.ToBoolean(reader[isIdentityOrdinal]);
                var isComputed = Convert.ToBoolean(reader[isComputedOrdinal]);
                var isFileStream = Convert.ToBoolean(reader[isFileStreamOrdinal]);
                var isXmlDocument = Convert.ToBoolean(reader[isXmlDocumentOrdinal]);

                //var commonDataType = DataTypes.GetCommonDataType(dataType);

                var userTableNamespaceBuilder = new StringBuilder(schemaName.Length + tableName.Length + 1);
                userTableNamespaceBuilder.Append(schemaName).Append(Dot).Append(tableName);

                var userTableNamespace = userTableNamespaceBuilder.ToString();
                if (!userTables.ContainsKey(userTableNamespace))
                    continue;

                var userTable = userTables[userTableNamespace];
                if (userTable == null)
                    continue;

                var userTableColumn = new UserTableColumn
                {
                    UserTable = userTable,
                    ObjectName = objectName,
                    ColumnOrdinal = columnOrdinal, // Note: naming scheme conflict, this is correct
                    DataType = dataType,
                    //CommonDataType = commonDataType,
                    MaxLength = maxLength,
                    Precision = precision,
                    Scale = scale,
                    Collation = collation,
                    HasDefault = hasDefault,
                    HasForeignKey = hasForeignKey,
                    HasXmlCollection = hasXmlCollection,
                    IsUserDefined = isUserDefined,
                    IsAssemblyType = isAssemblyType,
                    IsNullable = isNullable,
                    IsAnsiPadded = isAnsiPadded,
                    IsRowGuidColumn = isRowGuidColumn,
                    IsIdentity = isIdentity,
                    IsComputed = isComputed,
                    IsFileStream = isFileStream,
                    IsXmlDocument = isXmlDocument
                };

                userTable.UserTableColumns.Add(userTableColumn);
            }
        }

        public static void Get(Catalog catalog, Dictionary<string, UserTable> userTables, DbConnection connection, IMetadataScriptFactory metadataScriptFactory)
        {
            using (var command = connection.CreateCommand())
            {
                command.CommandText = metadataScriptFactory.UserTableColumns(catalog.ObjectName);
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
    }
}
