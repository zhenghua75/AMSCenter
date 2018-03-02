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
	/// wfmOrderReduce ��ժҪ˵����
	/// </summary>
	public partial class wfmOrderReduce : wfmBase
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// �ڴ˴������û������Գ�ʼ��ҳ��
			if(Session["ProductList"] == null)
			{
				btnOK.Visible = false;
			}
			else
			{
				DataTable dtOrderBookDetail = (DataTable) Session["ProductList"];
				if(dtOrderBookDetail.Rows.Count > 0)
				{
					btnOK.Visible = true;
				}
				else
				{
					btnOK.Visible = false;
				}
				
			}
			if(!this.IsPostBack)
			{
//				string strReduceType = "select * from tbCommCode where vcCommSign='RTYPE'";
//				DataTable dtReduceType = Helper.Query(strReduceType);
//
//
//				this.ddlReduceType.DataSource = dtReduceType;
//				this.ddlReduceType.DataValueField = "vcCommCode";
//				this.ddlReduceType.DataTextField = "vcCommName";
//				this.ddlReduceType.DataBind();

				BindNameCode(ddlReduceType, "cnvcType='ORDEROPERTYPE'");


				if(Session["ProductList"] != null)
				{
					DataTable dtOrderBookDetail = (DataTable) Session["ProductList"];
					this.DataGrid2.DataSource = dtOrderBookDetail;
					this.DataGrid2.DataBind();
				}
				if(Request["OrderSerialNo"] == null)
				{
					Popup("��Ч������ˮ");
					return;
				}
				if(Request["OrderState"] == null)
				{
					Popup("��Ч����");
					return;
				}
				string strOrderState = Request["OrderState"].ToString();
				if(strOrderState != "1")
				{
					Popup("δ���Ӽ������̣��ɽ��б༭");
					return;
				}
				txtOrderSerialNo.Text = Request["OrderSerialNo"].ToString();	
				txtOrderSerialNo.Enabled = false;
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

		}
		#endregion

		protected void btnOK_Click(object sender, System.EventArgs e)
		{
			try
			{
				DataTable dtOrderReduce = (DataTable) Session["ProductList"];
				OrderFacade order = new OrderFacade();
				string strOrderSerialNo = txtOrderSerialNo.Text;
				OperLog operLog = new OperLog();
				operLog.cnvcOperID = oper.strLoginID;
				operLog.cnvcDeptID = oper.strDeptID;
				operLog.cnvcOperType = "����";

				order.OrderReduce(strOrderSerialNo, ddlReduceType.SelectedValue, txtReduceComments.Text, dtOrderReduce, operLog);
				Session["ProductList"] = null;
				btnCancel_Click(null, null);
				btnOK.Visible = false;
				Popup("�����ɹ�");

				DataTable dtOrderBookDetail = (DataTable) Session["ProductList"];
				this.DataGrid2.DataSource = dtOrderBookDetail;
				this.DataGrid2.DataBind();
			}
			catch(Exception ex)
			{
				Popup(ex.Message);
			}
		}

		protected void btnCancel_Click(object sender, System.EventArgs e)
		{
			this.txtReduceComments.Text = "";
		}

		protected void btnReturn_Click(object sender, System.EventArgs e)
		{
			this.Response.Redirect("wfmOrderQuery.aspx");
		}
	}
}
