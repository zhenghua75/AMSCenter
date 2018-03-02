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
using System.Management;
using AMSApp.zhenghua.Business;

namespace AMSApp
{
	/// <summary>
	/// Summary description for _default.
	/// </summary>
	public partial class _default : System.Web.UI.Page
	{
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtMACAddr;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtIPAddr;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtDNSName;
		//Manager m1;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here 
//			DataTable dttmp=(DataTable)Application["MAC"];
//			if(dttmp!=null||dttmp.Rows.Count>0)
//			{
//				bool okflag=false;
//				ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
//				ManagementObjectCollection moc2 = mc.GetInstances();
//				foreach(ManagementObject mo in moc2)
//				{
//					if((bool)mo["IPEnabled"] == true)
//					{
//						for(int i=0;i<dttmp.Rows.Count;i++)
//						{
//							if(dttmp.Rows[i][0].ToString()==mo["MacAddress"].ToString())
//							{
//								okflag=true;
//								mo.Dispose();
//								break;
//							}
//							mo.Dispose();
//						}
//					}
//					if(okflag)
//					{
//						break;
//					}
//				}
//
//				if(!okflag)
//				{
//					Response.Redirect("sorry.htm");
//					return;
//				}
//			}
//			else
//			{
//				Response.Redirect("sorry.htm");
//				return;
//			}
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


		//根据错误信息重定向到错误页面，根目录
		public void SetErrorMsgPage(string strMsg)
		{			
			Session["CommMsg"]=strMsg;
			Response.Redirect("wfmFalse.aspx",false);
		}

		protected void Button1_Click(object sender, System.EventArgs e)
		{
			string strLoginid=this.txtLoginID.Text.Trim();
			if(strLoginid=="admin" || strLoginid=="orange")
			{
				string strpwd=this.txtPwd.Text.Trim();
				if(strLoginid==""||strpwd=="")
				{
					this.SetErrorMsgPage("请输入用户名和密码！");
				}
				else
				{
					Hashtable htapp=(Hashtable)Application["appconf"];
					string strcons=(string)htapp["cons"];
					Manager m1=new Manager(strcons);
					CMSMStruct.LoginStruct ls1=m1.GetLoginInfo(strLoginid);
					if(ls1==null)
					{
						this.SetErrorMsgPage("对不起，用户不存在！");
						Session["Login"]=null;
					}
					else
					{
						if(ls1.strPwd!=strpwd)
						{
							this.SetErrorMsgPage("对不起，密码不正确！");
						}
						else
						{
							DataTable dtDeptMap=(DataTable)Application["DeptMapInfo"];
							foreach(DataRow dr in dtDeptMap.Rows)
							{
								if(dr["cnvcOldDeptID"].ToString()==ls1.strDeptID)
								{
									ls1.strNewDeptID=dr["cnvcNewDeptID"].ToString();
									break;
								}
							}
							CMSMStruct.OperStruct OperNew=new CMSMStruct.OperStruct();
							OperNew.strDeptID=ls1.strDeptID;
							OperNew.strOperID=ls1.strLoginID;
							OperNew.strMacAddress=this.Request.Form["txtMACAddr"].ToString();
							m1.InsertOperLog(OperNew);
							Session["Login"]=ls1;
							//Session["tbNotice"] = Helper.Query("select cnnNoticeID,cnvcComments,Convert(varchar(10),cndReleaseDate,21) as cndReleaseDate from tbNotice where cnvcIsActive ='1'");
							Response.Redirect("wfmMain.aspx",false);
						}
					}
				}
			}
			else
			{
				try
				{
					DataTable dtMac=(DataTable)Application["MAC"];
					if(dtMac==null || dtMac.Rows.Count==0 )
					{
						Response.Redirect("sorry.htm");
						return;
					
					}
					else
					{
						string strmac=this.Request.Form["txtMACAddr"].ToString();
                    
						AMSLog clog=new AMSLog();
						clog.WriteLine("LoginID:"+strLoginid+";    Mac:"+strmac+";");
						
						if(strmac=="")
						{
						
							Response.Redirect("sorry.htm");
							return;
						}

						else
						{
							bool okflag=false;
							if(strLoginid=="admin")
							{
								okflag=true;
							}
							else
							{
								for(int i=0;i<dtMac.Rows.Count;i++)
								{
									if(dtMac.Rows[i][0].ToString()==strmac)
									{
										okflag=true;
										break;
									}
								}
							}
							if(!okflag)
							{
								Response.Redirect("nopromexplor.htm");
								return;
							}
							else
							{
								//							string strLoginid=this.txtLoginID.Text.Trim();
								string strpwd=this.txtPwd.Text.Trim();
								if(strLoginid==""||strpwd=="")
								{
									this.SetErrorMsgPage("请输入用户名和密码！");
								}
								else
								{
									Hashtable htapp=(Hashtable)Application["appconf"];
									string strcons=(string)htapp["cons"];
									Manager m1=new Manager(strcons);
									CMSMStruct.LoginStruct ls1=m1.GetLoginInfo(strLoginid);
									if(ls1==null)
									{
										this.SetErrorMsgPage("对不起，用户不存在！");
										Session["Login"]=null;
									}
									else
									{
										if(ls1.strPwd!=strpwd)
										{
											this.SetErrorMsgPage("对不起，密码不正确！");
										}
										else
										{
											DataTable dtDeptMap=(DataTable)Application["DeptMapInfo"];
											foreach(DataRow dr in dtDeptMap.Rows)
											{
												if(dr["cnvcOldDeptID"].ToString()==ls1.strDeptID)
												{
													ls1.strNewDeptID=dr["cnvcNewDeptID"].ToString();
													break;
												}
											}
											CMSMStruct.OperStruct OperNew=new CMSMStruct.OperStruct();
											OperNew.strDeptID=ls1.strDeptID;
											OperNew.strOperID=ls1.strLoginID;
											OperNew.strMacAddress=this.Request.Form["txtMACAddr"].ToString();
											m1.InsertOperLog(OperNew);
											Session["Login"]=ls1;
											//Session["tbNotice"] = Helper.Query("select cnnNoticeID,cnvcComments,Convert(varchar(10),cndReleaseDate,21) as cndReleaseDate from tbNotice where cnvcIsActive ='1'");
											Response.Redirect("wfmMain.aspx",false);
										}
									}
								}
							}
						}
					}
				}
				catch(Exception er)
				{
					AMSLog clog=new AMSLog();
					clog.WriteLine(er);
					Response.Redirect("sorry.htm");
				}
			}
		}
	}
}
