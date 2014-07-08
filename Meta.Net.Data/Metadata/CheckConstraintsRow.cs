using System.Data;

namespace Meta.Net.Data.Metadata
{
    public class CheckConstraintsRow : DataRow
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

        public string ColumnName
        {
            get
            {
                return ((string)(this[DataTable.ColumnNameColumn]));
            }
            set
            {
                this[DataTable.ColumnNameColumn] = value;
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

        public bool IsNotTrusted
        {
            get
            {
                return ((bool)(this[DataTable.IsNotTrustedColumn]));
            }
            set
            {
                this[DataTable.IsNotTrustedColumn] = value;
            }
        }

        public bool IsSystemNamed
        {
            get
            {
                return ((bool)(this[DataTable.IsSystemNamedColumn]));
            }
            set
            {
                this[DataTable.IsSystemNamedColumn] = value;
            }
        }

        public bool IsTableConstraint
        {
            get
            {
                return ((bool)(this[DataTable.IsTableConstraintColumn]));
            }
            set
            {
                this[DataTable.IsTableConstraintColumn] = value;
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

		#region Fields (1) 

        public CheckConstraintsDataTable DataTable;

		#endregion Fields 

		#region Constructors (1) 

        public CheckConstraintsRow(DataRowBuilder rb)
            : base(rb)
        {
            DataTable = ((CheckConstraintsDataTable)(Table));
        }

		#endregion Constructors 
    }
}
