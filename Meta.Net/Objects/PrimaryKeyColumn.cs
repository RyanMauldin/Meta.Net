using System.Runtime.Serialization;
using Meta.Net.Abstract;
using Meta.Net.Interfaces;

namespace Meta.Net.Objects
{
    [DataContract]
    public class PrimaryKeyColumn : PrimaryKeyBasedObject, IIndexColumn
    {
        public static readonly string DefaultDescription = "Primary Key Column";

        public override string Description
        {
            get { return DefaultDescription; }
        }

        [DataMember] public int KeyOrdinal { get; set; }
        [DataMember] public int PartitionOrdinal { get; set; }
        [DataMember] public bool IsDescendingKey { get; set; }
        [DataMember] public bool IsIncludedColumn { get; set; }

        public PrimaryKeyColumn()
        {
            IsDescendingKey = false;
            KeyOrdinal = 1;
            PartitionOrdinal = 0;
        }

        //public static bool CompareDefinitions(DataContext sourceDataContext, DataContext targetDataContext, PrimaryKeyColumn sourcePrimaryKeyColumn, PrimaryKeyColumn targetPrimaryKeyColumn)
        //{
        //    return CompareObjectNames(sourcePrimaryKeyColumn, targetPrimaryKeyColumn)
        //        && DataIndexColumns.CompareDataIndexColumn(sourceDataContext, targetDataContext, sourcePrimaryKeyColumn, targetPrimaryKeyColumn);
        //}

        //public static bool CompareObjectNames(PrimaryKeyColumn sourcePrimaryKeyColumn, PrimaryKeyColumn targetPrimaryKeyColumn, StringComparer stringComparer = null)
        //{
        //    if (stringComparer == null)
        //        stringComparer = StringComparer.OrdinalIgnoreCase;

        //    return stringComparer.Compare(sourcePrimaryKeyColumn.ObjectName, targetPrimaryKeyColumn.ObjectName) == 0;
        //}
    }
}
