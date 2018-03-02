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
using CommCenter;

namespace AMSApp.Storage
{
	/// <summary>
	/// Summary description for wfmProviderDetail.
	/// </summary>
	public partial class wfmProviderDetail : wfmBase
	{

		BusiComm.StorageBusi StoBusi;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			if(Session["Login"]==null)
			{
				Response.Redirect("../Exit.aspx");
				return;
			}

			string strPVid=Request.QueryString["PVID"];
			string strPDid=Request.QueryString["PDID"];
			Hashtable htapp=(Hashtable)Application["appconf"];
			string strcons=(string)htapp["cons"];
			StoBusi=new BusiComm.StorageBusi(strcons);

			if(!IsPostBack)
			{
				this.txtProviderName.Enabled=false;
				if(strPVid==""||strPVid==null||strPDid==""||strPDid==null)
				{
					this.btAdd.Enabled=true;
					this.btMod.Enabled=false;
					lbltitle.Text="新供应商资料录入";
					this.FillDropDownList("AllMaterial",this.ddlProduct);
					this.ddlProviderTime.Items.Add(new ListItem("及时","0"));
					this.ddlProviderTime.Items.Add(new ListItem("一般","1"));
					this.ddlProviderTime.Items.Add(new ListItem("不及时","2"));
					this.ddlProviderTime.SelectedIndex=0;
					this.ddlProviderQuality.Items.Add(new ListItem("好","0"));
					this.ddlProviderQuality.Items.Add(new ListItem("一般","1"));
					this.ddlProviderQuality.Items.Add(new ListItem("不好","2"));
					this.ddlProviderQuality.SelectedIndex=0;
				}
				else
				{
					this.btnFind.Enabled=false;
					this.btAdd.Enabled=false;
					this.btMod.Enabled=true;
					this.FillDropDownList("AllMaterial",this.ddlProduct);
					this.ddlProviderTime.Items.Add(new ListItem("及时","0"));
					this.ddlProviderTime.Items.Add(new ListItem("一般","1"));
					this.ddlProviderTime.Items.Add(new ListItem("不及时","2"));
					this.ddlProviderTime.SelectedIndex=0;
					this.ddlProviderQuality.Items.Add(new ListItem("好","0"));
					this.ddlProviderQuality.Items.Add(new ListItem("一般","1"));
					this.ddlProviderQuality.Items.Add(new ListItem("不好","2"));
					this.ddlProviderQuality.SelectedIndex=0;

					CMSMStruct.ProviderStruct ps1=StoBusi.GetProviderDetailOne(strPVid,strPDid);
					this.txtProviderCode.Text=ps1.strProviderCode;
					this.txtProviderName.Text=ps1.strProviderName;
					this.ddlProduct.SelectedIndex=this.ddlProduct.Items.IndexOf(this.ddlProduct.Items.FindByText(ps1.strProductName));
					this.txtProviderPrice.Text=ps1.strProviderPrice;
					this.txtProviderUnit.Text=ps1.strProviderUnit;
					this.ddlProviderTime.SelectedIndex=this.ddlProviderTime.Items.IndexOf(this.ddlProviderTime.Items.FindByText(ps1.strProviderTime));
					this.ddlProviderQuality.SelectedIndex=this.ddlProviderQuality.Items.IndexOf(this.ddlProviderQuality.Items.FindByText(ps1.strProviderQuality));
					this.txtProviderValue.Text=ps1.strProviderValue;
					this.txtLinkName.Text=ps1.strLinkName;
					this.txtLinkPhone.Text=ps1.strLinkPhone;
					this.txtLinkAddress.Text=ps1.strLinkAddress;
					lbltitle.Text="供应商资料修改删除";
					Session["psold"]=ps1;
					this.txtProviderCode.Enabled=false;
					this.ddlProduct.Enabled=false;
				}
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

		protected void btAdd_Click(object sender, System.EventArgs e)
		{
			CMSMStruct.ProviderStruct ps1=new CMSMStruct.ProviderStruct();
			ps1.strProviderCode=this.txtProviderCode.Text.Trim();
			ps1.strProviderName=this.txtProviderName.Text.Trim();
			ps1.strProductCode=this.ddlProduct.SelectedValue;
			ps1.strProductName=this.ddlProduct.SelectedItem.Text;
			ps1.strProviderPrice=this.txtProviderPrice.Text.Trim();
			ps1.strProviderUnit=this.txtProviderUnit.Text.Trim();
			ps1.strProviderTime=this.ddlProviderTime.SelectedItem.Text;
			ps1.strProviderQuality=this.ddlProviderQuality.SelectedItem.Text;
			ps1.strProviderValue=this.txtProviderValue.Text.Trim();
			ps1.strLinkName=this.txtLinkName.Text.Trim();
			ps1.strLinkPhone=this.txtLinkPhone.Text.Trim();
			ps1.strLinkAddress=this.txtLinkAddress.Text.Trim();

			if(ps1.strProviderCode==""||ps1.strProviderCode.Length>=10)
			{
				this.SetErrorMsgPageBydirHistory("供应商编码不能为空或长度过长！");
				return;
			}
			if(ps1.strProviderName==""||ps1.strProviderName.Length>20)
			{
				this.SetErrorMsgPageBydirHistory("供应商名称不能为空或长度过长！");
				return;
			}
			if(ps1.strProviderCode=="")
			{
				this.SetErrorMsgPageBydirHistory("供应产品不能为空！");
				return;
			}
			if(ps1.strProviderPrice==""||ps1.strProviderUnit=="")
			{
				this.SetErrorMsgPageBydirHistory("供应产品单价和单位不能为空！");
				return;
			}

			Hashtable htapp=(Hashtable)Application["appconf"];
			string strcons=(string)htapp["cons"];
			StoBusi=new BusiComm.StorageBusi(strcons);
			try
			{
				string strProviderName=StoBusi.IsExistProvider(ps1.strProviderCode);
				if(strProviderName!=""&&strProviderName!=ps1.strProviderName)
				{
					this.SetErrorMsgPageBydirHistory("供应商编码和名称发生冲突，该编码对应的供应商名称为："+strProviderName);
					return;
				}
				else if(StoBusi.IsExistProviderProduct(ps1.strProviderCode,ps1.strProductCode))
				{
					this.SetErrorMsgPageBydirHistory("该供应商供应的产品资料已经存在！");
					return;
				}
				else
				{
					if(StoBusi.NewProviderAdd(ps1))
					{
						this.SetSuccMsgPageBydir("新增供应商资料成功！","Storage/wfmProviderDetail.aspx");
						return;
					}
					else
					{
						this.SetErrorMsgPageBydir("新增供应商资料时发生错误，请重试！");
						return;
					}
				}
			}
			catch(Exception er)
			{
				this.clog.WriteLine(er);
				this.SetErrorMsgPageBydir("查询错误，请重试！");
				return;
			}
		}

		protected void btMod_Click(object sender, System.EventArgs e)
		{
			CMSMStruct.ProviderStruct ps1=new CMSMStruct.ProviderStruct();
			ps1.strProviderCode=this.txtProviderCode.Text.Trim();
			ps1.strProviderName=this.txtProviderName.Text.Trim();
			ps1.strProductCode=this.ddlProduct.SelectedValue;
			ps1.strProductName=this.ddlProduct.SelectedItem.Text;
			ps1.strProviderPrice=this.txtProviderPrice.Text.Trim();
			ps1.strProviderUnit=this.txtProviderUnit.Text.Trim();
			ps1.strProviderTime=this.ddlProviderTime.SelectedItem.Text;
			ps1.strProviderQuality=this.ddlProviderQuality.SelectedItem.Text;
			ps1.strProviderValue=this.txtProviderValue.Text.Trim();
			ps1.strLinkName=this.txtLinkName.Text.Trim();
			ps1.strLinkPhone=this.txtLinkPhone.Text.Trim();
			ps1.strLinkAddress=this.txtLinkAddress.Text.Trim();

			if(ps1.strProviderCode==""||ps1.strProviderCode.Length>=10)
			{
				this.SetErrorMsgPageBydirHistory("供应商编码不能为空或长度过长！");
				return;
			}
			if(ps1.strProviderName==""||ps1.strProviderName.Length>20)
			{
				this.SetErrorMsgPageBydirHistory("供应商名称不能为空或长度过长！");
				return;
			}
			if(ps1.strProviderCode=="")
			{
				this.SetErrorMsgPageBydirHistory("供应产品不能为空！");
				return;
			}
			if(ps1.strProviderPrice==""||ps1.strProviderUnit=="")
			{
				this.SetErrorMsgPageBydirHistory("供应产品单价和单位不能为空！");
				return;
			}

			CMSMStruct.ProviderStruct psold=(CMSMStruct.ProviderStruct)Session["psold"];
			Hashtable htapp=(Hashtable)Application["appconf"];
			string strcons=(string)htapp["cons"];
			StoBusi=new BusiComm.StorageBusi(strcons);
			try
			{
				if(StoBusi.ModProviderInfo(ps1,psold))
				{
					this.SetSuccMsgPageBydir("修改供应商资料成功！","Storage/wfmProvider.aspx");
					return;
				}
				else
				{
					this.SetErrorMsgPageBydir("修改供应商资料时发生错误，请重试！");
					return;
				}
			}
			catch(Exception er)
			{
				this.clog.WriteLine(er);
				this.SetErrorMsgPageBydir("查询错误，请重试！");
				return;
			}
		}

		protected void btcancel_Click(object sender, System.EventArgs e)
		{
			this.RedirectPage("wfmProvider.aspx");
		}

		protected void btnFind_Click(object sender, System.EventArgs e)
		{
			string strProviderCode=this.txtProviderCode.Text.Trim();
			if(strProviderCode=="")
			{
				this.SetErrorMsgPageBydirHistory("请输入供应商编码！");
				return;
			}
			Hashtable htapp=(Hashtable)Application["appconf"];
			string strcons=(string)htapp["cons"];
			StoBusi=new BusiComm.StorageBusi(strcons);
			try
			{
				string strProviderName=StoBusi.IsExistProvider(strProviderCode);
				if(strProviderName=="")
				{
					this.txtProviderName.Text="";
					this.txtProviderName.Enabled=true;
				}
				else
				{
					this.txtProviderName.Text=strProviderName;
					this.txtProviderName.Enabled=false;
				}
			}
			catch(Exception er)
			{
				this.clog.WriteLine(er);
				this.SetErrorMsgPageBydir("查询错误，请重试！");
				return;
			}
		}
	}
}
