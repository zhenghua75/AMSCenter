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

namespace AMSApp.zhenghua.Produce
{
	/// <summary>
	/// wfmDividModify 的摘要说明。
	/// </summary>
	public partial class wfmDividModify : wfmBase
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
			if(!this.IsPostBack)
			{
				this.BindDept(ddlProduceDept, "cnvcDeptType <>'Corp'");
				//this.BindDept(ddlOrderDept, "cnvcDeptType <>'Corp'");
				//ListItem li = new ListItem("所有", "%");
				//ddlOrderDept.Items.Insert(0, li);
				//this.BindNameCode(ddlGroup, "cnvcType='GROUP'");
				if(Request["ProduceSerialNo"] == null)
				{
					Popup("无效链接");
					return;
				}
				//string strOperType = Request["OperType"].ToString();
				//ViewState["OperType"] = strOperType;
				string strProduceSerialNo = Request["ProduceSerialNo"].ToString();				
				BindProduceLog(strProduceSerialNo);
				//QueryProduceDetail();
			}
		}

		private void BindProduceLog(string strProduceSerialNo)
		{
			string strSql = "select * from tbProduceLog where cnnProduceSerialNo=" + strProduceSerialNo;
			DataTable dtProduceLog = Helper.Query(strSql);
			if(dtProduceLog.Rows.Count > 0)
			{
				ProduceLog produceLog = new ProduceLog(dtProduceLog);
				this.ddlProduceDept.Items.FindByValue(produceLog.cnvcProduceDeptID).Selected = true;
				this.txtProduceSerialNo.Text = produceLog.cnnProduceSerialNo.ToString();
				this.txtProduceDate.Text = produceLog.cndProduceDate.ToString("yyyy-MM-dd");
				
				this.txtProduceDate.Enabled = false;
				this.txtProduceSerialNo.Enabled = false;
				this.ddlProduceDept.Enabled = false;
				txtProduceState.Text = produceLog.cnvcProduceState;
//				if(produceLog.cnvcProduceState == "3" || produceLog.cnvcProduceState == "4")
//				{
//					this.btnCheck.Enabled = false;
//				}
//				else
//				{
//					this.btnCheck.Enabled = true;
//				}
				

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

		protected void btnReturn_Click(object sender, System.EventArgs e)
		{
			this.Response.Redirect("wfmProducePlanQueryGoods.aspx");
		}
		private void QueryProduceDetail()
		{
			string strSql = "select a.cnnProduceSerialNo,a.cnvcCode,a.cnvcName,a.cnvcUnit,a.cnnCount as cnnOrderCount,0 as cnnAddCount,0 as cnnReduceCount,0 as cnnProduceCount,isnull(b.cnnCheckCount,0) as cnnCheckCount,isnull(b.cnnAssignCount,0) as cnnAssignCount from tbProduceDetail a left outer join tbProduceCheckLog b"
					+" on a.cnnProduceSerialNo=b.cnnProduceSerialNo and a.cnvcCode=b.cnvcCode where a.cnnProduceSerialNo=" + txtProduceSerialNo.Text+" order by a.cnvcCode";
			string strAddSql = "select cnnProduceSerialNo,cnvcCode,cnvcName,cnvcUnit,cnnCount as cnnAddCount from tbProduceDetailAdd where cnvcState='2' and cnnProduceSerialNo=" +
				txtProduceSerialNo.Text+" order by cnvcCode";
			string strReduceSql = "select cnnProduceSerialNo,cnvcCode,cnvcName,cnvcUnit,cnnCount as cnnReduceCount from tbProduceDetailReduce where cnvcState='2' and cnnProduceSerialNo=" +
				txtProduceSerialNo.Text+" order by cnvcCode";
//			string strAssignSql = "select cnnProduceSerialNo,cnvcCode,cnvcName,cnvcUnit,cnnAssignCount from tbProduceCheckLog where cnnProduceSerialNo=" +
//				txtProduceSerialNo.Text+" order by cnvcCode";
			DataTable dtProduce = Helper.Query(strSql);
			DataTable dtProduceAdd = Helper.Query(strAddSql);
			DataTable dtProduceReduce = Helper.Query(strReduceSql);
			//DataTable dtAssign = Helper.Query(strAssignSql);

			foreach(DataRow drAdd in dtProduceAdd.Rows)
			{
				string strProductCode = drAdd["cnvcCode"].ToString();
				DataRow[] drProduces = dtProduce.Select("cnvcCode='" + strProductCode + "'");
				if(drProduces.Length == 0)
				{
					DataRow drProduce = dtProduce.NewRow();
					drProduce["cnnProduceSerialNo"] = drAdd["cnnProduceSerialNo"];
					drProduce["cnvcCode"] = drAdd["cnvcCode"];
					drProduce["cnvcName"] = drAdd["cnvcName"];
					drProduce["cnnOrderCount"] = 0;
					drProduce["cnnAddCount"] = drAdd["cnnCount"];
					drProduce["cnnRedueCount"] = 0;
					drProduce["cnnProduceCount"] = 0;
					drProduce["cnnCheckCount"] = 0;					
					drProduce["cnvcUnit"] = drAdd["cnvcUnit"];
					dtProduceAdd.Rows.Add(drProduce);
				}
			}
			foreach(DataRow drProduce in dtProduce.Rows)
			{
				string strProductCode = drProduce["cnvcCode"].ToString();
				DataRow[] drProduceAdds = dtProduceAdd.Select("cnvcCode='" + strProductCode + "'");
				if(drProduceAdds.Length > 0)
				{
					drProduce["cnnAddCount"] = drProduceAdds[0]["cnnAddCount"];
				}
				DataRow[] drProduceReduces = dtProduceReduce.Select("cnvcCode='" + strProductCode + "'");
				if(drProduceReduces.Length > 0)
				{
					drProduce["cnnReduceCount"] = drProduceReduces[0]["cnnReduceCount"];
				}
				decimal dOrderCount = Convert.ToDecimal(drProduce["cnnOrderCount"]);
				decimal dAddCount = Convert.ToDecimal(drProduce["cnnAddCount"]);
				decimal dReduceCount = Convert.ToDecimal(drProduce["cnnReduceCount"]);
				decimal dProduceCount = dOrderCount + dAddCount - dReduceCount;
				drProduce["cnnProduceCount"] = dProduceCount;
				//drProduce["cnnCheckCount"] = dProduceCount;
			}

			
			Session["tbProduceDetail"] = dtProduce;
		}
		private void BindCheckLog()
		{
			string strSql = "select * from tbProduceCheckLog where cnnProduceSerialNo=" + txtProduceSerialNo.Text+" order by cnvcCode";
			DataTable dtCheck = Helper.Query(strSql);
			DataGrid1.DataSource = dtCheck;
			DataGrid1.DataBind();
		}
		private void BindGrid()
		{
			if(Session["tbProduceDetail"] == null)
			{
				btnCheckQuery_Click(null,null);
			}
			else
			{
				btnQuery_Click(null,null);
			}
		}

		protected void btnQuery_Click(object sender, System.EventArgs e)
		{
			//BindGrid();
			if(Session["tbProduceDetail"] == null)
			{
				QueryProduceDetail();
			}
			DataTable dtProduce = (DataTable) Session["tbProduceDetail"];
				
			this.DataGrid1.DataSource = dtProduce;
			this.DataGrid1.DataBind();

			this.bntEdit.Visible = true;
			this.btnEndEdit.Visible = false;
			this.btnEditConfirm.Visible = false;
		}

		private void DataGrid1_CancelCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			this.DataGrid1.EditItemIndex = -1;
			this.BindGrid();			
		}

		private void DataGrid1_EditCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			this.DataGrid1.EditItemIndex = e.Item.ItemIndex;
			this.BindGrid();
		}

		private void DataGrid1_UpdateCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
//			AssignDetailLog detailLog = new AssignDetailLog();
//			detailLog.cnnAssignSerialNo = Convert.ToDecimal(txtProduceSerialNo.Text);
//			detailLog.cnnOrderSerialNo = Convert.ToDecimal(e.Item.Cells[0].Text);
//			detailLog.cnvcProductCode = e.Item.Cells[3].Text;
//			detailLog.cnnCount = Convert.ToDecimal(((TextBox) e.Item.Cells[8].Controls[0]).Text);
//			detailLog.cnvcOperID = oper.strLoginID;
//
//
//			detailLog.cnnPrice = Convert.ToDecimal(e.Item.Cells[6].Text);
//			detailLog.cnvcUnit = e.Item.Cells[5].Text;
//			detailLog.cnnOrderCount = Convert.ToDecimal(e.Item.Cells[7].Text);
//			detailLog.cnnSum = Math.Round(detailLog.cnnPrice*detailLog.cnnCount, 2);//Convert.ToDecimal(e.Item.Cells[9].Text);
//
//			GoodsFacade gf = new GoodsFacade();
//			gf.UpdateAssignLog(detailLog);
//

			try
			{
//				if(txtProduceState.Text == "4")
//					throw new Exception("已分货，无法调整盘点量");
				string strCheckCount = ((TextBox) e.Item.Cells[9].Controls[0]).Text;
				if(this.JudgeIsNull(strCheckCount))
					throw new Exception("请输入盘点量");
				if(!this.JudgeIsNum(strCheckCount))
					throw new Exception("请输入数字");
				string strProduceCount = e.Item.Cells[7].Text;
				string strAssignCount = e.Item.Cells[8].Text;
				
				decimal dProduceCount = Convert.ToDecimal(strProduceCount);
				decimal dCheckCount = Convert.ToDecimal(strCheckCount);
				decimal dAssignCount = Convert.ToDecimal(strAssignCount);
				if(dCheckCount >dProduceCount-dAssignCount)
					throw new Exception("盘点量过大，无法自动分货，请手工调整分货量");
				if(Session["tbProduceDetail"] == null)
				{
					ProduceCheckLog check = new ProduceCheckLog();
					check.cnvcOperID = oper.strLoginID;
					check.cnnProduceSerialNo = Convert.ToDecimal(e.Item.Cells[0].Text);
					check.cnvcCode = e.Item.Cells[1].Text;
					check.cnnCheckCount = Convert.ToDecimal(strCheckCount);
					GoodsFacade gf = new GoodsFacade();

					OperLog operLog = new OperLog();
					operLog.cnvcOperID = oper.strLoginID;
					operLog.cnvcDeptID = oper.strDeptID;
					operLog.cnvcOperType = "调整盘点量";

					gf.UpdateProduceCheck(check,operLog);
				}
				else
				{
//					if(Session["tbProduceDetail"] == null)
//					{
//						Popup("数据异常，请查询");
//						return;
//					}
					string strCode = e.Item.Cells[1].Text;
					//string strCheckCount = ((TextBox) e.Item.Cells[9].Controls[0]).Text;
					DataTable dtProduce = (DataTable) Session["tbProduceDetail"];
					DataRow[] drProduct = dtProduce.Select("cnvcCode='" + strCode + "'");
					drProduct[0]["cnnCheckCount"] = strCheckCount;
					Session["tbProduceDetail"] = dtProduce;
				}

				this.DataGrid1.EditItemIndex = -1;
				this.BindGrid();
				this.Popup("修改成功");
			}
			catch(Exception ex)
			{
				Popup(ex.Message);
			}
			
		}

		protected void btnCheck_Click(object sender, System.EventArgs e)
		{
			try
			{
				if(Session["tbProduceDetail"] == null)
				{
					throw new Exception("请首先使用【计划查询】按钮，查询计划情况");				
				}
				DataTable dtProduce = (DataTable) Session["tbProduceDetail"];
				GoodsFacade gf = new GoodsFacade();
				OperLog operLog = new OperLog();
				operLog.cnvcOperID = oper.strLoginID;
				operLog.cnvcDeptID = oper.strDeptID;
				operLog.cnvcOperType = "盘点入库";
				gf.ProduceCheck(dtProduce,operLog, ddlProduceDept.SelectedValue,txtProduceSerialNo.Text);
				Popup("盘点完成");
				//this.btnCheck.Enabled = false;
				this.DataGrid1.DataSource = null;
				this.DataGrid1.DataBind();
				Session["tbProduceDetail"] = null;

				this.bntEdit.Visible = false;
				this.btnEndEdit.Visible = false;
				this.btnEditConfirm.Visible = false;
			}
			catch(Exception ex)
			{
				Popup(ex.Message);
			}
			
		}

		private void DataGrid1_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			this.DataGrid1.CurrentPageIndex = e.NewPageIndex;
			BindGrid();

		}

		protected void btnCheckQuery_Click(object sender, System.EventArgs e)
		{
			Session.Remove("tbProduceDetail");
			BindCheckLog();

			this.bntEdit.Visible = true;
			this.btnEndEdit.Visible = false;
			this.btnEditConfirm.Visible = false;
		}

		protected void bntEdit_Click(object sender, System.EventArgs e)
		{
			try
			{
				foreach(DataGridItem gdi in this.DataGrid1.Items)
				{
					TextBox txt = (TextBox) gdi.Cells[9].Controls[1];
					txt.Enabled = true;
				}

				this.bntEdit.Visible = false;
				this.btnEndEdit.Visible = true;
				this.btnEditConfirm.Visible = true;
			}
			catch(Exception ex)
			{
				Popup(ex.Message);
			}
		}

		protected void btnEndEdit_Click(object sender, System.EventArgs e)
		{
			try
			{
				foreach(DataGridItem gdi in this.DataGrid1.Items)
				{
					TextBox txt = (TextBox) gdi.Cells[9].Controls[1];
					txt.Enabled = false;
				}
				this.bntEdit.Visible = true;
				this.btnEndEdit.Visible = false;
				this.btnEditConfirm.Visible = true;
			}
			catch(Exception ex)
			{
				Popup(ex.Message);
			}
		}

		protected void btnEditConfirm_Click(object sender, System.EventArgs e)
		{
			try
			{
				ArrayList alCheck = new ArrayList();
				foreach(DataGridItem dgi in this.DataGrid1.Items)
				{
					string strCheckCount = ((TextBox) dgi.Cells[9].Controls[1]).Text;
					if(this.JudgeIsNull(strCheckCount))
						throw new Exception("请输入盘点量");
					if(!this.JudgeIsNum(strCheckCount))
						throw new Exception("请输入数字");
					string strProduceCount = dgi.Cells[7].Text;
					string strAssignCount = dgi.Cells[8].Text;
				
					decimal dProduceCount = Convert.ToDecimal(strProduceCount);
					decimal dCheckCount = Convert.ToDecimal(strCheckCount);
					decimal dAssignCount = Convert.ToDecimal(strAssignCount);
					if(dCheckCount >dProduceCount-dAssignCount)
						throw new Exception("盘点量过大，无法自动分货，请手工调整分货量");
					if(Session["tbProduceDetail"] == null)
					{
						ProduceCheckLog check = new ProduceCheckLog();
						check.cnvcOperID = oper.strLoginID;
						check.cnnProduceSerialNo = Convert.ToDecimal(dgi.Cells[0].Text);
						check.cnvcCode = dgi.Cells[1].Text;
						check.cnnCheckCount = Convert.ToDecimal(strCheckCount);
						alCheck.Add(check);
					}
					else
					{
						string strCode = dgi.Cells[1].Text;
						DataTable dtProduce = (DataTable) Session["tbProduceDetail"];
						DataRow[] drProduct = dtProduce.Select("cnvcCode='" + strCode + "'");
						drProduct[0]["cnnCheckCount"] = strCheckCount;
						Session["tbProduceDetail"] = dtProduce;
					}
				}

				if(alCheck.Count > 0)
				{					
					GoodsFacade gf = new GoodsFacade();

					OperLog operLog = new OperLog();
					operLog.cnvcOperID = oper.strLoginID;
					operLog.cnvcDeptID = oper.strDeptID;
					operLog.cnvcOperType = "调整盘点量";
					
					gf.BatchUpdateProduceCheck(alCheck,this.txtProduceSerialNo.Text,operLog);
				}				

				//this.DataGrid1.EditItemIndex = -1;
				this.BindGrid();
				this.Popup("修改成功");
			}
			catch(Exception ex)
			{
				Popup(ex.Message);
			}
		}
	}
}
