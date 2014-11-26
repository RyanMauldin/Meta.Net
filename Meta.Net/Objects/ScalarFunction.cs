using Meta.Net.Abstract;
using Meta.Net.Interfaces;

namespace Meta.Net.Objects
{
    //[Serializable]
    public class ScalarFunction : BaseModule
    {
        public static readonly string DefaultDescription = "Scalar Function";

        public override string Description
        {
            get { return DefaultDescription; }
        }

        public override IMetaObject DeepClone()
        {
            return new ScalarFunction
            {
                ObjectName = ObjectName,
                Definition = Definition,
                UsesAnsiNulls = UsesAnsiNulls,
                UsesQuotedIdentifier = UsesQuotedIdentifier
            };
        }

        public override IMetaObject ShallowClone()
        {
            return new ScalarFunction
            {
                ObjectName = ObjectName,
                Definition = Definition,
                UsesAnsiNulls = UsesAnsiNulls,
                UsesQuotedIdentifier = UsesQuotedIdentifier
            };
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

        //public ScalarFunction(SerializationInfo info, StreamingContext context)
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

        //public static ScalarFunction FromJson(string json)
        //{
        //    return JsonConvert.DeserializeObject<ScalarFunction>(json);
        //}

        //public static string ToJson(ScalarFunction scalarFunction, Formatting formatting = Formatting.Indented)
        //{
        //    return JsonConvert.SerializeObject(scalarFunction, formatting);
        //}
    }
}
