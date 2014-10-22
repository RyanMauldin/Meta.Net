using System;
using Meta.Net.Abstract;

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

        private static void Init(ComputedColumn computedColumn, UserTable userTable, string objectName)
        {
            computedColumn.UserTable = userTable;
            computedColumn.ObjectName = GetDefaultObjectName(computedColumn, objectName);
            computedColumn.Definition = "";
            computedColumn.IsPersisted = false;
            computedColumn.IsNullable = true;
        }

        public ComputedColumn(UserTable userTable, string objectName)
        {
            Init(this, userTable, objectName);
        }

        public ComputedColumn()
        {
            
        }

        /// <summary>
        /// Deep Clone and Shallow Clone... Leaf Node.
        /// A clone of this class's isntance specific metadata.
        /// </summary>
        /// <param name="computedColumn">The computed column to clone.</param>
        /// <returns>A clone of this class's isntance specific metadata.</returns>
        public static ComputedColumn Clone(ComputedColumn computedColumn)
        {
            return new ComputedColumn
            {
                ObjectName = computedColumn.ObjectName,
                Definition = computedColumn.Definition,
                IsPersisted = computedColumn.IsPersisted,
                IsNullable = computedColumn.IsNullable
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
