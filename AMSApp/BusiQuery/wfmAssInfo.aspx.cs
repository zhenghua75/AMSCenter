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
using cc;
using System.IO;

namespace AMSApp.BusiQuery
{
    public class DataGridTemplate : ITemplate
    {
        public DataGridTemplate()
        {
        }

        public void InstantiateIn(System.Web.UI.Control container)
        {
            LinkButton lb = new LinkButton();
            lb.CommandName = "Delete";
            lb.Text = "挂失";
            lb.OnClientClick = "return confirm('删除?');";
            container.Controls.Add(lb);
        }
    }
	/// <summary>
	/// Summary description for wfmAssInfo.
	/// </summary>
	public partial class wfmAssInfo : wfmBase
	{
		protected System.Web.UI.HtmlControls.HtmlInputText txtBegin;

		protected ucPageView UcPageView1;
		protected string strCreateBeginDate;
		protected string strExcelPath = string.Empty;
		BusiComm.BusiQuery busiq;
		BusiComm.Manager m1;
		protected string strBeginDate;
		protected string strEndDate;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			this.btnExcel.Attributes.Add("onclick","javascript:window.open('../DataGridToExcel.aspx', 'Sample', 'toolbar=no,location=no,directories=no,status=yes,menubar=yes,scrollbars=no,resizable=yes,copyhistory=yes,width=790,height=520,left=0,top=0')");
			if(Session["Login"]!=null)
			{
				CMSMStruct.LoginStruct ls1=(CMSMStruct.LoginStruct)Session["Login"];
//				if(ls1.strLimit=="CL004")
//				{
//					this.SetErrorMsgPageBydir("对不起，你没有权限使用此功能！");
//					return;
//				}
				if(ls1.strLimit!="CL001")
				{
//					this.chkCreate.Enabled=false;
//					this.btCreate.Enabled=false;
				}
				if (!IsPostBack )
				{
					this.FillDropDownList("tbCommCode", ddlAssType, "vcCommSign ='AT'","全部");
                    this.FillDropDownList("tbCommCode", ddlDept, "vcCommSign  like 'MD%' and vcCommCode<>'FYZX1'", "全部");
					this.FillDropDownList("tbCommCode", ddlAssState, "vcCommSign ='AS'","全部");
                    this.FillDropDownList("AllREGION", ddlRegion, "", "全部");
					this.ddlAssType.Items.Remove(this.ddlAssType.Items[3]);
//					this.chkCreate.Checked=false;
//					this.btCreate.Enabled=false;
					strCreateBeginDate=DateTime.Now.ToShortDateString();
					strBeginDate=DateTime.Now.ToShortDateString();
					strEndDate=DateTime.Now.ToShortDateString();
					Session.Remove("QUERY");
					Session.Remove("toExcel");
					Session.Remove("page_view");

				}
				else
				{
					strCreateBeginDate = Request.Form["txtBegin"].ToString();
					strBeginDate = Request.Form["txtBegin"].ToString();
					strEndDate =  Request.Form["txtEnd"].ToString();
				}
                
                
			}
			else
			{
				Response.Redirect("../Exit.aspx");
			}
		}
        

        private bool CheckAuthority()
        {
            CMSMStruct.LoginStruct ls1 = new CommCenter.CMSMStruct.LoginStruct();
            if (Session["Login"] == null)
            {
                Response.Redirect("Exit.aspx");
            }
            else
            {
                ls1 = (CMSMStruct.LoginStruct)Session["Login"];
            }
            Hashtable htOperFunc = (Hashtable)Application["OperFunc"];
            ArrayList almenu = (ArrayList)htOperFunc[ls1.strLoginID];
            if (almenu != null)
            {
                for (int i = 0; i < almenu.Count; i++)
                {
                    CMSMStruct.MenuStruct ms1 = (CMSMStruct.MenuStruct)almenu[i];
                    if(ms1.strFuncAddress=="wfmAssInfo_loss")
                    {
                        return true;
                    }
                }
            }
            return false;
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
				this.SetErrorMsgPageBydir("时间不能为空，请重新选择时间！");
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
			string strAssState=ddlAssState.SelectedValue;
			string strDeptID=ddlDept.SelectedValue;
			if(strAssType=="全部")
			{
				strAssType="";
			}
			htPara.Add("strAssType",strAssType);
			if(strAssState=="全部")
			{
				strAssState="";
			}
			htPara.Add("strAssState",strAssState);
			if(strDeptID=="全部")
			{
				strDeptID="";
			}
			htPara.Add("strDeptID",strDeptID);
			htPara.Add("strBeginDate",strBeginDate);
			htPara.Add("strEndDate",strEndDate);
			string strLinkPhone = this.txtLinkPhone.Text;
			htPara.Add("strLinkPhone",strLinkPhone);
			try
			{
				DataTable dtout=busiq.GetAssInfo(htPara);
				if(dtout==null)
				{
					this.SetErrorMsgPageBydir("查询出错，请重试！");
					btnExcel.Enabled=false;
					return;
				}
				else
				{
//					this.TableConvert(dtout,"会员类型","tbCommCode","vcCommSign='AT'");
					this.TableConvert(dtout,"会员状态","tbCommCode","vcCommSign='AS'");
					this.TableConvert(dtout,"门店","tbCommCode","vcCommSign='MD'");
					dtout.TableName="会员资料清单";
					DataTable dtexcel=dtout.Copy();
					Session["QUERY"] = dtout;
					for(int i=0;i<dtexcel.Rows.Count;i++)
					{
						if(dtexcel.Rows[i][0].ToString().Substring(0,1)!="V")
						{
							dtexcel.Rows[i][0]="'"+dtexcel.Rows[i][0].ToString();
						}
						dtexcel.Rows[i][2]="'"+dtexcel.Rows[i][2].ToString();
//						dtexcel.Rows[i][10]="'"+dtexcel.Rows[i][10].ToString();
					}
					Session["toExcel"]=dtexcel;
					CMSMStruct.LoginStruct ls1=(CMSMStruct.LoginStruct)Session["Login"];
					if(dtout.Rows.Count<=0)
					{
						btnExcel.Enabled=false;
					}
					else
					{
						if(ls1.strLimit=="CL001")
						{
							btnExcel.Enabled=true;
						}
					}
				}
                if (this.CheckAuthority())
                {
                    dtout.Columns.Add("挂失");
                    foreach (DataRow dr in dtout.Rows)
                    {
                        if (dr["会员状态"].ToString() == "正常在用")
                        {
                            dr["挂失"] = "<a href='wfmAssInfoDetail.aspx?OperFlag=0&"
                                + "vcCardId=" + dr["会员卡号"].ToString()
                                + "'>挂失</a>";
                        }
                        else if (dr["会员状态"].ToString() == "挂失")
                        {
                            dr["挂失"] = "<a href='wfmAssInfoDetail.aspx?OperFlag=1&"
                                + "vcCardId=" + dr["会员卡号"].ToString()
                                + "'>解挂</a>";
                        }
                    }
                }
				UcPageView1.MyDataGrid.PageSize = 30;
				DataView dvOut =new DataView(dtout);
				this.UcPageView1.MyDataSource = dvOut;
				this.UcPageView1.BindGrid();
                
			}
			catch(Exception er)
			{
				this.clog.WriteLine(er);
				this.SetErrorMsgPageBydir("查询错误，请重试！");
				return;
			}
		}

//		private void btCreate_Click(object sender, System.EventArgs e)
//		{
//			strCreateBeginDate = Request.Form["txtBegin"].ToString();
//			if(strCreateBeginDate==""||strCreateBeginDate==null)
//			{
//				this.SetErrorMsgPageBydir("时间不能为空，请重新选择时间！");
//				return;
//			}
//			DateTime dtToday=DateTime.Now;
//			string strToday=dtToday.Year.ToString();
//			if(dtToday.Month<10)
//			{
//				strToday=strToday+"0"+dtToday.Month.ToString();
//			}
//			else
//			{
//				strToday=strToday+dtToday.Month.ToString();
//			}
//			if(dtToday.Day<10)
//			{
//				strToday=strToday+"0"+dtToday.Day.ToString();
//			}
//			else
//			{
//				strToday=strToday+dtToday.Day.ToString();
//			}
//			Hashtable htapp=(Hashtable)Application["appconf"];
//			string strcons=(string)htapp["cons"];
//			m1=new BusiComm.Manager(strcons);
//			int todaycount=int.Parse(m1.getAssSerial(strToday));
//			if(!CenterToDept(todaycount,strCreateBeginDate,dtToday,strToday))
//			{
//				this.SetErrorMsgPageBydir("生成会员数据出错，请重试！");
//			}
//			else
//			{
//				this.SetSuccMsgPageBydir("生成会员数据成功！","");
//			}
//		}

//		private void chkCreate_CheckedChanged(object sender, EventArgs e)
//		{
//			if(this.chkCreate.Checked)
//			{
//				this.btCreate.Enabled=true;
//			}
//			else
//			{
//				this.btCreate.Enabled=false;
//			}
//		}

		#region 中心到分店数据生成
		private bool CenterToDept(int lastserial,string strBeginDate,DateTime dtCreateDate,string strCreateDate)
		{
			int maxFileID=lastserial+1;
			string filePath=@"E:\AMSDataSoft\CenterToDept\";
			string downFileName="";
			if(maxFileID<10)
			{
				downFileName="CEN00" + strCreateDate + "0" + maxFileID.ToString() +".L00";
			}
			else
			{
				downFileName="CEN00" + strCreateDate + maxFileID.ToString() +".L00";
			}

			//创建数据
			if(!CreateDataLocal(strBeginDate,filePath+downFileName))
			{
				return false;
			}

			FileInfo file_size=new FileInfo(filePath+downFileName);
			long fsize=file_size.Length;

			DateTime dtFinDate=DateTime.Now;
			string sqlset="'" + downFileName.Trim() + "'," + fsize.ToString() + ",1,'" + dtCreateDate.ToShortDateString() + " " + dtCreateDate.ToLongTimeString() + "','" + dtFinDate.ToShortDateString() + " " + dtFinDate.ToLongTimeString() + "','CEN'";
			if(!m1.writeDataLog(sqlset))
			{
				clog.WriteLine("写数据生成日志失败，但文件已生成！");
			}

			return true;
		}
		#endregion

		#region 创建数据
		private bool CreateDataLocal(string strlasttime,string strdownfiles)
		{	
			#region 加载会员资料
			ArrayList alDownUser=new ArrayList();
			alDownUser=m1.DownAssData(strlasttime);
			if(alDownUser==null)
			{
				clog.WriteLine("加载会员资料错误");
				return false;
			}
			#endregion

			StreamWriter swFile = new StreamWriter(strdownfiles+".tmp",true);

			#region 写会员资料
			CMSMStruct.AssociatorStruct asstmp=null;
			StructToString sts=new StructToString();
			swFile.WriteLine("USERTOL=" + alDownUser.Count.ToString());
			for(int i=0;i<alDownUser.Count;i++)
			{
				asstmp=alDownUser[i] as CMSMStruct.AssociatorStruct;
				swFile.WriteLine(sts.ToAssString(asstmp));
			}
			swFile.WriteLine("END");
			#endregion

			swFile.Close();

			#region 加密
			DESEncryptor dese=new DESEncryptor();
			dese.InputFilePath=strdownfiles+".tmp";
			dese.OutFilePath=strdownfiles;
			dese.EncryptKey="cmsmyykx";
			dese.FileDesEncrypt();
			if(dese.NoteMessage!=null)
			{
				clog.WriteLine(dese.NoteMessage);
				return false;
			}
			dese=null;
			#endregion

			if(System.IO.File.Exists(strdownfiles+".tmp"))
				System.IO.File.Delete(strdownfiles+".tmp");

			return true;
		}
		#endregion


        protected void ddlRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.BindRegion(this.ddlRegion.SelectedValue, this.ddlDept);
        }

	}
}
