<%@ Page language="c#" Codebehind="wfmFormulaQuery.aspx.cs" AutoEventWireup="True" Inherits="AMSApp.zhenghua.Formula.wfmFormulaQuery" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>wfmFormulaQuery</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../DataGrid.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../../js/isInt.js"></script>
  </HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table align="center">
				<tr>
					<td align="center">
						<asp:Label id="Label5" runat="server" CssClass="title">�䷽ά��</asp:Label></td>
				</tr>
			</table>
			<table align="center">
				<tr>
					<td><asp:label id="Label4" runat="server" CssClass="lable">��Ʒ���ͣ�</asp:label></td>
					<td><asp:dropdownlist id="ddlProductType" runat="server" AutoPostBack="True" onselectedindexchanged="ddlProductType_SelectedIndexChanged"></asp:dropdownlist></td>
					<td><asp:label id="Label1" runat="server" CssClass="lable">��Ʒ���</asp:label></td>
					<td><asp:dropdownlist id="ddlProductClass" runat="server"></asp:dropdownlist></td>
                    <td><asp:label id="Label6" runat="server" CssClass="lable">��Ʒ״̬��</asp:label></td>
					<td><asp:dropdownlist id="ddlProductIsUse" runat="server"></asp:dropdownlist></td>
					<td><asp:label id="Label2" runat="server" CssClass="lable">��Ʒ���룺</asp:label></td>
					<td><asp:textbox id="txtProductCode" runat="server" CssClass="textbox"></asp:textbox></td>
					<td><asp:label id="Label3" runat="server" CssClass="lable">��Ʒ���ƣ�</asp:label></td>
					<td><asp:textbox id="txtProductName" runat="server" CssClass="textbox"></asp:textbox></td>
				</tr>
				<tr>
					<td colspan="8" align="center">
						<asp:button id="btnQuery" runat="server" Text="��ѯ" CssClass="button" onclick="btnQuery_Click"></asp:button>
						<asp:button id="btnCancel" runat="server" Text="ȡ��" CssClass="button" onclick="btnCancel_Click"></asp:button>
						<asp:Button id="btnAdd" runat="server" Text="���" CssClass="button" onclick="btnAdd_Click"></asp:Button>
						<asp:Button id="btnCost" runat="server" Text="�ɱ�ˢ��" CssClass="button" onclick="btnCost_Click"></asp:Button>
						<asp:button style="Z-INDEX: 0" id="btnExcel" runat="server" Text="����" Width="56px" Font-Size="10pt"></asp:button>
                        <asp:Button ID="Button1" runat="server" CssClass="button" Text="�¼ܲ�Ʒ�Ƴ�" 
                            onclick="Button1_Click" Width="124px" />
					</td>
				</tr>
			</table>
			<table width="100%">
				<tr>
					<td valign="top" align="center">
						<asp:DataGrid id="DataGrid1" runat="server" AutoGenerateColumns="False" AllowPaging="True" CssClass="datagrid"
							BorderWidth="1px" BorderColor="Black" PageSize="20" PagerStyle-Visible="False">
							<FooterStyle BorderWidth="1px" CssClass="dg_footer"></FooterStyle>
							<SelectedItemStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_selected"></SelectedItemStyle>
							<AlternatingItemStyle CssClass="dg_alter"></AlternatingItemStyle>
							<ItemStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_item"></ItemStyle>
							<HeaderStyle BorderWidth="1px" BorderStyle="Solid" BorderColor="Black" CssClass="dg_header"></HeaderStyle>
							<Columns>
								<asp:BoundColumn DataField="cnvcProductCode" HeaderText="��Ʒ����"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcProductName" HeaderText="��Ʒ����"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcProductTypeComments" HeaderText="��Ʒ����"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcProductClassComments" HeaderText="��Ʒ���"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnnCostSum" HeaderText="�ɱ�"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnnPrice" HeaderText="�۸�"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcUnit" HeaderText="��λ"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcFeel" HeaderText="�ڸ�"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcOrganise" HeaderText="��֯"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcColor" HeaderText="��ɫ"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcTaste" HeaderText="��ζ"></asp:BoundColumn>
                                <asp:BoundColumn DataField="IsUse" HeaderText="״̬"></asp:BoundColumn>
								<asp:HyperLinkColumn Text="ϸ��" Target="_self" DataNavigateUrlField="cnvcProductCode" DataNavigateUrlFormatString="wfmFormula.aspx?OperFlag=Detail&amp;ResetFlag=true&amp;ProductCode={0}" HeaderText="ϸ��"></asp:HyperLinkColumn>
								<asp:HyperLinkColumn Text="�༭" Target="_self" DataNavigateUrlField="cnvcProductCode" DataNavigateUrlFormatString="wfmFormula.aspx?OperFlag=Edit&amp;ResetFlag=true&amp;ProductCode={0}" HeaderText="�༭"></asp:HyperLinkColumn>
							</Columns>
							<PagerStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_page" Mode="NumericPages"></PagerStyle>
						</asp:DataGrid>
					</td>
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
