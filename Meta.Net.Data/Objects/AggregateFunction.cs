using System;
using System.Runtime.Serialization;
using Meta.Net.Data.Interfaces;
using Meta.Net.Data.Metadata;
using Newtonsoft.Json;

namespace Meta.Net.Data.Objects
{
    [Serializable]
    public class AggregateFunction : IDataObject, IDataModule
    {
        #region Properties (8)

        public Catalog Catalog
        {
            get
            {
                var schema = Schema;
                return schema == null
                    ? null
                    : schema.Catalog;
            }
        }

        public string Definition { get; set; }

        public string Description { get; set; }

        public string Namespace
        {
            get
            {
                if (Schema == null)
                    return ObjectName;

                return Schema.ObjectName + "." + ObjectName;

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
                if (Schema != null)
                {
                    if (Schema.RenameAggregateFunction(Schema, _objectName, value))
                    {
                        _objectName = value;
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(value)) return;
                    _objectName = value;
                }
            }
        }

        public Schema Schema { get; set; }

        public bool UsesAnsiNulls { get; set; }

        public bool UsesQuotedIdentifier { get; set; }

        #endregion Properties

        #region Fields (1)

        [NonSerialized]
        private string _objectName;

        #endregion Fields

        #region Constructors (5)

        public AggregateFunction(SerializationInfo info, StreamingContext context)
        {
            // Set Null Members
            Schema = null;

            // Deserialize Members
            ObjectName = info.GetString("ObjectName");
            Definition = info.GetString("Definition");
            Description = info.GetString("Description");
            UsesAnsiNulls = info.GetBoolean("UsesAnsiNulls");
            UsesQuotedIdentifier = info.GetBoolean("UsesQuotedIdentifier");
        }

        public AggregateFunction(Schema schema, ModulesRow modulesRow)
        {
            Init(this, schema, modulesRow.ObjectName, modulesRow);
        }

        public AggregateFunction(Schema schema, string objectName)
        {
            Init(this, schema, objectName, null);
        }

        public AggregateFunction(string objectName)
        {
            Init(this, null, objectName, null);
        }

        public AggregateFunction()
        {
            Init(this, null, null, null);
        }

        #endregion Constructors

        #region Methods (10)

        #region Public Methods (9)

        /// <summary>
        /// Deep Clone and Shallow Clone... Leaf Node.
        /// A clone of this class's isntance specific metadata.
        /// </summary>
        /// <param name="aggregateFunction">The aggregate function to clone.</param>
        /// <returns>A clone of this class's isntance specific metadata.</returns>
        public static AggregateFunction Clone(AggregateFunction aggregateFunction)
        {
            return new AggregateFunction(aggregateFunction.ObjectName)
                {
                    Definition = aggregateFunction.Definition,
                    UsesAnsiNulls = aggregateFunction.UsesAnsiNulls,
                    UsesQuotedIdentifier = aggregateFunction.UsesQuotedIdentifier
                };
        }

        public static bool CompareDefinitions(AggregateFunction sourceAggregateFunction, AggregateFunction targetAggregateFunction)
        {
            if (!CompareObjectNames(sourceAggregateFunction, targetAggregateFunction))
                return false;

            if (sourceAggregateFunction.UsesAnsiNulls != targetAggregateFunction.UsesAnsiNulls)
                return false;

            if (sourceAggregateFunction.UsesQuotedIdentifier != targetAggregateFunction.UsesQuotedIdentifier)
                return false;

            return DataProperties.DefinitionComparer.Compare(
                sourceAggregateFunction.Definition, targetAggregateFunction.Definition) == 0;
        }

        public static bool CompareObjectNames(AggregateFunction sourceAggregateFunction, AggregateFunction targetAggregateFunction,
            StringComparison stringComparison = StringComparison.OrdinalIgnoreCase)
        {
            switch (stringComparison)
            {
                case StringComparison.CurrentCulture:
                    return StringComparer.CurrentCulture.Compare(
                        sourceAggregateFunction.ObjectName, targetAggregateFunction.ObjectName) == 0;
                case StringComparison.CurrentCultureIgnoreCase:
                    return StringComparer.CurrentCultureIgnoreCase.Compare(
                        sourceAggregateFunction.ObjectName, targetAggregateFunction.ObjectName) == 0;
                case StringComparison.InvariantCulture:
                    return StringComparer.InvariantCulture.Compare(
                        sourceAggregateFunction.ObjectName, targetAggregateFunction.ObjectName) == 0;
                case StringComparison.InvariantCultureIgnoreCase:
                    return StringComparer.InvariantCultureIgnoreCase.Compare(
                        sourceAggregateFunction.ObjectName, targetAggregateFunction.ObjectName) == 0;
                case StringComparison.Ordinal:
                    return StringComparer.Ordinal.Compare(
                        sourceAggregateFunction.ObjectName, targetAggregateFunction.ObjectName) == 0;
                case StringComparison.OrdinalIgnoreCase:
                    return StringComparer.OrdinalIgnoreCase.Compare(
                        sourceAggregateFunction.ObjectName, targetAggregateFunction.ObjectName) == 0;
                default:
                    return StringComparer.OrdinalIgnoreCase.Compare(
                        sourceAggregateFunction.ObjectName, targetAggregateFunction.ObjectName) == 0;
            }
        }

        public static AggregateFunction FromJson(string json)
        {
            return JsonConvert.DeserializeObject<AggregateFunction>(json);
        }

        public static void GenerateAlterScripts(
            DataContext sourceDataContext, DataContext targetDataContext,
            AggregateFunction aggregateFunction, DataSyncActionsCollection dataSyncActions, DataProperties dataProperties)
        {
            if (DataProperties.IgnoredModules.Contains(aggregateFunction.Namespace))
                return;

            var dataSyncAction = DataActionFactory.AlterAggregateFunction(sourceDataContext, targetDataContext, aggregateFunction);
            if (dataSyncAction != null)
                dataSyncActions.Add(dataSyncAction);
        }

        public static void GenerateCreateScripts(
            DataContext sourceDataContext, DataContext targetDataContext,
            AggregateFunction aggregateFunction, DataSyncActionsCollection dataSyncActions, DataProperties dataProperties)
        {
            if (DataProperties.IgnoredModules.Contains(aggregateFunction.Namespace))
                return;

            var dataSyncAction = DataActionFactory.CreateAggregateFunction(sourceDataContext, targetDataContext, aggregateFunction);
            if (dataSyncAction != null)
                dataSyncActions.Add(dataSyncAction);
        }

        public static void GenerateDropScripts(
            DataContext sourceDataContext, DataContext targetDataContext,
            AggregateFunction aggregateFunction, DataSyncActionsCollection dataSyncActions, DataProperties dataProperties)
        {
            if (DataProperties.IgnoredModules.Contains(aggregateFunction.Namespace))
                return;

            if (!dataProperties.TightSync)
                return;

            var dataSyncAction = DataActionFactory.DropAggregateFunction(sourceDataContext, targetDataContext, aggregateFunction);
            if (dataSyncAction != null)
                dataSyncActions.Add(dataSyncAction);
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            // Serialize Members
            info.AddValue("ObjectName", ObjectName);
            info.AddValue("Definition", Definition);
            info.AddValue("Description", Description);
            info.AddValue("UsesAnsiNulls", UsesAnsiNulls);
            info.AddValue("UsesQuotedIdentifier", UsesQuotedIdentifier);
        }

        public static string ToJson(AggregateFunction aggregateFunction, Formatting formatting = Formatting.Indented)
        {
            return JsonConvert.SerializeObject(aggregateFunction, formatting);
        }

        #endregion Public Methods
        #region Private Methods (1)

        private static void Init(AggregateFunction aggregateFunction, Schema schema, string objectName, ModulesRow modulesRow)
        {
            aggregateFunction.Schema = schema;
            aggregateFunction._objectName = string.IsNullOrEmpty(objectName)
                ? "Default"
                : objectName;
            aggregateFunction.Description = "Aggregate Function";

            if (modulesRow == null)
            {
                aggregateFunction.Definition = "";
                aggregateFunction.UsesAnsiNulls = true;
                aggregateFunction.UsesQuotedIdentifier = true;
                return;
            }

            aggregateFunction.Definition = modulesRow.Definition;
            aggregateFunction.UsesAnsiNulls = modulesRow.UsesAnsiNulls;
            aggregateFunction.UsesQuotedIdentifier = modulesRow.UsesQuotedIdentifier;
        }

        #endregion Private Methods

        #endregion Methods
    }
}
