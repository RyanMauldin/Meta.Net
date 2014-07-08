using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Meta.Net.Data.Enum;
using Meta.Net.Data.Interfaces;
using Meta.Net.Data.Metadata;
using Newtonsoft.Json;

namespace Meta.Net.Data.Objects
{
    [Serializable]
    public class Server : IDataObject
    {
		#region Properties (5) 

        public DataContext DataContext { get; set; }

        public Dictionary<string, Catalog> Catalogs { get; private set; }

        private DataGenerics DataGenerics { get; set; }

        public string Description { get; set; }

        private MetadataDataSet MetadataDataSet { get; set; }

        public string ObjectName
        {
            get
            {
                return _objectName;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                    return;

                _objectName = value;
            }
        }

		#endregion Properties 

		#region Fields (1) 

        [NonSerialized]
        private string _objectName;

		#endregion Fields 

		#region Constructors (3) 

        public Server(SerializationInfo info, StreamingContext context)
        {
            // Set Null Members
            MetadataDataSet = null;
            DataGenerics = null;

            // Deserialize Members
            ObjectName = info.GetString("ObjectName");
            Description = info.GetString("Description");

            // Deserialize Catalogs
            var catalogs = info.GetInt32("Catalogs");
            Catalogs = new Dictionary<string, Catalog>(StringComparer.OrdinalIgnoreCase);

            for (var i = 0; i < catalogs; i++)
            {
                var catalog = (Catalog)info.GetValue("Catalog" + i, typeof (Catalog));
                catalog.Server = this;
                Catalogs.Add(catalog.ObjectName, catalog);
            }
        }

        public Server(string objectName, DataContext dataContext)
        {
            Init(this, objectName, dataContext);
        }

        public Server(DataContext dataContext)
        {
            Init(this, null, dataContext);
        }

		#endregion Constructors 

		#region Methods (25) 

		#region Public Methods (24) 

        public static bool AddCatalog(Server server, Catalog catalog)
        {
            if (server.Catalogs.ContainsKey(catalog.ObjectName))
                return false;
        
            if (catalog.Server == null)
            {
                catalog.Server = server;
                server.Catalogs.Add(catalog.ObjectName, catalog);
                return true;
            }

            if (catalog.Server.Equals(server))
            {
                server.Catalogs.Add(catalog.ObjectName, catalog);
                return true;
            }

            return false;
        }

        public static bool AddCatalog(Server server, string objectName)
        {
            if (string.IsNullOrEmpty(objectName))
                return false;

            if (server.Catalogs.ContainsKey(objectName))
                return false;
            
            var catalog = new Catalog(server, objectName);
            server.Catalogs.Add(objectName, catalog);

            return true;
        }

        /// <summary>
        /// Deep Clear... disassociates all objects in each catalog
        /// first before clearing out the collections in hopes of better
        /// memory dissipation from garbage collection.
        /// </summary>
        /// <param name="server">The server to deep clear.</param>
        public static void Clear(Server server)
        {
            foreach (var catalog in server.Catalogs.Values)
                Catalog.Clear(catalog);

            server.Catalogs.Clear();
        }

        /// <summary>
        /// Deep Clone...
        /// A clone of this class and clones of all assosiated metadata.
        /// </summary>
        /// <param name="server">The server to deep clone.</param>
        /// <param name="dataContext">The DataContext to use for the clone.</param>
        /// <returns>A clone of this class and clones of all assosiated metadata.</returns>
        public static Server Clone(Server server, DataContext dataContext)
        {
            var serverClone = new Server(server.ObjectName, dataContext);
            
            foreach (var catalog in server.Catalogs.Values)
                AddCatalog(serverClone, Catalog.Clone(catalog));

            return serverClone;
        }

        public static bool CompareDefinitions(Server sourceServer, Server targetServer)
        {
            return CompareObjectNames(sourceServer, targetServer);
        }

        public static bool CompareMatchedServer(DataContext sourceDataContext, DataContext targetDataContext,
            Server matchedServer, Server sourceServer, Server targetServer, Server alteredServer)
        {
            var globalCompareState = true;

            foreach(var matchedCatalog in matchedServer.Catalogs.Values)
            {
                Catalog sourceCatalog;
                Catalog targetCatalog;

                sourceServer.Catalogs.TryGetValue(matchedCatalog.ObjectName, out sourceCatalog);
                targetServer.Catalogs.TryGetValue(matchedCatalog.ObjectName, out targetCatalog);

                if (sourceCatalog == null || targetCatalog == null)
                    throw new Exception(string.Format("Source and/or target catalogs did not exist for the matching catalog {0} during Server.CompareMatchedServer() method.", matchedCatalog.Namespace));
                
                var alteredCatalog = Catalog.ShallowClone(sourceCatalog);

                if (!AddCatalog(alteredServer, alteredCatalog))
                    throw new Exception(string.Format("Unable to add alteredCatalog {0} to alteredServer in Server.CompareMatchedServer() as it may already exist and should not.", sourceCatalog.Namespace));

                if (!Catalog.CompareMatchedCatalog(sourceDataContext, targetDataContext, matchedCatalog, sourceCatalog, targetCatalog, alteredCatalog))
                    globalCompareState = false;

                if (Catalog.ObjectCount(alteredCatalog) == 0 && !RemoveCatalog(alteredServer, alteredCatalog))
                    throw new Exception(string.Format("Unable to remove alteredCatalog {0} for alteredServer in Server.CompareMatchedServer().", sourceCatalog.Namespace));
            }

            return globalCompareState;
        }

        public static bool CompareObjectNames(Server sourceServer, Server targetServer, StringComparison stringComparison = StringComparison.OrdinalIgnoreCase)
        {
            switch (stringComparison)
            {
                case StringComparison.CurrentCulture:
                    return StringComparer.CurrentCulture.Compare(
                        sourceServer.ObjectName, targetServer.ObjectName) == 0;
                case StringComparison.CurrentCultureIgnoreCase:
                    return StringComparer.CurrentCultureIgnoreCase.Compare(
                        sourceServer.ObjectName, targetServer.ObjectName) == 0;
                case StringComparison.InvariantCulture:
                    return StringComparer.InvariantCulture.Compare(
                        sourceServer.ObjectName, targetServer.ObjectName) == 0;
                case StringComparison.InvariantCultureIgnoreCase:
                    return StringComparer.InvariantCultureIgnoreCase.Compare(
                        sourceServer.ObjectName, targetServer.ObjectName) == 0;
                case StringComparison.Ordinal:
                    return StringComparer.Ordinal.Compare(
                        sourceServer.ObjectName, targetServer.ObjectName) == 0;
                case StringComparison.OrdinalIgnoreCase:
                    return StringComparer.OrdinalIgnoreCase.Compare(
                        sourceServer.ObjectName, targetServer.ObjectName) == 0;
                default:
                    return StringComparer.OrdinalIgnoreCase.Compare(
                        sourceServer.ObjectName, targetServer.ObjectName) == 0;
            }
        }

        /// <summary>
        /// Removes all objects in the target Server from the source Server.
        /// </summary>
        /// <param name="sourceDataContext">The source DataContext.</param>
        /// <param name="targetDataContext">The target DataContext.</param>
        /// <param name="sourceServer">The source Server.</param>
        /// <param name="targetServer">The target Server.</param>
        /// <param name="dataComparisonType">
        /// The completeness of comparisons between matching objects.
        /// </param>
        public static void ExceptWith(DataContext sourceDataContext, DataContext targetDataContext,
            Server sourceServer, Server targetServer, DataComparisonType dataComparisonType = DataComparisonType.SchemaLevelNamespaces)
        {
            var matchingCatalogs = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            matchingCatalogs.UnionWith(sourceServer.Catalogs.Keys);
            matchingCatalogs.IntersectWith(targetServer.Catalogs.Keys);

            foreach (var matchedCatalog in matchingCatalogs)
            {
                Catalog sourceCatalog;
                if (!sourceServer.Catalogs.TryGetValue(matchedCatalog, out sourceCatalog))
                    continue;

                Catalog targetCatalog;
                if (!targetServer.Catalogs.TryGetValue(matchedCatalog, out targetCatalog))
                    continue;

                if (sourceCatalog == null || targetCatalog == null)
                    throw new Exception(string.Format("Source and/or target catalogs did not exist for the matching catalog [{0}] during Server.ExceptWith() method.", matchedCatalog));

                switch (dataComparisonType)
                {
                    case DataComparisonType.Definitions:
                        if (Catalog.CompareDefinitions(sourceCatalog, targetCatalog))
                        {
                            Catalog.ExceptWith(sourceDataContext, targetDataContext, sourceCatalog, targetCatalog, dataComparisonType);
                            if (Catalog.ObjectCount(sourceCatalog) == 0)
                                RemoveCatalog(sourceServer, matchedCatalog);
                        }
                        break;
                    case DataComparisonType.Namespaces:
                    case DataComparisonType.SchemaLevelNamespaces:
                        if (Catalog.CompareObjectNames(sourceCatalog, targetCatalog))
                        {
                            Catalog.ExceptWith(sourceDataContext, targetDataContext, sourceCatalog, targetCatalog, dataComparisonType);
                            if (Catalog.ObjectCount(sourceCatalog) == 0)
                                RemoveCatalog(sourceServer, matchedCatalog);
                            
                        }
                        break;
                }
            }
        }

        public static bool Fill(Server server, DataConnectionManager dataConnectionManager)
        {
            Clear(server);

            if (dataConnectionManager == null)
                return false;

            dataConnectionManager.Open();
            if (!dataConnectionManager.IsOpen() || dataConnectionManager.IsBusy())
                return false;

            server.MetadataDataSet = new MetadataDataSet(dataConnectionManager.DataConnectionInfo, dataConnectionManager.CatalogName);
            server.DataGenerics = new DataGenerics();

            server.MetadataDataSet.FillCatalogs(dataConnectionManager);
            server.DataGenerics.FillCatalogs(server.MetadataDataSet);
            
            foreach (var str in server.DataGenerics.Catalogs)
            {
                CatalogsRow catalogsRow;
                if (!server.DataGenerics.CatalogRows.TryGetValue(str + ".", out catalogsRow))
                    continue;

                var catalog = new Catalog(server, catalogsRow);

                dataConnectionManager.ChangeCatalog(catalog.ObjectName);
                server.MetadataDataSet.ChangeCatalog(dataConnectionManager.DataConnectionInfo, catalog.ObjectName);
                
                if (!dataConnectionManager.IsOpen() || dataConnectionManager.IsBusy())
                {
                    Clear(server);
                    return false;
                }
                
                server.MetadataDataSet.Fill(dataConnectionManager);
                server.DataGenerics.Fill(server.MetadataDataSet);
                Catalog.Fill(catalog, server.DataGenerics);
                AddCatalog(server, catalog);
            }

            server.DataGenerics = null;
            server.MetadataDataSet = null;
            
            return true;
        }

        public static bool FillCatalog(Server server, DataConnectionManager dataConnectionManager, Catalog catalog)
        {
            if (!server.Catalogs.ContainsKey(catalog.ObjectName))
                return false;

            Catalog catalogObject;
            if (!server.Catalogs.TryGetValue(catalog.ObjectName, out catalogObject))
                return false;

            if (!catalog.Equals(catalogObject))
                return false;

            if (dataConnectionManager == null)
                return false;

            dataConnectionManager.Open();
            if (!dataConnectionManager.IsOpen() || dataConnectionManager.IsBusy())
                return false;

            dataConnectionManager.ChangeCatalog(catalog.ObjectName);

            if (!dataConnectionManager.IsOpen() || dataConnectionManager.IsBusy())
            {
                Clear(server);
                return false;
            }

            server.MetadataDataSet = new MetadataDataSet(dataConnectionManager.DataConnectionInfo, catalog.ObjectName);
            server.DataGenerics = new DataGenerics();

            server.MetadataDataSet.Fill(dataConnectionManager);
            server.DataGenerics.Fill(server.MetadataDataSet);
            Catalog.Fill(catalog, server.DataGenerics);

            server.DataGenerics = null;
            server.MetadataDataSet = null;

            return true;
        }

        public static Server FromJson(string json)
        {
            return JsonConvert.DeserializeObject<Server>(json);
        }

        public static void GenerateAlterScripts(DataContext sourceDataContext, DataContext targetDataContext,
            Server alteredServer, Server sourceServer, Server targetServer, Server droppedServer, Server createdServer,
            Server matchedServer, DataSyncActionsCollection dataSyncActions, DataProperties dataProperties)
        {
            foreach (var alteredCatalog in alteredServer.Catalogs.Values)
            {
                Catalog sourceCatalog;
                Catalog targetCatalog;
                Catalog droppedCatalog;
                Catalog createdCatalog;
                Catalog matchedCatalog;

                sourceServer.Catalogs.TryGetValue(alteredCatalog.ObjectName, out sourceCatalog);
                targetServer.Catalogs.TryGetValue(alteredCatalog.ObjectName, out targetCatalog);
                droppedServer.Catalogs.TryGetValue(alteredCatalog.ObjectName, out droppedCatalog);
                createdServer.Catalogs.TryGetValue(alteredCatalog.ObjectName, out createdCatalog);
                matchedServer.Catalogs.TryGetValue(alteredCatalog.ObjectName, out matchedCatalog);

                if (sourceCatalog == null || targetCatalog == null)
                    throw new Exception(string.Format("Source and/or target catalogs did not exist for the altered catalog {0} during Server.GenerateAlterScripts() method.", alteredCatalog.Namespace));

                Catalog.GenerateAlterScripts(sourceDataContext, targetDataContext, alteredCatalog, sourceCatalog, targetCatalog, droppedCatalog, createdCatalog, matchedCatalog, dataSyncActions, dataProperties);
            }
        }

        public static void GenerateCreateScripts(DataContext sourceDataContext, DataContext targetDataContext, Server createdServer,
            Server sourceServer, Server targetServer, DataSyncActionsCollection dataSyncActions, DataProperties dataProperties)
        {
            foreach (var createdCatalog in createdServer.Catalogs.Values)
            {
                Catalog sourceCatalog;
                Catalog targetCatalog;

                sourceServer.Catalogs.TryGetValue(createdCatalog.ObjectName, out sourceCatalog);
                targetServer.Catalogs.TryGetValue(createdCatalog.ObjectName, out targetCatalog);

                Catalog.GenerateCreateScripts(sourceDataContext, targetDataContext, createdCatalog, sourceCatalog, targetCatalog, dataSyncActions, dataProperties);
            }
                
        }

        public static void GenerateDropScripts(DataContext sourceDataContext, DataContext targetDataContext, Server droppedServer,
            Server sourceServer, Server targetServer, DataSyncActionsCollection dataSyncActions, DataProperties dataProperties)
        {
            if (ObjectCount(droppedServer) == 0 || !dataProperties.TightSync)

            foreach (var droppedCatalog in droppedServer.Catalogs.Values)
            {
                Catalog sourceCatalog;
                Catalog targetCatalog;

                sourceServer.Catalogs.TryGetValue(droppedCatalog.ObjectName, out sourceCatalog);
                targetServer.Catalogs.TryGetValue(droppedCatalog.ObjectName, out targetCatalog);

                Catalog.GenerateDropScripts(sourceDataContext, targetDataContext, droppedCatalog, sourceCatalog, targetCatalog, dataSyncActions, dataProperties);
            }
        }

        public static bool GetCatalogs(Server server, DataConnectionManager dataConnectionManager)
        {
            if (dataConnectionManager == null)
                return false;

            var connectionActive = dataConnectionManager.IsOpen();
            dataConnectionManager.Open();

            if (server.MetadataDataSet == null)
                server.MetadataDataSet = new MetadataDataSet(dataConnectionManager.DataConnectionInfo, dataConnectionManager.CatalogName);

            if (server.DataGenerics == null)
                server.DataGenerics = new DataGenerics();

            if (dataConnectionManager.IsBusy())
                return false;
            
            server.MetadataDataSet.FillCatalogs(dataConnectionManager);

            if (!connectionActive)
                dataConnectionManager.Close();

            server.DataGenerics.FillCatalogs(server.MetadataDataSet);

            Clear(server);

            foreach (var str in server.DataGenerics.Catalogs)
            {
                CatalogsRow catalogsRow;
                if (!server.DataGenerics.CatalogRows.TryGetValue(str + ".", out catalogsRow))
                    continue;

                var catalog = new Catalog(server, catalogsRow);
                AddCatalog(server, catalog);
            }

            server.DataGenerics.Clear();
            server.MetadataDataSet.Clear();

            return true;
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            // Serialize Members
            info.AddValue("ObjectName", ObjectName);
            info.AddValue("Description", Description);

            // Serialize Catalogs
            info.AddValue("Catalogs", Catalogs.Count);

            var i = 0;
            foreach (var catalog in Catalogs.Values)
                info.AddValue("Catalog" + i++, catalog);
        }

        /// <summary>
        /// Modifies the source Server to contain only objects that are
        /// present in the source Server and in the target Server.
        /// </summary>
        /// <param name="sourceDataContext">The source DataContext.</param>
        /// <param name="targetDataContext">The target DataContext.</param>
        /// <param name="sourceServer">The source Server.</param>
        /// <param name="targetServer">The target Server.</param>
        /// <param name="dataComparisonType">
        /// The completeness of comparisons between matching objects.
        /// </param>
        public static void IntersectWith(DataContext sourceDataContext, DataContext targetDataContext, Server sourceServer,
            Server targetServer, DataComparisonType dataComparisonType = DataComparisonType.SchemaLevelNamespaces)
        {
            var matchingCatalogs = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            var removableCatalogs = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            matchingCatalogs.UnionWith(sourceServer.Catalogs.Keys);
            matchingCatalogs.IntersectWith(targetServer.Catalogs.Keys);

            removableCatalogs.UnionWith(sourceServer.Catalogs.Keys);
            removableCatalogs.ExceptWith(matchingCatalogs);

            foreach (var catalog in removableCatalogs)
                RemoveCatalog(sourceServer, catalog);

            foreach (var catalog in matchingCatalogs)
            {
                Catalog sourceCatalog;
                if (!sourceServer.Catalogs.TryGetValue(catalog, out sourceCatalog))
                    continue;

                Catalog targetCatalog;
                if (!targetServer.Catalogs.TryGetValue(catalog, out targetCatalog))
                    continue;

                switch (dataComparisonType)
                {
                    case DataComparisonType.Definitions:
                        if (Catalog.CompareDefinitions(sourceCatalog, targetCatalog))
                            Catalog.IntersectWith(sourceDataContext, targetDataContext, sourceCatalog, targetCatalog, dataComparisonType);
                        break;
                    case DataComparisonType.Namespaces:
                    case DataComparisonType.SchemaLevelNamespaces:
                        if (Catalog.CompareObjectNames(sourceCatalog, targetCatalog))
                            Catalog.IntersectWith(sourceDataContext, targetDataContext, sourceCatalog, targetCatalog, dataComparisonType);
                        break;
                }
            }
        }

        public static long ObjectCount(Server server, bool deepCount = false)
        {
            if (!deepCount)
                return server.Catalogs.Count;

            return server.Catalogs.Count + server.Catalogs.Values.Sum(catalog => Catalog.ObjectCount(catalog, true));
        }

        public static bool RemoveCatalog(Server server, string objectName)
        {
            return !string.IsNullOrEmpty(objectName) && server.Catalogs.Remove(objectName);
        }

        public static bool RemoveCatalog(Server server, Catalog catalog)
        {
            Catalog catalogObject;
            if (!server.Catalogs.TryGetValue(catalog.ObjectName, out catalogObject))
                return false;
                               
            return catalog.Equals(catalogObject)
                && server.Catalogs.Remove(catalog.ObjectName);
        }

        public static bool RenameCatalog(Server server, string objectName, string newObjectName)
        {
            if (string.IsNullOrEmpty(objectName))
                return false;

            if (string.IsNullOrEmpty(newObjectName))
                return false;

            if (server.Catalogs.ContainsKey(newObjectName))
                return false;
            
            Catalog catalog;
            if (!server.Catalogs.TryGetValue(objectName, out catalog))
                return false;

            server.Catalogs.Remove(objectName);
            catalog.Server = null;
            catalog.ObjectName = newObjectName;
            catalog.Server = server;
            server.Catalogs.Add(newObjectName, catalog);
                
            return true;
        }

        /// <summary>
        /// Shallow Clone...
        /// A clone of this class's isntance specific metadata.
        /// </summary>
        /// <param name="server">The server to shallow clone.</param>
        /// <param name="dataContext">The data context to use.</param>
        /// <returns>A clone of this class's isntance specific metadata.</returns>
        public static Server ShallowClone(Server server, DataContext dataContext)
        {
            return new Server(server.ObjectName, dataContext);
        }

        public static string ToJson(Server server, Formatting formatting = Formatting.Indented)
        {
            return JsonConvert.SerializeObject(server, formatting);
        }

        /// <summary>
        /// Modifies the source Server to contain objects that are
        /// present in both iteself and in the target Server.
        /// </summary>
        /// <param name="sourceDataContext">The source DataContext.</param>
        /// <param name="targetDataContext">The target DataContext.</param>
        /// <param name="sourceServer">The source Server.</param>
        /// <param name="targetServer">The target Server.</param>
        /// <param name="dataComparisonType">
        /// The completeness of comparisons between matching objects.
        /// </param>
        public static void UnionWith(DataContext sourceDataContext, DataContext targetDataContext, Server sourceServer,
            Server targetServer, DataComparisonType dataComparisonType = DataComparisonType.SchemaLevelNamespaces)
        {
            var matchingCatalogs = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            var addableCatalogs = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            matchingCatalogs.UnionWith(sourceServer.Catalogs.Keys);
            matchingCatalogs.IntersectWith(targetServer.Catalogs.Keys);

            addableCatalogs.UnionWith(targetServer.Catalogs.Keys);
            addableCatalogs.ExceptWith(matchingCatalogs);

            foreach (var catalog in addableCatalogs)
            {
                Catalog targetCatalog;
                if (!targetServer.Catalogs.TryGetValue(catalog, out targetCatalog))
                    continue;

                AddCatalog(sourceServer, Catalog.Clone(targetCatalog));
            }

            foreach (var catalog in matchingCatalogs)
            {
                Catalog sourceCatalog;
                if (!sourceServer.Catalogs.TryGetValue(catalog, out sourceCatalog))
                    continue;

                Catalog targetCatalog;
                if (!targetServer.Catalogs.TryGetValue(catalog, out targetCatalog))
                    continue;

                switch (dataComparisonType)
                {
                    case DataComparisonType.Definitions:
                        if (Catalog.CompareDefinitions(sourceCatalog, targetCatalog))
                            Catalog.UnionWith(sourceDataContext, targetDataContext, sourceCatalog, targetCatalog, dataComparisonType);
                        break;
                    case DataComparisonType.Namespaces:
                    case DataComparisonType.SchemaLevelNamespaces:
                        if (Catalog.CompareObjectNames(sourceCatalog, targetCatalog))
                            Catalog.UnionWith(sourceDataContext, targetDataContext, sourceCatalog, targetCatalog, dataComparisonType);
                        break;
                }
            }
        }

		#endregion Public Methods 
		#region Private Methods (1) 

        private static void Init(Server server, string objectName, DataContext dataContext)
        {
            server.DataContext = dataContext;
            server.Catalogs = new Dictionary<string, Catalog>(StringComparer.OrdinalIgnoreCase);
            server._objectName = string.IsNullOrEmpty(objectName)
                ? "Default"
                : objectName;
            server.Description = "Server";
            server.MetadataDataSet = null;
            server.DataGenerics = null;
        }

		#endregion Private Methods 

		#endregion Methods 
    }
}
