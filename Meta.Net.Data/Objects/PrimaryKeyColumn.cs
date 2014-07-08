using System;
using System.Runtime.Serialization;
using Meta.Net.Data.Converters;
using Meta.Net.Data.Interfaces;
using Meta.Net.Data.Metadata;
using Newtonsoft.Json;

namespace Meta.Net.Data.Objects
{
    [Serializable]
    public class PrimaryKeyColumn : IDataObject, IDataUserTableBasedObject, IDataIndexColumn
    {
		#region Properties (10) 

        public Catalog Catalog
        {
            get
            {
                var primaryKey = PrimaryKey;
                if (primaryKey == null)
                    return null;

                var userTable = primaryKey.UserTable;
                if (userTable == null)
                    return null;

                var schema = userTable.Schema;
                return schema == null
                    ? null
                    : schema.Catalog;
            }
        }

        public string Description { get; set; }

        public bool IsDescendingKey { get; set; }

        public bool IsIncludedColumn { get; set; }

        public int KeyOrdinal { get; set; }

        public string Namespace
        {
            get
            {
                if (PrimaryKey == null)
                    return ObjectName;

                return PrimaryKey.Namespace + "." + ObjectName;
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
                if (PrimaryKey != null)
                {
                    if (PrimaryKey.RenamePrimaryKeyColumn(PrimaryKey, _objectName, value))
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

        public int PartitionOrdinal { get; set; }

        public PrimaryKey PrimaryKey { get; set; }

        public Schema Schema
        {
            get
            {
                var primaryKey = PrimaryKey;
                if (primaryKey == null)
                    return null;

                var userTable = primaryKey.UserTable;
                return userTable == null
                    ? null
                    : userTable.Schema;
            }
        }

        public UserTable UserTable
        {
            get
            {
                var primaryKey = PrimaryKey;
                return primaryKey == null
                    ? null
                    : primaryKey.UserTable;
            }
        }

		#endregion Properties 

		#region Fields (1) 

        [NonSerialized]
        private string _objectName;

		#endregion Fields 

		#region Constructors (5) 

        public PrimaryKeyColumn(SerializationInfo info, StreamingContext context)
        {
            // Set Null Members
            PrimaryKey = null;

            // Deserialize Members
            ObjectName = info.GetString("ObjectName");
            Description = info.GetString("Description");
            IsDescendingKey = info.GetBoolean("IsDescendingKey");
            KeyOrdinal = info.GetInt32("KeyOrdinal");
            PartitionOrdinal = info.GetInt32("PartitionOrdinal");
        }

        public PrimaryKeyColumn(PrimaryKey primaryKey, PrimaryKeysRow primaryKeysRow)
        {
            Init(this, primaryKey, primaryKeysRow.ColumnName, primaryKeysRow);
        }

        public PrimaryKeyColumn(PrimaryKey primaryKey, string objectName)
        {
            Init(this, primaryKey, objectName, null);
        }

        public PrimaryKeyColumn(string objectName)
        {
            Init(this, null, objectName, null);
        }

        public PrimaryKeyColumn()
        {
            Init(this, null, null, null);
        }

		#endregion Constructors 

		#region Methods (7) 

		#region Public Methods (6) 

        /// <summary>
        /// Deep Clone and Shallow Clone... Leaf Node.
        /// A clone of this class's isntance specific metadata.
        /// </summary>
        /// <param name="primaryKeyColumn">The primary key column to clone.</param>
        /// <returns>A clone of this class's isntance specific metadata.</returns>
        public static PrimaryKeyColumn Clone(PrimaryKeyColumn primaryKeyColumn)
        {
            return new PrimaryKeyColumn(primaryKeyColumn.ObjectName)
                {
                    KeyOrdinal = primaryKeyColumn.KeyOrdinal,
                    PartitionOrdinal = primaryKeyColumn.PartitionOrdinal
                };
        }

        public static bool CompareDefinitions(DataContext sourceDataContext, DataContext targetDataContext, PrimaryKeyColumn sourcePrimaryKeyColumn, PrimaryKeyColumn targetPrimaryKeyColumn)
        {
            return CompareObjectNames(sourcePrimaryKeyColumn, targetPrimaryKeyColumn)
                && DataIndexColumns.CompareDataIndexColumn(sourceDataContext, targetDataContext, sourcePrimaryKeyColumn, targetPrimaryKeyColumn);
        }

        public static bool CompareObjectNames(PrimaryKeyColumn sourcePrimaryKeyColumn, PrimaryKeyColumn targetPrimaryKeyColumn,
            StringComparison stringComparison = StringComparison.OrdinalIgnoreCase)
        {
            switch (stringComparison)
            {
                case StringComparison.CurrentCulture:
                    return StringComparer.CurrentCulture.Compare(
                        sourcePrimaryKeyColumn.ObjectName, targetPrimaryKeyColumn.ObjectName) == 0;
                case StringComparison.CurrentCultureIgnoreCase:
                    return StringComparer.CurrentCultureIgnoreCase.Compare(
                        sourcePrimaryKeyColumn.ObjectName, targetPrimaryKeyColumn.ObjectName) == 0;
                case StringComparison.InvariantCulture:
                    return StringComparer.InvariantCulture.Compare(
                        sourcePrimaryKeyColumn.ObjectName, targetPrimaryKeyColumn.ObjectName) == 0;
                case StringComparison.InvariantCultureIgnoreCase:
                    return StringComparer.InvariantCultureIgnoreCase.Compare(
                        sourcePrimaryKeyColumn.ObjectName, targetPrimaryKeyColumn.ObjectName) == 0;
                case StringComparison.Ordinal:
                    return StringComparer.Ordinal.Compare(
                        sourcePrimaryKeyColumn.ObjectName, targetPrimaryKeyColumn.ObjectName) == 0;
                case StringComparison.OrdinalIgnoreCase:
                    return StringComparer.OrdinalIgnoreCase.Compare(
                        sourcePrimaryKeyColumn.ObjectName, targetPrimaryKeyColumn.ObjectName) == 0;
                default:
                    return StringComparer.OrdinalIgnoreCase.Compare(
                        sourcePrimaryKeyColumn.ObjectName, targetPrimaryKeyColumn.ObjectName) == 0;
            }
        }

        public static PrimaryKeyColumn FromJson(string json)
        {
            return JsonConvert.DeserializeObject<PrimaryKeyColumn>(json);
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            // Serialize Members
            info.AddValue("ObjectName", ObjectName);
            info.AddValue("Description", Description);
            info.AddValue("IsDescendingKey", IsDescendingKey);
            info.AddValue("KeyOrdinal", KeyOrdinal);
            info.AddValue("PartitionOrdinal", PartitionOrdinal);
        }

        public static string ToJson(PrimaryKeyColumn primaryKeyColumn
            , Formatting formatting = Formatting.Indented)
        {
            return JsonConvert.SerializeObject(primaryKeyColumn, formatting);
        }

		#endregion Public Methods 
		#region Private Methods (1) 

        private static void Init(PrimaryKeyColumn primaryKeyColumn, PrimaryKey primaryKey,
            string objectName, PrimaryKeysRow primaryKeysRow)
        {
            primaryKeyColumn.PrimaryKey = primaryKey;
            primaryKeyColumn._objectName = string.IsNullOrEmpty(objectName)
                ? "Default"
                : objectName;
            primaryKeyColumn.Description = "Primary Key Column";

            if (primaryKeysRow == null)
            {
                primaryKeyColumn.IsDescendingKey = false;
                primaryKeyColumn.KeyOrdinal = 1;
                primaryKeyColumn.PartitionOrdinal = 0;
                return;
            }

            primaryKeyColumn.IsDescendingKey = primaryKeysRow.IsDescendingKey;
            primaryKeyColumn.KeyOrdinal = primaryKeysRow.KeyOrdinal;
            primaryKeyColumn.PartitionOrdinal = primaryKeysRow.PartitionOrdinal;
        }

		#endregion Private Methods 

		#endregion Methods 
    }
}
