using System.Data;

namespace Meta.Net.Data.Metadata
{
    public class ForeignKeysRow: DataRow
    {
		#region Properties (17) 

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

        public int DeleteAction
        {
            get
            {
                return ((int)(this[DataTable.DeleteActionColumn]));
            }
            set
            {
                this[DataTable.DeleteActionColumn] = value;
            }
        }

        public string DeleteActionDescription
        {
            get
            {
                return ((string)(this[DataTable.DeleteActionDescriptionColumn]));
            }
            set
            {
                this[DataTable.DeleteActionDescriptionColumn] = value;
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

        public int KeyOrdinal
        {
            get
            {
                return ((int)(this[DataTable.KeyOrdinalColumn]));
            }
            set
            {
                this[DataTable.KeyOrdinalColumn] = value;
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

        public string NamespaceGroup
        {
            get
            {
                return ((string)(this[DataTable.NamespaceGroupColumn]));
            }
            set
            {
                this[DataTable.NamespaceGroupColumn] = value;
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

        public int UpdateAction
        {
            get
            {
                return ((int)(this[DataTable.UpdateActionColumn]));
            }
            set
            {
                this[DataTable.UpdateActionColumn] = value;
            }
        }

        public string UpdateActionDescription
        {
            get
            {
                return ((string)(this[DataTable.UpdateActionDescriptionColumn]));
            }
            set
            {
                this[DataTable.UpdateActionDescriptionColumn] = value;
            }
        }

		#endregion Properties 

		#region Fields (1) 

        public ForeignKeysDataTable DataTable;

		#endregion Fields 

		#region Constructors (1) 

        public ForeignKeysRow(DataRowBuilder rb)
            : base(rb)
        {
            DataTable = ((ForeignKeysDataTable)(Table));
        }

		#endregion Constructors 
    }
}
