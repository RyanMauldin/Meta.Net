using System.Data;

namespace Meta.Net.Data.Metadata
{
    public class ForeignKeyMapsRow: DataRow
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

        public ForeignKeyMapsDataTable DataTable { get; set; }

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

        public string NamespaceInverse
        {
            get
            {
                return ((string)(this[DataTable.NamespaceInverseColumn]));
            }
            set
            {
                this[DataTable.NamespaceInverseColumn] = value;
            }
        }

        public string NamespaceInverseGroup
        {
            get
            {
                return ((string)(this[DataTable.NamespaceInverseGroupColumn]));
            }
            set
            {
                this[DataTable.NamespaceInverseGroupColumn] = value;
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

        public string ReferencedCatalogName
        {
            get
            {
                return ((string)(this[DataTable.ReferencedCatalogNameColumn]));
            }
            set
            {
                this[DataTable.ReferencedCatalogNameColumn] = value;
            }
        }

        public string ReferencedColumnName
        {
            get
            {
                return ((string)(this[DataTable.ReferencedColumnNameColumn]));
            }
            set
            {
                this[DataTable.ReferencedColumnNameColumn] = value;
            }
        }

        public string ReferencedObjectName
        {
            get
            {
                return ((string)(this[DataTable.ReferencedObjectNameColumn]));
            }
            set
            {
                this[DataTable.ReferencedObjectNameColumn] = value;
            }
        }

        public string ReferencedSchemaName
        {
            get
            {
                return ((string)(this[DataTable.ReferencedSchemaNameColumn]));
            }
            set
            {
                this[DataTable.ReferencedSchemaNameColumn] = value;
            }
        }

        public string ReferencedTableName
        {
            get
            {
                return ((string)(this[DataTable.ReferencedTableNameColumn]));
            }
            set
            {
                this[DataTable.ReferencedTableNameColumn] = value;
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

        public ForeignKeyMapsRow(DataRowBuilder rb)
            : base(rb)
        {
            DataTable = ((ForeignKeyMapsDataTable)(Table));
        }

		#endregion Constructors 
    }
}
