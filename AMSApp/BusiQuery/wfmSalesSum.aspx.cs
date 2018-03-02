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

namespace AMSApp.BusiQuery
{
	/// <summary>
	/// wfmSalesSum 的摘要说明。
	/// </summary>
	public partial class wfmSalesSum : wfmBase
	{
		BusiComm.BusiQuery busiq;
		protected ucPageView UcPageView1;
		protected void Page_Load(object sender, System.EventArgs e)
		{
			this.Button2.Attributes.Add("onclick","javascript:window.open('../DataGridToExcel.aspx', 'Sample', 'toolbar=no,location=no,directories=no,status=yes,menubar=yes,scrollbars=no,resizable=yes,copyhistory=yes,width=790,height=520,left=0,top=0')");
			// 在此处放置用户代码以初始化页面
			if(Session["Login"]==null)
			{
				Response.Redirect("../Exit.aspx");
			}
			if (!IsPostBack )
			{
				Session.Remove("QUERY");
				Session.Remove("toExcel");
				Session.Remove("page_view");
				MonthList();
			}
		}
		private void MonthList()
		{
			int prevYear = DateTime.Today.Year-1;
			int curYear = DateTime.Today.Year;
			int nextYear = DateTime.Today.Year+1;

			ListItem liprev = new ListItem(prevYear.ToString()+"年",prevYear.ToString());
			ddlYear.Items.Add(liprev);
			ListItem li = new ListItem(curYear.ToString()+"年",curYear.ToString());
			li.Selected=true;
			ddlYear.Items.Add(li);
			ListItem linext = new ListItem(nextYear.ToString()+"年",nextYear.ToString());
			ddlYear.Items.Add(linext);

			for(int i=1;i<=12;i++)
			{
				ListItem liMonth = new ListItem(i.ToString()+"月",i.ToString());
				if(i==DateTime.Today.Month)
				{
					liMonth.Selected=true;
				}
				else
				{
					liMonth.Selected=false;
				}
				ddlMonths.Items.Add(liMonth);
				ddlNextMonths.Items.Add(liMonth);
			}
			ListItem liempty = new ListItem("","");
			ddlMonths.Items.Add(liempty);
			ddlNextMonths.Items.Add(liempty);
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

		protected void Button1_Click(object sender, System.EventArgs e)
		{
			try
			{
				//查询
				Session.Remove("QUERY");
				Session.Remove("toExcel");

				Hashtable htapp=(Hashtable)Application["appconf"];
				string strcons=(string)htapp["cons"];
				busiq=new BusiComm.BusiQuery(strcons);

				DataTable dtout=busiq.GetSalesSum(ddlYear.SelectedValue,ddlMonths.SelectedValue,ddlNextMonths.SelectedValue);
                this.TableConvert(dtout, "门店", "tbCommCode", "vcCommSign='MD' and vcCommCode<>'FYZX1'");

				dtout.TableName="销售汇总表";
				DataTable dtexcel=dtout.Copy();
				Session["QUERY"] = dtout;
				Session["toExcel"]=dtexcel;
				CMSMStruct.LoginStruct ls1=(CMSMStruct.LoginStruct)Session["Login"];
				if(dtout.Rows.Count<=0)
				{
					Button2.Enabled=false;
				}
				else
				{
					if(ls1.strLimit=="CL001")
					{
						Button2.Enabled=true;
					}
				}
				UcPageView1.MyDataGrid.PageSize = 30;
				DataView dvOut =new DataView(dtout);
				this.UcPageView1.MyDataSource = dvOut;
				this.UcPageView1.BindGrid();
			}
			catch(Exception ex)
			{
				this.clog.WriteLine(ex);
				this.SetErrorMsgPageBydir("查询错误，请重试！");
				return;
			}
		}

		protected void Button2_Click(object sender, System.EventArgs e)
		{
			//导出
		}
	}
}
