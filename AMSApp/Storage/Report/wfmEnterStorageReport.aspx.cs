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

namespace AMSApp.Storage.Report
{
	/// <summary>
	/// Summary description for wfmEnterSorageReport.
	/// </summary>
	public partial class wfmEnterStorageReport : wfmBase
	{
	
		protected ucPageView UcPageView1;
		BusiComm.StorageBusi StoBusi;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			this.btnExcel.Attributes.Add("onclick","javascript:window.open('../../DataGridToExcel.aspx', 'Sample', 'toolbar=no,location=no,directories=no,status=yes,menubar=yes,scrollbars=no,resizable=yes,copyhistory=yes,width=790,height=520,left=0,top=0')");
			if(Session["Login"]!=null)
			{
				CMSMStruct.LoginStruct ls1=(CMSMStruct.LoginStruct)Session["Login"];
				if (!IsPostBack )
				{
					this.ddlQueryType.Items.Add(new ListItem("ԭ���Ϸֹ�Ӧ�̽��ֱ���","MoreProviderEnter"));
					this.ddlQueryType.Items.Add(new ListItem("����Ӧ�̷�ԭ���Ͻ��ֱ���","OneProviderEnter"));

					string strvalue="";
					string strYear=DateTime.Now.Year.ToString();
					for(int i=0;i<12;i++)
					{
						if(i!=0&&DateTime.Now.AddMonths(-i).Month==12)
						{
							strYear=DateTime.Now.AddYears(-1).Year.ToString();
						}
						if(DateTime.Now.AddMonths(-i).Month<10)
						{
							strvalue=strYear+"0"+(DateTime.Now.AddMonths(-i).Month).ToString();
						}
						else
						{
							strvalue=strYear+(DateTime.Now.AddMonths(-i).Month).ToString();
						}
						this.ddlMonth.Items.Add(new ListItem(strvalue,strvalue));
					}

					this.FillDropDownList("tbNameCodeToStorage",this.ddlMaterialType,"vcCommSign='PRODUCTTYPE' and vcCommCode in('Pack','Raw')","ȫ��");

					string strType=this.ddlMaterialType.SelectedValue;
					this.FillDropDownList("AllMaterial",this.ddlMaterilName,"cnvcProductType='"+strType+"'","ȫ��");

					Session.Remove("QUERY");
					Session.Remove("toExcel");
					Session.Remove("page_view");
				}

				if(this.UcPageView1.MyDataGrid.DataSource!=null)
				{
					if(((DataView)this.UcPageView1.MyDataGrid.DataSource).Count>0)
					{
						UcPageView1.FootBar.Visible = true;
						btnExcel.Enabled=true;
					}
					else
					{
						btnExcel.Enabled=false;
					}
				}
				else
				{
					btnExcel.Enabled=false;
				}
			}
			else
			{
				Response.Redirect("../Exit.aspx");
			}
		}

		protected void btnQuery_Click(object sender, System.EventArgs e)
		{
			Session.Remove("QUERY");
			Session.Remove("toExcel");
			string strQueryType=this.ddlQueryType.SelectedValue;
			string strMonth=this.ddlMonth.SelectedValue;
			string strProviderID=this.txtProviderID.Text.Trim();
			string strProviderName=this.txtProviderName.Text.Trim();
			string strMaterialType=this.ddlMaterialType.SelectedValue;
			string strMaterialID=this.ddlMaterilName.SelectedValue;
			string strMaterialName=this.ddlMaterilName.SelectedItem.Text;

			Hashtable htPara=new Hashtable();
			htPara.Add("strQueryType",strQueryType);
			htPara.Add("strMonth",strMonth);
			htPara.Add("strProviderID",strProviderID);
			htPara.Add("strProviderName",strProviderName);
			htPara.Add("strMaterialType",strMaterialType);
			htPara.Add("strMaterialID",strMaterialID);
			htPara.Add("strMaterialName",strMaterialName);
			
			Hashtable htapp=(Hashtable)Application["appconf"];
			string strcons=(string)htapp["cons"];
			StoBusi=new BusiComm.StorageBusi(strcons);
			try
			{
				DataTable dtout=StoBusi.QueryBillEnterReport(htPara);
				if(dtout==null)
				{
					this.SetErrorMsgPageBy2dir("��ѯ�����������ԣ�");
					return;
				}
				else
				{
					dtout.TableName="���ֱ���";
					DataTable dtexcel=dtout.Copy();
					Session["QUERY"] = dtout;
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
				
				UcPageView1.MyDataGrid.PageSize = 20;
				DataView dvOut =new DataView(dtout);
				this.UcPageView1.MyDataSource = dvOut;
				this.UcPageView1.BindGrid();
			}
			catch(Exception er)
			{
				this.clog.WriteLine(er);
				this.SetErrorMsgPageBy2dir("��ѯ���������ԣ�");
				return;
			}
		}

		protected void ddlMaterialType_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			this.ddlMaterilName.Items.Clear();
			string strType=this.ddlMaterialType.SelectedValue;
			this.FillDropDownList("AllMaterial",this.ddlMaterilName,"cnvcProductType='"+strType+"'","ȫ��");
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
	}
}