<%@ Register TagPrefix="uc1" TagName="ucPageView" Src="../ucPageView.ascx" %>

<%@ Page Language="c#" CodeBehind="wfmWorkDailyEvery.aspx.cs" AutoEventWireup="True"
    Inherits="AMSApp.Employ.wfmWorkDailyEvery" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>wfmWorkDailyEvery</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <script language="javascript" src="../js/calendar.js"></script>
</head>
<body ms_positioning="GridLayout" bgcolor="#feeff8">
    <form id="Form1" method="post" runat="server" bgcolor="#feeff8">
    <table id="Table3" cellspacing="1" cellpadding="5" width="95%" align="center" border="0">
        <tr>
            <td style="font-weight: bold; font-size: 15pt; color: #330033" align="center">
                员工日工作安排计划
            </td>
        </tr>
    </table>
    <table id="Table5" cellspacing="0" cellpadding="0" width="95%" align="center" border="1">
        <tr>
            <td>
                <table id="Table2" cellspacing="0" cellpadding="1" width="100%" border="0">
                    <tr>
                        <td style="width: 70px" align="right">
                            <asp:Label ID="Label1" runat="server" Width="60px" Font-Size="10pt">门店</asp:Label>
                        </td>
                        <td style="width: 124px">
                            <asp:DropDownList ID="ddlDept" runat="server" Width="144px" Font-Size="10pt" Height="24px">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 143px" align="right">
                            <asp:Label ID="Label4" runat="server" Width="82px" Font-Size="10pt">要排班的日期</asp:Label>
                        </td>
                        <td style="width: 227px" align="left">
                            <input id="txtSchDate" onfocus="HS_setDate(this)" readonly type="text" size="17"
                                value="<%=strSchDate%>" name="txtSchDate">
                        </td>
                        <td style="width: 70px" align="right">
		<FONT face="宋体">
			                <asp:Label ID="Label9" runat="server" Font-Size="10pt" Text="片区"></asp:Label>
		</FONT>
	                    </td>
                        <td style="width: 124px">
		<FONT face="宋体">
                                        <asp:DropDownList ID="ddlRegion" runat="server" AutoPostBack="True" 
                                            onselectedindexchanged="ddlRegion_SelectedIndexChanged">
                                        </asp:DropDownList>
		</FONT>
	                    </td>
                        <td style="width: 103px">
                            <font face="宋体">
										<asp:button id="btquery" runat="server" Width="65px" Text="查询" onclick="btquery_Click"></asp:button></font>
                        </td>
                        <td style="width: 109px">
                            <asp:Button ID="btnadd" runat="server" Width="65px" Text="添加" OnClick="btnadd_Click">
                            </asp:Button>
                        </td>
                        <td style="width: 137px">
                            <asp:Button ID="btnExport" runat="server" Width="65px" Text="导入" OnClick="btnExport_Click">
                            </asp:Button>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table id="Table4" cellspacing="1" cellpadding="1" width="95%" border="0" align="center">
        <tr>
            <td align="center">
                <uc1:ucPageView ID="UcPageView1" runat="server" Visible="true"></uc1:ucPageView>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
