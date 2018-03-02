<%@ Page language="c#" Codebehind="default.aspx.cs" AutoEventWireup="True" Inherits="AMSApp._default" EnableSessionState="True" debug="False" enableViewState="True" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>面包工坊网络中心登录</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<SCRIPT language="JScript" event="OnCompleted(hResult,pErrorObject, pAsyncContext)" for="foo">
			document.forms[0].txtMACAddr.value=unescape(MACAddr);
			document.forms[0].txtIPAddr.value=unescape(IPAddr);
			document.forms[0].txtDNSName.value=unescape(sDNSName);
			//document.formbar.submit();
		</SCRIPT>
		<SCRIPT language="JScript" event="OnObjectReady(objObject,objAsyncContext)" for="foo">
			if(objObject.IPEnabled != null && objObject.IPEnabled != "undefined" && objObject.IPEnabled == true)
			{
				if(objObject.MACAddress != null && objObject.MACAddress != "undefined")
				MACAddr = objObject.MACAddress;
				if(objObject.IPEnabled && objObject.IPAddress(0) != null && objObject.IPAddress(0) != "undefined")
				IPAddr = objObject.IPAddress(0);
				if(objObject.DNSHostName != null && objObject.DNSHostName != "undefined")
				sDNSName = objObject.DNSHostName;
			}
		</SCRIPT>
		<META content="MSHTML 6.00.2800.1106" name="GENERATOR">
	</HEAD>
	<body MS_POSITIONING="GridLayout" style="BACKGROUND-IMAGE: url(image/defbg.jpg); BACKGROUND-REPEAT: repeat-x">
		<OBJECT id="locator" classid="CLSID:76A64158-CB41-11D1-8B02-00600806D9B6" VIEWASTEXT>
		</OBJECT>
		<OBJECT id="foo" classid="CLSID:75718C9A-F029-11d1-A1AC-00C04FB6C223" VIEWASTEXT>
		</OBJECT>
		<SCRIPT language="jscript">
			var MACAddr ;
			var IPAddr ;
			var DomainAddr;
			var sDNSName;
			var service = locator.ConnectServer();
			service.Security_.ImpersonationLevel=3;
			service.InstancesOfAsync(foo, 'Win32_NetworkAdapterConfiguration');
		</SCRIPT>
		<table align="center" width="655" height="100%" border="0">
			<tr height="40%">
				<td></td>
			</tr>
			<tr height="350">
				<td background="image/login.jpg">
					<form id="Form1" method="post" runat="server">
						<table width="100%" height="100%" border="0">
							<tr height="30%">
								<td></td>
								<td></td>
							</tr>
							<tr height="30%">
								<td></td>
								<td></td>
							</tr>
							<tr valign="middle">
								<td width="240"></td>
								<td>&nbsp;&nbsp;&nbsp;&nbsp;
									<TABLE id="Table1" cellSpacing="1" cellPadding="1" width="384" border="0" style="WIDTH: 384px; HEIGHT: 32px">
										<TR>
											<TD style="WIDTH: 62px"><STRONG><FONT color="#660000">用户名:</FONT></STRONG></TD>
											<TD style="WIDTH: 92px">
												<asp:TextBox id="txtLoginID" runat="server" Width="88px" Font-Size="10pt"></asp:TextBox></TD>
											<TD style="WIDTH: 52px"><STRONG><FONT color="#660000">密 码:</FONT></STRONG></TD>
											<TD>
												<asp:TextBox id="txtPwd" runat="server" Width="88px" TextMode="Password" Font-Size="10pt"></asp:TextBox></TD>
											<TD><FONT face="宋体">
													<asp:Button id="Button1" runat="server" Text="进 入" BackColor="Orange" BorderStyle="Dotted" ForeColor="White"
														BorderColor="Gray" Font-Bold="True" Font-Names="YouYuan" Font-Size="10pt" onclick="Button1_Click"></asp:Button></FONT></TD>
										</TR>
									</TABLE>
								</td>
							</tr>
							<tr>
								<td colspan="2"><INPUT name="txtMACAddr" type="hidden"> <INPUT name="txtIPAddr" type="hidden"> <INPUT name="txtDNSName" type="hidden"></td>
							</tr>
						</table>
					</form>
				</td>
			</tr>
			<tr height="40%">
				<td></td>
			</tr>
		</table>
	</body>
</HTML>
