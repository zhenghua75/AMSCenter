IF  not EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbGoodsDeptPrice]') AND type in (N'U'))
begin
create table dbo.tbGoodsDeptPrice
(
    vcDeptID varchar(10) not null,
    vcGoodsID varchar(10) not null,    
    nPrice numeric(8,2) not null,
    vcComments varchar(50) null,
    CONSTRAINT [PK_TBGOODSDEPTPRICE] PRIMARY KEY CLUSTERED
    (
       vcGoodsID,
       vcDeptID
    ) 
)
end