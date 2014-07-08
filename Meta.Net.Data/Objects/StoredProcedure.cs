using System;
using System.Runtime.Serialization;
using Meta.Net.Data.Interfaces;
using Meta.Net.Data.Metadata;
using Newtonsoft.Json;

namespace Meta.Net.Data.Objects
{
    [Serializable]
    public class StoredProcedure : IDataObject, IDataModule
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
                    if (Schema.RenameStoredProcedure(Schema, _objectName, value))
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

        public StoredProcedure(SerializationInfo info, StreamingContext context)
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

        public StoredProcedure(Schema schema, ModulesRow modulesRow)
		{
		    Init(this, schema, modulesRow.ObjectName, modulesRow);
		}

        public StoredProcedure(Schema schema, string objectName)
        {
            Init(this, schema, objectName, null);
        }

        public StoredProcedure(string objectName)
        {
            Init(this, null, objectName, null);
        }

        public StoredProcedure()
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
        /// <param name="storedProcedure">The stored procedure to clone.</param>
        /// <returns>A clone of this class's isntance specific metadata.</returns>
        public static StoredProcedure Clone(StoredProcedure storedProcedure)
        {
            return new StoredProcedure(storedProcedure.ObjectName)
                {
                    Definition = storedProcedure.Definition,
                    UsesAnsiNulls = storedProcedure.UsesAnsiNulls,
                    UsesQuotedIdentifier = storedProcedure.UsesQuotedIdentifier
                };
        }

        public static bool CompareDefinitions(StoredProcedure sourceStoredProcedure, StoredProcedure targetStoredProcedure)
        {
            if (!CompareObjectNames(sourceStoredProcedure, targetStoredProcedure))
                return false;

            if (sourceStoredProcedure.UsesAnsiNulls != targetStoredProcedure.UsesAnsiNulls)
                return false;

            if (sourceStoredProcedure.UsesQuotedIdentifier != targetStoredProcedure.UsesQuotedIdentifier)
                return false;

            return DataProperties.DefinitionComparer.Compare(
                sourceStoredProcedure.Definition, targetStoredProcedure.Definition) == 0;
        }

        public static bool CompareObjectNames(StoredProcedure sourceStoredProcedure, StoredProcedure targetStoredProcedure,
            StringComparison stringComparison = StringComparison.OrdinalIgnoreCase)
        {
            switch (stringComparison)
            {
                case StringComparison.CurrentCulture:
                    return StringComparer.CurrentCulture.Compare(
                        sourceStoredProcedure.ObjectName, targetStoredProcedure.ObjectName) == 0;
                case StringComparison.CurrentCultureIgnoreCase:
                    return StringComparer.CurrentCultureIgnoreCase.Compare(
                        sourceStoredProcedure.ObjectName, targetStoredProcedure.ObjectName) == 0;
                case StringComparison.InvariantCulture:
                    return StringComparer.InvariantCulture.Compare(
                        sourceStoredProcedure.ObjectName, targetStoredProcedure.ObjectName) == 0;
                case StringComparison.InvariantCultureIgnoreCase:
                    return StringComparer.InvariantCultureIgnoreCase.Compare(
                        sourceStoredProcedure.ObjectName, targetStoredProcedure.ObjectName) == 0;
                case StringComparison.Ordinal:
                    return StringComparer.Ordinal.Compare(
                        sourceStoredProcedure.ObjectName, targetStoredProcedure.ObjectName) == 0;
                case StringComparison.OrdinalIgnoreCase:
                    return StringComparer.OrdinalIgnoreCase.Compare(
                        sourceStoredProcedure.ObjectName, targetStoredProcedure.ObjectName) == 0;
                default:
                    return StringComparer.OrdinalIgnoreCase.Compare(
                        sourceStoredProcedure.ObjectName, targetStoredProcedure.ObjectName) == 0;
            }
        }

        public static StoredProcedure FromJson(string json)
        {
            return JsonConvert.DeserializeObject<StoredProcedure>(json);
        }

        public static void GenerateAlterScripts(
            DataContext sourceDataContext, DataContext targetDataContext,
            StoredProcedure storedProcedure, DataSyncActionsCollection dataSyncActions, DataProperties dataProperties)
        {
            if (DataProperties.IgnoredModules.Contains(storedProcedure.Namespace))
                return;

            var dataSyncAction = DataActionFactory.AlterStoredProcedure(sourceDataContext, targetDataContext, storedProcedure);
            if (dataSyncAction != null)
                dataSyncActions.Add(dataSyncAction);
        }

        public static void GenerateCreateScripts(
            DataContext sourceDataContext, DataContext targetDataContext,
            StoredProcedure storedProcedure, DataSyncActionsCollection dataSyncActions, DataProperties dataProperties)
        {
            if (DataProperties.IgnoredModules.Contains(storedProcedure.Namespace))
                return;

            var dataSyncAction = DataActionFactory.CreateStoredProcedure(sourceDataContext, targetDataContext, storedProcedure);
            if (dataSyncAction != null)
                dataSyncActions.Add(dataSyncAction);
        }

        public static void GenerateDropScripts(
            DataContext sourceDataContext, DataContext targetDataContext,
            StoredProcedure storedProcedure, DataSyncActionsCollection dataSyncActions, DataProperties dataProperties)
        {
            if (DataProperties.IgnoredModules.Contains(storedProcedure.Namespace))
                return;

            if (!dataProperties.TightSync)
                return;

            var dataSyncAction = DataActionFactory.DropStoredProcedure(sourceDataContext, targetDataContext, storedProcedure);
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

        public static string ToJson(StoredProcedure storedProcedure, Formatting formatting = Formatting.Indented)
        {
            return JsonConvert.SerializeObject(storedProcedure, formatting);
        }

		#endregion Public Methods 
		#region Private Methods (1) 

        private static void Init(StoredProcedure storedProcedure, Schema schema, string objectName, ModulesRow modulesRow)
        {
            storedProcedure.Schema = schema;
            storedProcedure._objectName = string.IsNullOrEmpty(objectName)
                ? "Default"
                : objectName;
            storedProcedure.Description = "Stored Procedure";

            if (modulesRow == null)
            {
                storedProcedure.Definition = "";
                storedProcedure.UsesAnsiNulls = true;
                storedProcedure.UsesQuotedIdentifier = true;
                return;
            }

            storedProcedure.Definition = modulesRow.Definition;
            storedProcedure.UsesAnsiNulls = modulesRow.UsesAnsiNulls;
            storedProcedure.UsesQuotedIdentifier = modulesRow.UsesQuotedIdentifier;
        }

		#endregion Private Methods 

		#endregion Methods 
    }
}
