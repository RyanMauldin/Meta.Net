using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Meta.Net.Objects;

namespace Meta.Net.Metadata.Factories
{
    internal class UserTableColumnFactory
    {
        private int SchemaNameOrdinal { get; set; }
        private int TableNameOrdinal { get; set; }
        private int ObjectNameOrdinal { get; set; }
        private int ColumnOrdinalOrdinal { get; set; }
        private int DataTypeOrdinal { get; set; }
        private int MaxLengthOrdinal { get; set; }
        private int PrecisionOrdinal { get; set; }
        private int ScaleOrdinal { get; set; }
        private int CollationOrdinal { get; set; }
        private int HasDefaultOrdinal { get; set; }
        private int HasForeignKeyOrdinal { get; set; }
        private int HasXmlCollectionOrdinal { get; set; }
        private int IsUserDefinedOrdinal { get; set; }
        private int IsAssemblyTypeOrdinal { get; set; }
        private int IsNullableOrdinal { get; set; }
        private int IsAnsiPaddedOrdinal { get; set; }
        private int IsRowGuidColumnOrdinal { get; set; }
        private int IsIdentityOrdinal { get; set; }
        private int IsComputedOrdinal { get; set; }
        private int IsFileStreamOrdinal { get; set; }
        private int IsXmlDocumentOrdinal { get; set; }

        public UserTableColumnFactory(IDataRecord reader)
        {
            SchemaNameOrdinal = reader.GetOrdinal("SchemaName");
            TableNameOrdinal = reader.GetOrdinal("TableName");
            ObjectNameOrdinal = reader.GetOrdinal("ObjectName");
            ColumnOrdinalOrdinal = reader.GetOrdinal("ColumnOrdinal");
            DataTypeOrdinal = reader.GetOrdinal("DataType");
            MaxLengthOrdinal = reader.GetOrdinal("MaxLength");
            PrecisionOrdinal = reader.GetOrdinal("Precision");
            ScaleOrdinal = reader.GetOrdinal("Scale");
            CollationOrdinal = reader.GetOrdinal("Collation");
            HasDefaultOrdinal = reader.GetOrdinal("HasDefault");
            HasForeignKeyOrdinal = reader.GetOrdinal("HasForeignKey");
            HasXmlCollectionOrdinal = reader.GetOrdinal("HasXmlCollection");
            IsUserDefinedOrdinal = reader.GetOrdinal("IsUserDefined");
            IsAssemblyTypeOrdinal = reader.GetOrdinal("IsAssemblyType");
            IsNullableOrdinal = reader.GetOrdinal("IsNullable");
            IsAnsiPaddedOrdinal = reader.GetOrdinal("IsAnsiPadded");
            IsRowGuidColumnOrdinal = reader.GetOrdinal("IsRowGuidColumn");
            IsIdentityOrdinal = reader.GetOrdinal("IsIdentity");
            IsComputedOrdinal = reader.GetOrdinal("IsComputed");
            IsFileStreamOrdinal = reader.GetOrdinal("IsFileStream");
            IsXmlDocumentOrdinal = reader.GetOrdinal("IsXmlDocument");
        }

        public void CreateUserTableColumn(
            Dictionary<string, UserTable> userTables,
            IDataRecord reader)
        {
            var schemaName = Convert.ToString(reader[SchemaNameOrdinal]);
            var tableName = Convert.ToString(reader[TableNameOrdinal]);

            //var commonDataType = DataTypes.GetCommonDataType(dataType);

            var userTableNamespaceBuilder = new StringBuilder(schemaName.Length + tableName.Length + 1);
            userTableNamespaceBuilder.Append(schemaName).Append(Constants.Dot).Append(tableName);

            var userTableNamespace = userTableNamespaceBuilder.ToString();
            if (!userTables.ContainsKey(userTableNamespace))
                return;

            var userTable = userTables[userTableNamespace];
            if (userTable == null)
                return;

            var userTableColumn = new UserTableColumn
            {
                UserTable = userTable,
                ObjectName = Convert.ToString(reader[ObjectNameOrdinal]),
                ColumnOrdinal = Convert.ToInt32(reader[ColumnOrdinalOrdinal]), // Note: naming scheme conflict, this is correct
                DataType = Convert.ToString(reader[DataTypeOrdinal]),
                //CommonDataType = commonDataType,
                MaxLength = Convert.ToInt64(reader[MaxLengthOrdinal]),
                Precision = Convert.ToInt32(reader[PrecisionOrdinal]),
                Scale = Convert.ToInt32(reader[ScaleOrdinal]),
                Collation = Convert.ToString(reader[CollationOrdinal]),
                HasDefault = Convert.ToBoolean(reader[HasDefaultOrdinal]),
                HasForeignKey = Convert.ToBoolean(reader[HasForeignKeyOrdinal]),
                HasXmlCollection = Convert.ToBoolean(reader[HasXmlCollectionOrdinal]),
                IsUserDefined = Convert.ToBoolean(reader[IsUserDefinedOrdinal]),
                IsAssemblyType = Convert.ToBoolean(reader[IsAssemblyTypeOrdinal]),
                IsNullable = Convert.ToBoolean(reader[IsNullableOrdinal]),
                IsAnsiPadded = Convert.ToBoolean(reader[IsAnsiPaddedOrdinal]),
                IsRowGuidColumn = Convert.ToBoolean(reader[IsRowGuidColumnOrdinal]),
                IsIdentity = Convert.ToBoolean(reader[IsIdentityOrdinal]),
                IsComputed = Convert.ToBoolean(reader[IsComputedOrdinal]),
                IsFileStream = Convert.ToBoolean(reader[IsFileStreamOrdinal]),
                IsXmlDocument = Convert.ToBoolean(reader[IsXmlDocumentOrdinal])
            };

            userTable.UserTableColumns.Add(userTableColumn);
        }
    }
}
