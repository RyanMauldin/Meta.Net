using System.Data.Common;
using Meta.Net.Data.Interfaces;

namespace Meta.Net.Data.Metadata
{
    public class SchemasTableAdapter
    {
		#region Properties (2) 

        public string CommandText { get; set; }

        public DbDataAdapter DataAdapter { get; set; }

		#endregion Properties 

		#region Constructors (1) 

        public SchemasTableAdapter(DbDataAdapter dataAdapter, IDataMetadataScriptProvider dataMetadataScriptProvider, string catalogName)
        {

            CommandText = dataMetadataScriptProvider.SchemasSql2(catalogName);
            var dataTableMapping = new DataTableMapping { SourceTable = "Table", DataSetTable = "Schemas" };
            dataTableMapping.ColumnMappings.Add("CatalogName", "CatalogName");
            dataTableMapping.ColumnMappings.Add("Description", "Description");
            dataTableMapping.ColumnMappings.Add("Namespace", "Namespace");
            dataTableMapping.ColumnMappings.Add("ObjectName", "ObjectName");
            
            DataAdapter = dataAdapter;
            DataAdapter.TableMappings.Add(dataTableMapping);
        }

		#endregion Constructors 

		#region Methods (3) 

		#region Public Methods (3) 

        public int Fill(DataConnectionManager dataConnectionManager, SchemasDataTable schemasDataTable)
        {
            return Fill(dataConnectionManager.DataConnection, schemasDataTable);  
        }

        public int Fill(DbConnection dbConnection, SchemasDataTable schemasDataTable)
        {
            schemasDataTable.Clear();
            using (var dbCommmand = dbConnection.CreateCommand())
            {
                dbCommmand.CommandText = CommandText;
                DataAdapter.SelectCommand = dbCommmand;
                schemasDataTable.BeginLoadData();
                var result = DataAdapter.Fill(schemasDataTable);
                schemasDataTable.EndLoadData();
                DataAdapter.SelectCommand = null;
                return result;
            }
        }

        public SchemasDataTable GetData(DbConnection dbConnection)
        {
            var schemasDataTable = new SchemasDataTable();
            Fill(dbConnection, schemasDataTable);
            return schemasDataTable;
        }

		#endregion Public Methods 

		#endregion Methods 
    }
}
