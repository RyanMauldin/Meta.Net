using System;
using System.Collections.Generic;

namespace Meta.Net.Interfaces
{
    /// <summary>
    /// Interface provided for SyncManager and SyncComparer objects to implement.
    /// This helps break down implementation specific complexities during
    /// comparison activites.
    /// </summary>
    public interface ISyncComparer
    {
        # region members (1)

        /// <summary>
        /// Contains a list of exceptions encountered during comparison activities.
        /// </summary>
        List<Exception> Exceptions { get; }

        # endregion memebers

        # region methods (1)

        /// <summary>
        /// Generates an instance specific SyncActionCollection to be cycled through
        /// in the SyncManager.Sync() method to complete the synchronization of instance
        /// specific objects. * Databases, File Systems, etc. This method should never perform
        /// any synchronization activites, but only generate the synchronization actions needed
        /// to synchronize the instance specific objects.
        /// </summary>
        /// <returns>
        /// True  - synchronized
        /// False - ready for synchronization
        /// False - exceptions encountered
        /// </returns>
        bool CompareForSync();

        #endregion methods
    }
}
