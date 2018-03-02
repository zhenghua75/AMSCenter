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

namespace AMSApp.zhenghua.Order
{
	/// <summary>
	/// wfmOrderQuery 的摘要说明。
	/// </summary>
	public partial class wfmOrderQuery : wfmBase
	{
		protected System.Web.UI.WebControls.TextBox TextBox3;	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if(!this.IsPostBack)
			{
				BindDept(ddlSalesRoom, "cnvcDeptType='SalesRoom'");
				BindDept(ddlProduceDept, "cnvcDeptType<>'Corp'");
				BindNameCode(ddlOrderType, "cnvcType='ORDERTYPE'");
				BindNameCode(ddlOrderState, "cnvcType='ORDERSTATE'");
				BindOper(ddlOrderOper, "");
				txtArrivedDate.Attributes.Add("onclick", "setDayHM(this);");
				this.tblCustom.Visible = false;
				ListItem li = new ListItem("所有", "%");
				this.ddlOrderOper.Items.Insert(0, li);
				this.ddlOrderState.Items.Insert(0, li);
				this.ddlOrderType.Items.Insert(0, li);
				this.ddlProduceDept.Items.Insert(0, li);
				this.ddlSalesRoom.Items.Insert(0, li);
				Session["tbOrderBook"] =null;
			}

			this.FootBar.Visible = false;
			if(Session["tbOrderBook"]!=null)
			{
				if(((DataTable)Session["tbOrderBook"]).Rows.Count>0)
				{
					this.FootBar.Visible = true;
				}

			}
			if(DataGrid1.DataSource!=null)
			{
				if(((DataTable)DataGrid1.DataSource).Rows.Count>0)
				{
					this.FootBar.Visible = true;
				}
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

		}
		#endregion

		private void Query()
		{
			string strSql =
				" select cnnOrderSerialNo,cnvcOrderState,cndShipDate,cndOrderDate,cnvcOrderOperID,cnvcOrderDeptID,cnvcProduceDeptID,cnvcOrderType,cnvcOrderState "
				//"b.cnvcDeptName as cnvcOrderDeptIDComments,c.cnvcDeptName as cnvcProduceDeptIDComments, "
				// + " d.cnvcName as cnvcOrderTypeComments, "
				// + " e.vcLoginID as cnvcOrderOperIDComments, "
				// + " f.cnvcName as cnvcOrderStateComments "
				+ " from tbOrderBook "
				//+ " left outer join tbDept b on a.cnvcOrderDeptID=b.cnvcDeptID "
				// + " left outer join tbDept c on a.cnvcProduceDeptID=c.cnvcDeptID "
				// +
				// " left outer join (select * from tbNameCode where cnvcType='ORDERTYPE') d on a.cnvcOrderType=d.cnvcCode "
				// + " left outer join tbLogin e on a.cnvcOrderOperID=e.vcLoginID  "
				// + " left outer join (select * from tbNameCode where cnvcType='ORDERSTATE') f on a.cnvcOrderState=f.cnvcCode"
				+ " where 1=1 ";
			strSql += " and cnvcOrderOperID like '"+ddlOrderOper.SelectedValue+"'";
			strSql += " and cnvcOrderState like '" + ddlOrderState.SelectedValue + "'";
			strSql += " and cnvcOrderType like '" + ddlOrderType.SelectedValue + "'";
			strSql += " and cnvcProduceDeptID like '" + ddlProduceDept.SelectedValue + "'";
			strSql += " and cnvcOrderDeptID like '" + ddlSalesRoom.SelectedValue + "'";
			if(txtOrderSerialNo.Text.Length > 0)
			{
				strSql += " and cnnOrderSerialNo = " + txtOrderSerialNo.Text;
			}
			if(txtOrderDate.Text.Length > 0)
			{
				strSql += " and convert(char(10),cndOrderDate,120)='" + txtOrderDate.Text + "'";
			}
			if(txtShipDate.Text.Length > 0)
			{
				strSql += " and convert(char(10),cndShipDate,120)='" + txtShipDate.Text + "'";
			}
			if(ddlOrderType.SelectedValue == "WDO")
			{
				if(txtCustomName.Text.Length > 0)
				{
					strSql += " and cnvcCustomName like '%" + txtCustomName.Text + "%'";
				}
				if(txtShipAddress.Text.Length > 0)
				{
					strSql += " and cnvcShipAddress like '%" + txtShipAddress.Text + "%'";
				}
				if(txtLinkPhone.Text.Length > 0)
				{
					strSql += " and cnvcLinkPhone like '%" + txtLinkPhone.Text + "%'";
				}
				if(txtArrivedDate.Text.Length > 0)
				{
					strSql += " and cndArrivedDate <= '" + txtArrivedDate.Text + "'";
				}
			}
			DataTable dtOrderBook = Helper.Query(strSql);
			this.DataTableConvert(dtOrderBook, "cnvcOrderOperID", "tbLogin", "vcLoginID", "vcOperName", "");
			this.DataTableConvert(dtOrderBook, "cnvcOrderType", "tbNameCode", "cnvcCode", "cnvcName", "cnvcType='ORDERTYPE'");
			this.DataTableConvert(dtOrderBook, "cnvcOrderState", "tbNameCode", "cnvcCode", "cnvcName", "cnvcType='ORDERSTATE'");
			this.DataTableConvert(dtOrderBook, "cnvcOrderDeptID", "tbDept", "cnvcDeptID", "cnvcDeptName", "");
			this.DataTableConvert(dtOrderBook, "cnvcProduceDeptID", "tbDept", "cnvcDeptID", "cnvcDeptName", "");
			
			//this.DataGrid1.DataSource = dtOrderBook;
			//this.DataGrid1.DataBind();
			Session["tbOrderBook"] = dtOrderBook;
			string strReport = "select a.*,b.cnvcProductCode,b.cnvcProductName,b.cnnOrderCount,b.cnvcProduct_Statd,b.cnvcUnit,"
				+"b.cnnPrice,b.cnnSum from tbOrderBook a "
				//							+"left outer join tbOrderBookDetail b  "
				+"left outer join (select d.cnnOrderSerialNo,'' as cnvcType,null as cnnOperSerialNo,null as cnvcOperType,d.cnvcProductCode,d.cnvcProductName,c.cnvcProduct_Statd,d.cnvcUnit,d.cnnPrice,d.cnnOrderCount as cnnOrderCount,d.cnnSum,d.cnvcOperID,d.cndOperDate from tbOrderBookDetail d  left outer join(select cnvcMaterialCode as cnvcProductCode,convert(varchar(10),cnnStatdardCount)  + cnvcStandardUnit +'/'+ cnvcUnit as cnvcProduct_Statd,cnvcUnit from tbMaterial  union all select cnvcProductCode as cnvcProductCode,cnvcUnit as cnvcProduct_Statd,cnvcUnit from tbFormula) c on d.cnvcProductCode=c.cnvcProductCode ) b"
				+" on a.cnnOrderSerialNo=b.cnnOrderSerialNo"
				+ " where 1=1 ";

			strReport += " and a.cnvcOrderOperID like '"+ddlOrderOper.SelectedValue+"'";
			strReport += " and a.cnvcOrderState like '" + ddlOrderState.SelectedValue + "'";
			strReport += " and a.cnvcOrderType like '" + ddlOrderType.SelectedValue + "'";
			strReport += " and a.cnvcProduceDeptID like '" + ddlProduceDept.SelectedValue + "'";
			strReport += " and a.cnvcOrderDeptID like '" + ddlSalesRoom.SelectedValue + "'";
			if(txtOrderSerialNo.Text.Length > 0)
			{
				strReport += " and a.cnnOrderSerialNo = " + txtOrderSerialNo.Text;
			}
			if(txtOrderDate.Text.Length > 0)
			{
				strReport += " and convert(char(10),a.cndOrderDate,120)='" + txtOrderDate.Text + "'";
			}
			if(txtShipDate.Text.Length > 0)
			{
				strReport += " and convert(char(10),a.cndShipDate,120)='" + txtShipDate.Text + "'";
			}
			if(ddlOrderType.SelectedValue == "WDO")
			{
				if(txtCustomName.Text.Length > 0)
				{
					strReport += " and a.cnvcCustomName like '%" + txtCustomName.Text + "%'";
				}
				if(txtShipAddress.Text.Length > 0)
				{
					strReport += " and a.cnvcShipAddress like '%" + txtShipAddress.Text + "%'";
				}
				if(txtLinkPhone.Text.Length > 0)
				{
					strReport += " and a.cnvcLinkPhone like '%" + txtLinkPhone.Text + "%'";
				}
				if(txtArrivedDate.Text.Length > 0)
				{
					strReport += " and a.cndArrivedDate <= '" + txtArrivedDate.Text + "'";
				}
			}
			Session["OrderReport"] = strReport;

			string strSumReport = "select a.cnvcOrderDeptID,b.cnvcProductCode,b.cnvcProductName,b.cnvcProduct_Statd,b.cnvcUnit,b.cnnPrice,"
				+ " sum(b.cnnOrderCount) as cnnCount,sum(b.cnnSum) as cnnSum  from tbOrderBook a "
				+"left outer join (select d.cnnOrderSerialNo,'' as cnvcType,null as cnnOperSerialNo,null as cnvcOperType,d.cnvcProductCode,d.cnvcProductName,c.cnvcProduct_Statd,d.cnvcUnit,d.cnnPrice,d.cnnOrderCount as cnnOrderCount,d.cnnSum,d.cnvcOperID,d.cndOperDate from tbOrderBookDetail d  left outer join(select cnvcMaterialCode as cnvcProductCode,convert(varchar(10),cnnStatdardCount)  + cnvcStandardUnit +'/'+ cnvcUnit as cnvcProduct_Statd,cnvcUnit from tbMaterial  union all select cnvcProductCode as cnvcProductCode,cnvcUnit as cnvcProduct_Statd,cnvcUnit from tbFormula) c on d.cnvcProductCode=c.cnvcProductCode )  b on a.cnnOrderSerialNo=b.cnnOrderSerialNo "
				+ " where 1=1 ";
			strSumReport += " and a.cnvcOrderOperID like '"+ddlOrderOper.SelectedValue+"'";
			strSumReport += " and a.cnvcOrderState like '" + ddlOrderState.SelectedValue + "'";
			strSumReport += " and a.cnvcOrderType like '" + ddlOrderType.SelectedValue + "'";
			strSumReport += " and a.cnvcProduceDeptID like '" + ddlProduceDept.SelectedValue + "'";
			strSumReport += " and a.cnvcOrderDeptID like '" + ddlSalesRoom.SelectedValue + "'";
			if(txtOrderSerialNo.Text.Length > 0)
			{
				strSumReport += " and a.cnnOrderSerialNo = " + txtOrderSerialNo.Text;
			}
			if(txtOrderDate.Text.Length > 0)
			{
				strSumReport += " and convert(char(10),a.cndOrderDate,120)='" + txtOrderDate.Text + "'";
			}
			if(txtShipDate.Text.Length > 0)
			{
				strSumReport += " and convert(char(10),a.cndShipDate,120)='" + txtShipDate.Text + "'";
			}
			if(ddlOrderType.SelectedValue == "WDO")
			{
				if(txtCustomName.Text.Length > 0)
				{
					strSumReport += " and a.cnvcCustomName like '%" + txtCustomName.Text + "%'";
				}
				if(txtShipAddress.Text.Length > 0)
				{
					strSumReport += " and a.cnvcShipAddress like '%" + txtShipAddress.Text + "%'";
				}
				if(txtLinkPhone.Text.Length > 0)
				{
					strSumReport += " and a.cnvcLinkPhone like '%" + txtLinkPhone.Text + "%'";
				}
				if(txtArrivedDate.Text.Length > 0)
				{
					strSumReport += " and a.cndArrivedDate <= '" + txtArrivedDate.Text + "'";
				}
			}
			strSumReport += " group by a.cnvcOrderDeptID,b.cnvcProductCode,b.cnvcProductName,b.cnvcUnit,b.cnvcProduct_Statd,b.cnnPrice";
			Session["OrderSumReport"] = strSumReport;
		}
		private void BindGrid1()
		{
			int iRecordCount = 0;
			if(Session["tbOrderBook"] !=null)
			{
				DataTable dtout = (DataTable)Session["tbOrderBook"];
				iRecordCount = dtout.Rows.Count;
				this.DataGrid1.DataSource = dtout;
				this.DataGrid1.DataBind();
			}
			if(iRecordCount>0)
			{
				FootBar.Visible = true;
			}
			else
			{
				FootBar.Visible = false;
			}		
			ShowPageLabel(lbPageLabel,iRecordCount);	
		}
		private void DataGrid1_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			this.DataGrid1.CurrentPageIndex = e.NewPageIndex;
			BindGrid1();
		}



		protected void btnQuery_Click(object sender, System.EventArgs e)
		{
			this.DataGrid1.CurrentPageIndex = 0;
			Query();
			BindGrid1();
		}

		private void ddlOrderType_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(ddlOrderType.SelectedValue == "WDO")
			{
				this.tblCustom.Visible = true;
			}
			else
			{
				this.tblCustom.Visible = false;
			}
		}

		private void DataGrid1_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			if(e.Item.ItemType == ListItemType.Item ||e.Item.ItemType == ListItemType.AlternatingItem)
			{								
				//LinkButton btnDelete = (LinkButton)(e.Item.Cells[8].Controls[0]);
				//btnDelete.Attributes.Add("onClick","JavaScript:return confirm('确定删除？')");

				if(e.Item.Cells[12].Text != "0")
				{
					HyperLink btnEdit = (HyperLink)(e.Item.Cells[9].Controls[0]);
					btnEdit.Attributes.Add("onClick","JavaScript:alert('不可编辑');return false;");
				}
				
				if(e.Item.Cells[12].Text != "1")
				{
					HyperLink btnOrderAdd = (HyperLink)(e.Item.Cells[10].Controls[1]);
					btnOrderAdd.Attributes.Add("onClick","JavaScript:alert('不可加单');return false;");

					HyperLink btnOrderReduce = (HyperLink)(e.Item.Cells[11].Controls[1]);
					btnOrderReduce.Attributes.Add("onClick","JavaScript:alert('不可减单');return false;");
				}							
			} 
		}

		protected void ddlOrderType_SelectedIndexChanged_1(object sender, System.EventArgs e)
		{
			if(ddlOrderType.SelectedValue == "WDO")
			{
				this.tblCustom.Visible = true;
			}
			else
			{
				this.tblCustom.Visible = false;
			}
		}

		protected void Button2_Click(object sender, System.EventArgs e)
		{
			txtOrderSerialNo.Text = "";
			this.txtArrivedDate.Text = "";
			this.txtCustomName.Text = "";
			this.txtLinkPhone.Text = "";
			this.txtOrderDate.Text = "";
			this.txtOrderSerialNo.Text = "";
			this.txtShipAddress.Text = "";
			this.txtShipDate.Text = "";
		}

		protected void btnPrint_Click(object sender, System.EventArgs e)
		{
			if(Session["OrderReport"] == null)
			{
				Popup("请先查询结果");
				return;
			}
			this.Response.Redirect("wfmOrderReport.aspx?OrderType="+this.ddlOrderType.SelectedItem.Text);
		}

		protected void btnSumPrint_Click(object sender, System.EventArgs e)
		{
			if(Session["OrderSumReport"] == null)
			{
				Popup("请先查询结果");
				return;
			}
			this.Response.Redirect("wfmOrderSumReport.aspx");
		}
		protected void SetDataGridCurrentPageIndex(DataGrid myDataGrid,string strArg)
		{
			switch(strArg)
			{
				case ("next"):
					if (DataGrid1.CurrentPageIndex < (myDataGrid.PageCount - 1))
						DataGrid1.CurrentPageIndex ++;
					break;
				case ("prev"):
					if (DataGrid1.CurrentPageIndex > 0)
						DataGrid1.CurrentPageIndex --;
					break;
				case ("last"):
					DataGrid1.CurrentPageIndex = (myDataGrid.PageCount - 1);
					break;
				case ("jump"):
					int iTempIndex = Convert.ToInt16(Request["page_number"])-1;//PageNumber.Value)-1;
					if(iTempIndex > DataGrid1.PageCount-1)
						iTempIndex = DataGrid1.PageCount-1;
					if(iTempIndex < 0)
						iTempIndex = 0;
					DataGrid1.CurrentPageIndex = iTempIndex;
					break;
				default:
					//page number
					DataGrid1.CurrentPageIndex = Convert.ToInt32(strArg);
					break;
			}			
		}	
		protected void PagerButtonClick(Object sender, EventArgs e) 
		{
			//used by external paging UI
			String arg = ((LinkButton)sender).CommandArgument;
			SetDataGridCurrentPageIndex(DataGrid1,arg);
			BindGrid1();
		}
		public void ShowPageLabel(Label myLable,int iRecordCount) 
		{
			myLable.Text = "第 " + (DataGrid1.CurrentPageIndex+1) +" 页/共 " + DataGrid1.PageCount+" 页，共"+iRecordCount+"条记录";
		}
	}
}
