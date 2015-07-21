using System.Runtime.Serialization;
using Meta.Net.Abstract;

namespace Meta.Net.Objects
{
    [DataContract]
    public class StoredProcedure : BaseModule
    {
        public static readonly string DefaultDescription = "Stored Procedure";

        public override string Description
        {
            get { return DefaultDescription; }
        }

        //public static void GenerateAlterScripts(
        //    DataContext sourceDataContext, DataContext targetDataContext,
        //    StoredProcedure storedProcedure, DataSyncActionsCollection dataSyncActions, DataProperties dataProperties)
        //{
        //    if (DataProperties.IgnoredModules.Contains(storedProcedure.Namespace))
        //        return;

        //    var dataSyncAction = DataActionFactory.AlterStoredProcedure(sourceDataContext, targetDataContext, storedProcedure);
        //    if (dataSyncAction != null)
        //        dataSyncActions.Add(dataSyncAction);
        //}

        //public static void GenerateCreateScripts(
        //    DataContext sourceDataContext, DataContext targetDataContext,
        //    StoredProcedure storedProcedure, DataSyncActionsCollection dataSyncActions, DataProperties dataProperties)
        //{
        //    if (DataProperties.IgnoredModules.Contains(storedProcedure.Namespace))
        //        return;

        //    var dataSyncAction = DataActionFactory.CreateStoredProcedure(sourceDataContext, targetDataContext, storedProcedure);
        //    if (dataSyncAction != null)
        //        dataSyncActions.Add(dataSyncAction);
        //}

        //public static void GenerateDropScripts(
        //    DataContext sourceDataContext, DataContext targetDataContext,
        //    StoredProcedure storedProcedure, DataSyncActionsCollection dataSyncActions, DataProperties dataProperties)
        //{
        //    if (DataProperties.IgnoredModules.Contains(storedProcedure.Namespace))
        //        return;

        //    if (!dataProperties.TightSync)
        //        return;

        //    var dataSyncAction = DataActionFactory.DropStoredProcedure(sourceDataContext, targetDataContext, storedProcedure);
        //    if (dataSyncAction != null)
        //        dataSyncActions.Add(dataSyncAction);
        //}
    }
}
