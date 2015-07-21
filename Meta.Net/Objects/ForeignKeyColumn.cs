using System;
using System.Runtime.Serialization;
using Meta.Net.Abstract;
using Meta.Net.Interfaces;

namespace Meta.Net.Objects
{
    [DataContract]
    public class ForeignKeyColumn : ForeignKeyBasedObject
    {
        [DataMember(Name = "ReferencedColumn")]
        private IIndexColumn _referencedColumn;

        public static readonly string DefaultDescription = "Foreign Key Column";

        public override string Description
        {
            get { return DefaultDescription; }
        }

        [DataMember] public override UserTable ReferencedUserTable { get; set; }

        [DataMember] public int KeyOrdinal { get; set; }

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
    }
}
