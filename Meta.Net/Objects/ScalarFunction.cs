using System.Runtime.Serialization;
using Meta.Net.Abstract;
using Meta.Net.Interfaces;

namespace Meta.Net.Objects
{
    [DataContract]
    public class ScalarFunction : BaseModule
    {
        public static readonly string DefaultDescription = "Scalar Function";

        public override string Description
        {
            get { return DefaultDescription; }
        }

        //public static void GenerateAlterScripts(
        //    DataContext sourceDataContext, DataContext targetDataContext,
        //    ScalarFunction scalarFunction, DataSyncActionsCollection dataSyncActions, DataProperties dataProperties)
        //{
        //    if (DataProperties.IgnoredModules.Contains(scalarFunction.Namespace))
        //        return;

        //    var dataSyncAction = DataActionFactory.AlterScalarFunction(sourceDataContext, targetDataContext, scalarFunction);
        //    if (dataSyncAction != null)
        //        dataSyncActions.Add(dataSyncAction);
        //}

        //public static void GenerateCreateScripts(
        //    DataContext sourceDataContext, DataContext targetDataContext,
        //    ScalarFunction scalarFunction, DataSyncActionsCollection dataSyncActions, DataProperties dataProperties)
        //{
        //    if (DataProperties.IgnoredModules.Contains(scalarFunction.Namespace))
        //        return;

        //    var dataSyncAction = DataActionFactory.CreateScalarFunction(sourceDataContext, targetDataContext, scalarFunction);
        //    if (dataSyncAction != null)
        //        dataSyncActions.Add(dataSyncAction);
        //}

        //public static void GenerateDropScripts(
        //    DataContext sourceDataContext, DataContext targetDataContext,
        //    ScalarFunction scalarFunction, DataSyncActionsCollection dataSyncActions, DataProperties dataProperties)
        //{
        //    if (DataProperties.IgnoredModules.Contains(scalarFunction.Namespace))
        //        return;

        //    if (!dataProperties.TightSync)
        //        return;

        //    var dataSyncAction = DataActionFactory.DropScalarFunction(sourceDataContext, targetDataContext, scalarFunction);
        //    if (dataSyncAction != null)
        //        dataSyncActions.Add(dataSyncAction);
        //}
    }
}
