using System.Data;

namespace Meta.Net.Data.Metadata
{
    public class ComputedColumnsRow: DataRow
    {
		#region Properties (9) 

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

        public ComputedColumnsDataTable DataTable { get; set; }

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

        public bool IsNullable
        {
            get
            {
                return ((bool)(this[DataTable.IsNullableColumn]));
            }
            set
            {
                this[DataTable.IsNullableColumn] = value;
            }
        }

        public bool IsPersisted
        {
            get
            {
                return ((bool)(this[DataTable.IsPersistedColumn]));
            }
            set
            {
                this[DataTable.IsPersistedColumn] = value;
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

        public string TableName
        {
            get
            {
                return ((string)(this[DataTable.TableNameColumn]));
            }
            set
            {
                this[DataTable.TableNameColumn] = value;
            }
        }

		#endregion Properties 

		#region Constructors (1) 

        public ComputedColumnsRow(DataRowBuilder rb)
            : base(rb)
        {
            DataTable = ((ComputedColumnsDataTable)(Table));
        }

		#endregion Constructors 
    }
}
