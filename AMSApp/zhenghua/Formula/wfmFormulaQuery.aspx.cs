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
using AMSApp.zhenghua.Business;
using AMSApp.zhenghua.Entity;

namespace AMSApp.zhenghua.Formula
{
	/// <summary>
	/// wfmFormulaQuery ��ժҪ˵����
	/// </summary>
	public partial class wfmFormulaQuery : wfmBase
	{
	

		protected void Page_Load(object sender, System.EventArgs e)
		{

			// �ڴ˴������û������Գ�ʼ��ҳ��
			this.btnExcel.Attributes.Add("onclick","javascript:window.open('../../DataGridToExcel.aspx', 'Sample', 'toolbar=no,location=no,directories=no,status=yes,menubar=yes,scrollbars=no,resizable=yes,copyhistory=yes,width=790,height=520,left=0,top=0')");
			if(!this.IsPostBack)
			{
//				string strProductType = "select * from tbCommCode  where vcCommSign = 'PType' and (vcCommCode='SPro' or vcCommCode = 'FPro')";
//				DataTable dtProductType = Helper.Query(strProductType);
//				this.ddlProductType.DataSource = dtProductType;
//				this.ddlProductType.DataValueField = "vcCommCode";
//				this.ddlProductType.DataTextField = "vcCommName";
//				this.ddlProductType.DataBind();

				this.BindNameCode(ddlProductType, "cnvcType = 'PRODUCTTYPE' and (cnvcCode='SEMIPRODUCT' or cnvcCode = 'FINALPRODUCT')");

//				string strSql = "select * from tbProductClass";
//				DataTable dtProductClass = Helper.Query(strSql);
//				this.ddlProductClass.DataSource = dtProductClass;
//				this.ddlProductClass.DataValueField = "cnvcProductClassCode";
//				this.ddlProductClass.DataTextField = "cnvcProductClassName";
//				this.ddlProductClass.DataBind();

                this.ddlProductIsUse.Items.Add("����");
                this.ddlProductIsUse.Items.Add("����");
                this.ddlProductIsUse.Items.Add("�¼�");

				ListItem li = new ListItem("����", "%");
				this.ddlProductType.Items.Insert(0, li);
				BindProductClass(ddlProductClass,
					"cnvcProductType in('SEMIPRODUCT','FINALPRODUCT') and cnvcProductType like '" +
					ddlProductType.SelectedValue + "'");

				
				this.ddlProductClass.Items.Insert(0, li);

				//BindDataGrid();
				Session["tbFormula"]=null;
			}
			this.FootBar.Visible = false;
			if(Session["tbFormula"]!=null)
			{
				if(((DataTable)Session["tbFormula"]).Rows.Count>0)
				{
					this.FootBar.Visible = true;
				}

			}
			if(DataGrid1.DataSource!=null)
			{
				if(((DataTable)DataGrid1.DataSource).Rows.Count>0)
				{
					this.FootBar.Visible = true;
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
			this.DataGrid1.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DataGrid1_PageIndexChanged);

		}
		#endregion

		protected void btnAdd_Click(object sender, System.EventArgs e)
		{
			//���
			this.Response.Redirect("./wfmFormula.aspx?OperFlag=Add&ResetFlag=true");
		}

		protected void btnCancel_Click(object sender, System.EventArgs e)
		{
			//ȡ��
			this.txtProductCode.Text = "";
			this.txtProductName.Text = "";
			this.DataGrid1.DataSource = null;
			this.DataGrid1.DataBind();
		}
		private void Query()
		{
            string strSql = "select cnvcProductCode,cnvcProductName,cnvcProductType,cnvcProductClass,cnnCostSum,cnvcUnit,cnvcFeel,cnvcOrganise,cnvcColor,cnvcTaste,b.nPrice as cnnPrice,CASE a.IsUse when '0' then '�¼�' else '����' end as IsUse from tbFormula a left outer join tbGoods b on a.cnvcProductCode=b.vcGoodsID where 1=1 ";
			if(ddlProductType.SelectedValue == "%")
			{
				strSql += " and (cnvcProductType='SEMIPRODUCT' or cnvcProductType='FINALPRODUCT')";
			}
			else
			{
				strSql += " and cnvcProductType='" + ddlProductType.SelectedValue+"'";
			}
            if (this.ddlProductIsUse.SelectedValue=="����")
            {
                strSql += " and IsUse='1'";
            }
            if (this.ddlProductIsUse.SelectedValue == "�¼�")
            {
                strSql += " and IsUse='0'";
            }
			strSql += " and isnull(cnvcProductClass,'') like '" + ddlProductClass.SelectedValue + "' and cnvcProductCode like '%" +
				txtProductCode.Text + "%' and cnvcProductName like '%" + txtProductName.Text + "%'";
			
			DataTable dtFormula = Helper.Query(strSql);
			this.DataTableConvert(dtFormula, "cnvcProductType", "tbNameCode", "cnvcCode", "cnvcName", "cnvcType='PRODUCTTYPE'");
			this.DataTableConvert(dtFormula, "cnvcProductClass", "tbProductClass", "cnvcProductClassCode", "cnvcProductClassName", "");

			Session["tbFormula"] = dtFormula;


			DataTable dtexcel=dtFormula.Copy();
			dtexcel.Columns["cnvcProductCode"].ColumnName="��Ʒ����";
			dtexcel.Columns["cnvcProductTypeComments"].ColumnName="��Ʒ����";
			dtexcel.Columns["cnvcProductClassComments"].ColumnName="��Ʒ���";
			dtexcel.Columns["cnvcProductName"].ColumnName="��Ʒ����";
			//			dtexcel.Columns["cnvcProductType"].ColumnName="��Ʒ���ͱ���";
			//			dtexcel.Columns["cnvcProductClass"].ColumnName="��Ʒ���ID";
			dtexcel.Columns["cnnCostSum"].ColumnName="�ɱ�";
			dtexcel.Columns["cnnPrice"].ColumnName="����";
			dtexcel.Columns["cnvcUnit"].ColumnName="��λ";
			dtexcel.Columns["cnvcFeel"].ColumnName="�ڸ�";
			dtexcel.Columns["cnvcOrganise"].ColumnName="��֯";
			dtexcel.Columns["cnvcColor"].ColumnName="��ɫ";
			dtexcel.Columns["cnvcTaste"].ColumnName="��ζ";
            dtexcel.Columns["IsUse"].ColumnName = "״̬";
            
			dtexcel.Columns.Remove("cnvcProductType");
			dtexcel.Columns.Remove("cnvcProductClass");
			
			Session["toExcel"]=dtexcel;


		}
		private void BindDataGrid()
		{

			int iRecordCount = 0;
			if(Session["tbFormula"] !=null)
			{
				DataTable dtout = (DataTable)Session["tbFormula"];
				iRecordCount = dtout.Rows.Count;
				this.DataGrid1.DataSource = dtout;
				this.DataGrid1.DataBind();
			}
			if(iRecordCount>0)
			{
				FootBar.Visible = true;
			}
			else
			{
				FootBar.Visible = false;
			}		
			ShowPageLabel(lbPageLabel,iRecordCount);	
		}
		protected void btnQuery_Click(object sender, System.EventArgs e)
		{
			//��ѯ
			Session.Remove("toExcel");
			this.DataGrid1.CurrentPageIndex = 0;
			Query();
			BindDataGrid();
		}

		private void DataGrid1_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DataGrid1.CurrentPageIndex = e.NewPageIndex;
			BindDataGrid();			
		}

		protected void ddlProductType_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			//
			BindProductClass(ddlProductClass,
			                 "cnvcProductType in('SEMIPRODUCT','FINALPRODUCT') and cnvcProductType like '" +
			                 ddlProductType.SelectedValue + "'");
			ListItem li = new ListItem("����", "%");
			this.ddlProductClass.Items.Insert(0, li);
		}

		protected void btnCost_Click(object sender, System.EventArgs e)
		{
			//�ɱ�ˢ��
			try
			{
				OperLog operLog = new OperLog();
				operLog.cnvcOperID = oper.strLoginID;
				operLog.cnvcDeptID = oper.strDeptID;
				operLog.cnvcOperType = "�ɱ�ˢ��";

				MaterialFacade mf = new MaterialFacade();
				mf.UpdateCost(operLog);
				Popup("�ɱ�ˢ�³ɹ�");
			}
			catch(Exception ex)
			{
				Popup(ex.Message);
			}
			
		}

		protected void SetDataGridCurrentPageIndex(DataGrid myDataGrid,string strArg)
		{
			switch(strArg)
			{
				case ("next"):
					if (DataGrid1.CurrentPageIndex < (myDataGrid.PageCount - 1))
						DataGrid1.CurrentPageIndex ++;
					break;
				case ("prev"):
					if (DataGrid1.CurrentPageIndex > 0)
						DataGrid1.CurrentPageIndex --;
					break;
				case ("last"):
					DataGrid1.CurrentPageIndex = (myDataGrid.PageCount - 1);
					break;
				case ("jump"):
					int iTempIndex = Convert.ToInt16(Request["page_number"])-1;//PageNumber.Value)-1;
					if(iTempIndex > DataGrid1.PageCount-1)
						iTempIndex = DataGrid1.PageCount-1;
					if(iTempIndex < 0)
						iTempIndex = 0;
					DataGrid1.CurrentPageIndex = iTempIndex;
					break;
				default:
					//page number
					DataGrid1.CurrentPageIndex = Convert.ToInt32(strArg);
					break;
			}			
		}	
		protected void PagerButtonClick(Object sender, EventArgs e) 
		{
			//used by external paging UI
			String arg = ((LinkButton)sender).CommandArgument;
			SetDataGridCurrentPageIndex(DataGrid1,arg);
			BindDataGrid();
		}
		public void ShowPageLabel(Label myLable,int iRecordCount) 
		{
			myLable.Text = "�� " + (DataGrid1.CurrentPageIndex+1) +" ҳ/�� " + DataGrid1.PageCount+" ҳ����"+iRecordCount+"����¼";
		}

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
			{
                OperLog operLog = new OperLog();
                operLog.cnvcOperID = oper.strLoginID;
                operLog.cnvcDeptID = oper.strDeptID;
                operLog.cnvcOperType = "�¼ܲ�Ʒ�Ƴ�";

                MaterialFacade mf = new MaterialFacade();
                mf.DelFormula(operLog);
                Popup("�Ƴ��ɹ���");
                btnQuery_Click(null, null);
            }
            catch (Exception ex)
            {
                Popup(ex.Message);
            }
        }
	}
}
