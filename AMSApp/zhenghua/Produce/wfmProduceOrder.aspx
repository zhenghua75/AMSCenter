<%@ Page language="c#" Codebehind="wfmProduceOrder.aspx.cs" AutoEventWireup="True" Inherits="AMSApp.zhenghua.Produce.wfmProduceOrder" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>wfmProduceOrder</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<script src="../scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
		<LINK href="../DataGrid.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body bgColor="#feeff8" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table align="center">
				<tr>
					<td><asp:label id="lblTitle" runat="server" CssClass="title">�����ƻ���������</asp:label></td>
				</tr>
			</table>
			<table align="center">
				<tr>
					<td><asp:label id="Label7" runat="server" CssClass="lable">������ţ�</asp:label></td>
					<td colSpan="3"><asp:textbox id="txtProduceSerialNo" runat="server" CssClass="textbox"></asp:textbox></td>
				</tr>
				<tr>
					<td><asp:label id="Label3" runat="server" CssClass="lable">������λ��</asp:label></td>
					<td><asp:dropdownlist id="ddlProduceDept" runat="server"></asp:dropdownlist></td>
					<td><asp:label CssClass="lable" id="Label2" runat="server">�������ڣ�</asp:label></td>
					<td><asp:textbox id="txtProduceDate" onfocus="WdatePicker()" runat="server" CssClass="textbox"></asp:textbox></td>
				</tr>
				<tr>
					<td colSpan="4" align="center"><asp:label id="Label6" runat="server" CssClass="lable">����������������</asp:label></td>
				</tr>
				<tr>
					<td><FONT face="����"><asp:label id="Label4" runat="server" CssClass="lable">��ʼ���ڣ�</asp:label></FONT></td>
					<td><FONT face="����"><asp:textbox id="txtShipBeginDate" onfocus="WdatePicker()" runat="server"  CssClass="textbox"></asp:textbox></FONT></td>
					<td><asp:label id="Label5" runat="server" CssClass="lable">�������ڣ�</asp:label></td>
					<td><asp:textbox id="txtShipEndDate" onfocus="WdatePicker()" runat="server"  CssClass="textbox"></asp:textbox></td>
				</tr>
				<tr>
					<td colSpan="4" align="center"><asp:button id="btnLinkOrder" runat="server" Text="��������" CssClass="button" onclick="btnLinkOrder_Click"></asp:button><asp:button id="btnQueryOrder" runat="server" Text="�����嵥" CssClass="button" onclick="btnQueryOrder_Click"></asp:button><asp:button id="btnQueryProduct" runat="server" Text="��Ʒ�嵥" CssClass="button" onclick="btnQueryProduct_Click"></asp:button><asp:button id="btnModify" runat="server" Text="�޸�" CssClass="button" onclick="btnModify_Click"></asp:button><asp:button id="btnCancel" runat="server" Text="ȡ��" CssClass="button" onclick="btnCancel_Click"></asp:button><asp:button id="btnReturn" runat="server" Text="����" CssClass="button" onclick="btnReturn_Click"></asp:button></td>
				</tr>
			</table>
			<table align="center" width="100%">
				<tr>
					<td><asp:datagrid id="DataGrid1" runat="server" AllowPaging="True" AutoGenerateColumns="False" Caption="����"
							CssClass="datagrid" BorderWidth="1px" BorderColor="Black" PageSize="20">
							<FooterStyle BorderWidth="1px" CssClass="dg_footer"></FooterStyle>
							<SelectedItemStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_selected"></SelectedItemStyle>
							<AlternatingItemStyle CssClass="dg_alter"></AlternatingItemStyle>
							<ItemStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_item"></ItemStyle>
							<HeaderStyle BorderWidth="1px" BorderStyle="Solid" BorderColor="Black" CssClass="dg_header"></HeaderStyle>
							<Columns>
								<asp:BoundColumn DataField="cnnProduceSerialNo" HeaderText="�������"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnnOrderSerialNo" HeaderText="�������"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnnAddSerialNo" HeaderText="�ӵ����"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnnReduceSerialNo" HeaderText="�������"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcOrderDeptIDComments" HeaderText="��������"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcProduceDeptIDComments" HeaderText="��������"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcOrderTypeComments" HeaderText="��������"></asp:BoundColumn>
								<asp:BoundColumn DataField="cndShipDate" HeaderText="��������" DataFormatString="{0:d}"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcOrderOperIDComments" HeaderText="����Ա"></asp:BoundColumn>
								<asp:BoundColumn DataField="cndOrderDate" HeaderText="����ʱ��"></asp:BoundColumn>
							</Columns>
							<PagerStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_page" Mode="NumericPages"></PagerStyle>
						</asp:datagrid></td>
				</tr>
			</table>
			<table width="100%" align="center">
				<tr>
					<td align="center"><asp:datagrid id="DataGrid2" runat="server" AllowPaging="True" AutoGenerateColumns="False" Caption="��Ʒ"
							CssClass="datagrid" BorderWidth="1px" BorderColor="Black" PageSize="20">
							<FooterStyle BorderWidth="1px" CssClass="dg_footer"></FooterStyle>
							<SelectedItemStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_selected"></SelectedItemStyle>
							<AlternatingItemStyle CssClass="dg_alter"></AlternatingItemStyle>
							<ItemStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_item"></ItemStyle>
							<HeaderStyle BorderWidth="1px" BorderStyle="Solid" BorderColor="Black" CssClass="dg_header"></HeaderStyle>
							<Columns>
								<asp:BoundColumn DataField="cnvcCode" HeaderText="��Ʒ����"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcName" HeaderText="��Ʒ����"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcUnit" HeaderText="��λ"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnnCount" HeaderText="����"></asp:BoundColumn>
							</Columns>
							<PagerStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_page" Mode="NumericPages"></PagerStyle>
						</asp:datagrid></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
