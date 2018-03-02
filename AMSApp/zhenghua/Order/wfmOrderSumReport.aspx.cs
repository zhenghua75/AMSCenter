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
	/// wfmDividReport ��ժҪ˵����
	/// </summary>
	public partial class wfmOrderSumReport : wfmBase
	{
		protected System.Web.UI.WebControls.Label Label2;
		//protected System.Web.UI.WebControls.Label lblOper;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.Label Label6;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// �ڴ˴������û������Գ�ʼ��ҳ��
			if(!this.IsPostBack)
			{
				if(Session["OrderSumReport"] == null)
				{
					Popup("��Ч����");
					return;
				}				
				string strReport = Session["OrderSumReport"].ToString();
				DataTable dtReport = Helper.Query(strReport);
				

				this.DataTableConvert(dtReport, "cnvcOrderDeptID", "tbDept", "cnvcDeptID", "cnvcDeptName", "");
				//this.DataTableConvert(dtReport, "cnvcOrderOperID", "tbLogin", "vcLoginID", "vcOperName", "");
				
				DataTable dtDept = (DataTable)Application["tbDept"];
				
				//this.DataTableConvert(dtReport, "cnvcOrderType", "tbNameCode", "cnvcCode", "cnvcName", "cnvcType='ORDERTYPE'");
				//this.DataTableConvert(dtReport, "cnvcOrderState", "tbNameCode", "cnvcCode", "cnvcName", "cnvcType='ORDERSTATE'");
				//
				//this.DataTableConvert(dtReport, "cnvcProduceDeptID", "tbDept", "cnvcDeptID", "cnvcDeptName", "");
			
				DataTable dtpt = new DataTable();
				dtpt.Columns.Add("��Ʒ����");
				//dtpt.Columns.Add("��Ʒ����");
				dtpt.Columns.Add("��Ʒ����");
				dtpt.Columns.Add("���");
				dtpt.Columns.Add("��λ");
				foreach(DataRow drDept in dtDept.Rows)
				{
					if(!drDept["cnvcDeptType"].ToString().Equals("Corp"))//&&!drDept["cnvcDeptType"].ToString().Equals("FYZX1"))
					{
						dtpt.Columns.Add(drDept["cnvcDeptName"].ToString());	
					}
						
				}
				//dtpt.Columns.Add("�ϼ�");
				dtpt.Columns.Add("�ϼ�");
				//Hashtable htProduct = new Hashtable();
				foreach(DataRow drReport in dtReport.Rows)
				{
					//DataRow drpt = null;
					DataRow[] drpts = dtpt.Select("��Ʒ����='"+drReport["cnvcProductCode"].ToString()+"'");
					if(drpts.Length > 0)
					{
						DataRow drpt = drpts[0];
						drpt["��Ʒ����"] = drReport["cnvcProductCode"].ToString();
						drpt["��Ʒ����"] = drReport["cnvcProductName"].ToString();
						drpt["���"] = drReport["cnvcProduct_Statd"].ToString();
						drpt["��λ"] = drReport["cnvcUnit"].ToString();
						drpt[drReport["cnvcOrderDeptIDComments"].ToString()] = drReport["cnnCount"].ToString();
//						DataRow[] drReport2 = dtAssign2.Select("cnvcProductCode='" + drReport["cnvcProductCode"].ToString() + "'");
//						if(drReport2.Length > 0)
//							drpt["�ϼ�"] = drReport2[0]["cnnCount"];
						//dtpt.Rows.Add(drpt);
					}
					else
					{
						DataRow drpt = dtpt.NewRow();
						drpt["��Ʒ����"] = drReport["cnvcProductCode"].ToString();
						drpt["��Ʒ����"] = drReport["cnvcProductName"].ToString();
						drpt["���"] = drReport["cnvcProduct_Statd"].ToString();
						drpt["��λ"] = drReport["cnvcUnit"].ToString();
						drpt[drReport["cnvcOrderDeptIDComments"].ToString()] = drReport["cnnCount"].ToString();
//						DataRow[] drReport2 = dtAssign2.Select("cnvcProductCode='" + drReport["cnvcProductCode"].ToString() + "'");
//						if(drReport2.Length > 0)
//							drpt["�ϼ�"] = drReport2[0]["cnnCount"];
						dtpt.Rows.Add(drpt);

					}
				
				
				}
				//�ϼ�����
				foreach(DataRow dr in dtpt.Rows)
				{
					foreach(DataRow drDept in dtDept.Rows)
					{
						if(!drDept["cnvcDeptType"].ToString().Equals("Corp"))//&&!drDept["cnvcDeptType"].ToString().Equals("FYZX1"))
						{
							string strCount = dr[drDept["cnvcDeptName"].ToString()].ToString();	
							if(strCount == "")
								strCount = "0";
							string strSum = dr["�ϼ�"].ToString();
							if(strSum == "")
								strSum = "0";
							dr["�ϼ�"] = decimal.Parse(strCount)+decimal.Parse(strSum);
						}
						
					}
				}

				this.DataGrid1.DataSource = dtpt;
				this.DataGrid1.DataBind();

				//this.lblOper.Text = this.oper.strOperName;
				this.lblDate.Text = DateTime.Now.ToString("yyyy��MM��dd��");
//				if(Request["OrderType"] != null)
//				{
//					this.lblDate.Text += Request["OrderType"].ToString();
//				}
				this.lblDate.Text += "���ܱ�";
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

		protected void btnReturn_Click(object sender, System.EventArgs e)
		{
			Session["OrderSumReport"] = null;
			this.Response.Redirect("wfmOrderQuery.aspx");
		}
	}
}
