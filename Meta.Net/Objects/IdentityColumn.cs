using System;
using Meta.Net.Abstract;

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

		private static void Init(IdentityColumn identityColumn, UserTable userTable, string objectName)
        {
            identityColumn.UserTable = userTable;
		    identityColumn.ObjectName = GetDefaultObjectName(identityColumn, objectName);
            identityColumn.SeedValue = 1;
            identityColumn.IncrementValue = 1;
            identityColumn.IsNotForReplication = false;
        }

        public IdentityColumn(UserTable userTable, string objectName)
        {
            Init(this, userTable, objectName);
        }

        public IdentityColumn()
        {
            
        }

		/// <summary>
        /// Deep Clone and Shallow Clone... Leaf Node.
        /// A clone of this class's instance specific metadata.
        /// </summary>
        /// <param name="identityColumn">The identity column to clone.</param>
        /// <returns>A clone of this class's instance specific metadata.</returns>
        public static IdentityColumn Clone(IdentityColumn identityColumn)
        {
            return new IdentityColumn
            {
                ObjectName = identityColumn.ObjectName,
                SeedValue = identityColumn.SeedValue,
                IncrementValue = identityColumn.IncrementValue,
                IsNotForReplication = identityColumn.IsNotForReplication
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

        //public static bool CompareObjectNames(IdentityColumn sourceIdentityColumn, IdentityColumn targetIdentityColumn, StringComparer stringComparer = null)
        //{
        //    if (stringComparer == null)
        //        stringComparer = StringComparer.OrdinalIgnoreCase;

        //    return stringComparer.Compare(sourceIdentityColumn.ObjectName, targetIdentityColumn.ObjectName) == 0;
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
