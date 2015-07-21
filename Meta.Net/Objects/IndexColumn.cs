using System.Runtime.Serialization;
using Meta.Net.Abstract;
using Meta.Net.Interfaces;

namespace Meta.Net.Objects
{
    [DataContract]
    public class IndexColumn : IndexBasedObject, IIndexColumn
    {
        public static readonly string DefaultDescription = "Index Column";

        public override string Description
        {
            get { return DefaultDescription; }
        }

        [DataMember] public bool IsDescendingKey { get; set; }
        [DataMember] public bool IsIncludedColumn { get; set; }
        [DataMember] public int KeyOrdinal { get; set; }
        [DataMember] public int PartitionOrdinal { get; set; }

        public IndexColumn()
        {
            IsDescendingKey = false;
            IsIncludedColumn = false;
            KeyOrdinal = 1;
            PartitionOrdinal = 0;
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
    }
}
