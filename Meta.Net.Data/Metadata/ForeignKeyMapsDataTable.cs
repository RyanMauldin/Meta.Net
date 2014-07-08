using System;
using System.Data;

namespace Meta.Net.Data.Metadata
{
    public class ForeignKeyMapsDataTable: DataTable
    {
		#region Properties (18) 

        public DataColumn CatalogNameColumn { get; set; }

        public DataColumn ColumnNameColumn { get; set; }

        public int Count
        {
            get
            {
                return Rows.Count;
            }
        }

        public DataColumn DescriptionColumn { get; set; }

        public DataColumn KeyOrdinalColumn { get; set; }

        public DataColumn NamespaceColumn { get; set; }

        public DataColumn NamespaceGroupColumn { get; set; }

        public DataColumn NamespaceInverseColumn { get; set; }

        public DataColumn NamespaceInverseGroupColumn { get; set; }

        public DataColumn ObjectNameColumn { get; set; }

        public DataColumn ReferencedCatalogNameColumn { get; set; }

        public DataColumn ReferencedColumnNameColumn { get; set; }

        public DataColumn ReferencedObjectNameColumn { get; set; }

        public DataColumn ReferencedSchemaNameColumn { get; set; }

        public DataColumn ReferencedTableNameColumn { get; set; }

        public DataColumn SchemaNameColumn { get; set; }

        public DataColumn TableNameColumn { get; set; }

        public ForeignKeyMapsRow this[int index]
        {
            get
            {
                return ((ForeignKeyMapsRow)(Rows[index]));
            }
        }

		#endregion Properties 

		#region Constructors (1) 

        public ForeignKeyMapsDataTable()
        {
            TableName = "ForeignKeyMaps";

            CatalogNameColumn = new DataColumn("CatalogName", typeof(string), null, MappingType.Element);
            ColumnNameColumn = new DataColumn("ColumnName", typeof(string), null, MappingType.Element);
            DescriptionColumn = new DataColumn("Description", typeof(string), null, MappingType.Element);
            KeyOrdinalColumn = new DataColumn("KeyOrdinal", typeof(int), null, MappingType.Element);
            NamespaceColumn = new DataColumn("Namespace", typeof(string), null, MappingType.Element);
            NamespaceGroupColumn = new DataColumn("NamespaceGroup", typeof(string), null, MappingType.Element);
            NamespaceInverseColumn = new DataColumn("NamespaceInverse", typeof(string), null, MappingType.Element);
            NamespaceInverseGroupColumn = new DataColumn("NamespaceInverseGroup", typeof(string), null, MappingType.Element);
            ObjectNameColumn = new DataColumn("ObjectName", typeof(string), null, MappingType.Element);
            ReferencedCatalogNameColumn = new DataColumn("ReferencedCatalogName", typeof(string), null, MappingType.Element);
            ReferencedColumnNameColumn = new DataColumn("ReferencedColumnName", typeof(string), null, MappingType.Element);
            ReferencedObjectNameColumn = new DataColumn("ReferencedObjectName", typeof(string), null, MappingType.Element);
            ReferencedSchemaNameColumn = new DataColumn("ReferencedSchemaName", typeof(string), null, MappingType.Element);
            ReferencedTableNameColumn = new DataColumn("ReferencedTableName", typeof(string), null, MappingType.Element);
            SchemaNameColumn = new DataColumn("SchemaName", typeof(string), null, MappingType.Element);
            TableNameColumn = new DataColumn("TableName", typeof(string), null, MappingType.Element);
            
            base.Columns.Add(CatalogNameColumn);
            base.Columns.Add(ColumnNameColumn);
            base.Columns.Add(DescriptionColumn);
            base.Columns.Add(KeyOrdinalColumn);
            base.Columns.Add(NamespaceColumn);
            base.Columns.Add(NamespaceGroupColumn);
            base.Columns.Add(NamespaceInverseColumn);
            base.Columns.Add(NamespaceInverseGroupColumn);
            base.Columns.Add(ObjectNameColumn);
            base.Columns.Add(ReferencedCatalogNameColumn);
            base.Columns.Add(ReferencedColumnNameColumn);
            base.Columns.Add(ReferencedObjectNameColumn);
            base.Columns.Add(ReferencedSchemaNameColumn);
            base.Columns.Add(ReferencedTableNameColumn);
            base.Columns.Add(SchemaNameColumn);
            base.Columns.Add(TableNameColumn);
        }

		#endregion Constructors 

		#region Methods (5) 

		#region Public Methods (3) 

        public void AddForeignKeyMapsRow(ForeignKeyMapsRow row)
        {
            Rows.Add(row);
        }

        public ForeignKeyMapsRow NewForeignKeyMapsRow()
        {
            return ((ForeignKeyMapsRow)(NewRow()));
        }

        public void RemoveForeignKeyMapsRow(ForeignKeyMapsRow row)
        {
            Rows.Remove(row);
        }

		#endregion Public Methods 
		#region Protected Methods (2) 

        protected override Type GetRowType()
        {
            return typeof(ForeignKeyMapsRow);
        }

        protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
        {
            return new ForeignKeyMapsRow(builder);
        }

		#endregion Protected Methods 

		#endregion Methods 
    }
}
