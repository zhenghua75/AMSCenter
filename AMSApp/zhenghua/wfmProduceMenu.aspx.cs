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
	/// wfmProduceMenu 的摘要说明。
	/// </summary>
	public partial class wfmProduceMenu : System.Web.UI.Page
	{





	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
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


			#region 控制当前显示菜单
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
	}
}
