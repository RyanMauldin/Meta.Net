using System;
using System.Runtime.Serialization;
using Meta.Net.Data.Interfaces;
using Meta.Net.Data.Metadata;
using Newtonsoft.Json;

namespace Meta.Net.Data.Objects
{
    [Serializable]
    public class ScalarFunction : IDataObject, IDataModule
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
                    if (Schema.RenameScalarFunction(Schema, _objectName, value))
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

        public ScalarFunction(SerializationInfo info, StreamingContext context)
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

        public ScalarFunction(Schema schema, ModulesRow modulesRow)
        {
            Init(this, schema, modulesRow.ObjectName, modulesRow);
        }

        public ScalarFunction(Schema schema, string objectName)
        {
            Init(this, schema, objectName, null);
        }

        public ScalarFunction(string objectName)
        {
            Init(this, null, objectName, null);
        }

        public ScalarFunction()
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
        /// <param name="scalarFunction">The scalar function to clone.</param>
        /// <returns>A clone of this class's isntance specific metadata.</returns>
        public static ScalarFunction Clone(ScalarFunction scalarFunction)
        {
            return new ScalarFunction(scalarFunction.ObjectName)
                {
                    Definition = scalarFunction.Definition,
                    UsesAnsiNulls = scalarFunction.UsesAnsiNulls,
                    UsesQuotedIdentifier = scalarFunction.UsesQuotedIdentifier
                };
        }

        public static bool CompareDefinitions(ScalarFunction sourceScalarFunction, ScalarFunction targetScalarFunction)
        {
            if (!CompareObjectNames(sourceScalarFunction, targetScalarFunction))
                return false;

            if (sourceScalarFunction.UsesAnsiNulls != targetScalarFunction.UsesAnsiNulls)
                return false;

            if (sourceScalarFunction.UsesQuotedIdentifier != targetScalarFunction.UsesQuotedIdentifier)
                return false;

            return DataProperties.DefinitionComparer.Compare(
                sourceScalarFunction.Definition, targetScalarFunction.Definition) == 0;
        }

        public static bool CompareObjectNames(ScalarFunction sourceScalarFunction, ScalarFunction targetScalarFunction,
            StringComparison stringComparison = StringComparison.OrdinalIgnoreCase)
        {
            switch (stringComparison)
            {
                case StringComparison.CurrentCulture:
                    return StringComparer.CurrentCulture.Compare(
                        sourceScalarFunction.ObjectName, targetScalarFunction.ObjectName) == 0;
                case StringComparison.CurrentCultureIgnoreCase:
                    return StringComparer.CurrentCultureIgnoreCase.Compare(
                        sourceScalarFunction.ObjectName, targetScalarFunction.ObjectName) == 0;
                case StringComparison.InvariantCulture:
                    return StringComparer.InvariantCulture.Compare(
                        sourceScalarFunction.ObjectName, targetScalarFunction.ObjectName) == 0;
                case StringComparison.InvariantCultureIgnoreCase:
                    return StringComparer.InvariantCultureIgnoreCase.Compare(
                        sourceScalarFunction.ObjectName, targetScalarFunction.ObjectName) == 0;
                case StringComparison.Ordinal:
                    return StringComparer.Ordinal.Compare(
                        sourceScalarFunction.ObjectName, targetScalarFunction.ObjectName) == 0;
                case StringComparison.OrdinalIgnoreCase:
                    return StringComparer.OrdinalIgnoreCase.Compare(
                        sourceScalarFunction.ObjectName, targetScalarFunction.ObjectName) == 0;
                default:
                    return StringComparer.OrdinalIgnoreCase.Compare(
                        sourceScalarFunction.ObjectName, targetScalarFunction.ObjectName) == 0;
            }
        }

        public static ScalarFunction FromJson(string json)
        {
            return JsonConvert.DeserializeObject<ScalarFunction>(json);
        }

        public static void GenerateAlterScripts(
            DataContext sourceDataContext, DataContext targetDataContext,
            ScalarFunction scalarFunction, DataSyncActionsCollection dataSyncActions, DataProperties dataProperties)
        {
            if (DataProperties.IgnoredModules.Contains(scalarFunction.Namespace))
                return;

            var dataSyncAction = DataActionFactory.AlterScalarFunction(sourceDataContext, targetDataContext, scalarFunction);
            if (dataSyncAction != null)
                dataSyncActions.Add(dataSyncAction);
        }

        public static void GenerateCreateScripts(
            DataContext sourceDataContext, DataContext targetDataContext,
            ScalarFunction scalarFunction, DataSyncActionsCollection dataSyncActions, DataProperties dataProperties)
        {
            if (DataProperties.IgnoredModules.Contains(scalarFunction.Namespace))
                return;

            var dataSyncAction = DataActionFactory.CreateScalarFunction(sourceDataContext, targetDataContext, scalarFunction);
            if (dataSyncAction != null)
                dataSyncActions.Add(dataSyncAction);
        }

        public static void GenerateDropScripts(
            DataContext sourceDataContext, DataContext targetDataContext,
            ScalarFunction scalarFunction, DataSyncActionsCollection dataSyncActions, DataProperties dataProperties)
        {
            if (DataProperties.IgnoredModules.Contains(scalarFunction.Namespace))
                return;

            if (!dataProperties.TightSync)
                return;

            var dataSyncAction = DataActionFactory.DropScalarFunction(sourceDataContext, targetDataContext, scalarFunction);
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

        public static string ToJson(ScalarFunction scalarFunction, Formatting formatting = Formatting.Indented)
        {
            return JsonConvert.SerializeObject(scalarFunction, formatting);
        }

        #endregion Public Methods
        #region Private Methods (1)

        private static void Init(ScalarFunction scalarFunction, Schema schema,
            string objectName, ModulesRow modulesRow)
        {
            scalarFunction.Schema = schema;
            scalarFunction._objectName = string.IsNullOrEmpty(objectName)
                ? "Default"
                : objectName;
            scalarFunction.Description = "Scalar Function";

            if (modulesRow == null)
            {
                scalarFunction.Definition = "";
                scalarFunction.UsesAnsiNulls = true;
                scalarFunction.UsesQuotedIdentifier = true;
                return;
            }

            scalarFunction.Definition = modulesRow.Definition;
            scalarFunction.UsesAnsiNulls = modulesRow.UsesAnsiNulls;
            scalarFunction.UsesQuotedIdentifier = modulesRow.UsesQuotedIdentifier;
        }

        #endregion Private Methods

        #endregion Methods
    }
}
