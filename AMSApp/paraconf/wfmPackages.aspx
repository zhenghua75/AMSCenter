<%@ Page language="c#" Codebehind="wfmPackages.aspx.cs" AutoEventWireup="True" Inherits="AMSApp.paraconf.wfmPackages" %>
<%@ Register TagPrefix="uc1" TagName="ucPageView" Src="../ucPageView.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>wfmPackages</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="1" cellPadding="5" width="95%" border="0">
				<TR>
					<TD style="FONT-WEIGHT: bold; FONT-SIZE: 15pt; COLOR: #330033" align="center">套餐管理</TD>
				</TR>
				<tr>
					<td align="center"><asp:label id="Label4" runat="server" Font-Size="10pt">套餐编号：</asp:label><asp:label id="lblPackageId" runat="server" Font-Size="10pt"></asp:label><asp:label id="Label3" runat="server" Font-Size="10pt">，套餐名称：</asp:label><asp:label id="lblPackageName" runat="server" Font-Size="10pt"></asp:label><asp:label id="Label5" runat="server" Font-Size="10pt">，套餐单价：</asp:label>
						<asp:Label id="lblPackagePrice" runat="server" Font-Size="10pt"></asp:Label>
						<asp:Button id="btnAdd" runat="server" Text="添加" onclick="btnAdd_Click"></asp:Button>
						<asp:Button id="Button1" runat="server" Text="返回商品管理" onclick="Button1_Click"></asp:Button></td>
				</tr>
			</TABLE>
			<TABLE id="Table4" cellSpacing="1" cellPadding="1" width="95%" border="0">
				<TR>
					<TD align="center"><uc1:ucpageview id="UcPageView1" runat="server" Visible="true"></uc1:ucpageview></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
