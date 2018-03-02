<%@ Page Language="c#" CodeBehind="wfmSaleDailyChart.aspx.cs" AutoEventWireup="True"
    Inherits="AMSApp.Storage.Report.wfmSaleDailyChart" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>wfmSaleDailyChart</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
</head>
<body ms_positioning="GridLayout" bgcolor="#feeff8">
    <form id="Form1" method="post" runat="server">
    <table id="Table1" style="z-index: 101; position: absolute; top: 9px; left: 29px"
        cellspacing="3" cellpadding="3" width="95%" align="center" border="0">
        <tr>
            <td align="center" style="color: #330033; font-size: 15pt; font-weight: bold">
                各分店销售额日走势（万元）
            </td>
        </tr>
    </table>
    <table class="table_content_group" id="Table2" style="z-index: 102; position: absolute;
        font-size: 10pt; top: 46px; left: 29px" cellspacing="0" cellpadding="0" width="95%"
        border="1">
        <tr>
            <td style="height: 29px" width="40%" align="right">
                <p>
                </p>
                <p>
                    <font face="宋体"> 部门
								<asp:dropdownlist style="Z-INDEX: 0" id="ddlDept" runat="server" Width="144px" AutoPostBack="True"
									Font-Size="10pt"></asp:dropdownlist>
                                    片区
                                        <asp:DropDownList ID="ddlRegion" runat="server" AutoPostBack="True" 
                                            
                        onselectedindexchanged="ddlRegion_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </font>
                </p>
            </td>
            <td style="height: 29px" width="40%" align="right">
                <p>
                </p>
                <p>
                    <font face="宋体"> 月份
								<asp:dropdownlist style="Z-INDEX: 0" id="ddlAcctMonth" runat="server" Width="112px" AutoPostBack="True"
									Font-Size="10pt"></asp:dropdownlist></font>
                </p>
            </td>
            <td style="width: 143px; height: 29px" nowrap align="right">
                Y轴标尺
            </td>
            <td style="height: 29px" width="40%">
                <asp:DropDownList ID="ddlYAXis" runat="server" Width="88px">
                    <asp:ListItem Value="2">2</asp:ListItem>
                    <asp:ListItem Value="3" Selected="True">3</asp:ListItem>
                    <asp:ListItem Value="4">4</asp:ListItem>
                    <asp:ListItem Value="5">5</asp:ListItem>
                    <asp:ListItem Value="10">10</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td style="height: 29px" width="40%">
                <p>
                </p>
                <p>
                    <input id="btnOk" type="button" value="查  询" name="btnOk" runat="server" onserverclick="btnOk_ServerClick"></p>
            </td>
            <td style="height: 29px" width="10%">
                <font face="宋体"></font>
            </td>
        </tr>
    </table>
    <asp:Image ID="Image1" Style="z-index: 103; position: absolute; top: 87px; left: 32px"
        runat="server"></asp:Image>
    </form>
</body>
</html>
