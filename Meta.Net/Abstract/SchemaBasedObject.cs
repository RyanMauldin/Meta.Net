using System.Text;
using Meta.Net.Interfaces;
using Meta.Net.Objects;

namespace Meta.Net.Abstract
{
    public abstract class SchemaBasedObject : CatalogBasedObect
    {
        public override Catalog Catalog
        {
            get
            {
                return Schema == null
                    ? null
                    : Schema.Catalog;
            }
        }

        public virtual Schema Schema { get; set; }

        public override string Namespace
        {
            get
            {
                var objectName = ObjectName;
                var schema = Schema;
                var schemaObjectName = schema.ObjectName;

                var builder = new StringBuilder(schemaObjectName.Length + objectName.Length + 1);
                builder.Append(schemaObjectName).Append(Dot).Append(objectName);
                return builder.ToString();
            }
        }

        /// <summary>
        /// This data object must be a Schema.
        /// </summary>
        public override IMetaObject ParentMetaObject
        {
            get { return Schema; }
            set { Schema = value as Schema; }
        }

        /// <summary>
        /// This data object can only have a Schema as a parent.
        /// </summary>
        /// <param name="metaObject">The data object wished to be assigned as the parent.</param>
        /// <returns>True if the dataObject is a Schema.</returns>
        public override bool CanBeAssignedParentMetaObject(IMetaObject metaObject)
        {
            return metaObject is Schema;
        }
    }
}
