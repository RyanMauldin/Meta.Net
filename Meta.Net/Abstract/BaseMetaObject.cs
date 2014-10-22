using System;
using System.Text;
using Meta.Net.Interfaces;

namespace Meta.Net.Abstract
{
    public abstract class BaseMetaObject : IMetaObject
    {
        public static string Dot = ".";
        public string ObjectName { get; set; }
        public abstract string Description { get; }
        public abstract string Namespace { get; }
        public abstract IMetaObject ParentMetaObject { get; set; }
        public abstract bool CanBeAssignedParentMetaObject(IMetaObject metaObject);

        public static bool CompareObjectNames(IMetaObject sourceDataObject, IMetaObject targetDataObject, StringComparer stringComparer = null)
        {
            if (stringComparer == null)
                stringComparer = StringComparer.OrdinalIgnoreCase;

            return stringComparer.Compare(sourceDataObject.ObjectName, targetDataObject.ObjectName) == 0;
        }

        public static string GetDefaultObjectName(IMetaObject dataObject, string objectName)
        {
            if (!string.IsNullOrEmpty(objectName))
                return objectName;

            var description = dataObject.Description;
            var guid = Guid.NewGuid().ToString();
            var builder = new StringBuilder(description.Length + guid.Length);
            builder.Append(description).Append(guid).Replace(" ", "").Replace("-", "");
            return builder.ToString();
        }
    }
}
