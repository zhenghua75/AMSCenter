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
						<asp:Label id="Label13" runat="server" CssClass="title">����ά��</asp:Label></td>
				</tr>
			</table>
			<table align="center" width="100%">
				<tr>
					<td>
						<asp:Label id="Label10" runat="server" CssClass="lable">�µ����У�</asp:Label></td>
					<td>
						<asp:DropDownList id="ddlSalesRoom" runat="server"></asp:DropDownList></td>
					<td>
						<asp:Label id="Label11" runat="server" CssClass="lable">������λ��</asp:Label></td>
					<td>
						<asp:DropDownList id="ddlProduceDept" runat="server"></asp:DropDownList></td>
					<td>
						<asp:Label id="Label12" runat="server" CssClass="lable">�������ͣ�</asp:Label></td>
					<td>
						<asp:DropDownList id="ddlOrderType" runat="server" AutoPostBack="True" onselectedindexchanged="ddlOrderType_SelectedIndexChanged_1"></asp:DropDownList></td>
					<td><asp:label id="Label4" runat="server" CssClass="lable">����״̬��</asp:label></td>
					<td><asp:dropdownlist id="ddlOrderState" runat="server"></asp:dropdownlist></td>
				</tr>
				<tr>
					<td><asp:label id="Label9" runat="server" CssClass="lable">������ˮ��</asp:label></td>
					<td><asp:textbox id="txtOrderSerialNo" runat="server" CssClass="textbox"></asp:textbox></td>
					<td><asp:label id="Label1" runat="server" CssClass="lable">��������Ա��</asp:label></td>
					<td><asp:dropdownlist id="ddlOrderOper" runat="server"></asp:dropdownlist></td>
					<td><asp:label id="Label2" runat="server" CssClass="lable">�������ڣ�</asp:label></td>
					<td><asp:textbox id="txtOrderDate" runat="server" onfocus="WdatePicker()"  Width="80px"
							CssClass="textbox"></asp:textbox></td>
					<td><asp:label id="Label3" runat="server" CssClass="lable">�������ڣ�</asp:label></td>
					<td><asp:textbox id="txtShipDate" runat="server" onfocus="WdatePicker()"  Width="80px"
							CssClass="textbox"></asp:textbox></td>
				</tr>
			</table>
			<table runat="server" id="tblCustom" align="center">
				<tr>
					<td><asp:label id="Label5" runat="server" CssClass="lable">�ͻ�����/��λ��</asp:label></td>
					<td><asp:textbox id="txtCustomName" runat="server" TextMode="MultiLine" Height="70px" CssClass="textbox"></asp:textbox></td>
					<td><asp:label id="Label6" runat="server" CssClass="lable">�ͻ���ַ��</asp:label></td>
					<td><asp:textbox id="txtShipAddress" runat="server" TextMode="MultiLine" Height="70px" CssClass="textbox"></asp:textbox></td>
				</tr>
				<tr>
					<td><asp:label id="Label7" runat="server" CssClass="lable">��ϵ�绰��</asp:label></td>
					<td><asp:textbox id="txtLinkPhone" runat="server" CssClass="textbox"></asp:textbox></td>
					<td><asp:label id="Label8" runat="server" CssClass="lable">Ҫ�󵽻�ʱ�䣺</asp:label></td>
					<td><asp:textbox id="txtArrivedDate" runat="server" ReadOnly="True" CssClass="textbox" Width="120px"></asp:textbox></td>
				</tr>
			</table>
			<table align="center">
				<tr>
					<td align="center"><asp:button id="btnQuery" runat="server" Text="��ѯ" CssClass="button" onclick="btnQuery_Click"></asp:button><asp:button id="Button2" runat="server" Text="ȡ��" CssClass="button" onclick="Button2_Click"></asp:button>
						<asp:Button id="btnPrint" runat="server" Text="��ϸ��ӡ" CssClass="button" onclick="btnPrint_Click"></asp:Button>
						<asp:Button id="btnSumPrint" runat="server" Text="���ܴ�ӡ" onclick="btnSumPrint_Click"></asp:Button></td>
				</tr>
			</table>
			<table align="center" width="100%">
				<tr>
					<td><asp:datagrid id="DataGrid1" runat="server" AllowPaging="True" AutoGenerateColumns="False" Caption="����"
							CssClass="datagrid" BorderWidth="1px" BorderColor="Black" PageSize="20" PagerStyle-Visible="False">
							<FooterStyle BorderWidth="1px" CssClass="dg_footer"></FooterStyle>
							<SelectedItemStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_selected"></SelectedItemStyle>
							<AlternatingItemStyle CssClass="dg_alter"></AlternatingItemStyle>
							<ItemStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_item"></ItemStyle>
							<HeaderStyle BorderWidth="1px" BorderStyle="Solid" BorderColor="Black" CssClass="dg_header"></HeaderStyle>
							<Columns>
								<asp:BoundColumn DataField="cnnOrderSerialNo" HeaderText="��ˮ"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcOrderDeptIDComments" HeaderText="����"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcProduceDeptIDComments" HeaderText="������λ"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcOrderTypeComments" HeaderText="��������"></asp:BoundColumn>
								<asp:BoundColumn DataField="cndShipDate" HeaderText="��������" DataFormatString="{0:D}"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcOrderStateComments" HeaderText="״̬"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcOrderOperIDComments" HeaderText="����Ա"></asp:BoundColumn>
								<asp:BoundColumn DataField="cndOrderDate" HeaderText="����ʱ��"></asp:BoundColumn>
								<asp:HyperLinkColumn Text="ϸ��" Target="_self" DataNavigateUrlField="cnnOrderSerialNo" DataNavigateUrlFormatString="wfmOrderQueryDetail.aspx?OperFlag=Detail&amp;OrderSerialNo={0}"
									HeaderText="ϸ��"></asp:HyperLinkColumn>
								<asp:HyperLinkColumn Text="�༭" DataNavigateUrlField="cnnOrderSerialNo" DataNavigateUrlFormatString="wfmOrderQueryDetail.aspx?OperFlag=Edit&amp;OrderSerialNo={0}"
									HeaderText="�༭"></asp:HyperLinkColumn>
								<asp:TemplateColumn HeaderText="�ӵ�">
									<ItemTemplate>
										<asp:HyperLink id="HyperLink1" runat="server" Text="�ӵ�" Target="_self" NavigateUrl='<%# "wfmOrderAdd.aspx?OrderSerialNo="+DataBinder.Eval(Container.DataItem,"cnnOrderSerialNo") + "&OrderState="+DataBinder.Eval(Container.DataItem,"cnvcOrderState")%>'>
										</asp:HyperLink>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="����">
									<ItemTemplate>
										<asp:HyperLink ID="HyperLink2" runat="server" Text="����" NavigateUrl='<%# "wfmOrderReduce.aspx?OrderSerialNo="+DataBinder.Eval(Container.DataItem,"cnnOrderSerialNo") + "&OrderState="+DataBinder.Eval(Container.DataItem,"cnvcOrderState")%>' Target="_self">
										</asp:HyperLink>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn Visible="False" DataField="cnvcOrderState" HeaderText="����״̬"></asp:BoundColumn>
							</Columns>
							<PagerStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_page" Mode="NumericPages"></PagerStyle>
						</asp:datagrid></td>
				</tr>
				<tr id="FootBar" runat="server" name="FootBar">
						<td align="center">
						<asp:label id="lbPageLabel" runat="server" Font-Size="10pt"></asp:label>
						<asp:linkbutton id="btnFirst" onclick="PagerButtonClick" runat="server" Font-Name="verdana" Font-size="8pt"
				ForeColor="navy" CommandArgument="0" Text="��ҳ"></asp:linkbutton>|
			<asp:linkbutton id="btnPrev" onclick="PagerButtonClick" runat="server" Font-Name="verdana" Font-size="8pt"
				ForeColor="navy" CommandArgument="prev" Text="��ҳ"></asp:linkbutton>|
			<asp:linkbutton id="btnNext" onclick="PagerButtonClick" runat="server" Font-Name="verdana" Font-size="8pt"
				ForeColor="navy" CommandArgument="next" Text="��ҳ"></asp:linkbutton>|
			<asp:linkbutton id="btnLast" onclick="PagerButtonClick" runat="server" Font-Name="verdana" Font-size="8pt"
				ForeColor="navy" CommandArgument="last" Text="βҳ"></asp:linkbutton>| <font size="2">
				������</font><input id="page_number" type="text" size="3" value="<%=DataGrid1.CurrentPageIndex+1%>" name="page_number" /><font size="2">ҳ</font>
			<asp:linkbutton id="btnGo" onmouseover="javascript:if((!isInt(page_number.value))||(page_number.value<=0)){alert('��תҳ�����Ϊ��������');page_number.focus();return false;};"
				onclick="PagerButtonClick" runat="server" Font-Name="verdana" Font-size="8pt" ForeColor="navy"
				CommandArgument="jump" Text="GO">GO!</asp:linkbutton></FONT>
						</td>
					</tr>
			</table>
		</form>
	</body>
</HTML>
