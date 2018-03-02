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
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
namespace AMSApp.zhenghua.Formula
{
	/// <summary>
	/// wfmFormula 的摘要说明。
	/// </summary>
	public partial class wfmFormula : wfmBase
	{
		//protected System.Web.UI.WebControls.Image Image1;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
			if(!this.IsPostBack)
			{				
				//清空数据
				this.txtProductCode.Enabled=false;
				if(Request["ResetFlag"] != null)
				{
					Session["Dosage"] = null;
					Session["Packing"] = null;
					Session["OperStandard"] = null;
					Session["Formula"] = null;
					Session["OperFlag"] = null;
					Session["OperFlag"] = "Add";
					DynamicImage1.Image = null;
					lblMaterialCostSum.Text = "";
					lblPackingCostSum.Text = "";
					lblCostSum.Text = "";
				}

				this.BindNameCode(ddlProductType,
				                  "cnvcType = 'PRODUCTTYPE' and (cnvcCode='SEMIPRODUCT' or cnvcCode = 'FINALPRODUCT')");
				this.BindNameCode(ddlUnit, "cnvcType='LEASTUNIT'");
//				if(ddlProductType.SelectedValue == "SEMIPRODUCT")
//				{
//					this.Label5.Visible = true;
//					this.txtPortionCount.Visible = true;
//				}
//				else
//				{
//					this.Label5.Visible = false;
//					this.txtPortionCount.Visible = false;
//				}
				//this.ddlProductType.SelectedIndex = -1;

				//this.BindProductClass(ddlProductClass, "");
				//this.ddlProductClass.SelectedIndex = -1;
				//this.BindProductClass(ddlProductClass, "cnvcProductType='" + ddlProductType.SelectedValue + "'");
				if(this.Request["OperFlag"] == null)
				{
					this.Response.Redirect("../wfmError.aspx");
					return;
				}
				string strOperFlag = this.Request["OperFlag"].ToString();
				Session["OperFlag"] = strOperFlag;
				string strProductCode = "";
				if(strOperFlag != "Add")
				{
					if(this.Request["ProductCode"] == null)
					{
						this.Response.Redirect("../wfmError.aspx");
						return;
					}
					strProductCode = this.Request["ProductCode"].ToString();
				}				
				OperDispControl(strOperFlag, strProductCode);

				BindCode();

//				if(ViewState["Product"] == null)
//				{
//					string strProduct = "select * from vwProduct";	
//					DataTable dtProduct = Helper.Query(strProduct);
//					ViewState["Product"] = dtProduct;
//				}
			}
			//RetainScrollPosition();
			
		}
		#region 控制显示位置
		protected void RetainScrollPosition()
		{
			StringBuilder saveScrollPosition =new StringBuilder ();
			StringBuilder setScrollPosition =new StringBuilder ();
			this.RegisterHiddenField ("__SCROLLPOS","0");
			this.RegisterHiddenField ("__SCROLLWPOS","0");
			saveScrollPosition.Append("<script language='javascript'>") ;
			saveScrollPosition.Append("function saveScrollPosition() {") ;
			saveScrollPosition.Append(" document.forms[0].__SCROLLPOS.value = thebody.scrollTop;") ;
			saveScrollPosition.Append(" document.forms[0].__SCROLLWPOS.value = thebody.scrollLeft;") ;
			saveScrollPosition.Append("}") ;
			saveScrollPosition.Append("thebody.onscroll=saveScrollPosition;") ;
			saveScrollPosition.Append("</script>") ;
			RegisterStartupScript("saveScroll", saveScrollPosition.ToString()) ;

			if(Page.IsPostBack )
			{
				setScrollPosition.Append("<script language='javascript'>") ;
				setScrollPosition.Append("function setScrollPosition() {") ;
				setScrollPosition.Append(" thebody.scrollTop = " + Request["__SCROLLPOS"] + ";") ;
				setScrollPosition.Append(" thebody.scrollLeft = " + Request["__SCROLLWPOS"] + ";") ;
				setScrollPosition.Append("}") ;
				setScrollPosition.Append("thebody.onload=setScrollPosition;") ;
				setScrollPosition.Append("</script>") ;
				RegisterStartupScript("setScroll", setScrollPosition.ToString()) ;

			}
		} 
		#endregion
		private void OperDispControl(string strOperFlag,string strProductCode)
		{
			//添加、修改、查看 时的显示控制		
			DataTable dtDosage = null;
			DataTable dtPacking = null;
			DataTable dtOperStandard = null;
			AMSApp.zhenghua.Entity.Formula formula = null;

			DataTable dtAddDosage = null;
			DataTable dtAddPacking = null;
			DataTable dtAddOperStandard = null;
			if(Session["Dosage"] != null)
			{
				dtAddDosage = (DataTable) Session["Dosage"];
			}
			if(Session["Packing"] != null)
			{
				dtAddPacking = (DataTable) Session["Packing"];
			}
			if(Session["OperStandard"] != null)
			{
				dtAddOperStandard = (DataTable) Session["OperStandard"];
			}
			if(Session["Formula"] != null)
			{
				formula = (AMSApp.zhenghua.Entity.Formula) Session["Formula"];
			}
			
			decimal dSum = 0;
			decimal dCount = 0;
			switch(strOperFlag)
			{
				case "Add"://1、添加是所有控件可用，并显示初始数据
					#region 添加时控件显示控制
					//DropDownList
					ddlProductType.Enabled = true;
					ddlProductClass.Enabled = true;
					ddlUnit.Enabled = true;
					//TextBox
					txtProductCode.Enabled = false;
					txtProductName.Enabled = true;
					txtPortionCount.Enabled = true;
					txtFeel.Enabled = true;
					txtColor.Enabled = true;
					txtOrganise.Enabled = true;
					txtTaste.Enabled = true;
					//DataGrid
					dgDosage.ShowFooter = true;
					dgPacking.ShowFooter = true;
					dgOperStandard.ShowFooter = true;

					lblFormula.Text = "配方添加";

					btnAdd.Visible = true;
					btnModify.Visible = false;
					//btnCancel.Visible = true;
					btnReturn.Visible = true;
					#endregion

					#region 添加时控件数据控制					
					if(formula == null)
					{
						formula = new AMSApp.zhenghua.Entity.Formula();
						Session["Formula"] = formula;
					}
					if(formula.cnvcProductType != "")
					{
						ddlProductType.Items.FindByValue(formula.cnvcProductType).Selected = true;						
					}
					if(ddlProductType.SelectedValue == "SEMIPRODUCT")
					{
						this.Label5.Visible = true;
						this.txtPortionCount.Visible = true;
					}
					else
					{
						this.Label5.Visible = false;
						this.txtPortionCount.Visible = false;
					}
					this.BindProductClass(ddlProductClass, "cnvcProductType='" + ddlProductType.SelectedValue + "' and cnvcProductClassCode<>'8001~8999'");
					if(formula.cnvcProductClass != "")
					{
						//this.BindProductClass(ddlProductClass, "cnvcProductType='" + ddlProductType.SelectedValue + "'");
						ListItem li = ddlProductClass.Items.FindByValue(formula.cnvcProductClass);
						if(li != null)
						{
							li.Selected = true;
						}
						//ddlProductClass.Items.FindByValue(formula.cnvcProductClass).Selected = true;
					}
						
					if(formula.cnvcUnit != "")
						ddlUnit.Items.FindByValue(formula.cnvcUnit).Selected = true;
					txtProductCode.Text = formula.cnvcProductCode;
					txtProductName.Text = formula.cnvcProductName;
					txtPortionCount.Text = ((int)formula.cnnPortionCount).ToString();
					txtFeel.Text = formula.cnvcFeel;
					txtColor.Text = formula.cnvcColor;
					txtOrganise.Text = formula.cnvcOrganise;
					txtTaste.Text = formula.cnvcTaste;
					lblMaterialCostSum.Text = formula.cnnMaterialCostSum.ToString();
					lblPackingCostSum.Text = formula.cnnPackingCostSum.ToString();
					lblCostSum.Text = formula.cnnCostSum.ToString();
					if(formula.cnbProductImage != null)
					{
						MemoryStream   ms   =   new   MemoryStream(formula.cnbProductImage);  
						Bitmap   bmp   =   (Bitmap)System.Drawing.Image.FromStream(ms);
						DynamicImage1.Image = bmp;
					}
					if(dtAddDosage != null)
					{
						this.dgDosage.DataSource = dtAddDosage;
						dSum = 0;
						dCount = 0;
						foreach(DataRow dr in dtAddDosage.Rows)
						{
							if(dr["cnnSum"].ToString() != "")
							{
								dSum += decimal.Parse(dr["cnnSum"].ToString());
							}			
							if(dr["cnnCount"].ToString() != "")
							{
								dCount += decimal.Parse(dr["cnnCount"].ToString());
							}
						}
						this.txtPortionCount.Text = dCount.ToString();
						if(this.ddlUnit.SelectedItem.Text!="克" && ddlUnit.SelectedItem.Text!="毫升")
						{
							this.txtPortionCount.Text="1";
						}
						lblMaterialCostSum.Text = dSum.ToString();
					}
					else
					{
						dtDosage = BindDefaultDosage();	
						this.dgDosage.DataSource = dtDosage;
						Session["Dosage"] = dtDosage;
					}					
					this.dgDosage.DataBind();

					
					if(dtAddPacking != null)
					{
						this.dgPacking.DataSource = dtAddPacking;
						dSum = 0;
						foreach(DataRow dr in dtAddPacking.Rows)
						{
							dSum += decimal.Parse(dr["cnnSum"].ToString());
						}
						lblPackingCostSum.Text = dSum.ToString();
					}
					else
					{
						dtPacking = BindDefaultPacking();
						this.dgPacking.DataSource = dtPacking;
						Session["Packing"] = dtPacking;
					}					
					this.dgPacking.DataBind();

					
					if(dtAddOperStandard != null)
					{
						this.dgOperStandard.DataSource = dtAddOperStandard;
					}
					else
					{
						dtOperStandard = BindDefaultOperStandard();
						this.dgOperStandard.DataSource = dtOperStandard;
						Session["OperStandard"] = dtOperStandard;
					}					
					this.dgOperStandard.DataBind();

					SetCostSum();
					#endregion
					return;
				case "Edit":
					#region 修改时控件显示控制
					//DropDownList
					ddlProductType.Enabled = false;
					ddlProductClass.Enabled = false;
					ddlUnit.Enabled = true;
					//TextBox
					txtProductCode.Enabled = false;
					txtProductName.Enabled = true;
					txtPortionCount.Enabled = true;
					txtFeel.Enabled = true;
					txtColor.Enabled = true;
					txtOrganise.Enabled = true;
					txtTaste.Enabled = true;
					//DataGrid
					dgDosage.ShowFooter = true;
					dgPacking.ShowFooter = true;
					dgOperStandard.ShowFooter = true;

					btnAdd.Visible = false;
					btnModify.Visible = true;
					//btnCancel.Visible = true;
					btnReturn.Visible = true;
					#endregion

					#region 修改时控件数据控制
					
					BindFormula(strProductCode);

					if(dtAddDosage != null)
					{
						this.dgDosage.DataSource = dtAddDosage;		
						//this.SetMaterialCostSum(dtAddDosage);
					}
					else
					{
						dtDosage = BindDosage(strProductCode);
						Session["Dosage"] = dtDosage;
						this.dgDosage.DataSource = dtDosage;	
						//this.SetMaterialCostSum(dtDosage);
					}
					this.dgDosage.DataBind();


					if(dtAddPacking != null)
					{
						this.dgPacking.DataSource = dtAddPacking;
						//this.SetPackingCostSum(dtAddPacking);
					}
					else
					{
						dtPacking = BindPacking(strProductCode);
						this.dgPacking.DataSource = dtPacking;
						Session["Packing"] = dtPacking;
						//this.SetPackingCostSum(dtPacking);
					}
					this.dgPacking.DataBind();
					

					if(dtAddOperStandard != null)
					{
						this.dgOperStandard.DataSource = dtAddOperStandard;
					}
					else
					{
						dtOperStandard = BindOperStandard(strProductCode);
						this.dgOperStandard.DataSource = dtOperStandard;
						Session["OperStandard"] = dtOperStandard;
					}					
					this.dgOperStandard.DataBind();
					#endregion
					return;
				case "Detail":
					#region 查询时控件显示控制
					//DropDownList
					ddlProductType.Enabled = false;
					ddlProductClass.Enabled = false;
					ddlUnit.Enabled = false;
					//TextBox
					txtProductCode.Enabled = false;
					txtProductName.Enabled = false;
					txtPortionCount.Enabled = false;
					txtFeel.Enabled = false;
					txtColor.Enabled = false;
					txtOrganise.Enabled = false;
					txtTaste.Enabled = false;
					//DataGrid
					DataGridColumn dgcd6 = dgDosage.Columns[6];
					DataGridColumn dgcd7 = dgDosage.Columns[7];
					dgDosage.Columns.Remove(dgcd6);
					dgDosage.Columns.Remove(dgcd7);
					dgDosage.ShowFooter = false;

					DataGridColumn dgcp5 = dgPacking.Columns[5];
					DataGridColumn dgcp6 = dgPacking.Columns[6];
					dgPacking.Columns.Remove(dgcp5);
					dgPacking.Columns.Remove(dgcp6);
					dgPacking.ShowFooter = false;

					DataGridColumn dgco3 = dgOperStandard.Columns[3];
					DataGridColumn dgco4 = dgOperStandard.Columns[4];
					dgOperStandard.Columns.Remove(dgco3);
					dgOperStandard.Columns.Remove(dgco4);
					dgOperStandard.ShowFooter = false;

					fileImage.Visible = false;
					btnFileImage.Visible = false;
					btnAdd.Visible = false;
					btnModify.Visible = false;
					//btnCancel.Visible = false;
					btnReturn.Visible = true;

					lblFormula.Text = "配方资料查看";
					#endregion

					#region 查询时控件数据控制
					BindFormula(strProductCode);

					dtDosage = BindDosage(strProductCode);
					this.dgDosage.DataSource = dtDosage;
					this.dgDosage.DataBind();

					dtPacking = BindPacking(strProductCode);
					this.dgPacking.DataSource = dtPacking;
					this.dgPacking.DataBind();

					dtOperStandard = BindOperStandard(strProductCode);
					this.dgOperStandard.DataSource = dtOperStandard;
					this.dgOperStandard.DataBind();
					#endregion
					return;
				case "Return":
					return;
				default:
					return;
			}
		}

		#region 设置汇总项
		private void SetCostSum()
		{
			if(lblMaterialCostSum.Text != "")
			{
				lblCostSum.Text = lblMaterialCostSum.Text;
				if(lblPackingCostSum.Text != "")
				{
					lblCostSum.Text = (decimal.Parse(lblMaterialCostSum.Text)+decimal.Parse(lblPackingCostSum.Text)).ToString();
				}
			}
			else
			{
				lblCostSum.Text = lblPackingCostSum.Text;
			}
		}
		private void SetMaterialCostSum(DataTable dtDosage)
		{
			decimal dSum = 0;
			decimal dCount = 0;
			foreach(DataRow dr in dtDosage.Rows)
			{
				if(dr["cnnSum"].ToString() != "")
					dSum += decimal.Parse(dr["cnnSum"].ToString());
				if(dr["cnnCount"].ToString() != "")
					dCount += decimal.Parse(dr["cnnCount"].ToString());
			}
			lblMaterialCostSum.Text = dSum.ToString();
			this.txtPortionCount.Text = dCount.ToString();
			if(this.ddlUnit.SelectedItem.Text!="克" && ddlUnit.SelectedItem.Text!="毫升")
			{
				this.txtPortionCount.Text="1";
			}
		}
		private void SetPackingCostSum(DataTable dtPacking)
		{
			decimal dSum = 0;
			foreach(DataRow dr in dtPacking.Rows)
			{
				if(dr["cnnSum"].ToString() != "")
					dSum += decimal.Parse(dr["cnnSum"].ToString());
			}
			lblPackingCostSum.Text = dSum.ToString();
		}
		#endregion

		#region 绑定缺省数据
		private DataTable  BindDefaultDosage()
		{
			Dosage dosage = new Dosage();
			DataTable dtDosage = dosage.ToTable().Clone();
			return dtDosage;
		}
		private DataTable  BindDefaultPacking()
		{
			Dosage dosage = new Dosage();
			DataTable dtDosage = dosage.ToTable().Clone();
			return dtDosage;
		}
		private DataTable BindDefaultOperStandard()
		{
			OperStandard oStandard = new OperStandard();
			DataTable dtOperStandard = oStandard.ToTable().Clone();
			return dtOperStandard;
		}
		#endregion
		private void BindFormula(string strProductCode)
		{
			string strSql = "select * from tbFormula where cnvcProductCode = '"+strProductCode+"'";
			DataTable dtFormula = Helper.Query(strSql);
			AMSApp.zhenghua.Entity.Formula formula = new AMSApp.zhenghua.Entity.Formula(dtFormula);
			Session["Formula"] = formula;
			if(formula.cnvcProductType != "")
			{
				ddlProductType.Items.FindByValue(formula.cnvcProductType).Selected = true;
			}			
			if(ddlProductType.SelectedValue == "SEMIPRODUCT")
			{
				this.Label5.Visible = true;
				this.txtPortionCount.Visible = true;
			}
			else
			{
				this.Label5.Visible = false;
				this.txtPortionCount.Visible = false;
			}
			this.BindProductClass(ddlProductClass, "cnvcProductType='" + ddlProductType.SelectedValue + "' and cnvcProductClassCode<>'8001~8999'");
			if(formula.cnvcProductClass != "")
			{
				
				ListItem li = ddlProductClass.Items.FindByValue(formula.cnvcProductClass);
				if(li != null)
				{
					li.Selected = true;
				}
				//ddlProductClass.Items.FindByValue(formula.cnvcProductClass).Selected = true;
			}
			if(formula.cnvcUnit != "")
			ddlUnit.Items.FindByValue(formula.cnvcUnit).Selected = true;
			txtProductCode.Text = formula.cnvcProductCode;
			txtProductName.Text = formula.cnvcProductName;
			txtPortionCount.Text = formula.cnnPortionCount.ToString();
			txtFeel.Text = formula.cnvcFeel;
			txtColor.Text = formula.cnvcColor;
			txtOrganise.Text = formula.cnvcOrganise;
			txtTaste.Text = formula.cnvcTaste;
			lblMaterialCostSum.Text = formula.cnnMaterialCostSum.ToString();
			lblPackingCostSum.Text = formula.cnnPackingCostSum.ToString();
			lblCostSum.Text = formula.cnnCostSum.ToString();

			chkIsUse.Checked = !formula.IsUse;
			if(formula.cnbProductImage != null)
			{
				MemoryStream   ms   =   new   MemoryStream(formula.cnbProductImage);  
				Bitmap   bmp   =   (Bitmap)System.Drawing.Image.FromStream(ms);
				DynamicImage1.Image = bmp;
			}
		}
		#region 绑定数据
		private DataTable  BindDosage(string strProductCode)
		{
			string strSql = "select * from tbDosage where cnvcProductCode = '"+strProductCode+"' and cnvcProductType<>'Pack' order by cnvcCode";
			DataTable dtDosage = Helper.Query(strSql);
			return dtDosage;
		}
		private DataTable  BindPacking(string strProductCode)
		{
			string strSql = "select * from tbDosage where cnvcProductCode = '"+strProductCode+"' and cnvcProductType='Pack' order by cnvcCode";
			DataTable dtDosage = Helper.Query(strSql);
			return dtDosage;
		}
		private DataTable BindOperStandard(string strProductCode)
		{
			string strSql = "select * from tbOperStandard where cnvcProductCode = '"+strProductCode+"' order by cnnSort";
			DataTable dtOperSandard = Helper.Query(strSql);
			return dtOperSandard;
		}
		#endregion

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
			this.dgDosage.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dgDosage_ItemCommand);
			this.dgDosage.CancelCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dgDosage_CancelCommand);
			this.dgDosage.EditCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dgDosage_EditCommand);
			this.dgDosage.UpdateCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dgDosage_UpdateCommand);
			this.dgDosage.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgDosage_ItemDataBound);
			this.dgPacking.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dgPacking_ItemCommand);
			this.dgPacking.CancelCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dgPacking_CancelCommand);
			this.dgPacking.EditCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dgPacking_EditCommand);
			this.dgPacking.UpdateCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dgPacking_UpdateCommand);
			this.dgPacking.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgPacking_ItemDataBound);
			this.dgOperStandard.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dgOperStandard_ItemCommand);
			this.dgOperStandard.CancelCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dgOperStandard_CancelCommand);
			this.dgOperStandard.EditCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dgOperStandard_EditCommand);
			this.dgOperStandard.UpdateCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dgOperStandard_UpdateCommand);
			this.dgOperStandard.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgOperStandard_ItemDataBound);

		}
		#endregion		

		#region 包材
		private void dgPacking_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
//			DataTable dtProduct = null;
//			DataTable dtPacking = null;
//			if(ViewState["Product"] == null)
//			{
//				string strProduct = "select * from vwProduct";	
//				dtProduct = Helper.Query(strProduct);
//				ViewState["Product"] = dtProduct;
//			}
//			if(Application["vwProduct"] == null)
//			{
//				Popup("产品信息导入失败");
//				return;
//			}
//			if(ViewState["Packing"] == null)
//			{
//				dtProduct = (DataTable) ViewState["Product"];
//				dtPacking = dtProduct.Clone();			
//				DataRow[] drPackings = dtProduct.Select("cnvcProductTypeCode='Pack'");
//				int iColloms = dtProduct.Columns.Count;
//				foreach(DataRow drPacking in drPackings)
//				{
//					object[] oArray = new object[iColloms];
//					drPacking.ItemArray.CopyTo(oArray, 0);
//					dtPacking.Rows.Add(oArray);
//				}
//				ViewState["Packing"] = dtPacking;
//			}
//			else
//			{
//				dtPacking = (DataTable) ViewState["Packing"];
//			}
			
			if(e.Item.ItemType==ListItemType.Footer)
			{
				DataTable dtPacking = Helper.Query("select cnvcProductCode+'-'+cnvcProductName as cnvccodename,* from vwProduct where cnvcProductTypeCode='Pack' order by cnvcProductName ");
				DropDownList ddlFooterPacking=(DropDownList)e.Item.FindControl("ddlFooterPacking");
				ddlFooterPacking.DataSource = dtPacking;
				ddlFooterPacking.DataValueField = "cnvcProductCode";
				ddlFooterPacking.DataTextField = "cnvcProductName";				
				ddlFooterPacking.DataBind();			
				ListItem li = new ListItem("", "");
				ddlFooterPacking.Items.Insert(0, li);
			}			

			if(e.Item.ItemType == ListItemType.Item ||e.Item.ItemType == ListItemType.AlternatingItem)
			{
				if(e.Item.Cells.Count == 8)
				{
					LinkButton btnDelete = (LinkButton)(e.Item.Cells[6].Controls[1]);
					btnDelete.Attributes.Add("onClick","JavaScript:return confirm('确定删除？')");
					e.Item.Attributes.Add("onMouseOver","this.style.backgroundColor='#FFCC66'");
					e.Item.Attributes.Add("onMouseOut","this.style.backgroundColor='#ffffff'");
				}
			} 
			
		}

		private void dgPacking_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			if(Session["Packing"] == null)
			{
				Popup("程序异常，未找到包材数据");
				return;
			}	
			if(e.CommandName == "Add")
			{						
				string strCount = ((TextBox) e.Item.Cells[2].Controls[1]).Text;
				if(strCount == "")
				{
					Popup("请输入用量");
					return;
				}
				if(!Regex.IsMatch(strCount,@"^[+|-]{0,1}(\d*)\.{0,1}\d{0,}$"))
				{
					Popup("请输入数字");
					return;
				}
				DataTable dtPacking = (DataTable) Session["Packing"];
				Dosage dosage = new Dosage();
				dosage.cnvcCode = ((DropDownList) e.Item.Cells[0].FindControl("ddlFooterPacking")).SelectedValue;
				dosage.cnvcName = ((DropDownList) e.Item.Cells[0].FindControl("ddlFooterPacking")).SelectedItem.Text;
				dosage.cnvcProductType = "Pack";
				dosage.cnnCount = decimal.Parse(((TextBox)e.Item.Cells[2].Controls[1]).Text);
				dosage.cnnPrice = decimal.Parse(e.Item.Cells[3].Text);
				dosage.cnnSum = dosage.cnnCount*dosage.cnnPrice;
				dosage.cnvcUnit = e.Item.Cells[1].Text;
				
				DataRow[] drPacking = dtPacking.Select("cnvcCode='" + dosage.cnvcCode + "'");
				if(drPacking.Length > 0)
				{
					Popup("相同包材已存在");
					return;
				}
				
				DataRow drDosage = dosage.ToRow();
				object[] oArray = new object[dtPacking.Columns.Count];
				drDosage.ItemArray.CopyTo(oArray,0);
				dtPacking.Rows.Add(oArray);
				Session["Packing"] = dtPacking;
				dgPacking.DataSource = dtPacking;
				dgPacking.DataBind();
				
				decimal dSum = 0;
				foreach(DataRow dr in dtPacking.Rows)
				{
					dSum += decimal.Parse(dr["cnnSum"].ToString());
				}
				lblPackingCostSum.Text = dSum.ToString();
				SetCostSum();
				
			}
			if(e.CommandName == "Delete")
			{
				string strCode = e.Item.Cells[7].Text;
				DataTable dtPacking = (DataTable) Session["Packing"];
				DataRow[] drs = dtPacking.Select("cnvcCode='" + strCode + "'");
				dtPacking.Rows.Remove(drs[0]);
				Session["Packing"] = dtPacking;
				dgPacking.DataSource = dtPacking;
				dgPacking.DataBind();

				decimal dSum = 0;
				foreach(DataRow dr in dtPacking.Rows)
				{
					dSum += decimal.Parse(dr["cnnSum"].ToString());
				}
				lblPackingCostSum.Text = dSum.ToString();
				SetCostSum();
			}
			if(e.CommandName == "Query")
			{
				string strquery = ((TextBox) e.Item.Cells[4].FindControl("txtQuery")).Text;

				DataTable dtPacking = Helper.Query("select * from vwProduct where cnvcProductTypeCode='Pack' and cnvcProductName like '%"+strquery+"%'");
				DropDownList ddlFooterPacking=(DropDownList)e.Item.FindControl("ddlFooterPacking");
				ddlFooterPacking.DataSource = dtPacking;
				ddlFooterPacking.DataValueField = "cnvcProductCode";
				ddlFooterPacking.DataTextField = "cnvcProductName";				
				ddlFooterPacking.DataBind();	
				ListItem li = new ListItem("", "");
				ddlFooterPacking.Items.Insert(0, li);
			}
		}
		protected void ddlFooterPacking_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			DropDownList ddl = (DropDownList)sender;
			string strCode = ddl.SelectedValue;

			DataTable dtProduct = Helper.Query("select cnvcUnit,cnnPrice from vwProduct where cnvcProductCode='" + strCode + "'");
			TableCell cell = (TableCell)ddl.Parent;
			DataGridItem item = (DataGridItem)cell.Parent;
			item.Cells[1].Text = dtProduct.Rows[0]["cnvcUnit"].ToString();
			item.Cells[3].Text = dtProduct.Rows[0]["cnnPrice"].ToString();
							
		}

		private void dgPacking_EditCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			//编辑
			dgPacking.EditItemIndex = e.Item.ItemIndex;
			DataTable dtPacking = (DataTable) Session["Packing"];
			dgPacking.DataSource = dtPacking;
			dgPacking.DataBind();
		}

		private void dgPacking_CancelCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			dgPacking.EditItemIndex = -1;
			DataTable dtPacking = (DataTable) Session["Packing"];
			dgPacking.DataSource = dtPacking;
			dgPacking.DataBind();
		}

		private void dgPacking_UpdateCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			try
			{
				if(Session["Packing"] == null)
				{
					Popup("程序异常，未找到包材数据");
				}			
				string strCount = ((TextBox) e.Item.Cells[2].Controls[1]).Text;
				if(strCount == "")
				{
					Popup("请输入用量");
					return;
				}
				if(!Regex.IsMatch(strCount,@"^[+|-]{0,1}(\d*)\.{0,1}\d{0,}$"))
				{
					Popup("请输入数字");
					return;
				}
				string strCode = e.Item.Cells[7].Text;
				DataTable dtPacking = (DataTable) Session["Packing"];
				Dosage dosage = new Dosage();
				dosage.cnvcCode = strCode;
				dosage.cnnCount = decimal.Parse(((TextBox)e.Item.Cells[2].Controls[1]).Text);
				dosage.cnnPrice = decimal.Parse(((Label)e.Item.Cells[3].Controls[1]).Text);
				dosage.cnnSum = dosage.cnnCount*dosage.cnnPrice;
				dosage.cnvcUnit = ((Label)e.Item.Cells[1].Controls[1]).Text;
				
				DataRow[] drPacking = dtPacking.Select("cnvcCode='" + dosage.cnvcCode + "'");
				drPacking[0]["cnnCount"] = dosage.cnnCount;
				drPacking[0]["cnnSum"] = dosage.cnnSum;

				dgPacking.EditItemIndex = -1;				
				dgPacking.DataSource = dtPacking;
				dgPacking.DataBind();

				Session["Packing"] = dtPacking;

				decimal dSum = 0;
				foreach(DataRow dr in dtPacking.Rows)
				{
					dSum += decimal.Parse(dr["cnnSum"].ToString());
				}
				lblPackingCostSum.Text = dSum.ToString();
				SetCostSum();
			}
			catch(Exception ex)
			{
				Popup(ex.Message);
			}
			
		}
		
		#endregion

		#region 操作标准
		private void dgOperStandard_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			if(Session["OperStandard"] == null)
			{
				Popup("操作标准数据读取异常");
				return;
			}
			if(e.CommandName == "Add")
			{
				string strSort = ((TextBox) e.Item.Cells[0].Controls[1]).Text;
				if(strSort == "")
				{
					Popup("请输入序号");
					return;
				}
				if(!Regex.IsMatch(strSort,@"^[+|-]{0,1}(\d*)\.{0,1}\d{0,}$"))
				{
					Popup("序号请输入数字");
					return;
				}
				DataTable dtOperStandard = (DataTable) Session["OperStandard"];
				
				DataRow[] drOperStandards = dtOperStandard.Select("cnnSort='" + strSort + "'");
				if(drOperStandards.Length > 0)
				{
					Popup("相同序号操作标准已存在");
					return;
				}								

				//添加操作标准会话				
				OperStandard os = new OperStandard();
				os.cnnSort = int.Parse(((TextBox) e.Item.Cells[0].Controls[1]).Text);
				os.cnvcStandard = ((TextBox) e.Item.Cells[1].Controls[1]).Text;
				os.cnvcKey = ((TextBox) e.Item.Cells[2].Controls[1]).Text;

				DataRow drDosage = os.ToRow();
				object[] oArray = new object[dtOperStandard.Columns.Count];
				drDosage.ItemArray.CopyTo(oArray,0);
				dtOperStandard.Rows.Add(oArray);
				DataView dv = dtOperStandard.DefaultView;
				dv.Sort = "cnnSort";
				Session["OperStandard"] = dtOperStandard;
				dgOperStandard.DataSource = dv;
				dgOperStandard.DataBind();

			}
			if(e.CommandName == "Delete")
			{
				string strSort = ((Label)e.Item.Cells[0].Controls[1]).Text;
				DataTable dtOperStandard = (DataTable) Session["OperStandard"];
				DataRow[] drs = dtOperStandard.Select("cnnSort=" + strSort );
				dtOperStandard.Rows.Remove(drs[0]);
				DataView dv = dtOperStandard.DefaultView;
				dv.Sort = "cnnSort";
				dgOperStandard.EditItemIndex = -1;
				dgOperStandard.DataSource = dv;
				dgOperStandard.DataBind();
				Session["OperStandard"] = dtOperStandard;
			}
		}

		private void dgOperStandard_EditCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			if(Session["OperStandard"] == null)
			{
				Popup("操作标准数据读取异常");
				return;
			}
			DataTable dtOperStandard = (DataTable) Session["OperStandard"];
			DataView dv = dtOperStandard.DefaultView;
			dv.Sort = "cnnSort";
			dgOperStandard.EditItemIndex = e.Item.ItemIndex;
			dgOperStandard.DataSource = dv;
			dgOperStandard.DataBind();
			
		}

		private void dgOperStandard_CancelCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			dgOperStandard.EditItemIndex = -1;
			if(Session["OperStandard"] == null)
			{
				Popup("操作标准数据读取异常");
				return;
			}
			DataTable dtOperStandard = (DataTable) Session["OperStandard"];
			DataView dv = dtOperStandard.DefaultView;
			dv.Sort = "cnnSort";
			
			dgOperStandard.DataSource = dv;
			dgOperStandard.DataBind();
		}

		private void dgOperStandard_UpdateCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{			
			try
			{
				if(Session["OperStandard"] == null)
				{
					Popup("操作标准数据读取异常");
					return;
				}		
				string strSort =  ((Label)e.Item.Cells[0].Controls[1]).Text;
				DataTable dtOperStandard = (DataTable) Session["OperStandard"];
				
				DataRow[] drOperStandards = dtOperStandard.Select("cnnSort='" + strSort + "'");

				drOperStandards[0]["cnvcStandard"] = ((TextBox) e.Item.Cells[1].Controls[1]).Text;
				drOperStandards[0]["cnvcKey"] = ((TextBox) e.Item.Cells[2].Controls[1]).Text;

				DataView dv = dtOperStandard.DefaultView;
				dv.Sort = "cnnSort";
				dgOperStandard.EditItemIndex = -1;
				dgOperStandard.DataSource = dv;
				dgOperStandard.DataBind();
				Session["OperStandard"] = dtOperStandard;
			}
			catch(Exception ex)
			{
				Popup(ex.Message);
			}
		}

		private void dgOperStandard_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			if(e.Item.ItemType == ListItemType.Item ||e.Item.ItemType == ListItemType.AlternatingItem)
			{
				if(e.Item.Cells.Count == 5)
				{
					LinkButton btnDelete = (LinkButton)(e.Item.Cells[4].Controls[1]);
					btnDelete.Attributes.Add("onClick","JavaScript:return confirm('确定删除？')");
					e.Item.Attributes.Add("onMouseOver","this.style.backgroundColor='#FFCC66'");
					e.Item.Attributes.Add("onMouseOut","this.style.backgroundColor='#ffffff'");
				}
			} 
		}
		#endregion

		#region 配料表
		private void dgDosage_EditCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{			
			if(Session["Dosage"] == null)
			{
				Popup("配料数据读取异常");
				return;
			}
			dgDosage.EditItemIndex = e.Item.ItemIndex;
			DataTable dtDosage = (DataTable) Session["Dosage"];
			dgDosage.DataSource = dtDosage;
			dgDosage.DataBind();
		}

		private void dgDosage_CancelCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			if(Session["Dosage"] == null)
			{
				Popup("配料数据读取异常");
				return;
			}
			dgDosage.EditItemIndex = -1;
			DataTable dtDosage = (DataTable) Session["Dosage"];
			dgDosage.DataSource = dtDosage;
			dgDosage.DataBind();
		}

		private void dgDosage_UpdateCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			try
			{
				if(Session["Dosage"] == null)
				{
					Popup("配料数据读取异常");
					return;
				}
				string strCount = ((TextBox) e.Item.Cells[3].Controls[0]).Text;
				if(strCount == "")
				{
					Popup("请输入用量");
					return;
				}
				if(!Regex.IsMatch(strCount,@"^[+|-]{0,1}(\d*)\.{0,1}\d{0,}$"))
				{
					Popup("请输入数字");
					return;
				}

				dgDosage.EditItemIndex = -1;
				string strCode = e.Item.Cells[0].Text;
				DataTable dtDosage = (DataTable) Session["Dosage"];
				DataRow[] drDosages = dtDosage.Select("cnvcCode='" + strCode + "'");
				if(drDosages.Length > 0)
				{
					//string strCount = ((TextBox) e.Item.Cells[3].Controls[1]).Text;
					string strPrice = e.Item.Cells[4].Text;
					string strSum = (decimal.Parse(strCount)*decimal.Parse(strPrice)).ToString();
					drDosages[0]["cnnCount"] = strCount;
					drDosages[0]["cnnSum"] = strSum;
				}
				dgDosage.DataSource = dtDosage;
				dgDosage.DataBind();
				Session["Dosage"] = dtDosage;

				decimal dSum = 0;
				decimal dCount = 0;
				foreach(DataRow dr in dtDosage.Rows)
				{
					if(dr["cnnSum"].ToString() != "")
					{
						dSum += decimal.Parse(dr["cnnSum"].ToString());
					}		
					if(dr["cnnCount"].ToString() != "")
					{
						dCount += decimal.Parse(dr["cnnCount"].ToString());
					}
				}
				lblMaterialCostSum.Text = dSum.ToString();
				this.txtPortionCount.Text = dCount.ToString();
				if(this.ddlUnit.SelectedItem.Text!="克" && ddlUnit.SelectedItem.Text!="毫升")
				{
					this.txtPortionCount.Text="1";
				}
				SetCostSum();
			}
			catch(Exception ex)
			{
				Popup(ex.Message);
			}			
		}

		private void dgDosage_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			if(e.Item.ItemType == ListItemType.Item ||e.Item.ItemType == ListItemType.AlternatingItem)
			{				
				if(e.Item.Cells.Count == 8)
				{
					LinkButton btnDelete = (LinkButton)(e.Item.Cells[7].Controls[1]);
					btnDelete.Attributes.Add("onClick","JavaScript:return confirm('确定删除？')");
					e.Item.Attributes.Add("onMouseOver","this.style.backgroundColor='#FFCC66'");
					e.Item.Attributes.Add("onMouseOut","this.style.backgroundColor='#ffffff'");
				}
			} 
			//控制修改TextBox的宽度     
			System.Web.UI.WebControls.TextBox   tb;   
			int   intLength;  
			if(e.Item.ItemType==ListItemType.EditItem)
			{
				//循环所有单元   
				for(int   i=0;   i<e.Item.Cells.Count-1;i++)   
				{   
					//单元内是否有控件   
					if(e.Item.Cells[i].Controls.Count>0)   
					{   
						//如果是TextBox控件   
						if(e.Item.Cells[i].Controls[0].GetType().ToString()=="System.Web.UI.WebControls.TextBox")   
						{   
							tb   =   (TextBox)e.Item.Cells[i].Controls[0];   
							intLength   =   0;   
							intLength   =   tb.Text.Length;   
							intLength   =   intLength   *   7;   
							if(intLength==0)   intLength=20;   
							tb.Width   =   Unit.Pixel(70);   
							//tb.CssClass="DataGridTextBox";   //你的CSS样式表名称   
						}   
					}   
				}
			}
		}
		private void dgDosage_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			if(e.CommandName == "Add")
			{
				//保持数据
				if(Session["Formula"] == null)
				{
					Popup("配方数据读取错误");
					return;
				}
				AMSApp.zhenghua.Entity.Formula formula = (AMSApp.zhenghua.Entity.Formula) Session["Formula"];				
				formula.cnvcProductType = ddlProductType.SelectedValue;				
				formula.cnvcProductClass = ddlProductClass.SelectedValue;				
				formula.cnvcUnit = ddlUnit.SelectedValue;
				formula.cnvcProductCode = txtProductCode.Text;
				formula.cnvcProductName = txtProductName.Text;
				if(txtPortionCount.Text != "")
				{
					formula.cnnPortionCount = decimal.Parse(txtPortionCount.Text);
				}				
				formula.cnvcFeel = txtFeel.Text;
				formula.cnvcColor = txtColor.Text;
				formula.cnvcOrganise = txtOrganise.Text;
				formula.cnvcTaste = txtTaste.Text;

				Session["Formula"] = formula;

				this.Response.Redirect("wfmMaterialQuery.aspx?OperFlag='"+Session["OperFlag"].ToString()+"'");
			}
			if(e.CommandName == "Delete")
			{
				string strCode = e.Item.Cells[0].Text;
				DataTable dtDosage = (DataTable) Session["Dosage"];
				DataRow[] drs = dtDosage.Select("cnvcCode='" + strCode + "'");
				dtDosage.Rows.Remove(drs[0]);
				Session["Dosage"] = dtDosage;
				dgDosage.DataSource = dtDosage;
				dgDosage.DataBind();

				decimal dSum = 0;
				decimal dCount = 0;
				foreach(DataRow dr in dtDosage.Rows)
				{
					if(dr["cnnSum"].ToString() != "")
					{
						dSum += decimal.Parse(dr["cnnSum"].ToString());
					}
					if(dr["cnnCount"].ToString() != "")
					{
						dCount += decimal.Parse(dr["cnnCount"].ToString());
					}
				}
				lblMaterialCostSum.Text = dSum.ToString();
				this.txtPortionCount.Text = dCount.ToString();
				if(this.ddlUnit.SelectedItem.Text!="克" && ddlUnit.SelectedItem.Text!="毫升")
				{
					this.txtPortionCount.Text="1";
				}
				SetCostSum();
			}
		}
		#endregion

		protected void btnReturn_Click(object sender, System.EventArgs e)
		{
			//返回
			Session["Dosage"] = null;
			Session["Packing"] = null;
			Session["OperStandard"] = null;
			Session["Formula"] = null;
			Session["OperFlag"] = null;
			Session["OperFlag"] = "Add";
			DynamicImage1.Image = null;
			lblMaterialCostSum.Text = "";
			lblPackingCostSum.Text = "";
			lblCostSum.Text = "";
			this.Response.Redirect("./wfmFormulaQuery.aspx");
		}
		
		protected void btnAdd_Click(object sender, System.EventArgs e)
		{
			//添加
			try
			{
				string strProductCode = txtProductCode.Text;
				string strProductName = txtProductName.Text;
				if(strProductCode == "")
				{
					Popup("请输入产品编码");
					return;
				}
				if(strProductName == "")
				{
					Popup("请输入产品名称");
					return;
				}
				if(IsOut(strProductName,40))
				{
					Popup("产品名称超长，只能是20个汉字或40个英文数字！");
					return;
				}
				if(!JudgeIsCode(ddlProductType.SelectedValue,ddlProductClass.SelectedValue,strProductCode))
				{
					Popup("编码错误");
					return;
				}
				string strSql = "select * from vwProduct where cnvcProductCode='" + strProductCode + "'";
				DataTable dtProduct = Helper.Query(strSql);
				if(dtProduct.Rows.Count > 0)
				{
					Popup("相同编码成品已存在");
					return;
				}
				AMSApp.zhenghua.Entity.Formula formula = new AMSApp.zhenghua.Entity.Formula();
				formula.cnvcProductType = ddlProductType.SelectedValue;				
				formula.cnvcProductClass = ddlProductClass.SelectedValue;				
				formula.cnvcUnit = ddlUnit.SelectedValue;
				formula.cnvcProductCode = txtProductCode.Text;
				formula.cnvcProductName = txtProductName.Text;
				formula.IsUse = !this.chkIsUse.Checked;
				if(txtPortionCount.Text != "")
				{
					formula.cnnPortionCount = decimal.Parse(txtPortionCount.Text);
				}			
				else
				{
					formula.cnnPortionCount = 1;
				}
				formula.cnvcFeel = txtFeel.Text;
				formula.cnvcColor = txtColor.Text;
				formula.cnvcOrganise = txtOrganise.Text;
				formula.cnvcTaste = txtTaste.Text;
				if(lblMaterialCostSum.Text != "")
					formula.cnnMaterialCostSum = decimal.Parse(lblMaterialCostSum.Text);
				if(lblPackingCostSum.Text != "")
					formula.cnnPackingCostSum = decimal.Parse(lblPackingCostSum.Text);
				formula.cnnCostSum = formula.cnnMaterialCostSum + formula.cnnPackingCostSum;
				if(Session["Formula"] != null)
				{
					AMSApp.zhenghua.Entity.Formula formula1 = (AMSApp.zhenghua.Entity.Formula) Session["Formula"];
					formula.cnbProductImage = formula1.cnbProductImage;
				}
				if(formula.cnbProductImage == null)
				{
					Bitmap bmp = new Bitmap(Server.MapPath("~") + @"\zhenghua\images\no.jpg");
					//System.Drawing.Image image = System.Drawing.Image.FromFile(Server.MapPath("~")+@"\zhenghua\images\ProductDefault.jpg");
					MemoryStream Ms = new MemoryStream();
					bmp.Save(Ms, System.Drawing.Imaging.ImageFormat.Jpeg);
					byte[] img = new byte[Ms.Length]; 
					img = Ms.GetBuffer();
					formula.cnbProductImage = img;
				}
				DataTable dtDosage = (DataTable) Session["Dosage"];
				foreach(DataRow drDosage in dtDosage.Rows)
				{
					drDosage["cnvcProductCode"] = strProductCode;
				}
				DataTable dtPacking = (DataTable) Session["Packing"];
				foreach(DataRow drPacking in dtPacking.Rows)
				{
					drPacking["cnvcProductCode"] = strProductCode;
				}
				DataTable dtOperStandard = (DataTable) Session["OperStandard"];
				foreach(DataRow drOperStandard in dtOperStandard.Rows)
				{
					drOperStandard["cnvcProductCode"] = strProductCode;
				}
				OperLog operLog = new OperLog();
				operLog.cnvcOperID = oper.strLoginID;
				operLog.cnvcDeptID = oper.strDeptID;
				operLog.cnvcOperType = "添加配方";

				Business.MaterialFacade mf = new MaterialFacade();
				mf.AddFormula(formula, dtDosage, dtPacking, dtOperStandard,operLog);
				Popup("配方添加成功！");
				Session["Dosage"] = null;
				Session["Packing"] = null;
				Session["OperStandard"] = null;
				Session["Formula"] = null;
				Session["OperFlag"] = null;
				Session["OperFlag"] = "Add";
				DynamicImage1.Image = null;
				lblMaterialCostSum.Text = "";
				lblPackingCostSum.Text = "";
				lblCostSum.Text = "";
				//chkIsUse.Checked = false;
				if(ViewState["Product"] == null)
				{
					string strProduct = "select * from vwProduct";	
					DataTable dtAllProduct = Helper.Query(strProduct);
					ViewState["Product"] = dtAllProduct;
				}
				OperDispControl("Add", "");
				BindCode();
			}
			catch(Exception ex)
			{
				Popup(ex.Message);
			}
		}

		protected void btnFileImage_Click(object sender, System.EventArgs e)
		{
			//上传图片
			AMSApp.zhenghua.Entity.Formula formula = null;
			if(Session["Formula"] == null)
			{
				formula = new AMSApp.zhenghua.Entity.Formula();
				formula.cnvcProductType = ddlProductType.SelectedValue;				
				formula.cnvcProductClass = ddlProductClass.SelectedValue;				
				formula.cnvcUnit = ddlUnit.SelectedValue;
				formula.cnvcProductCode = txtProductCode.Text;
				formula.cnvcProductName = txtProductName.Text;
				if(txtPortionCount.Text != "")
				{
					formula.cnnPortionCount = decimal.Parse(txtPortionCount.Text);
				}				
				formula.cnvcFeel = txtFeel.Text;
				formula.cnvcColor = txtColor.Text;
				formula.cnvcOrganise = txtOrganise.Text;
				formula.cnvcTaste = txtTaste.Text;
			}
			else
			{
				formula = (AMSApp.zhenghua.Entity.Formula)Session["Formula"];
			}
			int   fLen   =   fileImage.PostedFile.ContentLength;  
			if(fLen > 100*1024)
			{
				this.Popup("图片大于100K，无法上传");
				return;
			}
			formula.cnbProductImage = new   byte[fLen];    
			fileImage.PostedFile.InputStream.Read(formula.cnbProductImage,0,fLen); 
			Session["Formula"] = formula;
			Bitmap bmp = new Bitmap(fileImage.PostedFile.InputStream);
			DynamicImage1.Image = bmp;
		}

		protected void btnModify_Click(object sender, System.EventArgs e)
		{
			try
			{
				string strProductCode = txtProductCode.Text;
				string strProductName = txtProductName.Text;
				if(strProductCode == "")
				{
					Popup("请输入产品编码");
					return;
				}
				if(strProductName == "")
				{
					Popup("请输入产品名称");
					return;
				}

				if(!JudgeIsCode(ddlProductType.SelectedValue,ddlProductClass.SelectedValue,strProductCode))
				{
					Popup(strProductCode+"编码不属于"+ddlProductClass.SelectedItem.Text);
					return;
				}
				
				AMSApp.zhenghua.Entity.Formula formula = new AMSApp.zhenghua.Entity.Formula();
				formula.cnvcProductType = ddlProductType.SelectedValue;				
				formula.cnvcProductClass = ddlProductClass.SelectedValue;				
				formula.cnvcUnit = ddlUnit.SelectedValue;
				formula.cnvcProductCode = txtProductCode.Text;
				formula.cnvcProductName = txtProductName.Text;
				if(txtPortionCount.Text != "")
				{
					formula.cnnPortionCount = decimal.Parse(txtPortionCount.Text);
				}				
				formula.cnvcFeel = txtFeel.Text;
				formula.cnvcColor = txtColor.Text;
				formula.cnvcOrganise = txtOrganise.Text;
				formula.cnvcTaste = txtTaste.Text;
				formula.IsUse = !this.chkIsUse.Checked;
				
				if(lblMaterialCostSum.Text != "")
					formula.cnnMaterialCostSum = decimal.Parse(lblMaterialCostSum.Text);
				if(lblPackingCostSum.Text != "")
					formula.cnnPackingCostSum = decimal.Parse(lblPackingCostSum.Text);
				formula.cnnCostSum = formula.cnnMaterialCostSum + formula.cnnPackingCostSum;
				if(Session["Formula"] != null)
				{
					AMSApp.zhenghua.Entity.Formula formula1 = (AMSApp.zhenghua.Entity.Formula) Session["Formula"];
					formula.cnbProductImage = formula1.cnbProductImage;
				}
				

				DataTable dtDosage = (DataTable) Session["Dosage"];
				foreach(DataRow drDosage in dtDosage.Rows)
				{
					drDosage["cnvcProductCode"] = strProductCode;
				}
				DataTable dtPacking = (DataTable) Session["Packing"];
				foreach(DataRow drPacking in dtPacking.Rows)
				{
					drPacking["cnvcProductCode"] = strProductCode;
				}
				DataTable dtOperStandard = (DataTable) Session["OperStandard"];
				foreach(DataRow drOperStandard in dtOperStandard.Rows)
				{
					drOperStandard["cnvcProductCode"] = strProductCode;
				}
				OperLog operLog = new OperLog();
				operLog.cnvcOperID = oper.strLoginID;
				operLog.cnvcDeptID = oper.strDeptID;
				operLog.cnvcOperType = "修改配方";

				Business.MaterialFacade mf = new MaterialFacade();
				mf.UpdateFormula(formula, dtDosage, dtPacking, dtOperStandard,operLog);
				Popup("配方修改成功！");
//				Session["Dosage"] = null;
//				Session["Packing"] = null;
//				Session["OperStandard"] = null;
//				Session["Formula"] = null;
//				Session["OperFlag"] = null;
//				Session["OperFlag"] = "Add";
//				DynamicImage1.Image = null;
//				lblMaterialCostSum.Text = "";
//				lblPackingCostSum.Text = "";
//				lblCostSum.Text = "";
//				if(ViewState["Product"] == null)
//				{
//					string strProduct = "select * from vwProduct";	
//					DataTable dtAllProduct = Helper.Query(strProduct);
//					ViewState["Product"] = dtAllProduct;
//				}
//				OperDispControl("Edit", strProductCode);
			}
			catch(Exception ex)
			{
				Popup(ex.Message);
			}
		}

		protected void ddlProductType_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			this.BindProductClass(ddlProductClass, "cnvcProductType='" + ddlProductType.SelectedValue + "' and cnvcProductClassCode<>'8001~8999'");
			if(ddlProductType.SelectedValue == "SEMIPRODUCT")
			{
				this.Label5.Visible = true;
				this.txtPortionCount.Visible = true;
			}
			else
			{
				this.Label5.Visible = false;
				this.txtPortionCount.Visible = false;
			}
			BindCode();
		}

		protected void ddlProductClass_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			
			BindCode();

		}

		private void BindCode()
		{
			string strClass = ddlProductClass.SelectedValue;
			string strType = ddlProductType.SelectedValue;
			string strOperFlag = Session["OperFlag"].ToString();
			if(strOperFlag=="Add")
			{
				if(strType == "FINALPRODUCT")
				{
			
					string[] strClasses = strClass.Split('~');
					string strCodeBegin = strClasses[0];
					string strCodeEnd = strClasses[1];

					string strFSql = " select top 1 cnvcProductCode from tbFormula "
						+" where cnvcProductCode>='"+strCodeBegin+"' and cnvcProductCode<'"+strCodeEnd
						+"' and cnvcProductType='FINALPRODUCT' "
						+" order by cnvcProductCode desc ";
					DataTable dt = Helper.Query(strFSql);
					if(dt.Rows.Count > 0)
					{
						string strCode = dt.Rows[0][0].ToString();
						if(this.JudgeIsNum(strCode))
						{
							int iCode = int.Parse(strCode);
							if(this.JudgeIsNum(strCodeEnd))
							{
								int iEnd = int.Parse(strCodeEnd);
								if(iCode+1<=iEnd)
								{
									this.txtProductCode.Text = Convert.ToString(iCode+1);
								}
								else
								{
									Popup("无合适的产品编码");
									this.txtProductCode.Text = "";
								}

							}
						}
					}
					else
					{
						Popup(this.ddlProductClass.SelectedItem.Text+"类别的产品未入库，从头开始编码");
						this.txtProductCode.Text = strCodeBegin;
					}
				}
				if(strType == "SEMIPRODUCT")
				{
					string[] strClasses = strClass.Split('~');
					string strLetter = strClass.Substring(0,1);
					string strCodeBegin = strClasses[0].Substring(1);
					string strCodeEnd = strClasses[1].Substring(1);

					string strFSql = " select top 1  cnvcProductCode from tbFormula "
						+" where substring(cnvcProductCode,2,"+strCodeBegin.Length.ToString()+")>="+strCodeBegin+" and substring(cnvcProductCode,2,"+strCodeEnd.Length.ToString()+")<"+strCodeEnd+" and "
						+ " cnvcProductType='SEMIPRODUCT' and substring(cnvcProductCode,1,1)='"+strLetter+"' "
						+ " order by cnvcProductCode desc ";

					DataTable dt = Helper.Query(strFSql);
					if(dt.Rows.Count > 0)
					{
						string strCode = dt.Rows[0][0].ToString();
						string strCode1 = strCode.Substring(1);
						string strLetter1 = strCode.Substring(0,1);
						if(strLetter1 == strLetter)
						{
							if(this.JudgeIsNum(strCode1))
							{
								int iCode = int.Parse(strCode1);
								if(this.JudgeIsNum(strCodeEnd))
								{
									int iEnd = int.Parse(strCodeEnd);
									if(iCode+1<=iEnd)
									{
										//if(strOperFlag=="Add")
										//if(iCode+1<100)
											this.txtProductCode.Text = strLetter+Convert.ToString(iCode+1).PadLeft(3,'0');
										//else
											//this.txtProductCode.Text = strLetter+Convert.ToString(iCode+1);
									}
									else
									{
										Popup("无合适的半成品编码");
										this.txtProductCode.Text = "";
									}
								}
							}
						}
					}
					else
					{
						Popup(this.ddlProductClass.SelectedItem.Text+"类别的半成品未入库，从头开始编码");
						this.txtProductCode.Text = strLetter+strCodeBegin;
					}
				}
			}
		}
	}
}
