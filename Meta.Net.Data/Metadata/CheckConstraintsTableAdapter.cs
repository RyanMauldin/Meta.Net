using System.Data.Common;
using Meta.Net.Data.Interfaces;

namespace Meta.Net.Data.Metadata
{
    public class CheckConstraintsTableAdapter
    {
		#region Properties (2) 

        public string CommandText { get; set; }

        public DbDataAdapter DataAdapter { get; set; }

		#endregion Properties 

		#region Constructors (1) 

        public CheckConstraintsTableAdapter(DbDataAdapter dataAdapter, IDataMetadataScriptProvider dataMetadataScriptProvider, string catalogName)
        {
            CommandText = dataMetadataScriptProvider.CheckConstraintsSql2(catalogName);
            var dataTableMapping = new DataTableMapping { SourceTable = "Table", DataSetTable = "CheckConstraints" };
            dataTableMapping.ColumnMappings.Add("CatalogName", "CatalogName");
            dataTableMapping.ColumnMappings.Add("ColumnName", "ColumnName");
            dataTableMapping.ColumnMappings.Add("Definition", "Definition");
            dataTableMapping.ColumnMappings.Add("Description", "Description");
            dataTableMapping.ColumnMappings.Add("IsDisabled", "IsDisabled");
            dataTableMapping.ColumnMappings.Add("IsNotForReplication", "IsNotForReplication");
            dataTableMapping.ColumnMappings.Add("IsNotTrusted", "IsNotTrusted");
            dataTableMapping.ColumnMappings.Add("IsSystemNamed", "IsSystemNamed");
            dataTableMapping.ColumnMappings.Add("IsTableConstraint", "IsTableConstraint");
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

        public int Fill(DataConnectionManager dataConnectionManager, CheckConstraintsDataTable checkConstraintsDataTable)
        {
            return Fill(dataConnectionManager.DataConnection, checkConstraintsDataTable); 
        }

        public int Fill(DbConnection dbConnection, CheckConstraintsDataTable checkConstraintsDataTable)
        {
            checkConstraintsDataTable.Clear();
            using (var dbCommmand = dbConnection.CreateCommand())
            {
                dbCommmand.CommandText = CommandText;
                DataAdapter.SelectCommand = dbCommmand;
                checkConstraintsDataTable.BeginLoadData();
                var result = DataAdapter.Fill(checkConstraintsDataTable);
                checkConstraintsDataTable.EndLoadData();
                DataAdapter.SelectCommand = null;
                return result;
            }
        }

        public CheckConstraintsDataTable GetData(DbConnection dbConnection)
        {
            var checkConstraintsDataTable = new CheckConstraintsDataTable();
            Fill(dbConnection, checkConstraintsDataTable);
            return checkConstraintsDataTable;
        }

		#endregion Public Methods 

		#endregion Methods 
    }
}
