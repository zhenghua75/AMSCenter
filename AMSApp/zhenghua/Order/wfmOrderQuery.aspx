<%@ Page language="c#" Codebehind="wfmOrderQuery.aspx.cs" AutoEventWireup="True" Inherits="AMSApp.zhenghua.Order.wfmOrderQuery" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>wfmOrderQuery</title>
        <script src="../scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
		<LINK href="../DataGrid.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../../js/isInt.js"></script>
	</HEAD>
	<body MS_POSITIONING="GridLayout" bgcolor="#feeff8">
		<form id="Form1" method="post" runat="server">
			<table align="center">
				<tr>
					<td>
						<asp:Label id="Label13" runat="server" CssClass="title">订单维护</asp:Label></td>
				</tr>
			</table>
			<table align="center" width="100%">
				<tr>
					<td>
						<asp:Label id="Label10" runat="server" CssClass="lable">下单门市：</asp:Label></td>
					<td>
						<asp:DropDownList id="ddlSalesRoom" runat="server"></asp:DropDownList></td>
					<td>
						<asp:Label id="Label11" runat="server" CssClass="lable">生产单位：</asp:Label></td>
					<td>
						<asp:DropDownList id="ddlProduceDept" runat="server"></asp:DropDownList></td>
					<td>
						<asp:Label id="Label12" runat="server" CssClass="lable">订单类型：</asp:Label></td>
					<td>
						<asp:DropDownList id="ddlOrderType" runat="server" AutoPostBack="True" onselectedindexchanged="ddlOrderType_SelectedIndexChanged_1"></asp:DropDownList></td>
					<td><asp:label id="Label4" runat="server" CssClass="lable">订单状态：</asp:label></td>
					<td><asp:dropdownlist id="ddlOrderState" runat="server"></asp:dropdownlist></td>
				</tr>
				<tr>
					<td><asp:label id="Label9" runat="server" CssClass="lable">订单流水：</asp:label></td>
					<td><asp:textbox id="txtOrderSerialNo" runat="server" CssClass="textbox"></asp:textbox></td>
					<td><asp:label id="Label1" runat="server" CssClass="lable">订单操作员：</asp:label></td>
					<td><asp:dropdownlist id="ddlOrderOper" runat="server"></asp:dropdownlist></td>
					<td><asp:label id="Label2" runat="server" CssClass="lable">订单日期：</asp:label></td>
					<td><asp:textbox id="txtOrderDate" runat="server" onfocus="WdatePicker()"  Width="80px"
							CssClass="textbox"></asp:textbox></td>
					<td><asp:label id="Label3" runat="server" CssClass="lable">发货日期：</asp:label></td>
					<td><asp:textbox id="txtShipDate" runat="server" onfocus="WdatePicker()"  Width="80px"
							CssClass="textbox"></asp:textbox></td>
				</tr>
			</table>
			<table runat="server" id="tblCustom" align="center">
				<tr>
					<td><asp:label id="Label5" runat="server" CssClass="lable">客户姓名/单位：</asp:label></td>
					<td><asp:textbox id="txtCustomName" runat="server" TextMode="MultiLine" Height="70px" CssClass="textbox"></asp:textbox></td>
					<td><asp:label id="Label6" runat="server" CssClass="lable">送货地址：</asp:label></td>
					<td><asp:textbox id="txtShipAddress" runat="server" TextMode="MultiLine" Height="70px" CssClass="textbox"></asp:textbox></td>
				</tr>
				<tr>
					<td><asp:label id="Label7" runat="server" CssClass="lable">联系电话：</asp:label></td>
					<td><asp:textbox id="txtLinkPhone" runat="server" CssClass="textbox"></asp:textbox></td>
					<td><asp:label id="Label8" runat="server" CssClass="lable">要求到货时间：</asp:label></td>
					<td><asp:textbox id="txtArrivedDate" runat="server" ReadOnly="True" CssClass="textbox" Width="120px"></asp:textbox></td>
				</tr>
			</table>
			<table align="center">
				<tr>
					<td align="center"><asp:button id="btnQuery" runat="server" Text="查询" CssClass="button" onclick="btnQuery_Click"></asp:button><asp:button id="Button2" runat="server" Text="取消" CssClass="button" onclick="Button2_Click"></asp:button>
						<asp:Button id="btnPrint" runat="server" Text="明细打印" CssClass="button" onclick="btnPrint_Click"></asp:Button>
						<asp:Button id="btnSumPrint" runat="server" Text="汇总打印" onclick="btnSumPrint_Click"></asp:Button></td>
				</tr>
			</table>
			<table align="center" width="100%">
				<tr>
					<td><asp:datagrid id="DataGrid1" runat="server" AllowPaging="True" AutoGenerateColumns="False" Caption="订单"
							CssClass="datagrid" BorderWidth="1px" BorderColor="Black" PageSize="20" PagerStyle-Visible="False">
							<FooterStyle BorderWidth="1px" CssClass="dg_footer"></FooterStyle>
							<SelectedItemStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_selected"></SelectedItemStyle>
							<AlternatingItemStyle CssClass="dg_alter"></AlternatingItemStyle>
							<ItemStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_item"></ItemStyle>
							<HeaderStyle BorderWidth="1px" BorderStyle="Solid" BorderColor="Black" CssClass="dg_header"></HeaderStyle>
							<Columns>
								<asp:BoundColumn DataField="cnnOrderSerialNo" HeaderText="流水"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcOrderDeptIDComments" HeaderText="门市"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcProduceDeptIDComments" HeaderText="生产单位"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcOrderTypeComments" HeaderText="订单类型"></asp:BoundColumn>
								<asp:BoundColumn DataField="cndShipDate" HeaderText="发货日期" DataFormatString="{0:D}"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcOrderStateComments" HeaderText="状态"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcOrderOperIDComments" HeaderText="操作员"></asp:BoundColumn>
								<asp:BoundColumn DataField="cndOrderDate" HeaderText="订单时间"></asp:BoundColumn>
								<asp:HyperLinkColumn Text="细节" Target="_self" DataNavigateUrlField="cnnOrderSerialNo" DataNavigateUrlFormatString="wfmOrderQueryDetail.aspx?OperFlag=Detail&amp;OrderSerialNo={0}"
									HeaderText="细节"></asp:HyperLinkColumn>
								<asp:HyperLinkColumn Text="编辑" DataNavigateUrlField="cnnOrderSerialNo" DataNavigateUrlFormatString="wfmOrderQueryDetail.aspx?OperFlag=Edit&amp;OrderSerialNo={0}"
									HeaderText="编辑"></asp:HyperLinkColumn>
								<asp:TemplateColumn HeaderText="加单">
									<ItemTemplate>
										<asp:HyperLink id="HyperLink1" runat="server" Text="加单" Target="_self" NavigateUrl='<%# "wfmOrderAdd.aspx?OrderSerialNo="+DataBinder.Eval(Container.DataItem,"cnnOrderSerialNo") + "&OrderState="+DataBinder.Eval(Container.DataItem,"cnvcOrderState")%>'>
										</asp:HyperLink>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="减单">
									<ItemTemplate>
										<asp:HyperLink ID="HyperLink2" runat="server" Text="减单" NavigateUrl='<%# "wfmOrderReduce.aspx?OrderSerialNo="+DataBinder.Eval(Container.DataItem,"cnnOrderSerialNo") + "&OrderState="+DataBinder.Eval(Container.DataItem,"cnvcOrderState")%>' Target="_self">
										</asp:HyperLink>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn Visible="False" DataField="cnvcOrderState" HeaderText="订单状态"></asp:BoundColumn>
							</Columns>
							<PagerStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_page" Mode="NumericPages"></PagerStyle>
						</asp:datagrid></td>
				</tr>
				<tr id="FootBar" runat="server" name="FootBar">
						<td align="center">
						<asp:label id="lbPageLabel" runat="server" Font-Size="10pt"></asp:label>
						<asp:linkbutton id="btnFirst" onclick="PagerButtonClick" runat="server" Font-Name="verdana" Font-size="8pt"
				ForeColor="navy" CommandArgument="0" Text="首页"></asp:linkbutton>|
			<asp:linkbutton id="btnPrev" onclick="PagerButtonClick" runat="server" Font-Name="verdana" Font-size="8pt"
				ForeColor="navy" CommandArgument="prev" Text="上页"></asp:linkbutton>|
			<asp:linkbutton id="btnNext" onclick="PagerButtonClick" runat="server" Font-Name="verdana" Font-size="8pt"
				ForeColor="navy" CommandArgument="next" Text="下页"></asp:linkbutton>|
			<asp:linkbutton id="btnLast" onclick="PagerButtonClick" runat="server" Font-Name="verdana" Font-size="8pt"
				ForeColor="navy" CommandArgument="last" Text="尾页"></asp:linkbutton>| <font size="2">
				跳到第</font><input id="page_number" type="text" size="3" value="<%=DataGrid1.CurrentPageIndex+1%>" name="page_number" /><font size="2">页</font>
			<asp:linkbutton id="btnGo" onmouseover="javascript:if((!isInt(page_number.value))||(page_number.value<=0)){alert('跳转页码必须为正整数！');page_number.focus();return false;};"
				onclick="PagerButtonClick" runat="server" Font-Name="verdana" Font-size="8pt" ForeColor="navy"
				CommandArgument="jump" Text="GO">GO!</asp:linkbutton></FONT>
						</td>
					</tr>
			</table>
		</form>
	</body>
</HTML>
