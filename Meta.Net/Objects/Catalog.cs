using System;
using System.Collections.Generic;
using System.Linq;
using Meta.Net.Abstract;
using Meta.Net.Types;

namespace Meta.Net.Objects
{
    //[Serializable]
    public class Catalog : ServerBasedObject
    {
        public static readonly string DefaultDescription = "Catalog";

        public override string Description
        {
            get { return DefaultDescription; }
        }

        public string CollationName { get; set; }
        public int CompatibilityLevel { get; set; }
        public string CreateDate { get; set; }
        public bool IsAnsiNullDefaultOn { get; set; }
        public bool IsAnsiNullsOn { get; set; }
        public bool IsAnsiPaddingOn { get; set; }
        public bool IsAnsiWarningsOn { get; set; }
        public bool IsArithabortOn { get; set; }
        public bool IsAutoCloseOn { get; set; }
        public bool IsAutoCreateStatsOn { get; set; }
        public bool IsAutoShrinkOn { get; set; }
        public bool IsAutoUpdateStatsAsyncOn { get; set; }
        public bool IsAutoUpdateStatsOn { get; set; }
        public bool IsCleanlyShutdown { get; set; }
        public bool IsConcatNullYieldsNullOn { get; set; }
        public bool IsCursorCloseOnCommitOn { get; set; }
        public bool IsDateCorrelationOn { get; set; }
        public bool IsDbChainingOn { get; set; }
        public bool IsFulltextEnabled { get; set; }
        public bool IsInStandby { get; set; }
        public bool IsLocalCursorDefault { get; set; }
        public bool IsMasterKeyEncryptedByServer { get; set; }
        public bool IsNumericRoundabortOn { get; set; }
        public bool IsParameterizationForced { get; set; }
        public bool IsQuotedIdentifierOn { get; set; }
        public bool IsReadOnly { get; set; }
        public bool IsRecursiveTriggersOn { get; set; }
        public bool IsSupplementalLoggingEnabled { get; set; }
        public bool IsTrustworthyOn { get; set; }
        public int PageVerifyOption { get; set; }
        public string PageVerifyOptionDescription { get; set; }
        public int RecoveryModel { get; set; }
        public string RecoveryModelDescription { get; set; }
        public int State { get; set; }
        public string StateDescription { get; set; }
        public int UserAccess { get; set; }
        public string UserAccessDescription { get; set; }

        public DataObjectLookup<Catalog, Schema> Schemas { get; private set; }
        public Dictionary<string, ForeignKey> ForeignKeyPool { get; private set; }
        public Dictionary<string, List<ForeignKey>> ReferencedUserTablePool { get; private set; }

        private static void Init(Catalog catalog, Server server, string objectName)
        {
            catalog.ForeignKeyPool = new Dictionary<string, ForeignKey>(StringComparer.OrdinalIgnoreCase);
            catalog.ReferencedUserTablePool = new Dictionary<string, List<ForeignKey>>(StringComparer.OrdinalIgnoreCase);
            catalog.Schemas = new DataObjectLookup<Catalog, Schema>(catalog);
            catalog.Server = server;
            catalog.ObjectName = GetDefaultObjectName(catalog, objectName);
            catalog.CollationName = "";
            catalog.CompatibilityLevel = 100;
            catalog.CreateDate = DateTime.Now.ToString("u");
            catalog.IsAnsiNullDefaultOn = true;
            catalog.IsAnsiNullsOn = true;
            catalog.IsAnsiPaddingOn = true;
            catalog.IsAnsiWarningsOn = true;
            catalog.IsArithabortOn = true;
            catalog.IsAutoCloseOn = false;
            catalog.IsAutoCreateStatsOn = true;
            catalog.IsAutoShrinkOn = false;
            catalog.IsAutoUpdateStatsAsyncOn = false;
            catalog.IsAutoUpdateStatsOn = true;
            catalog.IsCleanlyShutdown = true;
            catalog.IsConcatNullYieldsNullOn = false;
            catalog.IsCursorCloseOnCommitOn = false;
            catalog.IsDateCorrelationOn = false;
            catalog.IsDbChainingOn = false;
            catalog.IsFulltextEnabled = false;
            catalog.IsInStandby = false;
            catalog.IsLocalCursorDefault = false;
            catalog.IsMasterKeyEncryptedByServer = false;
            catalog.IsNumericRoundabortOn = false;
            catalog.IsParameterizationForced = false;
            catalog.IsQuotedIdentifierOn = true;
            catalog.IsReadOnly = false;
            catalog.IsRecursiveTriggersOn = false;
            catalog.IsSupplementalLoggingEnabled = false;
            catalog.IsTrustworthyOn = true;
            catalog.PageVerifyOption = 2;
            catalog.PageVerifyOptionDescription = "CHECKSUM";
            catalog.RecoveryModel = 3;
            catalog.RecoveryModelDescription = "SIMPLE";
            catalog.State = 0;
            catalog.StateDescription = "ONLINE";
            catalog.UserAccess = 0;
            catalog.UserAccessDescription = "MULTI_USER";
        }

        public Catalog(Server server, string objectName)
        {
            Init(this, server, objectName);
        }

        public Catalog()
        {
            Schemas = new DataObjectLookup<Catalog, Schema>(this);
            ForeignKeyPool = new Dictionary<string, ForeignKey>(StringComparer.OrdinalIgnoreCase);
            ReferencedUserTablePool = new Dictionary<string, List<ForeignKey>>(StringComparer.OrdinalIgnoreCase);
        }

        public static void AddSchema(Catalog catalog, Schema schema)
        {
            if (schema.Catalog != null && !schema.Catalog.Equals(catalog))
                RemoveSchema(schema.Catalog, schema);

            catalog.Schemas.Add(schema);
        }

        /// <summary>
        /// Deep Clear... disassociates all objects in each schema
        /// first before clearing out the collections in hopes of better
        /// memory dissipation from garbage collection.
        /// </summary>
        /// <param name="catalog">The catalog to deep clear.</param>
        public static void Clear(Catalog catalog)
        {
            foreach (var schema in catalog.Schemas)
                Schema.Clear(schema);

            catalog.Schemas.Clear();
            catalog.ForeignKeyPool.Clear();
            catalog.ReferencedUserTablePool.Clear();
        }

        /// <summary>
        /// Deep Clone...
        /// A clone of this class and clones of all assosiated metadata.
        /// Makes a call to Schema.LinkForeignKeys(schema) internally after each schema
        /// in the specified catalog has been deep cloned to auto-populate the catalog
        /// properties Catalog.ForeignKeyPool and Catalog.ReferencedUserTablePool.
        /// </summary>
        /// <param name="catalog">The catalog to deep clone.</param>
        /// <returns>A clone of this class and clones of all assosiated metadata.</returns>
        public static Catalog Clone(Catalog catalog)
        {
            var catalogClone = new Catalog
            {
                ObjectName = catalog.ObjectName,
                CollationName = catalog.CollationName,
                CompatibilityLevel = catalog.CompatibilityLevel,
                CreateDate = catalog.CreateDate,
                IsAnsiNullDefaultOn = catalog.IsAnsiNullDefaultOn,
                IsAnsiNullsOn = catalog.IsAnsiNullsOn,
                IsAnsiPaddingOn = catalog.IsAnsiPaddingOn,
                IsAnsiWarningsOn = catalog.IsAnsiWarningsOn,
                IsArithabortOn = catalog.IsArithabortOn,
                IsAutoCloseOn = catalog.IsAutoCloseOn,
                IsAutoCreateStatsOn = catalog.IsAutoCreateStatsOn,
                IsAutoShrinkOn = catalog.IsAutoShrinkOn,
                IsAutoUpdateStatsAsyncOn = catalog.IsAutoUpdateStatsAsyncOn,
                IsAutoUpdateStatsOn = catalog.IsAutoUpdateStatsOn,
                IsCleanlyShutdown = catalog.IsCleanlyShutdown,
                IsConcatNullYieldsNullOn = catalog.IsConcatNullYieldsNullOn,
                IsCursorCloseOnCommitOn = catalog.IsCursorCloseOnCommitOn,
                IsDateCorrelationOn = catalog.IsDateCorrelationOn,
                IsDbChainingOn = catalog.IsDbChainingOn,
                IsFulltextEnabled = catalog.IsFulltextEnabled,
                IsInStandby = catalog.IsInStandby,
                IsLocalCursorDefault = catalog.IsLocalCursorDefault,
                IsMasterKeyEncryptedByServer = catalog.IsMasterKeyEncryptedByServer,
                IsNumericRoundabortOn = catalog.IsNumericRoundabortOn,
                IsParameterizationForced = catalog.IsParameterizationForced,
                IsQuotedIdentifierOn = catalog.IsQuotedIdentifierOn,
                IsReadOnly = catalog.IsReadOnly,
                IsRecursiveTriggersOn = catalog.IsRecursiveTriggersOn,
                IsSupplementalLoggingEnabled = catalog.IsSupplementalLoggingEnabled,
                IsTrustworthyOn = catalog.IsTrustworthyOn,
                PageVerifyOption = catalog.PageVerifyOption,
                PageVerifyOptionDescription = catalog.PageVerifyOptionDescription,
                RecoveryModel = catalog.RecoveryModel,
                RecoveryModelDescription = catalog.RecoveryModelDescription,
                State = catalog.State,
                StateDescription = catalog.StateDescription,
                UserAccess = catalog.UserAccess,
                UserAccessDescription = catalog.UserAccessDescription
            };

            foreach (var schemaClone in catalog.Schemas.Select(Schema.Clone))
            {
                AddSchema(catalogClone, schemaClone);
                //Schema.LinkForeignKeys(schemaClone);
            }
            
            return catalogClone;
        }

        //public static bool CompareDefinitions(Catalog sourceCatalog, Catalog targetCatalog)
        //{
        //    return CompareObjectNames(sourceCatalog, targetCatalog);
        //}

        //public static bool CompareMatchedCatalog(DataContext sourceDataContext, DataContext targetDataContext,
        //    Catalog matchedCatalog, Catalog sourceCatalog, Catalog targetCatalog, Catalog alteredCatalog)
        //{
        //    var globalCompareState = true;

        //    foreach (var matchedSchema in matchedCatalog.Schemas)
        //    {
        //        var sourceSchema = sourceCatalog.Schemas[matchedSchema.Namespace];
        //        if (sourceSchema == null) // TODO
        //            throw new Exception(string.Format("Source catalog did not exist for the matching catalog {0} during Server.CompareMatchedServer() method.", matchedCatalog.Namespace));

        //        var targetSchema = targetCatalog.Schemas[matchedSchema.Namespace];
        //        if (targetSchema == null)
        //            throw new Exception(string.Format("Target catalog did not exist for the matching catalog {0} during Server.CompareMatchedServer() method.", matchedCatalog.Namespace));
                
        //        var alteredSchema = Schema.ShallowClone(sourceSchema);

        //        AddSchema(alteredCatalog, alteredSchema);
                
        //        if (!Schema.CompareMatchedSchema(sourceDataContext, targetDataContext, matchedSchema, sourceSchema, targetSchema, alteredSchema))
        //            globalCompareState = false;

        //        if (Schema.ObjectCount(alteredSchema) == 0)
        //            RemoveSchema(alteredCatalog, alteredSchema);
        //    }

        //    return globalCompareState;
        //}

        //public static bool CompareObjectNames(Catalog sourceCatalog, Catalog targetCatalog, StringComparer stringComparer = null)
        //{
        //    if (stringComparer == null)
        //        stringComparer = StringComparer.OrdinalIgnoreCase;

        //    return stringComparer.Compare(sourceCatalog.ObjectName, targetCatalog.ObjectName) == 0;
        //}

        ///// <summary>
        ///// Removes all objects in the target Catalog from the source Catalog.
        ///// </summary>
        ///// <param name="sourceDataContext">The source DataContext.</param>
        ///// <param name="targetDataContext">The target DataContext.</param>
        ///// <param name="sourceCatalog">The source Catalog.</param>
        ///// <param name="targetCatalog">The target Catalog.</param>
        ///// <param name="dataComparisonType">
        ///// The completeness of comparisons between matching objects.
        ///// </param>
        //public static void ExceptWith(DataContext sourceDataContext, DataContext targetDataContext, Catalog sourceCatalog, Catalog targetCatalog,
        //    DataComparisonType dataComparisonType = DataComparisonType.SchemaLevelNamespaces)
        //{
        //    var matchingSchemas = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        //    matchingSchemas.UnionWith(sourceCatalog.Schemas.Keys);
        //    matchingSchemas.IntersectWith(targetCatalog.Schemas.Keys);

        //    foreach (var schema in matchingSchemas)
        //    {
        //        var sourceSchema = sourceCatalog.Schemas[schema];
        //        var targetSchema = targetCatalog.Schemas[schema];
        //        if (sourceSchema == null || targetSchema == null)
        //            continue;

        //        switch (dataComparisonType)
        //        {
        //            case DataComparisonType.Definitions:
        //                if (Schema.CompareDefinitions(sourceSchema, targetSchema))
        //                {
        //                    Schema.ExceptWith(sourceDataContext, targetDataContext, sourceSchema, targetSchema, dataComparisonType);
        //                    if (Schema.ObjectCount(sourceSchema) == 0)
        //                        RemoveSchema(sourceCatalog, schema);
        //                }
        //                break;
        //            case DataComparisonType.Namespaces:
        //            case DataComparisonType.SchemaLevelNamespaces:
        //                if (Schema.CompareObjectNames(sourceSchema, targetSchema))
        //                {
        //                    Schema.ExceptWith(sourceDataContext, targetDataContext, sourceSchema, targetSchema, dataComparisonType);
        //                    if (Schema.ObjectCount(sourceSchema) == 0)
        //                        RemoveSchema(sourceCatalog, schema);
        //                }
        //                break;
        //        }
        //    }
        //}

        

        //public static void GenerateAlterScripts(DataContext sourceDataContext, DataContext targetDataContext, Catalog alteredCatalog,
        //    Catalog sourceCatalog, Catalog targetCatalog, Catalog droppedCatalog, Catalog createdCatalog, Catalog matchedCatalog,
        //    DataSyncActionsCollection dataSyncActions, DataProperties dataProperties)
        //{
        //    var dataDependencyBuilder = new DataDependencyBuilder();

        //    if (droppedCatalog != null)
        //        dataDependencyBuilder.PreloadDroppedDependencies(droppedCatalog);

        //    if (createdCatalog != null)
        //        dataDependencyBuilder.PreloadCreatedDependencies(createdCatalog);

        //    foreach (var alteredSchema in alteredCatalog.Schemas)
        //    {
        //        var sourceSchema = sourceCatalog.Schemas[alteredSchema.Namespace];
        //        var targetSchema = targetCatalog.Schemas[alteredSchema.Namespace];

        //        if (sourceSchema == null || targetSchema == null)
        //            throw new Exception(string.Format("Source and/or target schemas did not exist for the altered schema {0} during Catalog.GenerateAlterScripts() method.", alteredSchema.Namespace));

        //        Schema droppedSchema = null;
        //        Schema createdSchema = null;
        //        Schema matchedSchema = null;
                
        //        if (droppedCatalog != null)
        //            droppedSchema = droppedCatalog.Schemas[alteredSchema.Namespace];

        //        if (createdCatalog != null)
        //            createdSchema = createdCatalog.Schemas[alteredSchema.Namespace];

        //        if (matchedCatalog != null)
        //            matchedSchema = matchedCatalog.Schemas[alteredSchema.Namespace];
                
        //        Schema.GenerateAlterScripts(sourceDataContext, targetDataContext, alteredSchema, sourceSchema, targetSchema,
        //            droppedSchema, createdSchema, matchedSchema, dataSyncActions, dataDependencyBuilder, dataProperties);
        //    }
        //}

        //public static void GenerateCreateScripts(DataContext sourceDataContext, DataContext targetDataContext, Catalog createdCatalog,
        //    Catalog sourceCatalog, Catalog targetCatalog, DataSyncActionsCollection dataSyncActions, DataProperties dataProperties)
        //{
        //    if (!DataProperties.NonRemovableCatalogs.Contains(createdCatalog.Namespace))
        //        if (sourceCatalog != null && targetCatalog == null)
        //        {
        //            var dataSyncAction = DataActionFactory.CreateCatalog(sourceDataContext, targetDataContext, createdCatalog);
        //            if (dataSyncAction != null)
        //                dataSyncActions.Add(dataSyncAction);
        //        }

        //    foreach (var createdSchema in createdCatalog.Schemas)
        //    {
        //        Schema sourceSchema = null;
        //        Schema targetSchema = null;

        //        if (sourceCatalog != null)
        //            sourceSchema = sourceCatalog.Schemas[createdSchema.Namespace];

        //        if (targetCatalog != null)
        //            targetSchema = targetCatalog.Schemas[createdSchema.Namespace];

        //        Schema.GenerateCreateScripts(sourceDataContext, targetDataContext, createdSchema, sourceSchema,
        //            targetSchema, dataSyncActions, dataProperties);
        //    }
        //}

        //public static void GenerateDropScripts(DataContext sourceDataContext, DataContext targetDataContext, Catalog droppedCatalog,
        //    Catalog sourceCatalog, Catalog targetCatalog, DataSyncActionsCollection dataSyncActions, DataProperties dataProperties)
        //{
        //    if (ObjectCount(droppedCatalog) == 0 || !dataProperties.TightSync)
        //        return;

        //    if (!DataProperties.NonRemovableCatalogs.Contains(droppedCatalog.Namespace))
        //        if (sourceCatalog == null && targetCatalog != null && dataProperties.TightSync)
        //        {
        //            var dataSyncAction = DataActionFactory.DropCatalog(sourceDataContext, targetDataContext, droppedCatalog);
        //            if (dataSyncAction != null)
        //                dataSyncActions.Add(dataSyncAction);
        //        }

        //    foreach (var droppedSchema in droppedCatalog.Schemas)
        //    {
        //        Schema sourceSchema = null;
        //        Schema targetSchema = null;

        //        if (sourceCatalog != null)
        //            sourceSchema = sourceCatalog.Schemas[droppedSchema.Namespace];

        //        if (targetCatalog != null)
        //            targetSchema = targetCatalog.Schemas[droppedSchema.Namespace];

        //        Schema.GenerateDropScripts(sourceDataContext, targetDataContext, droppedSchema, sourceSchema,
        //            targetSchema, dataSyncActions, dataProperties);
        //    }
        //}

        ///// <summary>
        ///// Modifies the source Catalog to contain only objects that are
        ///// present in the source Catalog and in the target Catalog.
        ///// </summary>
        ///// <param name="sourceDataContext">The source DataContext.</param>
        ///// <param name="targetDataContext">The target DataContext.</param>
        ///// <param name="sourceCatalog">The source Catalog.</param>
        ///// <param name="targetCatalog">The target Catalog.</param>
        ///// <param name="dataComparisonType">
        ///// The completeness of comparisons between matching objects.
        ///// </param>
        //public static void IntersectWith(DataContext sourceDataContext, DataContext targetDataContext, Catalog sourceCatalog,
        //    Catalog targetCatalog, DataComparisonType dataComparisonType = DataComparisonType.SchemaLevelNamespaces)
        //{
        //    var matchingSchemas = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        //    var removableSchemas = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        //    matchingSchemas.UnionWith(sourceCatalog.Schemas.Keys);
        //    matchingSchemas.IntersectWith(targetCatalog.Schemas.Keys);

        //    removableSchemas.UnionWith(sourceCatalog.Schemas.Keys);
        //    removableSchemas.ExceptWith(matchingSchemas);

        //    foreach (var schema in removableSchemas)
        //        RemoveSchema(sourceCatalog, schema);

        //    foreach (var schema in matchingSchemas)
        //    {
        //        var sourceSchema = sourceCatalog.Schemas[schema];
        //        var targetSchema = targetCatalog.Schemas[schema];
        //        if (sourceSchema == null || targetSchema == null)
        //            continue;

        //        switch (dataComparisonType)
        //        {
        //            case DataComparisonType.Definitions:
        //                if (Schema.CompareDefinitions(sourceSchema, targetSchema))
        //                    Schema.IntersectWith(sourceDataContext, targetDataContext, sourceSchema, targetSchema, dataComparisonType);
        //                break;
        //            case DataComparisonType.Namespaces:
        //            case DataComparisonType.SchemaLevelNamespaces:
        //                if (Schema.CompareObjectNames(sourceSchema, targetSchema))
        //                    Schema.IntersectWith(sourceDataContext, targetDataContext, sourceSchema, targetSchema, dataComparisonType);
        //                break;
        //        }
        //    }
        //}

        public static long ObjectCount(Catalog catalog, bool deepCount = false)
        {
            if (!deepCount)
                return catalog.Schemas.Count;

            return catalog.Schemas.Count +
                   catalog.Schemas.Sum(
                       schema => Schema.ObjectCount(schema, true));
        }

        public static void RemoveSchema(Catalog catalog, string objectNamespace)
        {
            catalog.Schemas.Remove(objectNamespace);
        }

        public static void RemoveSchema(Catalog catalog, Schema schema)
        {
            catalog.Schemas.Remove(schema.Namespace);
        }

        public static void RenameSchema(Catalog catalog, string objectNamespace, string newObjectName)
        {
            var schema = catalog.Schemas[objectNamespace];
            if (schema == null)
                throw new Exception(string.Format("{0} could not be found in {1} {2} to rename to {3}",
                    objectNamespace, catalog.Description, catalog.Namespace, newObjectName));

            catalog.Schemas.Rename(schema, newObjectName);
        }

        /// <summary>
        /// Shallow Clone...
        /// A clone of this class's instance specific metadata.
        /// </summary>
        /// <param name="catalog">The catalog to shallow clone.</param>
        /// <returns>A clone of this class's instance specific metadata.</returns>
        public static Catalog ShallowClone(Catalog catalog)
        {
            return new Catalog
            {
                ObjectName = catalog.ObjectName,
                CollationName = catalog.CollationName,
                CompatibilityLevel = catalog.CompatibilityLevel,
                CreateDate = catalog.CreateDate,
                IsAnsiNullDefaultOn = catalog.IsAnsiNullDefaultOn,
                IsAnsiNullsOn = catalog.IsAnsiNullsOn,
                IsAnsiPaddingOn = catalog.IsAnsiPaddingOn,
                IsAnsiWarningsOn = catalog.IsAnsiWarningsOn,
                IsArithabortOn = catalog.IsArithabortOn,
                IsAutoCloseOn = catalog.IsAutoCloseOn,
                IsAutoCreateStatsOn = catalog.IsAutoCreateStatsOn,
                IsAutoShrinkOn = catalog.IsAutoShrinkOn,
                IsAutoUpdateStatsAsyncOn = catalog.IsAutoUpdateStatsAsyncOn,
                IsAutoUpdateStatsOn = catalog.IsAutoUpdateStatsOn,
                IsCleanlyShutdown = catalog.IsCleanlyShutdown,
                IsConcatNullYieldsNullOn = catalog.IsConcatNullYieldsNullOn,
                IsCursorCloseOnCommitOn = catalog.IsCursorCloseOnCommitOn,
                IsDateCorrelationOn = catalog.IsDateCorrelationOn,
                IsDbChainingOn = catalog.IsDbChainingOn,
                IsFulltextEnabled = catalog.IsFulltextEnabled,
                IsInStandby = catalog.IsInStandby,
                IsLocalCursorDefault = catalog.IsLocalCursorDefault,
                IsMasterKeyEncryptedByServer = catalog.IsMasterKeyEncryptedByServer,
                IsNumericRoundabortOn = catalog.IsNumericRoundabortOn,
                IsParameterizationForced = catalog.IsParameterizationForced,
                IsQuotedIdentifierOn = catalog.IsQuotedIdentifierOn,
                IsReadOnly = catalog.IsReadOnly,
                IsRecursiveTriggersOn = catalog.IsRecursiveTriggersOn,
                IsSupplementalLoggingEnabled = catalog.IsSupplementalLoggingEnabled,
                IsTrustworthyOn = catalog.IsTrustworthyOn,
                PageVerifyOption = catalog.PageVerifyOption,
                PageVerifyOptionDescription = catalog.PageVerifyOptionDescription,
                RecoveryModel = catalog.RecoveryModel,
                RecoveryModelDescription = catalog.RecoveryModelDescription,
                State = catalog.State,
                StateDescription = catalog.StateDescription,
                UserAccess = catalog.UserAccess,
                UserAccessDescription = catalog.UserAccessDescription
            };
        }

        ///// <summary>
        ///// Modifies the source Catalog to contain all objects that are
        ///// present in both iteself and in the target Catalog.
        ///// </summary>
        ///// <param name="sourceDataContext">The source DataContext.</param>
        ///// <param name="targetDataContext">The target DataContext.</param>
        ///// <param name="sourceCatalog">The source Catalog.</param>
        ///// <param name="targetCatalog">The target Catalog.</param>
        ///// <param name="dataComparisonType">
        ///// The completeness of comparisons between matching objects.
        ///// </param>
        //public static void UnionWith(DataContext sourceDataContext, DataContext targetDataContext, Catalog sourceCatalog,
        //    Catalog targetCatalog, DataComparisonType dataComparisonType = DataComparisonType.SchemaLevelNamespaces)
        //{
        //    var matchingSchemas = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        //    var addableSchemas = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        //    matchingSchemas.UnionWith(sourceCatalog.Schemas.Keys);
        //    matchingSchemas.IntersectWith(targetCatalog.Schemas.Keys);

        //    addableSchemas.UnionWith(targetCatalog.Schemas.Keys);
        //    addableSchemas.ExceptWith(matchingSchemas);

        //    foreach (var schema in addableSchemas)
        //    {
        //        var targetSchema = targetCatalog.Schemas[schema];
        //        if (targetSchema == null)
        //            continue;

        //        AddSchema(sourceCatalog, Schema.Clone(targetSchema));
        //    }

        //    foreach (var schema in matchingSchemas)
        //    {
        //        var sourceSchema = sourceCatalog.Schemas[schema];
        //        var targetSchema = targetCatalog.Schemas[schema];
        //        if (sourceSchema == null || targetSchema == null)
        //            continue;

        //        switch (dataComparisonType)
        //        {
        //            case DataComparisonType.Definitions:
        //                if (Schema.CompareDefinitions(sourceSchema, targetSchema))
        //                    Schema.UnionWith(sourceDataContext, targetDataContext, sourceSchema, targetSchema, dataComparisonType);
        //                break;
        //            case DataComparisonType.Namespaces:
        //            case DataComparisonType.SchemaLevelNamespaces:
        //                if (Schema.CompareObjectNames(sourceSchema, targetSchema))
        //                    Schema.UnionWith(sourceDataContext, targetDataContext, sourceSchema, targetSchema, dataComparisonType);
        //                break;
        //        }
        //    }
        //}

        public static bool UsesCaseSensitiveCollation(Catalog catalog)
        {
            // TODO: Find reason for needing!
            return catalog.CollationName.IndexOf("_CS_", StringComparison.OrdinalIgnoreCase) >= 0;
        }

        ///// <summary>
        ///// Deserialization Constructor.
        ///// Makes a call to Schema.LinkForeignKeys(schema) internally after each schema
        ///// in the catalog being deserialized has deserialized underlying schemas to
        ///// and automatically restores the catalog properties Catalog.ForeignKeyPool
        ///// and Catalog.ReferencedUserTablePool.
        ///// </summary>
        ///// <param name="info">The serialization information.</param>
        ///// <param name="context">The streaming context.</param>
        //public Catalog(SerializationInfo info, StreamingContext context)
        //{
        //    // Holding off on the serialzation in this manner because, this is
        //    // extremely complicated to do in this manner do to data object
        //    // associations, especially
        //    // Set Null Members
        //    Server = null;

        //    // Deserialize Members
        //    ObjectName = info.GetString("ObjectName");
        //    Description = info.GetString("Description");
        //    CollationName = info.GetString("CollationName");
        //    CompatibilityLevel = info.GetInt32("CompatibilityLevel");
        //    CreateDate = info.GetString("CreateDate");
        //    IsAnsiNullDefaultOn = info.GetBoolean("IsAnsiNullDefaultOn");
        //    IsAnsiNullsOn = info.GetBoolean("IsAnsiNullsOn");
        //    IsAnsiPaddingOn = info.GetBoolean("IsAnsiPaddingOn");
        //    IsAnsiWarningsOn = info.GetBoolean("IsAnsiWarningsOn");
        //    IsArithabortOn = info.GetBoolean("IsArithabortOn");
        //    IsAutoCloseOn = info.GetBoolean("IsAutoCloseOn");
        //    IsAutoCreateStatsOn = info.GetBoolean("IsAutoCreateStatsOn");
        //    IsAutoShrinkOn = info.GetBoolean("IsAutoShrinkOn");
        //    IsAutoUpdateStatsAsyncOn = info.GetBoolean("IsAutoUpdateStatsAsyncOn");
        //    IsAutoUpdateStatsOn = info.GetBoolean("IsAutoUpdateStatsOn");
        //    IsCleanlyShutdown = info.GetBoolean("IsCleanlyShutdown");
        //    IsConcatNullYieldsNullOn = info.GetBoolean("IsConcatNullYieldsNullOn");
        //    IsCursorCloseOnCommitOn = info.GetBoolean("IsCursorCloseOnCommitOn");
        //    IsDateCorrelationOn = info.GetBoolean("IsDateCorrelationOn");
        //    IsDbChainingOn = info.GetBoolean("IsDbChainingOn");
        //    IsFulltextEnabled = info.GetBoolean("IsFulltextEnabled");
        //    IsInStandby = info.GetBoolean("IsInStandby");
        //    IsLocalCursorDefault = info.GetBoolean("IsLocalCursorDefault");
        //    IsMasterKeyEncryptedByServer = info.GetBoolean("IsMasterKeyEncryptedByServer");
        //    IsNumericRoundabortOn = info.GetBoolean("IsNumericRoundabortOn");
        //    IsParameterizationForced = info.GetBoolean("IsParameterizationForced");
        //    IsQuotedIdentifierOn = info.GetBoolean("IsQuotedIdentifierOn");
        //    IsReadOnly = info.GetBoolean("IsReadOnly");
        //    IsRecursiveTriggersOn = info.GetBoolean("IsRecursiveTriggersOn");
        //    IsSupplementalLoggingEnabled = info.GetBoolean("IsSupplementalLoggingEnabled");
        //    IsTrustworthyOn = info.GetBoolean("IsTrustworthyOn");
        //    PageVerifyOption = info.GetInt32("PageVerifyOption");
        //    PageVerifyOptionDescription = info.GetString("PageVerifyOptionDescription");
        //    RecoveryModel = info.GetInt32("RecoveryModel");
        //    RecoveryModelDescription = info.GetString("RecoveryModelDescription");
        //    State = info.GetInt32("State");
        //    StateDescription = info.GetString("StateDescription");
        //    UserAccess = info.GetInt32("UserAccess");
        //    UserAccessDescription = info.GetString("UserAccessDescription");

        //    // Deserialize Schemas
        //    var schemas = info.GetInt32("Schemas");
        //    Schemas = new DataObjectLookup<Schema>();

        //    for (var i = 0; i < schemas; i++)
        //    {
        //        var schema = (Schema)info.GetValue("Schema" + i, typeof(Schema));
        //        schema.Catalog = this;
        //        Schemas.Add(schema);
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
        //    info.AddValue("CollationName", CollationName);
        //    info.AddValue("CompatibilityLevel", CompatibilityLevel);
        //    info.AddValue("CreateDate", CreateDate);
        //    info.AddValue("IsAnsiNullDefaultOn", IsAnsiNullDefaultOn);
        //    info.AddValue("IsAnsiNullsOn", IsAnsiNullsOn);
        //    info.AddValue("IsAnsiPaddingOn", IsAnsiPaddingOn);
        //    info.AddValue("IsAnsiWarningsOn", IsAnsiWarningsOn);
        //    info.AddValue("IsArithabortOn", IsArithabortOn);
        //    info.AddValue("IsAutoCloseOn", IsAutoCloseOn);
        //    info.AddValue("IsAutoCreateStatsOn", IsAutoCreateStatsOn);
        //    info.AddValue("IsAutoShrinkOn", IsAutoShrinkOn);
        //    info.AddValue("IsAutoUpdateStatsAsyncOn", IsAutoUpdateStatsAsyncOn);
        //    info.AddValue("IsAutoUpdateStatsOn", IsAutoUpdateStatsOn);
        //    info.AddValue("IsCleanlyShutdown", IsCleanlyShutdown);
        //    info.AddValue("IsConcatNullYieldsNullOn", IsConcatNullYieldsNullOn);
        //    info.AddValue("IsCursorCloseOnCommitOn", IsCursorCloseOnCommitOn);
        //    info.AddValue("IsDateCorrelationOn", IsDateCorrelationOn);
        //    info.AddValue("IsDbChainingOn", IsDbChainingOn);
        //    info.AddValue("IsFulltextEnabled", IsFulltextEnabled);
        //    info.AddValue("IsInStandby", IsInStandby);
        //    info.AddValue("IsLocalCursorDefault", IsLocalCursorDefault);
        //    info.AddValue("IsMasterKeyEncryptedByServer", IsMasterKeyEncryptedByServer);
        //    info.AddValue("IsNumericRoundabortOn", IsNumericRoundabortOn);
        //    info.AddValue("IsParameterizationForced", IsParameterizationForced);
        //    info.AddValue("IsQuotedIdentifierOn", IsQuotedIdentifierOn);
        //    info.AddValue("IsReadOnly", IsReadOnly);
        //    info.AddValue("IsRecursiveTriggersOn", IsRecursiveTriggersOn);
        //    info.AddValue("IsSupplementalLoggingEnabled", IsSupplementalLoggingEnabled);
        //    info.AddValue("IsTrustworthyOn", IsTrustworthyOn);
        //    info.AddValue("PageVerifyOption", PageVerifyOption);
        //    info.AddValue("PageVerifyOptionDescription", PageVerifyOptionDescription);
        //    info.AddValue("RecoveryModel", RecoveryModel);
        //    info.AddValue("RecoveryModelDescription", RecoveryModelDescription);
        //    info.AddValue("State", State);
        //    info.AddValue("StateDescription", StateDescription);
        //    info.AddValue("UserAccess", UserAccess);
        //    info.AddValue("UserAccessDescription", UserAccessDescription);

        //    // Serialize Schemas
        //    info.AddValue("Schemas", Schemas.Count);

        //    var i = 0;
        //    foreach (var schema in Schemas)
        //        info.AddValue("Schema" + i++, schema);
        //}

        //public static Catalog FromJson(string json)
        //{
        //    return JsonConvert.DeserializeObject<Catalog>(json);
        //}

        //public static string ToJson(Catalog catalog, Formatting formatting = Formatting.Indented)
        //{
        //    return JsonConvert.SerializeObject(catalog, formatting);
        //}
    }
}
