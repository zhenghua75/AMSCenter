using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
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
	/// wfmSelfProduce 的摘要说明。
	/// </summary>
	public partial class wfmSelfProduce : wfmBase
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
			if(!this.IsPostBack)
			{
				BindDept(ddlProduceDept, "");
				if(Request["OrderSerialNo"] == null)
				{
					this.Popup("无效链接");
					return;
				}
				string strOrderSerialNo = Request["OrderSerialNo"].ToString();
				DataTable dtOrderBook = Helper.Query("select * from tbOrderBook where cnnOrderSerialNo=" + strOrderSerialNo);
				if(dtOrderBook.Rows.Count == 0)
				{
					Popup("未找到订单");
					return;
				}
				OrderBook ob = new OrderBook(dtOrderBook);
				this.txtOrderSerialNo.Text = ob.cnnOrderSerialNo.ToString();
				ListItem li = this.ddlProduceDept.Items.FindByValue(ob.cnvcProduceDeptID);
				if(li != null)
				{
					li.Selected = true;
				}
				this.txtShipDate.Text = ob.cndShipDate.ToString("yyyy-MM-dd");

				this.ddlProduceDept.Enabled = false;
				this.txtOrderSerialNo.Enabled = false;
				this.txtShipDate.Enabled = false;
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
			this.DataGrid1.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.DataGrid1_ItemDataBound);
			this.DataGrid2.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DataGrid2_PageIndexChanged);

		}
		#endregion

		private void BindOrderDetail()
		{
			DataTable dtOrderDetail =
				Helper.Query("select * from tbOrderBookDetail where cnnOrderSerialNo=" + txtOrderSerialNo.Text);
			this.DataTableConvert(dtOrderDetail, "cnvcOperID", "tbLogin", "vcLoginID", "vcOperName", "");
			this.DataGrid1.DataSource = dtOrderDetail;			
			this.DataGrid1.DataBind();
		}
		protected void btnProductList_Click(object sender, System.EventArgs e)
		{
			this.DataGrid2.DataSource = null;
			this.DataGrid2.DataBind();
			BindOrderDetail();
		}

		private void DataGrid1_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			this.DataGrid1.CurrentPageIndex = e.NewPageIndex;
			BindOrderDetail();
		}

		protected decimal total;
		private void DataGrid1_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			if ((e.Item.ItemType == ListItemType.Item)||(e.Item.ItemType == ListItemType.AlternatingItem))
			{
				total+= Convert.ToDecimal(e.Item.Cells[5].Text.ToString());
			}
			if(e.Item.ItemType==ListItemType.Footer)
			{
				 e.Item.Cells[4].Text = "合计：";
				 e.Item.Cells[5].Text = total.ToString();
			}
		}
		private void BindStorage()
		{
			DataTable dtStorage = Helper.Query("select * from tbStorage where cnnCount>0 and cnvcStorageDeptID='" + ddlProduceDept.SelectedValue+"'");
			DataGrid2.DataSource = dtStorage;
			DataGrid2.DataBind();
		}

		private void DataGrid2_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			this.DataGrid2.CurrentPageIndex = e.NewPageIndex;
			BindStorage();
		}

		protected void btnStorage_Click(object sender, System.EventArgs e)
		{
			this.DataGrid1.DataSource = null;
			this.DataGrid1.DataBind();
			BindStorage();
		}

		protected void btnProduce_Click(object sender, System.EventArgs e)
		{
			try
			{
				OperLog operLog = new OperLog();
				operLog.cnvcOperID = oper.strLoginID;
				operLog.cnvcDeptID = oper.strDeptID;
				operLog.cnvcOperType = "门市自生产";

				ProduceFacade pf = new ProduceFacade();
				pf.SelfProduce(txtOrderSerialNo.Text, ddlProduceDept.SelectedValue, operLog);
				Popup("自生产完成，已更新库存量");
			}
			catch(Exception ex)
			{
				Popup(ex.Message);
			}
		}

		protected void btnReturn_Click(object sender, System.EventArgs e)
		{
			this.Response.Redirect("wfmSalesRoomProduce.aspx");
		}
	}
}
