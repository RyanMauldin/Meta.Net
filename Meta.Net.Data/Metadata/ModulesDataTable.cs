using System;
using System.Data;

namespace Meta.Net.Data.Metadata
{
    public class ModulesDataTable: DataTable
    {
		#region Properties (15) 

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

        public DataColumn IsDisabledColumn { get; set; }

        public DataColumn IsNotForReplicationColumn { get; set; }

        public DataColumn NamespaceColumn { get; set; }

        public DataColumn ObjectNameColumn { get; set; }

        public DataColumn SchemaNameColumn { get; set; }

        public ModulesRow this[int index]
        {
            get
            {
                return ((ModulesRow)(Rows[index]));
            }
        }

        public DataColumn TriggerForObjectNameColumn { get; set; }

        public DataColumn TriggerForSchemaColumn { get; set; }

        public DataColumn TypeDescriptionColumn { get; set; }

        public DataColumn UsesAnsiNullsColumn { get; set; }

        public DataColumn UsesQuotedIdentifierColumn { get; set; }

		#endregion Properties 

		#region Constructors (1) 

        public ModulesDataTable()
        {
            TableName = "Modules";

            CatalogNameColumn = new DataColumn("CatalogName", typeof(string), null, MappingType.Element);
            DefinitionColumn = new DataColumn("Definition", typeof(string), null, MappingType.Element);
            DescriptionColumn = new DataColumn("Description", typeof(string), null, MappingType.Element);
            IsDisabledColumn = new DataColumn("IsDisabled", typeof(bool), null, MappingType.Element);
            IsNotForReplicationColumn = new DataColumn("IsNotForReplication", typeof(bool), null, MappingType.Element);
            NamespaceColumn = new DataColumn("Namespace", typeof(string), null, MappingType.Element);
            ObjectNameColumn = new DataColumn("ObjectName", typeof(string), null, MappingType.Element);
            SchemaNameColumn = new DataColumn("SchemaName", typeof(string), null, MappingType.Element);
            TriggerForObjectNameColumn = new DataColumn("TriggerForObjectName", typeof(string), null, MappingType.Element);
            TriggerForSchemaColumn = new DataColumn("TriggerForSchema", typeof(string), null, MappingType.Element);
            TypeDescriptionColumn = new DataColumn("TypeDescription", typeof(string), null, MappingType.Element);
            UsesAnsiNullsColumn = new DataColumn("UsesAnsiNulls", typeof(bool), null, MappingType.Element);
            UsesQuotedIdentifierColumn = new DataColumn("UsesQuotedIdentifier", typeof(bool), null, MappingType.Element);

            base.Columns.Add(CatalogNameColumn);
            base.Columns.Add(DefinitionColumn);
            base.Columns.Add(DescriptionColumn);
            base.Columns.Add(IsDisabledColumn);
            base.Columns.Add(IsNotForReplicationColumn);
            base.Columns.Add(NamespaceColumn);
            base.Columns.Add(ObjectNameColumn);
            base.Columns.Add(SchemaNameColumn);
            base.Columns.Add(TriggerForObjectNameColumn);
            base.Columns.Add(TriggerForSchemaColumn);
            base.Columns.Add(TypeDescriptionColumn);
            base.Columns.Add(UsesAnsiNullsColumn);
            base.Columns.Add(UsesQuotedIdentifierColumn);
        }

		#endregion Constructors 

		#region Methods (5) 

		#region Public Methods (3) 

        public void AddModulesRow(ModulesRow row)
        {
            Rows.Add(row);
        }

        public ModulesRow NewModulesRow()
        {
            return ((ModulesRow)(NewRow()));
        }

        public void RemoveModulesRow(ModulesRow row)
        {
            Rows.Remove(row);
        }

		#endregion Public Methods 
		#region Protected Methods (2) 

        protected override Type GetRowType()
        {
            return typeof(ModulesRow);
        }

        protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
        {
            return new ModulesRow(builder);
        }

		#endregion Protected Methods 

		#endregion Methods 
    }
}
