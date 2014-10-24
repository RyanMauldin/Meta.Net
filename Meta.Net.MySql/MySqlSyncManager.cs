using System.Collections.Specialized;
using Meta.Net.Objects;
using Meta.Net.Sync;

namespace Meta.Net.MySql
{
    public class MySqlSyncManager : DataSyncManager
    {
         public MySqlSyncManager(Server sourceServer, Server targetServer, DataProperties dataProperties )
            : base(sourceServer, targetServer, dataProperties) {
            }

        public MySqlSyncManager(DataConnectionInfo sourceDataConnectionInfo, DataConnectionInfo targetDataConnectionInfo, DataProperties dataProperties)
            : base(sourceDataConnectionInfo, targetDataConnectionInfo, dataProperties) {
            }

        public MySqlSyncManager(Server sourceServer, DataConnectionInfo targetDataConnectionInfo, DataProperties dataProperties)
            : base(sourceServer, targetDataConnectionInfo, dataProperties) {
            }

        public MySqlSyncManager()
            : base(new Server { DataContext = new MySqlContext() }, new Server { DataContext = new MySqlContext() }, new DataProperties())
        {
            
        }

        protected override void ChangeCatalog(DataContext sourceDataContext, DataContext targetDataContext, StringCollection stringCollection, Catalog catalog) { }

        protected override void RestoreIsoState(DataContext sourceDataContext, DataContext targetDataContext, StringCollection stringCollection) { }

        protected override void RestoreIsoState(DataContext sourceDataContext, DataContext targetDataContext, StringCollection stringCollection, string delimiter) { }

        protected override void SetIsoState(DataContext sourceDataContext, DataContext targetDataContext, DataSyncAction syncAction, StringCollection strs) { }

        protected override void SetIsoState(DataContext sourceDataContext, DataContext targetDataContext, DataSyncAction syncAction, StringCollection stringCollection, string delimiter) { }

        protected override bool SyncInternal() { return true; }
    }
}
