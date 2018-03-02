<%@ Page language="c#" Codebehind="wfmStorageReport.aspx.cs" AutoEventWireup="True" Inherits="AMSApp.wfmStorageReport" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>wfmStorageReport</title>
		<meta content="False" name="vs_snapToGrid">
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<style type="text/css">* { BORDER-RIGHT: 0px; PADDING-RIGHT: 0px; BORDER-TOP: 0px; PADDING-LEFT: 0px; PADDING-BOTTOM: 0px; MARGIN: 0px; BORDER-LEFT: 0px; PADDING-TOP: 0px; BORDER-BOTTOM: 0px }
	BODY { FONT-SIZE: 12px; FONT-FAMILY: arial, 宋体, serif }
	#nav { WIDTH: 140px; LINE-HEIGHT: 24px; LIST-STYLE-TYPE: none; TEXT-ALIGN: left }
	#nav A { DISPLAY: block; PADDING-LEFT: 5px; WIDTH: 140px }
	#nav LI { BACKGROUND: #ccc; FLOAT: left; BORDER-BOTTOM: #fff 1px solid }
	#nav LI A:hover { BACKGROUND: #cc0000 }
	#nav A:link { COLOR: #666; TEXT-DECORATION: none }
	#nav A:visited { COLOR: #666; TEXT-DECORATION: none }
	#nav A:hover { FONT-WEIGHT: bold; COLOR: #fff; TEXT-DECORATION: none }
	#nav LI UL { LIST-STYLE-TYPE: none; TEXT-ALIGN: left }
	#nav LI UL LI { PADDING-LEFT: 5px; BACKGROUND: #ebebeb; WIDTH: 140px }
	#nav LI UL A { PADDING-LEFT: 5px; WIDTH: 140px }
	#nav LI UL A:link { COLOR: #666; TEXT-DECORATION: none }
	#nav LI UL A:visited { COLOR: #666; TEXT-DECORATION: none }
	#nav LI UL A:hover { FONT-WEIGHT: normal; BACKGROUND: #cc0000; COLOR: #f3f3f3; TEXT-DECORATION: none }
	#nav LI:hover UL { LEFT: auto }
	#nav LI.sfhover UL { LEFT: auto }
	#content { CLEAR: left }
	#nav UL.collapsed { DISPLAY: none }
	#PARENT { PADDING-LEFT: 5px; WIDTH: 140px }
		</style>
	</HEAD>
	<body leftMargin="0" background="image/coolwp2.jpg" MS_POSITIONING="GridLayout">
		<P>
			<TABLE id="tblStorageReportMenu" style="Z-INDEX: 101; LEFT: 8px; POSITION: absolute; TOP: 8px"
				cellSpacing="1" cellPadding="1" width="146" align="left" border="0" runat="server">
				<TR id="trnoprom" runat="server">
					<TD style="FONT-WEIGHT: bold; COLOR: #330033; HEIGHT: 38px" align="center" bgColor="#ebf0ec">没有权限</TD>
				</TR>
				<TR id="trSaleDailyChart" runat="server">
					<TD style="HEIGHT: 38px" align="center" background="image/anniu.jpg"><A style="FONT-SIZE: 12pt; COLOR: #ffffff; TEXT-DECORATION: none" onclick="parent.right.location='Storage/Report/wfmSaleDailyChart.aspx'"
							href="javascript:">日销售趋势图</A></TD>
				</TR>
				<TR id="trEnterStorageReport" runat="server">
					<TD style="HEIGHT: 38px" align="center" background="image/anniu.jpg"><A style="FONT-SIZE: 12pt; COLOR: #ffffff; TEXT-DECORATION: none" onclick="parent.right.location='Storage/Report/wfmEnterStorageReport.aspx'"
							href="javascript:">进仓报表</A></TD>
				</TR>
				<TR id="trReceiveMaterialReport" runat="server">
					<TD style="HEIGHT: 38px" align="center" background="image/anniu.jpg"><A style="FONT-SIZE: 12pt; COLOR: #ffffff; TEXT-DECORATION: none" onclick="parent.right.location='Storage/Report/wfmReceiveMaterialReport.aspx'"
							href="javascript:">领料报表</A></TD>
				</TR>
				<TR id="trStorageCheckLog" runat="server">
					<TD style="HEIGHT: 38px" align="center" background="image/anniu.jpg"><A style="FONT-SIZE: 12pt; COLOR: #ffffff; TEXT-DECORATION: none" onclick="parent.right.location='Storage/Report/wfmStorageCheckLog.aspx'"
							href="javascript:">盘点日志查询</A></TD>
				</TR>								
			</TABLE>
		</P>
	</body>
</HTML>
