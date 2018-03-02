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
					<td><asp:label id="lblTitle" runat="server" CssClass="title">生产计划关联订单</asp:label></td>
				</tr>
			</table>
			<table align="center">
				<tr>
					<td><asp:label id="Label7" runat="server" CssClass="lable">生产序号：</asp:label></td>
					<td colSpan="3"><asp:textbox id="txtProduceSerialNo" runat="server" CssClass="textbox"></asp:textbox></td>
				</tr>
				<tr>
					<td><asp:label id="Label3" runat="server" CssClass="lable">生产单位：</asp:label></td>
					<td><asp:dropdownlist id="ddlProduceDept" runat="server"></asp:dropdownlist></td>
					<td><asp:label CssClass="lable" id="Label2" runat="server">生产日期：</asp:label></td>
					<td><asp:textbox id="txtProduceDate" onfocus="WdatePicker()" runat="server" CssClass="textbox"></asp:textbox></td>
				</tr>
				<tr>
					<td colSpan="4" align="center"><asp:label id="Label6" runat="server" CssClass="lable">关联订单发货日期</asp:label></td>
				</tr>
				<tr>
					<td><FONT face="宋体"><asp:label id="Label4" runat="server" CssClass="lable">开始日期：</asp:label></FONT></td>
					<td><FONT face="宋体"><asp:textbox id="txtShipBeginDate" onfocus="WdatePicker()" runat="server"  CssClass="textbox"></asp:textbox></FONT></td>
					<td><asp:label id="Label5" runat="server" CssClass="lable">结束日期：</asp:label></td>
					<td><asp:textbox id="txtShipEndDate" onfocus="WdatePicker()" runat="server"  CssClass="textbox"></asp:textbox></td>
				</tr>
				<tr>
					<td colSpan="4" align="center"><asp:button id="btnLinkOrder" runat="server" Text="关联订单" CssClass="button" onclick="btnLinkOrder_Click"></asp:button><asp:button id="btnQueryOrder" runat="server" Text="订单清单" CssClass="button" onclick="btnQueryOrder_Click"></asp:button><asp:button id="btnQueryProduct" runat="server" Text="产品清单" CssClass="button" onclick="btnQueryProduct_Click"></asp:button><asp:button id="btnModify" runat="server" Text="修改" CssClass="button" onclick="btnModify_Click"></asp:button><asp:button id="btnCancel" runat="server" Text="取消" CssClass="button" onclick="btnCancel_Click"></asp:button><asp:button id="btnReturn" runat="server" Text="返回" CssClass="button" onclick="btnReturn_Click"></asp:button></td>
				</tr>
			</table>
			<table align="center" width="100%">
				<tr>
					<td><asp:datagrid id="DataGrid1" runat="server" AllowPaging="True" AutoGenerateColumns="False" Caption="订单"
							CssClass="datagrid" BorderWidth="1px" BorderColor="Black" PageSize="20">
							<FooterStyle BorderWidth="1px" CssClass="dg_footer"></FooterStyle>
							<SelectedItemStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_selected"></SelectedItemStyle>
							<AlternatingItemStyle CssClass="dg_alter"></AlternatingItemStyle>
							<ItemStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_item"></ItemStyle>
							<HeaderStyle BorderWidth="1px" BorderStyle="Solid" BorderColor="Black" CssClass="dg_header"></HeaderStyle>
							<Columns>
								<asp:BoundColumn DataField="cnnProduceSerialNo" HeaderText="生产序号"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnnOrderSerialNo" HeaderText="订单序号"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnnAddSerialNo" HeaderText="加单序号"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnnReduceSerialNo" HeaderText="减单序号"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcOrderDeptIDComments" HeaderText="订单部门"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcProduceDeptIDComments" HeaderText="生产部门"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcOrderTypeComments" HeaderText="订单类型"></asp:BoundColumn>
								<asp:BoundColumn DataField="cndShipDate" HeaderText="发货日期" DataFormatString="{0:d}"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcOrderOperIDComments" HeaderText="操作员"></asp:BoundColumn>
								<asp:BoundColumn DataField="cndOrderDate" HeaderText="操作时间"></asp:BoundColumn>
							</Columns>
							<PagerStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_page" Mode="NumericPages"></PagerStyle>
						</asp:datagrid></td>
				</tr>
			</table>
			<table width="100%" align="center">
				<tr>
					<td align="center"><asp:datagrid id="DataGrid2" runat="server" AllowPaging="True" AutoGenerateColumns="False" Caption="产品"
							CssClass="datagrid" BorderWidth="1px" BorderColor="Black" PageSize="20">
							<FooterStyle BorderWidth="1px" CssClass="dg_footer"></FooterStyle>
							<SelectedItemStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_selected"></SelectedItemStyle>
							<AlternatingItemStyle CssClass="dg_alter"></AlternatingItemStyle>
							<ItemStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_item"></ItemStyle>
							<HeaderStyle BorderWidth="1px" BorderStyle="Solid" BorderColor="Black" CssClass="dg_header"></HeaderStyle>
							<Columns>
								<asp:BoundColumn DataField="cnvcCode" HeaderText="产品代码"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcName" HeaderText="产品名称"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcUnit" HeaderText="单位"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnnCount" HeaderText="数量"></asp:BoundColumn>
							</Columns>
							<PagerStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_page" Mode="NumericPages"></PagerStyle>
						</asp:datagrid></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
