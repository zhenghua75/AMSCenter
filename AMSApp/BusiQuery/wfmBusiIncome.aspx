<%@ Page Language="c#" CodeBehind="wfmBusiIncome.aspx.cs" AutoEventWireup="True"
    Inherits="AMSApp.BusiQuery.wfmBusiIncome" %>

<%@ Register TagPrefix="uc1" TagName="ucPageView" Src="../ucPageView.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>wfmBusiIncome</title>
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
                ҵ����ͳ�Ʊ���
            </td>
        </tr>
    </table>
    <table id="Table2" cellspacing="1" cellpadding="1" width="95%" border="1">
        <tr>
            <td>
                <table id="Table3" cellspacing="1" cellpadding="1" width="100%" border="0">
                    <tr>
                        <td style="width: 171px" align="right">
                            <asp:Label ID="Label6" runat="server" Font-Size="10pt">�ŵ�</asp:Label>
                        </td>
                        <td style="width: 156px">
                            <asp:DropDownList ID="ddlDept" runat="server" Font-Size="10pt" Width="144px">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 81px" align="right">
                            <asp:Label ID="Label5" runat="server" Font-Size="10pt">��ʼ����</asp:Label>
                        </td>
                        <td style="width: 259px">
                            <input id="txtBegin" onfocus="HS_setDate(this)" readonly value="<%=strBeginDate%>"
                                name="txtBegin" style="width: 152px; height: 22px">
                        </td>
                        <td>
                            <asp:Button ID="btQuery" runat="server" Font-Size="10pt" Width="56px" Text="��ѯ" OnClick="btQuery_Click">
                            </asp:Button>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 171px" align="right">
		<FONT face="����">
			                <asp:Label ID="Label9" runat="server" Font-Size="10pt" Text="Ƭ��"></asp:Label>
		</FONT>
                        </td>
                        <td style="width: 156px">
		<FONT face="����">
                                        <asp:DropDownList ID="ddlRegion" runat="server" AutoPostBack="True" 
                                            onselectedindexchanged="ddlRegion_SelectedIndexChanged">
                                        </asp:DropDownList>
		</FONT>
                        </td>
                        <td style="width: 81px" align="right">
                            <asp:Label ID="Label1" runat="server" Font-Size="10pt">��������</asp:Label>
                        </td>
                        <td style="width: 259px">
                            <input id="txtEnd" onfocus="HS_setDate(this)" readonly value="<%=strEndDate%>" name="txtEnd"
                                style="width: 152px; height: 22px">
                        </td>
                        <td>
                            <font face="����">
										<asp:button id="btnExcel" runat="server" Font-Size="10pt" Width="56px" Text="����"></asp:button></font>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="5" style="color: #cc0000; font-size: 10pt">
                            ע��ԭ״̬�еĿ��û��ֺͽ�����ѯ�ŵ��޹أ�����ʾֵΪ���л�Ա��ֹ����ʼ���ڵĻ��ֺ�����ܺ͡�<br>
                            &nbsp;&nbsp;&nbsp; ��״̬�еĿ��û��ֺͽ�����ѯ�ŵ��޹أ�����ʾֵΪ���л�Ա��ֹ���������ڵĻ��ֺ�����ܺ͡�<br>
                            &nbsp;&nbsp;&nbsp; �����Ա�еĿ��û��ֺͽ�����ѯ�ŵ��޹أ�����ʾֵΪ��ѯ���ڷ�Χ�ڵ������Ա��ֹ���������ڵĻ��ֺ�����ܺ͡�
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
