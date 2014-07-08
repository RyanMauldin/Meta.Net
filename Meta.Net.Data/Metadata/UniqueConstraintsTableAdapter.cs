using System.Data.Common;
using Meta.Net.Data.Interfaces;

namespace Meta.Net.Data.Metadata
{
    public class UniqueConstraintsTableAdapter
    {
		#region Properties (2) 

        public string CommandText { get; set; }

        public DbDataAdapter DataAdapter { get; set; }

		#endregion Properties 

		#region Constructors (1) 

        public UniqueConstraintsTableAdapter(DbDataAdapter dataAdapter, IDataMetadataScriptProvider dataMetadataScriptProvider, string catalogName)
        {
            CommandText = dataMetadataScriptProvider.UniqueConstraintsSql2(catalogName);
            var dataTableMapping = new DataTableMapping { SourceTable = "Table", DataSetTable = "UniqueConstraints" };
            dataTableMapping.ColumnMappings.Add("AllowPageLocks", "AllowPageLocks");
            dataTableMapping.ColumnMappings.Add("AllowRowLocks", "AllowRowLocks");
            dataTableMapping.ColumnMappings.Add("CatalogName", "CatalogName");
            dataTableMapping.ColumnMappings.Add("ColumnName", "ColumnName");
            dataTableMapping.ColumnMappings.Add("Description", "Description");
            dataTableMapping.ColumnMappings.Add("FileGroup", "FileGroup");
            dataTableMapping.ColumnMappings.Add("FillFactor", "FillFactor");
            dataTableMapping.ColumnMappings.Add("IgnoreDupKey", "IgnoreDupKey");
            dataTableMapping.ColumnMappings.Add("IndexType", "IndexType");
            dataTableMapping.ColumnMappings.Add("IsClustered", "IsClustered");
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

        public int Fill(DataConnectionManager dataConnectionManager, UniqueConstraintsDataTable uniqueConstraintsDataTable)
        {
            return Fill(dataConnectionManager.DataConnection, uniqueConstraintsDataTable);
        }

        public int Fill(DbConnection dbConnection, UniqueConstraintsDataTable uniqueConstraintsDataTable)
        {
            uniqueConstraintsDataTable.Clear();
            using (var dbCommmand = dbConnection.CreateCommand())
            {
                dbCommmand.CommandText = CommandText;
                DataAdapter.SelectCommand = dbCommmand;
                uniqueConstraintsDataTable.BeginLoadData();
                var result = DataAdapter.Fill(uniqueConstraintsDataTable);
                uniqueConstraintsDataTable.EndLoadData();
                DataAdapter.SelectCommand = null;
                return result;
            }
        }

        public UniqueConstraintsDataTable GetData(DbConnection dbConnection)
        {
            var uniqueConstraintsDataTable = new UniqueConstraintsDataTable();
            Fill(dbConnection, uniqueConstraintsDataTable);
            return uniqueConstraintsDataTable;
        }

		#endregion Public Methods 

		#endregion Methods 
    }
}
