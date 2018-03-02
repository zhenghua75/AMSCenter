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
	/// wfmTimeSales ��ժҪ˵����
	/// </summary>
	public partial class wfmTimeSales : wfmBase
	{
	
		protected ucPageView UcPageView1;
		BusiComm.BusiQuery busiq;
		protected void Page_Load(object sender, System.EventArgs e)
		{
			this.Button2.Attributes.Add("onclick","javascript:window.open('../DataGridToExcel.aspx', 'Sample', 'toolbar=no,location=no,directories=no,status=yes,menubar=yes,scrollbars=no,resizable=yes,copyhistory=yes,width=790,height=520,left=0,top=0')");
			// �ڴ˴������û������Գ�ʼ��ҳ��
			if(Session["Login"]==null)
			{
				Response.Redirect("../Exit.aspx");
			}
			if (!IsPostBack )
			{
				Session.Remove("QUERY");
				Session.Remove("toExcel");
				Session.Remove("page_view");
				initCtl();
			}
		}
		private void initCtl()
		{
            this.FillDropDownList("tbCommCode", ddlDept, "vcCommSign ='MD' and vcCommCode<>'FYZX1'", "ȫ��");
            this.FillDropDownList("AllREGION", ddlRegion, "", "ȫ��");
            this.txtBeginDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
			this.txtEndDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
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

		protected void Button1_Click(object sender, System.EventArgs e)
		{
			//��ѯ
			try
			{
				//��ѯ
				Session.Remove("QUERY");
				Session.Remove("toExcel");

				Hashtable htapp=(Hashtable)Application["appconf"];
				string strcons=(string)htapp["cons"];
				busiq=new BusiComm.BusiQuery(strcons);

				string strDeptId = this.ddlDept.SelectedValue;
				if(strDeptId == "ȫ��")
				{
					strDeptId = "";
				}
				string strBeginDate = Convert.ToDateTime(this.txtBeginDate.Text).ToString("yyyy-MM-dd");
				string strEndDate = Convert.ToDateTime(this.txtEndDate.Text).ToString("yyyy-MM-dd");

				DataTable dtout=busiq.GetTimeSales(strDeptId,strBeginDate,strEndDate);
				//this.TableConvert(dtout,"�ŵ�","tbCommCode","vcCommSign='MD'");

				dtout.TableName="�ŵ�ʱ������ͳ��";
				DataTable dtexcel=dtout.Copy();
				Session["QUERY"] = dtout;
				Session["toExcel"]=dtexcel;
				CMSMStruct.LoginStruct ls1=(CMSMStruct.LoginStruct)Session["Login"];
				if(dtout.Rows.Count<=0)
				{
					Button2.Enabled=false;
				}
				else
				{
					if(ls1.strLimit=="CL001")
					{
						Button2.Enabled=true;
					}
				}
				UcPageView1.MyDataGrid.PageSize = 30;
				DataView dvOut =new DataView(dtout);
				this.UcPageView1.MyDataSource = dvOut;
				this.UcPageView1.BindGrid();
			}
			catch(Exception ex)
			{
				this.clog.WriteLine(ex);
				this.SetErrorMsgPageBydir("��ѯ���������ԣ�");
				return;
			}
		}

		protected void Button2_Click(object sender, System.EventArgs e)
		{
			//����
		}

        protected void ddlRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.BindRegion(this.ddlRegion.SelectedValue, this.ddlDept);
        }
	}
}
