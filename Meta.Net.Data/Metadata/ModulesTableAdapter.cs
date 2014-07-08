using System.Data.Common;
using Meta.Net.Data.Interfaces;

namespace Meta.Net.Data.Metadata
{
    public class ModulesTableAdapter
    {
		#region Properties (2) 

        public string CommandText { get; set; }

        public DbDataAdapter DataAdapter { get; set; }

		#endregion Properties 

		#region Constructors (1) 

        public ModulesTableAdapter(DbDataAdapter dataAdapter, IDataMetadataScriptProvider dataMetadataScriptProvider, string catalogName)
        {
            CommandText = dataMetadataScriptProvider.ModulesSql2(catalogName);
            var dataTableMapping = new DataTableMapping { SourceTable = "Table", DataSetTable = "Modules" };
            dataTableMapping.ColumnMappings.Add("Namespace", "Namespace");
            dataTableMapping.ColumnMappings.Add("CatalogName", "CatalogName");
            dataTableMapping.ColumnMappings.Add("SchemaName", "SchemaName");
            dataTableMapping.ColumnMappings.Add("ObjectName", "ObjectName");
            dataTableMapping.ColumnMappings.Add("Description", "Description");
            dataTableMapping.ColumnMappings.Add("TypeDescription", "TypeDescription");
            dataTableMapping.ColumnMappings.Add("Definition", "Definition");
            dataTableMapping.ColumnMappings.Add("UsesAnsiNulls", "UsesAnsiNulls");
            dataTableMapping.ColumnMappings.Add("UsesQuotedIdentifier", "UsesQuotedIdentifier");
            dataTableMapping.ColumnMappings.Add("IsNotForReplication", "IsNotForReplication");
            dataTableMapping.ColumnMappings.Add("IsDisabled", "IsDisabled");
            dataTableMapping.ColumnMappings.Add("TriggerForSchema", "TriggerForSchema");
            dataTableMapping.ColumnMappings.Add("TriggerForObjectName", "TriggerForObjectName");

            DataAdapter = dataAdapter;
            DataAdapter.TableMappings.Add(dataTableMapping);
        }

		#endregion Constructors 

		#region Methods (3) 

		#region Public Methods (3) 

        public int Fill(DataConnectionManager dataConnectionManager, ModulesDataTable modulesDataTable)
        {
            return Fill(dataConnectionManager.DataConnection, modulesDataTable);
        }

        public int Fill(DbConnection dbConnection, ModulesDataTable modulesDataTable)
        {
            modulesDataTable.Clear();
            using (var dbCommmand = dbConnection.CreateCommand())
            {
                dbCommmand.CommandText = CommandText;
                DataAdapter.SelectCommand = dbCommmand;
                modulesDataTable.BeginLoadData();
                var result = DataAdapter.Fill(modulesDataTable);
                modulesDataTable.EndLoadData();
                DataAdapter.SelectCommand = null;
                return result;
            }
        }

        public ModulesDataTable GetData(DbConnection dbConnection)
        {
            var modulesDataTable = new ModulesDataTable();
            Fill(dbConnection, modulesDataTable);
            return modulesDataTable;
        }

		#endregion Public Methods 

		#endregion Methods 
    }
}
