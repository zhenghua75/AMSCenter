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
	/// wfmMaterialQuery ��ժҪ˵����
	/// </summary>
	public partial class wfmMaterialQuery : wfmBase
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// �ڴ˴������û������Գ�ʼ��ҳ��
			if(!this.IsPostBack)
			{
//				string strProductType = "select * from tbCommCode  where vcCommSign = 'PType' and vcCommCode<>'Pack'";
//				DataTable dtProductType = Helper.Query(strProductType);
//				this.ddlProductType.DataSource = dtProductType;
//				this.ddlProductType.DataValueField = "vcCommCode";
//				this.ddlProductType.DataTextField = "vcCommName";
//				this.ddlProductType.DataBind();

				BindNameCode(ddlProductType, "cnvcType = 'PRODUCTTYPE' and cnvcCode<>'Pack' and cnvcCode<>'FINALPRODUCT'");

//				string strSql = "select * from tbProductClass";
//				DataTable dtProductClass = Helper.Query(strSql);
//				this.ddlProductClass.DataSource = dtProductClass;
//				this.ddlProductClass.DataValueField = "cnvcProductClassCode";
//				this.ddlProductClass.DataTextField = "cnvcProductClassName";
//				this.ddlProductClass.DataBind();

				BindProductClass(ddlProductClass, "");

				ListItem li = new ListItem();
				li.Text = "����";
				li.Value = "%";

				this.ddlProductClass.Items.Insert(0, li);
				this.ddlProductType.Items.Insert(0, li);
			}
			
		}

		private void BindGrid()
		{
			string strSql = "select 'False' as cnvcChecked,* from vwProduct where 1=1 ";
			if(ddlProductType.SelectedValue == "%")
			{
				strSql += " and cnvcProductTypeCode <> 'Pack' and cnvcProductTypeCode<>'FINALPRODUCT'";
			}
			else
			{
				strSql += " and cnvcProductTypeCode='" + ddlProductType.SelectedValue+"'";
			}
			
			strSql +=" and cnvcProductClassCode like '%" + ddlProductClass.SelectedValue + "%' and cnvcProductCode like '%" +
			            txtProductCode.Text + "%' and cnvcProductName like '%" + txtProductName.Text + "%'";
			DataTable dtProduct = Helper.Query(strSql);
			if(Session["Dosage"] != null)
			{
				DataTable dtDosage = (DataTable) Session["Dosage"];
				//�޳��е�ԭ��
				foreach(DataRow drDosage in dtDosage.Rows)
				{
					string strProductCode = drDosage["cnvcCode"].ToString();
					DataRow[] drs = dtProduct.Select("cnvcProductCode='" + strProductCode + "'");
					if(drs.Length>0)
					{
						foreach(DataRow dr in drs)
						{
							dtProduct.Rows.Remove(dr);
						}
					}
				}
			}
			DataGrid1.DataSource = dtProduct;
			DataGrid1.DataBind();
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
			this.DataGrid1.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGrid1_ItemCommand);
			this.DataGrid1.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DataGrid1_PageIndexChanged_1);

		}
		#endregion

		protected void btnQuery_Click(object sender, System.EventArgs e)
		{
			//��ѯ
			BindGrid();
			
		}

		protected void btnReturn_Click(object sender, System.EventArgs e)
		{
			//����
			if(Session["Formula"] != null)
			{
				AMSApp.zhenghua.Entity.Formula formula = (AMSApp.zhenghua.Entity.Formula) Session["Formula"];
				this.Response.Redirect("./wfmFormula.aspx?OperFlag="+Session["OperFlag"].ToString()+"&ProductCode="+formula.cnvcProductCode);
			}
			
		}

		private void DataGrid1_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			//��ҳ
			DataGrid1.CurrentPageIndex = e.NewPageIndex;
			BindGrid();
		}
		#region bak
		protected void chkSelected_CheckedChanged(object sender, System.EventArgs e)
		{			
			if(Session["Dosage"] == null)
			{
				Popup("�������ݶ�ȡ����");
				return;
			}
			CheckBox chk = (CheckBox)sender;
			if(chk.Checked)
			{
				TableCell cell = (TableCell)chk.Parent;
				DataGridItem item = (DataGridItem)cell.Parent;
				DataTable dtDosage =  (DataTable) Session["Dosage"];
				
				DataRow drDosage = dtDosage.NewRow();
				drDosage["cnnPrice"] = item.Cells[6].Text;
				drDosage["cnvcCode"] = item.Cells[3].Text;
				drDosage["cnvcName"] = item.Cells[4].Text;
				drDosage["cnvcProductType"] = item.Cells[1].Text;

				DataRow[] drs = dtDosage.Select("cnvcCode='"+item.Cells[3].Text+"'");
				if(drs.Length > 0)
				{
					Popup("���ϱ��������ԭ��");
					return;
				}

				dtDosage.Rows.Add(drDosage);				
			}
			
		}
		#endregion
		protected void btnCancel_Click(object sender, System.EventArgs e)
		{
			//
			this.txtProductCode.Text = "";
			this.txtProductName.Text = "";
			this.DataGrid1.DataSource = null;
			this.DataGrid1.DataBind();
		}

		private void DataGrid1_PageIndexChanged_1(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DataGrid1.CurrentPageIndex = e.NewPageIndex;
			BindGrid();
		}

		private void DataGrid1_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			if(e.CommandName == "Add")
			{
				if(Session["Dosage"] == null)
				{
					Popup("�������ݶ�ȡ����");
					return;
				}
				DataTable dtDosage =  (DataTable) Session["Dosage"];
				
				DataRow drDosage = dtDosage.NewRow();
				drDosage["cnnPrice"] = e.Item.Cells[6].Text;
				drDosage["cnvcCode"] = e.Item.Cells[3].Text;
				drDosage["cnvcName"] = e.Item.Cells[4].Text;
				drDosage["cnvcUnit"] = e.Item.Cells[5].Text;
				drDosage["cnvcProductType"] = e.Item.Cells[7].Text;

				DataRow[] drs = dtDosage.Select("cnvcCode='"+e.Item.Cells[3].Text+"'");
				if(drs.Length > 0)
				{
					Popup("���ϱ��������ԭ��");
					return;
				}

				dtDosage.Rows.Add(drDosage);	
				Session["Dosage"] = dtDosage;
				BindGrid();
			}
		}
	}
}
