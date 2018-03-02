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
	/// wfmDividReport 的摘要说明。
	/// </summary>
	public partial class wfmOrderSumReport : wfmBase
	{
		protected System.Web.UI.WebControls.Label Label2;
		//protected System.Web.UI.WebControls.Label lblOper;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.Label Label6;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
			if(!this.IsPostBack)
			{
				if(Session["OrderSumReport"] == null)
				{
					Popup("无效链接");
					return;
				}				
				string strReport = Session["OrderSumReport"].ToString();
				DataTable dtReport = Helper.Query(strReport);
				

				this.DataTableConvert(dtReport, "cnvcOrderDeptID", "tbDept", "cnvcDeptID", "cnvcDeptName", "");
				//this.DataTableConvert(dtReport, "cnvcOrderOperID", "tbLogin", "vcLoginID", "vcOperName", "");
				
				DataTable dtDept = (DataTable)Application["tbDept"];
				
				//this.DataTableConvert(dtReport, "cnvcOrderType", "tbNameCode", "cnvcCode", "cnvcName", "cnvcType='ORDERTYPE'");
				//this.DataTableConvert(dtReport, "cnvcOrderState", "tbNameCode", "cnvcCode", "cnvcName", "cnvcType='ORDERSTATE'");
				//
				//this.DataTableConvert(dtReport, "cnvcProduceDeptID", "tbDept", "cnvcDeptID", "cnvcDeptName", "");
			
				DataTable dtpt = new DataTable();
				dtpt.Columns.Add("产品编码");
				//dtpt.Columns.Add("产品编码");
				dtpt.Columns.Add("产品名称");
				dtpt.Columns.Add("规格");
				dtpt.Columns.Add("单位");
				foreach(DataRow drDept in dtDept.Rows)
				{
					if(!drDept["cnvcDeptType"].ToString().Equals("Corp"))//&&!drDept["cnvcDeptType"].ToString().Equals("FYZX1"))
					{
						dtpt.Columns.Add(drDept["cnvcDeptName"].ToString());	
					}
						
				}
				//dtpt.Columns.Add("合计");
				dtpt.Columns.Add("合计");
				//Hashtable htProduct = new Hashtable();
				foreach(DataRow drReport in dtReport.Rows)
				{
					//DataRow drpt = null;
					DataRow[] drpts = dtpt.Select("产品编码='"+drReport["cnvcProductCode"].ToString()+"'");
					if(drpts.Length > 0)
					{
						DataRow drpt = drpts[0];
						drpt["产品编码"] = drReport["cnvcProductCode"].ToString();
						drpt["产品名称"] = drReport["cnvcProductName"].ToString();
						drpt["规格"] = drReport["cnvcProduct_Statd"].ToString();
						drpt["单位"] = drReport["cnvcUnit"].ToString();
						drpt[drReport["cnvcOrderDeptIDComments"].ToString()] = drReport["cnnCount"].ToString();
//						DataRow[] drReport2 = dtAssign2.Select("cnvcProductCode='" + drReport["cnvcProductCode"].ToString() + "'");
//						if(drReport2.Length > 0)
//							drpt["合计"] = drReport2[0]["cnnCount"];
						//dtpt.Rows.Add(drpt);
					}
					else
					{
						DataRow drpt = dtpt.NewRow();
						drpt["产品编码"] = drReport["cnvcProductCode"].ToString();
						drpt["产品名称"] = drReport["cnvcProductName"].ToString();
						drpt["规格"] = drReport["cnvcProduct_Statd"].ToString();
						drpt["单位"] = drReport["cnvcUnit"].ToString();
						drpt[drReport["cnvcOrderDeptIDComments"].ToString()] = drReport["cnnCount"].ToString();
//						DataRow[] drReport2 = dtAssign2.Select("cnvcProductCode='" + drReport["cnvcProductCode"].ToString() + "'");
//						if(drReport2.Length > 0)
//							drpt["合计"] = drReport2[0]["cnnCount"];
						dtpt.Rows.Add(drpt);

					}
				
				
				}
				//合计数据
				foreach(DataRow dr in dtpt.Rows)
				{
					foreach(DataRow drDept in dtDept.Rows)
					{
						if(!drDept["cnvcDeptType"].ToString().Equals("Corp"))//&&!drDept["cnvcDeptType"].ToString().Equals("FYZX1"))
						{
							string strCount = dr[drDept["cnvcDeptName"].ToString()].ToString();	
							if(strCount == "")
								strCount = "0";
							string strSum = dr["合计"].ToString();
							if(strSum == "")
								strSum = "0";
							dr["合计"] = decimal.Parse(strCount)+decimal.Parse(strSum);
						}
						
					}
				}

				this.DataGrid1.DataSource = dtpt;
				this.DataGrid1.DataBind();

				//this.lblOper.Text = this.oper.strOperName;
				this.lblDate.Text = DateTime.Now.ToString("yyyy年MM月dd日");
//				if(Request["OrderType"] != null)
//				{
//					this.lblDate.Text += Request["OrderType"].ToString();
//				}
				this.lblDate.Text += "汇总表";
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

		}
		#endregion

		protected void btnReturn_Click(object sender, System.EventArgs e)
		{
			Session["OrderSumReport"] = null;
			this.Response.Redirect("wfmOrderQuery.aspx");
		}
	}
}
