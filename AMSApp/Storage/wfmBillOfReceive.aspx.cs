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
using System.Text;
using CommCenter;

namespace AMSApp.Storage
{
	/// <summary>
	/// Summary description for wfmBillOfReceive.
	/// </summary>
	public partial class wfmBillOfReceive : wfmBase
	{

		protected string strEndDate;
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
					this.FillDropDownList("tbNameCodeToStorage", this.ddlOrderState,"vcCommSign='RECEIVE_OS'","全部");
					this.FillDropDownList("NewDept", this.ddlReceiveDept,"","全部");
//					this.FillDropDownList("tbNameCodeToStorage", this.ddlGroup,"vcCommSign='GROUP'","全部");
					if(ls1.strDeptID!="CEN00"&&ls1.strLimit!="CL001")
					{
						this.ddlReceiveDept.SelectedIndex=this.ddlReceiveDept.Items.IndexOf(this.ddlReceiveDept.Items.FindByValue(ls1.strNewDeptID));
						this.ddlReceiveDept.Enabled=false;
					}
					strBeginDate=DateTime.Now.ToShortDateString();
					strEndDate=DateTime.Now.ToShortDateString();
					Session.Remove("QUERY");
					Session.Remove("page_view");
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
			this.DataGrid1.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DataGrid1_PageIndexChanged);

		}
		#endregion

		protected void btnQuery_Click(object sender, System.EventArgs e)
		{
			Session.Remove("Query");
			strBeginDate = Request.Form["txtBegin"].ToString();
			strEndDate =  Request.Form["txtEnd"].ToString();
			if(strBeginDate==""||strEndDate==""||strBeginDate==null||strEndDate==null)
			{
				this.SetErrorMsgPageBydir("时间不能为空，请重新选择时间！");
				return;
			}

			string strDeptID=this.ddlReceiveDept.SelectedValue;
			if(strDeptID=="")
			{
				this.SetErrorMsgPageBydir("领用单位不能为空！");
				return;
			}
//			string strGroup=this.ddlGroup.SelectedValue;
//			if(strGroup=="")
//			{
//				this.SetErrorMsgPageBydir("生产组不能为空！");
//				return;
//			}

			Hashtable htPara=new Hashtable();
			htPara.Add("strDeptID",strDeptID);
//			htPara.Add("strGroup",strGroup);
			htPara.Add("strBeginDate",strBeginDate);
			htPara.Add("strEndDate",strEndDate);
			htPara.Add("strOrderState",this.ddlOrderState.SelectedValue);
			
			Hashtable htapp=(Hashtable)Application["appconf"];
			string strcons=(string)htapp["cons"];
			StoBusi=new BusiComm.StorageBusi(strcons);
			try
			{
				DataTable dtout=StoBusi.GetBillOfReceive(htPara);
				if(dtout==null)
				{
					this.SetErrorMsgPageBydir("查询出错，请重试！");
					return;
				}
				else
				{
					this.TableConvert(dtout,"cnvcReceiveDeptID","NewDept");
					this.TableConvert(dtout,"cnvcGroup","tbNameCodeToStorage","vcCommSign='GROUP'");
					this.TableConvert(dtout,"cnvcBillState","tbNameCodeToStorage","vcCommSign='RECEIVE_OS'");
					dtout.TableName="领料单";
				}
				
				Session["Query"]=dtout;
				this.DataGrid1.DataSource = dtout;
				this.DataGrid1.DataBind();
			}
			catch(Exception er)
			{
				this.clog.WriteLine(er);
				this.SetErrorMsgPageBydir("查询错误，请重试！");
				return;
			}
		}

		protected void btnAdd_Click(object sender, System.EventArgs e)
		{
			Response.Redirect("wfmBillOfReceiveDetail.aspx");
		}

		protected void Button1_Click(object sender, System.EventArgs e)
		{
			Response.Redirect("wfmMakeBillRelativeReceive.aspx");
		}

		private void DataGrid1_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
		{
			this.DataGrid1.CurrentPageIndex = e.NewPageIndex;
			this.DataGrid1.DataSource = (DataTable)Session["Query"];
			this.DataGrid1.DataBind();
		}

		protected void btnSend_Click(object sender, System.EventArgs e)
		{
			Response.Redirect("wfmBillOfReceiveSend.aspx");
		}

		protected void btnPrint_Click(object sender, System.EventArgs e)
		{
			Response.Redirect("wfmPrintReceiveSend.aspx");
		}
	}
}
