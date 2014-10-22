using Meta.Net.Abstract;

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

        public AggregateFunction(Schema schema, string objectName)
        {
            Init(this, schema, objectName);
        }

        public AggregateFunction()
        {

        }

        /// <summary>
        /// Deep Clone of this class's isntance specific metadata associated to the same schema
        /// as found in the schemaLookup. If the schemaLookup does not contain the schema,
        /// this method will return null. The schema in the schemaLookup does not have to
        /// be the same instance of the schema in the aggregateFunction parameter.
        /// </summary>
        /// <param name="aggregateFunction">The aggregate function to clone.</param>
        /// <returns>A clone of this class's isntance specific metadata, or null if the schemaLookup does not contain a schema with the same name.</returns>
        public static AggregateFunction Clone(AggregateFunction aggregateFunction)
        {
            return new AggregateFunction
            {
                ObjectName = aggregateFunction.ObjectName,
                Definition = aggregateFunction.Definition,
                UsesAnsiNulls = aggregateFunction.UsesAnsiNulls,
                UsesQuotedIdentifier = aggregateFunction.UsesQuotedIdentifier
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
