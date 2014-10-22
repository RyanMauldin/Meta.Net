using System.Collections.Generic;

namespace Meta.Net.Sync
{
    /// <summary>
    /// Provided as a Collection of DataSyncAction objects.
    /// </summary>
    public class DataSyncActionsCollection : List<DataSyncAction>
    {
        public new void Add(DataSyncAction action)
        {
            if (string.IsNullOrEmpty(action.Script))
                return;

            base.Add(action);

        }
    }
}
