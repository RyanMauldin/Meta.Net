using Meta.Net.Abstract;
using Meta.Net.Interfaces;

namespace Meta.Net.Objects
{
    //[Serializable]
    public class StoredProcedure : BaseModule
    {
        public static readonly string DefaultDescription = "Stored Procedure";

        public override string Description
        {
            get { return DefaultDescription; }
        }

        public override IMetaObject DeepClone()
        {
            return new StoredProcedure
            {
                ObjectName = ObjectName,
                Definition = Definition,
                UsesAnsiNulls = UsesAnsiNulls,
                UsesQuotedIdentifier = UsesQuotedIdentifier
            };
        }

        public override IMetaObject ShallowClone()
        {
            return new StoredProcedure
            {
                ObjectName = ObjectName,
                Definition = Definition,
                UsesAnsiNulls = UsesAnsiNulls,
                UsesQuotedIdentifier = UsesQuotedIdentifier
            };
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

        //public StoredProcedure(SerializationInfo info, StreamingContext context)
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

        //public static StoredProcedure FromJson(string json)
        //{
        //    return JsonConvert.DeserializeObject<StoredProcedure>(json);
        //}

        //public static string ToJson(StoredProcedure storedProcedure, Formatting formatting = Formatting.Indented)
        //{
        //    return JsonConvert.SerializeObject(storedProcedure, formatting);
        //}
    }
}
