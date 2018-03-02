using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using AMSApp.zhenghua.Business;
using AMSApp.zhenghua.Common;
using CommCenter;
using System.Text;
namespace AMSApp.zhenghua
{
	/// <summary>
	/// wfmBase 的摘要说明。
	/// </summary>
	public partial class wfmBase : System.Web.UI.Page
	{
        protected override void OnLoad(EventArgs e)
        {
            if (Session["Login"] == null)
            {
                Response.Redirect(this.Request.ApplicationPath + "/Exit.aspx");
                return;
            }
            CMSMStruct.LoginStruct ls = (CMSMStruct.LoginStruct)Session["Login"];
            Session["Login"] = ls;
            base.OnLoad(e);
        }
		public CMSMStruct.LoginStruct oper
		{
			get
			{
				return (CMSMStruct.LoginStruct) Session["Login"];
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
			this.Error += new System.EventHandler(this.wfmBase_Error);

		}
		#endregion

		//弹出窗口
		public void Popup(string strComments)
		{
			strComments = strComments.Replace("'","");
			strComments = strComments.Replace("\r\n","");
			this.Response.Write("<script>alert('"+strComments+"');</script>");
		}
		protected bool IsOut(string str, int len)
		{
			//string result = string.Empty;// 最终返回的结果
			int byteLen = System.Text.Encoding.Default.GetByteCount(str);// 单字节字符长度
			int charLen = str.Length;// 把字符平等对待时的字符串长度
			int byteCount = 0;// 记录读取进度
			int pos = 0;// 记录截取位置
			bool bOut = false;
			if (byteLen > len)
			{
				for (int i = 0; i < charLen; i++)
				{
					if (Convert.ToInt32(str.ToCharArray()[i]) > 255)// 按中文字符计算加2
						byteCount += 2;
					else// 按英文字符计算加1
						byteCount += 1;
					if (byteCount > len)// 超出时只记下上一个有效位置
					{
						pos = i;
						break;
					}
					else if(byteCount == len)// 记下当前位置
					{
						pos = i + 1;
						break;
					}
				}

				if(pos >= 0)
					//result = str.Substring(0, pos);
					bOut=true;
			}
			else
				bOut=false;

			return bOut;
		}
		public bool JudgeIsNull(string strText,string strMessage)
		{
			if(strText.Trim().Length == 0)
			{
				Popup(strMessage+"不能为空");
				return true;
			}
			return false;
		}
		public bool JudgeIsNull(string strText)
		{
			if(strText.Trim().Length == 0)
			{
				//Popup(strMessage+"不能为空");
				return true;
			}
			return false;
		}
		public bool JudgeIsNum(string strText,string strMessage)
		{
			if(!Regex.IsMatch(strText,@"^[+|-]{0,1}(\d*)\.{0,1}\d{0,}$"))
			{
				Popup(strMessage+"请输入数字");
				return false;
			}
			return true;
		}
		public bool JudgeIsNum(string strText)
		{
			if(!Regex.IsMatch(strText,@"^[+|-]{0,1}(\d*)\.{0,1}\d{0,}$"))
			{
				//Popup(strMessage+"请输入数字");
				return false;
			}
			return true;
		}
		public bool JudgeIsCode(string strProductType,string strProductClass,string strProductCode)
		{
			bool isTrue = false;
			try
			{
				switch(strProductType)
				{
					case "Raw":
					case "Pack":
					case "FINALPRODUCT":
						//用数字
						string[] strProductClasses = strProductClass.Split('~');
						int iProductCode = Convert.ToInt32(strProductCode);
						int iUpProductCode = Convert.ToInt32(strProductClasses[0]);
						int iDownProductCode = Convert.ToInt32(strProductClasses[1]);
						if(iUpProductCode <= iProductCode && iProductCode <= iDownProductCode)
						{
							isTrue = true;
						}
						break;
					case "SEMIPRODUCT":
						//带字母数字
						string[] strProductClasses1 = strProductClass.Split('~');
						string strProductClassLetter = strProductClasses1[0].Substring(0, 1);
						string strPorductCodeLetter = strProductCode.Substring(0, 1);
						//if(strProductClassLetter =)
						int iProductCode1 = Convert.ToInt32(strProductCode.Substring(1,strProductCode.Length-1));
						int iUpProductCode1 = Convert.ToInt32(strProductClasses1[0].Substring(1,strProductCode.Length-1));
						int iDownProductCode1 = Convert.ToInt32(strProductClasses1[1].Substring(1,strProductCode.Length-1));
						if(iUpProductCode1 <= iProductCode1 && iProductCode1 <= iDownProductCode1)
						{
							if(strProductClassLetter == strPorductCodeLetter)
							{
								isTrue = true;
							}
							
						}
						break;
				}
			}
			catch(Exception)
			{
				//Popup("编码错误");
				isTrue = false;
			}
			return isTrue;
		}
		public void BindNameCode(DropDownList ddl,string strFilter)
		{
			DataTable dtNameCode = null;			
			if(Application["tbNameCode"] != null)
			{
				dtNameCode = (DataTable) Application["tbNameCode"];
			}
			else
			{
				dtNameCode = Helper.Query("select * from  tbNameCode");
				Application["tbNameCode"] = dtNameCode;
			}
			DataView dvNameCode = new DataView(dtNameCode);
			dvNameCode.RowFilter = strFilter;
			ddl.DataSource = dvNameCode;			
			ddl.DataValueField = "cnvcCode";
			ddl.DataTextField = "cnvcName";
			ddl.DataBind();
		}
		public void BindProductClass(DropDownList ddl,string strFilter)
		{
			DataTable dtProductClass = null;			
			if(Application["tbProductClass"] != null)
			{
				dtProductClass = (DataTable) Application["tbProductClass"];
			}
			else
			{
				dtProductClass = Helper.Query("select * from  tbProductClass");
				Application["tbProductClass"] = dtProductClass;
			}
			DataView dvProductClass = new DataView(dtProductClass);
			dvProductClass.RowFilter = strFilter;
			ddl.DataSource = dvProductClass;			
			ddl.DataValueField = "cnvcProductClassCode";
			ddl.DataTextField = "cnvcProductClassName";
			ddl.DataBind();
		}

		public void BindDept(DropDownList ddl,string strFilter)
		{
			DataTable dtDept = null;			
			if(Application["tbDept"] != null)
			{
				dtDept = (DataTable) Application["tbDept"];
			}
			else
			{
				dtDept = Helper.Query("select * from  tbDept");
				Application["tbDept"] = dtDept;
			}
			DataView dvDept = new DataView(dtDept);
			dvDept.RowFilter = strFilter;
			ddl.DataSource = dvDept;			
			ddl.DataValueField = "cnvcDeptID";
			ddl.DataTextField = "cnvcDeptName";
			ddl.DataBind();
		}
		public void BindCommCode(DropDownList ddl,string strFilter)
		{
			DataTable dtDept = null;			
			if(Application["tbCommCodeBDEPT"] != null)
			{
				dtDept = (DataTable) Application["tbCommCodeBDEPT"];
			}
			else
			{
				dtDept = Helper.Query("select * from  tbCommCode");
				Application["tbCommCodeBDEPT"] = dtDept;
			}
			DataView dvDept = new DataView(dtDept);
			dvDept.RowFilter = strFilter;
			ddl.DataSource = dvDept;			
			ddl.DataValueField = "vcCommCode";
			ddl.DataTextField = "vcCommName";
			ddl.DataBind();
		}
        public void SetErrorMsgPage(string strMsg)
        {
            Session["CommMsg"] = strMsg;
            this.RedirectPage("wfmFalse.aspx");
        }
        public void RedirectPage(string strPage)
        {
            if (strPage == null && strPage.Trim().Length == 0)
            {
                this.SetErrorMsgPage("页面错误！");
            }
            else
            {
                Response.Redirect(strPage, false);
            }
        }
        public void SetErrorMsgPageBydir(string strMsg)
        {
            Session["CommMsg"] = strMsg;
            this.RedirectPage("../wfmFalse.aspx");
        }
        public void FillDropDownList(string showItem, DropDownList ddl, string filter, string strNewItem)
        {
            FillDropDownList(showItem, ddl, filter);
            ddl.Items.Insert(0, strNewItem);
        }
        public void FillDropDownList(string showItem, DropDownList ddl, string filter)
        {
            DataTable dt = (DataTable)Application[showItem];
            if (dt == null)
            {
                this.SetErrorMsgPageBydir("Application 中代码没有找到！");
                return;
            }
            DataView dv = new DataView(dt);
            dv.RowFilter = filter;
            //dv.Sort = "cnvcComments";
            ddl.DataSource = dv;
            ddl.DataValueField = "vcCommCode";
            ddl.DataTextField = "vcCommName";
            ddl.DataBind();
        }
        protected void BindRegion(string strRegion, DropDownList ddl)
        {
            if (strRegion == "全部")
            {
                //this.FillDropDownList("tbCommCode", ddl, "vcCommSign  like 'MD%' and vcCommCode<>'FYZX1'", "全部");
                this.BindCommCode(ddl, "vcCommSign='md' and vcCommCode not in ('MDXSB','MDCW1','FYZX1','CEN00')");
                ListItem li = new ListItem("所有", "%");
                ddl.Items.Add(li);
            }
            else
            {
                //this.FillDropDownList("tbCommCode", ddl, "vcCommSign='MD' and vcCommCode<>'FYZX1' and vcComments='门店" + strRegion + "'", "全部");
                this.BindCommCode(ddl, "vcCommSign='md' and vcCommCode not in ('MDXSB','MDCW1','FYZX1','CEN00') and vcComments like '门店|" + strRegion + "%'");
                ListItem li = new ListItem("所有", "%");
                ddl.Items.Add(li);
            }
        }
		public void BindBDept(DropDownList ddl)
		{
			BindCommCode(ddl,"vccommsign='BDEPT'");
		}
		public void BindBDept(DropDownList ddl,ListItem li)
		{
			BindCommCode(ddl,"vccommsign='BDEPT'");
			ddl.Items.Insert(0,li);
		}
		public void BindOper(DropDownList ddl,string strFilter)
		{
			DataTable dtOper = null;			
			if(Application["tbLogin"] != null)
			{
				dtOper = (DataTable) Application["tbLogin"];
			}
			else
			{
				dtOper = Helper.Query("select * from  tbLogin");
				Application["tbLogin"] = dtOper;
			}
			DataView dvOper = new DataView(dtOper);
			dvOper.RowFilter = strFilter;
			ddl.DataSource = dvOper;			
			ddl.DataValueField = "vcLoginID";
			ddl.DataTextField = "vcOperName";
			ddl.DataBind();
		}

		//DataTable里面的代码转换
		public void DataTableConvert(DataTable dt,string columnName,string strApplicationName,string strIDColumnName,string strCommentsColumnName,string filter)
		{
			if(dt == null)
			{
				Popup("无查询结果");
				return;
			}
			string strTemp ;			
			string strCommentColumnName = columnName+"Comments";
			//判断新列是否存在，已经存在就不添加，不存在就添加
			if(dt.Columns[strCommentColumnName]==null)
			{			
				dt.Columns.Add(strCommentColumnName,typeof(string));
			}
			foreach (DataRow dr in dt.Rows)
			{
				strTemp = this.CodeConvert(strApplicationName,strIDColumnName,dr[columnName].ToString(),strCommentsColumnName,filter);					
				dr[strCommentColumnName] = strTemp;					
			}
		}		

		public void DataTableConvert(DataTable dt,string columnName,string columnNameComment,string strApplicationName,string strIDColumnName,string strCommentsColumnName,string filter)
		{
			if(dt == null)
			{
				Popup("无查询结果");
				return;
			}
			string strTemp ;			
			//string strCommentColumnName = columnName+"Comments";
			//判断新列是否存在，已经存在就不添加，不存在就添加
			if(dt.Columns[columnNameComment]==null)
			{			
				dt.Columns.Add(columnNameComment,typeof(string));
			}
			foreach (DataRow dr in dt.Rows)
			{
				strTemp = this.CodeConvert(strApplicationName,strIDColumnName,dr[columnName].ToString(),strCommentsColumnName,filter);					
				dr[columnNameComment] = strTemp;					
			}
		}		

		//根据 selectId 返回tbCodeName  表中的 中文注释
		public string CodeConvert(string strApplicationName,string strIDColumnName,string selectId,string strCommentsColumnName,string filter)
		{
			this.FillApplication(strApplicationName);
			DataTable dt=(DataTable)Application[strApplicationName];			
			string strRemark ;
			if(dt==null)
			{
				throw new Exception("Application 中代码没有找到！");
			}			
			DataView dw = new DataView(dt);			
			string strfilter = "";
			if(filter == "")
			{
				strfilter = strIDColumnName+" = '"+selectId+"'"; 
			}
			else
			{
				strfilter = filter +" and "+strIDColumnName+" = '"+selectId+"'"; 
			}
			
			dw.RowFilter = strfilter;			
			if(dw.Count == 1)
			{
				strRemark = dw[0].Row[strCommentsColumnName].ToString();
			}
			else
			{
				strRemark = selectId;
			}
			return strRemark;				
		}
		private void FillApplication(string strApplicationName)
		{
			if(Application["tbDept"] == null)
			{
				Application["tbDept"] = Helper.Query("select * from tbDept");
			}
			if(Application["tbLogin"] == null)
			{
				Application["tbLogin"] = Helper.Query("select * from tbLogin");
			}
			if(Application["tbNameCode"] == null)
			{
				Application["tbNameCode"] = Helper.Query("select * from tbNameCode");
			}
			if(Application["tbProductClass"] == null)
			{
				Application["tbProductClass"] = Helper.Query("select * from tbProductClass");
			}
		}

		public void DataGridToExcel(DataGrid dg,string strFileName)
		{
			Response.Clear();
			//Response.AddHeader("content-disposition", "attachment;filename="+strFileName+".xls");
			Response.AddHeader("content-disposition", "attachment;filename="+System.Web.HttpUtility.UrlEncode(strFileName)+".xls");
			//Response.Charset = "";
			Response.Charset = "GB2312";
			Response.Cache.SetCacheability(HttpCacheability.NoCache);
			Response.ContentType = "application/vnd.xls";
			System.IO.StringWriter stringWrite = new System.IO.StringWriter();
			System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
			dg.RenderControl(htmlWrite);
			Response.Write(stringWrite.ToString());
			Response.End();
		}
		protected void wfmBase_Error(object sender, System.EventArgs e)
		{
			// 记录错误日志 
//			Exception errorLast = Server.GetLastError();
//
//			if (errorLast is ConcurrentException || errorLast.InnerException is ConcurrentException)
//			{
//				Server.ClearError();
//				Popup("其它用户修改或删除了当前信息，页面刷新获取了最新的数据！");
//				Server.Transfer(Request.Url.PathAndQuery);
//				return;
//			}
//			else if (errorLast is SqlException)
//			{
//				SqlException se = errorLast as SqlException;
//				if (SqlErrorCode.Duplicate_Key == se.Number)
//				{
//					Server.ClearError();
//					Popup("非常抱歉，将要创建的信息已存在！");
//					Server.Transfer(Request.Url.PathAndQuery);
//					return;
//				}
//			}
//
//			LogAdapter.WriteInterfaceException(errorLast);
//
//			Response.Redirect("../wfmError.aspx");

		}

		#region zhenghua@add 2010-04-14 导出EXCEL
		public string ExportTable(DataTable tb) 
		{ 
			StringBuilder sb = new StringBuilder(); 
			//data = ds.DataSetName + "\n"; 
			int count = 0; 

			//			foreach (DataTable tb in ds.Tables) 
			//			{ 
			//data += tb.TableName + "\n"; 
				
			sb.Append("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=gb2312\">"); 
			sb.Append("<table cellspacing=\"0\" cellpadding=\"5\" rules=\"all\" border=\"1\">"); 
			//写出标题
			sb.Append("<tr style=\"font-weight: bold; white-space: nowrap;\">"); 
			sb.Append("<td colspan=\""+tb.Columns.Count+"\" align=\"center\">" + tb.TableName + "</td>"); 
			sb.Append("</tr>"); 
			//写出列名 
			sb.Append("<tr style=\"font-weight: bold; white-space: nowrap;\">"); 
			foreach (DataColumn column in tb.Columns) 
			{ 
//				if(column.ColumnName.IndexOf("0#柴油")>0||
//					column.ColumnName.IndexOf("93#汽油")>0||
//					column.ColumnName.IndexOf("97#汽油")>0)
//				{
//					string strcn = column.ColumnName;
//					strcn = strcn.Replace("style='color:White;background-color:#000084;font-size:Small;font-weight:bold;'","style='font-weight: bold; white-space: nowrap;'");
//					strcn = strcn.Replace("<table>","<table cellspacing='0' cellpadding='5' rules='all' border='1'>");
//					sb.Append("<td colspan='3'>" + strcn + "</td>"); 
//				}
//				else if(column.ColumnName.IndexOf("汽油小计")>0)
//				{
//					string strcn = column.ColumnName;
//					strcn = strcn.Replace("style='color:White;background-color:#000084;font-size:Small;font-weight:bold;'","style='font-weight: bold; white-space: nowrap;'");
//					strcn = strcn.Replace("<table>","<table cellspacing='0' cellpadding='5' rules='all' border='1'>");
//					sb.Append("<td>" + strcn + "</td>"); 
//				}
//				else
//				{
					sb.Append("<td>" + column.ColumnName + "</td>"); 
				//}
			} 
			sb.Append("</tr>"); 

			//写出数据 
			foreach (DataRow row in tb.Rows) 
			{ 
				sb.Append("<tr>"); 
				foreach (DataColumn column in tb.Columns) 
				{ 
//					if (column.ColumnName.Equals("证件编号") || column.ColumnName.Equals("报名编号")) 
//						sb.Append("<td style=\"vnd.ms-excel.numberformat:@\">" + row[column].ToString() + "</td>"); 
//						////style="vnd.ms-excel.numberformat:@" 可以去除自动科学计数法的困扰 
//					else 
						sb.Append("<td>" + row[column].ToString() + "</td>"); 
				} 
				sb.Append("</tr>"); 
				count++; 
			} 
			sb.Append("</table>"); 
			//} 

			return sb.ToString(); 
		} 

		//		public void ExportDsToXls(Page page, string sql)
		//		{
		//			ExportDsToXls(page, "FileName", sql);
		//		}
		//		public void ExportDsToXls(Page page, string fileName, string sql)
		//		{
		//			DataSet ds = DBUtil.GetDataSet(sql);
		//			if (ds != null) ExportDsToXls(page, fileName, ds);
		//		}
		public void ExportToXls(Page page, string strfile)
		{
			ExportToXls(page, "FileName", strfile);
		}
		public void ExportToXls(Page page, string fileName, string strfile)
		{
			page.Response.Clear();
			page.Response.Buffer = true;
			page.Response.Charset = "GB2312";
//			page.Response.Charset = "UTF-8";
			page.Response.AppendHeader("Content-Disposition", "attachment;filename=" + fileName + System.DateTime.Now.ToString("_yyyyMMdd_hhmm") + ".xls");
			page.Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");//设置输出流为简体中文
			page.Response.ContentType = "application/ms-excel";//设置输出文件类型为excel文件。 
			page.EnableViewState = false;
			page.Response.Write(strfile);
			page.Response.End();
		}
		#endregion
	}
}
