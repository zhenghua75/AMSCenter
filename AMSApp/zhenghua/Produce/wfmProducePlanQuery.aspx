<%@ Page language="c#" Codebehind="wfmProducePlanQuery.aspx.cs" AutoEventWireup="True" Inherits="AMSApp.zhenghua.Produce.wfmProducePlanQuery" %>
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
						<asp:Label id="Label1" runat="server" CssClass="title">生产计划维护</asp:Label></td>
				</tr>
			</table>
			<table align="center">
				<tr>
					<td>
						<asp:Label id="Label5" runat="server" CssClass="lable">生产单位：</asp:Label></td>
					<td>
						<asp:DropDownList id="ddlProduceDept" runat="server"></asp:DropDownList></td>
					<td>
						<asp:Label id="Label2" runat="server" CssClass="lable">开始日期：</asp:Label></td>
					<td>
						<asp:TextBox id="txtProduceBeginDate" runat="server" ReadOnly="True" onfocus="WdatePicker()" CssClass="textbox"></asp:TextBox></td>
					<td>
						<asp:Label id="Label3" runat="server" CssClass="lable">结束日期：</asp:Label></td>
					<td>
						<asp:TextBox id="txtProduceEndDate" runat="server" ReadOnly="True" onfocus="WdatePicker()" CssClass="textbox"></asp:TextBox></td>
				</tr>
				<tr>
					<td colspan="6" align="center">
						<asp:Button id="btnQuery" runat="server" Text="查询" CssClass="button" onclick="btnQuery_Click"></asp:Button>
						<asp:Button id="btnCancel" runat="server" Text="取消" CssClass="button" onclick="btnCancel_Click"></asp:Button>
						<asp:Button id="btnAdd" runat="server" Text="添加" CssClass="button" onclick="btnAdd_Click"></asp:Button></td>
				</tr>
			</table>
			<table align="center" width="100%">
				<tr>
					<td>
						<asp:DataGrid id="DataGrid1" runat="server" AutoGenerateColumns="False" AllowPaging="True" CssClass="datagrid"
							BorderWidth="1px" BorderColor="Black" PageSize="20">
							<FooterStyle BorderWidth="1px" CssClass="dg_footer"></FooterStyle>
							<SelectedItemStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_selected"></SelectedItemStyle>
							<AlternatingItemStyle CssClass="dg_alter"></AlternatingItemStyle>
							<ItemStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_item"></ItemStyle>
							<HeaderStyle BorderWidth="1px" BorderStyle="Solid" BorderColor="Black" CssClass="dg_header"></HeaderStyle>
							<Columns>
								<asp:BoundColumn DataField="cnnProduceSerialNo" ReadOnly="True" HeaderText="生产序号"></asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="cnvcProduceDeptID" HeaderText="单位ID"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcProduceDeptIDComments" HeaderText="生产单位"></asp:BoundColumn>
								<asp:BoundColumn DataField="cndProduceDate" HeaderText="生产日期" DataFormatString="{0:d}"></asp:BoundColumn>
								<asp:BoundColumn DataField="cndShipBeginDate" HeaderText="发货开始日期" DataFormatString="{0:d}"></asp:BoundColumn>
								<asp:BoundColumn DataField="cndShipEndDate" HeaderText="发货结束日期" DataFormatString="{0:d}"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcProduceStateComments" ReadOnly="True" HeaderText="状态"></asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="cnvcProduceState" HeaderText="状态编码"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcOperIDComments" ReadOnly="True" HeaderText="操作员"></asp:BoundColumn>
								<asp:BoundColumn DataField="cndOperDate" ReadOnly="True" HeaderText="操作时间"></asp:BoundColumn>
								<asp:HyperLinkColumn Text="修改" DataNavigateUrlField="cnnProduceSerialNo" DataNavigateUrlFormatString="wfmProduceOrder.aspx?OperType=Edit&amp;ProduceSerialNo={0}"
									HeaderText="修改"></asp:HyperLinkColumn>
								<asp:HyperLinkColumn Text="订单" Target="_self" DataNavigateUrlField="cnnProduceSerialNo" DataNavigateUrlFormatString="wfmProduceOrder.aspx?OperType=Order&amp;ProduceSerialNo={0}"
									HeaderText="订单"></asp:HyperLinkColumn>
								<asp:HyperLinkColumn Text="加单" DataNavigateUrlField="cnnProduceSerialNo" DataNavigateUrlFormatString="wfmProduceOrder.aspx?OperType=Add&amp;ProduceSerialNo={0}"
									HeaderText="加单"></asp:HyperLinkColumn>
								<asp:HyperLinkColumn Text="减单" DataNavigateUrlField="cnnProduceSerialNo" DataNavigateUrlFormatString="wfmProduceOrder.aspx?OperType=Reduce&amp;ProduceSerialNo={0}"
									HeaderText="减单"></asp:HyperLinkColumn>
							</Columns>
							<PagerStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_page" Mode="NumericPages"></PagerStyle>
						</asp:DataGrid></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
