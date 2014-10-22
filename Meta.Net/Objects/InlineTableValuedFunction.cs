using Meta.Net.Abstract;

namespace Meta.Net.Objects
{
    //[Serializable]
    public class InlineTableValuedFunction : BaseModule
    {
        public static readonly string DefaultDescription = "Inline Table-Valued Function";

        public override string Description
        {
            get { return DefaultDescription; }
        }

        public InlineTableValuedFunction(Schema schema, string objectName)
        {
            Init(this, schema, objectName);
        }

        public InlineTableValuedFunction()
        {
            
        }

        /// <summary>
        /// Deep Clone and Shallow Clone... Leaf Node.
        /// A clone of this class's isntance specific metadata.
        /// </summary>
        /// <param name="inlineTableValuedFunction">The inline table-valued function to clone.</param>
        /// <returns>A clone of this class's isntance specific metadata.</returns>
        public static InlineTableValuedFunction Clone(InlineTableValuedFunction inlineTableValuedFunction)
        {
            return new InlineTableValuedFunction
            {
                ObjectName = inlineTableValuedFunction.ObjectName,
                Definition = inlineTableValuedFunction.Definition,
                UsesAnsiNulls = inlineTableValuedFunction.UsesAnsiNulls,
                UsesQuotedIdentifier = inlineTableValuedFunction.UsesQuotedIdentifier
            };
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

        //public InlineTableValuedFunction(SerializationInfo info, StreamingContext context)
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

        //public static string ToJson(InlineTableValuedFunction inlineTableValuedFunction, Formatting formatting = Formatting.Indented)
        //{
        //    return JsonConvert.SerializeObject(inlineTableValuedFunction, formatting);
        //}

        //public static InlineTableValuedFunction FromJson(string json)
        //{
        //    return JsonConvert.DeserializeObject<InlineTableValuedFunction>(json);
        //}
    }
}
