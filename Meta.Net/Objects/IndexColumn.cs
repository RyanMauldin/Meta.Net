using Meta.Net.Abstract;
using Meta.Net.Interfaces;

namespace Meta.Net.Objects
{
    //[Serializable]
    public class IndexColumn : IndexBasedMetaObject, IIndexColumn
    {
        public static readonly string DefaultDescription = "Index Column";

        public override string Description
        {
            get { return DefaultDescription; }
        }

        public bool IsDescendingKey { get; set; }
        public bool IsIncludedColumn { get; set; }
        public int KeyOrdinal { get; set; }
        public int PartitionOrdinal { get; set; }

        private static void Init(IndexColumn indexColumn, Index index, string objectName)
        {
            indexColumn.Index = index;
            indexColumn.ObjectName = GetDefaultObjectName(indexColumn, objectName);
            indexColumn.IsDescendingKey = false;
            indexColumn.IsIncludedColumn = false;
            indexColumn.KeyOrdinal = 1;
            indexColumn.PartitionOrdinal = 0;
        }

        public IndexColumn(Index index, string objectName)
        {
            Init(this, index, objectName);
        }

        public IndexColumn()
        {
            
        }

		/// <summary>
        /// Deep Clone and Shallow Clone... Leaf Node.
        /// A clone of this class's isntance specific metadata.
        /// </summary>
        /// <param name="indexColumn">The index column to clone.</param>
        /// <returns>A clone of this class's isntance specific metadata.</returns>
        public static IndexColumn Clone(IndexColumn indexColumn)
        {
            return new IndexColumn
            {
                ObjectName = indexColumn.ObjectName,
                IsDescendingKey = indexColumn.IsDescendingKey,
                IsIncludedColumn = indexColumn.IsIncludedColumn,
                KeyOrdinal = indexColumn.KeyOrdinal,
                PartitionOrdinal = indexColumn.PartitionOrdinal
            };
        }

        //public static bool CompareDefinitions(DataContext sourceDataContext, DataContext targetDataContext, IndexColumn sourceIndexColumn, IndexColumn targetIndexColumn)
        //{
        //    return CompareObjectNames(sourceIndexColumn, targetIndexColumn)
        //        && DataIndexColumns.CompareDataIndexColumn(sourceDataContext, targetDataContext, sourceIndexColumn, targetIndexColumn);
        //}

        //public static bool CompareObjectNames(IndexColumn sourceIndexColumn, IndexColumn targetIndexColumn, StringComparer stringComparer = null)
        //{
        //    if (stringComparer == null)
        //        stringComparer = StringComparer.OrdinalIgnoreCase;

        //    return stringComparer.Compare(sourceIndexColumn.ObjectName, targetIndexColumn.ObjectName) == 0;
        //}

        //public IndexColumn(SerializationInfo info, StreamingContext context)
        //{
        //    // Holding off on the serialzation in this manner because, this is
        //    // extremely complicated to do in this manner do to data object
        //    // associations, especially
        //    // Set Null Members
        //    Index = null;

        //    // Deserialize Members
        //    ObjectName = info.GetString("ObjectName");
        //    Description = info.GetString("Description");
        //    IsDescendingKey = info.GetBoolean("IsDescendingKey");
        //    IsIncludedColumn = info.GetBoolean("IsIncludedColumn");
        //    KeyOrdinal = info.GetInt32("KeyOrdinal");
        //    PartitionOrdinal = info.GetInt32("PartitionOrdinal");
        //}

        //public void GetObjectData(SerializationInfo info, StreamingContext context)
        //{
        //    // Holding off on the serialzation in this manner because, this is
        //    // extremely complicated to do in this manner do to data object
        //    // associations, especially
        //    // Serialize Members
        //    info.AddValue("ObjectName", ObjectName);
        //    info.AddValue("Description", Description);
        //    info.AddValue("IsDescendingKey", IsDescendingKey);
        //    info.AddValue("IsIncludedColumn", IsIncludedColumn);
        //    info.AddValue("KeyOrdinal", KeyOrdinal);
        //    info.AddValue("PartitionOrdinal", PartitionOrdinal);
        //}

        //public static IndexColumn FromJson(string json)
        //{
        //    return JsonConvert.DeserializeObject<IndexColumn>(json);
        //}

        //public static string ToJson(IndexColumn indexColumn, Formatting formatting = Formatting.Indented)
        //{
        //    return JsonConvert.SerializeObject(indexColumn, formatting);
        //}
    }
}
