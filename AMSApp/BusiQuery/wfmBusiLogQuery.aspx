<%@ Register TagPrefix="uc1" TagName="ucPageView" Src="../ucPageView.ascx" %>

<%@ Page Language="c#" CodeBehind="wfmBusiLogQuery.aspx.cs" AutoEventWireup="True"
    Inherits="AMSApp.BusiQuery.wfmBusiLogQuery" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>wfmBusiLogQuery</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <script language="javascript" src="../js/calendar.js"></script>
</head>
<body ms_positioning="GridLayout" onload="<%=strExcelPath%>" bgcolor="#feeff8">
    <form id="Form1" method="post" runat="server">
    <table id="Table1" cellspacing="1" cellpadding="5" width="95%" border="0">
        <tr>
            <td style="color: #330033; font-size: 15pt; font-weight: bold" align="center">
                操作员日志查询
            </td>
        </tr>
    </table>
    <table id="Table2" cellspacing="1" cellpadding="1" width="95%" border="1">
        <tr>
            <td>
                <table id="Table3" cellspacing="1" cellpadding="1" width="100%" border="0">
                    <tr>
                        <td style="width: 171px" align="right">
                            <font face="宋体">
										<asp:label id="Label2" runat="server" Font-Size="10pt">会员卡号</asp:label></font>
                        </td>
                        <td style="width: 151px">
                            <asp:TextBox ID="txtCardID" runat="server" Font-Size="10pt" Width="96px" MaxLength="7"></asp:TextBox>
                        </td>
                        <td style="width: 171px" align="right">
		<FONT face="宋体">
			                <asp:Label ID="Label9" runat="server" Font-Size="10pt" Text="片区"></asp:Label>
		</FONT>
	                    </td><td>
		<FONT face="宋体">
                                        <asp:DropDownList ID="ddlRegion" runat="server" AutoPostBack="True" 
                                            onselectedindexchanged="ddlRegion_SelectedIndexChanged">
                                        </asp:DropDownList>
		</FONT>
	                    </td><td></td>
                    </tr>
                    <tr>
                        <td style="width: 171px" align="right">
                            <asp:Label ID="Label5" runat="server" Font-Size="10pt">开始时间</asp:Label>
                        </td>
                        <td style="width: 156px">
                            <input id="txtBegin" onfocus="HS_setDate(this)" readonly size="11" value="<%=strBeginDate%>"
                                name="txtBegin">
                        </td>
                        <td style="width: 171px" align="right">
                            <asp:Label ID="Label6" runat="server" Font-Size="10pt">门店</asp:Label>
                        </td>
                        <td style="width: 259px">
                            <asp:DropDownList ID="ddlDept" runat="server" AutoPostBack="True" Font-Size="10pt"
                                Width="144px" OnSelectedIndexChanged="ddlDept_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:Button ID="btQuery" runat="server" Font-Size="10pt" Width="56px" Text="查询" OnClick="btQuery_Click">
                            </asp:Button>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 171px" align="right">
                            <asp:Label ID="Label4" runat="server" Font-Size="10pt">结束时间</asp:Label>
                        </td>
                        <td style="width: 156px">
                            <input id="txtEnd" onfocus="HS_setDate(this)" readonly size="11" value="<%=strEndDate%>"
                                name="txtEnd">
                        </td>
                        <td style="width: 171px" align="right">
                            <asp:Label ID="Label7" runat="server" Font-Size="10pt">操作员</asp:Label>
                        </td>
                        <td style="width: 259px">
                            <asp:DropDownList ID="ddlOper" runat="server" Font-Size="10pt" Width="144px">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <font face="宋体">
										<asp:button id="btnExcel" runat="server" Font-Size="10pt" Width="56px" Text="导出"></asp:button></font>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table id="Table4" cellspacing="1" cellpadding="1" width="95%" border="0">
        <tr>
            <td align="center">
                <uc1:ucPageView ID="UcPageView1" runat="server" Visible="true"></uc1:ucPageView>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
