using System.Data.Common;
using Meta.Net.Data.Interfaces;

namespace Meta.Net.Data.Metadata
{
    public class ForeignKeysTableAdapter
    {
		#region Properties (2) 

        public string CommandText { get; set; }

        public DbDataAdapter DataAdapter { get; set; }

		#endregion Properties 

		#region Constructors (1) 

        public ForeignKeysTableAdapter(DbDataAdapter dataAdapter, IDataMetadataScriptProvider dataMetadataScriptProvider, string catalogName)
        {
            CommandText = dataMetadataScriptProvider.ForeignKeysSql2(catalogName);
            var dataTableMapping = new DataTableMapping { SourceTable = "Table", DataSetTable = "ForeignKeys" };
            dataTableMapping.ColumnMappings.Add("CatalogName", "CatalogName");
            dataTableMapping.ColumnMappings.Add("ColumnName", "ColumnName");
            dataTableMapping.ColumnMappings.Add("DeleteAction", "DeleteAction");
            dataTableMapping.ColumnMappings.Add("DeleteActionDescription", "DeleteActionDescription");
            dataTableMapping.ColumnMappings.Add("Description", "Description");
            dataTableMapping.ColumnMappings.Add("IsDisabled", "IsDisabled");
            dataTableMapping.ColumnMappings.Add("IsNotForReplication", "IsNotForReplication");
            dataTableMapping.ColumnMappings.Add("IsNotTrusted", "IsNotTrusted");
            dataTableMapping.ColumnMappings.Add("IsSystemNamed", "IsSystemNamed");
            dataTableMapping.ColumnMappings.Add("KeyOrdinal", "KeyOrdinal");
            dataTableMapping.ColumnMappings.Add("Namespace", "Namespace");
            dataTableMapping.ColumnMappings.Add("NamespaceGroup", "NamespaceGroup");
            dataTableMapping.ColumnMappings.Add("ObjectName", "ObjectName");
            dataTableMapping.ColumnMappings.Add("SchemaName", "SchemaName");
            dataTableMapping.ColumnMappings.Add("TableName", "TableName");
            dataTableMapping.ColumnMappings.Add("UpdateAction", "UpdateAction");
            dataTableMapping.ColumnMappings.Add("UpdateActionDescription", "UpdateActionDescription");
            
            DataAdapter = dataAdapter;
            DataAdapter.TableMappings.Add(dataTableMapping);
        }

		#endregion Constructors 

		#region Methods (3) 

		#region Public Methods (3) 

        public int Fill(DataConnectionManager dataConnectionManager, ForeignKeysDataTable foreignKeysDataTable)
        {
            return Fill(dataConnectionManager.DataConnection, foreignKeysDataTable);
        }

        public int Fill(DbConnection dbConnection, ForeignKeysDataTable foreignKeysDataTable)
        {
            foreignKeysDataTable.Clear();
            using (var dbCommmand = dbConnection.CreateCommand())
            {
                dbCommmand.CommandText = CommandText;
                DataAdapter.SelectCommand = dbCommmand;
                foreignKeysDataTable.BeginLoadData();
                var result = DataAdapter.Fill(foreignKeysDataTable);
                foreignKeysDataTable.EndLoadData();
                DataAdapter.SelectCommand = null;
                return result;
            }
        }

        public ForeignKeysDataTable GetData(DbConnection dbConnection)
        {
            var foreignKeysDataTable = new ForeignKeysDataTable();
            Fill(dbConnection, foreignKeysDataTable);
            return foreignKeysDataTable;
        }

		#endregion Public Methods 

		#endregion Methods 
    }
}
