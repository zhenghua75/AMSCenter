<%@ Page language="c#" Codebehind="wfmMakeLog.aspx.cs" AutoEventWireup="True" Inherits="AMSApp.zhenghua.Produce.wfmMakeLog" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>wfmMakeLog</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<script src="../scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
		<LINK href="../DataGrid.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout" bgColor="#feeff8">
		<form id="Form1" method="post" runat="server">
			<table align="center">
				<tr>
					<td><asp:label id="Label1" runat="server" CssClass="title">����Ԥ����</asp:label></td>
				</tr>
			</table>
			<table align="center">
				<tr>
					<td><asp:label id="Label7" runat="server" CssClass="lable">������ţ�</asp:label></td>
					<td><asp:textbox id="txtProduceSerialNo" runat="server" CssClass="textbox"></asp:textbox></td>
					<td><asp:label id="Label3" runat="server" CssClass="lable">������λ��</asp:label></td>
					<td><asp:dropdownlist id="ddlProduceDept" runat="server"></asp:dropdownlist></td>
					<td><asp:label CssClass="lable" id="Label2" runat="server">�������ڣ�</asp:label></td>
					<td><asp:textbox id="txtProduceDate" onfocus="WdatePicker()" runat="server" CssClass="textbox"></asp:textbox></td>
				</tr>
				<tr>
					<td colSpan="6" align="center" style="HEIGHT: 26px"><asp:button id="btnMakeLog" runat="server" Text="�������" CssClass="button" onclick="btnMakeLog_Click"></asp:button><asp:button id="btnMakeAdd" runat="server" Text="���ɼӵ����" onclick="btnMakeAdd_Click"></asp:button><asp:button id="btnMakeReduce" runat="server" Text="���ɼ������" onclick="btnMakeReduce_Click"></asp:button><asp:button id="btnReturn" runat="server" Text="����" onclick="btnReturn_Click"></asp:button></td>
				</tr>
				<tr>
					<td colspan="6" align="center"><asp:button id="btnQueryMake" runat="server" Text="����嵥" CssClass="button" onclick="btnQueryMake_Click"></asp:button>
						<asp:Button id="Button1" runat="server" Text="�ӵ�����嵥" CssClass="button" onclick="Button1_Click"></asp:Button>
						<asp:Button id="Button2" runat="server" Text="��������嵥" CssClass="button" onclick="Button2_Click"></asp:Button>
						<asp:Button id="Button3" runat="server" Text="���ȶ�" CssClass="button" onclick="Button3_Click"></asp:Button></td>
				</tr>
			</table>
			<table align="center" width="100%">
				<tr>
					<td align="center">
						<asp:DataGrid id="DataGrid1" runat="server" AutoGenerateColumns="False" AllowPaging="True" Caption="���"
							CssClass="datagrid" BorderWidth="1px" BorderColor="Black" PageSize="20">
							<FooterStyle BorderWidth="1px" CssClass="dg_footer"></FooterStyle>
							<SelectedItemStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_selected"></SelectedItemStyle>
							<AlternatingItemStyle CssClass="dg_alter"></AlternatingItemStyle>
							<ItemStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_item"></ItemStyle>
							<HeaderStyle BorderWidth="1px" BorderStyle="Solid" BorderColor="Black" CssClass="dg_header"></HeaderStyle>
							<Columns>
								<asp:BoundColumn DataField="cnnMakeSerialNo" HeaderText="������ˮ"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcGroupComments" HeaderText="������"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcMakeName" HeaderText="��������"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcOperIDComments" HeaderText="������"></asp:BoundColumn>
								<asp:BoundColumn DataField="cndOperDate" HeaderText="��������" DataFormatString="{0:D}"></asp:BoundColumn>
								<asp:HyperLinkColumn Text="ϸ��" DataNavigateUrlField="cnnMakeSerialNo" DataNavigateUrlFormatString="wfmMakeDetail.aspx?MakeType=0&amp;MakeSerialNo={0}"
									HeaderText="ϸ��"></asp:HyperLinkColumn>
							</Columns>
							<PagerStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_page" Mode="NumericPages"></PagerStyle>
						</asp:DataGrid></td>
				</tr>
			</table>
			<table align="center" width="100%">
				<tr>
					<td align="center">
						<asp:DataGrid id="Datagrid2" runat="server" AutoGenerateColumns="False" AllowPaging="True" Caption="�ӵ����"
							CssClass="datagrid" BorderWidth="1px" BorderColor="Black" PageSize="20">
							<FooterStyle BorderWidth="1px" CssClass="dg_footer"></FooterStyle>
							<SelectedItemStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_selected"></SelectedItemStyle>
							<AlternatingItemStyle CssClass="dg_alter"></AlternatingItemStyle>
							<ItemStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_item"></ItemStyle>
							<HeaderStyle BorderWidth="1px" BorderStyle="Solid" BorderColor="Black" CssClass="dg_header"></HeaderStyle>
							<Columns>
								<asp:BoundColumn DataField="cnnMakeSerialNo" HeaderText="������ˮ"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcGroupComments" HeaderText="������"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcMakeName" HeaderText="��������"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcOperIDComments" HeaderText="������"></asp:BoundColumn>
								<asp:BoundColumn DataField="cndOperDate" HeaderText="��������" DataFormatString="{0:D}"></asp:BoundColumn>
								<asp:HyperLinkColumn Text="ϸ��" DataNavigateUrlField="cnnMakeSerialNo" DataNavigateUrlFormatString="wfmMakeDetail.aspx?MakeType=1&amp;MakeSerialNo={0}"
									HeaderText="ϸ��"></asp:HyperLinkColumn>
							</Columns>
							<PagerStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_page" Mode="NumericPages"></PagerStyle>
						</asp:DataGrid></td>
				</tr>
			</table>
			<table align="center" width="100%">
				<tr>
					<td align="center">
						<asp:DataGrid id="Datagrid3" runat="server" AutoGenerateColumns="False" AllowPaging="True" Caption="�������"
							CssClass="datagrid" BorderWidth="1px" BorderColor="Black" PageSize="20">
							<FooterStyle BorderWidth="1px" CssClass="dg_footer"></FooterStyle>
							<SelectedItemStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_selected"></SelectedItemStyle>
							<AlternatingItemStyle CssClass="dg_alter"></AlternatingItemStyle>
							<ItemStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_item"></ItemStyle>
							<HeaderStyle BorderWidth="1px" BorderStyle="Solid" BorderColor="Black" CssClass="dg_header"></HeaderStyle>
							<Columns>
								<asp:BoundColumn DataField="cnnMakeSerialNo" HeaderText="������ˮ"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcGroupComments" HeaderText="������"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcMakeName" HeaderText="��������"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcOperIDComments" HeaderText="������"></asp:BoundColumn>
								<asp:BoundColumn DataField="cndOperDate" HeaderText="��������" DataFormatString="{0:D}"></asp:BoundColumn>
								<asp:HyperLinkColumn Text="ϸ��" DataNavigateUrlField="cnnMakeSerialNo" DataNavigateUrlFormatString="wfmMakeDetail.aspx?MakeType=2&amp;MakeSerialNo={0}"
									HeaderText="ϸ��"></asp:HyperLinkColumn>
							</Columns>
							<PagerStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_page" Mode="NumericPages"></PagerStyle>
						</asp:DataGrid></td>
				</tr>
			</table>
			<table align="center" width="100%">
				<tr>
					<td align="center">
						<asp:DataGrid id="DataGrid4" runat="server" AllowPaging="True" AutoGenerateColumns="False" CssClass="datagrid"
							BorderWidth="1px" BorderColor="Black" Caption="���ȶ�">
							<FooterStyle BorderWidth="1px" CssClass="dg_footer"></FooterStyle>
							<SelectedItemStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_selected"></SelectedItemStyle>
							<AlternatingItemStyle CssClass="dg_alter"></AlternatingItemStyle>
							<ItemStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_item"></ItemStyle>
							<HeaderStyle BorderWidth="1px" BorderStyle="Solid" BorderColor="Black" CssClass="dg_header"></HeaderStyle>
							<Columns>
								<asp:BoundColumn DataField="cnvcProductCode" HeaderText="��Ʒ����"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcProductName" HeaderText="��Ʒ����"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcUnit" HeaderText="��λ"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnnStorageCount" HeaderText="��ǰ�����"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnnDStorageCount" HeaderText="��Ҫ�����"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnnCount" HeaderText="��ֵ����"></asp:BoundColumn>
							</Columns>
							<PagerStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_page" Mode="NumericPages"></PagerStyle>
						</asp:DataGrid></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
