using System;
using System.Data;

namespace Meta.Net.Data.Metadata
{
    public class SchemasDataTable: DataTable
    {
		#region Properties (6) 

        public DataColumn CatalogNameColumn { get; set; }

        public int Count
        {
            get
            {
                return Rows.Count;
            }
        }

        public DataColumn DescriptionColumn { get; set; }

        public DataColumn NamespaceColumn { get; set; }

        public DataColumn ObjectNameColumn { get; set; }

        public SchemasRow this[int index]
        {
            get
            {
                return ((SchemasRow)(Rows[index]));
            }
        }

		#endregion Properties 

		#region Constructors (1) 

        public SchemasDataTable()
        {
            TableName = "Schemas";

            CatalogNameColumn = new DataColumn("CatalogName", typeof(string), null, MappingType.Element);
            DescriptionColumn = new DataColumn("Description", typeof(string), null, MappingType.Element);
            NamespaceColumn = new DataColumn("Namespace", typeof(string), null, MappingType.Element);
            ObjectNameColumn = new DataColumn("ObjectName", typeof(string), null, MappingType.Element);
            
            base.Columns.Add(CatalogNameColumn);
            base.Columns.Add(DescriptionColumn);
            base.Columns.Add(NamespaceColumn);
            base.Columns.Add(ObjectNameColumn);
        }

		#endregion Constructors 

		#region Methods (5) 

		#region Public Methods (3) 

        public void AddSchemasRow(SchemasRow row)
        {
            Rows.Add(row);
        }

        public SchemasRow NewSchemasRow()
        {
            return ((SchemasRow)(NewRow()));
        }

        public void RemoveSchemasRow(SchemasRow row)
        {
            Rows.Remove(row);
        }

		#endregion Public Methods 
		#region Protected Methods (2) 

        protected override Type GetRowType()
        {
            return typeof(SchemasRow);
        }

        protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
        {
            return new SchemasRow(builder);
        }

		#endregion Protected Methods 

		#endregion Methods 
    }
}
