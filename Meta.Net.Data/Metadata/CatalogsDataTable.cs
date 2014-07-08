using System;
using System.Data;

namespace Meta.Net.Data.Metadata
{
    public class CatalogsDataTable: DataTable
    {
		#region Properties (42) 

        public DataColumn CollationNameColumn { get; set; }

        public DataColumn CompatibilityLevelColumn { get; set; }

        public int Count
        {
            get
            {
                return Rows.Count;
            }
        }

        public DataColumn CreateDateColumn { get; set; }

        public DataColumn DescriptionColumn { get; set; }

        public DataColumn IsAnsiNullDefaultOnColumn { get; set; }

        public DataColumn IsAnsiNullsOnColumn { get; set; }

        public DataColumn IsAnsiPaddingOnColumn { get; set; }

        public DataColumn IsAnsiWarningsOnColumn { get; set; }

        public DataColumn IsArithabortOnColumn { get; set; }

        public DataColumn IsAutoCloseOnColumn { get; set; }

        public DataColumn IsAutoCreateStatsOnColumn { get; set; }

        public DataColumn IsAutoShrinkOnColumn { get; set; }

        public DataColumn IsAutoUpdateStatsAsyncOnColumn { get; set; }

        public DataColumn IsAutoUpdateStatsOnColumn { get; set; }

        public DataColumn IsCleanlyShutdownColumn { get; set; }

        public DataColumn IsConcatNullYieldsNullOnColumn { get; set; }

        public DataColumn IsCursorCloseOnCommitOnColumn { get; set; }

        public DataColumn IsDateCorrelationOnColumn { get; set; }

        public DataColumn IsDbChainingOnColumn { get; set; }

        public DataColumn IsFulltextEnabledColumn { get; set; }

        public DataColumn IsInStandbyColumn { get; set; }

        public DataColumn IsLocalCursorDefaultColumn { get; set; }

        public DataColumn IsMasterKeyEncryptedByServerColumn { get; set; }

        public DataColumn IsNumericRoundabortOnColumn { get; set; }

        public DataColumn IsParameterizationForcedColumn { get; set; }

        public DataColumn IsQuotedIdentifierOnColumn { get; set; }

        public DataColumn IsReadOnlyColumn { get; set; }

        public DataColumn IsRecursiveTriggersOnColumn { get; set; }

        public DataColumn IsSupplementalLoggingEnabledColumn { get; set; }

        public DataColumn IsTrustworthyOnColumn { get; set; }

        public DataColumn NamespaceColumn { get; set; }

        public DataColumn ObjectNameColumn { get; set; }

        public DataColumn PageVerifyOptionColumn { get; set; }

        public DataColumn PageVerifyOptionDescriptionColumn { get; set; }

        public DataColumn RecoveryModelColumn { get; set; }

        public DataColumn RecoveryModelDescriptionColumn { get; set; }

        public DataColumn StateColumn { get; set; }

        public DataColumn StateDescriptionColumn { get; set; }

        public CatalogsRow this[int index]
        {
            get
            {
                return ((CatalogsRow)(Rows[index]));
            }
        }

        public DataColumn UserAccessColumn { get; set; }

        public DataColumn UserAccessDescriptionColumn { get; set; }

		#endregion Properties 

		#region Constructors (1) 

        public CatalogsDataTable()
        {
            TableName = "Catalogs";

            CollationNameColumn = new DataColumn("CollationName", typeof(string), null, MappingType.Element);
            CompatibilityLevelColumn = new DataColumn("CompatibilityLevel", typeof(int), null, MappingType.Element);
            CreateDateColumn = new DataColumn("CreateDate", typeof(string), null, MappingType.Element);
            DescriptionColumn = new DataColumn("Description", typeof(string), null, MappingType.Element);
            IsAnsiNullDefaultOnColumn = new DataColumn("IsAnsiNullDefaultOn", typeof(bool), null, MappingType.Element);
            IsAnsiNullsOnColumn = new DataColumn("IsAnsiNullsOn", typeof(bool), null, MappingType.Element);
            IsAnsiPaddingOnColumn = new DataColumn("IsAnsiPaddingOn", typeof(bool), null, MappingType.Element);
            IsAnsiWarningsOnColumn = new DataColumn("IsAnsiWarningsOn", typeof(bool), null, MappingType.Element);
            IsArithabortOnColumn = new DataColumn("IsArithabortOn", typeof(bool), null, MappingType.Element);
            IsAutoCloseOnColumn = new DataColumn("IsAutoCloseOn", typeof(bool), null, MappingType.Element);
            IsAutoCreateStatsOnColumn = new DataColumn("IsAutoCreateStatsOn", typeof(bool), null, MappingType.Element);
            IsAutoShrinkOnColumn = new DataColumn("IsAutoShrinkOn", typeof(bool), null, MappingType.Element);
            IsAutoUpdateStatsAsyncOnColumn = new DataColumn("IsAutoUpdateStatsAsyncOn", typeof(bool), null, MappingType.Element);
            IsAutoUpdateStatsOnColumn = new DataColumn("IsAutoUpdateStatsOn", typeof(bool), null, MappingType.Element);
            IsCleanlyShutdownColumn = new DataColumn("IsCleanlyShutdown", typeof(bool), null, MappingType.Element);
            IsConcatNullYieldsNullOnColumn = new DataColumn("IsConcatNullYieldsNullOn", typeof(bool), null, MappingType.Element);
            IsCursorCloseOnCommitOnColumn = new DataColumn("IsCursorCloseOnCommitOn", typeof(bool), null, MappingType.Element);
            IsDateCorrelationOnColumn = new DataColumn("IsDateCorrelationOn", typeof(bool), null, MappingType.Element);
            IsDbChainingOnColumn = new DataColumn("IsDbChainingOn", typeof(bool), null, MappingType.Element);
            IsFulltextEnabledColumn = new DataColumn("IsFulltextEnabled", typeof(bool), null, MappingType.Element);
            IsInStandbyColumn = new DataColumn("IsInStandby", typeof(bool), null, MappingType.Element);
            IsLocalCursorDefaultColumn = new DataColumn("IsLocalCursorDefault", typeof(bool), null, MappingType.Element);
            IsMasterKeyEncryptedByServerColumn = new DataColumn("IsMasterKeyEncryptedByServer", typeof(bool), null, MappingType.Element);
            IsNumericRoundabortOnColumn = new DataColumn("IsNumericRoundabortOn", typeof(bool), null, MappingType.Element);
            IsParameterizationForcedColumn = new DataColumn("IsParameterizationForced", typeof(bool), null, MappingType.Element);
            IsQuotedIdentifierOnColumn = new DataColumn("IsQuotedIdentifierOn", typeof(bool), null, MappingType.Element);
            IsReadOnlyColumn = new DataColumn("IsReadOnly", typeof(bool), null, MappingType.Element);
            IsRecursiveTriggersOnColumn = new DataColumn("IsRecursiveTriggersOn", typeof(bool), null, MappingType.Element);
            IsSupplementalLoggingEnabledColumn = new DataColumn("IsSupplementalLoggingEnabled", typeof(bool), null, MappingType.Element);
            IsTrustworthyOnColumn = new DataColumn("IsTrustworthyOn", typeof(bool), null, MappingType.Element);
            NamespaceColumn = new DataColumn("Namespace", typeof(string), null, MappingType.Element);
            ObjectNameColumn = new DataColumn("ObjectName", typeof(string), null, MappingType.Element);
            PageVerifyOptionColumn = new DataColumn("PageVerifyOption", typeof(int), null, MappingType.Element);
            PageVerifyOptionDescriptionColumn = new DataColumn("PageVerifyOptionDescription", typeof(string), null, MappingType.Element);
            RecoveryModelColumn = new DataColumn("RecoveryModel", typeof(int), null, MappingType.Element);
            RecoveryModelDescriptionColumn = new DataColumn("RecoveryModelDescription", typeof(string), null, MappingType.Element);
            StateColumn = new DataColumn("State", typeof(int), null, MappingType.Element);
            StateDescriptionColumn = new DataColumn("StateDescription", typeof(string), null, MappingType.Element);
            UserAccessColumn = new DataColumn("UserAccess", typeof(int), null, MappingType.Element);
            UserAccessDescriptionColumn = new DataColumn("UserAccessDescription", typeof(string), null, MappingType.Element);

            base.Columns.Add(CollationNameColumn);
            base.Columns.Add(CompatibilityLevelColumn);
            base.Columns.Add(CreateDateColumn);
            base.Columns.Add(DescriptionColumn);
            base.Columns.Add(IsAnsiNullDefaultOnColumn);
            base.Columns.Add(IsAnsiNullsOnColumn);
            base.Columns.Add(IsAnsiPaddingOnColumn);
            base.Columns.Add(IsAnsiWarningsOnColumn);
            base.Columns.Add(IsArithabortOnColumn);
            base.Columns.Add(IsAutoCloseOnColumn);
            base.Columns.Add(IsAutoCreateStatsOnColumn);
            base.Columns.Add(IsAutoShrinkOnColumn);
            base.Columns.Add(IsAutoUpdateStatsAsyncOnColumn);
            base.Columns.Add(IsAutoUpdateStatsOnColumn);
            base.Columns.Add(IsCleanlyShutdownColumn);
            base.Columns.Add(IsConcatNullYieldsNullOnColumn);
            base.Columns.Add(IsCursorCloseOnCommitOnColumn);
            base.Columns.Add(IsDateCorrelationOnColumn);
            base.Columns.Add(IsDbChainingOnColumn);
            base.Columns.Add(IsFulltextEnabledColumn);
            base.Columns.Add(IsInStandbyColumn);
            base.Columns.Add(IsLocalCursorDefaultColumn);
            base.Columns.Add(IsMasterKeyEncryptedByServerColumn);
            base.Columns.Add(IsNumericRoundabortOnColumn);
            base.Columns.Add(IsParameterizationForcedColumn);
            base.Columns.Add(IsQuotedIdentifierOnColumn);
            base.Columns.Add(IsReadOnlyColumn);
            base.Columns.Add(IsRecursiveTriggersOnColumn);
            base.Columns.Add(IsSupplementalLoggingEnabledColumn);
            base.Columns.Add(IsTrustworthyOnColumn);
            base.Columns.Add(NamespaceColumn);
            base.Columns.Add(ObjectNameColumn);
            base.Columns.Add(PageVerifyOptionColumn);
            base.Columns.Add(PageVerifyOptionDescriptionColumn);
            base.Columns.Add(RecoveryModelColumn);
            base.Columns.Add(RecoveryModelDescriptionColumn);
            base.Columns.Add(StateColumn);
            base.Columns.Add(StateDescriptionColumn);
            base.Columns.Add(UserAccessColumn);
            base.Columns.Add(UserAccessDescriptionColumn);
        }

		#endregion Constructors 

		#region Methods (5) 

		#region Public Methods (3) 

        public void AddCatalogsRow(CatalogsRow row)
        {
            Rows.Add(row);
        }

        public CatalogsRow NewCatalogsRow()
        {
            return ((CatalogsRow)(NewRow()));
        }

        public void RemoveCatalogsRow(CatalogsRow row)
        {
            Rows.Remove(row);
        }

		#endregion Public Methods 
		#region Protected Methods (2) 

        protected override Type GetRowType()
        {
            return typeof(CatalogsRow);
        }

        protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
        {
            return new CatalogsRow(builder);
        }

		#endregion Protected Methods 

		#endregion Methods 
    }
}
