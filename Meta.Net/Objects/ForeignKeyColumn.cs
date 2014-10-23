using System;
using Meta.Net.Abstract;
using Meta.Net.Interfaces;

namespace Meta.Net.Objects
{
    //[Serializable]
    public class ForeignKeyColumn : ForeignKeyBasedObject
    {
        private IIndexColumn _referencedColumn;

        public static readonly string DefaultDescription = "Foreign Key Column";

        public override string Description
        {
            get { return DefaultDescription; }
        }

        public override UserTable ReferencedUserTable { get; set; }

        public int KeyOrdinal { get; set; }

        public IIndexColumn ReferencedColumn
        {
            get { return _referencedColumn; }
            set
            {
                if (value == null)
                {
                    _referencedColumn = null;
                    return;
                }

                if (!(value is PrimaryKeyColumn || value is UniqueConstraintColumn))
                    throw new Exception(string.Format("Referenced Column is not a Primary Key or Unique Constraint for {0}.{1}", value.UserTable, value.ObjectName));

                if (!ReferencedUserTable.Equals(value.UserTable))
                    throw new Exception(string.Format("Referenced User Table is not correct for Referenced Column {0}.{1}; Note to developers, the assigned Reference Column may not contain the same referenced user table object instance as assigned on foreign key and should be.", value.UserTable, value.ObjectName));

                _referencedColumn = value;
            }
        }

        private static void Init(ForeignKeyColumn foreignKeyColumn, ForeignKey foreignKey, string objectName)
        {
            foreignKeyColumn.ForeignKey = foreignKey;
            foreignKeyColumn.ObjectName = GetDefaultObjectName(foreignKeyColumn, objectName);
            // TODO: foreignKeyColumn.ReferencedColumnName = "";
        }

        public ForeignKeyColumn(ForeignKey foreignKey, string objectName)
        {
            Init(this, foreignKey, objectName);
        }

        public ForeignKeyColumn()
        {
            
        }

        /// <summary>
        /// Deep Clone and Shallow Clone... Leaf Node.
        /// A clone of this class's instance specific metadata.
        /// </summary>
        /// <param name="foreignKeyColumn">The foreign key column to clone.</param>
        /// <returns>A clone of this class's instance specific metadata.</returns>
        public static ForeignKeyColumn Clone(ForeignKeyColumn foreignKeyColumn)
        {
            return new ForeignKeyColumn
            {
                ObjectName = foreignKeyColumn.ObjectName,
                KeyOrdinal = foreignKeyColumn.KeyOrdinal,
                ReferencedUserTable = foreignKeyColumn.ReferencedUserTable 
                //TODO: ReferencedColumnName = foreignKeyColumn.ReferencedColumnName
            };
        }

        //public static bool CompareDefinitions(ForeignKeyColumn sourceForeignKeyColumn, ForeignKeyColumn targetForeignKeyColumn)
        //{
        //    if (!CompareObjectNames(sourceForeignKeyColumn, targetForeignKeyColumn))
        //        return false;

        //    if (sourceForeignKeyColumn.KeyOrdinal != targetForeignKeyColumn.KeyOrdinal)
        //        return false;

        //    return StringComparer.OrdinalIgnoreCase.Compare(
        //        sourceForeignKeyColumn.ReferencedColumn.ObjectName, targetForeignKeyColumn.ReferencedColumn.ObjectName) == 0;
        //}

        //public static bool CompareObjectNames(ForeignKeyColumn sourceForeignKeyColumn, ForeignKeyColumn targetForeignKeyColumn, StringComparer stringComparer = null)
        //{
        //    if (stringComparer == null)
        //        stringComparer = StringComparer.OrdinalIgnoreCase;

        //    return stringComparer.Compare(sourceForeignKeyColumn.ObjectName, targetForeignKeyColumn.ObjectName) == 0;
        //}

        //public static ForeignKeyColumn FromJson(string json)
        //{
        //    return JsonConvert.DeserializeObject<ForeignKeyColumn>(json);
        //}

        //public void GetObjectData(SerializationInfo info, StreamingContext context)
        //{
        //    // Holding off on the serialzation in this manner because, this is
        //    // extremely complicated to do in this manner do to data object
        //    // associations, especially
        //    // Serialize Members
        //    info.AddValue("ObjectName", ObjectName);
        //    info.AddValue("Description", Description);
        //    info.AddValue("KeyOrdinal", KeyOrdinal);
        //    //TODO: Serialize info.AddValue("ReferencedColumnName", ReferencedColumnName);
        //}

        //public static string ToJson(ForeignKeyColumn foreignKeyColumn, Formatting formatting = Formatting.Indented)
        //{
        //    return JsonConvert.SerializeObject(foreignKeyColumn, formatting);
        //}

        //public ForeignKeyColumn(SerializationInfo info, StreamingContext context)
        //{
        //    // Holding off on the serialzation in this manner because, this is
        //    // extremely complicated to do in this manner do to data object
        //    // associations, especially
        //    // Set Null Members
        //    ForeignKey = null;

        //    // Deserialize Members
        //    ObjectName = info.GetString("ObjectName");
        //    Description = info.GetString("Description");
        //    KeyOrdinal = info.GetInt32("KeyOrdinal");
        //    // TODO: References need to be deserialized, with usertables in syc
        //}
    }
}
