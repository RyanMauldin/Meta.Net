using System;
using System.Runtime.Serialization;
using Meta.Net.Data.Interfaces;
using Meta.Net.Data.Metadata;
using Newtonsoft.Json;

namespace Meta.Net.Data.Objects
{
    [Serializable]
    public class InlineTableValuedFunction : IDataObject, IDataModule
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
                    if (Schema.RenameInlineTableValuedFunction(Schema, _objectName, value))
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

        public bool UsesAnsiNulls { get; set; }

        public bool UsesQuotedIdentifier { get; set; }

        #endregion Properties

        #region Fields (1)

        [NonSerialized]
        private string _objectName;

        #endregion Fields

        #region Constructors (5)

        public InlineTableValuedFunction(SerializationInfo info, StreamingContext context)
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

        public InlineTableValuedFunction(Schema schema, ModulesRow modulesRow)
        {
            Init(this, schema, modulesRow.ObjectName, modulesRow);
        }

        public InlineTableValuedFunction(Schema schema, string objectName)
        {
            Init(this, schema, objectName, null);
        }

        public InlineTableValuedFunction(string objectName)
        {
            Init(this, null, objectName, null);
        }

        public InlineTableValuedFunction()
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
        /// <param name="inlineTableValuedFunction">The inline table-valued function to clone.</param>
        /// <returns>A clone of this class's isntance specific metadata.</returns>
        public static InlineTableValuedFunction Clone(InlineTableValuedFunction inlineTableValuedFunction)
        {
            return new InlineTableValuedFunction(inlineTableValuedFunction.ObjectName)
                {
                    Definition = inlineTableValuedFunction.Definition,
                    UsesAnsiNulls = inlineTableValuedFunction.UsesAnsiNulls,
                    UsesQuotedIdentifier = inlineTableValuedFunction.UsesQuotedIdentifier
                };
        }

        public static bool CompareDefinitions(InlineTableValuedFunction sourceInlineTableValuedFunction, InlineTableValuedFunction targetInlineTableValuedFunction)
        {
            if (!CompareObjectNames(sourceInlineTableValuedFunction, targetInlineTableValuedFunction))
                return false;

            if (sourceInlineTableValuedFunction.UsesAnsiNulls != targetInlineTableValuedFunction.UsesAnsiNulls)
                return false;

            if (sourceInlineTableValuedFunction.UsesQuotedIdentifier != targetInlineTableValuedFunction.UsesQuotedIdentifier)
                return false;

            return DataProperties.DefinitionComparer.Compare(
                sourceInlineTableValuedFunction.Definition, targetInlineTableValuedFunction.Definition) == 0;
        }

        public static bool CompareObjectNames(InlineTableValuedFunction sourceInlineTableValuedFunction, InlineTableValuedFunction targetInlineTableValuedFunction,
            StringComparison stringComparison = StringComparison.OrdinalIgnoreCase)
        {
            switch (stringComparison)
            {
                case StringComparison.CurrentCulture:
                    return StringComparer.CurrentCulture.Compare(
                        sourceInlineTableValuedFunction.ObjectName, targetInlineTableValuedFunction.ObjectName) == 0;
                case StringComparison.CurrentCultureIgnoreCase:
                    return StringComparer.CurrentCultureIgnoreCase.Compare(
                        sourceInlineTableValuedFunction.ObjectName, targetInlineTableValuedFunction.ObjectName) == 0;
                case StringComparison.InvariantCulture:
                    return StringComparer.InvariantCulture.Compare(
                        sourceInlineTableValuedFunction.ObjectName, targetInlineTableValuedFunction.ObjectName) == 0;
                case StringComparison.InvariantCultureIgnoreCase:
                    return StringComparer.InvariantCultureIgnoreCase.Compare(
                        sourceInlineTableValuedFunction.ObjectName, targetInlineTableValuedFunction.ObjectName) == 0;
                case StringComparison.Ordinal:
                    return StringComparer.Ordinal.Compare(
                        sourceInlineTableValuedFunction.ObjectName, targetInlineTableValuedFunction.ObjectName) == 0;
                case StringComparison.OrdinalIgnoreCase:
                    return StringComparer.OrdinalIgnoreCase.Compare(
                        sourceInlineTableValuedFunction.ObjectName, targetInlineTableValuedFunction.ObjectName) == 0;
                default:
                    return StringComparer.OrdinalIgnoreCase.Compare(
                        sourceInlineTableValuedFunction.ObjectName, targetInlineTableValuedFunction.ObjectName) == 0;
            }
        }

        public static InlineTableValuedFunction FromJson(string json)
        {
            return JsonConvert.DeserializeObject<InlineTableValuedFunction>(json);
        }

        public static void GenerateAlterScripts(
            DataContext sourceDataContext, DataContext targetDataContext,
            InlineTableValuedFunction inlineTableValuedFunction, DataSyncActionsCollection dataSyncActions, DataProperties dataProperties)
        {
            if (DataProperties.IgnoredModules.Contains(inlineTableValuedFunction.Namespace))
                return;

            var dataSyncAction = DataActionFactory.AlterInlineTableValuedFunction(sourceDataContext, targetDataContext, inlineTableValuedFunction);
            if (dataSyncAction != null)
                dataSyncActions.Add(dataSyncAction);
        }

        public static void GenerateCreateScripts(
            DataContext sourceDataContext, DataContext targetDataContext,
            InlineTableValuedFunction inlineTableValuedFunction, DataSyncActionsCollection dataSyncActions, DataProperties dataProperties)
        {
            if (DataProperties.IgnoredModules.Contains(inlineTableValuedFunction.Namespace))
                return;

            var dataSyncAction = DataActionFactory.CreateInlineTableValuedFunction(sourceDataContext, targetDataContext, inlineTableValuedFunction);
            if (dataSyncAction != null)
                dataSyncActions.Add(dataSyncAction);
        }

        public static void GenerateDropScripts(
            DataContext sourceDataContext, DataContext targetDataContext,
            InlineTableValuedFunction inlineTableValuedFunction, DataSyncActionsCollection dataSyncActions, DataProperties dataProperties)
        {
            if (DataProperties.IgnoredModules.Contains(inlineTableValuedFunction.Namespace))
                return;

            if (!dataProperties.TightSync)
                return;

            var dataSyncAction = DataActionFactory.DropInlineTableValuedFunction(sourceDataContext, targetDataContext, inlineTableValuedFunction);
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

        public static string ToJson(InlineTableValuedFunction inlineTableValuedFunction, Formatting formatting = Formatting.Indented)
        {
            return JsonConvert.SerializeObject(inlineTableValuedFunction, formatting);
        }

        #endregion Public Methods
        #region Private Methods (1)

        private static void Init(InlineTableValuedFunction inlineTableValuedFunction, Schema schema, string objectName, ModulesRow modulesRow)
        {
            inlineTableValuedFunction.Schema = schema;
            inlineTableValuedFunction._objectName = string.IsNullOrEmpty(objectName)
                ? "Default"
                : objectName;
            inlineTableValuedFunction.Description = "Inline Table-Valued Function";

            if (modulesRow == null)
            {
                inlineTableValuedFunction.Definition = "";
                inlineTableValuedFunction.UsesAnsiNulls = true;
                inlineTableValuedFunction.UsesQuotedIdentifier = true;
                return;
            }

            inlineTableValuedFunction.Definition = modulesRow.Definition;
            inlineTableValuedFunction.UsesAnsiNulls = modulesRow.UsesAnsiNulls;
            inlineTableValuedFunction.UsesQuotedIdentifier = modulesRow.UsesQuotedIdentifier;
        }

        #endregion Private Methods

        #endregion Methods
    }
}
