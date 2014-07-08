using System;
using System.Data;

namespace Meta.Net.Data.Metadata
{
    public class IndexesDataTable: DataTable
    {
		#region Properties (24) 

        public DataColumn AllowPageLocksColumn { get; set; }

        public DataColumn AllowRowLocksColumn { get; set; }

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

        public DataColumn FileGroupColumn { get; set; }

        public DataColumn FillFactorColumn { get; set; }

        public DataColumn IgnoreDupKeyColumn { get; set; }

        public DataColumn IndexTypeColumn { get; set; }

        public DataColumn IsClusteredColumn { get; set; }

        public DataColumn IsDescendingKeyColumn { get; set; }

        public DataColumn IsDisabledColumn { get; set; }

        public DataColumn IsIncludedColumnColumn { get; set; }

        public DataColumn IsPaddedColumn { get; set; }

        public DataColumn IsUniqueColumn { get; set; }

        public DataColumn KeyOrdinalColumn { get; set; }

        public DataColumn NamespaceColumn { get; set; }

        public DataColumn NamespaceGroupColumn { get; set; }

        public DataColumn ObjectNameColumn { get; set; }

        public DataColumn PartitionOrdinalColumn { get; set; }

        public DataColumn SchemaNameColumn { get; set; }

        public DataColumn TableNameColumn { get; set; }

        public IndexesRow this[int index]
        {
            get
            {
                return ((IndexesRow)(Rows[index]));
            }
        }

		#endregion Properties 

		#region Constructors (1) 

        public IndexesDataTable()
        {
            TableName = "Indexes";
            
            AllowPageLocksColumn = new DataColumn("AllowPageLocks", typeof(bool), null, MappingType.Element);
            AllowRowLocksColumn = new DataColumn("AllowRowLocks", typeof(bool), null, MappingType.Element);
            CatalogNameColumn = new DataColumn("CatalogName", typeof(string), null, MappingType.Element);
            ColumnNameColumn = new DataColumn("ColumnName", typeof(string), null, MappingType.Element);
            DescriptionColumn = new DataColumn("Description", typeof(string), null, MappingType.Element);
            FileGroupColumn = new DataColumn("FileGroup", typeof(string), null, MappingType.Element);
            FillFactorColumn = new DataColumn("FillFactor", typeof(int), null, MappingType.Element);
            IgnoreDupKeyColumn = new DataColumn("IgnoreDupKey", typeof(bool), null, MappingType.Element);
            IndexTypeColumn = new DataColumn("IndexType", typeof(string), null, MappingType.Element);
            IsClusteredColumn = new DataColumn("IsClustered", typeof(bool), null, MappingType.Element);
            IsDescendingKeyColumn = new DataColumn("IsDescendingKey", typeof(bool), null, MappingType.Element);
            IsDisabledColumn = new DataColumn("IsDisabled", typeof(bool), null, MappingType.Element);
            IsIncludedColumnColumn = new DataColumn("IsIncludedColumn", typeof(bool), null, MappingType.Element);
            IsPaddedColumn = new DataColumn("IsPadded", typeof(bool), null, MappingType.Element);
            IsUniqueColumn = new DataColumn("IsUnique", typeof(bool), null, MappingType.Element);
            KeyOrdinalColumn = new DataColumn("KeyOrdinal", typeof(int), null, MappingType.Element);
            NamespaceColumn = new DataColumn("Namespace", typeof(string), null, MappingType.Element);
            NamespaceGroupColumn = new DataColumn("NamespaceGroup", typeof(string), null, MappingType.Element);
            ObjectNameColumn = new DataColumn("ObjectName", typeof(string), null, MappingType.Element);
            PartitionOrdinalColumn = new DataColumn("PartitionOrdinal", typeof(int), null, MappingType.Element);
            SchemaNameColumn = new DataColumn("SchemaName", typeof(string), null, MappingType.Element);
            TableNameColumn = new DataColumn("TableName", typeof(string), null, MappingType.Element);
            
            base.Columns.Add(AllowPageLocksColumn);
            base.Columns.Add(AllowRowLocksColumn);
            base.Columns.Add(CatalogNameColumn);
            base.Columns.Add(ColumnNameColumn);
            base.Columns.Add(DescriptionColumn);
            base.Columns.Add(FileGroupColumn);
            base.Columns.Add(FillFactorColumn);
            base.Columns.Add(IgnoreDupKeyColumn);
            base.Columns.Add(IndexTypeColumn);
            base.Columns.Add(IsClusteredColumn);
            base.Columns.Add(IsDescendingKeyColumn);
            base.Columns.Add(IsDisabledColumn);
            base.Columns.Add(IsIncludedColumnColumn);
            base.Columns.Add(IsPaddedColumn);
            base.Columns.Add(IsUniqueColumn);
            base.Columns.Add(KeyOrdinalColumn);
            base.Columns.Add(NamespaceColumn);
            base.Columns.Add(NamespaceGroupColumn);
            base.Columns.Add(ObjectNameColumn);
            base.Columns.Add(PartitionOrdinalColumn);
            base.Columns.Add(SchemaNameColumn);
            base.Columns.Add(TableNameColumn);
        }

		#endregion Constructors 

		#region Methods (5) 

		#region Public Methods (3) 

        public void AddIndexesRow(IndexesRow row)
        {
            Rows.Add(row);
        }

        public IndexesRow NewIndexesRow()
        {
            return ((IndexesRow)(NewRow()));
        }

        public void RemoveIndexesRow(IndexesRow row)
        {
            Rows.Remove(row);
        }

		#endregion Public Methods 
		#region Protected Methods (2) 

        protected override Type GetRowType()
        {
            return typeof(IndexesRow);
        }

        protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
        {
            return new IndexesRow(builder);
        }

		#endregion Protected Methods 

		#endregion Methods 
    }
}
