using System;
using System.Data;
using System.Data.Common;
using Meta.Net.Interfaces;
using Meta.Net.Objects;

namespace Meta.Net.Metadata
{
    public static class ModulesAdapter
    {
        public static void Read(Catalog catalog, IDataReader reader)
        {
            var schemaNameOrdinal = reader.GetOrdinal("SchemaName");
            var objectNameOrdinal = reader.GetOrdinal("ObjectName");
            var typeDescriptionOrdinal = reader.GetOrdinal("TypeDescription");
            var definitionOrdinal = reader.GetOrdinal("Definition");
            var usesAnsiNullsOrdinal = reader.GetOrdinal("UsesAnsiNulls");
            var usesQuotedIdentifierOrdinal = reader.GetOrdinal("UsesQuotedIdentifier");
            var isDisabledOrdinal = reader.GetOrdinal("IsDisabled");
            var isNotForReplicationOrdinal = reader.GetOrdinal("IsNotForReplication");
            var triggerForSchemaOrdinal = reader.GetOrdinal("TriggerForSchema");
            var triggerForObjectNameOrdinal = reader.GetOrdinal("TriggerForSchema");

            while (reader.Read())
            {
                var schemaName = Convert.ToString(reader[schemaNameOrdinal]);
                var objectName = Convert.ToString(reader[objectNameOrdinal]);
                var typeDescription = Convert.ToString(reader[typeDescriptionOrdinal]);
                var definition = Convert.ToString(reader[definitionOrdinal]);
                var usesAnsiNulls = Convert.ToBoolean(reader[usesAnsiNullsOrdinal]);
                var usesQuotedIdentifier = Convert.ToBoolean(reader[usesQuotedIdentifierOrdinal]);
                var isDisabled = Convert.ToBoolean(reader[isDisabledOrdinal]);
                var isNotForReplication = Convert.ToBoolean(reader[isNotForReplicationOrdinal]);
                var triggerForSchema = Convert.ToString(reader[triggerForSchemaOrdinal]);
                var triggerForObjectName = Convert.ToString(reader[triggerForObjectNameOrdinal]);

                var schema = catalog.Schemas[schemaName];
                if (schema == null)
                    continue;

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
                        continue;
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
                        continue;
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
                        continue;
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
                        continue;
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
                        continue;
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
                        continue;
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
                        continue;
                    //case "CLR_SCALAR_FUNCTION":
                    //case "CLR_STORED_PROCEDURE":
                    //case "CLR_TABLE_VALUED_FUNCTION":
                    //case "CLR_TRIGGER":
                }
            }
        }

        public static void Get(Catalog catalog, DbConnection connection, IMetadataScriptFactory metadataScriptFactory)
        {
            using (var command = connection.CreateCommand())
            {
                command.CommandText = metadataScriptFactory.Modules(catalog.ObjectName);
                using (var reader = command.ExecuteReader())
                {
                    if (!reader.HasRows)
                    {
                        reader.Close();
                        return;
                    }

                    Read(catalog, reader);

                    reader.Close();
                }
            }
        }
    }
}
