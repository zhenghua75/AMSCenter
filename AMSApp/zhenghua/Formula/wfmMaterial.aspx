<%@ Page language="c#" Codebehind="wfmMaterial.aspx.cs" AutoEventWireup="True" Inherits="AMSApp.zhenghua.Formula.wfmMaterial" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>wfmMaterial</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../DataGrid.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../../js/isInt.js"></script>
	</HEAD>
	<body MS_POSITIONING="GridLayout" bgcolor="#feeff8">
		<form id="Form1" method="post" runat="server">
			<table align="center">
				<tr>
					<td>
						<asp:Label id="Label5" runat="server" CssClass="title">ԭ�ϲ���ά��</asp:Label></td>
				</tr>
			</table>
			<table align="center">
				<tr>
					<td>
						<asp:Label id="Label3" runat="server" CssClass="lable">ԭ�ϱ��룺</asp:Label></td>
					<td>
						<asp:TextBox id="txtMaterialCode" runat="server" CssClass="textbox"></asp:TextBox></td>
					<td>
						<asp:Label id="Label4" runat="server" CssClass="lable">ԭ�����ƣ�</asp:Label></td>
					<td>
						<asp:TextBox id="txtMaterialName" runat="server" CssClass="textbox"></asp:TextBox></td>
				</tr>
				<tr>
					<td align="center" colspan="4">
						<asp:Button id="btnQuery" runat="server" Text="��ѯ" CssClass="button" onclick="btnQuery_Click"></asp:Button>
						<asp:Button id="btnCancel" runat="server" Text="ȡ��" CssClass="button" onclick="btnCancel_Click"></asp:Button>
						<asp:Button id="btnAdd" runat="server" Text="���" CssClass="button" onclick="btnAdd_Click"></asp:Button>
                        <asp:Button ID="Button1" runat="server" Text="�¼�ԭ�����Ƴ�" CssClass="button" 
                            onclick="Button1_Click" Width="108px"/>
                        </td>
				</tr>
			</table>
			<table width="100%">
				<tr>
					<td align="center">
						<asp:DataGrid id="DataGrid1" runat="server" AutoGenerateColumns="False" AllowPaging="True" AllowSorting="True"
							CssClass="datagrid" BorderWidth="1px" BorderColor="Black" PageSize="20" PagerStyle-Visible="False">
							<FooterStyle BorderWidth="1px" CssClass="dg_footer"></FooterStyle>
							<SelectedItemStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_selected"></SelectedItemStyle>
							<AlternatingItemStyle CssClass="dg_alter"></AlternatingItemStyle>
							<ItemStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_item"></ItemStyle>
							<HeaderStyle BorderWidth="1px" BorderStyle="Solid" BorderColor="Black" CssClass="dg_header"></HeaderStyle>
							<Columns>
								<asp:TemplateColumn HeaderText="ԭ�ϱ���">
									<ItemTemplate>
										<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.cnvcMaterialCode") %>'>
										</asp:Label>
									</ItemTemplate>
									<EditItemTemplate>
										<asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.cnvcMaterialCode") %>' Width="60px" ReadOnly=True>
										</asp:TextBox>
									</EditItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="cnvcMaterialName" HeaderText="ԭ������"></asp:BoundColumn>
								<asp:TemplateColumn HeaderText="������λ">
									<ItemTemplate>
										<asp:Label id=Label1 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.cnvcLeastUnit") %>'>
										</asp:Label>
									</ItemTemplate>
									<FooterTemplate>
									</FooterTemplate>
									<EditItemTemplate>
										<asp:DropDownList id="ddlLeastUnit" runat="server"></asp:DropDownList>
									</EditItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="cnnPrice" HeaderText="�����۸�"></asp:BoundColumn>
								<asp:TemplateColumn HeaderText="��Ʒ����">
									<ItemTemplate>
										<asp:Label id=Label2 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.cnvcProductTypeComments") %>'>
										</asp:Label>
									</ItemTemplate>
									<EditItemTemplate>
										<asp:DropDownList id="ddlProductType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlProductType_SelectedIndexChanged"></asp:DropDownList>
									</EditItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="��Ʒ���">
									<ItemTemplate>
										<asp:Label id=Label6 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.cnvcProductClassComments") %>'>
										</asp:Label>
									</ItemTemplate>
									<EditItemTemplate>
										<asp:DropDownList id="ddlProductClass" runat="server"></asp:DropDownList>
									</EditItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="cnnConversion" HeaderText="�����ϵ"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcUnit" HeaderText="���ֵ�λ"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcStandardUnit" HeaderText="���λ"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnnStatdardCount" HeaderText="�������"></asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="�Ƿ�����">
									<ItemTemplate>
										<asp:Checkbox runat="server" Enabled="false" Checked='<%# DataBinder.Eval(Container, "DataItem.IsUse") %>'>
										</asp:Checkbox>
									</ItemTemplate>
									<EditItemTemplate>
										<asp:Checkbox id="chkIsUse" runat="server" Checked='<%# DataBinder.Eval(Container, "DataItem.IsUse") %>'></asp:Checkbox>
									</EditItemTemplate>
								</asp:TemplateColumn>
								<asp:EditCommandColumn ButtonType="LinkButton" UpdateText="����" CancelText="ȡ��" EditText="�༭"></asp:EditCommandColumn>
								<asp:BoundColumn Visible="False" DataField="cnvcOldMaterialCode" ReadOnly="True" HeaderText="��ԭ�ϱ���"></asp:BoundColumn>
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
