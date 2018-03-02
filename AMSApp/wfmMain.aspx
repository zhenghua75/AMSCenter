<%@ Page language="c#" Codebehind="wfmMain.aspx.cs" AutoEventWireup="True" Inherits="AMSApp.wfmMain" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<TITLE>面包工坊网络中心</TITLE>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<SCRIPT language="JavaScript">
			function MM_redirectorPage(url){
				window.location.href = url;
				return;
			}

			function MM_reloadPage(init) {  //reloads the window if Nav4 resized
				if (init==true) with (navigator) 
				{
					if ((appName=="Netscape")&&(parseInt(appVersion)==4)) {
						document.MM_pgW=innerWidth; document.MM_pgH=innerHeight; onresize=MM_reloadPage; 
						}
				}
				else if (innerWidth!=document.MM_pgW || innerHeight!=document.MM_pgH) 
					location.reload();
			}
			
			MM_reloadPage(true);

			function check(checkmsg,url) {
				if (confirm(checkmsg)) {
					MM_redirectorPage(url);
				}
			}
		</SCRIPT>
	</HEAD>
	<frameset framespacing="0" border="1" rows="120,80%" frameborder="0">
		<frame name="top" scrolling="no" src="wfmMainTop.aspx">
		<frameset id="frame" framespacing="0" border="1" cols="149,85%" frameborder="0">
			<frame name="left" scrolling="auto" noresize marginwidth="0" marginheight="0" src="">
			<frame name="right" scrolling="auto" src="wfmWelcome.aspx">
		</frameset>
	</frameset>
</HTML>
