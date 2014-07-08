using System.Data.Common;
using Meta.Net.Data.Interfaces;

namespace Meta.Net.Data.Metadata
{
    public class ComputedColumnsTableAdapter
    {
		#region Properties (2) 

        public string CommandText { get; set; }

        public DbDataAdapter DataAdapter { get; set; }

		#endregion Properties 

		#region Constructors (1) 

        public ComputedColumnsTableAdapter(DbDataAdapter dataAdapter, IDataMetadataScriptProvider dataMetadataScriptProvider, string catalogName)
        {
            CommandText = dataMetadataScriptProvider.ComputedColumnsSql2(catalogName);
            var dataTableMapping = new DataTableMapping { SourceTable = "Table", DataSetTable = "ComputedColumns" };
            dataTableMapping.ColumnMappings.Add("CatalogName", "CatalogName");
            dataTableMapping.ColumnMappings.Add("Definition", "Definition");
            dataTableMapping.ColumnMappings.Add("Description", "Description");
            dataTableMapping.ColumnMappings.Add("IsNullable", "IsNullable");
            dataTableMapping.ColumnMappings.Add("IsPersisted", "IsPersisted");
            dataTableMapping.ColumnMappings.Add("Namespace", "Namespace");
            dataTableMapping.ColumnMappings.Add("ObjectName", "ObjectName");
            dataTableMapping.ColumnMappings.Add("SchemaName", "SchemaName");
            dataTableMapping.ColumnMappings.Add("TableName", "TableName");

            DataAdapter = dataAdapter;
            DataAdapter.TableMappings.Add(dataTableMapping);
        }

		#endregion Constructors 

		#region Methods (3) 

		#region Public Methods (3) 

        public int Fill(DataConnectionManager dataConnectionManager, ComputedColumnsDataTable computedColumnsDataTable)
        {
            return Fill(dataConnectionManager.DataConnection, computedColumnsDataTable); 
        }

        public int Fill(DbConnection dbConnection, ComputedColumnsDataTable computedColumnsDataTable)
        {
            computedColumnsDataTable.Clear();
            using (var dbCommmand = dbConnection.CreateCommand())
            {
                dbCommmand.CommandText = CommandText;
                DataAdapter.SelectCommand = dbCommmand;
                computedColumnsDataTable.BeginLoadData();
                var result = DataAdapter.Fill(computedColumnsDataTable);
                computedColumnsDataTable.EndLoadData();
                DataAdapter.SelectCommand = null;
                return result;
            }
        }

        public ComputedColumnsDataTable GetData(DbConnection dbConnection)
        {
            var computedColumnsDataTable = new ComputedColumnsDataTable();
            Fill(dbConnection, computedColumnsDataTable);
            return computedColumnsDataTable;
        }

		#endregion Public Methods 

		#endregion Methods 
    }
}
