<%@ Page language="c#" Codebehind="wfmSaleDailyCheck.aspx.cs" AutoEventWireup="True" Inherits="AMSApp.Storage.wfmSaleDailyCheck" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>wfmSaleDailyCheck</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	</HEAD>
	<body bgColor="#feeff8" MS_POSITIONING="GridLayout">
		<FONT face="����">
			<FORM id="Form1" method="post" runat="server">
				<TABLE id="Table1" cellSpacing="1" cellPadding="5" width="95%" border="0">
					<TR>
						<TD style="FONT-WEIGHT: bold; FONT-SIZE: 15pt; COLOR: #330033" align="center">������̵��</TD>
					</TR>
					<TR>
						<TD align="left" style="FONT-SIZE: 10pt; COLOR: #ff0000">ע������̵�ֻ���̵㵱�տ�棬����ÿ���°�ʱ���С�</TD>
					</TR>
				</TABLE>
				<TABLE id="Table2" cellSpacing="1" cellPadding="1" width="95%" border="1">
					<TR>
						<TD>
							<TABLE id="Table3" cellSpacing="1" cellPadding="1" width="100%" border="0">
								<TR>
									<TD style="WIDTH: 200px" align="right"><asp:label id="Label6" runat="server" Font-Size="10pt">�ŵ꣺</asp:label></TD>
									<TD style="WIDTH: 195px"><asp:dropdownlist id="ddlDept" runat="server" Font-Size="10pt" Width="160px" AutoPostBack="true" onselectedindexchanged="ddlDept_SelectedIndexChanged"></asp:dropdownlist></TD>
									<TD style="WIDTH: 109px" align="right"><asp:label id="Label5" runat="server" Font-Size="10pt">���ڣ�</asp:label></TD>
									<TD style="WIDTH: 249px"><asp:label id="lblCheckDate" runat="server" Font-Size="10pt" Width="202px"></asp:label></TD>
									<td style="WIDTH: 16px"><asp:button id="btQuery" runat="server" Font-Size="10pt" Width="56px" Text="��ѯ" onclick="btQuery_Click"></asp:button></td>
									<TD style="WIDTH: 72px"></TD>
								</TR>
								<TR>
									<TD style="WIDTH: 200px" align="right"><asp:label id="Label1" runat="server" Font-Size="10pt">��Ʒ���ͣ�</asp:label></TD>
									<TD style="WIDTH: 195px"><asp:dropdownlist id="ddlProductType" runat="server" Font-Size="10pt" Width="160px" AutoPostBack="true" onselectedindexchanged="ddlProductType_SelectedIndexChanged"></asp:dropdownlist></TD>
									<TD style="WIDTH: 109px" align="right"><asp:label id="Label2" runat="server" Font-Size="10pt">��Ʒ���</asp:label></TD>
									<TD style="WIDTH: 249px"><asp:dropdownlist id="ddlProductClass" runat="server" Font-Size="10pt" Width="160px" AutoPostBack="false"></asp:dropdownlist></TD>
									<td style="WIDTH: 16px">
										<asp:button id="btnreset" runat="server" Font-Size="10pt" Width="56px" Text="��ѡ" onclick="btnreset_Click"></asp:button></td>
									<TD style="WIDTH: 72px"><FONT face="����"></FONT></TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
				</TABLE>
				<TABLE id="Table5" cellSpacing="1" cellPadding="1" width="95%" border="1">
					<TR>
						<TD>
							<TABLE id="Table6" cellSpacing="1" cellPadding="1" width="100%" border="0">
								<TR>
									<TD style="WIDTH: 90px" align="right"><asp:label id="Label10" runat="server" Font-Size="10pt">����</asp:label></TD>
									<TD style="WIDTH: 171px"><asp:textbox id="Textbox1" runat="server" Font-Size="10pt" Width="160px"></asp:textbox></TD>
									<TD style="WIDTH: 77px" align="right"><asp:label id="Label11" runat="server" Font-Size="10pt">�̵���</asp:label></TD>
									<TD style="WIDTH: 185px"><asp:textbox id="Textbox2" runat="server" Font-Size="10pt"></asp:textbox></TD>
									<TD style="WIDTH: 62px" align="right"><asp:label id="Label13" runat="server" Font-Size="10pt">������</asp:label></TD>
									<td style="WIDTH: 197px"><asp:textbox id="Textbox3" runat="server" Font-Size="10pt"></asp:textbox></td>
									<TD style="WIDTH: 131px" align="left"><asp:button id="btnEdit" runat="server" Font-Size="10pt" Width="72px" Text="�༭" onclick="btnEdit_Click"></asp:button></TD>
									<TD style="WIDTH: 150px" align="left"><asp:button id="btnCheckOk" runat="server" Font-Size="10pt" Width="64px" Text="�̵�ȷ��" onclick="btnCheckOk_Click"></asp:button></TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
				</TABLE>
				<TABLE id="Table4" cellSpacing="1" cellPadding="1" width="95%" border="0">
					<TR>
						<TD align="center"><asp:datagrid id="DataGrid1" runat="server" AutoGenerateColumns="False" Font-Names="Verdana" Width="100%"
								AlternatingItemStyle-BackColor="#660033" HeaderStyle-BackColor="SteelBlue" Font-Size="X-Small" Font-Name="Verdana"
								CellPadding="3" BorderWidth="1px" BorderColor="Black" PagerStyle-HorizontalAlign="Right">
								<FooterStyle Wrap="False"></FooterStyle>
								<SelectedItemStyle Wrap="False"></SelectedItemStyle>
								<EditItemStyle Wrap="False"></EditItemStyle>
								<AlternatingItemStyle Wrap="False" ForeColor="Black" BackColor="#E6E6E6"></AlternatingItemStyle>
								<ItemStyle Wrap="False" ForeColor="Black" BackColor="White"></ItemStyle>
								<HeaderStyle Font-Size="Small" Font-Bold="True" Wrap="False" ForeColor="White" BackColor="#880028"></HeaderStyle>
								<Columns>
									<asp:BoundColumn Visible="False" DataField="cnvcProductCode" ReadOnly="True" HeaderText="��Ʒ����"></asp:BoundColumn>
									<asp:BoundColumn DataField="cnvcProductName" ReadOnly="True" HeaderText="��Ʒ����"></asp:BoundColumn>
									<asp:BoundColumn DataField="cnnProductPrice" ReadOnly="True" HeaderText="��Ʒ����"></asp:BoundColumn>
									<asp:BoundColumn DataField="cnvcUnit" ReadOnly="True" HeaderText="��λ"></asp:BoundColumn>
									<asp:BoundColumn DataField="cnnOriginalStorage" ReadOnly="True" HeaderText="�ڳ����"></asp:BoundColumn>
									<asp:BoundColumn DataField="cnnOrderCount" ReadOnly="True" HeaderText="������"></asp:BoundColumn>
									<asp:BoundColumn DataField="cnnReceiveCount" ReadOnly="True" HeaderText="������"></asp:BoundColumn>
									<asp:BoundColumn DataField="cnnMoveOutCount" ReadOnly="True" HeaderText="��������"></asp:BoundColumn>
									<asp:BoundColumn DataField="cnnMoveInCount" ReadOnly="True" HeaderText="��������"></asp:BoundColumn>
									<asp:BoundColumn DataField="cnnLoseCount" ReadOnly="True" HeaderText="�����"></asp:BoundColumn>
									<asp:BoundColumn DataField="cnnFreeCount" ReadOnly="True" HeaderText="ʣ����"></asp:BoundColumn>
									<asp:BoundColumn DataField="cnnUseCount" ReadOnly="True" HeaderText="ʹ����"></asp:BoundColumn>
									<asp:BoundColumn DataField="cnnSellCount" ReadOnly="True" HeaderText="������"></asp:BoundColumn>
									<asp:BoundColumn DataField="cnnSystemCount" ReadOnly="True" HeaderText="ϵͳ���"></asp:BoundColumn>
									<asp:BoundColumn DataField="cnnRealCount" ReadOnly="True" HeaderText="ʵ�ʿ��"></asp:BoundColumn>
									<asp:TemplateColumn HeaderText="ʵ�ʿ��">
										<ItemTemplate>
											<asp:textbox id="Textbox5" runat="server" Font-Size="10pt" Width="80px"></asp:textbox>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="cnnDifferentCount" ReadOnly="True" HeaderText="������"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="cnnDifferentSum" ReadOnly="True" HeaderText="������"></asp:BoundColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right"></PagerStyle>
							</asp:datagrid></TD>
					</TR>
				</TABLE>
			</FORM>
		</FONT>
	</body>
</HTML>
