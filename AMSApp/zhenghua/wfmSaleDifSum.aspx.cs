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
using BusiComm;
using CommCenter;
namespace AMSApp.zhenghua.Produce
{
	/// <summary>
	/// wfmCostReport 的摘要说明。
	/// </summary>
	public partial class wfmSaleDifSum : wfmBase
	{


		//protected ucPageView UcPageView1;
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
			if(Session["Login"]!=null)
			{
				if(!this.IsPostBack)
				{
					//				this.BindDept(ddlDept, "cnvcDeptType <>'Corp'");
					//				ListItem li = new ListItem("所有", "%");
					//				this.ddlDept.Items.Add(li);
					//				this.SetDDL(this.ddlDept,this.oper.strDeptID);
					//				if(this.oper.strDeptID !="CEN00")
					//				{				
					//					this.ddlDept.Enabled = false;		
					//				}
					CMSMStruct.LoginStruct ls1=(CMSMStruct.LoginStruct)Session["Login"];

					this.txtBeginDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
					this.txtEndDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
					string strtype = this.Request["type"].ToString();
					this.BindCommCode(ddlDept,"vcCommSign='md' and vcCommCode not in ('MDXSB','MDCW1','FYZX1','CEN00')");
					ListItem li = new ListItem("所有","%");
					ddlDept.Items.Add(li);

                    this.FillDropDownList("AllREGION", ddlRegion, "", "全部");
					string str = "";
					if(strtype=="1")
					{
						this.Label3.Text = "销售差异汇总表";
						str="wfmSaleDifSum1_";
						lblOperName.Visible=false;
						txtOperName.Visible=false;
					}
					else
					{
						str="wfmSaleDifSum2_";
						this.Label3.Text="收银员收款差异统计表";
					}

					Session["Get_Dept_Dif"]=null;
					Session["Get_Oper_Dif"]=null;

					#region 控制按钮显示
					this.Button3.Visible = false;
					Hashtable htOperFunc=(Hashtable)Application["OperFunc"];
					ArrayList almenu=(ArrayList)htOperFunc[ls1.strLoginID];
					

					if(almenu!=null)
					{
						for(int i=0;i<almenu.Count;i++)
						{
							CMSMStruct.MenuStruct ms1=(CMSMStruct.MenuStruct)almenu[i];
							System.Web.UI.WebControls.Button btnCurrent = this.FindControl(ms1.strFuncAddress.Replace(str,String.Empty)) as System.Web.UI.WebControls.Button;
							if(btnCurrent!=null)
							{
								btnCurrent.Visible = true;
							}				
						}
					}
					#endregion
				}

				this.FootBar.Visible = false;
				if(this.Label3.Text == "销售差异汇总表")
				{
					if(Session["Get_Dept_Dif"]!=null)
					{
						if(((DataTable)Session["Get_Dept_Dif"]).Rows.Count>0)
						{
							this.FootBar.Visible = true;
						}

					}
				}
				else
				{
					if(Session["Get_Oper_Dif"]!=null)
					{
						if(((DataTable)Session["Get_Oper_Dif"]).Rows.Count>0)
						{
							this.FootBar.Visible = true;
						}

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
			this.DataGrid1.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DataGrid1_PageIndexChanged);
			this.DataGrid1.CancelCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGrid1_CancelCommand);
			this.DataGrid1.EditCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGrid1_EditCommand);
			this.DataGrid1.UpdateCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGrid1_UpdateCommand);

		}
		#endregion

//		private DataTable query()
//		{
//			DataTable dtOut1 = Helper.QueryLongTrans("SaleDifSum '"+txtBeginDate.Text+"','"+txtEndDate.Text+"'");			
//			return dtOut1;
//		}
		protected void Button1_Click(object sender, System.EventArgs e)
		{
			//导出
			if(this.Label3.Text == "销售差异汇总表")
			{
				if(Session["Get_Dept_Dif"]!=null)
				{
					DataTable dta = Session["Get_Dept_Dif"] as DataTable;	
					DataTable dt = dta.Copy();
					//this.DataTableConvert(dt, "cnvcDeptID","cnvcDeptName", "tbCommCode", "vcCommCode", "vcCommName", "");
					dt.TableName=this.txtBeginDate.Text+"至"+this.txtEndDate.Text+"销售差异汇总表";
					dt.Columns.Remove("cnvcType");
//					dt.Columns.Remove("cndBusinessDate");
					dt.Columns.Remove("cnvcDeptID");
					dt.Columns.Remove("cnvcOperName");
				
					dt.Columns["cnvcDeptName"].ColumnName="部门";
					dt.Columns["cnnFact_Cash"].ColumnName="现金";
					dt.Columns["cnnFact_Pos"].ColumnName="POS收入";
					dt.Columns["cnnFact_Replace"].ColumnName="代金券";
					dt.Columns["cnnFact_Check"].ColumnName="支票";
					dt.Columns["cnnFact_Sum"].ColumnName="实缴合计";
					dt.Columns["cnnPayable_Sum"].ColumnName="应缴金额";
					dt.Columns["cnnDif_Sum"].ColumnName="差异合计";
					dt.Columns["cnnDif_More"].ColumnName="多打";
					dt.Columns["cnnDif_Add"].ColumnName="多充";
					dt.Columns["cnnDif_Dif"].ColumnName="长/短款";
					dt.Columns["cnnPayable_Sale"].ColumnName="系统销售金额";
					dt.Columns["cnnPayable_Retail"].ColumnName="零售消费";
					dt.Columns["cnnPayable_Member"].ColumnName="会员消费";
					dt.Columns["cnnPayable_Card"].ColumnName="会员卡充值";
					dt.Columns["cnnPayable_Sale_Tmp"].ColumnName="实际销售金额";
					dt.Columns["cndBusinessDate"].ColumnName="日期";
				
					string str = this.ExportTable(dt);
					this.ExportToXls(this,"销售差异汇总表",str);
				}
			}
			else
			{
				if(Session["Get_Oper_Dif"]!=null)
				{
					DataTable dta = Session["Get_Oper_Dif"] as DataTable;	
					DataTable dt = dta.Copy();
					//this.DataTableConvert(dt, "cnvcDeptID","cnvcDeptName", "tbCommCode", "vcCommCode", "vcCommName", "");
					dt.TableName=this.txtBeginDate.Text+"至"+this.txtEndDate.Text+"收银员收款差异统计表";
					dt.Columns.Remove("cnvcType");
//					dt.Columns.Remove("cndBusinessDate");
					dt.Columns.Remove("cnvcDeptID");
					//dt.Columns.Remove("cnvcDeptName");
					//dt.Columns.Remove("cnvcOperName");
				
					dt.Columns["cnvcDeptName"].ColumnName="部门";
					dt.Columns["cnvcOperName"].ColumnName="操作员";
					dt.Columns["cnnFact_Cash"].ColumnName="现金";
					dt.Columns["cnnFact_Pos"].ColumnName="POS收入";
					dt.Columns["cnnFact_Replace"].ColumnName="代金券";
					dt.Columns["cnnFact_Check"].ColumnName="支票";
					dt.Columns["cnnFact_Sum"].ColumnName="实缴合计";
					dt.Columns["cnnPayable_Sum"].ColumnName="应缴金额";
					dt.Columns["cnnDif_Sum"].ColumnName="差异合计";
					dt.Columns["cnnDif_More"].ColumnName="多打";
					dt.Columns["cnnDif_Add"].ColumnName="多充";
					dt.Columns["cnnDif_Dif"].ColumnName="长/短款";
					dt.Columns["cnnPayable_Sale"].ColumnName="系统销售金额";
					dt.Columns["cnnPayable_Retail"].ColumnName="零售消费";
					dt.Columns["cnnPayable_Member"].ColumnName="会员消费";
					dt.Columns["cnnPayable_Card"].ColumnName="会员卡充值";
					dt.Columns["cnnPayable_Sale_Tmp"].ColumnName="实际销售金额";
					dt.Columns["cndBusinessDate"].ColumnName="日期";
				
					string str = this.ExportTable(dt);
					this.ExportToXls(this,"收银员收款差异统计表",str);
				}
			}
		}

		private bool IsFirstDayOfMonth(string strdatetime)
		{
			DateTime datetime = Convert.ToDateTime(strdatetime);
			return datetime == datetime.AddDays(1 - datetime.Day);
		}
		private bool IsLastDayOfMonth(string strdatetime)
		{
			DateTime datetime = Convert.ToDateTime(strdatetime);
			return datetime == datetime.AddDays(1 - datetime.Day).AddMonths(1).AddDays(-1);
		}
		protected void Button2_Click(object sender, System.EventArgs e)
		{
			//查询

//			string strsql = "select * from tbDifference where cndBusinessDate between '"+txtBeginDate.Text+"' and '"+txtBeginDate.Text+"'"
//				+" group by cnvcDeptID";
			string strsql= "select ";
			if(this.Label3.Text == "销售差异汇总表")
			{
				strsql+= "cnvcDeptID,'' as cnvcDeptName,'' as cnvcOperName";
			}
			else
			{
				strsql+= "cnvcDeptID,'' as cnvcDeptName,cnvcOperName";
			}
strsql +=",sum(cnnFact_Cash) as cnnFact_Cash,sum(cnnFact_Pos) as cnnFact_Pos ,sum(cnnFact_Replace) as cnnFact_Replace"
+" ,sum(cnnFact_Check) as cnnFact_Check,sum(cnnFact_Sum) as cnnFact_Sum,"
+"sum(cnnPayable_Sum) as cnnPayable_Sum ,"
+"sum(cnnDif_Sum) as cnnDif_Sum,sum(cnnDif_More) as cnnDif_More,sum(cnnDif_Add) as cnnDif_Add,sum(cnnDif_Dif) as cnnDif_Dif,"
+"sum(cnnPayable_Sale) as cnnPayable_Sale,"
+"sum(cnnPayable_Retail) as cnnPayable_Retail,"
+"sum(cnnPayable_Member) as cnnPayable_Member,sum(cnnPayable_Card) as cnnPayable_Card ,sum(cnnPayable_Sale-cnnDIf_More) as cnnPayable_Sale_Tmp,";
			if(this.Label3.Text=="销售差异汇总表" && ddlDept.SelectedItem.Text=="所有" && IsFirstDayOfMonth(txtBeginDate.Text) && IsLastDayOfMonth(txtEndDate.Text))
			{
				strsql += "'' as cndBusinessDate,";
			}
			else
			{
				strsql += "CONVERT(varchar(12) , cndBusinessDate, 111 )  as cndBusinessDate ,";
			}
	strsql += "cnvcType"
+" from tbDifference ";
			if(this.Label3.Text=="销售差异汇总表")
			{
				strsql +=" where cndBusinessDate between '"+txtBeginDate.Text+"' and '"+txtEndDate.Text+"' and cnvcDeptID like '"+ddlDept.SelectedValue+"'";
			}
			else
			{
				strsql +=" where cndBusinessDate between '"+txtBeginDate.Text+"' and '"+txtEndDate.Text+"' and cnvcDeptID like '"+ddlDept.SelectedValue+"'";
				if(txtOperName.Text.Trim()!="")
				{
					strsql += " and cnvcOperName like '%"+txtOperName.Text+"%' ";
				}
			}
			if(this.Label3.Text == "销售差异汇总表")
			{
				if(ddlDept.SelectedItem.Text=="所有" && IsFirstDayOfMonth(txtBeginDate.Text) && IsLastDayOfMonth(txtEndDate.Text))
				{
					strsql+=" and cnvcType='部门' and cnvcDeptID not in ('MDXSB','MDCW1','FYZX1','CEN00') group by cnvcDeptID,cnvcType ";
				}
				else
				{
					strsql+=" and cnvcType='部门' and cnvcDeptID not in ('MDXSB','MDCW1','FYZX1','CEN00') group by cnvcDeptID,cndBusinessDate,cnvcType ";
				}
			}
			else
			{
				strsql+=" and cnvcType='操作员' and cnvcDeptID not in ('MDXSB','MDCW1','FYZX1','CEN00')  group by cnvcDeptID,cnvcOperName,cndBusinessDate,cnvcType ";
			}

			DataTable dt = Helper.Query(strsql);
			//if(this.Label3.Text == "销售差异汇总表")
			
			this.DataTableConvert(dt, "cnvcDeptID","cnvcDeptName", "tbCommCode", "vcCommCode", "vcCommName", "");

			
			dt.Rows.Add(GetSum(dt,ddlDept.SelectedValue));
			this.DataGrid1.CurrentPageIndex=0;
			//this.DataGrid1.DataSource = dt;
			//this.DataGrid1.DataBind();
			if(this.Label3.Text == "销售差异汇总表")
			{
				Session["Get_Dept_Dif"] = dt;
				BindDataGrid();
				this.DataGrid1.Columns[1].Visible = true;
				this.DataGrid1.Columns[2].Visible = false;
			}
			else
			{
				Session["Get_Oper_Dif"] = dt;
				BindDataGrid();
				this.DataGrid1.Columns[1].Visible = true;
				this.DataGrid1.Columns[2].Visible = true;
			}
			this.DataGrid1.Columns[17].Visible = true;
			this.DataGrid1.Columns[18].Visible = true;
			this.DataGrid1.Columns[19].Visible = false;
			this.DataGrid1.Columns[20].Visible = false;
		}
		private DataRow GetSum(DataTable dt,string strFilter)
		{
//			if(strFilter=="所有")strFilter="";
//			else
//			strFilter="cnvcDeptID='"+strFilter+"'";
			strFilter="";
			DataRow dr = dt.NewRow();
			dr["cnvcDeptID"]="";
			dr["cnvcDeptName"] = "合计";
			dr["cnnFact_Cash"] = dt.Compute("sum(cnnFact_Cash)",strFilter);
			dr["cnnFact_Pos"] = dt.Compute("sum(cnnFact_Pos)",strFilter);
			dr["cnnFact_Replace"] = dt.Compute("sum(cnnFact_Replace)",strFilter);
			dr["cnnFact_Check"] = dt.Compute("sum(cnnFact_Check)",strFilter);
			dr["cnnFact_Sum"] = dt.Compute("sum(cnnFact_Sum)",strFilter);

			dr["cnnPayable_Sum"] = dt.Compute("sum(cnnPayable_Sum)",strFilter);
			dr["cnnDif_Sum"] = dt.Compute("sum(cnnDif_Sum)",strFilter);
			dr["cnnDif_More"] = dt.Compute("sum(cnnDif_More)",strFilter);
			dr["cnnDif_Add"] = dt.Compute("sum(cnnDif_Add)",strFilter);
			dr["cnnDif_Dif"] = dt.Compute("sum(cnnDif_Dif)",strFilter);

			dr["cnnPayable_Sale"] = dt.Compute("sum(cnnPayable_Sale)",strFilter);
			dr["cnnPayable_Retail"] = dt.Compute("sum(cnnPayable_Retail)",strFilter);
			dr["cnnPayable_Member"] = dt.Compute("sum(cnnPayable_Member)",strFilter);
			dr["cnnPayable_Card"] = dt.Compute("sum(cnnPayable_Card)",strFilter);
			dr["cnnPayable_Sale_Tmp"] = dt.Compute("sum(cnnPayable_Sale_Tmp)",strFilter);
			return dr;
		}
		protected void Button3_Click(object sender, System.EventArgs e)
		{
			//编辑数据
			if(this.Label3.Text == "销售差异汇总表")
			{
				string strsql = "select cnvcDeptID,'' as cnvcDeptName,'' as cnvcOperName,cnnFact_Cash,cnnFact_Pos,cnnFact_Replace,cnnFact_Check,"
					+"cnnFact_Sum,cnnPayable_Sum,cnnDif_Sum,cnnDIf_More,cnnDif_Add,cnnDif_Dif,cnnPayable_Sale,cnnPayable_Retail,cnnPayable_Member,cnnPayable_Card,cnnPayable_Sale-cnnDIf_More as cnnPayable_Sale_Tmp ,CONVERT(varchar(12) , cndBusinessDate, 111 )  as cndBusinessDate,cnvcType from tbDifference where cndBusinessDate between '"
					+txtBeginDate.Text+"' and '"+txtEndDate.Text+"' and cnvcType='部门' and cnvcDeptID like '"+ddlDept.SelectedValue+"'";
				DataTable dt = Helper.Query(strsql);
								
				this.DataTableConvert(dt, "cnvcDeptID","cnvcDeptName", "tbCommCode", "vcCommCode", "vcCommName", "");

			
				dt.Rows.Add(GetSum(dt,ddlDept.SelectedValue));
				this.DataGrid1.CurrentPageIndex=0;
				//this.DataGrid1.DataSource = dt;
				//this.DataGrid1.DataBind();
			
				Session["Get_Dept_Dif"] = dt;
				BindDataGrid();

				this.DataGrid1.Columns[1].Visible = true;
				this.DataGrid1.Columns[2].Visible = false;
				this.DataGrid1.Columns[17].Visible = true;
				this.DataGrid1.Columns[18].Visible = true;
				this.DataGrid1.Columns[19].Visible = false;
				this.DataGrid1.Columns[20].Visible = true;
			}
			else
			{
				string strsql = "select cnvcDeptID,'' as cnvcDeptName,cnvcOperName,cnnFact_Cash,cnnFact_Pos,cnnFact_Replace,cnnFact_Check,"
					+"cnnFact_Sum,cnnPayable_Sum,cnnDif_Sum,cnnDIf_More,cnnDif_Add,cnnDif_Dif,cnnPayable_Sale,cnnPayable_Retail,cnnPayable_Member,cnnPayable_Card,cnnPayable_Sale-cnnDIf_More as cnnPayable_Sale_Tmp,CONVERT(varchar(12) , cndBusinessDate, 111 )  as cndBusinessDate,cnvcType "
					+" from tbDifference  where cndBusinessDate between '"
					+txtBeginDate.Text+"' and '"+txtEndDate.Text+"' and cnvcType='操作员' and cnvcDeptID like '"+ddlDept.SelectedValue+"'";
				DataTable dt = Helper.Query(strsql);
				
				this.DataTableConvert(dt, "cnvcDeptID","cnvcDeptName", "tbCommCode", "vcCommCode", "vcCommName", "");

			
				dt.Rows.Add(GetSum(dt,ddlDept.SelectedValue));
				this.DataGrid1.CurrentPageIndex=0;
				//this.DataGrid1.DataSource = dt;
				//this.DataGrid1.DataBind();
			
				Session["Get_Oper_Dif"] = dt;

				BindDataGrid();

				this.DataGrid1.Columns[1].Visible = true;
				this.DataGrid1.Columns[2].Visible = true;
				this.DataGrid1.Columns[17].Visible = true;
				this.DataGrid1.Columns[18].Visible = true;
				this.DataGrid1.Columns[19].Visible = false;
				this.DataGrid1.Columns[20].Visible = true;
			}
	}

		private void BindDataGrid()
		{
			int iRecordCount = 0;
			if(this.Label3.Text == "销售差异汇总表")
			{
				if(Session["Get_Dept_Dif"] != null)
				{
					DataTable dt = (DataTable)Session["Get_Dept_Dif"];
					iRecordCount = dt.Rows.Count;
					this.DataGrid1.DataSource = dt;
					this.DataGrid1.DataBind();
				}
			}
			else
			{
				if(Session["Get_Oper_Dif"] != null)
				{
					DataTable dt = (DataTable)Session["Get_Oper_Dif"];
					iRecordCount = dt.Rows.Count;
					this.DataGrid1.DataSource = dt;
					this.DataGrid1.DataBind();
				}
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
		private void DataGrid1_ItemCreated(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
//			if(e.Item.ItemType==ListItemType.Header)
//			{
//				TableCellCollection tcl=e.Item.Cells;
//				tcl.Clear();
//				tcl.Add(new TableHeaderCell());
//				tcl[0].ColumnSpan =15;                
//				tcl[0].Text="tr> "
//		+"<td align='Center' rowspan='2'>部门</td> "
//		+"<td colspan='5' align='center'>实缴金额</td> "		
//		+"<td rowspan='2'>应缴金额</td> "
//		+"<td colspan='4' align='center'>差异</td> "
//		+"<td rowspan='2'>销售金额</td> "
//		+"<td rowspan='2'>零售消费</td> "
//		+"<td rowspan='2'>会员消费</td> "
//		+"<td rowspan='2'>会员卡充值</td> "
//	+"</tr> "
//	+"<tr> "
//		+"<td>现金</td> "
//		+"<td>POS收入</td> "
//		+"<td>代金券</td> "
//		+"<td>支票</td> "
//		+"<td>合计</td> "
//		+"<td>合计</td> "
//		+"<td>多打</td> "
//		+"<td>多充</td> "
//		+"<td>长/短款</td> "
//	+"</tr>";
//
//			}
		}

		private void DataGrid1_EditCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			this.DataGrid1.EditItemIndex = e.Item.ItemIndex;
			BindDataGrid();
		}

		private void DataGrid1_CancelCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			this.DataGrid1.EditItemIndex = -1;
			BindDataGrid();
		}

		private void DataGrid1_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			//this.DataGrid1.CurrentPageIndex = t
		}

		private void DataGrid1_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			this.DataGrid1.CurrentPageIndex = e.NewPageIndex;
			BindDataGrid();
		}

		private void DataGrid1_UpdateCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			//修改
			try
			{
				//实缴
				string strFact_Cash = ((TextBox)e.Item.Cells[3].Controls[1]).Text;
				string strFact_Pos = ((TextBox)e.Item.Cells[4].Controls[1]).Text;
				string strFact_Replace = ((TextBox)e.Item.Cells[5].Controls[1]).Text;
				string strFact_Check = ((TextBox)e.Item.Cells[6].Controls[1]).Text;

				if(!this.JudgeIsNum(strFact_Cash)) throw new Exception("【现金】请输入数字");
				if(!this.JudgeIsNum(strFact_Pos)) throw new Exception("【POS收入】请输入数字");
				if(!this.JudgeIsNum(strFact_Replace)) throw new Exception("【代金券】请输入数字");
				if(!this.JudgeIsNum(strFact_Check)) throw new Exception("【支票】请输入数字");

				string strDif_More = ((TextBox)e.Item.Cells[10].Controls[1]).Text;
				string strDif_Add = ((TextBox)e.Item.Cells[11].Controls[1]).Text;
				string strDif_Dif = "0";

				if(!this.JudgeIsNum(strDif_More)) throw new Exception("【多打】请输入数字");
				if(!this.JudgeIsNum(strDif_Add)) throw new Exception("【多充】请输入数字");


				decimal dFact_Cash = Convert.ToDecimal(strFact_Cash);
				decimal dFact_Pos = Convert.ToDecimal(strFact_Pos);
				decimal dFact_Replace = Convert.ToDecimal(strFact_Replace);
				decimal dFact_Check = Convert.ToDecimal(strFact_Check);

				decimal dFact_Sum = dFact_Cash+dFact_Pos+dFact_Replace+dFact_Check;
				

				decimal dDif_More = Convert.ToDecimal(strDif_More);
				decimal dDif_Add = Convert.ToDecimal(strDif_Add);
				decimal dDif_Dif = Convert.ToDecimal(strDif_Dif);
				
				string strPayable_Sum = e.Item.Cells[8].Text;
				decimal dPayable_Sum = Convert.ToDecimal(strPayable_Sum);
				decimal dDif_Sum = dPayable_Sum - dFact_Sum;
				dDif_Dif=dFact_Sum-(dPayable_Sum-dDif_More-dDif_Add);
				strDif_Dif = dDif_Dif.ToString();

				string strDeptID = e.Item.Cells[0].Text;
				string strOperName=e.Item.Cells[2].Text;
				string strType = e.Item.Cells[19].Text;
				string strBusinessDate = e.Item.Cells[18].Text;

				
				if(strOperName=="&nbsp;")//"&nbsp;"
				{
					strOperName="";
				}
				if(strDeptID=="&nbsp;")
				{
					strDeptID="";
				}
				if(strDeptID=="") throw new Exception("不能编辑！");
				string strsql = "update tbDifference set cnnFact_Cash="+strFact_Cash
					+",cnnFact_Pos="+strFact_Pos
					+",cnnFact_Replace="+strFact_Replace
					+",cnnFact_Check="+strFact_Check
					+",cnnFact_Sum="+dFact_Sum.ToString()
					+",cnnDif_Sum="+dDif_Sum.ToString()
					+",cnnDif_More="+strDif_More
					+",cnnDif_Add="+strDif_Add
					+",cnnDif_Dif="+strDif_Dif
					+" where cnvcDeptID='"+strDeptID+"'"
					+" and cnvcType='"+strType+"'"
					+" and cndBusinessDate='"+strBusinessDate+"'"
					+" and cnvcOperName='"+strOperName+"'";

				Helper.ExcuteNoQuery(strsql);

				e.Item.Cells[9].Text = dDif_Sum.ToString();
				e.Item.Cells[7].Text = dFact_Sum.ToString();
				e.Item.Cells[12].Text = dDif_Dif.ToString();
				e.Item.Cells[17].Text = (dPayable_Sum-dDif_More).ToString();
				if(dPayable_Sum != dFact_Sum + dDif_Sum) throw new Exception("【实缴合计】+【差异合计】 不等于 【应缴金额】");
				this.Popup("修改成功！");
				this.DataGrid1.EditItemIndex = -1;
				Button3_Click(null,null);

			}
			catch(Exception ex)
			{
				this.Popup(ex.Message);
			}
		}

		protected void Button4_Click(object sender, System.EventArgs e)
		{
			//刷新数据
			try
			{
				if(this.Label3.Text == "销售差异汇总表")
				{
					DataTable dt = Helper.QueryLongTrans("Get_Dept_Dif '"+txtBeginDate.Text+"','"+txtEndDate.Text+"','"+ddlDept.SelectedValue+"'");	

					if(dt.Rows.Count>0)
					{
						AMSApp.zhenghua.Entity.OperLog ol = new AMSApp.zhenghua.Entity.OperLog();
						MaterialFacade mf = new MaterialFacade();
						mf.BatchDifference(dt,ol);
					}
				}
				else
				{
					DataTable dt = Helper.QueryLongTrans("Get_Oper_Dif '"+txtBeginDate.Text+"','"+txtEndDate.Text+"','"+ddlDept.SelectedValue+"'");	

					if(dt.Rows.Count>0)
					{
						AMSApp.zhenghua.Entity.OperLog ol = new AMSApp.zhenghua.Entity.OperLog();
						MaterialFacade mf = new MaterialFacade();
						mf.BatchDifference(dt,ol);
					}
				}
				this.Popup("刷新数据成功！");
				Button3_Click(null,null);
			}
			catch(Exception ex)
			{
				this.Popup(ex.Message);
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
			myLable.Text = "第 " + (DataGrid1.CurrentPageIndex+1) +" 页/共 " + DataGrid1.PageCount+" 页，共"+iRecordCount+"条记录";
		}

        protected void ddlRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.BindRegion(this.ddlRegion.SelectedValue, this.ddlDept);
        }
	}
}
