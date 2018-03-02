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

namespace AMSApp.zhenghua.Produce
{
	/// <summary>
	/// wfmProducePlanQuery ��ժҪ˵����
	/// </summary>
	public partial class wfmProducePlanQuery : wfmBase
	{
		protected System.Web.UI.WebControls.Label Label4;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// �ڴ˴������û������Գ�ʼ��ҳ��
			if(!this.IsPostBack)
			{
				this.BindDept(ddlProduceDept, "cnvcDeptType <>'Corp'");
				ListItem li = new ListItem("����", "%");
				ddlProduceDept.Items.Insert(0, li);
			}
		}

		#region Web ������������ɵĴ���
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: �õ����� ASP.NET Web ���������������ġ�
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// �����֧������ķ��� - ��Ҫʹ�ô���༭���޸�
		/// �˷��������ݡ�
		/// </summary>
		private void InitializeComponent()
		{    
			this.DataGrid1.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DataGrid1_PageIndexChanged);
			this.DataGrid1.CancelCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGrid1_CancelCommand);
			this.DataGrid1.EditCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGrid1_EditCommand);
			this.DataGrid1.UpdateCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGrid1_UpdateCommand);

		}
		#endregion

		protected void btnAdd_Click(object sender, System.EventArgs e)
		{
			this.Response.Redirect("wfmProducePlan.aspx");
		}

		protected void btnCancel_Click(object sender, System.EventArgs e)
		{
			this.txtProduceBeginDate.Text = "";
			this.txtProduceEndDate.Text = "";
		}

		protected void btnQuery_Click(object sender, System.EventArgs e)
		{
			this.DataGrid1.CurrentPageIndex = 0;
			BindGrid();
		}

		private void BindGrid()
		{
			string strSql = "select * from tbProduceLog where cnvcProduceState<>'3'";
//			string strSql = "select a.*,b.cnvcDeptName as cnvcProduceDeptIDComments,c.cnvcName as cnvcProduceStateComments,d.vcOperName as cnvcOperIDComments  from tbProduceLog a ";
//			strSql += " left outer join tbDept b on a.cnvcProduceDeptID=b.cnvcDeptID ";
//			strSql += " left outer join (select * from tbNameCode where cnvcType='PRODUCESTATE') c on a.cnvcProduceState=c.cnvcCode ";
//			strSql += " left outer join tbLogin d on a.cnvcOperID=d.vcLoginID";
			//strSql += " where a.cnvcProduceState<>'3' ";
			if(txtProduceBeginDate.Text.Trim().Length > 0)
			{
				strSql += " and cndProduceDate >='" + txtProduceBeginDate.Text + "'";
			}
			if(txtProduceEndDate.Text.Trim().Length > 0)
			{
				strSql += " and cndProduceDate <='" + txtProduceEndDate.Text + "'";
			}
			strSql += " and cnvcProduceDeptID like '"+ddlProduceDept.SelectedValue+"'";
			DataTable dtProduceLog = Helper.Query(strSql);
			this.DataTableConvert(dtProduceLog, "cnvcProduceDeptID", "tbDept", "cnvcDeptID", "cnvcDeptName", "");
			this.DataTableConvert(dtProduceLog, "cnvcProduceState", "tbNameCode", "cnvcCode", "cnvcName",
			                      "cnvcType='PRODUCESTATE'");
			this.DataTableConvert(dtProduceLog, "cnvcOperID", "tbLogin", "vcLoginID", "vcOperName", "");
			this.DataGrid1.DataSource = dtProduceLog;
			this.DataGrid1.DataBind();
		}

		private void DataGrid1_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			this.DataGrid1.CurrentPageIndex = e.NewPageIndex;
			BindGrid();
		}

		private void DataGrid1_CancelCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			this.DataGrid1.EditItemIndex = -1;
			BindGrid();
		}

		private void DataGrid1_EditCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			this.DataGrid1.EditItemIndex = e.Item.ItemIndex;
			BindGrid();
		}

		private void DataGrid1_UpdateCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{

			this.DataGrid1.EditItemIndex = e.Item.ItemIndex;
			BindGrid();
		}
	}
}
