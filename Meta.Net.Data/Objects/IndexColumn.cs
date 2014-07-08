using System;
using System.Runtime.Serialization;
using Meta.Net.Data.Converters;
using Meta.Net.Data.Interfaces;
using Meta.Net.Data.Metadata;
using Newtonsoft.Json;

namespace Meta.Net.Data.Objects
{
    [Serializable]
    public class IndexColumn : IDataObject, IDataUserTableBasedObject, IDataIndexColumn
    {
		#region Properties (11) 

        public Catalog Catalog
        {
            get
            {
                var index = Index;
                if (index == null)
                    return null;

                var userTable = index.UserTable;
                if (userTable == null)
                    return null;

                var schema = userTable.Schema;
                return schema == null
                    ? null
                    : schema.Catalog;
            }
        }

        public string Description { get; set; }

        public Index Index { get; set; }

        public bool IsDescendingKey { get; set; }

        public bool IsIncludedColumn { get; set; }

        public int KeyOrdinal { get; set; }

        public string Namespace
        {
            get
            {
                if (Index == null)
                    return ObjectName;

                return Index.Namespace + "." + ObjectName;
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
                if (Index != null)
                {
                    if (Index.RenameIndexColumn(Index, _objectName, value))
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
                var index = Index;
                if (index == null)
                    return null;

                var userTable = index.UserTable;
                return userTable == null
                    ? null
                    : userTable.Schema;
            }
        }

        public UserTable UserTable
        {
            get
            {
                var index = Index;
                return index == null
                    ? null
                    : index.UserTable;
            }
        }

		#endregion Properties 

		#region Fields (1) 

        [NonSerialized]
        private string _objectName;

		#endregion Fields 

		#region Constructors (5) 

        public IndexColumn(SerializationInfo info, StreamingContext context)
        {
            // Set Null Members
            Index = null;

            // Deserialize Members
            ObjectName = info.GetString("ObjectName");
            Description = info.GetString("Description");
            IsDescendingKey = info.GetBoolean("IsDescendingKey");
            IsIncludedColumn = info.GetBoolean("IsIncludedColumn");
            KeyOrdinal = info.GetInt32("KeyOrdinal");
            PartitionOrdinal = info.GetInt32("PartitionOrdinal");
        }

        public IndexColumn(Index index, IndexesRow indexesRow)
        {
            Init(this, index, indexesRow.ColumnName, indexesRow);
        }

        public IndexColumn(Index index, string objectName)
        {
            Init(this, index, objectName, null);
        }

        public IndexColumn(string objectName)
        {
            Init(this, null, objectName, null);
        }

        public IndexColumn()
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
        /// <param name="indexColumn">The index column to clone.</param>
        /// <returns>A clone of this class's isntance specific metadata.</returns>
        public static IndexColumn Clone(IndexColumn indexColumn)
        {
            return new IndexColumn(indexColumn.ObjectName)
                {
                    IsDescendingKey = indexColumn.IsDescendingKey,
                    IsIncludedColumn = indexColumn.IsIncludedColumn,
                    KeyOrdinal = indexColumn.KeyOrdinal,
                    PartitionOrdinal = indexColumn.PartitionOrdinal
                };
        }

        public static bool CompareDefinitions(DataContext sourceDataContext, DataContext targetDataContext, IndexColumn sourceIndexColumn, IndexColumn targetIndexColumn)
        {
            return CompareObjectNames(sourceIndexColumn, targetIndexColumn)
                && DataIndexColumns.CompareDataIndexColumn(sourceDataContext, targetDataContext, sourceIndexColumn, targetIndexColumn);
        }

        public static bool CompareObjectNames(IndexColumn sourceIndexColumn, IndexColumn targetIndexColumn,
            StringComparison stringComparison = StringComparison.OrdinalIgnoreCase)
        {
            switch (stringComparison)
            {
                case StringComparison.CurrentCulture:
                    return StringComparer.CurrentCulture.Compare(
                        sourceIndexColumn.ObjectName, targetIndexColumn.ObjectName) == 0;
                case StringComparison.CurrentCultureIgnoreCase:
                    return StringComparer.CurrentCultureIgnoreCase.Compare(
                        sourceIndexColumn.ObjectName, targetIndexColumn.ObjectName) == 0;
                case StringComparison.InvariantCulture:
                    return StringComparer.InvariantCulture.Compare(
                        sourceIndexColumn.ObjectName, targetIndexColumn.ObjectName) == 0;
                case StringComparison.InvariantCultureIgnoreCase:
                    return StringComparer.InvariantCultureIgnoreCase.Compare(
                        sourceIndexColumn.ObjectName, targetIndexColumn.ObjectName) == 0;
                case StringComparison.Ordinal:
                    return StringComparer.Ordinal.Compare(
                        sourceIndexColumn.ObjectName, targetIndexColumn.ObjectName) == 0;
                case StringComparison.OrdinalIgnoreCase:
                    return StringComparer.OrdinalIgnoreCase.Compare(
                        sourceIndexColumn.ObjectName, targetIndexColumn.ObjectName) == 0;
                default:
                    return StringComparer.OrdinalIgnoreCase.Compare(
                        sourceIndexColumn.ObjectName, targetIndexColumn.ObjectName) == 0;
            }
        }

        public static IndexColumn FromJson(string json)
        {
            return JsonConvert.DeserializeObject<IndexColumn>(json);
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            // Serialize Members
            info.AddValue("ObjectName", ObjectName);
            info.AddValue("Description", Description);
            info.AddValue("IsDescendingKey", IsDescendingKey);
            info.AddValue("IsIncludedColumn", IsIncludedColumn);
            info.AddValue("KeyOrdinal", KeyOrdinal);
            info.AddValue("PartitionOrdinal", PartitionOrdinal);
        }

        public static string ToJson(IndexColumn indexColumn, Formatting formatting = Formatting.Indented)
        {
            return JsonConvert.SerializeObject(indexColumn, formatting);
        }

		#endregion Public Methods 
		#region Private Methods (1) 

        private static void Init(IndexColumn indexColumn, Index index,
            string objectName, IndexesRow indexesRow)
        {
            indexColumn.Index = index;
            indexColumn._objectName = string.IsNullOrEmpty(objectName)
                ? "Default"
                : objectName;
            indexColumn.Description = "Index Column";

            if (indexesRow == null)
            {
                indexColumn.IsDescendingKey = false;
                indexColumn.IsIncludedColumn = false;
                indexColumn.KeyOrdinal = 1;
                indexColumn.PartitionOrdinal = 0;
                return;
            }

            indexColumn.IsDescendingKey = indexesRow.IsDescendingKey;
            indexColumn.IsIncludedColumn = indexesRow.IsIncludedColumn;
            indexColumn.KeyOrdinal = indexesRow.KeyOrdinal;
            indexColumn.PartitionOrdinal = indexesRow.PartitionOrdinal;
        }

		#endregion Private Methods 

		#endregion Methods 
    }
}
