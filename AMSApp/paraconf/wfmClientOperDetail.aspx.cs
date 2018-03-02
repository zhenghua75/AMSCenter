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
	/// wfmClientOperDetail ��ժҪ˵����
	/// </summary>
	public partial class wfmClientOperDetail : wfmBase
	{
		Manager m1;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// �ڴ˴������û������Գ�ʼ��ҳ��
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
						lbltitle.Text="�¿ͻ��˲���Ա¼��";
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
							ddlDept.Items.Add("�����ŵ�");
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
						lbltitle.Text="�ͻ��˲���Ա�޸�";
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

		protected void btAdd_Click(object sender, System.EventArgs e)
		{
			CMSMStruct.ClientOperStruct copernew=new CMSMStruct.ClientOperStruct();
			copernew.strDeptID=ddlDept.SelectedValue;
			if (this.txtOperID.Text.Length>9)
			{
				this.SetErrorMsgPageBydirHistory("�ͻ��˲���ԱID���ܴ���9�����ֳ��ȣ�");
				return;
			}
			if(txtOperID.Text.Trim()=="")
			{
				this.SetErrorMsgPageBydirHistory("�ͻ��˲���Ա��Ų���Ϊ�գ�");
				return;
			}
			else if(txtOperID.Text.Trim()=="admin"||txtOperID.Text.Trim()=="ϵͳ����Ա")
			{
				this.SetErrorMsgPageBydirHistory("admin��ϵͳ����ԱΪ�ؼ��ʣ���������Ӵ������Ա��");
				return;
			}
			else if(m1.ChkClientOperIDDup(txtOperID.Text.Trim()))
			{
				copernew.strOperID=txtOperID.Text.Trim();
			}
			else
			{
				this.SetErrorMsgPageBydirHistory("�ÿͻ��˲���Ա����Ѿ����ڣ����������룡");
				return;	
			}

			if(txtOperName.Text.Trim()=="")
			{
				this.SetErrorMsgPageBydirHistory("�ͻ��˲���Ա���Ʋ���Ϊ�գ�");
				return;
			}
			else if(txtOperName.Text.Trim()=="admin"||txtOperName.Text.Trim()=="ϵͳ����Ա")
			{
				this.SetErrorMsgPageBydirHistory("admin��ϵͳ����ԱΪ�ؼ��ʣ���������Ӵ������Ա��");
				return;
			}
			else if(m1.ChkClientOperNameDup(txtOperName.Text.Trim(),copernew.strDeptID))
			{
				copernew.strOperName=txtOperName.Text.Trim();
			}
			else
			{
				this.SetErrorMsgPageBydirHistory("�ÿͻ��˲���Ա�����Ѿ����ڣ����������룡");
				return;	
			}

			copernew.strLimit=ddlLimit.SelectedValue;

			if(!m1.InsertClientOper(copernew))
			{
				this.SetErrorMsgPageBydir("��ӿͻ��˲���Աʧ�ܣ������ԣ�");
				return;
			}
			else
			{
				this.SetSuccMsgPageBydir("��ӿͻ��˲���Ա�ɹ���","");
				return;
			}
		}

		protected void btMod_Click(object sender, System.EventArgs e)
		{
			CMSMStruct.ClientOperStruct coperold=(CMSMStruct.ClientOperStruct)Session["coperold"];
			if(coperold.strOperID!=txtOperID.Text.Trim())
			{
				this.SetErrorMsgPageBydir("����ʧ�ܣ������ԣ�");
				return;
			}

			CMSMStruct.ClientOperStruct copernew=new CMSMStruct.ClientOperStruct();
			copernew.strDeptID=ddlDept.SelectedValue;
			copernew.strOperID=coperold.strOperID;
			copernew.strOperName=txtOperName.Text.Trim();
			if(copernew.strOperName=="")
			{
				this.SetErrorMsgPageBydirHistory("�ͻ��˲���Ա���Ʋ���Ϊ�գ�");
				return;
			}
			else if(copernew.strOperName=="admin"||copernew.strOperName=="ϵͳ����Ա")
			{
				this.SetErrorMsgPageBydirHistory("admin��ϵͳ����ԱΪ�ؼ��ʣ���������Ӵ������Ա��");
				return;
			}
//			else if(!m1.ChkClientOperNameDup(copernew.strOperName,copernew.strDeptID))
//			{
//				this.SetErrorMsgPageBydirHistory("�ÿͻ��˲���Ա�����Ѿ����ڣ����������룡");
//				return;	
//			}

			copernew.strLimit=ddlLimit.SelectedValue;

			if(!m1.UpdateClientOper(copernew,coperold))
			{
				this.SetErrorMsgPageBydir("�޸Ŀͻ��˲���Աʧ�ܣ������ԣ�");
				return;
			}
			else
			{
				this.SetSuccMsgPageBydir("�޸Ŀͻ��˲���Ա�ɹ���","paraconf/wfmDeptOperManage.aspx");
				return;
			}
		}

		protected void btPwdBegin_Click(object sender, System.EventArgs e)
		{
			string strOperID=txtOperID.Text.Trim();
			if(strOperID=="")
			{
				this.SetErrorMsgPageBydirHistory("�ͻ��˲���Ա������󣬲���Ϊ�գ�");
				return;
			}

			if(!m1.UpdateClientOperPwdBegin(strOperID))
			{
				this.SetErrorMsgPageBydir("�ͻ��˲���Ա�����ʼ��ʧ�ܣ������ԣ�");
				return;
			}
			else
			{
				this.SetSuccMsgPageBydir("�ͻ��˲���Ա�����Ѿ���ʼ��Ϊ��000000����ȴ��ͻ�������ͬ���󣬲ſ���ʹ�ã�","paraconf/wfmDeptOperManage.aspx");
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
				this.SetErrorMsgPageBydirHistory("�ͻ��˲���Ա������󣬲���Ϊ�գ�");
				return;
			}

			if(!m1.UpdateClientOperFreeze(strOperID,strState))
			{
				this.SetErrorMsgPageBydir("�ͻ��˲���Ա����ʧ�ܣ������ԣ�");
				return;
			}
			else
			{
				this.SetSuccMsgPageBydir("�ͻ��˲���Ա����ɹ�����ȴ��ͻ�������ͬ��������Ч��","paraconf/wfmDeptOperManage.aspx");
				return;
			}
		}

		protected void btnFreeze__Click(object sender, System.EventArgs e)
		{
			string strOperID=txtOperID.Text.Trim();
			string strState="2";
			if(strOperID=="")
			{
				this.SetErrorMsgPageBydirHistory("�ͻ��˲���Ա������󣬲���Ϊ�գ�");
				return;
			}

			if(!m1.UpdateClientOperFreeze(strOperID,strState))
			{
				this.SetErrorMsgPageBydir("�ͻ��˲���Ա�ⶳʧ�ܣ������ԣ�");
				return;
			}
			else
			{
				this.SetSuccMsgPageBydir("�ͻ��˲���Ա�ⶳ�ɹ�����ȴ��ͻ�������ͬ�����ſ���ʹ�ã�","paraconf/wfmDeptOperManage.aspx");
				return;
			}
		}
	}
}
