using System;
using System.Linq;
using System.Runtime.Serialization;
using Meta.Net.Abstract;

namespace Meta.Net.Objects
{
    [DataContract]
    public class Catalog : ServerBasedObject
    {
        public static readonly string DefaultDescription = "Catalog";

        public override string Description
        {
            get { return DefaultDescription; }
        }
        
        [DataMember] public string CollationName { get; set; }
        [DataMember] public int CompatibilityLevel { get; set; }
        [DataMember] public string CreateDate { get; set; }
        [DataMember] public bool IsAnsiNullDefaultOn { get; set; }
        [DataMember] public bool IsAnsiNullsOn { get; set; }
        [DataMember] public bool IsAnsiPaddingOn { get; set; }
        [DataMember] public bool IsAnsiWarningsOn { get; set; }
        [DataMember] public bool IsArithabortOn { get; set; }
        [DataMember] public bool IsAutoCloseOn { get; set; }
        [DataMember] public bool IsAutoCreateStatsOn { get; set; }
        [DataMember] public bool IsAutoShrinkOn { get; set; }
        [DataMember] public bool IsAutoUpdateStatsAsyncOn { get; set; }
        [DataMember] public bool IsAutoUpdateStatsOn { get; set; }
        [DataMember] public bool IsCleanlyShutdown { get; set; }
        [DataMember] public bool IsConcatNullYieldsNullOn { get; set; }
        [DataMember] public bool IsCursorCloseOnCommitOn { get; set; }
        [DataMember] public bool IsDateCorrelationOn { get; set; }
        [DataMember] public bool IsDbChainingOn { get; set; }
        [DataMember] public bool IsFulltextEnabled { get; set; }
        [DataMember] public bool IsInStandby { get; set; }
        [DataMember] public bool IsLocalCursorDefault { get; set; }
        [DataMember] public bool IsMasterKeyEncryptedByServer { get; set; }
        [DataMember] public bool IsNumericRoundabortOn { get; set; }
        [DataMember] public bool IsParameterizationForced { get; set; }
        [DataMember] public bool IsQuotedIdentifierOn { get; set; }
        [DataMember] public bool IsReadOnly { get; set; }
        [DataMember] public bool IsRecursiveTriggersOn { get; set; }
        [DataMember] public bool IsSupplementalLoggingEnabled { get; set; }
        [DataMember] public bool IsTrustworthyOn { get; set; }
        [DataMember] public int PageVerifyOption { get; set; }
        [DataMember] public string PageVerifyOptionDescription { get; set; }
        [DataMember] public int RecoveryModel { get; set; }
        [DataMember] public string RecoveryModelDescription { get; set; }
        [DataMember] public int State { get; set; }
        [DataMember] public string StateDescription { get; set; }
        [DataMember] public int UserAccess { get; set; }
        [DataMember] public string UserAccessDescription { get; set; }

        [DataMember] public DataObjectLookup<Catalog, Schema> Schemas { get; private set; }

        public Catalog()
        {
            Schemas = new DataObjectLookup<Catalog, Schema>(this);
            CollationName = string.Empty;
            CompatibilityLevel = 100;
            CreateDate = DateTime.Now.ToString("u");
            IsAnsiNullDefaultOn = true;
            IsAnsiNullsOn = true;
            IsAnsiPaddingOn = true;
            IsAnsiWarningsOn = true;
            IsArithabortOn = true;
            IsAutoCloseOn = false;
            IsAutoCreateStatsOn = true;
            IsAutoShrinkOn = false;
            IsAutoUpdateStatsAsyncOn = false;
            IsAutoUpdateStatsOn = true;
            IsCleanlyShutdown = true;
            IsConcatNullYieldsNullOn = false;
            IsCursorCloseOnCommitOn = false;
            IsDateCorrelationOn = false;
            IsDbChainingOn = false;
            IsFulltextEnabled = false;
            IsInStandby = false;
            IsLocalCursorDefault = false;
            IsMasterKeyEncryptedByServer = false;
            IsNumericRoundabortOn = false;
            IsParameterizationForced = false;
            IsQuotedIdentifierOn = true;
            IsReadOnly = false;
            IsRecursiveTriggersOn = false;
            IsSupplementalLoggingEnabled = false;
            IsTrustworthyOn = true;
            PageVerifyOption = 2;
            PageVerifyOptionDescription = "CHECKSUM";
            RecoveryModel = 3;
            RecoveryModelDescription = "SIMPLE";
            State = 0;
            StateDescription = "ONLINE";
            UserAccess = 0;
            UserAccessDescription = "MULTI_USER";
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
        }

        public static void AddSchema(Catalog catalog, Schema schema)
        {
            if (schema.Catalog != null && !schema.Catalog.Equals(catalog))
                RemoveSchema(schema.Catalog, schema);

            catalog.Schemas.Add(schema);
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

        public static long ObjectCount(Catalog catalog, bool deepCount = false)
        {
            if (!deepCount)
                return catalog.Schemas.Count;

            return catalog.Schemas.Count +
                   catalog.Schemas.Sum(
                       schema => Schema.ObjectCount(schema, true));
        }

        public static bool UsesCaseSensitiveCollation(Catalog catalog)
        {
            // TODO: Find reason for needing!
            return catalog.CollationName.IndexOf("_CS_", StringComparison.OrdinalIgnoreCase) >= 0;
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
    }
}
