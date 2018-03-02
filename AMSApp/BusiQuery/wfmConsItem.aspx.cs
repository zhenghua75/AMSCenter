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
	/// Summary description for wfmConsItem.
	/// </summary>
	public partial class wfmConsItem : wfmBase
	{
		protected System.Web.UI.HtmlControls.HtmlInputText txtBegin;
		protected System.Web.UI.HtmlControls.HtmlInputText txtEnd;
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
					this.FillDropDownList("tbCommCode", ddlAssType, "vcCommSign ='AT'","ȫ��");
                    this.FillDropDownList("tbCommCode", ddlDept, "vcCommSign ='MD' and vcCommCode<>'FYZX1'", "ȫ��");
					this.FillDropDownList("tbCommCode", ddlBillType, "vcCommSign ='PT'","ȫ��");
					this.FillDropDownList("tbCommCode", ddlAssState, "vcCommSign ='AS'","����");
                    this.FillDropDownList("AllREGION", ddlRegion, "", "ȫ��");
					this.ddlConsFlag.Items.Add(new ListItem("��������","0"));
					this.ddlConsFlag.Items.Add(new ListItem("�ѳ���","9"));
					this.ddlConsFlag.SelectedIndex=0;
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


					#region ���ư�ť��ʾ
					this.Button1.Visible = false;
					this.Button2.Visible=false;
					Hashtable htOperFunc=(Hashtable)Application["OperFunc"];
					ArrayList almenu=(ArrayList)htOperFunc[ls1.strLoginID];
					if(almenu!=null)
					{
						for(int i=0;i<almenu.Count;i++)
						{
							CMSMStruct.MenuStruct ms1=(CMSMStruct.MenuStruct)almenu[i];
							System.Web.UI.WebControls.Button btnCurrent = this.FindControl(ms1.strFuncAddress.Replace("wfmConsItem_",String.Empty)) as System.Web.UI.WebControls.Button;
							if(btnCurrent!=null)
							{
								btnCurrent.Visible = true;
								this.Button2.Visible=true;
							}				
						}
					}
					#endregion
				}
				else
				{
					strBeginDate = Request.Form["txtBegin"].ToString();
					strEndDate =  Request.Form["txtEnd"].ToString();
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
			//Table4.Width= "1224px";
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
			string strSerial=txtSerial.Text.Trim();
            htPara.Add("strSerial",strSerial);
			string strAssType=ddlAssType.SelectedValue;
			string strOperName=ddlOper.SelectedValue;
			string strDeptID=ddlDept.SelectedValue;
			string strConsFlag=this.ddlConsFlag.SelectedValue;
			string strBillType=this.ddlBillType.SelectedValue;
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
			if(strBillType=="ȫ��")
			{
				strBillType="";
			}
			htPara.Add("strBillType",strBillType);
			htPara.Add("strBegin",strBeginDate);
			htPara.Add("strEnd",strEndDate);
			htPara.Add("strConsFlag",strConsFlag);
			string strConfirm = this.ddlConfirm.SelectedValue;
			if(strConfirm=="ȫ��")
			{
				strConfirm = "";
			}
			htPara.Add("strConfirm",strConfirm);
			string strPackage = this.ddlPackage.SelectedValue;
			if(strPackage=="ȫ��")
			{
				strPackage="";
			}
			htPara.Add("strPackage",strPackage);
			try
			{
				double dsum=0;
				double dcashsum=0;
				DataTable dtout=busiq.GetConsQuery(htPara);
				if(dtout==null)
				{
					this.SetErrorMsgPageBydir("��ѯ���������ԣ�");
					btnExcel.Enabled=false;
					return;
				}
				else
				{
					this.TableConvert(dtout,"��Ա����","tbCommCode","vcCommSign='AT'");
					this.TableConvert(dtout,"��������","tbCommCode","vcCommSign='PT'");
					Session["ConfirmConsItem"] = dtout.Copy();
					this.TableConvert(dtout,"�ŵ�","tbCommCode","vcCommSign='MD'");
					dtout.TableName="������ϸ";
					DataTable dtexcel=dtout.Copy();
					Session["QUERY"] = dtout;
					for(int i=0;i<dtexcel.Rows.Count;i++)
					{
						dsum+=Math.Round(double.Parse(dtexcel.Rows[i]["�ϼ�"].ToString()),2);
						if(dtexcel.Rows[i]["��������"].ToString()=="֧���ֽ�")
						{
							dcashsum+=Math.Round(double.Parse(dtexcel.Rows[i]["�ϼ�"].ToString()),2);
						}
						if(dtexcel.Rows[i]["��Ա����"].ToString().Substring(0,1)!="V")
						{
							dtexcel.Rows[i]["��Ա����"]="'"+dtexcel.Rows[i]["��Ա����"].ToString();
						}
						dtexcel.Rows[i]["��Ʒ����"]="'"+dtexcel.Rows[i]["��Ʒ����"].ToString();
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

				dsum=Math.Round(dsum,2);
				dcashsum=Math.Round(dcashsum,2);
				this.lblSum.Text="�����ܣ�"+dsum.ToString()+"Ԫ\r\r\r\r\r\r\r\r�ֽ���ܣ�"+dcashsum.ToString()+"Ԫ";
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

		protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
		{
			string strDept=ddlDept.SelectedValue;
			Hashtable htapp=(Hashtable)Application["appconf"];
			string strcons=(string)htapp["cons"];
			busiq=new BusiComm.BusiQuery(strcons);
			DataTable dtoper=busiq.GetConsOperList(strDept,strBeginDate,strEndDate);
			this.FillDropDownList(dtoper,ddlOper,"ȫ��");
			Session["QUERY"] = null;
			Session["ConfirmConsItem"] = null;
			Session["toExcel"] = null;
			this.UcPageView1.MyDataGrid.DataSource = null;
			this.UcPageView1.MyDataGrid.DataBind();
			this.UcPageView1.FootBar.Visible=false;
		}

		protected void Button1_Click(object sender, System.EventArgs e)
		{
			//ȷ�ϱ����˻�
			DataTable dt = (DataTable)Session["ConfirmConsItem"];
			ArrayList al = new ArrayList();
			foreach(DataRow dr in dt.Rows)
			{
				string type = dr["��������"].ToString();
				string comment = dr["��ע"].ToString();
				if((type=="��������" || type=="�˻�����") && comment!="��ȷ��")
				{
					string serial = dr["��ˮ"].ToString();
					string deptid = dr["�ŵ�"].ToString();
					al.Add(serial+","+deptid);
				}
			}
			if(al.Count>0)
			{
				
				Hashtable htapp=(Hashtable)Application["appconf"];
				string strcons=(string)htapp["cons"];
				Manager mr = new Manager(strcons);
				try
				{
					mr.ConfirmConsItem(al);
				}
				catch(Exception ex)
				{
					this.SetErrorMsgPageBydir("ȷ��ʧ�ܣ�"+ex.Message);
				}
			}
			this.SetErrorMsgPageBydir("ȷ�ϳɹ���");
		}

		protected void Button2_Click(object sender, System.EventArgs e)
		{
			//�˻������ѯ
			//Table4.Width= "95%";
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
			string strSerial=txtSerial.Text.Trim();
			htPara.Add("strSerial",strSerial);
			string strAssType=ddlAssType.SelectedValue;
			string strOperName=ddlOper.SelectedValue;
			string strDeptID=ddlDept.SelectedValue;
			string strConsFlag=this.ddlConsFlag.SelectedValue;
			string strBillType=this.ddlBillType.SelectedValue;
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
			if(strBillType=="ȫ��")
			{
				strBillType="";
			}
			htPara.Add("strBillType",strBillType);
			htPara.Add("strBegin",strBeginDate);
			htPara.Add("strEnd",strEndDate);
			htPara.Add("strConsFlag",strConsFlag);
			string strConfirm = this.ddlConfirm.SelectedValue;
			if(strConfirm=="ȫ��")
			{
				strConfirm = "";
			}
			htPara.Add("strConfirm",strConfirm);
			string strPackage = this.ddlPackage.SelectedValue;
			if(strPackage=="ȫ��")
			{
				strPackage="";
			}
			htPara.Add("strPackage",strPackage);
			try
			{
				double dsum=0;
				double dcashsum=0;
				DataTable dtout=busiq.GetConsQuery(htPara);
				if(dtout==null)
				{
					this.SetErrorMsgPageBydir("��ѯ���������ԣ�");
					btnExcel.Enabled=false;
					return;
				}
				else
				{
					this.TableConvert(dtout,"��Ա����","tbCommCode","vcCommSign='AT'");
					this.TableConvert(dtout,"��������","tbCommCode","vcCommSign='PT'");
					Session["ConfirmConsItem"] = dtout.Copy();
					this.TableConvert(dtout,"�ŵ�","tbCommCode","vcCommSign='MD'");
					dtout.TableName="������ϸ";
					DataTable dtexcel=dtout.Copy();
					dtout.Columns.Remove("��Ա����");
					dtout.Columns.Remove("��Ա����");
					dtout.Columns.Remove("��Ա����");
					dtout.Columns.Remove("����");
					dtout.Columns.Remove("�ϼ�");
					dtout.Columns.Remove("�Ƿ��ײ�");
					Session["QUERY"] = dtout;
					for(int i=0;i<dtexcel.Rows.Count;i++)
					{
						dsum+=Math.Round(double.Parse(dtexcel.Rows[i]["�ϼ�"].ToString()),2);
						if(dtexcel.Rows[i]["��������"].ToString()=="֧���ֽ�")
						{
							dcashsum+=Math.Round(double.Parse(dtexcel.Rows[i]["�ϼ�"].ToString()),2);
						}
						if(dtexcel.Rows[i]["��Ա����"].ToString().Substring(0,1)!="V")
						{
							dtexcel.Rows[i]["��Ա����"]="'"+dtexcel.Rows[i]["��Ա����"].ToString();
						}
						dtexcel.Rows[i]["��Ʒ����"]="'"+dtexcel.Rows[i]["��Ʒ����"].ToString();
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

				dsum=Math.Round(dsum,2);
				dcashsum=Math.Round(dcashsum,2);
				this.lblSum.Text="�����ܣ�"+dsum.ToString()+"Ԫ\r\r\r\r\r\r\r\r�ֽ���ܣ�"+dcashsum.ToString()+"Ԫ";
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

		protected void Button3_Click(object sender, System.EventArgs e)
		{
			//��ѯ�ײ�
			Session.Remove("QUERY");
			Session.Remove("toExcel");
			strBeginDate = Request.Form["txtBegin"].ToString();
			strEndDate =  Request.Form["txtEnd"].ToString();
			//Table4.Width= "1224px";
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
			string strSerial=txtSerial.Text.Trim();
			htPara.Add("strSerial",strSerial);
			string strAssType=ddlAssType.SelectedValue;
			string strOperName=ddlOper.SelectedValue;
			string strDeptID=ddlDept.SelectedValue;
			string strConsFlag=this.ddlConsFlag.SelectedValue;
			string strBillType=this.ddlBillType.SelectedValue;
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
			if(strBillType=="ȫ��")
			{
				strBillType="";
			}
			htPara.Add("strBillType",strBillType);
			htPara.Add("strBegin",strBeginDate);
			htPara.Add("strEnd",strEndDate);
			htPara.Add("strConsFlag",strConsFlag);
			string strConfirm = this.ddlConfirm.SelectedValue;
			if(strConfirm=="ȫ��")
			{
				strConfirm = "";
			}
			htPara.Add("strConfirm",strConfirm);
			htPara.Add("bPackage",true);
			string strPackage = this.ddlPackage.SelectedValue;
			if(strPackage=="ȫ��")
			{
				strPackage="";
			}
			htPara.Add("strPackage",strPackage);
			string strAssState=ddlAssState.SelectedValue;
			if (strAssState=="����")
			{
				strAssState="Roll";
			}
			htPara.Add("strAssState",strAssState);
			try
			{
				double dsum=0;
				double dcashsum=0;
				DataTable dtout=busiq.GetConsQuery(htPara);
				if(dtout==null)
				{
					this.SetErrorMsgPageBydir("��ѯ���������ԣ�");
					btnExcel.Enabled=false;
					return;
				}
				else
				{
					dtout.Columns["��ע"].ColumnName="�ײ�����";
					this.TableConvert(dtout,"��Ա����","tbCommCode","vcCommSign='AT'");
					this.TableConvert(dtout,"��������","tbCommCode","vcCommSign='PT'");
					Session["ConfirmConsItem"] = dtout.Copy();
					this.TableConvert(dtout,"�ŵ�","tbCommCode","vcCommSign='MD'");
					dtout.TableName="������ϸ";
					DataTable dtexcel=dtout.Copy();
					Session["QUERY"] = dtout;
					for(int i=0;i<dtexcel.Rows.Count;i++)
					{
						dsum+=Math.Round(double.Parse(dtexcel.Rows[i]["�ϼ�"].ToString()),2);
						if(dtexcel.Rows[i]["��������"].ToString()=="֧���ֽ�")
						{
							dcashsum+=Math.Round(double.Parse(dtexcel.Rows[i]["�ϼ�"].ToString()),2);
						}
						if(dtexcel.Rows[i]["��Ա����"].ToString().Substring(0,1)!="V")
						{
							dtexcel.Rows[i]["��Ա����"]="'"+dtexcel.Rows[i]["��Ա����"].ToString();
						}
						dtexcel.Rows[i]["��Ʒ����"]="'"+dtexcel.Rows[i]["��Ʒ����"].ToString();
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

				dsum=Math.Round(dsum,2);
				dcashsum=Math.Round(dcashsum,2);
				this.lblSum.Text="�����ܣ�"+dsum.ToString()+"Ԫ\r\r\r\r\r\r\r\r�ֽ���ܣ�"+dcashsum.ToString()+"Ԫ";
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

        protected void ddlRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.BindRegion(this.ddlRegion.SelectedValue, this.ddlDept);
        }
	}
}
