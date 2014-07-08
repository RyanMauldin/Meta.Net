using System.Data;

namespace Meta.Net.Data.Metadata
{
    public class UserTablesRow : DataRow
    {
		#region Properties (11) 

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

        public UserTablesDataTable DataTable { get; set; }

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

        public  string FileStreamFileGroup
        {
            get
            {
                return ((string)(this[DataTable.FileStreamFileGroupColumn]));
            }
            set
            {
                this[DataTable.FileStreamFileGroupColumn] = value;
            }
        }

        public bool HasTextNTextOrImageColumns
        {
            get
            {
                return ((bool)(this[DataTable.HasTextNTextOrImageColumnsColumn]));
            }
            set
            {
                this[DataTable.HasTextNTextOrImageColumnsColumn] = value;
            }
        }

        public string LobFileGroup
        {
            get
            {
                return ((string)(this[DataTable.LobFileGroupColumn]));
            }
            set
            {
                this[DataTable.LobFileGroupColumn] = value;
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

        public  string ObjectName
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

        public int TextInRowLimit
        {
            get
            {
                return ((int)(this[DataTable.TextInRowLimitColumn]));
            }
            set
            {
                this[DataTable.TextInRowLimitColumn] = value;
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

		#endregion Properties 

		#region Constructors (1) 

        public UserTablesRow(DataRowBuilder rb)
            : base(rb)
        {
            DataTable = ((UserTablesDataTable)(Table));
        }

		#endregion Constructors 
    }
}
