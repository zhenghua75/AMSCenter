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
					<td><asp:label id="Label1" runat="server" CssClass="title">分货</asp:label></td>
				</tr>
			</table>
			<table align="center">
				<tr>
					<td><asp:label id="Label7" runat="server" CssClass="lable">生产序号：</asp:label></td>
					<td><asp:textbox id="txtProduceSerialNo" runat="server" CssClass="textbox"></asp:textbox></td>
					<td><asp:label id="Label3" runat="server" CssClass="lable">生产单位：</asp:label></td>
					<td><asp:dropdownlist id="ddlProduceDept" runat="server"></asp:dropdownlist></td>
					<td><asp:label id="Label2" runat="server" CssClass="lable">生产日期：</asp:label></td>
					<td><asp:textbox id="txtProduceDate" CssClass="textbox" onfocus="WdatePicker()" runat="server" ></asp:textbox></td>
				</tr>
			</table>
			<table align="center">
				<tr>
					<td colspan="6" align="center">
						<asp:Label id="Label8" runat="server" CssClass="lable">查询条件</asp:Label></td>
				</tr>
				<tr>
					<td>
						<asp:Label id="Label9" runat="server" CssClass="lable">分货流水：</asp:Label></td>
					<td>
						<asp:DropDownList id="ddlAssignSerialNo" runat="server"></asp:DropDownList></td>
					<td>
						<asp:Label id="Label4" runat="server" CssClass="lable">订单部门</asp:Label></td>
					<td>
						<asp:DropDownList id="ddlOrderDept" runat="server"></asp:DropDownList></td>
					<td>
						<asp:Label id="Label5" runat="server" CssClass="lable">产品编码</asp:Label></td>
					<td>
						<asp:TextBox id="txtProductCode" runat="server" CssClass="textbox"></asp:TextBox></td>
					<td>
						<asp:Label id="Label6" runat="server" CssClass="lable">产品名称</asp:Label></td>
					<td>
						<asp:TextBox id="txtProductName" runat="server" CssClass="textbox"></asp:TextBox></td>
				</tr>
			</table>
			<table>
				<tr>
					<td colSpan="6" align="center" style="HEIGHT: 26px">
						<asp:button id="btnDivideGoods" runat="server" CssClass="button" Text="生成分货数据" onclick="btnDivideGoods_Click"></asp:button>
						<asp:button id="btnQueryGoods" runat="server" Text="分货凭条查询" CssClass="button" Width="84px" onclick="btnQueryGoods_Click"></asp:button>
						<asp:Button id="btnExcel" runat="server" Text="分货凭条导出EXCEL" onclick="btnExcel_Click"></asp:Button>
						<asp:button id="btnReturn" runat="server" Text="返回" CssClass="button" Width="91px" onclick="btnReturn_Click"></asp:button>
						<asp:TextBox id="txtProduceState" runat="server" Visible="False" Width="285px"></asp:TextBox>
						<asp:Button id="btnQuery" runat="server" CssClass="button" Text="分货结果查询" Width="130px" onclick="btnQuery_Click"></asp:Button>
						<asp:Button id="btnEdit" runat="server" Text="编辑" Visible="False" onclick="btnEdit_Click"></asp:Button>
						<asp:Button id="btnEndEdit" runat="server" Text="锁定编辑" Visible="False" onclick="btnEndEdit_Click"></asp:Button>
						<asp:Button id="btnEditConfirm" runat="server" Text="修改确认" style="Z-INDEX: 0" Visible="False" onclick="btnEditConfirm_Click"></asp:Button>
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
								<asp:BoundColumn DataField="cnnAssignSerialNo" ReadOnly="True" HeaderText="分货流水"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcShipDeptIDComments" ReadOnly="True" HeaderText="生产单位"></asp:BoundColumn>
								<asp:BoundColumn DataField="cndShipDate" ReadOnly="True" HeaderText="发货时间"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnnOrderSerialNo" ReadOnly="True" HeaderText="订单流水"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcReceiveDeptIDComments" ReadOnly="True" HeaderText="订单部门"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcOrderTypeComments" ReadOnly="True" HeaderText="订单类型"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcProductCode" ReadOnly="True" HeaderText="产品编码"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcProductName" ReadOnly="True" HeaderText="产品名称"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcUnit" ReadOnly="True" HeaderText="单位"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnnPrice" ReadOnly="True" HeaderText="价格"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnnOrderCount" ReadOnly="True" HeaderText="订单量"></asp:BoundColumn>
								<asp:TemplateColumn HeaderText="分货量">
									<ItemTemplate>
										<asp:TextBox style="Z-INDEX: 0" id=TextBox2 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.cnnCount") %>' Width="75px" Enabled="False">
										</asp:TextBox>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="cnnSum" ReadOnly="True" HeaderText="合计"></asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="cnvcOrderType" ReadOnly="True" HeaderText="订单类型ID"></asp:BoundColumn>
							</Columns>
							<PagerStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_page" Mode="NumericPages"></PagerStyle>
						</asp:DataGrid></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
