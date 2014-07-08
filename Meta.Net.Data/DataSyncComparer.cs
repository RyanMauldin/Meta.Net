using Meta.Net.Interfaces;
using System;
using System.Collections.Generic;
using Meta.Net.Data.Enum;
using Meta.Net.Data.Objects;

namespace Meta.Net.Data
{
    /// <summary>
    /// Compares all source and target database objects by namespace using HashSet
    /// operations UnionWith(), IntersectWith(), and ExceptWith(). This is an extremely
    /// fast way to do basic comparisons and avoids the whole need for large sets
    /// of objects that memic the database model and dependency structure. We do not
    /// know about altered definitions at this point for tables, table columns, 
    /// identity columns, default constraints, check constraints, computed columns,
    /// primary keys, unique constraints, indexes, foreign keys, or modules other than
    /// basic existance. Matched database objects here do not mean they truley match.
    /// </summary>
    public class DataSyncComparer : ISyncComparer
    {
		#region Properties (8) 

        public Server AlteredServer { get; set; }

        public Server CreatedServer { get; set; }

        public DataSyncManager DataSyncManager { get; set; }

        public Server DroppedServer { get; set; }

        public List<Exception> Exceptions { get; set; }

        public Server MatchedServer { get; set; }

        public Server SourceServer { get; set; }

        public Server TargetServer { get; set; }

		#endregion Properties 

		#region Constructors (1) 

        /// <summary>
        /// Initializes all members.
        /// </summary>
        /// <param name="dataSyncManager">the DataSyncManager object.</param>
        public DataSyncComparer(DataSyncManager dataSyncManager)
        {
            Exceptions = new List<Exception>();
            DataSyncManager = dataSyncManager;
            SourceServer = Server.Clone(DataSyncManager.SourceServer, dataSyncManager.SourceDataConnectionInfo.DataContext);
            TargetServer = Server.Clone(DataSyncManager.TargetServer, dataSyncManager.TargetDataConnectionInfo.DataContext);
        }

		#endregion Constructors 

		#region Methods (2) 

		#region Public Methods (2) 

        /// <summary>
        /// Clears Exceptions and all HashSets.
        /// </summary>
        public void Clear()
        {
            try
            {
                Exceptions.Clear();

                if (AlteredServer != null)
                    Server.Clear(AlteredServer);

                if (CreatedServer != null)
                    Server.Clear(CreatedServer);

                if (DroppedServer != null)
                    Server.Clear(DroppedServer);

                if (MatchedServer != null)
                    Server.Clear(MatchedServer);
            }
            catch (Exception ex)
            {
                Exceptions.Add(ex);
            }
        }

        /// <summary>
        /// Compares all source and target database objects by namespace using HashSet
        /// operations UnionWith(), IntersectWith(), and ExceptWith().
        /// </summary>
        /// <returns>
        /// True - no exceptions
        /// False - exceptions
        /// </returns>
        public bool CompareForSync()
        {
            try
            {
                Clear();

                if (SourceServer == null)
                    Exceptions.Add(new Exception("The Source Server was null for DataSyncComparer.CompareForSync()."));

                if (TargetServer == null)
                    Exceptions.Add(new Exception("The Target Server was null for DataSyncComparer.CompareForSync()."));

                if (SourceServer == null || TargetServer == null)
                    return false;

                var sourceDataContext = DataSyncManager.SourceDataConnectionInfo.DataContext;
                var targetDataContext = DataSyncManager.TargetDataConnectionInfo.DataContext;

                // DataComparisonType.SchemaLevelNamespaces
                // After processing the ExceptWith it removes all items in the Target server
                // from the Created server (clone of source) server at the schema level namespace only.
                // Meaning there is only completely new tables, stored procs, etc. Table columns
                // are left alone and will be added via AlteredServer.
                CreatedServer = Server.Clone(SourceServer, sourceDataContext);
                Server.ExceptWith(sourceDataContext, targetDataContext, CreatedServer, TargetServer);

                // DataComparisonType.SchemaLevelNamespaces
                // After processing the ExceptWith it removes all items in the Dropped server (clone of target)
                // from the Source server at the schema level namespace only.
                // Meaning the Dropped server only contains tables to drop, schemas to drop, etc.
                DroppedServer = Server.Clone(TargetServer, targetDataContext);
                Server.ExceptWith(sourceDataContext, targetDataContext, DroppedServer, SourceServer);

                // DataComparisonType.Definitions
                // After processing the IntersectWith it modifies the Matched Server (clone of source)
                // to contain all tables, stored procs, etc to contain only items that have an exact
                // definition match to items on the Target server.
                MatchedServer = Server.Clone(SourceServer, targetDataContext);
                Server.IntersectWith(sourceDataContext, targetDataContext,
                    MatchedServer, TargetServer, DataComparisonType.Definitions);

                // Altered Server will get populated from within CompareMatchedServer as
                // it iterates through the object collections determining the differences
                // between Created, Dropped, and Matched servers.
                AlteredServer = Server.ShallowClone(SourceServer, sourceDataContext);
                var result = Server.CompareMatchedServer(sourceDataContext, targetDataContext,
                    MatchedServer, SourceServer, TargetServer, AlteredServer);

                return (result
                        && CreatedServer.Catalogs.Count == 0
                        && DroppedServer.Catalogs.Count == 0
                        && AlteredServer.Catalogs.Count == 0);
            }
            catch (Exception exception)
            {
                Exceptions.Add(exception);
            }

            return Exceptions.Count == 0;
        }

		#endregion Public Methods 

		#endregion Methods 
    }
}
