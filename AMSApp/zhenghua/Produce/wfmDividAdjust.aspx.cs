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
	/// wfmDividModify 的摘要说明。
	/// </summary>
	public partial class wfmDividAdjust : wfmBase
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
			if(!this.IsPostBack)
			{
				this.BindDept(ddlProduceDept, "cnvcDeptType <>'Corp'");
				this.BindDept(ddlOrderDept, "cnvcDeptType <>'Corp'");
				ListItem li = new ListItem("所有", "%");
				ddlOrderDept.Items.Insert(0, li);
				this.BindNameCode(ddlOrderType, "cnvcType='ORDERTYPE'");
				this.ddlOrderType.Items.Insert(0, li);
				if(Request["ProduceSerialNo"] == null)
				{
					Popup("无效链接");
					return;
				}
				//string strOperType = Request["OperType"].ToString();
				//ViewState["OperType"] = strOperType;
				string strProduceSerialNo = Request["ProduceSerialNo"].ToString();
				BindProduceLog(strProduceSerialNo);
				//QueryProduceDetail();
			}
		}

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
				
				this.txtProduceDate.Enabled = false;
				this.txtProduceSerialNo.Enabled = false;
				this.ddlProduceDept.Enabled = false;
				txtProduceState.Text = produceLog.cnvcProduceState;
				
				BindAssignLog(strProduceSerialNo);

			}
		}
		private void BindAssignLog(string strProduceSerialNo)
		{
			string strAssignSql = "select distinct cnnAssignSerialNo from tbAssignLog where cnnProduceSerialNo=" + strProduceSerialNo;
			DataTable dtAssignLog = Helper.Query(strAssignSql);
			this.ddlAssignSerialNo.Items.Clear();
			this.ddlAssignSerialNo.DataSource = dtAssignLog;
			this.ddlAssignSerialNo.DataTextField = "cnnAssignSerialNo";
			this.ddlAssignSerialNo.DataValueField = "cnnAssignSerialNo";
			this.ddlAssignSerialNo.DataBind();
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
			this.Response.Redirect("wfmProducePlanQueryGoods.aspx");
		}
		private void BindGrid()
		{
			string strSql = "select a.cnnProduceSerialNo,a.cnnAssignSerialNo,a.cnnOrderSerialNo,c.cnvcOrderDeptID,c.cnvcOrderType,c.cndShipDate,c.cnvcCustomName from tbAssignLog a "
			                + " left outer join tbOrderBook c on a.cnnOrderSerialNo=c.cnnOrderSerialNo ";
			strSql += " where a.cnnAssignSerialNo=" + ddlAssignSerialNo.SelectedValue;//txtProduceSerialNo.Text;
			//strSql += " and a.cnvcShipDeptID ='" + ddlProduceDept.SelectedValue + "'";
			strSql += " and a.cnvcReceiveDeptID like '" + ddlOrderDept.SelectedValue + "'";
			strSql += " and c.cnvcOrderType like '" + ddlOrderType.SelectedValue + "'";
			DataTable dtAssign = Helper.Query(strSql);
			dtAssign.Columns.Add("cnvcLink");
			foreach(DataRow dr in dtAssign.Rows)
			{
				string strOrderType = dr["cnvcOrderType"].ToString();
				string strProduceSerialNo = dr["cnnProduceSerialNo"].ToString();
				string strOrderSerialNo = dr["cnnOrderSerialNo"].ToString();
				if(strOrderType == "MDO")
				{
					dr["cnvcLink"] = "wfmDividReport.aspx?OrderSerialNo="+strOrderSerialNo+"&ProduceSerialNo="+strProduceSerialNo+"&AssignSerialNo="+ddlAssignSerialNo.SelectedValue;
				}
				else
				{
					dr["cnvcLink"] = "wfmWDividReport.aspx?OrderSerialNo="+strOrderSerialNo+"&ProduceSerialNo="+strProduceSerialNo+"&AssignSerialNo="+ddlAssignSerialNo.SelectedValue;
				}
			}
			this.DataTableConvert(dtAssign, "cnvcOrderDeptID", "tbDept", "cnvcDeptID", "cnvcDeptName", "");
			this.DataTableConvert(dtAssign, "cnvcOrderType", "tbNameCode", "cnvcCode", "cnvcName", "cnvcType='ORDERTYPE'");
			this.DataGrid1.DataSource = dtAssign;
			this.DataGrid1.DataBind();
		}
		protected void btnQuery_Click(object sender, System.EventArgs e)
		{
			BindGrid();
		}
	}
}
