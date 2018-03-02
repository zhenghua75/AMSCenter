IF  not EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbOperStandardDel]') AND type in (N'U'))
begin
CREATE TABLE [dbo].[tbOperStandardDel](
	DelId int not null,
	[cnvcProductCode] [varchar](20) NOT NULL,
	[cnnSort] [int] NOT NULL,
	[cnvcStandard] [text] NULL,
	[cnvcKey] [text] NULL,
	 CONSTRAINT [PK_TBOPERSTANDARDDEL] PRIMARY KEY CLUSTERED 
	(
		DelId ASC,
		[cnvcProductCode] ASC,
		[cnnSort] ASC
	)
)
end