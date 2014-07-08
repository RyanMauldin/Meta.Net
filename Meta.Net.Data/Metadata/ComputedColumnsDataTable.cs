using System;
using System.Data;

namespace Meta.Net.Data.Metadata
{
    public class ComputedColumnsDataTable: DataTable
    {
		#region Properties (10) 

        public DataColumn CatalogNameColumn { get; set; }

        public int Count
        {
            get
            {
                return Rows.Count;
            }
        }

        public DataColumn DefinitionColumn { get; set; }

        public DataColumn DescriptionColumn { get; set; }

        public DataColumn IsNullableColumn { get; set; }

        public DataColumn IsPersistedColumn { get; set; }

        public DataColumn NamespaceColumn { get; set; }

        public DataColumn ObjectNameColumn { get; set; }

        public DataColumn SchemaNameColumn { get; set; }

        public DataColumn TableNameColumn { get; set; }

        public ComputedColumnsRow this[int index]
        {
            get
            {
                return ((ComputedColumnsRow)(Rows[index]));
            }
        }

		#endregion Properties 

		#region Constructors (1) 

        public ComputedColumnsDataTable()
        {
            TableName = "ComputedColumns";

            CatalogNameColumn = new DataColumn("CatalogName", typeof(string), null, MappingType.Element);
            DefinitionColumn = new DataColumn("Definition", typeof(string), null, MappingType.Element);
            DescriptionColumn = new DataColumn("Description", typeof(string), null, MappingType.Element);
            IsNullableColumn = new DataColumn("IsNullable", typeof(bool), null, MappingType.Element);
            IsPersistedColumn = new DataColumn("IsPersisted", typeof(bool), null, MappingType.Element);
            NamespaceColumn = new DataColumn("Namespace", typeof(string), null, MappingType.Element);
            ObjectNameColumn = new DataColumn("ObjectName", typeof(string), null, MappingType.Element);
            SchemaNameColumn = new DataColumn("SchemaName", typeof(string), null, MappingType.Element);
            TableNameColumn = new DataColumn("TableName", typeof(string), null, MappingType.Element);

            base.Columns.Add(CatalogNameColumn);
            base.Columns.Add(DefinitionColumn);
            base.Columns.Add(DescriptionColumn);
            base.Columns.Add(IsNullableColumn);
            base.Columns.Add(IsPersistedColumn);
            base.Columns.Add(NamespaceColumn);
            base.Columns.Add(ObjectNameColumn);
            base.Columns.Add(SchemaNameColumn);
            base.Columns.Add(TableNameColumn);
        }

		#endregion Constructors 

		#region Methods (5) 

		#region Public Methods (3) 

        public void AddComputedColumnsRow(ComputedColumnsRow row)
        {
            Rows.Add(row);
        }

        public ComputedColumnsRow NewComputedColumnsRow()
        {
            return ((ComputedColumnsRow)(NewRow()));
        }

        public void RemoveComputedColumnsRow(ComputedColumnsRow row)
        {
            Rows.Remove(row);
        }

		#endregion Public Methods 
		#region Protected Methods (2) 

        protected override Type GetRowType()
        {
            return typeof(ComputedColumnsRow);
        }

        protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
        {
            return new ComputedColumnsRow(builder);
        }

		#endregion Protected Methods 

		#endregion Methods 
    }
}
