<%@ Page language="c#" Codebehind="wfmQueryMenu.aspx.cs" AutoEventWireup="True" Inherits="AMSApp.wfmQueryMenu" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>wfmQueryMenu</title>
		<meta name="vs_snapToGrid" content="False">
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body MS_POSITIONING="GridLayout" leftmargin="0" background="image/coolwp2.jpg">
		<TABLE id="tblQueryMenu" cellSpacing="1" cellPadding="1" border="0" align="left"
			runat="server" style="width:146px;">
			<TR id="trnoprom" runat="server">
				<TD align="center" style="FONT-WEIGHT: bold; COLOR: #330033; HEIGHT: 38px" bgcolor="#ebf0ec">没有权限</TD>
			</TR>
			<TR id="trAssInfo" runat="server">
				<TD align="center" style="HEIGHT: 38px" background="image/anniu.jpg"><A style="FONT-SIZE: 12pt; COLOR: #ffffff; TEXT-DECORATION: none" onclick="parent.right.location='BusiQuery/wfmAssInfo.aspx'"
						href="javascript:">会员资料查询</A></TD>
			</TR>
			<TR id="trConsItem" runat="server">
				<TD align="center" style="HEIGHT: 38px" background="image/anniu.jpg"><A style="FONT-SIZE: 12pt; COLOR: #ffffff; TEXT-DECORATION: none" onclick="parent.right.location='BusiQuery/wfmConsItem.aspx'"
						href="javascript:">消费统计查询</A></TD>
			</TR>
			<TR valign="middle" id="trFillQuery" runat="server">
				<TD align="center" style="HEIGHT: 38px" background="image/anniu.jpg"><A style="FONT-SIZE: 12pt; COLOR: #ffffff; TEXT-DECORATION: none" onclick="parent.right.location='BusiQuery/wfmFillQuery.aspx'"
						href="javascript:">会员充值查询</A></TD>
			</TR>
			<TR valign="middle" id="trConsKindQuery" runat="server">
				<TD align="center" style="HEIGHT: 38px" background="image/anniu.jpg"><A style="FONT-SIZE: 12pt; COLOR: #ffffff; TEXT-DECORATION: none" onclick="parent.right.location='BusiQuery/wfmConsKindQuery.aspx'"
						href="javascript:">消费分类统计</A></TD>
			</TR>
			<TR valign="middle" id="trBusiLogQuery" runat="server">
				<TD align="center" style="HEIGHT: 38px" background="image/anniu.jpg"><A style="FONT-SIZE: 12pt; COLOR: #ffffff; TEXT-DECORATION: none" onclick="parent.right.location='BusiQuery/wfmBusiLogQuery.aspx'"
						href="javascript:">操作员日志</A></TD>
			</TR>
			<TR valign="middle" id="trTopQuery" runat="server">
				<TD align="center" style="HEIGHT: 38px" background="image/anniu.jpg"><A style="FONT-SIZE: 12pt; COLOR: #ffffff; TEXT-DECORATION: none" onclick="parent.right.location='BusiQuery/wfmTopQuery.aspx'"
						href="javascript:">销售排名榜</A></TD>
			</TR>
			<TR valign="middle" id="trBusiIncome" runat="server">
				<TD align="center" style="HEIGHT: 38px" background="image/anniu.jpg"><A style="FONT-SIZE: 12pt; COLOR: #ffffff; TEXT-DECORATION: none" onclick="parent.right.location='BusiQuery/wfmBusiIncome.aspx'"
						href="javascript:">业务量统计</A></TD>
			</TR>
			<TR valign="middle" id="trDataUpDownQuery" runat="server">
				<TD align="center" style="HEIGHT: 38px" background="image/anniu.jpg"><A style="FONT-SIZE: 12pt; COLOR: #ffffff; TEXT-DECORATION: none" onclick="parent.right.location='BusiQuery/wfmDataUpDownQuery.aspx'"
						href="javascript:">上下传数据查询</A></TD>
			</TR>
			<TR valign="middle" id="trDailyCashQuery" runat="server">
				<TD align="center" style="HEIGHT: 38px" background="image/anniu.jpg"><A style="FONT-SIZE: 12pt; COLOR: #ffffff; TEXT-DECORATION: none" onclick="parent.right.location='BusiQuery/wfmDailyCashQuery.aspx'"
						href="javascript:">当日收银</A></TD>
			</TR>			
			<TR valign="middle" id="trSaleDifSum" runat="server">
				<TD align="center" style="HEIGHT: 38px" background="image/anniu.jpg"><A style="FONT-SIZE: 12pt; COLOR: #ffffff; TEXT-DECORATION: none" onclick="parent.right.location='zhenghua/wfmSaleDifSum.aspx?type=1'"
						href="javascript:">销售差异汇总表</A></TD>
			</TR>		
			<TR valign="middle" id="trCashierDifSum" runat="server">
				<TD align="center" style="HEIGHT: 38px" background="image/anniu.jpg"><A style="FONT-SIZE: 12pt; COLOR: #ffffff; TEXT-DECORATION: none" onclick="parent.right.location='zhenghua/wfmSaleDifSum.aspx?type=2'"
						href="javascript:">收款差异统计表</A></TD>
			</TR>
			<TR id="trSaleDailyChart" runat="server">
					<TD style="HEIGHT: 38px" align="center" background="image/anniu.jpg"><A style="FONT-SIZE: 12pt; COLOR: #ffffff; TEXT-DECORATION: none" onclick="parent.right.location='Storage/Report/wfmSaleDailyChart.aspx'"
							href="javascript:">日销售趋势图</A></TD>
				</TR>		
				<TR id="trSalesSum" runat="server">
					<TD style="HEIGHT: 38px" align="center" background="image/anniu.jpg"><A style="FONT-SIZE: 12pt; COLOR: #ffffff; TEXT-DECORATION: none" onclick="parent.right.location='BusiQuery/wfmSalesSum.aspx'"
							href="javascript:">销售汇总表</A></TD>
				</TR>	
				<TR id="trTimeSales" runat="server">
					<TD style="HEIGHT: 38px" align="center" background="image/anniu.jpg"><A style="FONT-SIZE: 12pt; COLOR: #ffffff; TEXT-DECORATION: none" onclick="parent.right.location='BusiQuery/wfmTimeSales.aspx'"
							href="javascript:">门店时段消费统计</A></TD>
				</TR>
                
                <TR id="trSaleRatio" runat="server">
					<TD style="HEIGHT: 38px" align="center" background="image/anniu.jpg"><A style="FONT-SIZE: 10pt; COLOR: #ffffff; TEXT-DECORATION: none" onclick="parent.right.location='BusiQuery/wfmSaleRatio.aspx'"
							href="javascript:">月份各类产品销售占比表</A></TD>
				</TR>
                <TR id="trPerformanceOfDay" runat="server">
					<TD style="HEIGHT: 38px" align="center" background="image/anniu.jpg"><A style="FONT-SIZE: 12pt; COLOR: #ffffff; TEXT-DECORATION: none" onclick="parent.right.location='BusiQuery/wfmPerformanceOfDay.aspx'"
							href="javascript:">门店业绩日报表</A></TD>
				</TR>	
                <TR id="trConsItemUndo" runat="server">
					<TD style="HEIGHT: 38px" align="center" background="image/anniu.jpg"><A style="FONT-SIZE: 12pt; COLOR: #ffffff; TEXT-DECORATION: none" onclick="parent.right.location='BusiQuery/wfmConsItemUndo.aspx'"
							href="javascript:">零售撤销</A></TD>
				</TR>	
                <TR id="trUndoCheck" runat="server">
					<TD style="HEIGHT: 38px" align="center" background="image/anniu.jpg"><A style="FONT-SIZE: 12pt; COLOR: #ffffff; TEXT-DECORATION: none" onclick="parent.right.location='BusiQuery/wfmUndoCheck.aspx'"
							href="javascript:">撤销审核</A></TD>
				</TR>		
		</TABLE>
	</body>
</HTML>
