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
	/// wfmDeptOperManage 的摘要说明。
	/// </summary>
	public partial class wfmDeptOperManage : wfmBase
	{

		protected ucPageView UcPageView1;
		protected string strExcelPath = string.Empty;
		protected System.Web.UI.WebControls.DropDownList Dropdownlist1;
		protected System.Web.UI.WebControls.DropDownList Dropdownlist2;
		
		BusiComm.Manager m1;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
			
			this.btnExcel.Attributes.Add("onclick","javascript:window.open('../DataGridToExcel.aspx', 'Sample', 'toolbar=no,location=no,directories=no,status=yes,menubar=yes,scrollbars=no,resizable=yes,copyhistory=yes,width=790,height=520,left=0,top=0')");
			if(Session["Login"]!=null)
			{
				CMSMStruct.LoginStruct ls1=(CMSMStruct.LoginStruct)Session["Login"];
				
				if (!IsPostBack )
				{
					this.ddlOperState.Items.Add("全部");
					this.ddlOperState.Items.Add("正常");
					this.ddlOperState.Items.Add("冻结");
					this.btnExcel.Enabled=false;
					this.FillDropDownList("AllMD", ddlDept,"vcCommSign='MD' and vcCommCode not in('CEN00','FYZX1')","全部");
					if(ls1.strDeptID!="CEN00"&&ls1.strDeptID!="FYZX1")
					{
						ddlDept.Items.FindByValue(ls1.strDeptID).Selected=true;
						ddlDept.Enabled=false;
					}
					Session.Remove("QUERY");
					Session.Remove("toExcel");
					Session.Remove("page_view");
				}
			}
			else
			{
				Response.Redirect("../Exit.aspx");
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

		protected void btnQuery_Click(object sender, System.EventArgs e)
		{
			Session.Remove("QUERY");
			Session.Remove("toExcel");

			Hashtable htapp=(Hashtable)Application["appconf"];
			string strcons=(string)htapp["cons"];
			m1=new Manager(strcons);

			Hashtable htPara=new Hashtable();
			string strOperName=this.txtOperName.Text.Trim();
			htPara.Add("strOperName",strOperName);
			string strDeptID=ddlDept.SelectedValue;
			string strState=this.ddlOperState.SelectedValue;
			if (strState=="全部")
			{
				strState="";
			}
			else
			{
				if (strState=="正常")
				{
					strState="1";
				}
				else
				{
					strState="0";
				}
			}
			if(strDeptID=="全部")
			{
				strDeptID="";
			}
			htPara.Add("strState",strState);
			htPara.Add("strDeptID",strDeptID);
			CMSMStruct.LoginStruct ls1=(CMSMStruct.LoginStruct)Session["Login"];
			htPara.Add("strLoginID",ls1.strLoginID);
			
			try
			{
				DataTable dtout=m1.GetClientOper(htPara);
				if(dtout==null)
				{
					this.SetErrorMsgPageBydir("查询出错，请重试！");
					btnExcel.Enabled=false;
					return;
				}
				else
				{
					this.TableConvert(dtout,"门店","AllMD");
					this.TableConvert(dtout,"权限","tbCommCode","vcCommSign='LM'");
					dtout.TableName="客户端操作员清单";
					DataTable dtexcel=dtout.Copy();
					Session["QUERY"] = dtout;
					dtexcel.Columns.Remove("操作");
					dtexcel.Columns.Remove("功能权限");
					Session["toExcel"]=dtexcel;

					if(dtout.Rows.Count<=0)
					{
						btnExcel.Enabled=false;
					}
					else
					{
						btnExcel.Enabled=true;	
					}
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

		protected void btnAdd_Click(object sender, System.EventArgs e)
		{
			this.RedirectPage("wfmClientOperDetail.aspx");
		}
	}
}
