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
					<td><asp:label id="Label1" runat="server" CssClass="title">生产预估打单</asp:label></td>
				</tr>
			</table>
			<table align="center">
				<tr>
					<td><asp:label id="Label7" runat="server" CssClass="lable">生产序号：</asp:label></td>
					<td><asp:textbox id="txtProduceSerialNo" runat="server" CssClass="textbox"></asp:textbox></td>
					<td><asp:label id="Label3" runat="server" CssClass="lable">生产单位：</asp:label></td>
					<td><asp:dropdownlist id="ddlProduceDept" runat="server"></asp:dropdownlist></td>
					<td><asp:label CssClass="lable" id="Label2" runat="server">生产日期：</asp:label></td>
					<td><asp:textbox id="txtProduceDate" onfocus="WdatePicker()" runat="server" CssClass="textbox"></asp:textbox></td>
				</tr>
				<tr>
					<td colSpan="6" align="center" style="HEIGHT: 26px"><asp:button id="btnMakeLog" runat="server" Text="生成制令单" CssClass="button" onclick="btnMakeLog_Click"></asp:button><asp:button id="btnMakeAdd" runat="server" Text="生成加单制令单" onclick="btnMakeAdd_Click"></asp:button><asp:button id="btnMakeReduce" runat="server" Text="生成减单制令单" onclick="btnMakeReduce_Click"></asp:button><asp:button id="btnReturn" runat="server" Text="返回" onclick="btnReturn_Click"></asp:button></td>
				</tr>
				<tr>
					<td colspan="6" align="center"><asp:button id="btnQueryMake" runat="server" Text="制令单清单" CssClass="button" onclick="btnQueryMake_Click"></asp:button>
						<asp:Button id="Button1" runat="server" Text="加单制令单清单" CssClass="button" onclick="Button1_Click"></asp:Button>
						<asp:Button id="Button2" runat="server" Text="减单制令单清单" CssClass="button" onclick="Button2_Click"></asp:Button>
						<asp:Button id="Button3" runat="server" Text="库存比对" CssClass="button" onclick="Button3_Click"></asp:Button></td>
				</tr>
			</table>
			<table align="center" width="100%">
				<tr>
					<td align="center">
						<asp:DataGrid id="DataGrid1" runat="server" AutoGenerateColumns="False" AllowPaging="True" Caption="制令单"
							CssClass="datagrid" BorderWidth="1px" BorderColor="Black" PageSize="20">
							<FooterStyle BorderWidth="1px" CssClass="dg_footer"></FooterStyle>
							<SelectedItemStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_selected"></SelectedItemStyle>
							<AlternatingItemStyle CssClass="dg_alter"></AlternatingItemStyle>
							<ItemStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_item"></ItemStyle>
							<HeaderStyle BorderWidth="1px" BorderStyle="Solid" BorderColor="Black" CssClass="dg_header"></HeaderStyle>
							<Columns>
								<asp:BoundColumn DataField="cnnMakeSerialNo" HeaderText="制令流水"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcGroupComments" HeaderText="生产组"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcMakeName" HeaderText="制令名称"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcOperIDComments" HeaderText="制令人"></asp:BoundColumn>
								<asp:BoundColumn DataField="cndOperDate" HeaderText="制令日期" DataFormatString="{0:D}"></asp:BoundColumn>
								<asp:HyperLinkColumn Text="细节" DataNavigateUrlField="cnnMakeSerialNo" DataNavigateUrlFormatString="wfmMakeDetail.aspx?MakeType=0&amp;MakeSerialNo={0}"
									HeaderText="细节"></asp:HyperLinkColumn>
							</Columns>
							<PagerStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_page" Mode="NumericPages"></PagerStyle>
						</asp:DataGrid></td>
				</tr>
			</table>
			<table align="center" width="100%">
				<tr>
					<td align="center">
						<asp:DataGrid id="Datagrid2" runat="server" AutoGenerateColumns="False" AllowPaging="True" Caption="加单制令单"
							CssClass="datagrid" BorderWidth="1px" BorderColor="Black" PageSize="20">
							<FooterStyle BorderWidth="1px" CssClass="dg_footer"></FooterStyle>
							<SelectedItemStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_selected"></SelectedItemStyle>
							<AlternatingItemStyle CssClass="dg_alter"></AlternatingItemStyle>
							<ItemStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_item"></ItemStyle>
							<HeaderStyle BorderWidth="1px" BorderStyle="Solid" BorderColor="Black" CssClass="dg_header"></HeaderStyle>
							<Columns>
								<asp:BoundColumn DataField="cnnMakeSerialNo" HeaderText="制令流水"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcGroupComments" HeaderText="生产组"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcMakeName" HeaderText="制令名称"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcOperIDComments" HeaderText="制令人"></asp:BoundColumn>
								<asp:BoundColumn DataField="cndOperDate" HeaderText="制令日期" DataFormatString="{0:D}"></asp:BoundColumn>
								<asp:HyperLinkColumn Text="细节" DataNavigateUrlField="cnnMakeSerialNo" DataNavigateUrlFormatString="wfmMakeDetail.aspx?MakeType=1&amp;MakeSerialNo={0}"
									HeaderText="细节"></asp:HyperLinkColumn>
							</Columns>
							<PagerStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_page" Mode="NumericPages"></PagerStyle>
						</asp:DataGrid></td>
				</tr>
			</table>
			<table align="center" width="100%">
				<tr>
					<td align="center">
						<asp:DataGrid id="Datagrid3" runat="server" AutoGenerateColumns="False" AllowPaging="True" Caption="减单制令单"
							CssClass="datagrid" BorderWidth="1px" BorderColor="Black" PageSize="20">
							<FooterStyle BorderWidth="1px" CssClass="dg_footer"></FooterStyle>
							<SelectedItemStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_selected"></SelectedItemStyle>
							<AlternatingItemStyle CssClass="dg_alter"></AlternatingItemStyle>
							<ItemStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_item"></ItemStyle>
							<HeaderStyle BorderWidth="1px" BorderStyle="Solid" BorderColor="Black" CssClass="dg_header"></HeaderStyle>
							<Columns>
								<asp:BoundColumn DataField="cnnMakeSerialNo" HeaderText="制令流水"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcGroupComments" HeaderText="生产组"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcMakeName" HeaderText="制令名称"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcOperIDComments" HeaderText="制令人"></asp:BoundColumn>
								<asp:BoundColumn DataField="cndOperDate" HeaderText="制令日期" DataFormatString="{0:D}"></asp:BoundColumn>
								<asp:HyperLinkColumn Text="细节" DataNavigateUrlField="cnnMakeSerialNo" DataNavigateUrlFormatString="wfmMakeDetail.aspx?MakeType=2&amp;MakeSerialNo={0}"
									HeaderText="细节"></asp:HyperLinkColumn>
							</Columns>
							<PagerStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_page" Mode="NumericPages"></PagerStyle>
						</asp:DataGrid></td>
				</tr>
			</table>
			<table align="center" width="100%">
				<tr>
					<td align="center">
						<asp:DataGrid id="DataGrid4" runat="server" AllowPaging="True" AutoGenerateColumns="False" CssClass="datagrid"
							BorderWidth="1px" BorderColor="Black" Caption="库存比对">
							<FooterStyle BorderWidth="1px" CssClass="dg_footer"></FooterStyle>
							<SelectedItemStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_selected"></SelectedItemStyle>
							<AlternatingItemStyle CssClass="dg_alter"></AlternatingItemStyle>
							<ItemStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_item"></ItemStyle>
							<HeaderStyle BorderWidth="1px" BorderStyle="Solid" BorderColor="Black" CssClass="dg_header"></HeaderStyle>
							<Columns>
								<asp:BoundColumn DataField="cnvcProductCode" HeaderText="产品编码"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcProductName" HeaderText="产品名称"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcUnit" HeaderText="单位"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnnStorageCount" HeaderText="当前库存量"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnnDStorageCount" HeaderText="需要库存量"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnnCount" HeaderText="差值数量"></asp:BoundColumn>
							</Columns>
							<PagerStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_page" Mode="NumericPages"></PagerStyle>
						</asp:DataGrid></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
