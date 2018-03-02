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
	/// wfmProducePlan 的摘要说明。
	/// </summary>
	public partial class wfmProducePlan : wfmBase
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
			if(!this.IsPostBack)
			{
//				this.BindDept(ddlProduceDept, "cnvcDeptType <>'Corp'");
				this.BindDept(ddlProduceDept, "cnvcDeptType ='Factory'");
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

		protected void btnCancel_Click(object sender, System.EventArgs e)
		{
			this.txtProduceDate.Text = "";
			this.txtShipBeginDate.Text = "";
			this.txtShipEndDate.Text = "";
		}

		
		protected void btnOK_Click(object sender, System.EventArgs e)
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

				ProduceLog producePlan = new ProduceLog();
				producePlan.cnvcProduceDeptID = ddlProduceDept.SelectedValue;
				producePlan.cndProduceDate = DateTime.Parse(txtProduceDate.Text);
				producePlan.cndShipBeginDate = DateTime.Parse(txtShipBeginDate.Text);
				producePlan.cndShipEndDate = DateTime.Parse(txtShipEndDate.Text);
				producePlan.cnvcOperID = oper.strLoginID;
				producePlan.cnvcProduceState = "0";

				OperLog operLog = new OperLog();
				operLog.cnvcOperID = oper.strLoginID;
				operLog.cnvcDeptID = oper.strDeptID;
				operLog.cnvcOperType = "添加生产计划";

				ProduceFacade produce = new ProduceFacade();
				produce.AddProduceLog(producePlan,operLog);
				Popup("生产计划添加成功");
				btnCancel_Click(null, null);
			}
			catch(Exception ex)
			{
				Popup(ex.Message);
			}
		}

		protected void btnReturn_Click(object sender, System.EventArgs e)
		{
			this.Response.Redirect("wfmProducePlanQuery.aspx");
		}
	}
}
