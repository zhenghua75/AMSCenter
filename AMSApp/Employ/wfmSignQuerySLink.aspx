<%@ Register TagPrefix="uc1" TagName="ucPageView" Src="../ucPageView.ascx" %>
<%@ Page language="c#" Codebehind="wfmSignQuerySLink.aspx.cs" AutoEventWireup="True" Inherits="AMSApp.Employ.wfmSignQuerySLink" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>wfmSignQuery</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<SCRIPT language="javascript" src="../js/calendar.js"></SCRIPT>
	</HEAD>
	<body MS_POSITIONING="GridLayout" onload="<%=strExcelPath%>" bgcolor="#feeff8">
		<FONT face="����">
			<FORM id="Form1" method="post" runat="server">
				<TABLE id="Table1" cellSpacing="1" cellPadding="5" width="95%" border="0">
					<TR>
						<TD style="COLOR: #330033; FONT-SIZE: 15pt; FONT-WEIGHT: bold" align="center">�ŵ꿼����ϸ���</TD>
					</TR>
				</TABLE>
				<TABLE id="Table2" cellSpacing="1" cellPadding="1" width="95%" border="1">
					<TR>
						<TD>
							<TABLE id="Table3" cellSpacing="1" cellPadding="1" width="100%" border="0">
								<TR>
									<TD style="WIDTH: 171px" align="right">
										<asp:label id="Label5" runat="server" Font-Size="10pt" style="Z-INDEX: 0">��ʼʱ��</asp:label></TD>
									<TD style="WIDTH: 156px">
										<asp:TextBox style="Z-INDEX: 0" id="txtBegin" runat="server" Font-Size="10pt" ReadOnly="True"></asp:TextBox></TD>
									<TD style="WIDTH: 81px" align="right">
										<asp:label id="Label4" runat="server" Font-Size="10pt" style="Z-INDEX: 0">����ʱ��</asp:label></TD>
									<TD style="WIDTH: 259px">
										<asp:TextBox style="Z-INDEX: 0" id="txtEnd" runat="server" Font-Size="10pt" ReadOnly="True"></asp:TextBox></TD>
									<TD style="WIDTH: 101px">
										<asp:button id="btnExcel" runat="server" Font-Size="10pt" Width="56px" Text="����" style="Z-INDEX: 0"></asp:button></TD>
									<TD><INPUT type="button" style="CURSOR:hand" value="��  ��" onClick="javascript:window.history.back();"></TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
				</TABLE>
				<TABLE id="Table4" border="0" cellSpacing="1" cellPadding="1" width="95%">
					<TR>
						<TD align="center">
							<uc1:ucpageview id="UcPageView1" runat="server" Visible="true"></uc1:ucpageview></TD>
					</TR>
				</TABLE>
			</FORM>
		</FONT>
	</body>
</HTML>
