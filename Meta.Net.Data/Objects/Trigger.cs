using System;
using System.Runtime.Serialization;
using Meta.Net.Data.Interfaces;
using Meta.Net.Data.Metadata;
using Newtonsoft.Json;

namespace Meta.Net.Data.Objects
{
    [Serializable]
    public class Trigger : IDataObject, IDataModule
    {
        #region Properties (12)

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

        public bool IsDisabled { get; set; }

        public bool IsNotForReplication { get; set; }

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
                    if (Schema.RenameTrigger(Schema, _objectName, value))
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

        public Schema Schema { get; set; }

        public string TriggerForObjectName { get; set; }

        public string TriggerForSchema { get; set; }

        public bool UsesAnsiNulls { get; set; }

        public bool UsesQuotedIdentifier { get; set; }

        #endregion Properties

        #region Fields (1)

        [NonSerialized]
        private string _objectName;

        #endregion Fields

        #region Constructors (5)

        public Trigger(SerializationInfo info, StreamingContext context)
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

        public Trigger(Schema schema, ModulesRow modulesRow)
        {
            Init(this, schema, modulesRow.ObjectName, modulesRow);
        }

        public Trigger(Schema schema, string objectName)
        {
            Init(this, schema, objectName, null);
        }

        public Trigger(string objectName)
        {
            Init(this, null, objectName, null);
        }

        public Trigger()
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
        /// <param name="trigger">The trigger to clone.</param>
        /// <returns>A clone of this class's isntance specific metadata.</returns>
        public static Trigger Clone(Trigger trigger)
        {
            return new Trigger(trigger.ObjectName)
                {
                    Definition = trigger.Definition,
                    IsDisabled = trigger.IsDisabled,
                    IsNotForReplication = trigger.IsNotForReplication,
                    TriggerForObjectName = trigger.TriggerForObjectName,
                    TriggerForSchema = trigger.TriggerForSchema,
                    UsesAnsiNulls = trigger.UsesAnsiNulls,
                    UsesQuotedIdentifier = trigger.UsesQuotedIdentifier
                };
        }

        public static bool CompareDefinitions(Trigger sourceTrigger, Trigger targetTrigger)
        {
            if (!CompareObjectNames(sourceTrigger, targetTrigger))
                return false;

            if (sourceTrigger.UsesAnsiNulls != targetTrigger.UsesAnsiNulls)
                return false;

            if (sourceTrigger.UsesQuotedIdentifier != targetTrigger.UsesQuotedIdentifier)
                return false;

            return DataProperties.DefinitionComparer.Compare(
                sourceTrigger.Definition, targetTrigger.Definition) == 0;
        }

        public static bool CompareObjectNames(Trigger sourceTrigger, Trigger targetTrigger,
            StringComparison stringComparison = StringComparison.OrdinalIgnoreCase)
        {
            switch (stringComparison)
            {
                case StringComparison.CurrentCulture:
                    return StringComparer.CurrentCulture.Compare(
                        sourceTrigger.ObjectName, targetTrigger.ObjectName) == 0;
                case StringComparison.CurrentCultureIgnoreCase:
                    return StringComparer.CurrentCultureIgnoreCase.Compare(
                        sourceTrigger.ObjectName, targetTrigger.ObjectName) == 0;
                case StringComparison.InvariantCulture:
                    return StringComparer.InvariantCulture.Compare(
                        sourceTrigger.ObjectName, targetTrigger.ObjectName) == 0;
                case StringComparison.InvariantCultureIgnoreCase:
                    return StringComparer.InvariantCultureIgnoreCase.Compare(
                        sourceTrigger.ObjectName, targetTrigger.ObjectName) == 0;
                case StringComparison.Ordinal:
                    return StringComparer.Ordinal.Compare(
                        sourceTrigger.ObjectName, targetTrigger.ObjectName) == 0;
                case StringComparison.OrdinalIgnoreCase:
                    return StringComparer.OrdinalIgnoreCase.Compare(
                        sourceTrigger.ObjectName, targetTrigger.ObjectName) == 0;
                default:
                    return StringComparer.OrdinalIgnoreCase.Compare(
                        sourceTrigger.ObjectName, targetTrigger.ObjectName) == 0;
            }
        }

        public static Trigger FromJson(string json)
        {
            return JsonConvert.DeserializeObject<Trigger>(json);
        }

        public static void GenerateAlterScripts(
            DataContext sourceDataContext, DataContext targetDataContext,
            Trigger trigger, DataSyncActionsCollection dataSyncActions, DataProperties dataProperties)
        {
            if (DataProperties.IgnoredModules.Contains(trigger.Namespace))
                return;

            var dataSyncAction = DataActionFactory.AlterTrigger(sourceDataContext, targetDataContext, trigger);
            if (dataSyncAction != null)
                dataSyncActions.Add(dataSyncAction);
        }

        public static void GenerateCreateScripts(
            DataContext sourceDataContext, DataContext targetDataContext,
            Trigger trigger, DataSyncActionsCollection dataSyncActions, DataProperties dataProperties)
        {
            if (DataProperties.IgnoredModules.Contains(trigger.Namespace))
                return;

            var dataSyncAction = DataActionFactory.CreateTrigger(sourceDataContext, targetDataContext, trigger);
            if (dataSyncAction != null)
                dataSyncActions.Add(dataSyncAction);
        }

        public static void GenerateDropScripts(
            DataContext sourceDataContext, DataContext targetDataContext,
            Trigger trigger, DataSyncActionsCollection dataSyncActions, DataProperties dataProperties)
        {
            if (DataProperties.IgnoredModules.Contains(trigger.Namespace))
                return;

            if (!dataProperties.TightSync)
                return;

            var dataSyncAction = DataActionFactory.DropTrigger(sourceDataContext, targetDataContext, trigger);
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

        public static string ToJson(Trigger trigger, Formatting formatting = Formatting.Indented)
        {
            return JsonConvert.SerializeObject(trigger, formatting);
        }

        #endregion Public Methods
        #region Private Methods (1)

        private static void Init(Trigger trigger, Schema schema, string objectName, ModulesRow modulesRow)
        {
            trigger.Schema = schema;
            trigger._objectName = string.IsNullOrEmpty(objectName)
                ? "Default"
                : objectName;
            trigger.Description = "Trigger";

            if (modulesRow == null)
            {
                trigger.Definition = "";
                trigger.IsDisabled = false;
                trigger.IsNotForReplication = false;
                trigger.TriggerForObjectName = "";
                trigger.TriggerForSchema = "";
                trigger.UsesAnsiNulls = true;
                trigger.UsesQuotedIdentifier = true;
                return;
            }

            trigger.Definition = modulesRow.Definition;
            trigger.IsDisabled = modulesRow.IsDisabled;
            trigger.IsNotForReplication = modulesRow.IsNotForReplication;
            trigger.TriggerForObjectName = modulesRow.TriggerForObjectName;
            trigger.TriggerForSchema = modulesRow.TriggerForSchema;
            trigger.UsesAnsiNulls = modulesRow.UsesAnsiNulls;
            trigger.UsesQuotedIdentifier = modulesRow.UsesQuotedIdentifier;
        }

        #endregion Private Methods

        #endregion Methods
    }
}
