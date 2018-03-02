<%@ Page Language="c#" CodeBehind="wfmConsItem.aspx.cs" AutoEventWireup="True" Inherits="AMSApp.BusiQuery.wfmConsItem" %>

<%@ Register TagPrefix="uc1" TagName="ucPageView" Src="../ucPageView.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>wfmConsItem</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content=" JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <script language="javascript" src="../js/calendar.js"></script>
</head>
<body ms_positioning="GridLayout" onload="<%=strExcelPath%>" bgcolor="#feeff8">
    <form id="Form1" method="post" runat="server">
    <table id="Table1" cellspacing="1" cellpadding="5" width="95%" border="0">
        <tr>
            <td style="color: #330033; font-size: 15pt; font-weight: bold" align="center">
                消费统计查询
            </td>
        </tr>
    </table>
    <table id="Table2" cellspacing="1" cellpadding="1" width="95%" border="1">
        <tr>
            <td>
                <table id="Table3" cellspacing="1" cellpadding="1" width="100%" border="0">
                    <tr>
                        <td style="width: 99px; height: 27px" align="right">
                            <asp:Label ID="Label1" runat="server" Font-Size="10pt">会员类型</asp:Label>
                        </td>
                        <td style="width: 147px; height: 27px">
                            <asp:DropDownList ID="ddlAssType" runat="server" Font-Size="10pt" Height="24px" Width="120px">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 99px; height: 27px" align="right">
                            <asp:Label ID="Label9" runat="server" Font-Size="10pt">消费流水</asp:Label>
                        </td>
                        <td style="width: 143px; height: 27px">
                            <asp:TextBox ID="txtSerial" runat="server" Font-Size="10pt" Width="112px"></asp:TextBox>
                        </td>
                        <td style="width: 99px; height: 27px" align="right">
                            <asp:Label ID="Label8" runat="server" Font-Size="10pt">有效状态</asp:Label>
                        </td>
                        <td style="width: 142px; height: 27px">
                            <asp:DropDownList ID="ddlConsFlag" runat="server" Font-Size="10pt" Width="112px"
                                AutoPostBack="True">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 99px; height: 27px" align="right">
                            <asp:Label ID="Label10" runat="server" Font-Size="10pt">付费类型</asp:Label>
                        </td>
                        <td style="height: 27px">
                            <asp:DropDownList ID="ddlBillType" runat="server" Font-Size="10pt" Width="120px"
                                AutoPostBack="True">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 99px; height: 27px" align="right">
		<FONT face="宋体">
			                <asp:Label ID="Label14" runat="server" Font-Size="10pt" Text="片区"></asp:Label>
		</FONT>
                        </td>
                        <td>
		<FONT face="宋体">
                                        <asp:DropDownList ID="ddlRegion" runat="server" AutoPostBack="True" 
                                            onselectedindexchanged="ddlRegion_SelectedIndexChanged">
                                        </asp:DropDownList>
		</FONT>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 99px" align="right">
                            <font face="宋体"><asp:label id="Label2" runat="server" Font-Size="10pt">会员卡号</asp:label></font>
                        </td>
                        <td style="width: 147px">
                            <asp:TextBox ID="txtCardID" runat="server" Font-Size="10pt" Width="120px" MaxLength="7"></asp:TextBox>
                        </td>
                        <td style="width: 99px; height: 27px;" align="right">
                            <asp:Label ID="Label5" runat="server" Font-Size="10pt">开始时间</asp:Label>
                        </td>
                        <td style="width: 143px">
                            <input id="txtBegin" name="txtBegin" value="<%=strBeginDate%>" onfocus="HS_setDate(this)"
                                readonly size="13" style="width: 112px; height: 22px">
                        </td>
                        <td style="width: 99px; height: 27px;" align="right">
                            <asp:Label ID="Label6" runat="server" Font-Size="10pt">门店</asp:Label>
                        </td>
                        <td style="width: 142px">
                            <asp:DropDownList ID="ddlDept" runat="server" AutoPostBack="True" Font-Size="10pt"
                                Width="112px" OnSelectedIndexChanged="ddlDept_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td align="right" style="width: 99px; height: 27px">
                            <asp:Label ID="Label11" runat="server" Font-Size="10pt">是否确认</asp:Label>
                        </td>
                        <td>
                            <font face="宋体">
										<asp:DropDownList id="ddlConfirm" runat="server" Width="120px">
											<asp:ListItem Value="全部" Selected="True">全部</asp:ListItem>
											<asp:ListItem Value="已确认">已确认</asp:ListItem>
											<asp:ListItem Value="未确认">未确认</asp:ListItem>
										</asp:DropDownList></font>
                        </td>
                        <td style="width: 99px; height: 27px;" align="right">
                            <asp:Label Style="z-index: 0" ID="Label13" runat="server" Font-Size="10pt">会员状态</asp:Label>
                            </td>
                            <td style="width: 142px">
                                <font face="宋体">
											<asp:dropdownlist style="Z-INDEX: 0" id="ddlAssState" runat="server" Font-Size="10pt" Width="144px"
												Height="24px"></asp:dropdownlist></font></FONT>
                            </td>
                        
                    </tr>
                    <tr>
                        <td style="width: 99px" align="right">
                            <asp:Label ID="Label3" runat="server" Font-Size="10pt">会员姓名</asp:Label>
                        </td>
                        <td style="width: 147px">
                            <asp:TextBox ID="txtAssName" runat="server" Font-Size="10pt" Width="120px"></asp:TextBox>
                        </td>
                        <td style="width: 99px; height: 27px;" align="right">
                            <asp:Label ID="Label4" runat="server" Font-Size="10pt">结束时间</asp:Label>
                        </td>
                        <td style="width: 143px">
                            <input id="txtEnd" name="txtEnd" value="<%=strEndDate%>" onfocus="HS_setDate(this)"
                                readonly size="13" style="width: 112px; height: 22px">
                        </td>
                        <td style="width: 99px; height: 27px;" align="right">
                            <asp:Label ID="Label7" runat="server" Font-Size="10pt">操作员</asp:Label>
                        </td>
                        <td style="width: 142px">
                            <asp:DropDownList ID="ddlOper" runat="server" Font-Size="10pt" Width="112px">
                            </asp:DropDownList>
                        </td>
                        <td align="right" style="width: 99px; height: 27px">
                            <asp:Label ID="Label12" runat="server" Font-Size="10pt">是否套餐</asp:Label>
                        </td>
                        <td>
										<asp:DropDownList id="ddlPackage" runat="server" Width="120px">
											<asp:ListItem Value="全部" Selected="True">全部</asp:ListItem>
											<asp:ListItem Value="是">是</asp:ListItem>
											<asp:ListItem Value="否">否</asp:ListItem>
										</asp:DropDownList>
                        </td>
                        <td colspan="2" align="center">
                            <asp:Button ID="btQuery" runat="server" Font-Size="10pt" Width="56px" Text="查询" OnClick="btQuery_Click">
                            </asp:Button>
                            <asp:Button ID="btnExcel" runat="server" Font-Size="10pt" Width="56px" Text="导出">
                            </asp:Button>
                            <asp:Button ID="Button1" runat="server" Text="确认" OnClick="Button1_Click"></asp:Button>
                            <asp:Button ID="Button2" runat="server" Text="退货耗损查询" OnClick="Button2_Click"></asp:Button>
                            <asp:Button ID="Button3" runat="server" Text="查询套餐" OnClick="Button3_Click"></asp:Button>
                        </td>
                    </tr>
                    </table>
            </td>
        </tr>
    </table>
    <table id="Table4" cellspacing="1" cellpadding="1" width="95%" border="0" runat="server">
        <tr>
            <td align="center">
                <uc1:ucPageView ID="UcPageView1" runat="server" Visible="true"></uc1:ucPageView>
            </td>
        </tr>
    </table>
    <table id="Table5" cellspacing="1" cellpadding="1" width="95%" border="0">
        <tr>
            <td align="center">
                <asp:Label ID="lblSum" runat="server" Font-Size="12pt" Width="90%" ForeColor="#C00000"></asp:Label>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
