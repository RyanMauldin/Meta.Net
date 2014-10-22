namespace Meta.Net.Interfaces
{
    public interface ITrigger : IModule
    {
        bool IsNotForReplication { get; set; }
        bool IsDisabled { get; set; }
        string TriggerForSchema { get; set; }
        string TriggerForObjectName { get; set; }
    }
}
