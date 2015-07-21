using System.Runtime.Serialization;
using System.Text;
using Meta.Net.Interfaces;
using Meta.Net.Objects;

namespace Meta.Net.Abstract
{
    [DataContract]
    [KnownType(typeof(IndexColumn))]
    public abstract class IndexBasedObject : UserTableBasedObject
    {
        public override UserTable UserTable
        {
            get
            {
                return Index == null
                    ? null
                    : Index.UserTable;
            }
        }

        [DataMember] public virtual Index Index { get; set; }

        public override string Namespace
        {
            get
            {
                var objectName = ObjectName;
                var index = Index;
                var indexObjectName = index.ObjectName;
                var userTable = index.UserTable;
                var userTableObjectName = userTable.ObjectName;
                var schema = userTable.Schema;
                var schemaObjectName = schema.ObjectName;

                var builder = new StringBuilder(schemaObjectName.Length + userTableObjectName.Length + indexObjectName.Length + objectName.Length + 3);
                builder.Append(schemaObjectName).
                    Append(Constants.Dot).
                    Append(userTableObjectName).
                    Append(Constants.Dot).
                    Append(indexObjectName).
                    Append(Constants.Dot).
                    Append(objectName);
                return builder.ToString();
            }
        }

        /// <summary>
        /// This data object must be an Index.
        /// </summary>
        public override IMetaObject ParentMetaObject
        {
            get { return Index; }
            set { Index = value as Index; }
        }

        /// <summary>
        /// This data object can only have an Index as a parent.
        /// </summary>
        /// <param name="metaObject">The data object wished to be assigned as the parent.</param>
        /// <returns>True if the dataObject is a Index.</returns>
        public override bool CanBeAssignedParentMetaObject(IMetaObject metaObject)
        {
            return metaObject is Index;
        }
    }
}
