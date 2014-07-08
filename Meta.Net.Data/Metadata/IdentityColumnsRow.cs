using System.Data;

namespace Meta.Net.Data.Metadata
{
    public class IdentityColumnsRow : DataRow
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

        public int IncrementValue
        {
            get
            {
                return ((int)(this[DataTable.IncrementValueColumn]));
            }
            set
            {
                this[DataTable.IncrementValueColumn] = value;
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

        public int SeedValue
        {
            get
            {
                return ((int)(this[DataTable.SeedValueColumn]));
            }
            set
            {
                this[DataTable.SeedValueColumn] = value;
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

		#region Fields (1) 

        public IdentityColumnsDataTable DataTable;

		#endregion Fields 

		#region Constructors (1) 

        public IdentityColumnsRow(DataRowBuilder rb)
            : base(rb)
        {
            DataTable = ((IdentityColumnsDataTable)(Table));
        }

		#endregion Constructors 
    }
}
