using System;
using System.Linq;
using System.Text;
using Meta.Net.Interfaces;
using Meta.Net.Objects;

namespace Meta.Net.Abstract
{
    public abstract class ForeignKeyBasedObject : UserTableBasedObject
    {
        public override UserTable UserTable
        {
            get
            {
                return ForeignKey == null
                    ? null
                    : ForeignKey.UserTable;
            }
        }

        public virtual UserTable ReferencedUserTable
        {
            get
            {
                if (ForeignKeyColumns == null || ForeignKeyColumns.Count == 0)
                    return null;

                var foreignKeyColumn = ForeignKeyColumns.FirstOrDefault();
                return foreignKeyColumn == null
                    ? null
                    : foreignKeyColumn.UserTable;
            }
            set
            {
                throw new Exception("Referenced User-Table is obtained through Foreign Key Column.");
            }
        }

        public virtual ForeignKey ForeignKey { get; set; }

        public virtual DataObjectLookup<ForeignKey, ForeignKeyColumn> ForeignKeyColumns
        {
            get
            {
                return ForeignKey == null
                    ? null
                    : ForeignKey.ForeignKeyColumns;
            }
        }

        public override string Namespace
        {
            get
            {
                var objectName = ObjectName;
                var foreignKey = ForeignKey;
                var foreignKeyObjectName = foreignKey.ObjectName;
                var userTable = foreignKey.UserTable;
                var userTableObjectName = userTable.ObjectName;
                var schema = userTable.Schema;
                var schemaObjectName = schema.ObjectName;

                var builder = new StringBuilder(schemaObjectName.Length + userTableObjectName.Length + foreignKeyObjectName.Length + objectName.Length + 3);
                builder.Append(schemaObjectName).
                    Append(Constants.Dot).
                    Append(userTableObjectName).
                    Append(Constants.Dot).
                    Append(foreignKeyObjectName).
                    Append(Constants.Dot).
                    Append(objectName);
                return builder.ToString();
            }
        }

        /// <summary>
        /// This data object must be a Foreign Key.
        /// </summary>
        public override IMetaObject ParentMetaObject
        {
            get { return ForeignKey; }
            set { ForeignKey = value as ForeignKey; }
        }

        /// <summary>
        /// This data object can only have a Foreign Key as a parent.
        /// </summary>
        /// <param name="metaObject">The data object wished to be assigned as the parent.</param>
        /// <returns>True if the dataObject is a Foreign Key.</returns>
        public override bool CanBeAssignedParentMetaObject(IMetaObject metaObject)
        {
            return metaObject is ForeignKey;
        }
    }
}
