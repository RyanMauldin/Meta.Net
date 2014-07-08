using System;
using System.Data;

namespace Meta.Net.Data.Metadata
{
    public class UserDefinedDataTypesDataTable: DataTable
    {
		#region Properties (16) 

        public DataColumn CatalogNameColumn { get; set; }

        public DataColumn CollationColumn { get; set; }

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

        public DataColumn IsAssemblyTypeColumn { get; set; }

        public DataColumn IsNullableColumn { get; set; }

        public DataColumn IsUserDefinedColumn { get; set; }

        public DataColumn MaxLengthColumn { get; set; }

        public DataColumn NamespaceColumn { get; set; }

        public DataColumn ObjectNameColumn { get; set; }

        public DataColumn PrecisionColumn { get; set; }

        public DataColumn ScaleColumn { get; set; }

        public DataColumn SchemaNameColumn { get; set; }

        public UserDefinedDataTypesRow this[int index]
        {
            get
            {
                return ((UserDefinedDataTypesRow)(Rows[index]));
            }
        }

		#endregion Properties 

		#region Constructors (1) 

        public UserDefinedDataTypesDataTable()
        {
            TableName = "UserDefinedDataTypes";

            CatalogNameColumn = new DataColumn("CatalogName", typeof(string), null, MappingType.Element);
            CollationColumn = new DataColumn("Collation", typeof(string), null, MappingType.Element);
            DataTypeColumn = new DataColumn("DataType", typeof(string), null, MappingType.Element);
            DescriptionColumn = new DataColumn("Description", typeof(string), null, MappingType.Element);
            HasDefaultColumn = new DataColumn("HasDefault", typeof(bool), null, MappingType.Element);
            IsAssemblyTypeColumn = new DataColumn("IsAssemblyType", typeof(bool), null, MappingType.Element);
            IsNullableColumn = new DataColumn("IsNullable", typeof(bool), null, MappingType.Element);
            IsUserDefinedColumn = new DataColumn("IsUserDefined", typeof(bool), null, MappingType.Element);
            MaxLengthColumn = new DataColumn("MaxLength", typeof(short), null, MappingType.Element);
            NamespaceColumn = new DataColumn("Namespace", typeof(string), null, MappingType.Element);
            ObjectNameColumn = new DataColumn("ObjectName", typeof(string), null, MappingType.Element);
            PrecisionColumn = new DataColumn("Precision", typeof(int), null, MappingType.Element);
            ScaleColumn = new DataColumn("Scale", typeof(int), null, MappingType.Element);
            SchemaNameColumn = new DataColumn("SchemaName", typeof(string), null, MappingType.Element);

            Columns.Add(CatalogNameColumn);
            Columns.Add(CollationColumn);
            Columns.Add(DataTypeColumn);
            Columns.Add(DescriptionColumn);
            Columns.Add(HasDefaultColumn);
            Columns.Add(IsAssemblyTypeColumn);
            Columns.Add(IsNullableColumn);
            Columns.Add(IsUserDefinedColumn);
            Columns.Add(MaxLengthColumn);
            Columns.Add(NamespaceColumn);
            Columns.Add(ObjectNameColumn);
            Columns.Add(PrecisionColumn);
            Columns.Add(ScaleColumn);
            Columns.Add(SchemaNameColumn);

        }

		#endregion Constructors 

		#region Methods (5) 

		#region Public Methods (3) 

        public void AddTableColumnsRow(UserDefinedDataTypesRow row)
        {
            Rows.Add(row);
        }

        public UserDefinedDataTypesRow NewTableColumnsRow()
        {
            return ((UserDefinedDataTypesRow)(NewRow()));
        }

        public void RemoveTableColumnsRow(UserDefinedDataTypesRow row)
        {
            Rows.Remove(row);
        }

		#endregion Public Methods 
		#region Protected Methods (2) 

        protected override Type GetRowType()
        {
            return typeof(UserDefinedDataTypesRow);
        }

        protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
        {
            return new UserDefinedDataTypesRow(builder);
        }

		#endregion Protected Methods 

		#endregion Methods 
    }
}
