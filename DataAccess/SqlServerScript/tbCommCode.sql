if exists (select * from syscolumns where name='vcCommSign' and id=OBJECT_ID(N'[dbo].[tbCommCode]') and length=5)
begin
IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[tbCommCode]') AND name = N'Type_vcCommCode')
DROP INDEX [Type_vcCommCode] ON [dbo].[tbCommCode] WITH ( ONLINE = OFF )

alter table dbo.tbCommCode alter column vcCommSign varchar(20) not null

CREATE NONCLUSTERED INDEX [Type_vcCommCode] ON [dbo].[tbCommCode] 
(
	[vcCommName] ASC,
	[vcCommCode] ASC,
	[vcCommSign] ASC
)
end

if exists (select * from syscolumns where name='vcComments' and id=OBJECT_ID(N'[dbo].[tbCommCode]') and length=20)
begin

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[tbCommCode]') AND name = N'Type_vcCommCode')
DROP INDEX [Type_vcCommCode] ON [dbo].[tbCommCode] WITH ( ONLINE = OFF )

alter table dbo.tbCommCode alter column vcComments varchar(50) not null

CREATE NONCLUSTERED INDEX [Type_vcCommCode] ON [dbo].[tbCommCode] 
(
	[vcCommName] ASC,
	[vcCommCode] ASC,
	[vcCommSign] ASC
)
end