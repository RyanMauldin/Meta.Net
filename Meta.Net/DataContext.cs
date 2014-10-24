using System.Linq;
using System.Text;
using Meta.Net.Types;
using Meta.Net.Interfaces;

namespace Meta.Net
{
    /// <summary>
    /// Supported universal data server context specific properties and methods.
    /// </summary>
    public abstract class DataContext
    {
		public abstract DataContextType ContextType { get; }

        public abstract string DefaultSchemaName { get; }

        public abstract string Delimiter { get; }

        public abstract string IdentifierCloseChar { get; }

        public abstract string IdentifierOpenChar { get; }

        public abstract bool IncludeSchemaNameInNamespace { get; }

        public abstract int MaxObjectNameLength { get; }

        public abstract int MaxAliasLength { get; }

        public abstract int MaxTempTableNameLength { get; }
        
        //public IMetaScriptFactory ScriptFactory
        //{
        //    get
        //    {
        //        return _scriptFactory
        //            ?? (_scriptFactory = CreateScriptFactory());
        //    }
        //    set { _scriptFactory = value; }
        //}

        //private IMetaScriptFactory _scriptFactory;

        public static string ConvertIdentifier(DataContext fromContext, DataContext toContext, string nameSpace)
        {
            var parts = nameSpace.Replace(fromContext.IdentifierOpenChar, "")
                                 .Replace(fromContext.IdentifierCloseChar, "")
                                 .Split('.');
            
            if (!toContext.IncludeSchemaNameInNamespace)
            {
                return toContext.CreateIdentifier(parts[0], parts[parts.Length < 3 ? 1 : 2]);
            }
            
            return parts.Length < 3 ? toContext.CreateIdentifier(parts[0], toContext.DefaultSchemaName, parts[1]) : toContext.CreateIdentifier(parts);
        }

        public string CreateIdentifier(params IMetaObject[] names)
        {
            var builder = new StringBuilder();

            foreach (var name in names)
            {
                builder.Append(IdentifierOpenChar);
                builder.Append(name.ObjectName);
                builder.Append(IdentifierCloseChar);
                if (name != names.Last())
                    builder.Append(".");
            }
            return builder.ToString();
        }

        public string CreateIdentifier(params string[] names)
        {
            return IdentifierOpenChar + string.Join(IdentifierCloseChar + "." + IdentifierOpenChar, names) +
                   IdentifierCloseChar;
        }

        public string CreateIdentifier(DataContext fromContext,string nameSpace)
        {
            return ConvertIdentifier(fromContext, this, nameSpace);
        }

        public abstract DataContext DeepClone();

        public abstract DataContext ShallowClone();

        //public abstract DataSyncManager CreateSyncManager(DataConnectionInfo source, DataConnectionInfo target, DataProperties properties);

        //protected abstract IMetaScriptFactory CreateScriptFactory();
    }
}
