using System;
using System.Text;
using System.IO;
using Meta.Net.Interfaces;

namespace Meta.Net.SqlServer.SqlServerModuleParser
{
    public class SqlServerModuleManager
    {
        public static string GetAlterDefinition(string createDefinition)
        {
            if (string.IsNullOrEmpty(createDefinition)
                || createDefinition.Length < 6) return "";

            var tokenizer = new SqlServerModuleTokenizer(new StringReader(createDefinition));
            var index = 0;
            var maxPossibleTokens = createDefinition.Length;

            try
            {
                var createFound = false;
                for (var i = 0; i < maxPossibleTokens - 6; i++)
                {
                    var token = tokenizer.Next();
                    if (token == null) break;
                    switch (token.Name)
                    {
                        case "MULTI_LINE_COMMENT":
                        case "SINGLE_LINE_COMMENT":
                        case "WHITESPACE":
                            index += token.Image.Length;
                            break;
                        case "DEFINITION":
                            createFound = true;
                            break;
                        //default:
                        //    break;
                    }

                    if (createFound) break;
                }

                if (!createFound) return "";

                var builder = new StringBuilder(createDefinition);
                builder = builder.Replace("CREATE", "ALTER", index, 6);
                return builder.ToString();

            }
            catch (Exception)
            {
                return "";
            }
        }

        public static SqlServerModuleType GetModuleType(IModule dataModule)
        {
            switch (dataModule.Description)
            {
                case "AGGREGATE_FUNCTION":
                    return SqlServerModuleType.AggregateFunction;
                case "CLR_SCALAR_FUNCTION":
                    return SqlServerModuleType.ClrScalarFunction;
                case "CLR_STORED_PROCEDURE":
                    return SqlServerModuleType.ClrStoredProcedure;
                case "CLR_TABLE_VALUED_FUNCTION":
                    return SqlServerModuleType.ClrTableValuedFunction;
                case "CLR_TRIGGER":
                    return SqlServerModuleType.ClrTrigger;
                case "SQL_INLINE_TABLE_VALUED_FUNCTION":
                    return SqlServerModuleType.SqlInlineTableValuedFunction;
                case "SQL_SCALAR_FUNCTION":
                    return SqlServerModuleType.SqlScalarFunction;
                case "SQL_STORED_PROCEDURE":
                    return SqlServerModuleType.SqlStoredProcedure;
                case "SQL_TABLE_VALUED_FUNCTION":
                    return SqlServerModuleType.SqlTableValuedFunction;
                case "SQL_TRIGGER":
                    return SqlServerModuleType.SqlTrigger;
                case "VIEW":
                    return SqlServerModuleType.View;
                default:
                    return SqlServerModuleType.Unknown;
            }
        }
    }
}
