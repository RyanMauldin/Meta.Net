using Meta.Net.Abstract;

namespace Meta.Net.Objects
{
    //[Serializable]
    public class TableValuedFunction : BaseModule
    {
        public static readonly string DefaultDescription = "Table-Valued Function";

        public override string Description
        {
            get { return DefaultDescription; }
        }

		public TableValuedFunction(Schema schema, string objectName)
        {
            Init(this, schema, objectName);
        }

        public TableValuedFunction()
        {
            
        }

        /// <summary>
        /// Deep Clone and Shallow Clone... Leaf Node.
        /// A clone of this class's isntance specific metadata.
        /// </summary>
        /// <param name="tableValuedFunction">The table-valued function to clone.</param>
        /// <returns>A clone of this class's isntance specific metadata.</returns>
        public static TableValuedFunction Clone(TableValuedFunction tableValuedFunction)
        {
            return new TableValuedFunction
            {
                ObjectName = tableValuedFunction.ObjectName,
                Definition = tableValuedFunction.Definition,
                UsesAnsiNulls = tableValuedFunction.UsesAnsiNulls,
                UsesQuotedIdentifier = tableValuedFunction.UsesQuotedIdentifier
            };
        }

        //public static void GenerateAlterScripts(
        //    DataContext sourceDataContext, DataContext targetDataContext,
        //    TableValuedFunction tableValuedFunction, DataSyncActionsCollection dataSyncActions, DataProperties dataProperties)
        //{
        //    if (DataProperties.IgnoredModules.Contains(tableValuedFunction.Namespace))
        //        return;

        //    var dataSyncAction = DataActionFactory.AlterTableValuedFunction(sourceDataContext, targetDataContext, tableValuedFunction);
        //    if (dataSyncAction != null)
        //        dataSyncActions.Add(dataSyncAction);
        //}

        //public static void GenerateCreateScripts(
        //    DataContext sourceDataContext, DataContext targetDataContext,
        //    TableValuedFunction tableValuedFunction, DataSyncActionsCollection dataSyncActions, DataProperties dataProperties)
        //{
        //    if (DataProperties.IgnoredModules.Contains(tableValuedFunction.Namespace))
        //        return;

        //    var dataSyncAction = DataActionFactory.CreateTableValuedFunction(sourceDataContext, targetDataContext, tableValuedFunction);
        //    if (dataSyncAction != null)
        //        dataSyncActions.Add(dataSyncAction);
        //}

        //public static void GenerateDropScripts(
        //    DataContext sourceDataContext, DataContext targetDataContext,
        //    TableValuedFunction tableValuedFunction, DataSyncActionsCollection dataSyncActions, DataProperties dataProperties)
        //{
        //    if (DataProperties.IgnoredModules.Contains(tableValuedFunction.Namespace))
        //        return;

        //    if (!dataProperties.TightSync)
        //        return;

        //    var dataSyncAction = DataActionFactory.DropTableValuedFunction(sourceDataContext, targetDataContext, tableValuedFunction);
        //    if (dataSyncAction != null)
        //        dataSyncActions.Add(dataSyncAction);
        //}

        //public TableValuedFunction(SerializationInfo info, StreamingContext context)
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

        //public static string ToJson(TableValuedFunction tableValuedFunction, Formatting formatting = Formatting.Indented)
        //{
        //    return JsonConvert.SerializeObject(tableValuedFunction, formatting);
        //}

        //public static TableValuedFunction FromJson(string json)
        //{
        //    return JsonConvert.DeserializeObject<TableValuedFunction>(json);
        //}
    }
}
