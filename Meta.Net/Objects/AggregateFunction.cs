using System.Runtime.Serialization;
using Meta.Net.Abstract;

namespace Meta.Net.Objects
{
    [DataContract]
    public class AggregateFunction : BaseModule
    {
        public static readonly string DefaultDescription = "Aggregate Function";

        public override string Description
        {
            get { return DefaultDescription; }
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
    }
}
