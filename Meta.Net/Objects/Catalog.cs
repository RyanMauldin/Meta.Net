using System;
using System.Linq;
using Meta.Net.Abstract;
using Meta.Net.Interfaces;

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

        public override IMetaObject DeepClone()
        {
            var catalog = new Catalog
            {
                ObjectName = ObjectName,
                CollationName = CollationName,
                CompatibilityLevel = CompatibilityLevel,
                CreateDate = CreateDate,
                IsAnsiNullDefaultOn = IsAnsiNullDefaultOn,
                IsAnsiNullsOn = IsAnsiNullsOn,
                IsAnsiPaddingOn = IsAnsiPaddingOn,
                IsAnsiWarningsOn = IsAnsiWarningsOn,
                IsArithabortOn = IsArithabortOn,
                IsAutoCloseOn = IsAutoCloseOn,
                IsAutoCreateStatsOn = IsAutoCreateStatsOn,
                IsAutoShrinkOn = IsAutoShrinkOn,
                IsAutoUpdateStatsAsyncOn = IsAutoUpdateStatsAsyncOn,
                IsAutoUpdateStatsOn = IsAutoUpdateStatsOn,
                IsCleanlyShutdown = IsCleanlyShutdown,
                IsConcatNullYieldsNullOn = IsConcatNullYieldsNullOn,
                IsCursorCloseOnCommitOn = IsCursorCloseOnCommitOn,
                IsDateCorrelationOn = IsDateCorrelationOn,
                IsDbChainingOn = IsDbChainingOn,
                IsFulltextEnabled = IsFulltextEnabled,
                IsInStandby = IsInStandby,
                IsLocalCursorDefault = IsLocalCursorDefault,
                IsMasterKeyEncryptedByServer = IsMasterKeyEncryptedByServer,
                IsNumericRoundabortOn = IsNumericRoundabortOn,
                IsParameterizationForced = IsParameterizationForced,
                IsQuotedIdentifierOn = IsQuotedIdentifierOn,
                IsReadOnly = IsReadOnly,
                IsRecursiveTriggersOn = IsRecursiveTriggersOn,
                IsSupplementalLoggingEnabled = IsSupplementalLoggingEnabled,
                IsTrustworthyOn = IsTrustworthyOn,
                PageVerifyOption = PageVerifyOption,
                PageVerifyOptionDescription = PageVerifyOptionDescription,
                RecoveryModel = RecoveryModel,
                RecoveryModelDescription = RecoveryModelDescription,
                State = State,
                StateDescription = StateDescription,
                UserAccess = UserAccess,
                UserAccessDescription = UserAccessDescription
            };

            catalog.Schemas.DeepClone(catalog);

            return catalog;
        }

        public override IMetaObject ShallowClone()
        {
            return new Catalog
            {
                ObjectName = ObjectName,
                CollationName = CollationName,
                CompatibilityLevel = CompatibilityLevel,
                CreateDate = CreateDate,
                IsAnsiNullDefaultOn = IsAnsiNullDefaultOn,
                IsAnsiNullsOn = IsAnsiNullsOn,
                IsAnsiPaddingOn = IsAnsiPaddingOn,
                IsAnsiWarningsOn = IsAnsiWarningsOn,
                IsArithabortOn = IsArithabortOn,
                IsAutoCloseOn = IsAutoCloseOn,
                IsAutoCreateStatsOn = IsAutoCreateStatsOn,
                IsAutoShrinkOn = IsAutoShrinkOn,
                IsAutoUpdateStatsAsyncOn = IsAutoUpdateStatsAsyncOn,
                IsAutoUpdateStatsOn = IsAutoUpdateStatsOn,
                IsCleanlyShutdown = IsCleanlyShutdown,
                IsConcatNullYieldsNullOn = IsConcatNullYieldsNullOn,
                IsCursorCloseOnCommitOn = IsCursorCloseOnCommitOn,
                IsDateCorrelationOn = IsDateCorrelationOn,
                IsDbChainingOn = IsDbChainingOn,
                IsFulltextEnabled = IsFulltextEnabled,
                IsInStandby = IsInStandby,
                IsLocalCursorDefault = IsLocalCursorDefault,
                IsMasterKeyEncryptedByServer = IsMasterKeyEncryptedByServer,
                IsNumericRoundabortOn = IsNumericRoundabortOn,
                IsParameterizationForced = IsParameterizationForced,
                IsQuotedIdentifierOn = IsQuotedIdentifierOn,
                IsReadOnly = IsReadOnly,
                IsRecursiveTriggersOn = IsRecursiveTriggersOn,
                IsSupplementalLoggingEnabled = IsSupplementalLoggingEnabled,
                IsTrustworthyOn = IsTrustworthyOn,
                PageVerifyOption = PageVerifyOption,
                PageVerifyOptionDescription = PageVerifyOptionDescription,
                RecoveryModel = RecoveryModel,
                RecoveryModelDescription = RecoveryModelDescription,
                State = State,
                StateDescription = StateDescription,
                UserAccess = UserAccess,
                UserAccessDescription = UserAccessDescription
            };
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
