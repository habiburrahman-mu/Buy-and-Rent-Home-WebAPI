/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.Properties ADD
	Status char(1) NOT NULL CONSTRAINT DF_Properties_Status DEFAULT 'N',
	IsDeleted bit NOT NULL CONSTRAINT DF_Properties_IsDeleted DEFAULT 0
GO
DECLARE @v sql_variant 
SET @v = N'A: Active,
D: Draft,
C: Complete'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Properties', NULL, NULL
GO
ALTER TABLE dbo.Properties SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
