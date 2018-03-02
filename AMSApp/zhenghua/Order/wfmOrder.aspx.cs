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
	/// wfmOrder 的摘要说明。
	/// </summary>
	public partial class wfmOrder : wfmBase
	{
		protected System.Web.UI.WebControls.TextBox TextBox1;
		protected System.Web.UI.WebControls.TextBox TextBox3;
		protected System.Web.UI.WebControls.TextBox TextBox4;
		protected System.Web.UI.WebControls.TextBox TextBox5;
		protected System.Web.UI.WebControls.TextBox TextBox6;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			//
			// 在此处放置用户代码以初始化页面
			BindDisp();
			if(!this.IsPostBack)
			{
				this.BindDept(ddlSalesRoom, "cnvcDeptType = 'SalesRoom'");

				CommCenter.CMSMStruct.LoginStruct ls1=(CommCenter.CMSMStruct.LoginStruct)Session["Login"];
				if(ls1 != null)
				{
					if(ls1.strDeptID!="CEN00"&&ls1.strLimit!="CL001")
					{
						ListItem li = this.ddlSalesRoom.Items.FindByValue(ls1.strNewDeptID);
						if(li != null)
							li.Selected = true;
						this.ddlSalesRoom.Enabled=false;
					}

				}
				this.BindDept(ddlProduceDept, "cnvcDeptType ='Factory'");
				this.BindNameCode(ddlOrderType, "cnvcType='ORDERTYPE'");
				txtArrivedDate.Attributes.Add("onclick", "setDayHM(this);");
				this.tblCustom.Visible = false;
				this.txtOrderComments.Text = "无特殊要求";
				if(Session["ProductList"] != null)
				{
					DataTable dtOrderBookDetail = (DataTable) Session["ProductList"];
					this.DataGrid1.DataSource = dtOrderBookDetail;
					this.DataGrid1.DataBind();

					if(dtOrderBookDetail.Rows.Count==0)
						this.lblSumText.Text = "";

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
			}
			
		}
		private void BindDisp()
		{
			if(Session["ProductList"] == null)
			{
				this.lblTitle.Text = "下单【未选择产品，请通过产品查询选择产品】";
				this.tblCustom.Visible = false;
				this.tblDetial.Visible = false;
				this.tblOper.Visible = false;
				this.tblOrder.Visible = false;
			}
			else
			{
				DataTable dtOrderBookDetail = (DataTable) Session["ProductList"];
				if(dtOrderBookDetail.Rows.Count > 0)
				{
					this.lblTitle.Text = "下单";
					if(ddlOrderType.SelectedValue == "WDO")
					{
						this.tblCustom.Visible = true;
					}
					else
					{
						this.tblCustom.Visible = false;
					}
					this.tblDetial.Visible = true;
					this.tblOper.Visible = true;
					this.tblOrder.Visible = true;
				}
				else
				{
					this.lblTitle.Text = "下单【未选择产品，请通过产品查询选择产品】";
					this.tblCustom.Visible = false;
					this.tblDetial.Visible = false;
					this.tblOper.Visible = false;
					this.tblOrder.Visible = false;
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

		}
		#endregion

		protected void ddlOrderType_SelectedIndexChanged(object sender, System.EventArgs e)
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

		protected void btnOK_Click(object sender, System.EventArgs e)
		{
			//下单
			try
			{                
				if(txtShipDate.Text.Length == 0)
				{
					throw new Exception("请输入发货日期");					
				}
				OrderBook orderBook = new OrderBook();
				if(ddlOrderType.SelectedValue == "WDO")
				{
					if(txtArrivedDate.Text.Length == 0)
					{
						throw new Exception("请输入要求到货时间");						
					}
					orderBook.cndArrivedDate = DateTime.Parse(txtArrivedDate.Text);
					orderBook.cnvcCustomName = txtCustomName.Text;
					orderBook.cnvcLinkPhone = txtLinkPhone.Text;
					orderBook.cnvcShipAddress = txtShipAddress.Text;
				}
				if(ddlOrderType.SelectedValue == "SELFPRODUCE")
				{
					if(!ddlSalesRoom.SelectedValue.Equals(ddlProduceDept.SelectedValue))
						throw new Exception("门市自生产下单门市必需和生产单位一致！");
				}
				orderBook.cndShipDate = DateTime.Parse(txtShipDate.Text);
				orderBook.cnvcOrderDeptID = ddlSalesRoom.SelectedValue;
				orderBook.cnvcOrderState = "0";
				orderBook.cnvcOrderType = ddlOrderType.SelectedValue;
				orderBook.cnvcProduceDeptID = ddlProduceDept.SelectedValue;
				orderBook.cnvcOrderComments = txtOrderComments.Text;
				DataTable dtDetail = (DataTable) Session["ProductList"];

				Hashtable htDetail = new Hashtable();
				foreach(DataRow dr in dtDetail.Rows)
				{
					string strProductCode = dr["cnvcProductCode"].ToString();
					string strProductName = dr["cnvcProductName"].ToString();
					string strsql = "select cnvcProductTypeCode as cnvcProductType from vwProduct where cnvcProductCode='"+strProductCode+"'";
					DataTable dtProductType = Helper.Query(strsql);
					if(dtProductType==null || dtProductType.Rows.Count==0) throw new Exception("获取"+strProductName+"的产品类型错误");
					string strProductType = dtProductType.Rows[0]["cnvcProductType"].ToString();
					if(strProductType=="Raw" || strProductType=="Pack")
					{						
						if(ddlOrderType.SelectedValue != "MATERIAL")
							throw new Exception(strProductCode+"-"+strProductName+"为原材料，订单类型请选择原材料订单");
					}
					if(strProductType=="SEMIPRODUCT" || strProductType=="FINALPRODUCT")
					{						
						if(ddlOrderType.SelectedValue == "MATERIAL")
							throw new Exception(strProductCode+"-"+strProductName+"不是原材料，订单类型不能选择原材料订单");
					}
					if(htDetail.ContainsKey(strProductCode))
					{
						throw new Exception(strProductCode+"-"+strProductName+"重复，请首先通过产品查询或者订单细节进行修改");
					}
					else
					{
						htDetail.Add(strProductCode,strProductName);
					}
				}

				OperLog operLog = new OperLog();
				operLog.cnvcOperID = oper.strLoginID;
				operLog.cnvcDeptID = oper.strDeptID;
				operLog.cnvcOperType = "下单";

				OrderFacade order = new OrderFacade();
				order.AddOrder(orderBook, dtDetail, operLog);
				Session["ProductList"] = null;
				btnCancel_Click(null, null);
				this.DataGrid1.DataSource = null;
				this.DataGrid1.DataBind();
				Popup("下单成功");			
				BindDisp();
			}
			catch(Exception ex)
			{
                AMSApp.zhenghua.Common.LogAdapter.WriteInterfaceException(ex);
				Popup(ex.Message);
			}
		}

		protected void btnCancel_Click(object sender, System.EventArgs e)
		{
			//取消
			this.txtShipDate.Text = "";
			this.txtCustomName.Text = "";
			this.txtShipAddress.Text = "";
			this.txtLinkPhone.Text = "";
			this.txtArrivedDate.Text = "";
			if(ddlOrderType.SelectedValue == "WDO")
			{
				this.tblCustom.Visible = true;
			}
			else
			{
				this.tblCustom.Visible = false;
			}
		}

		private void DataGrid1_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			this.DataGrid1.CurrentPageIndex = e.NewPageIndex;
			if(Session["ProductList"] != null)
			{
				DataTable dtOrderBookDetail = (DataTable) Session["ProductList"];
				this.DataGrid1.DataSource = dtOrderBookDetail;
				this.DataGrid1.DataBind();
			}
		}

	}
}
