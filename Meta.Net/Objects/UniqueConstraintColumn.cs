using System.Runtime.Serialization;
using Meta.Net.Abstract;
using Meta.Net.Interfaces;

namespace Meta.Net.Objects
{
    [DataContract]
    public class UniqueConstraintColumn : UniqueConstraintBasedObject, IIndexColumn
    {
        public static readonly string DefaultDescription = "Unique Constraint Column";

        public override string Description
        {
            get { return DefaultDescription; }
        }

        [DataMember] public bool IsDescendingKey { get; set; }
        [DataMember] public bool IsIncludedColumn { get; set; }
        [DataMember] public int KeyOrdinal { get; set; }
        [DataMember] public int PartitionOrdinal { get; set; }

        private static void Init(UniqueConstraintColumn uniqueConstraintColumn, UniqueConstraint uniqueConstraint, string objectName)
        {
            uniqueConstraintColumn.UniqueConstraint = uniqueConstraint;
            uniqueConstraintColumn.ObjectName = objectName;
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
        /// A clone of this class's instance specific metadata.
        /// </summary>
        /// <param name="uniqueConstraintColumn">The unique constraint column to clone.</param>
        /// <returns>A clone of this class's instance specific metadata.</returns>
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
    }
}
