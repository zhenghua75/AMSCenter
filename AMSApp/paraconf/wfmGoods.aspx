<%@ Register TagPrefix="uc1" TagName="ucPageView" Src="../ucPageView.ascx" %>
<%@ Page language="c#" Codebehind="wfmGoods.aspx.cs" AutoEventWireup="True" Inherits="AMSApp.paraconf.wfmGoods" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>wfmGoods</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	</HEAD>
	<body MS_POSITIONING="GridLayout" onload="<%=strExcelPath%>" bgcolor="#feeff8">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="1" cellPadding="5" width="95%" border="0">
				<TR>
					<TD align="center" style="FONT-WEIGHT: bold; FONT-SIZE: 15pt; COLOR: #330033">��Ʒ����</TD>
				</TR>
				<!--
				<TR>
					<TD align="left" style="FONT-WEIGHT: bold; FONT-SIZE: 10pt; COLOR: #3300ff; TEXT-DECORATION: underline">ע����Ʒ�������ɹ����벻ҪƵ��ʹ�ã�������ɲ������ң��뾡���ܰ�������ӻ��޸ĵ���Ʒ��Ϣ<Font style="FONT-WEIGHT: bold; FONT-SIZE: 10pt; COLOR: #ff0000; TEXT-DECORATION: underline">ȫ���޸���Ϻ�</Font>����һ�ν�����Ʒ���������Ա���ȷ���·ֵ���Ʒ��</TD>
				</TR>
				-->
			</TABLE>
			<table cellspacing="0" cellpadding="0" width="95%" border="1">
				<tr>
					<td>
						<TABLE id="Table2" cellSpacing="0" cellPadding="1" width="100%" border="0">
							<TR>
								<TD style="WIDTH: 42px"><asp:label id="Label1" runat="server" Width="40px" Font-Size="10pt">��ƷID</asp:label></TD>
								<TD style="WIDTH: 127px"><asp:textbox id="txtGoodsID" runat="server" Width="112px" Font-Size="10pt" Height="24px"></asp:textbox></TD>
								<TD style="WIDTH: 53px"><asp:label id="Label2" runat="server" Width="56px" Font-Size="10pt">��Ʒ����</asp:label></TD>
								<TD style="WIDTH: 118px"><FONT face="����"><asp:textbox id="txtGoodsName" runat="server" Width="112px" Font-Size="10pt" Height="24px"></asp:textbox></FONT></TD>
                                <td><asp:Label id="Label3" runat="server" Font-Size="10pt">�ŵ꣺</asp:Label></td><td><asp:DropDownList id="ddlMD" runat="server"></asp:DropDownList></td>
								<td style="WIDTH: 258px"></td>
								<TD><asp:button id="Button1" runat="server" Width="64px" Text="��ѯ" onclick="Button1_Click"></asp:button></TD>
								<TD><asp:button id="Button2" runat="server" Width="64px" Text="���" Visible="False" onclick="Button2_Click"></asp:button></TD>
								<TD><FONT face="����">
										<asp:button id="btnExcel" runat="server" Width="64px" Text="����"></asp:button>
									</FONT>
								</TD>
								<td><asp:Button id="Button3" runat="server" Text="��ѯ�ײ�" onclick="Button3_Click"></asp:Button></td>
                                <td>
                                    <asp:Button ID="Button4" runat="server" Text="��ѯ�ŵ굥��" onclick="Button4_Click" />
                                </td>
							</TR>
						</TABLE>
					</td>
				</tr>
			</table>
			<TABLE id="Table4" cellSpacing="1" cellPadding="1" width="95%" border="0">
				<TR>
					<TD align="center">
						<uc1:ucpageview id="UcPageView1" runat="server" Visible="true"></uc1:ucpageview></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
