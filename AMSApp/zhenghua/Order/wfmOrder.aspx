<%@ Page language="c#" Codebehind="wfmOrder.aspx.cs" AutoEventWireup="True" Inherits="AMSApp.zhenghua.Order.wfmOrder" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>wfmOrder</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<script src="../scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
		<LINK rel="stylesheet" type="text/css" href="../DataGrid.css">
	</HEAD>
	<body bgColor="#feeff8" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table align="center">
				<tr>
					<td><asp:label id="lblTitle" runat="server" CssClass="title">�µ�</asp:label></td>
				</tr>
			</table>
			<table id="tblOrder" align="center" runat="server">
				<tr>
					<td><asp:label id="Label2" runat="server" CssClass="lable">�µ����У�</asp:label></td>
					<td><asp:dropdownlist id="ddlSalesRoom" runat="server" CssClass="textbox"></asp:dropdownlist></td>
					<td><asp:label id="Label3" runat="server" CssClass="lable">������λ��</asp:label></td>
					<td><asp:dropdownlist id="ddlProduceDept" runat="server"></asp:dropdownlist></td>
					<td><asp:label id="Label4" runat="server" CssClass="lable">�������ͣ�</asp:label></td>
					<td><asp:dropdownlist id="ddlOrderType" runat="server" AutoPostBack="True" onselectedindexchanged="ddlOrderType_SelectedIndexChanged"></asp:dropdownlist></td>
					<td><asp:label id="Label5" runat="server" CssClass="lable">�������ڣ�</asp:label></td>
					<td><asp:textbox id="txtShipDate" onfocus="WdatePicker()" runat="server" CssClass="textbox"></asp:textbox></td>
				</tr>
			</table>
			<table id="tblCustom" align="center" runat="server">
				<tr>
					<td><asp:label id="Label6" runat="server" CssClass="lable">�ͻ�����/��λ��</asp:label></td>
					<td><asp:textbox id="txtCustomName" runat="server" Height="70px" TextMode="MultiLine" CssClass="textbox"></asp:textbox></td>
					<td><asp:label id="Label7" runat="server" CssClass="lable">�ͻ���ַ��</asp:label></td>
					<td><asp:textbox id="txtShipAddress" runat="server" Height="70px" TextMode="MultiLine" CssClass="textbox"></asp:textbox></td>
					<td>
						<asp:Label id="Label1" runat="server" CssClass="lable">����Ҫ��</asp:Label></td>
					<td>
						<asp:TextBox id="txtOrderComments" runat="server" TextMode="MultiLine" Height="70px" CssClass="textbox"></asp:TextBox></td>
				</tr>
				<tr>
					<td><asp:label id="Label8" runat="server" CssClass="lable">��ϵ�绰��</asp:label></td>
					<td><asp:textbox id="txtLinkPhone" runat="server" CssClass="textbox"></asp:textbox></td>
					<td><asp:label id="Label9" runat="server" CssClass="lable">Ҫ�󵽻�ʱ�䣺</asp:label></td>
					<td colSpan="3"><asp:textbox id="txtArrivedDate" runat="server" ReadOnly="True" Width="120px"></asp:textbox></td>
				</tr>
			</table>
			<table id="tblDetial" align="center" runat="server">
				<tr>
					<td><asp:datagrid id="DataGrid1" runat="server" Caption="��Ʒ�嵥" AllowPaging="True" AutoGenerateColumns="False"
							CssClass="datagrid" BorderWidth="1px" BorderColor="Black" PageSize="20">
							<FooterStyle BorderWidth="1px" CssClass="dg_footer"></FooterStyle>
							<SelectedItemStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_selected"></SelectedItemStyle>
							<AlternatingItemStyle CssClass="dg_alter"></AlternatingItemStyle>
							<ItemStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_item"></ItemStyle>
							<HeaderStyle BorderWidth="1px" BorderStyle="Solid" BorderColor="Black" CssClass="dg_header"></HeaderStyle>
							<Columns>
								<asp:BoundColumn DataField="cnvcProductTypeName" HeaderText="��Ʒ����"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcProductCode" HeaderText="��Ʒ����"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcProductName" HeaderText="��Ʒ����"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcProduct_Statd" HeaderText="���"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcUnit" HeaderText="��λ"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnnPrice" HeaderText="�۸�"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnnOrderCount" HeaderText="����"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnnSum" HeaderText="�ϼ�"></asp:BoundColumn>
							</Columns>
							<PagerStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_page" Mode="NumericPages"></PagerStyle>
						</asp:datagrid></td>
				</tr>
				<tr align="center">
					<td>
						<asp:Label id="lblSumText" runat="server"></asp:Label></td>
				</tr>
			</table>
			<table id="tblOper" align="center" runat="server">
				<tr>
					<td><asp:button id="btnOK" runat="server" CssClass="button" Text="ȷ��" onclick="btnOK_Click"></asp:button>
						<asp:Button id="btnCancel" runat="server" Text="ȡ��" CssClass="button" onclick="btnCancel_Click"></asp:Button></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
