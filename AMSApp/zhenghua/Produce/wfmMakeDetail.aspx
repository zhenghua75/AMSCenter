<%@ Page language="c#" Codebehind="wfmMakeDetail.aspx.cs" AutoEventWireup="True" Inherits="AMSApp.zhenghua.Produce.wfmMakeDetail" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>wfmMakeDetail</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK rel="stylesheet" type="text/css" href="../DataGrid.css">
		<style media="print">
			.Noprint { DISPLAY: none }
			.PageNext { PAGE-BREAK-AFTER: always }
		</style>
	</HEAD>
	<body MS_POSITIONING="GridLayout" bgColor="#feeff8">
		<form id="Form1" method="post" runat="server">
			<table align="center" class="NOPRINT">
				<tr>
					<td>
						<asp:Label id="Label1" runat="server" CssClass="title">���ϸ��</asp:Label></td>
				</tr>
			</table>
			<table align="center" class="NOPRINT">
				<tr>
					<td>
						<asp:Button id="btnExcel" runat="server" Text="����EXCEL" CssClass="button" onclick="btnExcel_Click"></asp:Button>
					<td>
						<OBJECT id="WebBrowser" height="0" width="0" classid="CLSID:8856F961-340A-11D0-A96B-00C04FD705A2"
							VIEWASTEXT>
							
						</OBJECT>					
						<input type="button" value="��ӡ" onclick="document.all.WebBrowser.ExecWB(6,1)" class="button">
						<input type="button" value="ֱ�Ӵ�ӡ" onclick="document.all.WebBrowser.ExecWB(6,6)" class="button">
						<input type="button" value="ҳ������" onclick="document.all.WebBrowser.ExecWB(8,1)" class="button">
						<input type="button" value="��ӡԤ��" onclick="document.all.WebBrowser.ExecWB(7,1)" class="button">
						<asp:Button id="btnReturn" runat="server" Text="����" CssClass="button" onclick="btnReturn_Click"></asp:Button></td>
					</td>
				</tr>
			</table>
			<table align="center" width="100%">
				<tr>
					<td align="center">
						<asp:DataGrid id="DataGrid1" runat="server" AutoGenerateColumns="False" ShowFooter="True" CssClass="datagrid"
							BorderWidth="1px" BorderColor="Black">
							<FooterStyle BorderWidth="1px" CssClass="dg_footer"></FooterStyle>
							<SelectedItemStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_selected"></SelectedItemStyle>
							<AlternatingItemStyle CssClass="dg_alter"></AlternatingItemStyle>
							<ItemStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_item"></ItemStyle>
							<HeaderStyle BorderWidth="1px" BorderStyle="Solid" BorderColor="Black" CssClass="dg_header"></HeaderStyle>
							<Columns>
								<asp:BoundColumn DataField="cnvcCode" HeaderText="��Ʒ����"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcName" HeaderText="��Ʒ����"></asp:BoundColumn>
								<asp:BoundColumn DataField="Speci" HeaderText="��Ʒ���"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcUnit" HeaderText="���ⵥλ"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnnCount" HeaderText="��������"></asp:BoundColumn>
								<asp:BoundColumn HeaderText="�ӵ�����"></asp:BoundColumn>
								<asp:BoundColumn HeaderText="��������"></asp:BoundColumn>
								<asp:BoundColumn HeaderText="ʵ������"></asp:BoundColumn>
							</Columns>
							<PagerStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_page" Mode="NumericPages"></PagerStyle>
						</asp:DataGrid></td>
				</tr>
			</table>
			<table align="center" width="100%">
				<tr>
					<td align="center">
						<asp:DataGrid id="Datagrid2" runat="server" AutoGenerateColumns="False" ShowFooter="True" CssClass="datagrid"
							BorderWidth="1px" BorderColor="Black">
							<FooterStyle BorderWidth="1px" CssClass="dg_footer"></FooterStyle>
							<SelectedItemStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_selected"></SelectedItemStyle>
							<AlternatingItemStyle CssClass="dg_alter"></AlternatingItemStyle>
							<ItemStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_item"></ItemStyle>
							<HeaderStyle BorderWidth="1px" BorderStyle="Solid" BorderColor="Black" CssClass="dg_header"></HeaderStyle>
							<Columns>
								<asp:BoundColumn DataField="cnvcCode" HeaderText="��Ʒ����"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcName" HeaderText="��Ʒ����"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcUnit" HeaderText="��λ"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnnCount" HeaderText="��������"></asp:BoundColumn>
							</Columns>
							<PagerStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_page" Mode="NumericPages"></PagerStyle>
						</asp:DataGrid></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
