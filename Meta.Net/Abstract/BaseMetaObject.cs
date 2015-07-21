using System;
using System.Runtime.Serialization;
using Meta.Net.Interfaces;
using Meta.Net.Objects;

namespace Meta.Net.Abstract
{
    [DataContract]
    [KnownType(typeof(BaseModule))]
    [KnownType(typeof(CatalogBasedObect))]
    [KnownType(typeof(ForeignKeyBasedObject))]
    [KnownType(typeof(IndexBasedObject))]
    [KnownType(typeof(PrimaryKeyBasedObject))]
    [KnownType(typeof(SchemaBasedObject))]
    [KnownType(typeof(ServerBasedObject))]
    [KnownType(typeof(UniqueConstraintBasedObject))]
    [KnownType(typeof(UserTableBasedObject))]
    [KnownType(typeof(AggregateFunction))]
    [KnownType(typeof(Catalog))]
    [KnownType(typeof(CheckConstraint))]
    [KnownType(typeof(ComputedColumn))]
    [KnownType(typeof(DefaultConstraint))]
    [KnownType(typeof(ForeignKey))]
    [KnownType(typeof(ForeignKeyColumn))]
    [KnownType(typeof(IdentityColumn))]
    [KnownType(typeof(Index))]
    [KnownType(typeof(IndexColumn))]
    [KnownType(typeof(InlineTableValuedFunction))]
    [KnownType(typeof(PrimaryKey))]
    [KnownType(typeof(PrimaryKeyColumn))]
    [KnownType(typeof(ScalarFunction))]
    [KnownType(typeof(Schema))]
    [KnownType(typeof(Server))]
    [KnownType(typeof(StoredProcedure))]
    [KnownType(typeof(TableValuedFunction))]
    [KnownType(typeof(Trigger))]
    [KnownType(typeof(UniqueConstraint))]
    [KnownType(typeof(UniqueConstraintColumn))]
    [KnownType(typeof(UserDefinedDataType))]
    [KnownType(typeof(UserTable))]
    [KnownType(typeof(UserTableColumn))]
    [KnownType(typeof(View))]
    public abstract class BaseMetaObject : IMetaObject
    {
        [DataMember(IsRequired = true)]
        public string ObjectName { get; set; }
        public abstract string Description { get; }
        public abstract string Namespace { get; }
        public abstract IMetaObject ParentMetaObject { get; set; }
        public abstract bool CanBeAssignedParentMetaObject(IMetaObject metaObject);

        protected BaseMetaObject()
        {
            ObjectName = string.Empty;
        }

        public static bool CompareObjectNames(IMetaObject sourceDataObject, IMetaObject targetDataObject, StringComparer stringComparer = null)
        {
            if (stringComparer == null)
                stringComparer = StringComparer.OrdinalIgnoreCase;

            return stringComparer.Compare(sourceDataObject.ObjectName, targetDataObject.ObjectName) == 0;
        }
    }
}
