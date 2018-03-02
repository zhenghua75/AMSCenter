<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="wfmUndoCheck.aspx.cs" Inherits="AMSApp.BusiQuery.wfmUndoCheck" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>撤销审核</title>
    <script type="text/javascript" src="../js/calendar.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table id="Table1" cellspacing="1" cellpadding="5" width="95%" border="0">
            <tr>
                <td style="color: #330033; font-size: 15pt; font-weight: bold" align="center">
                    撤销审核
                </td>
            </tr>
        </table>
        <table id="Table2" cellspacing="1" cellpadding="1" width="95%" border="1">
            <tr>
                <td>
                    <table id="Table3" cellspacing="1" cellpadding="1" width="100%" border="0">
                        <tr>
                            <td style="width: 82px; height: 27px" align="right">
                                <asp:Label ID="Label9" runat="server" Font-Size="10pt">消费流水</asp:Label>
                            </td>
                            <td style="width: 143px; height: 27px">
                                <asp:TextBox ID="txtSerial" runat="server" Font-Size="10pt" Width="112px"></asp:TextBox>
                            </td>
                            <td style="width: 82px" align="right">
                                <asp:Label ID="Label5" runat="server" Font-Size="10pt">开始时间</asp:Label>
                            </td>
                            <td style="width: 143px">
                                <asp:TextBox ID="txtBegin" runat="server" onfocus="HS_setDate(this)" size="13" Style="width: 112px;
                                    height: 22px"></asp:TextBox>
                            </td>
                            <td style="width: 82px" align="right">
                                <asp:Label ID="Label4" runat="server" Font-Size="10pt">结束时间</asp:Label>
                            </td>
                            <td style="width: 143px">
                                <asp:TextBox ID="txtEnd" runat="server" onfocus="HS_setDate(this)" size="13" Style="width: 112px;
                                    height: 22px"></asp:TextBox>
                            </td>
                            <td style="width: 81px" align="right">
                                <asp:Label ID="Label7" runat="server" Font-Size="10pt">操作员</asp:Label>
                            </td>
                            <td style="width: 142px">
                                <asp:DropDownList ID="ddlOper" runat="server" Font-Size="10pt" Width="112px">
                                </asp:DropDownList>
                            </td>
                            <td style="width: 81px" align="right">
                                <asp:Label ID="Label6" runat="server" Font-Size="10pt">门店</asp:Label>
                            </td>
                            <td style="width: 142px">
                                <asp:DropDownList ID="ddlDept" runat="server" AutoPostBack="True" Font-Size="10pt"
                                    Width="112px">
                                </asp:DropDownList>
                            </td>
                            <td style="width: 81px" align="right">
                                <asp:Label ID="Label1" runat="server" Text="有效状态" Font-Size="10pt"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="DropDownList1" runat="server">
                                    <asp:ListItem Text="全部" Value=""></asp:ListItem>
                                    <asp:ListItem Text="正常消费" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="已撤消" Value="9"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td style="width: 81px" align="right">
		<FONT face="宋体">
			                    <asp:Label ID="Label10" runat="server" Font-Size="10pt" Text="片区"></asp:Label>
		</FONT>
	                        </td>
                            <td style="width: 142px">
		<FONT face="宋体">
                                        <asp:DropDownList ID="ddlRegion" runat="server" AutoPostBack="True" 
                                            onselectedindexchanged="ddlRegion_SelectedIndexChanged">
                                        </asp:DropDownList>
		</FONT>
	                        </td>
                            <td align="center">
                                <asp:Button ID="btQuery" runat="server" Font-Size="10pt" Width="56px" Text="查询" OnClick="btQuery_Click">
                                </asp:Button>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <table id="Table4" cellspacing="1" cellpadding="1" width="95%" border="0" runat="server">
            <tr>
                <td align="center">
                    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" CellPadding="4" ForeColor="#333333"
                        AutoGenerateColumns="False" OnRowEditing="GridView1_RowEditing" OnPageIndexChanging="GridView1_PageIndexChanging"
                        OnRowCancelingEdit="GridView1_RowCancelingEdit" 
                        OnRowDeleting="GridView1_RowDeleting" onrowdatabound="GridView1_RowDataBound" 
                        PageSize="100" ShowFooter="True">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:BoundField DataField="iSerial" HeaderText="流水" ReadOnly="True" />
                            <asp:BoundField DataField="vcGoodsId" HeaderText="商品编码" ReadOnly="True" />
                            <asp:BoundField DataField="vcGoodsName" HeaderText="商品名称" ReadOnly="True" />
                            <asp:BoundField DataField="nPrice" HeaderText="单价" ReadOnly="True" />
                            <asp:BoundField DataField="iCount" HeaderText="数量" ReadOnly="True" />
                            <asp:BoundField DataField="nTRate" HeaderText="折扣" ReadOnly="True" />
                            <asp:BoundField DataField="nFee" HeaderText="合计" ReadOnly="True" />
                            <asp:BoundField DataField="vcComments" HeaderText="备注" ReadOnly="True" />
                            <asp:BoundField DataField="cFlag" HeaderText="有效状态" ReadOnly="True" />
                            <asp:BoundField DataField="dtConsDate" HeaderText="消费日期" ReadOnly="True" />
                            <asp:BoundField DataField="vcOperName" HeaderText="操作员" ReadOnly="True" />
                            <asp:BoundField DataField="vcDeptId" HeaderText="门店编码" ReadOnly="True" />
                            <asp:BoundField DataField="vcDeptName" HeaderText="门店名称" ReadOnly="True" />
                            <asp:BoundField DataField="IsUndo" HeaderText="是否撤销" ReadOnly="True" />
                            <asp:TemplateField ShowHeader="False">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Delete"
                                        Text="审核" OnClientClick="return confirm('是否审核?');"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                        <PagerSettings Mode="NumericFirstLast" FirstPageText="首页" LastPageText="尾页" NextPageText="下页"
                            PreviousPageText="上页" />
                        <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                        <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                        <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                        <SortedAscendingCellStyle BackColor="#FDF5AC" />
                        <SortedAscendingHeaderStyle BackColor="#4D0000" />
                        <SortedDescendingCellStyle BackColor="#FCF6C0" />
                        <SortedDescendingHeaderStyle BackColor="#820000" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
