using System;
using System.Runtime.Serialization;
using Meta.Net.Data.Interfaces;
using Meta.Net.Data.Metadata;
using Newtonsoft.Json;

namespace Meta.Net.Data.Objects
{
    [Serializable]
    public class View : IDataObject, IDataModule
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
                    if (Schema.RenameView(Schema, _objectName, value))
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

        public View(SerializationInfo info, StreamingContext context)
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

        public View(Schema schema, ModulesRow modulesRow)
        {
            Init(this, schema, modulesRow.ObjectName, modulesRow);
        }

        public View(Schema schema, string objectName)
        {
            Init(this, schema, objectName, null);
        }

        public View(string objectName)
        {
            Init(this, null, objectName, null);
        }

        public View()
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
        /// <param name="view">The view to clone.</param>
        /// <returns>A clone of this class's isntance specific metadata.</returns>
        public static View Clone(View view)
        {
            return new View(view.ObjectName)
                {
                    Definition = view.Definition,
                    UsesAnsiNulls = view.UsesAnsiNulls,
                    UsesQuotedIdentifier = view.UsesQuotedIdentifier
                };
        }

        public static bool CompareDefinitions(View sourceView, View targetView)
        {
            if (!CompareObjectNames(sourceView, targetView))
                return false;

            if (sourceView.UsesAnsiNulls != targetView.UsesAnsiNulls)
                return false;

            if (sourceView.UsesQuotedIdentifier != targetView.UsesQuotedIdentifier)
                return false;

            return DataProperties.DefinitionComparer.Compare(
                sourceView.Definition, targetView.Definition) == 0;
        }

        public static bool CompareObjectNames(View sourceView, View targetView,
            StringComparison stringComparison = StringComparison.OrdinalIgnoreCase)
        {
            switch (stringComparison)
            {
                case StringComparison.CurrentCulture:
                    return StringComparer.CurrentCulture.Compare(
                        sourceView.ObjectName, targetView.ObjectName) == 0;
                case StringComparison.CurrentCultureIgnoreCase:
                    return StringComparer.CurrentCultureIgnoreCase.Compare(
                        sourceView.ObjectName, targetView.ObjectName) == 0;
                case StringComparison.InvariantCulture:
                    return StringComparer.InvariantCulture.Compare(
                        sourceView.ObjectName, targetView.ObjectName) == 0;
                case StringComparison.InvariantCultureIgnoreCase:
                    return StringComparer.InvariantCultureIgnoreCase.Compare(
                        sourceView.ObjectName, targetView.ObjectName) == 0;
                case StringComparison.Ordinal:
                    return StringComparer.Ordinal.Compare(
                        sourceView.ObjectName, targetView.ObjectName) == 0;
                case StringComparison.OrdinalIgnoreCase:
                    return StringComparer.OrdinalIgnoreCase.Compare(
                        sourceView.ObjectName, targetView.ObjectName) == 0;
                default:
                    return StringComparer.OrdinalIgnoreCase.Compare(
                        sourceView.ObjectName, targetView.ObjectName) == 0;
            }
        }

        public static View FromJson(string json)
        {
            return JsonConvert.DeserializeObject<View>(json);
        }

        public static void GenerateAlterScripts(
            DataContext sourceDataContext, DataContext targetDataContext,
            View view, DataSyncActionsCollection dataSyncActions, DataProperties dataProperties)
        {
            if (DataProperties.IgnoredModules.Contains(view.Namespace))
                return;

            var dataSyncAction = DataActionFactory.AlterView(sourceDataContext, targetDataContext, view);
            if (dataSyncAction != null)
                dataSyncActions.Add(dataSyncAction);
        }

        public static void GenerateCreateScripts(
            DataContext sourceDataContext, DataContext targetDataContext,
            View view, DataSyncActionsCollection dataSyncActions, DataProperties dataProperties)
        {
            if (DataProperties.IgnoredModules.Contains(view.Namespace))
                return;

            var dataSyncAction = DataActionFactory.CreateView(sourceDataContext, targetDataContext, view);
            if (dataSyncAction != null)
                dataSyncActions.Add(dataSyncAction);
        }

        public static void GenerateDropScripts(
            DataContext sourceDataContext, DataContext targetDataContext,
            View view, DataSyncActionsCollection dataSyncActions, DataProperties dataProperties)
        {
            if (DataProperties.IgnoredModules.Contains(view.Namespace))
                return;

            if (!dataProperties.TightSync)
                return;

            var dataSyncAction = DataActionFactory.DropView(sourceDataContext, targetDataContext, view);
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

        public static string ToJson(View view, Formatting formatting = Formatting.Indented)
        {
            return JsonConvert.SerializeObject(view, formatting);
        }

        #endregion Public Methods
        #region Private Methods (1)

        private static void Init(View view, Schema schema, string objectName, ModulesRow modulesRow)
        {
            view.Schema = schema;
            view._objectName = string.IsNullOrEmpty(objectName)
                ? "Default"
                : objectName;
            view.Description = "View";

            if (modulesRow == null)
            {
                view.Definition = "";
                view.UsesAnsiNulls = true;
                view.UsesQuotedIdentifier = true;
                return;
            }

            view.Definition = modulesRow.Definition;
            view.UsesAnsiNulls = modulesRow.UsesAnsiNulls;
            view.UsesQuotedIdentifier = modulesRow.UsesQuotedIdentifier;
        }

        #endregion Private Methods

        #endregion Methods
    }
}
