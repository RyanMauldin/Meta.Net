using Meta.Net.Types;

namespace Meta.Net.SqlServer
{
    public class SqlServerContext : DataContext
    {
        public override string IdentifierOpenChar
        {
            get { return "["; }
        }

        public override string IdentifierCloseChar
        {
            get { return "]"; }
        }

        public override bool IncludeSchemaNameInNamespace
        {
            get { return true; }
        }

        public override DataContextType ContextType
        {
            get { return DataContextType.SqlServer; }
        }

        public override string DefaultSchemaName { get { return "dbo"; } }

        public override string Delimiter
        {
            get { return "GO\r\n"; }
        }

        //protected override IDataScriptFactory CreateScriptFactory()
        //{
        //    return new SqlServerScriptFactory();
        //}

        //public override DataSyncManager CreateSyncManager(
        //    DataConnectionInfo source, DataConnectionInfo target, DataProperties properties)
        //{
        //    return new SqlServerSyncManager(source, target, properties);
        //}
    }
}
