using System.Data;

namespace Meta.Net.Data.Metadata
{
    public class UniqueConstraintsRow: DataRow
    {
		#region Properties (20) 

        public bool AllowPageLocks
        {
            get
            {
                return ((bool)(this[DataTable.AllowPageLocksColumn]));
            }
            set
            {
                this[DataTable.AllowPageLocksColumn] = value;
            }
        }

        public bool AllowRowLocks
        {
            get
            {
                return ((bool)(this[DataTable.AllowRowLocksColumn]));
            }
            set
            {
                this[DataTable.AllowRowLocksColumn] = value;
            }
        }

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

        public string FileGroup
        {
            get
            {
                return ((string)(this[DataTable.FileGroupColumn]));
            }
            set
            {
                this[DataTable.FileGroupColumn] = value;
            }
        }

        public int FillFactor
        {
            get
            {
                return ((int)(this[DataTable.FillFactorColumn]));
            }
            set
            {
                this[DataTable.FillFactorColumn] = value;
            }
        }

        public bool IgnoreDupKey
        {
            get
            {
                return ((bool)(this[DataTable.IgnoreDupKeyColumn]));
            }
            set
            {
                this[DataTable.IgnoreDupKeyColumn] = value;
            }
        }

        public string IndexType
        {
            get
            {
                return ((string) (this[DataTable.IndexTypeColumn]));
            }
            set
            {
                this[DataTable.IndexTypeColumn] = value;
            }
        }

        public bool IsClustered
        {
            get
            {
                return ((bool)(this[DataTable.IsClusteredColumn]));
            }
            set
            {
                this[DataTable.IsClusteredColumn] = value;
            }
        }

        public bool IsDescendingKey
        {
            get
            {
                return ((bool)(this[DataTable.IsDescendingKeyColumn]));
            }
            set
            {
                this[DataTable.IsDescendingKeyColumn] = value;
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

        public bool IsPadded
        {
            get
            {
                return ((bool)(this[DataTable.IsPaddedColumn]));
            }
            set
            {
                this[DataTable.IsPaddedColumn] = value;
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

        public int PartitionOrdinal
        {
            get
            {
                return ((int)(this[DataTable.PartitionOrdinalColumn]));
            }
            set
            {
                this[DataTable.PartitionOrdinalColumn] = value;
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

        public UniqueConstraintsDataTable DataTable;

		#endregion Fields 

		#region Constructors (1) 

        public UniqueConstraintsRow(DataRowBuilder rb)
            : base(rb)
        {
            DataTable = ((UniqueConstraintsDataTable)(Table));
        }

		#endregion Constructors 
    }
}
