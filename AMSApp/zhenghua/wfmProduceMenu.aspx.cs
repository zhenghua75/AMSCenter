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
namespace AMSApp.zhenghua
{
	/// <summary>
	/// wfmProduceMenu ��ժҪ˵����
	/// </summary>
	public partial class wfmProduceMenu : System.Web.UI.Page
	{





	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// �ڴ˴������û������Գ�ʼ��ҳ��
			// Put user code to initialize the page here
			CMSMStruct.LoginStruct ls1=new CommCenter.CMSMStruct.LoginStruct();
			if(Session["Login"]==null)
			{
				Response.Redirect("Exit.aspx");
			}
			else
			{
				ls1=(CMSMStruct.LoginStruct)Session["Login"];
			}
			trnoprom.Visible = true;
			trMaterial.Visible = false;
			trFormulaQuery.Visible = false;
			trProductQuery.Visible = false;
			trOrderDetail.Visible = false;
			trOrder.Visible = false;
			trOrderQuery.Visible = false;
			trProducePlanQuery.Visible = false;
			trProducePlanQueryMake.Visible = false;
			trProducePlanQueryGoods.Visible = false;
			trSalesRoomProduce.Visible = false;


			#region ���Ƶ�ǰ��ʾ�˵�
			Hashtable htOperFunc=(Hashtable)Application["OperFunc"];
			ArrayList almenu=(ArrayList)htOperFunc[ls1.strLoginID];
			if(almenu!=null)
			{
				for(int i=0;i<almenu.Count;i++)
				{
					CMSMStruct.MenuStruct ms1=(CMSMStruct.MenuStruct)almenu[i];
					HtmlTableRow trCurrent = tblProduceMenu.FindControl("tr" + ms1.strFuncAddress.Replace("wfm", String.Empty)) as HtmlTableRow;
					
					if(trCurrent!=null)
					{
						trCurrent.Visible = true;
						trnoprom.Visible=false;
					}
					
					
				}
			}
			#endregion
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
	}
}
