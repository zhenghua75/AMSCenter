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
using AMSApp.zhenghua;
using AMSApp.zhenghua.Business;
using AMSApp.zhenghua.Entity;

namespace AMSApp.zhenghua.Formula
{
	/// <summary>
	/// wfmMaterial ��ժҪ˵����
	/// </summary>
	public partial class wfmMaterial : wfmBase
	{
	

		protected void Page_Load(object sender, System.EventArgs e)
		{
			// �ڴ˴������û������Գ�ʼ��ҳ��
			if(!this.IsPostBack)
			{
				//��grid
				//BindMaterial();
				Session["tbMaterial"]=null;
			}

			this.FootBar.Visible = false;
			if(Session["tbMaterial"]!=null)
			{
				if(((DataTable)Session["tbMaterial"]).Rows.Count>0)
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
		private void Query()
		{
			string strSql = "select cnvcMaterialCode as cnvcOldMaterialCode,* from tbMaterial where cnvcMaterialCode like '%" + txtMaterialCode.Text +
				"%' and cnvcMaterialName like '%" + txtMaterialName.Text + "%'";
			DataTable dtMaterial = Helper.Query(strSql);
			this.DataTableConvert(dtMaterial, "cnvcLeastUnit", "tbNameCode", "cnvcCode", "cnvcName", "cnvcType = 'LEASTUNIT'");
			this.DataTableConvert(dtMaterial, "cnvcProductType", "tbNameCode", "cnvcCode", "cnvcName", "cnvcType = 'PRODUCTTYPE'");
			this.DataTableConvert(dtMaterial, "cnvcProductClass", "tbProductClass", "cnvcProductClassCode", "cnvcProductClassName", "");
			Session["tbMaterial"] = dtMaterial;
		}
		private void BindMaterial()
		{
			

//			MaterialFacade mf = new MaterialFacade();
//			DataTable dtMaterial = mf.GetMaterials(this.txtMaterialCode.Text,this.txtMaterialName.Text);


//			DataGrid1.DataSource = dtMaterial;
//			DataGrid1.DataBind();
			int iRecordCount = 0;
			if(Session["tbMaterial"] !=null)
			{
				DataTable dtout = (DataTable)Session["tbMaterial"];
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
			this.DataGrid1.CancelCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGrid1_CancelCommand);
			this.DataGrid1.EditCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGrid1_EditCommand);
			this.DataGrid1.UpdateCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGrid1_UpdateCommand);
			this.DataGrid1.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.DataGrid1_ItemDataBound);

		}
		#endregion

		private void DataGrid1_EditCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			//�༭
			this.DataGrid1.EditItemIndex=e.Item.ItemIndex;
			BindMaterial();
		}

		private void DataGrid1_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			//��ҳ
			DataGrid1.CurrentPageIndex = e.NewPageIndex;
			BindMaterial();
		}

		private void DataGrid1_CancelCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			//ȡ��
			this.DataGrid1.EditItemIndex=-1;
			BindMaterial();
		}

		private void DataGrid1_UpdateCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			//����
			try
			{
				//if(!JudgeIsCode())
				Material mat = new Material();
				mat.cnvcMaterialCode = ((TextBox)e.Item.Cells[0].Controls[1]).Text;
				

				mat.cnvcMaterialName = ((TextBox)e.Item.Cells[1].Controls[0]).Text;
				mat.cnvcLeastUnit = ((DropDownList) e.Item.Cells[2].Controls[1]).SelectedValue;
				mat.cnnPrice = decimal.Parse(((TextBox)e.Item.Cells[3].Controls[0]).Text);
				mat.cnvcProductType = ((DropDownList) e.Item.Cells[4].Controls[1]).SelectedValue;
				mat.cnnConversion = decimal.Parse(((TextBox)e.Item.Cells[6].Controls[0]).Text);
				mat.cnvcUnit = ((TextBox)e.Item.Cells[7].Controls[0]).Text;
				mat.cnvcStandardUnit = ((TextBox)e.Item.Cells[8].Controls[0]).Text;
				mat.cnnStatdardCount = decimal.Parse(((TextBox)e.Item.Cells[9].Controls[0]).Text);
				
				mat.cnvcProductClass = ((DropDownList) e.Item.Cells[5].Controls[1]).SelectedValue;

                mat.IsUse = ((CheckBox)e.Item.Cells[10].Controls[1]).Checked;
				if(!JudgeIsCode(mat.cnvcProductType,mat.cnvcProductClass,mat.cnvcMaterialCode))
				{
					//Popup("�������");
					//return;
					throw new Exception("�������");
				}

				string strOldMaterialCode = e.Item.Cells[12].Text;
				
				if(strOldMaterialCode != mat.cnvcMaterialCode)
				{
					string strSql = "select * from tbMaterial where cnvcMaterialCode='"+mat.cnvcMaterialCode+"'";
					DataTable dtMaterial = Helper.Query(strSql);
					if(dtMaterial.Rows.Count > 0)
					{
						//throw new Exception("��ͬ����ԭ�ϲ����Ѵ���");
						Popup("��ͬ����ԭ�ϲ����Ѵ���");
						return;
					}
				}
				
				OperLog operLog = new OperLog();
				operLog.cnvcOperID = oper.strLoginID;
				operLog.cnvcDeptID = oper.strDeptID;
				operLog.cnvcOperType = "�޸�ԭ�ϲ���";

				MaterialFacade mf = new MaterialFacade();
				mf.UpdateMaterial(mat, strOldMaterialCode,operLog);
				this.DataGrid1.EditItemIndex=-1;
                Query();
				BindMaterial();
				Popup("���³ɹ���");
			}
			catch(Exception ex)
			{
				Popup(ex.Message);
				return;
			}
			
			
		}

		private void DataGrid1_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			//�����޸�TextBox�Ŀ��     
			System.Web.UI.WebControls.TextBox   tb;   
			int   intLength;  
 
//			DataTable dtLeastUnit = Helper.BindCommCode("Least");
//			DataTable dtProductType = Helper.BindCommCode("PType");
			

			if(e.Item.ItemType==ListItemType.EditItem)
			{
				DropDownList ddlLeastUnit=(DropDownList)e.Item.FindControl("ddlLeastUnit");

				BindNameCode(ddlLeastUnit, "cnvctype='LEASTUNIT'");

				ddlLeastUnit.Items.FindByText(Convert.ToString(DataBinder.Eval(e.Item.DataItem, "cnvcLeastUnit"))).Selected = true;

				DropDownList ddlProductType=(DropDownList)e.Item.FindControl("ddlProductType");
//				ddlProductType.DataSource = dtProductType;
//				ddlProductType.DataValueField = "vcCommCode";
//				ddlProductType.DataTextField = "vcCommName";
//				ddlProductType.DataBind();

				BindNameCode(ddlProductType, "cnvcType='PRODUCTTYPE' and (cnvcCode='Raw' or cnvcCode='Pack')");

				ddlProductType.Items.FindByValue(Convert.ToString(DataBinder.Eval(e.Item.DataItem, "cnvcProductType"))).Selected = true;

				DropDownList ddlProductClass=(DropDownList)e.Item.FindControl("ddlProductClass");
				BindProductClass(ddlProductClass,
				                 "cnvcProductType='" + Convert.ToString(DataBinder.Eval(e.Item.DataItem, "cnvcProductType")) + "'");
				ListItem li =
					ddlProductClass.Items.FindByValue(Convert.ToString(DataBinder.Eval(e.Item.DataItem, "cnvcProductClass")));
				if(li != null)
				{
					li.Selected = true;
				}
				//ddlProductClass.Items.FindByValue(Convert.ToString(DataBinder.Eval(e.Item.DataItem, "cnvcProductClass"))).Selected = true;

				//ѭ�����е�Ԫ   
				for(int   i=0;   i<e.Item.Cells.Count-1;i++)   
				{   
					//��Ԫ���Ƿ��пؼ�   
					if(e.Item.Cells[i].Controls.Count>0)   
					{   
						//�����TextBox�ؼ�   
						if(e.Item.Cells[i].Controls[0].GetType().ToString()=="System.Web.UI.WebControls.TextBox")   
						{   
							tb   =   (TextBox)e.Item.Cells[i].Controls[0];   
							intLength   =   0;   
							intLength   =   tb.Text.Length;   
							intLength   =   intLength   *   7;   
							if(intLength==0)   intLength=20;   
							tb.Width   =   Unit.Pixel(70);   
							//tb.CssClass="DataGridTextBox";   //���CSS��ʽ������   
						}   
					}   
				}
			}

		}

		protected void btnCancel_Click(object sender, System.EventArgs e)
		{
			//ȡ��
			this.txtMaterialCode.Text = "";
			this.txtMaterialName.Text = "";
			this.DataGrid1.DataSource = null;
			this.DataGrid1.DataBind();
		}

		protected void btnAdd_Click(object sender, System.EventArgs e)
		{
			this.Response.Redirect("./wfmAddMaterial.aspx");
		}

		protected void btnQuery_Click(object sender, System.EventArgs e)
		{
			//��ѯ
			this.DataGrid1.CurrentPageIndex = 0;
			Query();
			this.BindMaterial();

		}
		protected void ddlProductType_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			DropDownList ddl = (DropDownList)sender;
			string strCode = ddl.SelectedValue;
			TableCell cell = (TableCell)ddl.Parent;
			DataGridItem item = (DataGridItem)cell.Parent;
			DropDownList ddlProductClass=(DropDownList)item.FindControl("ddlProductClass");
			BindProductClass(ddlProductClass,
			                 "cnvcProductType='" + strCode + "'");
							
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
			BindMaterial();
		}
		public void ShowPageLabel(Label myLable,int iRecordCount) 
		{
			myLable.Text = "�� " + (DataGrid1.CurrentPageIndex+1) +" ҳ/�� " + DataGrid1.PageCount+" ҳ����"+iRecordCount+"����¼";
		}

        protected void Button1_Click(object sender, EventArgs e)
        {
            //�¼�ԭ�����Ƴ�
            try
			{
                OperLog operLog = new OperLog();
                operLog.cnvcOperID = oper.strLoginID;
                operLog.cnvcDeptID = oper.strDeptID;
                operLog.cnvcOperType = "�¼�ԭ�����Ƴ�";

                MaterialFacade mf = new MaterialFacade();
                mf.DelMaterial(operLog);
                Popup("�Ƴ��ɹ���");
                btnQuery_Click(null, null);
            }
            catch (Exception ex)
            {
                Popup(ex.Message);
                return;
            }
        }

	}
}
