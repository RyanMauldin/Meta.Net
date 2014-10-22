namespace Meta.Net.Sync.Interfaces
{
    /// <summary>
    /// Interface for creating a synchronization management object. The management
    /// object compares existing source and target instance specific objects
    /// using CompareForSync() and generates a SyncActionsCollection or derived collection
    /// of SyncActions to perform to bring the instance specific objects to a
    /// synchronized state . The managment object then cycles through that list of
    /// SyncActions in the Sync() method to synchronize the instance specific objects.
    /// </summary>
    public interface ISyncManager : ISyncComparer
    {
        /// <summary>
        /// Clears out the current state of the SyncManager and any Exceptions and
        /// implementaton specific details and prepares it for the next comparison.
        /// CompareForSync() should call this method every time before performing any
        /// comparisons.
        /// </summary>
        void Clear();

        /// <summary>
        /// Initiates the synchronization of instance specific objects by cycling
        /// through a SyncActionsCollection or derived collection and executing the
        /// required instance specific instructions in the SyncAction objects.
        /// </summary>
        /// <returns>
        /// True  - synchronized
        /// True  - SyncActionsCollection.Count == 0, because CompareForSync() not called.
        /// False - exceptions encountered
        /// </returns>
        bool Sync();
    }
}
