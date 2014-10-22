using System;
using System.Collections.Generic;
using System.Text;
using Meta.Net.Sync.Types;
using Meta.Net.Types;
using Meta.Net.Objects;

namespace Meta.Net.Sync.Converters
{
    public class DataTypes
    {
		private static Dictionary<string, CommonDataType> CommonLookup { get; set; }
        private static Dictionary<CommonDataType, string> MySqlToSqlServerLookup { get; set; }
        private static Dictionary<CommonDataType, string> SqlServerToMySqlLookup { get; set; }
        
        /// <summary>
        /// Static constructor. Initializes CommonLookup, MySqlToSqlServerLookup, and SqlServerToMySqlLookup.
        /// </summary>
        static DataTypes()
        {
            BuildCommonLookup();
            BuildMySqlToSqlServerLookup();
            BuildSqlServerToMySqlLookup();
        }

		/// <summary>
        /// Compares source and target user-table columns and converts datatypes and collations in
        /// regard to differing datasource specifics.
        /// </summary>
        /// <param name="sourceDataContext">The source DataContext.</param>
        /// <param name="targetDataContext">The target DataContext.</param>
        /// <param name="sourceUserTableColumn">The source UserTableColumn.</param>
        /// <param name="targetUserTableColumn">The target UserTableColumn.</param>
        /// <returns>
        ///     True - The definitions mached between two datasources in regard with differing datasource conversions.
        ///     False - The definitions did not match up.
        /// </returns>
        public static bool CompareDataType(DataContext sourceDataContext, DataContext targetDataContext, UserTableColumn sourceUserTableColumn, UserTableColumn targetUserTableColumn)
        {
            switch (sourceDataContext.ContextType)
            {
                case DataContextType.MySql:
                    switch (targetDataContext.ContextType)
                    {
                        case DataContextType.MySql:
                            return StringComparer.OrdinalIgnoreCase.Compare(
                                BuildMySqlToMySqlDefinition(sourceDataContext, targetDataContext, sourceUserTableColumn),
                                BuildMySqlToMySqlDefinition(sourceDataContext, targetDataContext, targetUserTableColumn)) == 0;
                        case DataContextType.SqlServer:
                            return StringComparer.OrdinalIgnoreCase.Compare(
                                BuildMySqlToSqlServerDefinition(sourceDataContext, targetDataContext, sourceUserTableColumn),
                                BuildSqlServerToSqlServerDefinition(sourceDataContext, targetDataContext, targetUserTableColumn)) == 0;
                        default:
                            return false;
                    }
                case DataContextType.SqlServer:
                    switch (targetDataContext.ContextType)
                    {
                        case DataContextType.MySql:
                            return StringComparer.OrdinalIgnoreCase.Compare(
                                BuildSqlServerToMySqlDefinition(sourceDataContext, targetDataContext, sourceUserTableColumn),
                                BuildMySqlToMySqlDefinition(sourceDataContext, targetDataContext, targetUserTableColumn)) == 0;
                        case DataContextType.SqlServer:
                            return StringComparer.OrdinalIgnoreCase.Compare(
                                BuildSqlServerToSqlServerDefinition(sourceDataContext, targetDataContext, sourceUserTableColumn),
                                BuildSqlServerToSqlServerDefinition(sourceDataContext, targetDataContext, targetUserTableColumn)) == 0;
                        default:
                            return false;
                    }
                default:
                    return true;
            }
        }

        /// <summary>
        /// Converts the referenced user-table column from its equivilent source DataContext definition
        /// to the target DataContext definition and adjust appropriate context specific values on the column
        /// passed in.
        /// </summary>
        /// <param name="sourceDataContext">The source DataContext.</param>
        /// <param name="targetDataContext">The target DataContext.</param>
        /// <param name="userTableColumn">The user-table column to convert in regard to source and target DataContext.</param>
        public static void ConvertDataType(DataContext sourceDataContext, DataContext targetDataContext, ref UserTableColumn userTableColumn)
        {
            switch (sourceDataContext.ContextType)
            {
                case DataContextType.MySql:
                    switch (targetDataContext.ContextType)
                    {
                        case DataContextType.MySql:
                            return;
                        case DataContextType.SqlServer:
                            ConvertMySqlToSqlServer(sourceDataContext, targetDataContext, ref userTableColumn);
                            return;
                        default:
                            return;
                    }
                case DataContextType.SqlServer:
                    switch (targetDataContext.ContextType)
                    {
                        case DataContextType.MySql:
                            ConvertSqlServerToMySql(sourceDataContext, targetDataContext, ref userTableColumn);
                            return;
                        case DataContextType.SqlServer:
                            return;
                        default:
                            return;
                    }
                default:
                    return;
            }
        }

        /// <summary>
        /// Gets a converted (if needed) version of the user-table column definition for use in
        /// ALTER TABLE and CREATE TABLE statements.
        /// </summary>
        /// <param name="sourceDataContext">The source DataContext.</param>
        /// <param name="targetDataContext">The target DataContext.</param>
        /// <param name="userTableColumn">The user-table column to convert if needed and to pull the data definition from.</param>
        /// <returns></returns>
        public static string ConvertDataTypeDefinition(DataContext sourceDataContext, DataContext targetDataContext, UserTableColumn userTableColumn)
        {
            switch (sourceDataContext.ContextType)
            {
                case DataContextType.MySql:
                    switch (targetDataContext.ContextType)
                    {
                        case DataContextType.MySql:
                            return BuildMySqlToMySqlDefinition(sourceDataContext, targetDataContext, userTableColumn);
                        case DataContextType.SqlServer:
                            return BuildMySqlToSqlServerDefinition(sourceDataContext, targetDataContext, userTableColumn);
                        default:
                            return null;
                    }
                case DataContextType.SqlServer:
                    switch (targetDataContext.ContextType)
                    {
                        case DataContextType.MySql:
                            return BuildSqlServerToMySqlDefinition(sourceDataContext, targetDataContext, userTableColumn);
                        case DataContextType.SqlServer:
                            return BuildSqlServerToSqlServerDefinition(sourceDataContext, targetDataContext, userTableColumn);
                        default:
                            return null;
                    }
                default:
                    return null;
            }
        }

        /// <summary>
        /// Gets the CommonDataType enumerated value from the dataType string such as int, decimal. This
        /// method uses non case sensitive comparisons when matching. The CommonDataType is useful for
        /// converting back and forth between multiple datasources.
        /// </summary>
        /// <param name="dataType">int, decimal, real, etc... from multiple datasources...</param>
        /// <returns>CommonDataType.Integer32, etc...</returns>
        public static CommonDataType GetCommonDataType(string dataType)
        {
            CommonDataType commonDataType;
            return !CommonLookup.TryGetValue(dataType, out commonDataType)
                ? CommonDataType.VarBinary
                : commonDataType;
        }

        /// <summary>
        /// Allows us to know whether or not we will need to drop the user-table column and then re-add it with the
        /// new data-type or column definition rather than being able to perform a table alter. Changing a
        /// user-table column's data-type from datetime to timestamp is a prime example of 
        /// </summary>
        /// <param name="sourceDataContext">The source DataContext.</param>
        /// <param name="targetDataContext">The target DataContext.</param>
        /// <param name="sourceUserTableColumn">The source UserTableColumn.</param>
        /// <param name="targetUserTableColumn">the target UserTableColumn.</param>
        /// <returns>
        ///     true - the datatype can be altered using ALTER TABLE Column Definition... syntax.
        ///     false - the UserTableColumn must be dropped and re-created for the datatype to be changed on the table.
        /// </returns>
        public static bool IsCompatibleAlter(DataContext sourceDataContext, DataContext targetDataContext, UserTableColumn sourceUserTableColumn, UserTableColumn targetUserTableColumn)
        {
            switch (sourceDataContext.ContextType)
            {
                case DataContextType.MySql:
                    switch (targetDataContext.ContextType)
                    {
                        case DataContextType.MySql:
                            return IsCompatibleMySqlToMySqlAlter(sourceUserTableColumn, targetUserTableColumn);
                        case DataContextType.SqlServer:
                            return IsCompatibleMySqlToSqlServerAlter(sourceUserTableColumn, targetUserTableColumn);
                        default:
                            return false;
                    }
                case DataContextType.SqlServer:
                    switch (targetDataContext.ContextType)
                    {
                        case DataContextType.MySql:
                            return IsCompatibleSqlServerToMySqlAlter(sourceUserTableColumn, targetUserTableColumn);
                        case DataContextType.SqlServer:
                            return IsCompatibleSqlServerToSqlServerAlter(sourceUserTableColumn, targetUserTableColumn);
                        default:
                            return false;
                    }
                default:
                    return false;
            }
        }

		/// <summary>
        /// Populates the CommonLookup member.
        /// </summary>
        private static void BuildCommonLookup()
        {
            CommonLookup = new Dictionary<string, CommonDataType>(StringComparer.OrdinalIgnoreCase)
            {
                {"bigint", CommonDataType.Integer64},
                {"binary", CommonDataType.Binary},
                {"bit", CommonDataType.Bit},
                {"blob", CommonDataType.Blob},
                {"bool", CommonDataType.Boolean},
                {"boolean", CommonDataType.Boolean},
                {"char", CommonDataType.Char},
                {"character", CommonDataType.Char},
                {"date", CommonDataType.Date},
                {"datetime", CommonDataType.DateTime},
                {"datetime2", CommonDataType.DateTime2},
                {"datetimeoffset", CommonDataType.DateTimeOffset},
                {"dec", CommonDataType.Decimal},
                {"decimal", CommonDataType.Decimal},
                {"double precision", CommonDataType.Double},
                {"double", CommonDataType.Double},
                {"enum", CommonDataType.Enum},
                {"fixed", CommonDataType.Decimal},
                {"float", CommonDataType.Float},
                {"geography", CommonDataType.Geography},
                {"geometry", CommonDataType.Geometry},
                {"hierarchyid", CommonDataType.HierarchyId},
                {"image", CommonDataType.Image},
                {"int", CommonDataType.Integer32},
                {"integer", CommonDataType.Integer32},
                {"longblob", CommonDataType.LongBlob},
                {"longtext", CommonDataType.LongText},
                {"mediumblob", CommonDataType.MediumBlob},
                {"mediumint", CommonDataType.Integer24},
                {"mediumtext", CommonDataType.MediumText},
                {"money", CommonDataType.Money},
                {"nchar", CommonDataType.NChar},
                {"ntext", CommonDataType.NText},
                {"numeric", CommonDataType.Numeric},
                {"nvarchar", CommonDataType.NVarChar},
                {"real", CommonDataType.Real},
                {"set", CommonDataType.Set},
                {"smalldatetime", CommonDataType.SmallDateTime},
                {"smallint", CommonDataType.Integer16},
                {"smallmoney", CommonDataType.SmallMoney},
                {"sql_variant", CommonDataType.SqlVariant},
                {"text", CommonDataType.Text},
                {"time", CommonDataType.Time},
                {"timestamp", CommonDataType.TimeStamp},
                {"tinyblob", CommonDataType.TinyBlob},
                {"tinyint", CommonDataType.Integer8},
                {"tinytext", CommonDataType.TinyText},
                {"uniqueidentifier", CommonDataType.UniqueIdentifier},
                {"varbinary", CommonDataType.VarBinary},
                {"varchar", CommonDataType.VarChar},
                {"xml", CommonDataType.Xml},
                {"year", CommonDataType.Year}
            };
        }

        /// <summary>
        /// Specific for MySql -> MySql
        /// Builds the text needed to add precision to the user-table column definition.
        /// </summary>
        /// <param name="userTableColumn">The user-table column to get the precision of.</param>
        /// <returns>Ex: (19, 4), (11), etc..</returns>
        private static string BuildMySqlToMySqlColumnPrecision(UserTableColumn userTableColumn)
        {
            switch (userTableColumn.DataType)
            {
                case "decimal":
                case "numeric":
                    if (userTableColumn.Precision > 0 && userTableColumn.Scale > 0)
                        return string.Format("({0},{1})", userTableColumn.Precision, userTableColumn.Scale);
                    break;
                case "float":
                // case "real":
                if (userTableColumn.Precision > 0)
                    return string.Format("({0})", userTableColumn.Precision);
                    break;
                case "datetimeoffset":
                    if (userTableColumn.Precision > 0)
                        return string.Format("({0})", userTableColumn.Precision);
                    break;
                case "char":
                case "binary":
                    if (userTableColumn.MaxLength > 0)
                        return string.Format("({0})", userTableColumn.MaxLength);
                    break;
                case "nchar":
                    if (userTableColumn.MaxLength > 0)
                        return string.Format("({0})", userTableColumn.MaxLength / 2);
                    break;
                case "varchar":
                case "varbinary":
                    if (userTableColumn.MaxLength == -1)
                        return "(255)"; // TODO: http://stackoverflow.com/questions/332798/equivalent-of-varcharmax-in-mysql
                    if (userTableColumn.MaxLength > 0)
                        return string.Format("({0})", userTableColumn.MaxLength);
                    break;
                case "nvarchar":
                    if (userTableColumn.MaxLength == -1)
                        return "(255)"; // TODO: http://stackoverflow.com/questions/332798/equivalent-of-varcharmax-in-mysql
                    if (userTableColumn.MaxLength > 0)
                        return string.Format("({0})", userTableColumn.MaxLength / 2);
                    break;
            }

            return string.Empty;
        }

        /// <summary>
        /// Generates the MySql column definition of the user-table column as it would appear
        /// in a CREATE TABLE or ALTER TABLE... SQL statement.
        /// </summary>
        /// <param name="sourceDataContext">Must be of DataContextType.MySql.</param>
        /// <param name="targetDataContext">Must be of DataContextType.MySql.</param>
        /// <param name="userTableColumn">The user-table column to create the converted definiton of.</param>
        /// <returns>The MySql column definition.</returns>
        private static string BuildMySqlToMySqlDefinition(DataContext sourceDataContext, DataContext targetDataContext, UserTableColumn userTableColumn)
        {
            if (sourceDataContext.ContextType != DataContextType.MySql)
                throw new Exception(string.Format("Invalid sourceDataContext type of {0} for DataTypes.BuildMySqlToMySqlDefinition", sourceDataContext.ContextType));

            if (targetDataContext.ContextType != DataContextType.MySql)
                throw new Exception(string.Format("Invalid targetDataContext type of {0} for DataTypes.BuildMySqlToMySqlDefinition", targetDataContext.ContextType));

            var builder = new StringBuilder();
            builder.AppendFormat("{0}{1}{2} {3}{4}",
                targetDataContext.IdentifierOpenChar,
                userTableColumn.ObjectName,
                targetDataContext.IdentifierCloseChar,
                userTableColumn.DataType,
                BuildMySqlToMySqlColumnPrecision(userTableColumn));

            if (!userTableColumn.IsUserDefined && userTableColumn.Collation.Length > 0)
            {
                var collation = DataCollations.ConvertCollation(sourceDataContext, targetDataContext, userTableColumn);
                if (!string.IsNullOrEmpty(collation))
                    builder.AppendFormat(" COLLATE {0}", userTableColumn.Collation);
            }

            builder.AppendFormat(" {0}", userTableColumn.IsNullable ? "NULL" : "NOT NULL");
            if (userTableColumn.IsIdentity)
                builder.Append(" AUTO_INCREMENT");

            return builder.ToString();
        }

        /// <summary>
        /// Specific for MySql -> SqlServer
        /// Builds the text needed to add precision to the user-table column definition.
        /// </summary>
        /// <param name="userTableColumn">The user-table column to get the precision of.</param>
        /// <returns>Ex: (19, 4), (11), etc..</returns>
        private static string BuildMySqlToSqlServerColumnPrecision(UserTableColumn userTableColumn)
        {
            switch (userTableColumn.DataType)
            {
                case "decimal":
                case "numeric":
                    if (userTableColumn.Precision > 0 && userTableColumn.Scale > 0)
                        return string.Format("({0},{1})", userTableColumn.Precision, userTableColumn.Scale);
                    break;
                case "float":
                // case "real":
                    if (userTableColumn.Precision > 0)
                        return string.Format("({0})", userTableColumn.Precision);
                    break;
                case "char":
                case "binary":
                    if (userTableColumn.MaxLength > 0)
                        return string.Format("({0})", userTableColumn.MaxLength);
                    break;
                case "nchar":
                    if (userTableColumn.MaxLength > 0)
                        return string.Format("({0})", userTableColumn.MaxLength / 2);
                    break;
                case "varchar":
                case "varbinary":
                    if (userTableColumn.MaxLength == -1)
                        return "(255)"; // TODO: http://stackoverflow.com/questions/332798/equivalent-of-varcharmax-in-mysql
                    if (userTableColumn.MaxLength > 0)
                        return string.Format("({0})", userTableColumn.MaxLength);
                    break;
                case "nvarchar":
                    if (userTableColumn.MaxLength == -1)
                        return "(255)";  // TODO: http://stackoverflow.com/questions/332798/equivalent-of-varcharmax-in-mysql
                    if (userTableColumn.MaxLength > 0)
                        return string.Format("({0})", userTableColumn.MaxLength / 2);
                    break;
            }

            return string.Empty;
        }

        /// <summary>
        /// Creates a clone of the userTableColumn and converts it from a MySql context to a
        /// SqlServer context and then generates the column definition as it would appear
        /// in a CREATE TABLE or ALTER TABLE... SQL statement.
        /// </summary>
        /// <param name="sourceDataContext">Must be of DataContextType.MySql.</param>
        /// <param name="targetDataContext">Must be of DataContextType.SqlServer.</param>
        /// <param name="userTableColumn">The user-table column to create the converted definiton of.</param>
        /// <returns>The MySql -> SqlServer converted column definition.</returns>
        private static string BuildMySqlToSqlServerDefinition(DataContext sourceDataContext, DataContext targetDataContext, UserTableColumn userTableColumn)
        {
            if (sourceDataContext.ContextType != DataContextType.MySql)
                throw new Exception(string.Format("Invalid sourceDataContext type of {0} for DataTypes.BuildMySqlToSqlServerDefinition", sourceDataContext.ContextType));

            if (targetDataContext.ContextType != DataContextType.SqlServer)
                throw new Exception(string.Format("Invalid targetDataContext type of {0} for DataTypes.BuildMySqlToSqlServerDefinition", targetDataContext.ContextType));

            var userTableColumnClone = UserTableColumn.Clone(userTableColumn);
            ConvertDataType(sourceDataContext, targetDataContext, ref userTableColumnClone);

            var builder = new StringBuilder();

            builder.AppendFormat("{0}{1}{2} {3}{4}{5}{6}",
                targetDataContext.IdentifierOpenChar,
                userTableColumnClone.ObjectName,
                targetDataContext.IdentifierCloseChar,
                targetDataContext.IdentifierOpenChar,
                userTableColumnClone.DataType,
                targetDataContext.IdentifierCloseChar,
                BuildMySqlToSqlServerColumnPrecision(userTableColumnClone));
            
            if (!userTableColumnClone.IsUserDefined && userTableColumnClone.Collation.Length > 0)
            {
                if (!string.IsNullOrEmpty(userTableColumnClone.Collation))
                    builder.AppendFormat(" COLLATE {0}", userTableColumnClone.Collation);
            }

            if (userTableColumnClone.IsIdentity)
            {
                var identityColumn = userTableColumn.UserTable.IdentityColumns[userTableColumn.Namespace];
                if (identityColumn != null && (DataProperties.ReplicateItemsMarkedNotForReplication || !identityColumn.IsNotForReplication))
                {
                    builder.AppendFormat(" IDENTITY ({0},{1})",
                        identityColumn.SeedValue,
                        identityColumn.IncrementValue);

                    if (identityColumn.IsNotForReplication)
                        builder.Append(" NOT FOR REPLICATION");
                }
            }

            if (userTableColumnClone.IsRowGuidColumn)
                builder.Append(" ROWGUIDCOL");

            builder.AppendFormat(" {0}", userTableColumnClone.IsNullable ? "NULL" : "NOT NULL");
            
            return builder.ToString();
        }

        /// <summary>
        /// Populates the MySqlToSqlServerLookup member.
        /// </summary>
        private static void BuildMySqlToSqlServerLookup()
        {
            MySqlToSqlServerLookup =
                new Dictionary<CommonDataType, string>
                {  
                    {CommonDataType.Binary, "binary"},
                    {CommonDataType.Bit, "bit"},
                    {CommonDataType.Blob, "varbinary"},
                    {CommonDataType.Boolean, "bit"},
                    {CommonDataType.Char, "char"},
                    {CommonDataType.Date, "date"},
                    {CommonDataType.DateTime, "datetime"},
                    {CommonDataType.Decimal, "decimal"},
                    {CommonDataType.Double, "double"},
                    {CommonDataType.Enum, "binary"},
                    {CommonDataType.Float, "float"},
                    {CommonDataType.Integer16, "smallint"},
                    {CommonDataType.Integer24, "int"}, // No mediumint in SqlServer
                    {CommonDataType.Integer32, "int"},
                    {CommonDataType.Integer64, "bigint"},
                    {CommonDataType.Integer8, "tinyint"},
                    {CommonDataType.LongBlob, "varbinary"},
                    {CommonDataType.LongText, "varchar"},
                    {CommonDataType.MediumBlob, "varbinary"},
                    {CommonDataType.MediumText, "varchar"},
                    {CommonDataType.NChar, "nchar"},
                    {CommonDataType.NText, "ntext"},
                    {CommonDataType.Numeric, "numeric"},
                    {CommonDataType.NVarChar, "nvarchar"},
                    {CommonDataType.Real, "real"},
                    {CommonDataType.Set, "binary"},
                    {CommonDataType.Text, "varchar"},
                    {CommonDataType.Time, "time"},
                    {CommonDataType.TimeStamp, "timestamp"},
                    {CommonDataType.TinyBlob, "varbinary"},
                    {CommonDataType.TinyText, "varchar"},
                    {CommonDataType.VarBinary, "varbinary"},
                    {CommonDataType.VarChar, "varchar"},
                    {CommonDataType.Year, "tinyint"}
                };
        }

        /// <summary>
        /// Specific for SqlServer -> MySql
        /// Builds the text needed to add precision to the user-table column definition.
        /// </summary>
        /// <param name="userTableColumn">The user-table column to get the precision of.</param>
        /// <returns>Ex: (19, 4), (11), etc..</returns>
        private static string BuildSqlServerToMySqlColumnPrecision(UserTableColumn userTableColumn)
        {
            if (userTableColumn.IsIdentity || userTableColumn.HasForeignKey)
                return string.Empty;

            switch (userTableColumn.DataType)
            {
                case "decimal":
                case "numeric":
                    if (userTableColumn.Precision > 0 && userTableColumn.Scale > 0)
                        return string.Format("({0},{1})", userTableColumn.Precision, userTableColumn.Scale);
                    break;
                case "float":
                // case "real":
                    if (userTableColumn.Precision > 0)
                        return string.Format("({0})", userTableColumn.Precision);
                    break;
                case "datetimeoffset":
                    if (userTableColumn.Precision > 0)
                        return string.Format("({0})", userTableColumn.Precision);
                    break;
                case "char":
                case "binary":
                    if (userTableColumn.MaxLength > 0)
                        return string.Format("({0})", userTableColumn.MaxLength);
                    break;
                case "nchar":
                    if (userTableColumn.MaxLength > 0)
                        return string.Format("({0})", userTableColumn.MaxLength / 2);
                    break;
                case "varchar":
                case "varbinary":
                    if (userTableColumn.MaxLength == -1)
                        return "(255)"; // TODO: http://stackoverflow.com/questions/332798/equivalent-of-varcharmax-in-mysql
                    if (userTableColumn.MaxLength > 0)
                        return string.Format("({0})", userTableColumn.MaxLength);
                    break;
                case "nvarchar":
                    if (userTableColumn.MaxLength == -1)
                        return "(255)"; // TODO: http://stackoverflow.com/questions/332798/equivalent-of-varcharmax-in-mysql
                    if (userTableColumn.MaxLength > 0)
                        return string.Format("({0})", userTableColumn.MaxLength / 2);
                    break;
            }

            return string.Empty;
        }

        /// <summary>
        /// Creates a clone of the userTableColumn and converts it from a SqlServer context to a
        /// MySql context and then generates the column definition as it would appear
        /// in a CREATE TABLE or ALTER TABLE... SQL statement.
        /// </summary>
        /// <param name="sourceDataContext">Must be of DataContextType.SqlServer.</param>
        /// <param name="targetDataContext">Must be of DataContextType.MySql.</param>
        /// <param name="userTableColumn">The user-table column to create the converted definiton of.</param>
        /// <returns>The SqlServer -> MySql converted column definition.</returns>
        private static string BuildSqlServerToMySqlDefinition(DataContext sourceDataContext, DataContext targetDataContext, UserTableColumn userTableColumn)
        {
            if (sourceDataContext.ContextType != DataContextType.SqlServer)
                throw new Exception(string.Format("Invalid sourceDataContext type of {0} for DataTypes.SqlServerToMySqlDefinition", sourceDataContext.ContextType));

            if (targetDataContext.ContextType != DataContextType.MySql)
                throw new Exception(string.Format("Invalid targetDataContext type of {0} for DataTypes.SqlServerToMySqlDefinition", targetDataContext.ContextType));

            var userTableColumnClone = UserTableColumn.Clone(userTableColumn);
            ConvertDataType(sourceDataContext, targetDataContext, ref userTableColumnClone);

            var builder = new StringBuilder();
            builder.AppendFormat("{0}{1}{2} {3}{4}",
                targetDataContext.IdentifierOpenChar,
                userTableColumnClone.ObjectName,
                targetDataContext.IdentifierCloseChar,
                userTableColumnClone.DataType,
                BuildSqlServerToMySqlColumnPrecision(userTableColumnClone));

            if (!userTableColumnClone.IsUserDefined && userTableColumnClone.Collation.Length > 0)
            {
                if (!string.IsNullOrEmpty(userTableColumnClone.Collation))
                    builder.AppendFormat(" COLLATE {0}", userTableColumnClone.Collation);
            }

            builder.AppendFormat(" {0}", userTableColumnClone.IsNullable ? "NULL" : "NOT NULL");
            if (userTableColumnClone.IsIdentity)
                builder.Append(" AUTO_INCREMENT");

            return builder.ToString();
        }

        /// <summary>
        /// Populates the SqlServerToMySqlLookup member.
        /// </summary>
        private static void BuildSqlServerToMySqlLookup()
        {
            SqlServerToMySqlLookup =
                new Dictionary<CommonDataType, string>
                {  
                    {CommonDataType.Binary, "binary"},
                    {CommonDataType.Bit, "bit"},
                    {CommonDataType.Boolean, "bit"},
                    {CommonDataType.Char, "char"},
                    {CommonDataType.Date, "date"},
                    {CommonDataType.DateTime, "datetime"},
                    {CommonDataType.DateTime2, "datetime"},
                    {CommonDataType.DateTimeOffset, "datetime"},
                    {CommonDataType.Decimal, "decimal"},
                    {CommonDataType.Double, "double"},
                    {CommonDataType.Float, "float"},
                    {CommonDataType.Geography, "blob"},
                    {CommonDataType.Geometry, "blob"},
                    {CommonDataType.HierarchyId, "blob"}, // nvarchar(4000)
                    {CommonDataType.Image, "blob"},
                    {CommonDataType.Integer16, "smallint"},
                    {CommonDataType.Integer32, "int"},
                    {CommonDataType.Integer64, "longint"},
                    {CommonDataType.Integer8, "tinyint"},
                    {CommonDataType.Money, "decimal"}, // DECIMAL(19,4)
                    {CommonDataType.NChar, "char"},
                    {CommonDataType.NText, "text"},
                    {CommonDataType.Numeric, "numeric"},
                    {CommonDataType.NVarChar, "varchar"},
                    {CommonDataType.Real, "real"},
                    {CommonDataType.SmallDateTime, "datetime"},
                    {CommonDataType.SmallMoney, "decimal"},
                    {CommonDataType.SqlVariant, "longblob"},
                    {CommonDataType.Text, "text"},
                    {CommonDataType.Time, "time"},
                    {CommonDataType.TimeStamp, "timestamp"},
                    {CommonDataType.UniqueIdentifier, "binary"}, // 16-byte GUID
                    {CommonDataType.VarBinary, "varbinary"},
                    {CommonDataType.VarChar, "varchar"},
                    {CommonDataType.Xml, "longblob"},
                };
        }

        /// <summary>
        /// Specific for SqlServer -> SqlServer
        /// Builds the text needed to add precision to the user-table column definition.
        /// </summary>
        /// <param name="userTableColumn">The user-table column to get the precision of.</param>
        /// <returns>Ex: (19, 4), (11), etc..</returns>
        private static string BuildSqlServerToSqlServerColumnPrecision(UserTableColumn userTableColumn)
        {
            switch (userTableColumn.DataType)
            {
                case "decimal":
                case "numeric":
                    if (userTableColumn.Precision > 0 && userTableColumn.Scale > 0)
                        return string.Format("({0},{1})", userTableColumn.Precision, userTableColumn.Scale);
                    break;
                case "float":
                // case "real":
                    if (userTableColumn.Precision > 0)
                        return string.Format("({0})", userTableColumn.Precision);
                    break;
                case "datetimeoffset":
                    if (userTableColumn.Precision > 0)
                        return string.Format("({0})", userTableColumn.Precision);
                    break;
                case "char":
                case "binary":
                    if (userTableColumn.MaxLength > 0)
                        return string.Format("({0})", userTableColumn.MaxLength);
                    break;
                case "nchar":
                    if (userTableColumn.MaxLength > 0)
                        return string.Format("({0})", userTableColumn.MaxLength / 2);
                    break;
                case "varchar":
                case "varbinary":
                    if (userTableColumn.MaxLength == -1)
                        return "(MAX)";
                    if (userTableColumn.MaxLength > 0)
                        return string.Format("({0})", userTableColumn.MaxLength);
                    break;
                case "nvarchar":
                    if (userTableColumn.MaxLength == -1)
                        return "(MAX)";
                    if (userTableColumn.MaxLength > 0)
                        return string.Format("({0})", userTableColumn.MaxLength / 2);
                    break;
            }

            return string.Empty;
        }

        /// <summary>
        /// Generates the SqlServer column definition of the user-table column as it would appear
        /// in a CREATE TABLE or ALTER TABLE... SQL statement.
        /// </summary>
        /// <param name="sourceDataContext">Must be of DataContextType.SqlServer.</param>
        /// <param name="targetDataContext">Must be of DataContextType.SqlServer.</param>
        /// <param name="userTableColumn">The user-table column to create the converted definiton of.</param>
        /// <returns>The SqlServer column definition.</returns>
        private static string BuildSqlServerToSqlServerDefinition(DataContext sourceDataContext, DataContext targetDataContext, UserTableColumn userTableColumn)
        {
            if (sourceDataContext.ContextType != DataContextType.SqlServer)
                throw new Exception(string.Format("Invalid sourceDataContext type of {0} for DataTypes.BuildSqlServerToSqlServerDefinition", sourceDataContext.ContextType));

            if (targetDataContext.ContextType != DataContextType.SqlServer)
                throw new Exception(string.Format("Invalid targetDataContext type of {0} for DataTypes.BuildSqlServerToSqlServerDefinition", targetDataContext.ContextType));

            var builder = new StringBuilder();

            if (userTableColumn.IsComputed)
            {
                var computedColumn = userTableColumn.UserTable.ComputedColumns[userTableColumn.Namespace];
                if (computedColumn != null)
                {
                    builder.AppendFormat("{0}{1}{2} AS {3}",
                        targetDataContext.IdentifierOpenChar,
                        computedColumn.ObjectName,
                        targetDataContext.IdentifierCloseChar,
                        computedColumn.Definition);

                    if (computedColumn.IsPersisted)
                        builder.Append(" PERSISTED");

                    
                    builder.Append(computedColumn.IsNullable ? " NULL" : " NOT NULL");
                    return builder.ToString();
                }
            }

            builder.AppendFormat("{0}{1}{2} {3}{4}{5}{6}",
                targetDataContext.IdentifierOpenChar,
                userTableColumn.ObjectName,
                targetDataContext.IdentifierCloseChar,
                targetDataContext.IdentifierOpenChar,
                userTableColumn.DataType,
                targetDataContext.IdentifierCloseChar,
                BuildSqlServerToSqlServerColumnPrecision(userTableColumn));

            if (!userTableColumn.IsUserDefined && userTableColumn.Collation.Length > 0)
            {
                var collation = DataCollations.ConvertCollation(sourceDataContext, targetDataContext, userTableColumn);
                if (!string.IsNullOrEmpty(collation))
                    builder.AppendFormat(" COLLATE {0}", userTableColumn.Collation);
            }

            if (userTableColumn.IsIdentity)
            {
                var identityColumn = userTableColumn.UserTable.IdentityColumns[userTableColumn.Namespace];
                if (identityColumn != null && (DataProperties.ReplicateItemsMarkedNotForReplication || !identityColumn.IsNotForReplication))
                {
                    builder.AppendFormat(" IDENTITY ({0},{1})",
                        identityColumn.SeedValue,
                        identityColumn.IncrementValue);

                    if (identityColumn.IsNotForReplication)
                        builder.Append(" NOT FOR REPLICATION");
                }
            }

            if (userTableColumn.IsRowGuidColumn)
                builder.Append(" ROWGUIDCOL");

            builder.AppendFormat(" {0}", userTableColumn.IsNullable ? "NULL" : "NOT NULL");

            return builder.ToString();
        }

        /// <summary>
        /// Converts the referenced UserTableColumn object from its MySql representation
        /// to its equivalent SqlServer representation.
        /// </summary>
        /// <param name="sourceDataContext">Must be of DataContextType.MySql.</param>
        /// <param name="targetDataContext">Must be of DataContextType.SqlServer.</param>
        /// <param name="userTableColumn">The user-table column to convert.</param>
        private static void ConvertMySqlToSqlServer(DataContext sourceDataContext, DataContext targetDataContext, ref UserTableColumn userTableColumn)
        {
            //if (sourceDataContext.ContextType != DataContextType.MySql)
            //    throw new Exception(string.Format("Invalid sourceDataContext type of {0} for DataTypes.ConvertMySqlToSqlServer", sourceDataContext.ContextType));

            //if (targetDataContext.ContextType != DataContextType.SqlServer)
            //    throw new Exception(string.Format("Invalid targetDataContext type of {0} for DataTypes.ConvertMySqlToSqlServer", targetDataContext.ContextType));

            //string dataType;
            //MySqlToSqlServerLookup.TryGetValue(userTableColumn.CommonDataType, out dataType);

            //userTableColumn.DataType = dataType ?? userTableColumn.DataType;
            //var originalCommonDataType = userTableColumn.CommonDataType;
            //userTableColumn.CommonDataType = GetCommonDataType(dataType ?? userTableColumn.DataType);

            //switch (originalCommonDataType)
            //{
            //    case CommonDataType.Blob:
            //    case CommonDataType.LongBlob:
            //    case CommonDataType.LongText:
            //    case CommonDataType.MediumBlob:
            //    case CommonDataType.MediumText:
            //    case CommonDataType.Text:
            //        userTableColumn.MaxLength = -1;
            //        userTableColumn.Precision = 0;
            //        userTableColumn.Scale = 0;
            //        break;
            //    case CommonDataType.TinyBlob:
            //    case CommonDataType.TinyText:
            //        userTableColumn.MaxLength = 255;
            //        userTableColumn.Precision = 0;
            //        userTableColumn.Scale = 0;
            //        break;
            //    case CommonDataType.Enum:
            //        userTableColumn.MaxLength = 2;
            //        userTableColumn.Precision = 0;
            //        userTableColumn.Scale = 0;
            //        break;
            //    case CommonDataType.Set:
            //        userTableColumn.MaxLength = 8;
            //        userTableColumn.Precision = 0;
            //        userTableColumn.Scale = 0;
            //        break;
            //}

            //userTableColumn.Collation = DataCollations.ConvertCollation(sourceDataContext, targetDataContext, userTableColumn);
        }

        /// <summary>
        /// Converts the referenced UserTableColumn object from its SqlServer representation
        /// to its equivalent MySql representation.
        /// </summary>
        /// <param name="sourceDataContext">Must be of DataContextType.SqlServer.</param>
        /// <param name="targetDataContext">Must be of DataContextType.MySql.</param>
        /// <param name="userTableColumn">The user-table column to convert.</param>
        private static void ConvertSqlServerToMySql(DataContext sourceDataContext, DataContext targetDataContext, ref UserTableColumn userTableColumn)
        {
            //if (sourceDataContext.ContextType != DataContextType.SqlServer)
            //    throw new Exception(string.Format("Invalid sourceDataContext type of {0} for DataTypes.ConvertSqlServerToMySql", sourceDataContext.ContextType));

            //if (targetDataContext.ContextType != DataContextType.MySql)
            //    throw new Exception(string.Format("Invalid targetDataContext type of {0} for DataTypes.ConvertSqlServerToMySql", targetDataContext.ContextType));

            //string dataType;
            //SqlServerToMySqlLookup.TryGetValue(userTableColumn.CommonDataType, out dataType);

            //userTableColumn.DataType = dataType ?? userTableColumn.DataType;
            //var originalCommonDataType = userTableColumn.CommonDataType;
            //userTableColumn.CommonDataType = GetCommonDataType(dataType ?? userTableColumn.DataType);

            //if (userTableColumn.IsIdentity || userTableColumn.HasForeignKey)
            //{
            //    if (userTableColumn.CommonDataType != CommonDataType.Integer32 || userTableColumn.CommonDataType != CommonDataType.Integer64)
            //    {
            //        userTableColumn.DataType = "int";
            //        userTableColumn.CommonDataType = CommonDataType.Integer32;
            //        userTableColumn.Collation = string.Empty;
            //        userTableColumn.MaxLength = 0;
            //        userTableColumn.Precision = 10;
            //        userTableColumn.Scale = 0;
            //    }
            //}

            //switch (originalCommonDataType)
            //{
            //    case CommonDataType.Geography:
            //    case CommonDataType.Geometry:
            //    case CommonDataType.HierarchyId:
            //        userTableColumn.Collation = string.Empty;
            //        userTableColumn.MaxLength = 65535;
            //        userTableColumn.Precision = 0;
            //        userTableColumn.Scale = 0;
            //        break;
            //    case CommonDataType.Image:
            //    case CommonDataType.Binary:
            //    case CommonDataType.VarBinary:
            //    case CommonDataType.SqlVariant:
            //    case CommonDataType.Xml:
            //        if (userTableColumn.MaxLength == -1 || userTableColumn.MaxLength > 255)
            //        {
            //            userTableColumn.DataType = "longblob";
            //            userTableColumn.CommonDataType = CommonDataType.LongBlob;
            //            userTableColumn.Collation = string.Empty;
            //            userTableColumn.MaxLength = -1;
            //            userTableColumn.Precision = 0;
            //            userTableColumn.Scale = 0;
            //        }
            //        break;
            //    case CommonDataType.Char:
            //    case CommonDataType.VarChar:
            //    case CommonDataType.NChar:
            //    case CommonDataType.NText:
            //    case CommonDataType.NVarChar:
            //        if (userTableColumn.MaxLength == -1 || userTableColumn.MaxLength > 255)
            //        {
            //            userTableColumn.DataType = "longtext";
            //            userTableColumn.CommonDataType = CommonDataType.LongText;
            //            userTableColumn.MaxLength = -1;
            //            userTableColumn.Precision = 0;
            //            userTableColumn.Scale = 0;
            //        }
            //        break;
            //    case CommonDataType.Money:
            //    case CommonDataType.SmallMoney:
            //        userTableColumn.Collation = string.Empty;
            //        userTableColumn.MaxLength = 0;
            //        userTableColumn.Precision = 19;
            //        userTableColumn.Scale = 4;
            //        break;
            //    case CommonDataType.UniqueIdentifier:
            //        userTableColumn.Collation = string.Empty;
            //        userTableColumn.MaxLength = 16;
            //        userTableColumn.Precision = 0;
            //        userTableColumn.Scale = 0;
            //        break;
            //}

            //userTableColumn.Collation = DataCollations.ConvertCollation(sourceDataContext, targetDataContext, userTableColumn);
        }

        /// <summary>
        /// This method is specific for MySql -> To -> MySql only.
        /// Allows us to know whether or not we will need to drop the user-table column and then re-add it with the
        /// new data-type or column definition rather than being able to perform a table alter. Changing a
        /// user-table column's data-type from datetime to timestamp is a prime example of 
        /// </summary>
        /// <param name="sourceUserTableColumn">The source UserTableColumn.</param>
        /// <param name="targetUserTableColumn">the target UserTableColumn.</param>
        /// <returns>
        ///     true - the datatype can be altered using ALTER TABLE Column Definition... syntax.
        ///     false - the UserTableColumn must be dropped and re-created for the datatype to be changed on the table.
        /// </returns>
        private static bool IsCompatibleMySqlToMySqlAlter(UserTableColumn sourceUserTableColumn, UserTableColumn targetUserTableColumn)
        {
            if (StringComparer.OrdinalIgnoreCase.Compare(sourceUserTableColumn.DataType, "timestamp") == 0
                || StringComparer.OrdinalIgnoreCase.Compare(targetUserTableColumn.DataType, "timestamp") == 0
                || sourceUserTableColumn.IsIdentity
                || targetUserTableColumn.IsIdentity)
                return false;

            if (StringComparer.OrdinalIgnoreCase.Compare(sourceUserTableColumn.DataType, targetUserTableColumn.DataType) != 0)
            {
                if (StringComparer.OrdinalIgnoreCase.Compare(sourceUserTableColumn.DataType, "text") == 0
                    || StringComparer.OrdinalIgnoreCase.Compare(sourceUserTableColumn.DataType, "ntext") == 0)
                {
                    if (!((StringComparer.OrdinalIgnoreCase.Compare(targetUserTableColumn.DataType, "varchar") == 0
                            && targetUserTableColumn.MaxLength == -1)
                        || (StringComparer.OrdinalIgnoreCase.Compare(targetUserTableColumn.DataType, "nvarchar") == 0
                            && targetUserTableColumn.MaxLength == -1)))
                        return false;
                }
            }

            return true;
        }

        /// <summary>
        /// This method is specific for MySql -> To -> SqlServer only.
        /// Allows us to know whether or not we will need to drop the user-table column and then re-add it with the
        /// new data-type or column definition rather than being able to perform a table alter. Changing a
        /// user-table column's data-type from datetime to timestamp is a prime example of 
        /// </summary>
        /// <param name="sourceUserTableColumn">The source UserTableColumn.</param>
        /// <param name="targetUserTableColumn">the target UserTableColumn.</param>
        /// <returns>
        ///     true - the datatype can be altered using ALTER TABLE Column Definition... syntax.
        ///     false - the UserTableColumn must be dropped and re-created for the datatype to be changed on the table.
        /// </returns>
        private static bool IsCompatibleMySqlToSqlServerAlter(UserTableColumn sourceUserTableColumn, UserTableColumn targetUserTableColumn)
        {
            if (StringComparer.OrdinalIgnoreCase.Compare(sourceUserTableColumn.DataType, "timestamp") == 0
                || StringComparer.OrdinalIgnoreCase.Compare(targetUserTableColumn.DataType, "timestamp") == 0
                || sourceUserTableColumn.IsRowGuidColumn
                || targetUserTableColumn.IsRowGuidColumn
                || sourceUserTableColumn.IsComputed
                || targetUserTableColumn.IsComputed
                || sourceUserTableColumn.IsIdentity
                || targetUserTableColumn.IsIdentity)
                return false;

            if (StringComparer.OrdinalIgnoreCase.Compare(sourceUserTableColumn.DataType, targetUserTableColumn.DataType) != 0)
            {
                if (StringComparer.OrdinalIgnoreCase.Compare(sourceUserTableColumn.DataType, "text") == 0
                    || StringComparer.OrdinalIgnoreCase.Compare(sourceUserTableColumn.DataType, "ntext") == 0)
                {
                    if (!((StringComparer.OrdinalIgnoreCase.Compare(targetUserTableColumn.DataType, "varchar") == 0
                            && targetUserTableColumn.MaxLength == -1)
                        || (StringComparer.OrdinalIgnoreCase.Compare(targetUserTableColumn.DataType, "nvarchar") == 0
                            && targetUserTableColumn.MaxLength == -1)
                        || StringComparer.OrdinalIgnoreCase.Compare(targetUserTableColumn.DataType, "xml") == 0))
                        return false;
                }

                if (StringComparer.OrdinalIgnoreCase.Compare(sourceUserTableColumn.DataType, "image") == 0)
                {
                    if (!(StringComparer.OrdinalIgnoreCase.Compare(targetUserTableColumn.DataType, "varbinary") == 0
                        && targetUserTableColumn.MaxLength == -1))
                        return false;
                }
            }

            return true;
        }

        /// <summary>
        /// This method is specific for SqlServer -> To -> MySql only.
        /// Allows us to know whether or not we will need to drop the user-table column and then re-add it with the
        /// new data-type or column definition rather than being able to perform a table alter. Changing a
        /// user-table column's data-type from datetime to timestamp is a prime example of 
        /// </summary>
        /// <param name="sourceUserTableColumn">The source UserTableColumn.</param>
        /// <param name="targetUserTableColumn">the target UserTableColumn.</param>
        /// <returns>
        ///     true - the datatype can be altered using ALTER TABLE Column Definition... syntax.
        ///     false - the UserTableColumn must be dropped and re-created for the datatype to be changed on the table.
        /// </returns>
        private static bool IsCompatibleSqlServerToMySqlAlter(UserTableColumn sourceUserTableColumn, UserTableColumn targetUserTableColumn)
        {
            if (StringComparer.OrdinalIgnoreCase.Compare(sourceUserTableColumn.DataType, "timestamp") == 0
                || StringComparer.OrdinalIgnoreCase.Compare(targetUserTableColumn.DataType, "timestamp") == 0
                || sourceUserTableColumn.IsRowGuidColumn
                || targetUserTableColumn.IsRowGuidColumn
                || sourceUserTableColumn.IsComputed
                || targetUserTableColumn.IsComputed
                || sourceUserTableColumn.IsIdentity
                || targetUserTableColumn.IsIdentity)
                return false;

            if (StringComparer.OrdinalIgnoreCase.Compare(sourceUserTableColumn.DataType, targetUserTableColumn.DataType) != 0)
            {
                if (StringComparer.OrdinalIgnoreCase.Compare(sourceUserTableColumn.DataType, "text") == 0
                    || StringComparer.OrdinalIgnoreCase.Compare(sourceUserTableColumn.DataType, "ntext") == 0)
                {
                    if (!((StringComparer.OrdinalIgnoreCase.Compare(targetUserTableColumn.DataType, "varchar") == 0
                            && targetUserTableColumn.MaxLength == -1)
                        || (StringComparer.OrdinalIgnoreCase.Compare(targetUserTableColumn.DataType, "nvarchar") == 0
                            && targetUserTableColumn.MaxLength == -1)
                        || StringComparer.OrdinalIgnoreCase.Compare(targetUserTableColumn.DataType, "xml") == 0))
                        return false;
                }

                if (StringComparer.OrdinalIgnoreCase.Compare(sourceUserTableColumn.DataType, "image") == 0)
                {
                    if (!(StringComparer.OrdinalIgnoreCase.Compare(targetUserTableColumn.DataType, "varbinary") == 0
                        && targetUserTableColumn.MaxLength == -1))
                        return false;
                }
            }

            return true;
        }

        /// <summary>
        /// This method is specific for SqlServer -> To -> SqlServer only.
        /// Allows us to know whether or not we will need to drop the user-table column and then re-add it with the
        /// new data-type or column definition rather than being able to perform a table alter. Changing a
        /// user-table column's data-type from datetime to timestamp is a prime example of 
        /// </summary>
        /// <param name="sourceUserTableColumn">The source UserTableColumn.</param>
        /// <param name="targetUserTableColumn">the target UserTableColumn.</param>
        /// <returns>
        ///     true - the datatype can be altered using ALTER TABLE Column Definition... syntax.
        ///     false - the UserTableColumn must be dropped and re-created for the datatype to be changed on the table.
        /// </returns>
        private static bool IsCompatibleSqlServerToSqlServerAlter(UserTableColumn sourceUserTableColumn, UserTableColumn targetUserTableColumn)
        {
            if (StringComparer.OrdinalIgnoreCase.Compare(sourceUserTableColumn.DataType, "timestamp") == 0
                || StringComparer.OrdinalIgnoreCase.Compare(targetUserTableColumn.DataType, "timestamp") == 0
                || sourceUserTableColumn.IsRowGuidColumn
                || targetUserTableColumn.IsRowGuidColumn
                || sourceUserTableColumn.IsComputed
                || targetUserTableColumn.IsComputed
                || sourceUserTableColumn.IsIdentity
                || targetUserTableColumn.IsIdentity)
                return false;

            if (StringComparer.OrdinalIgnoreCase.Compare(sourceUserTableColumn.DataType, targetUserTableColumn.DataType) != 0)
            {
                if (StringComparer.OrdinalIgnoreCase.Compare(sourceUserTableColumn.DataType, "text") == 0
                    || StringComparer.OrdinalIgnoreCase.Compare(sourceUserTableColumn.DataType, "ntext") == 0)
                {
                    if (!((StringComparer.OrdinalIgnoreCase.Compare(targetUserTableColumn.DataType, "varchar") == 0
                            && targetUserTableColumn.MaxLength == -1)
                        || (StringComparer.OrdinalIgnoreCase.Compare(targetUserTableColumn.DataType, "nvarchar") == 0
                            && targetUserTableColumn.MaxLength == -1)
                        || StringComparer.OrdinalIgnoreCase.Compare(targetUserTableColumn.DataType, "xml") == 0))
                        return false;
                }

                if (StringComparer.OrdinalIgnoreCase.Compare(sourceUserTableColumn.DataType, "image") == 0)
                {
                    if (!(StringComparer.OrdinalIgnoreCase.Compare(targetUserTableColumn.DataType, "varbinary") == 0
                        && targetUserTableColumn.MaxLength == -1))
                        return false;
                }
            }

            return true;
        } 
    }
}
