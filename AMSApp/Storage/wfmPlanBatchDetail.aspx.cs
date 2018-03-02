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
using System.Text.RegularExpressions;
using CommCenter;

namespace AMSApp.Storage
{
	/// <summary>
	/// Summary description for wfmPlanBatchDetail.
	/// </summary>
	public partial class wfmPlanBatchDetail : wfmBase
	{

		protected string strBeginDate;
		BusiComm.StorageBusi StoBusi;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			if(Session["Login"]!=null)
			{
				CMSMStruct.LoginStruct ls1=(CMSMStruct.LoginStruct)Session["Login"];
				if (!IsPostBack )
				{
					string strProductCode=Request.QueryString["PID"];
					string strProductName=Request.QueryString["PName"];
					string strUnit=Request.QueryString["PUnit"];
					string strMonth=Request.QueryString["month"];
					if(strProductCode==null||strProductCode==""||strProductName==null||strProductName==""||strUnit==null||strUnit=="")
					{
						this.SetErrorMsgPageBydir("��Ʒ��Ϣ���������ԣ�");
						return;
					}
					else
					{
						this.txtProductCode.Text=strProductCode;
						this.txtProductName.Text=strProductName;
						this.txtUnit.Text=strUnit;
						this.txtMonth.Text=strMonth;
					}
					strBeginDate=DateTime.Now.ToShortDateString();
					this.txtProductCode.ReadOnly=true;
					this.txtProductName.ReadOnly=true;
					this.txtUnit.ReadOnly=true;
					this.txtMonth.ReadOnly=true;
					this.ddlBatch.Items.Add(new ListItem("��һ��","1"));
					this.ddlBatch.Items.Add(new ListItem("�ڶ���","2"));
					this.ddlBatch.Items.Add(new ListItem("������","3"));
					this.ddlBatch.Items.Add(new ListItem("������","4"));
					this.ddlBatch.SelectedIndex=0;
				}
				else
				{
					strBeginDate = Request.Form["txtBegin"].ToString();
				}
			}
			else
			{
				Response.Redirect("../Exit.aspx");
			}
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    

		}
		#endregion

		protected void btcancel_Click(object sender, System.EventArgs e)
		{
			this.Response.Redirect("wfmStockPlan.aspx");
		}

		protected void btMod_Click(object sender, System.EventArgs e)
		{
			string strProductCode=this.txtProductCode.Text.Trim();
			string strProductName=this.txtProductName.Text.Trim();
			string strUnit=this.txtUnit.Text.Trim();
			string strBatch=this.ddlBatch.SelectedValue;
			string strStartDate=strBeginDate;
			string strCount=this.txtCount.Text.Trim();
			string strSumFee=this.txtSumFee.Text.Trim();
			string strMonth=this.txtMonth.Text.Trim();
			if(strCount==""||!Regex.IsMatch(strCount,@"^[+|-]{0,1}(\d*)\.{0,1}\d{0,}$"))
			{
				this.SetErrorMsgPageBydirHistory("�������������֣�");
				return;
			}
			if(strSumFee==""||!Regex.IsMatch(strSumFee,@"^[+|-]{0,1}(\d*)\.{0,1}\d{0,}$"))
			{
				this.SetErrorMsgPageBydirHistory("���ñ��������֣�");
				return;
			}

			Hashtable htpara=new Hashtable();
			htpara.Add("strProductCode",strProductCode);
			htpara.Add("strProductName",strProductName);
			htpara.Add("strUnit",strUnit);
			htpara.Add("strBatch",strBatch);
			htpara.Add("strStartDate",strStartDate);
			htpara.Add("strCount",strCount);
			htpara.Add("strSumFee",strSumFee);
			htpara.Add("strMonth",strMonth);

			Hashtable htapp=(Hashtable)Application["appconf"];
			string strcons=(string)htapp["cons"];
			StoBusi=new BusiComm.StorageBusi(strcons);
			try
			{
				if(StoBusi.UpdateSotckPlanBatch(htpara))
				{
					this.SetSuccMsgPageBydir("�޸ĳɹ���","Storage/wfmStockPlan.aspx");
					return;
				}
				else
				{
					this.SetErrorMsgPageBydir("�޸Ĳɹ��ƻ�ʱ�������������ԣ�");
					return;
				}
			}
			catch(Exception er)
			{
				this.clog.WriteLine(er);
				this.SetErrorMsgPageBydir("��ѯ���������ԣ�");
				return;
			}
		}
	}
}
