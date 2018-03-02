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

namespace AMSApp.paraconf
{
	/// <summary>
	/// Summary description for wfmLoginPwd.
	/// </summary>
	public partial class wfmLoginPwd : wfmBase
	{

		BusiComm.Manager m1;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			if(Session["Login"]==null)
			{
				Response.Redirect("../Exit.aspx");
				return;
			}

			this.txtLoginID.Enabled=false;
			CMSMStruct.LoginStruct ls1=(CMSMStruct.LoginStruct)Session["Login"];
			if(ls1!=null)
			{
				if(ls1.strLoginID!=null&&ls1.strLoginID!="")
				{
					this.txtLoginID.Text=ls1.strLoginID;
				}
				else
				{
					Response.Redirect("../Exit.aspx");
					return;
				}
			}
			else
			{
				Response.Redirect("../Exit.aspx");
				return;
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

		protected void btMod_Click(object sender, System.EventArgs e)
		{
			string strloginid=this.txtLoginID.Text.Trim();
			string strnewpwd=this.txtNewPwd.Text.Trim();
			string strnewpwdconf=this.txtNewPwdConf.Text.Trim();
			if(strnewpwd!=strnewpwdconf)
			{
				this.SetErrorMsgPageBydir("两次输入的密码不正确！");
				return;
			}
			else
			{
				Hashtable htapp=(Hashtable)Application["appconf"];
				string strcons=(string)htapp["cons"];
				m1=new BusiComm.Manager(strcons);
				if(!m1.UpdateOperPwd(strloginid,strnewpwd))
				{
					this.SetErrorMsgPageBydir("修改密码失败，请重试！");
					return;
				}
				else
				{
					this.SetSuccMsgPageBydir("修改密码成功！","wfmWelcome.aspx");
					return;
				}
			}
		}

		protected void btClose_Click(object sender, System.EventArgs e)
		{
			Response.Redirect("../wfmWelcome.aspx");
		}
	}
}
