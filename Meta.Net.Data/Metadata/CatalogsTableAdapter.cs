using System.Data.Common;
using Meta.Net.Data.Interfaces;

namespace Meta.Net.Data.Metadata
{
    public class CatalogsTableAdapter
    {
		#region Properties (2) 

        public string CommandText { get; set; }

        public DbDataAdapter DataAdapter { get; set; }

		#endregion Properties 

		#region Constructors (1) 

        public CatalogsTableAdapter(DbDataAdapter dataAdapter, IDataMetadataScriptProvider dataMetadataScriptProvider)
        {
            CommandText = dataMetadataScriptProvider.CatalogsSql;
            var dataTableMapping = new DataTableMapping { SourceTable = "Table", DataSetTable = "Catalogs" };
            dataTableMapping.ColumnMappings.Add("CollationName", "CollationName");
            dataTableMapping.ColumnMappings.Add("CompatibilityLevel", "CompatibilityLevel");
            dataTableMapping.ColumnMappings.Add("CreateDate", "CreateDate");
            dataTableMapping.ColumnMappings.Add("Description", "Description");
            dataTableMapping.ColumnMappings.Add("IsAnsiNullDefaultOn", "IsAnsiNullDefaultOn");
            dataTableMapping.ColumnMappings.Add("IsAnsiNullsOn", "IsAnsiNullsOn");
            dataTableMapping.ColumnMappings.Add("IsAnsiPaddingOn", "IsAnsiPaddingOn");
            dataTableMapping.ColumnMappings.Add("IsAnsiWarningsOn", "IsAnsiWarningsOn");
            dataTableMapping.ColumnMappings.Add("IsArithabortOn", "IsArithabortOn");
            dataTableMapping.ColumnMappings.Add("IsAutoCloseOn", "IsAutoCloseOn");
            dataTableMapping.ColumnMappings.Add("IsAutoCreateStatsOn", "IsAutoCreateStatsOn");
            dataTableMapping.ColumnMappings.Add("IsAutoShrinkOn", "IsAutoShrinkOn");
            dataTableMapping.ColumnMappings.Add("IsAutoUpdateStatsAsyncOn", "IsAutoUpdateStatsAsyncOn");
            dataTableMapping.ColumnMappings.Add("IsAutoUpdateStatsOn", "IsAutoUpdateStatsOn");
            dataTableMapping.ColumnMappings.Add("IsCleanlyShutdown", "IsCleanlyShutdown");
            dataTableMapping.ColumnMappings.Add("IsConcatNullYieldsNullOn", "IsConcatNullYieldsNullOn");
            dataTableMapping.ColumnMappings.Add("IsCursorCloseOnCommitOn", "IsCursorCloseOnCommitOn");
            dataTableMapping.ColumnMappings.Add("IsDateCorrelationOn", "IsDateCorrelationOn");
            dataTableMapping.ColumnMappings.Add("IsDbChainingOn", "IsDbChainingOn");
            dataTableMapping.ColumnMappings.Add("IsFulltextEnabled", "IsFulltextEnabled");
            dataTableMapping.ColumnMappings.Add("IsInStandby", "IsInStandby");
            dataTableMapping.ColumnMappings.Add("IsLocalCursorDefault", "IsLocalCursorDefault");
            dataTableMapping.ColumnMappings.Add("IsMasterKeyEncryptedByServer", "IsMasterKeyEncryptedByServer");
            dataTableMapping.ColumnMappings.Add("IsNumericRoundabortOn", "IsNumericRoundabortOn");
            dataTableMapping.ColumnMappings.Add("IsParameterizationForced", "IsParameterizationForced");
            dataTableMapping.ColumnMappings.Add("IsQuotedIdentifierOn", "IsQuotedIdentifierOn");
            dataTableMapping.ColumnMappings.Add("IsReadOnly", "IsReadOnly");
            dataTableMapping.ColumnMappings.Add("IsRecursiveTriggersOn", "IsRecursiveTriggersOn");
            dataTableMapping.ColumnMappings.Add("IsSupplementalLoggingEnabled", "IsSupplementalLoggingEnabled");
            dataTableMapping.ColumnMappings.Add("IsTrustworthyOn", "IsTrustworthyOn");
            dataTableMapping.ColumnMappings.Add("Namespace", "Namespace");
            dataTableMapping.ColumnMappings.Add("ObjectName", "ObjectName");
            dataTableMapping.ColumnMappings.Add("PageVerifyOption", "PageVerifyOption");
            dataTableMapping.ColumnMappings.Add("PageVerifyOptionDescription", "PageVerifyOptionDescription");
            dataTableMapping.ColumnMappings.Add("RecoveryModel", "RecoveryModel");
            dataTableMapping.ColumnMappings.Add("RecoveryModelDescription", "RecoveryModelDescription");
            dataTableMapping.ColumnMappings.Add("State", "State");
            dataTableMapping.ColumnMappings.Add("StateDescription", "StateDescription");
            dataTableMapping.ColumnMappings.Add("UserAccess", "UserAccess");
            dataTableMapping.ColumnMappings.Add("UserAccessDescription", "UserAccessDescription");

            DataAdapter = dataAdapter;
            DataAdapter.TableMappings.Add(dataTableMapping);
        }

        #endregion Constructors 

		#region Methods (3) 

		#region Public Methods (3) 

		public int Fill(DataConnectionManager dataConnectionManager, CatalogsDataTable catalogsDataTable)
		{
            return Fill(dataConnectionManager.DataConnection, catalogsDataTable); 
		}

        public int Fill(DbConnection dbConnection, CatalogsDataTable catalogsDataTable)
        {
            catalogsDataTable.Clear();
            using (var dbCommmand = dbConnection.CreateCommand())
            {
                dbCommmand.CommandText = CommandText;
                DataAdapter.SelectCommand = dbCommmand;
                catalogsDataTable.BeginLoadData();
                var result = DataAdapter.Fill(catalogsDataTable);
                catalogsDataTable.EndLoadData();
                DataAdapter.SelectCommand = null;
                return result;
            }
        }

        public CatalogsDataTable GetData(DbConnection dbConnection)
        {
            var catalogsDataTable = new CatalogsDataTable();
            Fill(dbConnection, catalogsDataTable);
            return catalogsDataTable;
        }

		#endregion Public Methods 

		#endregion Methods 
    }
}
