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
using System.Text.RegularExpressions;

namespace AMSApp.Storage
{
	/// <summary>
	/// Summary description for wfmBillValidEnter.
	/// </summary>
	public partial class wfmBillValidEnter : wfmBase
	{

		BusiComm.StorageBusi StoBusi;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			if(Session["Login"]!=null)
			{
				CMSMStruct.LoginStruct ls1=(CMSMStruct.LoginStruct)Session["Login"];
				if (!IsPostBack )
				{
					this.FillDropDownList("NewDept",ddlValidDept,"vcCommSign='SalesRoom'");
					if(ls1.strDeptID!="CEN00"&&ls1.strLimit!="CL001")
					{
						this.ddlValidDept.SelectedIndex=this.ddlValidDept.Items.IndexOf(this.ddlValidDept.Items.FindByValue(ls1.strNewDeptID));
						this.ddlValidDept.Enabled=false;
					}
					this.txtReceiveOper.Text=ls1.strOperName;
					this.lblReceiveDate.Text=DateTime.Now.ToShortDateString()+" "+DateTime.Now.ToLongTimeString();

					Session.Remove("QUERY");
					Session.Remove("page_view");
					this.btnValidEnter.Enabled=false;
					this.btnEdit.Enabled=false;
				}
			}
			else
			{
				Response.Redirect("../Exit.aspx");
			}
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
			this.DataGrid1.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DataGrid1_PageIndexChanged);

		}
		#endregion

		protected void btnQuery_Click(object sender, System.EventArgs e)
		{
			Session.Remove("QUERY");
			string strDeptID=this.ddlValidDept.SelectedValue;
			string strAssignID=this.txtAssignID.Text.Trim();
			string strOrderSerialNo=this.txtOrderSerialNo.Text.Trim();

			if(strAssignID=="")
			{
				this.SetErrorMsgPageBydirHistory("分货流水不能为空！");
				return;
			}

			if(strOrderSerialNo=="")
			{
				this.SetErrorMsgPageBydirHistory("订单流水不能为空！");
				return;
			}

			Hashtable htPara=new Hashtable();
			htPara.Add("strDeptID",strDeptID);
			htPara.Add("strAssignID",strAssignID);
			htPara.Add("strOrderSerialNo",strOrderSerialNo);
			
			Hashtable htapp=(Hashtable)Application["appconf"];
			string strcons=(string)htapp["cons"];
			StoBusi=new BusiComm.StorageBusi(strcons);
			try
			{
				DataTable dtout=StoBusi.GetAssignToValidEnter(htPara);
				if(dtout==null)
				{
					this.lblAssignID.Text="";
					this.lblOrderSerialNo.Text="";
					this.SetErrorMsgPageBydir("查询出错，请重试！");
					return;
				}
				else
				{
					if(dtout.Rows.Count>0)
					{
						this.btnEdit.Enabled=true;
					}
					else
					{
						this.btnEdit.Enabled=false;
					}
					this.TableConvert(dtout,"cnvcShipDeptName","NewDept");
					this.TableConvert(dtout,"cnvcProductTypeName","tbNameCodeToStorage","vcCommSign='PRODUCTTYPE'");
					this.lblAssignID.Text=strAssignID;
					this.lblOrderSerialNo.Text=strOrderSerialNo;
					dtout.TableName="验收信息表";
					DataTable dtexcel=dtout.Copy();
					Session["QUERY"] = dtout;
				}
				
				//				this.DataGrid1.PageSize = 30;
				this.DataGrid1.DataSource=dtout;
				this.DataGrid1.DataBind();
				this.btnEdit.Text="编辑";
				this.btnValidEnter.Enabled=false;

				this.DataGrid1.Columns[14].Visible=true;
				this.DataGrid1.Columns[15].Visible=true;
				this.DataGrid1.Columns[16].Visible=true;
				this.DataGrid1.Columns[17].Visible=false;
				this.DataGrid1.Columns[18].Visible=false;
				this.DataGrid1.Columns[19].Visible=false;
			}
			catch(Exception er)
			{
				this.lblAssignID.Text="";
				this.clog.WriteLine(er);
				this.SetErrorMsgPageBydir("查询错误，请重试！");
				return;
			}
		}

		protected void btnValidEnter_Click(object sender, System.EventArgs e)
		{
			if(this.lblAssignID.Text.Trim()=="")
			{
				this.SetErrorMsgPageBydir("分货流水错误，请重新查询！");
				return;
			}

			if(this.lblOrderSerialNo.Text.Trim()=="")
			{
				this.SetErrorMsgPageBydir("订单流水错误，请重新查询！");
				return;
			}

			DataTable dtIn=(DataTable)Session["QUERY"];
			bool isflag=false;
			if(dtIn==null||dtIn.Rows.Count==0)
			{
				this.SetErrorMsgPageBydirHistory("没有任何需要验收的分货单内容，请检查生产序号是否正确！");
				return;
			}
			else
			{
				foreach(DataRow dr in dtIn.Rows)
				{
					if(dr["cnnTravelCount"].ToString()==""||dr["cnnValidOkCount"].ToString()==""||dr["cnnValidNoCount"].ToString()=="")
					{
						isflag=true;
						break;
					}
				}
			}

			if(isflag)
			{
				this.SetErrorMsgPageBydirHistory("验收数量填写有误，请检查！");
				return;
			}

			string strDeptID=this.ddlValidDept.SelectedValue;
			string strAssignID=this.lblAssignID.Text.Trim();
			string strOrderSerialNo=this.lblOrderSerialNo.Text.Trim();
			string strReceiveOper=this.txtReceiveOper.Text.Trim();
			string strValidDate=DateTime.Now.ToShortDateString()+" "+DateTime.Now.ToLongTimeString();

			CMSMStruct.LoginStruct ls1=(CMSMStruct.LoginStruct)Session["Login"];
			Hashtable htPara=new Hashtable();
			htPara.Add("strDeptID",strDeptID);
			htPara.Add("strAssignID",strAssignID);
			htPara.Add("strOrderSerialNo",strOrderSerialNo);
			htPara.Add("strReceiveOper",strReceiveOper);
			htPara.Add("strValidDate",strValidDate);
			htPara.Add("strOperID",ls1.strOperName);
			
			Hashtable htapp=(Hashtable)Application["appconf"];
			string strcons=(string)htapp["cons"];
			StoBusi=new BusiComm.StorageBusi(strcons);
			try
			{
				if(StoBusi.AssignToValidEnterFinal(htPara,dtIn))
				{
					this.SetSuccMsgPageBydir("订单验收入库成功！","Storage/wfmBillValidEnter.aspx");
					return;
				}
				else
				{
					this.SetErrorMsgPageBydir("订单验收入库时发生错误，请重试！");
					return;
				}
			}
			catch(Exception er)
			{
				this.clog.WriteLine(er);
				this.SetErrorMsgPageBydir("查询错误，请重试！");
				return;
			}
		}

		protected void btnEdit_Click(object sender, System.EventArgs e)
		{
			if(btnEdit.Text=="编辑")
			{
				this.DataGrid1.Columns[14].Visible=false;
				this.DataGrid1.Columns[15].Visible=false;
				this.DataGrid1.Columns[16].Visible=false;
				this.DataGrid1.Columns[17].Visible=true;
				this.DataGrid1.Columns[18].Visible=true;
				this.DataGrid1.Columns[19].Visible=true;
				for(int i=0;i<this.DataGrid1.Items.Count;i++)
				{
					foreach(System.Web.UI.Control con1 in this.DataGrid1.Items[i].Cells[17].Controls)
					{
						if(con1 is System.Web.UI.WebControls.TextBox)
						{
							TextBox txt1=con1 as TextBox;
							txt1.Text=this.DataGrid1.Items[i].Cells[14].Text.Trim();
						}
					}
					foreach(System.Web.UI.Control con1 in this.DataGrid1.Items[i].Cells[18].Controls)
					{
						if(con1 is System.Web.UI.WebControls.TextBox)
						{
							TextBox txt2=con1 as TextBox;
							txt2.Text=this.DataGrid1.Items[i].Cells[15].Text.Trim();
						}
					}
					foreach(System.Web.UI.Control con1 in this.DataGrid1.Items[i].Cells[19].Controls)
					{
						if(con1 is System.Web.UI.WebControls.TextBox)
						{
							TextBox txt3=con1 as TextBox;
							txt3.Text=this.DataGrid1.Items[i].Cells[16].Text.Trim();
						}
					}
				}
				btnEdit.Text="锁定编辑";
				this.btnValidEnter.Enabled=false;
			}
			else if(btnEdit.Text=="锁定编辑")
			{
				bool checkflag=false;
				string strcheckerror="";
				string strTravelCount="";
				string strValidOkCount="";
				string strValidNoCount="";
				DataTable dtCheckList=(DataTable)Session["QUERY"];

				for(int i=0;i<this.DataGrid1.Items.Count;i++)
				{
					foreach(System.Web.UI.Control con1 in this.DataGrid1.Items[i].Cells[17].Controls)
					{
						if(con1 is System.Web.UI.WebControls.TextBox)
						{
							TextBox txt1=con1 as TextBox;
							strTravelCount=txt1.Text.Trim();
						}
					}
					foreach(System.Web.UI.Control con1 in this.DataGrid1.Items[i].Cells[18].Controls)
					{
						if(con1 is System.Web.UI.WebControls.TextBox)
						{
							TextBox txt2=con1 as TextBox;
							strValidOkCount=txt2.Text.Trim();
						}
					}
					foreach(System.Web.UI.Control con1 in this.DataGrid1.Items[i].Cells[19].Controls)
					{
						if(con1 is System.Web.UI.WebControls.TextBox)
						{
							TextBox txt3=con1 as TextBox;
							strValidNoCount=txt3.Text.Trim();
						}
					}

					if(strTravelCount==""||!Regex.IsMatch(strTravelCount,@"^[+|-]{0,1}(\d*)\.{0,1}\d{0,}$"))
					{
						strcheckerror="运输损耗量必须是数字！";
						checkflag=false;
						break;
					}
					if(strValidOkCount==""||!Regex.IsMatch(strValidOkCount,@"^[+|-]{0,1}(\d*)\.{0,1}\d{0,}$"))
					{
						strcheckerror="验收合格量必须是数字！";
						checkflag=false;
						break;
					}
					if(strValidNoCount==""||!Regex.IsMatch(strValidNoCount,@"^[+|-]{0,1}(\d*)\.{0,1}\d{0,}$"))
					{
						strcheckerror="验收不合格量必须是数字！";
						checkflag=false;
						break;
					}
	
					double RealCount=Math.Round(double.Parse(this.DataGrid1.Items[i].Cells[13].Text),2);
					double TravelCount=Math.Round(double.Parse(strTravelCount),2);
					double ValidOkCount=Math.Round(double.Parse(strValidOkCount),2);
					double ValidNoCount=Math.Round(double.Parse(strValidNoCount),2);
					if(TravelCount+ValidOkCount+ValidNoCount!=RealCount)
					{
						strcheckerror="实发量应等于运输损耗量+验收合格量+验收不合格量，请检查！";
						checkflag=false;
						break;
					}
					else
					{
						checkflag=true;	
						int pageindex=this.DataGrid1.CurrentPageIndex;
						dtCheckList.Rows[pageindex*20+i]["cnnTravelCount"]=strTravelCount;
						dtCheckList.Rows[pageindex*20+i]["cnnValidOkCount"]=strValidOkCount;
						dtCheckList.Rows[pageindex*20+i]["cnnValidNoCount"]=strValidNoCount;
					}
				}

				if(checkflag)
				{
					btnEdit.Text="编辑";
					this.btnValidEnter.Enabled=true;

					Session.Remove("QUERY");
					Session["QUERY"]=dtCheckList;
					this.DataGrid1.DataSource=dtCheckList;
					this.DataGrid1.DataBind();
					this.DataGrid1.Columns[14].Visible=true;
					this.DataGrid1.Columns[15].Visible=true;
					this.DataGrid1.Columns[16].Visible=true;
					this.DataGrid1.Columns[17].Visible=false;
					this.DataGrid1.Columns[18].Visible=false;
					this.DataGrid1.Columns[19].Visible=false;
				}
				else
				{
					this.SetErrorMsgPageBydirHistory(strcheckerror);
					return;
				}
			}
		}

//		private void DataGrid1_EditCommand(object source, DataGridCommandEventArgs e)
//		{
//			this.DataGrid1.EditItemIndex=e.Item.ItemIndex;
//			this.DataGrid1.DataSource=(DataTable)Session["QUERY"];
//			this.DataGrid1.DataBind();
//		}
//
//		private void DataGrid1_CancelCommand(object source, DataGridCommandEventArgs e)
//		{
//			this.DataGrid1.EditItemIndex=-1;
//			this.DataGrid1.DataSource=(DataTable)Session["QUERY"];
//			this.DataGrid1.DataBind();
//		}
//
//		private void DataGrid1_UpdateCommand(object source, DataGridCommandEventArgs e)
//		{
//			string strTravelCount=((TextBox)e.Item.Cells[12].Controls[0]).Text.Trim();
//			string strValidOkCount=((TextBox)e.Item.Cells[13].Controls[0]).Text.Trim();
//			string strValidNoCount=((TextBox)e.Item.Cells[14].Controls[0]).Text.Trim();
//			if(strTravelCount==""||!Regex.IsMatch(strTravelCount,@"^[+|-]{0,1}(\d*)\.{0,1}\d{0,}$"))
//			{
//				this.SetErrorMsgPageBydirHistory("运输损耗量必须是数字！");
//				return;
//			}
//			if(strValidOkCount==""||!Regex.IsMatch(strValidOkCount,@"^[+|-]{0,1}(\d*)\.{0,1}\d{0,}$"))
//			{
//				this.SetErrorMsgPageBydirHistory("验收合格量必须是数字！");
//				return;
//			}
//			if(strValidNoCount==""||!Regex.IsMatch(strValidNoCount,@"^[+|-]{0,1}(\d*)\.{0,1}\d{0,}$"))
//			{
//				this.SetErrorMsgPageBydirHistory("验收不合格量必须是数字！");
//				return;
//			}
//
//			double RealCount=Math.Round(double.Parse(e.Item.Cells[11].Text),2);
//			double TravelCount=Math.Round(double.Parse(strTravelCount),2);
//			double ValidOkCount=Math.Round(double.Parse(strValidOkCount),2);
//			double ValidNoCount=Math.Round(double.Parse(strValidNoCount),2);
//			if(TravelCount+ValidOkCount+ValidNoCount!=RealCount)
//			{
//				this.SetErrorMsgPageBydirHistory("实发量应等于运输损耗量+验收合格量+验收不合格量，请检查！");
//				return;
//			}
//
//			DataTable dttmp=(DataTable)Session["QUERY"];
//			Session.Remove("QUERY");
//			dttmp.Rows[e.Item.ItemIndex]["cnnTravelCount"]=TravelCount.ToString();
//			dttmp.Rows[e.Item.ItemIndex]["cnnValidOkCount"]=ValidOkCount.ToString();
//			dttmp.Rows[e.Item.ItemIndex]["cnnValidNoCount"]=ValidNoCount.ToString();
//			Session["QUERY"]=dttmp;
//			this.DataGrid1.EditItemIndex=-1;
//			this.DataGrid1.DataSource=(DataTable)Session["QUERY"];
//			this.DataGrid1.DataBind();
		//		}

		private void DataGrid1_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
		{
			this.DataGrid1.CurrentPageIndex=e.NewPageIndex;
			this.DataGrid1.DataSource=(DataTable)Session["QUERY"];
			this.DataGrid1.DataBind();
			for(int i=0;i<this.DataGrid1.Items.Count;i++)
			{
				foreach(System.Web.UI.Control con1 in this.DataGrid1.Items[i].Cells[17].Controls)
				{
					if(con1 is System.Web.UI.WebControls.TextBox)
					{
						TextBox txt1=con1 as TextBox;
						txt1.Text=this.DataGrid1.Items[i].Cells[14].Text.Trim();
					}
				}
				foreach(System.Web.UI.Control con1 in this.DataGrid1.Items[i].Cells[18].Controls)
				{
					if(con1 is System.Web.UI.WebControls.TextBox)
					{
						TextBox txt2=con1 as TextBox;
						txt2.Text=this.DataGrid1.Items[i].Cells[15].Text.Trim();
					}
				}
				foreach(System.Web.UI.Control con1 in this.DataGrid1.Items[i].Cells[19].Controls)
				{
					if(con1 is System.Web.UI.WebControls.TextBox)
					{
						TextBox txt3=con1 as TextBox;
						txt3.Text=this.DataGrid1.Items[i].Cells[16].Text.Trim();
					}
				}
			}
		}
	}
}
