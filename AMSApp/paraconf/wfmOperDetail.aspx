<%@ Page language="c#" Codebehind="wfmOperDetail.aspx.cs" AutoEventWireup="True" Inherits="AMSApp.paraconf.wfmOperDetail" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>wfmOperDetail</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<FORM id="Form1" method="post" runat="server" bgcolor="#feeff8">
			<TABLE id="Table2" cellSpacing="1" cellPadding="5" width="60%" align="center" border="0">
				<TR>
					<TD style="COLOR: #330033; FONT-SIZE: 15pt; FONT-WEIGHT: bold" align="center">
						<asp:Label id="lbltitle" runat="server" Height="24px" Width="380px"></asp:Label></TD>
				</TR>
			</TABLE>
			<TABLE id="Table1" cellSpacing="10" cellPadding="5" width="763" align="center" border="0"
				style="WIDTH: 763px; HEIGHT: 144px">
				<TR>
					<TD style="WIDTH: 90px; FONT-SIZE: 10pt" align="right">��¼ID</TD>
					<TD style="WIDTH: 136px">
						<asp:TextBox id="txtLoginID" runat="server" Height="24px" Width="135px" Font-Size="10pt"></asp:TextBox></TD>
					<TD style="WIDTH: 40px"></TD>
					<TD style="WIDTH: 79px; FONT-SIZE: 10pt" align="right">����Ա����</TD>
					<TD>
						<asp:TextBox id="txtOperName" runat="server" Height="24px" Width="136px" Font-Size="10pt"></asp:TextBox></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 90px; FONT-SIZE: 10pt" align="right"><FONT face="����">�鿴Ȩ��</FONT></TD>
					<TD style="WIDTH: 136px">
						<asp:DropDownList id="ddlLimit" runat="server" Width="136px" Font-Size="10pt"></asp:DropDownList></TD>
					<TD style="WIDTH: 40px"><FONT face="����"></FONT></TD>
					<TD style="WIDTH: 79px; FONT-SIZE: 10pt" align="right">�ŵ�</TD>
					<TD>
						<asp:DropDownList id="ddlDept" runat="server" Width="136px" Font-Size="10pt"></asp:DropDownList></TD>
				</TR>
				<TR>
					<TD align="center" colspan="5">
						<asp:Button id="btAdd" runat="server" Font-Size="10pt" Width="64px" Text="���" onclick="btAdd_Click"></asp:Button>&nbsp;&nbsp;&nbsp;&nbsp;
						<asp:Button id="btMod" runat="server" Width="64px" Font-Size="10pt" Text="����" onclick="btMod_Click"></asp:Button>&nbsp;&nbsp;&nbsp;&nbsp;
						<asp:Button id="btDel" runat="server" Width="64px" Font-Size="10pt" Text="ɾ��" onclick="btDel_Click"></asp:Button>&nbsp;&nbsp;&nbsp;&nbsp;
						<asp:Button id="btnInitPwd" runat="server" Width="84px" Font-Size="10pt" Text="�����ʼ��" onclick="btnInitPwd_Click"></asp:Button>&nbsp;&nbsp;&nbsp;&nbsp;
						<asp:Button id="btcancel" runat="server" Font-Size="10pt" Width="103px" Text="���ز���Ա����" onclick="btcancel_Click"></asp:Button></TD>
				</TR>
			</TABLE>
			<TABLE id="Table2" cellSpacing="1" cellPadding="5" width="60%" align="center" border="0">
				<TR>
					<TD style="COLOR: #330033; FONT-SIZE: 15pt; FONT-WEIGHT: bold" align="center"><FONT style="Z-INDEX: 0" color="#cc0033" size="4">�����������Ĳ���Ա����Ĭ��Ϊ��123456</FONT></TD>
				</TR>
			</TABLE>
		</FORM>
	</body>
</HTML>
