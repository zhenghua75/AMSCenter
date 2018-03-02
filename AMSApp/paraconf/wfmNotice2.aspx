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
		<FONT face="宋体">
			<FORM id="Form1" method="post" runat="server">
				<TABLE id="Table3" cellSpacing="1" cellPadding="5" width="95%" border="0">
					<TR>
						<TD style="FONT-WEIGHT: bold; FONT-SIZE: 15pt; COLOR: #330033" align="center">系统通知管理</TD>
					</TR>
				</TABLE>
				<TABLE id="Table5" cellSpacing="0" cellPadding="0" width="95%" border="1">
					<TR>
						<TD>
							<TABLE id="Table2" cellSpacing="0" cellPadding="1" width="100%" border="0">
								<TR>
									<TD style="WIDTH: 545px" align="right">
										<asp:label id="Label5" runat="server" Font-Size="10pt">开始时间</asp:label></TD>
									<TD style="WIDTH: 671px"><INPUT id=txtBegin style="FONT-SIZE: 10pt; WIDTH: 128px; HEIGHT: 22px" onfocus=HS_setDate(this) readOnly type=text size=16 value="<%=strBeginDate%>" name=txtBegin></TD>
									<TD style="WIDTH: 486px">
										<asp:label id="Label4" runat="server" Font-Size="10pt">结束时间</asp:label></TD>
									<TD style="WIDTH: 324px"><INPUT id=txtEnd style="FONT-SIZE: 10pt; WIDTH: 128px; HEIGHT: 22px" onfocus=HS_setDate(this) readOnly type=text size=16 value="<%=strEndDate%>" name=txtEnd></TD>
									<TD style="WIDTH: 243px" align="right">
										<asp:button Text="查询" id="btQuery" runat="server" Width="64px" onclick="btQuery_Click"></asp:button>
										<asp:button id="btAdd" runat="server" Width="64px" Text="添加" onclick="btAdd_Click"></asp:button>
										<asp:button id="btnExcel" runat="server" Width="64px" Text="导出"></asp:button></TD>
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
									<asp:BoundColumn DataField="cnnNoticeID" ReadOnly="True" HeaderText="通知ID"></asp:BoundColumn>
									<asp:TemplateColumn HeaderText="通知内容" ItemStyle-Width="500px">
										<ItemTemplate>
											<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.cnvcComments") %>'>
											</asp:Label>
										</ItemTemplate>
										<EditItemTemplate>
											<asp:TextBox runat="server" ID="txtComments" Width="500px" Height="100px" TextMode="MultiLine" Text='<%# DataBinder.Eval(Container, "DataItem.cnvcComments") %>'>
											</asp:TextBox>
										</EditItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="cndReleaseDate" ReadOnly="True" HeaderText="发布日期"></asp:BoundColumn>
									<asp:TemplateColumn HeaderText="失效日期">
										<ItemTemplate>
											<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.cndInvalidDate", "{0:yyyy-MM-dd}") %>'>
											</asp:Label>
										</ItemTemplate>
										<EditItemTemplate>
											<asp:TextBox runat="server" ID="txtInvalidDate" Width="80px" Text='<%# DataBinder.Eval(Container, "DataItem.cndInvalidDate", "{0:yyyy-MM-dd}")  %>' onfocus=HS_setDate(this) >
											</asp:TextBox>
										</EditItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="是否有效">
										<ItemTemplate>
											<asp:Label runat="server" Text='<%# ReturnIsAcitve(Convert.ToString(DataBinder.Eval(Container, "DataItem.cnvcIsActive")) )%>'>
											</asp:Label>
										</ItemTemplate>
										<EditItemTemplate>
											<asp:DropDownList ID="ddlIsActive" Runat="server"></asp:DropDownList>
											</asp:TextBox>
										</EditItemTemplate>
									</asp:TemplateColumn>
									<asp:EditCommandColumn ButtonType="LinkButton" UpdateText="更新" CancelText="取消" EditText="编辑"></asp:EditCommandColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Center" ForeColor="#330099" BackColor="#FFFFCC" Mode="NumericPages"></PagerStyle>
							</asp:DataGrid></TD>
					</TR>
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
				</TABLE>
			</FORM>
		</FONT>
	</body>
</HTML>
