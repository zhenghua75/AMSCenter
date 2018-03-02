<%@ Page language="c#" Codebehind="wfmProducePlanQueryMake.aspx.cs" AutoEventWireup="True" Inherits="AMSApp.zhenghua.Produce.wfmProducePlanQueryMake" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>wfmProducePlanQuery</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<script src="../scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
		<LINK href="../DataGrid.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout" bgcolor="#feeff8">
		<form id="Form1" method="post" runat="server">
			<table align="center">
				<tr>
					<td>
						<asp:Label id="Label1" runat="server" CssClass="title">Ԥ����</asp:Label></td>
				</tr>
			</table>
			<table align="center">
				<tr>
					<td>
						<asp:Label id="Label5" runat="server" CssClass="lable">������λ��</asp:Label></td>
					<td>
						<asp:DropDownList id="ddlProduceDept" runat="server"></asp:DropDownList></td>
					<td>
						<asp:Label id="Label2" runat="server" CssClass="lable">��ʼ���ڣ�</asp:Label></td>
					<td>
						<asp:TextBox id="txtProduceBeginDate" runat="server" ReadOnly="True" onfocus="WdatePicker()" CssClass="textbox"></asp:TextBox></td>
					<td>
						<asp:Label id="Label3" runat="server" CssClass="lable">�������ڣ�</asp:Label></td>
					<td>
						<asp:TextBox id="txtProduceEndDate" runat="server" ReadOnly="True" onfocus="WdatePicker()" CssClass="textbox"></asp:TextBox></td>
				</tr>
				<tr>
					<td colspan="6" align="center">
						<asp:Button id="btnQuery" runat="server" Text="��ѯ" CssClass="button" onclick="btnQuery_Click"></asp:Button>
						<asp:Button id="btnCancel" runat="server" Text="ȡ��" CssClass="button" onclick="btnCancel_Click"></asp:Button></td>
				</tr>
			</table>
			<table align="center" width="100%">
				<tr>
					<td align="center">
						<asp:DataGrid id="DataGrid1" runat="server" AutoGenerateColumns="False" AllowPaging="True" CssClass="datagrid"
							BorderWidth="1px" BorderColor="Black" PageSize="20">
							<FooterStyle BorderWidth="1px" CssClass="dg_footer"></FooterStyle>
							<SelectedItemStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_selected"></SelectedItemStyle>
							<AlternatingItemStyle CssClass="dg_alter"></AlternatingItemStyle>
							<ItemStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_item"></ItemStyle>
							<HeaderStyle BorderWidth="1px" BorderStyle="Solid" BorderColor="Black" CssClass="dg_header"></HeaderStyle>
							<Columns>
								<asp:BoundColumn DataField="cnnProduceSerialNo" ReadOnly="True" HeaderText="�������"></asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="cnvcProduceDeptID" HeaderText="��λID"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcProduceDeptIDComments" HeaderText="������λ"></asp:BoundColumn>
								<asp:BoundColumn DataField="cndProduceDate" HeaderText="��������" DataFormatString="{0:d}"></asp:BoundColumn>
								<asp:BoundColumn DataField="cndShipBeginDate" HeaderText="������ʼ����" DataFormatString="{0:d}"></asp:BoundColumn>
								<asp:BoundColumn DataField="cndShipEndDate" HeaderText="������������" DataFormatString="{0:d}"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcProduceStateComments" ReadOnly="True" HeaderText="״̬"></asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="cnvcProduceState" HeaderText="״̬����"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcOperIDComments" ReadOnly="True" HeaderText="����Ա"></asp:BoundColumn>
								<asp:BoundColumn DataField="cndOperDate" ReadOnly="True" HeaderText="����ʱ��"></asp:BoundColumn>
								<asp:HyperLinkColumn Text="��" DataNavigateUrlField="cnnProduceSerialNo" DataNavigateUrlFormatString="wfmMakeLog.aspx?OperType=Make&amp;ProduceSerialNo={0}"
									HeaderText="��"></asp:HyperLinkColumn>
							</Columns>
							<PagerStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_page" Mode="NumericPages"></PagerStyle>
						</asp:DataGrid></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>