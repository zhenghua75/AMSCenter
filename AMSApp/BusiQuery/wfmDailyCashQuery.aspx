<%@ Page Language="c#" CodeBehind="wfmDailyCashQuery.aspx.cs" AutoEventWireup="True"
    Inherits="AMSApp.BusiQuery.wfmDailyCashQuery" %>

<%@ Register TagPrefix="uc1" TagName="ucPageView" Src="../ucPageView.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>wfmDailyCashQuery</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <script language="javascript" src="../js/calendar.js"></script>
    <link rel="stylesheet" href="../css/window.css">
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
    <table id="Table1" border="0" cellspacing="1" cellpadding="5" width="95%">
        <tr>
            <td style="color: #330033; font-size: 15pt; font-weight: bold" align="center">
                操作员当日收银统计
            </td>
        </tr>
    </table>
    <table id="Table2" border="1" cellspacing="1" cellpadding="1" width="95%">
        <tr>
            <td>
                <table id="Table3" border="0" cellspacing="1" cellpadding="1" width="100%">
                    <tr>
                        <td style="width: 129px" align="right">
                            <asp:Label ID="Label6" runat="server" Font-Size="10pt" Style="z-index: 0">门店</asp:Label>
                        </td>
                        <td style="width: 202px">
                            <asp:DropDownList Style="z-index: 0" ID="ddlDept" runat="server" Font-Size="10pt"
                                Width="184px" AutoPostBack="True" OnSelectedIndexChanged="ddlDept_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 171px" align="right">
                            <asp:Label ID="Label5" runat="server" Font-Size="10pt" Style="z-index: 0">开始时间</asp:Label>
                        </td>
                        <td style="width: 156px" align="left">
                            <input id="txtBegin" onfocus="HS_setDate(this)" value="<%=strBeginDate%>" readonly
                                size="18" name="txtBegin" style="z-index: 0; width: 144px; height: 21px">
                        </td>
                        <td style="width: 81px" align="right">
		<FONT face="宋体">
                                        <asp:Label ID="Label9" runat="server" Font-Size="10pt" Text="片区"></asp:Label>
		</FONT>
                        </td>
                        <td style="width: 33px">
		<FONT face="宋体">
                                        <asp:DropDownList ID="ddlRegion" runat="server" AutoPostBack="True" 
                                            onselectedindexchanged="ddlRegion_SelectedIndexChanged">
                                        </asp:DropDownList>
		</FONT>
                        </td>
                        <td>
                            <asp:Button ID="btQuery" runat="server" Font-Size="10pt" Width="56px" Text="查询" OnClick="btQuery_Click">
                            </asp:Button>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 129px" align="right">
                            <font face="宋体">
										<asp:label id="Label7" runat="server" Font-Size="10pt" style="Z-INDEX: 0">操作员</asp:label></font>
                        </td>
                        <td style="width: 202px">
                            <asp:DropDownList ID="ddlOper" runat="server" Font-Size="10pt" Width="184px" Style="z-index: 0">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 171px" align="right">
                            <asp:Label ID="Label4" runat="server" Font-Size="10pt" Style="z-index: 0">结束时间</asp:Label>
                        </td>
                        <td style="width: 156px">
                            <input id="txtEnd" onfocus="HS_setDate(this)" value="<%=strEndDate%>" readonly size="18"
                                name="txtEnd" style="z-index: 0; width: 144px; height: 21px">
                        </td>
                        <td style="width: 81px" align="right">
                        </td>
                        <td style="width: 33px">
                        </td>
                        <td>
                            <font face="宋体"></font>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 129px" align="right">
                        </td>
                        <td style="width: 202px">
                        </td>
                        <td style="width: 171px" align="right">
                        </td>
                        <td style="width: 156px">
                        </td>
                        <td style="width: 81px" align="right">
                        </td>
                        <td style="width: 33px">
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table id="Table4" border="0" cellspacing="1" cellpadding="1" width="10000px">
        <tr>
            <td align="center">
                <uc1:ucPageView ID="UcPageView1" runat="server" Visible="true"></uc1:ucPageView>
            </td>
        </tr>
    </table>
    <table id="Table5" border="0" cellspacing="1" cellpadding="1" width="95%">
        <tr>
            <td align="center">
                <asp:Label ID="lblSum" runat="server" Font-Size="12pt" Width="90%" ForeColor="#C00000"></asp:Label>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
