namespace Meta.Net.Interfaces
{
    public interface IMetaObject
    {
        string ObjectName { get; set; }
        string Description { get; }
        string Namespace { get; }
        IMetaObject ParentMetaObject { get; set; }
        bool CanBeAssignedParentMetaObject(IMetaObject metaObject);
    }
}
