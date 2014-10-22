using System.Text;
using Meta.Net.Interfaces;
using Meta.Net.Objects;

namespace Meta.Net.Abstract
{
    public abstract class UniqueConstraintBasedObject : UserTableBasedObject
    {
        public override UserTable UserTable
        {
            get
            {
                return UniqueConstraint == null
                    ? null
                    : UniqueConstraint.UserTable;
            }
        }

        public virtual UniqueConstraint UniqueConstraint { get; set; }

        public override string Namespace
        {
            get
            {
                var objectName = ObjectName;
                var uniqueConstraint = UniqueConstraint;
                var uniqueConstraintObjectName = uniqueConstraint.ObjectName;
                var userTable = uniqueConstraint.UserTable;
                var userTableObjectName = userTable.ObjectName;
                var schema = userTable.Schema;
                var schemaObjectName = schema.ObjectName;

                var builder = new StringBuilder(schemaObjectName.Length + userTableObjectName.Length + uniqueConstraintObjectName.Length + objectName.Length + 3);
                builder.Append(schemaObjectName).Append(Dot).Append(userTableObjectName).Append(Dot).Append(uniqueConstraintObjectName).Append(Dot).Append(objectName);
                return builder.ToString();
            }
        }

        /// <summary>
        /// This data object must be a Unique Constraint.
        /// </summary>
        public override IMetaObject ParentMetaObject
        {
            get { return UniqueConstraint; }
            set { UniqueConstraint = value as UniqueConstraint; }
        }

        /// <summary>
        /// This data object can only have a Unique Constraint as a parent.
        /// </summary>
        /// <param name="metaObject">The data object wished to be assigned as the parent.</param>
        /// <returns>True if the dataObject is a Unique Constraint.</returns>
        public override bool CanBeAssignedParentMetaObject(IMetaObject metaObject)
        {
            return metaObject is UniqueConstraint;
        }
    }
}
