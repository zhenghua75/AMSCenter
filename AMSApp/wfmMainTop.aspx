<%@ Page language="c#" Codebehind="wfmMainTop.aspx.cs" AutoEventWireup="True" Inherits="AMSApp.wfmMainTop" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>wfmMainTop</title>
		<meta content="True" name="vs_snapToGrid">
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language="javascript">		
			var Condition = false;
			function window_onunload(url)
			{
			  if(Condition==false)
			  {
			     window.open(url+'?xclose=yes');
			  }			  
			}
			function openwin(surl)
			{
				//window.showModalDialog(surl,"_new","dialogHeight:360px;dialogWidth:360px;status:no");
				//var win  = 
				window.open(surl,"_new","height=380,width=410,toolbar=no,status=no,left=250,top=10,scrollbars=no,resizable=no");
				//win.focus();
			}	
		</script>
	</HEAD>
	<body bgColor="#ba4d85" leftMargin="0" topMargin="0" onload="<%=strPopStr%>" onunload="window_onunload('Exit.aspx')"
		ms_positioning="GridLayout">
		<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
			<TR>
				<TD style="BACKGROUND-IMAGE: url(image/banner.jpg); BACKGROUND-REPEAT: no-repeat;" height="80"
					width="100%" vAlign="bottom" align="right"><asp:label id="Label1" Height="23px" Width="200px" Font-Size="12pt" ForeColor="White" runat="server"></asp:label></TD>
			</TR>
			<TR>
				<TD style="BACKGROUND-IMAGE: url(image/banner1.jpg); BACKGROUND-REPEAT: no-repeat;"
					height="40" noWrap width="100%">
					<table height="35" width="100%" border="0">
						<tr>
							<td style="WIDTH: 142px" vAlign="bottom"><asp:label id="lbloper" Height="16px" Width="144px" Font-Size="11pt" ForeColor="White" runat="server"></asp:label></td>
							<td vAlign="bottom">&nbsp;&nbsp;&nbsp;<A style="FONT-SIZE: 11pt; COLOR: #ffffff; TEXT-DECORATION: none" onclick="parent.right.location='wfmWelcome.aspx';"
									href="javascript:">主界面</A>&nbsp;&nbsp;&nbsp; <A style="FONT-SIZE: 11pt; COLOR: #ffffff; TEXT-DECORATION: none" onclick="parent.left.location='wfmParaMenu.aspx';parent.right.location='wfmWelcome.aspx';"
									href="javascript:">参数管理</A>&nbsp;&nbsp;&nbsp; <A style="FONT-SIZE: 11pt; COLOR: #ffffff; TEXT-DECORATION: none" onclick="parent.left.location='wfmQueryMenu.aspx';parent.right.location='wfmWelcome.aspx';"
									href="javascript:">销售报表</A>&nbsp;&nbsp;&nbsp; <A style="FONT-SIZE: 11pt; COLOR: #ffffff; TEXT-DECORATION: none" onclick="parent.left.location='wfmEmpMenu.aspx';parent.right.location='wfmWelcome.aspx';"
									href="javascript:">员工管理</A>&nbsp;&nbsp;&nbsp; <A style="FONT-SIZE: 11pt; COLOR: #ffffff; TEXT-DECORATION: none" onclick="parent.left.location='zhenghua/wfmProduceMenu.aspx';parent.right.location='wfmWelcome.aspx';"
									href="javascript:">生产管理</A>&nbsp;&nbsp;&nbsp; <!--<A style="FONT-SIZE: 11pt; COLOR: #ffffff; TEXT-DECORATION: none" onclick="parent.left.location='wfmStorageMenu.aspx';parent.right.location='wfmWelcome.aspx';"
									href="javascript:">库存管理</A>&nbsp;&nbsp;&nbsp; <A style="FONT-SIZE: 11pt; COLOR: #ffffff; TEXT-DECORATION: none" onclick="parent.left.location='wfmStorageReport.aspx';parent.right.location='wfmWelcome.aspx';"
									href="javascript:">库存报表</A>&nbsp;&nbsp;&nbsp; --><A style="FONT-SIZE: 11pt; COLOR: #ffffff; TEXT-DECORATION: none" onclick="parent.left.location='wfmHelpMenu.aspx';parent.right.location='wfmWelcome.aspx';"
									href="javascript:">帮助</A>&nbsp;&nbsp;&nbsp; <A style="FONT-SIZE: 11pt; COLOR: #ffffff; TEXT-DECORATION: none" href="javascript:Condition=true;check('您是否确认退出系统？','Exit.aspx');"
									target="_parent">注销退出</A>
							</td>
						</tr>
					</table>
				</TD>
			</TR>
		</TABLE>
	</body>
</HTML>
