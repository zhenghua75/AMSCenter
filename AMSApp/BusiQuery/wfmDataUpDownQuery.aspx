<%@ Register TagPrefix="uc1" TagName="ucPageView" Src="../ucPageView.ascx" %>
<%@ Page language="c#" Codebehind="wfmDataUpDownQuery.aspx.cs" AutoEventWireup="True" Inherits="AMSApp.BusiQuery.wfmDataUpDownQuery" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>wfmDataUpDownQuery</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content=" JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<SCRIPT language="javascript" src="../js/calendar.js"></SCRIPT>
	</HEAD>
	<body MS_POSITIONING="GridLayout" onload="<%=strExcelPath%>" bgcolor="#feeff8">
		<FORM id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="1" cellPadding="5" width="95%" border="0">
				<TR>
					<TD style="FONT-WEIGHT: bold; FONT-SIZE: 15pt; COLOR: #330033" align="center">上下传数据日志查询</TD>
				</TR>
			</TABLE>
			<TABLE id="Table2" cellSpacing="1" cellPadding="1" width="95%" border="1">
				<TR>
					<TD>
						<TABLE id="Table3" cellSpacing="1" cellPadding="1" width="100%" border="0">
							<TR>
								<TD style="WIDTH: 205px" align="right"><FONT face="宋体">
										<asp:label id="Label1" runat="server" Font-Size="10pt">上下传类型</asp:label></FONT></TD>
								<TD style="WIDTH: 151px">
									<asp:dropdownlist id="ddlUpDownType" runat="server" Font-Size="10pt" Height="24px" Width="144px"></asp:dropdownlist></TD>
								<TD style="WIDTH: 171px" align="right">
									<asp:label id="Label5" runat="server" Font-Size="10pt">开始时间</asp:label></TD>
								<TD style="WIDTH: 156px"><INPUT id=txtBegin onfocus=HS_setDate(this) readOnly type=text size=11 value="<%=strBeginDate%>" name=txtBegin></TD>
								<TD>
									<asp:button id="btQuery" runat="server" Font-Size="10pt" Width="56px" Text="查询" onclick="btQuery_Click"></asp:button></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 205px" align="right">
									<asp:label id="Label2" runat="server" Font-Size="10pt">门店</asp:label></TD>
								<TD style="WIDTH: 151px">
									<asp:dropdownlist id="ddlDept" runat="server" Font-Size="10pt" Width="144px" Height="24px"></asp:dropdownlist></TD>
								<TD style="WIDTH: 171px" align="right">
									<asp:label id="Label4" runat="server" Font-Size="10pt">结束时间</asp:label></TD>
								<TD style="WIDTH: 156px"><INPUT id=txtEnd onfocus=HS_setDate(this) readOnly type=text size=11 value="<%=strEndDate%>" name=txtEnd></TD>
								<TD><FONT face="宋体">
										<asp:button id="btnExcel" runat="server" Font-Size="10pt" Width="56px" Text="导出"></asp:button></FONT></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
			<TABLE id="Table4" cellSpacing="1" cellPadding="1" width="95%" border="0">
				<TR>
					<TD align="center">
						<uc1:ucpageview id="UcPageView1" runat="server" Visible="true"></uc1:ucpageview></TD>
				</TR>
			</TABLE>
		</FORM>
	</body>
</HTML>
