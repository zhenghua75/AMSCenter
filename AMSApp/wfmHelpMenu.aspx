<%@ Page language="c#" Codebehind="wfmHelpMenu.aspx.cs" AutoEventWireup="True" Inherits="AMSApp.wfmHelpMenu" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>wfmHelpMenu</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body MS_POSITIONING="GridLayout" background="image/coolwp2.jpg">
		<form id="Form1" method="post" runat="server">
			<P>
				<TABLE id="tblStorageMenu" style="Z-INDEX: 101; LEFT: 8px; POSITION: absolute; TOP: 8px"
					cellSpacing="1" cellPadding="1" width="146" align="left" border="0" runat="server">
					<TR id="trProductMake" runat="server">
						<TD style="HEIGHT: 38px" align="center" background="image/anniu.jpg"><A style="FONT-SIZE: 12pt; COLOR: #ffffff; TEXT-DECORATION: none" onclick="parent.right.location='AMSHelp/操作手册-生成管理.mht'"
								href="javascript:">生产管理</A></TD>
					</TR>
					<TR id="trStorage" runat="server">
						<TD style="HEIGHT: 38px" align="center" background="image/anniu.jpg"><A style="FONT-SIZE: 12pt; COLOR: #ffffff; TEXT-DECORATION: none" onclick="parent.right.location='AMSHelp/操作手册-库存管理.mht'"
								href="javascript:">库存管理</A></TD>
					</TR>
				</TABLE>
			</P>
		</form>
	</body>
</HTML>
