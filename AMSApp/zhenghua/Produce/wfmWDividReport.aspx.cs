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
	/// wfmDividReport 的摘要说明。
	/// </summary>
	public partial class wfmWDividReport : wfmBase
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
			this.btnPrint.Attributes.Add("onclick","document.all.WebBrowser.ExecWB(6,1);");
			this.btnDPrint.Attributes.Add("onclick","document.all.WebBrowser.ExecWB(6,6);");
			if(!this.IsPostBack)
			{
				if(Request["OrderSerialNo"]== null ||Request["ProduceSerialNo"]== null||Request["AssignSerialNo"]==null)
				{
					Popup("无效链接");
				}
				string strOrderSerialNo = Request["OrderSerialNo"].ToString();
				string strAssignSerialNo = Request["AssignSerialNo"].ToString();
				txtOrderSerialNo.Text = strOrderSerialNo;
				txtProduceSerialNo.Text = Request["ProduceSerialNo"].ToString();
				txtAssignSerialNo.Text = strAssignSerialNo;
				BindOrder(strOrderSerialNo,strAssignSerialNo);
			}
		}
		private void BindOrder(string strOrderSerialNo,string strAssignSerialNo)
		{
			string strOrder = "select * from tbOrderBook where cnnOrderSerialNo=" + strOrderSerialNo;
			DataTable dtOrder = Helper.Query(strOrder);
			if(dtOrder.Rows.Count == 0)
			{
				Popup("数据异常，无此订单");
				return;
			}
			this.DataTableConvert(dtOrder, "cnvcOrderDeptID", "tbDept", "cnvcDeptID", "cnvcDeptName", "");
			this.DataTableConvert(dtOrder, "cnvcProduceDeptID", "tbDept", "cnvcDeptID", "cnvcDeptName", "");
			OrderBook order = new OrderBook(dtOrder);
			string strAssign = "select * from tbAssignDetail where cnnAssignSerialNo=" + strAssignSerialNo;
			DataTable dtAssign = Helper.Query(strAssign);
			if(dtAssign.Rows.Count == 0)
			{
				Popup("数据异常，此订单无分货数据");
				return;
			}
			this.DataGrid1.DataSource = dtAssign;
			this.DataGrid1.DataBind();

			//this.lblOper.Text = oper.strOperName;
			//this.lblOrderDept.Text = dtOrder.Rows[0]["cnvcOrderDeptIDComments"].ToString();
			//this.lblShipDate.Text = order.cndShipDate.ToString("yyyy-MM-dd");

			this.lblTitle.Text = "面包工坊"+dtOrder.Rows[0]["cnvcProduceDeptIDComments"].ToString()+DateTime.Now.ToString("MM月dd日")+"订单送货报表";
			this.lblCustomName.Text = order.cnvcCustomName;
			this.lblShipAddress.Text = order.cnvcShipAddress;
			this.lblLinkPhone.Text = order.cnvcLinkPhone;
			this.lblArrivedDate.Text = order.cndArrivedDate.ToString("yyyy年MM月dd日hh点mm分");
			this.lblCount.Text = dtAssign.Compute("sum(cnnCount)","").ToString();
			this.lblSum.Text = dtAssign.Compute("sum(cnnSum)", "").ToString();
			this.lblShipDate.Text = DateTime.Now.ToString("yyyy年MM月dd日hh点mm分");
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

		}
		#endregion

		protected void btnReturn_Click(object sender, System.EventArgs e)
		{
			this.Response.Redirect("wfmDividAdjust.aspx?ProduceSerialNo="+txtProduceSerialNo.Text);
		}

		protected void btnPrint_ServerClick(object sender, System.EventArgs e)
		{
			try
			{
				ProduceFacade pf = new ProduceFacade();
				OperLog operLog = new OperLog();
				operLog.cnvcOperID = oper.strLoginID;
				operLog.cnvcDeptID = oper.strDeptID;
				operLog.cnvcOperType = "发货单";
				pf.AssignPrint(txtAssignSerialNo.Text,operLog);
			}
			catch(Exception ex)
			{
				Popup(ex.Message);
			}
		}

		protected void btnDPrint_ServerClick(object sender, System.EventArgs e)
		{
			try
			{
				ProduceFacade pf = new ProduceFacade();
				OperLog operLog = new OperLog();
				operLog.cnvcOperID = oper.strLoginID;
				operLog.cnvcDeptID = oper.strDeptID;
				operLog.cnvcOperType = "发货单";
				pf.AssignPrint(txtAssignSerialNo.Text,operLog);
			}
			catch(Exception ex)
			{
				Popup(ex.Message);
			}
		}
	}
}
