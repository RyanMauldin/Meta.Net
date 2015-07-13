using System.Text;
using Meta.Net.Interfaces;
using Meta.Net.Objects;

namespace Meta.Net.Abstract
{
    public abstract class PrimaryKeyBasedObject : UserTableBasedObject
    {
        public override UserTable UserTable
        {
            get
            {
                return PrimaryKey == null
                    ? null
                    : PrimaryKey.UserTable;
            }
        }

        public virtual PrimaryKey PrimaryKey { get; set; }

        public override string Namespace
        {
            get
            {
                var objectName = ObjectName;
                var primaryKey = PrimaryKey;
                var primaryKeyObjectName = primaryKey.ObjectName;
                var userTable = primaryKey.UserTable;
                var userTableObjectName = userTable.ObjectName;
                var schema = userTable.Schema;
                var schemaObjectName = schema.ObjectName;

                var builder = new StringBuilder(schemaObjectName.Length + userTableObjectName.Length + primaryKeyObjectName.Length + objectName.Length + 3);
                builder.Append(schemaObjectName).
                    Append(Constants.Dot).
                    Append(userTableObjectName).
                    Append(Constants.Dot).
                    Append(primaryKeyObjectName).
                    Append(Constants.Dot).
                    Append(objectName);
                return builder.ToString();
            }
        }

        /// <summary>
        /// This data object must be a Primary Key.
        /// </summary>
        public override IMetaObject ParentMetaObject
        {
            get { return PrimaryKey; }
            set { PrimaryKey = value as PrimaryKey; }
        }

        /// <summary>
        /// This data object can only have a Primary Key as a parent.
        /// </summary>
        /// <param name="metaObject">The data object wished to be assigned as the parent.</param>
        /// <returns>True if the dataObject is a Primary Key.</returns>
        public override bool CanBeAssignedParentMetaObject(IMetaObject metaObject)
        {
            return metaObject is PrimaryKey;
        }
    }
}
