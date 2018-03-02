<%@ Page language="c#" Codebehind="wfmPackagesGoods.aspx.cs" AutoEventWireup="True" Inherits="AMSApp.paraconf.wfmPackagesGoods" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>wfmPackagesGoods</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="1" cellPadding="5" width="95%" border="0">
				<TR>
					<TD style="COLOR: #330033; FONT-SIZE: 15pt; FONT-WEIGHT: bold" align="center">套餐商品添加</TD>
				</TR>
				<TR>
					<TD style="COLOR: #3300ff; FONT-SIZE: 10pt; FONT-WEIGHT: bold; TEXT-DECORATION: underline"
						align="left"><asp:label id="Label4" runat="server" Font-Size="10pt">套餐编号：</asp:label><asp:label id="lblPackageId" runat="server" Font-Size="10pt"></asp:label><asp:label id="Label3" runat="server" Font-Size="10pt">，套餐名称：</asp:label><asp:label id="lblPackageName" runat="server" Font-Size="10pt"></asp:label><asp:label id="Label5" runat="server" Font-Size="10pt">，套餐单价：</asp:label><asp:label id="lblPackagePrice" runat="server" Font-Size="10pt"></asp:label></TD>
				</TR>
			</TABLE>
			<table cellSpacing="0" cellPadding="0" width="95%" border="1">
				<tr>
					<td>
						<TABLE id="Table2" cellSpacing="0" cellPadding="1" width="100%" border="0">
							<TR>
								<TD style="WIDTH: 42px"><asp:label id="Label1" runat="server" Font-Size="10pt" Width="40px">商品ID</asp:label></TD>
								<TD style="WIDTH: 127px"><asp:textbox id="txtGoodsID" runat="server" Font-Size="10pt" Width="112px" Height="24px"></asp:textbox></TD>
								<TD style="WIDTH: 53px"><asp:label id="Label2" runat="server" Font-Size="10pt" Width="56px">商品名称</asp:label></TD>
								<TD style="WIDTH: 118px"><FONT face="宋体"><asp:textbox id="txtGoodsName" runat="server" Font-Size="10pt" Width="112px" Height="24px"></asp:textbox></FONT></TD>
								<td style="WIDTH: 258px"></td>
								<TD><asp:button id="Button1" runat="server" Width="64px" Text="查询" onclick="Button1_Click"></asp:button><asp:button id="Button2" runat="server" Text="返回套餐管理" onclick="Button2_Click"></asp:button></TD>
								<td></td>
							</TR>
						</TABLE>
					</td>
				</tr>
			</table>
			<table cellSpacing="0" cellPadding="0" width="95%" border="1">
				<tr>
					<td align="center"><asp:datagrid id="DataGrid1" runat="server" Width="95%" AutoGenerateColumns="False" BorderColor="#CC9966"
							BorderStyle="None" BorderWidth="1px" BackColor="White" CellPadding="4">
							<FooterStyle ForeColor="#330099" BackColor="#FFFFCC"></FooterStyle>
							<SelectedItemStyle Font-Bold="True" ForeColor="#663399" BackColor="#FFCC66"></SelectedItemStyle>
							<ItemStyle ForeColor="#330099" BackColor="White"></ItemStyle>
							<HeaderStyle Font-Bold="True" ForeColor="#FFFFCC" BackColor="#990000"></HeaderStyle>
							<Columns>
								<asp:BoundColumn DataField="vcGoodsId" ReadOnly="True" HeaderText="商品编号"></asp:BoundColumn>
								<asp:BoundColumn DataField="vcGoodsName" ReadOnly="True" HeaderText="商品名称"></asp:BoundColumn>
								<asp:BoundColumn DataField="nPrice" ReadOnly="True" HeaderText="商品单价"></asp:BoundColumn>
								<asp:BoundColumn DataField="nPackagesPrice" HeaderText="套餐单价"></asp:BoundColumn>
								<asp:BoundColumn DataField="vcComments" HeaderText="数量"></asp:BoundColumn>
								<asp:EditCommandColumn ButtonType="LinkButton" UpdateText="更新" CancelText="取消" EditText="编辑"></asp:EditCommandColumn>
							</Columns>
							<PagerStyle HorizontalAlign="Center" ForeColor="#330099" BackColor="#FFFFCC"></PagerStyle>
						</asp:datagrid></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
