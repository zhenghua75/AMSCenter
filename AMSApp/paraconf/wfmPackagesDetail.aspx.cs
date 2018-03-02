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
	/// wfmPackagesDetail ��ժҪ˵����
	/// </summary>
	public partial class wfmPackagesDetail : wfmBase
	{
	
		Manager m1;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			// �ڴ˴������û������Գ�ʼ��ҳ��
			if(!this.IsPostBack)
			{
				this.txtPackageId.Enabled = false;
				this.txtPacakgeName.Enabled = false;
				this.txtPackagePrice.Enabled=false;
			
				string strOperFlag = Request["OperFlag"];
				string strPackageId = Request["vcPackageId"];
				string strPackageName = Request["vcPackageName"];
				string strPackagePrice = Request["nPackagePrice"];
				switch(strOperFlag)
				{
					case "modify":
						string strGoodsId = Request["vcGoodsId"];
						string strGoodsName = Request["vcGoodsName"];
						string strGoodsPrice = Request["nGoodsPrice"];

						string strComment = Request["vcComments"];

						this.txtPackageId.Text = strPackageId;
						this.txtPacakgeName.Text = strPackageName;
						this.txtPackagePrice.Text = strPackagePrice;

						this.txtGoodsId.Text = strGoodsId;
						this.txtGoodsName.Text = strGoodsName;
						this.txtGoodsPrice.Text = strGoodsPrice;

						this.txtComments.Text = strComment;

						this.Button1.Visible = false;
						
						this.txtGoodsId.Enabled = false;
						this.txtGoodsName.Enabled = false;
						break;
					case "add":
						this.txtPackageId.Text = strPackageId;
						this.txtPacakgeName.Text = strPackageName;
						this.txtPackagePrice.Text = strPackagePrice;

						this.Button2.Visible=false;
						this.Button3.Visible = false;

						//this.txtGoodsName.Enabled=false;
						break;
				}
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

		private void SetManager()
		{
			Hashtable htapp=(Hashtable)Application["appconf"];
			string strcons=(string)htapp["cons"];
			m1=new Manager(strcons);
			
		
			
			//m1.AddPackage(
		}
		private PackagesStruct getPackageStruct()
		{
			PackagesStruct ps = new PackagesStruct();
			ps.strPackageId = this.txtPackageId.Text;
			ps.strPackageName = this.txtPacakgeName.Text;
			ps.dPackagePrice = Convert.ToDouble(this.txtPackagePrice.Text);
			ps.strGoodsId = this.txtGoodsId.Text;
			ps.strGoodsName = this.txtGoodsName.Text;
			ps.dGoodsPrice = Convert.ToDouble(this.txtGoodsPrice.Text);
			ps.strComments = this.txtComments.Text;
			
			return ps;
		}
		private void Validpage()
		{
			if(this.txtGoodsId.Text.Trim().Length==0)
				throw new Exception("��������Ʒ���");
			if(this.txtGoodsName.Text.Trim().Length==0)
				throw new Exception("��������Ʒ����");
			if(this.txtGoodsPrice.Text.Trim().Length==0)
				throw new Exception("��������Ʒ����");
			try
			{
				Convert.ToDouble(this.txtGoodsPrice.Text);
				Convert.ToInt32(this.txtComments.Text);
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}
		protected void Button1_Click(object sender, System.EventArgs e)
		{
			//����ײ�
			try
			{
				Validpage();
				PackagesStruct ps = this.getPackageStruct();
				this.SetManager();
				m1.AddPackage(ps);
				this.SetSuccMsgPageBydir("��ӳɹ�","paraconf/wfmPackages.aspx?vcPackageId="+this.txtPackageId.Text+"&vcPackageName="+this.txtPacakgeName.Text+"&nPackagePrice="+this.txtPackagePrice.Text);
			}
			catch(Exception ex)
			{
				this.clog.WriteLine(ex);
				this.SetErrorMsgPageBydirHistory(ex.Message);
				return;
			}
		}

		protected void Button2_Click(object sender, System.EventArgs e)
		{
			//�޸��ײ�
			try
			{
				Validpage();
				PackagesStruct ps = this.getPackageStruct();
				this.SetManager();
				m1.UpdatePackage(ps);
				this.SetSuccMsgPageBydir("�޸ĳɹ�","paraconf/wfmPackages.aspx?vcPackageId="+this.txtPackageId.Text+"&vcPackageName="+this.txtPacakgeName.Text+"&nPackagePrice="+this.txtPackagePrice.Text);
			}
			catch(Exception ex)
			{
				this.clog.WriteLine(ex);
				this.SetErrorMsgPageBydir(ex.Message);
				return;
			}
		}

		protected void Button3_Click(object sender, System.EventArgs e)
		{
			//ɾ���ײ�
			try
			{
				Validpage();
				PackagesStruct ps = this.getPackageStruct();
				this.SetManager();
				m1.DeletePackage(ps);
				this.SetSuccMsgPageBydir("ɾ���ɹ�","paraconf/wfmPackages.aspx?vcPackageId="+this.txtPackageId.Text+"&vcPackageName="+this.txtPacakgeName.Text+"&nPackagePrice="+this.txtPackagePrice.Text);
			}
			catch(Exception ex)
			{
				this.clog.WriteLine(ex);
				this.SetErrorMsgPageBydir(ex.Message);
				return;
			}
		}

		protected void Button4_Click(object sender, System.EventArgs e)
		{
			//�����ײ͹���
			this.RedirectPage("wfmPackages.aspx?vcPackageId="+this.txtPackageId.Text+"&vcPackageName="+this.txtPacakgeName.Text+"&nPackagePrice="+this.txtPackagePrice.Text);
		}
	}
}
