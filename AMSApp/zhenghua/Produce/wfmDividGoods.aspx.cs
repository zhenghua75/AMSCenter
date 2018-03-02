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
	/// wfmDividGoods ��ժҪ˵����
	/// </summary>
	public partial class wfmDividGoods : wfmBase
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// �ڴ˴������û������Գ�ʼ��ҳ��
			if(!this.IsPostBack)
			{
				this.BindDept(ddlProduceDept, "cnvcDeptType <>'Corp'");
				this.BindDept(ddlOrderDept, "cnvcDeptType <>'Corp'");
				ListItem li = new ListItem("����", "%");
				ddlOrderDept.Items.Insert(0, li);
				//this.BindNameCode(ddlGroup, "cnvcType='GROUP'");
				if(Request["ProduceSerialNo"] == null)
				{
					Popup("��Ч����");
					return;
				}
				//string strOperType = Request["OperType"].ToString();
				//ViewState["OperType"] = strOperType;
				string strProduceSerialNo = Request["ProduceSerialNo"].ToString();
				BindProduceLog(strProduceSerialNo);
				this.btnExcel.Enabled = false;
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
			this.DataGrid2.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DataGrid2_PageIndexChanged);
			this.DataGrid2.CancelCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGrid2_CancelCommand);
			this.DataGrid2.EditCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGrid2_EditCommand);
			this.DataGrid2.UpdateCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGrid2_UpdateCommand);

		}
		#endregion

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
//				if(produceLog.cnvcProduceState == "4")
//				{
//					this.btnDivideGoods.Enabled = false;
//				}
//				else
//				{
//					this.btnDivideGoods.Enabled = true;
//				}

			}
			BindAssignLog(strProduceSerialNo);
			

		}
		private void BindAssignLog(string strProduceSerialNo)
		{
			string strAssignSql = "select distinct cnnAssignSerialNo from tbAssignLog where cnnProduceSerialNo=" + strProduceSerialNo;
			DataTable dtAssignLog = Helper.Query(strAssignSql);
			this.ddlAssignSerialNo.Items.Clear();
			this.ddlAssignSerialNo.DataSource = dtAssignLog;
			this.ddlAssignSerialNo.DataTextField = "cnnAssignSerialNo";
			this.ddlAssignSerialNo.DataValueField = "cnnAssignSerialNo";
			this.ddlAssignSerialNo.DataBind();
		}
		protected void btnReturn_Click(object sender, System.EventArgs e)
		{
			this.Response.Redirect("wfmProducePlanQueryGoods.aspx");
		}

		protected void btnDivideGoods_Click(object sender, System.EventArgs e)
		{
			try
			{
				ProduceLog pLog = new ProduceLog();
				pLog.cnnProduceSerialNo = decimal.Parse(txtProduceSerialNo.Text);
				pLog.cnvcOperID = oper.strLoginID;

				OperLog operLog = new OperLog();
				operLog.cnvcOperID = oper.strLoginID;
				operLog.cnvcDeptID = oper.strDeptID;
				operLog.cnvcOperType = "�ֻ�";

				GoodsFacade goods = new GoodsFacade();
				goods.AddAssignLog(pLog,operLog);
				BindAssignLog(txtProduceSerialNo.Text);
				Popup("�ֻ��ɹ�");
				//this.btnDivideGoods.Enabled = false;
			}
			catch(Exception ex)
			{
				Popup(ex.Message);
			}
			
		}

		protected void btnQueryGoods_Click(object sender, System.EventArgs e)
		{
			try
			{
				if(ddlAssignSerialNo.Items.Count <1)
					throw new Exception("���������ɷֻ�����");
				//���ɷֻ�ƾ��
				string strSql = "select a.cnvcReceiveDeptID,b.cnvcProductCode,b.cnvcProductName,sum(b.cnnCount) as cnnCount from tbAssignLog a "
					+ " left outer join tbAssignDetail b "
					+ " on a.cnnAssignSerialNo=b.cnnAssignSerialNo "
					+ " and a.cnnOrderSerialNo=b.cnnOrderSerialNo "
					+ " where a.cnnAssignSerialNo=" + ddlAssignSerialNo.SelectedValue//txtProduceSerialNo.Text
					+ " group by a.cnvcReceiveDeptID,b.cnvcProductCode,b.cnvcProductName  order by b.cnvcProductCode";
				DataTable dtAssign = Helper.Query(strSql);
				this.DataTableConvert(dtAssign, "cnvcReceiveDeptID", "tbDept", "cnvcDeptID", "cnvcDeptName", "");//cnvcshipDeptID
				DataTable dtDept = (DataTable)Application["tbDept"];
			
				string strSql2 = "select b.cnvcProductCode,b.cnvcProductName,sum(b.cnnCount) as cnnCount from tbAssignLog a "
					+ " left outer join tbAssignDetail b "
					+ " on a.cnnAssignSerialNo=b.cnnAssignSerialNo "
					+ " and a.cnnOrderSerialNo=b.cnnOrderSerialNo "
					+ " where a.cnnAssignSerialNo=" + ddlAssignSerialNo.SelectedValue//txtProduceSerialNo.Text
					+ " group by b.cnvcProductCode,b.cnvcProductName  order by b.cnvcProductCode";
				DataTable dtAssign2 = Helper.Query(strSql2);

				this.DataGrid1.Caption = "�ֻ�ƾ��";
				DataTable dtpt = new DataTable();
				dtpt.Columns.Add("��Ʒ����");
				//dtpt.Columns.Add("��Ʒ����");
				dtpt.Columns.Add("��Ʒ����");
				foreach(DataRow drDept in dtDept.Rows)
				{
					if(!drDept["cnvcDeptType"].ToString().Equals("Corp"))//&&!drDept["cnvcDeptType"].ToString().Equals("FYZX1"))
					{
						dtpt.Columns.Add(drDept["cnvcDeptName"].ToString());	
					}
						
				}
				dtpt.Columns.Add("�ϼ�");
				//dtpt.Columns.Add("�ϼ�");
				Hashtable htProduct = new Hashtable();
				foreach(DataRow drAssign in dtAssign.Rows)
				{
					//DataRow drpt = null;
					DataRow[] drpts = dtpt.Select("��Ʒ����='"+drAssign["cnvcProductCode"].ToString()+"'");
					if(drpts.Length > 0)
					{
						DataRow drpt = drpts[0];
						drpt["��Ʒ����"] = drAssign["cnvcProductCode"].ToString();
						drpt["��Ʒ����"] = drAssign["cnvcProductName"].ToString();
						drpt[drAssign["cnvcReceiveDeptIDComments"].ToString()] = drAssign["cnnCount"].ToString();
						DataRow[] drAssign2 = dtAssign2.Select("cnvcProductCode='" + drAssign["cnvcProductCode"].ToString() + "'");
						if(drAssign2.Length > 0)
							drpt["�ϼ�"] = drAssign2[0]["cnnCount"];
						//dtpt.Rows.Add(drpt);
					}
					else
					{
						DataRow drpt = dtpt.NewRow();
						drpt["��Ʒ����"] = drAssign["cnvcProductCode"].ToString();
						drpt["��Ʒ����"] = drAssign["cnvcProductName"].ToString();
						drpt[drAssign["cnvcReceiveDeptIDComments"].ToString()] = drAssign["cnnCount"].ToString();
						DataRow[] drAssign2 = dtAssign2.Select("cnvcProductCode='" + drAssign["cnvcProductCode"].ToString() + "'");
						if(drAssign2.Length > 0)
							drpt["�ϼ�"] = drAssign2[0]["cnnCount"];
						dtpt.Rows.Add(drpt);

					}
				
				
				}
				DataView dv = new DataView(dtpt);
				dv.Sort = "��Ʒ����";
				this.DataGrid1.DataSource = dv;
				this.DataGrid1.DataBind();

				this.DataGrid2.DataSource = null;
				this.DataGrid2.DataBind();
				this.btnExcel.Enabled=true;
				this.btnEdit.Visible = false;
			}
			catch(Exception ex)
			{
				Popup(ex.Message);
			}
		}

		private void BindAssignDetail()
		{
			string strSql = "select a.*,b.cnvcShipDeptID,b.cnvcReceiveDeptID,b.cndShipDate,c.cnvcOrderType  "
			                + " from tbAssignDetail a "
			                + " left outer join tbAssignLog  b on a.cnnAssignSerialNo=b.cnnAssignSerialNo and a.cnnOrderSerialNo=b.cnnOrderSerialNo "
			                + " left outer join tbOrderBook c on a.cnnOrderSerialNo=c.cnnOrderSerialNo "
			                + " where a.cnnAssignSerialNo=" + ddlAssignSerialNo.SelectedValue + " and cnvcReceiveDeptID like '" +
			                ddlOrderDept.SelectedValue + "' and a.cnnOrderSerialNo not in (select cnnOrderSerialNo from tbAssignLog where cnnPrintFlag>0 and cnnAssignSerialNo=" + ddlAssignSerialNo.SelectedValue + ") ";
			if(txtProductCode.Text != "")
			{
				strSql += " and cnvcProductCode like '%" + txtProductCode.Text + "%'";
			}
			if(txtProductName.Text != "")
			{
				strSql += " and cnvcProductName like '%" + txtProductName.Text + "%'";
			}
			strSql += " order by a.cnvcProductCode";
			DataTable dtAssignDetail = Helper.Query(strSql);
			this.DataTableConvert(dtAssignDetail, "cnvcShipDeptID", "tbDept", "cnvcDeptID", "cnvcDeptName", "");
			this.DataTableConvert(dtAssignDetail, "cnvcReceiveDeptID", "tbDept", "cnvcDeptID", "cnvcDeptName", "");
			this.DataTableConvert(dtAssignDetail, "cnvcOrderType", "tbNameCode", "cnvcCode", "cnvcName", "cnvcType='ORDERTYPE'");
			this.DataGrid2.DataSource = dtAssignDetail;
			this.DataGrid2.DataBind();
			this.DataGrid1.DataSource = null;
			this.DataGrid1.DataBind();
		}
		protected void btnQuery_Click(object sender, System.EventArgs e)
		{
			try
			{
				if(ddlAssignSerialNo.Items.Count < 1)
					throw new Exception("���������ɷֻ�����");
				this.btnExcel.Enabled=false;
				BindAssignDetail();
				this.btnEdit.Visible = true;
				this.btnEndEdit.Visible = false;
				this.btnEditConfirm.Visible = false;
			}
			catch(Exception ex)
			{
				Popup(ex.Message);
			}
		}

		private void DataGrid2_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			this.DataGrid2.CurrentPageIndex = e.NewPageIndex;
			BindAssignDetail();
		}

		private void DataGrid2_CancelCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			this.DataGrid2.EditItemIndex = -1;
			BindAssignDetail();

		}

		private void DataGrid2_EditCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			this.DataGrid2.EditItemIndex = e.Item.ItemIndex;
			BindAssignDetail();
		}

		private void DataGrid2_UpdateCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			try
			{
//				if(txtProduceState.Text == "4")
//				{
//					throw new Exception("�˶��������䣬�����޸ķֻ���");
//				}
				string strCount = ((TextBox) e.Item.Cells[11].Controls[0]).Text;
				if(this.JudgeIsNull(strCount))
					throw new Exception("�������̵���");
				if(!this.JudgeIsNum(strCount))
					throw new Exception("����������");
				AssignDetail assign = new AssignDetail();
				assign.cnnAssignSerialNo = Convert.ToDecimal(e.Item.Cells[0].Text);
				assign.cnnProduceSerialNo = Convert.ToDecimal(txtProduceSerialNo.Text);
				assign.cnnOrderSerialNo = Convert.ToDecimal(e.Item.Cells[3].Text);
				assign.cnvcProductCode = e.Item.Cells[6].Text;
				assign.cnnCount = Convert.ToDecimal(((TextBox) e.Item.Cells[11].Controls[0]).Text);				
				string strOrderType = e.Item.Cells[14].Text;
				decimal dOrderCount = Convert.ToDecimal(e.Item.Cells[10].Text);
				if(strOrderType=="WDO")
				{
					if(dOrderCount > assign.cnnCount)
					{
						throw new Exception("�ⶩ�����ֻ����������ڶ�����");
					}
				}
				OperLog operLog = new OperLog();
				operLog.cnvcOperID = oper.strLoginID;
				operLog.cnvcDeptID = oper.strDeptID;
				operLog.cnvcOperType = "�����ֻ���";

				GoodsFacade gf = new GoodsFacade();
				gf.UpdateAssignLog(assign,operLog);
				this.Popup("�޸ĳɹ�");
				this.DataGrid2.EditItemIndex = -1;
				BindAssignDetail();
			}
			catch(Exception ex)
			{
				Popup(ex.Message);					
			}

		}

		protected void btnExcel_Click(object sender, System.EventArgs e)
		{
			this.DataGridToExcel(this.DataGrid1, this.ddlProduceDept.SelectedItem.Text+"�ֻ�ƾ��");
		}

		protected void btnEdit_Click(object sender, System.EventArgs e)
		{
			try
			{
				
				foreach(DataGridItem gdi in this.DataGrid2.Items)
				{
					TextBox txt = (TextBox) gdi.Cells[11].Controls[1];
					txt.Enabled = true;
				}
				this.btnEdit.Visible = false;
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
				
				foreach(DataGridItem gdi in this.DataGrid2.Items)
				{
					TextBox txt = (TextBox) gdi.Cells[11].Controls[1];
					txt.Enabled = false;
				}
				this.btnEdit.Visible = true;
				this.btnEditConfirm.Visible = true;
				this.btnEndEdit.Visible = false;
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
				//				if(txtProduceState.Text == "4")
				//				{
				//					throw new Exception("�˶��������䣬�����޸ķֻ���");
				//				}
				ArrayList alDetail = new ArrayList();
				foreach(DataGridItem dgi in this.DataGrid2.Items)
				{
					string strCount = ((TextBox) dgi.Cells[11].Controls[1]).Text;
					if(this.JudgeIsNull(strCount))
						throw new Exception("�������̵���");
					if(!this.JudgeIsNum(strCount))
						throw new Exception("����������");
					AssignDetail assign = new AssignDetail();
					assign.cnnAssignSerialNo = Convert.ToDecimal(dgi.Cells[0].Text);
					assign.cnnProduceSerialNo = Convert.ToDecimal(txtProduceSerialNo.Text);
					assign.cnnOrderSerialNo = Convert.ToDecimal(dgi.Cells[3].Text);
					assign.cnvcProductCode = dgi.Cells[6].Text;
					assign.cnnCount = Convert.ToDecimal(((TextBox) dgi.Cells[11].Controls[1]).Text);				
					string strOrderType =dgi.Cells[13].Text;
					decimal dOrderCount = Convert.ToDecimal(dgi.Cells[10].Text);
					if(strOrderType=="WDO")
					{
						if(dOrderCount > assign.cnnCount)
						{
							throw new Exception("�ⶩ�����ֻ����������ڶ�����");
						}
					}
					alDetail.Add(assign);
					

				}

				OperLog operLog = new OperLog();
				operLog.cnvcOperID = oper.strLoginID;
				operLog.cnvcDeptID = oper.strDeptID;
				operLog.cnvcOperType = "�����ֻ���";

				GoodsFacade gf = new GoodsFacade();
				gf.BatchUpdateAssignLog(alDetail,operLog,txtProduceSerialNo.Text);

				this.Popup("�޸ĳɹ�");
				//this.DataGrid2.EditItemIndex = -1;
				BindAssignDetail();

				this.btnEdit.Visible = true;
				this.btnEndEdit.Visible = false;
				this.btnEditConfirm.Visible = false;
			}
			catch(Exception ex)
			{
				Popup(ex.Message);					
			}
			
		}
	}
}
