<%@ Page language="c#" Codebehind="wfmGoodsDetail.aspx.cs" AutoEventWireup="True" Inherits="AMSApp.paraconf.wfmGoodsDetail" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>wfmGoodsDetail</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
        <script type="text/javascript" src="../js/calendar.js"> </script>
	</HEAD>
	<body MS_POSITIONING="GridLayout" bgcolor="#feeff8">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table2" align="center" cellSpacing="1" cellPadding="5" width="60%" border="0">
				<TR>
					<TD align="center" style="FONT-WEIGHT: bold; FONT-SIZE: 15pt; COLOR: #330033">
						<asp:Label id="lbltitle" runat="server" Height="24px" Width="380px">Label</asp:Label></TD>
				</TR>
			</TABLE>
			<TABLE id="Table1" align="center" cellSpacing="10" cellPadding="5" width="60%" border="0">
				<TR>
					<TD style="FONT-SIZE: 10pt; WIDTH: 90px" align="right">��ƷID</TD>
					<TD style="WIDTH: 136px">
						<asp:TextBox id="txtGoodsID" runat="server" Font-Size="10pt" Width="135px" Height="24px"></asp:TextBox></TD>
					<td style="WIDTH: 40px"></td>
					<TD style="FONT-SIZE: 10pt;WIDTH: 61px" align="right">��Ʒ����</TD>
					<TD>
						<asp:TextBox id="txtGoodsName" runat="server" Font-Size="10pt" Width="136px" Height="24px"></asp:TextBox></TD>
				</TR>
				<TR>
					<TD style="FONT-SIZE: 10pt;WIDTH: 90px" align="right"><FONT face="����">ƴ����д</FONT></TD>
					<TD style="WIDTH: 136px">
						<asp:TextBox id="txtSpell" runat="server" Font-Size="10pt" Width="136px" Height="24px"></asp:TextBox></TD>
					<td style="WIDTH: 40px"><FONT face="����"></FONT></td>
					<TD style="FONT-SIZE: 10pt;WIDTH: 61px" align="right">����</TD>
					<TD>
						<asp:TextBox id="txtPrice" runat="server" Font-Size="10pt" Width="136px" Height="24px"></asp:TextBox></TD>
				</TR>
				<TR>
					<TD style="FONT-SIZE: 10pt;WIDTH: 90px" align="right">���ֶһ���ֵ</TD>
					<TD style="WIDTH: 136px">
						<asp:TextBox id="txtigvalue" runat="server" Font-Size="10pt" Width="136px" Height="24px">-1</asp:TextBox></TD>
					<td style="WIDTH: 40px"></td>
					<TD style="FONT-SIZE: 10pt; WIDTH: 61px" align="right"><FONT face="����"></FONT></TD>
					<TD>
						<asp:CheckBox id="chkPackage" runat="server" Text="�Ƿ��ײ�"></asp:CheckBox></TD>
				</TR>
                <TR>
					<TD style="FONT-SIZE: 10pt;WIDTH: 90px" align="right"></TD>
					<TD style="WIDTH: 136px">
                    <asp:CheckBox id="IsNew" runat="server" Text="�Ƿ���Ʒ"></asp:CheckBox>
						</TD>
					<td style="WIDTH: 40px"></td>
					<TD style="FONT-SIZE: 10pt; WIDTH: 61px" align="right"><FONT face="����"></FONT></TD>
					<TD>
						<asp:CheckBox id="IsKey" runat="server" Text="�Ƿ��ص��Ʒ"></asp:CheckBox></TD>
				</TR>
                <TR>
					<TD style="FONT-SIZE: 10pt;WIDTH: 90px" align="right">��λ</TD>
					<TD style="WIDTH: 136px">
						<asp:TextBox id="Unit" runat="server" Font-Size="10pt" Width="136px" Height="24px"></asp:TextBox></TD>
					<td style="WIDTH: 40px"></td>
					<TD style="FONT-SIZE: 10pt; WIDTH: 61px" align="right">����ʱ��</TD>
					<TD>
						<asp:TextBox id="NewDate" onfocus="HS_setDate(this)" runat="server" Font-Size="10pt" Width="136px" Height="24px"></asp:TextBox></TD>
				</TR>
                <TR>
					<TD style="FONT-SIZE: 10pt;WIDTH: 90px" align="right"></TD>
					<TD style="WIDTH: 136px">
                    <asp:CheckBox id="IsDeptPrice" runat="server" Text="�Ƿ��ŵ굥��"></asp:CheckBox>
						</TD>
					<td style="WIDTH: 40px"></td>
					<TD style="FONT-SIZE: 10pt; WIDTH: 61px" align="right"><FONT face="����"></FONT></TD>
					<TD>
						</TD>
				</TR>
				<TR>
					<TD colspan="5" align="center">
						<asp:Button id="btAdd" runat="server" Font-Size="10pt" Width="64px" Text="���" Visible="False" onclick="btAdd_Click"></asp:Button>
						<asp:Button id="btMod" runat="server" Width="64px" Font-Size="10pt" Text="����" onclick="btMod_Click"></asp:Button>
						<asp:Button id="btDel" runat="server" Width="64px" Font-Size="10pt" Text="ɾ��" onclick="btDel_Click"></asp:Button>
						<asp:Button id="btcancel" runat="server" Font-Size="10pt" Width="89px" Text="������Ʒ����" onclick="btcancel_Click"></asp:Button>
						<asp:Button id="btnPackage" runat="server" Text="�༭�ײ�" onclick="btnPackage_Click"></asp:Button></TD>
				</TR>
				<TR>
					<TD style="FONT-SIZE: 10pt;WIDTH: 90px;HEIGHT: 78px" align="right"></TD>
					<TD colspan="4" style="HEIGHT: 78px">
						<asp:TextBox id="txtComments" runat="server" Font-Size="10pt" Width="443px" Height="88px" TextMode="MultiLine"
							Visible="False"></asp:TextBox></TD>
				</TR>
				<tr>
					<td colspan="5" align="center">
						<asp:Label id="lblPromt" runat="server" Width="512px" ForeColor="Red"></asp:Label></td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
