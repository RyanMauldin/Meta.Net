using Meta.Net.Abstract;
using Meta.Net.Interfaces;

namespace Meta.Net.Objects
{
    //[Serializable]
    public class UniqueConstraintColumn : UniqueConstraintBasedObject, IIndexColumn
    {
        public static readonly string DefaultDescription = "Unique Constraint Column";

        public override string Description
        {
            get { return DefaultDescription; }
        }

        public bool IsDescendingKey { get; set; }
        public bool IsIncludedColumn { get; set; }
        public int KeyOrdinal { get; set; }
        public int PartitionOrdinal { get; set; }

        private static void Init(UniqueConstraintColumn uniqueConstraintColumn, UniqueConstraint uniqueConstraint, string objectName)
        {
            uniqueConstraintColumn.UniqueConstraint = uniqueConstraint;
            uniqueConstraintColumn.ObjectName = GetDefaultObjectName(uniqueConstraintColumn, objectName);
            uniqueConstraintColumn.IsDescendingKey = false;
            uniqueConstraintColumn.KeyOrdinal = 1;
            uniqueConstraintColumn.PartitionOrdinal = 0;
        }

		public UniqueConstraintColumn(UniqueConstraint uniqueConstraint, string objectName)
        {
            Init(this, uniqueConstraint, objectName);
        }

        public UniqueConstraintColumn()
        {
            
        }

		/// <summary>
        /// Deep Clone and Shallow Clone... Leaf Node.
        /// A clone of this class's isntance specific metadata.
        /// </summary>
        /// <param name="uniqueConstraintColumn">The unique constraint column to clone.</param>
        /// <returns>A clone of this class's isntance specific metadata.</returns>
        public static UniqueConstraintColumn Clone(UniqueConstraintColumn uniqueConstraintColumn)
        {
            return new UniqueConstraintColumn
            {
                ObjectName = uniqueConstraintColumn.ObjectName,
                KeyOrdinal = uniqueConstraintColumn.KeyOrdinal,
                PartitionOrdinal = uniqueConstraintColumn.PartitionOrdinal
            };
        }

        //public static bool CompareDefinitions(DataContext sourceDataContext, DataContext targetDataContext, UniqueConstraintColumn sourceUniqueConstraintColumn, UniqueConstraintColumn targetUniqueConstraintColumn)
        //{
        //    return CompareObjectNames(sourceUniqueConstraintColumn, targetUniqueConstraintColumn)
        //        && DataIndexColumns.CompareDataIndexColumn(sourceDataContext, targetDataContext, sourceUniqueConstraintColumn, targetUniqueConstraintColumn);
        //}

        //public static bool CompareObjectNames(UniqueConstraintColumn sourceUniqueConstraintColumn, UniqueConstraintColumn targetUniqueConstraintColumn, StringComparer stringComparer = null)
        //{
        //    if (stringComparer == null)
        //        stringComparer = StringComparer.OrdinalIgnoreCase;

        //    return stringComparer.Compare(sourceUniqueConstraintColumn.ObjectName, targetUniqueConstraintColumn.ObjectName) == 0;
        //} 

        //public UniqueConstraintColumn(SerializationInfo info, StreamingContext context)
        //{
        //    // Holding off on the serialzation in this manner because, this is
        //    // extremely complicated to do in this manner do to data object
        //    // associations, especially
        //    // Set Null Members
        //    UniqueConstraint = null;

        //    // Deserialize Members
        //    ObjectName = info.GetString("ObjectName");
        //    Description = info.GetString("Description");
        //    IsDescendingKey = info.GetBoolean("IsDescendingKey");
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
        //    info.AddValue("KeyOrdinal", KeyOrdinal);
        //    info.AddValue("PartitionOrdinal", PartitionOrdinal);
        //}

        //public static UniqueConstraintColumn FromJson(string json)
        //{
        //    return JsonConvert.DeserializeObject<UniqueConstraintColumn>(json);
        //}

        //public static string ToJson(UniqueConstraintColumn uniqueConstraintColumn, Formatting formatting = Formatting.Indented)
        //{
        //    return JsonConvert.SerializeObject(uniqueConstraintColumn, formatting);
        //}
    }
}
