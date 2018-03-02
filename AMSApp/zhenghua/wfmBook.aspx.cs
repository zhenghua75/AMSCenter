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
namespace AMSApp.zhenghua
{
	/// <summary>
	/// wfmBook 的摘要说明。
	/// </summary>
	public partial class wfmBook : wfmBase
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
			//this.btnBook.Attributes.Add("onclick","");			
			this.btnBook.Attributes["onclick"] = this.GetPostBackEventReference(this.btnBook) + ";this.disabled=true;";
			if(!this.IsPostBack)
			{
//				this.txtBook.Text = "";
//				this.txtFlag.Text = "ADD";
//				this.txtSerialNo.Text = "";
//				this.btnBook.Text = "添加留言";				

				this.txtBeginDate1.Text = DateTime.Now.ToString("yyyy-MM-dd");
				this.txtEndDate1.Text = DateTime.Now.ToString("yyyy-MM-dd");
				this.txtBeginDate2.Text = DateTime.Now.ToString("yyyy-MM-dd");
				this.txtEndDate2.Text = DateTime.Now.ToString("yyyy-MM-dd");

				this.BindBDept(this.ddlCheckDept);
				this.BindBDept(this.ddlCheckDept1,new ListItem("所有","%"));
				this.BindBDept(this.ddlCheckDept2,new ListItem("所有","%"));


//				string strsql = "select top 100 * from tbbook where cnvcstate='0' order by cnnserialno desc";
//				DataTable dt = Helper.Query(strsql);
//				this.DataGrid1.CurrentPageIndex = 0;
//				this.DataGrid1.DataSource = dt;
//				this.DataGrid1.DataBind();
//			
//				strsql = "select top 100 * from tbbook where cnvcstate='1' order by cnnserialno desc";
//				DataTable dt2 = Helper.Query(strsql);
//				this.DataGrid2.CurrentPageIndex = 0;
//				this.DataGrid2.DataSource = dt2;
//				this.DataGrid2.DataBind();
				this.btnQuery1_Click(null,null);
				this.btnQuery2_Click(null,null);
				btnReset_Click(null,null);

				
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
			this.DataGrid1.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.DataGrid1_ItemDataBound);
			this.DataGrid2.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DataGrid2_PageIndexChanged);
			this.DataGrid2.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.DataGrid2_ItemDataBound);

		}
		#endregion

		protected void btnBook_Click(object sender, System.EventArgs e)
		{
			try
			{
				OperLog operLog = new OperLog();
				operLog.cnvcOperID = oper.strLoginID;
				operLog.cnvcDeptID = oper.strDeptID;
				Book book = new Book();
				book.cmvcPublishID= oper.strLoginID;
				book.cnvcPublishName = oper.strOperName;
				book.cnvcCheckDept = this.ddlCheckDept.SelectedItem.Text;
				if(this.txtSerialNo.Text != "")
					book.cnnSerialNo = Convert.ToInt32(this.txtSerialNo.Text);
				if(this.txtBook.Text.Trim().Length == 0) throw new Exception("请输入留言");
				Business.BookFacade bf = new BookFacade();
				switch(this.txtFlag.Text)
				{
					case "ADD":
						operLog.cnvcOperType = "添加留言";					
						book.cnvcBook = this.txtBook.Text;
						book.cnvcState = "0";
						bf.AddBook(book,operLog);
						this.Popup("添加留言成功");
						break;
					case "MODIFY":
						operLog.cnvcOperType = "修改留言";
						book.cnvcBook = this.txtBook.Text;		
						book.cnvcCheckDept = this.ddlCheckDept.SelectedItem.Text;
						bf.UpdateBook(book,operLog);
						this.Popup("修改留言成功");
						break;
					case "CANCELCHECK":
						operLog.cnvcOperType = "取消确认留言";
						book.cnvcCheckID= oper.strLoginID;
						book.cnvcCheckName = oper.strOperName;
						book.cnvcState = "0";
						bf.CheckBook(book,operLog);
						this.Popup("取消确认留言成功");
						break;
					case "CHECK":
						operLog.cnvcOperType = "确认留言";
						book.cnvcCheckID= oper.strLoginID;
						book.cnvcCheckName = oper.strOperName;
						book.cnvcState = "1";
						bf.CheckBook(book,operLog);
						this.Popup("确认留言成功");
						break;
					case "RETURN":
						operLog.cnvcOperType = "回复留言";
						Entity.BookReturn br = new BookReturn();
						br.cnnBookID = Convert.ToInt32(txtSerialNo.Text);
						br.cnvcReturn = txtBook.Text;
						br.cnvcReturnID = oper.strLoginID;
						br.cnvcReturnName = oper.strOperName;
						bf.BookReturn(br,operLog);
						BindReturn(txtSerialNo.Text);
						this.Popup("回复成功");
						break;
				}
				this.btnBook.Enabled = true;
				btnReset_Click(null,null);
				this.btnQuery1_Click(null,null);
				this.btnQuery2_Click(null,null);
			}
			catch(Exception ex)
			{
				this.Popup(ex.Message);
			}
			
			
		}

		protected void btnQuery1_Click(object sender, System.EventArgs e)
		{
			//
			string strsql = "select bk.*,od.vccommname as cnvcpublishdept,bk.cnvcCheckDept from tbbook bk "
+" left join ( "
+" select lg.vcloginid,lg.vcdeptid,dept.vccommname from tblogin lg  "
+" left join (select vccommcode,vccommname from tbcommcode where vccommsign='MD') "
+"  dept on lg.vcdeptid=dept.vccommcode) od "
+"  on bk.cmvcpublishid=od.vcloginid "
//+" left join (select vccommcode,vccommname from tbcommcode where vccommsign='BDEPT') "
//+"  dept on bk.cnvccheckdept=dept.vccommname "
				+"where cnvcstate='0' and convert(char(10),cndpublishdate,121)>='"+this.txtBeginDate1.Text
				+"' and convert(char(10),cndpublishdate,121)<='"+this.txtEndDate1.Text+"'";
			if(this.ddlCheckDept1.SelectedItem.Text != "所有")
				strsql+= " and bk.cnvcCheckDept like '"+this.ddlCheckDept1.SelectedItem.Text+"'";
			DataTable dt = Helper.Query(strsql);
			//this.DataGrid1.CurrentPageIndex = 0;
			
			this.DataGrid1.DataSource = dt;
			this.DataGrid1.DataBind();
			btnReset_Click(null,null);
		}

		protected void btnQuery2_Click(object sender, System.EventArgs e)
		{
			//
			string strsql = "select bk.*,od.vccommname as cnvcpublishdept,cnvcCheckDept from tbbook bk "
				+"  left join ( "
				+"  select lg.vcloginid,lg.vcdeptid,dept.vccommname from tblogin lg  "
				+"  left join (select vccommcode,vccommname from tbcommcode where vccommsign='MD') "
				+"  dept on lg.vcdeptid=dept.vccommcode) od "
				+"  on bk.cmvcpublishid=od.vcloginid "
				//+"  left join ( "
				//+"  select lg.vcloginid,lg.vcdeptid,dept.vccommname from tblogin lg  "
				//+"  left join (select vccommcode,vccommname from tbcommcode where vccommsign='MD') "
				//+"  dept on lg.vcdeptid=dept.vccommcode) od2 "
				//+"  on bk.cnvccheckid=od2.vcloginid "
				+"  where cnvcstate='1' and convert(char(10),cndcheckdate,121)>='"+this.txtBeginDate2.Text
				+"' and convert(char(10),cndcheckdate,121)<='"+this.txtEndDate2.Text+"'";
			if(this.ddlCheckDept2.SelectedItem.Text != "所有")
				strsql+=  " and bk.cnvcCheckDept like '"+this.ddlCheckDept2.SelectedItem.Text+"'";
			DataTable dt = Helper.Query(strsql);
			//this.DataGrid2.CurrentPageIndex = 0;
			this.DataGrid2.DataSource = dt;
			this.DataGrid2.DataBind();
			btnReset_Click(null,null);
		}

		protected void btnReset_Click(object sender, System.EventArgs e)
		{
			//
			this.txtBook.Text = "";
			this.txtFlag.Text = "ADD";
			this.txtSerialNo.Text = "";
			this.btnBook.Text = "添加留言";
		}

		protected void DataGrid1_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			//
			string strserialno = this.DataGrid1.SelectedItem.Cells[0].Text;
			string strsql = "select * from tbbook where cnnserialno="+strserialno;
			DataTable dt = Helper.Query(strsql);
			Entity.Book book = new Book(dt);
			
			this.txtSerialNo.Text = book.cnnSerialNo.ToString();
			this.txtBook.Text = book.cnvcBook;
			if(book.cmvcPublishID == oper.strLoginID)
			{
				this.txtFlag.Text = "MODIFY";
				this.btnBook.Text = "修改留言";
			}
			else
			{
				this.txtFlag.Text = "CHECK";
				this.btnBook.Text = "确认留言";	
			}
			this.DataGrid2.SelectedIndex = -1;
			BindReturn(book.cnnSerialNo.ToString());
			
		}

		private void BindReturn(string strSerialNo)
		{
			string strsql = "select * from tbBookReturn where cnnBookID="+strSerialNo+" order by cnnSerialNo desc";
			DataTable dt = Helper.Query(strsql);
			lblReturn.Text = "";
			foreach(DataRow dr in dt.Rows)
			{
				Entity.BookReturn br = new BookReturn(dr);
				lblReturn.Text += "<hr>";
				lblReturn.Text += "<p class='bookreturn'>"+br.cnvcReturn+"</p>";
				lblReturn.Text += "<p class='bookoper'>"+br.cnvcReturnName+"</p>";
				lblReturn.Text += "<p class='bookdate'>"+br.cndReturnDate.ToString("yyyy-MM-dd hh:mm:ss")+"</p>";					
				lblReturn.Text += "<hr>";
			}
		}
		protected void DataGrid2_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			//
			string strserialno = this.DataGrid2.SelectedItem.Cells[1].Text;
			string strsql = "select * from tbbook where cnnserialno="+strserialno;
			DataTable dt = Helper.Query(strsql);
			Entity.Book book = new Book(dt);
			if(book.cnvcCheckID == oper.strLoginID)
			{
				this.txtFlag.Text = "CANCELCHECK";
				this.btnBook.Text = "取消确认留言";
			}
			else
			{
				this.txtFlag.Text = "NOOPER";
				this.btnBook.Text = "查看";
			}
			this.txtSerialNo.Text = book.cnnSerialNo.ToString();
			this.txtBook.Text = book.cnvcBook;
			this.DataGrid1.SelectedIndex = -1;
			BindReturn(book.cnnSerialNo.ToString());
		}

		private void DataGrid1_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			this.DataGrid1.CurrentPageIndex = e.NewPageIndex;
			this.btnQuery1_Click(null,null);
		}

		private void DataGrid2_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			this.DataGrid2.CurrentPageIndex = e.NewPageIndex;
			this.btnQuery2_Click(null,null);
		}

		private void DataGrid1_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			if(e.Item.Cells[4].Text.ToString().Length>25)   
			{   
				e.Item.Cells[4].Text=e.Item.Cells[4].Text.Substring(0,25)+"……";   
			}   
		}

		private void DataGrid2_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			if(e.Item.Cells[5].Text.ToString().Length>25)   
			{   
				e.Item.Cells[5].Text=e.Item.Cells[5].Text.Substring(0,25)+"……";   
			}   
		}

		protected void btnReturn_Click(object sender, System.EventArgs e)
		{
			this.txtFlag.Text = "RETURN";
			this.txtBook.Text = "";
			this.btnBook.Text = "回复留言【"+txtSerialNo.Text+"】";
		}
	}
}
