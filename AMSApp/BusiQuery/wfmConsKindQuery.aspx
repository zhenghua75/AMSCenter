<%@ Page Language="c#" CodeBehind="wfmConsKindQuery.aspx.cs" AutoEventWireup="True"
    Inherits="AMSApp.BusiQuery.wfmConsKindQuery" %>

<%@ Register TagPrefix="uc1" TagName="ucPageView" Src="../ucPageView.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>wfmConsKindQuery</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <script language="javascript" src="../js/calendar.js"></script>
</head>
<body bgcolor="#feeff8" onload="<%=strExcelPath%>" ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
    <table id="Table1" cellspacing="1" cellpadding="5" width="95%" border="0">
        <tr>
            <td style="font-weight: bold; font-size: 15pt; color: #330033" align="center">
                消费分类统计
            </td>
        </tr>
    </table>
    <table id="Table2" cellspacing="1" cellpadding="1" width="95%" border="1">
        <tr>
            <td>
                <table id="Table3" cellspacing="1" cellpadding="1" width="100%" border="0">
                    <tr>
                        <td style="width: 129px" align="right">
                            <asp:Label ID="Label5" runat="server" Font-Size="10pt">开始时间</asp:Label>
                        </td>
                        <td style="width: 151px">
                            <input id="txtBegin" onfocus="HS_setDate(this)" readonly type="text" size="11" value="<%=strBeginDate%>"
                                name="txtBegin">
                        </td>
                        <td style="width: 171px" align="right">
                            <asp:CheckBox ID="chbAssType" runat="server" Font-Size="10pt" Checked="True" Text="会员类型"
                                OnCheckedChanged="chbAssType_CheckedChanged"></asp:CheckBox>
                        </td>
                        <td style="width: 156px">
                            <asp:DropDownList ID="ddlAssType" runat="server" Font-Size="10pt" Height="24px" Width="144px">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 81px" align="right">
		<FONT face="宋体">
			                <asp:Label ID="Label9" runat="server" Font-Size="10pt" Text="片区"></asp:Label>
		</FONT>
                        </td>
                        <td style="width: 197px">
		<FONT face="宋体">
                                        <asp:DropDownList ID="ddlRegion" runat="server" AutoPostBack="True" 
                                            onselectedindexchanged="ddlRegion_SelectedIndexChanged">
                                        </asp:DropDownList>
		</FONT>
                        </td>
                        <td>
                            <asp:Button ID="btQuery" runat="server" Font-Size="10pt" Text="查询" Width="56px" OnClick="btQuery_Click">
                            </asp:Button>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 129px" align="right">
                            <font face="宋体"><asp:label id="Label4" runat="server" Font-Size="10pt">结束时间</asp:label></font>
                        </td>
                        <td style="width: 151px">
                            <input id="txtEnd" onfocus="HS_setDate(this)" readonly type="text" size="11" value="<%=strEndDate%>"
                                name="txtEnd">
                        </td>
                        <td style="width: 171px" align="right">
                            <font face="宋体"><asp:checkbox id="chbGoodsType" runat="server" Font-Size="10pt" Checked="True" Text="商品类型" oncheckedchanged="chbGoodsType_CheckedChanged"></asp:checkbox></font>
                        </td>
                        <td style="width: 156px">
                            <asp:DropDownList ID="ddlGoodsType" runat="server" Font-Size="10pt" Height="24px"
                                Width="144px">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 81px" align="right">
                            <asp:Label ID="Label1" runat="server" Font-Size="10pt">产品名称</asp:Label>
                        </td>
                        <td style="width: 197px" align="left">
                            <asp:TextBox ID="txtGoodsName" runat="server" Font-Size="10pt" Width="176px"></asp:TextBox>
                        </td>
                        <td>
                            <font face="宋体"><asp:button id="btnExcel" runat="server" Font-Size="10pt" Text="导出" Width="56px"></asp:button></font>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 129px" align="right">
                            <asp:CheckBox ID="chbDate" runat="server" Font-Size="10pt" Text="日期" Checked="True">
                            </asp:CheckBox>
                        </td>
                        <td style="width: 151px">
                        </td>
                        <td style="width: 171px" align="right">
                            <asp:CheckBox ID="chbDept" runat="server" Font-Size="10pt" Checked="True" Text="门店"
                                OnCheckedChanged="chbDept_CheckedChanged"></asp:CheckBox>
                        </td>
                        <td style="width: 156px">
                            <font face="宋体"><asp:dropdownlist id="ddlDept" runat="server" Font-Size="10pt" Width="144px"></asp:dropdownlist></font>
                        </td>
                        <td style="width: 81px" align="right">
                        </td>
                        <td style="width: 197px">
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table id="Table4" cellspacing="1" cellpadding="1" width="95%" border="0">
        <tr>
            <td align="center" width="35%">
                <asp:Label ID="lblSumCount" runat="server" Font-Size="12pt" Width="258px" ForeColor="Red">总数量：0</asp:Label>
            </td>
            <td align="center" width="35%">
                <asp:Label ID="lblSumFee" runat="server" Font-Size="12pt" Width="283px" ForeColor="Red">总金额：0元</asp:Label>
            </td>
            <td width="30%">
            </td>
        </tr>
        <tr>
            <td align="center" colspan="3">
                <uc1:ucPageView ID="UcPageView1" runat="server" Visible="true"></uc1:ucPageView>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
