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

namespace AMSApp.zhenghua.Produce
{
	/// <summary>
	/// wfmProduceOrder 的摘要说明。
	/// </summary>
	public partial class wfmProduceOrder : wfmBase
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
			if(!this.IsPostBack)
			{
				this.BindDept(ddlProduceDept, "cnvcDeptType <>'Corp'");
				if(Request["OperType"] == null || Request["ProduceSerialNo"] == null)
				{
					Popup("无效链接");
					return;
				}
				string strOperType = Request["OperType"].ToString();
				ViewState["OperType"] = strOperType;
				string strProduceSerialNo = Request["ProduceSerialNo"].ToString();

				BindDisp(strOperType);
				BindProduceLog(strProduceSerialNo);
				
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
			this.DataGrid1.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DataGrid1_PageIndexChanged);
			this.DataGrid2.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DataGrid2_PageIndexChanged);

		}
		#endregion

		private void BindProduceLog(string strProduceSerialNo)
		{
			string strSql = "select * from tbProduceLog where cnnProduceSerialNo=" + strProduceSerialNo;
			DataTable dtProduceLog = Helper.Query(strSql);
			if(dtProduceLog.Rows.Count > 0)
			{
				ProduceLog produceLog = new ProduceLog(dtProduceLog);
				this.ddlProduceDept.Items.FindByValue(produceLog.cnvcProduceDeptID).Selected = true;
				this.txtProduceSerialNo.Text = produceLog.cnnProduceSerialNo.ToString();
				this.txtProduceDate.Text = produceLog.cndProduceDate.ToString("yyyy-MM-dd");
				this.txtShipBeginDate.Text = produceLog.cndShipBeginDate.ToString("yyyy-MM-dd");
				this.txtShipEndDate.Text = produceLog.cndShipEndDate.ToString("yyyy-MM-dd");
					
				if(produceLog.cnvcProduceState!= "0")
				{
					this.btnModify.Visible = false;
					this.txtProduceDate.Enabled = false;
					this.txtProduceSerialNo.Enabled = false;
					this.txtShipBeginDate.Enabled = false;
					this.txtShipEndDate.Enabled = false;
				}

			}
		}
		protected void btnReturn_Click(object sender, System.EventArgs e)
		{
			this.Response.Redirect("wfmProducePlanQuery.aspx");
		}

		protected void btnCancel_Click(object sender, System.EventArgs e)
		{
			string strOperType = ViewState["OperType"].ToString();
			if(strOperType == "Edit")
			{
				BindProduceLog(this.txtProduceSerialNo.Text);
			}
		}
		private void BindDisp(string strOperType)
		{
			switch(strOperType)
			{
				case "Edit":
					this.ddlProduceDept.Enabled = false;
					this.txtProduceDate.Enabled = true;
					this.txtProduceSerialNo.Enabled = false;
					this.txtShipBeginDate.Enabled = true;
					this.txtShipEndDate.Enabled = true;

					this.btnModify.Visible = true;
					this.btnLinkOrder.Visible = false;
					this.btnQueryOrder.Visible = false;
					this.btnQueryProduct.Visible = false;

					this.DataGrid1.Visible = false;
					this.DataGrid2.Visible = false;

					this.lblTitle.Text = "生产计划修改";
					break;
				case "Order":
					this.ddlProduceDept.Enabled = false;
					this.txtProduceDate.Enabled = false;
					this.txtProduceSerialNo.Enabled = false;
					this.txtShipBeginDate.Enabled = false;
					this.txtShipEndDate.Enabled = false;

					this.btnModify.Visible = false;
					this.btnLinkOrder.Visible = true;
					this.btnQueryOrder.Visible = true;
					this.btnQueryProduct.Visible = true;
					this.btnCancel.Visible = false;

					this.DataGrid1.Visible = false;
					this.DataGrid2.Visible = false;

					this.DataGrid1.Columns[2].Visible = false;
					this.DataGrid1.Columns[3].Visible = false;

					this.DataGrid1.Caption = "订单";
					this.lblTitle.Text = "生产计划关联订单";
					this.btnLinkOrder.Text = "关联订单";
					this.btnQueryOrder.Text = "订单清单";
					break;
				case "Add":
					this.ddlProduceDept.Enabled = false;
					this.txtProduceDate.Enabled = false;
					this.txtProduceSerialNo.Enabled = false;
					this.txtShipBeginDate.Enabled = false;
					this.txtShipEndDate.Enabled = false;

					this.btnModify.Visible = false;
					this.btnLinkOrder.Visible = true;
					this.btnQueryOrder.Visible = true;
					this.btnQueryProduct.Visible = true;
					this.btnCancel.Visible = false;

					this.DataGrid1.Visible = false;
					this.DataGrid2.Visible = false;

					this.DataGrid1.Columns[2].Visible = true;
					this.DataGrid1.Columns[3].Visible = false;

					this.DataGrid1.Caption = "加单";
					this.lblTitle.Text = "生产计划关联加单";
					this.btnLinkOrder.Text = "关联加单";
					this.btnQueryOrder.Text = "加单清单";
					break;
				case "Reduce":
					this.ddlProduceDept.Enabled = false;
					this.txtProduceDate.Enabled = false;
					this.txtProduceSerialNo.Enabled = false;
					this.txtShipBeginDate.Enabled = false;
					this.txtShipEndDate.Enabled = false;

					this.btnModify.Visible = false;
					this.btnLinkOrder.Visible = true;
					this.btnQueryOrder.Visible = true;
					this.btnQueryProduct.Visible = true;
					this.btnCancel.Visible = false;

					this.DataGrid1.Visible = false;
					this.DataGrid2.Visible = false;

					this.DataGrid1.Columns[2].Visible = false;
					this.DataGrid1.Columns[3].Visible = true;

					this.DataGrid1.Caption = "减单";
					this.lblTitle.Text = "生产计划关联减单";
					this.btnLinkOrder.Text = "关联减单";
					this.btnQueryOrder.Text = "减单清单";
					break;
			}
		}

		protected void btnModify_Click(object sender, System.EventArgs e)
		{
			try
			{
				if(JudgeIsNull(txtProduceDate.Text,"生产日期"))
				{
					//Popup();
					return;
				}
				if(JudgeIsNull(txtShipBeginDate.Text,"开始日期"))
				{
					//Popup();
					return;
				}
				if(JudgeIsNull(txtShipEndDate.Text,"结束日期"))
				{
					//Popup();
					return;
				}
				ProduceLog pLog = new ProduceLog();
				pLog.cnnProduceSerialNo = decimal.Parse(txtProduceSerialNo.Text);
				pLog.cndProduceDate = DateTime.Parse(txtProduceDate.Text);
				pLog.cndShipBeginDate = DateTime.Parse(txtShipBeginDate.Text);
				pLog.cndShipEndDate = DateTime.Parse(txtShipEndDate.Text);
				pLog.cnvcProduceDeptID = ddlProduceDept.SelectedValue;
				pLog.cnvcOperID = oper.strLoginID;

				OperLog operLog = new OperLog();
				operLog.cnvcOperID = oper.strLoginID;
				operLog.cnvcDeptID = oper.strDeptID;
				operLog.cnvcOperType = "修改生产计划";

				ProduceFacade pFacade = new ProduceFacade();
				pFacade.UpdateProduceLog(pLog,operLog);
				Popup("修改成功");
			}
			catch(Exception ex)
			{
				Popup(ex.Message);
			}
		}

		protected void btnLinkOrder_Click(object sender, System.EventArgs e)
		{
			try
			{
				ProduceLog pLog = new ProduceLog();
				pLog.cnnProduceSerialNo = decimal.Parse(txtProduceSerialNo.Text);
				pLog.cndProduceDate = DateTime.Parse(txtProduceDate.Text);
				pLog.cndShipBeginDate = DateTime.Parse(txtShipBeginDate.Text);
				pLog.cndShipEndDate = DateTime.Parse(txtShipEndDate.Text);
				pLog.cnvcProduceDeptID = ddlProduceDept.SelectedValue;
				pLog.cnvcOperID = oper.strLoginID;

				OperLog operLog = new OperLog();
				operLog.cnvcOperID = oper.strLoginID;
				operLog.cnvcDeptID = oper.strDeptID;
				


				ProduceFacade pFacade = new ProduceFacade();
				switch(ViewState["OperType"].ToString())
				{
					case "Order":
						operLog.cnvcOperType = "关联订单";
						pFacade.LindOrder(pLog,operLog);
						Popup("关联订单成功");
						break;
					case "Add":
						operLog.cnvcOperType = "关联加单";
						pFacade.LindOrderAdd(pLog,operLog);
						Popup("关联加单成功");
						break;
					case "Reduce":
						operLog.cnvcOperType = "关联减单";
						pFacade.LindOrderReduce(pLog,operLog);
						Popup("关联减单成功");
						break;
				}
			}		
			catch(Exception ex)
			{
				Popup(ex.Message);
			}
			
		}

		protected void btnQueryOrder_Click(object sender, System.EventArgs e)
		{
			string strOperType = ViewState["OperType"].ToString();
			string strSql = "";
			DataTable dtProduceOrderLog = null;
			switch(strOperType)
			{
				case "Order":
					strSql = "select a.cnnProduceSerialNo,b.cnnOrderSerialNo,'' as cnnAddSerialNo,'' as cnnReduceSerialNo,b.cnvcOrderDeptID,b.cnvcProduceDeptID,b.cnvcOrderType,b.cndShipDate,b.cnvcOrderOperID,b.cndOrderDate from tbProduceOrderLog a "
						+ " left join tbOrderBook b on a.cnnOrderSerialNo=b.cnnOrderSerialNo "
						+ " where a.cnvcType='0' and a.cnnProduceSerialNo=" + txtProduceSerialNo.Text;
					dtProduceOrderLog = Helper.Query(strSql);
					this.DataTableConvert(dtProduceOrderLog, "cnvcOrderType", "tbNameCode", "cnvcCode", "cnvcName", "cnvcType='ORDERTYPE'");
					this.DataGrid1.Columns[6].HeaderText = "订单类型";
					break;
				case "Add":
					strSql = "select a.cnnProduceSerialNo,b.cnnOrderSerialNo,c.cnnAddSerialNo,'' as cnnReduceSerialNo,b.cnvcOrderDeptID,b.cnvcProduceDeptID,cnvcAddType as cnvcOrderType,b.cndShipDate,c.cnvcOperID as cnvcOrderOperID,c.cndOperDate as cndOrderDate from tbProduceOrderLog a "
					         +
					         " left outer join (select distinct cnnAddSerialNo,cnnOrderSerialNo,cnvcAddType,cnvcOperID,cndOperDate from tbOrderAddLog) c on a.cnnOrderSerialNo=c.cnnAddSerialNo  "
					         + " left outer join tbOrderBook b on c.cnnOrderSerialNo=b.cnnOrderSerialNo "
					         + " where a.cnvcType='1' and a.cnnProduceSerialNo=" + txtProduceSerialNo.Text;
					dtProduceOrderLog = Helper.Query(strSql);
					this.DataTableConvert(dtProduceOrderLog, "cnvcOrderType", "tbNameCode", "cnvcCode", "cnvcName", "cnvcType='ORDEROPERTYPE'");
					this.DataGrid1.Columns[6].HeaderText = "加单类型";
					break;
				case "Reduce":
					strSql = "select a.cnnProduceSerialNo,b.cnnOrderSerialNo,'' as cnnAddSerialNo,c.cnnReduceSerialNo,b.cnvcOrderDeptID,b.cnvcProduceDeptID,cnvcReduceType as cnvcOrderType,b.cndShipDate,c.cnvcOperID as cnvcOrderOperID,c.cndOperDate as cndOrderDate from tbProduceOrderLog a "
						+ " left join (select  distinct cnnReduceSerialNo,cnnOrderSerialNo,cnvcReduceType,cnvcOperID,cndOperDate from tbOrderReduceLog) c on a.cnnOrderSerialNo=c.cnnReduceSerialNo "
						+ " left join tbOrderBook b on c.cnnOrderSerialNo=b.cnnOrderSerialNo "
						+ " where a.cnvcType='2' and cnnProduceSerialNo=" + txtProduceSerialNo.Text;
					dtProduceOrderLog = Helper.Query(strSql);
					this.DataTableConvert(dtProduceOrderLog, "cnvcOrderType", "tbNameCode", "cnvcCode", "cnvcName", "cnvcType='ORDEROPERTYPE'");
					this.DataGrid1.Columns[6].HeaderText = "减单类型";
					break;
			}

			//dtProduceOrderLog = Helper.Query(strSql);
			this.DataTableConvert(dtProduceOrderLog, "cnvcOrderDeptID", "tbDept", "cnvcDeptID", "cnvcDeptName", "");
			this.DataTableConvert(dtProduceOrderLog, "cnvcProduceDeptID", "tbDept", "cnvcDeptID", "cnvcDeptName", "");			
			this.DataTableConvert(dtProduceOrderLog, "cnvcOrderOperID", "tbLogin", "vcLoginID", "vcOperName", "");

			this.DataGrid1.DataSource = dtProduceOrderLog;
			this.DataGrid1.DataBind();
			this.DataGrid1.Visible = true;
			this.DataGrid2.Visible = false;
			
		}

		protected void btnQueryProduct_Click(object sender, System.EventArgs e)
		{
			string strOperType = ViewState["OperType"].ToString();
			string strSql = "";
			DataTable dtDetail = null;
			switch(strOperType)
			{
				case "Order":
					strSql = "select * from tbProduceDetail where cnnProduceSerialNo=" + txtProduceSerialNo.Text;

					break;
				case "Add":
					strSql = "select * from tbProduceDetailAdd where cnnProduceSerialNo=" + txtProduceSerialNo.Text;
					break;
				case "Reduce":
					strSql = "select * from tbProduceDetailReduce where cnnProduceSerialNo=" + txtProduceSerialNo.Text;
					break;
			}

			dtDetail = Helper.Query(strSql);
			this.DataGrid2.DataSource = dtDetail;
			this.DataGrid2.DataBind();
			this.DataGrid2.Visible = true;
			this.DataGrid1.Visible = false;
		}

		private void DataGrid1_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			this.DataGrid1.CurrentPageIndex = e.NewPageIndex;
			this.btnQueryOrder_Click(null, null);
		}

		private void DataGrid2_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			this.DataGrid2.CurrentPageIndex = e.NewPageIndex;
			this.btnQueryProduct_Click(null, null);
		}
	}
}
