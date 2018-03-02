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
namespace AMSApp.Pop
{
	/// <summary>
	/// Summary description for notice.
	/// </summary>
	public partial class notice : System.Web.UI.Page
	{
		public string strComments = "";
		public string strReleaseDate = "";
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			
			DataTable dtNotice = Helper.Query("select cnnNoticeID,cnvcComments,Convert(varchar(10),cndReleaseDate,21) as cndReleaseDate from tbNotice where cnvcIsActive ='1' and convert(char(10),cndInvalidDate,121) >=convert(char(10),getdate(),121)");//(DataTable)Session["tbNotice"];
			if(	dtNotice != null && dtNotice.Rows.Count >0)
			{
				DataRow drTemp = dtNotice.Rows[0];
				//TableNotice tbNotice = new TableNotice();
				strComments = drTemp["cnvcComments"].ToString();
				strReleaseDate = drTemp["cndReleaseDate"].ToString();
			}
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    

		}
		#endregion
	}
}
