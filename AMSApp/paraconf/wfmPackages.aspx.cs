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
	/// wfmPackages 的摘要说明。
	/// </summary>
	public partial class wfmPackages : wfmBase
	{
		Manager m1;
		protected ucPageView UcPageView1;
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
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
							this.SetErrorMsgPageBydir("查询出错，请重试！");
							return;
						}
						else
						{
							dtout.TableName="套餐信息清单";
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
						this.SetErrorMsgPageBydir("查询错误，请重试！");
						return;
					}

				}
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

		protected void btnAdd_Click(object sender, System.EventArgs e)
		{
			//添加套餐
			this.RedirectPage("wfmPackagesGoods.aspx?OperFlag=add&vcPackageId="+this.lblPackageId.Text+"&vcPackageName="+this.lblPackageName.Text+"&nPackagePrice="+this.lblPackagePrice.Text);
		}

		protected void Button1_Click(object sender, System.EventArgs e)
		{
			this.RedirectPage("wfmGoods.aspx");
		}
	}
}
