using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Meta.Net.Data.Enum;
using Meta.Net.Data.Interfaces;
using Meta.Net.Data.Metadata;
using Newtonsoft.Json;

namespace Meta.Net.Data.Objects
{
    [Serializable]
    public class Schema : IDataObject, IDataCatalogBasedObject
    {
		#region Properties (13) 

        public Dictionary<string, AggregateFunction> AggregateFunctions { get; private set; }

		public Catalog Catalog { get; set; }

        public string Description { get; set; }

        public Dictionary<string, InlineTableValuedFunction> InlineTableValuedFunctions { get; private set; }

        public string Namespace
        {
            get { return ObjectName; }
        }

        public string ObjectName
        {
            get
            {
                return _objectName;
            }
            set
            {
                if (Catalog != null)
                {
                    if (Catalog.RenameSchema(Catalog, _objectName, value))
                        _objectName = value;
                }
                else
                {
                    if (string.IsNullOrEmpty(value))
                        return;

                    _objectName = value;
                }
            }
        }

        public Dictionary<string, ScalarFunction> ScalarFunctions { get; private set; }

        public Dictionary<string, StoredProcedure> StoredProcedures { get; private set; }

        public Dictionary<string, TableValuedFunction> TableValuedFunctions { get; private set; }

        public Dictionary<string, Trigger> Triggers { get; private set; }

        public Dictionary<string, UserDefinedDataType> UserDefinedDataTypes { get; private set; }

        public Dictionary<string, UserTable> UserTables { get; private set; }

        public Dictionary<string, View> Views { get; private set; }

		#endregion Properties 

		#region Fields (1) 

        [NonSerialized]
        private string _objectName;

		#endregion Fields 

		#region Constructors (5) 

        public Schema(SerializationInfo info, StreamingContext context)
        {
            // Set Null Members
            Catalog = null;

            // Deserialize Members
            ObjectName = info.GetString("ObjectName");
            Description = info.GetString("Description");

            // Deserialize Aggregate Functions
            var aggregateFunctions = info.GetInt32("AggregateFunctions");
            AggregateFunctions = new Dictionary<string, AggregateFunction>(StringComparer.OrdinalIgnoreCase);

            for (var i = 0; i < aggregateFunctions; i++)
            {
                var aggregateFunction = (AggregateFunction)info.GetValue("AggregateFunction" + i, typeof(AggregateFunction));
                aggregateFunction.Schema = this;
                AggregateFunctions.Add(aggregateFunction.ObjectName, aggregateFunction);
            }

            // Deserialize Inline Table-Valued Functions
            var inlineTableValuedFunctions = info.GetInt32("InlineTableValuedFunctions");
            InlineTableValuedFunctions = new Dictionary<string, InlineTableValuedFunction>(StringComparer.OrdinalIgnoreCase);

            for (var i = 0; i < inlineTableValuedFunctions; i++)
            {
                var inlineTableValuedFunction = (InlineTableValuedFunction)info.GetValue("InlineTableValuedFunction" + i, typeof(InlineTableValuedFunction));
                inlineTableValuedFunction.Schema = this;
                InlineTableValuedFunctions.Add(inlineTableValuedFunction.ObjectName, inlineTableValuedFunction);
            }

            // Deserialize Scalar Functions
            var scalarFunctions = info.GetInt32("ScalarFunctions");
            ScalarFunctions = new Dictionary<string, ScalarFunction>(StringComparer.OrdinalIgnoreCase);

            for (var i = 0; i < scalarFunctions; i++)
            {
                var scalarFunction = (ScalarFunction)info.GetValue("ScalarFunction" + i, typeof(ScalarFunction));
                scalarFunction.Schema = this;
                ScalarFunctions.Add(scalarFunction.ObjectName, scalarFunction);
            }

            // Deserialize Stored Procedures
            var storedProcedures = info.GetInt32("StoredProcedures");
            StoredProcedures = new Dictionary<string, StoredProcedure>(StringComparer.OrdinalIgnoreCase);

            for (var i = 0; i < storedProcedures; i++)
            {
                var storedProcedure = (StoredProcedure)info.GetValue("StoredProcedure" + i, typeof(StoredProcedure));
                storedProcedure.Schema = this;
                StoredProcedures.Add(storedProcedure.ObjectName, storedProcedure);
            }

            // Deserialize Table-Valued Functions
            var tableValuedFunctions = info.GetInt32("TableValuedFunctions");
            TableValuedFunctions = new Dictionary<string, TableValuedFunction>(StringComparer.OrdinalIgnoreCase);

            for (var i = 0; i < tableValuedFunctions; i++)
            {
                var tableValuedFunction = (TableValuedFunction)info.GetValue("TableValuedFunction" + i, typeof(TableValuedFunction));
                tableValuedFunction.Schema = this;
                TableValuedFunctions.Add(tableValuedFunction.ObjectName, tableValuedFunction);
            }

            // Deserialize Triggers
            var triggers = info.GetInt32("Triggers");
            Triggers = new Dictionary<string, Trigger>(StringComparer.OrdinalIgnoreCase);

            for (var i = 0; i < triggers; i++)
            {
                var trigger = (Trigger)info.GetValue("Trigger" + i, typeof(Trigger));
                trigger.Schema = this;
                Triggers.Add(trigger.ObjectName, trigger);
            }

            // Deserialize User-Defined Data Types
            var userDefinedDataTypes = info.GetInt32("UserDefinedDataTypes");
            UserDefinedDataTypes = new Dictionary<string, UserDefinedDataType>(StringComparer.OrdinalIgnoreCase);

            for (var i = 0; i < userDefinedDataTypes; i++)
            {
                var userDefinedDataType = (UserDefinedDataType)info.GetValue("UserDefinedDataType" + i, typeof(UserDefinedDataType));
                userDefinedDataType.Schema = this;
                UserDefinedDataTypes.Add(userDefinedDataType.ObjectName, userDefinedDataType);
            }

            // Deserialize User-Tables
            var userTables = info.GetInt32("UserTables");
            UserTables = new Dictionary<string, UserTable>(StringComparer.OrdinalIgnoreCase);

            for (var i = 0; i < userTables; i++)
            {
                var userTable = (UserTable)info.GetValue("UserTable" + i, typeof(UserTable));
                userTable.Schema = this;
                UserTables.Add(userTable.ObjectName, userTable);
            }

            // Deserialize Views
            var views = info.GetInt32("Views");
            Views = new Dictionary<string, View>(StringComparer.OrdinalIgnoreCase);

            for (var i = 0; i < views; i++)
            {
                var view = (View)info.GetValue("View" + i, typeof(View));
                view.Schema = this;
                Views.Add(view.ObjectName, view);
            }
        }

        public Schema(Catalog catalog, SchemasRow schemasRow)
		{
		    Init(this, catalog, schemasRow.ObjectName);
		}

        public Schema(Catalog catalog, string objectName)
        {
            Init(this, catalog, objectName);
        }

        public Schema(string objectName)
        {
            Init(this, null, objectName);
        }

        public Schema()
        {
            Init(this, null, null);
        }

		#endregion Constructors 

		#region Methods (64) 

		#region Public Methods (63) 

        public static bool AddAggregateFunction(Schema schema, AggregateFunction aggregateFunction)
        {
            if (schema.AggregateFunctions.ContainsKey(aggregateFunction.ObjectName))
                return false;

            if (aggregateFunction.Schema == null)
            {
                aggregateFunction.Schema = schema;
                schema.AggregateFunctions.Add(aggregateFunction.ObjectName, aggregateFunction);
                return true;
            }

            if (aggregateFunction.Schema.Equals(schema))
            {
                schema.AggregateFunctions.Add(aggregateFunction.ObjectName, aggregateFunction);
                return true;
            }

            return false;
        }

        public static bool AddAggregateFunction(Schema schema, string objectName)
        {
            if (string.IsNullOrEmpty(objectName))
                return false;

            if (schema.AggregateFunctions.ContainsKey(objectName))
                return false;

            var aggregateFunction = new AggregateFunction(schema, objectName);
            schema.AggregateFunctions.Add(objectName, aggregateFunction);
            
            return true;
        }

        public static bool AddInlineTableValuedFunction(Schema schema, InlineTableValuedFunction inlineTableValuedFunction)
        {
            if (schema.InlineTableValuedFunctions.ContainsKey(inlineTableValuedFunction.ObjectName))
                return false;

            if (inlineTableValuedFunction.Schema == null)
            {
                inlineTableValuedFunction.Schema = schema;
                schema.InlineTableValuedFunctions.Add(inlineTableValuedFunction.ObjectName, inlineTableValuedFunction);
                return true;
            }

            if (inlineTableValuedFunction.Schema.Equals(schema))
            {
                schema.InlineTableValuedFunctions.Add(inlineTableValuedFunction.ObjectName, inlineTableValuedFunction);
                return true;
            }

            return false;
        }

        public static bool AddInlineTableValuedFunction(Schema schema, string objectName)
        {
            if (string.IsNullOrEmpty(objectName))
                return false;

            if (schema.InlineTableValuedFunctions.ContainsKey(objectName))
                return false;

            var inlineTableValuedFunction = new InlineTableValuedFunction(schema, objectName);
            schema.InlineTableValuedFunctions.Add(objectName, inlineTableValuedFunction);
            
            return true;
        }

        public static bool AddScalarFunction(Schema schema, ScalarFunction scalarFunction)
        {
            if (schema.ScalarFunctions.ContainsKey(scalarFunction.ObjectName))
                return false;

            if (scalarFunction.Schema == null)
            {
                scalarFunction.Schema = schema;
                schema.ScalarFunctions.Add(scalarFunction.ObjectName, scalarFunction);
                return true;
            }

            if (scalarFunction.Schema.Equals(schema))
            {
                schema.ScalarFunctions.Add(scalarFunction.ObjectName, scalarFunction);
                return true;
            }

            return false;
        }

        public static bool AddScalarFunction(Schema schema, string objectName)
        {
            if (string.IsNullOrEmpty(objectName))
                return false;

            if (schema.ScalarFunctions.ContainsKey(objectName))
                return false;

            var scalarFunction = new ScalarFunction(schema, objectName);
            schema.ScalarFunctions.Add(objectName, scalarFunction);

            return true;
        }

        public static bool AddStoredProcedure(Schema schema, StoredProcedure storedProcedure)
        {
            if (schema.StoredProcedures.ContainsKey(storedProcedure.ObjectName))
                return false;

            if (storedProcedure.Schema == null)
            {
                storedProcedure.Schema = schema;
                schema.StoredProcedures.Add(storedProcedure.ObjectName, storedProcedure);
                return true;
            }

            if (storedProcedure.Schema.Equals(schema))
            {
                schema.StoredProcedures.Add(storedProcedure.ObjectName, storedProcedure);
                return true;
            }

            return false;
        }

        public static bool AddStoredProcedure(Schema schema, string objectName)
        {
            if (string.IsNullOrEmpty(objectName))
                return false;

            if (schema.StoredProcedures.ContainsKey(objectName))
                return false;

            var storedProcedure = new StoredProcedure(schema, objectName);
            schema.StoredProcedures.Add(objectName, storedProcedure);
            
            return true;
        }

        public static bool AddTableValuedFunction(Schema schema, TableValuedFunction tableValuedFunction)
        {
            if (schema.TableValuedFunctions.ContainsKey(tableValuedFunction.ObjectName))
                return false;

            if (tableValuedFunction.Schema == null)
            {
                tableValuedFunction.Schema = schema;
                schema.TableValuedFunctions.Add(tableValuedFunction.ObjectName, tableValuedFunction);
                return true;
            }

            if (tableValuedFunction.Schema.Equals(schema))
            {
                schema.TableValuedFunctions.Add(tableValuedFunction.ObjectName, tableValuedFunction);
                return true;
            }

            return false;
        }

        public static bool AddTableValuedFunction(Schema schema, string objectName)
        {
            if (string.IsNullOrEmpty(objectName))
                return false;

            if (schema.TableValuedFunctions.ContainsKey(objectName))
                return false;

            var tableValuedFunction = new TableValuedFunction(schema, objectName);
            schema.TableValuedFunctions.Add(objectName, tableValuedFunction);

            return true;
        }

        public static bool AddTrigger(Schema schema, Trigger trigger)
        {
            if (schema.Triggers.ContainsKey(trigger.ObjectName))
                return false;

            if (trigger.Schema == null)
            {
                trigger.Schema = schema;
                schema.Triggers.Add(trigger.ObjectName, trigger);
                return true;
            }

            if (trigger.Schema.Equals(schema))
            {
                schema.Triggers.Add(trigger.ObjectName, trigger);
                return true;
            }

            return false;
        }

        public static bool AddTrigger(Schema schema, string objectName)
        {
            if (string.IsNullOrEmpty(objectName))
                return false;

            if (schema.Triggers.ContainsKey(objectName))
                return false;

            var trigger = new Trigger(schema, objectName);
            schema.Triggers.Add(objectName, trigger);
            
            return true;
        }

        public static bool AddUserDefinedDataType(Schema schema, UserDefinedDataType userDefinedDataType)
        {
            if (schema.UserDefinedDataTypes.ContainsKey(userDefinedDataType.ObjectName))
                return false;

            if (userDefinedDataType.Schema == null)
            {
                userDefinedDataType.Schema = schema;
                schema.UserDefinedDataTypes.Add(userDefinedDataType.ObjectName, userDefinedDataType);
                return true;
            }

            if (userDefinedDataType.Schema.Equals(schema))
            {
                schema.UserDefinedDataTypes.Add(userDefinedDataType.ObjectName, userDefinedDataType);
                return true;
            }

            return false;
        }

        public static bool AddUserDefinedDataType(Schema schema, string objectName)
        {
            if (string.IsNullOrEmpty(objectName))
                return false;

            if (schema.UserDefinedDataTypes.ContainsKey(objectName))
                return false;

            var userDefinedDataType = new UserDefinedDataType(schema, objectName);
            schema.UserDefinedDataTypes.Add(objectName, userDefinedDataType);
            
            return true;
        }

        public static bool AddUserTable(Schema schema, UserTable userTable)
        {
            if (schema.UserTables.ContainsKey(userTable.ObjectName))
                return false;

            if (userTable.Schema == null)
            {
                userTable.Schema = schema;
                schema.UserTables.Add(userTable.ObjectName, userTable);
                return true;
            }

            if (userTable.Schema.Equals(schema))
            {
                schema.UserTables.Add(userTable.ObjectName, userTable);
                return true;
            }

            return false;
        }

        public static bool AddUserTable(Schema schema, string objectName)
        {
            if (string.IsNullOrEmpty(objectName))
                return false;

            if (schema.UserTables.ContainsKey(objectName))
                return false;

            var userTable = new UserTable(schema, objectName);
            schema.UserTables.Add(objectName, userTable);
            
            return true;
        }

        public static bool AddView(Schema schema, View view)
        {
            if (schema.Views.ContainsKey(view.ObjectName))
                return false;

            if (view.Schema == null)
            {
                view.Schema = schema;
                schema.Views.Add(view.ObjectName, view);
                return true;
            }

            if (view.Schema.Equals(schema))
            {
                schema.Views.Add(view.ObjectName, view);
                return true;
            }

            return false;
        }

        public static bool AddView(Schema schema, string objectName)
        {
            if (string.IsNullOrEmpty(objectName))
                return false;

            if (schema.Views.ContainsKey(objectName))
                return false;

            var view = new View(schema, objectName);
            schema.Views.Add(objectName, view);
            
            return true;
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
            foreach (var userTable in schema.UserTables.Values)
                UserTable.Clear(userTable);
            schema.UserTables.Clear();
            schema.UserDefinedDataTypes.Clear();
            schema.Views.Clear();
        }

        /// <summary>
        /// Deep Clone...
        /// A clone of this class and clones of all assosiated metadata.
        /// </summary>
        /// <param name="schema">The schema to deep clone.</param>
        /// <returns>A clone of this class and clones of all assosiated metadata.</returns>
        public static Schema Clone(Schema schema)
        {
            var schemaClone = new Schema(schema.ObjectName);

            foreach (var aggregateFunction in schema.AggregateFunctions.Values)
                AddAggregateFunction(schemaClone, AggregateFunction.Clone(aggregateFunction));

            foreach (var inlineTableValuedFunction in schema.InlineTableValuedFunctions.Values)
                AddInlineTableValuedFunction(schemaClone, InlineTableValuedFunction.Clone(inlineTableValuedFunction));

            foreach (var scalarFunction in schema.ScalarFunctions.Values)
                AddScalarFunction(schemaClone, ScalarFunction.Clone(scalarFunction));

            foreach (var storedProcedure in schema.StoredProcedures.Values)
                AddStoredProcedure(schemaClone, StoredProcedure.Clone(storedProcedure));

            foreach (var tableValuedFunction in schema.TableValuedFunctions.Values)
                AddTableValuedFunction(schemaClone, TableValuedFunction.Clone(tableValuedFunction));

            foreach (var trigger in schema.Triggers.Values)
                AddTrigger(schemaClone, Trigger.Clone(trigger));

            foreach (var userDefinedDataType in schema.UserDefinedDataTypes.Values)
                AddUserDefinedDataType(schemaClone, UserDefinedDataType.Clone(userDefinedDataType));

            foreach (var userTable in schema.UserTables.Values)
                AddUserTable(schemaClone, UserTable.Clone(userTable));

            foreach (var view in schema.Views.Values)
                AddView(schemaClone, View.Clone(view));

            return schemaClone;
        }

        public static bool CompareDefinitions(Schema sourceSchema, Schema targetSchema)
        {
            return CompareObjectNames(sourceSchema, targetSchema);
        }

        public static bool CompareMatchedSchema(DataContext sourceDataContext, DataContext targetDataContext, Schema matchedSchema,
            Schema sourceSchema, Schema targetSchema, Schema alteredSchema)
        {
            var globalCompareState = true;

            foreach (var matchedAggregateFunction in matchedSchema.AggregateFunctions.Values)
            {
                AggregateFunction sourceAggregateFunction;
                AggregateFunction targetAggregateFunction;

                sourceSchema.AggregateFunctions.TryGetValue(matchedAggregateFunction.ObjectName, out sourceAggregateFunction);
                targetSchema.AggregateFunctions.TryGetValue(matchedAggregateFunction.ObjectName, out targetAggregateFunction);

                if (sourceAggregateFunction == null || targetAggregateFunction == null)
                    throw new Exception(string.Format("Source and/or target aggregate functions did not exist for the matching aggregate function {0} during Schema.CompareMatchedSchema() method.", matchedAggregateFunction.Namespace));

                if (AggregateFunction.CompareDefinitions(sourceAggregateFunction, targetAggregateFunction))
                    continue;

                globalCompareState = false;

                if (!AddAggregateFunction(alteredSchema, AggregateFunction.Clone(sourceAggregateFunction)))
                    throw new Exception(string.Format("Unable to add alteredAggregateFunction {0} to alteredSchema in Schema.CompareMatchedSchema() as it may already exist and should not.", sourceAggregateFunction.Namespace));
            }

            foreach (var matchedInlineTableValuedFunction in matchedSchema.InlineTableValuedFunctions.Values)
            {
                InlineTableValuedFunction sourceInlineTableValuedFunction;
                InlineTableValuedFunction targetInlineTableValuedFunction;

                sourceSchema.InlineTableValuedFunctions.TryGetValue(matchedInlineTableValuedFunction.ObjectName, out sourceInlineTableValuedFunction);
                targetSchema.InlineTableValuedFunctions.TryGetValue(matchedInlineTableValuedFunction.ObjectName, out targetInlineTableValuedFunction);

                if (sourceInlineTableValuedFunction == null || targetInlineTableValuedFunction == null)
                    throw new Exception(string.Format("Source and/or target inline table-valued function did not exist for the matching inline table-valued function {0} during Schema.CompareMatchedSchema() method.", matchedInlineTableValuedFunction.Namespace));

                if (InlineTableValuedFunction.CompareDefinitions(sourceInlineTableValuedFunction, targetInlineTableValuedFunction))
                    continue;

                globalCompareState = false;

                if (!AddInlineTableValuedFunction(alteredSchema, InlineTableValuedFunction.Clone(sourceInlineTableValuedFunction)))
                    throw new Exception(string.Format("Unable to add alteredInlineTableValuedFunction {0} to alteredSchema in Schema.CompareMatchedSchema() as it may already exist and should not.", sourceInlineTableValuedFunction.Namespace));
            }

            foreach (var matchedScalarFunction in matchedSchema.ScalarFunctions.Values)
            {
                ScalarFunction sourceScalarFunction;
                ScalarFunction targetScalarFunction;

                sourceSchema.ScalarFunctions.TryGetValue(matchedScalarFunction.ObjectName, out sourceScalarFunction);
                targetSchema.ScalarFunctions.TryGetValue(matchedScalarFunction.ObjectName, out targetScalarFunction);

                if (sourceScalarFunction == null || targetScalarFunction == null)
                    throw new Exception(string.Format("Source and/or target scalar function did not exist for the matching scalar function {0} during Schema.CompareMatchedSchema() method.", matchedScalarFunction.Namespace));

                if (ScalarFunction.CompareDefinitions(sourceScalarFunction, targetScalarFunction))
                    continue;

                globalCompareState = false;

                if (!AddScalarFunction(alteredSchema, ScalarFunction.Clone(sourceScalarFunction)))
                    throw new Exception(string.Format("Unable to add alteredScalarFunction {0} to alteredSchema in Schema.CompareMatchedSchema() as it may already exist and should not.", sourceScalarFunction.Namespace));
            }

            foreach (var matchedStoredProcedure in matchedSchema.StoredProcedures.Values)
            {
                StoredProcedure sourceStoredProcedure;
                StoredProcedure targetStoredProcedure;

                sourceSchema.StoredProcedures.TryGetValue(matchedStoredProcedure.ObjectName, out sourceStoredProcedure);
                targetSchema.StoredProcedures.TryGetValue(matchedStoredProcedure.ObjectName, out targetStoredProcedure);

                if (sourceStoredProcedure == null || targetStoredProcedure == null)
                    throw new Exception(string.Format("Source and/or target stored procedure did not exist for the matching stored procedure {0} during Schema.CompareMatchedSchema() method.", matchedStoredProcedure.Namespace));

                if (StoredProcedure.CompareDefinitions(sourceStoredProcedure, targetStoredProcedure))
                    continue;

                globalCompareState = false;

                if (!AddStoredProcedure(alteredSchema, StoredProcedure.Clone(sourceStoredProcedure)))
                    throw new Exception(string.Format("Unable to add alteredStoredProcedure {0} to alteredSchema in Schema.CompareMatchedSchema() as it may already exist and should not.", sourceStoredProcedure.Namespace));
            }

            foreach (var matchedTableValuedFunction in matchedSchema.TableValuedFunctions.Values)
            {
                TableValuedFunction sourceTableValuedFunction;
                TableValuedFunction targetTableValuedFunction;

                sourceSchema.TableValuedFunctions.TryGetValue(matchedTableValuedFunction.ObjectName, out sourceTableValuedFunction);
                targetSchema.TableValuedFunctions.TryGetValue(matchedTableValuedFunction.ObjectName, out targetTableValuedFunction);

                if (sourceTableValuedFunction == null || targetTableValuedFunction == null)
                    throw new Exception(string.Format("Source and/or target table-valued function did not exist for the matching table-valued function {0} during Schema.CompareMatchedSchema() method.", matchedTableValuedFunction.Namespace));

                if (TableValuedFunction.CompareDefinitions(sourceTableValuedFunction, targetTableValuedFunction))
                    continue;

                globalCompareState = false;

                if (!AddTableValuedFunction(alteredSchema, TableValuedFunction.Clone(sourceTableValuedFunction)))
                    throw new Exception(string.Format("Unable to add alteredTableValuedFunction {0} to alteredSchema in Schema.CompareMatchedSchema() as it may already exist and should not.", sourceTableValuedFunction.Namespace));
            }

            foreach (var matchedTrigger in matchedSchema.Triggers.Values)
            {
                Trigger sourceTrigger;
                Trigger targetTrigger;

                sourceSchema.Triggers.TryGetValue(matchedTrigger.ObjectName, out sourceTrigger);
                targetSchema.Triggers.TryGetValue(matchedTrigger.ObjectName, out targetTrigger);
                    
                if (sourceTrigger == null || targetTrigger == null)
                    throw new Exception(string.Format("Source and/or target trigger did not exist for the matching trigger {0} during Schema.CompareMatchedSchema() method.", matchedTrigger.Namespace));

                if (Trigger.CompareDefinitions(sourceTrigger, targetTrigger))
                    continue;

                globalCompareState = false;

                if (!AddTrigger(alteredSchema, Trigger.Clone(sourceTrigger)))
                    throw new Exception(string.Format("Unable to add alteredTrigger {0} to alteredSchema in Schema.CompareMatchedSchema() as it may already exist and should not.", sourceTrigger.Namespace));
            }

            foreach (var matchedUserDefinedDataType in matchedSchema.UserDefinedDataTypes.Values)
            {
                UserDefinedDataType sourceUserDefinedDataType;
                UserDefinedDataType targetUserDefinedDataType;

                sourceSchema.UserDefinedDataTypes.TryGetValue(matchedUserDefinedDataType.ObjectName, out sourceUserDefinedDataType);
                targetSchema.UserDefinedDataTypes.TryGetValue(matchedUserDefinedDataType.ObjectName, out targetUserDefinedDataType);
                
                if (sourceUserDefinedDataType == null || targetUserDefinedDataType == null)
                    throw new Exception(string.Format("Source and/or target user-defined data type did not exist for the matching user-defined data type {0} during Schema.CompareMatchedSchema() method.", matchedUserDefinedDataType.Namespace));

                if (UserDefinedDataType.CompareDefinitions(sourceUserDefinedDataType, targetUserDefinedDataType))
                    continue;

                globalCompareState = false;

                if (!AddUserDefinedDataType(alteredSchema, UserDefinedDataType.Clone(sourceUserDefinedDataType)))
                    throw new Exception(string.Format("Unable to add alteredUserDefinedDataType {0} to alteredSchema in Schema.CompareMatchedSchema() as it may already exist and should not.", sourceUserDefinedDataType.Namespace));
            }

            foreach (var matchedUserTable in matchedSchema.UserTables.Values)
            {
                UserTable sourceUserTable;
                UserTable targetUserTable;

                sourceSchema.UserTables.TryGetValue(matchedUserTable.ObjectName, out sourceUserTable);
                targetSchema.UserTables.TryGetValue(matchedUserTable.ObjectName, out targetUserTable);

                // Logic
                if (sourceUserTable == null || targetUserTable == null)
                    throw new Exception(string.Format("Source and/or target user-tables did not exist for the matching user-table {0} during Schema.CompareMatchedSchema() method.", matchedUserTable.Namespace));

                // Grab a clone of the source user-table and union it with the target user-table and
                // then except it with the matched user-table's definitions to see a copy of everything
                // that is different between the source and target user-tables.
                var alteredUserTable = UserTable.Clone(sourceUserTable);
                UserTable.UnionWith(alteredUserTable, targetUserTable, sourceDataContext, targetDataContext, DataComparisonType.Definitions);
                UserTable.ExceptWith(sourceDataContext, targetDataContext, alteredUserTable, matchedUserTable, DataComparisonType.Definitions);

                // If the except with operation was an exact match between the source and target
                // user-tables continue and this method will return true if all user-tables
                // are an exact match.
                if (UserTable.ObjectCount(alteredUserTable) == 0)
                    continue;

                // A change should be made to a user-table
                globalCompareState = false;

                // Otherwise we need to make two clones of the altered user-table and except each
                // with either the source user-table to find addable elements or the target
                // user-table to find droppable elements.
                var addableUserTable = UserTable.Clone(alteredUserTable);
                UserTable.ExceptWith(sourceDataContext, targetDataContext, addableUserTable, targetUserTable, DataComparisonType.Namespaces);

                var droppableUserTable = UserTable.Clone(alteredUserTable);
                UserTable.ExceptWith(sourceDataContext, targetDataContext, droppableUserTable, sourceUserTable, DataComparisonType.Namespaces);

                // Figure out which ones still remain to be altered by excepting a clone of the
                // altered user-table with the addable and droppable elements.
                var alterableUserTable = UserTable.Clone(alteredUserTable);
                UserTable.ExceptWith(sourceDataContext, targetDataContext, alterableUserTable, addableUserTable, DataComparisonType.Namespaces);
                UserTable.ExceptWith(sourceDataContext, targetDataContext, alterableUserTable, droppableUserTable, DataComparisonType.Namespaces);

                alteredUserTable.ObjectName += "<=>IsEqual";
                addableUserTable.ObjectName += "<+>ToAdd";
                droppableUserTable.ObjectName += "<x>ToDrop";
                alterableUserTable.ObjectName += "<~>ToAlter";

                // Now add each table to the alteredSchema regardless of whether or not they
                // have any elements as something needs to exist for each later, regardless...
                if (!AddUserTable(alteredSchema, alteredUserTable))
                    throw new Exception(string.Format("Unable to add alteredUserTable {0} to alteredSchema in Schema.CompareMatchedSchema() as it may already exist and should not.", alteredUserTable.Namespace));

                if (!AddUserTable(alteredSchema, addableUserTable))
                    throw new Exception(string.Format("Unable to add addableUserTable {0} to alteredSchema in Schema.CompareMatchedSchema() as it may already exist and should not.", addableUserTable.Namespace));

                if (!AddUserTable(alteredSchema, droppableUserTable))
                    throw new Exception(string.Format("Unable to add droppableUserTable {0} to alteredSchema in Schema.CompareMatchedSchema() as it may already exist and should not.", droppableUserTable.Namespace));

                if (!AddUserTable(alteredSchema, alterableUserTable))
                    throw new Exception(string.Format("Unable to add alterableUserTable {0} to alteredSchema in Schema.CompareMatchedSchema() as it may already exist and should not.", alterableUserTable.Namespace));
            }

            foreach (var matchedView in matchedSchema.Views.Values)
            {
                View sourceView;
                if (!sourceSchema.Views.TryGetValue(matchedView.ObjectName, out sourceView))
                    sourceView = null;

                View targetView;
                if (!targetSchema.Views.TryGetValue(matchedView.ObjectName, out targetView))
                    targetView = null;

                if (sourceView == null || targetView == null)
                    throw new Exception(string.Format("Source and/or target view did not exist for the matching view {0} during Schema.CompareMatchedSchema() method.", matchedView.Namespace));

                if (View.CompareDefinitions(sourceView, targetView))
                    continue;

                globalCompareState = false;

                if (!AddView(alteredSchema, View.Clone(sourceView)))
                    throw new Exception(string.Format("Unable to add alteredView {0} to alteredSchema in Schema.CompareMatchedSchema() as it may already exist and should not.", sourceView.Namespace));
            }

            return globalCompareState;
        }

        public static bool CompareObjectNames(Schema sourceSchema, Schema targetSchema,
            StringComparison stringComparison = StringComparison.OrdinalIgnoreCase)
        {
            switch (stringComparison)
            {
                case StringComparison.CurrentCulture:
                    return StringComparer.CurrentCulture.Compare(
                        sourceSchema.ObjectName, targetSchema.ObjectName) == 0;
                case StringComparison.CurrentCultureIgnoreCase:
                    return StringComparer.CurrentCultureIgnoreCase.Compare(
                        sourceSchema.ObjectName, targetSchema.ObjectName) == 0;
                case StringComparison.InvariantCulture:
                    return StringComparer.InvariantCulture.Compare(
                        sourceSchema.ObjectName, targetSchema.ObjectName) == 0;
                case StringComparison.InvariantCultureIgnoreCase:
                    return StringComparer.InvariantCultureIgnoreCase.Compare(
                        sourceSchema.ObjectName, targetSchema.ObjectName) == 0;
                case StringComparison.Ordinal:
                    return StringComparer.Ordinal.Compare(
                        sourceSchema.ObjectName, targetSchema.ObjectName) == 0;
                case StringComparison.OrdinalIgnoreCase:
                    return StringComparer.OrdinalIgnoreCase.Compare(
                        sourceSchema.ObjectName, targetSchema.ObjectName) == 0;
                default:
                    return StringComparer.OrdinalIgnoreCase.Compare(
                        sourceSchema.ObjectName, targetSchema.ObjectName) == 0;
            }
        }

        /// <summary>
        /// Removes all objects in the target Schema from the source Schema.
        /// </summary>
        /// <param name="sourceDataContext">The source DataContext.</param>
        /// <param name="targetDataContext">The target DataContext.</param>
        /// <param name="sourceSchema">The source Schema.</param>
        /// <param name="targetSchema">The target schema.</param>
        /// <param name="dataComparisonType">
        /// The completeness of comparisons between matching objects.
        /// </param>
        public static void ExceptWith(
            DataContext sourceDataContext, DataContext targetDataContext,
            Schema sourceSchema, Schema targetSchema,
            DataComparisonType dataComparisonType = DataComparisonType.SchemaLevelNamespaces)
        {
            var matchingAggregateFunctions = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            matchingAggregateFunctions.UnionWith(sourceSchema.AggregateFunctions.Keys);
            matchingAggregateFunctions.IntersectWith(targetSchema.AggregateFunctions.Keys);

            foreach (var aggregateFunction in matchingAggregateFunctions)
            {
                AggregateFunction sourceAggregateFunction;
                if (!sourceSchema.AggregateFunctions.TryGetValue(aggregateFunction, out sourceAggregateFunction))
                    continue;

                AggregateFunction targetAggregateFunction;
                if (!sourceSchema.AggregateFunctions.TryGetValue(aggregateFunction, out targetAggregateFunction))
                    continue;

                switch (dataComparisonType)
                {
                    case DataComparisonType.Definitions:
                        if (AggregateFunction.CompareDefinitions(sourceAggregateFunction, targetAggregateFunction))
                            RemoveAggregateFunction(sourceSchema, aggregateFunction);
                        break;
                    case DataComparisonType.Namespaces:
                    case DataComparisonType.SchemaLevelNamespaces:
                        if (AggregateFunction.CompareObjectNames(sourceAggregateFunction, targetAggregateFunction))
                            RemoveAggregateFunction(sourceSchema, aggregateFunction);
                        break;
                }
            }

            var matchingInlineTableValuedFunctions = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            matchingInlineTableValuedFunctions.UnionWith(sourceSchema.InlineTableValuedFunctions.Keys);
            matchingInlineTableValuedFunctions.IntersectWith(targetSchema.InlineTableValuedFunctions.Keys);

            foreach (var inlineTableValuedFunction in matchingInlineTableValuedFunctions)
            {
                InlineTableValuedFunction sourceInlineTableValuedFunction;
                if (!sourceSchema.InlineTableValuedFunctions.TryGetValue(inlineTableValuedFunction, out sourceInlineTableValuedFunction))
                    continue;

                InlineTableValuedFunction targetInlineTableValuedFunction;
                if (!targetSchema.InlineTableValuedFunctions.TryGetValue(inlineTableValuedFunction, out targetInlineTableValuedFunction))
                    continue;

                switch (dataComparisonType)
                {
                    case DataComparisonType.Definitions:
                        if (InlineTableValuedFunction.CompareDefinitions(sourceInlineTableValuedFunction, targetInlineTableValuedFunction))
                            RemoveInlineTableValuedFunction(sourceSchema, inlineTableValuedFunction);
                        break;
                    case DataComparisonType.Namespaces:
                    case DataComparisonType.SchemaLevelNamespaces:
                        if (InlineTableValuedFunction.CompareObjectNames(sourceInlineTableValuedFunction, targetInlineTableValuedFunction))
                            RemoveInlineTableValuedFunction(sourceSchema, inlineTableValuedFunction);
                        break;
                }
            }

            var matchingScalarFunctions = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            matchingScalarFunctions.UnionWith(sourceSchema.ScalarFunctions.Keys);
            matchingScalarFunctions.IntersectWith(targetSchema.ScalarFunctions.Keys);
            
            foreach (var scalarFunction in matchingScalarFunctions)
            {
                ScalarFunction sourceScalarFunction;
                if (!sourceSchema.ScalarFunctions.TryGetValue(scalarFunction, out sourceScalarFunction))
                    continue;

                ScalarFunction targetScalarFunction;
                if (!targetSchema.ScalarFunctions.TryGetValue(scalarFunction, out targetScalarFunction))
                    continue;

                switch (dataComparisonType)
                {
                    case DataComparisonType.Definitions:
                        if (ScalarFunction.CompareDefinitions(sourceScalarFunction, targetScalarFunction))
                            RemoveScalarFunction(sourceSchema, scalarFunction);
                        break;
                    case DataComparisonType.Namespaces:
                    case DataComparisonType.SchemaLevelNamespaces:
                        if (ScalarFunction.CompareObjectNames(sourceScalarFunction, targetScalarFunction))
                            RemoveScalarFunction(sourceSchema, scalarFunction);
                        break;
                }
            }

            var matchingStoredProcedures = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            matchingStoredProcedures.UnionWith(sourceSchema.StoredProcedures.Keys);
            matchingStoredProcedures.IntersectWith(targetSchema.StoredProcedures.Keys);
            
            foreach (var storedProcedure in matchingStoredProcedures)
            {
                StoredProcedure sourceStoredProcedure;
                if (!sourceSchema.StoredProcedures.TryGetValue(storedProcedure, out sourceStoredProcedure))
                    continue;

                StoredProcedure targetStoredProcedure;
                if (!targetSchema.StoredProcedures.TryGetValue(storedProcedure, out targetStoredProcedure))
                    continue;

                switch (dataComparisonType)
                {
                    case DataComparisonType.Definitions:
                        if (StoredProcedure.CompareDefinitions(sourceStoredProcedure, targetStoredProcedure))
                            RemoveStoredProcedure(sourceSchema, storedProcedure);
                        break;
                    case DataComparisonType.Namespaces:
                    case DataComparisonType.SchemaLevelNamespaces:
                        if (StoredProcedure.CompareObjectNames(sourceStoredProcedure, targetStoredProcedure))
                            RemoveStoredProcedure(sourceSchema, storedProcedure);
                        break;
                }
            }

            var matchingTableValuedFunctions = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            matchingTableValuedFunctions.UnionWith(sourceSchema.TableValuedFunctions.Keys);
            matchingTableValuedFunctions.IntersectWith(targetSchema.TableValuedFunctions.Keys);

            foreach (var tableValuedFunction in matchingTableValuedFunctions)
            {
                TableValuedFunction sourceTableValuedFunction;
                if (!sourceSchema.TableValuedFunctions.TryGetValue(tableValuedFunction, out sourceTableValuedFunction))
                    continue;

                TableValuedFunction targetTableValuedFunction;
                if (!targetSchema.TableValuedFunctions.TryGetValue(tableValuedFunction, out targetTableValuedFunction))
                    continue;

                switch (dataComparisonType)
                {
                    case DataComparisonType.Definitions:
                        if (TableValuedFunction.CompareDefinitions(sourceTableValuedFunction, targetTableValuedFunction))
                            RemoveTableValuedFunction(sourceSchema, tableValuedFunction);
                        break;
                    case DataComparisonType.Namespaces:
                    case DataComparisonType.SchemaLevelNamespaces:
                        if (TableValuedFunction.CompareObjectNames(sourceTableValuedFunction, targetTableValuedFunction))
                            RemoveTableValuedFunction(sourceSchema, tableValuedFunction);
                        break;
                }
            }

            var matchingTriggers = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            matchingTriggers.UnionWith(sourceSchema.Triggers.Keys);
            matchingTriggers.IntersectWith(targetSchema.Triggers.Keys);
            
            foreach (var trigger in matchingTriggers)
            {
                Trigger sourceTrigger;
                if (!sourceSchema.Triggers.TryGetValue(trigger, out sourceTrigger))
                    continue;

                Trigger targetTrigger;
                if (!targetSchema.Triggers.TryGetValue(trigger, out targetTrigger))
                    continue;

                switch (dataComparisonType)
                {
                    case DataComparisonType.Definitions:
                        if (Trigger.CompareDefinitions(sourceTrigger, targetTrigger))
                            RemoveTrigger(sourceSchema, trigger);
                        break;
                    case DataComparisonType.Namespaces:
                    case DataComparisonType.SchemaLevelNamespaces:
                        if (Trigger.CompareObjectNames(sourceTrigger, targetTrigger))
                            RemoveTrigger(sourceSchema, trigger);
                        break;
                }
            }

            var matchingUserDefinedDataTypes = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            matchingUserDefinedDataTypes.UnionWith(sourceSchema.UserDefinedDataTypes.Keys);
            matchingUserDefinedDataTypes.IntersectWith(targetSchema.UserDefinedDataTypes.Keys);
            
            foreach (var userDefinedDataType in matchingUserDefinedDataTypes)
            {
                UserDefinedDataType sourceUserDefinedDataType;
                if (!sourceSchema.UserDefinedDataTypes.TryGetValue(userDefinedDataType, out sourceUserDefinedDataType))
                    continue;

                UserDefinedDataType targetUserDefinedDataType;
                if (!targetSchema.UserDefinedDataTypes.TryGetValue(userDefinedDataType, out targetUserDefinedDataType))
                    continue;

                switch (dataComparisonType)
                {
                    case DataComparisonType.Definitions:
                        if (UserDefinedDataType.CompareDefinitions(sourceUserDefinedDataType, targetUserDefinedDataType))
                            RemoveUserDefinedDataType(sourceSchema, userDefinedDataType);
                        break;
                    case DataComparisonType.Namespaces:
                    case DataComparisonType.SchemaLevelNamespaces:
                        if (UserDefinedDataType.CompareObjectNames(sourceUserDefinedDataType, targetUserDefinedDataType))
                            RemoveUserDefinedDataType(sourceSchema, userDefinedDataType);
                        break;
                }
            }

            var matchingUserTables = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            matchingUserTables.UnionWith(sourceSchema.UserTables.Keys);
            matchingUserTables.IntersectWith(targetSchema.UserTables.Keys);
            
            foreach (var userTable in matchingUserTables)
            {
                UserTable sourceUserTable;
                if (!sourceSchema.UserTables.TryGetValue(userTable, out sourceUserTable))
                    continue;

                UserTable targetUserTable;
                if (!targetSchema.UserTables.TryGetValue(userTable, out targetUserTable))
                    continue;

                switch (dataComparisonType)
                {
                    case DataComparisonType.Definitions:
                        if (UserTable.CompareDefinitions(sourceUserTable, targetUserTable))
                        {
                            UserTable.ExceptWith(sourceDataContext, targetDataContext, sourceUserTable, targetUserTable, dataComparisonType);
                            if (UserTable.ObjectCount(sourceUserTable) == 0)
                                RemoveUserTable(sourceSchema, userTable);
                        }
                        break;
                    case DataComparisonType.Namespaces:
                        if (UserTable.CompareObjectNames(sourceUserTable, targetUserTable))
                        {
                            UserTable.ExceptWith(sourceDataContext, targetDataContext, sourceUserTable, targetUserTable, dataComparisonType);
                            if (UserTable.ObjectCount(sourceUserTable) == 0)
                                RemoveUserTable(sourceSchema, userTable);
                        }
                        break;
                    case DataComparisonType.SchemaLevelNamespaces:
                        if (UserTable.CompareObjectNames(sourceUserTable, targetUserTable))
                            RemoveUserTable(sourceSchema, userTable);
                        break;
                }
            }

            var matchingViews = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            matchingViews.UnionWith(sourceSchema.Views.Keys);
            matchingViews.IntersectWith(targetSchema.Views.Keys);
            
            foreach (var view in matchingViews)
            {
                View sourceView;
                if (!sourceSchema.Views.TryGetValue(view, out sourceView))
                    continue;

                View targetView;
                if (!targetSchema.Views.TryGetValue(view, out targetView))
                    continue;

                switch (dataComparisonType)
                {
                    case DataComparisonType.Definitions:
                        if (View.CompareDefinitions(sourceView, targetView))
                            RemoveView(sourceSchema, view);
                        break;
                    case DataComparisonType.Namespaces:
                    case DataComparisonType.SchemaLevelNamespaces:
                        if (View.CompareObjectNames(sourceView, targetView))
                            RemoveView(sourceSchema, view);
                        break;
                }
            }
        }

        public static void Fill(Schema schema, DataGenerics generics)
		{
            Clear(schema);

            var predicate = new StringPredicate(schema.Namespace + ".", StringComparison.OrdinalIgnoreCase);

            var modules = generics.Modules.FindAll(predicate.StartsWith);
            foreach (var str in modules)
            {
                ModulesRow modulesRow;
                if (!generics.ModuleRows.TryGetValue(str + ".", out modulesRow)) continue;

                switch (modulesRow.TypeDescription)
                {
                    case "AGGREGATE_FUNCTION":
                        var aggregateFunction = new AggregateFunction(schema, modulesRow);
                        AddAggregateFunction(schema, aggregateFunction);
                        break;
                    case "SQL_INLINE_TABLE_VALUED_FUNCTION":
                        var inlineTableValuedFunction = new InlineTableValuedFunction(schema, modulesRow);
                        AddInlineTableValuedFunction(schema, inlineTableValuedFunction);
                        break;
                    case "SQL_SCALAR_FUNCTION":
                    case "FUNCTION":
                        var scalarFunction = new ScalarFunction(schema, modulesRow);
                        AddScalarFunction(schema, scalarFunction);
                        break;
                    case "SQL_STORED_PROCEDURE":
                    case "PROCEDURE":
                        var storedProcedure = new StoredProcedure(schema, modulesRow);
                        AddStoredProcedure(schema, storedProcedure);
                        break;
                    case "SQL_TABLE_VALUED_FUNCTION":
                        var tableValuedFunction = new TableValuedFunction(schema, modulesRow);
                        AddTableValuedFunction(schema, tableValuedFunction);
                        break;
                    case "SQL_TRIGGER":
                    case "TRIGGER":
                        var trigger = new Trigger(schema, modulesRow);
                        AddTrigger(schema, trigger);
                        break;
                    case "VIEW":
                        var view = new View(schema, modulesRow);
                        AddView(schema, view);
                        break;
                    //case "CLR_SCALAR_FUNCTION":
                    //case "CLR_STORED_PROCEDURE":
                    //case "CLR_TABLE_VALUED_FUNCTION":
                    //case "CLR_TRIGGER":
                }
            }

            var userDefinedDataTypes = generics.UserDefinedDataTypes.FindAll(predicate.StartsWith);
            foreach (var str in userDefinedDataTypes)
            {
                UserDefinedDataTypesRow userDefinedDataTypesRow;
                if (!generics.UserDefinedDataTypeRows.TryGetValue(str + ".", out userDefinedDataTypesRow))
                    continue;

                var userDefinedDataType = new UserDefinedDataType(schema, userDefinedDataTypesRow);
                AddUserDefinedDataType(schema, userDefinedDataType);
            }

            var userTables = generics.UserTables.FindAll(predicate.StartsWith);
            foreach (var str in userTables)
            {
                UserTablesRow userTablesRow;
                if (!generics.UserTableRows.TryGetValue(str + ".", out userTablesRow))
                    continue;

                var userTable = new UserTable(schema, userTablesRow);
                UserTable.Fill(userTable, generics);
                AddUserTable(schema, userTable);
            }
		}

        public static Schema FromJson(string json)
        {
            return JsonConvert.DeserializeObject<Schema>(json);
        }

        public static void GenerateAlterScripts(DataContext sourceDataContext, DataContext targetDataContext, Schema alteredSchema,
            Schema sourceSchema, Schema targetSchema, Schema droppedSchema, Schema createdSchema, Schema matchedSchema,
            DataSyncActionsCollection dataSyncActions, DataDependencyBuilder dataDependencyBuilder, DataProperties dataProperties)
        {
            // DataDependecyBuilder would only be null if the call did not
            // originate from GenerateAlterCatalogs
            if (dataDependencyBuilder == null)
            {
                dataDependencyBuilder = new DataDependencyBuilder();

                if (droppedSchema != null)
                    dataDependencyBuilder.PreloadDroppedDependencies(droppedSchema);

                if (createdSchema != null)
                    dataDependencyBuilder.PreloadCreatedDependencies(createdSchema);
            }

            foreach (var alteredAggregateFunction in alteredSchema.AggregateFunctions.Values)
                AggregateFunction.GenerateAlterScripts(sourceDataContext, targetDataContext, alteredAggregateFunction, dataSyncActions, dataProperties);

            foreach (var alteredInlineTableValuedFunction in alteredSchema.InlineTableValuedFunctions.Values)
                InlineTableValuedFunction.GenerateAlterScripts(sourceDataContext, targetDataContext, alteredInlineTableValuedFunction, dataSyncActions, dataProperties);

            foreach (var alteredScalarFunction in alteredSchema.ScalarFunctions.Values)
                ScalarFunction.GenerateAlterScripts(sourceDataContext, targetDataContext, alteredScalarFunction, dataSyncActions, dataProperties);

            foreach (var alteredStoredProcedure in alteredSchema.StoredProcedures.Values)
                StoredProcedure.GenerateAlterScripts(sourceDataContext, targetDataContext, alteredStoredProcedure, dataSyncActions, dataProperties);

            foreach (var alteredTableValuedFunction in alteredSchema.TableValuedFunctions.Values)
                TableValuedFunction.GenerateAlterScripts(sourceDataContext, targetDataContext, alteredTableValuedFunction, dataSyncActions, dataProperties);

            foreach (var alteredTrigger in alteredSchema.Triggers.Values)
                Trigger.GenerateAlterScripts(sourceDataContext, targetDataContext, alteredTrigger, dataSyncActions, dataProperties);

            foreach (var alteredUserDefinedDataType in alteredSchema.UserDefinedDataTypes.Values)
            {
                UserDefinedDataType.GenerateDropScripts(sourceDataContext, targetDataContext, alteredUserDefinedDataType, dataSyncActions, dataProperties);
                UserDefinedDataType.GenerateCreateScripts(sourceDataContext, targetDataContext, alteredUserDefinedDataType, dataSyncActions, dataProperties);
            }

            foreach (var alteredUserTable in
                alteredSchema.UserTables.Values.Where(
                    alteredUserTable => alteredUserTable.ObjectName.EndsWith("<=>IsEqual", StringComparison.OrdinalIgnoreCase)))
            {
                var alteredUserTableObjectName = alteredUserTable.ObjectName.Substring(0, alteredUserTable.ObjectName. Length - 10);

                UserTable sourceUserTable;
                if (!sourceSchema.UserTables.TryGetValue(alteredUserTableObjectName, out sourceUserTable))
                    sourceUserTable = null;

                UserTable targetUserTable;
                if (!targetSchema.UserTables.TryGetValue(alteredUserTableObjectName, out targetUserTable))
                    targetUserTable = null;

                UserTable droppedUserTable = null;
                if (droppedSchema != null)
                    if (!droppedSchema.UserTables.TryGetValue(alteredUserTableObjectName, out droppedUserTable))
                        droppedUserTable = null;

                UserTable createdUserTable = null;
                if (createdSchema != null)
                    if (!createdSchema.UserTables.TryGetValue(alteredUserTableObjectName, out createdUserTable))
                        createdUserTable = null;

                UserTable matchedUserTable = null;
                if (matchedSchema != null)
                    if (!matchedSchema.UserTables.TryGetValue(alteredUserTableObjectName, out matchedUserTable))
                        matchedUserTable = null;

                // Logic
                if (sourceUserTable == null || targetUserTable == null)
                    throw new Exception(string.Format("Source and/or target user-tables did not exist for the altered user-table {0} during Schema.GenerateAlterScripts() method.", alteredUserTable.ObjectName));

                UserTable addableUserTable;
                if (!alteredSchema.UserTables.TryGetValue(alteredUserTableObjectName + "<+>ToAdd", out addableUserTable))
                    addableUserTable = null;

                UserTable droppableUserTable;
                if (!alteredSchema.UserTables.TryGetValue(alteredUserTableObjectName + "<x>ToDrop", out droppableUserTable))
                    droppableUserTable = null;

                UserTable alterableUserTable;
                if (!alteredSchema.UserTables.TryGetValue(alteredUserTableObjectName + "<~>ToAlter", out alterableUserTable))
                    alterableUserTable = null;

                if (addableUserTable == null || droppableUserTable == null || alterableUserTable == null)
                    throw new Exception(string.Format("One or more extension tables ending with <+>ToAdd, <x>ToDrop, and <~>ToAlter did not exist for the altered user-table {0} during Schema.GenerateAlterScripts() method. Please insure all exist in the altered schema before using this method.", alteredUserTable.Namespace));

                // Cannot be held within the enumeration, since renaming will modify the collection
                // and throw an exception.
                var addableUserTableClone = UserTable.Clone(addableUserTable);
                addableUserTableClone.ObjectName = alteredUserTableObjectName;
                addableUserTableClone.Schema = alteredSchema;
                LinkForeignKeys(addableUserTableClone.Schema);

                var droppableUserTableClone = UserTable.Clone(droppableUserTable);
                droppableUserTableClone.ObjectName = alteredUserTableObjectName;
                droppableUserTableClone.Schema = alteredSchema;
                LinkForeignKeys(droppableUserTableClone.Schema);

                var alterableUserTableClone = UserTable.Clone(alterableUserTable);
                alterableUserTableClone.ObjectName = alteredUserTableObjectName;
                alterableUserTableClone.Schema = alteredSchema;
                LinkForeignKeys(alterableUserTableClone.Schema);

                foreach (var identityColumn in addableUserTableClone.IdentityColumns.Values)
                    UserTable.AddIdentityColumn(alterableUserTableClone, IdentityColumn.Clone(identityColumn));

                foreach (var computedColumn in addableUserTableClone.ComputedColumns.Values)
                    UserTable.AddComputedColumn(alterableUserTableClone, ComputedColumn.Clone(computedColumn));

                UserTable.GenerateAlterScripts(sourceDataContext, targetDataContext, alteredUserTable, addableUserTableClone, droppableUserTableClone,
                    alterableUserTableClone, droppedUserTable, createdUserTable, sourceUserTable, targetUserTable, matchedUserTable, dataSyncActions,
                    dataDependencyBuilder, dataProperties);
            }

            foreach (var alteredView in alteredSchema.Views.Values)
                View.GenerateAlterScripts(sourceDataContext, targetDataContext, alteredView, dataSyncActions, dataProperties);
        }

        public static void GenerateCreateScripts(
            DataContext sourceDataContext, DataContext targetDataContext,
            Schema createdSchema, Schema sourceSchema, Schema targetSchema,
            DataSyncActionsCollection dataSyncActions, DataProperties dataProperties)
        {
            if (!DataProperties.NonRemovableSchemas.Contains(createdSchema.Namespace))
                if (sourceSchema != null && targetSchema == null)
                {
                    var dataSyncAction = DataActionFactory.CreateSchema(sourceDataContext, targetDataContext, createdSchema);
                    if (dataSyncAction != null)
                        dataSyncActions.Add(dataSyncAction);
                }

            if (ObjectCount(createdSchema) == 0)
                return;

            foreach (var aggregateFunction in createdSchema.AggregateFunctions.Values)
                AggregateFunction.GenerateCreateScripts(sourceDataContext, targetDataContext, aggregateFunction, dataSyncActions, dataProperties);
            foreach (var inlineTableValuedFunction in createdSchema.InlineTableValuedFunctions.Values)
                InlineTableValuedFunction.GenerateCreateScripts(sourceDataContext, targetDataContext, inlineTableValuedFunction, dataSyncActions, dataProperties);
            foreach (var scalarFunction in createdSchema.ScalarFunctions.Values)
                ScalarFunction.GenerateCreateScripts(sourceDataContext, targetDataContext, scalarFunction, dataSyncActions, dataProperties);
            foreach (var storedProcedure in createdSchema.StoredProcedures.Values)
                StoredProcedure.GenerateCreateScripts(sourceDataContext, targetDataContext, storedProcedure, dataSyncActions, dataProperties);
            foreach (var tableValuedFunction in createdSchema.TableValuedFunctions.Values)
                TableValuedFunction.GenerateCreateScripts(sourceDataContext, targetDataContext, tableValuedFunction, dataSyncActions, dataProperties);
            foreach (var trigger in createdSchema.Triggers.Values)
                Trigger.GenerateCreateScripts(sourceDataContext, targetDataContext, trigger, dataSyncActions, dataProperties);
            foreach (var userDefinedDataType in createdSchema.UserDefinedDataTypes.Values)
                UserDefinedDataType.GenerateCreateScripts(sourceDataContext, targetDataContext, userDefinedDataType, dataSyncActions, dataProperties);
            foreach (var userTable in createdSchema.UserTables.Values)
                UserTable.GenerateCreateScripts(sourceDataContext, targetDataContext, userTable, dataSyncActions, dataProperties);
            foreach (var view in createdSchema.Views.Values)
                View.GenerateCreateScripts(sourceDataContext, targetDataContext, view, dataSyncActions, dataProperties);
        }

        public static void GenerateDropScripts(DataContext sourceDataContext, DataContext targetDataContext, Schema droppedSchema,
            Schema sourceSchema, Schema targetSchema, DataSyncActionsCollection dataSyncActions, DataProperties dataProperties)
        {
            if (ObjectCount(droppedSchema) == 0 || !dataProperties.TightSync)
                return;

            if (!DataProperties.NonRemovableSchemas.Contains(droppedSchema.Namespace))
                if (sourceSchema == null && targetSchema != null && dataProperties.TightSync)
                {
                    var dataSyncAction = DataActionFactory.DropSchema(sourceDataContext, targetDataContext, droppedSchema);
                    if (dataSyncAction != null)
                        dataSyncActions.Add(dataSyncAction);
                }

            foreach (var aggregateFunction in droppedSchema.AggregateFunctions.Values)
                AggregateFunction.GenerateDropScripts(sourceDataContext, targetDataContext, aggregateFunction, dataSyncActions, dataProperties);
            foreach (var inlineTableValuedFunction in droppedSchema.InlineTableValuedFunctions.Values)
                InlineTableValuedFunction.GenerateDropScripts(sourceDataContext, targetDataContext, inlineTableValuedFunction, dataSyncActions, dataProperties);
            foreach (var scalarFunction in droppedSchema.ScalarFunctions.Values)
                ScalarFunction.GenerateDropScripts(sourceDataContext, targetDataContext, scalarFunction, dataSyncActions, dataProperties);
            foreach (var storedProcedure in droppedSchema.StoredProcedures.Values)
                StoredProcedure.GenerateDropScripts(sourceDataContext, targetDataContext, storedProcedure, dataSyncActions, dataProperties);
            foreach (var tableValuedFunction in droppedSchema.TableValuedFunctions.Values)
                TableValuedFunction.GenerateDropScripts(sourceDataContext, targetDataContext, tableValuedFunction, dataSyncActions, dataProperties);
            foreach (var trigger in droppedSchema.Triggers.Values)
                Trigger.GenerateDropScripts(sourceDataContext, targetDataContext, trigger, dataSyncActions, dataProperties);
            foreach (var userDefinedDataType in droppedSchema.UserDefinedDataTypes.Values)
                UserDefinedDataType.GenerateDropScripts(sourceDataContext, targetDataContext, userDefinedDataType, dataSyncActions, dataProperties);
            foreach (var userTable in droppedSchema.UserTables.Values)
                UserTable.GenerateDropScripts(sourceDataContext, targetDataContext, userTable, dataSyncActions, dataProperties);
            foreach (var view in droppedSchema.Views.Values)
                View.GenerateDropScripts(sourceDataContext, targetDataContext, view, dataSyncActions, dataProperties);
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            // Serialize Members
            info.AddValue("ObjectName", ObjectName);
            info.AddValue("Description", Description);

            // Serialize Aggregate Functions
            info.AddValue("AggregateFunctions", AggregateFunctions.Count);

            var i = 0;
            foreach (var aggregateFunction in AggregateFunctions.Values)
                info.AddValue("AggregateFunction" + i++, aggregateFunction);

            // Serialize Inline Table-Valued Functions
            info.AddValue("InlineTableValuedFunctions", InlineTableValuedFunctions.Count);

            i = 0;
            foreach (var inlineTableValuedFunction in InlineTableValuedFunctions.Values)
                info.AddValue("InlineTableValuedFunction" + i++, inlineTableValuedFunction);

            // Serialize Scalar Functions
            info.AddValue("ScalarFunctions", ScalarFunctions.Count);

            i = 0;
            foreach (var scalarFunction in ScalarFunctions.Values)
                info.AddValue("ScalarFunction" + i++, scalarFunction);

            // Serialize Stored Procedures
            info.AddValue("StoredProcedures", StoredProcedures.Count);

            i = 0;
            foreach (var storedProcedure in StoredProcedures.Values)
                info.AddValue("StoredProcedure" + i++, storedProcedure);

            // Serialize Table-Valued Functions
            info.AddValue("TableValuedFunctions", TableValuedFunctions.Count);

            i = 0;
            foreach (var tableValuedFunction in TableValuedFunctions.Values)
                info.AddValue("TableValuedFunction" + i++, tableValuedFunction);

            // Serialize Triggers
            info.AddValue("Triggers", Triggers.Count);

            i = 0;
            foreach (var trigger in Triggers.Values)
                info.AddValue("Trigger" + i++, trigger);

            // Serialize User-Defined Data Types
            info.AddValue("UserDefinedDataTypes", UserDefinedDataTypes.Count);

            i = 0;
            foreach (var userDefinedDataType in UserDefinedDataTypes.Values)
                info.AddValue("UserDefinedDataType" + i++, userDefinedDataType);

            // Serialize User-Tables
            info.AddValue("UserTables", UserTables.Count);

            i = 0;
            foreach (var userTable in UserTables.Values)
                info.AddValue("UserTable" + i++, userTable);

            // Serialize Views
            info.AddValue("Views", Views.Count);

            i = 0;
            foreach (var view in Views.Values)
                info.AddValue("View" + i++, view);
        }

        /// <summary>
        /// Modifies the source Schema to contain only objects that are
        /// present in the source Schema and in the target Schema.
        /// </summary>
        /// <param name="sourceDataContext">The source DataContext.</param>
        /// <param name="targetDataContext">The target DataContext.</param>
        /// <param name="sourceSchema">The source Schema.</param>
        /// <param name="targetSchema">The target schema.</param>
        /// <param name="dataComparisonType">
        /// The completeness of comparisons between matching objects.
        /// </param>
        public static void IntersectWith(DataContext sourceDataContext, DataContext targetDataContext, Schema sourceSchema,
            Schema targetSchema, DataComparisonType dataComparisonType = DataComparisonType.SchemaLevelNamespaces)
        {
            var matchingAggregateFunctions = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            var removableAggregateFunctions = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            matchingAggregateFunctions.UnionWith(sourceSchema.AggregateFunctions.Keys);
            matchingAggregateFunctions.IntersectWith(targetSchema.AggregateFunctions.Keys);

            removableAggregateFunctions.UnionWith(sourceSchema.AggregateFunctions.Keys);
            removableAggregateFunctions.ExceptWith(matchingAggregateFunctions);

            foreach (var aggregateFunction in removableAggregateFunctions)
                RemoveAggregateFunction(sourceSchema, aggregateFunction);

            foreach (var aggregateFunction in matchingAggregateFunctions)
            {
                AggregateFunction sourceAggregateFunction;
                if (!sourceSchema.AggregateFunctions.TryGetValue(aggregateFunction, out sourceAggregateFunction))
                    continue;

                AggregateFunction targetAggregateFunction;
                if (!targetSchema.AggregateFunctions.TryGetValue(aggregateFunction, out targetAggregateFunction))
                    continue;

                switch (dataComparisonType)
                {
                    case DataComparisonType.Definitions:
                        if (!AggregateFunction.CompareDefinitions(sourceAggregateFunction, targetAggregateFunction))
                            RemoveAggregateFunction(sourceSchema, aggregateFunction);
                        break;
                    case DataComparisonType.Namespaces:
                    case DataComparisonType.SchemaLevelNamespaces:
                        if (!AggregateFunction.CompareObjectNames(sourceAggregateFunction, targetAggregateFunction))
                            RemoveAggregateFunction(sourceSchema, aggregateFunction);
                        break;
                }
            }

            var matchingInlineTableValuedFunctions = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            var removableInlineTableValuedFunctions = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            matchingInlineTableValuedFunctions.UnionWith(sourceSchema.InlineTableValuedFunctions.Keys);
            matchingInlineTableValuedFunctions.IntersectWith(targetSchema.InlineTableValuedFunctions.Keys);

            removableInlineTableValuedFunctions.UnionWith(sourceSchema.InlineTableValuedFunctions.Keys);
            removableInlineTableValuedFunctions.ExceptWith(matchingInlineTableValuedFunctions);

            foreach (var inlineTableValuedFunction in removableInlineTableValuedFunctions)
                RemoveInlineTableValuedFunction(sourceSchema, inlineTableValuedFunction);

            foreach (var inlineTableValuedFunction in matchingInlineTableValuedFunctions)
            {
                InlineTableValuedFunction sourceInlineTableValuedFunction;
                if (!sourceSchema.InlineTableValuedFunctions.TryGetValue(inlineTableValuedFunction, out sourceInlineTableValuedFunction))
                    continue;

                InlineTableValuedFunction targetInlineTableValuedFunction;
                if (!targetSchema.InlineTableValuedFunctions.TryGetValue(inlineTableValuedFunction, out targetInlineTableValuedFunction))
                    continue;

                switch (dataComparisonType)
                {
                    case DataComparisonType.Definitions:
                        if (!InlineTableValuedFunction.CompareDefinitions(sourceInlineTableValuedFunction, targetInlineTableValuedFunction))
                            RemoveInlineTableValuedFunction(sourceSchema, inlineTableValuedFunction);
                        break;
                    case DataComparisonType.Namespaces:
                    case DataComparisonType.SchemaLevelNamespaces:
                        if (!InlineTableValuedFunction.CompareObjectNames(sourceInlineTableValuedFunction, targetInlineTableValuedFunction))
                            RemoveInlineTableValuedFunction(sourceSchema, inlineTableValuedFunction);
                        break;
                }
            }

            var matchingScalarFunctions = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            var removableScalarFunctions = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            matchingScalarFunctions.UnionWith(sourceSchema.ScalarFunctions.Keys);
            matchingScalarFunctions.IntersectWith(targetSchema.ScalarFunctions.Keys);

            removableScalarFunctions.UnionWith(sourceSchema.ScalarFunctions.Keys);
            removableScalarFunctions.ExceptWith(matchingScalarFunctions);

            foreach (var scalarFunction in removableScalarFunctions)
                RemoveScalarFunction(sourceSchema, scalarFunction);

            foreach (var scalarFunction in matchingScalarFunctions)
            {
                ScalarFunction sourceScalarFunction;
                if (!sourceSchema.ScalarFunctions.TryGetValue(scalarFunction, out sourceScalarFunction))
                    continue;

                ScalarFunction targetScalarFunction;
                if (!targetSchema.ScalarFunctions.TryGetValue(scalarFunction, out targetScalarFunction))
                    continue;

                switch (dataComparisonType)
                {
                    case DataComparisonType.Definitions:
                        if (!ScalarFunction.CompareDefinitions(sourceScalarFunction, targetScalarFunction))
                            RemoveScalarFunction(sourceSchema, scalarFunction);
                        break;
                    case DataComparisonType.Namespaces:
                    case DataComparisonType.SchemaLevelNamespaces:
                        if (!ScalarFunction.CompareObjectNames(sourceScalarFunction, targetScalarFunction))
                            RemoveScalarFunction(sourceSchema, scalarFunction);
                        break;
                }
            }

            var matchingStoredProcedures = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            var removableStoredProcedures = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            matchingStoredProcedures.UnionWith(sourceSchema.StoredProcedures.Keys);
            matchingStoredProcedures.IntersectWith(targetSchema.StoredProcedures.Keys);

            removableStoredProcedures.UnionWith(sourceSchema.StoredProcedures.Keys);
            removableStoredProcedures.ExceptWith(matchingStoredProcedures);

            foreach (var storedProcedure in removableStoredProcedures)
                RemoveStoredProcedure(sourceSchema, storedProcedure);

            foreach (var storedProcedure in matchingStoredProcedures)
            {
                StoredProcedure sourceStoredProcedure;
                if (!sourceSchema.StoredProcedures.TryGetValue(storedProcedure, out sourceStoredProcedure))
                    continue;

                StoredProcedure targetStoredProcedure;
                if (!targetSchema.StoredProcedures.TryGetValue(storedProcedure, out targetStoredProcedure))
                    continue;

                switch (dataComparisonType)
                {
                    case DataComparisonType.Definitions:
                        if (!StoredProcedure.CompareDefinitions(sourceStoredProcedure, targetStoredProcedure))
                            RemoveStoredProcedure(sourceSchema, storedProcedure);
                        break;
                    case DataComparisonType.Namespaces:
                    case DataComparisonType.SchemaLevelNamespaces:
                        if (!StoredProcedure.CompareObjectNames(sourceStoredProcedure, targetStoredProcedure))
                            RemoveStoredProcedure(sourceSchema, storedProcedure);
                        break;
                }
            }

            var matchingTableValuedFunctions = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            var removableTableValuedFunctions = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            matchingTableValuedFunctions.UnionWith(sourceSchema.TableValuedFunctions.Keys);
            matchingTableValuedFunctions.IntersectWith(targetSchema.TableValuedFunctions.Keys);

            removableTableValuedFunctions.UnionWith(sourceSchema.TableValuedFunctions.Keys);
            removableTableValuedFunctions.ExceptWith(matchingTableValuedFunctions);

            foreach (var tableValuedFunction in removableTableValuedFunctions)
                RemoveTableValuedFunction(sourceSchema, tableValuedFunction);

            foreach (var tableValuedFunction in matchingTableValuedFunctions)
            {
                TableValuedFunction sourceTableValuedFunction;
                if (!sourceSchema.TableValuedFunctions.TryGetValue(tableValuedFunction, out sourceTableValuedFunction))
                    continue;

                TableValuedFunction targetTableValuedFunction;
                if (!targetSchema.TableValuedFunctions.TryGetValue(tableValuedFunction, out targetTableValuedFunction))
                    continue;

                switch (dataComparisonType)
                {
                    case DataComparisonType.Definitions:
                        if (!TableValuedFunction.CompareDefinitions(sourceTableValuedFunction, targetTableValuedFunction))
                            RemoveTableValuedFunction(sourceSchema, tableValuedFunction);
                        break;
                    case DataComparisonType.Namespaces:
                    case DataComparisonType.SchemaLevelNamespaces:
                        if (!TableValuedFunction.CompareDefinitions(sourceTableValuedFunction, targetTableValuedFunction))
                            RemoveTableValuedFunction(sourceSchema, tableValuedFunction);
                        break;
                }
            }

            var matchingTriggers = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            var removableTriggers = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            matchingTriggers.UnionWith(sourceSchema.Triggers.Keys);
            matchingTriggers.IntersectWith(targetSchema.Triggers.Keys);

            removableTriggers.UnionWith(sourceSchema.Triggers.Keys);
            removableTriggers.ExceptWith(matchingTriggers);

            foreach (var trigger in removableTriggers)
                RemoveTrigger(sourceSchema, trigger);

            foreach (var trigger in matchingTriggers)
            {
                Trigger sourceTrigger;
                if (!sourceSchema.Triggers.TryGetValue(trigger, out sourceTrigger))
                    continue;

                Trigger targetTrigger;
                if (!targetSchema.Triggers.TryGetValue(trigger, out targetTrigger))
                    continue;

                switch (dataComparisonType)
                {
                    case DataComparisonType.Definitions:
                        if (!Trigger.CompareDefinitions(sourceTrigger, targetTrigger))
                            RemoveTrigger(sourceSchema, trigger);
                        break;
                    case DataComparisonType.Namespaces:
                    case DataComparisonType.SchemaLevelNamespaces:
                        if (!Trigger.CompareObjectNames(sourceTrigger, targetTrigger))
                            RemoveTrigger(sourceSchema, trigger);
                        break;
                }
            }

            var matchingUserDefinedDataTypes = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            var removableUserDefinedDataTypes = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            matchingUserDefinedDataTypes.UnionWith(sourceSchema.UserDefinedDataTypes.Keys);
            matchingUserDefinedDataTypes.IntersectWith(targetSchema.UserDefinedDataTypes.Keys);

            removableUserDefinedDataTypes.UnionWith(sourceSchema.UserDefinedDataTypes.Keys);
            removableUserDefinedDataTypes.ExceptWith(matchingUserDefinedDataTypes);

            foreach (var userDefinedDataType in removableUserDefinedDataTypes)
                RemoveUserDefinedDataType(sourceSchema, userDefinedDataType);

            foreach (var userDefinedDataType in matchingUserDefinedDataTypes)
            {
                UserDefinedDataType sourceUserDefinedDataType;
                if (!sourceSchema.UserDefinedDataTypes.TryGetValue(userDefinedDataType, out sourceUserDefinedDataType))
                    continue;

                UserDefinedDataType targetUserDefinedDataType;
                if (!targetSchema.UserDefinedDataTypes.TryGetValue(userDefinedDataType, out targetUserDefinedDataType))
                    continue;

                switch (dataComparisonType)
                {
                    case DataComparisonType.Definitions:
                        if (!UserDefinedDataType.CompareDefinitions(sourceUserDefinedDataType, targetUserDefinedDataType))
                            RemoveUserDefinedDataType(sourceSchema, userDefinedDataType);
                        break;
                    case DataComparisonType.Namespaces:
                    case DataComparisonType.SchemaLevelNamespaces:
                        if (!UserDefinedDataType.CompareObjectNames(sourceUserDefinedDataType, targetUserDefinedDataType))
                            RemoveUserDefinedDataType(sourceSchema, userDefinedDataType);
                        break;
                }
            }

            var matchingUserTables = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            var removableUserTables = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            matchingUserTables.UnionWith(sourceSchema.UserTables.Keys);
            matchingUserTables.IntersectWith(targetSchema.UserTables.Keys);

            removableUserTables.UnionWith(sourceSchema.UserTables.Keys);
            removableUserTables.ExceptWith(matchingUserTables);

            foreach (var userTable in removableUserTables)
                RemoveUserTable(sourceSchema, userTable);
            
            foreach (var userTable in matchingUserTables)
            {
                UserTable sourceUserTable;
                if (!sourceSchema.UserTables.TryGetValue(userTable, out sourceUserTable))
                    continue;

                UserTable targetUserTable;
                if (!targetSchema.UserTables.TryGetValue(userTable, out targetUserTable))
                    continue;

                switch (dataComparisonType)
                {
                    case DataComparisonType.Definitions:
                        if (UserTable.CompareDefinitions(sourceUserTable, targetUserTable))
                        {
                            UserTable.IntersectWith(sourceDataContext, targetDataContext, sourceUserTable, targetUserTable, dataComparisonType);
                            if (UserTable.ObjectCount(sourceUserTable) == 0)
                                RemoveUserTable(sourceSchema, userTable);
                        }
                        break;
                    case DataComparisonType.Namespaces:
                        if (UserTable.CompareObjectNames(sourceUserTable, targetUserTable))
                        {
                            UserTable.IntersectWith(sourceDataContext, targetDataContext, sourceUserTable, targetUserTable, dataComparisonType);
                            if (UserTable.ObjectCount(sourceUserTable) == 0)
                                RemoveUserTable(sourceSchema, userTable);
                        }
                        else
                        {
                            RemoveUserTable(sourceSchema, userTable);
                        }
                        break;
                    case DataComparisonType.SchemaLevelNamespaces:
                        if (!UserTable.CompareObjectNames(sourceUserTable, targetUserTable))
                            RemoveUserTable(sourceSchema, userTable);
                        break;
                }
            }

            var matchingViews = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            var removableViews = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            matchingViews.UnionWith(sourceSchema.Views.Keys);
            matchingViews.IntersectWith(targetSchema.Views.Keys);

            removableViews.UnionWith(sourceSchema.Views.Keys);
            removableViews.ExceptWith(matchingViews);

            foreach (var view in removableViews)
                RemoveView(sourceSchema, view);

            foreach (var view in matchingViews)
            {
                View sourceView;
                if (!sourceSchema.Views.TryGetValue(view, out sourceView))
                    continue;

                View targetView;
                if (!targetSchema.Views.TryGetValue(view, out targetView))
                    continue;

                switch (dataComparisonType)
                {
                    case DataComparisonType.Definitions:
                        if (!View.CompareDefinitions(sourceView, targetView))
                            RemoveView(sourceSchema, view);
                        break;
                    case DataComparisonType.Namespaces:
                    case DataComparisonType.SchemaLevelNamespaces:
                        if (!View.CompareObjectNames(sourceView, targetView))
                            RemoveView(sourceSchema, view);
                        break;
                }
            }
        }

        /// <summary>
        /// This method is called automatically through a chain of calls after Catalog.Clone()
        /// method has been called and will simply return if the Catalog... schema.Catalog...
        /// is equal to null. This method has been added to assist in populating
        /// Catalog.ForeignKeyPool and Catalog.ReferencedUserTablePool in case anything custom
        /// has to be done for any reason to those lists. UserTable.AddForeignKey automatically
        /// calls ForeignKey.LinkForeignKey and this method only needs to be used if a custom
        /// cloning method is created or after a serialization operation has completed and the
        /// calls should only be channeled through the chain of objects originating from the
        /// Catalog object as no action will take place unless the object you are passing in
        /// has a Catalog and if the foreign keys do not already exist in those lists.
        /// </summary>
        /// <param name="schema">The schema with user-tables that have foreign keys to link.</param>
        public static void LinkForeignKeys(Schema schema)
        {
            if (schema.Catalog == null)
                return;

            foreach (var userTable in schema.UserTables.Values.Where(
                userTable => userTable.ForeignKeys.Count > 0))
                UserTable.LinkForeignKeys(userTable);
        }

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
                   schema.UserTables.Values.Sum(
                       userTable => UserTable.ObjectCount(userTable, true));
        }

        public static bool RemoveAggregateFunction(Schema schema, string objectName)
        {
            return !string.IsNullOrEmpty(objectName)
                && schema.AggregateFunctions.Remove(objectName);
        }

        public static bool RemoveAggregateFunction(Schema schema, AggregateFunction aggregateFunction)
        {
            AggregateFunction aggregateFunctionObject;
            if (!schema.AggregateFunctions.TryGetValue(aggregateFunction.ObjectName, out aggregateFunctionObject))
                return false;

            return aggregateFunction.Equals(aggregateFunctionObject)
                && schema.AggregateFunctions.Remove(aggregateFunction.ObjectName);
        }

        public static bool RemoveInlineTableValuedFunction(Schema schema, string objectName)
        {
            return !string.IsNullOrEmpty(objectName)
                && schema.InlineTableValuedFunctions.Remove(objectName);
        }

        public static bool RemoveInlineTableValuedFunction(Schema schema, InlineTableValuedFunction inlineTableValuedFunction)
        {
            InlineTableValuedFunction inlineTableValuedFunctionObject;
            if (!schema.InlineTableValuedFunctions.TryGetValue(inlineTableValuedFunction.ObjectName, out inlineTableValuedFunctionObject))
                return false;

            return inlineTableValuedFunction.Equals(inlineTableValuedFunctionObject)
                && schema.InlineTableValuedFunctions.Remove(inlineTableValuedFunction.ObjectName);
        }

        public static bool RemoveScalarFunction(Schema schema, string objectName)
        {
            return !string.IsNullOrEmpty(objectName)
                && schema.ScalarFunctions.Remove(objectName);
        }

        public static bool RemoveScalarFunction(Schema schema, ScalarFunction scalarFunction)
        {
            ScalarFunction scalarFunctionObject;
            if (!schema.ScalarFunctions.TryGetValue(scalarFunction.ObjectName, out scalarFunctionObject))
                return false;

            return scalarFunction.Equals(scalarFunctionObject)
                && schema.ScalarFunctions.Remove(scalarFunction.ObjectName);
        }

        public static bool RemoveStoredProcedure(Schema schema, string objectName)
        {
            return !string.IsNullOrEmpty(objectName)
                && schema.StoredProcedures.Remove(objectName);
        }

        public static bool RemoveStoredProcedure(Schema schema, StoredProcedure storedProcedure)
        {
            StoredProcedure storedProcedureObject;
            if (!schema.StoredProcedures.TryGetValue(storedProcedure.ObjectName, out storedProcedureObject))
                return false;

            return storedProcedure.Equals(storedProcedureObject)
                && schema.StoredProcedures.Remove(storedProcedure.ObjectName);
        }

        public static bool RemoveTableValuedFunction(Schema schema, string objectName)
        {
            return !string.IsNullOrEmpty(objectName)
                && schema.TableValuedFunctions.Remove(objectName);
        }

        public static bool RemoveTableValuedFunction(Schema schema, TableValuedFunction tableValuedFunction)
        {
            TableValuedFunction tableValuedFunctionObject;
            if (!schema.TableValuedFunctions.TryGetValue(tableValuedFunction.ObjectName, out tableValuedFunctionObject))
                return false;

            return tableValuedFunction.Equals(tableValuedFunctionObject)
                && schema.TableValuedFunctions.Remove(tableValuedFunction.ObjectName);
        }

        public static bool RemoveTrigger(Schema schema, string objectName)
        {
            return !string.IsNullOrEmpty(objectName)
                && schema.Triggers.Remove(objectName);
        }

        public static bool RemoveTrigger(Schema schema, Trigger trigger)
        {
            Trigger triggerObject;
            if (!schema.Triggers.TryGetValue(trigger.ObjectName, out triggerObject))
                return false;

            return trigger.Equals(triggerObject)
                && schema.Triggers.Remove(trigger.ObjectName);
        }

        public static bool RemoveUserDefinedDataType(Schema schema, string objectName)
        {
            return !string.IsNullOrEmpty(objectName)
                && schema.UserDefinedDataTypes.Remove(objectName);
        }

        public static bool RemoveUserDefinedDataType(Schema schema, UserDefinedDataType userDefinedDataType)
        {
            UserDefinedDataType userDefinedDataTypeObject;
            if (!schema.UserDefinedDataTypes.TryGetValue(userDefinedDataType.ObjectName, out userDefinedDataTypeObject))
                return false;

            return userDefinedDataType.Equals(userDefinedDataTypeObject)
                && schema.UserDefinedDataTypes.Remove(userDefinedDataType.ObjectName);
        }

        public static bool RemoveUserTable(Schema schema, string objectName)
        {
            return !string.IsNullOrEmpty(objectName)
                && schema.UserTables.Remove(objectName);
        }

        public static bool RemoveUserTable(Schema schema, UserTable userTable)
        {
            UserTable userTableObject;
            if (!schema.UserTables.TryGetValue(userTable.ObjectName, out userTableObject))
                return false;

            return userTable.Equals(userTableObject)
                && schema.UserTables.Remove(userTable.ObjectName);
        }

        public static bool RemoveView(Schema schema, string objectName)
        {
            return !string.IsNullOrEmpty(objectName)
                && schema.Views.Remove(objectName);
        }

        public static bool RemoveView(Schema schema, View view)
        {
            View aggregateObject;
            if (!schema.Views.TryGetValue(view.ObjectName, out aggregateObject))
                return false;

            return view.Equals(aggregateObject)
                && schema.Views.Remove(view.ObjectName);
        }

        public static bool RenameAggregateFunction(Schema schema, string objectName, string newObjectName)
        {
            if (string.IsNullOrEmpty(objectName))
                return false;

            if (string.IsNullOrEmpty(newObjectName))
                return false;

            if (schema.AggregateFunctions.ContainsKey(newObjectName))
                return false;

            AggregateFunction aggregateFunction;
            if (!schema.AggregateFunctions.TryGetValue(objectName, out aggregateFunction))
                return false;

            schema.AggregateFunctions.Remove(objectName);
            aggregateFunction.Schema = null;
            aggregateFunction.ObjectName = newObjectName;
            aggregateFunction.Schema = schema;
            schema.AggregateFunctions.Add(newObjectName, aggregateFunction);
            
            return true;
        }

        public static bool RenameInlineTableValuedFunction(Schema schema, string objectName, string newObjectName)
        {
            if (string.IsNullOrEmpty(objectName))
                return false;

            if (string.IsNullOrEmpty(newObjectName))
                return false;

            if (schema.InlineTableValuedFunctions.ContainsKey(newObjectName))
                return false;

            InlineTableValuedFunction inlineTableValuedFunction;
            if (!schema.InlineTableValuedFunctions.TryGetValue(objectName, out inlineTableValuedFunction))
                return false;

            schema.InlineTableValuedFunctions.Remove(objectName);
            inlineTableValuedFunction.Schema = null;
            inlineTableValuedFunction.ObjectName = newObjectName;
            inlineTableValuedFunction.Schema = schema;
            schema.InlineTableValuedFunctions.Add(newObjectName, inlineTableValuedFunction);

            return true;
        }

        public static bool RenameScalarFunction(Schema schema, string objectName, string newObjectName)
        {
            if (string.IsNullOrEmpty(objectName))
                return false;

            if (string.IsNullOrEmpty(newObjectName))
                return false;

            if (schema.ScalarFunctions.ContainsKey(newObjectName))
                return false;

            ScalarFunction scalarFunction;
            if (!schema.ScalarFunctions.TryGetValue(objectName, out scalarFunction))
                return false;

            schema.ScalarFunctions.Remove(objectName);
            scalarFunction.Schema = null;
            scalarFunction.ObjectName = newObjectName;
            scalarFunction.Schema = schema;
            schema.ScalarFunctions.Add(newObjectName, scalarFunction);

            return true;
        }

        public static bool RenameStoredProcedure(Schema schema, string objectName, string newObjectName)
        {
            if (string.IsNullOrEmpty(objectName))
                return false;

            if (string.IsNullOrEmpty(newObjectName))
                return false;

            if (schema.StoredProcedures.ContainsKey(newObjectName))
                return false;

            StoredProcedure storedProcedure;
            if (!schema.StoredProcedures.TryGetValue(objectName, out storedProcedure))
                return false;

            schema.StoredProcedures.Remove(objectName);
            storedProcedure.Schema = null;
            storedProcedure.ObjectName = newObjectName;
            storedProcedure.Schema = schema;
            schema.StoredProcedures.Add(newObjectName, storedProcedure);
            
            return true;
        }

        public static bool RenameTableValuedFunction(Schema schema, string objectName, string newObjectName)
        {
            if (string.IsNullOrEmpty(objectName))
                return false;

            if (string.IsNullOrEmpty(newObjectName))
                return false;

            if (schema.TableValuedFunctions.ContainsKey(newObjectName))
                return false;

            TableValuedFunction tableValuedFunction;
            if (!schema.TableValuedFunctions.TryGetValue(objectName, out tableValuedFunction))
                return false;

            schema.TableValuedFunctions.Remove(objectName);
            tableValuedFunction.Schema = null;
            tableValuedFunction.ObjectName = newObjectName;
            tableValuedFunction.Schema = schema;
            schema.TableValuedFunctions.Add(newObjectName, tableValuedFunction);
            
            return true;
        }

        public static bool RenameTrigger(Schema schema, string objectName, string newObjectName)
        {
            if (string.IsNullOrEmpty(objectName))
                return false;

            if (string.IsNullOrEmpty(newObjectName))
                return false;

            if (schema.Triggers.ContainsKey(newObjectName))
                return false;
            
            Trigger trigger;
            if (!schema.Triggers.TryGetValue(objectName, out trigger))
                return false;

            schema.Triggers.Remove(objectName);
            trigger.Schema = null;
            trigger.ObjectName = newObjectName;
            trigger.Schema = schema;
            schema.Triggers.Add(newObjectName, trigger);
            
            return true;
        }

        public static bool RenameUserDefinedDataType(Schema schema, string objectName, string newObjectName)
        {
            if (string.IsNullOrEmpty(objectName))
                return false;

            if (string.IsNullOrEmpty(newObjectName))
                return false;

            if (schema.UserDefinedDataTypes.ContainsKey(newObjectName))
                return false;
            
            UserDefinedDataType userDefinedDataType;
            if (!schema.UserDefinedDataTypes.TryGetValue(objectName, out userDefinedDataType))
                return false;

            schema.UserDefinedDataTypes.Remove(objectName);
            userDefinedDataType.Schema = null;
            userDefinedDataType.ObjectName = newObjectName;
            userDefinedDataType.Schema = schema;
            schema.UserDefinedDataTypes.Add(newObjectName, userDefinedDataType);
            
            return true;
        }

        public static bool RenameUserTable(Schema schema, string objectName, string newObjectName)
        {
            if (string.IsNullOrEmpty(objectName))
                return false;

            if (string.IsNullOrEmpty(newObjectName))
                return false;

            if (schema.UserTables.ContainsKey(newObjectName))
                return false;
            
            UserTable userTable;
            if (!schema.UserTables.TryGetValue(objectName, out userTable))
                return false;

            schema.UserTables.Remove(objectName);
            userTable.Schema = null;
            userTable.ObjectName = newObjectName;
            userTable.Schema = schema;
            schema.UserTables.Add(newObjectName, userTable);
            
            return true;
        }

        public static bool RenameView(Schema schema, string objectName, string newObjectName)
        {
            if (string.IsNullOrEmpty(objectName))
                return false;

            if (string.IsNullOrEmpty(newObjectName))
                return false;

            if (schema.Views.ContainsKey(newObjectName))
                return false;
            
            View view;
            if (!schema.Views.TryGetValue(objectName, out view))
                return false;

            schema.Views.Remove(objectName);
            view.Schema = null;
            view.ObjectName = newObjectName;
            view.Schema = schema;
            schema.Views.Add(newObjectName, view);
            
            return true;
        }

        /// <summary>
        /// Shallow Clone...
        /// A clone of this class's isntance specific metadata.
        /// </summary>
        /// <param name="schema">The schema to shallow clone.</param>
        /// <returns>A clone of this class's isntance specific metadata.</returns>
        public static Schema ShallowClone(Schema schema)
        {
            return new Schema(schema.ObjectName);
        }

        public static string ToJson(Schema schema, Formatting formatting = Formatting.Indented)
        {
            return JsonConvert.SerializeObject(schema, formatting);
        }

        /// <summary>
        /// Modifies the source Schema to contain all objects that are
        /// present in both iteself and in the target Schema.
        /// </summary>
        /// <param name="sourceDataContext">The source DataContext.</param>
        /// <param name="targetDataContext">The target DataContext.</param>
        /// <param name="sourceSchema">The source Schema.</param>
        /// <param name="targetSchema">The target schema.</param>
        /// <param name="dataComparisonType">
        /// The completeness of comparisons between matching objects.
        /// </param>
        public static void UnionWith(DataContext sourceDataContext, DataContext targetDataContext, Schema sourceSchema, Schema targetSchema,
            DataComparisonType dataComparisonType = DataComparisonType.SchemaLevelNamespaces)
        {
            var matchingAggregateFunctions = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            var addableAggregateFunctions = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            matchingAggregateFunctions.UnionWith(sourceSchema.AggregateFunctions.Keys);
            matchingAggregateFunctions.IntersectWith(targetSchema.AggregateFunctions.Keys);

            addableAggregateFunctions.UnionWith(targetSchema.AggregateFunctions.Keys);
            addableAggregateFunctions.ExceptWith(matchingAggregateFunctions);

            foreach (var aggregateFunction in addableAggregateFunctions)
            {
                AggregateFunction targetAggregateFunction;
                if (!targetSchema.AggregateFunctions.TryGetValue(aggregateFunction, out targetAggregateFunction))
                    continue;

                AddAggregateFunction(sourceSchema, AggregateFunction.Clone(targetAggregateFunction));
            }

            var matchingInlineTableValuedFunctions = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            var addableInlineTableValuedFunctions = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            matchingInlineTableValuedFunctions.UnionWith(sourceSchema.InlineTableValuedFunctions.Keys);
            matchingInlineTableValuedFunctions.IntersectWith(targetSchema.InlineTableValuedFunctions.Keys);

            addableInlineTableValuedFunctions.UnionWith(targetSchema.InlineTableValuedFunctions.Keys);
            addableInlineTableValuedFunctions.ExceptWith(matchingInlineTableValuedFunctions);

            foreach (var inlineTableValuedFunction in addableInlineTableValuedFunctions)
            {
                InlineTableValuedFunction targetInlineTableValuedFunction;
                if (!targetSchema.InlineTableValuedFunctions.TryGetValue(inlineTableValuedFunction, out targetInlineTableValuedFunction))
                    continue;

                AddInlineTableValuedFunction(sourceSchema, InlineTableValuedFunction.Clone(targetInlineTableValuedFunction));
            }

            var matchingScalarFunctions = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            var addableScalarFunctions = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            matchingScalarFunctions.UnionWith(sourceSchema.ScalarFunctions.Keys);
            matchingScalarFunctions.IntersectWith(targetSchema.ScalarFunctions.Keys);

            addableScalarFunctions.UnionWith(targetSchema.ScalarFunctions.Keys);
            addableScalarFunctions.ExceptWith(matchingScalarFunctions);

            foreach (var scalarFunction in addableScalarFunctions)
            {
                ScalarFunction targetScalarFunction;
                if (!targetSchema.ScalarFunctions.TryGetValue(scalarFunction, out targetScalarFunction))
                    continue;

                AddScalarFunction(sourceSchema, ScalarFunction.Clone(targetScalarFunction));
            }

            var matchingStoredProcedures = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            var addableStoredProcedures = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            matchingStoredProcedures.UnionWith(sourceSchema.StoredProcedures.Keys);
            matchingStoredProcedures.IntersectWith(targetSchema.StoredProcedures.Keys);

            addableStoredProcedures.UnionWith(targetSchema.StoredProcedures.Keys);
            addableStoredProcedures.ExceptWith(matchingStoredProcedures);

            foreach (var storedProcedure in addableStoredProcedures)
            {
                StoredProcedure targetStoredProcedure;
                if (!targetSchema.StoredProcedures.TryGetValue(storedProcedure, out targetStoredProcedure))
                    continue;

                AddStoredProcedure(sourceSchema, StoredProcedure.Clone(targetStoredProcedure));
            }

            var matchingTableValuedFunctions = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            var addableTableValuedFunctions = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            matchingTableValuedFunctions.UnionWith(sourceSchema.TableValuedFunctions.Keys);
            matchingTableValuedFunctions.IntersectWith(targetSchema.TableValuedFunctions.Keys);

            addableTableValuedFunctions.UnionWith(targetSchema.TableValuedFunctions.Keys);
            addableTableValuedFunctions.ExceptWith(matchingTableValuedFunctions);

            foreach (var tableValuedFunction in addableTableValuedFunctions)
            {
                TableValuedFunction targetTableValuedFunction;
                if (!targetSchema.TableValuedFunctions.TryGetValue(tableValuedFunction, out targetTableValuedFunction))
                    continue;

                AddTableValuedFunction(sourceSchema, TableValuedFunction.Clone(targetTableValuedFunction));
            }

            var matchingTriggers = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            var addableTriggers = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            matchingTriggers.UnionWith(sourceSchema.Triggers.Keys);
            matchingTriggers.IntersectWith(targetSchema.Triggers.Keys);

            addableTriggers.UnionWith(targetSchema.Triggers.Keys);
            addableTriggers.ExceptWith(matchingTriggers);

            foreach (var trigger in addableTriggers)
            {
                Trigger targetTrigger;
                if (!targetSchema.Triggers.TryGetValue(trigger, out targetTrigger))
                    continue;

                AddTrigger(sourceSchema, Trigger.Clone(targetTrigger));
            }

            var matchingUserDefinedDataTypes = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            var addableUserDefinedDataTypes = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            matchingUserDefinedDataTypes.UnionWith(sourceSchema.UserDefinedDataTypes.Keys);
            matchingUserDefinedDataTypes.IntersectWith(targetSchema.UserDefinedDataTypes.Keys);

            addableUserDefinedDataTypes.UnionWith(targetSchema.UserDefinedDataTypes.Keys);
            addableUserDefinedDataTypes.ExceptWith(matchingUserDefinedDataTypes);

            foreach (var userDefinedDataType in addableUserDefinedDataTypes)
            {
                UserDefinedDataType targetUserDefinedDataType;
                if (!targetSchema.UserDefinedDataTypes.TryGetValue(userDefinedDataType, out targetUserDefinedDataType))
                    continue;

                AddUserDefinedDataType(sourceSchema, UserDefinedDataType.Clone(targetUserDefinedDataType));
            }

            var matchingUserTables = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            var addableUserTables = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            matchingUserTables.UnionWith(sourceSchema.UserTables.Keys);
            matchingUserTables.IntersectWith(targetSchema.UserTables.Keys);

            addableUserTables.UnionWith(targetSchema.UserTables.Keys);
            addableUserTables.ExceptWith(matchingUserTables);

            foreach (var userTable in addableUserTables)
            {
                UserTable targetUserTable;
                if (!targetSchema.UserTables.TryGetValue(userTable, out targetUserTable))
                    continue;

                AddUserTable(sourceSchema, UserTable.Clone(targetUserTable));
            }

            foreach (var userTable in matchingUserTables)
            {
                UserTable sourceUserTable;
                if (!sourceSchema.UserTables.TryGetValue(userTable, out sourceUserTable))
                    continue;

                UserTable targetUserTable;
                if (!targetSchema.UserTables.TryGetValue(userTable, out targetUserTable))
                    continue;

                switch (dataComparisonType)
                {
                    case DataComparisonType.Definitions:
                        if (UserTable.CompareDefinitions(sourceUserTable, targetUserTable))
                            UserTable.UnionWith(sourceUserTable, targetUserTable, sourceDataContext,
                                targetDataContext, dataComparisonType);
                        break;
                    case DataComparisonType.Namespaces:
                        if (UserTable.CompareObjectNames(sourceUserTable, targetUserTable))
                            UserTable.UnionWith(sourceUserTable, targetUserTable, sourceDataContext,
                                targetDataContext, dataComparisonType);
                        break;
                    //case DataComparisonType.SchemaLevelNamespaces:
                    //    // Do Nothing
                    //    break;
                }
            }

            var matchingViews = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            var addableViews = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            matchingViews.UnionWith(sourceSchema.Views.Keys);
            matchingViews.IntersectWith(targetSchema.Views.Keys);

            addableViews.UnionWith(targetSchema.Views.Keys);
            addableViews.ExceptWith(matchingViews);

            foreach (var view in addableViews)
            {
                View targetView;
                if (!targetSchema.Views.TryGetValue(view, out targetView))
                    continue;

                AddView(sourceSchema, View.Clone(targetView));
            }
        }

		#endregion Public Methods 
		#region Private Methods (1) 

        private static void Init(Schema schema, Catalog catalog, string objectName)
        {
            schema.AggregateFunctions = new Dictionary<string, AggregateFunction>(StringComparer.OrdinalIgnoreCase);
            schema.InlineTableValuedFunctions = new Dictionary<string, InlineTableValuedFunction>(StringComparer.OrdinalIgnoreCase);
            schema.TableValuedFunctions = new Dictionary<string, TableValuedFunction>(StringComparer.OrdinalIgnoreCase);
            schema.ScalarFunctions = new Dictionary<string, ScalarFunction>(StringComparer.OrdinalIgnoreCase);
            schema.StoredProcedures = new Dictionary<string, StoredProcedure>(StringComparer.OrdinalIgnoreCase);
            schema.Triggers = new Dictionary<string, Trigger>(StringComparer.OrdinalIgnoreCase);
            schema.UserDefinedDataTypes = new Dictionary<string, UserDefinedDataType>(StringComparer.OrdinalIgnoreCase);
            schema.UserTables = new Dictionary<string, UserTable>(StringComparer.OrdinalIgnoreCase);
            schema.Views = new Dictionary<string, View>(StringComparer.OrdinalIgnoreCase);

            schema.Catalog = catalog;
            schema._objectName = string.IsNullOrEmpty(objectName)
                ? "Default"
                : objectName;

            schema.Description = "Schema";
        }

		#endregion Private Methods 

		#endregion Methods 
    }
}
