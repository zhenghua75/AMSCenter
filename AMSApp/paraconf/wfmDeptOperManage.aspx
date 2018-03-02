<%@ Page language="c#" Codebehind="wfmDeptOperManage.aspx.cs" AutoEventWireup="True" Inherits="AMSApp.paraconf.wfmDeptOperManage" %>
<%@ Register TagPrefix="uc1" TagName="ucPageView" Src="../ucPageView.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>wfmDeptOperManage</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK rel="stylesheet" href="../css/window.css">
	</HEAD>
	<body onload="<%=strExcelPath%>" 
MS_POSITIONING="GridLayout">
		<FORM id="Form1" method="post" runat="server">
			<TABLE id="Table3" border="0" cellSpacing="1" cellPadding="5" width="95%">
				<TR>
					<TD style="COLOR: #330033; FONT-SIZE: 15pt; FONT-WEIGHT: bold" align="center">客户端操作员管理</TD>
				</TR>
			</TABLE>
			<TABLE id="Table5" border="1" cellSpacing="0" cellPadding="0" width="95%">
				<TR>
					<TD>
						<TABLE id="Table2" border="0" cellSpacing="0" cellPadding="1" width="100%">
							<TR>
								<TD style="WIDTH: 42px" align="right"><asp:label id="Label1" runat="server" Font-Size="10pt" Width="88px">操作员名称</asp:label></TD>
								<TD style="WIDTH: 115px"><asp:textbox id="txtOperName" runat="server" Font-Size="10pt" Width="112px" Height="24px"></asp:textbox></TD>
								<TD style="WIDTH: 36px" align="right"><asp:label id="Label2" runat="server" Font-Size="10pt" Width="34px" Height="14px">部门</asp:label></TD>
								<TD style="WIDTH: 324px"><FONT face="宋体" style="Z-INDEX: 0"><asp:dropdownlist id="ddlDept" runat="server" Font-Size="10pt" Width="112px" AutoPostBack="True" Height="22px"></asp:dropdownlist>
										<asp:label id="Label3" runat="server" Width="32px" Font-Size="10pt" Height="14px">状态</asp:label><FONT style="Z-INDEX: 0" face="宋体">
											<asp:dropdownlist id="ddlOperState" runat="server" Width="96px" Font-Size="10pt" AutoPostBack="True"
												Height="22px"></asp:dropdownlist></FONT></FONT></TD>
								<TD style="WIDTH: 247px">
								<TD><asp:button id="btnQuery" runat="server" Width="64px" Text="查询" style="Z-INDEX: 0" onclick="btnQuery_Click"></asp:button></TD>
								<TD><asp:button id="btnAdd" runat="server" Width="64px" Text="添加" onclick="btnAdd_Click"></asp:button></TD>
								<TD><asp:button id="btnExcel" runat="server" Width="64px" Text="导出"></asp:button></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
			<TABLE id="Table4" border="0" cellSpacing="1" cellPadding="1" width="95%">
				<TR>
					<TD align="center"><uc1:ucpageview id="UcPageView1" runat="server" Visible="true"></uc1:ucpageview></TD>
				</TR>
			</TABLE>
		</FORM>
	</body>
</HTML>
