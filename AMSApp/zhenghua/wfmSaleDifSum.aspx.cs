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
	/// wfmCostReport ��ժҪ˵����
	/// </summary>
	public partial class wfmSaleDifSum : wfmBase
	{


		//protected ucPageView UcPageView1;
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// �ڴ˴������û������Գ�ʼ��ҳ��
			if(Session["Login"]!=null)
			{
				if(!this.IsPostBack)
				{
					//				this.BindDept(ddlDept, "cnvcDeptType <>'Corp'");
					//				ListItem li = new ListItem("����", "%");
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
					ListItem li = new ListItem("����","%");
					ddlDept.Items.Add(li);

                    this.FillDropDownList("AllREGION", ddlRegion, "", "ȫ��");
					string str = "";
					if(strtype=="1")
					{
						this.Label3.Text = "���۲�����ܱ�";
						str="wfmSaleDifSum1_";
						lblOperName.Visible=false;
						txtOperName.Visible=false;
					}
					else
					{
						str="wfmSaleDifSum2_";
						this.Label3.Text="����Ա�տ����ͳ�Ʊ�";
					}

					Session["Get_Dept_Dif"]=null;
					Session["Get_Oper_Dif"]=null;

					#region ���ư�ť��ʾ
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
				if(this.Label3.Text == "���۲�����ܱ�")
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

		}
		#endregion

//		private DataTable query()
//		{
//			DataTable dtOut1 = Helper.QueryLongTrans("SaleDifSum '"+txtBeginDate.Text+"','"+txtEndDate.Text+"'");			
//			return dtOut1;
//		}
		protected void Button1_Click(object sender, System.EventArgs e)
		{
			//����
			if(this.Label3.Text == "���۲�����ܱ�")
			{
				if(Session["Get_Dept_Dif"]!=null)
				{
					DataTable dta = Session["Get_Dept_Dif"] as DataTable;	
					DataTable dt = dta.Copy();
					//this.DataTableConvert(dt, "cnvcDeptID","cnvcDeptName", "tbCommCode", "vcCommCode", "vcCommName", "");
					dt.TableName=this.txtBeginDate.Text+"��"+this.txtEndDate.Text+"���۲�����ܱ�";
					dt.Columns.Remove("cnvcType");
//					dt.Columns.Remove("cndBusinessDate");
					dt.Columns.Remove("cnvcDeptID");
					dt.Columns.Remove("cnvcOperName");
				
					dt.Columns["cnvcDeptName"].ColumnName="����";
					dt.Columns["cnnFact_Cash"].ColumnName="�ֽ�";
					dt.Columns["cnnFact_Pos"].ColumnName="POS����";
					dt.Columns["cnnFact_Replace"].ColumnName="����ȯ";
					dt.Columns["cnnFact_Check"].ColumnName="֧Ʊ";
					dt.Columns["cnnFact_Sum"].ColumnName="ʵ�ɺϼ�";
					dt.Columns["cnnPayable_Sum"].ColumnName="Ӧ�ɽ��";
					dt.Columns["cnnDif_Sum"].ColumnName="����ϼ�";
					dt.Columns["cnnDif_More"].ColumnName="���";
					dt.Columns["cnnDif_Add"].ColumnName="���";
					dt.Columns["cnnDif_Dif"].ColumnName="��/�̿�";
					dt.Columns["cnnPayable_Sale"].ColumnName="ϵͳ���۽��";
					dt.Columns["cnnPayable_Retail"].ColumnName="��������";
					dt.Columns["cnnPayable_Member"].ColumnName="��Ա����";
					dt.Columns["cnnPayable_Card"].ColumnName="��Ա����ֵ";
					dt.Columns["cnnPayable_Sale_Tmp"].ColumnName="ʵ�����۽��";
					dt.Columns["cndBusinessDate"].ColumnName="����";
				
					string str = this.ExportTable(dt);
					this.ExportToXls(this,"���۲�����ܱ�",str);
				}
			}
			else
			{
				if(Session["Get_Oper_Dif"]!=null)
				{
					DataTable dta = Session["Get_Oper_Dif"] as DataTable;	
					DataTable dt = dta.Copy();
					//this.DataTableConvert(dt, "cnvcDeptID","cnvcDeptName", "tbCommCode", "vcCommCode", "vcCommName", "");
					dt.TableName=this.txtBeginDate.Text+"��"+this.txtEndDate.Text+"����Ա�տ����ͳ�Ʊ�";
					dt.Columns.Remove("cnvcType");
//					dt.Columns.Remove("cndBusinessDate");
					dt.Columns.Remove("cnvcDeptID");
					//dt.Columns.Remove("cnvcDeptName");
					//dt.Columns.Remove("cnvcOperName");
				
					dt.Columns["cnvcDeptName"].ColumnName="����";
					dt.Columns["cnvcOperName"].ColumnName="����Ա";
					dt.Columns["cnnFact_Cash"].ColumnName="�ֽ�";
					dt.Columns["cnnFact_Pos"].ColumnName="POS����";
					dt.Columns["cnnFact_Replace"].ColumnName="����ȯ";
					dt.Columns["cnnFact_Check"].ColumnName="֧Ʊ";
					dt.Columns["cnnFact_Sum"].ColumnName="ʵ�ɺϼ�";
					dt.Columns["cnnPayable_Sum"].ColumnName="Ӧ�ɽ��";
					dt.Columns["cnnDif_Sum"].ColumnName="����ϼ�";
					dt.Columns["cnnDif_More"].ColumnName="���";
					dt.Columns["cnnDif_Add"].ColumnName="���";
					dt.Columns["cnnDif_Dif"].ColumnName="��/�̿�";
					dt.Columns["cnnPayable_Sale"].ColumnName="ϵͳ���۽��";
					dt.Columns["cnnPayable_Retail"].ColumnName="��������";
					dt.Columns["cnnPayable_Member"].ColumnName="��Ա����";
					dt.Columns["cnnPayable_Card"].ColumnName="��Ա����ֵ";
					dt.Columns["cnnPayable_Sale_Tmp"].ColumnName="ʵ�����۽��";
					dt.Columns["cndBusinessDate"].ColumnName="����";
				
					string str = this.ExportTable(dt);
					this.ExportToXls(this,"����Ա�տ����ͳ�Ʊ�",str);
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
			//��ѯ

//			string strsql = "select * from tbDifference where cndBusinessDate between '"+txtBeginDate.Text+"' and '"+txtBeginDate.Text+"'"
//				+" group by cnvcDeptID";
			string strsql= "select ";
			if(this.Label3.Text == "���۲�����ܱ�")
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
			if(this.Label3.Text=="���۲�����ܱ�" && ddlDept.SelectedItem.Text=="����" && IsFirstDayOfMonth(txtBeginDate.Text) && IsLastDayOfMonth(txtEndDate.Text))
			{
				strsql += "'' as cndBusinessDate,";
			}
			else
			{
				strsql += "CONVERT(varchar(12) , cndBusinessDate, 111 )  as cndBusinessDate ,";
			}
	strsql += "cnvcType"
+" from tbDifference ";
			if(this.Label3.Text=="���۲�����ܱ�")
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
			if(this.Label3.Text == "���۲�����ܱ�")
			{
				if(ddlDept.SelectedItem.Text=="����" && IsFirstDayOfMonth(txtBeginDate.Text) && IsLastDayOfMonth(txtEndDate.Text))
				{
					strsql+=" and cnvcType='����' and cnvcDeptID not in ('MDXSB','MDCW1','FYZX1','CEN00') group by cnvcDeptID,cnvcType ";
				}
				else
				{
					strsql+=" and cnvcType='����' and cnvcDeptID not in ('MDXSB','MDCW1','FYZX1','CEN00') group by cnvcDeptID,cndBusinessDate,cnvcType ";
				}
			}
			else
			{
				strsql+=" and cnvcType='����Ա' and cnvcDeptID not in ('MDXSB','MDCW1','FYZX1','CEN00')  group by cnvcDeptID,cnvcOperName,cndBusinessDate,cnvcType ";
			}

			DataTable dt = Helper.Query(strsql);
			//if(this.Label3.Text == "���۲�����ܱ�")
			
			this.DataTableConvert(dt, "cnvcDeptID","cnvcDeptName", "tbCommCode", "vcCommCode", "vcCommName", "");

			
			dt.Rows.Add(GetSum(dt,ddlDept.SelectedValue));
			this.DataGrid1.CurrentPageIndex=0;
			//this.DataGrid1.DataSource = dt;
			//this.DataGrid1.DataBind();
			if(this.Label3.Text == "���۲�����ܱ�")
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
//			if(strFilter=="����")strFilter="";
//			else
//			strFilter="cnvcDeptID='"+strFilter+"'";
			strFilter="";
			DataRow dr = dt.NewRow();
			dr["cnvcDeptID"]="";
			dr["cnvcDeptName"] = "�ϼ�";
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
			//�༭����
			if(this.Label3.Text == "���۲�����ܱ�")
			{
				string strsql = "select cnvcDeptID,'' as cnvcDeptName,'' as cnvcOperName,cnnFact_Cash,cnnFact_Pos,cnnFact_Replace,cnnFact_Check,"
					+"cnnFact_Sum,cnnPayable_Sum,cnnDif_Sum,cnnDIf_More,cnnDif_Add,cnnDif_Dif,cnnPayable_Sale,cnnPayable_Retail,cnnPayable_Member,cnnPayable_Card,cnnPayable_Sale-cnnDIf_More as cnnPayable_Sale_Tmp ,CONVERT(varchar(12) , cndBusinessDate, 111 )  as cndBusinessDate,cnvcType from tbDifference where cndBusinessDate between '"
					+txtBeginDate.Text+"' and '"+txtEndDate.Text+"' and cnvcType='����' and cnvcDeptID like '"+ddlDept.SelectedValue+"'";
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
					+txtBeginDate.Text+"' and '"+txtEndDate.Text+"' and cnvcType='����Ա' and cnvcDeptID like '"+ddlDept.SelectedValue+"'";
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
			if(this.Label3.Text == "���۲�����ܱ�")
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
//		+"<td align='Center' rowspan='2'>����</td> "
//		+"<td colspan='5' align='center'>ʵ�ɽ��</td> "		
//		+"<td rowspan='2'>Ӧ�ɽ��</td> "
//		+"<td colspan='4' align='center'>����</td> "
//		+"<td rowspan='2'>���۽��</td> "
//		+"<td rowspan='2'>��������</td> "
//		+"<td rowspan='2'>��Ա����</td> "
//		+"<td rowspan='2'>��Ա����ֵ</td> "
//	+"</tr> "
//	+"<tr> "
//		+"<td>�ֽ�</td> "
//		+"<td>POS����</td> "
//		+"<td>����ȯ</td> "
//		+"<td>֧Ʊ</td> "
//		+"<td>�ϼ�</td> "
//		+"<td>�ϼ�</td> "
//		+"<td>���</td> "
//		+"<td>���</td> "
//		+"<td>��/�̿�</td> "
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
			//�޸�
			try
			{
				//ʵ��
				string strFact_Cash = ((TextBox)e.Item.Cells[3].Controls[1]).Text;
				string strFact_Pos = ((TextBox)e.Item.Cells[4].Controls[1]).Text;
				string strFact_Replace = ((TextBox)e.Item.Cells[5].Controls[1]).Text;
				string strFact_Check = ((TextBox)e.Item.Cells[6].Controls[1]).Text;

				if(!this.JudgeIsNum(strFact_Cash)) throw new Exception("���ֽ�����������");
				if(!this.JudgeIsNum(strFact_Pos)) throw new Exception("��POS���롿����������");
				if(!this.JudgeIsNum(strFact_Replace)) throw new Exception("������ȯ������������");
				if(!this.JudgeIsNum(strFact_Check)) throw new Exception("��֧Ʊ������������");

				string strDif_More = ((TextBox)e.Item.Cells[10].Controls[1]).Text;
				string strDif_Add = ((TextBox)e.Item.Cells[11].Controls[1]).Text;
				string strDif_Dif = "0";

				if(!this.JudgeIsNum(strDif_More)) throw new Exception("���������������");
				if(!this.JudgeIsNum(strDif_Add)) throw new Exception("����䡿����������");


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
				if(strDeptID=="") throw new Exception("���ܱ༭��");
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
				if(dPayable_Sum != dFact_Sum + dDif_Sum) throw new Exception("��ʵ�ɺϼơ�+������ϼơ� ������ ��Ӧ�ɽ�");
				this.Popup("�޸ĳɹ���");
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
			//ˢ������
			try
			{
				if(this.Label3.Text == "���۲�����ܱ�")
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
				this.Popup("ˢ�����ݳɹ���");
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
			myLable.Text = "�� " + (DataGrid1.CurrentPageIndex+1) +" ҳ/�� " + DataGrid1.PageCount+" ҳ����"+iRecordCount+"����¼";
		}

        protected void ddlRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.BindRegion(this.ddlRegion.SelectedValue, this.ddlDept);
        }
	}
}
