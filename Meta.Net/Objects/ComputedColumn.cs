using System.Runtime.Serialization;
using Meta.Net.Abstract;

namespace Meta.Net.Objects
{
    [DataContract]
    public class ComputedColumn : UserTableBasedObject
    {
        public static readonly string DefaultDescription = "Computed Column";

        public override string Description
        {
            get { return DefaultDescription; }
        }

        [DataMember] public string Definition { get; set; }
        [DataMember] public bool IsNullable { get; set; }
        [DataMember] public bool IsPersisted { get; set; }

        public ComputedColumn()
        {
            Definition = string.Empty;
            IsPersisted = false;
            IsNullable = true;
        }

        //public static bool CompareObjectNames(ComputedColumn sourceComputedColumn, ComputedColumn targetComputedColumn,
        //    StringComparer stringComparer = null)
        //{
        //    if (stringComparer == null)
        //        stringComparer = StringComparer.OrdinalIgnoreCase;

        //    return stringComparer.Compare(sourceComputedColumn.ObjectName, targetComputedColumn.ObjectName) == 0;
        //}

        //public static bool CompareDefinitions(ComputedColumn sourceComputedColumn, ComputedColumn targetComputedColumn)
        //{
        //    if (!CompareObjectNames(sourceComputedColumn, targetComputedColumn))
        //        return false;

        //    if (sourceComputedColumn.IsPersisted != targetComputedColumn.IsPersisted)
        //        return false;

        //    return DataProperties.DefinitionComparer.Compare(
        //        sourceComputedColumn.Definition, targetComputedColumn.Definition) == 0;
        //}
    }
}
