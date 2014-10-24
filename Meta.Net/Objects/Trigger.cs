﻿using Meta.Net.Abstract;
using Meta.Net.Interfaces;

namespace Meta.Net.Objects
{
    //[Serializable]
    public class Trigger : BaseModule, ITrigger
    {
        public static readonly string DefaultDescription = "Trigger";

        public override string Description
        {
            get { return DefaultDescription; }
        }

        public bool IsDisabled { get; set; }
        public bool IsNotForReplication { get; set; }
        public string TriggerForObjectName { get; set; }
        public string TriggerForSchema { get; set; }

        public Trigger()
        {
            IsDisabled = false;
            IsNotForReplication = false;
            TriggerForObjectName = string.Empty;
            TriggerForSchema = string.Empty;
        }

        public override IMetaObject DeepClone()
        {
            return new Trigger
            {
                ObjectName = ObjectName == null ? null : string.Copy(ObjectName),
                Definition = Definition == null ? null : string.Copy(Definition),
                UsesAnsiNulls = UsesAnsiNulls,
                UsesQuotedIdentifier = UsesQuotedIdentifier,
                IsDisabled = IsDisabled,
                IsNotForReplication = IsNotForReplication,
                TriggerForObjectName = TriggerForObjectName,
                TriggerForSchema = TriggerForSchema
            };
        }

        public override IMetaObject ShallowClone()
        {
            return new Trigger
            {
                ObjectName = ObjectName,
                Definition = Definition,
                UsesAnsiNulls = UsesAnsiNulls,
                UsesQuotedIdentifier = UsesQuotedIdentifier,
                IsDisabled = IsDisabled,
                IsNotForReplication = IsNotForReplication,
                TriggerForObjectName = TriggerForObjectName,
                TriggerForSchema = TriggerForSchema
            };
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

        //public Trigger(SerializationInfo info, StreamingContext context)
        //{
        //    // Holding off on the serialzation in this manner because, this is
        //    // extremely complicated to do in this manner do to data object
        //    // associations, especially
        //    // Set Null Members
        //    Schema = null;

        //    // Deserialize Members
        //    ObjectName = info.GetString("ObjectName");
        //    Definition = info.GetString("Definition");
        //    Description = info.GetString("Description");
        //    UsesAnsiNulls = info.GetBoolean("UsesAnsiNulls");
        //    UsesQuotedIdentifier = info.GetBoolean("UsesQuotedIdentifier");
        //}

        //public void GetObjectData(SerializationInfo info, StreamingContext context)
        //{
        //    // Holding off on the serialzation in this manner because, this is
        //    // extremely complicated to do in this manner do to data object
        //    // associations, especially
        //    // Serialize Members
        //    info.AddValue("ObjectName", ObjectName);
        //    info.AddValue("Definition", Definition);
        //    info.AddValue("Description", Description);
        //    info.AddValue("UsesAnsiNulls", UsesAnsiNulls);
        //    info.AddValue("UsesQuotedIdentifier", UsesQuotedIdentifier);
        //}

        //public static Trigger FromJson(string json)
        //{
        //    return JsonConvert.DeserializeObject<Trigger>(json);
        //}

        //public static string ToJson(Trigger trigger, Formatting formatting = Formatting.Indented)
        //{
        //    return JsonConvert.SerializeObject(trigger, formatting);
        //}
    }
}
