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
	/// wfmPackagesGoods ��ժҪ˵����
	/// </summary>
	public partial class wfmPackagesGoods : wfmBase
	{
	
		Manager m1;
		private const string PackagesGoodsSession="PackagesGoodsSession";
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// �ڴ˴������û������Գ�ʼ��ҳ��
			if(!this.IsPostBack)
			{
				string strPackageId = Request["vcPackageId"];
				string strPackageName = Request["vcPackageName"];
				string strPackagePrice = Request["nPackagePrice"];

				this.lblPackageId.Text = strPackageId;
				this.lblPackageName.Text = strPackageName;
				this.lblPackagePrice.Text = strPackagePrice;
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
			this.DataGrid1.CancelCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGrid1_CancelCommand);
			this.DataGrid1.EditCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGrid1_EditCommand);
			this.DataGrid1.UpdateCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGrid1_UpdateCommand);

		}
		#endregion

		private void SetManager()
		{
			Hashtable htapp=(Hashtable)Application["appconf"];
			string strcons=(string)htapp["cons"];
			m1=new Manager(strcons);
		}
		private void BindGrid()
		{
			if(Session[PackagesGoodsSession]!=null)
			{
				DataTable dt = Session[PackagesGoodsSession] as DataTable;
				this.DataGrid1.DataSource = dt;
				this.DataGrid1.DataBind();
			}
		}
		protected void Button1_Click(object sender, System.EventArgs e)
		{
			//��ѯ��Ʒ
			try
			{
				this.SetManager();
				string strPackageId = this.lblPackageId.Text;
				string strGoodsId = this.txtGoodsID.Text;
				string strGoodsName = this.txtGoodsName.Text;

				DataTable dt = m1.GetPackagesGoods(strPackageId,strGoodsId,strGoodsName);
				Session[PackagesGoodsSession] = dt;
				this.DataGrid1.CurrentPageIndex=0;
				this.DataGrid1.EditItemIndex = -1;
				BindGrid();
				
			}
			catch(Exception ex)
			{
				this.clog.WriteLine(ex);
				this.SetErrorMsgPageBydirHistory(ex.Message);
				return;
			}
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

		protected void Button2_Click(object sender, System.EventArgs e)
		{
			//�����ײ͹���
			this.RedirectPage("wfmPackages.aspx?vcPackageId="+this.lblPackageId.Text+"&vcPackageName="+this.lblPackageName.Text+"&nPackagePrice="+this.lblPackagePrice.Text);
		}

		private void DataGrid1_UpdateCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			//�༭
			//����ײ�
			try
			{
				string strGoodsId = e.Item.Cells[0].Text;
				string strGoodsName = e.Item.Cells[1].Text;

				string strGoodsPrice = ((TextBox)e.Item.Cells[3].Controls[0]).Text;				
				string strComments = ((TextBox)e.Item.Cells[4].Controls[0]).Text;
				PackagesStruct ps = new PackagesStruct();
				ps.strPackageId = this.lblPackageId.Text;
				ps.strPackageName = this.lblPackageName.Text;
				ps.dPackagePrice = Convert.ToDouble(this.lblPackagePrice.Text);

				ps.strGoodsId = strGoodsId;
				ps.strGoodsName = strGoodsName;

				ps.dGoodsPrice = Convert.ToDouble(strGoodsPrice);
				ps.strComments = strComments;

				this.SetManager();
				m1.AddPackage(ps);
				//this.SetSuccMsgPageBydir("��ӳɹ�","paraconf/wfmPackages.aspx?vcPackageId="+this.txtPackageId.Text+"&vcPackageName="+this.txtPacakgeName.Text+"&nPackagePrice="+this.txtPackagePrice.Text);
				this.Popup("��ӳɹ�");
				Button1_Click(null,null);
			}
			catch(Exception ex)
			{
				this.clog.WriteLine(ex);
				this.SetErrorMsgPageBydirHistory(ex.Message);
				return;
			}
		}
	}
}
