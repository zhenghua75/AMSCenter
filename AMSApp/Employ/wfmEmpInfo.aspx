<%@ Register TagPrefix="uc1" TagName="ucPageView" Src="../ucPageView.ascx" %>

<%@ Page Language="c#" CodeBehind="wfmEmpInfo.aspx.cs" AutoEventWireup="True" Inherits="AMSApp.Employ.wfmEmpInfo" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>wfmEmpInfo</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
</head>
<body ms_positioning="GridLayout" onload="<%=strExcelPath%>" bgcolor="#feeff8">
    <font face="����">
			<FORM id="Form1" method="post" runat="server">
				<TABLE id="Table3" cellSpacing="1" cellPadding="5" width="95%" border="0">
					<TR>
						<TD style="COLOR: #330033; FONT-SIZE: 15pt; FONT-WEIGHT: bold" align="center">Ա��������Ϣά��</TD>
					</TR>
				</TABLE>
				<TABLE id="Table5" cellSpacing="0" cellPadding="0" width="95%" border="1">
					<TR>
						<TD>
							<TABLE id="Table2" cellSpacing="0" cellPadding="1" width="100%" border="0">
								<TR>
									<TD style="WIDTH: 184px" align="right">
										<asp:label id="Label1" runat="server" Width="60px" Font-Size="10pt">Ա������</asp:label></TD>
									<TD style="WIDTH: 127px">
										<asp:textbox id="txtCardID" runat="server" Width="112px" Font-Size="10pt" Height="24px"></asp:textbox></TD>
									<TD style="WIDTH: 132px" align="right">
										<asp:label id="Label2" runat="server" Width="34px" Font-Size="10pt">״̬</asp:label></TD>
									<TD style="WIDTH: 118px">
										<asp:dropdownlist id="ddlstate" runat="server" Font-Size="10pt" Width="144px" Height="24px"></asp:dropdownlist></TD>
									<TD style="WIDTH: 127px"></TD>
									<TD style="WIDTH: 105px" align="right">
		<FONT face="����">
			                            <asp:Label ID="Label9" runat="server" Font-Size="10pt" Text="Ƭ��"></asp:Label>
		</FONT>
	                                </TD>
									<TD>
		<FONT face="����">
                                        <asp:DropDownList ID="ddlRegion" runat="server" AutoPostBack="True" 
                                            onselectedindexchanged="ddlRegion_SelectedIndexChanged">
                                        </asp:DropDownList>
		</FONT>
	                                </TD>
								</TR>
								<TR>
									<TD style="WIDTH: 184px" align="right">
										<asp:label id="Label3" runat="server" Width="54px" Font-Size="10pt">Ա������</asp:label></TD>
									<TD style="WIDTH: 127px">
										<asp:textbox id="txtEmpName" runat="server" Width="112px" Font-Size="10pt" Height="24px"></asp:textbox></TD>
									<TD style="WIDTH: 132px" align="right">
										<asp:label id="Label4" runat="server" Width="34px" Font-Size="10pt">�ŵ�</asp:label></TD>
									<TD style="WIDTH: 118px">
										<asp:dropdownlist id="ddlDept" runat="server" Font-Size="10pt" Width="144px" Height="24px"></asp:dropdownlist></TD>
									<TD style="WIDTH: 127px"></TD>
									<TD style="WIDTH: 105px">
										<asp:button id="btQuery" runat="server" Width="64px" Text="��ѯ" onclick="btQuery_Click"></asp:button></TD>
									<TD>
										<asp:button id="btnExcel" runat="server" Width="64px" Text="����"></asp:button></TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
				</TABLE>
				<TABLE id="Table4" cellSpacing="1" cellPadding="1" width="1304px" border="0">
					<TR>
						<TD align="center">
							<uc1:ucpageview id="UcPageView1" runat="server" Visible="true"></uc1:ucpageview></TD>
					</TR>
				</TABLE>
			</FORM>
		</font>
</body>
</html>
