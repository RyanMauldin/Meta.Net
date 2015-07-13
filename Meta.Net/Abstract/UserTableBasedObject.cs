using System.Text;
using Meta.Net.Interfaces;
using Meta.Net.Objects;

namespace Meta.Net.Abstract
{
    public abstract class UserTableBasedObject : SchemaBasedObject
    {
        public override Schema Schema
        {
            get
            {
                return UserTable == null
                    ? null
                    : UserTable.Schema;
            }
        }

        public virtual UserTable UserTable { get; set; }

        public override string Namespace
        {
            get
            {
                var objectName = ObjectName;
                var userTable = UserTable;
                var userTableObjectName = userTable.ObjectName;
                var schema = userTable.Schema;
                var schemaObjectName = schema.ObjectName;

                var builder = new StringBuilder(schemaObjectName.Length + userTableObjectName.Length + objectName.Length + 2);
                builder.Append(schemaObjectName).
                    Append(Constants.Dot).
                    Append(userTableObjectName).
                    Append(Constants.Dot).
                    Append(objectName);
                return builder.ToString();
            }
        }

        /// <summary>
        /// This data object must be a User-Table.
        /// </summary>
        public override IMetaObject ParentMetaObject
        {
            get { return UserTable; }
            set { UserTable = value as UserTable; }
        }

        /// <summary>
        /// This data object can only have a User-Table as a parent.
        /// </summary>
        /// <param name="metaObject">The data object wished to be assigned as the parent.</param>
        /// <returns>True if the dataObject is a User-Table.</returns>
        public override bool CanBeAssignedParentMetaObject(IMetaObject metaObject)
        {
            return metaObject is UserTable;
        }
    }
}
