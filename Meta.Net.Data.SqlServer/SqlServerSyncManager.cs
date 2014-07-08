using System;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;
using Meta.Net.Data.Enum;
using Meta.Net.Data.Interfaces;
using Meta.Net.Data.Objects;

namespace Meta.Net.Data.SqlServer
{
    /// <summary>
    /// Entry point for synchronizing SQLServer databases. This object should only be
    /// instantiated through DataSyncManager objects.
    /// </summary>
    public class SqlServerSyncManager : DataSyncManager
    {
        #region Constructors (3)

        public SqlServerSyncManager(Server sourceServer, Server targetServer, DataProperties dataProperties)
            : base(sourceServer, targetServer, dataProperties)
        {
        }

        public SqlServerSyncManager(DataConnectionInfo sourceDataConnectionInfo, DataConnectionInfo targetDataConnectionInfo, DataProperties dataProperties)
            : base(sourceDataConnectionInfo, targetDataConnectionInfo, dataProperties)
        {
        }

        public SqlServerSyncManager(Server sourceServer, DataConnectionInfo targetDataConnectionInfo, DataProperties dataProperties)
            : base(sourceServer, targetDataConnectionInfo, dataProperties)
        {
        }

        #endregion Constructors

        #region Properties (11)

        private bool CurrentAnsiNulls { get; set; }

        private bool CurrentAnsiPadding { get; set; }

        private bool CurrentAnsiWarnings { get; set; }

        private Catalog CurrentCatalog { get; set; }

        private bool CurrentQuotedIdentifier { get; set; }

        private bool InitialAnsiNulls { get; set; }

        private bool InitialAnsiPadding { get; set; }

        private bool InitialAnsiWarnings { get; set; }

        private Catalog InitialCatalog { get; set; }

        private bool InitialQuotedIdentifier { get; set; }

        #endregion Properties

        #region Methods (14)

        // Protected Methods (6) 

        protected override void ChangeCatalog(DataContext sourceDataContext, DataContext targetDataContext, StringCollection stringCollection, Catalog catalog)
        {

            stringCollection.Add(DataActionFactory.UseCatalog(sourceDataContext, targetDataContext, catalog).ScriptAction());
            CurrentCatalog = catalog;
            if (InitialCatalog == null)
            {
                InitialCatalog = catalog;
                InitialAnsiNulls = catalog.IsAnsiNullsOn;
                CurrentAnsiNulls = catalog.IsAnsiNullsOn;
                InitialAnsiPadding = catalog.IsAnsiPaddingOn;
                CurrentAnsiPadding = catalog.IsAnsiPaddingOn;
                InitialAnsiWarnings = catalog.IsAnsiWarningsOn;
                CurrentAnsiWarnings = catalog.IsAnsiWarningsOn;
                InitialQuotedIdentifier = catalog.IsQuotedIdentifierOn;
                CurrentQuotedIdentifier = catalog.IsQuotedIdentifierOn;
            }

        }

        protected override void RestoreIsoState(DataContext sourceDataContext, DataContext targetDataContext, StringCollection stringCollection)
        {
            if (InitialAnsiNulls && !CurrentAnsiNulls)
                SetAnsiNullsOn(stringCollection);
            else if (!InitialAnsiNulls && CurrentAnsiNulls)
                SetAnsiNullsOff(stringCollection);

            if (InitialAnsiPadding && !CurrentAnsiPadding)
                SetAnsiPaddingOn(stringCollection);
            else if (!InitialAnsiPadding && CurrentAnsiPadding)
                SetAnsiPaddingOff(stringCollection);

            if (InitialQuotedIdentifier && !CurrentQuotedIdentifier)
                SetQuotedIdentifierOn(stringCollection);
            else if (!InitialQuotedIdentifier && CurrentQuotedIdentifier)
                SetQuotedIdentifierOff(stringCollection);

            if (InitialAnsiWarnings && !CurrentAnsiWarnings)
                SetAnsiWarningsOn(stringCollection);
            else if (!InitialAnsiWarnings && CurrentAnsiWarnings)
                SetAnsiWarningsOff(stringCollection);
        }

        protected override void RestoreIsoState(DataContext sourceDataContext, DataContext targetDataContext, StringCollection stringCollection, string delimiter)
        {
            if (InitialAnsiNulls && !CurrentAnsiNulls)
            {
                SetAnsiNullsOn(stringCollection);
                stringCollection.Add(delimiter);
            }
            else if (!InitialAnsiNulls && CurrentAnsiNulls)
            {
                SetAnsiNullsOff(stringCollection);
                stringCollection.Add(delimiter);
            }

            if (InitialAnsiPadding && !CurrentAnsiPadding)
            {
                SetAnsiPaddingOn(stringCollection);
                stringCollection.Add(delimiter);
            }
            else if (!InitialAnsiPadding && CurrentAnsiPadding)
            {
                SetAnsiPaddingOff(stringCollection);
                stringCollection.Add(delimiter);
            }

            if (InitialQuotedIdentifier && !CurrentQuotedIdentifier)
            {
                SetQuotedIdentifierOn(stringCollection);
                stringCollection.Add(delimiter);
            }
            else if (!InitialQuotedIdentifier && CurrentQuotedIdentifier)
            {
                SetQuotedIdentifierOff(stringCollection);
                stringCollection.Add(delimiter);
            }

            if (InitialAnsiWarnings && !CurrentAnsiWarnings)
            {
                SetAnsiWarningsOn(stringCollection);
                stringCollection.Add(delimiter);
            }
            else if (!InitialAnsiWarnings && CurrentAnsiWarnings)
            {
                SetAnsiWarningsOff(stringCollection);
                stringCollection.Add(delimiter);
            }
        }

        protected override void SetIsoState(DataContext sourceDataContext, DataContext targetDataContext, DataSyncAction syncAction, StringCollection stringCollection)
        {
            switch (syncAction.Type)
            {
                case DataSyncOperationType.CreateAggregateFunction:
                case DataSyncOperationType.CreateInlineTableValuedFunction:
                case DataSyncOperationType.CreateScalarFunction:
                case DataSyncOperationType.CreateStoredProcedure:
                case DataSyncOperationType.CreateTableValuedFunction:
                case DataSyncOperationType.CreateTrigger:
                case DataSyncOperationType.CreateView:
                case DataSyncOperationType.AlterAggregateFunction:
                case DataSyncOperationType.AlterInlineTableValuedFunction:
                case DataSyncOperationType.AlterScalarFunction:
                case DataSyncOperationType.AlterStoredProcedure:
                case DataSyncOperationType.AlterTableValuedFunction:
                case DataSyncOperationType.AlterTrigger:
                case DataSyncOperationType.AlterView:
                    IDataModule module = null;
                    if (syncAction.DataObject is IDataModule)
                        module = (IDataModule)syncAction.DataObject;

                    if (module != null)
                    {
                        if (CurrentCatalog == null)
                            ChangeCatalog(sourceDataContext, targetDataContext, stringCollection, module.Catalog);

                        if (string.Compare(module.Catalog.ObjectName, CurrentCatalog.ObjectName, StringComparison.OrdinalIgnoreCase) != 0)
                            ChangeCatalog(sourceDataContext, targetDataContext, stringCollection, module.Catalog);

                        if (module.UsesAnsiNulls && !CurrentAnsiNulls)
                            SetAnsiNullsOn(stringCollection);
                        else if (!module.UsesAnsiNulls && CurrentAnsiNulls)
                            SetAnsiNullsOff(stringCollection);

                        if (module.UsesQuotedIdentifier && !CurrentQuotedIdentifier)
                            SetQuotedIdentifierOn(stringCollection);
                        else if (!module.UsesQuotedIdentifier && CurrentQuotedIdentifier)
                            SetQuotedIdentifierOff(stringCollection);
                    }


                    if (!CurrentAnsiPadding)
                        SetAnsiPaddingOn(stringCollection);

                    if (!CurrentAnsiWarnings)
                        SetAnsiWarningsOn(stringCollection);
                    break;
                case DataSyncOperationType.CreateUserTable:
                    UserTable userTable = null;
                    if (syncAction.DataObject is UserTable)
                        userTable = (UserTable)syncAction.DataObject;

                    if (userTable != null)
                    {
                        if (CurrentCatalog == null)
                            ChangeCatalog(sourceDataContext, targetDataContext, stringCollection, userTable.Catalog);

                        if (string.Compare(userTable.Catalog.ObjectName, CurrentCatalog.ObjectName, StringComparison.OrdinalIgnoreCase) != 0)
                            ChangeCatalog(sourceDataContext, targetDataContext, stringCollection, userTable.Catalog);

                        if (userTable.UsesAnsiNulls && !CurrentAnsiNulls)
                            SetAnsiNullsOn(stringCollection);
                        else if (!userTable.UsesAnsiNulls && CurrentAnsiNulls)
                            SetAnsiNullsOff(stringCollection);

                        var createWithAnsiPadding = true;
                        foreach (var userTableColumn in userTable.UserTableColumns.Values)
                        {
                            if (!userTableColumn.IsAnsiPadded && IsDataTypeAnsiPaddable(userTableColumn.DataType))
                                createWithAnsiPadding = false;
                        }

                        if (createWithAnsiPadding && !CurrentAnsiPadding)
                            SetAnsiPaddingOn(stringCollection);
                        else if (!createWithAnsiPadding && CurrentAnsiPadding)
                            SetAnsiPaddingOff(stringCollection);
                    }

                    if (!CurrentQuotedIdentifier)
                        SetQuotedIdentifierOn(stringCollection);

                    if (!CurrentAnsiWarnings)
                        SetAnsiWarningsOn(stringCollection);
                    break;
                case DataSyncOperationType.AddUserTableColumn:
                case DataSyncOperationType.AlterUserTableColumn:
                    if (!CurrentAnsiNulls)
                        SetAnsiNullsOn(stringCollection);

                    if (!CurrentQuotedIdentifier)
                        SetQuotedIdentifierOn(stringCollection);

                    UserTableColumn alteredUserTableColumn = null;
                    if (syncAction.DataObject is UserTableColumn)
                        alteredUserTableColumn = (UserTableColumn)syncAction.DataObject;

                    if (alteredUserTableColumn != null)
                    {
                        if (CurrentCatalog == null)
                            ChangeCatalog(sourceDataContext, targetDataContext, stringCollection, alteredUserTableColumn.Catalog);

                        if (string.Compare(alteredUserTableColumn.Catalog.ObjectName, CurrentCatalog.ObjectName, StringComparison.OrdinalIgnoreCase) != 0)
                            ChangeCatalog(sourceDataContext, targetDataContext, stringCollection, alteredUserTableColumn.Catalog);

                        if (alteredUserTableColumn.IsAnsiPadded && !CurrentAnsiPadding)
                            SetAnsiPaddingOn(stringCollection);
                        else if (!alteredUserTableColumn.IsAnsiPadded && CurrentAnsiPadding)
                            SetAnsiPaddingOff(stringCollection);
                    }

                    if (CurrentAnsiWarnings)
                        SetAnsiWarningsOff(stringCollection);
                    break;
                default:
                    IDataCatalogBasedObject catalogBasedDataObject = null;
                    if (syncAction.DataObject is IDataCatalogBasedObject)
                        catalogBasedDataObject = (IDataCatalogBasedObject)syncAction.DataObject;

                    if (catalogBasedDataObject != null)
                    {
                        if (CurrentCatalog == null)
                            ChangeCatalog(sourceDataContext, targetDataContext, stringCollection, catalogBasedDataObject.Catalog);

                        if (string.Compare(catalogBasedDataObject.Catalog.ObjectName, CurrentCatalog.ObjectName, StringComparison.OrdinalIgnoreCase) != 0)
                            ChangeCatalog(sourceDataContext, targetDataContext, stringCollection, catalogBasedDataObject.Catalog);
                    }

                    if (!CurrentAnsiNulls)
                        SetAnsiNullsOn(stringCollection);

                    if (!CurrentAnsiPadding)
                        SetAnsiPaddingOn(stringCollection);

                    if (!CurrentQuotedIdentifier)
                        SetQuotedIdentifierOn(stringCollection);

                    if (!CurrentAnsiWarnings)
                        SetAnsiWarningsOn(stringCollection);
                    break;
            }
        }

        protected override void SetIsoState(DataContext sourceDataContext, DataContext targetDataContext, DataSyncAction syncAction, StringCollection stringCollection, string delimiter)
        {
            switch (syncAction.Type)
            {
                case DataSyncOperationType.CreateAggregateFunction:
                case DataSyncOperationType.CreateInlineTableValuedFunction:
                case DataSyncOperationType.CreateScalarFunction:
                case DataSyncOperationType.CreateStoredProcedure:
                case DataSyncOperationType.CreateTableValuedFunction:
                case DataSyncOperationType.CreateTrigger:
                case DataSyncOperationType.CreateView:
                case DataSyncOperationType.AlterAggregateFunction:
                case DataSyncOperationType.AlterInlineTableValuedFunction:
                case DataSyncOperationType.AlterScalarFunction:
                case DataSyncOperationType.AlterStoredProcedure:
                case DataSyncOperationType.AlterTableValuedFunction:
                case DataSyncOperationType.AlterTrigger:
                case DataSyncOperationType.AlterView:

                    IDataModule module = null;
                    if (syncAction.DataObject is IDataModule)
                        module = (IDataModule)syncAction.DataObject;

                    if (module != null)
                    {
                        if (CurrentCatalog == null)
                        {
                            ChangeCatalog(sourceDataContext, targetDataContext, stringCollection, module.Catalog);
                            stringCollection.Add(delimiter);
                        }

                        if (string.Compare(module.Catalog.ObjectName, CurrentCatalog.ObjectName, StringComparison.OrdinalIgnoreCase) != 0)
                        {
                            ChangeCatalog(sourceDataContext, targetDataContext, stringCollection, module.Catalog);
                            stringCollection.Add(delimiter);
                        }

                        if (module.UsesAnsiNulls && !CurrentAnsiNulls)
                        {
                            SetAnsiNullsOn(stringCollection);
                            stringCollection.Add(delimiter);
                        }
                        else if (!module.UsesAnsiNulls && CurrentAnsiNulls)
                        {
                            SetAnsiNullsOff(stringCollection);
                            stringCollection.Add(delimiter);
                        }

                        if (module.UsesQuotedIdentifier && !CurrentQuotedIdentifier)
                        {
                            SetQuotedIdentifierOn(stringCollection);
                            stringCollection.Add(delimiter);
                        }
                        else if (!module.UsesQuotedIdentifier && CurrentQuotedIdentifier)
                        {
                            SetQuotedIdentifierOff(stringCollection);
                            stringCollection.Add(delimiter);
                        }
                    }

                    if (!CurrentAnsiPadding)
                    {
                        SetAnsiPaddingOn(stringCollection);
                        stringCollection.Add(delimiter);
                    }

                    if (!CurrentAnsiWarnings)
                    {
                        SetAnsiWarningsOn(stringCollection);
                        stringCollection.Add(delimiter);
                    }

                    break;
                case DataSyncOperationType.CreateUserTable:
                    UserTable userTable = null;
                    if (syncAction.DataObject is UserTable)
                        userTable = (UserTable)syncAction.DataObject;

                    if (userTable != null)
                    {
                        if (CurrentCatalog == null)
                        {
                            ChangeCatalog(sourceDataContext, targetDataContext, stringCollection, userTable.Catalog);
                            stringCollection.Add(delimiter);
                        }

                        if (string.Compare(userTable.Catalog.ObjectName, CurrentCatalog.ObjectName, StringComparison.OrdinalIgnoreCase) != 0)
                        {
                            ChangeCatalog(sourceDataContext, targetDataContext, stringCollection, userTable.Catalog);
                            stringCollection.Add(delimiter);
                        }

                        if (userTable.UsesAnsiNulls && !CurrentAnsiNulls)
                        {
                            SetAnsiNullsOn(stringCollection);
                            stringCollection.Add(delimiter);
                        }
                        else if (!userTable.UsesAnsiNulls && CurrentAnsiNulls)
                        {
                            SetAnsiNullsOff(stringCollection);
                            stringCollection.Add(delimiter);
                        }

                        var createWithAnsiPadding = true;
                        foreach (var userTableColumn in userTable.UserTableColumns.Values)
                        {
                            if (!userTableColumn.IsAnsiPadded && IsDataTypeAnsiPaddable(userTableColumn.DataType))
                                createWithAnsiPadding = false;
                        }

                        if (createWithAnsiPadding && !CurrentAnsiPadding)
                        {
                            SetAnsiPaddingOn(stringCollection);
                            stringCollection.Add(delimiter);
                        }
                        else if (!createWithAnsiPadding && CurrentAnsiPadding)
                        {
                            SetAnsiPaddingOff(stringCollection);
                            stringCollection.Add(delimiter);
                        }
                    }

                    if (!CurrentQuotedIdentifier)
                    {
                        SetQuotedIdentifierOn(stringCollection);
                        stringCollection.Add(delimiter);
                    }

                    if (!CurrentAnsiWarnings)
                    {
                        SetAnsiWarningsOn(stringCollection);
                        stringCollection.Add(delimiter);
                    }
                    break;
                case DataSyncOperationType.AddUserTableColumn:
                case DataSyncOperationType.AlterUserTableColumn:
                    if (!CurrentAnsiNulls)
                    {
                        SetAnsiNullsOn(stringCollection);
                        stringCollection.Add(delimiter);
                    }

                    if (!CurrentQuotedIdentifier)
                    {
                        SetQuotedIdentifierOn(stringCollection);
                        stringCollection.Add(delimiter);
                    }

                    UserTableColumn alteredUserTableColumn = null;
                    if (syncAction.DataObject is UserTableColumn)
                        alteredUserTableColumn = (UserTableColumn)syncAction.DataObject;

                    if (alteredUserTableColumn != null)
                    {
                        if (CurrentCatalog == null)
                        {
                            ChangeCatalog(sourceDataContext, targetDataContext, stringCollection, alteredUserTableColumn.Catalog);
                            stringCollection.Add(delimiter);
                        }

                        if (string.Compare(alteredUserTableColumn.Catalog.ObjectName, CurrentCatalog.ObjectName, StringComparison.OrdinalIgnoreCase) != 0)
                        {
                            ChangeCatalog(sourceDataContext, targetDataContext, stringCollection, alteredUserTableColumn.Catalog);
                            stringCollection.Add(delimiter);
                        }

                        if (alteredUserTableColumn.IsAnsiPadded && !CurrentAnsiPadding)
                        {
                            SetAnsiPaddingOn(stringCollection);
                            stringCollection.Add(delimiter);
                        }
                        else if (!alteredUserTableColumn.IsAnsiPadded && CurrentAnsiPadding)
                        {
                            SetAnsiPaddingOff(stringCollection);
                            stringCollection.Add(delimiter);
                        }
                    }

                    if (CurrentAnsiWarnings)
                    {
                        SetAnsiWarningsOff(stringCollection);
                        stringCollection.Add(delimiter);
                    }

                    break;
                default:
                    IDataCatalogBasedObject catalogBasedDataObject = null;
                    if (syncAction.DataObject is IDataCatalogBasedObject)
                        catalogBasedDataObject = (IDataCatalogBasedObject)syncAction.DataObject;

                    if (catalogBasedDataObject != null)
                    {
                        if (CurrentCatalog == null)
                        {
                            ChangeCatalog(sourceDataContext, targetDataContext, stringCollection, catalogBasedDataObject.Catalog);
                            stringCollection.Add(delimiter);
                        }

                        if (string.Compare(catalogBasedDataObject.Catalog.ObjectName, CurrentCatalog.ObjectName, StringComparison.OrdinalIgnoreCase) != 0)
                        {
                            ChangeCatalog(sourceDataContext, targetDataContext, stringCollection, catalogBasedDataObject.Catalog);
                            stringCollection.Add(delimiter);
                        }
                    }

                    if (!CurrentAnsiNulls)
                    {
                        SetAnsiNullsOn(stringCollection);
                        stringCollection.Add(delimiter);
                    }

                    if (!CurrentQuotedIdentifier)
                    {
                        SetQuotedIdentifierOn(stringCollection);
                        stringCollection.Add(delimiter);
                    }

                    if (!CurrentAnsiPadding)
                    {
                        SetAnsiPaddingOn(stringCollection);
                        stringCollection.Add(delimiter);
                    }

                    if (!CurrentAnsiWarnings)
                    {
                        SetAnsiWarningsOn(stringCollection);
                        stringCollection.Add(delimiter);
                    }

                    break;
            }
        }

        /// <summary>
        /// Initiates the synchronization of source and target databases by cycling
        /// through a DataSyncActionsCollection and executing the script member
        /// of each DataSyncAction object.
        /// </summary>
        /// <returns>
        /// True  - synchronized
        /// True  - SyncActionsCollection.Count == 0, because CompareForSync() not called.
        /// False - exceptions encountered
        /// </returns>
        protected override bool SyncInternal()
        {
            Exceptions.Clear();
            if (DataSyncActionsCollection.Count == 0)
                return true;

            var scripts = Script();
            if (scripts.Count == 0)
                return true;

            var start = new TimeSpan(DateTime.Now.Ticks);
            DataTimer.RaiseTimerStatusEvent(
                string.Format("Connecting to database with connection name [{0}]",
                TargetDataConnectionInfo.Name));

            var random = new Random();
            var transactionNumber = random.Next();
            var transactionName = string.Format("IdioSyncracy{0}", transactionNumber);

            using (var sqlConnection = new SqlConnection(TargetDataConnectionInfo.ConnectionString))
            {
                sqlConnection.Open();

                var finish = new TimeSpan(DateTime.Now.Ticks);
                var interval = finish.Subtract(start);
                DataTimer.RaiseTimerStatusEvent(interval, "Database connection established.");

                if (!TargetDataConnectionInfo.IsAcceptableVersion(sqlConnection))
                    TargetDataConnectionInfo.ThrowVersionException();

                var sqlCommand = sqlConnection.CreateCommand();
                var sqlTransaction = sqlConnection.BeginTransaction(transactionName);
                sqlCommand.Connection = sqlConnection;
                sqlCommand.Transaction = sqlTransaction;
                sqlCommand.Parameters.Add("@ReturnValue", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;

                try
                {
                    foreach (var script in scripts)
                    {
                        sqlCommand.CommandText = script;
                        var rowsAffected = sqlCommand.ExecuteNonQuery();

                        if (rowsAffected == -1)
                            continue;

                        var exception = new Exception("The following query failed to execute: " + sqlCommand.CommandText);
                        Exceptions.Add(exception);
                        sqlTransaction.Rollback(transactionName);

                        return false;
                    }

                    sqlTransaction.Commit();

                    finish = new TimeSpan(DateTime.Now.Ticks);
                    interval = finish.Subtract(start);
                    DataTimer.RaiseTimerStatusEvent(interval, "SQL Transaction commited successfully.");

                    sqlConnection.Close();

                    return true;
                }
                catch (Exception ex1)
                {
                    Exceptions.Add(ex1);

                    try
                    {
                        sqlTransaction.Rollback(transactionName);
                        sqlConnection.Close();
                    }
                    catch (Exception ex2)
                    {
                        // This catch block will handle any errors that may have occurred
                        // on the server that would cause the rollback to fail, such as
                        // a closed connection.

                        Exceptions.Add(ex2);
                    }

                    return false;
                }
            }
        }
        // Private Methods (8) 
        private static bool IsDataTypeAnsiPaddable(string dataType)
        {
            return (dataType == "char" || dataType == "varchar" || dataType == "binary" || dataType == "varbinary");
        }

        private void SetAnsiNullsOff(StringCollection stringCollection)
        {
            stringCollection.Add("SET ANSI_NULLS OFF");
            CurrentAnsiNulls = false;
        }

        private void SetAnsiNullsOn(StringCollection stringCollection)
        {
            stringCollection.Add("SET ANSI_NULLS ON");
            CurrentAnsiNulls = true;
        }

        private void SetAnsiPaddingOff(StringCollection stringCollection)
        {
            stringCollection.Add("SET ANSI_PADDING OFF");
            CurrentAnsiPadding = false;
        }

        private void SetAnsiPaddingOn(StringCollection stringCollection)
        {
            stringCollection.Add("SET ANSI_PADDING ON");
            CurrentAnsiPadding = true;
        }

        private void SetAnsiWarningsOff(StringCollection stringCollection)
        {
            stringCollection.Add("SET ANSI_WARNINGS OFF");
            CurrentAnsiWarnings = false;
        }

        private void SetAnsiWarningsOn(StringCollection stringCollection)
        {
            stringCollection.Add("SET ANSI_WARNINGS ON");
            CurrentAnsiWarnings = true;
        }

        private void SetQuotedIdentifierOff(StringCollection stringCollection)
        {
            stringCollection.Add("SET QUOTED_IDENTIFIER OFF");
            CurrentQuotedIdentifier = false;
        }

        private void SetQuotedIdentifierOn(StringCollection stringCollection)
        {
            stringCollection.Add("SET QUOTED_IDENTIFIER ON");
            CurrentQuotedIdentifier = true;
        }

        #endregion Methods 
    }
}
