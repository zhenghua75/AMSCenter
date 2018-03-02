<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="wfmAssInfoDetail.aspx.cs" Inherits="AMSApp.BusiQuery.wfmAssInfoDetail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body bgColor=#feeff8 >
    <form id="form1" runat="server">
    <div>
    <TABLE id="Table1" border="0" cellSpacing="1" cellPadding="5" width="95%">
					<TR>
						<TD style="FONT-WEIGHT: bold; FONT-SIZE: 15pt; COLOR: #330033" align="center">会员挂失</TD>
					</TR>
				</TABLE>
                <TABLE id="Table2" border="1" cellSpacing="1" cellPadding="1" width="95%">
					<TR>
						<TD>
                <table border="0" cellSpacing="1" cellPadding="1" width="200px">
                    <tr>
                        <td>
                            <asp:Label ID="Label1" runat="server" Text="会员卡号" Font-Size="10pt"></asp:Label>
                        </td><td>
                            <asp:Label ID="lblCardId" runat="server" Text="lblCardId" Font-Size="10pt"></asp:Label>
                        </td>
                        </tr><tr>
                        <td>
                            <asp:Label ID="Label3" runat="server" Text="会员姓名" Font-Size="10pt"></asp:Label>
                        </td><td>
                            <asp:Label ID="lblAssName" runat="server" Text="lblAssName" Font-Size="10pt"></asp:Label>
                        </td>
                        </tr><tr>
                        <td>
                            <asp:Label ID="Label5" runat="server" Text="联系电话" Font-Size="10pt"></asp:Label>
                        </td><td>
                            <asp:Label ID="lblLinkPhone" runat="server" Text="lblLinkPhone" Font-Size="10pt"></asp:Label>
                        </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label7" runat="server" Text="会员状态" Font-Size="10pt"></asp:Label>
                            </td><td>
                                <asp:Label ID="lblAssState" runat="server" Text="lblAssState" Font-Size="10pt"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label9" runat="server" Text="当前余额" Font-Size="10pt"></asp:Label>
                            </td><td>
                                <asp:Label ID="lblCharge" runat="server" Text="lblCharge" Font-Size="10pt"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label11" runat="server" Text="门店" Font-Size="10pt"></asp:Label>
                            </td><td>
                                <asp:Label ID="lblDept" runat="server" Text="lblDept" Font-Size="10pt"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label13" runat="server" Text="创建日期" Font-Size="10pt"></asp:Label>
                            </td><td>
                                <asp:Label ID="lblCreateDate" runat="server" Text="lblCreateDate" Font-Size="10pt"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label15" runat="server" Text="联系地址" Font-Size="10pt"></asp:Label>
                            </td><td>
                                <asp:Label ID="lblLinkAddress" runat="server" Text="lblLinkAddress" Font-Size="10pt"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label17" runat="server" Text="操作日期" Font-Size="10pt"></asp:Label>
                            </td><td>
                                <asp:Label ID="lblOperDate" runat="server" Text="lblOperDate" Font-Size="10pt"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                        <td>
                            <asp:HiddenField ID="hfAssId" runat="server" />
                            <asp:HiddenField ID="hfAssState" runat="server" />
                            <asp:Button ID="Button1" runat="server" Text="挂失" onclick="Button1_Click" />
                        </td><td>
                            <asp:Button ID="Button2" runat="server" onclick="Button2_Click" Text="返回" />
                        </td>
                    </tr>
                    
                </table>
                </TD></TR></TABLE>
    </div>
    </form>
</body>
</html>
