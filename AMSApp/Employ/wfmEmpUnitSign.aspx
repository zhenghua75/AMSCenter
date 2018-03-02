<%@ Page Language="c#" CodeBehind="wfmEmpUnitSign.aspx.cs" AutoEventWireup="True"
    Inherits="AMSApp.Employ.wfmEmpUnitSign" %>

<%@ Register TagPrefix="uc1" TagName="ucPageView" Src="../ucPageView.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>wfmEmpUnitSign</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
</head>
<body ms_positioning="GridLayout" onload="<%=strExcelPath%>" bgcolor="#feeff8">
    <form id="Form1" method="post" runat="server">
    <table id="Table1" cellspacing="1" cellpadding="5" width="95%" border="0">
        <tr>
            <td style="font-weight: bold; font-size: 15pt; color: #330033" align="center">
                个人考勤查询
            </td>
        </tr>
    </table>
    <table id="Table2" cellspacing="1" cellpadding="1" width="95%" border="1">
        <tr>
            <td>
                <table id="Table3" cellspacing="1" cellpadding="1" width="100%" border="0">
                    <tr>
                        <td style="width: 171px" align="right">
                            <asp:Label ID="Label6" runat="server" Font-Size="10pt">员工卡号</asp:Label>
                        </td>
                        <td style="width: 156px">
                            <asp:TextBox ID="txtCardID" runat="server" Font-Size="10pt" MaxLength="4"></asp:TextBox>
                        </td>
                        <td style="width: 113px" align="right">
                            <asp:Label ID="Label5" runat="server" Font-Size="10pt">查询月份</asp:Label>
                        </td>
                        <td style="width: 243px">
                            <asp:TextBox ID="txtMonth" runat="server" Font-Size="10pt" Width="136px"></asp:TextBox><font
                                face="宋体"><FONT color="#ff6600" size="2">&nbsp;如：200801</FONT></font>
                        </td>
                        <td>
                            <asp:Button ID="btQuery" runat="server" Font-Size="10pt" Width="56px" Text="查询" OnClick="btQuery_Click">
                            </asp:Button>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 171px" align="right">
                            <asp:Label ID="Label1" runat="server" Font-Size="10pt">员工姓名</asp:Label>
                        </td>
                        <td style="width: 156px">
                            <asp:TextBox ID="txtEmpName" runat="server" Font-Size="10pt"></asp:TextBox>
                        </td>
                        <td style="width: 113px" align="right">
                            <asp:Label ID="Label2" runat="server" Font-Size="10pt">查询类型</asp:Label>
                        </td>
                        <td style="width: 243px">
                            <font face="宋体">
										<asp:DropDownList id="ddlType" runat="server" Font-Size="10pt" Width="168px"></asp:DropDownList></font>
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
