using System;
using System.Data;
using Meta.Net.Data.Interfaces;

namespace Meta.Net.Data.Metadata
{
    public class UserDefinedDataTypesRow : DataRow, IDataColumnDescripterRow
    {
		#region Properties (15) 

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

        public string Collation
        {
            get
            {
                return ((string)(this[DataTable.CollationColumn]));
            }
            set
            {
                this[DataTable.CollationColumn] = value;
            }
        }

        public UserDefinedDataTypesDataTable DataTable { get; set; }

        public string DataType
        {
            get
            {
                return ((string)(this[DataTable.DataTypeColumn]));
            }
            set
            {
                this[DataTable.DataTypeColumn] = value;
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

        public bool HasDefault
        {
            get
            {
                return ((bool)(this[DataTable.HasDefaultColumn]));
            }
            set
            {
                this[DataTable.HasDefaultColumn] = value;
            }
        }

        public bool IsAssemblyType
        {
            get
            {
                return ((bool)(this[DataTable.IsAssemblyTypeColumn]));
            }
            set
            {
                this[DataTable.IsAssemblyTypeColumn] = value;
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

        public bool IsUserDefined
        {
            get
            {
                return ((bool)(this[DataTable.IsUserDefinedColumn]));
            }
            set
            {
                this[DataTable.IsUserDefinedColumn] = value;
            }
        }

        public Int64 MaxLength
        {
            get
            {
                return ((int)(this[DataTable.MaxLengthColumn]));
            }
            set
            {
                this[DataTable.MaxLengthColumn] = value;
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

        public int Precision
        {
            get
            {
                return ((int)(this[DataTable.PrecisionColumn]));
            }
            set
            {
                this[DataTable.PrecisionColumn] = value;
            }
        }

        public int Scale
        {
            get
            {
                return ((int)(this[DataTable.ScaleColumn]));
            }
            set
            {
                this[DataTable.ScaleColumn] = value;
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

		#endregion Properties 

		#region Constructors (1) 

        public UserDefinedDataTypesRow(DataRowBuilder rb)
            : base(rb)
        {
            DataTable = ((UserDefinedDataTypesDataTable)(Table));
        }

		#endregion Constructors 
    }
}
