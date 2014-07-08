using System;
using System.Data;

namespace Meta.Net.Data.Metadata
{
    public class IdentityColumnsDataTable : DataTable
    {
		#region Properties (11) 

        public DataColumn CatalogNameColumn { get; set; }

        public int Count
        {
            get
            {
                return Rows.Count;
            }
        }

        public DataColumn DescriptionColumn { get; set; }

        public DataColumn IncrementValueColumn { get; set; }

        public DataColumn IsNotForReplicationColumn { get; set; }

        public DataColumn NamespaceColumn { get; set; }

        public DataColumn ObjectNameColumn { get; set; }

        public DataColumn SchemaNameColumn { get; set; }

        public DataColumn SeedValueColumn { get; set; }

        public DataColumn TableNameColumn { get; set; }

        public IdentityColumnsRow this[int index]
        {
            get
            {
                return ((IdentityColumnsRow)(Rows[index]));
            }
        }

		#endregion Properties 

		#region Constructors (1) 

        public IdentityColumnsDataTable()
        {
            TableName = "IdentityColumns";
            
            CatalogNameColumn = new DataColumn("CatalogName", typeof(string), null, MappingType.Element);
            DescriptionColumn = new DataColumn("Description", typeof(string), null, MappingType.Element);
            IncrementValueColumn = new DataColumn("IncrementValue", typeof(int), null, MappingType.Element);
            IsNotForReplicationColumn = new DataColumn("IsNotForReplication", typeof(bool), null, MappingType.Element);
            NamespaceColumn = new DataColumn("Namespace", typeof(string), null, MappingType.Element);
            ObjectNameColumn = new DataColumn("ObjectName", typeof(string), null, MappingType.Element);
            SchemaNameColumn = new DataColumn("SchemaName", typeof(string), null, MappingType.Element);
            SeedValueColumn = new DataColumn("SeedValue", typeof(int), null, MappingType.Element);
            TableNameColumn = new DataColumn("TableName", typeof(string), null, MappingType.Element);
            
            base.Columns.Add(CatalogNameColumn);
            base.Columns.Add(DescriptionColumn);
            base.Columns.Add(IncrementValueColumn);
            base.Columns.Add(IsNotForReplicationColumn);
            base.Columns.Add(NamespaceColumn);
            base.Columns.Add(ObjectNameColumn);
            base.Columns.Add(SchemaNameColumn);
            base.Columns.Add(SeedValueColumn);
            base.Columns.Add(TableNameColumn);
        }

		#endregion Constructors 

		#region Methods (5) 

		#region Public Methods (3) 

        public void AddIdentityColumnsRow(IdentityColumnsRow row)
        {
            Rows.Add(row);
        }

        public IdentityColumnsRow NewIdentityColumnsRow()
        {
            return ((IdentityColumnsRow)(NewRow()));
        }

        public void RemoveIdentityColumnsRow(IdentityColumnsRow row)
        {
            Rows.Remove(row);
        }

		#endregion Public Methods 
		#region Protected Methods (2) 

        protected override Type GetRowType()
        {
            return typeof(IdentityColumnsRow);
        }

        protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
        {
            return new IdentityColumnsRow(builder);
        }

		#endregion Protected Methods 

		#endregion Methods 
    }
}
