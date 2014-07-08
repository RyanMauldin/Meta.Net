using System;
using System.Data;

namespace Meta.Net.Data.Metadata
{
    public class CheckConstraintsDataTable : DataTable
    {
		#region Properties (15) 

        public DataColumn CatalogNameColumn { get; set; }

        public DataColumn ColumnNameColumn { get; set; }

        public int Count
        {
            get
            {
                return Rows.Count;
            }
        }

        public DataColumn DefinitionColumn { get; set; }

        public DataColumn DescriptionColumn { get; set; }

        public DataColumn IsDisabledColumn { get; set; }

        public DataColumn IsNotForReplicationColumn { get; set; }

        public DataColumn IsNotTrustedColumn { get; set; }

        public DataColumn IsSystemNamedColumn { get; set; }

        public DataColumn IsTableConstraintColumn { get; set; }

        public DataColumn NamespaceColumn { get; set; }

        public DataColumn ObjectNameColumn { get; set; }

        public DataColumn SchemaNameColumn { get; set; }

        public DataColumn TableNameColumn { get; set; }

        public CheckConstraintsRow this[int index]
        {
            get
            {
                return ((CheckConstraintsRow)(Rows[index]));
            }
        }

		#endregion Properties 

		#region Constructors (1) 

        public CheckConstraintsDataTable()
        {
            TableName = "CheckConstraints";

            CatalogNameColumn = new DataColumn("CatalogName", typeof(string), null, MappingType.Element);
            ColumnNameColumn = new DataColumn("ColumnName", typeof(string), null, MappingType.Element);
            DefinitionColumn = new DataColumn("Definition", typeof(string), null, MappingType.Element);
            DescriptionColumn = new DataColumn("Description", typeof(string), null, MappingType.Element);
            IsDisabledColumn = new DataColumn("IsDisabled", typeof(bool), null, MappingType.Element);
            IsNotForReplicationColumn = new DataColumn("IsNotForReplication", typeof(bool), null, MappingType.Element);
            IsNotTrustedColumn = new DataColumn("IsNotTrusted", typeof(bool), null, MappingType.Element);
            IsSystemNamedColumn = new DataColumn("IsSystemNamed", typeof(bool), null, MappingType.Element);
            IsTableConstraintColumn = new DataColumn("IsTableConstraint", typeof(bool), null, MappingType.Element);
            NamespaceColumn = new DataColumn("Namespace", typeof(string), null, MappingType.Element);
            ObjectNameColumn = new DataColumn("ObjectName", typeof(string), null, MappingType.Element);
            SchemaNameColumn = new DataColumn("SchemaName", typeof(string), null, MappingType.Element);
            TableNameColumn = new DataColumn("TableName", typeof(string), null, MappingType.Element);

            base.Columns.Add(CatalogNameColumn);
            base.Columns.Add(ColumnNameColumn);
            base.Columns.Add(DefinitionColumn);
            base.Columns.Add(DescriptionColumn);
            base.Columns.Add(IsDisabledColumn);
            base.Columns.Add(IsNotForReplicationColumn);
            base.Columns.Add(IsNotTrustedColumn);
            base.Columns.Add(IsSystemNamedColumn);
            base.Columns.Add(IsTableConstraintColumn);
            base.Columns.Add(NamespaceColumn);
            base.Columns.Add(ObjectNameColumn);
            base.Columns.Add(SchemaNameColumn);
            base.Columns.Add(TableNameColumn);
        }

		#endregion Constructors 

		#region Methods (5) 

		#region Public Methods (3) 

        public void AddCheckConstraintsRow(CheckConstraintsRow row)
        {
            Rows.Add(row);
        }

        public CheckConstraintsRow NewCheckConstraintsRow()
        {
            return ((CheckConstraintsRow)(NewRow()));
        }

        public void RemoveCheckConstraintsRow(CheckConstraintsRow row)
        {
            Rows.Remove(row);
        }

		#endregion Public Methods 
		#region Protected Methods (2) 

        protected override Type GetRowType()
        {
            return typeof(CheckConstraintsRow);
        }

        protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
        {
            return new CheckConstraintsRow(builder);
        }

		#endregion Protected Methods 

		#endregion Methods 
    }
}
