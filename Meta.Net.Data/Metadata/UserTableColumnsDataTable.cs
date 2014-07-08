using System;
using System.Data;

namespace Meta.Net.Data.Metadata
{
    public class UserTableColumnsDataTable: DataTable
    {
		#region Properties (25) 

        public DataColumn CatalogNameColumn { get; set; }

        public DataColumn CollationColumn { get; set; }

        public DataColumn ColumnOrdinalColumn { get; set; }

        public int Count
        {
            get
            {
                return Rows.Count;
            }
        }

        public DataColumn DataTypeColumn { get; set; }

        public DataColumn DescriptionColumn { get; set; }

        public DataColumn HasDefaultColumn { get; set; }

        public DataColumn HasForeignKeyColumn { get; set; }

        public DataColumn HasXmlCollectionColumn { get; set; }

        public DataColumn IsAnsiPaddedColumn { get; set; }

        public DataColumn IsAssemblyTypeColumn { get; set; }

        public DataColumn IsComputedColumn { get; set; }

        public DataColumn IsFileStreamColumn { get; set; }

        public DataColumn IsIdentityColumn { get; set; }

        public DataColumn IsNullableColumn { get; set; }

        public DataColumn IsRowGuidColumnColumn { get; set; }

        public DataColumn IsUserDefinedColumn { get; set; }

        public DataColumn IsXmlDocumentColumn { get; set; }

        public DataColumn MaxLengthColumn { get; set; }

        public DataColumn NamespaceColumn { get; set; }

        public DataColumn ObjectNameColumn { get; set; }

        public DataColumn PrecisionColumn { get; set; }

        public DataColumn ScaleColumn { get; set; }

        public DataColumn SchemaNameColumn { get; set; }

        public DataColumn TableNameColumn { get; set; }

        public UserTableColumnsRow this[int index]
        {
            get
            {
                return ((UserTableColumnsRow)(Rows[index]));
            }
        }

		#endregion Properties 

		#region Constructors (1) 

        public UserTableColumnsDataTable()
        {
            TableName = "UserTableColumns";

            CatalogNameColumn = new DataColumn("CatalogName", typeof(string), null, MappingType.Element);
            CollationColumn = new DataColumn("Collation", typeof(string), null, MappingType.Element);
            ColumnOrdinalColumn = new DataColumn("ColumnOrdinal", typeof(int), null, MappingType.Element);
            DataTypeColumn = new DataColumn("DataType", typeof(string), null, MappingType.Element);
            DescriptionColumn = new DataColumn("Description", typeof(string), null, MappingType.Element);
            HasDefaultColumn = new DataColumn("HasDefault", typeof(bool), null, MappingType.Element);
            HasForeignKeyColumn = new DataColumn("HasForeignKey", typeof(bool), null, MappingType.Element);
            HasXmlCollectionColumn = new DataColumn("HasXmlCollection", typeof(bool), null, MappingType.Element);
            IsAnsiPaddedColumn = new DataColumn("IsAnsiPadded", typeof(bool), null, MappingType.Element);
            IsAssemblyTypeColumn = new DataColumn("IsAssemblyType", typeof(bool), null, MappingType.Element);
            IsComputedColumn = new DataColumn("IsComputed", typeof(bool), null, MappingType.Element);
            IsFileStreamColumn = new DataColumn("IsFileStream", typeof(bool), null, MappingType.Element);
            IsIdentityColumn = new DataColumn("IsIdentity", typeof(bool), null, MappingType.Element);
            IsNullableColumn = new DataColumn("IsNullable", typeof(bool), null, MappingType.Element);
            IsRowGuidColumnColumn = new DataColumn("IsRowGuidColumn", typeof(bool), null, MappingType.Element);
            IsUserDefinedColumn = new DataColumn("IsUserDefined", typeof(bool), null, MappingType.Element);
            IsXmlDocumentColumn = new DataColumn("IsXmlDocument", typeof(bool), null, MappingType.Element);
            MaxLengthColumn = new DataColumn("MaxLength", typeof(Int64), null, MappingType.Element);
            NamespaceColumn = new DataColumn("Namespace", typeof(string), null, MappingType.Element);
            ObjectNameColumn = new DataColumn("ObjectName", typeof(string), null, MappingType.Element);
            PrecisionColumn = new DataColumn("Precision", typeof(int), null, MappingType.Element);
            ScaleColumn = new DataColumn("Scale", typeof(int), null, MappingType.Element);
            SchemaNameColumn = new DataColumn("SchemaName", typeof(string), null, MappingType.Element);
            TableNameColumn = new DataColumn("TableName", typeof(string), null, MappingType.Element);

            base.Columns.Add(CatalogNameColumn);
            base.Columns.Add(CollationColumn);
            base.Columns.Add(ColumnOrdinalColumn);
            base.Columns.Add(DataTypeColumn);
            base.Columns.Add(DescriptionColumn);
            base.Columns.Add(HasDefaultColumn);
            base.Columns.Add(HasForeignKeyColumn);
            base.Columns.Add(HasXmlCollectionColumn);
            base.Columns.Add(IsAnsiPaddedColumn);
            base.Columns.Add(IsAssemblyTypeColumn);
            base.Columns.Add(IsComputedColumn);
            base.Columns.Add(IsFileStreamColumn);
            base.Columns.Add(IsIdentityColumn);
            base.Columns.Add(IsNullableColumn);
            base.Columns.Add(IsRowGuidColumnColumn);
            base.Columns.Add(IsUserDefinedColumn);
            base.Columns.Add(IsXmlDocumentColumn);
            base.Columns.Add(MaxLengthColumn);
            base.Columns.Add(NamespaceColumn);
            base.Columns.Add(ObjectNameColumn);
            base.Columns.Add(PrecisionColumn);
            base.Columns.Add(ScaleColumn);
            base.Columns.Add(SchemaNameColumn);
            base.Columns.Add(TableNameColumn);
        }

		#endregion Constructors 

		#region Methods (5) 

		#region Public Methods (3) 

        public void AddTableColumnsRow(UserTableColumnsRow row)
        {
            Rows.Add(row);
        }

        public UserTableColumnsRow NewTableColumnsRow()
        {
            return ((UserTableColumnsRow)(NewRow()));
        }

        public void RemoveTableColumnsRow(UserTableColumnsRow row)
        {
            Rows.Remove(row);
        }

		#endregion Public Methods 
		#region Protected Methods (2) 

        protected override Type GetRowType()
        {
            return typeof(UserTableColumnsRow);
        }

        protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
        {
            return new UserTableColumnsRow(builder);
        }

		#endregion Protected Methods 

		#endregion Methods 
    }
}
