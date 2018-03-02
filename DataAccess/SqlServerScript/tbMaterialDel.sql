IF  not EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbMaterialDel]') AND type in (N'U'))
begin
CREATE TABLE [dbo].[tbMaterialDel](
    DelId int not null,
	[cnvcMaterialCode] [varchar](20) NOT NULL,
	[cnvcMaterialName] [varchar](40) NULL,
	[cnvcLeastUnit] [varchar](20) NULL,
	[cnnConversion] [numeric](10, 2) NULL,
	[cnvcUnit] [varchar](20) NULL,
	[cnvcStandardUnit] [varchar](20) NULL,
	[cnnStatdardCount] [numeric](10, 2) NULL,
	[cnnPrice] [numeric](10, 4) NULL,
	[cnvcProductType] [varchar](20) NULL,
	[cnvcProductClass] [varchar](20) NULL,
	[IsUse] [bit] NOT NULL,
	CONSTRAINT [PK_TBMATERIALDEL] PRIMARY KEY CLUSTERED 
	(
		DelId ASC,
		[cnvcMaterialCode] ASC
	)
)
end