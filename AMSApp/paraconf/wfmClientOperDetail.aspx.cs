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
	/// wfmClientOperDetail 的摘要说明。
	/// </summary>
	public partial class wfmClientOperDetail : wfmBase
	{
		Manager m1;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
			if(Session["Login"]!=null)
			{
				Hashtable htapp=(Hashtable)Application["appconf"];
				string strcons=(string)htapp["cons"];
				m1=new Manager(strcons);
				this.btnFreeze_.Enabled=false;

				CMSMStruct.LoginStruct ls1=(CMSMStruct.LoginStruct)Session["Login"];
				string strid=Request.QueryString["id"];

				if (!IsPostBack )
				{
					Session.Remove("coperold");
					Session.Remove("copernew");
					this.FillDropDownList("AllMD", ddlDept,"vcCommSign='MD' and vcCommCode not in('CEN00','FYZX1')");
					if(ls1.strDeptID!="CEN00"&&ls1.strDeptID!="FYZX1")
					{
						ddlDept.Items.FindByValue(ls1.strDeptID).Selected=true;
						ddlDept.Enabled=false;
					}
					this.FillDropDownList("tbCommCode", ddlLimit,"vcCommSign='LM'");
					if(strid==""||strid==null)
					{
						this.btAdd.Enabled=true;
						this.btPwdBegin.Enabled=false;
						this.btMod.Enabled=false;
						this.btnFreeze.Enabled=false;
						lbltitle.Text="新客户端操作员录入";
					}
					else
					{
						this.btAdd.Enabled=false;
						CMSMStruct.ClientOperStruct coperold=m1.GetClientOperInfo(strid);
						this.txtOperID.Text=coperold.strOperID;
						this.txtOperName.Text=coperold.strOperName;
						ddlLimit.Items.FindByValue(coperold.strLimit).Selected=true;
						if(coperold.strDeptID=="*")
						{
							ddlDept.Items.Clear();
							ddlDept.Items.Add("所有门店");
							ddlDept.Enabled=false;
							this.btPwdBegin.Enabled=false;
							this.txtOperName.Enabled=false;
							this.ddlLimit.Enabled=false;
							this.btMod.Enabled=false;
						}
						else
							ddlDept.Items.FindByValue(coperold.strDeptID).Selected=true;
						if(ls1.strLimit!="CL001")
						{
							ddlLimit.Enabled=false;
							ddlDept.Enabled=false;
						}
						if(coperold.strActiveFlag!="1")
						{
							this.btnFreeze.Enabled=false;
							this.btMod.Enabled=false;
							this.btPwdBegin.Enabled=false;
							this.txtOperName.Enabled=false;
							this.ddlDept.Enabled=false;
							this.ddlLimit.Enabled=false;
						}
						if(coperold.strActiveFlag=="0")
						{
							this.btnFreeze_.Enabled=true;
						}
						lbltitle.Text="客户端操作员修改";
						txtOperID.Enabled=false;
						Session["coperold"]=coperold;
					}
				}
			}
			else
			{
				Response.Redirect("../Exit.aspx");
			}
		}

		#region Web 窗体设计器生成的代码
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: 该调用是 ASP.NET Web 窗体设计器所必需的。
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{    

		}
		#endregion

		protected void btAdd_Click(object sender, System.EventArgs e)
		{
			CMSMStruct.ClientOperStruct copernew=new CMSMStruct.ClientOperStruct();
			copernew.strDeptID=ddlDept.SelectedValue;
			if (this.txtOperID.Text.Length>9)
			{
				this.SetErrorMsgPageBydirHistory("客户端操作员ID不能大于9个数字长度！");
				return;
			}
			if(txtOperID.Text.Trim()=="")
			{
				this.SetErrorMsgPageBydirHistory("客户端操作员编号不能为空！");
				return;
			}
			else if(txtOperID.Text.Trim()=="admin"||txtOperID.Text.Trim()=="系统管理员")
			{
				this.SetErrorMsgPageBydirHistory("admin或系统管理员为关键词，不允许添加此类操作员！");
				return;
			}
			else if(m1.ChkClientOperIDDup(txtOperID.Text.Trim()))
			{
				copernew.strOperID=txtOperID.Text.Trim();
			}
			else
			{
				this.SetErrorMsgPageBydirHistory("该客户端操作员编号已经存在，请重新输入！");
				return;	
			}

			if(txtOperName.Text.Trim()=="")
			{
				this.SetErrorMsgPageBydirHistory("客户端操作员名称不能为空！");
				return;
			}
			else if(txtOperName.Text.Trim()=="admin"||txtOperName.Text.Trim()=="系统管理员")
			{
				this.SetErrorMsgPageBydirHistory("admin或系统管理员为关键词，不允许添加此类操作员！");
				return;
			}
			else if(m1.ChkClientOperNameDup(txtOperName.Text.Trim(),copernew.strDeptID))
			{
				copernew.strOperName=txtOperName.Text.Trim();
			}
			else
			{
				this.SetErrorMsgPageBydirHistory("该客户端操作员名称已经存在，请重新输入！");
				return;	
			}

			copernew.strLimit=ddlLimit.SelectedValue;

			if(!m1.InsertClientOper(copernew))
			{
				this.SetErrorMsgPageBydir("添加客户端操作员失败，请重试！");
				return;
			}
			else
			{
				this.SetSuccMsgPageBydir("添加客户端操作员成功！","");
				return;
			}
		}

		protected void btMod_Click(object sender, System.EventArgs e)
		{
			CMSMStruct.ClientOperStruct coperold=(CMSMStruct.ClientOperStruct)Session["coperold"];
			if(coperold.strOperID!=txtOperID.Text.Trim())
			{
				this.SetErrorMsgPageBydir("保存失败，请重试！");
				return;
			}

			CMSMStruct.ClientOperStruct copernew=new CMSMStruct.ClientOperStruct();
			copernew.strDeptID=ddlDept.SelectedValue;
			copernew.strOperID=coperold.strOperID;
			copernew.strOperName=txtOperName.Text.Trim();
			if(copernew.strOperName=="")
			{
				this.SetErrorMsgPageBydirHistory("客户端操作员名称不能为空！");
				return;
			}
			else if(copernew.strOperName=="admin"||copernew.strOperName=="系统管理员")
			{
				this.SetErrorMsgPageBydirHistory("admin或系统管理员为关键词，不允许添加此类操作员！");
				return;
			}
//			else if(!m1.ChkClientOperNameDup(copernew.strOperName,copernew.strDeptID))
//			{
//				this.SetErrorMsgPageBydirHistory("该客户端操作员名称已经存在，请重新输入！");
//				return;	
//			}

			copernew.strLimit=ddlLimit.SelectedValue;

			if(!m1.UpdateClientOper(copernew,coperold))
			{
				this.SetErrorMsgPageBydir("修改客户端操作员失败，请重试！");
				return;
			}
			else
			{
				this.SetSuccMsgPageBydir("修改客户端操作员成功！","paraconf/wfmDeptOperManage.aspx");
				return;
			}
		}

		protected void btPwdBegin_Click(object sender, System.EventArgs e)
		{
			string strOperID=txtOperID.Text.Trim();
			if(strOperID=="")
			{
				this.SetErrorMsgPageBydirHistory("客户端操作员编号有误，不能为空！");
				return;
			}

			if(!m1.UpdateClientOperPwdBegin(strOperID))
			{
				this.SetErrorMsgPageBydir("客户端操作员密码初始化失败，请重试！");
				return;
			}
			else
			{
				this.SetSuccMsgPageBydir("客户端操作员密码已经初始化为：000000，需等待客户端数据同步后，才可以使用！","paraconf/wfmDeptOperManage.aspx");
				return;
			}
		}

		protected void btcancel_Click(object sender, System.EventArgs e)
		{
			this.RedirectPage("wfmDeptOperManage.aspx");
		}

		protected void btnFreeze_Click(object sender, System.EventArgs e)
		{
			string strOperID=txtOperID.Text.Trim();
			string strState="1";
			if(strOperID=="")
			{
				this.SetErrorMsgPageBydirHistory("客户端操作员编号有误，不能为空！");
				return;
			}

			if(!m1.UpdateClientOperFreeze(strOperID,strState))
			{
				this.SetErrorMsgPageBydir("客户端操作员冻结失败，请重试！");
				return;
			}
			else
			{
				this.SetSuccMsgPageBydir("客户端操作员冻结成功，需等待客户端数据同步，才有效！","paraconf/wfmDeptOperManage.aspx");
				return;
			}
		}

		protected void btnFreeze__Click(object sender, System.EventArgs e)
		{
			string strOperID=txtOperID.Text.Trim();
			string strState="2";
			if(strOperID=="")
			{
				this.SetErrorMsgPageBydirHistory("客户端操作员编号有误，不能为空！");
				return;
			}

			if(!m1.UpdateClientOperFreeze(strOperID,strState))
			{
				this.SetErrorMsgPageBydir("客户端操作员解冻失败，请重试！");
				return;
			}
			else
			{
				this.SetSuccMsgPageBydir("客户端操作员解冻成功，需等待客户端数据同步，才可以使用！","paraconf/wfmDeptOperManage.aspx");
				return;
			}
		}
	}
}
