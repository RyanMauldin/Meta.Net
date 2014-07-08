using System.Data;

namespace Meta.Net.Data.Metadata
{
    public class DefaultConstraintsRow : DataRow
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

        public DefaultConstraintsDataTable DataTable;

		#endregion Fields 

		#region Constructors (1) 

        public DefaultConstraintsRow(DataRowBuilder rb)
            : base(rb)
        {
            DataTable = ((DefaultConstraintsDataTable)(Table));
        }

		#endregion Constructors 
    }
}
