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
	/// wfmMakeDetail ��ժҪ˵����
	/// </summary>
	public partial class wfmMakeDetail : wfmBase
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{			
			// �ڴ˴������û������Գ�ʼ��ҳ��
			//btnPrint.Attributes.Add("onclick","return PrintDataGrid(document.all('DataGrid1'))");
			if(!this.IsPostBack)
			{
				if(Request["MakeSerialNo"] == null)
				{
					Popup("��Ч����");
					return;
				}
				string strDetailSql="";
				string strMakeSerialNo = Request["MakeSerialNo"].ToString();
				string strMakeType = Request["MakeType"].ToString();
				string strMakeLogSql = "select * from tbMakeLog where cnnMakeSerialNo="+strMakeSerialNo;
				string strSql = "select cnvcCode,cnvcname,'' as Speci,cnvcunit,cnnCount from tbMakeDetail where cnnMakeSerialNo="+strMakeSerialNo;
				DataTable dtDetailtmp = Helper.Query(strSql);
				MakeDetail mDetail = new MakeDetail(dtDetailtmp);
				string strcode=mDetail.cnvcCode.ToString();
				if (strcode.Length.ToString()!="5")
				{
					strDetailSql = "select cnvcCode,cnvcname,'' as Speci,cnvcunit,cnnCount from tbMakeDetail where cnnMakeSerialNo="+strMakeSerialNo;
				}
				else 
				{
					strDetailSql = "select a.cnvcCode,a.cnvcname,convert(varchar,b.cnnStatdardCount)+ b.cnvcUnit + '/'+cnvcStandardUnit as Speci,a.cnvcunit,a.cnnCount from tbMakeDetail a,tbMaterial b where a.cnvcCode=b.cnvcMaterialCode and cnnMakeSerialNo="+strMakeSerialNo ;
				}
				
				DataTable dtDetail = Helper.Query(strDetailSql);
				DataTable dtMakeLog = Helper.Query(strMakeLogSql);
				MakeLog mLog = new MakeLog(dtMakeLog);
				if(mLog.cnvcMakeType == "0")
				{
					this.DataGrid1.Caption = mLog.cnvcMakeName + DateTime.Now.ToString("yyyy��MM��dd��")+"<br><div align='left'>������ˮ��"+mLog.cnnProduceSerialNo.ToString()+"      ������ˮ��"+strMakeSerialNo+"</div>";
					this.DataGrid1.DataSource = dtDetail;
					this.DataGrid1.DataBind();
					
				}
				else
				{
					this.Datagrid2.Caption = mLog.cnvcMakeName + DateTime.Now.ToString("yyyy��MM��dd��")+"<br><div align='left'>������ˮ��"+mLog.cnnProduceSerialNo.ToString()+"      ������ˮ��"+strMakeSerialNo+"</div>";
					this.Datagrid2.DataSource = dtDetail;
					this.Datagrid2.DataBind();
				}
				

				//Session["toExcel"] = dtDetail;
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
			this.DataGrid1.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.DataGrid1_ItemDataBound);
			this.Datagrid2.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.Datagrid2_ItemDataBound);

		}
		#endregion

		protected void btnReturn_Click(object sender, System.EventArgs e)
		{
			if(Session["ProduceSerialNo"] != null)
			this.Response.Redirect("wfmMakeLog.aspx?ProduceSerialNo="+Session["ProduceSerialNo"].ToString());
		}

		protected void btnExcel_Click(object sender, System.EventArgs e)
		{
			if(this.DataGrid1.Items.Count > 0)
			{
				this.DataGridToExcel(DataGrid1, this.DataGrid1.Caption);
			}
			else
			{
				this.DataGridToExcel(Datagrid2, this.Datagrid2.Caption);
			}
			
		}

		private void DataGrid1_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			if(e.Item.ItemType==ListItemType.Footer)
			{
				e.Item.Cells[0].ColumnSpan=4;    
				e.Item.Cells[0].Text = "����������ˣ�";
				for(int  j=1;j<4;j++)      
				{      
					e.Item.Cells[j].Visible=false;      
				}   
				e.Item.Cells[4].ColumnSpan=3;     
				e.Item.Cells[4].Text = "�����ˣ�"+oper.strOperName;
				for(int  j=5;j<7;j++)      
				{      
					e.Item.Cells[j].Visible=false;      
				}
			}
		}

		private void Datagrid2_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			if(e.Item.ItemType==ListItemType.Footer)      
			{      
				e.Item.Cells[0].ColumnSpan=2;     
				e.Item.Cells[0].Text = "����������ˣ�";
				e.Item.Cells[1].Visible=false;     
				e.Item.Cells[2].ColumnSpan=2;  
				e.Item.Cells[2].Text = "�����ˣ�"+oper.strOperName;
				e.Item.Cells[3].Visible=false; 				
			}
		}
	}
}
