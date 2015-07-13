using System;
using System.Data;
using Meta.Net.Objects;

namespace Meta.Net.Metadata.Factories
{
    internal class CatalogFactory
    {
        private int ObjectNameOrdinal { get; set; }
        private int CreateDateOrdinal { get; set; }
        private int CompatibilityLevelOrdinal { get; set; }
        private int CollationNameOrdinal { get; set; }
        private int UserAccessOrdinal { get; set; }
        private int UserAccessDescriptionOrdinal { get; set; }
        private int IsReadOnlyOrdinal { get; set; }
        private int IsAutoCloseOnOrdinal { get; set; }
        private int IsAutoShrinkOnOrdinal { get; set; }
        private int StateOrdinal { get; set; }
        private int StateDescriptionOrdinal { get; set; }
        private int IsInStandbyOrdinal { get; set; }
        private int IsCleanlyShutdownOrdinal { get; set; }
        private int IsSupplementalLoggingEnabledOrdinal { get; set; }
        private int RecoveryModelOrdinal { get; set; }
        private int RecoveryModelDescriptionOrdinal { get; set; }
        private int PageVerifyOptionOrdinal { get; set; }
        private int PageVerifyOptionDescriptionOrdinal { get; set; }
        private int IsAutoCreateStatsOnOrdinal { get; set; }
        private int IsAutoUpdateStatsOnOrdinal { get; set; }
        private int IsAutoUpdateStatsAsyncOnOrdinal { get; set; }
        private int IsAnsiNullDefaultOnOrdinal { get; set; }
        private int IsAnsiNullsOnOrdinal { get; set; }
        private int IsAnsiPaddingOnOrdinal { get; set; }
        private int IsAnsiWarningsOnOrdinal { get; set; }
        private int IsArithabortOnOrdinal { get; set; }
        private int IsConcatNullYieldsNullOnOrdinal { get; set; }
        private int IsNumericRoundabortOnOrdinal { get; set; }
        private int IsQuotedIdentifierOnOrdinal { get; set; }
        private int IsRecursiveTriggersOnOrdinal { get; set; }
        private int IsCursorCloseOnCommitOnOrdinal { get; set; }
        private int IsLocalCursorDefaultOrdinal { get; set; }
        private int IsFulltextEnabledOrdinal { get; set; }
        private int IsTrustworthyOnOrdinal { get; set; }
        private int IsDbChainingOnOrdinal { get; set; }
        private int IsParameterizationForcedOrdinal { get; set; }
        private int IsMasterKeyEncryptedByServerOrdinal { get; set; }
        private int IsDateCorrelationOnOrdinal { get; set; }

        public CatalogFactory(IDataRecord reader)
        {
            ObjectNameOrdinal = reader.GetOrdinal("ObjectName");
            CreateDateOrdinal = reader.GetOrdinal("CreateDate");
            CompatibilityLevelOrdinal = reader.GetOrdinal("CompatibilityLevel");
            CollationNameOrdinal = reader.GetOrdinal("CollationName");
            UserAccessOrdinal = reader.GetOrdinal("UserAccess");
            UserAccessDescriptionOrdinal = reader.GetOrdinal("UserAccessDescription");
            IsReadOnlyOrdinal = reader.GetOrdinal("IsReadOnly");
            IsAutoCloseOnOrdinal = reader.GetOrdinal("IsAutoCloseOn");
            IsAutoShrinkOnOrdinal = reader.GetOrdinal("IsAutoShrinkOn");
            StateOrdinal = reader.GetOrdinal("State");
            StateDescriptionOrdinal = reader.GetOrdinal("StateDescription");
            IsInStandbyOrdinal = reader.GetOrdinal("IsInStandby");
            IsCleanlyShutdownOrdinal = reader.GetOrdinal("IsCleanlyShutdown");
            IsSupplementalLoggingEnabledOrdinal = reader.GetOrdinal("IsSupplementalLoggingEnabled");
            RecoveryModelOrdinal = reader.GetOrdinal("RecoveryModel");
            RecoveryModelDescriptionOrdinal = reader.GetOrdinal("RecoveryModelDescription");
            PageVerifyOptionOrdinal = reader.GetOrdinal("PageVerifyOption");
            PageVerifyOptionDescriptionOrdinal = reader.GetOrdinal("PageVerifyOptionDescription");
            IsAutoCreateStatsOnOrdinal = reader.GetOrdinal("IsAutoCreateStatsOn");
            IsAutoUpdateStatsOnOrdinal = reader.GetOrdinal("IsAutoUpdateStatsOn");
            IsAutoUpdateStatsAsyncOnOrdinal = reader.GetOrdinal("IsAutoUpdateStatsAsyncOn");
            IsAnsiNullDefaultOnOrdinal = reader.GetOrdinal("IsAnsiNullDefaultOn");
            IsAnsiNullsOnOrdinal = reader.GetOrdinal("IsAnsiNullsOn");
            IsAnsiPaddingOnOrdinal = reader.GetOrdinal("IsAnsiPaddingOn");
            IsAnsiWarningsOnOrdinal = reader.GetOrdinal("IsAnsiWarningsOn");
            IsArithabortOnOrdinal = reader.GetOrdinal("IsArithabortOn");
            IsConcatNullYieldsNullOnOrdinal = reader.GetOrdinal("IsConcatNullYieldsNullOn");
            IsNumericRoundabortOnOrdinal = reader.GetOrdinal("IsNumericRoundabortOn");
            IsQuotedIdentifierOnOrdinal = reader.GetOrdinal("IsQuotedIdentifierOn");
            IsRecursiveTriggersOnOrdinal = reader.GetOrdinal("IsRecursiveTriggersOn");
            IsCursorCloseOnCommitOnOrdinal = reader.GetOrdinal("IsCursorCloseOnCommitOn");
            IsLocalCursorDefaultOrdinal = reader.GetOrdinal("IsLocalCursorDefault");
            IsFulltextEnabledOrdinal = reader.GetOrdinal("IsFulltextEnabled");
            IsTrustworthyOnOrdinal = reader.GetOrdinal("IsTrustworthyOn");
            IsDbChainingOnOrdinal = reader.GetOrdinal("IsDbChainingOn");
            IsParameterizationForcedOrdinal = reader.GetOrdinal("IsParameterizationForced");
            IsMasterKeyEncryptedByServerOrdinal = reader.GetOrdinal("IsMasterKeyEncryptedByServer");
            IsDateCorrelationOnOrdinal = reader.GetOrdinal("IsDateCorrelationOn");
        }

        public void CreateCatalog(
            Server server,
            IDataRecord reader)
        {
            var catalog = new Catalog
            {
                Server = server,
                ObjectName = Convert.ToString(reader[ObjectNameOrdinal]),
                CreateDate = Convert.ToString(reader[CreateDateOrdinal]),
                CompatibilityLevel = Convert.ToInt32(reader[CompatibilityLevelOrdinal]),
                CollationName = Convert.ToString(reader[CollationNameOrdinal]),
                UserAccess = Convert.ToInt32(reader[UserAccessOrdinal]),
                UserAccessDescription = Convert.ToString(reader[UserAccessDescriptionOrdinal]),
                IsReadOnly = Convert.ToBoolean(reader[IsReadOnlyOrdinal]),
                IsAutoCloseOn = Convert.ToBoolean(reader[IsAutoCloseOnOrdinal]),
                IsAutoShrinkOn = Convert.ToBoolean(reader[IsAutoShrinkOnOrdinal]),
                State = Convert.ToInt32(reader[StateOrdinal]),
                StateDescription = Convert.ToString(reader[StateDescriptionOrdinal]),
                IsInStandby = Convert.ToBoolean(reader[IsInStandbyOrdinal]),
                IsCleanlyShutdown = Convert.ToBoolean(reader[IsCleanlyShutdownOrdinal]),
                IsSupplementalLoggingEnabled = Convert.ToBoolean(reader[IsSupplementalLoggingEnabledOrdinal]),
                RecoveryModel = Convert.ToInt32(reader[RecoveryModelOrdinal]),
                RecoveryModelDescription = Convert.ToString(reader[RecoveryModelDescriptionOrdinal]),
                PageVerifyOption = Convert.ToInt32(reader[PageVerifyOptionOrdinal]),
                PageVerifyOptionDescription = Convert.ToString(reader[PageVerifyOptionDescriptionOrdinal]),
                IsAutoCreateStatsOn = Convert.ToBoolean(reader[IsAutoCreateStatsOnOrdinal]),
                IsAutoUpdateStatsOn = Convert.ToBoolean(reader[IsAutoUpdateStatsOnOrdinal]),
                IsAutoUpdateStatsAsyncOn = Convert.ToBoolean(reader[IsAutoUpdateStatsAsyncOnOrdinal]),
                IsAnsiNullDefaultOn = Convert.ToBoolean(reader[IsAnsiNullDefaultOnOrdinal]),
                IsAnsiNullsOn = Convert.ToBoolean(reader[IsAnsiNullsOnOrdinal]),
                IsAnsiPaddingOn = Convert.ToBoolean(reader[IsAnsiPaddingOnOrdinal]),
                IsAnsiWarningsOn = Convert.ToBoolean(reader[IsAnsiWarningsOnOrdinal]),
                IsArithabortOn = Convert.ToBoolean(reader[IsArithabortOnOrdinal]),
                IsConcatNullYieldsNullOn = Convert.ToBoolean(reader[IsConcatNullYieldsNullOnOrdinal]),
                IsNumericRoundabortOn = Convert.ToBoolean(reader[IsNumericRoundabortOnOrdinal]),
                IsQuotedIdentifierOn = Convert.ToBoolean(reader[IsQuotedIdentifierOnOrdinal]),
                IsRecursiveTriggersOn = Convert.ToBoolean(reader[IsRecursiveTriggersOnOrdinal]),
                IsCursorCloseOnCommitOn = Convert.ToBoolean(reader[IsCursorCloseOnCommitOnOrdinal]),
                IsLocalCursorDefault = Convert.ToBoolean(reader[IsLocalCursorDefaultOrdinal]),
                IsFulltextEnabled = Convert.ToBoolean(reader[IsFulltextEnabledOrdinal]),
                IsTrustworthyOn = Convert.ToBoolean(reader[IsTrustworthyOnOrdinal]),
                IsDbChainingOn = Convert.ToBoolean(reader[IsDbChainingOnOrdinal]),
                IsParameterizationForced = Convert.ToBoolean(reader[IsParameterizationForcedOrdinal]),
                IsMasterKeyEncryptedByServer = Convert.ToBoolean(reader[IsMasterKeyEncryptedByServerOrdinal]),
                IsDateCorrelationOn = Convert.ToBoolean(reader[IsDateCorrelationOnOrdinal])
            };

            server.Catalogs.Add(catalog);
        }
    }
}
