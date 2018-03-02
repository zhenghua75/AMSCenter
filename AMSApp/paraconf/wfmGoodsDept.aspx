<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="wfmGoodsDept.aspx.cs" Inherits="AMSApp.paraconf.wfmGoodsDept" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">

        function SelectheaderCheckboxes(headerchk) {

            var gvcheck = document.getElementById('GridView1');

            var i;

            //Condition to check header checkbox selected or not if that is true checked all checkboxes

            if (headerchk.checked) {

                for (i = 0; i < gvcheck.rows.length; i++) {

                    var inputs = gvcheck.rows[i].getElementsByTagName('input');

                    inputs[0].checked = true;

                }

            }

            //if condition fails uncheck all checkboxes in gridview

            else {

                for (i = 0; i < gvcheck.rows.length; i++) {

                    var inputs = gvcheck.rows[i].getElementsByTagName('input');

                    inputs[0].checked = false;

                }

            }

            Selectchildcheckboxes(headerchk);

        }

        //function to check header checkbox based on child checkboxes condition

        function Selectchildcheckboxes(header) {

            var ck = header;

            var count = 0;

            var gvcheck = document.getElementById('GridView1');

            var headerchk = document.getElementById(header);

            var rowcount = gvcheck.rows.length;

            //By using this for loop we will count how many checkboxes has checked

            for (i = 1; i < gvcheck.rows.length; i++) {

                var inputs = gvcheck.rows[i].getElementsByTagName('input');

                if (inputs[0].checked) {

                    count++;

                }

            }

            //Condition to check all the checkboxes selected or not

            if (count == rowcount - 1) {

                headerchk.checked = true;

            }

            else {

                headerchk.checked = false;

            }

        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table id="Table1" cellspacing="1" cellpadding="5" width="95%" border="0">
            <tr>
                <td style="font-weight: bold; font-size: 15pt; color: #330033" align="center">
                    商品门店管理
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Label ID="Label4" runat="server" Font-Size="10pt">商品编号：</asp:Label>
                    <asp:Label ID="lblGoodsId" runat="server" Font-Size="10pt"></asp:Label>
                    <asp:Label ID="Label3" runat="server" Font-Size="10pt">，商品名称：</asp:Label>
                    <asp:Label ID="lblGoodsName" runat="server" Font-Size="10pt"></asp:Label>
                    <asp:Label ID="Label5" runat="server" Font-Size="10pt">，商品单价：</asp:Label>
                    <asp:Label ID="lblPrice" runat="server" Font-Size="10pt"></asp:Label>
                    <asp:Button ID="btnEdit" runat="server" Text="确定" OnClick="btnEdit_Click"></asp:Button>
                    <asp:Button ID="Button1" runat="server" Text="返回商品管理" OnClick="Button1_Click"></asp:Button>
                </td>
            </tr>
        </table>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4"
            ForeColor="#333333" GridLines="None" HorizontalAlign="Center" 
            onrowdatabound="GridView1_RowDataBound">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:TemplateField HeaderText="选择">
                    <HeaderTemplate>
                        <asp:CheckBox runat="server" ID="chkAll" onclick="SelectheaderCheckboxes(this)" />
                    </HeaderTemplate>
                    <EditItemTemplate>
                        <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%# Bind("IsSelect") %>' />
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%# Bind("IsSelect") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="vcDeptId" HeaderText="门店编号" />
                <asp:BoundField DataField="vcDeptName" HeaderText="门店名称" />
                <asp:BoundField DataField="vcGoodsId" HeaderText="商品编号" />
                <asp:BoundField DataField="vcGoodsName" HeaderText="商品名称" />
                <asp:BoundField DataField="nPrice" HeaderText="商品单价" />
            </Columns>
            <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
            <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
            <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
            <SortedAscendingCellStyle BackColor="#FDF5AC" />
            <SortedAscendingHeaderStyle BackColor="#4D0000" />
            <SortedDescendingCellStyle BackColor="#FCF6C0" />
            <SortedDescendingHeaderStyle BackColor="#820000" />
        </asp:GridView>
    </div>
    </form>
</body>
</html>
