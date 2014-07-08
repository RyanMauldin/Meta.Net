using System;
using System.Runtime.Serialization;
using Meta.Net.Data.Interfaces;
using Meta.Net.Data.Metadata;
using Newtonsoft.Json;

namespace Meta.Net.Data.Objects
{
    [Serializable]
    public class UserDefinedDataType : IDataObject, IDataCatalogBasedObject
    {
        #region Properties (14)

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

        public string Collation { get; set; }

        public string DataType { get; set; }

        public string Description { get; set; }

        public bool HasDefault { get; set; }

        public bool IsAssemblyType { get; set; }

        public bool IsNullable { get; set; }

        public bool IsUserDefined { get; set; }

        public Int64 MaxLength { get; set; }

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
                    if (Schema.RenameUserDefinedDataType(Schema, _objectName, value))
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

        public int Precision { get; set; }

        public int Scale { get; set; }

        public Schema Schema { get; set; }

        #endregion Properties

        #region Fields (1)

        [NonSerialized]
        private string _objectName;

        #endregion Fields

        #region Constructors (5)

        public UserDefinedDataType(SerializationInfo info, StreamingContext context)
        {
            // Set Null Members
            Schema = null;

            // Deserialize Members
            ObjectName = info.GetString("ObjectName");
            Description = info.GetString("Description");
            Collation = info.GetString("Collation");
            DataType = info.GetString("DataType");
            Description = info.GetString("Description");
            HasDefault = info.GetBoolean("HasDefault");
            IsAssemblyType = info.GetBoolean("IsAssemblyType");
            IsNullable = info.GetBoolean("IsNullable");
            IsUserDefined = info.GetBoolean("IsUserDefined");
            MaxLength = info.GetInt32("MaxLength");
            Precision = info.GetInt32("Precision");
            Scale = info.GetInt32("Scale");
        }

        public UserDefinedDataType(Schema schema, UserDefinedDataTypesRow userDefinedDataTypesRow)
        {
            Init(this, schema, userDefinedDataTypesRow.ObjectName, userDefinedDataTypesRow);
        }

        public UserDefinedDataType(Schema schema, string objectName)
        {
            Init(this, schema, objectName, null);
        }

        public UserDefinedDataType(string objectName)
        {
            Init(this, null, objectName, null);
        }

        public UserDefinedDataType()
        {
            Init(this, null, null, null);
        }

        #endregion Constructors

        #region Methods (9)

        #region Public Methods (8)

        /// <summary>
        /// Deep Clone and Shallow Clone... Leaf Node.
        /// A clone of this class's isntance specific metadata.
        /// </summary>
        /// <param name="userDefinedDataType">The user-defined data type to clone.</param>
        /// <returns>A clone of this class's isntance specific metadata.</returns>
        public static UserDefinedDataType Clone(UserDefinedDataType userDefinedDataType)
        {
            return new UserDefinedDataType(userDefinedDataType.ObjectName)
                {
                    Collation = userDefinedDataType.Collation,
                    DataType = userDefinedDataType.DataType,
                    HasDefault = userDefinedDataType.HasDefault,
                    IsAssemblyType = userDefinedDataType.IsAssemblyType,
                    IsNullable = userDefinedDataType.IsNullable,
                    IsUserDefined = userDefinedDataType.IsUserDefined,
                    MaxLength = userDefinedDataType.MaxLength,
                    Precision = userDefinedDataType.Precision,
                    Scale = userDefinedDataType.Scale
                };
        }

        public static bool CompareDefinitions(UserDefinedDataType sourceUserDefinedDataType, UserDefinedDataType targetUserDefinedDataType)
        {
            if (!CompareObjectNames(sourceUserDefinedDataType, targetUserDefinedDataType))
                return false;

            if (sourceUserDefinedDataType.HasDefault != targetUserDefinedDataType.HasDefault)
                return false;

            if (sourceUserDefinedDataType.IsAssemblyType != targetUserDefinedDataType.IsAssemblyType)
                return false;

            if (sourceUserDefinedDataType.IsNullable != targetUserDefinedDataType.IsNullable)
                return false;

            if (sourceUserDefinedDataType.IsUserDefined != targetUserDefinedDataType.IsUserDefined)
                return false;

            if (sourceUserDefinedDataType.MaxLength != targetUserDefinedDataType.MaxLength)
                return false;

            if (sourceUserDefinedDataType.Precision != targetUserDefinedDataType.Precision)
                return false;

            if (sourceUserDefinedDataType.Scale != targetUserDefinedDataType.Scale)
                return false;

            if (StringComparer.OrdinalIgnoreCase.Compare(sourceUserDefinedDataType.Collation, targetUserDefinedDataType.Collation) != 0)
                return false;

            return StringComparer.OrdinalIgnoreCase.Compare(
                sourceUserDefinedDataType.DataType, targetUserDefinedDataType.DataType) == 0;
        }

        public static bool CompareObjectNames(UserDefinedDataType sourceUserDefinedDataType, UserDefinedDataType targetUserDefinedDataType,
            StringComparison stringComparison = StringComparison.OrdinalIgnoreCase)
        {
            switch (stringComparison)
            {
                case StringComparison.CurrentCulture:
                    return StringComparer.CurrentCulture.Compare(
                        sourceUserDefinedDataType.ObjectName, targetUserDefinedDataType.ObjectName) == 0;
                case StringComparison.CurrentCultureIgnoreCase:
                    return StringComparer.CurrentCultureIgnoreCase.Compare(
                        sourceUserDefinedDataType.ObjectName, targetUserDefinedDataType.ObjectName) == 0;
                case StringComparison.InvariantCulture:
                    return StringComparer.InvariantCulture.Compare(
                        sourceUserDefinedDataType.ObjectName, targetUserDefinedDataType.ObjectName) == 0;
                case StringComparison.InvariantCultureIgnoreCase:
                    return StringComparer.InvariantCultureIgnoreCase.Compare(
                        sourceUserDefinedDataType.ObjectName, targetUserDefinedDataType.ObjectName) == 0;
                case StringComparison.Ordinal:
                    return StringComparer.Ordinal.Compare(
                        sourceUserDefinedDataType.ObjectName, targetUserDefinedDataType.ObjectName) == 0;
                case StringComparison.OrdinalIgnoreCase:
                    return StringComparer.OrdinalIgnoreCase.Compare(
                        sourceUserDefinedDataType.ObjectName, targetUserDefinedDataType.ObjectName) == 0;
                default:
                    return StringComparer.OrdinalIgnoreCase.Compare(
                        sourceUserDefinedDataType.ObjectName, targetUserDefinedDataType.ObjectName) == 0;
            }
        }

        public static UserDefinedDataType FromJson(string json)
        {
            return JsonConvert.DeserializeObject<UserDefinedDataType>(json);
        }

        public static void GenerateCreateScripts(
            DataContext sourceDataContext, DataContext targetDataContext,
            UserDefinedDataType userDefinedDataType, DataSyncActionsCollection dataSyncActions, DataProperties dataProperties)
        {
            var dataSyncAction = DataActionFactory.CreateUserDefinedDataType(sourceDataContext, targetDataContext, userDefinedDataType);
            if (dataSyncAction != null)
                dataSyncActions.Add(dataSyncAction);
        }

        public static void GenerateDropScripts(
            DataContext sourceDataContext, DataContext targetDataContext,
            UserDefinedDataType userDefinedDataType, DataSyncActionsCollection dataSyncActions, DataProperties dataProperties)
        {
            if (!dataProperties.TightSync)
                return;

            var dataSyncAction = DataActionFactory.DropUserDefinedDataType(sourceDataContext, targetDataContext, userDefinedDataType);
            if (dataSyncAction != null)
                dataSyncActions.Add(dataSyncAction);
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            // Serialize Members
            info.AddValue("ObjectName", ObjectName);
            info.AddValue("Description", Description);
            info.AddValue("Collation", Collation);
            info.AddValue("DataType", DataType);
            info.AddValue("Description", Description);
            info.AddValue("HasDefault", HasDefault);
            info.AddValue("IsAssemblyType", IsAssemblyType);
            info.AddValue("IsNullable", IsNullable);
            info.AddValue("IsUserDefined", IsUserDefined);
            info.AddValue("MaxLength", MaxLength);
            info.AddValue("Precision", Precision);
            info.AddValue("Scale", Scale);
        }

        public static string ToJson(UserDefinedDataType userDefinedDataType, Formatting formatting = Formatting.Indented)
        {
            return JsonConvert.SerializeObject(userDefinedDataType, formatting);
        }

        #endregion Public Methods
        #region Private Methods (1)

        private static void Init(UserDefinedDataType userDefinedDataType, Schema schema, string objectName, IDataColumnDescripterRow iDataColumnDescripterRow)
        {
            userDefinedDataType.Schema = schema;
            userDefinedDataType._objectName = string.IsNullOrEmpty(objectName)
                ? "Default"
                : objectName;
            userDefinedDataType.Description = "User-Defined Data Type";

            if (iDataColumnDescripterRow == null)
            {
                userDefinedDataType.Collation = "";
                userDefinedDataType.DataType = "INT";
                userDefinedDataType.HasDefault = false;
                userDefinedDataType.IsAssemblyType = false;
                userDefinedDataType.IsNullable = false;
                userDefinedDataType.IsUserDefined = false;
                userDefinedDataType.MaxLength = 0;
                userDefinedDataType.Precision = 0;
                userDefinedDataType.Scale = 0;
                return;
            }

            userDefinedDataType.Collation = iDataColumnDescripterRow.Collation;
            userDefinedDataType.DataType = iDataColumnDescripterRow.DataType;
            userDefinedDataType.HasDefault = iDataColumnDescripterRow.HasDefault;
            userDefinedDataType.IsAssemblyType = iDataColumnDescripterRow.IsAssemblyType;
            userDefinedDataType.IsNullable = iDataColumnDescripterRow.IsNullable;
            userDefinedDataType.IsUserDefined = iDataColumnDescripterRow.IsUserDefined;
            userDefinedDataType.MaxLength = iDataColumnDescripterRow.MaxLength;
            userDefinedDataType.Precision = iDataColumnDescripterRow.Precision;
            userDefinedDataType.Scale = iDataColumnDescripterRow.Scale;
        }

        #endregion Private Methods

        #endregion Methods
    }
}
