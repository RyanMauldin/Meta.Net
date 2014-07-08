using System;
using System.Runtime.Serialization;
using Meta.Net.Data.Converters;
using Meta.Net.Data.Interfaces;
using Meta.Net.Data.Metadata;
using Newtonsoft.Json;

namespace Meta.Net.Data.Objects
{
    [Serializable]
    public class UniqueConstraintColumn : IDataObject, IDataUserTableBasedObject, IDataIndexColumn
    {
		#region Properties (10) 

        public Catalog Catalog
        {
            get
            {
                var uniqueConstraint = UniqueConstraint;
                if (uniqueConstraint == null)
                    return null;

                var userTable = uniqueConstraint.UserTable;
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
                if (UniqueConstraint == null)
                    return ObjectName;

                return UniqueConstraint.Namespace + "." + ObjectName;
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
                if (UniqueConstraint != null)
                {
                    if (UniqueConstraint.RenameUniqueConstraintColumn(UniqueConstraint, _objectName, value))
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

        public Schema Schema
        {
            get
            {
                var uniqueConstraint = UniqueConstraint;
                if (uniqueConstraint == null)
                    return null;

                var userTable = uniqueConstraint.UserTable;
                return userTable == null
                    ? null
                    : userTable.Schema;
            }
        }

        public UniqueConstraint UniqueConstraint { get; set; }

        public UserTable UserTable
        {
            get
            {
                var uniqueConstraint = UniqueConstraint;
                return uniqueConstraint == null
                    ? null
                    : uniqueConstraint.UserTable;
            }
        }

		#endregion Properties 

		#region Fields (1) 

        [NonSerialized]
        private string _objectName;

		#endregion Fields 

		#region Constructors (5) 

        public UniqueConstraintColumn(SerializationInfo info, StreamingContext context)
        {
            // Set Null Members
            UniqueConstraint = null;

            // Deserialize Members
            ObjectName = info.GetString("ObjectName");
            Description = info.GetString("Description");
            IsDescendingKey = info.GetBoolean("IsDescendingKey");
            KeyOrdinal = info.GetInt32("KeyOrdinal");
            PartitionOrdinal = info.GetInt32("PartitionOrdinal");
        }

        public UniqueConstraintColumn(UniqueConstraint uniqueConstraint, UniqueConstraintsRow uniqueConstraintsRow)
        {
            Init(this, uniqueConstraint, uniqueConstraintsRow.ColumnName, uniqueConstraintsRow);
        }

        public UniqueConstraintColumn(UniqueConstraint uniqueConstraint, string objectName)
        {
            Init(this, uniqueConstraint, objectName, null);
        }

        public UniqueConstraintColumn(string objectName)
        {
            Init(this, null, objectName, null);
        }

        public UniqueConstraintColumn()
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
        /// <param name="uniqueConstraintColumn">The unique constraint column to clone.</param>
        /// <returns>A clone of this class's isntance specific metadata.</returns>
        public static UniqueConstraintColumn Clone(UniqueConstraintColumn uniqueConstraintColumn)
        {
            return new UniqueConstraintColumn(uniqueConstraintColumn.ObjectName)
                {
                    KeyOrdinal = uniqueConstraintColumn.KeyOrdinal,
                    PartitionOrdinal = uniqueConstraintColumn.PartitionOrdinal
                };
        }

        public static bool CompareDefinitions(DataContext sourceDataContext, DataContext targetDataContext, UniqueConstraintColumn sourceUniqueConstraintColumn, UniqueConstraintColumn targetUniqueConstraintColumn)
        {
            return CompareObjectNames(sourceUniqueConstraintColumn, targetUniqueConstraintColumn)
                && DataIndexColumns.CompareDataIndexColumn(sourceDataContext, targetDataContext, sourceUniqueConstraintColumn, targetUniqueConstraintColumn);
        }

        public static bool CompareObjectNames(UniqueConstraintColumn sourceUniqueConstraintColumn, UniqueConstraintColumn targetUniqueConstraintColumn,
            StringComparison stringComparison = StringComparison.OrdinalIgnoreCase)
        {
            switch (stringComparison)
            {
                case StringComparison.CurrentCulture:
                    return StringComparer.CurrentCulture.Compare(
                        sourceUniqueConstraintColumn.ObjectName, targetUniqueConstraintColumn.ObjectName) == 0;
                case StringComparison.CurrentCultureIgnoreCase:
                    return StringComparer.CurrentCultureIgnoreCase.Compare(
                        sourceUniqueConstraintColumn.ObjectName, targetUniqueConstraintColumn.ObjectName) == 0;
                case StringComparison.InvariantCulture:
                    return StringComparer.InvariantCulture.Compare(
                        sourceUniqueConstraintColumn.ObjectName, targetUniqueConstraintColumn.ObjectName) == 0;
                case StringComparison.InvariantCultureIgnoreCase:
                    return StringComparer.InvariantCultureIgnoreCase.Compare(
                        sourceUniqueConstraintColumn.ObjectName, targetUniqueConstraintColumn.ObjectName) == 0;
                case StringComparison.Ordinal:
                    return StringComparer.Ordinal.Compare(
                        sourceUniqueConstraintColumn.ObjectName, targetUniqueConstraintColumn.ObjectName) == 0;
                case StringComparison.OrdinalIgnoreCase:
                    return StringComparer.OrdinalIgnoreCase.Compare(
                        sourceUniqueConstraintColumn.ObjectName, targetUniqueConstraintColumn.ObjectName) == 0;
                default:
                    return StringComparer.OrdinalIgnoreCase.Compare(
                        sourceUniqueConstraintColumn.ObjectName, targetUniqueConstraintColumn.ObjectName) == 0;
            }
        }

        public static UniqueConstraintColumn FromJson(string json)
        {
            return JsonConvert.DeserializeObject<UniqueConstraintColumn>(json);
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

        public static string ToJson(UniqueConstraintColumn uniqueConstraintColumn, Formatting formatting = Formatting.Indented)
        {
            return JsonConvert.SerializeObject(uniqueConstraintColumn, formatting);
        }

		#endregion Public Methods 
		#region Private Methods (1) 

        private static void Init(UniqueConstraintColumn uniqueConstraintColumn, UniqueConstraint uniqueConstraint,
            string objectName, UniqueConstraintsRow uniqueConstraintsRow)
        {
            uniqueConstraintColumn.UniqueConstraint = uniqueConstraint;
            uniqueConstraintColumn._objectName = string.IsNullOrEmpty(objectName)
                ? "Default"
                : objectName;
            uniqueConstraintColumn.Description = "Unique Constraint Column";

            if (uniqueConstraintsRow == null)
            {
                uniqueConstraintColumn.IsDescendingKey = false;
                uniqueConstraintColumn.KeyOrdinal = 1;
                uniqueConstraintColumn.PartitionOrdinal = 0;
                return;
            }

            uniqueConstraintColumn.IsDescendingKey = uniqueConstraintsRow.IsDescendingKey;
            uniqueConstraintColumn.KeyOrdinal = uniqueConstraintsRow.KeyOrdinal;
            uniqueConstraintColumn.PartitionOrdinal = uniqueConstraintsRow.PartitionOrdinal;
        }

		#endregion Private Methods 

		#endregion Methods 
    }
}
