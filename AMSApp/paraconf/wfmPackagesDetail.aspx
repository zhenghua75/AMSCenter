<%@ Page language="c#" Codebehind="wfmPackagesDetail.aspx.cs" AutoEventWireup="True" Inherits="AMSApp.paraconf.wfmPackagesDetail" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>wfmPackagesDetail</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="1" cellPadding="5" width="95%" border="0">
				<TR>
					<TD align="center" style="COLOR: #330033; FONT-SIZE: 15pt; FONT-WEIGHT: bold">�ײ���ϸ����</TD>
				</TR>
			</TABLE>
			<table width="95%" align="center">
				<tr>
					<td align="left"><asp:Label id="Label1" runat="server" Font-Size="10pt">�ײͱ�ţ�</asp:Label></td>
					<td><asp:TextBox id="txtPackageId" runat="server"></asp:TextBox></td>
					<td align="left"><asp:Label id="Label2" runat="server" Font-Size="10pt">�ײ����ƣ�</asp:Label></td>
					<td><asp:TextBox id="txtPacakgeName" runat="server"></asp:TextBox></td>
					<td align="left">
						<asp:Label id="Label7" runat="server" Font-Size="10pt">�ײ͵��ۣ�</asp:Label></td>
					<td>
						<asp:TextBox id="txtPackagePrice" runat="server"></asp:TextBox></td>
				</tr>
				<tr>
					<td align="left"><asp:Label id="Label3" runat="server" Font-Size="10pt">��Ʒ��ţ�</asp:Label></td>
					<td><asp:TextBox id="txtGoodsId" runat="server"></asp:TextBox></td>
					<td align="left"><asp:Label id="Label4" runat="server" Font-Size="10pt">��Ʒ���ƣ�</asp:Label></td>
					<td><asp:TextBox id="txtGoodsName" runat="server"></asp:TextBox></td>
					<td><asp:Label id="Label5" runat="server" Font-Size="10pt">��Ʒ���ۣ�</asp:Label></td>
					<td><asp:TextBox id="txtGoodsPrice" runat="server"></asp:TextBox></td>
				</tr>
				<tr>
					<td align="left">
						<asp:Label id="Label6" runat="server" Font-Size="10pt">������</asp:Label></td>
					<TD>
						<asp:TextBox id="txtComments" runat="server"></asp:TextBox></TD>
					<TD align="left"></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
				</tr>
				<TR>
					<TD align="center" colSpan="6">
						<asp:Button id="Button1" runat="server" Text="���" onclick="Button1_Click"></asp:Button>
						<asp:Button id="Button2" runat="server" Text="�޸�" onclick="Button2_Click"></asp:Button>
						<asp:Button id="Button3" runat="server" Text="ɾ��" onclick="Button3_Click"></asp:Button>
						<asp:Button id="Button4" runat="server" Text="�����ײ͹���" onclick="Button4_Click"></asp:Button></TD>
				</TR>
			</table>
		</form>
	</body>
</HTML>
