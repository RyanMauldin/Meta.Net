using Meta.Net.Data.Enum;
using Meta.Net.Data.Interfaces;

namespace Meta.Net.Data.MySql
{
    public class MySqlContext : DataContext
    {
        public override string IdentifierOpenChar
        {
            get { return "`"; }
        }

        public override string IdentifierCloseChar
        {
            get { return "`"; }
        }

        public override bool IncludeSchemaNameInNamespace
        {
            get { return false; }
        }

        public override DataContextType ContextType
        {
            get { return DataContextType.MySql; }
        }

        public override string DefaultSchemaName { get { return "dbo"; } }

        public override string Delimiter
        {
            get { return ";\r\n"; }
        }

        protected override IDataScriptFactory CreateScriptFactory()
        {
            return new MySqlScriptFactory();
        }

        public override DataSyncManager CreateSyncManager(DataConnectionInfo source, DataConnectionInfo target, DataProperties properties)
        {
            return new MySqlSyncManager(source, target, properties);
        }
    }
}
