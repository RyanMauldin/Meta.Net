using System;
using System.Data;

namespace Meta.Net.Data.Metadata
{
    public class ForeignKeysDataTable: DataTable
    {
		#region Properties (19) 

        public DataColumn CatalogNameColumn { get; set; }

        public DataColumn ColumnNameColumn { get; set; }

        public int Count
        {
            get
            {
                return Rows.Count;
            }
        }

        public DataColumn DeleteActionColumn { get; set; }

        public DataColumn DeleteActionDescriptionColumn { get; set; }

        public DataColumn DescriptionColumn { get; set; }

        public DataColumn IsDisabledColumn { get; set; }

        public DataColumn IsNotForReplicationColumn { get; set; }

        public DataColumn IsNotTrustedColumn { get; set; }

        public DataColumn IsSystemNamedColumn { get; set; }

        public DataColumn KeyOrdinalColumn { get; set; }

        public DataColumn NamespaceColumn { get; set; }

        public DataColumn NamespaceGroupColumn { get; set; }

        public DataColumn ObjectNameColumn { get; set; }

        public DataColumn SchemaNameColumn { get; set; }

        public DataColumn TableNameColumn { get; set; }

        public ForeignKeysRow this[int index]
        {
            get
            {
                return ((ForeignKeysRow)(Rows[index]));
            }
        }

        public DataColumn UpdateActionColumn { get; set; }

        public DataColumn UpdateActionDescriptionColumn { get; set; }

		#endregion Properties 

		#region Constructors (1) 

        public ForeignKeysDataTable()
        {
            TableName = "ForeignKeys";

            CatalogNameColumn = new DataColumn("CatalogName", typeof(string), null, MappingType.Element);
            ColumnNameColumn = new DataColumn("ColumnName", typeof(string), null, MappingType.Element);
            DeleteActionColumn = new DataColumn("DeleteAction", typeof(int), null, MappingType.Element);
            DeleteActionDescriptionColumn = new DataColumn("DeleteActionDescription", typeof(string), null, MappingType.Element);
            DescriptionColumn = new DataColumn("Description", typeof(string), null, MappingType.Element);
            IsDisabledColumn = new DataColumn("IsDisabled", typeof(bool), null, MappingType.Element);
            IsNotForReplicationColumn = new DataColumn("IsNotForReplication", typeof(bool), null, MappingType.Element);
            IsNotTrustedColumn = new DataColumn("IsNotTrusted", typeof(bool), null, MappingType.Element);
            IsSystemNamedColumn = new DataColumn("IsSystemNamed", typeof(bool), null, MappingType.Element);
            KeyOrdinalColumn = new DataColumn("KeyOrdinal", typeof(int), null, MappingType.Element);
            NamespaceColumn = new DataColumn("Namespace", typeof(string), null, MappingType.Element);
            NamespaceGroupColumn = new DataColumn("NamespaceGroup", typeof(string), null, MappingType.Element);
            ObjectNameColumn = new DataColumn("ObjectName", typeof(string), null, MappingType.Element);
            SchemaNameColumn = new DataColumn("SchemaName", typeof(string), null, MappingType.Element);
            TableNameColumn = new DataColumn("TableName", typeof(string), null, MappingType.Element);
            UpdateActionColumn = new DataColumn("UpdateAction", typeof(int), null, MappingType.Element);
            UpdateActionDescriptionColumn = new DataColumn("UpdateActionDescription", typeof(string), null, MappingType.Element);
             
            base.Columns.Add(CatalogNameColumn);
            base.Columns.Add(ColumnNameColumn);
            base.Columns.Add(DeleteActionColumn);
            base.Columns.Add(DeleteActionDescriptionColumn);
            base.Columns.Add(DescriptionColumn);
            base.Columns.Add(IsDisabledColumn);
            base.Columns.Add(IsNotForReplicationColumn);
            base.Columns.Add(IsNotTrustedColumn);
            base.Columns.Add(IsSystemNamedColumn);
            base.Columns.Add(KeyOrdinalColumn);
            base.Columns.Add(NamespaceColumn);
            base.Columns.Add(NamespaceGroupColumn);
            base.Columns.Add(ObjectNameColumn);
            base.Columns.Add(SchemaNameColumn);
            base.Columns.Add(TableNameColumn);
            base.Columns.Add(UpdateActionColumn);
            base.Columns.Add(UpdateActionDescriptionColumn);   
        }

		#endregion Constructors 

		#region Methods (5) 

		#region Public Methods (3) 

        public void AddForeignKeysRow(ForeignKeysRow row)
        {
            Rows.Add(row);
        }

        public ForeignKeysRow NewForeignKeysRow()
        {
            return ((ForeignKeysRow)(NewRow()));
        }

        public void RemoveForeignKeysRow(ForeignKeysRow row)
        {
            Rows.Remove(row);
        }

		#endregion Public Methods 
		#region Protected Methods (2) 

        protected override Type GetRowType()
        {
            return typeof(ForeignKeysRow);
        }

        protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
        {
            return new ForeignKeysRow(builder);
        }

		#endregion Protected Methods 

		#endregion Methods 
    }
}
