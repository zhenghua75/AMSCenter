<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="wfmGoodsDeptPriceDetail.aspx.cs" Inherits="AMSApp.paraconf.wfmGoodsDeptPriceDetail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <TABLE id="Table1" cellSpacing="1" cellPadding="5" width="95%" border="0">
				<TR>
					<TD align="center" style="COLOR: #330033; FONT-SIZE: 15pt; FONT-WEIGHT: bold">商品门店单价明细管理</TD>
				</TR>
			</TABLE>
			<table width="95%" align="center">
				<tr>
					<td align="left"><asp:Label id="Label1" runat="server" Font-Size="10pt">门店：</asp:Label></td>
					<td><asp:DropDownList id="ddlMD" runat="server"></asp:DropDownList></td>					
					<td align="left"><asp:Label id="Label3" runat="server" Font-Size="10pt">商品编号：</asp:Label></td>
					<td><asp:TextBox id="txtGoodsId" runat="server"></asp:TextBox></td>
					<td align="left"><asp:Label id="Label4" runat="server" Font-Size="10pt">商品名称：</asp:Label></td>
					<td><asp:TextBox id="txtGoodsName" runat="server"></asp:TextBox></td>
                    <td><asp:Label id="Label2" runat="server" Font-Size="10pt">商品单价：</asp:Label></td>
					<td><asp:TextBox id="txtPrice" runat="server"></asp:TextBox></td>
					<td><asp:Label id="Label5" runat="server" Font-Size="10pt">商品门店单价：</asp:Label></td>
					<td><asp:TextBox id="txtDeptPrice" runat="server"></asp:TextBox></td>
				</tr>
				<TR>
					<TD align="center" colSpan="10">
						<asp:Button id="Button1" runat="server" Text="添加" onclick="Button1_Click"></asp:Button>
						<asp:Button id="Button2" runat="server" Text="修改" onclick="Button2_Click"></asp:Button>
						<asp:Button id="Button3" runat="server" Text="删除" onclick="Button3_Click"></asp:Button>
						<asp:Button id="Button4" runat="server" Text="返回商品门店单价管理" onclick="Button4_Click"></asp:Button></TD>
				</TR>
			</table>
    </form>
</body>
</html>
