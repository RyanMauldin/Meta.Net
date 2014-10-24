using Meta.Net.Abstract;
using Meta.Net.Interfaces;

namespace Meta.Net.Objects
{
    //[Serializable]
    public class PrimaryKeyColumn : PrimaryKeyBasedObject, IIndexColumn
    {
        public static readonly string DefaultDescription = "Primary Key Column";

        public override string Description
        {
            get { return DefaultDescription; }
        }

        public int KeyOrdinal { get; set; }
        public int PartitionOrdinal { get; set; }
        public bool IsDescendingKey { get; set; }
        public bool IsIncludedColumn { get; set; }

        public PrimaryKeyColumn()
        {
            IsDescendingKey = false;
            KeyOrdinal = 1;
            PartitionOrdinal = 0;
        }

        public override IMetaObject DeepClone()
        {
            return new PrimaryKeyColumn
            {
                ObjectName = ObjectName == null ? null : string.Copy(ObjectName),
                KeyOrdinal = KeyOrdinal,
                PartitionOrdinal = PartitionOrdinal
            };
        }

        public override IMetaObject ShallowClone()
        {
            return new PrimaryKeyColumn
            {
                ObjectName = ObjectName,
                KeyOrdinal = KeyOrdinal,
                PartitionOrdinal = PartitionOrdinal
            };
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

        //public PrimaryKeyColumn(SerializationInfo info, StreamingContext context)
        //{
        //    // Holding off on the serialzation in this manner because, this is
        //    // extremely complicated to do in this manner do to data object
        //    // associations, especially
        //    // Set Null Members
        //    PrimaryKey = null;

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

        //public static PrimaryKeyColumn FromJson(string json)
        //{
        //    return JsonConvert.DeserializeObject<PrimaryKeyColumn>(json);
        //}

        //public static string ToJson(PrimaryKeyColumn primaryKeyColumn
        //    , Formatting formatting = Formatting.Indented)
        //{
        //    return JsonConvert.SerializeObject(primaryKeyColumn, formatting);
        //}
    }
}
