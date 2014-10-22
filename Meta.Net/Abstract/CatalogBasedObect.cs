using Meta.Net.Interfaces;
using Meta.Net.Objects;

namespace Meta.Net.Abstract
{
    public abstract class CatalogBasedObect : ServerBasedObject
    {
        public override Server Server
        {
            get
            {
                return Catalog == null
                    ? null
                    : Catalog.Server;
            }
        }

        public virtual Catalog Catalog { get; set; }

        /// <summary>
        /// This data object can only have a Catalog as a parent.
        /// </summary>
        public override IMetaObject ParentMetaObject
        {
            get { return Catalog; }
            set { Catalog = value as Catalog; }
        }

        /// <summary>
        /// This data object can only have a Server as a parent.
        /// </summary>
        /// <param name="metaObject">The data object wished to be assigned as the parent.</param>
        /// <returns>True if the dataObject is a Server.</returns>
        public override bool CanBeAssignedParentMetaObject(IMetaObject metaObject)
        {
            return metaObject is Catalog;
        }
    }
}
