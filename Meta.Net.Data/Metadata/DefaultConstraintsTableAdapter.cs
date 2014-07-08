using System.Data.Common;
using Meta.Net.Data.Interfaces;

namespace Meta.Net.Data.Metadata
{
    public class DefaultConstraintsTableAdapter
    {
        #region Properties (2)

        public string CommandText { get; set; }

        public DbDataAdapter DataAdapter { get; set; }

        #endregion Properties

        #region Constructors (1)

        public DefaultConstraintsTableAdapter(DbDataAdapter dataAdapter, IDataMetadataScriptProvider dataMetadataScriptProvider, string catalogName)
        {
            CommandText = dataMetadataScriptProvider.DefaultConstraintsSql2(catalogName);
            var dataTableMapping = new DataTableMapping { SourceTable = "Table", DataSetTable = "DefaultConstraints" };
            dataTableMapping.ColumnMappings.Add("CatalogName", "CatalogName");
            dataTableMapping.ColumnMappings.Add("ColumnName", "ColumnName");
            dataTableMapping.ColumnMappings.Add("Definition", "Definition");
            dataTableMapping.ColumnMappings.Add("Description", "Description");
            dataTableMapping.ColumnMappings.Add("IsSystemNamed", "IsSystemNamed");
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

        public int Fill(DataConnectionManager dataConnectionManager, DefaultConstraintsDataTable defaultConstraintsDataTable)
        {
            return Fill(dataConnectionManager.DataConnection, defaultConstraintsDataTable);
        }

        public int Fill(DbConnection dbConnection, DefaultConstraintsDataTable defaultConstraintsDataTable)
        {
            defaultConstraintsDataTable.Clear();
            using (var dbCommmand = dbConnection.CreateCommand())
            {
                dbCommmand.CommandText = CommandText;
                DataAdapter.SelectCommand = dbCommmand;
                defaultConstraintsDataTable.BeginLoadData();
                var result = DataAdapter.Fill(defaultConstraintsDataTable);
                defaultConstraintsDataTable.EndLoadData();
                DataAdapter.SelectCommand = null;
                return result;
            }
        }

        public DefaultConstraintsDataTable GetData(DbConnection dbConnection)
        {
            var defaultConstraintsDataTable = new DefaultConstraintsDataTable();
            Fill(dbConnection, defaultConstraintsDataTable);
            return defaultConstraintsDataTable;
        }

        #endregion Public Methods

        #endregion Methods
    }
}
