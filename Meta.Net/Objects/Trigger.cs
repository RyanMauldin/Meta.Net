using System.Runtime.Serialization;
using Meta.Net.Abstract;
using Meta.Net.Interfaces;

namespace Meta.Net.Objects
{
    [DataContract]
    public class Trigger : BaseModule, ITrigger
    {
        public static readonly string DefaultDescription = "Trigger";

        public override string Description
        {
            get { return DefaultDescription; }
        }

        [DataMember] public bool IsDisabled { get; set; }
        [DataMember] public bool IsNotForReplication { get; set; }
        [DataMember] public string TriggerForObjectName { get; set; }
        [DataMember] public string TriggerForSchema { get; set; }

        public Trigger()
        {
            IsDisabled = false;
            IsNotForReplication = false;
            TriggerForObjectName = string.Empty;
            TriggerForSchema = string.Empty;
        }
        
        //public static bool CompareDefinitions(Trigger sourceTrigger, Trigger targetTrigger)
        //{
        //    var result = BaseModule.CompareDefinitions(sourceTrigger, targetTrigger);
        //    if (!result)
        //        return false;

        //    if (sourceTrigger.IsDisabled != targetTrigger.IsDisabled)
        //        return false;

        //    if (sourceTrigger.IsNotForReplication != targetTrigger.IsNotForReplication)
        //        return false;

        //    if (StringComparer.OrdinalIgnoreCase.Compare(sourceTrigger.TriggerForSchema, targetTrigger.TriggerForSchema) != 0)
        //        return false;

        //    return StringComparer.OrdinalIgnoreCase.Compare(sourceTrigger.TriggerForObjectName, targetTrigger.TriggerForObjectName) == 0;
        //}

        //public static void GenerateAlterScripts(
        //    DataContext sourceDataContext, DataContext targetDataContext,
        //    Trigger trigger, DataSyncActionsCollection dataSyncActions, DataProperties dataProperties)
        //{
        //    if (DataProperties.IgnoredModules.Contains(trigger.Namespace))
        //        return;

        //    var dataSyncAction = DataActionFactory.AlterTrigger(sourceDataContext, targetDataContext, trigger);
        //    if (dataSyncAction != null)
        //        dataSyncActions.Add(dataSyncAction);
        //}

        //public static void GenerateCreateScripts(
        //    DataContext sourceDataContext, DataContext targetDataContext,
        //    Trigger trigger, DataSyncActionsCollection dataSyncActions, DataProperties dataProperties)
        //{
        //    if (DataProperties.IgnoredModules.Contains(trigger.Namespace))
        //        return;

        //    var dataSyncAction = DataActionFactory.CreateTrigger(sourceDataContext, targetDataContext, trigger);
        //    if (dataSyncAction != null)
        //        dataSyncActions.Add(dataSyncAction);
        //}

        //public static void GenerateDropScripts(
        //    DataContext sourceDataContext, DataContext targetDataContext,
        //    Trigger trigger, DataSyncActionsCollection dataSyncActions, DataProperties dataProperties)
        //{
        //    if (DataProperties.IgnoredModules.Contains(trigger.Namespace))
        //        return;

        //    if (!dataProperties.TightSync)
        //        return;

        //    var dataSyncAction = DataActionFactory.DropTrigger(sourceDataContext, targetDataContext, trigger);
        //    if (dataSyncAction != null)
        //        dataSyncActions.Add(dataSyncAction);
        //}
    }
}
