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
using System.Collections.Specialized;

namespace AMSApp.BusiQuery
{
	/// <summary>
	/// Summary description for wfmFillQuery.
	/// </summary>
	public partial class wfmFillQuery : wfmBase
	{

		protected string strEndDate;
		protected ucPageView UcPageView1;
		protected string strBeginDate;
		protected string strExcelPath = string.Empty;
		BusiComm.BusiQuery busiq;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			this.btnExcel.Attributes.Add("onclick","javascript:window.open('../DataGridToExcel.aspx', 'Sample', 'toolbar=no,location=no,directories=no,status=yes,menubar=yes,scrollbars=no,resizable=yes,copyhistory=yes,width=790,height=520,left=0,top=0')");
			if(Session["Login"]!=null)
			{
				CMSMStruct.LoginStruct ls1=(CMSMStruct.LoginStruct)Session["Login"];
//				if(ls1.strLimit!="CL001")
//				{
//					this.SetErrorMsgPageBydir("�Բ�����û��Ȩ��ʹ�ô˹��ܣ�");
//					return;
//				}
				if (!IsPostBack )
				{
					this.ddlFillType.Items.Add(new ListItem("������ֵ","������ֵ"));
					this.ddlFillType.Items.Add(new ListItem("������ֵ","������ֵ"));
					this.ddlFillType.Items.Add(new ListItem("����ֵ","����ֵ"));
					this.ddlFillType.Items.Add(new ListItem("���ѳ���","���ѳ���"));
					this.ddlFillType.Items.Add(new ListItem("�ϲ�ת��","�ϲ�ת��"));
					this.ddlFillType.Items.Add(new ListItem("��ֵ����","��ֵ����"));
					this.ddlFillType.SelectedIndex=0;
					this.FillDropDownList("tbCommCode", ddlAssType, "vcCommSign ='AT'","ȫ��");
                    this.FillDropDownList("tbCommCode", ddlDept, "vcCommSign ='MD' and vcCommCode<>'FYZX1'", "ȫ��");
					this.FillDropDownList("tbCommCode", ddlAssState, "vcCommSign ='AS'","����");
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
					Session.Remove("QUERY");
					Session.Remove("toExcel");
					Session.Remove("page_view");
				}
				else
				{
					strBeginDate = Request.Form["txtBegin"].ToString();
					strEndDate =  Request.Form["txtEnd"].ToString();
				}
				if(this.UcPageView1.MyDataGrid.DataSource!=null)
				{
					if(((DataView)this.UcPageView1.MyDataGrid.DataSource).Count>0)
					{
						UcPageView1.FootBar.Visible = true;
						btnExcel.Enabled=true;
					}
					else
					{
						btnExcel.Enabled=false;
					}
				}
				else
				{
					btnExcel.Enabled=false;
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
			string strCardID=txtCardID.Text.Trim();
			htPara.Add("strCardID",strCardID);
			string strAssName=txtAssName.Text.Trim();
			htPara.Add("strAssName",strAssName);
			string strAssType=ddlAssType.SelectedValue;
			string strOperName=ddlOper.SelectedValue;
			string strDeptID=ddlDept.SelectedValue;
			string strFillType=ddlFillType.SelectedValue;
			string strAssState=ddlAssState.SelectedValue;
			if (strAssState=="����")
			{
				strAssState="Roll";
			}
			htPara.Add("strAssState",strAssState);
			if(strAssType=="ȫ��")
			{
				strAssType="";
			}
			htPara.Add("strAssType",strAssType);
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
			switch(strFillType)
			{
				case "������ֵ":
					strFillType="Norm";
					break;
				case "������ֵ":
					strFillType="%����%";
					break;
				case "����ֵ":
					strFillType="%����ֵ%";
					break;
				case "���ѳ���":
					strFillType="%���ѳ���%";
					break;
				case "�ϲ�ת��":
					strFillType="%�ϲ�%";
					break;
				case "��ֵ����":
					strFillType="%��ֵ����%";
					break;
			}
			htPara.Add("strFillType",strFillType);
			htPara.Add("strBegin",strBeginDate);
			htPara.Add("strEnd",strEndDate);
			
			try
			{
				double dsum=0;
				double dsum2=0;
				DataTable dtout=busiq.GetFillQuery(htPara);

				if(dtout==null)
				{
					this.SetErrorMsgPageBydir("��ѯ�����������ԣ�");
					btnExcel.Enabled=false;
					return;
				}
				else
				{
					this.TableConvert(dtout,"��Ա����","tbCommCode","vcCommSign='AT'");
					this.TableConvert(dtout,"����Ա�ŵ�","tbCommCode","vcCommSign='MD'");
					dtout.TableName="��ֵ��ϸ";
					DataTable dtexcel=dtout.Copy();
					Session["QUERY"] = dtout;
					for(int i=0;i<dtexcel.Rows.Count;i++)
					{
						dsum+=double.Parse(dtexcel.Rows[i]["��ֵ���"].ToString());
						dsum2+=double.Parse(dtexcel.Rows[i]["������"].ToString());
						if(dtexcel.Rows[i][3].ToString().Substring(0,1)!="V")
						{
							dtexcel.Rows[i][3]="'"+dtexcel.Rows[i][3].ToString();
						}
					}
					Session["toExcel"]=dtexcel;
					if(dtout.Rows.Count<=0)
					{
						btnExcel.Enabled=false;
					}
					else
					{
						btnExcel.Enabled=true;	
					}
				}
				
				this.lblSum.Text="��ֵ��"+dsum.ToString()+"Ԫ�����ͽ�"+dsum2.ToString()+"�����ܽ�"+Convert.ToString(dsum+dsum2);
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
			Session["QUERY"] = null;
			Session["toExcel"] = null;
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