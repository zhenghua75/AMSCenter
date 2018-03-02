IF  not EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DelSerial]') AND type in (N'U'))
begin
create table dbo.DelSerial
(
	Id int not null identity(1,1) primary key,	
	Fill char(1) not null,
)
end