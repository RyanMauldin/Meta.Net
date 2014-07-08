using System.Data.Common;
using Meta.Net.Data.Interfaces;

namespace Meta.Net.Data.Metadata
{
    public class IdentityColumnsTableAdapter
    {
		#region Properties (2) 

        public string CommandText { get; set; }

        public DbDataAdapter DataAdapter { get; set; }

		#endregion Properties 

		#region Constructors (1) 

        public IdentityColumnsTableAdapter(DbDataAdapter dataAdapter, IDataMetadataScriptProvider dataMetadataScriptProvider, string catalogName)
        {
            CommandText = dataMetadataScriptProvider.IdentityColumnsSql2(catalogName);
            var dataTableMapping = new DataTableMapping { SourceTable = "Table", DataSetTable = "IdentityColumns" };
            dataTableMapping.ColumnMappings.Add("CatalogName", "CatalogName");
            dataTableMapping.ColumnMappings.Add("Description", "Description");
            dataTableMapping.ColumnMappings.Add("IncrementValue", "IncrementValue");
            dataTableMapping.ColumnMappings.Add("IsNotForReplication", "IsNotForReplication");
            dataTableMapping.ColumnMappings.Add("Namespace", "Namespace");
            dataTableMapping.ColumnMappings.Add("ObjectName", "ObjectName");
            dataTableMapping.ColumnMappings.Add("SchemaName", "SchemaName");
            dataTableMapping.ColumnMappings.Add("SeedValue", "SeedValue");
            dataTableMapping.ColumnMappings.Add("TableName", "TableName");

            DataAdapter = dataAdapter;
            DataAdapter.TableMappings.Add(dataTableMapping);
        }

		#endregion Constructors 

		#region Methods (3) 

		#region Public Methods (3) 

        public int Fill(DataConnectionManager dataConnectionManager, IdentityColumnsDataTable identityColumnsDataTable)
        {
            return Fill(dataConnectionManager.DataConnection, identityColumnsDataTable);
        }

        public int Fill(DbConnection dbConnection, IdentityColumnsDataTable identityColumnsDataTable)
        {
            identityColumnsDataTable.Clear();
            using (var dbCommmand = dbConnection.CreateCommand())
            {
                dbCommmand.CommandText = CommandText;
                DataAdapter.SelectCommand = dbCommmand;
                identityColumnsDataTable.BeginLoadData();
                var result = DataAdapter.Fill(identityColumnsDataTable);
                identityColumnsDataTable.EndLoadData();
                DataAdapter.SelectCommand = null;
                return result;
            }
        }

        public IdentityColumnsDataTable GetData(DbConnection dbConnection)
        {
            var identityColumnsDataTable = new IdentityColumnsDataTable();
            Fill(dbConnection, identityColumnsDataTable);
            return identityColumnsDataTable;
        }

		#endregion Public Methods 

		#endregion Methods 
    }
}
