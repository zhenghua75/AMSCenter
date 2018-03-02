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
	/// wfmPackages ��ժҪ˵����
	/// </summary>
	public partial class wfmPackages : wfmBase
	{
		Manager m1;
		protected ucPageView UcPageView1;
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// �ڴ˴������û������Գ�ʼ��ҳ��
			if(!this.IsPostBack)
			{
				if(Request["vcPackageId"]!="")
				{
					string strPackageId = Request["vcPackageId"];
					string strPackageName = Request["vcPackageName"];
					string strPackgePrice = Request["nPackagePrice"];
					
					this.lblPackageId.Text = strPackageId;
					this.lblPackageName.Text = strPackageName;
					this.lblPackagePrice.Text = strPackgePrice;

					Session.Remove("QUERY");
					Session.Remove("toExcel");

					Hashtable htapp=(Hashtable)Application["appconf"];
					string strcons=(string)htapp["cons"];
					m1=new Manager(strcons);
					try
					{
						DataTable dtout=m1.GetPackages(strPackageId);
						if(dtout==null)
						{
							this.SetErrorMsgPageBydir("��ѯ���������ԣ�");
							return;
						}
						else
						{
							dtout.TableName="�ײ���Ϣ�嵥";
							Session["QUERY"] = dtout;
						}
						UcPageView1.MyDataGrid.PageSize = 30;
						DataView dvOut =new DataView(dtout);
						this.UcPageView1.MyDataSource = dvOut;
						this.UcPageView1.BindGrid();
					}
					catch(Exception er)
					{
						this.clog.WriteLine(er);
						this.SetErrorMsgPageBydir("��ѯ���������ԣ�");
						return;
					}

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

		protected void btnAdd_Click(object sender, System.EventArgs e)
		{
			//����ײ�
			this.RedirectPage("wfmPackagesGoods.aspx?OperFlag=add&vcPackageId="+this.lblPackageId.Text+"&vcPackageName="+this.lblPackageName.Text+"&nPackagePrice="+this.lblPackagePrice.Text);
		}

		protected void Button1_Click(object sender, System.EventArgs e)
		{
			this.RedirectPage("wfmGoods.aspx");
		}
	}
}
