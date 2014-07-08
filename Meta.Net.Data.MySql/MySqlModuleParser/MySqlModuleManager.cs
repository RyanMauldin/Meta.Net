using System;
using System.Text;
using System.IO;
using Meta.Net.Data.Metadata;

namespace Meta.Net.Data.MySql.MySqlModuleParser
{
    public class MySqlModuleManager
    {
        public static string GetAlterDefinition(string createDefinition)
        {
            if (string.IsNullOrEmpty(createDefinition)
                || createDefinition.Length < 6) return "";
            
            var tokenizer = new MySqlModuleTokenizer(new StringReader(createDefinition));
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

        public static MySqlModuleType GetModuleType(ModulesRow modulesRow)
        {
            switch (modulesRow.Description)
            {
                case "FUNCTION":
                    return MySqlModuleType.Function;
                case "PROCEDURE":
                    return MySqlModuleType.Procedure;
                case "TRIGGER":
                    return MySqlModuleType.Trigger;
                case "VIEW":
                    return MySqlModuleType.View;
                default:
                    return MySqlModuleType.Unknown;
            }
        }
    }
}
