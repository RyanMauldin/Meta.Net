using System.Data;

namespace Meta.Net.Data.Metadata
{
    public class CatalogsRow: DataRow
    {
		#region Properties (41) 

        public string CollationName
        {
            get
            {
                return ((string)(this[DataTable.CollationNameColumn]));
            }
            set
            {
                this[DataTable.CollationNameColumn] = value;
            }
        }

        public int CompatibilityLevel
        {
            get
            {
                return ((int)(this[DataTable.CompatibilityLevelColumn]));
            }
            set
            {
                this[DataTable.CompatibilityLevelColumn] = value;
            }
        }

        public string CreateDate
        {
            get
            {
                return ((string)(this[DataTable.CreateDateColumn]));
            }
            set
            {
                this[DataTable.CreateDateColumn] = value;
            }
        }

        public CatalogsDataTable DataTable { get; set; }

        public string Description
        {
            get
            {
                return ((string)(this[DataTable.DescriptionColumn]));
            }
            set
            {
                this[DataTable.DescriptionColumn] = value;
            }
        }

        public bool IsAnsiNullDefaultOn
        {
            get
            {
                return ((bool)(this[DataTable.IsAnsiNullDefaultOnColumn]));
            }
            set
            {
                this[DataTable.IsAnsiNullDefaultOnColumn] = value;
            }
        }

        public bool IsAnsiNullsOn
        {
            get
            {
                return ((bool)(this[DataTable.IsAnsiNullsOnColumn]));
            }
            set
            {
                this[DataTable.IsAnsiNullsOnColumn] = value;
            }
        }

        public bool IsAnsiPaddingOn
        {
            get
            {
                return ((bool)(this[DataTable.IsAnsiPaddingOnColumn]));
            }
            set
            {
                this[DataTable.IsAnsiPaddingOnColumn] = value;
            }
        }

        public bool IsAnsiWarningsOn
        {
            get
            {
                return ((bool)(this[DataTable.IsAnsiWarningsOnColumn]));
            }
            set
            {
                this[DataTable.IsAnsiWarningsOnColumn] = value;
            }
        }

        public bool IsArithabortOn
        {
            get
            {
                return ((bool)(this[DataTable.IsArithabortOnColumn]));
            }
            set
            {
                this[DataTable.IsArithabortOnColumn] = value;
            }
        }

        public bool IsAutoCloseOn
        {
            get
            {
                return ((bool)(this[DataTable.IsAutoCloseOnColumn]));
            }
            set
            {
                this[DataTable.IsAutoCloseOnColumn] = value;
            }
        }

        public bool IsAutoCreateStatsOn
        {
            get
            {
                return ((bool)(this[DataTable.IsAutoCreateStatsOnColumn]));
            }
            set
            {
                this[DataTable.IsAutoCreateStatsOnColumn] = value;
            }
        }

        public bool IsAutoShrinkOn
        {
            get
            {
                return ((bool)(this[DataTable.IsAutoShrinkOnColumn]));
            }
            set
            {
                this[DataTable.IsAutoShrinkOnColumn] = value;
            }
        }

        public bool IsAutoUpdateStatsAsyncOn
        {
            get
            {
                return ((bool)(this[DataTable.IsAutoUpdateStatsAsyncOnColumn]));
            }
            set
            {
                this[DataTable.IsAutoUpdateStatsAsyncOnColumn] = value;
            }
        }

        public bool IsAutoUpdateStatsOn
        {
            get
            {
                return ((bool)(this[DataTable.IsAutoUpdateStatsOnColumn]));
            }
            set
            {
                this[DataTable.IsAutoUpdateStatsOnColumn] = value;
            }
        }

        public bool IsCleanlyShutdown
        {
            get
            {
                return ((bool)(this[DataTable.IsCleanlyShutdownColumn]));
            }
            set
            {
                this[DataTable.IsCleanlyShutdownColumn] = value;
            }
        }

        public bool IsConcatNullYieldsNullOn
        {
            get
            {
                return ((bool)(this[DataTable.IsConcatNullYieldsNullOnColumn]));
            }
            set
            {
                this[DataTable.IsConcatNullYieldsNullOnColumn] = value;
            }
        }

        public bool IsCursorCloseOnCommitOn
        {
            get
            {
                return ((bool)(this[DataTable.IsCursorCloseOnCommitOnColumn]));
            }
            set
            {
                this[DataTable.IsCursorCloseOnCommitOnColumn] = value;
            }
        }

        public bool IsDateCorrelationOn
        {
            get
            {
                return ((bool)(this[DataTable.IsDateCorrelationOnColumn]));
            }
            set
            {
                this[DataTable.IsDateCorrelationOnColumn] = value;
            }
        }

        public bool IsDbChainingOn
        {
            get
            {
                return ((bool)(this[DataTable.IsDbChainingOnColumn]));
            }
            set
            {
                this[DataTable.IsDbChainingOnColumn] = value;
            }
        }

        public bool IsFulltextEnabled
        {
            get
            {
                return ((bool)(this[DataTable.IsFulltextEnabledColumn]));
            }
            set
            {
                this[DataTable.IsFulltextEnabledColumn] = value;
            }
        }

        public bool IsInStandby
        {
            get
            {
                return ((bool)(this[DataTable.IsInStandbyColumn]));
            }
            set
            {
                this[DataTable.IsInStandbyColumn] = value;
            }
        }

        public bool IsLocalCursorDefault
        {
            get
            {
                return ((bool)(this[DataTable.IsLocalCursorDefaultColumn]));
            }
            set
            {
                this[DataTable.IsLocalCursorDefaultColumn] = value;
            }
        }

        public bool IsMasterKeyEncryptedByServer
        {
            get
            {
                return ((bool)(this[DataTable.IsMasterKeyEncryptedByServerColumn]));
            }
            set
            {
                this[DataTable.IsMasterKeyEncryptedByServerColumn] = value;
            }
        }

        public bool IsNumericRoundabortOn
        {
            get
            {
                return ((bool)(this[DataTable.IsNumericRoundabortOnColumn]));
            }
            set
            {
                this[DataTable.IsNumericRoundabortOnColumn] = value;
            }
        }

        public bool IsParameterizationForced
        {
            get
            {
                return ((bool)(this[DataTable.IsParameterizationForcedColumn]));
            }
            set
            {
                this[DataTable.IsParameterizationForcedColumn] = value;
            }
        }

        public bool IsQuotedIdentifierOn
        {
            get
            {
                return ((bool)(this[DataTable.IsQuotedIdentifierOnColumn]));
            }
            set
            {
                this[DataTable.IsQuotedIdentifierOnColumn] = value;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return ((bool)(this[DataTable.IsReadOnlyColumn]));
            }
            set
            {
                this[DataTable.IsReadOnlyColumn] = value;
            }
        }

        public bool IsRecursiveTriggersOn
        {
            get
            {
                return ((bool)(this[DataTable.IsRecursiveTriggersOnColumn]));
            }
            set
            {
                this[DataTable.IsRecursiveTriggersOnColumn] = value;
            }
        }

        public bool IsSupplementalLoggingEnabled
        {
            get
            {
                return ((bool)(this[DataTable.IsSupplementalLoggingEnabledColumn]));
            }
            set
            {
                this[DataTable.IsSupplementalLoggingEnabledColumn] = value;
            }
        }

        public bool IsTrustworthyOn
        {
            get
            {
                return ((bool)(this[DataTable.IsTrustworthyOnColumn]));
            }
            set
            {
                this[DataTable.IsTrustworthyOnColumn] = value;
            }
        }

        public string Namespace
        {
            get
            {
                return this[DataTable.NamespaceColumn] as string;
            }
            set
            {
                this[DataTable.NamespaceColumn] = value;
            }
        }

        public string ObjectName
        {
            get
            {
                return ((string)(this[DataTable.ObjectNameColumn]));
            }
            set
            {
                this[DataTable.ObjectNameColumn] = value;
            }
        }

        public int PageVerifyOption
        {
            get
            {
                return ((int)(this[DataTable.PageVerifyOptionColumn]));
            }
            set
            {
                this[DataTable.PageVerifyOptionColumn] = value;
            }
        }

        public string PageVerifyOptionDescription
        {
            get
            {
                return ((string)(this[DataTable.PageVerifyOptionDescriptionColumn]));
            }
            set
            {
                this[DataTable.PageVerifyOptionDescriptionColumn] = value;
            }
        }

        public int RecoveryModel
        {
            get
            {
                return ((int)(this[DataTable.RecoveryModelColumn]));
            }
            set
            {
                this[DataTable.RecoveryModelColumn] = value;
            }
        }

        public string RecoveryModelDescription
        {
            get
            {
                return ((string)(this[DataTable.RecoveryModelDescriptionColumn]));
            }
            set
            {
                this[DataTable.RecoveryModelDescriptionColumn] = value;
            }
        }

        public int State
        {
            get
            {
                return ((int)(this[DataTable.StateColumn]));
            }
            set
            {
                this[DataTable.StateColumn] = value;
            }
        }

        public string StateDescription
        {
            get
            {
                return ((string)(this[DataTable.StateDescriptionColumn]));
            }
            set
            {
                this[DataTable.StateDescriptionColumn] = value;
            }
        }

        public int UserAccess
        {
            get
            {
                return ((int)(this[DataTable.UserAccessColumn]));
            }
            set
            {
                this[DataTable.UserAccessColumn] = value;
            }
        }

        public string UserAccessDescription
        {
            get
            {
                return ((string)(this[DataTable.UserAccessDescriptionColumn]));
            }
            set
            {
                this[DataTable.UserAccessDescriptionColumn] = value;
            }
        }

		#endregion Properties 

		#region Constructors (1) 

        public CatalogsRow(DataRowBuilder rb)
            : base(rb)
        {
            DataTable = ((CatalogsDataTable)(Table));
        }

		#endregion Constructors 
    }
}
