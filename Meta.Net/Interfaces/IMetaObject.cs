//using System.Runtime.Serialization;

namespace Meta.Net.Interfaces
{
    public interface IMetaObject //: ISerializable
    {
        string ObjectName { get; set; }
        string Description { get; }
        string Namespace { get; }
        IMetaObject ParentMetaObject { get; set; }
        bool CanBeAssignedParentMetaObject(IMetaObject metaObject);
        IMetaObject DeepClone();
        IMetaObject ShallowClone();
    }
}
