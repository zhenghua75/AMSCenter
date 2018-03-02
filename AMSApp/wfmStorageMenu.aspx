<%@ Page language="c#" Codebehind="wfmStorageMenu.aspx.cs" AutoEventWireup="True" Inherits="AMSApp.wfmStorageMenu" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>wfmStorageMenu</title>
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
			<TABLE id="tblStorageMenu" style="Z-INDEX: 101; LEFT: 8px; POSITION: absolute; TOP: 8px"
				cellSpacing="1" cellPadding="1" width="146" align="left" border="0" runat="server">
				<TR id="trnoprom" runat="server">
					<TD style="FONT-WEIGHT: bold; COLOR: #330033; HEIGHT: 38px" align="center" bgColor="#ebf0ec">没有权限</TD>
				</TR>
				<TR id="trSaleDailyCheck" runat="server">
					<TD style="HEIGHT: 38px" align="center" background="image/anniu.jpg"><A style="FONT-SIZE: 12pt; COLOR: #ffffff; TEXT-DECORATION: none" onclick="parent.right.location='Storage/wfmSaleDailyCheck.aspx'"
							href="javascript:">库存日盘点</A></TD>
				</TR>
				<TR id="trSaleLoseNew" runat="server">
					<TD style="HEIGHT: 38px" align="center" background="image/anniu.jpg"><A style="FONT-SIZE: 12pt; COLOR: #ffffff; TEXT-DECORATION: none" onclick="parent.right.location='Storage/wfmSaleLoseQuery.aspx'"
							href="javascript:">报损</A></TD>
				</TR>
				<TR id="trProvider" runat="server">
					<TD style="HEIGHT: 38px" align="center" background="image/anniu.jpg"><A style="FONT-SIZE: 12pt; COLOR: #ffffff; TEXT-DECORATION: none" onclick="parent.right.location='Storage/wfmProvider.aspx'"
							href="javascript:">供应商</A></TD>
				</TR>
				<TR id="trStockPlan" runat="server">
					<TD style="HEIGHT: 38px" align="center" background="image/anniu.jpg"><A style="FONT-SIZE: 12pt; COLOR: #ffffff; TEXT-DECORATION: none" onclick="parent.right.location='Storage/wfmStockPlan.aspx'"
							href="javascript:">采购计划</A></TD>
				</TR>
				<TR id="trBillOfEnterStorage" runat="server">
					<TD style="HEIGHT: 38px" align="center" background="image/anniu.jpg"><A style="FONT-SIZE: 12pt; COLOR: #ffffff; TEXT-DECORATION: none" onclick="parent.right.location='Storage/wfmBillOfEnterStorage.aspx'"
							href="javascript:">原材料进仓</A></TD>
				</TR>
				<TR id="trBillOfReceive" runat="server">
					<TD style="HEIGHT: 38px" align="center" background="image/anniu.jpg"><A style="FONT-SIZE: 12pt; COLOR: #ffffff; TEXT-DECORATION: none" onclick="parent.right.location='Storage/wfmBillOfReceive.aspx'"
							href="javascript:">原材料领用</A></TD>
				</TR>
				<TR id="trBillValidEnter" runat="server">
					<TD style="HEIGHT: 38px" align="center" background="image/anniu.jpg"><A style="FONT-SIZE: 12pt; COLOR: #ffffff; TEXT-DECORATION: none" onclick="parent.right.location='Storage/wfmBillValidEnter.aspx'"
							href="javascript:">订单验收入库</A></TD>
				</TR>
				<TR id="trProductMove" runat="server">
					<TD style="HEIGHT: 38px" align="center" background="image/anniu.jpg"><A style="FONT-SIZE: 12pt; COLOR: #ffffff; TEXT-DECORATION: none" onclick="parent.right.location='Storage/wfmProductMove.aspx'"
							href="javascript:">调拨</A></TD>
				</TR>
				<TR id="trDestroyConfirm" runat="server">
					<TD style="HEIGHT: 38px" align="center" background="image/anniu.jpg"><A style="FONT-SIZE: 12pt; COLOR: #ffffff; TEXT-DECORATION: none" onclick="parent.right.location='Storage/wfmDestroyConfirm.aspx'"
							href="javascript:">损耗确认</A></TD>
				</TR>
				<TR id="trStorageSet" runat="server">
					<TD style="HEIGHT: 38px" align="center" background="image/anniu.jpg"><A style="FONT-SIZE: 12pt; COLOR: #ffffff; TEXT-DECORATION: none" onclick="parent.right.location='Storage/wfmStorageSet.aspx'"
							href="javascript:">当前库存</A></TD>
				</TR>									
			</TABLE>
		</P>
	</body>
</HTML>
