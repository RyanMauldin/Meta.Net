using System.Collections.Specialized;
using System.Linq;
using Meta.Net.Interfaces;
using Meta.Net.Objects;
using System;
using System.Collections.Generic;
using Meta.Net.Sync.Interfaces;

// NOTE: I am busy rewriting the entire Meta.Net, Meta.Net.MySql, and Meta.Net.SqlServer libraries first
// before even considering attempting the Sync again. I must at least get those perfect, before rewriting
// this enite Meta.Net.Sync library. For one, dependencies were wrong and their order was not solid. Also,
// Schemas are one thing, but getting and maintaining the data as well, is an entirely different story and
// it had no buffering.

namespace Meta.Net.Sync
{

    /// <summary>
    /// A delegate method for subscribing to DataEvents. DataProperties like TightSync
    /// will drop an entire schema and all of the database objects in that schema if it does not
    /// exist in the source database. It is important to warn users of actions like this in the
    /// case that this is not a desired result. The default for non-subscribed events is false and
    /// the TightSyn property is will be overridden to avoid dropping entire schemas and tables,
    /// foreign keys and modules (functions, stored procs, constraints, etc...).
    /// </summary>
    /// <param name="type">database warning, etc.</param>
    /// <param name="title">titlebar of dialog window</param>
    /// <param name="subTitle">title to display inside dialog window</param>
    /// <param name="message">message to display inside dialog window</param>
    /// <returns>
    /// True - user pressed OK in dialog window.
    /// False - user pressed Cancel in dialog window.
    /// </returns>
    public delegate bool DataEventMessage(string type, string title, string subTitle, string message);

    /// <summary>
    /// Main entry point for synchronization of database objects. The thread instantiating this
    /// class should call the CompareForSync() method first to create a list of DataSyncActions
    /// to perform to synchronize the two datasources. The Sync() method can be called next
    /// to cycle through the DataSyncActions collection to make the changes to the target
    /// database and/or synchronize the data.
    /// 
    /// Optional Event Subscription Locations :
    /// DataSyncManger.DataEvent - Delegate Type [DataEventMessage] - used for user responses
    /// to events like TightSync warnings when whole schemas will be dropped. If not subscribed to,
    /// the default response is false. If the thread instantiating this object is a console type
    /// application and this option is always wanted, simply subscribe to the delegate and always
    /// return true.
    /// 
    /// DataSyncManger.Timer.TimerStatus - Delegate Type [DataTimerStatus] - Subscribe to this
    /// delegate if you wish to recieve status updates on synchronization tasks.
    /// </summary>
    public abstract class DataSyncManager : ISyncManager, IScriptable
    {
		#region Constructors (3) 

        protected DataSyncManager()
        {
            throw new NotImplementedException();
            Exceptions = new List<Exception>();
            DataTimer = new DataTimer();
            
            DataSyncActionsCollection = new DataSyncActionsCollection();
            DataProperties =  new DataProperties();
            TotalDataEventWaitTime = new TimeSpan(DateTime.Now.Ticks);
            TotalDataEventWaitTime = TotalDataEventWaitTime.Subtract(TotalDataEventWaitTime);

            SourceDataConnectionInfo = null;
            SourceDataConnectionManager = null;
            SourceServer = null;

            TargetDataConnectionInfo = null;
            TargetDataConnectionManager = null;
            TargetServer = null;
        }

        /// <summary>
        /// Constructor initializes the instance specific DataSyncManager and sets up all
        /// objects needed to perform CompareForSync() and Sync() methods from the
        /// source server and target server. The source and/or target server objects
        /// may/may not be custom server models pulled from a Json file or could have
        /// been created on the fly and passed to this DataSyncManager object.
        /// </summary>
        /// <param name="sourceServer">source Server object</param>
        /// <param name="targetServer">target Server object</param>
        /// <param name="dataProperties">data properties</param>
        protected DataSyncManager(Server sourceServer, Server targetServer, DataProperties dataProperties)
        {
            throw new NotImplementedException();
            Exceptions = new List<Exception>();
            DataTimer = new DataTimer();
            DataSyncActionsCollection = new DataSyncActionsCollection();
            DataProperties = dataProperties ?? new DataProperties();
            TotalDataEventWaitTime = new TimeSpan(DateTime.Now.Ticks);
            TotalDataEventWaitTime = TotalDataEventWaitTime.Subtract(TotalDataEventWaitTime);

            SourceDataConnectionInfo = null;
            SourceDataConnectionManager = null;
            SourceServer = sourceServer; // Could be null...

            TargetDataConnectionInfo = null;
            TargetDataConnectionManager = null;
            TargetServer = targetServer; // Could be null...
        }

        /// <summary>
        /// Constructor initializes the instance specific DataSyncManager and sets up all
        /// objects needed to perform CompareForSync() and Sync() methods from the
        /// source and target connections.
        /// </summary>
        /// <param name="sourceDataConnectionInfo">source data connection info</param>
        /// <param name="targetDataConnectionInfo">target data connection info</param>
        /// <param name="dataProperties">data properties</param>
        protected DataSyncManager(DataConnectionInfo sourceDataConnectionInfo
            , DataConnectionInfo targetDataConnectionInfo, DataProperties dataProperties)
        {
            throw new NotImplementedException();
            Exceptions = new List<Exception>();
            DataTimer = new DataTimer();
            DataSyncActionsCollection = new DataSyncActionsCollection();
            DataProperties = dataProperties ?? new DataProperties();
            TotalDataEventWaitTime = new TimeSpan(DateTime.Now.Ticks);
            TotalDataEventWaitTime = TotalDataEventWaitTime.Subtract(TotalDataEventWaitTime);
            SourceServer = null;
            TargetServer = null;

            try
            {
                SourceDataConnectionInfo = sourceDataConnectionInfo;
            }
            catch (Exception ex)
            {
                SourceDataConnectionInfo = null;
                SourceDataConnectionManager = null;
                Exceptions.Add(ex);
            }

            try
            {
                TargetDataConnectionInfo = targetDataConnectionInfo;
            }
            catch (Exception ex)
            {
                TargetDataConnectionInfo = null;
                TargetDataConnectionManager = null;
                Exceptions.Add(ex);
            }
        }

        /// <summary>
        /// Constructor initializes the instance specific DataSyncManager and sets up all
        /// objects needed to perform CompareForSync() and Sync() methods from the
        /// source server and target connection. The source server may be a custom server
        /// model pulled from a Json file or created on the fly and passed to this DataSyncManager object.
        /// </summary>
        /// <param name="sourceServer">The source Server object.</param>
        /// <param name="targetDataConnectionInfo">target data connection info</param>
        /// <param name="dataProperties">data properties</param>
        protected DataSyncManager(Server sourceServer, DataConnectionInfo targetDataConnectionInfo, DataProperties dataProperties)
        {
            throw new NotImplementedException();
            Exceptions = new List<Exception>();
            DataTimer = new DataTimer();
            DataSyncActionsCollection = new DataSyncActionsCollection();
            DataProperties = dataProperties ?? new DataProperties();
            TotalDataEventWaitTime = new TimeSpan(DateTime.Now.Ticks);
            TotalDataEventWaitTime = TotalDataEventWaitTime.Subtract(TotalDataEventWaitTime);

            SourceDataConnectionInfo = null;
            SourceDataConnectionManager = null;
            SourceServer = sourceServer; // Could be null...
            TargetServer = null;

            try
            {
                TargetDataConnectionInfo = targetDataConnectionInfo;
            }
            catch (Exception ex)
            {
                TargetDataConnectionInfo = null;
                TargetDataConnectionManager = null;
                Exceptions.Add(ex);
            }
        }

		#endregion Constructors 

		#region Properties (13) 

        /// <summary>
        /// The UI specific properties that should be implemented and passed down
        /// to this object on instantiation.
        /// </summary>
        public DataProperties DataProperties { get; private set; }

        /// <summary>
        /// The synchronization actions that must be performed to synchronize the target
        /// database objects to the source database object specifications.
        /// </summary>
        public DataSyncActionsCollection DataSyncActionsCollection { get; private set; }

        /// <summary> 
        /// Generates the initial synchronization comparisons between source and
        /// target database objects, but does not have detailed information on
        /// matches. Detailed matching is done in the implementation specific
        /// SyncManager objects, such as the SqlServerSyncManager object which
        /// is also responsible for generating the DataSyncActions needed
        /// to synchronize the source and target databases.
        /// </summary>
        public DataSyncComparer DataSyncComparer { get; private set; }

        /// <summary>
        /// The DataTimer that tracks and provides status update messages on timing intervals
        /// for all data synchronization tasks. Subscribe to Timer.TimerStatus with a delegate
        /// of type DataTimerStatus to recieve status notifications.
        /// </summary>
        public DataTimer DataTimer { get; private set; }


        /// <summary>
        /// A list of exceptions that were thrown during CompareForSync() or Sync()
        /// methods. This list is cleared at the beginning of either call.
        /// </summary>
        public List<Exception> Exceptions { get; private set; }

        /// <summary>
        /// The connection information for the source database.
        /// </summary>
        public DataConnectionInfo SourceDataConnectionInfo { get; private set; }

        /// <summary>
        /// The data connection manager for the source database.
        /// </summary>
        public DataConnectionManager SourceDataConnectionManager { get; private set; }

        /// <summary>
        /// Class of generic lists and dictionaries used to store all metadata for a 
        /// the source database. All lists and dictionaries use a namespace format for
        /// unique entries. For example, the TableColumns List is a List of strings with
        /// a namespace as follows:
        /// [SchemaName].[TableName].[ColumnName]... [dbo].[Customers].[FirstName]
        /// </summary>
        public Server SourceServer { get; private set; }

        /// <summary>
        /// The connection information for the target database.
        /// </summary>
        public DataConnectionInfo TargetDataConnectionInfo { get; private set; }

        /// <summary>
        /// The data connection manager for the target database.
        /// </summary>
        public DataConnectionManager TargetDataConnectionManager { get; private set; }

        /// <summary>
        /// Class of generic lists and dictionaries used to store all metadata for a 
        /// the target database. All lists and dictionaries use a namespace format for
        /// unique entries. For example, the TableColumns List is a List of strings with
        /// a namespace as follows:
        /// [SchemaName].[TableName].[ColumnName]... [dbo].[Customers].[FirstName]
        /// </summary>
        public Server TargetServer { get; private set; }

        /// <summary>
        /// Timespan used for calculating total wait times the application was waiting
        /// on a response from the user during RaiseEvent() method calls.
        /// </summary>
        private TimeSpan TotalDataEventWaitTime { get; set; }

		#endregion Properties 

		#region Delegates and Events (1) 

		// Events (1) 

        /// <summary>
        /// Event to subscribe to for receiving DataEvent messages.
        /// </summary>
        public event DataEventMessage DataEvent;

		#endregion Delegates and Events 

		#region Methods (22) 

		// Public Methods (13) 

        /// <summary>
        /// Method clears the SyncActions and Exceptions generated during CompareForSync()
        /// or Sync() method calls.
        /// </summary>
        public void Clear()
        {
            try
            {
                DataSyncActionsCollection.Clear();
                Exceptions.Clear();
            }
            catch (Exception exception)
            {
                Exceptions.Add(exception);
            }
        }

        ///// <summary>
        ///// Generates an instance specific DataSyncActionCollection to be cycled through
        ///// in the Sync() method to complete the synchronization of source and target
        ///// databases. This method never performs any synchronization activites,
        ///// but only generates the synchronization actions needed to synchronize the
        ///// source and target databases.
        ///// </summary>
        ///// <returns>
        ///// True  - synchronized
        ///// False - ready for synchronization
        ///// False - exceptions encountered
        ///// </returns>
        //public bool CompareForSync()
        //{
        //    var compareForSyncResult = true;

        //    try
        //    {
        //        if (SourceServer == null)
        //            Exceptions.Add(new Exception(
        //            "The source Server object was unable to be pulled or was not provided."));

        //        if (TargetServer == null)
        //            Exceptions.Add(new Exception(
        //            "The source Server object was unable to be pulled or was not provided."));

        //        if (SourceServer == null || TargetServer == null)
        //            return false;

        //        var sourceDataContext = SourceServer.DataContext;
        //        var targetDataContext = TargetServer.DataContext;

        //        var ticks = DateTime.Now.Ticks;
        //        TotalDataEventWaitTime = new TimeSpan(ticks);
        //        TotalDataEventWaitTime = TotalDataEventWaitTime.Subtract(TotalDataEventWaitTime);
        //        var start = new TimeSpan(ticks);
        //        DataTimer.RaiseTimerStatusEvent("Starting comparison of source and target database metadata.");

        //        var comparerStart = new TimeSpan(DateTime.Now.Ticks);
        //        DataTimer.RaiseTimerStatusEvent("Creating comparison data object sets for catalog comparisons.");

        //        if (DataSyncComparer == null)
        //        {
        //            DataSyncComparer = new DataSyncComparer(this);
        //        }
        //        else
        //        {
        //            DataSyncComparer.Clear();
        //            DataSyncComparer.SourceServer = Server.Clone(SourceServer, sourceDataContext);
        //            DataSyncComparer.TargetServer = Server.Clone(TargetServer, targetDataContext);
        //        }

        //        DataActionFactory.ContextScriptFactory = TargetDataConnectionInfo.DataContext.ScriptFactory;

        //        // See if we need to rename any Catalogs on the Source Server before comparisons.
        //        // TODO: Pass in specific catalogs for Server.Build....
        //        if (DataProperties.CatalogsToCompare.Count > 0)
        //        {
        //            var catalogsToRemoveFromSource = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        //            var catalogsToRemoveFromTarget = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        //            // Find source catalogs that will not be compared.
        //            foreach (var sourceCatalogKey in
        //                DataSyncComparer.SourceServer.Catalogs.Keys.Where(
        //                    sourceCatalogKey => !DataProperties.CatalogsToCompare.ContainsKey(sourceCatalogKey)))
        //                catalogsToRemoveFromSource.Add(sourceCatalogKey);

        //            // Remove source catalogs that will not be compared.
        //            foreach (var sourceCatalogKey in catalogsToRemoveFromSource)
        //                Server.RemoveCatalog(DataSyncComparer.SourceServer, sourceCatalogKey);

        //            // Find target catalogs that will not be compared.
        //            foreach (var targetCatalogKey in
        //                DataSyncComparer.TargetServer.Catalogs.Keys.Where(
        //                    targetCatalogKey => !DataProperties.CatalogsToCompare.ContainsValue(targetCatalogKey)))
        //                catalogsToRemoveFromTarget.Add(targetCatalogKey);

        //            // Remove target catalogs that will not be compared.
        //            foreach (var targetCatalogKey in catalogsToRemoveFromTarget)
        //                Server.RemoveCatalog(DataSyncComparer.TargetServer, targetCatalogKey);

        //            // Rename this source catalog to match against the appropriate target catalog.
        //            // The renames will be appened with "<~>Renamed" so that their use can only be consumed once
        //            // per match as well as allowing name swaps to correctly be mapped to one another.
        //            foreach (var catalogKeyValuePair in
        //                DataProperties.CatalogsToCompare.Where(
        //                    catalogKeyValuePair => DataSyncComparer.SourceServer.Catalogs.ContainsKey(catalogKeyValuePair.Key)))
        //                        Server.RenameCatalog(DataSyncComparer.SourceServer, catalogKeyValuePair.Key, catalogKeyValuePair.Value + "<~>Renamed");

        //            // Removing the "<~>Renamed" portion to catalog instances to appropriate names without the
        //            // "<~>Renamed" appended value. Values that cannot be renamed already have a catalog with
        //            // that name in place and should result in an error as these cannot be
        //            foreach (var catalogKeyValuePair in
        //                DataProperties.CatalogsToCompare.Where(
        //                    catalogKeyValuePair => DataSyncComparer.SourceServer.Catalogs.ContainsKey(catalogKeyValuePair.Value + "<~>Renamed")))
        //                        Server.RenameCatalog(DataSyncComparer.SourceServer, catalogKeyValuePair.Value + "<~>Renamed", catalogKeyValuePair.Value);

        //            if (Exceptions.Count > 0)
        //                return false;
        //        }

        //        var comparerFinish = new TimeSpan(DateTime.Now.Ticks);
        //        var comparerInterval = comparerFinish.Subtract(comparerStart);
        //        DataTimer.RaiseTimerStatusEvent(comparerInterval, "Finished creating data objects sets for catalog comparisons.");

        //        var syncComparerStart = new TimeSpan(DateTime.Now.Ticks);
        //        DataTimer.RaiseTimerStatusEvent("Comparing data objects and generating sync actions from comparisons.");
        //        // TODO: Do Converts to replace needing these commented out lines.
        //        // SourceDataConnectionInfo.DataContext = TargetServer.DataContext;
        //        // SourceServer.DataContext = TargetServer.DataContext;
        //        // DataSyncComparer.SourceServer.DataContext = TargetServer.DataContext;
        //        compareForSyncResult = DataSyncComparer.CompareForSync();
               
        //        // Add the server specific actions
        //        CompareServerSpecific();
        //        if (!CompareDataForSync() || Exceptions.Count > 0)
        //            return false;

        //        DataSyncActionsCollection = DataActionFactory.SortSyncActions(sourceDataContext, targetDataContext, DataSyncActionsCollection);
        //        // See if we need to rename any Catalogs on the Source Server after comparisons.
        //        if (DataProperties.CatalogsToCompare.Count > 0)
        //        {
        //            // Rename this source catalog to match against the appropriate source catalog.
        //            // The renames will be appened with "<~>Renamed" so that their use can only be consumed once
        //            // per match as well as allowing name swaps to correctly be mapped to one another.
        //            foreach (var catalogKeyValuePair in
        //                DataProperties.CatalogsToCompare.Where(
        //                    catalogKeyValuePair => DataSyncComparer.SourceServer.Catalogs.ContainsKey(catalogKeyValuePair.Value)))
        //                    Server.RenameCatalog(DataSyncComparer.SourceServer, catalogKeyValuePair.Value, catalogKeyValuePair.Key + "<~>Renamed");

        //            // Removing the "<~>Renamed" portion to catalog instances to appropriate names without the
        //            // "<~>Renamed" appended value. Values that cannot be renamed already have a catalog with
        //            // that name in place and should result in an error as these cannot be
        //            foreach (var catalogKeyValuePair in
        //                DataProperties.CatalogsToCompare.Where(
        //                    catalogKeyValuePair => DataSyncComparer.SourceServer.Catalogs.ContainsKey(catalogKeyValuePair.Key + "<~>Renamed")))
        //                        Server.RenameCatalog(DataSyncComparer.SourceServer, catalogKeyValuePair.Key + "<~>Renamed", catalogKeyValuePair.Key);


        //            if (Exceptions.Count > 0)
        //                return false;
        //        }

        //        var syncComparerFinish = new TimeSpan(DateTime.Now.Ticks);
        //        var syncComparerInterval = syncComparerFinish.Subtract(syncComparerStart);
        //        DataTimer.RaiseTimerStatusEvent(syncComparerInterval, "Finished comparing data objects and generating sync actions from comparisons.");
        //        Exceptions.AddRange(DataSyncComparer.Exceptions);

        //        var finish = new TimeSpan(DateTime.Now.Ticks);
        //        var interval = finish.Subtract(start);
        //        interval = interval.Subtract(TotalDataEventWaitTime);
        //        DataTimer.RaiseTimerStatusEvent(interval, "Finished comparison of source and target database metadata.");
        //        if (TotalDataEventWaitTime.Ticks > 0)
        //        {
        //            DataTimer.RaiseTimerStatusEvent(TotalDataEventWaitTime, "Total user response time in dialog windows.");
        //        }
        //    }
        //    catch (Exception exception)
        //    {
        //        Exceptions.Add(exception);
        //    }
            
        //    return Exceptions.Count == 0 && DataSyncActionsCollection.Count == 0 && compareForSyncResult;
        //}

        //public bool FetchSourceDataModelFromJson(string json)
        //{
        //    var ticks = DateTime.Now.Ticks;
        //    TotalDataEventWaitTime = new TimeSpan(ticks);
        //    TotalDataEventWaitTime = TotalDataEventWaitTime.Subtract(TotalDataEventWaitTime);
        //    var start = new TimeSpan(ticks);
        //    DataTimer.RaiseTimerStatusEvent("Fetching source database JSon metadata.");

        //    Clear();

        //    try
        //    {
        //        if (string.IsNullOrEmpty(json) || string.IsNullOrWhiteSpace(json))
        //        {
        //            Exceptions.Add(new Exception(
        //                "Source metadeta could not be pulled from JSon."));
        //            return false;
        //        }

        //        var server = Server.FromJson(json);
        //        if (server == null)
        //        {
        //            Exceptions.Add(new Exception(
        //                "Source metadeta could not be pulled from JSon."));
        //            return false;
        //        }

        //        SourceServer = server;
        //        if (DataProperties.CatalogsToCompare.Count > 0)
        //            foreach (var catalogKey in
        //                DataProperties.CatalogsToCompare.Keys.Where(
        //                    catalogKey => !SourceServer.Catalogs.ContainsKey(catalogKey)
        //                ))
        //                Server.RemoveCatalog(SourceServer, catalogKey);
        //    }
        //    catch (Exception ex)
        //    {
        //        Exceptions.Add(ex);
        //        return false;
        //    }

        //    var finish = new TimeSpan(DateTime.Now.Ticks);
        //    var interval = finish.Subtract(start);
        //    interval = interval.Subtract(TotalDataEventWaitTime);
        //    DataTimer.RaiseTimerStatusEvent(interval, "Finished fetching source database JSon metadata.");
        //    if (TotalDataEventWaitTime.Ticks > 0)
        //        DataTimer.RaiseTimerStatusEvent(TotalDataEventWaitTime, "Total user response time in dialog windows.");

        //    return true;
        //}

        public bool FetchSourceDataModelFromServer(bool closeDataConnectionAfterUse = true)
        {
            if (SourceDataConnectionInfo == null)
            {
                Exceptions.Add(new Exception("Source connection info has not been provided."));
                return false;
            }

            var ticks = DateTime.Now.Ticks;
            TotalDataEventWaitTime = new TimeSpan(ticks);
            TotalDataEventWaitTime = TotalDataEventWaitTime.Subtract(TotalDataEventWaitTime);
            var start = new TimeSpan(ticks);
            DataTimer.RaiseTimerStatusEvent("Fetching source database metadata.");

            Clear();

            // Open source data connection if it does not already exist and should exist.
            if (SourceDataConnectionManager == null)
            {
                var sourceStart = new TimeSpan(DateTime.Now.Ticks);
                DataTimer.RaiseTimerStatusEvent(string.Format("Creating and opening connection for source server: [{0}]", SourceDataConnectionInfo.Name));
                SourceDataConnectionManager = new DataConnectionManager(SourceDataConnectionInfo);
                SourceDataConnectionManager.Open();
                var sourceFinish = new TimeSpan(DateTime.Now.Ticks);
                var sourceInterval = sourceFinish.Subtract(sourceStart);
                DataTimer.RaiseTimerStatusEvent(
                    sourceInterval,
                    string.Format("Finished creating and opening connection for source server: [{0}]",
                        SourceDataConnectionInfo.Name));
            }
            else if (!SourceDataConnectionManager.IsOpen())
            {
                if (SourceDataConnectionManager.IsBusy())
                {
                    Exceptions.Add(new Exception(
                        string.Format("Source connection for source connection: [{0}]... is busy! Please try again in a few moments.",
                        SourceDataConnectionInfo.Name)
                       ));
                    return false;
                }

                var sourceStart = new TimeSpan(DateTime.Now.Ticks);
                DataTimer.RaiseTimerStatusEvent(
                    string.Format("Opening connection for source connection: [{0}]",
                                  SourceDataConnectionInfo.Name));
                SourceDataConnectionManager.Open();
                var sourceFinish = new TimeSpan(DateTime.Now.Ticks);
                var sourceInterval = sourceFinish.Subtract(sourceStart);
                DataTimer.RaiseTimerStatusEvent(
                    sourceInterval,
                    string.Format("Finished opening connection for source server: [{0}]",
                        SourceDataConnectionInfo.Name));
            }

            SourceServer = null;
            DataSyncComparer = null;

            if (!SourceDataConnectionManager.IsOpen())
            {
                Exceptions.Add(new Exception(
                    string.Format("Source connection for server: [{0}]... could not be opened.",
                    SourceDataConnectionInfo.Name)));
                return false;
            }

            var sourceServerStart = new TimeSpan(DateTime.Now.Ticks);
            DataTimer.RaiseTimerStatusEvent(
                string.Format("Creating and pulling data objects for source connection [{0}]",
                                SourceDataConnectionInfo.Name));
            SourceServer = new Server()
            {
                ObjectName = SourceDataConnectionInfo.Name,
                DataContext = SourceDataConnectionInfo.DataContext
            };

            Server.BuildCatalogs(SourceServer, SourceDataConnectionManager, closeDataConnectionAfterUse);

            var sourceServerFinish = new TimeSpan(DateTime.Now.Ticks);
            var sourceServerInterval = sourceServerFinish.Subtract(sourceServerStart);
            DataTimer.RaiseTimerStatusEvent(
                sourceServerInterval,
                string.Format(
                    "Finished creating data objects for source connection [{0}]",
                    SourceDataConnectionInfo.Name));

            var finish = new TimeSpan(DateTime.Now.Ticks);
            var interval = finish.Subtract(start);
            interval = interval.Subtract(TotalDataEventWaitTime);
            DataTimer.RaiseTimerStatusEvent(interval, "Finished fetching source database metadata.");
            if (TotalDataEventWaitTime.Ticks > 0)
                DataTimer.RaiseTimerStatusEvent(TotalDataEventWaitTime, "Total user response time in dialog windows.");

            return true;
        }

        //public bool FetchTargetDataModelFromJson(string json)
        //{

        //    var ticks = DateTime.Now.Ticks;
        //    TotalDataEventWaitTime = new TimeSpan(ticks);
        //    TotalDataEventWaitTime = TotalDataEventWaitTime.Subtract(TotalDataEventWaitTime);
        //    var start = new TimeSpan(ticks);
        //    DataTimer.RaiseTimerStatusEvent("Fetching target database JSon metadata.");

        //    Clear();

        //    try
        //    {
        //        if (string.IsNullOrEmpty(json) || string.IsNullOrWhiteSpace(json))
        //        {
        //            Exceptions.Add(new Exception(
        //                "Target metadeta could not be pulled from JSon."));
        //            return false;
        //        }

        //        var server = Server.FromJson(json);
        //        if (server == null)
        //        {
        //            Exceptions.Add(new Exception(
        //                "Target metadeta could not be pulled from JSon."));
        //            return false;
        //        }

        //        TargetServer = server;

        //        if (DataProperties.CatalogsToCompare.Count > 0)
        //            foreach (var catalogValue in
        //                DataProperties.CatalogsToCompare.Values.Where(
        //                    catalogValue => !TargetServer.Catalogs.ContainsKey(catalogValue)
        //                ))
        //                Server.RemoveCatalog(TargetServer, catalogValue);
        //    }
        //    catch (Exception ex)
        //    {
        //        Exceptions.Add(ex);
        //        return false;
        //    }

        //    var finish = new TimeSpan(DateTime.Now.Ticks);
        //    var interval = finish.Subtract(start);
        //    interval = interval.Subtract(TotalDataEventWaitTime);
        //    DataTimer.RaiseTimerStatusEvent(interval, "Finished fetching target database JSon metadata.");
        //    if (TotalDataEventWaitTime.Ticks > 0)
        //        DataTimer.RaiseTimerStatusEvent(TotalDataEventWaitTime, "Total user response time in dialog windows.");

        //    return true;
        //}

        public bool FetchTargetDataModelFromServer(bool closeDataConnectionAfterUse = true)
        {
            if (TargetDataConnectionInfo == null)
            {
                Exceptions.Add(new Exception("Target connection info has not been provided."));
                return false;
            }

            var ticks = DateTime.Now.Ticks;
            TotalDataEventWaitTime = new TimeSpan(ticks);
            TotalDataEventWaitTime = TotalDataEventWaitTime.Subtract(TotalDataEventWaitTime);
            var start = new TimeSpan(ticks);
            DataTimer.RaiseTimerStatusEvent("Fetching target database metadata.");

            Clear();

            // Open target data connection if it does not already exist and should exist.
            if (TargetDataConnectionManager == null)
            {
                var targetStart = new TimeSpan(DateTime.Now.Ticks);
                DataTimer.RaiseTimerStatusEvent(string.Format("Creating and opening connection for target server: [{0}]", TargetDataConnectionInfo.Name));
                TargetDataConnectionManager = new DataConnectionManager(TargetDataConnectionInfo);
                TargetDataConnectionManager.Open();
                var targetFinish = new TimeSpan(DateTime.Now.Ticks);
                var targetInterval = targetFinish.Subtract(targetStart);
                DataTimer.RaiseTimerStatusEvent(
                    targetInterval,
                    string.Format("Finished creating and opening connection for target server: [{0}]",
                        TargetDataConnectionInfo.Name));
            }
            else if (!TargetDataConnectionManager.IsOpen())
            {
                if (TargetDataConnectionManager.IsBusy())
                {
                    Exceptions.Add(new Exception(
                        string.Format("Target connection for server: [{0}]... is busy! Please try again in a few moments.",
                        TargetDataConnectionInfo.Name)
                       ));
                    return false;
                }

                var targetStart = new TimeSpan(DateTime.Now.Ticks);
                DataTimer.RaiseTimerStatusEvent(
                    string.Format("Opening connection for target connection: [{0}]",
                                  TargetDataConnectionInfo.Name));
                TargetDataConnectionManager.Open();
                var targetFinish = new TimeSpan(DateTime.Now.Ticks);
                var targetInterval = targetFinish.Subtract(targetStart);
                DataTimer.RaiseTimerStatusEvent(
                    targetInterval,
                    string.Format("Finished opening connection for target server: [{0}]",
                        TargetDataConnectionInfo.Name));
            }

            TargetServer = null;
            DataSyncComparer = null;

            if (!TargetDataConnectionManager.IsOpen())
            {
                Exceptions.Add(new Exception(
                    string.Format("Connection for target server: [{0}]... could not be opened.",
                    TargetDataConnectionInfo.Name)));
                return false;
            }

            var targetServerStart = new TimeSpan(DateTime.Now.Ticks);
            DataTimer.RaiseTimerStatusEvent(
                string.Format("Creating and pulling data objects for target connection [{0}]",
                                TargetDataConnectionInfo.Name));
            TargetServer = new Server
            {
                ObjectName = TargetDataConnectionInfo.Name,
                DataContext = TargetDataConnectionInfo.DataContext
            };

            Server.BuildCatalogs(TargetServer, TargetDataConnectionManager, closeDataConnectionAfterUse);

            var targetServerFinish = new TimeSpan(DateTime.Now.Ticks);
            var targetServerInterval = targetServerFinish.Subtract(targetServerStart);
            DataTimer.RaiseTimerStatusEvent(
                targetServerInterval,
                string.Format(
                    "Finished creating data objects for target connection [{0}]",
                    TargetDataConnectionInfo.Name));

            var finish = new TimeSpan(DateTime.Now.Ticks);
            var interval = finish.Subtract(start);
            interval = interval.Subtract(TotalDataEventWaitTime);
            DataTimer.RaiseTimerStatusEvent(interval, "Finished fetching target database metadata.");
            if (TotalDataEventWaitTime.Ticks > 0)
                DataTimer.RaiseTimerStatusEvent(TotalDataEventWaitTime, "Total user response time in dialog windows.");

            return true;
        }

        public UserTable FindUserTable(DataContext sourceDataContext, DataContext targetDataContext, string nameSpace, Server server)
        {
            if (sourceDataContext == null || targetDataContext == null) 
                throw new Exception("The datacontext parameters in DataSyncManager.FindUserTable can not be null.");

            
            var userTable = server.Catalogs.SelectMany(p => p.Schemas).Select(p => p.UserTables[nameSpace]).FirstOrDefault(p => p != null);
            if (userTable == null)
                throw new Exception(string.Format("user-table {0} does not exist.", nameSpace));
            
            return userTable;
        }

        /// <summary>
        /// Method to raise a DataEvent to the parent thread if it is listening for a
        /// DataEvent. An instance specific SyncManager, such as a SqlServerSyncManager,
        /// will call this method when DataProperties like TightSynchronization will drop
        /// an entire schema and all of the database objects in that schema if it does not
        /// exist in the source database. This event should display a dialog to the user
        /// to get a true/false response back to cancel that from happening.
        /// </summary>
        /// <param name="type">database warning, etc.</param>
        /// <param name="title">titlebar of dialog window</param>
        /// <param name="subTitle">title to display inside dialog window</param>
        /// <param name="message">message to display inside dialog window</param>
        /// <returns>
        /// True - user pressed OK in dialog window.
        /// False - user pressed Cancel in dialog window.
        /// </returns>
        public bool RaiseEvent(string type, string title, string subTitle, string message)
        {
            try
            {
                if (DataEvent != null)
                {
                    var start = new TimeSpan(DateTime.Now.Ticks);
                    var result = DataEvent(type, title, subTitle, message);
                    var finish = new TimeSpan(DateTime.Now.Ticks);
                    TotalDataEventWaitTime = TotalDataEventWaitTime.Add(finish.Subtract(start));
                    return result;
                }
            }
            catch (Exception exception)
            {
                Exceptions.Add(exception);
            }

            return false;
        }

        /// <summary>
        /// Refreshes Source and Target.
        /// </summary>
        public void Refresh()
        {
            try
            {
                //Source.Refresh();
                //Target.Refresh();
            }
            catch (Exception exception)
            {
                Exceptions.Add(exception);
            }
        }

        public StringCollection Script()
        {
            return ScriptInternal(null);
        }

        /// <summary>
        /// Generates a T-SQL Script from the SyncActions to be processed
        /// against a live database.
        /// </summary>
        /// <returns>StringCollection of runnable SQL commands</returns>
        public StringCollection Script(string delimiter)
        {
            try
            {
                return ScriptInternal(delimiter);
            }
            catch (Exception exception)
            {
                Exceptions.Add(exception);
            }

            return new StringCollection();
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
        public bool Sync()
        {
            throw new NotImplementedException();
            try
            {
                var ticks = DateTime.Now.Ticks;
                TotalDataEventWaitTime = new TimeSpan(ticks);
                TotalDataEventWaitTime = TotalDataEventWaitTime.Subtract(TotalDataEventWaitTime);
                var start = new TimeSpan(ticks);
                DataTimer.RaiseTimerStatusEvent("Starting synchronization of source and target databases.");

                Exceptions.Clear();

                // For server specific stuff
                SyncInternal();

                var finish = new TimeSpan(DateTime.Now.Ticks);
                var interval = finish.Subtract(start);
                interval = interval.Subtract(TotalDataEventWaitTime);
                DataTimer.RaiseTimerStatusEvent(interval, "Finished synchronization.");
            }
            catch (Exception exception)
            {
                Exceptions.Add(exception);
            }

            return Exceptions.Count == 0;
        }

        //public bool SyncData()
        //{
        //    if (!CompareForSync())
        //        throw new Exception("The data cannot be synced because the schemas do not match.  Please run a schema synchronization first.");
            
        //    return CompareDataForSync();
        //}

		// Protected Methods (8) 

        protected abstract void ChangeCatalog(DataContext sourceDataContext, DataContext targetDataContext, StringCollection stringCollection, Catalog catalog);

        protected abstract void RestoreIsoState(DataContext sourceDataContext, DataContext targetDataContext, StringCollection stringCollection);

        protected abstract void RestoreIsoState(DataContext sourceDataContext, DataContext targetDataContext, StringCollection stringCollection, string delimiter);

        protected abstract void SetIsoState(DataContext sourceDataContext, DataContext targetDataContext, DataSyncAction syncAction, StringCollection strs);

        protected abstract void SetIsoState(DataContext sourceDataContext, DataContext targetDataContext, DataSyncAction syncAction, StringCollection stringCollection, string delimiter);

        protected abstract bool SyncInternal();

        protected virtual void CompareServerSpecific()
        {
            Clear();

            var sourceDataContext = SourceDataConnectionInfo.DataContext;
            var targetDataContext = TargetDataConnectionInfo.DataContext;

            var sourceServerClone = DataSyncComparer.SourceServer.DeepClone();
            var targetServerClone = DataSyncComparer.TargetServer.DeepClone();
            var droppedServerClone = DataSyncComparer.DroppedServer.DeepClone();
            var createdServerClone = DataSyncComparer.CreatedServer.DeepClone();
            var alteredServerClone = DataSyncComparer.AlteredServer.DeepClone();
            var matchedServerClone = DataSyncComparer.MatchedServer.DeepClone();

            //Server.GenerateDropScripts(sourceDataContext, targetDataContext, droppedServerClone,
            //    sourceServerClone, targetServerClone, DataSyncActionsCollection, DataProperties);
            //Server.GenerateCreateScripts(sourceDataContext, targetDataContext, createdServerClone,
            //    sourceServerClone, targetServerClone, DataSyncActionsCollection, DataProperties);
            //Server.GenerateAlterScripts(sourceDataContext, targetDataContext, alteredServerClone,
            //    sourceServerClone, targetServerClone, droppedServerClone, createdServerClone,
            //    matchedServerClone, DataSyncActionsCollection, DataProperties);
        }

        protected virtual StringCollection ScriptInternal(string delimiter)
        {
            var sourceDataContext = SourceServer.DataContext;
            var targetDataContext = TargetServer.DataContext;

            var stringCollection = new StringCollection();
            foreach (var dataSyncAction in
                DataSyncActionsCollection.Where(
                    dataSyncAction => dataSyncAction.Process))
            {
                SetIsoState(sourceDataContext, targetDataContext, dataSyncAction, stringCollection);
                stringCollection.Add(dataSyncAction.ScriptAction());
                if (!string.IsNullOrEmpty(delimiter))
                {
                    stringCollection.Add(delimiter);
                }
            }

            RestoreIsoState(sourceDataContext, targetDataContext, stringCollection);
            return stringCollection;
        }
		// Private Methods (1) 

        /// <summary>
        /// Compares all the tables in 'DataProperties' for data syncronization
        /// </summary>
        /// <returns>bool - True if success</returns>
        //private bool CompareDataForSync(bool closeConnectionOnFill = true)
        //{
        //    try
        //    {
        //        var sourceDataContext = SourceServer.DataContext;
        //        var targetDataContext = TargetServer.DataContext;

        //        // Loop through each table to compare
        //        foreach (var userTable in DataProperties.TablesForSync)
        //        {

        //            // Get our tables to compare for data
        //            var sourceTable = FindUserTable(sourceDataContext, targetDataContext, userTable, SourceServer);

        //            // If the tables arn't found throw an exception
        //            if (sourceTable == null)
        //                throw new Exception(string.Format("Source User-Table '{0}' was not found.", userTable));

        //            // Script out the insert statements for each row of data in the table.
        //            if (!SourceDataConnectionManager.IsOpen())
        //                SourceDataConnectionManager.Open();

        //            if (StringComparer.OrdinalIgnoreCase.Compare(
        //                sourceTable.Catalog.ObjectName, SourceDataConnectionManager.CatalogName) != 0)
        //                SourceDataConnectionManager.ChangeCatalog(sourceTable.Catalog.ObjectName);

        //            var sourceDataSet = sourceTable.GetDataset(SourceDataConnectionManager);

        //            // Truncate the table in the target if rows exist and create sync actions for each data row.
        //            if (sourceDataSet == null || sourceDataSet.Tables.Count == 0)
        //                throw new Exception(string.Format("Error pulling data for User-Table '{0}'.", userTable));

        //            if (sourceDataSet.Tables[0].Rows.Count <= 0)
        //                continue;

        //            DataSyncActionsCollection.Add(DataActionFactory.TruncateTable(sourceDataContext, targetDataContext, sourceTable));

        //            foreach (DataRow row in sourceDataSet.Tables[0].Rows)
        //                DataSyncActionsCollection.Add(DataActionFactory.InsertRow(sourceDataContext, targetDataContext, sourceTable, row.ItemArray));
        //        }

        //        if (closeConnectionOnFill)
        //            SourceDataConnectionManager.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        Exceptions.Add(ex);
        //        return false;
        //    }

        //    return Exceptions.Count == 0;
        //}

		#endregion Methods 
    }
}
