<%@ Register TagPrefix="uc1" TagName="ucPageView" Src="../ucPageView.ascx" %>
<%@ Page language="c#" Codebehind="wfmLoginOper.aspx.cs" AutoEventWireup="True" Inherits="AMSApp.paraconf.wfmLoginOper" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>wfmLoginOper</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body onload="<%=strExcelPath%>" bgColor=#feeff8 
MS_POSITIONING="GridLayout">
		<FONT face="宋体">
			<FORM id="Form1" method="post" runat="server">
				<TABLE id="Table3" border="0" cellSpacing="1" cellPadding="5" width="95%">
					<TR>
						<TD style="COLOR: #330033; FONT-SIZE: 15pt; FONT-WEIGHT: bold" align="center">网站操作员管理</TD>
					</TR>
				</TABLE>
				<TABLE id="Table5" border="1" cellSpacing="0" cellPadding="0" width="95%">
					<TR>
						<TD>
							<TABLE id="Table2" border="0" cellSpacing="0" cellPadding="1" width="100%">
								<TR>
									<TD style="WIDTH: 42px" align="right"><asp:label id="Label1" runat="server" Font-Size="10pt" Width="88px">操作员名称</asp:label></TD>
									<TD style="WIDTH: 127px"><asp:textbox id="txtLoginName" runat="server" Font-Size="10pt" Width="112px" Height="24px"></asp:textbox></TD>
									<TD style="WIDTH: 53px" align="right"><asp:label id="Label2" runat="server" Font-Size="10pt" Width="56px">部门</asp:label></TD>
									<TD style="WIDTH: 118px"><FONT face="宋体"><asp:dropdownlist id="ddlDept" runat="server" Font-Size="10pt" Width="168px" AutoPostBack="True"></asp:dropdownlist></FONT></TD>
									<TD style="WIDTH: 142px"></TD>
									<TD><asp:button id="Button1" runat="server" Width="64px" Text="查询" onclick="Button1_Click"></asp:button></TD>
									<TD><asp:button id="Button2" runat="server" Width="64px" Text="添加" onclick="Button2_Click"></asp:button></TD>
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
		</FONT>
	</body>
</HTML>
