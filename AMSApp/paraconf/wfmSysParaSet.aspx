<%@ Page language="c#" Codebehind="wfmSysParaSet.aspx.cs" AutoEventWireup="True" Inherits="AMSApp.paraconf.wfmSysParaSet" %>
<%@ Register TagPrefix="uc1" TagName="ucPageView" Src="../ucPageView.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>wfmSysParaSet</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	</HEAD>
	<body bgColor="#feeff8" MS_POSITIONING="GridLayout">
		<FORM id="Form2" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="1" cellPadding="5" width="95%" border="0">
				<TR>
					<TD style="FONT-WEIGHT: bold; FONT-SIZE: 15pt; COLOR: #330033" align="center">系统参数管理</TD>
				</TR>
			</TABLE>
			<TABLE id="Table2" height="500" cellSpacing="1" cellPadding="1" width="95%" border="0">
				<TR>
					<TD width="50%">
						<TABLE id="Table3" cellSpacing="1" cellPadding="1" width="100%" border="1" height="100%">
							<TR>
								<TD colspan="3" style="FONT-SIZE: 10pt; COLOR: #3366cc">新商品推荐设置</TD>
							</TR>
							<TR>
								<TD style="FONT-SIZE: 10pt; WIDTH: 184px">现有商品</TD>
								<TD style="WIDTH: 49px"></TD>
								<TD style="FONT-SIZE: 10pt">推荐新品</TD>
							</TR>
							<TR>
								<TD style="WIDTH: 184px"><FONT face="宋体">
										<asp:ListBox id="lbtcurrent" runat="server" Height="444px" Width="176px" Font-Size="10pt"></asp:ListBox></FONT></TD>
								<TD style="WIDTH: 49px">
									<TABLE id="Table5" cellSpacing="1" cellPadding="1" width="83" border="0" style="WIDTH: 83px; HEIGHT: 225px"
										align="center">
										<TR>
											<TD align="center">
												<asp:Button id="btAdd" runat="server" Text="添加>>" onclick="btAdd_Click"></asp:Button></TD>
										</TR>
										<TR>
											<TD align="center">
												<asp:Button id="btDel" runat="server" Text="<<移除" onclick="btDel_Click"></asp:Button></TD>
										</TR>
										<TR>
											<TD align="center">
												<asp:Button id="btNewGoods" runat="server" Text="保存" Width="61px" onclick="btNewGoods_Click"></asp:Button></TD>
										</TR>
									</TABLE>
								</TD>
								<TD>
									<asp:ListBox id="lbtNew" runat="server" Height="446px" Width="176px" Font-Size="10pt"></asp:ListBox></TD>
							</TR>
						</TABLE>
					</TD>
					<TD width="100%">
						<TABLE id="Table4" cellSpacing="1" cellPadding="1" width="100%" height="100%" border="0">
							<TR>
								<TD style="HEIGHT: 82px">
									<TABLE id="Table6" cellSpacing="1" cellPadding="1" width="100%" border="1">
										<TR>
											<TD colspan="5" style="FONT-SIZE: 10pt; COLOR: #3366cc">消费积分设置：</TD>
										</TR>
										<TR>
											<TD style="FONT-SIZE: 10pt; WIDTH: 91px" align="right">消费</TD>
											<TD style="WIDTH: 85px" align="center">
												<asp:TextBox id="txtFee" runat="server" Width="94px" Font-Size="10pt"></asp:TextBox></TD>
											<TD style="FONT-SIZE: 10pt; WIDTH: 69px" align="center">元，赠送</TD>
											<TD style="WIDTH: 67px" align="center">
												<asp:TextBox id="txtIg" runat="server" Width="94px" Font-Size="10pt"></asp:TextBox></TD>
											<TD style="FONT-SIZE: 10pt">积分</TD>
										</TR>
										<TR>
											<TD style="FONT-SIZE: 10pt; WIDTH: 344px; COLOR: #cc0000" colspan="4">注意：该参数如果不设置，将视为消费无积分</TD>
											<TD>
												<asp:Button id="btIg" runat="server" Width="48px" Text="设置" onclick="btIg_Click"></asp:Button></TD>
										</TR>
									</TABLE>
								</TD>
							</TR>
							<TR>
								<TD style="HEIGHT: 117px">
									<TABLE id="Table7" cellSpacing="1" cellPadding="1" width="100%" border="1">
										<TR>
											<TD colspan="3" style="FONT-SIZE: 10pt; COLOR: #3366cc">充值赠款金额设置：</TD>
										</TR>
										<TR>
											<TD style="FONT-SIZE: 10pt; WIDTH: 194px" align="right">100-500赠款比例</TD>
											<TD style="WIDTH: 96px">
												<asp:TextBox id="txtPromRate1" runat="server" Width="94px" Font-Size="10pt"></asp:TextBox></TD>
											<TD style="FONT-SIZE: 10pt">%</TD>
										</TR>
										<TR>
											<TD style="FONT-SIZE: 10pt; WIDTH: 194px" align="right">500-1000赠款比例</TD>
											<TD style="WIDTH: 96px">
												<asp:TextBox id="txtPromRate2" runat="server" Width="94px" Font-Size="10pt"></asp:TextBox></TD>
											<TD style="FONT-SIZE: 10pt">%</TD>
										</TR>
										<TR>
											<TD style="FONT-SIZE: 10pt; WIDTH: 194px" align="right">1000以上赠款比例</TD>
											<TD style="WIDTH: 96px">
												<asp:TextBox id="txtPromRate3" runat="server" Width="94px" Font-Size="10pt"></asp:TextBox></TD>
											<TD style="FONT-SIZE: 10pt">%</TD>
										</TR>
										<TR>
											<TD colspan="2" style="FONT-SIZE: 10pt; WIDTH: 295px; COLOR: #cc0000">注意：该参数如果不设置，将视为充值无赠款</TD>
											<TD>
												<asp:Button id="btProm" runat="server" Width="48px" Text="设置" onclick="btProm_Click"></asp:Button></TD>
										</TR>
									</TABLE>
								</TD>
							</TR>
                            <tr>
                                <td>
                                    <TABLE id="Table8" cellSpacing="1" cellPadding="1" width="100%" border="1">
										<TR>
											<TD style="FONT-SIZE: 10pt; COLOR: #3366cc">充值赠款金额设置：</TD>
										</TR>
										<TR>
                                        <td>
                                        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" CellPadding="4" 
                        ForeColor="#333333" AutoGenerateColumns="False" 
                        onrowediting="GridView1_RowEditing" 
                        onpageindexchanging="GridView1_PageIndexChanging" 
                        onrowcancelingedit="GridView1_RowCancelingEdit" 
                        onrowdeleting="GridView1_RowDeleting" 
                        onrowupdating="GridView1_RowUpdating" ShowFooter="True" 
                        onrowcommand="GridView1_RowCommand" PageSize="100">
                        <AlternatingRowStyle BackColor="White" />
                        <EmptyDataTemplate>
                        <table>
                        <tr>
                            <td>起始金额</td><td><asp:TextBox ID="txtBeginValue" runat="Server"></asp:TextBox></td>
                            <td>结束金额</td><td><asp:TextBox ID="txtEndValue" runat="Server"></asp:TextBox></td>
                            <td>赠款比例</td><td><asp:TextBox ID="txtPromRatio" runat="Server"></asp:TextBox></td>
                            <td><asp:LinkButton ID="LinkButton13" runat="server" CausesValidation="False" 
                                        CommandName="EmptyInsert" Text="添加"></asp:LinkButton></td>
                        </tr>
                        </table>
</EmptyDataTemplate>
                        <Columns>
                            <asp:TemplateField HeaderText="起始金额">
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("vcBeginValue") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("vcBeginValue") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="TextBox12" runat="Server"></asp:TextBox>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="结束金额">
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("vcEndValue") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("vcEndValue") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="TextBox22" runat="Server"></asp:TextBox>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="赠款比例%">
                                <ItemTemplate>
                                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("vcPromRatio") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("vcPromRatio") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="TextBox32" runat="Server"></asp:TextBox>
                                </FooterTemplate>
                            </asp:TemplateField>     
                            <asp:TemplateField Visible="False">
                                <ItemTemplate>
                                    <asp:Label ID="Label7" runat="server" Text='<%# Bind("vcCommName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField Visible="False">
                                <ItemTemplate>
                                    <asp:Label ID="Label8" runat="server" Text='<%# Bind("vcCommCode") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>                      
                            <asp:CommandField ShowEditButton="True" UpdateText="确定" />
                            <asp:TemplateField ShowHeader="False">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" 
                                        CommandName="Delete" Text="删除" OnClientClick="return confirm('删除?');"></asp:LinkButton>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:LinkButton ID="LinkButton12" runat="server" CausesValidation="False" 
                                        CommandName="Insert" Text="添加"></asp:LinkButton>
                                </FooterTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                        <PagerSettings Mode="NumericFirstLast" FirstPageText="首页" LastPageText="尾页" 
                            NextPageText="下页" PreviousPageText="上页" />
                        <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                        <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                        <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                        <SortedAscendingCellStyle BackColor="#FDF5AC" />
                        <SortedAscendingHeaderStyle BackColor="#4D0000" />
                        <SortedDescendingCellStyle BackColor="#FCF6C0" />
                        <SortedDescendingHeaderStyle BackColor="#820000" />                       
                    </asp:GridView>
                                        </td>
                                        </TR>
                                        </TABLE>
                                </td>
                            </tr>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
		</FORM>
	</body>
</HTML>
