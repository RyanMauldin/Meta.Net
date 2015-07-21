using System.Runtime.Serialization;
using Meta.Net.Abstract;

namespace Meta.Net.Objects
{
    [DataContract]
    public class IdentityColumn : UserTableBasedObject
    {
        public static readonly string DefaultDescription = "Identity Column";

        public override string Description
        {
            get { return DefaultDescription; }
        }

        [DataMember] public int IncrementValue { get; set; }
        [DataMember] public bool IsNotForReplication { get; set; }
        [DataMember] public int SeedValue { get; set; }

		public IdentityColumn()
        {
            SeedValue = 1;
            IncrementValue = 1;
            IsNotForReplication = false;
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
    }
}
