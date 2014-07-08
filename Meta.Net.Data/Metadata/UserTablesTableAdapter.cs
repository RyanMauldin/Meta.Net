using System.Data.Common;
using Meta.Net.Data.Interfaces;

namespace Meta.Net.Data.Metadata
{
    public class UserTablesTableAdapter
    {
        #region Properties (2)

        public string CommandText { get; set; }

        public DbDataAdapter DataAdapter { get; set; }

        #endregion Properties

        #region Constructors (1)

        public UserTablesTableAdapter(DbDataAdapter dataAdapter, IDataMetadataScriptProvider dataMetadataScriptProvider, string catalogName)
        {
            CommandText = dataMetadataScriptProvider.UserTablesSql2(catalogName);
            var dataTableMapping = new DataTableMapping { SourceTable = "Table", DataSetTable = "UserTables" };
            dataTableMapping.ColumnMappings.Add("Namespace", "Namespace");
            dataTableMapping.ColumnMappings.Add("CatalogName", "CatalogName");
            dataTableMapping.ColumnMappings.Add("SchemaName", "SchemaName");
            dataTableMapping.ColumnMappings.Add("ObjectName", "ObjectName");
            dataTableMapping.ColumnMappings.Add("Description", "Description");
            dataTableMapping.ColumnMappings.Add("TypeDescription", "TypeDescription");
            dataTableMapping.ColumnMappings.Add("FileStreamFileGroup", "FileStreamFileGroup");
            dataTableMapping.ColumnMappings.Add("LobFileGroup", "LobFileGroup");
            dataTableMapping.ColumnMappings.Add("HasTextNTextOrImageColumns", "HasTextNTextOrImageColumns");
            dataTableMapping.ColumnMappings.Add("UsesAnsiNulls", "UsesAnsiNulls");
            dataTableMapping.ColumnMappings.Add("TextInRowLimit", "TextInRowLimit");

            DataAdapter = dataAdapter;
            DataAdapter.TableMappings.Add(dataTableMapping);
        }

        #endregion Constructors

        #region Methods (3)

        #region Public Methods (3)

        public int Fill(DataConnectionManager dataConnectionManager, UserTablesDataTable userTablesDataTable)
        {
            return Fill(dataConnectionManager.DataConnection, userTablesDataTable);
        }

        public int Fill(DbConnection dbConnection, UserTablesDataTable userTablesDataTable)
        {
            userTablesDataTable.Clear();
            using (var dbCommmand = dbConnection.CreateCommand())
            {
                dbCommmand.CommandText = CommandText;
                DataAdapter.SelectCommand = dbCommmand;
                userTablesDataTable.BeginLoadData();
                var result = DataAdapter.Fill(userTablesDataTable);
                userTablesDataTable.EndLoadData();
                DataAdapter.SelectCommand = null;
                return result;
            }
        }

        public UserTablesDataTable GetData(DbConnection dbConnection)
        {
            var userTablesDataTable = new UserTablesDataTable();
            Fill(dbConnection, userTablesDataTable);
            return userTablesDataTable;
        }

        #endregion Public Methods

        #endregion Methods
    }
}
