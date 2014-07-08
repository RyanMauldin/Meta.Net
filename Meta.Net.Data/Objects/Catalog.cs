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
    public class Catalog : IDataObject
    {
        #region Properties (44)

        public string CollationName { get; set; }

        public int CompatibilityLevel { get; set; }

        public string CreateDate { get; set; }

        public string Description { get; set; }

        public Dictionary<string, ForeignKey> ForeignKeyPool { get; private set; }

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

        public string Namespace
        {
            get
            {
                return ObjectName;
            }
        }

        public string ObjectName
        {
            get
            {
                return _objectName;
            }

            set
            {
                if (Server != null)
                {
                    if (Server.RenameCatalog(Server, _objectName, value))
                        _objectName = value;
                }
                else
                {
                    if (string.IsNullOrEmpty(value))
                        return;

                    _objectName = value;
                }
            }
        }

        public int PageVerifyOption { get; set; }

        public string PageVerifyOptionDescription { get; set; }

        public int RecoveryModel { get; set; }

        public string RecoveryModelDescription { get; set; }

        public Dictionary<string, List<ForeignKey>> ReferencedUserTablePool { get; private set; }

        public Dictionary<string, Schema> Schemas { get; private set; }

        public Server Server { get; set; }

        public int State { get; set; }

        public string StateDescription { get; set; }

        public int UserAccess { get; set; }

        public string UserAccessDescription { get; set; }

        #endregion Properties

        #region Fields (1)

        [NonSerialized]
        private string _objectName;

        #endregion Fields

        #region Constructors (5)

        /// <summary>
        /// Deserialization Constructor.
        /// Makes a call to Schema.LinkForeignKeys(schema) internally after each schema
        /// in the catalog being deserialized has deserialized underlying schemas to
        /// and automatically restores the catalog properties Catalog.ForeignKeyPool
        /// and Catalog.ReferencedUserTablePool.
        /// </summary>
        /// <param name="info">The serialization information.</param>
        /// <param name="context">The streaming context.</param>
        public Catalog(SerializationInfo info, StreamingContext context)
        {
            // Set Null Members
            Server = null;

            // Deserialize Members
            ObjectName = info.GetString("ObjectName");
            Description = info.GetString("Description");
            CollationName = info.GetString("CollationName");
            CompatibilityLevel = info.GetInt32("CompatibilityLevel");
            CreateDate = info.GetString("CreateDate");
            IsAnsiNullDefaultOn = info.GetBoolean("IsAnsiNullDefaultOn");
            IsAnsiNullsOn = info.GetBoolean("IsAnsiNullsOn");
            IsAnsiPaddingOn = info.GetBoolean("IsAnsiPaddingOn");
            IsAnsiWarningsOn = info.GetBoolean("IsAnsiWarningsOn");
            IsArithabortOn = info.GetBoolean("IsArithabortOn");
            IsAutoCloseOn = info.GetBoolean("IsAutoCloseOn");
            IsAutoCreateStatsOn = info.GetBoolean("IsAutoCreateStatsOn");
            IsAutoShrinkOn = info.GetBoolean("IsAutoShrinkOn");
            IsAutoUpdateStatsAsyncOn = info.GetBoolean("IsAutoUpdateStatsAsyncOn");
            IsAutoUpdateStatsOn = info.GetBoolean("IsAutoUpdateStatsOn");
            IsCleanlyShutdown = info.GetBoolean("IsCleanlyShutdown");
            IsConcatNullYieldsNullOn = info.GetBoolean("IsConcatNullYieldsNullOn");
            IsCursorCloseOnCommitOn = info.GetBoolean("IsCursorCloseOnCommitOn");
            IsDateCorrelationOn = info.GetBoolean("IsDateCorrelationOn");
            IsDbChainingOn = info.GetBoolean("IsDbChainingOn");
            IsFulltextEnabled = info.GetBoolean("IsFulltextEnabled");
            IsInStandby = info.GetBoolean("IsInStandby");
            IsLocalCursorDefault = info.GetBoolean("IsLocalCursorDefault");
            IsMasterKeyEncryptedByServer = info.GetBoolean("IsMasterKeyEncryptedByServer");
            IsNumericRoundabortOn = info.GetBoolean("IsNumericRoundabortOn");
            IsParameterizationForced = info.GetBoolean("IsParameterizationForced");
            IsQuotedIdentifierOn = info.GetBoolean("IsQuotedIdentifierOn");
            IsReadOnly = info.GetBoolean("IsReadOnly");
            IsRecursiveTriggersOn = info.GetBoolean("IsRecursiveTriggersOn");
            IsSupplementalLoggingEnabled = info.GetBoolean("IsSupplementalLoggingEnabled");
            IsTrustworthyOn = info.GetBoolean("IsTrustworthyOn");
            PageVerifyOption = info.GetInt32("PageVerifyOption");
            PageVerifyOptionDescription = info.GetString("PageVerifyOptionDescription");
            RecoveryModel = info.GetInt32("RecoveryModel");
            RecoveryModelDescription = info.GetString("RecoveryModelDescription");
            State = info.GetInt32("State");
            StateDescription = info.GetString("StateDescription");
            UserAccess = info.GetInt32("UserAccess");
            UserAccessDescription = info.GetString("UserAccessDescription");

            // Deserialize Schemas
            var schemas = info.GetInt32("Schemas");
            Schemas = new Dictionary<string, Schema>(StringComparer.OrdinalIgnoreCase);

            for (var i = 0; i < schemas; i++)
            {
                var schema = (Schema)info.GetValue("Schema" + i, typeof(Schema));
                schema.Catalog = this;
                Schemas.Add(schema.ObjectName, schema);
                // TODO
                //Schema.LinkForeignKeys(schema, dataContext);
            }
        }

        public Catalog(Server server, CatalogsRow catalogsRow)
        {
            Init(this, server, catalogsRow.ObjectName, catalogsRow);
        }

        public Catalog(Server server, string objectName)
        {
            Init(this, server, objectName, null);
        }

        public Catalog(string objectName)
        {
            Init(this, null, objectName, null);
        }

        public Catalog()
        {
            Init(this, null, null, null);
        }

        #endregion Constructors

        #region Methods (24)

        #region Public Methods (23)

        public static bool AddSchema(Catalog catalog, Schema schema)
        {
            if (catalog.Schemas.ContainsKey(schema.ObjectName))
                return false;

            if (schema.Catalog == null)
            {
                schema.Catalog = catalog;
                catalog.Schemas.Add(schema.ObjectName, schema);
                return true;
            }

            if (schema.Catalog.Equals(catalog))
            {
                catalog.Schemas.Add(schema.ObjectName, schema);
                return true;
            }

            return false;
        }

        public static bool AddSchema(Catalog catalog, string objectName)
        {
            if (string.IsNullOrEmpty(objectName))
                return false;

            if (catalog.Schemas.ContainsKey(objectName))
                return false;

            var schema = new Schema(catalog, objectName);
            catalog.Schemas.Add(objectName, schema);

            return true;
        }

        /// <summary>
        /// Deep Clear... disassociates all objects in each schema
        /// first before clearing out the collections in hopes of better
        /// memory dissipation from garbage collection.
        /// </summary>
        /// <param name="catalog">The catalog to deep clear.</param>
        public static void Clear(Catalog catalog)
        {
            foreach (var schema in catalog.Schemas.Values)
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
            var catalogClone = new Catalog(catalog.ObjectName)
            {
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

            foreach (var schemaClone in catalog.Schemas.Values.Select(Schema.Clone))
            {
                AddSchema(catalogClone, schemaClone);
                Schema.LinkForeignKeys(schemaClone);
            }


            return catalogClone;
        }

        public static bool CompareDefinitions(Catalog sourceCatalog, Catalog targetCatalog)
        {
            return CompareObjectNames(sourceCatalog, targetCatalog);
        }

        public static bool CompareMatchedCatalog(DataContext sourceDataContext, DataContext targetDataContext,
            Catalog matchedCatalog, Catalog sourceCatalog, Catalog targetCatalog, Catalog alteredCatalog)
        {
            var globalCompareState = true;

            foreach (var matchedSchema in matchedCatalog.Schemas.Values)
            {
                Schema sourceSchema;
                Schema targetSchema;

                sourceCatalog.Schemas.TryGetValue(matchedSchema.ObjectName, out sourceSchema);
                targetCatalog.Schemas.TryGetValue(matchedSchema.ObjectName, out targetSchema);

                if (sourceSchema == null || targetSchema == null)
                    throw new Exception(string.Format("Source and/or target schemas did not exist for the matching schema {0} during Catalog.CompareMatchedCatalog() method.", matchedSchema.Namespace));

                var alteredSchema = Schema.ShallowClone(sourceSchema);
                if (!AddSchema(alteredCatalog, alteredSchema))
                    throw new Exception(string.Format("Unable to add alteredSchema {0} to alteredCatalog in Catalog.CompareMatchedCatalog() as it may already exist and should not.", sourceSchema.Namespace));

                if (!Schema.CompareMatchedSchema(sourceDataContext, targetDataContext, matchedSchema, sourceSchema, targetSchema, alteredSchema))
                    globalCompareState = false;

                if (Schema.ObjectCount(alteredSchema) == 0 && !RemoveSchema(alteredCatalog, alteredSchema))
                        throw new Exception(string.Format("Unable to remove alteredSchema {0} for alteredSchema in Catalog.CompareMatchedCatalog().", sourceSchema.Namespace));
            }

            return globalCompareState;
        }

        public static bool CompareObjectNames(Catalog sourceCatalog, Catalog targetCatalog, StringComparison stringComparison = StringComparison.OrdinalIgnoreCase)
        {
            switch (stringComparison)
            {
                case StringComparison.CurrentCulture:
                    return StringComparer.CurrentCulture.Compare(
                        sourceCatalog.ObjectName, targetCatalog.ObjectName) == 0;
                case StringComparison.CurrentCultureIgnoreCase:
                    return StringComparer.CurrentCultureIgnoreCase.Compare(
                        sourceCatalog.ObjectName, targetCatalog.ObjectName) == 0;
                case StringComparison.InvariantCulture:
                    return StringComparer.InvariantCulture.Compare(
                        sourceCatalog.ObjectName, targetCatalog.ObjectName) == 0;
                case StringComparison.InvariantCultureIgnoreCase:
                    return StringComparer.InvariantCultureIgnoreCase.Compare(
                        sourceCatalog.ObjectName, targetCatalog.ObjectName) == 0;
                case StringComparison.Ordinal:
                    return StringComparer.Ordinal.Compare(
                        sourceCatalog.ObjectName, targetCatalog.ObjectName) == 0;
                case StringComparison.OrdinalIgnoreCase:
                    return StringComparer.OrdinalIgnoreCase.Compare(
                        sourceCatalog.ObjectName, targetCatalog.ObjectName) == 0;
                default:
                    return StringComparer.OrdinalIgnoreCase.Compare(
                        sourceCatalog.ObjectName, targetCatalog.ObjectName) == 0;
            }
        }

        /// <summary>
        /// Removes all objects in the target Catalog from the source Catalog.
        /// </summary>
        /// <param name="sourceDataContext">The source DataContext.</param>
        /// <param name="targetDataContext">The target DataContext.</param>
        /// <param name="sourceCatalog">The source Catalog.</param>
        /// <param name="targetCatalog">The target Catalog.</param>
        /// <param name="dataComparisonType">
        /// The completeness of comparisons between matching objects.
        /// </param>
        public static void ExceptWith(DataContext sourceDataContext, DataContext targetDataContext, Catalog sourceCatalog, Catalog targetCatalog,
            DataComparisonType dataComparisonType = DataComparisonType.SchemaLevelNamespaces)
        {
            var matchingSchemas = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            matchingSchemas.UnionWith(sourceCatalog.Schemas.Keys);
            matchingSchemas.IntersectWith(targetCatalog.Schemas.Keys);

            foreach (var schema in matchingSchemas)
            {
                Schema sourceSchema;
                if (!sourceCatalog.Schemas.TryGetValue(schema, out sourceSchema))
                    continue;

                Schema targetSchema;
                if (!targetCatalog.Schemas.TryGetValue(schema, out targetSchema))
                    continue;

                switch (dataComparisonType)
                {
                    case DataComparisonType.Definitions:
                        if (Schema.CompareDefinitions(sourceSchema, targetSchema))
                        {
                            Schema.ExceptWith(sourceDataContext, targetDataContext, sourceSchema, targetSchema, dataComparisonType);
                            if (Schema.ObjectCount(sourceSchema) == 0)
                                RemoveSchema(sourceCatalog, schema);
                        }
                        break;
                    case DataComparisonType.Namespaces:
                    case DataComparisonType.SchemaLevelNamespaces:
                        if (Schema.CompareObjectNames(sourceSchema, targetSchema))
                        {
                            Schema.ExceptWith(sourceDataContext, targetDataContext, sourceSchema, targetSchema, dataComparisonType);
                            if (Schema.ObjectCount(sourceSchema) == 0)
                                RemoveSchema(sourceCatalog, schema);
                        }
                        break;
                }
            }
        }

        public static void Fill(Catalog catalog, DataGenerics generics)
        {
            Clear(catalog);

            foreach (var str in generics.Schemas)
            {
                SchemasRow schemasRow;
                if (!generics.SchemaRows.TryGetValue(str + ".", out schemasRow))
                    continue;

                var schema = new Schema(catalog, schemasRow);
                Schema.Fill(schema, generics);
                AddSchema(catalog, schema);
            }
        }

        public static Catalog FromJson(string json)
        {
            return JsonConvert.DeserializeObject<Catalog>(json);
        }

        public static void GenerateAlterScripts(DataContext sourceDataContext, DataContext targetDataContext, Catalog alteredCatalog,
            Catalog sourceCatalog, Catalog targetCatalog, Catalog droppedCatalog, Catalog createdCatalog, Catalog matchedCatalog,
            DataSyncActionsCollection dataSyncActions, DataProperties dataProperties)
        {
            var dataDependencyBuilder = new DataDependencyBuilder();

            if (droppedCatalog != null)
                dataDependencyBuilder.PreloadDroppedDependencies(droppedCatalog);

            if (createdCatalog != null)
                dataDependencyBuilder.PreloadCreatedDependencies(createdCatalog);

            foreach (var alteredSchema in alteredCatalog.Schemas.Values)
            {
                Schema sourceSchema;
                Schema targetSchema;
                Schema droppedSchema = null;
                Schema createdSchema = null;
                Schema matchedSchema = null;

                sourceCatalog.Schemas.TryGetValue(alteredSchema.ObjectName, out sourceSchema);
                targetCatalog.Schemas.TryGetValue(alteredSchema.ObjectName, out targetSchema);
                
                if (droppedCatalog != null)
                    droppedCatalog.Schemas.TryGetValue(alteredSchema.ObjectName, out droppedSchema);

                if (createdCatalog != null)
                    createdCatalog.Schemas.TryGetValue(alteredSchema.ObjectName, out createdSchema);

                if (matchedCatalog != null)
                    matchedCatalog.Schemas.TryGetValue(alteredSchema.ObjectName, out matchedSchema);

                if (sourceSchema == null || targetSchema == null)
                    throw new Exception(string.Format("Source and/or target schemas did not exist for the altered schema {0} during Catalog.GenerateAlterScripts() method.", alteredSchema.Namespace));

                Schema.GenerateAlterScripts(sourceDataContext, targetDataContext, alteredSchema, sourceSchema, targetSchema,
                    droppedSchema, createdSchema, matchedSchema, dataSyncActions, dataDependencyBuilder, dataProperties);
            }
        }

        public static void GenerateCreateScripts(DataContext sourceDataContext, DataContext targetDataContext, Catalog createdCatalog,
            Catalog sourceCatalog, Catalog targetCatalog, DataSyncActionsCollection dataSyncActions, DataProperties dataProperties)
        {
            if (!DataProperties.NonRemovableCatalogs.Contains(createdCatalog.Namespace))
                if (sourceCatalog != null && targetCatalog == null)
                {
                    var dataSyncAction = DataActionFactory.CreateCatalog(sourceDataContext, targetDataContext, createdCatalog);
                    if (dataSyncAction != null)
                        dataSyncActions.Add(dataSyncAction);
                }

            foreach (var createdSchema in createdCatalog.Schemas.Values)
            {
                Schema sourceSchema = null;
                Schema targetSchema = null;

                if (sourceCatalog != null)
                    sourceCatalog.Schemas.TryGetValue(createdSchema.ObjectName, out sourceSchema);

                if (targetCatalog != null)
                    targetCatalog.Schemas.TryGetValue(createdSchema.ObjectName, out targetSchema);

                Schema.GenerateCreateScripts(sourceDataContext, targetDataContext, createdSchema, sourceSchema,
                    targetSchema, dataSyncActions, dataProperties);
            }
        }

        public static void GenerateDropScripts(DataContext sourceDataContext, DataContext targetDataContext, Catalog droppedCatalog,
            Catalog sourceCatalog, Catalog targetCatalog, DataSyncActionsCollection dataSyncActions, DataProperties dataProperties)
        {
            if (ObjectCount(droppedCatalog) == 0 || !dataProperties.TightSync)
                return;

            if (!DataProperties.NonRemovableCatalogs.Contains(droppedCatalog.Namespace))
                if (sourceCatalog == null && targetCatalog != null && dataProperties.TightSync)
                {
                    var dataSyncAction = DataActionFactory.DropCatalog(sourceDataContext, targetDataContext, droppedCatalog);
                    if (dataSyncAction != null)
                        dataSyncActions.Add(dataSyncAction);
                }

            foreach (var droppedSchema in droppedCatalog.Schemas.Values)
            {
                Schema sourceSchema = null;
                Schema targetSchema = null;

                if (sourceCatalog != null)
                    sourceCatalog.Schemas.TryGetValue(droppedSchema.ObjectName, out sourceSchema);

                if (targetCatalog != null)
                    targetCatalog.Schemas.TryGetValue(droppedSchema.ObjectName, out targetSchema);

                Schema.GenerateDropScripts(sourceDataContext, targetDataContext, droppedSchema, sourceSchema,
                    targetSchema, dataSyncActions, dataProperties);
            }
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            // Serialize Members
            info.AddValue("ObjectName", ObjectName);
            info.AddValue("Description", Description);
            info.AddValue("CollationName", CollationName);
            info.AddValue("CompatibilityLevel", CompatibilityLevel);
            info.AddValue("CreateDate", CreateDate);
            info.AddValue("IsAnsiNullDefaultOn", IsAnsiNullDefaultOn);
            info.AddValue("IsAnsiNullsOn", IsAnsiNullsOn);
            info.AddValue("IsAnsiPaddingOn", IsAnsiPaddingOn);
            info.AddValue("IsAnsiWarningsOn", IsAnsiWarningsOn);
            info.AddValue("IsArithabortOn", IsArithabortOn);
            info.AddValue("IsAutoCloseOn", IsAutoCloseOn);
            info.AddValue("IsAutoCreateStatsOn", IsAutoCreateStatsOn);
            info.AddValue("IsAutoShrinkOn", IsAutoShrinkOn);
            info.AddValue("IsAutoUpdateStatsAsyncOn", IsAutoUpdateStatsAsyncOn);
            info.AddValue("IsAutoUpdateStatsOn", IsAutoUpdateStatsOn);
            info.AddValue("IsCleanlyShutdown", IsCleanlyShutdown);
            info.AddValue("IsConcatNullYieldsNullOn", IsConcatNullYieldsNullOn);
            info.AddValue("IsCursorCloseOnCommitOn", IsCursorCloseOnCommitOn);
            info.AddValue("IsDateCorrelationOn", IsDateCorrelationOn);
            info.AddValue("IsDbChainingOn", IsDbChainingOn);
            info.AddValue("IsFulltextEnabled", IsFulltextEnabled);
            info.AddValue("IsInStandby", IsInStandby);
            info.AddValue("IsLocalCursorDefault", IsLocalCursorDefault);
            info.AddValue("IsMasterKeyEncryptedByServer", IsMasterKeyEncryptedByServer);
            info.AddValue("IsNumericRoundabortOn", IsNumericRoundabortOn);
            info.AddValue("IsParameterizationForced", IsParameterizationForced);
            info.AddValue("IsQuotedIdentifierOn", IsQuotedIdentifierOn);
            info.AddValue("IsReadOnly", IsReadOnly);
            info.AddValue("IsRecursiveTriggersOn", IsRecursiveTriggersOn);
            info.AddValue("IsSupplementalLoggingEnabled", IsSupplementalLoggingEnabled);
            info.AddValue("IsTrustworthyOn", IsTrustworthyOn);
            info.AddValue("PageVerifyOption", PageVerifyOption);
            info.AddValue("PageVerifyOptionDescription", PageVerifyOptionDescription);
            info.AddValue("RecoveryModel", RecoveryModel);
            info.AddValue("RecoveryModelDescription", RecoveryModelDescription);
            info.AddValue("State", State);
            info.AddValue("StateDescription", StateDescription);
            info.AddValue("UserAccess", UserAccess);
            info.AddValue("UserAccessDescription", UserAccessDescription);

            // Serialize Schemas
            info.AddValue("Schemas", Schemas.Count);

            var i = 0;
            foreach (var schema in Schemas.Values)
                info.AddValue("Schema" + i++, schema);
        }

        /// <summary>
        /// Modifies the source Catalog to contain only objects that are
        /// present in the source Catalog and in the target Catalog.
        /// </summary>
        /// <param name="sourceDataContext">The source DataContext.</param>
        /// <param name="targetDataContext">The target DataContext.</param>
        /// <param name="sourceCatalog">The source Catalog.</param>
        /// <param name="targetCatalog">The target Catalog.</param>
        /// <param name="dataComparisonType">
        /// The completeness of comparisons between matching objects.
        /// </param>
        public static void IntersectWith(DataContext sourceDataContext, DataContext targetDataContext, Catalog sourceCatalog,
            Catalog targetCatalog, DataComparisonType dataComparisonType = DataComparisonType.SchemaLevelNamespaces)
        {
            var matchingSchemas = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            var removableSchemas = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            matchingSchemas.UnionWith(sourceCatalog.Schemas.Keys);
            matchingSchemas.IntersectWith(targetCatalog.Schemas.Keys);

            removableSchemas.UnionWith(sourceCatalog.Schemas.Keys);
            removableSchemas.ExceptWith(matchingSchemas);

            foreach (var schema in removableSchemas)
                RemoveSchema(sourceCatalog, schema);

            foreach (var schema in matchingSchemas)
            {
                Schema sourceSchema;
                if (!sourceCatalog.Schemas.TryGetValue(schema, out sourceSchema))
                    continue;

                Schema targetSchema;
                if (!targetCatalog.Schemas.TryGetValue(schema, out targetSchema))
                    continue;

                switch (dataComparisonType)
                {
                    case DataComparisonType.Definitions:
                        if (Schema.CompareDefinitions(sourceSchema, targetSchema))
                            Schema.IntersectWith(sourceDataContext, targetDataContext, sourceSchema, targetSchema, dataComparisonType);
                        break;
                    case DataComparisonType.Namespaces:
                    case DataComparisonType.SchemaLevelNamespaces:
                        if (Schema.CompareObjectNames(sourceSchema, targetSchema))
                            Schema.IntersectWith(sourceDataContext, targetDataContext, sourceSchema, targetSchema, dataComparisonType);
                        break;
                }
            }
        }

        public static long ObjectCount(Catalog catalog, bool deepCount = false)
        {
            if (!deepCount)
                return catalog.Schemas.Count;

            return catalog.Schemas.Count +
                   catalog.Schemas.Values.Sum(
                       schema => Schema.ObjectCount(schema, true));
        }

        public static bool RemoveSchema(Catalog catalog, string objectName)
        {
            return !string.IsNullOrEmpty(objectName)
                && catalog.Schemas.Remove(objectName);
        }

        public static bool RemoveSchema(Catalog catalog, Schema schema)
        {
            Schema schemaObject;
            if (!catalog.Schemas.TryGetValue(schema.ObjectName, out schemaObject))
                return false;

            return schema.Equals(schemaObject)
                && catalog.Schemas.Remove(schema.ObjectName);
        }

        public static bool RenameSchema(Catalog catalog, string objectName, string newObjectName)
        {
            if (string.IsNullOrEmpty(objectName))
                return false;

            if (string.IsNullOrEmpty(newObjectName))
                return false;

            if (catalog.Schemas.ContainsKey(newObjectName))
                return false;

            Schema schema;
            if (!catalog.Schemas.TryGetValue(objectName, out schema))
                return false;

            catalog.Schemas.Remove(objectName);
            schema.Catalog = null;
            schema.ObjectName = newObjectName;
            schema.Catalog = catalog;
            catalog.Schemas.Add(newObjectName, schema);

            return true;
        }

        /// <summary>
        /// Shallow Clone...
        /// A clone of this class's isntance specific metadata.
        /// </summary>
        /// <param name="catalog">The catalog to shallow clone.</param>
        /// <returns>A clone of this class's isntance specific metadata.</returns>
        public static Catalog ShallowClone(Catalog catalog)
        {
            return new Catalog(catalog.ObjectName)
            {
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

        public static string ToJson(Catalog catalog, Formatting formatting = Formatting.Indented)
        {
            return JsonConvert.SerializeObject(catalog, formatting);
        }

        /// <summary>
        /// Modifies the source Catalog to contain all objects that are
        /// present in both iteself and in the target Catalog.
        /// </summary>
        /// <param name="sourceDataContext">The source DataContext.</param>
        /// <param name="targetDataContext">The target DataContext.</param>
        /// <param name="sourceCatalog">The source Catalog.</param>
        /// <param name="targetCatalog">The target Catalog.</param>
        /// <param name="dataComparisonType">
        /// The completeness of comparisons between matching objects.
        /// </param>
        public static void UnionWith(DataContext sourceDataContext, DataContext targetDataContext, Catalog sourceCatalog,
            Catalog targetCatalog, DataComparisonType dataComparisonType = DataComparisonType.SchemaLevelNamespaces)
        {
            var matchingSchemas = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            var addableSchemas = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            matchingSchemas.UnionWith(sourceCatalog.Schemas.Keys);
            matchingSchemas.IntersectWith(targetCatalog.Schemas.Keys);

            addableSchemas.UnionWith(targetCatalog.Schemas.Keys);
            addableSchemas.ExceptWith(matchingSchemas);

            foreach (var schema in addableSchemas)
            {
                Schema targetSchema;
                if (!targetCatalog.Schemas.TryGetValue(schema, out targetSchema))
                    continue;

                AddSchema(sourceCatalog, Schema.Clone(targetSchema));
            }

            foreach (var schema in matchingSchemas)
            {
                Schema sourceSchema;
                if (!sourceCatalog.Schemas.TryGetValue(schema, out sourceSchema))
                    continue;

                Schema targetSchema;
                if (!targetCatalog.Schemas.TryGetValue(schema, out targetSchema))
                    continue;

                switch (dataComparisonType)
                {
                    case DataComparisonType.Definitions:
                        if (Schema.CompareDefinitions(sourceSchema, targetSchema))
                            Schema.UnionWith(sourceDataContext, targetDataContext, sourceSchema, targetSchema, dataComparisonType);
                        break;
                    case DataComparisonType.Namespaces:
                    case DataComparisonType.SchemaLevelNamespaces:
                        if (Schema.CompareObjectNames(sourceSchema, targetSchema))
                            Schema.UnionWith(sourceDataContext, targetDataContext, sourceSchema, targetSchema, dataComparisonType);
                        break;
                }
            }
        }

        public static bool UsesCaseSensitiveCollation(Catalog catalog)
        {
            // TODO: Find reason for needing!
            return catalog.CollationName.IndexOf("_CS_", StringComparison.OrdinalIgnoreCase) >= 0;
        }

        #endregion Public Methods
        #region Private Methods (1)

        private static void Init(Catalog catalog, Server server, string objectName, CatalogsRow catalogsRow)
        {
            catalog.ForeignKeyPool = new Dictionary<string, ForeignKey>(StringComparer.OrdinalIgnoreCase);
            catalog.ReferencedUserTablePool = new Dictionary<string, List<ForeignKey>>(StringComparer.OrdinalIgnoreCase);
            catalog.Schemas = new Dictionary<string, Schema>(StringComparer.OrdinalIgnoreCase);
            catalog.Server = server;
            catalog._objectName = string.IsNullOrEmpty(objectName)
                ? "Default"
                : objectName;

            catalog.Description = "Catalog";

            if (catalogsRow == null)
            {
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
                return;
            }

            catalog.CollationName = catalogsRow.CollationName;
            catalog.CompatibilityLevel = catalogsRow.CompatibilityLevel;
            catalog.CreateDate = catalogsRow.CreateDate;
            catalog.IsAnsiNullDefaultOn = catalogsRow.IsAnsiNullDefaultOn;
            catalog.IsAnsiNullsOn = catalogsRow.IsAnsiNullsOn;
            catalog.IsAnsiPaddingOn = catalogsRow.IsAnsiPaddingOn;
            catalog.IsAnsiWarningsOn = catalogsRow.IsAnsiWarningsOn;
            catalog.IsArithabortOn = catalogsRow.IsArithabortOn;
            catalog.IsAutoCloseOn = catalogsRow.IsAutoCloseOn;
            catalog.IsAutoCreateStatsOn = catalogsRow.IsAutoCreateStatsOn;
            catalog.IsAutoShrinkOn = catalogsRow.IsAutoShrinkOn;
            catalog.IsAutoUpdateStatsAsyncOn = catalogsRow.IsAutoUpdateStatsAsyncOn;
            catalog.IsAutoUpdateStatsOn = catalogsRow.IsAutoUpdateStatsOn;
            catalog.IsCleanlyShutdown = catalogsRow.IsCleanlyShutdown;
            catalog.IsConcatNullYieldsNullOn = catalogsRow.IsConcatNullYieldsNullOn;
            catalog.IsCursorCloseOnCommitOn = catalogsRow.IsCursorCloseOnCommitOn;
            catalog.IsDateCorrelationOn = catalogsRow.IsDateCorrelationOn;
            catalog.IsDbChainingOn = catalogsRow.IsDbChainingOn;
            catalog.IsFulltextEnabled = catalogsRow.IsFulltextEnabled;
            catalog.IsInStandby = catalogsRow.IsInStandby;
            catalog.IsLocalCursorDefault = catalogsRow.IsLocalCursorDefault;
            catalog.IsMasterKeyEncryptedByServer = catalogsRow.IsMasterKeyEncryptedByServer;
            catalog.IsNumericRoundabortOn = catalogsRow.IsNumericRoundabortOn;
            catalog.IsParameterizationForced = catalogsRow.IsParameterizationForced;
            catalog.IsQuotedIdentifierOn = catalogsRow.IsQuotedIdentifierOn;
            catalog.IsReadOnly = catalogsRow.IsReadOnly;
            catalog.IsRecursiveTriggersOn = catalogsRow.IsRecursiveTriggersOn;
            catalog.IsSupplementalLoggingEnabled = catalogsRow.IsSupplementalLoggingEnabled;
            catalog.IsTrustworthyOn = catalogsRow.IsTrustworthyOn;
            catalog.PageVerifyOption = catalogsRow.PageVerifyOption;
            catalog.PageVerifyOptionDescription = catalogsRow.PageVerifyOptionDescription;
            catalog.RecoveryModel = catalogsRow.RecoveryModel;
            catalog.RecoveryModelDescription = catalogsRow.RecoveryModelDescription;
            catalog.State = catalogsRow.State;
            catalog.StateDescription = catalogsRow.StateDescription;
            catalog.UserAccess = catalogsRow.UserAccess;
            catalog.UserAccessDescription = catalogsRow.UserAccessDescription;
        }

        #endregion Private Methods

        #endregion Methods
    }
}
