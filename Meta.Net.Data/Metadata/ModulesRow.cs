using System.Data;

namespace Meta.Net.Data.Metadata
{
    public class ModulesRow: DataRow
    {
		#region Properties (13) 

        public string CatalogName
        {
            get
            {
                return ((string)(this[DataTable.CatalogNameColumn]));
            }
            set
            {
                this[DataTable.CatalogNameColumn] = value;
            }
        }

        public string Definition
        {
            get
            {
                return ((string)(this[DataTable.DefinitionColumn]));
            }
            set
            {
                this[DataTable.DefinitionColumn] = value;
            }
        }

        public string Description
        {
            get
            {
                return ((string)(this[DataTable.DescriptionColumn]));
            }
            set
            {
                this[DataTable.DescriptionColumn] = value;
            }
        }

        public bool IsDisabled
        {
            get
            {
                return ((bool)(this[DataTable.IsDisabledColumn]));
            }
            set
            {
                this[DataTable.IsDisabledColumn] = value;
            }
        }

        public bool IsNotForReplication
        {
            get
            {
                return ((bool)(this[DataTable.IsNotForReplicationColumn]));
            }
            set
            {
                this[DataTable.IsNotForReplicationColumn] = value;
            }
        }

        public string Namespace
        {
            get
            {
                return ((string)(this[DataTable.NamespaceColumn]));
            }
            set
            {
                this[DataTable.NamespaceColumn] = value;
            }
        }

        public string ObjectName
        {
            get
            {
                return ((string)(this[DataTable.ObjectNameColumn]));
            }
            set
            {
                this[DataTable.ObjectNameColumn] = value;
            }
        }

        public string SchemaName
        {
            get
            {
                return ((string)(this[DataTable.SchemaNameColumn]));
            }
            set
            {
                this[DataTable.SchemaNameColumn] = value;
            }
        }

        public string TriggerForObjectName
        {
            get
            {
                return ((string)(this[DataTable.TriggerForObjectNameColumn]));
            }
            set
            {
                this[DataTable.TriggerForObjectNameColumn] = value;
            }
        }

        public string TriggerForSchema
        {
            get
            {
                return ((string)(this[DataTable.TriggerForSchemaColumn]));
            }
            set
            {
                this[DataTable.TriggerForSchemaColumn] = value;
            }
        }

        public string TypeDescription
        {
            get
            {
                return ((string)(this[DataTable.TypeDescriptionColumn]));
            }
            set
            {
                this[DataTable.TypeDescriptionColumn] = value;
            }
        }

        public bool UsesAnsiNulls
        {
            get
            {
                return ((bool)(this[DataTable.UsesAnsiNullsColumn]));
            }
            set
            {
                this[DataTable.UsesAnsiNullsColumn] = value;
            }
        }

        public bool UsesQuotedIdentifier
        {
            get
            {
                return ((bool)(this[DataTable.UsesQuotedIdentifierColumn]));
            }
            set
            {
                this[DataTable.UsesQuotedIdentifierColumn] = value;
            }
        }

		#endregion Properties 

		#region Fields (1) 

        public ModulesDataTable DataTable;

		#endregion Fields 

		#region Constructors (1) 

        public ModulesRow(DataRowBuilder rb)
            : base(rb)
        {
            DataTable = ((ModulesDataTable)(Table));
        }

		#endregion Constructors 
    }
}
