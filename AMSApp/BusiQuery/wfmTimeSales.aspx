<%@ Register TagPrefix="uc1" TagName="ucPageView" Src="../ucPageView.ascx" %>

<%@ Page Language="c#" CodeBehind="wfmTimeSales.aspx.cs" AutoEventWireup="True" Inherits="AMSApp.BusiQuery.wfmTimeSales" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>wfmTimeSales</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <script language="javascript" src="../js/calendar.js"></script>
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
    <table id="Table1" border="0" cellspacing="1" cellpadding="5" width="95%">
        <tr>
            <td style="font-weight: bold; font-size: 15pt; color: #330033" align="center">
                门店时段消费统计
            </td>
        </tr>
    </table>
    <table id="Table2" border="1" cellspacing="1" cellpadding="1" width="95%">
        <tr>
            <td>
                <table id="Table3" border="0" cellspacing="1" cellpadding="1" width="100%">
                    <tr>
                        <td align="right">
                            <asp:Label ID="Label1" runat="server" Font-Size="10pt">门店：</asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlDept" runat="server" Width="120px">
                            </asp:DropDownList>
                        </td>
                        <td align="right">
                            <asp:Label ID="Label2" runat="server" Font-Size="10pt">开始日期：</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtBeginDate" runat="server" onfocus="HS_setDate(this)"></asp:TextBox>
                        </td>
                        <td align="right">
                            <asp:Label ID="Label3" runat="server" Font-Size="10pt">结束日期：</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtEndDate" runat="server" onfocus="HS_setDate(this)"></asp:TextBox>
                        </td>
                        <td align="right">
		<FONT face="宋体">
			                <asp:Label ID="Label9" runat="server" Font-Size="10pt" Text="片区"></asp:Label>
		</FONT>
	                    </td>
                        <td>
		<FONT face="宋体">
                                        <asp:DropDownList ID="ddlRegion" runat="server" AutoPostBack="True" 
                                            onselectedindexchanged="ddlRegion_SelectedIndexChanged">
                                        </asp:DropDownList>
		</FONT>
	                    </td>
                        <td>
                            <asp:Button ID="Button1" runat="server" Text="查询" OnClick="Button1_Click"></asp:Button>
                            <asp:Button ID="Button2" runat="server" Text="导出" OnClick="Button2_Click"></asp:Button>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table id="Table4" border="0" cellspacing="1" cellpadding="1" width="1224">
        <tr>
            <td align="center">
                <uc1:ucPageView ID="UcPageView1" runat="server" Visible="true"></uc1:ucPageView>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
