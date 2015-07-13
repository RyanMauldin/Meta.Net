using System;
using System.Data;
using Meta.Net.Objects;

namespace Meta.Net.Metadata.Factories
{
    internal class ModuleFactory
    {
        private int SchemaNameOrdinal { get; set; }
        private int ObjectNameOrdinal { get; set; }
        private int TypeDescriptionOrdinal { get; set; }
        private int DefinitionOrdinal { get; set; }
        private int UsesAnsiNullsOrdinal { get; set; }
        private int UsesQuotedIdentifierOrdinal { get; set; }
        private int IsDisabledOrdinal { get; set; }
        private int IsNotForReplicationOrdinal { get; set; }
        private int TriggerForSchemaOrdinal { get; set; }
        private int TriggerForObjectNameOrdinal { get; set; }

        public ModuleFactory(IDataRecord reader)
        {
            SchemaNameOrdinal = reader.GetOrdinal("SchemaName");
            ObjectNameOrdinal = reader.GetOrdinal("ObjectName");
            TypeDescriptionOrdinal = reader.GetOrdinal("TypeDescription");
            DefinitionOrdinal = reader.GetOrdinal("Definition");
            UsesAnsiNullsOrdinal = reader.GetOrdinal("UsesAnsiNulls");
            UsesQuotedIdentifierOrdinal = reader.GetOrdinal("UsesQuotedIdentifier");
            IsDisabledOrdinal = reader.GetOrdinal("IsDisabled");
            IsNotForReplicationOrdinal = reader.GetOrdinal("IsNotForReplication");
            TriggerForSchemaOrdinal = reader.GetOrdinal("TriggerForSchema");
            TriggerForObjectNameOrdinal = reader.GetOrdinal("TriggerForSchema");
        }

        public void CreateModule(
            Catalog catalog,
            IDataRecord reader)
        {
            var schemaName = Convert.ToString(reader[SchemaNameOrdinal]);
            var objectName = Convert.ToString(reader[ObjectNameOrdinal]);
            var typeDescription = Convert.ToString(reader[TypeDescriptionOrdinal]);
            var definition = Convert.ToString(reader[DefinitionOrdinal]);
            var usesAnsiNulls = Convert.ToBoolean(reader[UsesAnsiNullsOrdinal]);
            var usesQuotedIdentifier = Convert.ToBoolean(reader[UsesQuotedIdentifierOrdinal]);
            var isDisabled = Convert.ToBoolean(reader[IsDisabledOrdinal]);
            var isNotForReplication = Convert.ToBoolean(reader[IsNotForReplicationOrdinal]);
            var triggerForSchema = Convert.ToString(reader[TriggerForSchemaOrdinal]);
            var triggerForObjectName = Convert.ToString(reader[TriggerForObjectNameOrdinal]);

            var schema = catalog.Schemas[schemaName];
            if (schema == null)
                return;

            switch (typeDescription)
            {
                case "AGGREGATE_FUNCTION":
                    var aggregateFunction = new AggregateFunction
                    {
                        Schema = schema,
                        ObjectName = objectName,
                        Definition = definition,
                        UsesAnsiNulls = usesAnsiNulls,
                        UsesQuotedIdentifier = usesQuotedIdentifier
                    };

                    schema.AggregateFunctions.Add(aggregateFunction);
                    return;
                case "SQL_INLINE_TABLE_VALUED_FUNCTION":
                    var inlineTableValuedFunction = new InlineTableValuedFunction
                    {
                        Schema = schema,
                        ObjectName = objectName,
                        Definition = definition,
                        UsesAnsiNulls = usesAnsiNulls,
                        UsesQuotedIdentifier = usesQuotedIdentifier
                    };

                    schema.InlineTableValuedFunctions.Add(inlineTableValuedFunction);
                    return;
                case "SQL_SCALAR_FUNCTION":
                case "FUNCTION":
                    var scalarFunction = new ScalarFunction
                    {
                        Schema = schema,
                        ObjectName = objectName,
                        Definition = definition,
                        UsesAnsiNulls = usesAnsiNulls,
                        UsesQuotedIdentifier = usesQuotedIdentifier
                    };

                    schema.ScalarFunctions.Add(scalarFunction);
                    return;
                case "SQL_STORED_PROCEDURE":
                case "PROCEDURE":
                    var storedProcedure = new StoredProcedure
                    {
                        Schema = schema,
                        ObjectName = objectName,
                        Definition = definition,
                        UsesAnsiNulls = usesAnsiNulls,
                        UsesQuotedIdentifier = usesQuotedIdentifier
                    };

                    schema.StoredProcedures.Add(storedProcedure);
                    return;
                case "SQL_TABLE_VALUED_FUNCTION":
                    var tableValuedFunction = new TableValuedFunction
                    {
                        Schema = schema,
                        ObjectName = objectName,
                        Definition = definition,
                        UsesAnsiNulls = usesAnsiNulls,
                        UsesQuotedIdentifier = usesQuotedIdentifier
                    };

                    schema.TableValuedFunctions.Add(tableValuedFunction);
                    return;
                case "SQL_TRIGGER":
                case "TRIGGER":
                    var trigger = new Trigger
                    {
                        Schema = schema,
                        ObjectName = objectName,
                        Definition = definition,
                        UsesAnsiNulls = usesAnsiNulls,
                        UsesQuotedIdentifier = usesQuotedIdentifier,
                        IsDisabled = isDisabled,
                        IsNotForReplication = isNotForReplication,
                        TriggerForSchema = triggerForSchema,
                        TriggerForObjectName = triggerForObjectName
                    };

                    schema.Triggers.Add(trigger);
                    return;
                case "VIEW":
                    var view = new View
                    {
                        Schema = schema,
                        ObjectName = objectName,
                        Definition = definition,
                        UsesAnsiNulls = usesAnsiNulls,
                        UsesQuotedIdentifier = usesQuotedIdentifier
                    };

                    schema.Views.Add(view);
                    return;
                //case "CLR_SCALAR_FUNCTION":
                //case "CLR_STORED_PROCEDURE":
                //case "CLR_TABLE_VALUED_FUNCTION":
                //case "CLR_TRIGGER":
            }
        }
    }
}
