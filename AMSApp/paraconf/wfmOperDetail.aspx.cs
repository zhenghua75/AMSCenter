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
using CommCenter;
using BusiComm;

namespace AMSApp.paraconf
{
	/// <summary>
	/// Summary description for wfmOperDetail.
	/// </summary>
	public partial class wfmOperDetail : wfmBase
	{
		Manager m1;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			if(Session["Login"]==null)
			{
				Response.Redirect("../Exit.aspx");
				return;
			}

			this.btDel.Attributes.Add("onclick","javascript:return window.confirm('你确定要删除吗？')");   

			string strid=Request.QueryString["id"];
			Hashtable htapp=(Hashtable)Application["appconf"];
			string strcons=(string)htapp["cons"];
			m1=new Manager(strcons);

			CMSMStruct.LoginStruct ls1=(CMSMStruct.LoginStruct)Session["Login"];
//			if(ls1.strLimit=="CL003")
//			{
//				this.SetErrorMsgPageBydir("对不起，你没有权限使用此功能！");
//				return;
//			}

			if(!IsPostBack)
			{
				Session.Remove("lsold");
				Session.Remove("lsnew");
				this.FillDropDownList("AllMD", ddlDept,"vcCommSign='MD'");
				if(ls1.strLimit=="CL002")
				{
					ddlDept.Items.FindByValue(ls1.strDeptID).Selected=true;
					ddlDept.Enabled=false;
				}

				this.FillDropDownList("tbCommCode", ddlLimit,"vcCommSign='CLT'");
				if(strid==""||strid==null)
				{
					this.btAdd.Enabled=true;
					this.btDel.Enabled=false;
					this.btMod.Enabled=false;
					this.btnInitPwd.Enabled=false;
					lbltitle.Text="新网站操作员录入";
				}
				else
				{
					this.btAdd.Enabled=false;
					this.btDel.Enabled=true;
					this.btMod.Enabled=true;
					this.btnInitPwd.Enabled=true;
					CMSMStruct.LoginStruct lsold=m1.GetLoginInfo(strid);
					txtLoginID.Text=lsold.strLoginID;
					txtOperName.Text=lsold.strOperName;
					ddlLimit.Items.FindByValue(lsold.strLimit).Selected=true;
					ddlDept.Items.FindByValue(lsold.strDeptID).Selected=true;
					if(ls1.strLimit!="CL001")
					{
						ddlLimit.Enabled=false;
						ddlDept.Enabled=false;
					}
					lbltitle.Text="网站操作员修改删除";
					txtLoginID.Enabled=false;
					Session["lsold"]=lsold;
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

		}
		#endregion

		protected void btAdd_Click(object sender, System.EventArgs e)
		{
			CMSMStruct.LoginStruct lsnew=new CMSMStruct.LoginStruct();
			if(txtLoginID.Text.Trim()=="")
			{
				this.SetErrorMsgPageBydir("登录ID不能为空！");
				return;
			}
			else if(m1.ChkLoginIDDup(txtLoginID.Text.Trim()))
			{
				lsnew.strLoginID=txtLoginID.Text.Trim();
			}
			else
			{
				this.SetErrorMsgPageBydir("该登录ID已经存在，请重新输入！");
				return;	
			}

			if(txtOperName.Text.Trim()=="")
			{
				this.SetErrorMsgPageBydir("操作员名称不能为空！");
				return;
			}
			else if(m1.ChkOperNameDup(txtOperName.Text.Trim()))
			{
				lsnew.strOperName=txtOperName.Text.Trim();
			}
			else
			{
				this.SetErrorMsgPageBydir("该操作员名称已经存在，请重新输入！");
				return;	
			}

			lsnew.strLimit=ddlLimit.SelectedValue;
			lsnew.strDeptID=ddlDept.SelectedValue;

			if(!m1.InsertLogin(lsnew))
			{
				this.SetErrorMsgPageBydir("添加网站操作员失败，请重试！");
				return;
			}
			else
			{
				this.SetSuccMsgPageBydir("添加网站操作员成功！","");
				return;
			}
		}

		protected void btMod_Click(object sender, System.EventArgs e)
		{
			CMSMStruct.LoginStruct lsold=(CMSMStruct.LoginStruct)Session["lsold"];
			if(lsold.strLoginID!=txtLoginID.Text.Trim())
			{
				this.SetErrorMsgPageBydir("保存失败，请重试！");
				return;
			}

			CMSMStruct.LoginStruct lsnew=new CMSMStruct.LoginStruct();
			lsnew.strLoginID=lsold.strLoginID;
			if(txtOperName.Text.Trim()=="")
			{
				this.SetErrorMsgPageBydir("操作员名称不能为空！");
				return;
			}
			else if(txtOperName.Text.Trim()==lsold.strOperName)
			{
				lsnew.strOperName=txtOperName.Text.Trim();
			}
			else if(m1.ChkOperNameDup(txtOperName.Text.Trim()))
			{
				lsnew.strOperName=txtOperName.Text.Trim();
			}
			else
			{
				this.SetErrorMsgPageBydir("该操作员名称已经存在，请重新输入！");
				return;	
			}

			lsnew.strLimit=ddlLimit.SelectedValue;
			lsnew.strDeptID=ddlDept.SelectedValue;

			if(!m1.UpdateLogin(lsnew,lsold))
			{
				this.SetErrorMsgPageBydir("添加网站操作员失败，请重试！");
				return;
			}
			else
			{
				this.SetSuccMsgPageBydir("添加网站操作员成功！","");
				return;
			}
		}

		protected void btDel_Click(object sender, System.EventArgs e)
		{
			CMSMStruct.LoginStruct lsold=(CMSMStruct.LoginStruct)Session["lsold"];
			if(lsold.strLoginID==null||lsold.strLoginID=="")
			{
				this.SetErrorMsgPageBydir("获取操作员错误！");
				return;
			}
			if(m1.DeleteLogin(lsold.strLoginID))
			{
				this.SetErrorMsgPageBydir("删除操作员成功！");
				return;
			}
			else
			{
				this.SetErrorMsgPageBydir("删除操作员失败！");
				return;
			}			
		}

		protected void btcancel_Click(object sender, System.EventArgs e)
		{
			this.RedirectPage("wfmLoginOper.aspx");
		}

		protected void btnInitPwd_Click(object sender, System.EventArgs e)
		{
			//密码初始化
			string strloginid=this.txtLoginID.Text.Trim();
			string strnewpwd="123456";//this.txtNewPwd.Text.Trim();
			//string strnewpwdconf=this.txtNewPwdConf.Text.Trim();
//			if(strnewpwd!=strnewpwdconf)
//			{
//				this.SetErrorMsgPageBydir("两次输入的密码不正确！");
//				return;
//			}
//			else
//			{
				Hashtable htapp=(Hashtable)Application["appconf"];
				string strcons=(string)htapp["cons"];
				m1=new BusiComm.Manager(strcons);
				if(!m1.UpdateOperPwd(strloginid,strnewpwd))
				{
					this.SetErrorMsgPageBydir("密码初始化失败，请重试！");
					return;
				}
				else
				{
					this.SetSuccMsgPageBydir("密码初始化成功！","wfmWelcome.aspx");
					return;
				}
			//}
		}
	}
}
