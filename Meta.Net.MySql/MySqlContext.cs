using Meta.Net.Types;

namespace Meta.Net.MySql
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

        public override int MaxObjectNameLength
        {
            get { return 64; }
        }

        public override int MaxAliasLength
        {
            get { return 256; }
        }

        public override int MaxTempTableNameLength
        {
            get { return 64; }
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

        public override DataContext DeepClone()
        {
            return new MySqlContext();
        }

        public override DataContext ShallowClone()
        {
            return new MySqlContext();
        }

        //protected override IDataScriptFactory CreateScriptFactory()
        //{
        //    return new MySqlScriptFactory();
        //}

        //public override DataSyncManager CreateSyncManager(DataConnectionInfo source, DataConnectionInfo target, DataProperties properties)
        //{
        //    return new MySqlSyncManager(source, target, properties);
        //}
    }
}
