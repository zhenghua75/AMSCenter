<%@ Page Language="c#" CodeBehind="wfmSignCalc.aspx.cs" AutoEventWireup="True" Inherits="AMSApp.Employ.wfmSignCalc" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>wfmSignCalc</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <script language="javascript" src="../js/calendar.js"></script>
</head>
<body ms_positioning="GridLayout" bgcolor="#feeff8">
    <form id="Form1" method="post" runat="server">
    <table id="Table1" cellspacing="1" cellpadding="5" width="95%" border="0">
        <tr>
            <td style="font-weight: bold; font-size: 15pt; color: #330033" align="center">
                考勤计算
            </td>
        </tr>
    </table>
    <table id="Table2" cellspacing="1" cellpadding="1" width="95%" border="1">
        <tr>
            <td>
                <table id="Table3" cellspacing="1" cellpadding="1" width="100%" border="0">
                    <tr>
                        <td style="width: 171px" align="right">
                            <asp:Label ID="Label6" runat="server" Font-Size="10pt">门店</asp:Label>
                        </td>
                        <td style="width: 156px">
                            <asp:DropDownList ID="ddlDept" runat="server" Font-Size="10pt" Width="144px">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 81px" align="right">
                            <asp:Label ID="Label5" runat="server" Font-Size="10pt">开始时间</asp:Label>
                        </td>
                        <td style="width: 259px">
                            <input id="txtBegin" onfocus="HS_setDate(this)" readonly type="text" size="11" value="<%=strBeginDate%>"
                                name="txtBegin">
                        </td>
                        <td>
                            <asp:Button ID="btQuery" runat="server" Font-Size="10pt" Width="96px" Text="查询计算情况"
                                OnClick="btQuery_Click"></asp:Button>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 171px" align="right">
		<FONT face="宋体">
			                <asp:Label ID="Label9" runat="server" Font-Size="10pt" Text="片区"></asp:Label>
		</FONT>
                        </td>
                        <td style="width: 156px">
		<FONT face="宋体">
                                        <asp:DropDownList ID="ddlRegion" runat="server" AutoPostBack="True" 
                                            onselectedindexchanged="ddlRegion_SelectedIndexChanged">
                                        </asp:DropDownList>
		</FONT>
                        </td>
                        <td style="width: 81px" align="right">
                            <asp:Label ID="Label4" runat="server" Font-Size="10pt">结束时间</asp:Label>
                        </td>
                        <td style="width: 259px">
                            <input id="txtEnd" onfocus="HS_setDate(this)" readonly type="text" size="11" value="<%=strEndDate%>"
                                name="txtEnd">
                        </td>
                        <td>
                            <font face="宋体">
										<asp:button id="btCalc" runat="server" Font-Size="10pt" Width="80px" Text="开始计算" onclick="btCalc_Click"></asp:button></font>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table id="Table4" cellspacing="1" cellpadding="1" width="95%" height="100" border="0">
        <tr>
            <td align="center">
                <asp:Label ID="lblCalcResult" runat="server" Width="826px" Height="71px" Font-Size="12pt"
                    ForeColor="SlateBlue"></asp:Label>
            </td>
        </tr>
    </table>
    <table id="Table6" cellspacing="1" cellpadding="1" width="95%" border="0">
        <tr>
            <td align="left">
                <asp:Label ID="lblQueryTitle" runat="server" Width="600px" Height="16px" Font-Size="12pt"
                    ForeColor="SlateBlue"></asp:Label>
            </td>
            <td align="right">
                <font face="宋体">注：蓝色--已计算；红色--未计算</font>
            </td>
        </tr>
    </table>
    <table id="Table5" cellspacing="1" cellpadding="1" width="95%" height="200" border="0">
        <tr>
            <td align="center">
                <asp:Label ID="lbl1" runat="server" Font-Size="12pt" ForeColor="Red" Font-Underline="True">1</asp:Label>
            </td>
            <td align="center">
                <asp:Label ID="lbl2" runat="server" Font-Size="12pt" ForeColor="Red" Font-Underline="True">2</asp:Label>
            </td>
            <td align="center">
                <asp:Label ID="lbl3" runat="server" Font-Size="12pt" ForeColor="Red" Font-Underline="True">3</asp:Label>
            </td>
            <td align="center">
                <asp:Label ID="lbl4" runat="server" Font-Size="12pt" ForeColor="Red" Font-Underline="True">4</asp:Label>
            </td>
            <td align="center">
                <asp:Label ID="lbl5" runat="server" Font-Size="12pt" ForeColor="Red" Font-Underline="True">5</asp:Label>
            </td>
            <td align="center">
                <asp:Label ID="lbl6" runat="server" Font-Size="12pt" ForeColor="Red" Font-Underline="True">6</asp:Label>
            </td>
            <td align="center">
                <asp:Label ID="lbl7" runat="server" Font-Size="12pt" ForeColor="Red" Font-Underline="True">7</asp:Label>
            </td>
            <td align="center">
                <asp:Label ID="lbl8" runat="server" Font-Size="12pt" ForeColor="Red" Font-Underline="True">8</asp:Label>
            </td>
            <td align="center">
                <asp:Label ID="lbl9" runat="server" Font-Size="12pt" ForeColor="Red" Font-Underline="True">9</asp:Label>
            </td>
            <td align="center">
                <asp:Label ID="lbl10" runat="server" Font-Size="12pt" ForeColor="Red" Font-Underline="True">10</asp:Label>
            </td>
        </tr>
        <tr>
            <td align="center">
                <asp:Label ID="lbl11" runat="server" Font-Size="12pt" ForeColor="Red" Font-Underline="True">11</asp:Label>
            </td>
            <td align="center">
                <asp:Label ID="lbl12" runat="server" Font-Size="12pt" ForeColor="Red" Font-Underline="True">12</asp:Label>
            </td>
            <td align="center">
                <asp:Label ID="lbl13" runat="server" Font-Size="12pt" ForeColor="Red" Font-Underline="True">13</asp:Label>
            </td>
            <td align="center">
                <asp:Label ID="lbl14" runat="server" Font-Size="12pt" ForeColor="Red" Font-Underline="True">14</asp:Label>
            </td>
            <td align="center">
                <asp:Label ID="lbl15" runat="server" Font-Size="12pt" ForeColor="Red" Font-Underline="True">15</asp:Label>
            </td>
            <td align="center">
                <asp:Label ID="lbl16" runat="server" Font-Size="12pt" ForeColor="Red" Font-Underline="True">16</asp:Label>
            </td>
            <td align="center">
                <asp:Label ID="lbl17" runat="server" Font-Size="12pt" ForeColor="Red" Font-Underline="True">17</asp:Label>
            </td>
            <td align="center">
                <asp:Label ID="lbl18" runat="server" Font-Size="12pt" ForeColor="Red" Font-Underline="True">18</asp:Label>
            </td>
            <td align="center">
                <asp:Label ID="lbl19" runat="server" Font-Size="12pt" ForeColor="Red" Font-Underline="True">19</asp:Label>
            </td>
            <td align="center">
                <asp:Label ID="lbl20" runat="server" Font-Size="12pt" ForeColor="Red" Font-Underline="True">20</asp:Label>
            </td>
        </tr>
        <tr>
            <td align="center">
                <asp:Label ID="lbl21" runat="server" Font-Size="12pt" ForeColor="Red" Font-Underline="True">21</asp:Label>
            </td>
            <td align="center">
                <asp:Label ID="lbl22" runat="server" Font-Size="12pt" ForeColor="Red" Font-Underline="True">22</asp:Label>
            </td>
            <td align="center">
                <asp:Label ID="lbl23" runat="server" Font-Size="12pt" ForeColor="Red" Font-Underline="True">23</asp:Label>
            </td>
            <td align="center">
                <asp:Label ID="lbl24" runat="server" Font-Size="12pt" ForeColor="Red" Font-Underline="True">24</asp:Label>
            </td>
            <td align="center">
                <asp:Label ID="lbl25" runat="server" Font-Size="12pt" ForeColor="Red" Font-Underline="True">25</asp:Label>
            </td>
            <td align="center">
                <asp:Label ID="lbl26" runat="server" Font-Size="12pt" ForeColor="Red" Font-Underline="True">26</asp:Label>
            </td>
            <td align="center">
                <asp:Label ID="lbl27" runat="server" Font-Size="12pt" ForeColor="Red" Font-Underline="True">27</asp:Label>
            </td>
            <td align="center">
                <asp:Label ID="lbl28" runat="server" Font-Size="12pt" ForeColor="Red" Font-Underline="True">28</asp:Label>
            </td>
            <td align="center">
                <asp:Label ID="lbl29" runat="server" Font-Size="12pt" ForeColor="Red" Font-Underline="True">29</asp:Label>
            </td>
            <td align="center">
                <asp:Label ID="lbl30" runat="server" Font-Size="12pt" ForeColor="Red" Font-Underline="True">30</asp:Label>
            </td>
        </tr>
        <tr>
            <td align="center">
                <asp:Label ID="lbl31" runat="server" Font-Size="12pt" ForeColor="Red" Font-Underline="True">31</asp:Label>
            </td>
            <td align="center">
                <font face="宋体"></font>
            </td>
            <td align="center">
                <font face="宋体"></font>
            </td>
            <td align="center">
                <font face="宋体"></font>
            </td>
            <td align="center">
                <font face="宋体"></font>
            </td>
            <td align="center">
                <font face="宋体"></font>
            </td>
            <td align="center">
                <font face="宋体"></font>
            </td>
            <td align="center">
                <font face="宋体"></font>
            </td>
            <td align="center">
                <font face="宋体"></font>
            </td>
            <td align="center">
                <font face="宋体"></font>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
