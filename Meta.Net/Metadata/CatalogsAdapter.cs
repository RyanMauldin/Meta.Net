using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using Meta.Net.Interfaces;
using Meta.Net.Objects;

namespace Meta.Net.Metadata
{
    public static class CatalogsAdapter
    {
        public static void Read(Server server, IDataReader reader)
        {
            var objectNameOrdinal = reader.GetOrdinal("ObjectName");
            var createDateOrdinal = reader.GetOrdinal("CreateDate");
            var compatibilityLevelOrdinal = reader.GetOrdinal("CompatibilityLevel");
            var collationNameOrdinal = reader.GetOrdinal("CollationName");
            var userAccessOrdinal = reader.GetOrdinal("UserAccess");
            var userAccessDescriptionOrdinal = reader.GetOrdinal("UserAccessDescription");
            var isReadOnlyOrdinal = reader.GetOrdinal("IsReadOnly");
            var isAutoCloseOnOrdinal = reader.GetOrdinal("IsAutoCloseOn");
            var isAutoShrinkOnOrdinal = reader.GetOrdinal("IsAutoShrinkOn");
            var stateOrdinal = reader.GetOrdinal("State");
            var stateDescriptionOrdinal = reader.GetOrdinal("StateDescription");
            var isInStandbyOrdinal = reader.GetOrdinal("IsInStandby");
            var isCleanlyShutdownOrdinal = reader.GetOrdinal("IsCleanlyShutdown");
            var isSupplementalLoggingEnabledOrdinal = reader.GetOrdinal("IsSupplementalLoggingEnabled");
            var recoveryModelOrdinal = reader.GetOrdinal("RecoveryModel");
            var recoveryModelDescriptionOrdinal = reader.GetOrdinal("RecoveryModelDescription");
            var pageVerifyOptionOrdinal = reader.GetOrdinal("PageVerifyOption");
            var pageVerifyOptionDescriptionOrdinal = reader.GetOrdinal("PageVerifyOptionDescription");
            var isAutoCreateStatsOnOrdinal = reader.GetOrdinal("IsAutoCreateStatsOn");
            var isAutoUpdateStatsOnOrdinal = reader.GetOrdinal("IsAutoUpdateStatsOn");
            var isAutoUpdateStatsAsyncOnOrdinal = reader.GetOrdinal("IsAutoUpdateStatsAsyncOn");
            var isAnsiNullDefaultOnOrdinal = reader.GetOrdinal("IsAnsiNullDefaultOn");
            var isAnsiNullsOnOrdinal = reader.GetOrdinal("IsAnsiNullsOn");
            var isAnsiPaddingOnOrdinal = reader.GetOrdinal("IsAnsiPaddingOn");
            var isAnsiWarningsOnOrdinal = reader.GetOrdinal("IsAnsiWarningsOn");
            var isArithabortOnOrdinal = reader.GetOrdinal("IsArithabortOn");
            var isConcatNullYieldsNullOnOrdinal = reader.GetOrdinal("IsConcatNullYieldsNullOn");
            var isNumericRoundabortOnOrdinal = reader.GetOrdinal("IsNumericRoundabortOn");
            var isQuotedIdentifierOnOrdinal = reader.GetOrdinal("IsQuotedIdentifierOn");
            var isRecursiveTriggersOnOrdinal = reader.GetOrdinal("IsRecursiveTriggersOn");
            var isCursorCloseOnCommitOnOrdinal = reader.GetOrdinal("IsCursorCloseOnCommitOn");
            var isLocalCursorDefaultOrdinal = reader.GetOrdinal("IsLocalCursorDefault");
            var isFulltextEnabledOrdinal = reader.GetOrdinal("IsFulltextEnabled");
            var isTrustworthyOnOrdinal = reader.GetOrdinal("IsTrustworthyOn");
            var isDbChainingOnOrdinal = reader.GetOrdinal("IsDbChainingOn");
            var isParameterizationForcedOrdinal = reader.GetOrdinal("IsParameterizationForced");
            var isMasterKeyEncryptedByServerOrdinal = reader.GetOrdinal("IsMasterKeyEncryptedByServer");
            var isDateCorrelationOnOrdinal = reader.GetOrdinal("IsDateCorrelationOn");

            while (reader.Read())
            {
                var objectName = Convert.ToString(reader[objectNameOrdinal]);
                var createDate = Convert.ToString(reader[createDateOrdinal]);
                var compatibilityLevel = Convert.ToInt32(reader[compatibilityLevelOrdinal]);
                var collationName = Convert.ToString(reader[collationNameOrdinal]);
                var userAccess = Convert.ToInt32(reader[userAccessOrdinal]);
                var userAccessDescription = Convert.ToString(reader[userAccessDescriptionOrdinal]);
                var isReadOnly = Convert.ToBoolean(reader[isReadOnlyOrdinal]);
                var isAutoCloseOn = Convert.ToBoolean(reader[isAutoCloseOnOrdinal]);
                var isAutoShrinkOn = Convert.ToBoolean(reader[isAutoShrinkOnOrdinal]);
                var state = Convert.ToInt32(reader[stateOrdinal]);
                var stateDescription = Convert.ToString(reader[stateDescriptionOrdinal]);
                var isInStandby = Convert.ToBoolean(reader[isInStandbyOrdinal]);
                var isCleanlyShutdown = Convert.ToBoolean(reader[isCleanlyShutdownOrdinal]);
                var isSupplementalLoggingEnabled = Convert.ToBoolean(reader[isSupplementalLoggingEnabledOrdinal]);
                var recoveryModel = Convert.ToInt32(reader[recoveryModelOrdinal]);
                var recoveryModelDescription = Convert.ToString(reader[recoveryModelDescriptionOrdinal]);
                var pageVerifyOption = Convert.ToInt32(reader[pageVerifyOptionOrdinal]);
                var pageVerifyOptionDescription = Convert.ToString(reader[pageVerifyOptionDescriptionOrdinal]);
                var isAutoCreateStatsOn = Convert.ToBoolean(reader[isAutoCreateStatsOnOrdinal]);
                var isAutoUpdateStatsOn = Convert.ToBoolean(reader[isAutoUpdateStatsOnOrdinal]);
                var isAutoUpdateStatsAsyncOn = Convert.ToBoolean(reader[isAutoUpdateStatsAsyncOnOrdinal]);
                var isAnsiNullDefaultOn = Convert.ToBoolean(reader[isAnsiNullDefaultOnOrdinal]);
                var isAnsiNullsOn = Convert.ToBoolean(reader[isAnsiNullsOnOrdinal]);
                var isAnsiPaddingOn = Convert.ToBoolean(reader[isAnsiPaddingOnOrdinal]);
                var isAnsiWarningsOn = Convert.ToBoolean(reader[isAnsiWarningsOnOrdinal]);
                var isArithabortOn = Convert.ToBoolean(reader[isArithabortOnOrdinal]);
                var isConcatNullYieldsNullOn = Convert.ToBoolean(reader[isConcatNullYieldsNullOnOrdinal]);
                var isNumericRoundabortOn = Convert.ToBoolean(reader[isNumericRoundabortOnOrdinal]);
                var isQuotedIdentifierOn = Convert.ToBoolean(reader[isQuotedIdentifierOnOrdinal]);
                var isRecursiveTriggersOn = Convert.ToBoolean(reader[isRecursiveTriggersOnOrdinal]);
                var isCursorCloseOnCommitOn = Convert.ToBoolean(reader[isCursorCloseOnCommitOnOrdinal]);
                var isLocalCursorDefault = Convert.ToBoolean(reader[isLocalCursorDefaultOrdinal]);
                var isFulltextEnabled = Convert.ToBoolean(reader[isFulltextEnabledOrdinal]);
                var isTrustworthyOn = Convert.ToBoolean(reader[isTrustworthyOnOrdinal]);
                var isDbChainingOn = Convert.ToBoolean(reader[isDbChainingOnOrdinal]);
                var isParameterizationForced = Convert.ToBoolean(reader[isParameterizationForcedOrdinal]);
                var isMasterKeyEncryptedByServer = Convert.ToBoolean(reader[isMasterKeyEncryptedByServerOrdinal]);
                var isDateCorrelationOn = Convert.ToBoolean(reader[isDateCorrelationOnOrdinal]);

                var catalog = new Catalog
                {
                    Server = server,
                    ObjectName = objectName,
                    CreateDate = createDate,
                    CompatibilityLevel = compatibilityLevel,
                    CollationName = collationName,
                    UserAccess = userAccess,
                    UserAccessDescription = userAccessDescription,
                    IsReadOnly = isReadOnly,
                    IsAutoCloseOn = isAutoCloseOn,
                    IsAutoShrinkOn = isAutoShrinkOn,
                    State = state,
                    StateDescription = stateDescription,
                    IsInStandby = isInStandby,
                    IsCleanlyShutdown = isCleanlyShutdown,
                    IsSupplementalLoggingEnabled = isSupplementalLoggingEnabled,
                    RecoveryModel = recoveryModel,
                    RecoveryModelDescription = recoveryModelDescription,
                    PageVerifyOption = pageVerifyOption,
                    PageVerifyOptionDescription = pageVerifyOptionDescription,
                    IsAutoCreateStatsOn = isAutoCreateStatsOn,
                    IsAutoUpdateStatsOn = isAutoUpdateStatsOn,
                    IsAutoUpdateStatsAsyncOn = isAutoUpdateStatsAsyncOn,
                    IsAnsiNullDefaultOn = isAnsiNullDefaultOn,
                    IsAnsiNullsOn = isAnsiNullsOn,
                    IsAnsiPaddingOn = isAnsiPaddingOn,
                    IsAnsiWarningsOn = isAnsiWarningsOn,
                    IsArithabortOn = isArithabortOn,
                    IsConcatNullYieldsNullOn = isConcatNullYieldsNullOn,
                    IsNumericRoundabortOn = isNumericRoundabortOn,
                    IsQuotedIdentifierOn = isQuotedIdentifierOn,
                    IsRecursiveTriggersOn = isRecursiveTriggersOn,
                    IsCursorCloseOnCommitOn = isCursorCloseOnCommitOn,
                    IsLocalCursorDefault = isLocalCursorDefault,
                    IsFulltextEnabled = isFulltextEnabled,
                    IsTrustworthyOn = isTrustworthyOn,
                    IsDbChainingOn = isDbChainingOn,
                    IsParameterizationForced = isParameterizationForced,
                    IsMasterKeyEncryptedByServer = isMasterKeyEncryptedByServer,
                    IsDateCorrelationOn = isDateCorrelationOn
                };

                server.Catalogs.Add(catalog);
            }
        }

        /// <summary>
        /// Fills only the catalogs in the server object.
        /// </summary>
        /// <param name="server">The server to fill catalogs only.</param>
        /// <param name="connection">The open database connection to use.</param>
        /// <param name="metadataScriptFactory">The metadata script factory determined by the database type.</param>
        public static void Get(Server server, DbConnection connection, IMetadataScriptFactory metadataScriptFactory)
        {
            using (var command = connection.CreateCommand())
            {
                command.CommandText = metadataScriptFactory.Catalogs();
                using (var reader = command.ExecuteReader())
                {
                    if (!reader.HasRows)
                    {
                        reader.Close();
                        return;
                    }

                    Read(server, reader);

                    reader.Close();
                }
            }
        }

        /// <summary>
        /// Fills only the catalogs in the server object.
        /// </summary>
        /// <param name="server">The server to fill catalogs only.</param>
        /// <param name="connection">The open database connection to use.</param>
        /// <param name="metadataScriptFactory">The script factory determined by the database type.</param>
        /// <param name="catalogs">The catalogs to filter.</param>
        public static void GetSpecific(Server server, DbConnection connection, IMetadataScriptFactory metadataScriptFactory, IList<string> catalogs)
        {
            using (var command = connection.CreateCommand())
            {
                command.CommandText = metadataScriptFactory.Catalogs(catalogs);
                using (var reader = command.ExecuteReader())
                {
                    if (!reader.HasRows)
                    {
                        reader.Close();
                        return;
                    }

                    Read(server, reader);

                    reader.Close();
                }
            }
        }

        /// <summary>
        /// Fills the catalogs in the server object as well as building out the schemas
        /// and all objects in the database connection.
        /// </summary>
        /// <param name="server">The server to fill catalogs only.</param>
        /// <param name="connection">The open database connection to use.</param>
        /// <param name="metadataScriptFactory">The script factory determined by the database type.</param>
        public static void Build(Server server, DbConnection connection, IMetadataScriptFactory metadataScriptFactory)
        {
            Get(server, connection, metadataScriptFactory);
            foreach (var catalog in server.Catalogs)
                SchemasAdapter.Build(catalog, connection, metadataScriptFactory);
        }

        /// <summary>
        /// Fills the catalogs in the server object as well as building out the schemas
        /// and all objects in the database connection.
        /// </summary>
        /// <param name="server">The server to fill catalogs only.</param>
        /// <param name="connection">The open database connection to use.</param>
        /// <param name="metadataScriptFactory">The script factory determined by the database type.</param>
        /// <param name="catalogs">The catalogs to filter.</param>
        public static void BuildSpecific(Server server, DbConnection connection, IMetadataScriptFactory metadataScriptFactory, IList<string> catalogs)
        {
            GetSpecific(server, connection, metadataScriptFactory, catalogs);
            foreach (var catalog in server.Catalogs)
                SchemasAdapter.Build(catalog, connection, metadataScriptFactory);
        }
    }
}
