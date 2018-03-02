<%@ Page Language="C#" AutoEventWireup="true" UICulture="zh-cn" CodeBehind="wfmKPI.aspx.cs" Inherits="AMSApp.BusiQuery.wfmKPI" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table id="Table1" border="0" cellspacing="1" cellpadding="5" width="95%">
            <tr>
                <td style="font-weight: bold; font-size: 15pt; color: #330033" align="center">
                    业绩指标
                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td>
                    月份
                </td>
                <td>
                    <asp:DropDownList ID="ddlMonth" runat="server">
                    </asp:DropDownList>
                </td>
                <td>
                    门店
                </td>
                <td>
                    <asp:DropDownList ID="ddlDeptId" runat="server">
                    </asp:DropDownList>
                </td>
                <td>
                    指标
                </td>
                <td>
                    <asp:TextBox ID="txtAmount" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="Button1" runat="server" Text="查询" OnClick="Button1_Click" />
                </td>
                <td>
                    <asp:Button ID="Button2" runat="server" Text="添加" OnClick="Button2_Click" />
                </td>
                <td>
                    <asp:Button ID="Button3" runat="server" Text="返回" onclick="Button3_Click" />
                </td>
            </tr>
        </table>
        <asp:GridView ID="GridView1" runat="server" BackColor="White" BorderColor="#CC9966"
            BorderStyle="None" BorderWidth="1px" CellPadding="4" AllowPaging="True" AutoGenerateColumns="False"
            AutoGenerateEditButton="True" OnRowCancelingEdit="GridView1_RowCancelingEdit"
            OnRowDeleting="GridView1_RowDeleting" OnRowEditing="GridView1_RowEditing" 
            OnRowUpdating="GridView1_RowUpdating" 
            onpageindexchanging="GridView1_PageIndexChanging">
            <Columns>
                <asp:TemplateField ShowHeader="False">
                    <ItemTemplate>
                        <asp:ImageButton runat="server" CommandName="Delete" OnClientClick="return confirm('是否删除？');"
                            ToolTip="删除" ImageUrl="~/image/1394364088_DeleteRed.png"></asp:ImageButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Month" HeaderText="月份" ReadOnly="True" SortExpression="Month" />
                <asp:BoundField DataField="vcDeptId" HeaderText="门店编码" ReadOnly="True" SortExpression="vcDeptId" />
                <asp:BoundField DataField="vcDeptIdComments" HeaderText="门店名称" ReadOnly="True" SortExpression="vcDeptIdComments" />
                <asp:BoundField DataField="Amount" HeaderText="指标" SortExpression="Amount" />
            </Columns>
            <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
            <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="#FFFFCC" />
            <PagerSettings Mode="NumericFirstLast" />
            <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
            <RowStyle BackColor="White" ForeColor="#330099" />
            <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
            <SortedAscendingCellStyle BackColor="#FEFCEB" />
            <SortedAscendingHeaderStyle BackColor="#AF0101" />
            <SortedDescendingCellStyle BackColor="#F6F0C0" />
            <SortedDescendingHeaderStyle BackColor="#7E0000" />
        </asp:GridView>
    </div>
    </form>
</body>
</html>
