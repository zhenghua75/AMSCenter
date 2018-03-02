<%@ Page language="c#" Codebehind="wfmProviderDetail.aspx.cs" AutoEventWireup="True" Inherits="AMSApp.Storage.wfmProviderDetail" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>wfmProviderDetail</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body MS_POSITIONING="GridLayout" bgcolor="#feeff8">
		<FORM id="Form1" method="post" runat="server">
			<TABLE id="Table2" cellSpacing="1" cellPadding="5" width="60%" align="center" border="0">
				<TR>
					<TD style="FONT-WEIGHT: bold; FONT-SIZE: 15pt; COLOR: #330033" align="center">
						<asp:Label id="lbltitle" runat="server" Height="24px" Width="380px">Label</asp:Label></TD>
				</TR>
			</TABLE>
			<TABLE id="Table1" cellSpacing="10" cellPadding="5" width="60%" align="center" border="0">
				<TR>
					<TD style="FONT-SIZE: 10pt; WIDTH: 90px" align="right">供应商编码</TD>
					<TD style="WIDTH: 136px">
						<asp:TextBox id="txtProviderCode" runat="server" Height="24px" Width="135px" Font-Size="10pt"></asp:TextBox></TD>
					<TD style="WIDTH: 40px"></TD>
					<TD style="FONT-SIZE: 10pt; WIDTH: 93px" align="right">供应商名称</TD>
					<TD>
						<asp:TextBox id="txtProviderName" runat="server" Height="24px" Width="136px" Font-Size="10pt"></asp:TextBox>
						<asp:Button id="btnFind" runat="server" Width="72px" Font-Size="10pt" Text="自动查找" onclick="btnFind_Click"></asp:Button></TD>
				</TR>
				<TR>
					<TD style="FONT-SIZE: 10pt; WIDTH: 90px" align="right"><FONT face="宋体">供应产品</FONT></TD>
					<TD style="WIDTH: 136px">
						<asp:DropDownList id="ddlProduct" runat="server" Width="136px" Font-Size="10pt"></asp:DropDownList></TD>
					<TD style="WIDTH: 40px"><FONT face="宋体"></FONT></TD>
					<TD style="FONT-SIZE: 10pt; WIDTH: 93px" align="right">供应价格</TD>
					<TD>
						<asp:TextBox id="txtProviderPrice" runat="server" Height="24px" Width="136px" Font-Size="10pt"></asp:TextBox></TD>
				</TR>
				<TR>
					<TD style="FONT-SIZE: 10pt; WIDTH: 90px" align="right">产品单位</TD>
					<TD style="WIDTH: 136px">
						<asp:TextBox id="txtProviderUnit" runat="server" Height="24px" Width="136px" Font-Size="10pt"></asp:TextBox></TD>
					<TD style="WIDTH: 40px"></TD>
					<TD style="FONT-SIZE: 10pt; WIDTH: 93px" align="right">供货及时度</TD>
					<TD>
						<asp:DropDownList id="ddlProviderTime" runat="server" Width="136px" Font-Size="10pt"></asp:DropDownList></TD>
				</TR>
				<TR>
					<TD style="FONT-SIZE: 10pt; WIDTH: 90px" align="right">产品品质</TD>
					<TD style="WIDTH: 136px">
						<asp:DropDownList id="ddlProviderQuality" runat="server" Width="136px" Font-Size="10pt"></asp:DropDownList></TD>
					<TD style="WIDTH: 40px"></TD>
					<TD style="FONT-SIZE: 10pt; WIDTH: 93px" align="right">性价比排名</TD>
					<TD>
						<asp:TextBox id="txtProviderValue" runat="server" Height="24px" Width="136px" Font-Size="10pt"></asp:TextBox></TD>
				</TR>
				<TR>
					<TD style="FONT-SIZE: 10pt; WIDTH: 90px" align="right">联系人</TD>
					<TD style="WIDTH: 136px">
						<asp:TextBox id="txtLinkName" runat="server" Width="136px" Height="24px" Font-Size="10pt"></asp:TextBox></TD>
					<TD style="WIDTH: 40px"></TD>
					<TD style="FONT-SIZE: 10pt; WIDTH: 93px" align="right">联系电话</TD>
					<TD>
						<asp:TextBox id="txtLinkPhone" runat="server" Height="24px" Width="136px" Font-Size="10pt"></asp:TextBox></TD>
				</TR>
				<TR>
					<TD style="FONT-SIZE: 10pt; WIDTH: 90px" align="right">联系地址</TD>
					<TD colspan="4">
						<asp:TextBox id="txtLinkAddress" runat="server" Height="64px" Width="440px" Font-Size="10pt"
							TextMode="MultiLine"></asp:TextBox></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 90px" align="center"></TD>
					<TD style="WIDTH: 136px" align="center">
						<asp:Button id="btAdd" runat="server" Width="64px" Font-Size="10pt" Text="添加" onclick="btAdd_Click"></asp:Button></TD>
					<TD style="WIDTH: 40px"></TD>
					<TD style="WIDTH: 93px" align="center">
						<asp:Button id="btMod" runat="server" Width="64px" Font-Size="10pt" Text="保存" onclick="btMod_Click"></asp:Button></TD>
					<TD align="center">
						<asp:Button id="btcancel" runat="server" Width="59px" Font-Size="10pt" Text="返回" onclick="btcancel_Click"></asp:Button></TD>
				</TR>
			</TABLE>
		</FORM>
	</body>
</HTML>
