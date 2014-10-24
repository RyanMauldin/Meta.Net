using System;
using System.Linq;
using Meta.Net.Abstract;
using Meta.Net.Interfaces;

namespace Meta.Net.Objects
{
    //[Serializable]
    public class Schema : CatalogBasedObect, ICatalog
    {
        public static readonly string DefaultDescription = "Schema";

        public override string Description
        {
            get { return DefaultDescription; }
        }

        public DataObjectLookup<Schema, AggregateFunction> AggregateFunctions { get; private set; }
        public DataObjectLookup<Schema, InlineTableValuedFunction> InlineTableValuedFunctions { get; private set; }
        public DataObjectLookup<Schema, ScalarFunction> ScalarFunctions { get; private set; }
        public DataObjectLookup<Schema, StoredProcedure> StoredProcedures { get; private set; }
        public DataObjectLookup<Schema, TableValuedFunction> TableValuedFunctions { get; private set; }
        public DataObjectLookup<Schema, Trigger> Triggers { get; private set; }
        public DataObjectLookup<Schema, UserDefinedDataType> UserDefinedDataTypes { get; private set; }
        public DataObjectLookup<Schema, UserTable> UserTables { get; private set; }
        public DataObjectLookup<Schema, View> Views { get; private set; }

		public Schema()
        {
            AggregateFunctions = new DataObjectLookup<Schema, AggregateFunction>(this);
            InlineTableValuedFunctions = new DataObjectLookup<Schema, InlineTableValuedFunction>(this);
            TableValuedFunctions = new DataObjectLookup<Schema, TableValuedFunction>(this);
            ScalarFunctions = new DataObjectLookup<Schema, ScalarFunction>(this);
            StoredProcedures = new DataObjectLookup<Schema, StoredProcedure>(this);
            Triggers = new DataObjectLookup<Schema, Trigger>(this);
            UserDefinedDataTypes = new DataObjectLookup<Schema, UserDefinedDataType>(this);
            UserTables = new DataObjectLookup<Schema, UserTable>(this);
            Views = new DataObjectLookup<Schema, View>(this);
        }

        public override IMetaObject DeepClone()
        {
            var schema = new Schema
            {
                ObjectName = ObjectName == null ? null : string.Copy(ObjectName)
            };

            schema.AggregateFunctions = AggregateFunctions.DeepClone(schema);
            schema.InlineTableValuedFunctions = InlineTableValuedFunctions.DeepClone(schema);
            schema.TableValuedFunctions = TableValuedFunctions.DeepClone(schema);
            schema.ScalarFunctions = ScalarFunctions.DeepClone(schema);
            schema.StoredProcedures = StoredProcedures.DeepClone(schema);
            schema.Triggers = Triggers.DeepClone(schema);
            schema.UserDefinedDataTypes = UserDefinedDataTypes.DeepClone(schema);
            schema.UserTables = UserTables.DeepClone(schema);
            schema.Views = Views.DeepClone(schema);

            return schema;
        }

        public override IMetaObject ShallowClone()
        {
            return new Schema
            {
                ObjectName = ObjectName
            };
        }

        public static void AddAggregateFunction(Schema schema, AggregateFunction aggregateFunction)
        {
            if (aggregateFunction.Schema != null && !aggregateFunction.Schema.Equals(schema))
                RemoveAggregateFunction(aggregateFunction.Schema, aggregateFunction);

            schema.AggregateFunctions.Add(aggregateFunction);
        }

        public static void AddInlineTableValuedFunction(Schema schema, InlineTableValuedFunction inlineTableValuedFunction)
        {
            if (inlineTableValuedFunction.Schema != null && !inlineTableValuedFunction.Schema.Equals(schema))
                RemoveInlineTableValuedFunction(inlineTableValuedFunction.Schema, inlineTableValuedFunction);

            schema.InlineTableValuedFunctions.Add(inlineTableValuedFunction);
        }

        public static void AddScalarFunction(Schema schema, ScalarFunction scalarFunction)
        {
            if (scalarFunction.Schema != null && !scalarFunction.Schema.Equals(schema))
                RemoveScalarFunction(scalarFunction.Schema, scalarFunction);

            schema.ScalarFunctions.Add(scalarFunction);
        }

        public static void AddStoredProcedure(Schema schema, StoredProcedure storedProcedure)
        {
            if (storedProcedure.Schema != null && !storedProcedure.Schema.Equals(schema))
                RemoveStoredProcedure(storedProcedure.Schema, storedProcedure);

            schema.StoredProcedures.Add(storedProcedure);
        }

        public static void AddTableValuedFunction(Schema schema, TableValuedFunction tableValuedFunction)
        {
            if (tableValuedFunction.Schema != null && !tableValuedFunction.Schema.Equals(schema))
                RemoveTableValuedFunction(tableValuedFunction.Schema, tableValuedFunction);

            schema.TableValuedFunctions.Add(tableValuedFunction);
        }

        public static void AddTrigger(Schema schema, Trigger trigger)
        {
            if (trigger.Schema != null && !trigger.Schema.Equals(schema))
                RemoveTrigger(trigger.Schema, trigger);

            schema.Triggers.Add(trigger);
        }

        public static void AddUserDefinedDataType(Schema schema, UserDefinedDataType userDefinedDataType)
        {
            if (userDefinedDataType.Schema != null && !userDefinedDataType.Schema.Equals(schema))
                RemoveUserDefinedDataType(userDefinedDataType.Schema, userDefinedDataType);

            schema.UserDefinedDataTypes.Add(userDefinedDataType);
        }

        public static void AddUserTable(Schema schema, UserTable userTable)
        {
            if (userTable.Schema != null && !userTable.Schema.Equals(schema))
                RemoveUserTable(userTable.Schema, userTable);

            schema.UserTables.Add(userTable);
        }

        public static void AddView(Schema schema, View view)
        {
            if (view.Schema != null && !view.Schema.Equals(schema))
                RemoveView(view.Schema, view);

            schema.Views.Add(view);
        }

        /// <summary>
        /// Deep Clear... disassociates all objects in each schema based object
        /// first before clearing out the collections in hopes of better
        /// memory dissipation from garbage collection.
        /// </summary>
        /// <param name="schema">The schema to deep clear.</param>
        public static void Clear(Schema schema)
        {
            schema.AggregateFunctions.Clear();
            schema.InlineTableValuedFunctions.Clear();
            schema.ScalarFunctions.Clear();
            schema.StoredProcedures.Clear();
            schema.TableValuedFunctions.Clear();
            schema.Triggers.Clear();
            foreach (var userTable in schema.UserTables)
                UserTable.Clear(userTable);
            schema.UserTables.Clear();
            schema.UserDefinedDataTypes.Clear();
            schema.Views.Clear();
        }

        //public static bool CompareDefinitions(Schema sourceSchema, Schema targetSchema)
        //{
        //    return CompareObjectNames(sourceSchema, targetSchema);
        //}

        //public static bool CompareMatchedSchema(DataContext sourceDataContext, DataContext targetDataContext, Schema matchedSchema,
        //    Schema sourceSchema, Schema targetSchema, Schema alteredSchema)
        //{
        //    var globalCompareState = true;

        //    foreach (var matchedAggregateFunction in matchedSchema.AggregateFunctions)
        //    {
        //        var sourceAggregateFunction = sourceSchema.AggregateFunctions[matchedAggregateFunction.Namespace];
        //        var targetAggregateFunction = targetSchema.AggregateFunctions[matchedAggregateFunction.Namespace];

        //        if (sourceAggregateFunction == null || targetAggregateFunction == null)
        //            throw new Exception(string.Format("Source and/or target aggregate functions did not exist for the matching aggregate function {0} during Schema.CompareMatchedSchema() method.", matchedAggregateFunction.Namespace));

        //        if (BaseModule.CompareDefinitions(sourceAggregateFunction, targetAggregateFunction))
        //            continue;

        //        globalCompareState = false;

        //        AddAggregateFunction(alteredSchema, AggregateFunction.Clone(sourceAggregateFunction));
        //    }

        //    foreach (var matchedInlineTableValuedFunction in matchedSchema.InlineTableValuedFunctions)
        //    {
        //        var sourceInlineTableValuedFunction = sourceSchema.InlineTableValuedFunctions[matchedInlineTableValuedFunction.Namespace];
        //        var targetInlineTableValuedFunction = targetSchema.InlineTableValuedFunctions[matchedInlineTableValuedFunction.Namespace];
                
        //        if (sourceInlineTableValuedFunction == null || targetInlineTableValuedFunction == null)
        //            throw new Exception(string.Format("Source and/or target inline table-valued function did not exist for the matching inline table-valued function {0} during Schema.CompareMatchedSchema() method.", matchedInlineTableValuedFunction.Namespace));

        //        if (BaseModule.CompareDefinitions(sourceInlineTableValuedFunction, targetInlineTableValuedFunction))
        //            continue;

        //        globalCompareState = false;

        //        AddInlineTableValuedFunction(alteredSchema, InlineTableValuedFunction.Clone(sourceInlineTableValuedFunction));
        //    }

        //    foreach (var matchedScalarFunction in matchedSchema.ScalarFunctions)
        //    {
        //        var sourceScalarFunction = sourceSchema.ScalarFunctions[matchedScalarFunction.Namespace];
        //        var targetScalarFunction = targetSchema.ScalarFunctions[matchedScalarFunction.Namespace];

        //        if (sourceScalarFunction == null || targetScalarFunction == null)
        //            throw new Exception(string.Format("Source and/or target scalar function did not exist for the matching scalar function {0} during Schema.CompareMatchedSchema() method.", matchedScalarFunction.Namespace));

        //        if (BaseModule.CompareDefinitions(sourceScalarFunction, targetScalarFunction))
        //            continue;

        //        globalCompareState = false;

        //        AddScalarFunction(alteredSchema, ScalarFunction.Clone(sourceScalarFunction));
        //    }

        //    foreach (var matchedStoredProcedure in matchedSchema.StoredProcedures)
        //    {
        //        var sourceStoredProcedure = sourceSchema.StoredProcedures[matchedStoredProcedure.Namespace];
        //        var targetStoredProcedure = targetSchema.StoredProcedures[matchedStoredProcedure.Namespace];

        //        if (sourceStoredProcedure == null || targetStoredProcedure == null)
        //            throw new Exception(string.Format("Source and/or target stored procedure did not exist for the matching stored procedure {0} during Schema.CompareMatchedSchema() method.", matchedStoredProcedure.Namespace));

        //        if (BaseModule.CompareDefinitions(sourceStoredProcedure, targetStoredProcedure))
        //            continue;

        //        globalCompareState = false;

        //        AddStoredProcedure(alteredSchema, StoredProcedure.Clone(sourceStoredProcedure));
        //    }

        //    foreach (var matchedTableValuedFunction in matchedSchema.TableValuedFunctions)
        //    {
        //        var sourceTableValuedFunction = sourceSchema.TableValuedFunctions[matchedTableValuedFunction.Namespace];
        //        var targetTableValuedFunction = targetSchema.TableValuedFunctions[matchedTableValuedFunction.Namespace];
                                                                                  
        //        if (sourceTableValuedFunction == null || targetTableValuedFunction == null)
        //            throw new Exception(string.Format("Source and/or target table-valued function did not exist for the matching table-valued function {0} during Schema.CompareMatchedSchema() method.", matchedTableValuedFunction.Namespace));

        //        if (BaseModule.CompareDefinitions(sourceTableValuedFunction, targetTableValuedFunction))
        //            continue;

        //        globalCompareState = false;

        //        AddTableValuedFunction(alteredSchema, TableValuedFunction.Clone(sourceTableValuedFunction));
        //    }

        //    foreach (var matchedTrigger in matchedSchema.Triggers)
        //    {
        //        var sourceTrigger = sourceSchema.Triggers[matchedTrigger.Namespace];
        //        var targetTrigger = targetSchema.Triggers[matchedTrigger.Namespace];

        //        if (sourceTrigger == null || targetTrigger == null)
        //            throw new Exception(string.Format("Source and/or target trigger did not exist for the matching trigger {0} during Schema.CompareMatchedSchema() method.", matchedTrigger.Namespace));

        //        if (Trigger.CompareDefinitions(sourceTrigger, targetTrigger))
        //            continue;

        //        globalCompareState = false;

        //        AddTrigger(alteredSchema, Trigger.Clone(sourceTrigger));
        //    }

        //    foreach (var matchedUserDefinedDataType in matchedSchema.UserDefinedDataTypes)
        //    {
        //        var sourceUserDefinedDataType = sourceSchema.UserDefinedDataTypes[matchedUserDefinedDataType.Namespace];
        //        var targetUserDefinedDataType = targetSchema.UserDefinedDataTypes[matchedUserDefinedDataType.Namespace];
                
        //        if (sourceUserDefinedDataType == null || targetUserDefinedDataType == null)
        //            throw new Exception(string.Format("Source and/or target user-defined data type did not exist for the matching user-defined data type {0} during Schema.CompareMatchedSchema() method.", matchedUserDefinedDataType.Namespace));

        //        if (UserDefinedDataType.CompareDefinitions(sourceUserDefinedDataType, targetUserDefinedDataType))
        //            continue;

        //        globalCompareState = false;

        //        AddUserDefinedDataType(alteredSchema, UserDefinedDataType.Clone(sourceUserDefinedDataType));
        //    }

        //    foreach (var matchedUserTable in matchedSchema.UserTables)
        //    {
        //        var sourceUserTable = sourceSchema.UserTables[matchedUserTable.Namespace];
        //        var targetUserTable = targetSchema.UserTables[matchedUserTable.Namespace];

        //        // Logic
        //        if (sourceUserTable == null || targetUserTable == null)
        //            throw new Exception(string.Format("Source and/or target user-tables did not exist for the matching user-table {0} during Schema.CompareMatchedSchema() method.", matchedUserTable.Namespace));

        //        // Grab a clone of the source user-table and union it with the target user-table and
        //        // then except it with the matched user-table's definitions to see a copy of everything
        //        // that is different between the source and target user-tables.
        //        var alteredUserTable = UserTable.Clone(sourceUserTable);
        //        UserTable.UnionWith(alteredUserTable, targetUserTable, sourceDataContext, targetDataContext, DataComparisonType.Definitions);
        //        UserTable.ExceptWith(sourceDataContext, targetDataContext, alteredUserTable, matchedUserTable, DataComparisonType.Definitions);

        //        // If the except with operation was an exact match between the source and target
        //        // user-tables continue and this method will return true if all user-tables
        //        // are an exact match.
        //        if (UserTable.ObjectCount(alteredUserTable) == 0)
        //            continue;

        //        // A change should be made to a user-table
        //        globalCompareState = false;

        //        // Otherwise we need to make two clones of the altered user-table and except each
        //        // with either the source user-table to find addable elements or the target
        //        // user-table to find droppable elements.
        //        var addableUserTable = UserTable.Clone(alteredUserTable);
        //        UserTable.ExceptWith(sourceDataContext, targetDataContext, addableUserTable, targetUserTable, DataComparisonType.Namespaces);

        //        var droppableUserTable = UserTable.Clone(alteredUserTable);
        //        UserTable.ExceptWith(sourceDataContext, targetDataContext, droppableUserTable, sourceUserTable, DataComparisonType.Namespaces);

        //        // Figure out which ones still remain to be altered by excepting a clone of the
        //        // altered user-table with the addable and droppable elements.
        //        var alterableUserTable = UserTable.Clone(alteredUserTable);
        //        UserTable.ExceptWith(sourceDataContext, targetDataContext, alterableUserTable, addableUserTable, DataComparisonType.Namespaces);
        //        UserTable.ExceptWith(sourceDataContext, targetDataContext, alterableUserTable, droppableUserTable, DataComparisonType.Namespaces);

        //        alteredUserTable.ObjectName += "<=>IsEqual";
        //        addableUserTable.ObjectName += "<+>ToAdd";
        //        droppableUserTable.ObjectName += "<x>ToDrop";
        //        alterableUserTable.ObjectName += "<~>ToAlter";

        //        // Now add each table to the alteredSchema regardless of whether or not they
        //        // have any elements as something needs to exist for each later, regardless...
        //        AddUserTable(alteredSchema, alteredUserTable);
        //        AddUserTable(alteredSchema, addableUserTable);
        //        AddUserTable(alteredSchema, droppableUserTable);
        //        AddUserTable(alteredSchema, alterableUserTable);
        //    }

        //    foreach (var matchedView in matchedSchema.Views)
        //    {
        //        var sourceView = sourceSchema.Views[matchedView.Namespace];
        //        var targetView = targetSchema.Views[matchedView.Namespace];
        //        if (BaseModule.CompareDefinitions(sourceView, targetView))
        //            continue;

        //        globalCompareState = false;

        //        AddView(alteredSchema, View.Clone(sourceView));
        //    }

        //    return globalCompareState;
        //}

        //public static bool CompareObjectNames(Schema sourceSchema, Schema targetSchema, StringComparer stringComparer = null)
        //{
        //    if (stringComparer == null)
        //        stringComparer = StringComparer.OrdinalIgnoreCase;

        //    return stringComparer.Compare(sourceSchema.Namespace, targetSchema.Namespace) == 0;
        //}

        ///// <summary>
        ///// Removes all objects in the target Schema from the source Schema.
        ///// </summary>
        ///// <param name="sourceDataContext">The source DataContext.</param>
        ///// <param name="targetDataContext">The target DataContext.</param>
        ///// <param name="sourceSchema">The source Schema.</param>
        ///// <param name="targetSchema">The target schema.</param>
        ///// <param name="dataComparisonType">
        ///// The completeness of comparisons between matching objects.
        ///// </param>
        //public static void ExceptWith(
        //    DataContext sourceDataContext, DataContext targetDataContext,
        //    Schema sourceSchema, Schema targetSchema,
        //    DataComparisonType dataComparisonType = DataComparisonType.SchemaLevelNamespaces)
        //{
        //    var matchingAggregateFunctions = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        //    matchingAggregateFunctions.UnionWith(sourceSchema.AggregateFunctions.Keys);
        //    matchingAggregateFunctions.IntersectWith(targetSchema.AggregateFunctions.Keys);

        //    foreach (var aggregateFunction in matchingAggregateFunctions)
        //    {
        //        var sourceAggregateFunction = sourceSchema.AggregateFunctions[aggregateFunction];
        //        if (sourceAggregateFunction == null)
        //            continue;

        //        var targetAggregateFunction = targetSchema.AggregateFunctions[aggregateFunction];
        //        if (targetAggregateFunction == null)
        //            continue;

        //        switch (dataComparisonType)
        //        {
        //            case DataComparisonType.Definitions:
        //                if (BaseModule.CompareDefinitions(sourceAggregateFunction, targetAggregateFunction))
        //                    RemoveAggregateFunction(sourceSchema, aggregateFunction);
        //                break;
        //            case DataComparisonType.Namespaces:
        //            case DataComparisonType.SchemaLevelNamespaces:
        //                if (BaseModule.CompareObjectNames(sourceAggregateFunction, targetAggregateFunction))
        //                    RemoveAggregateFunction(sourceSchema, aggregateFunction);
        //                break;
        //        }
        //    }

        //    var matchingInlineTableValuedFunctions = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        //    matchingInlineTableValuedFunctions.UnionWith(sourceSchema.InlineTableValuedFunctions.Keys);
        //    matchingInlineTableValuedFunctions.IntersectWith(targetSchema.InlineTableValuedFunctions.Keys);

        //    foreach (var inlineTableValuedFunction in matchingInlineTableValuedFunctions)
        //    {
        //        var sourceInlineTableValuedFunction = sourceSchema.InlineTableValuedFunctions[inlineTableValuedFunction];
        //        if (sourceInlineTableValuedFunction == null)
        //            continue;

        //        var targetInlineTableValuedFunction = targetSchema.InlineTableValuedFunctions[inlineTableValuedFunction];
        //        if (targetInlineTableValuedFunction == null)
        //            continue;

        //        switch (dataComparisonType)
        //        {
        //            case DataComparisonType.Definitions:
        //                if (BaseModule.CompareDefinitions(sourceInlineTableValuedFunction, targetInlineTableValuedFunction))
        //                    RemoveInlineTableValuedFunction(sourceSchema, inlineTableValuedFunction);
        //                break;
        //            case DataComparisonType.Namespaces:
        //            case DataComparisonType.SchemaLevelNamespaces:
        //                if (BaseModule.CompareObjectNames(sourceInlineTableValuedFunction, targetInlineTableValuedFunction))
        //                    RemoveInlineTableValuedFunction(sourceSchema, inlineTableValuedFunction);
        //                break;
        //        }
        //    }

        //    var matchingScalarFunctions = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        //    matchingScalarFunctions.UnionWith(sourceSchema.ScalarFunctions.Keys);
        //    matchingScalarFunctions.IntersectWith(targetSchema.ScalarFunctions.Keys);
            
        //    foreach (var scalarFunction in matchingScalarFunctions)
        //    {
        //        var sourceScalarFunction = sourceSchema.ScalarFunctions[scalarFunction];
        //        if (sourceScalarFunction == null)
        //            continue;

        //        var targetScalarFunction = targetSchema.ScalarFunctions[scalarFunction];
        //        if (targetScalarFunction == null)
        //            continue;

        //        switch (dataComparisonType)
        //        {
        //            case DataComparisonType.Definitions:
        //                if (BaseModule.CompareDefinitions(sourceScalarFunction, targetScalarFunction))
        //                    RemoveScalarFunction(sourceSchema, scalarFunction);
        //                break;
        //            case DataComparisonType.Namespaces:
        //            case DataComparisonType.SchemaLevelNamespaces:
        //                if (BaseModule.CompareObjectNames(sourceScalarFunction, targetScalarFunction))
        //                    RemoveScalarFunction(sourceSchema, scalarFunction);
        //                break;
        //        }
        //    }

        //    var matchingStoredProcedures = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        //    matchingStoredProcedures.UnionWith(sourceSchema.StoredProcedures.Keys);
        //    matchingStoredProcedures.IntersectWith(targetSchema.StoredProcedures.Keys);
            
        //    foreach (var storedProcedure in matchingStoredProcedures)
        //    {
        //        var sourceStoredProcedure = sourceSchema.StoredProcedures[storedProcedure];
        //        if (sourceStoredProcedure == null)
        //            continue;

        //        var targetStoredProcedure = targetSchema.StoredProcedures[storedProcedure];
        //        if (targetStoredProcedure == null)
        //            continue;

        //        switch (dataComparisonType)
        //        {
        //            case DataComparisonType.Definitions:
        //                if (BaseModule.CompareDefinitions(sourceStoredProcedure, targetStoredProcedure))
        //                    RemoveStoredProcedure(sourceSchema, storedProcedure);
        //                break;
        //            case DataComparisonType.Namespaces:
        //            case DataComparisonType.SchemaLevelNamespaces:
        //                if (BaseModule.CompareObjectNames(sourceStoredProcedure, targetStoredProcedure))
        //                    RemoveStoredProcedure(sourceSchema, storedProcedure);
        //                break;
        //        }
        //    }

        //    var matchingTableValuedFunctions = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        //    matchingTableValuedFunctions.UnionWith(sourceSchema.TableValuedFunctions.Keys);
        //    matchingTableValuedFunctions.IntersectWith(targetSchema.TableValuedFunctions.Keys);

        //    foreach (var tableValuedFunction in matchingTableValuedFunctions)
        //    {
        //        var sourceTableValuedFunction = sourceSchema.TableValuedFunctions[tableValuedFunction];
        //        if (sourceTableValuedFunction == null)
        //            continue;

        //        var targetTableValuedFunction = targetSchema.TableValuedFunctions[tableValuedFunction];
        //        if (targetTableValuedFunction == null)
        //            continue;

        //        switch (dataComparisonType)
        //        {
        //            case DataComparisonType.Definitions:
        //                if (BaseModule.CompareDefinitions(sourceTableValuedFunction, targetTableValuedFunction))
        //                    RemoveTableValuedFunction(sourceSchema, tableValuedFunction);
        //                break;
        //            case DataComparisonType.Namespaces:
        //            case DataComparisonType.SchemaLevelNamespaces:
        //                if (BaseModule.CompareObjectNames(sourceTableValuedFunction, targetTableValuedFunction))
        //                    RemoveTableValuedFunction(sourceSchema, tableValuedFunction);
        //                break;
        //        }
        //    }

        //    var matchingTriggers = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        //    matchingTriggers.UnionWith(sourceSchema.Triggers.Keys);
        //    matchingTriggers.IntersectWith(targetSchema.Triggers.Keys);
            
        //    foreach (var trigger in matchingTriggers)
        //    {
        //        var sourceTrigger = sourceSchema.Triggers[trigger];
        //        if (sourceTrigger == null)
        //            continue;

        //        var targetTrigger = targetSchema.Triggers[trigger];
        //        if (targetTrigger == null)
        //            continue;

        //        switch (dataComparisonType)
        //        {
        //            case DataComparisonType.Definitions:
        //                if (Trigger.CompareDefinitions(sourceTrigger, targetTrigger))
        //                    RemoveTrigger(sourceSchema, trigger);
        //                break;
        //            case DataComparisonType.Namespaces:
        //            case DataComparisonType.SchemaLevelNamespaces:
        //                if (BaseModule.CompareObjectNames(sourceTrigger, targetTrigger))
        //                    RemoveTrigger(sourceSchema, trigger);
        //                break;
        //        }
        //    }

        //    var matchingUserDefinedDataTypes = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        //    matchingUserDefinedDataTypes.UnionWith(sourceSchema.UserDefinedDataTypes.Keys);
        //    matchingUserDefinedDataTypes.IntersectWith(targetSchema.UserDefinedDataTypes.Keys);
            
        //    foreach (var userDefinedDataType in matchingUserDefinedDataTypes)
        //    {
        //        var sourceUserDefinedDataType = sourceSchema.UserDefinedDataTypes[userDefinedDataType];
        //        if (sourceUserDefinedDataType == null)
        //            continue;

        //        var targetUserDefinedDataType = targetSchema.UserDefinedDataTypes[userDefinedDataType];
        //        if (targetUserDefinedDataType == null)
        //            continue;

        //        switch (dataComparisonType)
        //        {
        //            case DataComparisonType.Definitions:
        //                if (UserDefinedDataType.CompareDefinitions(sourceUserDefinedDataType, targetUserDefinedDataType))
        //                    RemoveUserDefinedDataType(sourceSchema, userDefinedDataType);
        //                break;
        //            case DataComparisonType.Namespaces:
        //            case DataComparisonType.SchemaLevelNamespaces:
        //                if (UserDefinedDataType.CompareObjectNames(sourceUserDefinedDataType, targetUserDefinedDataType))
        //                    RemoveUserDefinedDataType(sourceSchema, userDefinedDataType);
        //                break;
        //        }
        //    }

        //    var matchingUserTables = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        //    matchingUserTables.UnionWith(sourceSchema.UserTables.Keys);
        //    matchingUserTables.IntersectWith(targetSchema.UserTables.Keys);
            
        //    foreach (var userTable in matchingUserTables)
        //    {
        //        var sourceUserTable = sourceSchema.UserTables[userTable];
        //        if (sourceUserTable == null)
        //            continue;

        //        var targetUserTable = targetSchema.UserTables[userTable];
        //        if (targetUserTable == null)
        //            continue;

        //        switch (dataComparisonType)
        //        {
        //            case DataComparisonType.Definitions:
        //                if (UserTable.CompareDefinitions(sourceUserTable, targetUserTable))
        //                {
        //                    UserTable.ExceptWith(sourceDataContext, targetDataContext, sourceUserTable, targetUserTable, dataComparisonType);
        //                    if (UserTable.ObjectCount(sourceUserTable) == 0)
        //                        RemoveUserTable(sourceSchema, userTable);
        //                }
        //                break;
        //            case DataComparisonType.Namespaces:
        //                if (UserTable.CompareObjectNames(sourceUserTable, targetUserTable))
        //                {
        //                    UserTable.ExceptWith(sourceDataContext, targetDataContext, sourceUserTable, targetUserTable, dataComparisonType);
        //                    if (UserTable.ObjectCount(sourceUserTable) == 0)
        //                        RemoveUserTable(sourceSchema, userTable);
        //                }
        //                break;
        //            case DataComparisonType.SchemaLevelNamespaces:
        //                if (UserTable.CompareObjectNames(sourceUserTable, targetUserTable))
        //                    RemoveUserTable(sourceSchema, userTable);
        //                break;
        //        }
        //    }

        //    var matchingViews = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        //    matchingViews.UnionWith(sourceSchema.Views.Keys);
        //    matchingViews.IntersectWith(targetSchema.Views.Keys);
            
        //    foreach (var view in matchingViews)
        //    {
        //        var sourceView = sourceSchema.Views[view];
        //        if (sourceView == null)
        //            continue;

        //        var targetView = targetSchema.Views[view];
        //        if (targetView == null)
        //            continue;

        //        switch (dataComparisonType)
        //        {
        //            case DataComparisonType.Definitions:
        //                if (BaseModule.CompareDefinitions(sourceView, targetView))
        //                    RemoveView(sourceSchema, view);
        //                break;
        //            case DataComparisonType.Namespaces:
        //            case DataComparisonType.SchemaLevelNamespaces:
        //                if (BaseModule.CompareObjectNames(sourceView, targetView))
        //                    RemoveView(sourceSchema, view);
        //                break;
        //        }
        //    }
        //}

        //public static Schema FromJson(string json)
        //{
        //    return JsonConvert.DeserializeObject<Schema>(json);
        //}

        //public static void GenerateAlterScripts(DataContext sourceDataContext, DataContext targetDataContext, Schema alteredSchema,
        //    Schema sourceSchema, Schema targetSchema, Schema droppedSchema, Schema createdSchema, Schema matchedSchema,
        //    DataSyncActionsCollection dataSyncActions, DataDependencyBuilder dataDependencyBuilder, DataProperties dataProperties)
        //{
        //    // DataDependecyBuilder would only be null if the call did not
        //    // originate from GenerateAlterCatalogs
        //    if (dataDependencyBuilder == null)
        //    {
        //        dataDependencyBuilder = new DataDependencyBuilder();

        //        if (droppedSchema != null)
        //            dataDependencyBuilder.PreloadDroppedDependencies(droppedSchema);

        //        if (createdSchema != null)
        //            dataDependencyBuilder.PreloadCreatedDependencies(createdSchema);
        //    }

        //    foreach (var alteredAggregateFunction in alteredSchema.AggregateFunctions)
        //        AggregateFunction.GenerateAlterScripts(sourceDataContext, targetDataContext, alteredAggregateFunction, dataSyncActions, dataProperties);

        //    foreach (var alteredInlineTableValuedFunction in alteredSchema.InlineTableValuedFunctions)
        //        InlineTableValuedFunction.GenerateAlterScripts(sourceDataContext, targetDataContext, alteredInlineTableValuedFunction, dataSyncActions, dataProperties);

        //    foreach (var alteredScalarFunction in alteredSchema.ScalarFunctions)
        //        ScalarFunction.GenerateAlterScripts(sourceDataContext, targetDataContext, alteredScalarFunction, dataSyncActions, dataProperties);

        //    foreach (var alteredStoredProcedure in alteredSchema.StoredProcedures)
        //        StoredProcedure.GenerateAlterScripts(sourceDataContext, targetDataContext, alteredStoredProcedure, dataSyncActions, dataProperties);

        //    foreach (var alteredTableValuedFunction in alteredSchema.TableValuedFunctions)
        //        TableValuedFunction.GenerateAlterScripts(sourceDataContext, targetDataContext, alteredTableValuedFunction, dataSyncActions, dataProperties);

        //    foreach (var alteredTrigger in alteredSchema.Triggers)
        //        Trigger.GenerateAlterScripts(sourceDataContext, targetDataContext, alteredTrigger, dataSyncActions, dataProperties);

        //    foreach (var alteredUserDefinedDataType in alteredSchema.UserDefinedDataTypes)
        //    {
        //        UserDefinedDataType.GenerateDropScripts(sourceDataContext, targetDataContext, alteredUserDefinedDataType, dataSyncActions, dataProperties);
        //        UserDefinedDataType.GenerateCreateScripts(sourceDataContext, targetDataContext, alteredUserDefinedDataType, dataSyncActions, dataProperties);
        //    }

        //    foreach (var alteredUserTable in
        //        alteredSchema.UserTables.Where(
        //            alteredUserTable => alteredUserTable.Namespace.EndsWith("<=>IsEqual", StringComparison.OrdinalIgnoreCase)))
        //    {
        //        var alteredUserTableNamespace = alteredUserTable.Namespace.Substring(0, alteredUserTable.Namespace.Length - 10);

        //        var sourceUserTable = sourceSchema.UserTables[alteredUserTableNamespace];
        //        if (sourceUserTable == null)
        //            throw new Exception(string.Format("Source user-table did not exist for the altered user-table {0} during Schema.GenerateAlterScripts() method.", alteredUserTable.Namespace));

        //        var targetUserTable = targetSchema.UserTables[alteredUserTableNamespace];
        //        if (targetUserTable == null)
        //            throw new Exception(string.Format("Target user-table did not exist for the altered user-table {0} during Schema.GenerateAlterScripts() method.", alteredUserTable.Namespace));

        //        UserTable droppedUserTable = null;
        //        if (droppedSchema != null)
        //            droppedUserTable = droppedSchema.UserTables[alteredUserTableNamespace];

        //        UserTable createdUserTable = null;
        //        if (createdSchema != null)
        //            createdUserTable = createdSchema.UserTables[alteredUserTableNamespace];

        //        UserTable matchedUserTable = null;
        //        if (matchedSchema != null)
        //            matchedUserTable = matchedSchema.UserTables[alteredUserTableNamespace];

        //        var addableUserTable = alteredSchema.UserTables[alteredUserTableNamespace + "<+>ToAdd"];
        //        var droppableUserTable = alteredSchema.UserTables[alteredUserTableNamespace + "<x>ToDrop"];
        //        var alterableUserTable = alteredSchema.UserTables[alteredUserTableNamespace + "<~>ToAlter"];
                
        //        if (addableUserTable == null || droppableUserTable == null || alterableUserTable == null)
        //            throw new Exception(string.Format("One or more extension tables ending with <+>ToAdd, <x>ToDrop, and <~>ToAlter did not exist for the altered user-table {0} during Schema.GenerateAlterScripts() method. Please insure all exist in the altered schema before using this method.", alteredUserTable.Namespace));

        //        // Cannot be held within the enumeration, since renaming will modify the collection
        //        // and throw an exception.
        //        var addableUserTableClone = UserTable.Clone(addableUserTable);
        //        addableUserTableClone.ObjectName = alteredUserTableNamespace;
        //        addableUserTableClone.Schema = alteredSchema;
        //        //LinkForeignKeys(addableUserTableClone.Schema);

        //        var droppableUserTableClone = UserTable.Clone(droppableUserTable);
        //        droppableUserTableClone.ObjectName = alteredUserTableNamespace;
        //        droppableUserTableClone.Schema = alteredSchema;
        //        //LinkForeignKeys(droppableUserTableClone.Schema);

        //        var alterableUserTableClone = UserTable.Clone(alterableUserTable);
        //        alterableUserTableClone.ObjectName = alteredUserTableNamespace;
        //        alterableUserTableClone.Schema = alteredSchema;
        //        //LinkForeignKeys(alterableUserTableClone.Schema);

        //        foreach (var identityColumn in addableUserTableClone.IdentityColumns)
        //            UserTable.AddIdentityColumn(alterableUserTableClone, IdentityColumn.Clone(identityColumn));

        //        foreach (var computedColumn in addableUserTableClone.ComputedColumns)
        //            UserTable.AddComputedColumn(alterableUserTableClone, ComputedColumn.Clone(computedColumn));

        //        UserTable.GenerateAlterScripts(sourceDataContext, targetDataContext, alteredUserTable, addableUserTableClone, droppableUserTableClone,
        //            alterableUserTableClone, droppedUserTable, createdUserTable, sourceUserTable, targetUserTable, matchedUserTable, dataSyncActions,
        //            dataDependencyBuilder, dataProperties);
        //    }

        //    foreach (var alteredView in alteredSchema.Views)
        //        View.GenerateAlterScripts(sourceDataContext, targetDataContext, alteredView, dataSyncActions, dataProperties);
        //}

        //public static void GenerateCreateScripts(
        //    DataContext sourceDataContext, DataContext targetDataContext,
        //    Schema createdSchema, Schema sourceSchema, Schema targetSchema,
        //    DataSyncActionsCollection dataSyncActions, DataProperties dataProperties)
        //{
        //    if (!DataProperties.NonRemovableSchemas.Contains(createdSchema.Namespace))
        //        if (sourceSchema != null && targetSchema == null)
        //        {
        //            var dataSyncAction = DataActionFactory.CreateSchema(sourceDataContext, targetDataContext, createdSchema);
        //            if (dataSyncAction != null)
        //                dataSyncActions.Add(dataSyncAction);
        //        }

        //    if (ObjectCount(createdSchema) == 0)
        //        return;

        //    foreach (var aggregateFunction in createdSchema.AggregateFunctions)
        //        AggregateFunction.GenerateCreateScripts(sourceDataContext, targetDataContext, aggregateFunction, dataSyncActions, dataProperties);
        //    foreach (var inlineTableValuedFunction in createdSchema.InlineTableValuedFunctions)
        //        InlineTableValuedFunction.GenerateCreateScripts(sourceDataContext, targetDataContext, inlineTableValuedFunction, dataSyncActions, dataProperties);
        //    foreach (var scalarFunction in createdSchema.ScalarFunctions)
        //        ScalarFunction.GenerateCreateScripts(sourceDataContext, targetDataContext, scalarFunction, dataSyncActions, dataProperties);
        //    foreach (var storedProcedure in createdSchema.StoredProcedures)
        //        StoredProcedure.GenerateCreateScripts(sourceDataContext, targetDataContext, storedProcedure, dataSyncActions, dataProperties);
        //    foreach (var tableValuedFunction in createdSchema.TableValuedFunctions)
        //        TableValuedFunction.GenerateCreateScripts(sourceDataContext, targetDataContext, tableValuedFunction, dataSyncActions, dataProperties);
        //    foreach (var trigger in createdSchema.Triggers)
        //        Trigger.GenerateCreateScripts(sourceDataContext, targetDataContext, trigger, dataSyncActions, dataProperties);
        //    foreach (var userDefinedDataType in createdSchema.UserDefinedDataTypes)
        //        UserDefinedDataType.GenerateCreateScripts(sourceDataContext, targetDataContext, userDefinedDataType, dataSyncActions, dataProperties);
        //    foreach (var userTable in createdSchema.UserTables)
        //        UserTable.GenerateCreateScripts(sourceDataContext, targetDataContext, userTable, dataSyncActions, dataProperties);
        //    foreach (var view in createdSchema.Views)
        //        View.GenerateCreateScripts(sourceDataContext, targetDataContext, view, dataSyncActions, dataProperties);
        //}

        //public static void GenerateDropScripts(DataContext sourceDataContext, DataContext targetDataContext, Schema droppedSchema,
        //    Schema sourceSchema, Schema targetSchema, DataSyncActionsCollection dataSyncActions, DataProperties dataProperties)
        //{
        //    if (ObjectCount(droppedSchema) == 0 || !dataProperties.TightSync)
        //        return;

        //    if (!DataProperties.NonRemovableSchemas.Contains(droppedSchema.Namespace))
        //        if (sourceSchema == null && targetSchema != null && dataProperties.TightSync)
        //        {
        //            var dataSyncAction = DataActionFactory.DropSchema(sourceDataContext, targetDataContext, droppedSchema);
        //            if (dataSyncAction != null)
        //                dataSyncActions.Add(dataSyncAction);
        //        }

        //    foreach (var aggregateFunction in droppedSchema.AggregateFunctions)
        //        AggregateFunction.GenerateDropScripts(sourceDataContext, targetDataContext, aggregateFunction, dataSyncActions, dataProperties);
        //    foreach (var inlineTableValuedFunction in droppedSchema.InlineTableValuedFunctions)
        //        InlineTableValuedFunction.GenerateDropScripts(sourceDataContext, targetDataContext, inlineTableValuedFunction, dataSyncActions, dataProperties);
        //    foreach (var scalarFunction in droppedSchema.ScalarFunctions)
        //        ScalarFunction.GenerateDropScripts(sourceDataContext, targetDataContext, scalarFunction, dataSyncActions, dataProperties);
        //    foreach (var storedProcedure in droppedSchema.StoredProcedures)
        //        StoredProcedure.GenerateDropScripts(sourceDataContext, targetDataContext, storedProcedure, dataSyncActions, dataProperties);
        //    foreach (var tableValuedFunction in droppedSchema.TableValuedFunctions)
        //        TableValuedFunction.GenerateDropScripts(sourceDataContext, targetDataContext, tableValuedFunction, dataSyncActions, dataProperties);
        //    foreach (var trigger in droppedSchema.Triggers)
        //        Trigger.GenerateDropScripts(sourceDataContext, targetDataContext, trigger, dataSyncActions, dataProperties);
        //    foreach (var userDefinedDataType in droppedSchema.UserDefinedDataTypes)
        //        UserDefinedDataType.GenerateDropScripts(sourceDataContext, targetDataContext, userDefinedDataType, dataSyncActions, dataProperties);
        //    foreach (var userTable in droppedSchema.UserTables)
        //        UserTable.GenerateDropScripts(sourceDataContext, targetDataContext, userTable, dataSyncActions, dataProperties);
        //    foreach (var view in droppedSchema.Views)
        //        View.GenerateDropScripts(sourceDataContext, targetDataContext, view, dataSyncActions, dataProperties);
        //}

        ///// <summary>
        ///// Modifies the source Schema to contain only objects that are
        ///// present in the source Schema and in the target Schema.
        ///// </summary>
        ///// <param name="sourceDataContext">The source DataContext.</param>
        ///// <param name="targetDataContext">The target DataContext.</param>
        ///// <param name="sourceSchema">The source Schema.</param>
        ///// <param name="targetSchema">The target schema.</param>
        ///// <param name="dataComparisonType">
        ///// The completeness of comparisons between matching objects.
        ///// </param>
        //public static void IntersectWith(DataContext sourceDataContext, DataContext targetDataContext, Schema sourceSchema,
        //    Schema targetSchema, DataComparisonType dataComparisonType = DataComparisonType.SchemaLevelNamespaces)
        //{
        //    var matchingAggregateFunctions = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        //    var removableAggregateFunctions = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        //    matchingAggregateFunctions.UnionWith(sourceSchema.AggregateFunctions.Keys);
        //    matchingAggregateFunctions.IntersectWith(targetSchema.AggregateFunctions.Keys);

        //    removableAggregateFunctions.UnionWith(sourceSchema.AggregateFunctions.Keys);
        //    removableAggregateFunctions.ExceptWith(matchingAggregateFunctions);

        //    foreach (var aggregateFunction in removableAggregateFunctions)
        //        RemoveAggregateFunction(sourceSchema, aggregateFunction);

        //    foreach (var aggregateFunction in matchingAggregateFunctions)
        //    {
        //        var sourceAggregateFunction = sourceSchema.AggregateFunctions[aggregateFunction];
        //        if (sourceAggregateFunction == null)
        //            continue;

        //        var targetAggregateFunction = targetSchema.AggregateFunctions[aggregateFunction];
        //        if (targetAggregateFunction == null)
        //            continue;

        //        switch (dataComparisonType)
        //        {
        //            case DataComparisonType.Definitions:
        //                if (!BaseModule.CompareDefinitions(sourceAggregateFunction, targetAggregateFunction))
        //                    RemoveAggregateFunction(sourceSchema, aggregateFunction);
        //                break;
        //            case DataComparisonType.Namespaces:
        //            case DataComparisonType.SchemaLevelNamespaces:
        //                if (!BaseModule.CompareObjectNames(sourceAggregateFunction, targetAggregateFunction))
        //                    RemoveAggregateFunction(sourceSchema, aggregateFunction);
        //                break;
        //        }
        //    }

        //    var matchingInlineTableValuedFunctions = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        //    var removableInlineTableValuedFunctions = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        //    matchingInlineTableValuedFunctions.UnionWith(sourceSchema.InlineTableValuedFunctions.Keys);
        //    matchingInlineTableValuedFunctions.IntersectWith(targetSchema.InlineTableValuedFunctions.Keys);

        //    removableInlineTableValuedFunctions.UnionWith(sourceSchema.InlineTableValuedFunctions.Keys);
        //    removableInlineTableValuedFunctions.ExceptWith(matchingInlineTableValuedFunctions);

        //    foreach (var inlineTableValuedFunction in removableInlineTableValuedFunctions)
        //        RemoveInlineTableValuedFunction(sourceSchema, inlineTableValuedFunction);

        //    foreach (var inlineTableValuedFunction in matchingInlineTableValuedFunctions)
        //    {
        //        var sourceInlineTableValuedFunction = sourceSchema.InlineTableValuedFunctions[inlineTableValuedFunction];
        //        if (sourceInlineTableValuedFunction == null)
        //            continue;

        //        var targetInlineTableValuedFunction = targetSchema.InlineTableValuedFunctions[inlineTableValuedFunction];
        //        if (targetInlineTableValuedFunction == null)
        //            continue;

        //        switch (dataComparisonType)
        //        {
        //            case DataComparisonType.Definitions:
        //                if (!BaseModule.CompareDefinitions(sourceInlineTableValuedFunction, targetInlineTableValuedFunction))
        //                    RemoveInlineTableValuedFunction(sourceSchema, inlineTableValuedFunction);
        //                break;
        //            case DataComparisonType.Namespaces:
        //            case DataComparisonType.SchemaLevelNamespaces:
        //                if (!BaseModule.CompareObjectNames(sourceInlineTableValuedFunction, targetInlineTableValuedFunction))
        //                    RemoveInlineTableValuedFunction(sourceSchema, inlineTableValuedFunction);
        //                break;
        //        }
        //    }

        //    var matchingScalarFunctions = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        //    var removableScalarFunctions = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        //    matchingScalarFunctions.UnionWith(sourceSchema.ScalarFunctions.Keys);
        //    matchingScalarFunctions.IntersectWith(targetSchema.ScalarFunctions.Keys);

        //    removableScalarFunctions.UnionWith(sourceSchema.ScalarFunctions.Keys);
        //    removableScalarFunctions.ExceptWith(matchingScalarFunctions);

        //    foreach (var scalarFunction in removableScalarFunctions)
        //        RemoveScalarFunction(sourceSchema, scalarFunction);

        //    foreach (var scalarFunction in matchingScalarFunctions)
        //    {
        //        var sourceScalarFunction = sourceSchema.ScalarFunctions[scalarFunction];
        //        if (sourceScalarFunction == null)
        //            continue;

        //        var targetScalarFunction = targetSchema.ScalarFunctions[scalarFunction];
        //        if (targetScalarFunction == null)
        //            continue;

        //        switch (dataComparisonType)
        //        {
        //            case DataComparisonType.Definitions:
        //                if (!BaseModule.CompareDefinitions(sourceScalarFunction, targetScalarFunction))
        //                    RemoveScalarFunction(sourceSchema, scalarFunction);
        //                break;
        //            case DataComparisonType.Namespaces:
        //            case DataComparisonType.SchemaLevelNamespaces:
        //                if (!BaseModule.CompareObjectNames(sourceScalarFunction, targetScalarFunction))
        //                    RemoveScalarFunction(sourceSchema, scalarFunction);
        //                break;
        //        }
        //    }

        //    var matchingStoredProcedures = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        //    var removableStoredProcedures = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        //    matchingStoredProcedures.UnionWith(sourceSchema.StoredProcedures.Keys);
        //    matchingStoredProcedures.IntersectWith(targetSchema.StoredProcedures.Keys);

        //    removableStoredProcedures.UnionWith(sourceSchema.StoredProcedures.Keys);
        //    removableStoredProcedures.ExceptWith(matchingStoredProcedures);

        //    foreach (var storedProcedure in removableStoredProcedures)
        //        RemoveStoredProcedure(sourceSchema, storedProcedure);

        //    foreach (var storedProcedure in matchingStoredProcedures)
        //    {
        //        var sourceStoredProcedure = sourceSchema.StoredProcedures[storedProcedure];
        //        if (sourceStoredProcedure == null)
        //            continue;

        //        var targetStoredProcedure = targetSchema.StoredProcedures[storedProcedure];
        //        if (targetStoredProcedure == null)
        //            continue;

        //        switch (dataComparisonType)
        //        {
        //            case DataComparisonType.Definitions:
        //                if (!BaseModule.CompareDefinitions(sourceStoredProcedure, targetStoredProcedure))
        //                    RemoveStoredProcedure(sourceSchema, storedProcedure);
        //                break;
        //            case DataComparisonType.Namespaces:
        //            case DataComparisonType.SchemaLevelNamespaces:
        //                if (!BaseModule.CompareObjectNames(sourceStoredProcedure, targetStoredProcedure))
        //                    RemoveStoredProcedure(sourceSchema, storedProcedure);
        //                break;
        //        }
        //    }

        //    var matchingTableValuedFunctions = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        //    var removableTableValuedFunctions = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        //    matchingTableValuedFunctions.UnionWith(sourceSchema.TableValuedFunctions.Keys);
        //    matchingTableValuedFunctions.IntersectWith(targetSchema.TableValuedFunctions.Keys);

        //    removableTableValuedFunctions.UnionWith(sourceSchema.TableValuedFunctions.Keys);
        //    removableTableValuedFunctions.ExceptWith(matchingTableValuedFunctions);

        //    foreach (var tableValuedFunction in removableTableValuedFunctions)
        //        RemoveTableValuedFunction(sourceSchema, tableValuedFunction);

        //    foreach (var tableValuedFunction in matchingTableValuedFunctions)
        //    {
        //        var sourceTableValuedFunction = sourceSchema.TableValuedFunctions[tableValuedFunction];
        //        if (sourceTableValuedFunction == null)
        //            continue;

        //        var targetTableValuedFunction = targetSchema.TableValuedFunctions[tableValuedFunction];
        //        if (targetTableValuedFunction == null)
        //            continue;

        //        switch (dataComparisonType)
        //        {
        //            case DataComparisonType.Definitions:
        //                if (!BaseModule.CompareDefinitions(sourceTableValuedFunction, targetTableValuedFunction))
        //                    RemoveTableValuedFunction(sourceSchema, tableValuedFunction);
        //                break;
        //            case DataComparisonType.Namespaces:
        //            case DataComparisonType.SchemaLevelNamespaces:
        //                if (!BaseModule.CompareDefinitions(sourceTableValuedFunction, targetTableValuedFunction))
        //                    RemoveTableValuedFunction(sourceSchema, tableValuedFunction);
        //                break;
        //        }
        //    }

        //    var matchingTriggers = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        //    var removableTriggers = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        //    matchingTriggers.UnionWith(sourceSchema.Triggers.Keys);
        //    matchingTriggers.IntersectWith(targetSchema.Triggers.Keys);

        //    removableTriggers.UnionWith(sourceSchema.Triggers.Keys);
        //    removableTriggers.ExceptWith(matchingTriggers);

        //    foreach (var trigger in removableTriggers)
        //        RemoveTrigger(sourceSchema, trigger);

        //    foreach (var trigger in matchingTriggers)
        //    {
        //        var sourceTrigger = sourceSchema.Triggers[trigger];
        //        if (sourceTrigger == null)
        //            continue;

        //        var targetTrigger = targetSchema.Triggers[trigger];
        //        if (targetTrigger == null)
        //            continue;

        //        switch (dataComparisonType)
        //        {
        //            case DataComparisonType.Definitions:
        //                if (!Trigger.CompareDefinitions(sourceTrigger, targetTrigger))
        //                    RemoveTrigger(sourceSchema, trigger);
        //                break;
        //            case DataComparisonType.Namespaces:
        //            case DataComparisonType.SchemaLevelNamespaces:
        //                if (!BaseModule.CompareObjectNames(sourceTrigger, targetTrigger))
        //                    RemoveTrigger(sourceSchema, trigger);
        //                break;
        //        }
        //    }

        //    var matchingUserDefinedDataTypes = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        //    var removableUserDefinedDataTypes = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        //    matchingUserDefinedDataTypes.UnionWith(sourceSchema.UserDefinedDataTypes.Keys);
        //    matchingUserDefinedDataTypes.IntersectWith(targetSchema.UserDefinedDataTypes.Keys);

        //    removableUserDefinedDataTypes.UnionWith(sourceSchema.UserDefinedDataTypes.Keys);
        //    removableUserDefinedDataTypes.ExceptWith(matchingUserDefinedDataTypes);

        //    foreach (var userDefinedDataType in removableUserDefinedDataTypes)
        //        RemoveUserDefinedDataType(sourceSchema, userDefinedDataType);

        //    foreach (var userDefinedDataType in matchingUserDefinedDataTypes)
        //    {
        //        var sourceUserDefinedDataType = sourceSchema.UserDefinedDataTypes[userDefinedDataType];
        //        if (sourceUserDefinedDataType == null)
        //            continue;

        //        var targetUserDefinedDataType = targetSchema.UserDefinedDataTypes[userDefinedDataType];
        //        if (targetUserDefinedDataType == null)
        //            continue;

        //        switch (dataComparisonType)
        //        {
        //            case DataComparisonType.Definitions:
        //                if (!UserDefinedDataType.CompareDefinitions(sourceUserDefinedDataType, targetUserDefinedDataType))
        //                    RemoveUserDefinedDataType(sourceSchema, userDefinedDataType);
        //                break;
        //            case DataComparisonType.Namespaces:
        //            case DataComparisonType.SchemaLevelNamespaces:
        //                if (!UserDefinedDataType.CompareObjectNames(sourceUserDefinedDataType, targetUserDefinedDataType))
        //                    RemoveUserDefinedDataType(sourceSchema, userDefinedDataType);
        //                break;
        //        }
        //    }

        //    var matchingUserTables = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        //    var removableUserTables = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        //    matchingUserTables.UnionWith(sourceSchema.UserTables.Keys);
        //    matchingUserTables.IntersectWith(targetSchema.UserTables.Keys);

        //    removableUserTables.UnionWith(sourceSchema.UserTables.Keys);
        //    removableUserTables.ExceptWith(matchingUserTables);

        //    foreach (var userTable in removableUserTables)
        //        RemoveUserTable(sourceSchema, userTable);
            
        //    foreach (var userTable in matchingUserTables)
        //    {
        //        var sourceUserTable = sourceSchema.UserTables[userTable];
        //        if (sourceUserTable == null)
        //            continue;

        //        var targetUserTable = targetSchema.UserTables[userTable];
        //        if (targetUserTable == null)
        //            continue;

        //        switch (dataComparisonType)
        //        {
        //            case DataComparisonType.Definitions:
        //                if (UserTable.CompareDefinitions(sourceUserTable, targetUserTable))
        //                {
        //                    UserTable.IntersectWith(sourceDataContext, targetDataContext, sourceUserTable, targetUserTable, dataComparisonType);
        //                    if (UserTable.ObjectCount(sourceUserTable) == 0)
        //                        RemoveUserTable(sourceSchema, userTable);
        //                }
        //                break;
        //            case DataComparisonType.Namespaces:
        //                if (UserTable.CompareObjectNames(sourceUserTable, targetUserTable))
        //                {
        //                    UserTable.IntersectWith(sourceDataContext, targetDataContext, sourceUserTable, targetUserTable, dataComparisonType);
        //                    if (UserTable.ObjectCount(sourceUserTable) == 0)
        //                        RemoveUserTable(sourceSchema, userTable);
        //                }
        //                else
        //                {
        //                    RemoveUserTable(sourceSchema, userTable);
        //                }
        //                break;
        //            case DataComparisonType.SchemaLevelNamespaces:
        //                if (!UserTable.CompareObjectNames(sourceUserTable, targetUserTable))
        //                    RemoveUserTable(sourceSchema, userTable);
        //                break;
        //        }
        //    }

        //    var matchingViews = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        //    var removableViews = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        //    matchingViews.UnionWith(sourceSchema.Views.Keys);
        //    matchingViews.IntersectWith(targetSchema.Views.Keys);

        //    removableViews.UnionWith(sourceSchema.Views.Keys);
        //    removableViews.ExceptWith(matchingViews);

        //    foreach (var view in removableViews)
        //        RemoveView(sourceSchema, view);

        //    foreach (var view in matchingViews)
        //    {
        //        var sourceView = sourceSchema.Views[view];
        //        if (sourceView == null)
        //            continue;

        //        var targetView = targetSchema.Views[view];
        //        if (targetView == null)
        //            continue;

        //        switch (dataComparisonType)
        //        {
        //            case DataComparisonType.Definitions:
        //                if (!BaseModule.CompareDefinitions(sourceView, targetView))
        //                    RemoveView(sourceSchema, view);
        //                break;
        //            case DataComparisonType.Namespaces:
        //            case DataComparisonType.SchemaLevelNamespaces:
        //                if (!BaseModule.CompareObjectNames(sourceView, targetView))
        //                    RemoveView(sourceSchema, view);
        //                break;
        //        }
        //    }
        //}

        ///// <summary>
        ///// This method is called automatically through a chain of calls after Catalog.Clone()
        ///// method has been called and will simply return if the Catalog... schema.Catalog...
        ///// is equal to null. This method has been added to assist in populating
        ///// Catalog.ForeignKeyPool and Catalog.ReferencedUserTablePool in case anything custom
        ///// has to be done for any reason to those lists. UserTable.AddForeignKey automatically
        ///// calls ForeignKey.LinkForeignKey and this method only needs to be used if a custom
        ///// cloning method is created or after a serialization operation has completed and the
        ///// calls should only be channeled through the chain of objects originating from the
        ///// Catalog object as no action will take place unless the object you are passing in
        ///// has a Catalog and if the foreign keys do not already exist in those lists.
        ///// </summary>
        ///// <param name="schema">The schema with user-tables that have foreign keys to link.</param>
        //public static void LinkForeignKeys(Schema schema)
        //{
        //    if (schema.Catalog == null)
        //        return;

        //    foreach (var userTable in schema.UserTables.Where(
        //        userTable => userTable.ForeignKeys.Count > 0))
        //        UserTable.LinkForeignKeys(userTable);
        //}

        public static long ObjectCount(Schema schema, bool deepCount = false)
        {
            var count = (long)schema.AggregateFunctions.Count;
            count += schema.InlineTableValuedFunctions.Count;
            count += schema.ScalarFunctions.Count;
            count += schema.StoredProcedures.Count;
            count += schema.TableValuedFunctions.Count;
            count += schema.Triggers.Count;
            count += schema.UserDefinedDataTypes.Count;
            count += schema.UserTables.Count;
            count += schema.Views.Count;

            if (!deepCount)
                return count;

            return count +
                   schema.UserTables.Sum(
                       userTable => UserTable.ObjectCount(userTable, true));
        }

        public static void RemoveAggregateFunction(Schema schema, string objectNamespace)
        {
            schema.AggregateFunctions.Remove(objectNamespace);
        }

        public static void RemoveAggregateFunction(Schema schema, AggregateFunction aggregateFunction)
        {
            schema.AggregateFunctions.Remove(aggregateFunction.Namespace);
        }

        public static void RemoveInlineTableValuedFunction(Schema schema, string objectNamespace)
        {
            schema.InlineTableValuedFunctions.Remove(objectNamespace);
        }

        public static void RemoveInlineTableValuedFunction(Schema schema, InlineTableValuedFunction inlineTableValuedFunction)
        {
            schema.InlineTableValuedFunctions.Remove(inlineTableValuedFunction.Namespace);
        }

        public static void RemoveScalarFunction(Schema schema, string objectNamespace)
        {
            schema.ScalarFunctions.Remove(objectNamespace);
        }

        public static void RemoveScalarFunction(Schema schema, ScalarFunction scalarFunction)
        {
            schema.ScalarFunctions.Remove(scalarFunction.Namespace);
        }

        public static void RemoveStoredProcedure(Schema schema, string objectNamespace)
        {
            schema.StoredProcedures.Remove(objectNamespace);
        }

        public static void RemoveStoredProcedure(Schema schema, StoredProcedure storedProcedure)
        {
            schema.StoredProcedures.Remove(storedProcedure.Namespace);
        }

        public static void RemoveTableValuedFunction(Schema schema, string objectNamespace)
        {
            schema.TableValuedFunctions.Remove(objectNamespace);
        }

        public static void RemoveTableValuedFunction(Schema schema, TableValuedFunction tableValuedFunction)
        {
            schema.TableValuedFunctions.Remove(tableValuedFunction.Namespace);
        }

        public static void RemoveTrigger(Schema schema, string objectNamespace)
        {
            schema.Triggers.Remove(objectNamespace);
        }

        public static void RemoveTrigger(Schema schema, Trigger trigger)
        {
            schema.Triggers.Remove(trigger.Namespace);
        }

        public static void RemoveUserDefinedDataType(Schema schema, string objectNamespace)
        {
            schema.UserDefinedDataTypes.Remove(objectNamespace);
        }

        public static void RemoveUserDefinedDataType(Schema schema, UserDefinedDataType userDefinedDataType)
        {
            schema.UserDefinedDataTypes.Remove(userDefinedDataType.Namespace);
        }

        public static void RemoveUserTable(Schema schema, string objectNamespace)
        {
            schema.UserTables.Remove(objectNamespace);
        }

        public static void RemoveUserTable(Schema schema, UserTable userTable)
        {
            schema.UserTables.Remove(userTable.Namespace);
        }

        public static void RemoveView(Schema schema, string objectNamespace)
        {
            schema.Views.Remove(objectNamespace);
        }

        public static void RemoveView(Schema schema, View view)
        {
            schema.Views.Remove(view.Namespace);
        }

        public static void RenameAggregateFunction(Schema schema, string objectNamespace, string newObjectName)
        {
            var aggregateFunction = schema.AggregateFunctions[objectNamespace];
            if (aggregateFunction == null)
                throw new Exception(string.Format("{0} could not be found in {1} {2} to rename to {3}",
                    objectNamespace, schema.Description, schema.Namespace, newObjectName));

            schema.AggregateFunctions.Rename(aggregateFunction, newObjectName);
        }

        public static void RenameInlineTableValuedFunction(Schema schema, string objectNamespace, string newObjectName)
        {
            var inlineTableValuedFunction = schema.InlineTableValuedFunctions[objectNamespace];
            if (inlineTableValuedFunction == null)
                throw new Exception(string.Format("{0} could not be found in {1} {2} to rename to {3}",
                    objectNamespace, schema.Description, schema.Namespace, newObjectName));

            schema.InlineTableValuedFunctions.Rename(inlineTableValuedFunction, newObjectName);
        }

        public static void RenameScalarFunction(Schema schema, string objectNamespace, string newObjectName)
        {
            var scalarFunction = schema.ScalarFunctions[objectNamespace];
            if (scalarFunction == null)
                throw new Exception(string.Format("{0} could not be found in {1} {2} to rename to {3}",
                    objectNamespace, schema.Description, schema.Namespace, newObjectName));

            schema.ScalarFunctions.Rename(scalarFunction, newObjectName);
        }

        public static void RenameStoredProcedure(Schema schema, string objectNamespace, string newObjectName)
        {
            var storedProcedure = schema.StoredProcedures[objectNamespace];
            if (storedProcedure == null)
                throw new Exception(string.Format("{0} could not be found in {1} {2} to rename to {3}",
                    objectNamespace, schema.Description, schema.Namespace, newObjectName));

            schema.StoredProcedures.Rename(storedProcedure, newObjectName);
        }

        public static void RenameTableValuedFunction(Schema schema, string objectNamespace, string newObjectName)
        {
            var tableValuedFunction = schema.TableValuedFunctions[objectNamespace];
            if (tableValuedFunction == null)
                throw new Exception(string.Format("{0} could not be found in {1} {2} to rename to {3}",
                    objectNamespace, schema.Description, schema.Namespace, newObjectName));

            schema.TableValuedFunctions.Rename(tableValuedFunction, newObjectName);
        }

        public static void RenameTrigger(Schema schema, string objectNamespace, string newObjectName)
        {
            var trigger = schema.Triggers[objectNamespace];
            if (trigger == null)
                throw new Exception(string.Format("{0} could not be found in {1} {2} to rename to {3}",
                    objectNamespace, schema.Description, schema.Namespace, newObjectName));

            schema.Triggers.Rename(trigger, newObjectName);
        }

        public static void RenameUserDefinedDataType(Schema schema, string objectNamespace, string newObjectName)
        {
            var userDefinedDataType = schema.UserDefinedDataTypes[objectNamespace];
            if (userDefinedDataType == null)
                throw new Exception(string.Format("{0} could not be found in {1} {2} to rename to {3}",
                    objectNamespace, schema.Description, schema.Namespace, newObjectName));

            schema.UserDefinedDataTypes.Rename(userDefinedDataType, newObjectName);
        }

        public static void RenameUserTable(Schema schema, string objectNamespace, string newObjectName)
        {
            var userTable = schema.UserTables[objectNamespace];
            if (userTable == null)
                throw new Exception(string.Format("{0} could not be found in {1} {2} to rename to {3}",
                    objectNamespace, schema.Description, schema.Namespace, newObjectName));

            schema.UserTables.Rename(userTable, newObjectName);
        }

        public static void RenameView(Schema schema, string objectNamespace, string newObjectName)
        {
            var view = schema.Views[objectNamespace];
            if (view == null)
                throw new Exception(string.Format("{0} could not be found in {1} {2} to rename to {3}",
                    objectNamespace, schema.Description, schema.Namespace, newObjectName));

            schema.Views.Rename(view, newObjectName);
        }

        /// <summary>
        /// Shallow Clone...
        /// A clone of this class's instance specific metadata.
        /// </summary>
        /// <param name="schema">The schema to shallow clone.</param>
        /// <returns>A clone of this class's instance specific metadata.</returns>
        public static Schema ShallowClone(Schema schema)
        {
            return new Schema
            {
                ObjectName = schema.ObjectName
            };
        }

        //public static string ToJson(Schema schema, Formatting formatting = Formatting.Indented)
        //{
        //    return JsonConvert.SerializeObject(schema, formatting);
        //}

        ///// <summary>
        ///// Modifies the source Schema to contain all objects that are
        ///// present in both iteself and in the target Schema.
        ///// </summary>
        ///// <param name="sourceDataContext">The source DataContext.</param>
        ///// <param name="targetDataContext">The target DataContext.</param>
        ///// <param name="sourceSchema">The source Schema.</param>
        ///// <param name="targetSchema">The target schema.</param>
        ///// <param name="dataComparisonType">
        ///// The completeness of comparisons between matching objects.
        ///// </param>
        //public static void UnionWith(DataContext sourceDataContext, DataContext targetDataContext, Schema sourceSchema, Schema targetSchema,
        //    DataComparisonType dataComparisonType = DataComparisonType.SchemaLevelNamespaces)
        //{
        //    var matchingAggregateFunctions = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        //    var addableAggregateFunctions = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        //    matchingAggregateFunctions.UnionWith(sourceSchema.AggregateFunctions.Keys);
        //    matchingAggregateFunctions.IntersectWith(targetSchema.AggregateFunctions.Keys);

        //    addableAggregateFunctions.UnionWith(targetSchema.AggregateFunctions.Keys);
        //    addableAggregateFunctions.ExceptWith(matchingAggregateFunctions);

        //    foreach (var aggregateFunction in addableAggregateFunctions)
        //    {
        //        var targetAggregateFunction = targetSchema.AggregateFunctions[aggregateFunction];
        //        if (targetAggregateFunction == null)
        //            continue;

        //        AddAggregateFunction(sourceSchema, AggregateFunction.Clone(targetAggregateFunction));
        //    }

        //    var matchingInlineTableValuedFunctions = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        //    var addableInlineTableValuedFunctions = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        //    matchingInlineTableValuedFunctions.UnionWith(sourceSchema.InlineTableValuedFunctions.Keys);
        //    matchingInlineTableValuedFunctions.IntersectWith(targetSchema.InlineTableValuedFunctions.Keys);

        //    addableInlineTableValuedFunctions.UnionWith(targetSchema.InlineTableValuedFunctions.Keys);
        //    addableInlineTableValuedFunctions.ExceptWith(matchingInlineTableValuedFunctions);

        //    foreach (var inlineTableValuedFunction in addableInlineTableValuedFunctions)
        //    {
        //        var targetInlineTableValuedFunction = targetSchema.InlineTableValuedFunctions[inlineTableValuedFunction];
        //        if (targetInlineTableValuedFunction == null)
        //            continue;

        //        AddInlineTableValuedFunction(sourceSchema, InlineTableValuedFunction.Clone(targetInlineTableValuedFunction));
        //    }

        //    var matchingScalarFunctions = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        //    var addableScalarFunctions = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        //    matchingScalarFunctions.UnionWith(sourceSchema.ScalarFunctions.Keys);
        //    matchingScalarFunctions.IntersectWith(targetSchema.ScalarFunctions.Keys);

        //    addableScalarFunctions.UnionWith(targetSchema.ScalarFunctions.Keys);
        //    addableScalarFunctions.ExceptWith(matchingScalarFunctions);

        //    foreach (var scalarFunction in addableScalarFunctions)
        //    {
        //        var targetScalarFunction = targetSchema.ScalarFunctions[scalarFunction];
        //        if (targetScalarFunction == null)
        //            continue;

        //        AddScalarFunction(sourceSchema, ScalarFunction.Clone(targetScalarFunction));
        //    }

        //    var matchingStoredProcedures = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        //    var addableStoredProcedures = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        //    matchingStoredProcedures.UnionWith(sourceSchema.StoredProcedures.Keys);
        //    matchingStoredProcedures.IntersectWith(targetSchema.StoredProcedures.Keys);

        //    addableStoredProcedures.UnionWith(targetSchema.StoredProcedures.Keys);
        //    addableStoredProcedures.ExceptWith(matchingStoredProcedures);

        //    foreach (var storedProcedure in addableStoredProcedures)
        //    {
        //        var targetStoredProcedure = targetSchema.StoredProcedures[storedProcedure];
        //        if (targetStoredProcedure == null)
        //            continue;

        //        AddStoredProcedure(sourceSchema, StoredProcedure.Clone(targetStoredProcedure));
        //    }

        //    var matchingTableValuedFunctions = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        //    var addableTableValuedFunctions = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        //    matchingTableValuedFunctions.UnionWith(sourceSchema.TableValuedFunctions.Keys);
        //    matchingTableValuedFunctions.IntersectWith(targetSchema.TableValuedFunctions.Keys);

        //    addableTableValuedFunctions.UnionWith(targetSchema.TableValuedFunctions.Keys);
        //    addableTableValuedFunctions.ExceptWith(matchingTableValuedFunctions);

        //    foreach (var tableValuedFunction in addableTableValuedFunctions)
        //    {
        //        var targetTableValuedFunction = targetSchema.TableValuedFunctions[tableValuedFunction];
        //        if (targetTableValuedFunction == null)
        //            continue;

        //        AddTableValuedFunction(sourceSchema, TableValuedFunction.Clone(targetTableValuedFunction));
        //    }

        //    var matchingTriggers = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        //    var addableTriggers = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        //    matchingTriggers.UnionWith(sourceSchema.Triggers.Keys);
        //    matchingTriggers.IntersectWith(targetSchema.Triggers.Keys);

        //    addableTriggers.UnionWith(targetSchema.Triggers.Keys);
        //    addableTriggers.ExceptWith(matchingTriggers);

        //    foreach (var trigger in addableTriggers)
        //    {
        //        var targetTrigger = targetSchema.Triggers[trigger];
        //        if (targetTrigger == null)
        //            continue;

        //        AddTrigger(sourceSchema, Trigger.Clone(targetTrigger));
        //    }

        //    var matchingUserDefinedDataTypes = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        //    var addableUserDefinedDataTypes = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        //    matchingUserDefinedDataTypes.UnionWith(sourceSchema.UserDefinedDataTypes.Keys);
        //    matchingUserDefinedDataTypes.IntersectWith(targetSchema.UserDefinedDataTypes.Keys);

        //    addableUserDefinedDataTypes.UnionWith(targetSchema.UserDefinedDataTypes.Keys);
        //    addableUserDefinedDataTypes.ExceptWith(matchingUserDefinedDataTypes);

        //    foreach (var userDefinedDataType in addableUserDefinedDataTypes)
        //    {
        //        var targetUserDefinedDataType = targetSchema.UserDefinedDataTypes[userDefinedDataType];
        //        if (targetUserDefinedDataType == null)
        //            continue;

        //        AddUserDefinedDataType(sourceSchema, UserDefinedDataType.Clone(targetUserDefinedDataType));
        //    }

        //    var matchingUserTables = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        //    var addableUserTables = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        //    matchingUserTables.UnionWith(sourceSchema.UserTables.Keys);
        //    matchingUserTables.IntersectWith(targetSchema.UserTables.Keys);

        //    addableUserTables.UnionWith(targetSchema.UserTables.Keys);
        //    addableUserTables.ExceptWith(matchingUserTables);

        //    foreach (var userTable in addableUserTables)
        //    {
        //        var targetUserTable = targetSchema.UserTables[userTable];
        //        if (targetUserTable == null)
        //            continue;

        //        AddUserTable(sourceSchema, UserTable.Clone(targetUserTable));
        //    }

        //    foreach (var userTable in matchingUserTables)
        //    {
        //        var sourceUserTable = sourceSchema.UserTables[userTable];
        //        if (sourceUserTable == null)
        //            continue;

        //        var targetUserTable = targetSchema.UserTables[userTable];
        //        if (targetUserTable == null)
        //            continue;

        //        switch (dataComparisonType)
        //        {
        //            case DataComparisonType.Definitions:
        //                if (UserTable.CompareDefinitions(sourceUserTable, targetUserTable))
        //                    UserTable.UnionWith(sourceUserTable, targetUserTable, sourceDataContext,
        //                        targetDataContext, dataComparisonType);
        //                break;
        //            case DataComparisonType.Namespaces:
        //                if (UserTable.CompareObjectNames(sourceUserTable, targetUserTable))
        //                    UserTable.UnionWith(sourceUserTable, targetUserTable, sourceDataContext,
        //                        targetDataContext, dataComparisonType);
        //                break;
        //            //case DataComparisonType.SchemaLevelNamespaces:
        //            //    // Do Nothing
        //            //    break;
        //        }
        //    }

        //    var matchingViews = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        //    var addableViews = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        //    matchingViews.UnionWith(sourceSchema.Views.Keys);
        //    matchingViews.IntersectWith(targetSchema.Views.Keys);

        //    addableViews.UnionWith(targetSchema.Views.Keys);
        //    addableViews.ExceptWith(matchingViews);

        //    foreach (var view in addableViews)
        //    {
        //        var targetView = targetSchema.Views[view];
        //        if (targetView == null)
        //            continue;

        //        AddView(sourceSchema, View.Clone(targetView));
        //    }
        //}

        //public Schema(SerializationInfo info, StreamingContext context)
        //{
        //    // Holding off on the serialzation in this manner because, this is
        //    // extremely complicated to do in this manner do to data object
        //    // associations, especially
        //    // Set Null Members
        //    Catalog = null;

        //    // Deserialize Members
        //    ObjectName = info.GetString("ObjectName");
        //    Description = info.GetString("Description");

        //    // Deserialize Aggregate Functions
        //    var aggregateFunctions = info.GetInt32("AggregateFunctions");
        //    AggregateFunctions = new DataObjectLookup<AggregateFunction>();

        //    for (var i = 0; i < aggregateFunctions; i++)
        //    {
        //        var aggregateFunction = (AggregateFunction)info.GetValue("AggregateFunction" + i, typeof(AggregateFunction));
        //        aggregateFunction.Schema = this;
        //        AggregateFunctions.Add(aggregateFunction);
        //    }

        //    // Deserialize Inline Table-Valued Functions
        //    var inlineTableValuedFunctions = info.GetInt32("InlineTableValuedFunctions");
        //    InlineTableValuedFunctions = new Dictionary<string, InlineTableValuedFunction>(StringComparer.OrdinalIgnoreCase);

        //    for (var i = 0; i < inlineTableValuedFunctions; i++)
        //    {
        //        var inlineTableValuedFunction = (InlineTableValuedFunction)info.GetValue("InlineTableValuedFunction" + i, typeof(InlineTableValuedFunction));
        //        inlineTableValuedFunction.Schema = this;
        //        InlineTableValuedFunctions.Add(inlineTableValuedFunction.ObjectName, inlineTableValuedFunction);
        //    }

        //    // Deserialize Scalar Functions
        //    var scalarFunctions = info.GetInt32("ScalarFunctions");
        //    ScalarFunctions = new Dictionary<string, ScalarFunction>(StringComparer.OrdinalIgnoreCase);

        //    for (var i = 0; i < scalarFunctions; i++)
        //    {
        //        var scalarFunction = (ScalarFunction)info.GetValue("ScalarFunction" + i, typeof(ScalarFunction));
        //        scalarFunction.Schema = this;
        //        ScalarFunctions.Add(scalarFunction.ObjectName, scalarFunction);
        //    }

        //    // Deserialize Stored Procedures
        //    var storedProcedures = info.GetInt32("StoredProcedures");
        //    StoredProcedures = new Dictionary<string, StoredProcedure>(StringComparer.OrdinalIgnoreCase);

        //    for (var i = 0; i < storedProcedures; i++)
        //    {
        //        var storedProcedure = (StoredProcedure)info.GetValue("StoredProcedure" + i, typeof(StoredProcedure));
        //        storedProcedure.Schema = this;
        //        StoredProcedures.Add(storedProcedure.ObjectName, storedProcedure);
        //    }

        //    // Deserialize Table-Valued Functions
        //    var tableValuedFunctions = info.GetInt32("TableValuedFunctions");
        //    TableValuedFunctions = new Dictionary<string, TableValuedFunction>(StringComparer.OrdinalIgnoreCase);

        //    for (var i = 0; i < tableValuedFunctions; i++)
        //    {
        //        var tableValuedFunction = (TableValuedFunction)info.GetValue("TableValuedFunction" + i, typeof(TableValuedFunction));
        //        tableValuedFunction.Schema = this;
        //        TableValuedFunctions.Add(tableValuedFunction.ObjectName, tableValuedFunction);
        //    }

        //    // Deserialize Triggers
        //    var triggers = info.GetInt32("Triggers");
        //    Triggers = new Dictionary<string, Trigger>(StringComparer.OrdinalIgnoreCase);

        //    for (var i = 0; i < triggers; i++)
        //    {
        //        var trigger = (Trigger)info.GetValue("Trigger" + i, typeof(Trigger));
        //        trigger.Schema = this;
        //        Triggers.Add(trigger.ObjectName, trigger);
        //    }

        //    // Deserialize User-Defined Data Types
        //    var userDefinedDataTypes = info.GetInt32("UserDefinedDataTypes");
        //    UserDefinedDataTypes = new Dictionary<string, UserDefinedDataType>(StringComparer.OrdinalIgnoreCase);

        //    for (var i = 0; i < userDefinedDataTypes; i++)
        //    {
        //        var userDefinedDataType = (UserDefinedDataType)info.GetValue("UserDefinedDataType" + i, typeof(UserDefinedDataType));
        //        userDefinedDataType.Schema = this;
        //        UserDefinedDataTypes.Add(userDefinedDataType.ObjectName, userDefinedDataType);
        //    }

        //    // Deserialize User-Tables
        //    var userTables = info.GetInt32("UserTables");
        //    UserTables = new Dictionary<string, UserTable>(StringComparer.OrdinalIgnoreCase);

        //    for (var i = 0; i < userTables; i++)
        //    {
        //        var userTable = (UserTable)info.GetValue("UserTable" + i, typeof(UserTable));
        //        userTable.Schema = this;
        //        UserTables.Add(userTable.ObjectName, userTable);
        //    }

        //    // Deserialize Views
        //    var views = info.GetInt32("Views");
        //    Views = new Dictionary<string, View>(StringComparer.OrdinalIgnoreCase);

        //    for (var i = 0; i < views; i++)
        //    {
        //        var view = (View)info.GetValue("View" + i, typeof(View));
        //        view.Schema = this;
        //        Views.Add(view.ObjectName, view);
        //    }
        //}

        //public void GetObjectData(SerializationInfo info, StreamingContext context)
        //{
        //    // Holding off on the serialzation in this manner because, this is
        //    // extremely complicated to do in this manner do to data object
        //    // associations, especially
        //    // Serialize Members
        //    info.AddValue("ObjectName", ObjectName);
        //    info.AddValue("Description", Description);

        //    // Serialize Aggregate Functions
        //    info.AddValue("AggregateFunctions", AggregateFunctions.Count);

        //    var i = 0;
        //    foreach (var aggregateFunction in AggregateFunctions)
        //        info.AddValue("AggregateFunction" + i++, aggregateFunction);

        //    // Serialize Inline Table-Valued Functions
        //    info.AddValue("InlineTableValuedFunctions", InlineTableValuedFunctions.Count);

        //    i = 0;
        //    foreach (var inlineTableValuedFunction in InlineTableValuedFunctions.Values)
        //        info.AddValue("InlineTableValuedFunction" + i++, inlineTableValuedFunction);

        //    // Serialize Scalar Functions
        //    info.AddValue("ScalarFunctions", ScalarFunctions.Count);

        //    i = 0;
        //    foreach (var scalarFunction in ScalarFunctions.Values)
        //        info.AddValue("ScalarFunction" + i++, scalarFunction);

        //    // Serialize Stored Procedures
        //    info.AddValue("StoredProcedures", StoredProcedures.Count);

        //    i = 0;
        //    foreach (var storedProcedure in StoredProcedures.Values)
        //        info.AddValue("StoredProcedure" + i++, storedProcedure);

        //    // Serialize Table-Valued Functions
        //    info.AddValue("TableValuedFunctions", TableValuedFunctions.Count);

        //    i = 0;
        //    foreach (var tableValuedFunction in TableValuedFunctions.Values)
        //        info.AddValue("TableValuedFunction" + i++, tableValuedFunction);

        //    // Serialize Triggers
        //    info.AddValue("Triggers", Triggers.Count);

        //    i = 0;
        //    foreach (var trigger in Triggers.Values)
        //        info.AddValue("Trigger" + i++, trigger);

        //    // Serialize User-Defined Data Types
        //    info.AddValue("UserDefinedDataTypes", UserDefinedDataTypes.Count);

        //    i = 0;
        //    foreach (var userDefinedDataType in UserDefinedDataTypes.Values)
        //        info.AddValue("UserDefinedDataType" + i++, userDefinedDataType);

        //    // Serialize User-Tables
        //    info.AddValue("UserTables", UserTables.Count);

        //    i = 0;
        //    foreach (var userTable in UserTables.Values)
        //        info.AddValue("UserTable" + i++, userTable);

        //    // Serialize Views
        //    info.AddValue("Views", Views.Count);

        //    i = 0;
        //    foreach (var view in Views.Values)
        //        info.AddValue("View" + i++, view);
        //}
    }
}
