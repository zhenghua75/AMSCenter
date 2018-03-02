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
using AMSApp.zhenghua.Business;

namespace AMSApp.paraconf
{
	/// <summary>
	/// Summary description for wfmNotice.
	/// </summary>
	public partial class wfmNotice2 : wfmBase
	{


		protected string strEndDate;
		protected string strBeginDate;
		protected string strExcelPath = string.Empty;
		//BusiComm.Manager m1;
	

		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			this.btnExcel.Attributes.Add("onclick","javascript:window.open('../DataGridToExcel.aspx', 'Sample', 'toolbar=no,location=no,directories=no,status=yes,menubar=yes,scrollbars=no,resizable=yes,copyhistory=yes,width=790,height=520,left=0,top=0')");
			if(Session["Login"]==null)
			{
				Response.Redirect("../Exit.aspx");
				return;
			}
			else
			{
				CMSMStruct.LoginStruct ls1=(CMSMStruct.LoginStruct)Session["Login"];
//				if(ls1.strLimit=="CL004")
//				{
//					this.SetErrorMsgPageBydir("�Բ�����û��Ȩ��ʹ�ô˹��ܣ�");
//					return;
//				}
				if(ls1.strLimit!="CL001")
				{
					this.btAdd.Enabled=false;
					btnExcel.Enabled=false;
				}
				if (!IsPostBack )
				{
//					if(ls1.strLimit!="CL001")
//					{
//						this.FillDropDownList("tbCommCode", ddlDept, "vcCommSign ='MD' and vcCommCode='"+ls1.strDeptID+"'");
//					}
//					else
//					{
//						this.FillDropDownList("tbCommCode", ddlDept, "vcCommSign ='MD'","ȫ��");
//					}
					strBeginDate=DateTime.Now.ToShortDateString();
					strEndDate=DateTime.Now.ToShortDateString();
					Session.Remove("QUERY");
					Session.Remove("toExcel");
					Session.Remove("page_view");
				}
				else
				{
					strBeginDate = Request.Form["txtBegin"].ToString();
					strEndDate =  Request.Form["txtEnd"].ToString();
				}
			}		
			if(!this.IsPostBack)
			{
				Session["tbNotice"]=null;
			}
			this.FootBar.Visible = false;
			if(Session["tbNotice"]!=null)
			{
				if(((DataTable)Session["tbNotice"]).Rows.Count>0)
				{
					this.FootBar.Visible = true;
				}

			}
			if(DataGrid1.DataSource!=null)
			{
				if(((DataTable)DataGrid1.DataSource).Rows.Count>0)
				{
					this.FootBar.Visible = true;
				}
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
			this.DataGrid1.ItemCreated += new System.Web.UI.WebControls.DataGridItemEventHandler(this.DataGrid1_ItemCreated);
			this.DataGrid1.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DataGrid1_PageIndexChanged);
			this.DataGrid1.CancelCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGrid1_CancelCommand);
			this.DataGrid1.EditCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGrid1_EditCommand);
			this.DataGrid1.UpdateCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGrid1_UpdateCommand);
			this.DataGrid1.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.DataGrid1_ItemDataBound);

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

			//Hashtable htapp=(Hashtable)Application["appconf"];
			//string strcons=(string)htapp["cons"];
			//m1=new BusiComm.Manager(strcons);

			//Hashtable htPara=new Hashtable();
			///string strDeptID=ddlDept.SelectedValue;
			//if(strDeptID=="ȫ��")
			//{
			//	strDeptID="";
			//}
//			CMSMStruct.LoginStruct ls1=(CMSMStruct.LoginStruct)Session["Login"];
//			if(ls1.strLimit!="CL001")
//			{
//				strDeptID=ls1.strDeptID+"','all";
//			}
//			htPara.Add("strDeptID",strDeptID);
//			htPara.Add("strBegin",strBeginDate);
//			htPara.Add("strEnd",strEndDate);
//			htPara.Add("strContent",txtContent.Text.Trim());
			
			try
			{
				//DataTable dtout=m1.GetNotice(htPara);
				DataTable dtout = Helper.Query("select cnnNoticeID,cnvcComments,Convert(varchar(10),cndReleaseDate,21) as cndReleaseDate,cndInvalidDate,cnvcIsActive from tbNotice where cndInvalidDate>='"+strBeginDate+"' and cndInvalidDate<='"+strEndDate+" 23:59:59'");//(DataTable)Session["tbNotice"];
				Session["tbNotice"] = dtout;
				if(dtout==null)
				{
					this.SetErrorMsgPageBydir("��ѯ���������ԣ�");
					btnExcel.Enabled=false;
					return;
				}
				else
				{
					//this.TableConvert(dtout,"�����ŵ�","tbCommCode","vcCommSign='MD'");
					dtout.TableName="ϵͳ֪ͨ�嵥";
					DataTable dtexcel=dtout.Copy();
					Session["QUERY"] = dtout;
					//dtexcel.Columns.Remove("����");
					Session["toExcel"]=dtexcel;
					if(dtout.Rows.Count<=0)
					{
						btnExcel.Enabled=false;
					}
//					else
//					{
//						if(ls1.strLimit=="CL001")
//						{
//							btnExcel.Enabled=true;
//						}
//					}
				}

				//UcPageView1.MyDataGrid.PageSize = 30;
				//DataView dvOut =new DataView(dtout);
				//this.UcPageView1.MyDataSource = dvOut;
				//this.UcPageView1.BindGrid();
				BindGrid();
				
			}
			catch(Exception er)
			{
				this.clog.WriteLine(er);
				this.SetErrorMsgPageBydir("��ѯ���������ԣ�");
				return;
			}
		}

		private void BindGrid()
		{
			int iRecordCount = 0;
			if(Session["tbNotice"] !=null)
			{
				DataTable dtout = (DataTable)Session["tbNotice"];
				iRecordCount = dtout.Rows.Count;
				this.DataGrid1.DataSource = dtout;
				this.DataGrid1.DataBind();
			}
			if(iRecordCount>0)
			{
				FootBar.Visible = true;
			}
			else
			{
				FootBar.Visible = false;
			}		
			ShowPageLabel(lbPageLabel,iRecordCount);	
		}

		protected void btAdd_Click(object sender, System.EventArgs e)
		{
			this.RedirectPage("wfmNoticeDetail2.aspx");
		}

		protected void DataGrid1_SelectedIndexChanged(object sender, System.EventArgs e)
		{

		}

		private void DataGrid1_EditCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			this.DataGrid1.EditItemIndex = e.Item.ItemIndex;
			BindGrid();
		}

		private void DataGrid1_CancelCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			this.DataGrid1.EditItemIndex = -1;
			BindGrid();
		}

		private void DataGrid1_UpdateCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			//����
			string strID = e.Item.Cells[0].Text;
			string strComments = ((TextBox)e.Item.FindControl("txtComments")).Text;
			string strInvalidDate = ((TextBox)e.Item.FindControl("txtInvalidDate")).Text;
			string strIsActive1 = ((DropDownList)e.Item.FindControl("ddlIsActive")).SelectedValue;
			string strIsActive = "";
			if(strIsActive1=="��Ч")
				strIsActive="1";
			else
				strIsActive="0";
			string strsql = "update tbNotice set cnvcComments='"+strComments+"',cndInvalidDate='"+strInvalidDate+"',cnvcIsActive='"+strIsActive+"' where cnnNoticeID="+strID;
			Helper.ExcuteNoQuery(strsql);
			this.Popup("ϵͳ֪ͨ���³ɹ�");
			BindGrid();
			this.DataGrid1.EditItemIndex = -1;
			btQuery_Click(null,null);
		}
		public string ReturnIsAcitve(string strIsActive)
		{
			if(strIsActive == "1")
				return "��Ч";
			else
				return "ʧЧ";
		}
		private void DataGrid1_ItemCreated(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			
		}

		private void DataGrid1_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			switch (e.Item.ItemType)
			{
				case ListItemType.EditItem:
					for (int i=4; i < DataGrid1.Columns.Count-2; i++)//ֻ�������༭����
					{
����                    if(e.Item.ItemType==ListItemType.EditItem)
����                    {
������                        e.Item.Cells[i].Attributes.Add("Width", "70px");
����                    }
					}
					
					if(e.Item.ItemType==ListItemType.EditItem)
					{
						DropDownList ddlIsActive=(DropDownList)e.Item.FindControl("ddlIsActive");
						ListItem li1 = new ListItem("��Ч","��Ч");
						ListItem li0 = new ListItem("ʧЧ","ʧЧ");
						ddlIsActive.Items.Add(li0);
						ddlIsActive.Items.Add(li1);
						string strIsActive = Convert.ToString(DataBinder.Eval(e.Item.DataItem,"cnvcIsActive"));
						if(strIsActive == "1")
							ddlIsActive.Items.FindByValue("��Ч").Selected=true;
						else
							ddlIsActive.Items.FindByValue("ʧЧ").Selected=true;
					}
					break;
				default:
					break;
			}            
		}

		private void DataGrid1_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			//��ҳ
			this.DataGrid1.CurrentPageIndex=e.NewPageIndex;
			BindGrid();
		}
		protected void SetDataGridCurrentPageIndex(DataGrid myDataGrid,string strArg)
		{
			switch(strArg)
			{
				case ("next"):
					if (DataGrid1.CurrentPageIndex < (myDataGrid.PageCount - 1))
						DataGrid1.CurrentPageIndex ++;
					break;
				case ("prev"):
					if (DataGrid1.CurrentPageIndex > 0)
						DataGrid1.CurrentPageIndex --;
					break;
				case ("last"):
					DataGrid1.CurrentPageIndex = (myDataGrid.PageCount - 1);
					break;
				case ("jump"):
					int iTempIndex = Convert.ToInt16(Request["page_number"])-1;//PageNumber.Value)-1;
					if(iTempIndex > DataGrid1.PageCount-1)
						iTempIndex = DataGrid1.PageCount-1;
					if(iTempIndex < 0)
						iTempIndex = 0;
					DataGrid1.CurrentPageIndex = iTempIndex;
					break;
				default:
					//page number
					DataGrid1.CurrentPageIndex = Convert.ToInt32(strArg);
					break;
			}			
		}	
		protected void PagerButtonClick(Object sender, EventArgs e) 
		{
			//used by external paging UI
			String arg = ((LinkButton)sender).CommandArgument;
			SetDataGridCurrentPageIndex(DataGrid1,arg);
			BindGrid();
		}
		public void ShowPageLabel(Label myLable,int iRecordCount) 
		{
			myLable.Text = "�� " + (DataGrid1.CurrentPageIndex+1) +" ҳ/�� " + DataGrid1.PageCount+" ҳ����"+iRecordCount+"����¼";
		}

	}
}
