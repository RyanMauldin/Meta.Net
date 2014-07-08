using System;
using System.Runtime.Serialization;
using Meta.Net.Data.Interfaces;
using Meta.Net.Data.Metadata;
using Newtonsoft.Json;

namespace Meta.Net.Data.Objects
{
    [Serializable]
    public class TableValuedFunction : IDataObject, IDataModule
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
                    if (Schema.RenameTableValuedFunction(Schema, _objectName, value))
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

        public TableValuedFunction(SerializationInfo info, StreamingContext context)
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

        public TableValuedFunction(Schema schema, ModulesRow modulesRow)
		{
		    Init(this, schema, modulesRow.ObjectName, modulesRow);
		}

        public TableValuedFunction(Schema schema, string objectName)
        {
            Init(this, schema, objectName, null);
        }

        public TableValuedFunction(string objectName)
        {
            Init(this, null, objectName, null);
        }

        public TableValuedFunction()
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
        /// <param name="tableValuedFunction">The table-valued function to clone.</param>
        /// <returns>A clone of this class's isntance specific metadata.</returns>
        public static TableValuedFunction Clone(TableValuedFunction tableValuedFunction)
        {
            return new TableValuedFunction(tableValuedFunction.ObjectName)
                {
                    Definition = tableValuedFunction.Definition,
                    UsesAnsiNulls = tableValuedFunction.UsesAnsiNulls,
                    UsesQuotedIdentifier = tableValuedFunction.UsesQuotedIdentifier
                };
        }

        public static bool CompareDefinitions(TableValuedFunction sourceTableValuedFunction, TableValuedFunction targetTableValuedFunction)
        {
            if (!CompareObjectNames(sourceTableValuedFunction, targetTableValuedFunction))
                return false;

            if (sourceTableValuedFunction.UsesAnsiNulls != targetTableValuedFunction.UsesAnsiNulls)
                return false;

            if (sourceTableValuedFunction.UsesQuotedIdentifier != targetTableValuedFunction.UsesQuotedIdentifier)
                return false;

            return DataProperties.DefinitionComparer.Compare(
                sourceTableValuedFunction.Definition, targetTableValuedFunction.Definition) == 0;
        }

        public static bool CompareObjectNames(TableValuedFunction sourceTableValuedFunction, TableValuedFunction targetTableValuedFunction,
            StringComparison stringComparison = StringComparison.OrdinalIgnoreCase)
        {
            switch (stringComparison)
            {
                case StringComparison.CurrentCulture:
                    return StringComparer.CurrentCulture.Compare(
                        sourceTableValuedFunction.ObjectName, targetTableValuedFunction.ObjectName) == 0;
                case StringComparison.CurrentCultureIgnoreCase:
                    return StringComparer.CurrentCultureIgnoreCase.Compare(
                        sourceTableValuedFunction.ObjectName, targetTableValuedFunction.ObjectName) == 0;
                case StringComparison.InvariantCulture:
                    return StringComparer.InvariantCulture.Compare(
                        sourceTableValuedFunction.ObjectName, targetTableValuedFunction.ObjectName) == 0;
                case StringComparison.InvariantCultureIgnoreCase:
                    return StringComparer.InvariantCultureIgnoreCase.Compare(
                        sourceTableValuedFunction.ObjectName, targetTableValuedFunction.ObjectName) == 0;
                case StringComparison.Ordinal:
                    return StringComparer.Ordinal.Compare(
                        sourceTableValuedFunction.ObjectName, targetTableValuedFunction.ObjectName) == 0;
                case StringComparison.OrdinalIgnoreCase:
                    return StringComparer.OrdinalIgnoreCase.Compare(
                        sourceTableValuedFunction.ObjectName, targetTableValuedFunction.ObjectName) == 0;
                default:
                    return StringComparer.OrdinalIgnoreCase.Compare(
                        sourceTableValuedFunction.ObjectName, targetTableValuedFunction.ObjectName) == 0;
            }
        }

        public static TableValuedFunction FromJson(string json)
        {
            return JsonConvert.DeserializeObject<TableValuedFunction>(json);
        }

        public static void GenerateAlterScripts(
            DataContext sourceDataContext, DataContext targetDataContext,
            TableValuedFunction tableValuedFunction, DataSyncActionsCollection dataSyncActions, DataProperties dataProperties)
        {
            if (DataProperties.IgnoredModules.Contains(tableValuedFunction.Namespace))
                return;

            var dataSyncAction = DataActionFactory.AlterTableValuedFunction(sourceDataContext, targetDataContext, tableValuedFunction);
            if (dataSyncAction != null)
                dataSyncActions.Add(dataSyncAction);
        }

        public static void GenerateCreateScripts(
            DataContext sourceDataContext, DataContext targetDataContext,
            TableValuedFunction tableValuedFunction, DataSyncActionsCollection dataSyncActions, DataProperties dataProperties)
        {
            if (DataProperties.IgnoredModules.Contains(tableValuedFunction.Namespace))
                return;

            var dataSyncAction = DataActionFactory.CreateTableValuedFunction(sourceDataContext, targetDataContext, tableValuedFunction);
            if (dataSyncAction != null)
                dataSyncActions.Add(dataSyncAction);
        }

        public static void GenerateDropScripts(
            DataContext sourceDataContext, DataContext targetDataContext,
            TableValuedFunction tableValuedFunction, DataSyncActionsCollection dataSyncActions, DataProperties dataProperties)
        {
            if (DataProperties.IgnoredModules.Contains(tableValuedFunction.Namespace))
                return;

            if (!dataProperties.TightSync)
                return;

            var dataSyncAction = DataActionFactory.DropTableValuedFunction(sourceDataContext, targetDataContext, tableValuedFunction);
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

        public static string ToJson(TableValuedFunction tableValuedFunction, Formatting formatting = Formatting.Indented)
        {
            return JsonConvert.SerializeObject(tableValuedFunction, formatting);
        }

		#endregion Public Methods 
		#region Private Methods (1) 

        private static void Init(TableValuedFunction tableValuedFunction, Schema schema, string objectName, ModulesRow modulesRow)
        {
            tableValuedFunction.Schema = schema;
            tableValuedFunction._objectName = string.IsNullOrEmpty(objectName)
                ? "Default"
                : objectName;
            tableValuedFunction.Description = "Table-Valued Function";

            if (modulesRow == null)
            {
                tableValuedFunction.Definition = "";
                tableValuedFunction.UsesAnsiNulls = true;
                tableValuedFunction.UsesQuotedIdentifier = true;
                return;
            }

            tableValuedFunction.Definition = modulesRow.Definition;
            tableValuedFunction.UsesAnsiNulls = modulesRow.UsesAnsiNulls;
            tableValuedFunction.UsesQuotedIdentifier = modulesRow.UsesQuotedIdentifier;
        }

		#endregion Private Methods 

		#endregion Methods 
    }
}
