using System.Data;

namespace Meta.Net.Data.Metadata
{
    public class SchemasRow: DataRow
    {
		#region Properties (6) 

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

        public SchemasDataTable DataTable { get; set; }

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

		#endregion Properties 

		#region Constructors (1) 

        public SchemasRow(DataRowBuilder rb)
            : base(rb)
        {
            DataTable = ((SchemasDataTable)(Table));
        }

		#endregion Constructors 
    }
}
