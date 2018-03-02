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

namespace AMSApp.BusiQuery
{
	/// <summary>
	/// wfmDailyCashQuery ��ժҪ˵����
	/// </summary>
	public partial class wfmDailyCashQuery : wfmBase
	{

		protected string strEndDate;
		protected ucPageView UcPageView1;
		protected string strBeginDate;
		BusiComm.BusiQuery busiq;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// �ڴ˴������û������Գ�ʼ��ҳ��
			if(Session["Login"]!=null)
			{
				CMSMStruct.LoginStruct ls1=(CMSMStruct.LoginStruct)Session["Login"];
				if (!IsPostBack )
				{
                    this.FillDropDownList("tbCommCode", ddlDept, "vcCommSign ='MD' and vcCommCode<>'FYZX1'", "ȫ��");
                    this.FillDropDownList("AllREGION", ddlRegion, "", "ȫ��");
                    if(ls1.strLimit!="CL001")
					{
						ddlDept.Items.FindByValue(ls1.strDeptID).Selected=true;
						ddlDept.Enabled=false;
                        ddlRegion.Enabled = false;
					}
					strBeginDate=DateTime.Now.ToShortDateString();
					strEndDate=DateTime.Now.ToShortDateString();
					string strDept=ddlDept.SelectedValue;
					Hashtable htapp=(Hashtable)Application["appconf"];
					string strcons=(string)htapp["cons"];
					busiq=new BusiComm.BusiQuery(strcons);
					DataTable dtoper=busiq.GetConsOperList(strDept,strBeginDate,strEndDate);
					this.FillDropDownList(dtoper,ddlOper,"ȫ��");
//					Session.Remove("QUERY");
//					Session.Remove("toExcel");
//					Session.Remove("page_view");
				}
				else
				{
					strBeginDate = Request.Form["txtBegin"].ToString();
					strEndDate =  Request.Form["txtEnd"].ToString();
				}
//				if(this.UcPageView1.MyDataGrid.DataSource!=null)
//				{
//					if(((DataView)this.UcPageView1.MyDataGrid.DataSource).Count>0)
//					{
//						UcPageView1.FootBar.Visible = true;
////						btnExcel.Enabled=true;
//					}
//					else
//					{
////						btnExcel.Enabled=false;
//					}
//				}
//				else
//				{
////					btnExcel.Enabled=false;
//				}
			}
			else
			{
				Response.Redirect("../Exit.aspx");
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

		protected void btQuery_Click(object sender, System.EventArgs e)
		{
			Session.Remove("QUERY");
			Session.Remove("toExcel");
			strBeginDate = Request.Form["txtBegin"].ToString();
			strEndDate =  Request.Form["txtEnd"].ToString();
			if(strBeginDate==""||strEndDate==""||strBeginDate==null||strEndDate==null)
			{
				this.SetErrorMsgPageBydir("ʱ�䲻��Ϊ�գ�������ѡ��ʱ�䣡");
				return;
			}

			Hashtable htapp=(Hashtable)Application["appconf"];
			string strcons=(string)htapp["cons"];
			busiq=new BusiComm.BusiQuery(strcons);

			Hashtable htPara=new Hashtable();
			string strOperName=ddlOper.SelectedValue;
			string strDeptID=ddlDept.SelectedValue;
			if(strOperName=="ȫ��")
			{
				strOperName="";
			}
			htPara.Add("strOperName",strOperName);
			if(strDeptID=="ȫ��")
			{
				strDeptID="";
			}
			htPara.Add("strDeptID",strDeptID);
			htPara.Add("strBegin",strBeginDate);
			htPara.Add("strEnd",strEndDate);
			
			try
			{
				DataTable dtout=busiq.GetDailyCashQuery(htPara);

				if(dtout==null)
				{
					this.SetErrorMsgPageBydir("��ѯ�����������ԣ�");
//					btnExcel.Enabled=false;
					return;
				}

				UcPageView1.MyDataGrid.PageSize = 30;
				DataView dvOut =new DataView(dtout);
				this.UcPageView1.MyDataSource = dvOut;
				this.UcPageView1.BindGrid();
			}
			catch(Exception er)
			{
				this.clog.WriteLine(er);
				this.SetErrorMsgPageBydir("��ѯ���������ԣ�");
				return;
			}
		}

		protected void ddlDept_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			string strDept=ddlDept.SelectedValue;
			Hashtable htapp=(Hashtable)Application["appconf"];
			string strcons=(string)htapp["cons"];
			busiq=new BusiComm.BusiQuery(strcons);
			DataTable dtoper=busiq.GetConsOperList(strDept,strBeginDate,strEndDate);
			this.FillDropDownList(dtoper,ddlOper,"ȫ��");
			this.UcPageView1.MyDataGrid.DataSource = null;
			this.UcPageView1.MyDataGrid.DataBind();
			this.UcPageView1.FootBar.Visible=false;
		}

        protected void ddlRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.BindRegion(this.ddlRegion.SelectedValue, this.ddlDept);
        }
	}
}