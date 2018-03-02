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
	/// wfmDividReport ��ժҪ˵����
	/// </summary>
	public partial class wfmDividReport : wfmBase
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// �ڴ˴������û������Գ�ʼ��ҳ��
			this.btnPrint.Attributes.Add("onclick","document.all.WebBrowser.ExecWB(6,1);");
			this.btnDPrint.Attributes.Add("onclick","document.all.WebBrowser.ExecWB(6,6);");
			if(!this.IsPostBack)
			{
				if(Request["OrderSerialNo"]== null ||Request["ProduceSerialNo"]== null||Request["AssignSerialNo"]==null)
				{
					Popup("��Ч����");
				}
				string strOrderSerialNo = Request["OrderSerialNo"].ToString();
				string strAssignSerialNo = Request["AssignSerialNo"].ToString();
				txtProduceSerialNo.Text = Request["ProduceSerialNo"].ToString();
				txtOrderSerialNo.Text = strOrderSerialNo;
				txtAssignSerialNo.Text = strAssignSerialNo;
				lblAssignSerialNo.Text = strAssignSerialNo;
				lblOrderSerialNo.Text = strOrderSerialNo;
				BindOrder(strOrderSerialNo,strAssignSerialNo);
			}
		}
		private void BindOrder(string strOrderSerialNo,string strAssignSerialNo)
		{
			string strOrder = "select * from tbOrderBook where cnnOrderSerialNo=" + strOrderSerialNo;
			DataTable dtOrder = Helper.Query(strOrder);
			if(dtOrder.Rows.Count == 0)
			{
				Popup("�����쳣���޴˶���");
				return;
			}
			this.DataTableConvert(dtOrder, "cnvcOrderDeptID", "tbDept", "cnvcDeptID", "cnvcDeptName", "");
			OrderBook order = new OrderBook(dtOrder);
			string strAssign = "select * from tbAssignDetail where cnnOrderSerialNo="+strOrderSerialNo+" and cnnAssignSerialNo=" + strAssignSerialNo +" order by cnvcProductCode";
			DataTable dtAssign = Helper.Query(strAssign);
			if(dtAssign.Rows.Count == 0)
			{
				Popup("�����쳣���˶����޷ֻ�����");
				return;
			}
			this.DataGrid1.DataSource = dtAssign;
			this.DataGrid1.DataBind();

			this.lblOper.Text = oper.strOperName;
			this.lblOrderDept.Text = dtOrder.Rows[0]["cnvcOrderDeptIDComments"].ToString();
			this.lblShipDate.Text = order.cndShipDate.ToString("yyyy��MM��dd��");
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
			this.Response.Redirect("wfmDividAdjust.aspx?ProduceSerialNo="+txtProduceSerialNo.Text);
		}

		protected void btnPrint_ServerClick(object sender, System.EventArgs e)
		{
			try
			{
				ProduceFacade pf = new ProduceFacade();
				OperLog operLog = new OperLog();
				operLog.cnvcOperID = oper.strLoginID;
				operLog.cnvcDeptID = oper.strDeptID;
				operLog.cnvcOperType = "������";
				pf.AssignPrint(txtAssignSerialNo.Text,operLog);
			}
			catch(Exception ex)
			{
				Popup(ex.Message);
			}
		}

		protected void btnDPrint_ServerClick(object sender, System.EventArgs e)
		{
			try
			{
				ProduceFacade pf = new ProduceFacade();
				OperLog operLog = new OperLog();
				operLog.cnvcOperID = oper.strLoginID;
				operLog.cnvcDeptID = oper.strDeptID;
				operLog.cnvcOperType = "������";
				pf.AssignPrint(txtAssignSerialNo.Text,operLog);
			}
			catch(Exception ex)
			{
				Popup(ex.Message);
			}
		}
	}
}
