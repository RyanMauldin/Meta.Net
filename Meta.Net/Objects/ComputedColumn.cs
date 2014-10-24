using Meta.Net.Abstract;
using Meta.Net.Interfaces;

namespace Meta.Net.Objects
{
    //[Serializable]
    public class ComputedColumn : UserTableBasedObject
    {
        public static readonly string DefaultDescription = "Computed Column";

        public override string Description
        {
            get { return DefaultDescription; }
        }

        public string Definition { get; set; }
        public bool IsNullable { get; set; }
        public bool IsPersisted { get; set; }

        public ComputedColumn()
        {
            Definition = string.Empty;
            IsPersisted = false;
            IsNullable = true;
        }

        public override IMetaObject DeepClone()
        {
            return new ComputedColumn
            {
                ObjectName = ObjectName == null ? null : string.Copy(ObjectName),
                Definition = Definition == null ? null : string.Copy(Definition),
                IsPersisted = IsPersisted,
                IsNullable = IsNullable
            };
        }

        public override IMetaObject ShallowClone()
        {
            return new ComputedColumn
            {
                ObjectName = ObjectName,
                Definition = Definition,
                IsPersisted = IsPersisted,
                IsNullable = IsNullable
            };
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

        //public ComputedColumn(SerializationInfo info, StreamingContext context)
        //{
        //    // Holding off on the serialzation in this manner because, this is
        //    // extremely complicated to do in this manner do to data object
        //    // associations, especially
        //    // Set Null Members
        //    UserTable = null;

        //    // Deserialize Members
        //    ObjectName = info.GetString("ObjectName");
        //    Description = info.GetString("Description");
        //    Definition = info.GetString("Definition");
        //    IsNullable = info.GetBoolean("IsNullable");
        //    IsPersisted = info.GetBoolean("IsPersisted");
        //}

        //public void GetObjectData(SerializationInfo info, StreamingContext context)
        //{
        //    // Holding off on the serialzation in this manner because, this is
        //    // extremely complicated to do in this manner do to data object
        //    // associations, especially
        //    // Serialize Members
        //    info.AddValue("ObjectName", ObjectName);
        //    info.AddValue("Description", Description);
        //    info.AddValue("Definition", Definition);
        //    info.AddValue("IsNullable", IsNullable);
        //    info.AddValue("IsPersisted", IsPersisted);
        //}

        //public static ComputedColumn FromJson(string json)
        //{
        //    return JsonConvert.DeserializeObject<ComputedColumn>(json);
        //}

        //public static string ToJson(ComputedColumn computedColumn, Formatting formatting = Formatting.Indented)
        //{
        //    return JsonConvert.SerializeObject(computedColumn, formatting);
        //}
    }
}
