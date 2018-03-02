<%@ Page language="c#" Codebehind="wfmDividGoods.aspx.cs" AutoEventWireup="True" Inherits="AMSApp.zhenghua.Produce.wfmDividGoods" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>wfmDividGoods</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../DataGrid.css" type="text/css" rel="stylesheet">
        <script src="../scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
	</HEAD>
	<body MS_POSITIONING="GridLayout" bgColor="#feeff8">
		<form id="Form1" method="post" runat="server">
			<table align="center">
				<tr>
					<td><asp:label id="Label1" runat="server" CssClass="title">�ֻ�</asp:label></td>
				</tr>
			</table>
			<table align="center">
				<tr>
					<td><asp:label id="Label7" runat="server" CssClass="lable">������ţ�</asp:label></td>
					<td><asp:textbox id="txtProduceSerialNo" runat="server" CssClass="textbox"></asp:textbox></td>
					<td><asp:label id="Label3" runat="server" CssClass="lable">������λ��</asp:label></td>
					<td><asp:dropdownlist id="ddlProduceDept" runat="server"></asp:dropdownlist></td>
					<td><asp:label id="Label2" runat="server" CssClass="lable">�������ڣ�</asp:label></td>
					<td><asp:textbox id="txtProduceDate" CssClass="textbox" onfocus="WdatePicker()" runat="server" ></asp:textbox></td>
				</tr>
			</table>
			<table align="center">
				<tr>
					<td colspan="6" align="center">
						<asp:Label id="Label8" runat="server" CssClass="lable">��ѯ����</asp:Label></td>
				</tr>
				<tr>
					<td>
						<asp:Label id="Label9" runat="server" CssClass="lable">�ֻ���ˮ��</asp:Label></td>
					<td>
						<asp:DropDownList id="ddlAssignSerialNo" runat="server"></asp:DropDownList></td>
					<td>
						<asp:Label id="Label4" runat="server" CssClass="lable">��������</asp:Label></td>
					<td>
						<asp:DropDownList id="ddlOrderDept" runat="server"></asp:DropDownList></td>
					<td>
						<asp:Label id="Label5" runat="server" CssClass="lable">��Ʒ����</asp:Label></td>
					<td>
						<asp:TextBox id="txtProductCode" runat="server" CssClass="textbox"></asp:TextBox></td>
					<td>
						<asp:Label id="Label6" runat="server" CssClass="lable">��Ʒ����</asp:Label></td>
					<td>
						<asp:TextBox id="txtProductName" runat="server" CssClass="textbox"></asp:TextBox></td>
				</tr>
			</table>
			<table>
				<tr>
					<td colSpan="6" align="center" style="HEIGHT: 26px">
						<asp:button id="btnDivideGoods" runat="server" CssClass="button" Text="���ɷֻ�����" onclick="btnDivideGoods_Click"></asp:button>
						<asp:button id="btnQueryGoods" runat="server" Text="�ֻ�ƾ����ѯ" CssClass="button" Width="84px" onclick="btnQueryGoods_Click"></asp:button>
						<asp:Button id="btnExcel" runat="server" Text="�ֻ�ƾ������EXCEL" onclick="btnExcel_Click"></asp:Button>
						<asp:button id="btnReturn" runat="server" Text="����" CssClass="button" Width="91px" onclick="btnReturn_Click"></asp:button>
						<asp:TextBox id="txtProduceState" runat="server" Visible="False" Width="285px"></asp:TextBox>
						<asp:Button id="btnQuery" runat="server" CssClass="button" Text="�ֻ������ѯ" Width="130px" onclick="btnQuery_Click"></asp:Button>
						<asp:Button id="btnEdit" runat="server" Text="�༭" Visible="False" onclick="btnEdit_Click"></asp:Button>
						<asp:Button id="btnEndEdit" runat="server" Text="�����༭" Visible="False" onclick="btnEndEdit_Click"></asp:Button>
						<asp:Button id="btnEditConfirm" runat="server" Text="�޸�ȷ��" style="Z-INDEX: 0" Visible="False" onclick="btnEditConfirm_Click"></asp:Button>
					</td>
				</tr>
			</table>
			<TABLE width="100%" align="center">
				<TR>
					<TD align="center">
						<asp:DataGrid id="DataGrid1" runat="server" CssClass="datagrid" BorderColor="Black" BorderWidth="1px">
							<FooterStyle BorderWidth="1px" CssClass="dg_footer"></FooterStyle>
							<SelectedItemStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_selected"></SelectedItemStyle>
							<AlternatingItemStyle CssClass="dg_alter"></AlternatingItemStyle>
							<ItemStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_item"></ItemStyle>
							<HeaderStyle BorderWidth="1px" BorderStyle="Solid" BorderColor="Black" CssClass="dg_header"></HeaderStyle>
							<PagerStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_page" Mode="NumericPages"></PagerStyle>
						</asp:DataGrid></TD>
				</TR>
			</TABLE>
			<TABLE width="100%" align="center">
				<TR>
					<TD align="center">
						<asp:DataGrid id="DataGrid2" runat="server" CssClass="datagrid" BorderColor="Black" BorderWidth="1px"
							PageSize="20" AutoGenerateColumns="False">
							<FooterStyle BorderWidth="1px" CssClass="dg_footer"></FooterStyle>
							<SelectedItemStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_selected"></SelectedItemStyle>
							<AlternatingItemStyle CssClass="dg_alter"></AlternatingItemStyle>
							<ItemStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_item"></ItemStyle>
							<HeaderStyle BorderWidth="1px" BorderStyle="Solid" BorderColor="Black" CssClass="dg_header"></HeaderStyle>
							<Columns>
								<asp:BoundColumn DataField="cnnAssignSerialNo" ReadOnly="True" HeaderText="�ֻ���ˮ"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcShipDeptIDComments" ReadOnly="True" HeaderText="������λ"></asp:BoundColumn>
								<asp:BoundColumn DataField="cndShipDate" ReadOnly="True" HeaderText="����ʱ��"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnnOrderSerialNo" ReadOnly="True" HeaderText="������ˮ"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcReceiveDeptIDComments" ReadOnly="True" HeaderText="��������"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcOrderTypeComments" ReadOnly="True" HeaderText="��������"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcProductCode" ReadOnly="True" HeaderText="��Ʒ����"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcProductName" ReadOnly="True" HeaderText="��Ʒ����"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcUnit" ReadOnly="True" HeaderText="��λ"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnnPrice" ReadOnly="True" HeaderText="�۸�"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnnOrderCount" ReadOnly="True" HeaderText="������"></asp:BoundColumn>
								<asp:TemplateColumn HeaderText="�ֻ���">
									<ItemTemplate>
										<asp:TextBox style="Z-INDEX: 0" id=TextBox2 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.cnnCount") %>' Width="75px" Enabled="False">
										</asp:TextBox>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="cnnSum" ReadOnly="True" HeaderText="�ϼ�"></asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="cnvcOrderType" ReadOnly="True" HeaderText="��������ID"></asp:BoundColumn>
							</Columns>
							<PagerStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_page" Mode="NumericPages"></PagerStyle>
						</asp:DataGrid></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
