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
using AMSApp.zhenghua;
using AMSApp.zhenghua.Business;
namespace AMSApp.zhenghua.Produce
{
	/// <summary>
	/// wfmCostReport 的摘要说明。
	/// </summary>
	public partial class wfmCashierDifSum : wfmBase
	{
		protected ucPageView UcPageView1;
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
			if(!this.IsPostBack)
			{
//				this.BindDept(ddlDept, "cnvcDeptType <>'Corp'");
//				ListItem li = new ListItem("所有", "%");
//				this.ddlDept.Items.Add(li);
//				this.SetDDL(this.ddlDept,this.oper.strDeptID);
//				if(this.oper.strDeptID !="CEN00")
//				{				
//					this.ddlDept.Enabled = false;		
//				}
				this.txtBeginDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
				this.txtEndDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
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

		private DataTable query()
		{
			DataTable dtOut1 = Helper.QueryLongTrans("CashierDifSum '"+txtBeginDate.Text+"','"+txtEndDate.Text+"'");			
			return dtOut1;
		}
		protected void Button1_Click(object sender, System.EventArgs e)
		{
			//导出
			DataTable dt = query();
			dt.TableName="收银员收款差异统计表";
			string str = this.ExportTable(dt);
			this.ExportToXls(this,"收银员收款差异统计表",str);
		}

		protected void Button2_Click(object sender, System.EventArgs e)
		{
			//查询
			DataTable dtOut1 = query();
			UcPageView1.MyDataGrid.PageSize = 30;
			DataView dvOut =new DataView(dtOut1);
			this.UcPageView1.MyDataSource = dvOut;
			this.UcPageView1.BindGrid();
		}
	}
}
