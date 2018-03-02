<%@ Page language="c#" Codebehind="wfmBillValidEnter.aspx.cs" AutoEventWireup="True" Inherits="AMSApp.Storage.wfmBillValidEnter" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>wfmBillValidEnter</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	</HEAD>
	<body bgColor="#feeff8" MS_POSITIONING="GridLayout">
		<FORM id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="1" cellPadding="5" width="95%" border="0">
				<TR>
					<TD style="FONT-WEIGHT: bold; FONT-SIZE: 15pt; COLOR: #330033" align="center">订单验收入库</TD>
				</TR>
			</TABLE>
			<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="95%" border="1">
				<TR>
					<TD>
						<TABLE id="Table2" cellSpacing="0" cellPadding="1" width="100%" border="0">
							<TR>
								<TD style="WIDTH: 160px" align="right"><asp:label id="Label1" runat="server" Width="57px" Font-Size="10pt">验收门店</asp:label></TD>
								<TD style="WIDTH: 124px"><asp:dropdownlist id="ddlValidDept" runat="server" Width="176px"></asp:dropdownlist></TD>
								<TD style="WIDTH: 97px" align="right"><FONT face="宋体"><asp:label id="Label2" runat="server" Width="57px" Font-Size="10pt">分货流水</asp:label></FONT></TD>
								<TD style="WIDTH: 207px"><FONT face="宋体"><asp:textbox id="txtAssignID" runat="server" Width="128px"></asp:textbox></FONT></TD>
								<TD style="WIDTH: 148px" align="right">
									<asp:label id="Label5" runat="server" Font-Size="10pt" Width="57px">订单流水</asp:label></TD>
								<TD style="WIDTH: 148px" align="left">
									<asp:textbox id="txtOrderSerialNo" runat="server" Width="128px"></asp:textbox></TD>
								<TD style="WIDTH: 76px"></TD>
								<TD style="WIDTH: 229px"><asp:button id="btnQuery" runat="server" Width="64px" Text="查询" onclick="btnQuery_Click"></asp:button></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
			<TABLE id="Table5" cellSpacing="1" cellPadding="1" width="95%" border="1">
				<TR>
					<TD>
						<TABLE id="Table6" cellSpacing="1" cellPadding="1" width="100%" border="0">
							<TR>
								<td style="WIDTH: 133px" align="right">
									<asp:label id="Label4" runat="server" Width="96px" Font-Size="10pt">当前分货流水：</asp:label></td>
								<td style="WIDTH: 110px">
									<asp:label id="lblAssignID" runat="server" Width="79px" Font-Size="10pt"></asp:label></td>
								<td style="WIDTH: 133px" align="right">
									<asp:label id="Label6" runat="server" Width="96px" Font-Size="10pt">当前订单流水：</asp:label></td>
								<td style="WIDTH: 110px">
									<asp:label id="lblOrderSerialNo" runat="server" Width="40px" Font-Size="10pt"></asp:label></td>
								<TD style="WIDTH: 325px" align="right"><asp:label id="Label10" runat="server" Font-Size="10pt">收货人：</asp:label></TD>
								<TD style="WIDTH: 71px"><asp:textbox id="txtReceiveOper" runat="server" Width="108px" Font-Size="10pt" ReadOnly="True"></asp:textbox></TD>
								<TD style="WIDTH: 113px" align="right"><asp:label id="Label3" runat="server" Width="71px" Font-Size="10pt">收货时间：</asp:label></TD>
								<TD style="WIDTH: 162px"><asp:label id="lblReceiveDate" runat="server" Width="191px" Font-Size="10pt"></asp:label></TD>
								<TD style="WIDTH: 179px" align="center"><asp:button id="btnEdit" runat="server" Width="76px" Font-Size="10pt" Text="编辑" onclick="btnEdit_Click"></asp:button></TD>
								<TD style="WIDTH: 179px" align="center"><asp:button id="btnValidEnter" runat="server" Width="80px" Font-Size="10pt" Text="验收入库" onclick="btnValidEnter_Click"></asp:button></TD>
							</TR>
							<tr>
								<td colspan="10" style="FONT-SIZE: 10pt; COLOR: #0000cc">注--编辑方法：点击“编辑”，对本页的产品运输损耗量、验收合格和验收不合格三个量进行编辑，点击“锁定编辑”，再切换到下一页，同样点击“编辑”，输入数量完后再点击“锁定编辑”，以此类推，直到所有页的产品数量都编辑完成后，最终才点击“验收入库”。</td>
							</tr>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
			<TABLE id="Table4" cellSpacing="1" cellPadding="1" width="100%" border="0">
				<TR>
					<TD align="center"><asp:datagrid id="DataGrid1" runat="server" Width="100%" Font-Size="X-Small" PagerStyle-HorizontalAlign="Right"
							BorderColor="Black" BorderWidth="1px" CellPadding="3" Font-Name="Verdana" HeaderStyle-BackColor="SteelBlue"
							AlternatingItemStyle-BackColor="#660033" Font-Names="Verdana" AutoGenerateColumns="False" PageSize="20" AllowPaging="True">
							<FooterStyle Wrap="False"></FooterStyle>
							<SelectedItemStyle Wrap="False"></SelectedItemStyle>
							<EditItemStyle Wrap="False"></EditItemStyle>
							<AlternatingItemStyle Wrap="False" ForeColor="Black" BackColor="#E6E6E6"></AlternatingItemStyle>
							<ItemStyle Wrap="False" ForeColor="Black" BackColor="White"></ItemStyle>
							<HeaderStyle Font-Size="Small" Font-Bold="True" Wrap="False" ForeColor="White" BackColor="#880028"></HeaderStyle>
							<Columns>
								<asp:BoundColumn DataField="cnnAssignSerialNo" ReadOnly="True" HeaderText="分货流水"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnnOrderSerialNo" ReadOnly="True" HeaderText="订单流水"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnnProduceSerialNo" ReadOnly="True" HeaderText="生产流水"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcShipDeptName" ReadOnly="True" HeaderText="发货单位"></asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="cnvcShipDeptID" ReadOnly="True" HeaderText="发货单位ID"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcShipOperID" ReadOnly="True" HeaderText="发货人"></asp:BoundColumn>
								<asp:BoundColumn DataField="cndShipDate" ReadOnly="True" HeaderText="发货时间"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcProductCode" ReadOnly="True" HeaderText="产品编码"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcProductName" ReadOnly="True" HeaderText="产品名称"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcProductTypeName" ReadOnly="True" HeaderText="产品类型"></asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="cnvcProductType" ReadOnly="True" HeaderText="产品类型ID"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcUnit" ReadOnly="True" HeaderText="单位"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnnOrderCount" ReadOnly="True" HeaderText="订单数量"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnnCount" ReadOnly="True" HeaderText="实发数量"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnnTravelCount" ReadOnly="True" HeaderText="运输损耗量"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnnValidOkCount" ReadOnly="True" HeaderText="验收合格"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnnValidNoCount" ReadOnly="True" HeaderText="验收不合格"></asp:BoundColumn>
								<asp:TemplateColumn HeaderText="运输损耗量">
									<ItemTemplate>
										<asp:textbox id="txtTravel" runat="server" Font-Size="10pt" Width="80px"></asp:textbox>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="验收合格">
									<ItemTemplate>
										<asp:textbox id="txtValidOk" runat="server" Font-Size="10pt" Width="80px"></asp:textbox>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="验收不合格">
									<ItemTemplate>
										<asp:textbox id="txtValidNo" runat="server" Font-Size="10pt" Width="80px"></asp:textbox>
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
							<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
						</asp:datagrid></TD>
				</TR>
			</TABLE>
		</FORM>
	</body>
</HTML>
