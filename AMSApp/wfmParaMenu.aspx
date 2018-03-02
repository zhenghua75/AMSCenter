<%@ Page language="c#" Codebehind="wfmParaMenu.aspx.cs" AutoEventWireup="True" Inherits="AMSApp.wfmParaMenu" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>wfmParaMenu</title>
		<meta name="vs_snapToGrid" content="False">
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body MS_POSITIONING="GridLayout" leftmargin="0" background="image/coolwp2.jpg">
		<TABLE id="tblParaMenu" cellSpacing="1" cellPadding="1" width="146" border="0" align="left"
			runat="server">
			<TR id="trnoprom" runat="server">
				<TD align="center" style="FONT-WEIGHT: bold; COLOR: #330033; HEIGHT: 38px" bgcolor="#ebf0ec">没有权限</TD>
			</TR>
			<TR id="trParaRefresh" runat="server">
				<TD align="center" style="HEIGHT: 38px" background="image/anniu.jpg"><A style="FONT-SIZE: 12pt; COLOR: #ffffff; TEXT-DECORATION: none" onclick="parent.right.location='paraconf/wfmParaRefresh.aspx'"
						href="javascript:">参数刷新</A></TD>
			</TR>
			<TR id="trGoods" runat="server">
				<TD align="center" style="HEIGHT: 38px" background="image/anniu.jpg"><A style="FONT-SIZE: 12pt; COLOR: #ffffff; TEXT-DECORATION: none" onclick="parent.right.location='paraconf/wfmGoods.aspx'"
						href="javascript:">商品管理</A></TD>
			</TR>
			<TR id="trLoginOper" runat="server">
				<TD align="center" style="HEIGHT: 38px" background="image/anniu.jpg"><A style="FONT-SIZE: 12pt; COLOR: #ffffff; TEXT-DECORATION: none" onclick="parent.right.location='paraconf/wfmLoginOper.aspx'"
						href="javascript:">网站操作员管理</A></TD>
			</TR>
			<TR id="trLoginPwd" runat="server">
				<TD align="center" style="HEIGHT: 38px" background="image/anniu.jpg"><A style="FONT-SIZE: 12pt; COLOR: #ffffff; TEXT-DECORATION: none" onclick="parent.right.location='paraconf/wfmLoginPwd.aspx'"
						href="javascript:">操作员密码修改</A></TD>
			</TR>
			<TR id="trNotice" runat="server">
				<TD align="center" style="HEIGHT: 38px" background="image/anniu.jpg"><A style="FONT-SIZE: 12pt; COLOR: #ffffff; TEXT-DECORATION: none" onclick="parent.right.location='paraconf/wfmNotice2.aspx'"
						href="javascript:">系统通知</A></TD>
			</TR>
			<TR id="trSysParaSet" runat="server">
				<TD align="center" style="HEIGHT: 38px" background="image/anniu.jpg"><A style="FONT-SIZE: 12pt; COLOR: #ffffff; TEXT-DECORATION: none" onclick="parent.right.location='paraconf/wfmSysParaSet.aspx'"
						href="javascript:">系统参数设定</A></TD>
			</TR>
			<TR id="trDeptManage" runat="server">
				<TD align="center" style="HEIGHT: 38px" background="image/anniu.jpg"><A style="FONT-SIZE: 12pt; COLOR: #ffffff; TEXT-DECORATION: none" onclick="parent.right.location='paraconf/wfmDeptManage.aspx'"
						href="javascript:">部门参数管理</A></TD>
			</TR>
			<TR id="trDeptOperManage" runat="server">
				<TD align="center" style="HEIGHT: 38px" background="image/anniu.jpg"><A style="FONT-SIZE: 12pt; COLOR: #ffffff; TEXT-DECORATION: none" onclick="parent.right.location='paraconf/wfmDeptOperManage.aspx'"
						href="javascript:">客户端操作员</A></TD>
			</TR>
			<TR id="trBook" runat="server">
				<TD align="center" style="HEIGHT: 38px" background="image/anniu.jpg"><A style="FONT-SIZE: 12pt; COLOR: #ffffff; TEXT-DECORATION: none" onclick="parent.right.location='zhenghua/wfmBook.aspx'"
						href="javascript:">留言本</A></TD>
			</TR>
            <TR id="trDeptInfo" runat="server">
				<TD align="center" style="HEIGHT: 38px" background="image/anniu.jpg"><A style="FONT-SIZE: 12pt; COLOR: #ffffff; TEXT-DECORATION: none" onclick="parent.right.location='paraconf/wfmDeptInfo.aspx'"
						href="javascript:">门店信息</A></TD>
			</TR>
		</TABLE>
	</body>
</HTML>
