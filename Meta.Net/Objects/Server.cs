using System;
using System.Collections.Generic;
using System.Linq;
using Meta.Net.Abstract;
using Meta.Net.Interfaces;
using Meta.Net.Metadata;

namespace Meta.Net.Objects
{
    //[Serializable]
    public class Server : BaseMetaObject
    {
        public static readonly string DefaultDescription = "Server";

        public override string Description
        {
            get { return DefaultDescription; }
        }

        public override string Namespace
        {
            get { return ObjectName; }
        }

        public DataContext DataContext { get; set; }

        /// <summary>
        /// The collection of catalogs assigned to this server.
        /// </summary>
        public DataObjectLookup<Server, Catalog> Catalogs { get; private set; }

        public Server()
        {
            DataContext = null;
            Catalogs = new DataObjectLookup<Server, Catalog>(this);
        }

        /// <summary>
        /// This data object can never have a parent data object assigned as it is the root of all data objects.
        /// </summary>
        public override IMetaObject ParentMetaObject
        {
            get { return null; }
            set { throw new Exception(string.Format("A {0} cannot have a parent data object assigned as it is the root object", Description)); }
        }

        /// <summary>
        /// This method will always return false for this data object.
        /// </summary>
        /// <param name="metaObject">The data object wished to be assigned as the parent.</param>
        /// <returns>This method will always return false for this data object.</returns>
        public override bool CanBeAssignedParentMetaObject(IMetaObject metaObject)
        {
            return false;
        }

        public override IMetaObject DeepClone()
        {
            var server = new Server
            {
                ObjectName = ObjectName,
                DataContext = DataContext.DeepClone()
            };
            server.Catalogs = Catalogs.DeepClone(server);
            return server;
        }

        public override IMetaObject ShallowClone()
        {
            return new Server
            {
                ObjectName = ObjectName,
                DataContext = DataContext.ShallowClone()
            };
        }

        /// <summary>
        /// Deep Clear... disassociates all objects in each catalog
        /// first, before clearing out the collections in hopes of better
        /// memory dissipation from garbage collection.
        /// </summary>
        /// <param name="server">The server to deep clear.</param>
        public static void Clear(Server server)
        {
            foreach (var catalog in server.Catalogs)
                Catalog.Clear(catalog);

            server.Catalogs.Clear();
        }

		public static void AddCatalog(Server server, Catalog catalog)
        {
            if (catalog.Server != null && !catalog.Server.Equals(server))
                RemoveCatalog(catalog.Server, catalog);

            server.Catalogs.Add(catalog);
        }

        public static void RemoveCatalog(Server server, string objectNamespace)
        {
            server.Catalogs.Remove(objectNamespace);
        }

        public static void RemoveCatalog(Server server, Catalog catalog)
        {
            server.Catalogs.Remove(catalog.Namespace);
        }

        public static void RenameCatalog(Server server, string objectNamespace, string newObjectName)
        {
            var catalog = server.Catalogs[objectNamespace];
            if (catalog == null)
                throw new Exception(string.Format("{0} could not be found in {1} {2} to rename to {3}",
                    objectNamespace, server.Description, server.Namespace, newObjectName));

            server.Catalogs.Rename(catalog, newObjectName);
        }

        public static bool GetCatalogs(Server server, DataConnectionManager dataConnectionManager, bool closeDataConnectionAfterUse = true)
        {
            Clear(server);

            if (dataConnectionManager == null)
                return false;

            dataConnectionManager.Open();
            if (!dataConnectionManager.IsOpen() || dataConnectionManager.IsBusy())
                return false;

            CatalogsAdapter.Get(server, dataConnectionManager.DataConnection, dataConnectionManager.DataConnectionInfo.CreateMetadataScriptFactory());

            if (closeDataConnectionAfterUse)
                dataConnectionManager.Close();

            return true;
        }

        public static bool GetSpecificCatalogs(Server server, DataConnectionManager dataConnectionManager, IList<string> catalogs, bool closeDataConnectionAfterUse = true)
        {
            Clear(server);

            if (dataConnectionManager == null)
                return false;

            dataConnectionManager.Open();
            if (!dataConnectionManager.IsOpen() || dataConnectionManager.IsBusy())
                return false;

            CatalogsAdapter.GetSpecific(server, dataConnectionManager.DataConnection, dataConnectionManager.DataConnectionInfo.CreateMetadataScriptFactory(), catalogs);

            if (closeDataConnectionAfterUse)
                dataConnectionManager.Close();

            return true;
        }

        public static bool BuildCatalogs(Server server, DataConnectionManager dataConnectionManager, bool closeDataConnectionAfterUse = true)
        {
            Clear(server);

            if (dataConnectionManager == null)
                return false;

            dataConnectionManager.Open();
            if (!dataConnectionManager.IsOpen() || dataConnectionManager.IsBusy())
                return false;

            CatalogsAdapter.Build(server, dataConnectionManager.DataConnection, dataConnectionManager.DataConnectionInfo.CreateMetadataScriptFactory());

            if (closeDataConnectionAfterUse)
                dataConnectionManager.Close();

            return true;
        }

        public static bool BuildSpecificCatalogs(Server server, DataConnectionManager dataConnectionManager, IList<string> catalogs, bool closeDataConnectionAfterUse = true)
        {
            Clear(server);

            if (dataConnectionManager == null)
                return false;

            dataConnectionManager.Open();
            if (!dataConnectionManager.IsOpen() || dataConnectionManager.IsBusy())
                return false;

            CatalogsAdapter.BuildSpecific(server, dataConnectionManager.DataConnection, dataConnectionManager.DataConnectionInfo.CreateMetadataScriptFactory(), catalogs);

            if (closeDataConnectionAfterUse)
                dataConnectionManager.Close();

            return true;
        }

        public static long ObjectCount(Server server, bool deepCount = false)
        {
            if (!deepCount)
                return server.Catalogs.Count;

            return server.Catalogs.Count
                + server.Catalogs.Sum(p => Catalog.ObjectCount(p, true));
        }

        //public static bool CompareDefinitions(Server sourceServer, Server targetServer)
        //{
        //    return CompareObjectNames(sourceServer, targetServer);
        //}

        //public static bool CompareMatchedServer(DataContext sourceDataContext, DataContext targetDataContext,
        //    Server matchedServer, Server sourceServer, Server targetServer, Server alteredServer)
        //{
        //    var globalCompareState = true;

        //    foreach(var matchedCatalog in matchedServer.Catalogs)
        //    {
        //        var sourceCatalog = sourceServer.Catalogs[matchedCatalog.Namespace];
        //        if (sourceCatalog == null)
        //            throw new Exception(string.Format("Source catalog did not exist for the matching catalog {0} during Server.CompareMatchedServer() method.", matchedCatalog.Namespace));

        //        var targetCatalog = targetServer.Catalogs[matchedCatalog.Namespace];
        //        if (targetCatalog == null)
        //            throw new Exception(string.Format("Target catalog did not exist for the matching catalog {0} during Server.CompareMatchedServer() method.", matchedCatalog.Namespace));
                
        //        var alteredCatalog = Catalog.ShallowClone(sourceCatalog);

        //        AddCatalog(alteredServer, alteredCatalog);

        //        if (!Catalog.CompareMatchedCatalog(sourceDataContext, targetDataContext, matchedCatalog, sourceCatalog, targetCatalog, alteredCatalog))
        //            globalCompareState = false;

        //        if (Catalog.ObjectCount(alteredCatalog) == 0)
        //            RemoveCatalog(alteredServer, alteredCatalog);
        //    }

        //    return globalCompareState;
        //}

        //public static bool CompareObjectNames(Server sourceServer, Server targetServer, StringComparer stringComparer = null)
        //{
        //    if (stringComparer == null)
        //        stringComparer = StringComparer.OrdinalIgnoreCase;

        //    return stringComparer.Compare(sourceServer.Namespace, targetServer.Namespace) == 0;
        //}

        ///// <summary>
        ///// Removes all objects in the target Server from the source Server.
        ///// </summary>
        ///// <param name="sourceDataContext">The source DataContext.</param>
        ///// <param name="targetDataContext">The target DataContext.</param>
        ///// <param name="sourceServer">The source Server.</param>
        ///// <param name="targetServer">The target Server.</param>
        ///// <param name="dataComparisonType">
        ///// The completeness of comparisons between matching objects.
        ///// </param>
        //public static void ExceptWith(DataContext sourceDataContext, DataContext targetDataContext,
        //    Server sourceServer, Server targetServer, DataComparisonType dataComparisonType = DataComparisonType.SchemaLevelNamespaces)
        //{
        //    var matchingCatalogs = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        //    matchingCatalogs.UnionWith(sourceServer.Catalogs.Keys);
        //    matchingCatalogs.IntersectWith(targetServer.Catalogs.Keys);

        //    foreach (var matchedCatalog in matchingCatalogs)
        //    {
        //        var sourceCatalog = sourceServer.Catalogs[matchedCatalog];
        //        var targetCatalog = targetServer.Catalogs[matchedCatalog];
        //        if (sourceCatalog == null || targetCatalog == null)
        //            throw new Exception(string.Format("Source and/or target catalogs did not exist for the matching catalog [{0}] during Server.ExceptWith() method.", matchedCatalog));

        //        switch (dataComparisonType)
        //        {
        //            case DataComparisonType.Definitions:
        //                if (Catalog.CompareDefinitions(sourceCatalog, targetCatalog))
        //                {
        //                    Catalog.ExceptWith(sourceDataContext, targetDataContext, sourceCatalog, targetCatalog, dataComparisonType);
        //                    if (Catalog.ObjectCount(sourceCatalog) == 0)
        //                        RemoveCatalog(sourceServer, matchedCatalog);
        //                }
        //                break;
        //            case DataComparisonType.Namespaces:
        //            case DataComparisonType.SchemaLevelNamespaces:
        //                if (Catalog.CompareObjectNames(sourceCatalog, targetCatalog))
        //                {
        //                    Catalog.ExceptWith(sourceDataContext, targetDataContext, sourceCatalog, targetCatalog, dataComparisonType);
        //                    if (Catalog.ObjectCount(sourceCatalog) == 0)
        //                        RemoveCatalog(sourceServer, matchedCatalog);
                            
        //                }
        //                break;
        //        }
        //    }
        //}
        
        //public static void GenerateAlterScripts(DataContext sourceDataContext, DataContext targetDataContext,
        //    Server alteredServer, Server sourceServer, Server targetServer, Server droppedServer, Server createdServer,
        //    Server matchedServer, DataSyncActionsCollection dataSyncActions, DataProperties dataProperties)
        //{
        //    foreach (var alteredCatalog in alteredServer.Catalogs)
        //    {
        //        var sourceCatalog = sourceServer.Catalogs[alteredCatalog.Namespace];
        //        var targetCatalog = targetServer.Catalogs[alteredCatalog.Namespace];

        //        if (sourceCatalog == null || targetCatalog == null)
        //            throw new Exception(string.Format("Source and/or target catalogs did not exist for the altered catalog {0} during Server.GenerateAlterScripts() method.", alteredCatalog.Namespace));

        //        var droppedCatalog = droppedServer.Catalogs[alteredCatalog.Namespace];
        //        var createdCatalog = createdServer.Catalogs[alteredCatalog.Namespace];
        //        var matchedCatalog = matchedServer.Catalogs[alteredCatalog.Namespace];

        //        Catalog.GenerateAlterScripts(sourceDataContext, targetDataContext, alteredCatalog, sourceCatalog, targetCatalog, droppedCatalog, createdCatalog, matchedCatalog, dataSyncActions, dataProperties);
        //    }
        //}

        //public static void GenerateCreateScripts(DataContext sourceDataContext, DataContext targetDataContext, Server createdServer,
        //    Server sourceServer, Server targetServer, DataSyncActionsCollection dataSyncActions, DataProperties dataProperties)
        //{
        //    foreach (var createdCatalog in createdServer.Catalogs)
        //    {
        //        var sourceCatalog = sourceServer.Catalogs[createdCatalog.Namespace];
        //        var targetCatalog = targetServer.Catalogs[createdCatalog.Namespace];

        //        Catalog.GenerateCreateScripts(sourceDataContext, targetDataContext, createdCatalog, sourceCatalog, targetCatalog, dataSyncActions, dataProperties);
        //    }
                
        //}

        //public static void GenerateDropScripts(DataContext sourceDataContext, DataContext targetDataContext, Server droppedServer,
        //    Server sourceServer, Server targetServer, DataSyncActionsCollection dataSyncActions, DataProperties dataProperties)
        //{
        //    if (ObjectCount(droppedServer) == 0 || !dataProperties.TightSync)

        //    foreach (var droppedCatalog in droppedServer.Catalogs)
        //    {
        //        var sourceCatalog = sourceServer.Catalogs[droppedCatalog.Namespace];
        //        var targetCatalog = targetServer.Catalogs[droppedCatalog.Namespace];

        //        Catalog.GenerateDropScripts(sourceDataContext, targetDataContext, droppedCatalog, sourceCatalog, targetCatalog, dataSyncActions, dataProperties);
        //    }
        //}

        ///// <summary>
        ///// Modifies the source Server to contain only objects that are
        ///// present in the source Server and in the target Server.
        ///// </summary>
        ///// <param name="sourceDataContext">The source DataContext.</param>
        ///// <param name="targetDataContext">The target DataContext.</param>
        ///// <param name="sourceServer">The source Server.</param>
        ///// <param name="targetServer">The target Server.</param>
        ///// <param name="dataComparisonType">
        ///// The completeness of comparisons between matching objects.
        ///// </param>
        //public static void IntersectWith(DataContext sourceDataContext, DataContext targetDataContext, Server sourceServer,
        //    Server targetServer, DataComparisonType dataComparisonType = DataComparisonType.SchemaLevelNamespaces)
        //{
        //    var matchingCatalogs = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        //    var removableCatalogs = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        //    matchingCatalogs.UnionWith(sourceServer.Catalogs.Keys);
        //    matchingCatalogs.IntersectWith(targetServer.Catalogs.Keys);

        //    removableCatalogs.UnionWith(sourceServer.Catalogs.Keys);
        //    removableCatalogs.ExceptWith(matchingCatalogs);

        //    foreach (var catalog in removableCatalogs)
        //        RemoveCatalog(sourceServer, catalog);

        //    foreach (var catalog in matchingCatalogs)
        //    {
        //        var sourceCatalog = sourceServer.Catalogs[catalog];
        //        var targetCatalog = targetServer.Catalogs[catalog];

        //        switch (dataComparisonType)
        //        {
        //            case DataComparisonType.Definitions:
        //                if (Catalog.CompareDefinitions(sourceCatalog, targetCatalog))
        //                    Catalog.IntersectWith(sourceDataContext, targetDataContext, sourceCatalog, targetCatalog, dataComparisonType);
        //                break;
        //            case DataComparisonType.Namespaces:
        //            case DataComparisonType.SchemaLevelNamespaces:
        //                if (Catalog.CompareObjectNames(sourceCatalog, targetCatalog))
        //                    Catalog.IntersectWith(sourceDataContext, targetDataContext, sourceCatalog, targetCatalog, dataComparisonType);
        //                break;
        //        }
        //    }
        //}

        ///// <summary>
        ///// Modifies the source Server to contain objects that are
        ///// present in both iteself and in the target Server.
        ///// </summary>
        ///// <param name="sourceDataContext">The source DataContext.</param>
        ///// <param name="targetDataContext">The target DataContext.</param>
        ///// <param name="sourceServer">The source Server.</param>
        ///// <param name="targetServer">The target Server.</param>
        ///// <param name="dataComparisonType">
        ///// The completeness of comparisons between matching objects.
        ///// </param>
        //public static void UnionWith(DataContext sourceDataContext, DataContext targetDataContext, Server sourceServer,
        //    Server targetServer, DataComparisonType dataComparisonType = DataComparisonType.SchemaLevelNamespaces)
        //{
        //    var matchingCatalogs = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        //    var addableCatalogs = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        //    matchingCatalogs.UnionWith(sourceServer.Catalogs.Keys);
        //    matchingCatalogs.IntersectWith(targetServer.Catalogs.Keys);

        //    addableCatalogs.UnionWith(targetServer.Catalogs.Keys);
        //    addableCatalogs.ExceptWith(matchingCatalogs);

        //    foreach (var catalog in addableCatalogs)
        //    {
        //        var targetCatalog = targetServer.Catalogs[catalog];
        //        if (targetCatalog == null)
        //            continue;

        //        AddCatalog(sourceServer, Catalog.Clone(targetCatalog));
        //    }

        //    foreach (var catalog in matchingCatalogs)
        //    {
        //        var sourceCatalog = sourceServer.Catalogs[catalog];
        //        var targetCatalog = targetServer.Catalogs[catalog];

        //        if (sourceCatalog == null || targetCatalog == null)
        //            continue;

        //        switch (dataComparisonType)
        //        {
        //            case DataComparisonType.Definitions:
        //                if (Catalog.CompareDefinitions(sourceCatalog, targetCatalog))
        //                    Catalog.UnionWith(sourceDataContext, targetDataContext, sourceCatalog, targetCatalog, dataComparisonType);
        //                break;
        //            case DataComparisonType.Namespaces:
        //            case DataComparisonType.SchemaLevelNamespaces:
        //                if (Catalog.CompareObjectNames(sourceCatalog, targetCatalog))
        //                    Catalog.UnionWith(sourceDataContext, targetDataContext, sourceCatalog, targetCatalog, dataComparisonType);
        //                break;
        //        }
        //    }
        //}

        //public Server(SerializationInfo info, StreamingContext context)
        //{
        //    // Holding off on the serialzation in this manner because, this is
        //    // extremely complicated to do in this manner do to data object
        //    // associations, especially
        //    // Deserialize Members
        //    ObjectName = info.GetString("ObjectName");
        //    Description = info.GetString("Description");

        //    // Deserialize Catalogs
        //    var catalogs = info.GetInt32("Catalogs");
        //    Catalogs = new DataObjectLookup<Catalog>();

        //    for (var i = 0; i < catalogs; i++)
        //    {
        //        var catalog = (Catalog)info.GetValue("Catalog" + i, typeof (Catalog));
        //        catalog.Server = this;
        //        Catalogs.Add(catalog);
        //        // TODO
        //        //Schema.LinkForeignKeys(schema, dataContext);
        //    }
        //}

        //public void GetObjectData(SerializationInfo info, StreamingContext context)
        //{
        //    // Holding off on the serialzation in this manner because, this is
        //    // extremely complicated to do in this manner do to data object
        //    // associations, especially
        //    // Serialize Members
        //    info.AddValue("ObjectName", ObjectName);
        //    info.AddValue("Description", Description);

        //    // Serialize Catalogs
        //    info.AddValue("Catalogs", Catalogs.Count);

        //    var i = 0;
        //    foreach (var catalog in Catalogs)
        //        info.AddValue("Catalog" + i++, catalog);
        //}

        //public static Server FromJson(string json)
        //{
        //    return JsonConvert.DeserializeObject<Server>(json);
        //}

        //public static string ToJson(Server server, Formatting formatting = Formatting.Indented)
        //{
        //    return JsonConvert.SerializeObject(server, formatting);
        //}
    }
}
