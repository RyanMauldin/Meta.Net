using System;
using Meta.Net.Interfaces;

namespace Meta.Net.Abstract
{
    public abstract class BaseMetaObject : IMetaObject
    {
        public readonly static string Dot = ".";
        public string ObjectName { get; set; }
        public abstract string Description { get; }
        public abstract string Namespace { get; }
        public abstract IMetaObject ParentMetaObject { get; set; }
        public abstract bool CanBeAssignedParentMetaObject(IMetaObject metaObject);
        public abstract IMetaObject DeepClone();
        public abstract IMetaObject ShallowClone();

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
