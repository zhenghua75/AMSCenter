using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using AMSApp.zhenghua.Business;
using AMSApp.zhenghua.Entity;

namespace AMSApp.zhenghua.Order
{
	/// <summary>
	/// wfmOrderQueryDetail 的摘要说明。
	/// </summary>
	public partial class wfmOrderQueryDetail : wfmBase
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
			if(!this.IsPostBack)
			{
//				string strDept = "select * from tbDept";
//				DataTable dtDept = Helper.Query(strDept);
//				string strOrderType = "select * from tbCommCode where vcCommSign='OType'";
//				string strOrderState = "select * from tbCommCode where vcCommSign='STATE'";
//				DataTable dtOrderType = Helper.Query(strOrderType);
//				DataTable dtOrderState = Helper.Query(strOrderState);
//
//				string strOper = "select * from tbLogin";
//				DataTable dtOper = Helper.Query(strOper);

//				this.ddlSalesRoom.DataSource = dtDept;
//				this.ddlSalesRoom.DataValueField = "cnvcDeptID";
//				this.ddlSalesRoom.DataTextField = "cnvcDeptName";
//				this.ddlSalesRoom.DataBind();
				
				BindDept(ddlSalesRoom, "cnvcDeptType='SalesRoom'");


//				this.ddlProduceDept.DataSource = dtDept;
//				this.ddlProduceDept.DataValueField = "cnvcDeptID";
//				this.ddlProduceDept.DataTextField = "cnvcDeptName";
//				this.ddlProduceDept.DataBind();

				BindDept(ddlProduceDept, "cnvcDeptType<>'Corp'");

//				this.ddlOrderType.DataSource = dtOrderType;
//				this.ddlOrderType.DataValueField = "vcCommCode";
//				this.ddlOrderType.DataTextField = "vcCommName";
//				this.ddlOrderType.DataBind();

				BindNameCode(ddlOrderType, "cnvcType='ORDERTYPE'");

//				this.ddlOrderState.DataSource = dtOrderState;
//				this.ddlOrderState.DataValueField = "vcCommCode";
//				this.ddlOrderState.DataTextField = "vcCommName";
//				this.ddlOrderState.DataBind();

				BindNameCode(ddlOrderState, "cnvcType='ORDERSTATE'");
//				this.ddlOrderOper.DataSource = dtOper;
//				this.ddlOrderOper.DataValueField = "vcLoginID";
//				this.ddlOrderOper.DataTextField = "vcOperName";
//				this.ddlOrderOper.DataBind();

				BindOper(ddlOrderOper, "");

				//this.txtOrderDate.Attributes.Add("onclick", "setDay(this);");
				
				txtArrivedDate.Attributes.Add("onclick", "setDayHM(this);");

				this.tblCustom.Visible = false;

				if(Request["OperFlag"] == null || Request["OrderSerialNo"] == null)
				{
					Popup("无效链接");
					return;
				}
				string strOperFlag = Request["OperFlag"].ToString();
				string strOrderSerialNo = Request["OrderSerialNo"].ToString();
				Disp(strOperFlag);
				//ViewState["OperFlag"] = strOperFlag;
				this.BindOrder(strOrderSerialNo);
				this.BindDetail(strOrderSerialNo);

				//txtArrivedDate.Attributes.Add("onclick", "setDayHM(this);");
				
				if(Session["ProductList"] != null)
				{
					DataTable dtOrderDetail = (DataTable) Session["ProductList"];
					this.DataGrid1.DataSource = dtOrderDetail;
					this.DataGrid1.DataBind();

					if(dtOrderDetail.Rows.Count==0)
						this.lblSumText.Text = "";

					if(dtOrderDetail.Rows.Count>0)
					{
						decimal dCount = 0;
						decimal dSum = 0;
						foreach(DataRow dr in dtOrderDetail.Rows)
						{
							dCount += Convert.ToDecimal(dr["cnnCount"].ToString());
							dSum += Convert.ToDecimal(dr["cnnSum"].ToString());
						}
						string strSumText = "【合计】";
						strSumText += "数量："+dCount.ToString();//dtOrderBookDetail.Compute("sum(cnnOrderCount)","true").ToString();
						strSumText += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
						strSumText += "金额："+dSum.ToString();//dtOrderBookDetail.Compute("sum(cnnSum)","true").ToString();	
						this.lblSumText.Text = strSumText;
					}
				}
			}
		}

		#region Web 窗体设计器生成的代码
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: 该调用是 ASP.NET Web 窗体设计器所必需的。
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{    
			this.DataGrid2.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DataGrid2_PageIndexChanged);
			this.DataGrid2.CancelCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGrid2_CancelCommand);
			this.DataGrid2.EditCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGrid2_EditCommand);
			this.DataGrid2.UpdateCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGrid2_UpdateCommand);
			this.DataGrid2.DeleteCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGrid2_DeleteCommand);
			this.DataGrid2.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.DataGrid2_ItemDataBound);
			this.DataGrid1.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGrid1_ItemCommand);

		}
		#endregion

		protected void ddlOrderType_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(ddlOrderType.SelectedValue == "WDO")
			{
				this.tblCustom.Visible = true;
			}
			else
			{
				this.tblCustom.Visible = false;
			}
			//Disp(ViewState["OperFlag"].ToString());
		}

		private void Disp(string strOperFlag)
		{
			
			switch(strOperFlag)
			{
				case "Edit":
					lblOrderState.Visible = false;
					ddlOrderState.Visible = false;
					lblOrderDate.Visible = false;
					txtOrderDate.Visible = false;
					lblOrderOper.Visible = false;
					ddlOrderOper.Visible = false;



					txtOrderSerialNo.Enabled = false;

					this.lblTitle.Text = "订单编辑";
					this.DataGrid2.Columns[0].Visible = false;
					this.DataGrid2.Columns[1].Visible = false;
					this.DataGrid2.Columns[2].Visible = false;

					this.DataGrid2.Columns[10].Visible = false;
					this.DataGrid2.Columns[11].Visible = false;
					break;
				case "Detail":
					this.ddlSalesRoom.Enabled = false;
					this.ddlProduceDept.Enabled = false;
					this.ddlOrderType.Enabled = false;
					this.txtOrderSerialNo.Enabled = false;
					this.txtShipDate.Enabled = false;
					this.ddlOrderState.Enabled = false;
					this.txtOrderDate.Enabled = false;
					this.ddlOrderOper.Enabled = false;

					this.btnModify.Visible = false;
					this.btnCancel.Visible = false;

					this.txtCustomName.Enabled = false;
					this.txtShipAddress.Enabled = false;
					this.txtLinkPhone.Enabled = false;
					this.txtArrivedDate.Enabled = false;

					this.lblTitle.Text = "订单查看";
					

					this.DataGrid2.Columns[12].Visible = false;
					this.DataGrid2.Columns[13].Visible = false;

					this.DataGrid1.Visible = false;
					break;
			}
		}
		private void BindOrder(string strOrderSerialNo)
		{
			string strSql = "select * from tbOrderBook where cnnOrderSerialNo=" + strOrderSerialNo;
			DataTable dtOrder = Helper.Query(strSql);
			OrderBook order = new OrderBook(dtOrder);
			this.ddlOrderOper.Items.FindByValue(order.cnvcOrderOperID).Selected = true;
			this.ddlOrderState.Items.FindByValue(order.cnvcOrderState).Selected = true;
			this.ddlOrderType.Items.FindByValue(order.cnvcOrderType).Selected = true;
			this.ddlProduceDept.Items.FindByValue(order.cnvcProduceDeptID).Selected = true;
			this.ddlSalesRoom.Items.FindByValue(order.cnvcOrderDeptID).Selected = true;

			if(order.cnvcOrderType == "WDO")
			{
				this.tblCustom.Visible = true;
				this.txtArrivedDate.Text = order.cndArrivedDate.ToString("yyyy-MM-dd hh:mm");
				this.txtLinkPhone.Text = order.cnvcLinkPhone;
				this.txtShipAddress.Text = order.cnvcShipAddress;
				this.txtCustomName.Text = order.cnvcCustomName;
				
			}
			this.txtOrderDate.Text = order.cndOrderDate.ToString("yyyy-MM-dd hh:mm");
			this.txtOrderSerialNo.Text = order.cnnOrderSerialNo.ToString();
			
			this.txtShipDate.Text = order.cndShipDate.ToString("yyyy-MM-dd");
			
		}
		private void BindDetail(string strOrderSerialNo)
		{
			string strSql = "select a.*,c.cnvcProduct_Statd from vwOrderDetail a left outer join(select cnvcMaterialCode as cnvcProductCode,convert(varchar(10),cnnStatdardCount)  + cnvcStandardUnit +'/'+ cnvcUnit as cnvcProduct_Statd,cnvcUnit from tbMaterial  union all select cnvcProductCode as cnvcProductCode,cnvcUnit as cnvcProduct_Statd,cnvcUnit from tbFormula) c on a.cnvcProductCode=c.cnvcProductCode where cnnOrderSerialNo="+strOrderSerialNo;
//			       strSql +"left outer join(select cnvcMaterialCode as cnvcProductCode,convert(varchar(10),cnnStatdardCount)  + cnvcStandardUnit +'/'+ cnvcUnit as cnvcProduct_Statd,cnvcUnit from tbMaterial  union all select cnvcProductCode as cnvcProductCode,cnvcUnit as cnvcProduct_Statd,cnvcUnit from tbFormula) c on a.cnvcProductCode=c.cnvcProductCode ";
//			       strSql +" where cnnOrderSerialNo="+strOrderSerialNo;
			DataTable dtOrderDetail = Helper.Query(strSql);

			if(Session["ProductList"] != null)
			{
				DataTable dtList = (DataTable) Session["ProductList"];
				foreach(DataRow drOrderDetail in dtOrderDetail.Rows)
				{
					string strProductCode = drOrderDetail["cnvcProductCode"].ToString();
					DataRow[] drList = dtList.Select("cnvcProductCode='" + strProductCode + "'");
					if(drList.Length > 0)
					{
						dtList.Rows.Remove(drList[0]);
					}	
					if(dtList.Rows.Count == 0)
					{
						this.DataGrid1.Visible = false;
					}
				}
				Session["ProductList"] = dtList;
			}

			if(dtOrderDetail.Rows.Count>0)
			{
				decimal dCount = 0;
				decimal dSum = 0;
				foreach(DataRow dr in dtOrderDetail.Rows)
				{
					dCount += Convert.ToDecimal(dr["cnnCount"].ToString());
					dSum += Convert.ToDecimal(dr["cnnSum"].ToString());
				}
				string strSumText = "【合计】";
				strSumText += "数量："+dCount.ToString();//dtOrderBookDetail.Compute("sum(cnnOrderCount)","true").ToString();
				strSumText += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
				strSumText += "金额："+dSum.ToString();//dtOrderBookDetail.Compute("sum(cnnSum)","true").ToString();	
				this.lblSumText.Text = strSumText;
			}
			this.DataTableConvert(dtOrderDetail, "cnvcOperType", "tbNameCode", "cnvcCode", "cnvcName", "cnvcType='ORDEROPERTYPE'");
			this.DataGrid2.DataSource = dtOrderDetail;
			this.DataGrid2.DataBind();
		}

		private void DataGrid2_CancelCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			this.DataGrid2.EditItemIndex = -1;
			BindDetail(txtOrderSerialNo.Text);
		}

		private void DataGrid2_EditCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			this.DataGrid2.EditItemIndex = e.Item.ItemIndex;
			BindDetail(txtOrderSerialNo.Text);
		}

		private void DataGrid2_DeleteCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			try
			{
				OrderBookDetail detail = new OrderBookDetail();
				detail.cnnOrderSerialNo = decimal.Parse(txtOrderSerialNo.Text);
				detail.cnvcProductCode = e.Item.Cells[3].Text;
			
				OperLog operLog = new OperLog();
				operLog.cnvcOperID = oper.strLoginID;
				operLog.cnvcDeptID = oper.strDeptID;
				operLog.cnvcOperType = "订单删除产品";

				OrderFacade order = new OrderFacade();
				order.DeleteDetail(detail,operLog);
				Popup("删除成功");
				BindDetail(txtOrderSerialNo.Text);
			}
			catch(Exception ex)
			{
				Popup(ex.Message);
			}
		}

		private void DataGrid2_UpdateCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			try
			{
				string strCount = ((TextBox) e.Item.Cells[8].Controls[0]).Text;

				if(this.JudgeIsNull(strCount,"数量"))
				{
					return;
				}
				if(!this.JudgeIsNum(strCount,"数量"))
				{
					return;
				}
				if(decimal.Parse(strCount) <= 0)
				{
					Popup("数量必需大于零");
					return;
				}


				OrderBookDetail detail = new OrderBookDetail();
				detail.cnnOrderSerialNo = decimal.Parse(txtOrderSerialNo.Text);
				detail.cnvcProductCode = e.Item.Cells[3].Text;
				detail.cnnOrderCount = decimal.Parse(((TextBox) e.Item.Cells[8].Controls[0]).Text);
				detail.cnvcOperID = oper.strLoginID;

				OperLog operLog = new OperLog();
				operLog.cnvcOperID = oper.strLoginID;
				operLog.cnvcDeptID = oper.strDeptID;
				operLog.cnvcOperType = "订单修改产品";

				OrderFacade order = new OrderFacade();
				order.UpdateDetail(detail,operLog);
				Popup("修改成功");
				this.DataGrid2.EditItemIndex = -1;
				BindDetail(txtOrderSerialNo.Text);
			}
			catch(Exception ex)
			{
				Popup(ex.Message);
			}
		}

		private void DataGrid2_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			if(e.Item.ItemType == ListItemType.Item ||e.Item.ItemType == ListItemType.AlternatingItem)
			{								
				LinkButton btnDelete = (LinkButton)(e.Item.Cells[13].Controls[0]);
				btnDelete.Attributes.Add("onClick","JavaScript:return confirm('确定删除？')");
				e.Item.Attributes.Add("onMouseOver","this.style.backgroundColor='#FFCC66'");
				e.Item.Attributes.Add("onMouseOut","this.style.backgroundColor='#ffffff'");
			} 
		}

		private void DataGrid2_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			this.DataGrid2.CurrentPageIndex = e.NewPageIndex;
			BindDetail(txtOrderSerialNo.Text);
		}

		private void DataGrid1_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			if(e.CommandName == "Add")
			{
				try
				{
					if(Session["ProductList"] == null)
					{
						Popup("请选择产品");
						return;
					}
					DataTable dtDetail = (DataTable) Session["ProductList"];
					if(dtDetail.Rows.Count == 0)
					{
						Popup("请选择订单里面没有的产品");
						return;
					}
					OrderFacade order = new OrderFacade();				
					string strOrderSerialNo = txtOrderSerialNo.Text;
					OperLog operLog = new OperLog();
					operLog.cnvcOperID = oper.strLoginID;
					operLog.cnvcDeptID = oper.strDeptID;
					operLog.cnvcOperType = "订单加产品";
					order.AddDetail(strOrderSerialNo, operLog, dtDetail);
					Session["ProductList"] = null;
					this.DataGrid1.DataSource = null;
					this.DataGrid1.DataBind();
					BindDetail(strOrderSerialNo);
					Popup("订单中加入产品成功");
				}
				catch(Exception ex)
				{
					Popup(ex.Message);
				}
			}
		}

		protected void btnReturn_Click(object sender, System.EventArgs e)
		{
			//返回
			this.Response.Redirect("wfmOrderQuery.aspx");
		}

		protected void btnCancel_Click(object sender, System.EventArgs e)
		{
			//
			this.BindOrder(txtOrderSerialNo.Text);
			this.BindDetail(txtOrderSerialNo.Text);
		}

		protected void btnModify_Click(object sender, System.EventArgs e)
		{
			//修改
			try
			{
				OrderBook order = new OrderBook();
				order.cnvcOrderOperID = oper.strLoginID;
				order.cnvcOrderType = ddlOrderType.SelectedValue;
				order.cnvcProduceDeptID = ddlProduceDept.SelectedValue;
				order.cnvcOrderDeptID = ddlSalesRoom.SelectedValue;

				if(order.cnvcOrderType == "WDO")
				{
					order.cndArrivedDate = DateTime.Parse(txtArrivedDate.Text);
					order.cnvcLinkPhone = txtLinkPhone.Text;
					order.cnvcShipAddress = txtShipAddress.Text;
					order.cnvcCustomName = txtCustomName.Text;
				
				}
				order.cnnOrderSerialNo = decimal.Parse(txtOrderSerialNo.Text);
				order.cndShipDate = DateTime.Parse(txtShipDate.Text);

				OperLog operLog = new OperLog();
				operLog.cnvcOperID = oper.strLoginID;
				operLog.cnvcDeptID = oper.strDeptID;
				operLog.cnvcOperType = "修改订单";

				OrderFacade orderFacade = new OrderFacade();
				orderFacade.UpdateOrder(order,operLog);
				Popup("修改成功");
			}
			catch(Exception ex)
			{
				Popup(ex.Message);
			}
		}
	}
}
