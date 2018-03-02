<%@ Register TagPrefix="uc2" TagName="ucPageView" Src="../ucPageView.ascx" %>
<%@ Page language="c#" Codebehind="wfmCashierDifSum.aspx.cs" AutoEventWireup="True" Inherits="AMSApp.zhenghua.Produce.wfmCashierDifSum" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>wfmCostReport</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
        <script src="scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
		<LINK href="DataGrid.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="1" cellPadding="5" width="95%" border="0">
				<TR>
					<TD style="FONT-WEIGHT: bold; FONT-SIZE: 15pt; COLOR: #330033" align="center">收银员收款差异统计表</TD>
				</TR>
			</TABLE>
			<TABLE id="Table2" cellSpacing="1" cellPadding="1" width="95%" border="1">
				<tr vAlign="top">
					<td align="center">
						<table cellSpacing="1" cellPadding="1" border="0">
							<TR>								
								<TD align="center"><asp:label id="Label2" runat="server">开始日期：</asp:label></TD>
								<td><asp:textbox id="txtBeginDate" onfocus="WdatePicker()" runat="server"></asp:textbox></td>
								<TD align="center"><asp:label id="Label1" runat="server">结束日期：</asp:label></TD>
								<td><asp:textbox id="txtEndDate" onfocus="WdatePicker()" runat="server"></asp:textbox></td>
								<td>
									<asp:Button id="Button2" runat="server" Text="查询" onclick="Button2_Click"></asp:Button></td>
								<td>
									<asp:Button id="Button1" runat="server" Text="导出" onclick="Button1_Click"></asp:Button></td>
							</TR>
						</table>
					</td>
				</tr>
				<tr vAlign="top">
					<td align="center">
					</td>
				</tr>
			</TABLE>
			<TABLE id="Table4" cellSpacing="1" cellPadding="1" width="95%" border="0">
				<TR>
					<TD align="center">
						<uc2:ucPageView id="UcPageView1" runat="server"></uc2:ucPageView></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
