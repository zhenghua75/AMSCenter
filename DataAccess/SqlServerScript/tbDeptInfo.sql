IF  not EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbDeptInfo]') AND type in (N'U'))
begin
create table dbo.tbDeptInfo
(
	vcDeptId varchar(5) null,
	vcDeptName varchar(20) not null primary key,
	vcAddress text null,
	vcTel varchar(200) null,
	vcManager varchar(200) null,
	vcAdsl VARCHAR(200) NULL,
	vcVpn varchar(200) null
)	
end	

if not exists (select * from syscolumns where name='vcManagerPhone' and id=OBJECT_ID(N'[dbo].[tbDeptInfo]'))
begin
alter table dbo.tbDeptInfo add vcManagerPhone varchar(200) null
end

if not exists (select * from syscolumns where name='vcAdslPwd' and id=OBJECT_ID(N'[dbo].[tbDeptInfo]'))
begin
alter table dbo.tbDeptInfo add vcAdslPwd varchar(200) null
end

if not exists (select * from syscolumns where name='vcVpnPwd' and id=OBJECT_ID(N'[dbo].[tbDeptInfo]'))
begin
alter table dbo.tbDeptInfo add vcVpnPwd varchar(200) null
end