<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="wfmSaleRatio.aspx.cs" Inherits="AMSApp.BusiQuery.wfmSaleRatio" %>

<%@ Register TagPrefix="uc1" TagName="ucPageView" Src="../ucPageView.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>月份各类产品销售占比表</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table id="Table1" border="0" cellspacing="1" cellpadding="5" width="95%">
            <tr>
                <td style="font-weight: bold; font-size: 15pt; color: #330033" align="center">
                    月份各类产品销售占比表
                </td>
            </tr>
        </table>
        <table id="Table2" border="1" cellspacing="1" cellpadding="1" width="95%">
            <tr>
                <td>
                    <table id="Table3" border="0" cellspacing="1" cellpadding="1" width="100%">
                        <tr>
                            <td align="right">
                                <asp:Label ID="Label1" runat="server" Font-Size="10pt">月份：</asp:Label>
                                <asp:DropDownList ID="ddlMonths" runat="server" Width="120px">
                                </asp:DropDownList>
                            </td>
                            <td align="right">
                                <asp:Button ID="Button1" runat="server" Text="查询" OnClick="Button1_Click"></asp:Button>
                                <asp:Button ID="Button2" runat="server" Text="导出" Enabled="False" OnClick="Button2_Click">
                                </asp:Button>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <table id="Table4" border="0" cellspacing="1" cellpadding="1" width="3048">
            <tr>
                <td align="center">
                    <uc1:ucPageView ID="UcPageView1" runat="server" Visible="true"></uc1:ucPageView>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
