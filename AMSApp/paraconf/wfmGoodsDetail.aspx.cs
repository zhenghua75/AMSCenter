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
	/// Summary description for wfmGoodsDetail.
	/// </summary>
	public partial class wfmGoodsDetail : wfmBase
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

			string strid=Request.QueryString["id"];
			Hashtable htapp=(Hashtable)Application["appconf"];
			string strcons=(string)htapp["cons"];
			m1=new Manager(strcons);

			if(!IsPostBack)
			{
				this.txtGoodsID.Text="8---";
				if(strid==""||strid==null)
				{
					this.btAdd.Enabled=true;
					this.btDel.Enabled=false;
					this.btMod.Enabled=false;
					this.txtGoodsID.ReadOnly=true;
					lbltitle.Text="����Ʒ����¼��";
				}
				else
				{
					this.btAdd.Enabled=false;
					this.btDel.Enabled=false;
					this.btMod.Enabled=true;
					CMSMStruct.GoodsStruct gs=m1.GetGoodsInfo(strid);
					txtGoodsID.Text=gs.strGoodsID;
					txtGoodsName.Text=gs.strGoodsName;
					txtSpell.Text=gs.strSpell;
					txtPrice.Text=gs.dPrice.ToString();
					txtigvalue.Text=gs.iIgValue.ToString();
					txtComments.Text=gs.strComments;

					this.chkPackage.Checked = gs.bPackage;
					this.txtGoodsID.ReadOnly=true;
					this.txtGoodsName.ReadOnly=true;

                    IsNew.Checked = gs.bNew;
                    IsKey.Checked = gs.bKey;
                    IsDeptPrice.Checked = gs.bDeptPrice;
                    this.Unit.Text = gs.Unit;
                    this.NewDate.Text = gs.NewDate;
					lbltitle.Text="��Ʒ�����޸�ɾ��";
					Session["gsold"]=gs;
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

		protected void btcancel_Click(object sender, System.EventArgs e)
		{
			this.RedirectPage("wfmGoods.aspx");
		}

		protected void btAdd_Click(object sender, System.EventArgs e)
		{
			CMSMStruct.GoodsStruct gs=new CMSMStruct.GoodsStruct();
			//��ȡ��8��ͷ����Ʒ���ID���ж��Ƿ��Ѿ��ﵽ999�֡�
			string strMaxID=m1.GetGoodsMaxID("8");
			if(int.Parse(strMaxID)>=8999)
			{
				this.SetErrorMsgPageBydir("�⹺��Ʒ�Ѿ�����999�ֵ�����ˮƽ����������ӣ�");
				return;	
			}

			if(txtGoodsName.Text.Trim()=="")
			{
				this.SetErrorMsgPageBydir("��Ʒ���Ʋ���Ϊ�գ�");
				return;
			}
			else if(m1.ChkGoodsNameDup(txtGoodsName.Text.Trim()))
			{
				gs.strGoodsName=txtGoodsName.Text.Trim();
			}
			else
			{
				this.SetErrorMsgPageBydir("����Ʒ�����Ѿ����ڣ����������룡");
				return;	
			}

			if(txtPrice.Text.Trim()=="")
			{
				this.SetErrorMsgPageBydir("��Ʒ���۲���Ϊ�գ�");
				return;
			}
			else
			{
				gs.dPrice=Double.Parse(txtPrice.Text.Trim());
			}

			if(txtigvalue.Text.Trim()==""||txtigvalue.Text.Trim()=="0"||int.Parse(txtigvalue.Text.Trim())<-1)
			{
				this.SetErrorMsgPageBydir("�һ���ֵ����ȷ��");
				return;
			}
			else
			{
				gs.iIgValue=int.Parse(txtigvalue.Text.Trim());
			}

			gs.strSpell=txtSpell.Text.Trim().ToLower();
			gs.strComments=txtComments.Text.Trim();
			gs.bPackage = this.chkPackage.Checked;
            //if (this.IsNew.Checked)
            //{
            //    gs.bNew = "1";
            //}
            //if(this.IsKey.Checked)
            //{
            //    gs.bKey = "1";
            //}
            gs.bNew = this.IsNew.Checked;
            gs.bKey = this.IsKey.Checked;
            gs.Unit = this.Unit.Text;
            if (!string.IsNullOrEmpty(this.NewDate.Text))
            {
                gs.NewDate = DateTime.Parse(this.NewDate.Text).ToString("yyyy-MM-dd");
            }
            //if (IsDeptPrice.Checked)
            //{
            //    gs.bDeptPrice = "1";
            //}
            gs.bDeptPrice = this.IsDeptPrice.Checked;
			if(!m1.InsertGoods(gs))
			{
				this.SetErrorMsgPageBydir("�����Ʒ��Ϣʧ�ܣ������ԣ�");
				return;
			}
			else
			{
				this.SetSuccMsgPageBydir("�����Ʒ��Ϣ�ɹ���","");
				return;
			}
		}

		protected void btMod_Click(object sender, System.EventArgs e)
		{
			CMSMStruct.GoodsStruct gsold=(CMSMStruct.GoodsStruct)Session["gsold"];
			if(gsold.strGoodsID!=txtGoodsID.Text.Trim())
			{
				this.SetErrorMsgPageBydir("����ʧ�ܣ������ԣ�");
				return;
			}

			CMSMStruct.GoodsStruct gsnew=new CMSMStruct.GoodsStruct();
			gsnew.strGoodsID=txtGoodsID.Text.Trim();

			if(txtGoodsName.Text.Trim()=="")
			{
				this.SetErrorMsgPageBydir("��Ʒ���Ʋ���Ϊ�գ�");
				return;
			}
			else if(m1.ChkNewGoodsNameDup(txtGoodsName.Text.Trim(),gsnew.strGoodsID))
			{
				gsnew.strGoodsName=txtGoodsName.Text.Trim();
			}
			else
			{
				this.SetErrorMsgPageBydir("����Ʒ�����Ѿ����ڣ����������룡");
				return;	
			}

			if(txtPrice.Text.Trim()=="")
			{
				this.SetErrorMsgPageBydir("��Ʒ���۲���Ϊ�գ�");
				return;
			}
			else
			{
				gsnew.dPrice=Double.Parse(txtPrice.Text.Trim());
			}

			if(txtigvalue.Text.Trim()==""||txtigvalue.Text.Trim()=="0"||int.Parse(txtigvalue.Text.Trim())<-1)
			{
				this.SetErrorMsgPageBydir("�һ���ֵ����ȷ��");
				return;
			}
			else
			{
				gsnew.iIgValue=int.Parse(txtigvalue.Text.Trim());
			}

			gsnew.strSpell=txtSpell.Text.Trim().ToLower();
			gsnew.strComments=txtComments.Text.Trim();
			gsnew.bPackage = this.chkPackage.Checked;

            //if (this.IsNew.Checked)
            //{
            //    gsnew.bNew = "1";
            //}
            //if (this.IsKey.Checked)
            //{
            //    gsnew.bKey = "1";
            //}
            gsnew.bNew = this.IsNew.Checked;
            gsnew.bKey = this.IsKey.Checked;

            gsnew.Unit = this.Unit.Text;
            if (!string.IsNullOrEmpty(this.NewDate.Text))
            {
                gsnew.NewDate = DateTime.Parse(this.NewDate.Text).ToString("yyyy-MM-dd");
            }
            //if (IsDeptPrice.Checked)
            //{
            //    gsnew.bDeptPrice = "1";
            //}
            gsnew.bDeptPrice = this.IsDeptPrice.Checked;
			if(!m1.UpdateGoods(gsnew,gsold))
			{
				this.SetErrorMsgPageBydir("������Ʒ�޸���Ϣʧ�ܣ������ԣ�");
				return;
			}
			else
			{
				this.SetSuccMsgPageBydir("������Ʒ�޸���Ϣ�ɹ���","");
				return;
			}
		}

		protected void btDel_Click(object sender, System.EventArgs e)
		{
		
		}

		protected void btnPackage_Click(object sender, System.EventArgs e)
		{
			//�༭�ײ�
			this.RedirectPage("wfmPackages.aspx?vcPackageId="+this.txtGoodsID.Text+"&vcPackageName="+this.txtGoodsName.Text+"&nPackagePrice="+this.txtPrice.Text);
		}

	}
}
