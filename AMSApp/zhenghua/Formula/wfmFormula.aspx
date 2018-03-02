<%@ Register TagPrefix="cc1" Namespace="AMSApp.zhenghua.ImageDisplay" Assembly="AMSApp" %>
<%@ Page language="c#" Codebehind="wfmFormula.aspx.cs" AutoEventWireup="True" Inherits="AMSApp.zhenghua.Formula.wfmFormula" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>wfmFormula</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK rel="stylesheet" type="text/css" href="../DataGrid.css">
	</HEAD>
	<body id="thebody" bgColor="#feeff8" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table border="1" cellSpacing="0" borderColor="black" cellPadding="0" align="center">
				<tr>
					<td>
						<table width="100%">
							<tr>
								<td colSpan="4" align="center"><asp:label id="lblFormula" runat="server" CssClass="title">配方添加</asp:label></td>
							</tr>
							<tr>
								<td colSpan="4" align="center"><asp:label id="Label4" runat="server" CssClass="lable">产品资料卡</asp:label></td>
							</tr>
							<tr>
								<td><asp:label id="Label11" runat="server" CssClass="lable">产品类型：</asp:label></td>
								<td><asp:dropdownlist id="ddlProductType" runat="server" AutoPostBack="True" onselectedindexchanged="ddlProductType_SelectedIndexChanged"></asp:dropdownlist></td>
								<td><asp:label id="Label1" runat="server" CssClass="lable">产品类别：</asp:label></td>
								<td><asp:dropdownlist id="ddlProductClass" runat="server" AutoPostBack="True" onselectedindexchanged="ddlProductClass_SelectedIndexChanged"></asp:dropdownlist></td>
							</tr>
							<tr>
								<td><asp:label id="Label3" runat="server" CssClass="lable">产品编码：</asp:label></td>
								<td><asp:textbox id="txtProductCode" runat="server" CssClass="textbox"></asp:textbox></td>
								<td><asp:label id="Label2" runat="server" CssClass="lable">产品名称：</asp:label></td>
								<td><asp:textbox id="txtProductName" runat="server" CssClass="textbox"></asp:textbox></td>
							</tr>
							<tr>
								<td><asp:label id="Label12" runat="server" CssClass="lable">产品单位：</asp:label></td>
								<td><asp:dropdownlist id="ddlUnit" runat="server"></asp:dropdownlist></td>
								<td></td>
								<td><asp:checkbox id="chkIsUse" runat="server" Text="是否下架"></asp:checkbox></td>
							</tr>
							<tr>
								<td><asp:label id="Label6" runat="server" CssClass="lable">成本合计：</asp:label></td>
								<td><asp:label id="lblCostSum" runat="server" CssClass="lable"></asp:label></td>
								<td><asp:label id="Label5" runat="server" CssClass="lable">份产：</asp:label></td>
								<td><asp:textbox id="txtPortionCount" runat="server" CssClass="textbox"></asp:textbox></td>
							</tr>
							<tr>
								<td><asp:label id="Label7" runat="server" CssClass="lable">口感：</asp:label></td>
								<td><asp:textbox id="txtFeel" runat="server" CssClass="textbox"></asp:textbox></td>
								<td><asp:label id="Label8" runat="server" CssClass="lable">颜色：</asp:label></td>
								<td><asp:textbox id="txtColor" runat="server" CssClass="textbox"></asp:textbox></td>
							</tr>
							<tr>
								<td><asp:label id="Label9" runat="server" CssClass="lable">组织：</asp:label></td>
								<td><asp:textbox id="txtOrganise" runat="server" CssClass="textbox"></asp:textbox></td>
								<td><asp:label id="Label10" runat="server" CssClass="lable">口味：</asp:label></td>
								<td><asp:textbox id="txtTaste" runat="server" CssClass="textbox"></asp:textbox></td>
							</tr>
						</table>
					</td>
					<td>
						<table>
							<tr>
								<td colSpan="2" align="center"><asp:label id="Label18" runat="server" CssClass="lable">产品图片</asp:label></td>
							</tr>
							<tr>
								<td colSpan="2"><cc1:dynamicimage id="DynamicImage1" class="image" runat="server" alt="产品图片"></cc1:dynamicimage></td>
							</tr>
							<tr>
								<td><INPUT id="fileImage" class="file" type="file" name="fileImage" runat="server"></td>
								<td><asp:button id="btnFileImage" runat="server" CssClass="button" Text="上传" onclick="btnFileImage_Click"></asp:button></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td vAlign="top" colSpan="2">
						<table width="100%">
							<tr>
								<td vAlign="top" width="50%">
									<table width="100%" height="100%">
										<tr vAlign="top">
											<td><asp:datagrid id="dgDosage" runat="server" CssClass="datagrid" Caption="配料表" AllowPaging="True"
													AutoGenerateColumns="False" ShowFooter="True" PageSize="20" BorderWidth="1px" BorderColor="Black">
													<FooterStyle BorderWidth="1px" CssClass="dg_footer"></FooterStyle>
													<SelectedItemStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_selected"></SelectedItemStyle>
													<AlternatingItemStyle CssClass="dg_alter"></AlternatingItemStyle>
													<ItemStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_item"></ItemStyle>
													<HeaderStyle BorderWidth="1px" BorderStyle="Solid" BorderColor="Black" CssClass="dg_header"></HeaderStyle>
													<Columns>
														<asp:BoundColumn Visible="False" DataField="cnvcCode" ReadOnly="True" HeaderText="原料编码"></asp:BoundColumn>
														<asp:BoundColumn DataField="cnvcName" ReadOnly="True" HeaderText="原料名称"></asp:BoundColumn>
														<asp:BoundColumn DataField="cnvcUnit" ReadOnly="True" HeaderText="单位"></asp:BoundColumn>
														<asp:BoundColumn DataField="cnnCount" HeaderText="用量"></asp:BoundColumn>
														<asp:BoundColumn DataField="cnnPrice" ReadOnly="True" HeaderText="材料价格"></asp:BoundColumn>
														<asp:BoundColumn DataField="cnnSum" ReadOnly="True" HeaderText="成本"></asp:BoundColumn>
														<asp:EditCommandColumn ButtonType="LinkButton" UpdateText="更新" HeaderText="编辑" CancelText="取消" EditText="编辑"></asp:EditCommandColumn>
														<asp:TemplateColumn HeaderText="操作">
															<ItemTemplate>
																<asp:LinkButton id="LinkButton1" runat="server" CommandName="Delete" Text="删除" CausesValidation="false"></asp:LinkButton>
															</ItemTemplate>
															<FooterTemplate>
																<asp:Button id="btnAddRaw" runat="server" CommandName="Add" Text="添加"></asp:Button>
															</FooterTemplate>
														</asp:TemplateColumn>
													</Columns>
													<PagerStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_page" Mode="NumericPages"></PagerStyle>
												</asp:datagrid></td>
										</tr>
										<tr height="20">
											<td>
												<table>
													<tr>
														<td style="WIDTH: 120px"><asp:label id="Label20" runat="server" CssClass="lable">原料成本合计：</asp:label></td>
														<td><asp:label id="lblMaterialCostSum" runat="server"></asp:label></td>
													</tr>
												</table>
											</td>
										</tr>
									</table>
								</td>
								<td vAlign="top" width="50%">
									<table border="0" cellSpacing="0" borderColor="black" cellPadding="0" width="500" height="100%">
										<tr>
											<td><asp:datagrid id="dgPacking" runat="server" CssClass="datagrid" Caption="产品包装材料部分" AllowPaging="True"
													AutoGenerateColumns="False" ShowFooter="True" PageSize="5" BorderWidth="1px" BorderColor="Black">
													<FooterStyle BorderWidth="1px" CssClass="dg_footer"></FooterStyle>
													<SelectedItemStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_selected"></SelectedItemStyle>
													<AlternatingItemStyle CssClass="dg_alter"></AlternatingItemStyle>
													<ItemStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_item"></ItemStyle>
													<HeaderStyle BorderWidth="1px" BorderStyle="Solid" BorderColor="Black" CssClass="dg_header"></HeaderStyle>
													<Columns>
														<asp:TemplateColumn HeaderText="材料名称">
															<ItemTemplate>
																<asp:Label id=Label13 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.cnvcName") %>'>
																</asp:Label>
															</ItemTemplate>
															<FooterTemplate>
																<asp:DropDownList id="ddlFooterPacking" Width="150" runat="server" OnSelectedIndexChanged="ddlFooterPacking_SelectedIndexChanged"
																	AutoPostBack="True"></asp:DropDownList>
															</FooterTemplate>
														</asp:TemplateColumn>
														<asp:TemplateColumn HeaderText="单位">
															<ItemTemplate>
																<asp:Label id=Label14 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.cnvcUnit") %>'>
																</asp:Label>
															</ItemTemplate>
														</asp:TemplateColumn>
														<asp:TemplateColumn HeaderText="用量">
															<ItemTemplate>
																<asp:Label id=Label15 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.cnnCount") %>'>
																</asp:Label>
															</ItemTemplate>
															<FooterTemplate>
																<asp:TextBox id="TextBox2" runat="server" Width="33px"></asp:TextBox>
															</FooterTemplate>
															<EditItemTemplate>
																<asp:TextBox id=TextBox1 runat="server" Width="66px" Text='<%# DataBinder.Eval(Container, "DataItem.cnnCount") %>'>
																</asp:TextBox>
															</EditItemTemplate>
														</asp:TemplateColumn>
														<asp:TemplateColumn HeaderText="材料价格">
															<ItemTemplate>
																<asp:Label id=Label23 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.cnnPrice") %>'>
																</asp:Label>
															</ItemTemplate>
														</asp:TemplateColumn>
														<asp:TemplateColumn HeaderText="成本">
															<ItemTemplate>
																<asp:Label id=Label22 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.cnnSum") %>'>
																</asp:Label>
															</ItemTemplate>
															<FooterTemplate>
																<asp:TextBox id="txtQuery" runat="server" Width="100"></asp:TextBox>
																<asp:Button id="Button2" runat="server" Text="查询" CommandName="Query"></asp:Button>
															</FooterTemplate>
														</asp:TemplateColumn>
														<asp:EditCommandColumn ButtonType="LinkButton" UpdateText="更新" HeaderText="编辑" CancelText="取消" EditText="编辑"></asp:EditCommandColumn>
														<asp:TemplateColumn HeaderText="操作">
															<ItemTemplate>
																<asp:LinkButton id="LinkButton2" runat="server" CommandName="Delete" Text="删除" CausesValidation="false"></asp:LinkButton>
															</ItemTemplate>
															<FooterTemplate>
																<asp:Button id="Button1" runat="server" Text="添加" CommandName="Add"></asp:Button>
															</FooterTemplate>
														</asp:TemplateColumn>
														<asp:BoundColumn Visible="False" DataField="cnvcCode" ReadOnly="True" HeaderText="材料编码"></asp:BoundColumn>
													</Columns>
													<PagerStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_page" Mode="NumericPages"></PagerStyle>
												</asp:datagrid></td>
										</tr>
										<tr height="20">
											<td>
												<table>
													<tr>
														<td style="WIDTH: 120px"><asp:label id="Label21" runat="server" CssClass="lable">包材成本合计：</asp:label></td>
														<td><asp:label id="lblPackingCostSum" runat="server"></asp:label></td>
													</tr>
												</table>
											</td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td vAlign="top" colSpan="2"><asp:datagrid id="dgOperStandard" runat="server" CssClass="datagrid" AllowPaging="True" AutoGenerateColumns="False"
							ShowFooter="True" BorderWidth="1px" BorderColor="Black" Height="100%">
							<FooterStyle BorderWidth="1px" CssClass="dg_footer"></FooterStyle>
							<SelectedItemStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_selected"></SelectedItemStyle>
							<AlternatingItemStyle CssClass="dg_alter"></AlternatingItemStyle>
							<ItemStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_item"></ItemStyle>
							<HeaderStyle BorderWidth="1px" BorderStyle="Solid" BorderColor="Black" CssClass="dg_header"></HeaderStyle>
							<Columns>
								<asp:TemplateColumn HeaderText="序号" FooterStyle-Width="69px" HeaderStyle-Width="69px" ItemStyle-Width="69px">
									<ItemTemplate>
										<asp:Label id=Label16 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.cnnSort") %>'>
										</asp:Label>
									</ItemTemplate>
									<FooterTemplate>
										<asp:TextBox id="TextBox4" runat="server" Width="69px"></asp:TextBox>
									</FooterTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="操作标准" FooterStyle-Width="150px" HeaderStyle-Width="150px" ItemStyle-Width="150px"
									HeaderStyle-Wrap="true" FooterStyle-Wrap="true" ItemStyle-Wrap="true">
									<ItemStyle Width="150px"></ItemStyle>
									<ItemTemplate>
										<asp:Label id=Label17 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.cnvcStandard") %>'>
										</asp:Label>
									</ItemTemplate>
									<FooterTemplate>
										<asp:TextBox id="TextBox12" runat="server" Height="60px" Width="150px" TextMode="MultiLine"></asp:TextBox>
									</FooterTemplate>
									<EditItemTemplate>
										<asp:TextBox id=TextBox11 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.cnvcStandard") %>' Height="60px" Width="150px" TextMode="MultiLine">
										</asp:TextBox>
									</EditItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="关键控制点" FooterStyle-Width="150px" HeaderStyle-Width="150px" ItemStyle-Width="150px">
									<ItemTemplate>
										<asp:Label id=Label19 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.cnvcKey") %>'>
										</asp:Label>
									</ItemTemplate>
									<FooterTemplate>
										<asp:TextBox id="TextBox14" runat="server" Height="60px" Width="150px" TextMode="MultiLine"></asp:TextBox>
									</FooterTemplate>
									<EditItemTemplate>
										<asp:TextBox id=TextBox13 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.cnvcKey") %>' Height="60px" Width="150px" TextMode="MultiLine">
										</asp:TextBox>
									</EditItemTemplate>
								</asp:TemplateColumn>
								<asp:EditCommandColumn ButtonType="LinkButton" UpdateText="更新" HeaderText="编辑" CancelText="取消" EditText="编辑">
									<ItemStyle Width="5%"></ItemStyle>
								</asp:EditCommandColumn>
								<asp:TemplateColumn HeaderText="操作">
									<ItemTemplate>
										<asp:LinkButton id="LinkButton3" runat="server" CommandName="Delete" Text="删除" CausesValidation="false"></asp:LinkButton>
									</ItemTemplate>
									<FooterTemplate>
										<asp:Button id="btnAddPacking" runat="server" Text="添加" CommandName="Add"></asp:Button>
									</FooterTemplate>
								</asp:TemplateColumn>
							</Columns>
							<PagerStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_page" Mode="NumericPages"></PagerStyle>
						</asp:datagrid></td>
				</tr>
				<tr>
					<td colSpan="2" align="center"><asp:button id="btnAdd" runat="server" CssClass="button" Text="添加" onclick="btnAdd_Click"></asp:button><asp:button id="btnModify" runat="server" CssClass="button" Text="修改" onclick="btnModify_Click"></asp:button><asp:button id="btnReturn" runat="server" CssClass="button" Text="返回" onclick="btnReturn_Click"></asp:button></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
