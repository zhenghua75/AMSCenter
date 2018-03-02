if not exists (select * from syscolumns where name='IsDeptPrice' and id=OBJECT_ID(N'[dbo].[tbGoods]'))
begin
alter table dbo.tbGoods add IsDeptPrice bit not null default(0)
end