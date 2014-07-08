using System;
using System.Data;
using Meta.Net.Data.Interfaces;

namespace Meta.Net.Data.Metadata
{
    public class UserTableColumnsRow: DataRow, IDataColumnDescripterRow
    {
		#region Properties (24) 

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

        public int ColumnOrdinal
        {
            get
            {
                return ((int)(this[DataTable.ColumnOrdinalColumn]));
            }
            set
            {
                this[DataTable.ColumnOrdinalColumn] = value;
            }
        }

        public UserTableColumnsDataTable DataTable { get; set; }

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

        public bool HasForeignKey
        {
            get
            {
                return ((bool)(this[DataTable.HasForeignKeyColumn]));
            }
            set
            {
                this[DataTable.HasForeignKeyColumn] = value;
            }
        }

        public bool HasXmlCollection
        {
            get
            {
                return ((bool)(this[DataTable.HasXmlCollectionColumn]));
            }
            set
            {
                this[DataTable.HasXmlCollectionColumn] = value;
            }
        }

        public bool IsAnsiPadded
        {
            get
            {
                return ((bool)(this[DataTable.IsAnsiPaddedColumn]));
            }
            set
            {
                this[DataTable.IsAnsiPaddedColumn] = value;
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

        public bool IsComputed
        {
            get
            {
                return ((bool)(this[DataTable.IsComputedColumn]));
            }
            set
            {
                this[DataTable.IsComputedColumn] = value;
            }
        }

        public bool IsFileStream
        {
            get
            {
                return ((bool)(this[DataTable.IsFileStreamColumn]));

            }
            set
            {
                this[DataTable.IsFileStreamColumn] = value;
            }
        }

        public bool IsIdentity
        {
            get
            {
                return ((bool)(this[DataTable.IsIdentityColumn]));
            }
            set
            {
                this[DataTable.IsIdentityColumn] = value;
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

        public bool IsRowGuidColumn
        {
            get
            {
                return ((bool)(this[DataTable.IsRowGuidColumnColumn]));
            }
            set
            {
                this[DataTable.IsRowGuidColumnColumn] = value;
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

        public bool IsXmlDocument
        {
            get
            {
                return ((bool)(this[DataTable.IsXmlDocumentColumn]));

            }
            set
            {
                this[DataTable.IsXmlDocumentColumn] = value;
            }
        }

        public Int64 MaxLength
        {
            get
            {
                return ((Int64)(this[DataTable.MaxLengthColumn]));
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

        public UserTableColumnsRow(DataRowBuilder rb)
            : base(rb)
        {
            DataTable = ((UserTableColumnsDataTable)(Table));
        }

		#endregion Constructors 
    }
}
