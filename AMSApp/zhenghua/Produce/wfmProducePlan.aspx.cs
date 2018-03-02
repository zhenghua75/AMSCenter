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
	/// wfmProducePlan ��ժҪ˵����
	/// </summary>
	public partial class wfmProducePlan : wfmBase
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// �ڴ˴������û������Գ�ʼ��ҳ��
			if(!this.IsPostBack)
			{
//				this.BindDept(ddlProduceDept, "cnvcDeptType <>'Corp'");
				this.BindDept(ddlProduceDept, "cnvcDeptType ='Factory'");
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

		protected void btnCancel_Click(object sender, System.EventArgs e)
		{
			this.txtProduceDate.Text = "";
			this.txtShipBeginDate.Text = "";
			this.txtShipEndDate.Text = "";
		}

		
		protected void btnOK_Click(object sender, System.EventArgs e)
		{
			try
			{
				if(JudgeIsNull(txtProduceDate.Text,"��������"))
				{
					//Popup();
					return;
				}
				if(JudgeIsNull(txtShipBeginDate.Text,"��ʼ����"))
				{
					//Popup();
					return;
				}
				if(JudgeIsNull(txtShipEndDate.Text,"��������"))
				{
					//Popup();
					return;
				}

				ProduceLog producePlan = new ProduceLog();
				producePlan.cnvcProduceDeptID = ddlProduceDept.SelectedValue;
				producePlan.cndProduceDate = DateTime.Parse(txtProduceDate.Text);
				producePlan.cndShipBeginDate = DateTime.Parse(txtShipBeginDate.Text);
				producePlan.cndShipEndDate = DateTime.Parse(txtShipEndDate.Text);
				producePlan.cnvcOperID = oper.strLoginID;
				producePlan.cnvcProduceState = "0";

				OperLog operLog = new OperLog();
				operLog.cnvcOperID = oper.strLoginID;
				operLog.cnvcDeptID = oper.strDeptID;
				operLog.cnvcOperType = "��������ƻ�";

				ProduceFacade produce = new ProduceFacade();
				produce.AddProduceLog(producePlan,operLog);
				Popup("�����ƻ���ӳɹ�");
				btnCancel_Click(null, null);
			}
			catch(Exception ex)
			{
				Popup(ex.Message);
			}
		}

		protected void btnReturn_Click(object sender, System.EventArgs e)
		{
			this.Response.Redirect("wfmProducePlanQuery.aspx");
		}
	}
}
