using System.Data.Common;
using Meta.Net.Data.Interfaces;

namespace Meta.Net.Data.Metadata
{
    public class PrimaryKeysTableAdapter
    {
		#region Properties (2) 

        public string CommandText { get; set; }

        public DbDataAdapter DataAdapter { get; set; }

		#endregion Properties 

		#region Constructors (1) 

        public PrimaryKeysTableAdapter(DbDataAdapter dataAdapter, IDataMetadataScriptProvider dataMetadataScriptProvider, string catalogName)
        {
            CommandText = dataMetadataScriptProvider.PrimaryKeysSql2(catalogName);
            var dataTableMapping = new DataTableMapping { SourceTable = "Table", DataSetTable = "PrimaryKeys" };
            dataTableMapping.ColumnMappings.Add("AllowPageLocks", "AllowPageLocks");
            dataTableMapping.ColumnMappings.Add("AllowRowLocks", "AllowRowLocks");
            dataTableMapping.ColumnMappings.Add("CatalogName", "CatalogName");
            dataTableMapping.ColumnMappings.Add("ColumnName", "ColumnName");
            dataTableMapping.ColumnMappings.Add("Description", "Description");
            dataTableMapping.ColumnMappings.Add("FileGroup", "FileGroup");
            dataTableMapping.ColumnMappings.Add("FillFactor", "FillFactor");
            dataTableMapping.ColumnMappings.Add("IgnoreDupKey", "IgnoreDupKey");
            dataTableMapping.ColumnMappings.Add("IndexType", "IndexType");
            dataTableMapping.ColumnMappings.Add("IsDescendingKey", "IsDescendingKey");
            dataTableMapping.ColumnMappings.Add("IsDisabled", "IsDisabled");
            dataTableMapping.ColumnMappings.Add("IsPadded", "IsPadded");
            dataTableMapping.ColumnMappings.Add("KeyOrdinal", "KeyOrdinal");
            dataTableMapping.ColumnMappings.Add("Namespace", "Namespace");
            dataTableMapping.ColumnMappings.Add("NamespaceGroup", "NamespaceGroup");
            dataTableMapping.ColumnMappings.Add("ObjectName", "ObjectName");
            dataTableMapping.ColumnMappings.Add("PartitionOrdinal", "PartitionOrdinal");
            dataTableMapping.ColumnMappings.Add("SchemaName", "SchemaName");
            dataTableMapping.ColumnMappings.Add("TableName", "TableName");

            DataAdapter = dataAdapter;
            DataAdapter.TableMappings.Add(dataTableMapping);
        }

		#endregion Constructors 

		#region Methods (3) 

		#region Public Methods (3) 

        public int Fill(DataConnectionManager dataConnectionManager, PrimaryKeysDataTable primaryKeysDataTable)
        {
            return Fill(dataConnectionManager.DataConnection, primaryKeysDataTable);
        }

        public int Fill(DbConnection dbConnection, PrimaryKeysDataTable primaryKeysDataTable)
        {
            primaryKeysDataTable.Clear();
            using (var dbCommmand = dbConnection.CreateCommand())
            {
                dbCommmand.CommandText = CommandText;
                DataAdapter.SelectCommand = dbCommmand;
                primaryKeysDataTable.BeginLoadData();
                var result = DataAdapter.Fill(primaryKeysDataTable);
                primaryKeysDataTable.EndLoadData();
                DataAdapter.SelectCommand = null;
                return result;
            }
        }

        public PrimaryKeysDataTable GetData(DbConnection dbConnection)
        {
            var primaryKeysDataTable = new PrimaryKeysDataTable();
            Fill(dbConnection, primaryKeysDataTable);
            return primaryKeysDataTable;
        }

		#endregion Public Methods 

		#endregion Methods 
    }
}
