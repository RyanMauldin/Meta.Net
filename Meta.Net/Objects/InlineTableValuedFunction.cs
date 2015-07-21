using System.Runtime.Serialization;
using Meta.Net.Abstract;

namespace Meta.Net.Objects
{
    [DataContract]
    public class InlineTableValuedFunction : BaseModule
    {
        public static readonly string DefaultDescription = "Inline Table-Valued Function";

        public override string Description
        {
            get { return DefaultDescription; }
        }

        //public static void GenerateAlterScripts(
        //    DataContext sourceDataContext, DataContext targetDataContext,
        //    InlineTableValuedFunction inlineTableValuedFunction, DataSyncActionsCollection dataSyncActions, DataProperties dataProperties)
        //{
        //    if (DataProperties.IgnoredModules.Contains(inlineTableValuedFunction.Namespace))
        //        return;

        //    var dataSyncAction = DataActionFactory.AlterInlineTableValuedFunction(sourceDataContext, targetDataContext, inlineTableValuedFunction);
        //    if (dataSyncAction != null)
        //        dataSyncActions.Add(dataSyncAction);
        //}

        //public static void GenerateCreateScripts(
        //    DataContext sourceDataContext, DataContext targetDataContext,
        //    InlineTableValuedFunction inlineTableValuedFunction, DataSyncActionsCollection dataSyncActions, DataProperties dataProperties)
        //{
        //    if (DataProperties.IgnoredModules.Contains(inlineTableValuedFunction.Namespace))
        //        return;

        //    var dataSyncAction = DataActionFactory.CreateInlineTableValuedFunction(sourceDataContext, targetDataContext, inlineTableValuedFunction);
        //    if (dataSyncAction != null)
        //        dataSyncActions.Add(dataSyncAction);
        //}

        //public static void GenerateDropScripts(
        //    DataContext sourceDataContext, DataContext targetDataContext,
        //    InlineTableValuedFunction inlineTableValuedFunction, DataSyncActionsCollection dataSyncActions, DataProperties dataProperties)
        //{
        //    if (DataProperties.IgnoredModules.Contains(inlineTableValuedFunction.Namespace))
        //        return;

        //    if (!dataProperties.TightSync)
        //        return;

        //    var dataSyncAction = DataActionFactory.DropInlineTableValuedFunction(sourceDataContext, targetDataContext, inlineTableValuedFunction);
        //    if (dataSyncAction != null)
        //        dataSyncActions.Add(dataSyncAction);
        //}
    }
}
