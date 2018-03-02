if not exists (select * from syscolumns where name='IsUse' and id=OBJECT_ID(N'[dbo].[tbMaterial]'))
begin
alter table dbo.tbMaterial add IsUse bit not null default(1)
end