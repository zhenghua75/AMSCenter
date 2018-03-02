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
	/// wfmTimeSales 的摘要说明。
	/// </summary>
	public partial class wfmTimeSales : wfmBase
	{
	
		protected ucPageView UcPageView1;
		BusiComm.BusiQuery busiq;
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
				initCtl();
			}
		}
		private void initCtl()
		{
            this.FillDropDownList("tbCommCode", ddlDept, "vcCommSign ='MD' and vcCommCode<>'FYZX1'", "全部");
            this.FillDropDownList("AllREGION", ddlRegion, "", "全部");
            this.txtBeginDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
			this.txtEndDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
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
			//查询
			try
			{
				//查询
				Session.Remove("QUERY");
				Session.Remove("toExcel");

				Hashtable htapp=(Hashtable)Application["appconf"];
				string strcons=(string)htapp["cons"];
				busiq=new BusiComm.BusiQuery(strcons);

				string strDeptId = this.ddlDept.SelectedValue;
				if(strDeptId == "全部")
				{
					strDeptId = "";
				}
				string strBeginDate = Convert.ToDateTime(this.txtBeginDate.Text).ToString("yyyy-MM-dd");
				string strEndDate = Convert.ToDateTime(this.txtEndDate.Text).ToString("yyyy-MM-dd");

				DataTable dtout=busiq.GetTimeSales(strDeptId,strBeginDate,strEndDate);
				//this.TableConvert(dtout,"门店","tbCommCode","vcCommSign='MD'");

				dtout.TableName="门店时段消费统计";
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

        protected void ddlRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.BindRegion(this.ddlRegion.SelectedValue, this.ddlDept);
        }
	}
}
