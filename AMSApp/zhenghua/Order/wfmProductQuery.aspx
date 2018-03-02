<%@ Page language="c#" Codebehind="wfmProductQuery.aspx.cs" AutoEventWireup="True" Inherits="AMSApp.zhenghua.Order.wfmProductQuery" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>wfmProductQuery</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK rel="stylesheet" type="text/css" href="../DataGrid.css">
		<script language="javascript" src="../../js/isInt.js"></script>
        <style type="text/css">
            .style1
            {
                width: 83px;
            }
            .style2
            {
                width: 231px;
            }
        </style>
  </HEAD>
	<body bgColor="#feeff8" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table align="center">
				<tr>
					<td colSpan="5" align="center"><asp:label id="Label3" runat="server" CssClass="title">��Ʒ��ѯ</asp:label></td>
				</tr>
				<tr>
					<td class="style1"><asp:label id="Label1" runat="server" CssClass="lable">��Ʒ���룺</asp:label></td>
					<td class="style2"><asp:textbox id="txtProductCode" runat="server" CssClass="textbox"></asp:textbox></td>
					<td><asp:label id="Label2" runat="server" CssClass="lable">��Ʒ���ƣ�</asp:label></td>
					<td><asp:textbox id="txtProductName" runat="server" CssClass="textbox"></asp:textbox></td>
				</tr>
				<tr>
					<td class="style1"><asp:label id="Label5" runat="server" CssClass="lable">�޸İٷֱȣ�</asp:label></td>
					<td class="style2"><asp:textbox id="txtPercent" runat="server" CssClass="textbox"></asp:textbox><asp:label id="Label6" runat="server">%</asp:label></td>
					<td><asp:label id="Label4" runat="server" CssClass="lable">��ͬ������</asp:label></td>
					<td><asp:textbox id="txtCount" runat="server" CssClass="textbox"></asp:textbox></td>
				</tr>
				<tr>
					<td><asp:label id="Label7" runat="server" CssClass="lable">��Ʒ���ͣ�</asp:label></td>
					<td><asp:dropdownlist id="ddlProductType" runat="server" AutoPostBack="True" onselectedindexchanged="ddlProductType_SelectedIndexChanged"></asp:dropdownlist></td>
					<td><asp:label id="Label8" runat="server" CssClass="lable">��Ʒ���</asp:label></td>
                    <td><asp:dropdownlist id="ddlProductClass" runat="server"></asp:dropdownlist></td>
					<td colSpan="2"><asp:checkbox id="chkSame" runat="server" Text="�Ƿ�ʹ��ͬ������" AutoPostBack="True" oncheckedchanged="chkSame_CheckedChanged"></asp:checkbox></td>
				</tr>
				<tr>
					<td colSpan="4"><asp:button id="btnQuery" runat="server" CssClass="button" Text="��ѯ��Ʒ" onclick="btnQuery_Click"></asp:button><asp:button id="btnPercent" runat="server" CssClass="button" Text="�������޸�" onclick="btnPercent_Click"></asp:button><asp:button id="btnAddList" runat="server" CssClass="button" Text="��ͬ���������嵥" Width="114px" onclick="btnAddList_Click"></asp:button><asp:button id="btnBatchAddList" runat="server" Text="���������嵥" onclick="btnBatchAddList_Click"></asp:button><asp:button id="btnOrderDetail" runat="server" CssClass="button" Text="��Ʒ�嵥" onclick="btnOrderDetail_Click"></asp:button><asp:button id="btnCancel" runat="server" CssClass="button" Text="ȡ��" onclick="btnCancel_Click"></asp:button></td>
				</tr>
			</table>
			<table width="100%" align="center">
				<tr vAlign="top" align="center">
					<td><asp:datagrid id="DataGrid1" runat="server" CssClass="datagrid" AutoGenerateColumns="False" AllowPaging="True"
							BorderWidth="1px" BorderColor="Black" PageSize="20" PagerStyle-Visible="False">
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
								<asp:TemplateColumn HeaderText="����">
									<ItemTemplate>
										<asp:TextBox id="TextBox1" runat="server" Width="78px"></asp:TextBox>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="�����嵥">
									<ItemTemplate>
										<asp:Button id="Button3" Text="�����嵥" runat="server" CommandName="putin"></asp:Button>
									</ItemTemplate>
								</asp:TemplateColumn>
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
			<table width="100%" align="center">
				<tr vAlign="top" align="center">
					<td><asp:datagrid id="DataGrid2" runat="server" CssClass="datagrid" AutoGenerateColumns="False" AllowPaging="True"
							BorderWidth="1px" BorderColor="Black" PageSize="20">
							<FooterStyle BorderWidth="1px" CssClass="dg_footer"></FooterStyle>
							<SelectedItemStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_selected"></SelectedItemStyle>
							<AlternatingItemStyle CssClass="dg_alter"></AlternatingItemStyle>
							<ItemStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_item"></ItemStyle>
							<HeaderStyle BorderWidth="1px" BorderStyle="Solid" BorderColor="Black" CssClass="dg_header"></HeaderStyle>
							<Columns>
								<asp:BoundColumn DataField="cnvcProductTypeName" ReadOnly="True" HeaderText="��Ʒ����"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcProductCode" ReadOnly="True" HeaderText="��Ʒ����"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcProductName" ReadOnly="True" HeaderText="��Ʒ����"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcProduct_Statd" ReadOnly="True" HeaderText="���"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcUnit" ReadOnly="True" HeaderText="��λ"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnnPrice" ReadOnly="True" HeaderText="�۸�"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnnOrderCount" HeaderText="����"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnnSum" ReadOnly="True" HeaderText="�ϼƽ��"></asp:BoundColumn>
								<asp:EditCommandColumn ButtonType="LinkButton" UpdateText="����" HeaderText="�༭" CancelText="ȡ��" EditText="�༭"></asp:EditCommandColumn>
								<asp:ButtonColumn Text="ɾ��" HeaderText="ɾ��" CommandName="Delete"></asp:ButtonColumn>
							</Columns>
							<PagerStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_page" Mode="NumericPages"></PagerStyle>
						</asp:datagrid></td>
				</tr>
				<tr align="center">
					<td><asp:label id="lblSumText" runat="server"></asp:label></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
