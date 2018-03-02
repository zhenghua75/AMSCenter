<%@ Register TagPrefix="uc1" TagName="ucPageView" Src="../ucPageView.ascx" %>

<%@ Page Language="c#" CodeBehind="wfmShcQuery.aspx.cs" AutoEventWireup="True" Inherits="AMSApp.Employ.wfmShcQuery" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>wfmShcQuery</title>
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
                员工每日排班表
            </td>
        </tr>
    </table>
    <table id="Table5" cellspacing="0" cellpadding="0" width="95%" align="center" border="1">
        <tr>
            <td>
                <table id="Table2" cellspacing="0" cellpadding="0" width="100%" border="0">
                    <tr>
                        <td style="width: 224px" align="right">
                        </td>
                        <td style="width: 174px" align="right">
                            <asp:Label ID="Label4" runat="server" Font-Size="10pt" Width="75px">排班日期</asp:Label>
                        </td>
                        <td style="width: 264px">
                            <font face="宋体"><INPUT id=txtSchDate style="WIDTH: 136px; HEIGHT: 22px" onfocus=HS_setDate(this) readOnly type=text size=17 value="<%=strSchDate%>" name=txtSchDate></font>
                        </td>
                        <td style="width: 158px">
                            <asp:Button ID="btquery" runat="server" Width="65px" Text="查询" OnClick="btquery_Click">
                            </asp:Button>
                        </td>
                        <td style="width: 137px">
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
