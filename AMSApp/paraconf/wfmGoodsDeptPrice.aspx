<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="wfmGoodsDeptPrice.aspx.cs" Inherits="AMSApp.paraconf.wfmGoodsDeptPrice" %>
<%@ Register TagPrefix="uc1" TagName="ucPageView" Src="../ucPageView.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <TABLE id="Table1" cellSpacing="1" cellPadding="5" width="95%" border="0">
				<TR>
					<TD style="FONT-WEIGHT: bold; FONT-SIZE: 15pt; COLOR: #330033" align="center">商品门店单价管理</TD>
				</TR>
				<tr>
					<td align="center"><asp:label id="Label4" runat="server" Font-Size="10pt">商品编号：</asp:label>
                    <asp:label id="lblGoodsId" runat="server" Font-Size="10pt"></asp:label>
                    <asp:label id="Label3" runat="server" Font-Size="10pt">，商品名称：</asp:label>
                    <asp:label id="lblGoodsName" runat="server" Font-Size="10pt"></asp:label>
                    <asp:label id="Label5" runat="server" Font-Size="10pt">，商品单价：</asp:label>
						<asp:Label id="lblPrice" runat="server" Font-Size="10pt"></asp:Label>
						<%--<asp:Button id="btnAdd" runat="server" Text="添加" onclick="btnAdd_Click"></asp:Button>--%>
						<asp:Button id="Button1" runat="server" Text="返回商品管理" onclick="Button1_Click"></asp:Button></td>
				</tr>
			</TABLE>
			<TABLE id="Table4" cellSpacing="1" cellPadding="1" width="95%" border="0">
				<TR>
					<TD align="center"><uc1:ucpageview id="UcPageView1" runat="server" Visible="true"></uc1:ucpageview></TD>
				</TR>
			</TABLE>
    </form>
</body>
</html>
