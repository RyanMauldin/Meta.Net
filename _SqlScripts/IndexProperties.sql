
CREATE TABLE [dbo].[TestIndex1]
(
        [ID] int NOT NULL
  , [ID2] int NOT NULL
  , [ID3] int NOT NULL
  , [ID4] int NOT NULL
)

ALTER TABLE [dbo].[TestIndex1] ADD CONSTRAINT [IX_TestIndex1ID] UNIQUE CLUSTERED
(
        [ID] ASC
) WITH (PAD_INDEX  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF)

ALTER TABLE [dbo].[TestIndex1] ADD CONSTRAINT [IX_TestIndex1ID3] UNIQUE NONCLUSTERED
(
        [ID] ASC
) WITH (PAD_INDEX  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF)


CREATE TABLE [dbo].[TestIndex2]
(
        [ID] int NOT NULL
  , [ID2] int NOT NULL
  , [ID3] int NOT NULL
  , [ID4] int NOT NULL
)

CREATE INDEX [IX_TestIndex2ID] ON [dbo].[TestIndex2](ID2, ID3)
CREATE NONCLUSTERED INDEX [IX_TestIndexID4] ON [dbo].[TestIndex2](ID4)
CREATE CLUSTERED INDEX [IX_TestIndexID5] ON [dbo].[TestIndex2](ID)

CREATE TABLE [dbo].[TestIndex3]
(
        [ID] int NOT NULL
  , [ID2] int NOT NULL
  , [ID3] int NOT NULL
  , [ID4] int NOT NULL
)

CREATE UNIQUE CLUSTERED INDEX [IX_TestIndexID55] ON [dbo].[TestIndex3](ID)
CREATE UNIQUE NONCLUSTERED INDEX [IX_TestIndexWTF] ON [dbo].[TestIndex3](ID4, ID3, ID2)

CREATE TABLE [Test].[TestIndex2]
(
        [ID] int NOT NULL
  , [ID2] int NOT NULL
  , [ID3] int NOT NULL
  , [ID4] int NOT NULL
)

 
ALTER TABLE [Test].[TestIndex2] ADD CONSTRAINT [FK_TestIndex2_dbo_TestIndex3_ID] FOREIGN KEY ([ID4]) REFERENCES [dbo].[TestIndex3]([id]) ON DELETE CASCADE ON UPDATE NO ACTION;


CREATE TABLE [Test].[TestIdentity]
(
        [ID] int IDENTITY(1, 1) NOT NULL
  , [ID2] int NOT NULL
  , [ID3] int NOT NULL
  , [ID4] int NOT NULL
)

CREATE TABLE [Test].[TestIdentity2]
(
        [ID] int IDENTITY(1, 1) NOT NULL
  , [ID2] int NOT NULL
  , [ID3] int NOT NULL
  , [ID4] int NOT NULL
)

CREATE TABLE [Test].[TestIdentity3]
(
        [ID] int IDENTITY(1, 3) NOT NULL
  , [ID2] int NOT NULL
  , [ID3] int NOT NULL
  , [ID4] int NOT NULL
  , PRIMARY KEY (ID)
)

CREATE TABLE [Test].[TestIdentity4]
(
        [ID] int NOT NULL
  , [ID2] int NOT NULL
  , [ID3] int NOT NULL
  , [ID4] int NOT NULL
  , PRIMARY KEY (ID)
)

ALTER TABLE [Test].[TestIdentity2] ADD CONSTRAINT [FK_TestIdentity2_TestIdentity3_ID] FOREIGN KEY ([ID]) REFERENCES [Test].[TestIdentity3]([ID])

ALTER TABLE [Test].[TestIdentity3] ADD CONSTRAINT [FK_TestIdentity3_TestIdentity2_ID] FOREIGN KEY ([ID]) REFERENCES [Test].[TestIdentity2]([ID])

ALTER TABLE [Test].[TestIdentity3] ADD CONSTRAINT [FK_TestIdentity3_TestIdentity4_ID] FOREIGN KEY ([ID]) REFERENCES [Test].[TestIdentity4]([ID]) ON DELETE CASCADE ON UPDATE NO ACTION;

CREATE UNIQUE CLUSTERED INDEX [IX_TestIndexID55] ON [Test].[TestIndex2](ID)
CREATE NONCLUSTERED INDEX [IX_TestIndexIncludes] ON [Test].[TestIndex2]([ID2]) INCLUDE ([ID3], [ID4])
CREATE UNIQUE NONCLUSTERED INDEX [IX_TestIndexUniqueIncludes] ON [Test].[TestIndex2]([ID3]) INCLUDE ([ID2], [ID4])

CREATE TABLE [Test].[MarketData]
(
    [ID] [bigint] IDENTITY(1,1) NOT NULL
  , [Ticker] [nvarchar](5) NOT NULL
  , [Date] [datetime] NOT NULL
  , [DayHigh] [decimal](38, 6) NOT NULL
  , [DayLow] [decimal](38, 6) NOT NULL
  , [DayOpen] [decimal](38, 6) NOT NULL
  , [Volume] [bigint] NOT NULL
  , [DayClose] [decimal](38, 6) NOT NULL
  , [DayAdjustedClose] [decimal](38, 6) NOT NULL
    -- PERSISTED COMPUTED COLUMN --
  , [DayType] AS
    (
        CASE
            WHEN volume > 200000000 and dayhigh-daylow /daylow > .05 THEN 'heavy volatility'
            WHEN volume > 100000000 and dayhigh-daylow /daylow > .03 THEN 'volatile'
            WHEN volume > 50000000 and dayhigh-daylow /daylow > .01 THEN 'fair'
            ELSE 'light'
        END
    ) PERSISTED NOT NULL
) ON [PRIMARY]

CREATE TABLE CheckTbl (col1 int, col2 int);
GO
CREATE FUNCTION CheckFnctn()
RETURNS int
AS
BEGIN
   DECLARE @retval int
   SELECT @retval = COUNT(*) FROM CheckTbl
   RETURN @retval
END;
GO
ALTER TABLE CheckTbl
ADD CONSTRAINT chkRowCount CHECK (dbo.CheckFnctn() >= 1 );
GO

CREATE TABLE [Test].[TestCheck]
(
        [ID] int IDENTITY(1, 1) NOT NULL
  , [ID2] int NOT NULL
  , [ID3] int NOT NULL
  , [ID4] int NOT NULL CHECK (ID4 > 5)
)

ALTER TABLE [Test].[TestCheck] WITH NOCHECK
    ADD CONSTRAINT [CK__TestCheck__ID4__71B2B7D7] CHECK (ID4 > 6)

ALTER TABLE [Test].[TestCheck]
DROP CONSTRAINT [CK__TestCheck__ID4__71B2B7D7]



-- disable the index
-- ALTER INDEX [IX_TestIndex2ID] ON [dbo].[TestIndex2] DISABLE
-- enable the index
-- ALTER INDEX [IX_TestIndex2ID] ON [dbo].[TestIndex2] REBUILD


select *
FROM [sys].[indexes] ind
        INNER JOIN [sys].[objects] obj ON ind.[object_id] = obj.[object_id]
        INNER JOIN [sys].[schemas] sch ON obj.[schema_id] = sch.[schema_id]
        INNER JOIN [sys].[tables] tab ON  ind.[object_id] = tab.[object_id]
        INNER JOIN [sys].[index_columns] icol ON ind.[object_id] = icol.[object_id] AND ind.[index_id] = icol.[index_id]
        INNER JOIN [sys].[columns] col ON ind.[object_id] = col.[object_id] AND icol.[column_id] = col.[column_id]
        INNER JOIN [sys].[filegroups] flg ON ind.data_space_id = flg.data_space_id
where obj.is_ms_shipped = 0


SELECT 'INFORMATION_SCHEMA.TABLE_CONSTRAINTS' [VIEW]
SELECT * FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS ORDER BY CONSTRAINT_NAME

SELECT 'INFORMATION_SCHEMA.KEY_COLUMN_USAGE' [VIEW]
SELECT * FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE ORDER BY CONSTRAINT_NAME

SELECT * FROM [sys].[columns]
SELECT *
FROM [sys].[objects] obj
        INNER JOIN [sys].[index_columns] icol ON obj.[object_id] = icol.[object_id]
        INNER JOIN [sys].[columns] col ON icol.[object_id] = col.[object_id] AND icol.[column_id] = col.[column_id]
WHERE obj.[is_ms_shipped] = 0 and obj.[object_id] = 1635536910

SELECT * FROM [sys].[objects]
SELECT * FROM [sys].[schemas]
SELECT * FROM [sys].[indexes]
SELECT * FROM [sys].[index_columns]
SELECT * FROM [sys].[columns]
SELECT * FROM [sys].[database_principals]
SELECT * FROM [sys].[server_principals]
SELECT * FROM [sys].[data_spaces]
SELECT * FROM [sys].[filegroups]
SELECT * FROM [sys].[database_files]
SELECT * FROM [sys].[assembly_files]

SELECT * FROM [sys].[sql_dependencies]

-- use this to  determine if this is an identity columns (seed_value, increment_value)
SELECT * FROM [sys].[identity_columns]

SELECT * FROM [sys].[assemblies]
SELECT * FROM [sys].[assembly_files]
SELECT * FROM [sys].[assembly_modules]
SELECT * FROM [sys].[assembly_references]
SELECT * FROM [sys].[assembly_types]

SELECT * FROM [sys].[xml_indexes]
SELECT * FROM [sys].[xml_schema_attributes]
SELECT * FROM [sys].[xml_schema_collections]
SELECT * FROM [sys].[xml_schema_component_placements]
SELECT * FROM [sys].[xml_schema_components]
SELECT * FROM [sys].[xml_schema_elements]
SELECT * FROM [sys].[xml_schema_facets]
SELECT * FROM [sys].[xml_schema_model_groups]
SELECT * FROM [sys].[xml_schema_namespaces]
SELECT * FROM [sys].[xml_schema_types]
SELECT * FROM [sys].[xml_schema_wildcard_namespaces]
SELECT * FROM [sys].[xml_schema_wildcards]