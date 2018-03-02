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
using BusiComm;
using CommCenter;

namespace AMSApp.paraconf
{
	/// <summary>
	/// wfmPackagesGoods 的摘要说明。
	/// </summary>
	public partial class wfmPackagesGoods : wfmBase
	{
	
		Manager m1;
		private const string PackagesGoodsSession="PackagesGoodsSession";
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
			if(!this.IsPostBack)
			{
				string strPackageId = Request["vcPackageId"];
				string strPackageName = Request["vcPackageName"];
				string strPackagePrice = Request["nPackagePrice"];

				this.lblPackageId.Text = strPackageId;
				this.lblPackageName.Text = strPackageName;
				this.lblPackagePrice.Text = strPackagePrice;
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
			this.DataGrid1.CancelCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGrid1_CancelCommand);
			this.DataGrid1.EditCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGrid1_EditCommand);
			this.DataGrid1.UpdateCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGrid1_UpdateCommand);

		}
		#endregion

		private void SetManager()
		{
			Hashtable htapp=(Hashtable)Application["appconf"];
			string strcons=(string)htapp["cons"];
			m1=new Manager(strcons);
		}
		private void BindGrid()
		{
			if(Session[PackagesGoodsSession]!=null)
			{
				DataTable dt = Session[PackagesGoodsSession] as DataTable;
				this.DataGrid1.DataSource = dt;
				this.DataGrid1.DataBind();
			}
		}
		protected void Button1_Click(object sender, System.EventArgs e)
		{
			//查询商品
			try
			{
				this.SetManager();
				string strPackageId = this.lblPackageId.Text;
				string strGoodsId = this.txtGoodsID.Text;
				string strGoodsName = this.txtGoodsName.Text;

				DataTable dt = m1.GetPackagesGoods(strPackageId,strGoodsId,strGoodsName);
				Session[PackagesGoodsSession] = dt;
				this.DataGrid1.CurrentPageIndex=0;
				this.DataGrid1.EditItemIndex = -1;
				BindGrid();
				
			}
			catch(Exception ex)
			{
				this.clog.WriteLine(ex);
				this.SetErrorMsgPageBydirHistory(ex.Message);
				return;
			}
		}

		private void DataGrid1_EditCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			this.DataGrid1.EditItemIndex = e.Item.ItemIndex;
			BindGrid();
		}

		private void DataGrid1_CancelCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			this.DataGrid1.EditItemIndex = -1;
			BindGrid();
		}

		protected void Button2_Click(object sender, System.EventArgs e)
		{
			//返回套餐管理
			this.RedirectPage("wfmPackages.aspx?vcPackageId="+this.lblPackageId.Text+"&vcPackageName="+this.lblPackageName.Text+"&nPackagePrice="+this.lblPackagePrice.Text);
		}

		private void DataGrid1_UpdateCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			//编辑
			//添加套餐
			try
			{
				string strGoodsId = e.Item.Cells[0].Text;
				string strGoodsName = e.Item.Cells[1].Text;

				string strGoodsPrice = ((TextBox)e.Item.Cells[3].Controls[0]).Text;				
				string strComments = ((TextBox)e.Item.Cells[4].Controls[0]).Text;
				PackagesStruct ps = new PackagesStruct();
				ps.strPackageId = this.lblPackageId.Text;
				ps.strPackageName = this.lblPackageName.Text;
				ps.dPackagePrice = Convert.ToDouble(this.lblPackagePrice.Text);

				ps.strGoodsId = strGoodsId;
				ps.strGoodsName = strGoodsName;

				ps.dGoodsPrice = Convert.ToDouble(strGoodsPrice);
				ps.strComments = strComments;

				this.SetManager();
				m1.AddPackage(ps);
				//this.SetSuccMsgPageBydir("添加成功","paraconf/wfmPackages.aspx?vcPackageId="+this.txtPackageId.Text+"&vcPackageName="+this.txtPacakgeName.Text+"&nPackagePrice="+this.txtPackagePrice.Text);
				this.Popup("添加成功");
				Button1_Click(null,null);
			}
			catch(Exception ex)
			{
				this.clog.WriteLine(ex);
				this.SetErrorMsgPageBydirHistory(ex.Message);
				return;
			}
		}
	}
}
