IF  not EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbDosageDel]') AND type in (N'U'))
begin
CREATE TABLE [dbo].[tbDosageDel](
	DelId int not null,
	[cnvcProductCode] [varchar](20) NOT NULL,
	[cnvcProductType] [varchar](20) NULL,
	[cnvcCode] [varchar](20) NOT NULL,
	[cnvcName] [varchar](40) NULL,
	[cnvcUnit] [varchar](20) NULL,
	[cnnCount] [numeric](10, 4) NULL,
	[cnnPrice] [numeric](10, 4) NULL,
	[cnnSum] [numeric](10, 4) NULL,
	 CONSTRAINT [PK_TBDOSAGEDEL] PRIMARY KEY CLUSTERED 
	(
		DelId ASC,
		[cnvcProductCode] ASC,
		[cnvcCode] ASC
	)
)
end