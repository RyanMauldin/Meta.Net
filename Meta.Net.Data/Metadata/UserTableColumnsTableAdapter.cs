using System.Data.Common;
using Meta.Net.Data.Interfaces;

namespace Meta.Net.Data.Metadata
{
    public class UserTableColumnsTableAdapter
    {
		#region Properties (2) 

        public string CommandText { get; set; }

        public DbDataAdapter DataAdapter { get; set; }

		#endregion Properties 

		#region Constructors (1) 

        public UserTableColumnsTableAdapter(DbDataAdapter dataAdapter, IDataMetadataScriptProvider dataMetadataScriptProvider, string catalogName)
        {
            CommandText = dataMetadataScriptProvider.UserTableColumnsSql2(catalogName);
            var dataTableMapping = new DataTableMapping { SourceTable = "Table", DataSetTable = "UserTableColumns" };
            dataTableMapping.ColumnMappings.Add("CatalogName", "CatalogName");
            dataTableMapping.ColumnMappings.Add("Collation", "Collation");
            dataTableMapping.ColumnMappings.Add("ColumnOrdinal", "ColumnOrdinal");
            dataTableMapping.ColumnMappings.Add("DataType", "DataType");
            dataTableMapping.ColumnMappings.Add("Description", "Description");
            dataTableMapping.ColumnMappings.Add("HasDefault", "HasDefault");
            dataTableMapping.ColumnMappings.Add("HasForeignKey", "HasForeignKey");
            dataTableMapping.ColumnMappings.Add("HasXmlCollection", "HasXmlCollection");
            dataTableMapping.ColumnMappings.Add("IsAnsiPadded", "IsAnsiPadded");
            dataTableMapping.ColumnMappings.Add("IsAssemblyType", "IsAssemblyType");
            dataTableMapping.ColumnMappings.Add("IsComputed", "IsComputed");
            dataTableMapping.ColumnMappings.Add("IsFileStream", "IsFileStream");
            dataTableMapping.ColumnMappings.Add("IsIdentity", "IsIdentity");
            dataTableMapping.ColumnMappings.Add("IsNullable", "IsNullable");
            dataTableMapping.ColumnMappings.Add("IsRowGuidColumn", "IsRowGuidColumn");
            dataTableMapping.ColumnMappings.Add("IsUserDefined", "IsUserDefined");
            dataTableMapping.ColumnMappings.Add("IsXmlDocument", "IsXmlDocument");
            dataTableMapping.ColumnMappings.Add("MaxLength", "MaxLength");
            dataTableMapping.ColumnMappings.Add("Namespace", "Namespace");
            dataTableMapping.ColumnMappings.Add("ObjectName", "ObjectName");
            dataTableMapping.ColumnMappings.Add("Precision", "Precision");
            dataTableMapping.ColumnMappings.Add("Scale", "Scale");
            dataTableMapping.ColumnMappings.Add("SchemaName", "SchemaName");
            dataTableMapping.ColumnMappings.Add("TableName", "TableName");
            dataTableMapping.ColumnMappings.Add("TypeDescription", "TypeDescription");

            DataAdapter = dataAdapter;
            DataAdapter.TableMappings.Add(dataTableMapping);
        }

		#endregion Constructors 

		#region Methods (3) 

		#region Public Methods (3) 

        public int Fill(DataConnectionManager dataConnectionManager, UserTableColumnsDataTable tableColumnsDataTable)
        {
            return Fill(dataConnectionManager.DataConnection, tableColumnsDataTable); 
        }

        public int Fill(DbConnection dbConnection, UserTableColumnsDataTable tableColumnsDataTable)
        {
            tableColumnsDataTable.Clear();
            using (var dbCommmand = dbConnection.CreateCommand())
            {
                dbCommmand.CommandText = CommandText;
                DataAdapter.SelectCommand = dbCommmand;
                tableColumnsDataTable.BeginLoadData();
                var result = DataAdapter.Fill(tableColumnsDataTable);
                tableColumnsDataTable.EndLoadData();
                DataAdapter.SelectCommand = null;
                return result;
            }
        }

        public UserTableColumnsDataTable GetData(DbConnection dbConnection)
        {
            var tableColumnsDataTable = new UserTableColumnsDataTable();
            Fill(dbConnection, tableColumnsDataTable);
            return tableColumnsDataTable;
        }

		#endregion Public Methods 

		#endregion Methods 
    }
}
