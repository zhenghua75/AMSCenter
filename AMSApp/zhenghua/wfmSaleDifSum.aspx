<%@ Register TagPrefix="uc2" TagName="ucPageView" Src="../ucPageView.ascx" %>

<%@ Page Language="c#" CodeBehind="wfmSaleDifSum.aspx.cs" AutoEventWireup="True"
    Inherits="AMSApp.zhenghua.Produce.wfmSaleDifSum" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>wfmCostReport</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <script src="scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <link href="DataGrid.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../js/isInt.js"></script>
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
    <table id="Table1" cellspacing="1" cellpadding="5" width="95%" border="0">
        <tr>
            <td style="color: #330033; font-size: 15pt; font-weight: bold" align="center">
                <asp:Label ID="Label3" runat="server">���۲�����ܱ�</asp:Label>
            </td>
        </tr>
    </table>
    <table id="Table2" cellspacing="1" cellpadding="1" width="95%" border="1">
        <tr valign="top">
            <td align="center">
                <table cellspacing="1" cellpadding="1" border="0">
                    <tr>
                        <td>
                            <asp:Label ID="Label4" runat="server">���ţ�</asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlDept" runat="server">
                            </asp:DropDownList>
                        </td>
                        <td align="center">
                            <asp:Label ID="lblOperName" runat="server">����Ա��</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtOperName" runat="server"></asp:TextBox>
                        </td>
                        <td align="center">
                            <asp:Label ID="Label2" runat="server">��ʼ���ڣ�</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtBeginDate" onfocus="WdatePicker()" runat="server"></asp:TextBox>
                        </td>
                        <td align="center">
                            <asp:Label ID="Label1" runat="server">�������ڣ�</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtEndDate" onfocus="WdatePicker()" runat="server"></asp:TextBox>
                        </td>
                        <td>
		<FONT face="����">
			                <asp:Label ID="Label9" runat="server" Font-Size="10pt" Text="Ƭ��"></asp:Label>
		</FONT>
	                    </td>
                        <td>
		<FONT face="����">
                                        <asp:DropDownList ID="ddlRegion" runat="server" AutoPostBack="True" 
                                            onselectedindexchanged="ddlRegion_SelectedIndexChanged">
                                        </asp:DropDownList>
		</FONT>
	                    </td>
                        <td>
                        <asp:Button ID="Button2" runat="server" Text="��ѯ" OnClick="Button2_Click"></asp:Button>
                            <asp:Button ID="Button1" runat="server" Text="����" OnClick="Button1_Click"></asp:Button><asp:Button
                                ID="Button3" runat="server" Text="�༭" OnClick="Button3_Click"></asp:Button><asp:Button
                                    ID="Button4" runat="server" Text="ˢ��" OnClick="Button4_Click"></asp:Button>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr valign="top">
            <td align="center">
                <font face="����"></font>
            </td>
        </tr>
    </table>
    <table id="Table5" cellspacing="1" cellpadding="1" width="1400" border="0">
        <tr>
            <td align="center">
                <asp:DataGrid ID="DataGrid1" runat="server" Width="100%" AutoGenerateColumns="False"
                    AllowPaging="True" Font-Names="Verdana" AlternatingItemStyle-BackColor="#660033"
                    HeaderStyle-BackColor="SteelBlue" Font-Size="X-Small" Font-Name="Verdana" CellPadding="3"
                    BorderWidth="1px" BorderColor="Black" PagerStyle-HorizontalAlign="Right" PagerStyle-Mode="NumericPages"
                    PageSize="30" PagerStyle-Visible="False">
                    <FooterStyle Wrap="False"></FooterStyle>
                    <SelectedItemStyle Wrap="False"></SelectedItemStyle>
                    <EditItemStyle Wrap="False"></EditItemStyle>
                    <AlternatingItemStyle Wrap="False" ForeColor="Black" BackColor="#E6E6E6"></AlternatingItemStyle>
                    <ItemStyle Wrap="False" ForeColor="Black" BackColor="White"></ItemStyle>
                    <HeaderStyle Font-Size="Small" Font-Bold="True" Wrap="False" ForeColor="White" BackColor="#880028">
                    </HeaderStyle>
                    <Columns>
                        <asp:BoundColumn Visible="False" DataField="cnvcDeptID" ReadOnly="True" HeaderText="����ID">
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="cnvcDeptName" ReadOnly="True" HeaderText="����"></asp:BoundColumn>
                        <asp:BoundColumn DataField="cnvcOperName" ReadOnly="True" HeaderText="����Ա"></asp:BoundColumn>
                        <asp:TemplateColumn HeaderText="�ֽ�">
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.cnnFact_Cash","{0:f}") %>'>
                                </asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox runat="server" Width="60px" Text='<%# DataBinder.Eval(Container, "DataItem.cnnFact_Cash","{0:f}") %>'>
                                </asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="POS����">
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.cnnFact_Pos","{0:f}") %>'>
                                </asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox runat="server" Width="60px" Text='<%# DataBinder.Eval(Container, "DataItem.cnnFact_Pos","{0:f}") %>'>
                                </asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="����ȯ">
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.cnnFact_Replace","{0:f}") %>'>
                                </asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox runat="server" Width="60px" Text='<%# DataBinder.Eval(Container, "DataItem.cnnFact_Replace","{0:f}") %>'>
                                </asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="֧Ʊ">
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.cnnFact_Check","{0:f}") %>'>
                                </asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox runat="server" Width="60px" Text='<%# DataBinder.Eval(Container, "DataItem.cnnFact_Check","{0:f}") %>'>
                                </asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateColumn>
                        <asp:BoundColumn DataField="cnnFact_Sum" ReadOnly="True" HeaderText="ʵ�ɺϼ�" DataFormatString="{0:f}">
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="cnnPayable_Sum" ReadOnly="True" HeaderText="Ӧ�ɽ��" DataFormatString="{0:f}">
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="cnnDif_Sum" ReadOnly="True" HeaderText="����ϼ�" DataFormatString="{0:f}">
                        </asp:BoundColumn>
                        <asp:TemplateColumn HeaderText="���">
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.cnnDif_More","{0:f}") %>'>
                                </asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox runat="server" Width="60px" Text='<%# DataBinder.Eval(Container, "DataItem.cnnDif_More","{0:f}") %>'>
                                </asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="���">
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.cnnDif_Add","{0:f}") %>'>
                                </asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox runat="server" Width="60px" Text='<%# DataBinder.Eval(Container, "DataItem.cnnDif_Add","{0:f}") %>'>
                                </asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateColumn>
                        <asp:BoundColumn DataField="cnnDif_Dif" ReadOnly="True" HeaderText="��/�̿�" DataFormatString="{0:f}">
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="cnnPayable_Sale" ReadOnly="True" HeaderText="ϵͳ���۽��"
                            DataFormatString="{0:f}"></asp:BoundColumn>
                        <asp:BoundColumn DataField="cnnPayable_Retail" ReadOnly="True" HeaderText="��������"
                            DataFormatString="{0:f}"></asp:BoundColumn>
                        <asp:BoundColumn DataField="cnnPayable_Member" ReadOnly="True" HeaderText="��Ա����"
                            DataFormatString="{0:f}"></asp:BoundColumn>
                        <asp:BoundColumn DataField="cnnPayable_Card" ReadOnly="True" HeaderText="��Ա����ֵ" DataFormatString="{0:f}">
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="cnnPayable_Sale_Tmp" ReadOnly="True" HeaderText="ʵ�����۽��"
                            DataFormatString="{0:f}"></asp:BoundColumn>
                        <asp:BoundColumn Visible="False" DataField="cndBusinessDate" ReadOnly="True" HeaderText="����"
                            DataFormatString="{0:yyyy-MM-dd}"></asp:BoundColumn>
                        <asp:BoundColumn Visible="False" DataField="cnvcType" ReadOnly="True" HeaderText="����">
                        </asp:BoundColumn>
                        <asp:EditCommandColumn ButtonType="LinkButton" UpdateText="����" CancelText="ȡ��" EditText="�༭">
                        </asp:EditCommandColumn>
                    </Columns>
                    <PagerStyle Font-Size="X-Small" HorizontalAlign="Right" Wrap="False" Mode="NumericPages">
                    </PagerStyle>
                </asp:DataGrid>
            </td>
        </tr>
        <tr id="FootBar" runat="server" name="FootBar">
            <td align="center">
                <asp:Label ID="lbPageLabel" runat="server" Font-Size="10pt"></asp:Label>
                <asp:LinkButton ID="btnFirst" OnClick="PagerButtonClick" runat="server" Font-Name="verdana"
                    Font-Size="8pt" ForeColor="navy" CommandArgument="0" Text="��ҳ"></asp:LinkButton>|
                <asp:LinkButton ID="btnPrev" OnClick="PagerButtonClick" runat="server" Font-Name="verdana"
                    Font-Size="8pt" ForeColor="navy" CommandArgument="prev" Text="��ҳ"></asp:LinkButton>|
                <asp:LinkButton ID="btnNext" OnClick="PagerButtonClick" runat="server" Font-Name="verdana"
                    Font-Size="8pt" ForeColor="navy" CommandArgument="next" Text="��ҳ"></asp:LinkButton>|
                <asp:LinkButton ID="btnLast" OnClick="PagerButtonClick" runat="server" Font-Name="verdana"
                    Font-Size="8pt" ForeColor="navy" CommandArgument="last" Text="βҳ"></asp:LinkButton>|
                <font size="2">
				������</font>
                <input id="page_number" type="text" size="3" value="<%=DataGrid1.CurrentPageIndex+1%>"
                    name="page_number" /><font size="2">ҳ</font>
                <asp:LinkButton ID="btnGo" onmouseover="javascript:if((!isInt(page_number.value))||(page_number.value<=0)){alert('��תҳ�����Ϊ��������');page_number.focus();return false;};"
                    OnClick="PagerButtonClick" runat="server" Font-Name="verdana" Font-Size="8pt"
                    ForeColor="navy" CommandArgument="jump" Text="GO">GO!</asp:LinkButton></FONT>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
