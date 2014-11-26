using Meta.Net.Abstract;
using Meta.Net.Interfaces;

namespace Meta.Net.Objects
{
    //[Serializable]
    public class IdentityColumn : UserTableBasedObject
    {
        public static readonly string DefaultDescription = "Identity Column";

        public override string Description
        {
            get { return DefaultDescription; }
        }

        public int IncrementValue { get; set; }
        public bool IsNotForReplication { get; set; }
        public int SeedValue { get; set; }

		public IdentityColumn()
        {
            SeedValue = 1;
            IncrementValue = 1;
            IsNotForReplication = false;
        }

        public override IMetaObject DeepClone()
        {
            return new IdentityColumn
            {
                ObjectName = ObjectName,
                SeedValue = SeedValue,
                IncrementValue = IncrementValue,
                IsNotForReplication = IsNotForReplication
            };
        }

        public override IMetaObject ShallowClone()
        {
            return new IdentityColumn
            {
                ObjectName = ObjectName,
                SeedValue = SeedValue,
                IncrementValue = IncrementValue,
                IsNotForReplication = IsNotForReplication
            };
        }

		//public static bool CompareDefinitions(IdentityColumn sourceIdentityColumn, IdentityColumn targetIdentityColumn)
        //{
        //    if (!CompareObjectNames(sourceIdentityColumn, targetIdentityColumn))
        //        return false;

        //    if (sourceIdentityColumn.IncrementValue != targetIdentityColumn.IncrementValue)
        //        return false;

        //    if (sourceIdentityColumn.IsNotForReplication != targetIdentityColumn.IsNotForReplication)
        //        return false;

        //    return sourceIdentityColumn.SeedValue == targetIdentityColumn.SeedValue;
        //}

        //public void GetObjectData(SerializationInfo info, StreamingContext context)
        //{
        //    // Holding off on the serialzation in this manner because, this is
        //    // extremely complicated to do in this manner do to data object
        //    // associations, especially
        //    // Serialize Members
        //    info.AddValue("ObjectName", ObjectName);
        //    info.AddValue("Description", Description);
        //    info.AddValue("IncrementValue", IncrementValue);
        //    info.AddValue("IsNotForReplication", IsNotForReplication);
        //    info.AddValue("SeedValue", SeedValue);
        //}

        //public IdentityColumn(SerializationInfo info, StreamingContext context)
        //{
        //    // Holding off on the serialzation in this manner because, this is
        //    // extremely complicated to do in this manner do to data object
        //    // associations, especially
        //    // Set Null Members
        //    UserTable = null;

        //    // Deserialize Members
        //    ObjectName = info.GetString("ObjectName");
        //    Description = info.GetString("Description");
        //    IncrementValue = info.GetInt32("IncrementValue");
        //    IsNotForReplication = info.GetBoolean("IsNotForReplication");
        //    SeedValue = info.GetInt32("SeedValue");
        //}

        //public static IdentityColumn FromJson(string json)
        //{
        //    return JsonConvert.DeserializeObject<IdentityColumn>(json);
        //}

        //public static string ToJson(IdentityColumn identityColumn, Formatting formatting = Formatting.Indented)
        //{
        //    return JsonConvert.SerializeObject(identityColumn, formatting);
        //}
    }
}
