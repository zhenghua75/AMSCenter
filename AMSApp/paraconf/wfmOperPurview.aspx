<%@ Page language="c#" Codebehind="wfmOperPurview.aspx.cs" AutoEventWireup="True" Inherits="AMSApp.paraconf.wfmOperPurview" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>wfmOperPurview</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK rel="stylesheet" href="../css/window.css">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<FORM id="Form1" method="post" runat="server">
			<TABLE id="Table3" border="0" cellSpacing="1" cellPadding="5" width="95%">
				<TR>
					<TD style="COLOR: #330033; FONT-SIZE: 15pt; FONT-WEIGHT: bold" align="center">操作员权限管理</TD>
				</TR>
			</TABLE>
			<TABLE id="Table5" border="1" cellSpacing="0" cellPadding="0" width="95%">
				<TR>
					<TD>
						<TABLE id="Table2" border="0" cellSpacing="0" cellPadding="1" width="100%" bgColor="#99cccc"
							height="30">
							<TR>
								<td></td>
								<TD style="WIDTH: 42px" align="right"><asp:label id="Label1" runat="server" Font-Size="10pt" Width="88px">操作员名称：</asp:label></TD>
								<TD style="WIDTH: 191px"><asp:label id="lblOperName" runat="server" Font-Size="10pt" Width="240px"></asp:label></TD>
								<TD style="WIDTH: 53px" align="right"><asp:label id="Label2" runat="server" Font-Size="10pt" Width="56px">登录ID：</asp:label></TD>
								<TD style="WIDTH: 118px"><FONT face="宋体"><asp:label id="lblOperID" runat="server" Font-Size="10pt" Width="123px"></asp:label></FONT></TD>
								<td><asp:textbox id="txtCS" runat="server" Visible="False"></asp:textbox></td>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
			<TABLE id="Table4" border="0" cellSpacing="0" cellPadding="0" width="95%">
				<TR>
					<TD align="center">
						<TABLE id="Table1" border="1" cellSpacing="1" borderColor="#ffffff" cellPadding="0" width="100%">
							<TR>
								<TD width="20%"></TD>
								<TD style="FONT-SIZE: 10pt" width="40%">功能列表</TD>
								<TD width="20%"></TD>
								<TD width="20%"></TD>
							</TR>
							<TR>
								<TD width="20%"></TD>
								<TD width="40%"><asp:checkboxlist id="cblFunc" runat="server" Font-Size="10pt"></asp:checkboxlist></TD>
								<TD vAlign="middle" width="20%" align="center">
									<P><asp:button id="btnok" runat="server" Width="64px" Text="确  定" onclick="btnok_Click"></asp:button></P>
									<P><INPUT style="CURSOR: hand" onclick="javascript:window.history.back();" value="返  回" type="button"></P>
								</TD>
								<TD width="20%"></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
		</FORM>
	</body>
</HTML>
