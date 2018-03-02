IF  not EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbFormulaDel]') AND type in (N'U'))
begin
CREATE TABLE [dbo].[tbFormulaDel](
	DelId int not null,
	[cnvcProductCode] [varchar](20) NOT NULL,
	[cnvcProductName] [varchar](40) NULL,
	[cnvcProductType] [varchar](20) NULL,
	[cnvcProductClass] [varchar](20) NULL,
	[cnbProductImage] [image] NULL,
	[cnnMaterialCostSum] [decimal](10, 4) NULL,
	[cnnPackingCostSum] [decimal](10, 4) NULL,
	[cnnCostSum] [decimal](10, 4) NULL,
	[cnvcUnit] [varchar](20) NULL,
	[cnnPortionCount] [numeric](10, 2) NULL,
	[cnvcPortionUnit] [varchar](20) NULL,
	[cnvcFeel] [text] NULL,
	[cnvcOrganise] [text] NULL,
	[cnvcColor] [text] NULL,
	[cnvcTaste] [text] NULL,
	[IsUse] [bit] NOT NULL,
	CONSTRAINT [PK_TBFORMULADEL] PRIMARY KEY CLUSTERED 
	(
		DelId ASC,
		[cnvcProductCode] ASC
	)
)
end