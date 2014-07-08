namespace Meta.Net.Interfaces
{
    /// <summary>
    /// Interface provided for implementation independant synchronization actions
    /// to be used in SyncActionCollection objects and used by SyncManager objects
    /// as steps towards completing a synchronization task.
    /// </summary>
    public interface ISyncAction
    {
        # region members (3)

        /// <summary>
        /// Identifies the object being synchronized.
        /// </summary>
        string Identifier { get; }

        /// <summary>
        /// Description of the object being synchronized.
        /// </summary>
        string Description { get; }

        /// <summary>
        /// Determines whether or not the action should be performed to bring the
        /// instance specific objects in question to the desired synchronization state.
        /// </summary>
        bool Process { get; set; }

        # endregion members
    }
}
