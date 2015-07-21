using System.Runtime.Serialization;
using Meta.Net.Interfaces;
using Meta.Net.Objects;

namespace Meta.Net.Abstract
{
    [DataContract]
    [KnownType(typeof(Catalog))]
    public abstract class ServerBasedObject : BaseMetaObject
    {
        [DataMember] public virtual Server Server { get; set; }

        public override string Namespace
        {
            get { return ObjectName; }
        }

        /// <summary>
        /// This data object can only have a Server as a parent.
        /// </summary>
        public override IMetaObject ParentMetaObject
        {
            get { return Server; }
            set { Server = value as Server; }
        }

        /// <summary>
        /// This data object can only have a Server as a parent.
        /// </summary>
        /// <param name="metaObject">The data object wished to be assigned as the parent.</param>
        /// <returns>True if the dataObject is a Server.</returns>
        public override bool CanBeAssignedParentMetaObject(IMetaObject metaObject)
        {
            return metaObject is Server;
        }
    }
}
