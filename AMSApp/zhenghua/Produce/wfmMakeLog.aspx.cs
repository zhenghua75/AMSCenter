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
	/// wfmMakeLog 的摘要说明。
	/// </summary>
	public partial class wfmMakeLog :wfmBase
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
			if(!this.IsPostBack)
			{
				this.BindDept(ddlProduceDept, "cnvcDeptType <>'Corp'");
				//this.BindNameCode(ddlGroup, "cnvcType='GROUP'");
				if(Request["ProduceSerialNo"] == null)
				{
					Popup("无效链接");
					return;
				}
				//string strOperType = Request["OperType"].ToString();
				//ViewState["OperType"] = strOperType;
				string strProduceSerialNo = Request["ProduceSerialNo"].ToString();
				BindProduceLog(strProduceSerialNo);
				Session["ProduceSerialNo"] = strProduceSerialNo;
				this.DataGrid1.Visible = false;
				this.Datagrid2.Visible = false;
				this.Datagrid3.Visible = false;

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
			this.Datagrid2.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.Datagrid2_PageIndexChanged);
			this.Datagrid3.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.Datagrid3_PageIndexChanged);
			this.DataGrid4.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DataGrid4_PageIndexChanged);

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
				
				this.txtProduceDate.Enabled = false;
				this.txtProduceSerialNo.Enabled = false;
				this.ddlProduceDept.Enabled = false;

			}
		}
		protected void btnMakeLog_Click(object sender, System.EventArgs e)
		{
			try
			{
				ProduceLog pLog = new ProduceLog();
				pLog.cnnProduceSerialNo = decimal.Parse(txtProduceSerialNo.Text);
				pLog.cnvcOperID = oper.strLoginID;

				OperLog operLog = new OperLog();
				operLog.cnvcOperID = oper.strLoginID;
				operLog.cnvcDeptID = oper.strDeptID;
				operLog.cnvcOperType = "订单制令";

				ProduceFacade pf = new ProduceFacade();
				pf.AddMakeLog(pLog, "0",operLog);
				Popup("制令成功");
			}
			catch(Exception ex)
			{
				Popup(ex.Message);
			}
		}

		protected void btnMakeAdd_Click(object sender, System.EventArgs e)
		{
			try
			{
				ProduceLog pLog = new ProduceLog();
				pLog.cnnProduceSerialNo = decimal.Parse(txtProduceSerialNo.Text);
				pLog.cnvcOperID = oper.strLoginID;

				OperLog operLog = new OperLog();
				operLog.cnvcOperID = oper.strLoginID;
				operLog.cnvcDeptID = oper.strDeptID;
				operLog.cnvcOperType = "加单制令";

				ProduceFacade pf = new ProduceFacade();
				pf.AddMakeLog(pLog, "1",operLog);
				Popup("制令成功");
			}
			catch(Exception ex)
			{
				Popup(ex.Message);
			}
		}

		protected void btnMakeReduce_Click(object sender, System.EventArgs e)
		{
			try
			{
				ProduceLog pLog = new ProduceLog();
				pLog.cnnProduceSerialNo = decimal.Parse(txtProduceSerialNo.Text);
				pLog.cnvcOperID = oper.strLoginID;

				OperLog operLog = new OperLog();
				operLog.cnvcOperID = oper.strLoginID;
				operLog.cnvcDeptID = oper.strDeptID;
				operLog.cnvcOperType = "减单制令";

				ProduceFacade pf = new ProduceFacade();
				pf.AddMakeLog(pLog, "2",operLog);
				Popup("制令成功");
			}
			catch(Exception ex)
			{
				Popup(ex.Message);
			}
		}

		protected void btnReturn_Click(object sender, System.EventArgs e)
		{
			this.Response.Redirect("wfmProducePlanQueryMake.aspx");
		}

		protected void btnQueryMake_Click(object sender, System.EventArgs e)
		{
			ViewState["MakeType"] = 0;
			BindGrid();
			this.DataGrid1.Visible = true;
			this.Datagrid2.Visible = false;
			this.Datagrid3.Visible = false;
			this.DataGrid4.Visible = false;
		}

		private void BindGrid()
		{
			string strProduceSerialNo = txtProduceSerialNo.Text;
			//string strGroup = ddlGroup.SelectedValue;
			string strMakeType = ViewState["MakeType"].ToString();

			string strSql = "select * from tbMakeLog where cnvcMakeType='"+strMakeType+"' and cnnProduceSerialNo=" + strProduceSerialNo;
			DataTable dtMakeLog = Helper.Query(strSql);
			this.DataTableConvert(dtMakeLog, "cnvcGroup", "tbNameCode", "cnvcCode", "cnvcName", "cnvcType='GROUP'");
			this.DataTableConvert(dtMakeLog, "cnvcOperID", "tbLogin", "vcLoginID", "vcOperName", "");
			this.DataGrid1.DataSource = dtMakeLog;
			this.DataGrid1.DataBind();
		}

		private void BindGrid2()
		{
			string strProduceSerialNo = txtProduceSerialNo.Text;
			//string strGroup = ddlGroup.SelectedValue;
			string strMakeType = ViewState["MakeType"].ToString();

			string strSql = "select * from tbMakeLog where cnvcMakeType='"+strMakeType+"' and cnnProduceSerialNo=" + strProduceSerialNo;
			DataTable dtMakeLog = Helper.Query(strSql);
			this.DataTableConvert(dtMakeLog, "cnvcGroup", "tbNameCode", "cnvcCode", "cnvcName", "cnvcType='GROUP'");
			this.DataTableConvert(dtMakeLog, "cnvcOperID", "tbLogin", "vcLoginID", "vcOperName", "");
			this.Datagrid2.DataSource = dtMakeLog;
			this.Datagrid2.DataBind();
		}

		private void BindGrid3()
		{
			string strProduceSerialNo = txtProduceSerialNo.Text;
			///string strGroup = ddlGroup.SelectedValue;
			string strMakeType = ViewState["MakeType"].ToString();

			string strSql = "select * from tbMakeLog where cnvcMakeType='"+strMakeType+"' and cnnProduceSerialNo=" + strProduceSerialNo;
			DataTable dtMakeLog = Helper.Query(strSql);
			this.DataTableConvert(dtMakeLog, "cnvcGroup", "tbNameCode", "cnvcCode", "cnvcName", "cnvcType='GROUP'");
			this.DataTableConvert(dtMakeLog, "cnvcOperID", "tbLogin", "vcLoginID", "vcOperName", "");
			this.Datagrid3.DataSource = dtMakeLog;
			this.Datagrid3.DataBind();
		}

		private void BindGrid4()
		{
			string strProduceSerialNo = txtProduceSerialNo.Text;
			
			string strSql = "select a.cnvcCode as cnvcProductCode,a.cnvcName as cnvcProductName,a.cnvcUnit, "
						+ " isnull(a.cnnCount,0) as cnnDStorageCount,isnull(b.cnnCount,0) as cnnStorageCount, "
						+ " isnull(b.cnnCount,0)-isnull(a.cnnCount,0) as cnnCount  from tbMakeDetail a "
						+ " left outer join tbStorage b on a.cnvcCode=b.cnvcProductCode "
						+ " where a.cnnMakeSerialNo in  "
						+ " ( "
						+ " select cnnMakeSerialNo from tbMakeLog "
						+ " where cnnProduceSerialNo="+strProduceSerialNo+" and cnvcGroup in('BZ','PL') "
						+ " )  and isnull(b.cnnCount,0)-isnull(a.cnnCount,0)<0";
			DataTable dtMakeDetail = Helper.Query(strSql);

			this.DataGrid4.DataSource = dtMakeDetail;
			this.DataGrid4.DataBind();
		}
		
		protected void Button1_Click(object sender, System.EventArgs e)
		{
			ViewState["MakeType"] = 1;
			BindGrid2();
			this.DataGrid1.Visible = false;
			this.Datagrid2.Visible = true;
			this.Datagrid3.Visible = false;
			this.DataGrid4.Visible = false;
		}

		protected void Button2_Click(object sender, System.EventArgs e)
		{
			ViewState["MakeType"] = 2;
			BindGrid3();
			this.DataGrid1.Visible = false;
			this.Datagrid2.Visible = false;
			this.Datagrid3.Visible = true;
			this.DataGrid4.Visible = false;
		}

		private void DataGrid1_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			this.DataGrid1.CurrentPageIndex = e.NewPageIndex;
			BindGrid();
		}

		private void Datagrid2_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			this.Datagrid2.CurrentPageIndex = e.NewPageIndex;
			BindGrid2();
		}

		private void Datagrid3_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			this.Datagrid3.CurrentPageIndex = e.NewPageIndex;
			BindGrid3();
		}

		protected void Button3_Click(object sender, System.EventArgs e)
		{
			BindGrid4();
			this.DataGrid1.Visible = false;
			this.Datagrid2.Visible = false;
			this.Datagrid3.Visible = false;
			this.DataGrid4.Visible = true;
		}

		private void DataGrid4_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			this.DataGrid4.CurrentPageIndex = e.NewPageIndex;
			BindGrid4();
		}
	}
}
