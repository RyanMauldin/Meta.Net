using System;
using System.Collections.Generic;
using Meta.Net.Data.Enum;
using Meta.Net.Data.Objects;

namespace Meta.Net.Data.Converters
{
    public static class DataCharacterSets
    {
		#region Properties (4) 

        public static HashSet<string> MySqlCharacterSets { get; set; }

        public static Dictionary<string, string> MySqlToSqlServerLookup { get; set; }

        public static HashSet<string> SqlServerCharacterSets { get; set; }

        public static Dictionary<string, string> SqlServerToMySqlLookup { get; set; }

		#endregion Properties 

		#region Constructors (1) 

        /// <summary>
        /// Static Constructor for DataCollations.
        /// Initializes LookupTables and prepares class for general use.
        /// </summary>
        static DataCharacterSets()
        {
            MySqlCharacterSets = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            SqlServerCharacterSets = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            MySqlToSqlServerLookup = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            SqlServerToMySqlLookup = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            MySqlCharacterSets.UnionWith(System.Enum.GetNames(typeof(MySqlCharacterSetType)));
            SqlServerCharacterSets.UnionWith(System.Enum.GetNames(typeof(SqlServerCharacterSetType)));
            BuildMySqlToSqlServerLookup();
            BuildSqlServerToMySqlLookup();
        }

		#endregion Constructors 

		#region Methods (3) 

		#region Public Methods (1) 

        /// <summary>
        /// Compares the Character Set between source and target user-table columns and even
        /// matches against differing data contexts.
        /// </summary>
        /// <param name="sourceDataContext">The source DataContext.</param>
        /// <param name="targetDataContext">The target DataContext.</param>
        /// <param name="sourceUserTableColumn">The source user-table column to get a character set from.</param>
        /// <param name="targetUserTableColumn">The target user-table column to get a character set from.</param>
        /// <returns>
        ///     True - The character sets matched between differing datasources.
        ///     False - The character sets could not be matched up.
        /// </returns>
        public static bool CompareCharacterSet(DataContext sourceDataContext, DataContext targetDataContext, UserTableColumn sourceUserTableColumn, UserTableColumn targetUserTableColumn)
        {
            // TODO: adjust to use sourceUserTableColumn.CharacterSet once added.
            if (string.IsNullOrEmpty(sourceUserTableColumn.Collation) && string.IsNullOrEmpty(targetUserTableColumn.Collation))
                return true;

            if (string.IsNullOrEmpty(sourceUserTableColumn.Collation))
                return false;

            if (string.IsNullOrEmpty(targetUserTableColumn.Collation))
                return false;

            if (sourceDataContext.ContextType == targetDataContext.ContextType)
                if (StringComparer.OrdinalIgnoreCase.Compare(
                    sourceUserTableColumn.Collation, targetUserTableColumn.Collation) != 0)
                    return false;

            switch (sourceDataContext.ContextType)
            {
                case DataContextType.MySql:
                    if (!MySqlCharacterSets.Contains(sourceUserTableColumn.Collation))
                        return true;

                    switch (targetDataContext.ContextType)
                    {
                        case DataContextType.SqlServer:
                            string collation;
                            return !MySqlToSqlServerLookup.TryGetValue(sourceUserTableColumn.Collation, out collation)
                                ? true
                                : StringComparer.OrdinalIgnoreCase.Compare(
                                    collation, targetUserTableColumn.Collation) == 0;
                        default:
                            return true;
                    }
                case DataContextType.SqlServer:
                    if (!SqlServerCharacterSets.Contains(sourceUserTableColumn.Collation))
                        return true;

                    switch (targetDataContext.ContextType)
                    {
                        case DataContextType.MySql:
                            string collation;
                            return !SqlServerToMySqlLookup.TryGetValue(sourceUserTableColumn.Collation, out collation)
                                ? true
                                : StringComparer.OrdinalIgnoreCase.Compare(
                                    collation, targetUserTableColumn.Collation) == 0;
                        default:
                            return true;
                    }
                default:
                    return true;
            }
        }

        /// <summary>
        /// Matches Character Set against differing data contexts and returns a string containing
        /// the collation for the target data context or string.Empty if it was not found.
        /// </summary>
        /// <param name="sourceDataContext">The source DataContext.</param>
        /// <param name="targetDataContext">The target Datacontext.</param>
        /// <param name="userTableColumn">The user-table column to pull Character Set from.</param>
        /// <returns>
        ///     The target Character Set to use for the given user-table column or string.Empty if one was not found.
        /// </returns>
        public static string ConvertCharacterSet(DataContext sourceDataContext, DataContext targetDataContext, UserTableColumn userTableColumn)
        {
            // TODO: adjust to use sourceUserTableColumn.CharacterSet once added.
            if (string.IsNullOrEmpty(userTableColumn.Collation))
                return string.Empty;

            if (sourceDataContext.ContextType == targetDataContext.ContextType)
                return userTableColumn.Collation;

            switch (sourceDataContext.ContextType)
            {
                case DataContextType.MySql:
                    if (!MySqlCharacterSets.Contains(userTableColumn.Collation))
                        return string.Empty;

                    switch (targetDataContext.ContextType)
                    {
                        case DataContextType.SqlServer:
                            string collation;
                            return !MySqlToSqlServerLookup.TryGetValue(userTableColumn.Collation, out collation)
                                ? string.Empty
                                : collation;
                        default:
                            return string.Empty;
                    }
                case DataContextType.SqlServer:
                    if (!SqlServerCharacterSets.Contains(userTableColumn.Collation))
                        return string.Empty;

                    switch (targetDataContext.ContextType)
                    {
                        case DataContextType.MySql:
                            string collation;
                            return !SqlServerToMySqlLookup.TryGetValue(userTableColumn.Collation, out collation)
                                ? string.Empty
                                : collation;
                        default:
                            return string.Empty;
                    }
                default:
                    return string.Empty;
            }
        }

		#endregion Public Methods 
		#region Private Methods (2) 

        /// <summary>
        /// Initializes MySqlToSqlServerLookup
        /// </summary>
        private static void BuildMySqlToSqlServerLookup()
        {
            // armscii8, ARMSCII-8 Armenian, armscii8_general_ci, 1
            // Latin1-General, case-insensitive, accent-sensitive, kanatype-insensitive, width-insensitive
            MySqlToSqlServerLookup.Add(System.Enum.GetName(typeof(MySqlCharacterSetType), MySqlCharacterSetType.armscii8),
                System.Enum.GetName(typeof(SqlServerCharacterSetType), SqlServerCharacterSetType.Latin1_General_CI_AS));

            // ascii, US ASCII, ascii_general_ci, 1
            // Latin1-General, case-insensitive, accent-sensitive, kanatype-insensitive, width-insensitive
            MySqlToSqlServerLookup.Add(System.Enum.GetName(typeof(MySqlCharacterSetType), MySqlCharacterSetType.ascii),
                System.Enum.GetName(typeof(SqlServerCharacterSetType), SqlServerCharacterSetType.Latin1_General_CI_AS));

            // big5, Big5 Traditional Chinese, big5_chinese_ci, 2
            // Chinese-Taiwan-Stroke, case-insensitive, accent-sensitive, kanatype-insensitive, width-insensitive
            MySqlToSqlServerLookup.Add(System.Enum.GetName(typeof(MySqlCharacterSetType), MySqlCharacterSetType.big5),
                System.Enum.GetName(typeof(SqlServerCharacterSetType), SqlServerCharacterSetType.Chinese_Taiwan_Stroke_CI_AS));

            // binary, Binary pseudo charset, binary, 1
            // Latin1-General, binary sort
            MySqlToSqlServerLookup.Add(System.Enum.GetName(typeof(MySqlCharacterSetType), MySqlCharacterSetType.binary),
                System.Enum.GetName(typeof(SqlServerCharacterSetType), SqlServerCharacterSetType.Latin1_General_BIN));

            // cp1250, Windows Central European, cp1250_general_ci, 1
            // Latin1-General, case-insensitive, accent-sensitive, kanatype-insensitive, width-insensitive for Unicode Data,
            // SQL Server Sort Order 82 on Code Page 1250 for non-Unicode Data
            MySqlToSqlServerLookup.Add(System.Enum.GetName(typeof(MySqlCharacterSetType), MySqlCharacterSetType.cp1250),
                System.Enum.GetName(typeof(SqlServerCharacterSetType), SqlServerCharacterSetType.SQL_Latin1_General_CP1250_CI_AS));

            // cp1251, Windows Cyrillic, cp1251_general_ci, 1
            // Latin1-General, case-insensitive, accent-sensitive, kanatype-insensitive, width-insensitive for Unicode Data,
            // SQL Server Sort Order 106 on Code Page 1251 for non-Unicode Data
            MySqlToSqlServerLookup.Add(System.Enum.GetName(typeof(MySqlCharacterSetType), MySqlCharacterSetType.cp1251),
                System.Enum.GetName(typeof(SqlServerCharacterSetType), SqlServerCharacterSetType.SQL_Latin1_General_CP1251_CI_AS));

            // cp1256, Windows Arabic, cp1256_general_ci, 1
            // Latin1-General, case-insensitive, accent-sensitive, kanatype-insensitive, width-insensitive for Unicode Data,
            // SQL Server Sort Order 146 on Code Page 1256 for non-Unicode Data
            MySqlToSqlServerLookup.Add(System.Enum.GetName(typeof(MySqlCharacterSetType), MySqlCharacterSetType.cp1256),
                System.Enum.GetName(typeof(SqlServerCharacterSetType), SqlServerCharacterSetType.SQL_Latin1_General_CP1256_CI_AS));

            // cp1257, Windows Baltic, cp1257_general_ci, 1
            // Latin1-General, case-insensitive, accent-sensitive, kanatype-insensitive, width-insensitive for Unicode Data,
            // SQL Server Sort Order 154 on Code Page 1257 for non-Unicode Data
            MySqlToSqlServerLookup.Add(System.Enum.GetName(typeof(MySqlCharacterSetType), MySqlCharacterSetType.cp1257),
                System.Enum.GetName(typeof(SqlServerCharacterSetType), SqlServerCharacterSetType.SQL_Latin1_General_CP1257_CI_AS));

            // cp850, DOS West European, cp850_general_ci, 1
            // Latin1-General, case-insensitive, accent-sensitive, kanatype-insensitive, width-insensitive for Unicode Data,
            // SQL Server Sort Order 42 on Code Page 850 for non-Unicode Data
            MySqlToSqlServerLookup.Add(System.Enum.GetName(typeof(MySqlCharacterSetType), MySqlCharacterSetType.cp850),
                System.Enum.GetName(typeof(SqlServerCharacterSetType), SqlServerCharacterSetType.SQL_Latin1_General_CP850_CI_AS));

            // cp852, DOS Central European, cp852_general_ci, 1
            // Latin1-General, case-insensitive, accent-sensitive, kanatype-insensitive, width-insensitive for Unicode Data,
            // SQL Server Sort Order 52 on Code Page 1252 for non-Unicode Data
            MySqlToSqlServerLookup.Add(System.Enum.GetName(typeof(MySqlCharacterSetType), MySqlCharacterSetType.cp852),
                System.Enum.GetName(typeof(SqlServerCharacterSetType), SqlServerCharacterSetType.SQL_Latin1_General_CP1_CI_AS));

            // cp866, DOS Russian, cp866_general_ci, 1
            // Cyrillic-General, case-insensitive, accent-sensitive, kanatype-insensitive, width-insensitive
            MySqlToSqlServerLookup.Add(System.Enum.GetName(typeof(MySqlCharacterSetType), MySqlCharacterSetType.cp866),
                System.Enum.GetName(typeof(SqlServerCharacterSetType), SqlServerCharacterSetType.Cyrillic_General_CI_AI));

            // cp932, SJIS for Windows Japanese, cp932_japanese_ci, 2
            // Japanese-Unicode, case-insensitive, accent-sensitive, kanatype-insensitive, width-insensitive
            MySqlToSqlServerLookup.Add(System.Enum.GetName(typeof(MySqlCharacterSetType), MySqlCharacterSetType.cp932),
                System.Enum.GetName(typeof(SqlServerCharacterSetType), SqlServerCharacterSetType.Japanese_Unicode_CI_AS));

            // dec8, DEC West European, dec8_swedish_ci, 1
            // Latin1-General, case-insensitive, accent-sensitive, kanatype-insensitive, width-insensitive for Unicode Data,
            // SQL Server Sort Order 52 on Code Page 1252 for non-Unicode Data
            MySqlToSqlServerLookup.Add(System.Enum.GetName(typeof(MySqlCharacterSetType), MySqlCharacterSetType.dec8),
                System.Enum.GetName(typeof(SqlServerCharacterSetType), SqlServerCharacterSetType.SQL_Latin1_General_CP1_CI_AS));

            // eucjpms, UJIS for Windows Japanese, eucjpms_japanese_ci, 3
            // Japanese-Unicode, case-insensitive, accent-sensitive, kanatype-insensitive, width-insensitive
            MySqlToSqlServerLookup.Add(System.Enum.GetName(typeof(MySqlCharacterSetType), MySqlCharacterSetType.eucjpms),
                System.Enum.GetName(typeof(SqlServerCharacterSetType), SqlServerCharacterSetType.Japanese_Unicode_CI_AS));

            // euckr, EUC-KR Korean, euckr_korean_ci, 2
            // Korean-Wansung, case-insensitive, accent-sensitive, kanatype-insensitive, width-insensitive
            MySqlToSqlServerLookup.Add(System.Enum.GetName(typeof(MySqlCharacterSetType), MySqlCharacterSetType.euckr),
                System.Enum.GetName(typeof(SqlServerCharacterSetType), SqlServerCharacterSetType.Korean_Wansung_CI_AS));

            // gb2312, GB2312 Simplified Chinese, gb2312_chinese_ci, 2
            // Chinese-Taiwan-Stroke, case-insensitive, accent-sensitive, kanatype-insensitive, width-insensitive
            MySqlToSqlServerLookup.Add(System.Enum.GetName(typeof(MySqlCharacterSetType), MySqlCharacterSetType.gb2312),
                System.Enum.GetName(typeof(SqlServerCharacterSetType), SqlServerCharacterSetType.Chinese_Taiwan_Stroke_CI_AS));

            // gbk, GBK Simplified Chinese, gbk_chinese_ci, 2
            // Chinese-Taiwan-Stroke, case-insensitive, accent-sensitive, kanatype-insensitive, width-insensitive
            MySqlToSqlServerLookup.Add(System.Enum.GetName(typeof(MySqlCharacterSetType), MySqlCharacterSetType.gbk),
                System.Enum.GetName(typeof(SqlServerCharacterSetType), SqlServerCharacterSetType.Chinese_Taiwan_Stroke_CI_AS));

            // geostd8, GEOSTD8 Georgian, geostd8_general_ci, 1
            // Latin1-General, case-insensitive, accent-sensitive, kanatype-insensitive, width-insensitive for Unicode Data,
            // SQL Server Sort Order 52 on Code Page 1252 for non-Unicode Data
            MySqlToSqlServerLookup.Add(System.Enum.GetName(typeof(MySqlCharacterSetType), MySqlCharacterSetType.geostd8),
                System.Enum.GetName(typeof(SqlServerCharacterSetType), SqlServerCharacterSetType.SQL_Latin1_General_CP1_CI_AS));

            // greek, ISO 8859-7 Greek, greek_general_ci, 1
            // Greek, case-insensitive, accent-sensitive, kanatype-insensitive, width-insensitive
            MySqlToSqlServerLookup.Add(System.Enum.GetName(typeof(MySqlCharacterSetType), MySqlCharacterSetType.greek),
                System.Enum.GetName(typeof(SqlServerCharacterSetType), SqlServerCharacterSetType.Greek_CI_AS));

            // hebrew, ISO 8859-8 Hebrew, hebrew_general_ci, 1
            // Hebrew, case-insensitive, accent-sensitive, kanatype-insensitive, width-insensitive
            MySqlToSqlServerLookup.Add(System.Enum.GetName(typeof(MySqlCharacterSetType), MySqlCharacterSetType.hebrew),
                System.Enum.GetName(typeof(SqlServerCharacterSetType), SqlServerCharacterSetType.Hebrew_CI_AS));

            // hp8, HP West European, hp8_english_ci, 1
            // Latin1-General, case-insensitive, accent-sensitive, kanatype-insensitive, width-insensitive for Unicode Data,
            // SQL Server Sort Order 52 on Code Page 1252 for non-Unicode Data
            MySqlToSqlServerLookup.Add(System.Enum.GetName(typeof(MySqlCharacterSetType), MySqlCharacterSetType.hp8),
                System.Enum.GetName(typeof(SqlServerCharacterSetType), SqlServerCharacterSetType.SQL_Latin1_General_CP1_CI_AS));

            // keybcs2, DOS Kamenicky Czech-Slovak, keybcs2_general_ci, 1
            // Czech, case-insensitive, accent-sensitive, kanatype-insensitive, width-insensitive
            MySqlToSqlServerLookup.Add(System.Enum.GetName(typeof(MySqlCharacterSetType), MySqlCharacterSetType.keybcs2),
                System.Enum.GetName(typeof(SqlServerCharacterSetType), SqlServerCharacterSetType.Czech_CI_AS));

            // koi8r, KOI8-R Relcom Russian, koi8r_general_ci, 1
            // Cyrillic-General, case-insensitive, accent-sensitive, kanatype-insensitive, width-insensitive
            MySqlToSqlServerLookup.Add(System.Enum.GetName(typeof(MySqlCharacterSetType), MySqlCharacterSetType.koi8r),
                System.Enum.GetName(typeof(SqlServerCharacterSetType), SqlServerCharacterSetType.Cyrillic_General_CI_AI));

            // koi8u, KOI8-U Ukrainian, koi8u_general_ci, 1
            // Ukrainian, case-insensitive, accent-sensitive, kanatype-insensitive, width-insensitive
            MySqlToSqlServerLookup.Add(System.Enum.GetName(typeof(MySqlCharacterSetType), MySqlCharacterSetType.koi8u),
                System.Enum.GetName(typeof(SqlServerCharacterSetType), SqlServerCharacterSetType.Ukrainian_CI_AS));

            // latin1, cp1252 West European, latin1_swedish_ci, 1
            // Finnish-Swedish, case-insensitive, accent-sensitive, kanatype-insensitive, width-insensitive for Unicode Data,
            // SQL Server Sort Order 185 on Code Page 1252 for non-Unicode Data
            MySqlToSqlServerLookup.Add(System.Enum.GetName(typeof(MySqlCharacterSetType), MySqlCharacterSetType.latin1),
                System.Enum.GetName(typeof(SqlServerCharacterSetType), SqlServerCharacterSetType.SQL_SwedishStd_Pref_CP1_CI_AS));

            // latin2, ISO 8859-2 Central European, latin2_general_ci, 1
            // Latin1-General, case-insensitive, accent-sensitive, kanatype-insensitive, width-insensitive for Unicode Data,
            // SQL Server Sort Order 52 on Code Page 1252 for non-Unicode Data
            MySqlToSqlServerLookup.Add(System.Enum.GetName(typeof(MySqlCharacterSetType), MySqlCharacterSetType.latin2),
                System.Enum.GetName(typeof(SqlServerCharacterSetType), SqlServerCharacterSetType.SQL_Latin1_General_CP1_CI_AS));

            // latin5, ISO 8859-9 Turkish, latin5_turkish_ci, 1
            // Turkish, case-insensitive, accent-sensitive, kanatype-insensitive, width-insensitive for Unicode Data,
            // SQL Server Sort Order 130 on Code Page 1254 for non-Unicode Data
            MySqlToSqlServerLookup.Add(System.Enum.GetName(typeof(MySqlCharacterSetType), MySqlCharacterSetType.latin5),
                System.Enum.GetName(typeof(SqlServerCharacterSetType), SqlServerCharacterSetType.SQL_Latin1_General_CP1254_CI_AS));

            // latin7, ISO 8859-13 Baltic, latin7_general_ci, 1
            // Latin1-General, case-insensitive, accent-sensitive, kanatype-insensitive, width-insensitive for Unicode Data,
            // SQL Server Sort Order 52 on Code Page 1252 for non-Unicode Data
            MySqlToSqlServerLookup.Add(System.Enum.GetName(typeof(MySqlCharacterSetType), MySqlCharacterSetType.latin7),
                System.Enum.GetName(typeof(SqlServerCharacterSetType), SqlServerCharacterSetType.SQL_Latin1_General_CP1_CI_AS));

            // macce, Mac Central European, macce_general_ci, 1
            // Latin1-General, case-insensitive, accent-sensitive, kanatype-insensitive, width-insensitive for Unicode Data,
            // SQL Server Sort Order 52 on Code Page 1252 for non-Unicode Data
            MySqlToSqlServerLookup.Add(System.Enum.GetName(typeof(MySqlCharacterSetType), MySqlCharacterSetType.macce),
                System.Enum.GetName(typeof(SqlServerCharacterSetType), SqlServerCharacterSetType.SQL_Latin1_General_CP1_CI_AS));

            // macroman, Mac West European, macroman_general_ci, 1
            // Latin1-General, case-insensitive, accent-sensitive, kanatype-insensitive, width-insensitive for Unicode Data,
            // SQL Server Sort Order 52 on Code Page 1252 for non-Unicode Data
            MySqlToSqlServerLookup.Add(System.Enum.GetName(typeof(MySqlCharacterSetType), MySqlCharacterSetType.macroman),
                System.Enum.GetName(typeof(SqlServerCharacterSetType), SqlServerCharacterSetType.SQL_Latin1_General_CP1_CI_AS));

            // sjis, Shift-JIS Japanese, sjis_japanese_ci, 2
            // Japanese-Unicode, case-insensitive, accent-sensitive, kanatype-insensitive, width-insensitive
            MySqlToSqlServerLookup.Add(System.Enum.GetName(typeof(MySqlCharacterSetType), MySqlCharacterSetType.sjis),
                System.Enum.GetName(typeof(SqlServerCharacterSetType), SqlServerCharacterSetType.Japanese_Unicode_CI_AS));

            // swe7, 7bit Swedish, swe7_swedish_ci, 1
            // Finnish-Swedish, case-insensitive, accent-sensitive, kanatype-insensitive, width-insensitive for Unicode Data,
            // SQL Server Sort Order 185 on Code Page 1252 for non-Unicode Data
            MySqlToSqlServerLookup.Add(System.Enum.GetName(typeof(MySqlCharacterSetType), MySqlCharacterSetType.swe7),
                System.Enum.GetName(typeof(SqlServerCharacterSetType), SqlServerCharacterSetType.SQL_SwedishStd_Pref_CP1_CI_AS));

            // tis620, TIS620 Thai, tis620_thai_ci, 1
            // Thai, case-insensitive, accent-sensitive, kanatype-insensitive, width-insensitive
            MySqlToSqlServerLookup.Add(System.Enum.GetName(typeof(MySqlCharacterSetType), MySqlCharacterSetType.tis620),
                System.Enum.GetName(typeof(SqlServerCharacterSetType), SqlServerCharacterSetType.Thai_CI_AS));

            // ucs2, UCS-2 Unicode, ucs2_general_ci, 2
            // Latin1-General, case-insensitive, accent-sensitive, kanatype-insensitive, width-insensitive for Unicode Data,
            // SQL Server Sort Order 52 on Code Page 1252 for non-Unicode Data
            MySqlToSqlServerLookup.Add(System.Enum.GetName(typeof(MySqlCharacterSetType), MySqlCharacterSetType.ucs2),
                System.Enum.GetName(typeof(SqlServerCharacterSetType), SqlServerCharacterSetType.SQL_Latin1_General_CP1_CI_AS));

            // ujis, EUC-JP Japanese, ujis_japanese_ci, 3
            // Japanese-Unicode, case-insensitive, accent-sensitive, kanatype-insensitive, width-insensitive
            MySqlToSqlServerLookup.Add(System.Enum.GetName(typeof(MySqlCharacterSetType), MySqlCharacterSetType.ujis),
                System.Enum.GetName(typeof(SqlServerCharacterSetType), SqlServerCharacterSetType.Japanese_Unicode_CI_AS));

            // utf16, UTF-16 Unicode, utf16_general_ci, 4
            // Latin1-General, case-insensitive, accent-sensitive, kanatype-insensitive, width-insensitive for Unicode Data,
            // SQL Server Sort Order 52 on Code Page 1252 for non-Unicode Data
            MySqlToSqlServerLookup.Add(System.Enum.GetName(typeof(MySqlCharacterSetType), MySqlCharacterSetType.utf16),
                System.Enum.GetName(typeof(SqlServerCharacterSetType), SqlServerCharacterSetType.SQL_Latin1_General_CP1_CI_AS));

            // utf32, UTF-32 Unicode, utf32_general_ci, 4
            // Latin1-General, case-insensitive, accent-sensitive, kanatype-insensitive, width-insensitive for Unicode Data,
            // SQL Server Sort Order 52 on Code Page 1252 for non-Unicode Data
            MySqlToSqlServerLookup.Add(System.Enum.GetName(typeof(MySqlCharacterSetType), MySqlCharacterSetType.utf32),
                System.Enum.GetName(typeof(SqlServerCharacterSetType), SqlServerCharacterSetType.SQL_Latin1_General_CP1_CI_AS));

            // utf8, UTF-8 Unicode, utf8_general_ci, 3
            // Latin1-General, case-insensitive, accent-sensitive, kanatype-insensitive, width-insensitive for Unicode Data,
            // SQL Server Sort Order 52 on Code Page 1252 for non-Unicode Data
            MySqlToSqlServerLookup.Add(System.Enum.GetName(typeof(MySqlCharacterSetType), MySqlCharacterSetType.utf8),
                System.Enum.GetName(typeof(SqlServerCharacterSetType), SqlServerCharacterSetType.SQL_Latin1_General_CP1_CI_AS));

            // utf8mb4, UTF-8 Unicode, utf8mb4_general_ci, 4
            // Latin1-General, case-insensitive, accent-sensitive, kanatype-insensitive, width-insensitive for Unicode Data,
            // SQL Server Sort Order 52 on Code Page 1252 for non-Unicode Data
            MySqlToSqlServerLookup.Add(System.Enum.GetName(typeof(MySqlCharacterSetType), MySqlCharacterSetType.utf8mb4),
                System.Enum.GetName(typeof(SqlServerCharacterSetType), SqlServerCharacterSetType.SQL_Latin1_General_CP1_CI_AS));
        }

        /// <summary>
        /// Initializes SqlServerToMySqlLookup
        /// </summary>
        private static void BuildSqlServerToMySqlLookup()
        {
            // Add ucs2 for greatest compatibility
            // ucs2, UCS-2 Unicode, ucs2_general_ci, 2
            foreach (var sqlServerCharacterSet in SqlServerCharacterSets)
            {
                SqlServerToMySqlLookup.Add(sqlServerCharacterSet,
                    System.Enum.GetName(typeof(MySqlCharacterSetType), MySqlCharacterSetType.ucs2));
            }    
        }

		#endregion Private Methods 

		#endregion Methods 
    }
}
