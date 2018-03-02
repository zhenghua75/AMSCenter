IF  not EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbConsItemUndo]') AND type in (N'U'))
begin
create table dbo.tbConsItemUndo
(
	iSerial bigint not null,
	CreateDate datetime not null,
	vcOperName varchar(10) not null,
	vcDeptId varchar(5) not null,
	CONSTRAINT [PK_TBCONSITEMUNDO] PRIMARY KEY CLUSTERED 
	(
		iSerial ASC,
		vcDeptId ASC
	)
)
end