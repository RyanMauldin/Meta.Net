using Meta.Net.Abstract;
using Meta.Net.Interfaces;

namespace Meta.Net.Objects
{
    //[Serializable]
    public class AggregateFunction : BaseModule
    {
        public static readonly string DefaultDescription = "Aggregate Function";

        public override string Description
        {
            get { return DefaultDescription; }
        }

        public override IMetaObject DeepClone()
        {
            return new AggregateFunction
            {
                ObjectName = ObjectName == null ? null : string.Copy(ObjectName),
                Definition = Definition == null ? null : string.Copy(Definition),
                UsesAnsiNulls = UsesAnsiNulls,
                UsesQuotedIdentifier = UsesQuotedIdentifier
            };
        }

        public override IMetaObject ShallowClone()
        {
            return new AggregateFunction
            {
                ObjectName = ObjectName,
                Definition = Definition,
                UsesAnsiNulls = UsesAnsiNulls,
                UsesQuotedIdentifier = UsesQuotedIdentifier
            };
        }
        
        //public static void GenerateAlterScripts(
        //    DataContext sourceDataContext, DataContext targetDataContext,
        //    AggregateFunction aggregateFunction, DataSyncActionsCollection dataSyncActions, DataProperties dataProperties)
        //{
        //    if (DataProperties.IgnoredModules.Contains(aggregateFunction.Namespace))
        //        return;

        //    var dataSyncAction = DataActionFactory.AlterAggregateFunction(sourceDataContext, targetDataContext, aggregateFunction);
        //    if (dataSyncAction != null)
        //        dataSyncActions.Add(dataSyncAction);
        //}

        //public static void GenerateCreateScripts(
        //    DataContext sourceDataContext, DataContext targetDataContext,
        //    AggregateFunction aggregateFunction, DataSyncActionsCollection dataSyncActions, DataProperties dataProperties)
        //{
        //    if (DataProperties.IgnoredModules.Contains(aggregateFunction.Namespace))
        //        return;

        //    var dataSyncAction = DataActionFactory.CreateAggregateFunction(sourceDataContext, targetDataContext, aggregateFunction);
        //    if (dataSyncAction != null)
        //        dataSyncActions.Add(dataSyncAction);
        //}

        //public static void GenerateDropScripts(
        //    DataContext sourceDataContext, DataContext targetDataContext,
        //    AggregateFunction aggregateFunction, DataSyncActionsCollection dataSyncActions, DataProperties dataProperties)
        //{
        //    if (DataProperties.IgnoredModules.Contains(aggregateFunction.Namespace))
        //        return;

        //    if (!dataProperties.TightSync)
        //        return;

        //    var dataSyncAction = DataActionFactory.DropAggregateFunction(sourceDataContext, targetDataContext, aggregateFunction);
        //    if (dataSyncAction != null)
        //        dataSyncActions.Add(dataSyncAction);
        //}

        //public AggregateFunction(SerializationInfo info, StreamingContext context)
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

        //public static AggregateFunction FromJson(string json)
        //{
        //    return JsonConvert.DeserializeObject<AggregateFunction>(json);
        //}

        //public static string ToJson(AggregateFunction aggregateFunction, Formatting formatting = Formatting.Indented)
        //{
        //    return JsonConvert.SerializeObject(aggregateFunction, formatting);
        //}
    }
}
