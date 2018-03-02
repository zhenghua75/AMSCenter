<%@ Page language="c#" Codebehind="wfmBook.aspx.cs" AutoEventWireup="True" Inherits="AMSApp.zhenghua.wfmBook" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>wfmBook</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK rel="stylesheet" type="text/css" href="DataGrid.css">
        <script src="scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
		<script type="text/javascript">
			function UnENbtn()
			{
				var btn = document.getElementById("btnBook");
				btn.disabled = true;
			}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table align="center">
				<tr>
					<td><asp:label id="Label1" runat="server" CssClass="title">���Ա�</asp:label></td>
				</tr>
			</table>
			<table width="100%" align="center">
				<tr>
					<td vAlign="top">
						<table>
							<tr>
								<td colSpan="3"><asp:label id="Label2" runat="server" CssClass="title">δ���</asp:label></td>
							</tr>
							<tr>
								<td><asp:label id="Label4" runat="server" CssClass="lable">�������ڣ�</asp:label><asp:textbox id="txtBeginDate1" onfocus="WdatePicker()" runat="server" CssClass="textbox" ReadOnly="True"></asp:textbox>-
									<asp:textbox id="txtEndDate1" onfocus="WdatePicker()" runat="server" CssClass="textbox" ReadOnly="True"></asp:textbox></td>
								<td><asp:label id="Label7" runat="server" CssClass="lable">���ղ��ţ�</asp:label>
									<asp:DropDownList id="ddlCheckDept1" runat="server"></asp:DropDownList></td>
								<td><asp:button id="btnQuery1" runat="server" CssClass="button" Text="��ѯ" onclick="btnQuery1_Click"></asp:button></td>
							</tr>
							<tr>
								<td colSpan="3"><asp:datagrid id="DataGrid1" runat="server" CssClass="datagrid" Width="608px" AllowPaging="True"
										AutoGenerateColumns="False" BorderWidth="1px" BorderColor="Black" onselectedindexchanged="DataGrid1_SelectedIndexChanged">
										<FooterStyle BorderWidth="1px" CssClass="dg_footer"></FooterStyle>
										<SelectedItemStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_selected"></SelectedItemStyle>
										<AlternatingItemStyle CssClass="dg_alter"></AlternatingItemStyle>
										<ItemStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_item"></ItemStyle>
										<HeaderStyle BorderWidth="1px" BorderStyle="Solid" BorderColor="Black" CssClass="dg_header"></HeaderStyle>
										<Columns>
											<asp:BoundColumn DataField="cnnSerialNo" HeaderText="���"></asp:BoundColumn>
											<asp:BoundColumn DataField="cndPublishDate" HeaderText="��������" DataFormatString="{0:yyyy-MM-dd}"></asp:BoundColumn>
											<asp:BoundColumn DataField="cnvcPublishName" HeaderText="������"></asp:BoundColumn>
											<asp:BoundColumn DataField="cnvcPublishDept" HeaderText="�����˲���"></asp:BoundColumn>
											<asp:BoundColumn DataField="cnvcBook" HeaderText="����">
												<ItemStyle Width="80px"></ItemStyle>
											</asp:BoundColumn>
											<asp:BoundColumn DataField="cnvcCheckName" HeaderText="ȡ��ȷ����"></asp:BoundColumn>
											<asp:BoundColumn DataField="cndCheckDate" HeaderText="ȡ��ȷ������" DataFormatString="{0:yyyy-MM-dd}"></asp:BoundColumn>
											<asp:BoundColumn DataField="cnvcCheckDept" HeaderText="���ղ���"></asp:BoundColumn>
											<asp:ButtonColumn Text="ѡ��" CommandName="Select"></asp:ButtonColumn>
										</Columns>
										<PagerStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_page" Mode="NumericPages"></PagerStyle>
									</asp:datagrid></td>
							</tr>
						</table>
					</td>
					<td vAlign="top">
						<table>
							<tr>
								<td colSpan="3"><asp:label id="Label3" runat="server" CssClass="title">�ѽ��</asp:label></td>
							</tr>
							<tr>
								<td><asp:label id="Label5" runat="server" CssClass="lable">ȷ�����ڣ�</asp:label><asp:textbox id="txtBeginDate2" onfocus="WdatePicker()" runat="server" CssClass="textbox" ReadOnly="True"></asp:textbox>-
									<asp:textbox id="txtEndDate2" onfocus="WdatePicker()" runat="server" CssClass="textbox" ReadOnly="True"></asp:textbox>
								</td>
								<td><asp:label id="Label9" runat="server" CssClass="lable">���ղ��ţ�</asp:label>
									<asp:DropDownList id="ddlCheckDept2" runat="server"></asp:DropDownList></td>
								<td><asp:button id="btnQuery2" runat="server" CssClass="button" Text="��ѯ" onclick="btnQuery2_Click"></asp:button></td>
							</tr>
							<tr>
								<td colSpan="3"><asp:datagrid id="DataGrid2" runat="server" CssClass="datagrid" Width="627px" AllowPaging="True"
										AutoGenerateColumns="False" BorderWidth="1px" BorderColor="Black" onselectedindexchanged="DataGrid2_SelectedIndexChanged">
										<FooterStyle BorderWidth="1px" CssClass="dg_footer"></FooterStyle>
										<SelectedItemStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_selected"></SelectedItemStyle>
										<AlternatingItemStyle CssClass="dg_alter"></AlternatingItemStyle>
										<ItemStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_item"></ItemStyle>
										<HeaderStyle BorderWidth="1px" BorderStyle="Solid" BorderColor="Black" CssClass="dg_header"></HeaderStyle>
										<Columns>
											<asp:ButtonColumn Text="ѡ��" CommandName="Select"></asp:ButtonColumn>
											<asp:BoundColumn DataField="cnnSerialNo" HeaderText="���"></asp:BoundColumn>
											<asp:BoundColumn DataField="cndPublishDate" HeaderText="��������" DataFormatString="{0:yyyy-MM-dd}"></asp:BoundColumn>
											<asp:BoundColumn DataField="cnvcPublishName" HeaderText="������"></asp:BoundColumn>
											<asp:BoundColumn DataField="cnvcPublishDept" HeaderText="�����˲���"></asp:BoundColumn>
											<asp:BoundColumn DataField="cnvcBook" HeaderText="����">
												<ItemStyle Width="80px"></ItemStyle>
											</asp:BoundColumn>
											<asp:BoundColumn DataField="cnvcCheckName" HeaderText="ȷ����"></asp:BoundColumn>											
											<asp:BoundColumn DataField="cndCheckDate" HeaderText="ȷ������" DataFormatString="{0:yyyy-MM-dd}"></asp:BoundColumn>
											<asp:BoundColumn DataField="cnvcCheckDept" HeaderText="���ղ���"></asp:BoundColumn>
										</Columns>
										<PagerStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_page" Mode="NumericPages"></PagerStyle>
									</asp:datagrid></td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<table>
				<tr>
					<td vAlign="top">
						<table>
							<tr>
								<td><asp:label id="Label6" runat="server" CssClass="title">����</asp:label></td>
								<td></td>
							</tr>
							<tr>
								<td><asp:textbox id="txtBook" runat="server" CssClass="textbox" Width="584px" Height="152px" TextMode="MultiLine"></asp:textbox></td>
								<td></td>
							</tr>
							<tr>
								<td colSpan="2"><asp:button id="btnBook" runat="server" CssClass="button" Text="��������" Width="224px" onclick="btnBook_Click"></asp:button>
									<asp:DropDownList id="ddlCheckDept" runat="server"></asp:DropDownList>
									<asp:button id="btnReset" runat="server" CssClass="button" Text="����" onclick="btnReset_Click"></asp:button><asp:textbox id="txtFlag" runat="server" CssClass="textbox" Visible="False"></asp:textbox>
									<asp:TextBox id="txtSerialNo" runat="server" CssClass="textbox" Visible="False"></asp:TextBox>
									<asp:Button id="btnReturn" runat="server" CssClass="button" Text="�ظ�" onclick="btnReturn_Click"></asp:Button>
								</td>
							</tr>
						</table>
					</td>
					<td>
						<table>
							<tr>
								<td><asp:Label id="Label8" runat="server" CssClass="title">�ظ�</asp:Label></td>
							</tr>
							<tr>
								<td><asp:Label id="lblReturn" runat="server"></asp:Label></td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
