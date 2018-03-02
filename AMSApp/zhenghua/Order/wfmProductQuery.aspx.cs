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
	/// wfmProductQuery 的摘要说明。
	/// </summary>
	public partial class wfmProductQuery : wfmBase
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
			if(!this.IsPostBack)
			{
				if(Session["ProductList"] != null)
				{
					DataTable dtOrderBookDetail = (DataTable) (Session["ProductList"]);
					if(dtOrderBookDetail.Rows.Count==0)
						this.lblSumText.Text = "";
				}
                this.BindNameCode(ddlProductType, "cnvcType = 'PRODUCTTYPE' ");
                ListItem li = new ListItem("所有", "%");
                this.ddlProductType.Items.Insert(0, li);
                BindProductClass(ddlProductClass,
                    "cnvcProductType in('SEMIPRODUCT','FINALPRODUCT','RAW','Pack') and cnvcProductType like '" +
                    ddlProductType.SelectedValue + "'");
                this.ddlProductClass.Items.Insert(0, li);

				Session["vwProduct"]=null;

			}

			this.FootBar.Visible = false;
			if(Session["vwProduct"]!=null)
			{
				if(((DataTable)Session["vwProduct"]).Rows.Count>0)
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
			this.DataGrid1.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGrid1_ItemCommand);
			this.DataGrid1.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DataGrid1_PageIndexChanged);
			this.DataGrid2.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGrid2_ItemCommand);
			this.DataGrid2.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DataGrid2_PageIndexChanged);
			this.DataGrid2.CancelCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGrid2_CancelCommand);
			this.DataGrid2.EditCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGrid2_EditCommand);
			this.DataGrid2.UpdateCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGrid2_UpdateCommand);
			this.DataGrid2.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.DataGrid2_ItemDataBound);

		}
		#endregion

		protected void btnCancel_Click(object sender, System.EventArgs e)
		{
			this.txtProductCode.Text = "";
			this.txtProductName.Text = "";
			this.txtCount.Text = "";
			this.DataGrid1.DataSource = null;
			this.DataGrid1.DataBind();
			this.DataGrid2.DataSource = null;
			this.DataGrid2.DataBind();
		}

		protected void btnQuery_Click(object sender, System.EventArgs e)
		{
			this.DataGrid1.CurrentPageIndex = 0;
			Query();
			BindProduct();
		}
        protected void ddlProductType_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            //
            BindProductClass(ddlProductClass,
                             "cnvcProductType in('SEMIPRODUCT','FINALPRODUCT','RAW','Pack') and cnvcProductType like '" +
                             ddlProductType.SelectedValue + "'");
            ListItem li = new ListItem("所有", "%");
            this.ddlProductClass.Items.Insert(0, li);
        }
		private void Query()
		{
			string strSql =
				"select a.cnvcProductTypeCode as cnvcProductType,a.cnvcProductType as cnvcProductTypeName,a.cnvcProductCode,a.cnvcProductName,cnvcProduct_Statd as cnvcProduct_Statd,a.cnvcUnit,b.nPrice as cnnPrice  from vwProduct a join vwGoods b on a.cnvcProductCode=b.vcGoodsID where a.cnvcProductCode like '%" +
				txtProductCode.Text + "%' and a.cnvcProductName like '%" + txtProductName.Text + "%' and a.cnvcProductTypeCode like '"+ddlProductType.SelectedValue+"'";

            strSql += " and isnull(cnvcProductClassCode,'') like '" + ddlProductClass.SelectedValue + "' ";
			DataTable dtProduct = Helper.Query(strSql);

			if(Session["ProductList"] != null)
			{
				DataTable dtProductList = (DataTable) Session["ProductList"];
				foreach(DataRow drProductList in dtProductList.Rows)
				{
					string strProductCode = drProductList["cnvcProductCode"].ToString();
					DataRow[] dr = dtProduct.Select("cnvcProductCode='" + strProductCode + "'");
					if(dr.Length > 0)
					{
						dtProduct.Rows.Remove(dr[0]);
					}
				}
			}
			Session["vwProduct"] = dtProduct;
		}
		private void BindProduct()
		{
			
			int iRecordCount = 0;
			if(Session["vwProduct"] !=null)
			{
				DataTable dtout = (DataTable)Session["vwProduct"];
				iRecordCount = dtout.Rows.Count;
				this.DataGrid1.DataSource = dtout;
				this.DataGrid1.DataBind();
				this.DataGrid1.Visible = true;
				this.DataGrid2.Visible = false;
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
			BindProduct();
		}

		private void DataGrid1_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			if(e.CommandName == "putin")
			{
				//放入清单
				DataTable dtProductList = null;
				if(Session["ProductList"] == null)
				{
					string strsql = "select  '' as cnvcProductTypeName,[cnnOrderSerialNo], [cnvcProductCode], [cnvcProductName], [cnnOrderCount], ''as cnvcProduct_Statd,[cnvcUnit], [cnnPrice], [cnnSum], [cnvcOperID], [cndOperDate], [cnnAssignCount] from tbOrderBookDetail where 1<>1";
					dtProductList = Helper.Query(strsql);
				}
				else
				{
					dtProductList = (DataTable) Session["ProductList"]; 
				}
				string strCount = ((TextBox) e.Item.Cells[6].Controls[1]).Text;

				if(this.JudgeIsNull(strCount,"数量"))
				{
					return;
				}
				if(!this.JudgeIsNum(strCount,"数量"))
				{
					return;
				}
				if(decimal.Parse(strCount) <= 0)
				{
					Popup("数量必需大于零");
					return;
				}

				DataRow drProductList = dtProductList.NewRow();
				drProductList["cnvcProductTypeName"] = e.Item.Cells[0].Text;
				drProductList["cndOperDate"] = DateTime.Now;
				decimal dOrderCount =decimal.Parse(((TextBox) e.Item.Cells[6].Controls[1]).Text);
				drProductList["cnnOrderCount"] = dOrderCount;
				decimal dPrice = decimal.Parse(e.Item.Cells[5].Text);
				drProductList["cnnPrice"] = dPrice;
				drProductList["cnvcProductCode"] = e.Item.Cells[1].Text;
				drProductList["cnvcProduct_Statd"] = e.Item.Cells[3].Text;
				drProductList["cnvcProductName"] = e.Item.Cells[2].Text;
				drProductList["cnvcUnit"] = e.Item.Cells[4].Text;
				drProductList["cnnSum"] = dPrice*dOrderCount;
				dtProductList.Rows.Add(drProductList);
				Session["ProductList"] = dtProductList;
				if((DataGrid1.CurrentPageIndex==DataGrid1.PageCount-1)&&DataGrid1.Items.Count==1)
				{
					if(DataGrid1.CurrentPageIndex-1>1)
					{
						DataGrid1.CurrentPageIndex = DataGrid1.CurrentPageIndex-1;
					}
					else
					{
						DataGrid1.CurrentPageIndex = 0;
					}
            
				} 
				BindProduct();
			}
		}

		protected void btnAddList_Click(object sender, System.EventArgs e)
		{
			if(this.JudgeIsNull(txtCount.Text,"数量"))
			{
				return;
			}
			if(!this.JudgeIsNum(txtCount.Text,"数量"))
			{
				return;
			}
			if(decimal.Parse(txtCount.Text) <= 0)
			{
				Popup("数量必需大于零");
				return;
			}
			if(this.DataGrid1.Items.Count > 0)
			{
				//放入清单
				DataTable dtProductList = null;
				if(Session["ProductList"] == null)
				{
					string strsql = "select  '' as cnvcProductTypeName,[cnnOrderSerialNo], [cnvcProductCode], [cnvcProductName], [cnnOrderCount], ''as cnvcProduct_Statd,[cnvcUnit], [cnnPrice], [cnnSum], [cnvcOperID], [cndOperDate], [cnnAssignCount] from tbOrderBookDetail where 1<>1";
					dtProductList = Helper.Query(strsql);
				}
				else
				{
					dtProductList = (DataTable) Session["ProductList"]; 
				}

				foreach(DataGridItem dgi in this.DataGrid1.Items)
				{

					DataRow drProductList = dtProductList.NewRow();
					drProductList["cnvcProductTypeName"] = dgi.Cells[0].Text;
					drProductList["cndOperDate"] = DateTime.Now;
					decimal dOrderCount =decimal.Parse(txtCount.Text);
					drProductList["cnnOrderCount"] = dOrderCount;
					decimal dPrice = decimal.Parse(dgi.Cells[4].Text);
					drProductList["cnnPrice"] = dPrice;
					drProductList["cnvcProduct_Statd"] = dgi.Cells[3].Text;
					drProductList["cnvcProductCode"] = dgi.Cells[1].Text;
					drProductList["cnvcProductName"] = dgi.Cells[2].Text;
					drProductList["cnvcUnit"] = dgi.Cells[3].Text;
					drProductList["cnnSum"] = dPrice*dOrderCount;
					dtProductList.Rows.Add(drProductList);
				}

				Session["ProductList"] = dtProductList;
				BindProduct();
				//btnQuery_Click(null, null);
			}
			BindGrid();
			this.DataGrid1.Visible = false;
			Session["vwProduct"]=null;
			BindProduct();
			this.DataGrid2.Visible = true;
		}

		protected void chkSame_CheckedChanged(object sender, System.EventArgs e)
		{
			//使用同期数据
			if(chkSame.Checked)
			{
				string strOrderBook = "select top 1 cnnOrderSerialNo from tbOrderBook where convert(char(10),cndOrderDate,120) = '" +
				                      DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd") + "'";
				DataTable dtOrderBook = Helper.Query(strOrderBook);
				if(dtOrderBook.Rows.Count > 0)
				{
					string strOrderSerialNo = dtOrderBook.Rows[0][0].ToString();
					if(strOrderSerialNo != "")
					{
						string strOrderBookDetail = "select b.cnvcProductType as cnvcProductTypeName,a.* from tbOrderBookDetail a "
+" left join vwProduct b on a.cnvcProductCode=b.cnvcProductCode where a.cnnOrderSerialNo=" + strOrderSerialNo;
						DataTable dtOrderBookDetail = Helper.Query(strOrderBookDetail);

						//DataTable dtProduct = Helper.Query(strSql);
						if(Session["ProductList"] != null)
						{
							DataTable dtProductList = (DataTable) Session["ProductList"];
							foreach(DataRow drOrderBookDetail in dtOrderBookDetail.Rows)
							{
								//OrderBookDetail detail = new OrderBookDetail(drOrderBookDetail);
								string strProductCode = drOrderBookDetail["cnvcProductCode"].ToString();
								DataRow[] dr = dtProductList.Select("cnvcProductCode='" + strProductCode + "'");
								if(dr.Length == 0)
								{
									dtProductList.Rows.Add(drOrderBookDetail.ItemArray);
								}
							}		
							Session["ProductList"] = dtProductList;
						}
						else
						{
							Session["ProductList"] = dtOrderBookDetail;
						}

					}
				}
			}
		}

		protected void btnPercent_Click(object sender, System.EventArgs e)
		{
			if(this.JudgeIsNull(txtPercent.Text,"百分比"))
			{
				return;
			}
			if(!this.JudgeIsNum(txtPercent.Text,"百分比"))
			{
				return;
			}
			if(Session["ProductList"] != null)
			{
				DataTable dtProductList = (DataTable) Session["ProductList"];
				foreach(DataRow drProductList in dtProductList.Rows)
				{
					
					OrderBookDetail detail = new OrderBookDetail(drProductList);
					detail.cnnOrderCount = Convert.ToDecimal(Math.Ceiling(Convert.ToDouble(detail.cnnOrderCount)*double.Parse(txtPercent.Text)/100));
					detail.cnnSum = Math.Round(detail.cnnOrderCount*detail.cnnPrice,2);
					drProductList["cnnOrderCount"] = detail.cnnOrderCount;
					drProductList["cnnSum"] = detail.cnnSum;
				}
				Session["ProductList"] = dtProductList;
			}
			BindGrid();
			this.DataGrid1.Visible = false;
			Session["vwProduct"]=null;
			BindProduct();
			this.DataGrid2.Visible = true;

		}

		private void BindGrid()
		{			
			if(Session["ProductList"] != null)
			{
				DataTable dtOrderBookDetail = (DataTable) (Session["ProductList"]);
				this.DataGrid2.DataSource = dtOrderBookDetail;
				this.DataGrid2.DataBind();

				if(dtOrderBookDetail.Rows.Count>0)
				{
					decimal dCount = 0;
					decimal dSum = 0;
					foreach(DataRow dr in dtOrderBookDetail.Rows)
					{
						dCount += Convert.ToDecimal(dr["cnnOrderCount"].ToString());
						dSum += Convert.ToDecimal(dr["cnnSum"].ToString());
					}
					string strSumText = "【合计】";
					strSumText += "数量："+dCount.ToString();//dtOrderBookDetail.Compute("sum(cnnOrderCount)","true").ToString();
					strSumText += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
					strSumText += "金额："+dSum.ToString();//dtOrderBookDetail.Compute("sum(cnnSum)","true").ToString();	
					this.lblSumText.Text = strSumText;
				}
			}
			else
			{
				Popup("未选择产品，请通过产品查询选择产品");
			}
		}

		private void DataGrid2_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			this.DataGrid2.CurrentPageIndex = e.NewPageIndex;
			BindGrid();
		}

		private void DataGrid2_CancelCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			this.DataGrid2.EditItemIndex = -1;
			BindGrid();
		}

		private void DataGrid2_EditCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			this.DataGrid2.EditItemIndex = e.Item.ItemIndex;
			this.BindGrid();
		}

		private void DataGrid2_UpdateCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			try
			{
				if(Session["ProductList"] == null)
				{
					Popup("请首先选择产品");
					return;
				}
				string strProductCode = e.Item.Cells[1].Text;				
				string strPrice = e.Item.Cells[4].Text;
				string strCount = ((TextBox) e.Item.Cells[5].Controls[0]).Text;
				decimal dSum = decimal.Parse(strPrice)*decimal.Parse(strCount);
				DataTable dtOrderBookDetail = (DataTable) Session["ProductList"];
				DataRow[] drOrderBookDetail = dtOrderBookDetail.Select("cnvcProductCode='" + strProductCode + "'");
				if(drOrderBookDetail.Length >0)
				{
					drOrderBookDetail[0]["cnnOrderCount"] = strCount;
					drOrderBookDetail[0]["cnnSum"] = dSum.ToString();
				}
				this.DataGrid2.EditItemIndex = -1;
				this.BindGrid();
			}
			catch(Exception ex)
			{
				Popup(ex.Message);
			}
		}

		private void DataGrid2_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			if(e.Item.ItemType == ListItemType.Item ||e.Item.ItemType == ListItemType.AlternatingItem)
			{								
				LinkButton btnDelete = (LinkButton)(e.Item.Cells[9].Controls[0]);
				btnDelete.Attributes.Add("onClick","JavaScript:return confirm('确定删除？')");
				e.Item.Attributes.Add("onMouseOver","this.style.backgroundColor='#FFCC66'");
				e.Item.Attributes.Add("onMouseOut","this.style.backgroundColor='#ffffff'");
			} 
		}

		private void DataGrid2_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			if(e.CommandName == "Delete")
			{
				string strProductCode = e.Item.Cells[1].Text;
				DataTable dtOrderBookDetail = (DataTable) Session["ProductList"];
				DataRow[] drOrderBookDetail = dtOrderBookDetail.Select("cnvcProductCode='" + strProductCode + "'");
				if(drOrderBookDetail.Length >0)
				{
					dtOrderBookDetail.Rows.Remove(drOrderBookDetail[0]);
				}
				this.BindGrid();
			}
		}

		protected void btnOrderDetail_Click(object sender, System.EventArgs e)
		{
			BindGrid();
			this.DataGrid1.Visible = false;
			Session["vwProduct"]=null;
			BindProduct();
			this.DataGrid2.Visible = true;
		}

		protected void btnBatchAddList_Click(object sender, System.EventArgs e)
		{
			//批量放入清单
			try
			{											
				if(this.DataGrid1.Items.Count > 0)
				{
					//放入清单
					DataTable dtProductList = null;
					if(Session["ProductList"] == null)
					{
						string strsql = "select  '' as cnvcProductTypeName,[cnnOrderSerialNo], [cnvcProductCode], [cnvcProductName], [cnnOrderCount], ''as cnvcProduct_Statd,[cnvcUnit], [cnnPrice], [cnnSum], [cnvcOperID], [cndOperDate], [cnnAssignCount] from tbOrderBookDetail where 1<>1";
						dtProductList = Helper.Query(strsql);
					}
					else
					{
						dtProductList = (DataTable) Session["ProductList"]; 
					}

					foreach(DataGridItem dgi in this.DataGrid1.Items)
					{
						string strCount = ((TextBox) dgi.Cells[6].Controls[1]).Text;
						if(this.JudgeIsNull(strCount))
						{
							continue;
						}
						if(!this.JudgeIsNum(strCount,"数量"))
						{
							return;
						}
						if(decimal.Parse(strCount) <= 0)
						{
							Popup("数量必需大于零");
							return;
						}
//						OrderBookDetail productList = new OrderBookDetail();
//						productList.cndOperDate = DateTime.Now;
//						productList.cnnOrderCount = decimal.Parse(strCount);
//						productList.cnnPrice = decimal.Parse(dgi.Cells[3].Text);
//						productList.cnvcProductCode = dgi.Cells[0].Text;
//						productList.cnvcProductName = dgi.Cells[1].Text;
//						productList.cnvcUnit = dgi.Cells[2].Text;
//						productList.cnnSum = productList.cnnPrice*productList.cnnOrderCount;
//						object[] oArray = new object[dtProductList.Columns.Count];
//						productList.ToRow().ItemArray.CopyTo(oArray, 0);
//						dtProductList.Rows.Add(oArray);

						DataRow drProductList = dtProductList.NewRow();
						drProductList["cnvcProductTypeName"] = dgi.Cells[0].Text;
						drProductList["cndOperDate"] = DateTime.Now;
						decimal dOrderCount =decimal.Parse(((TextBox) dgi.Cells[6].Controls[1]).Text);
						drProductList["cnnOrderCount"] = dOrderCount;
						decimal dPrice = decimal.Parse(dgi.Cells[5].Text);
						drProductList["cnnPrice"] = dPrice;
						drProductList["cnvcProductCode"] = dgi.Cells[1].Text;
						drProductList["cnvcProductName"] = dgi.Cells[2].Text;
						drProductList["cnvcProduct_Statd"] = dgi.Cells[3].Text;
						drProductList["cnvcUnit"] = dgi.Cells[4].Text;
						drProductList["cnnSum"] = dPrice*dOrderCount;
						dtProductList.Rows.Add(drProductList);
					}

					Session["ProductList"] = dtProductList;
					BindProduct();
					//btnQuery_Click(null, null);
				}
				if((DataGrid1.CurrentPageIndex==DataGrid1.PageCount-1)&&DataGrid1.Items.Count==1)
				{
					if(DataGrid1.CurrentPageIndex-1>1)
					{
						DataGrid1.CurrentPageIndex = DataGrid1.CurrentPageIndex-1;
					}
					else
					{
						DataGrid1.CurrentPageIndex = 0;
					}
            
				} 
				BindGrid();
				this.DataGrid1.Visible = false;
				Session["vwProduct"]=null;
				BindProduct();
				this.DataGrid2.Visible = true;
			}
			catch(Exception ex)
			{
				Popup(ex.Message);
			}
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
			BindProduct();
		}
		public void ShowPageLabel(Label myLable,int iRecordCount) 
		{
			myLable.Text = "第 " + (DataGrid1.CurrentPageIndex+1) +" 页/共 " + DataGrid1.PageCount+" 页，共"+iRecordCount+"条记录";
		}
	}
}
