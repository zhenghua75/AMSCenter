<%@ Register TagPrefix="uc1" TagName="ucPageView" Src="../ucPageView.ascx" %>
<%@ Page language="c#" Codebehind="wfmNotice2.aspx.cs" AutoEventWireup="True" Inherits="AMSApp.paraconf.wfmNotice2" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>wfmNotice</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<SCRIPT language="javascript" src="../js/calendar.js"></SCRIPT>
		<script language="javascript" src="../js/isInt.js"></script>
	</HEAD>
	<body MS_POSITIONING="GridLayout" onload="<%=strExcelPath%>" bgcolor="#feeff8">
		<FONT face="����">
			<FORM id="Form1" method="post" runat="server">
				<TABLE id="Table3" cellSpacing="1" cellPadding="5" width="95%" border="0">
					<TR>
						<TD style="FONT-WEIGHT: bold; FONT-SIZE: 15pt; COLOR: #330033" align="center">ϵͳ֪ͨ����</TD>
					</TR>
				</TABLE>
				<TABLE id="Table5" cellSpacing="0" cellPadding="0" width="95%" border="1">
					<TR>
						<TD>
							<TABLE id="Table2" cellSpacing="0" cellPadding="1" width="100%" border="0">
								<TR>
									<TD style="WIDTH: 545px" align="right">
										<asp:label id="Label5" runat="server" Font-Size="10pt">��ʼʱ��</asp:label></TD>
									<TD style="WIDTH: 671px"><INPUT id=txtBegin style="FONT-SIZE: 10pt; WIDTH: 128px; HEIGHT: 22px" onfocus=HS_setDate(this) readOnly type=text size=16 value="<%=strBeginDate%>" name=txtBegin></TD>
									<TD style="WIDTH: 486px">
										<asp:label id="Label4" runat="server" Font-Size="10pt">����ʱ��</asp:label></TD>
									<TD style="WIDTH: 324px"><INPUT id=txtEnd style="FONT-SIZE: 10pt; WIDTH: 128px; HEIGHT: 22px" onfocus=HS_setDate(this) readOnly type=text size=16 value="<%=strEndDate%>" name=txtEnd></TD>
									<TD style="WIDTH: 243px" align="right">
										<asp:button Text="��ѯ" id="btQuery" runat="server" Width="64px" onclick="btQuery_Click"></asp:button>
										<asp:button id="btAdd" runat="server" Width="64px" Text="���" onclick="btAdd_Click"></asp:button>
										<asp:button id="btnExcel" runat="server" Width="64px" Text="����"></asp:button></TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
				</TABLE>
				<TABLE id="Table4" cellSpacing="1" cellPadding="1" width="95%" border="0">
					<TR>
						<TD align="center">
							<asp:DataGrid id="DataGrid1" runat="server" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px"
								BackColor="White" CellPadding="4" AutoGenerateColumns="False" AllowPaging="True" PagerStyle-Visible="False" onselectedindexchanged="DataGrid1_SelectedIndexChanged">
								<FooterStyle ForeColor="#330099" BackColor="#FFFFCC"></FooterStyle>
								<SelectedItemStyle Font-Bold="True" ForeColor="#663399" BackColor="#FFCC66"></SelectedItemStyle>
								<ItemStyle ForeColor="#330099" BackColor="White"></ItemStyle>
								<HeaderStyle Font-Bold="True" ForeColor="#FFFFCC" BackColor="#990000"></HeaderStyle>
								<Columns>
									<asp:BoundColumn DataField="cnnNoticeID" ReadOnly="True" HeaderText="֪ͨID"></asp:BoundColumn>
									<asp:TemplateColumn HeaderText="֪ͨ����" ItemStyle-Width="500px">
										<ItemTemplate>
											<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.cnvcComments") %>'>
											</asp:Label>
										</ItemTemplate>
										<EditItemTemplate>
											<asp:TextBox runat="server" ID="txtComments" Width="500px" Height="100px" TextMode="MultiLine" Text='<%# DataBinder.Eval(Container, "DataItem.cnvcComments") %>'>
											</asp:TextBox>
										</EditItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="cndReleaseDate" ReadOnly="True" HeaderText="��������"></asp:BoundColumn>
									<asp:TemplateColumn HeaderText="ʧЧ����">
										<ItemTemplate>
											<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.cndInvalidDate", "{0:yyyy-MM-dd}") %>'>
											</asp:Label>
										</ItemTemplate>
										<EditItemTemplate>
											<asp:TextBox runat="server" ID="txtInvalidDate" Width="80px" Text='<%# DataBinder.Eval(Container, "DataItem.cndInvalidDate", "{0:yyyy-MM-dd}")  %>' onfocus=HS_setDate(this) >
											</asp:TextBox>
										</EditItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="�Ƿ���Ч">
										<ItemTemplate>
											<asp:Label runat="server" Text='<%# ReturnIsAcitve(Convert.ToString(DataBinder.Eval(Container, "DataItem.cnvcIsActive")) )%>'>
											</asp:Label>
										</ItemTemplate>
										<EditItemTemplate>
											<asp:DropDownList ID="ddlIsActive" Runat="server"></asp:DropDownList>
											</asp:TextBox>
										</EditItemTemplate>
									</asp:TemplateColumn>
									<asp:EditCommandColumn ButtonType="LinkButton" UpdateText="����" CancelText="ȡ��" EditText="�༭"></asp:EditCommandColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Center" ForeColor="#330099" BackColor="#FFFFCC" Mode="NumericPages"></PagerStyle>
							</asp:DataGrid></TD>
					</TR>
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
				</TABLE>
			</FORM>
		</FONT>
	</body>
</HTML>
