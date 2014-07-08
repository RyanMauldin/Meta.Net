using System.Data.Common;
using Meta.Net.Data.Interfaces;

namespace Meta.Net.Data.Metadata
{
    public class UserDefinedDataTypesTableAdapter
    {
		#region Properties (2) 

        public string CommandText { get; set; }

        public DbDataAdapter DataAdapter { get; set; }

		#endregion Properties 

		#region Constructors (1) 

        public UserDefinedDataTypesTableAdapter(DbDataAdapter dataAdapter, IDataMetadataScriptProvider dataMetadataScriptProvider, string catalogName)
        {
            CommandText = dataMetadataScriptProvider.UserDefinedDataTypesSql2(catalogName);
            var dataTableMapping = new DataTableMapping { SourceTable = "Table", DataSetTable = "UserDefinedDataTypes" };
            dataTableMapping.ColumnMappings.Add("CatalogName", "CatalogName");
            dataTableMapping.ColumnMappings.Add("Collation", "Collation");
            dataTableMapping.ColumnMappings.Add("DataType", "DataType");
            dataTableMapping.ColumnMappings.Add("Description", "Description");
            dataTableMapping.ColumnMappings.Add("HasDefault", "HasDefault");
            dataTableMapping.ColumnMappings.Add("IsAssemblyType", "IsAssemblyType");
            dataTableMapping.ColumnMappings.Add("IsNullable", "IsNullable");
            dataTableMapping.ColumnMappings.Add("IsUserDefined", "IsUserDefined");
            dataTableMapping.ColumnMappings.Add("MaxLength", "MaxLength");
            dataTableMapping.ColumnMappings.Add("Namespace", "Namespace");
            dataTableMapping.ColumnMappings.Add("ObjectName", "ObjectName");
            dataTableMapping.ColumnMappings.Add("Precision", "Precision");
            dataTableMapping.ColumnMappings.Add("Scale", "Scale");
            dataTableMapping.ColumnMappings.Add("SchemaName", "SchemaName");

            DataAdapter = dataAdapter;
            DataAdapter.TableMappings.Add(dataTableMapping);
        }

		#endregion Constructors 

		#region Methods (3) 

		#region Public Methods (3) 

        public int Fill(DataConnectionManager dataConnectionManager, UserDefinedDataTypesDataTable userDefinedDataTypesDataTable)
        {
            return Fill(dataConnectionManager.DataConnection, userDefinedDataTypesDataTable); 
        }

        public int Fill(DbConnection dbConnection, UserDefinedDataTypesDataTable userDefinedDataTypesDataTable)
        {
            userDefinedDataTypesDataTable.Clear();
            using (var dbCommmand = dbConnection.CreateCommand())
            {
                dbCommmand.CommandText = CommandText;
                DataAdapter.SelectCommand = dbCommmand;
                userDefinedDataTypesDataTable.BeginLoadData();
                var result = DataAdapter.Fill(userDefinedDataTypesDataTable);
                userDefinedDataTypesDataTable.EndLoadData();
                DataAdapter.SelectCommand = null;
                return result;
            }
        }

        public UserDefinedDataTypesDataTable GetData(DbConnection dbConnection)
        {
            var userDefinedDataTypesDataTable = new UserDefinedDataTypesDataTable();
            Fill(dbConnection, userDefinedDataTypesDataTable);
            return userDefinedDataTypesDataTable;
        }

		#endregion Public Methods 

		#endregion Methods 
    }
}
