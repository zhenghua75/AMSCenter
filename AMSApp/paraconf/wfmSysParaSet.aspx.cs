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
using BusiComm;
using CommCenter;

namespace AMSApp.paraconf
{
	public partial class wfmSysParaSet : wfmBase
	{
		protected System.Web.UI.HtmlControls.HtmlForm Form3;

		Manager m1;
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if(Session["Login"]!=null)
			{
//				CMSMStruct.LoginStruct ls1=(CMSMStruct.LoginStruct)Session["Login"];
//				if(ls1.strLimit!="CL001")
//				{
//					this.SetErrorMsgPageBydir("对不起，你没有权限使用此功能！");
//					return;
//				}
				if (!IsPostBack )
				{
					Hashtable htapp=(Hashtable)Application["appconf"];
					string strcons=(string)htapp["cons"];
					m1=new Manager(strcons);

					DataTable dtgoods=m1.GetAllGoodsName();
					for(int i=0;i<dtgoods.Rows.Count;i++)
					{
						lbtcurrent.Items.Add(dtgoods.Rows[i]["vcGoodsName"].ToString());
						if(dtgoods.Rows[i]["cNewFlag"].ToString()=="1")
						{
							lbtNew.Items.Add(dtgoods.Rows[i]["vcGoodsName"].ToString());
						}	
					}

					DataTable dtig=m1.GetIgPara();
					if(dtig.Rows.Count>0)
					{
						txtFee.Text=dtig.Rows[0]["vcCommName"].ToString();
						txtIg.Text=dtig.Rows[0]["vcCommCode"].ToString();
					}
					else
					{
						txtFee.Text="";
						txtIg.Text="";
					}

					Hashtable htprom=m1.GetPromRate();
					if(htprom.ContainsKey("FP1"))
					{
						txtPromRate1.Text=htprom["FP1"].ToString();
					}
					else
					{
						txtPromRate1.Text="0";
					}
					if(htprom.ContainsKey("FP2"))
					{
						txtPromRate2.Text=htprom["FP2"].ToString();
					}
					else
					{
						txtPromRate2.Text="0";
					}

					if(htprom.ContainsKey("FP3"))
					{
						txtPromRate3.Text=htprom["FP3"].ToString();
					}
					else
					{
						txtPromRate3.Text="0";
					}

                    //新的赠款比例
                    this.Query();
				}
			}
			else
			{
				Response.Redirect("../Exit.aspx");
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
			if(lbtcurrent.Items.Count==0)
			{
				this.SetErrorMsgPageBydir("没有商品信息，请先录入商品信息！");
				return;
			}
			if(lbtNew.Items.Count>=10)
			{
				this.SetErrorMsgPageBydir("推荐新品只能设置10个！");
				return;
			}
			for(int i=0;i<lbtNew.Items.Count;i++)
			{
				if(lbtcurrent.SelectedItem.Text==lbtNew.Items[i].ToString())
				{
					return;
				}
			}
			if(lbtcurrent.SelectedItem==null)
			{
				this.SetErrorMsgPageBydir("请确定选中某个商品！");
				return;
			}
			else
			{
				lbtNew.Items.Add(lbtcurrent.SelectedItem.Text);
			}
		}

		protected void btDel_Click(object sender, System.EventArgs e)
		{
			if(lbtNew.Items.Count==0)
			{
				this.SetErrorMsgPageBydir("目前还没有要推荐的商品！");
				return;
			}
			lbtNew.Items.Remove(lbtNew.SelectedItem);
		}

		protected void btNewGoods_Click(object sender, System.EventArgs e)
		{
			ArrayList al=new ArrayList();
			for(int i=0;i<lbtNew.Items.Count;i++)
			{
				al.Add(lbtNew.Items[i].Text.Trim());
			}
			Hashtable htapp=(Hashtable)Application["appconf"];
			string strcons=(string)htapp["cons"];
			m1=new Manager(strcons);

			if(m1.UpdateGoodsNewFlag(al))
			{
				this.SetSuccMsgPageBydir("推荐新品设置成功！","paraconf/wfmSysParaSet.aspx");
				return;
			}
			else
			{
				this.SetErrorMsgPageBydir("推荐新品设置失败！");
				return;
			}
		}

		protected void btIg_Click(object sender, System.EventArgs e)
		{
			CMSMStruct.CommStruct cos=new CMSMStruct.CommStruct();
			if(txtFee.Text.Trim()=="")
			{
				cos.strCommName="0";
			}
			else if(double.Parse(txtFee.Text.Trim())<0)
			{
				this.SetErrorMsgPageBydir("消费金额不能小于0！");
				return;
			}
			else
			{
				cos.strCommName=txtFee.Text.Trim();
			}

			if(txtIg.Text.Trim()=="")
			{
				cos.strCommCode="0";
			}
			else if(double.Parse(txtIg.Text.Trim())<0)
			{
				this.SetErrorMsgPageBydir("赠送积分分值不能小于0！");
				return;
			}
			else
			{
				cos.strCommCode=txtIg.Text.Trim();
			}
			cos.strCommSign="IG";
			cos.strComments="积分赠送";

			Hashtable htapp=(Hashtable)Application["appconf"];
			string strcons=(string)htapp["cons"];
			m1=new Manager(strcons);

			if(m1.UpdateIgComm(cos))
			{
				this.SetSuccMsgPageBydir("消费积分设置成功！","paraconf/wfmSysParaSet.aspx");
				return;
			}
			else
			{
				this.SetErrorMsgPageBydir("消费积分设置失败！");
				return;
			}
		}

		protected void btProm_Click(object sender, System.EventArgs e)
		{
			CMSMStruct.CommStruct cos=new CMSMStruct.CommStruct();
			Hashtable htpar=new Hashtable();
			if(txtPromRate1.Text.Trim()=="")
			{
				cos.strCommCode="0";
			}
			else if(double.Parse(txtPromRate1.Text.Trim())<0)
			{
				this.SetErrorMsgPageBydir("充值赠款比例不能小于0！");
				return;
			}
			else
			{
				cos.strCommCode=txtPromRate1.Text.Trim();
			}
			cos.strCommName="充值赠款100-500";
			cos.strCommSign="FP1";
			cos.strComments="充值赠款";
			htpar.Add("FP1",cos);

			CMSMStruct.CommStruct cos2=new CMSMStruct.CommStruct();
			if(txtPromRate2.Text.Trim()=="")
			{
				cos2.strCommCode="0";
			}
			else if(double.Parse(txtPromRate2.Text.Trim())<0)
			{
				this.SetErrorMsgPageBydir("充值赠款比例不能小于0！");
				return;
			}
			else
			{
				cos2.strCommCode=txtPromRate2.Text.Trim();
			}
			cos2.strCommName="充值赠款500-1000";
			cos2.strCommSign="FP2";
			cos2.strComments="充值赠款";
			htpar.Add("FP2",cos2);

			CMSMStruct.CommStruct cos3=new CMSMStruct.CommStruct();
			if(txtPromRate3.Text.Trim()=="")
			{
				cos3.strCommCode="0";
			}
			else if(double.Parse(txtPromRate3.Text.Trim())<0)
			{
				this.SetErrorMsgPageBydir("充值赠款比例不能小于0！");
				return;
			}
			else
			{
				cos3.strCommCode=txtPromRate3.Text.Trim();
			}
			cos3.strCommName="充值赠款1000以上";
			cos3.strCommSign="FP3";
			cos3.strComments="充值赠款";
			htpar.Add("FP3",cos3);

			Hashtable htapp=(Hashtable)Application["appconf"];
			string strcons=(string)htapp["cons"];
			m1=new Manager(strcons);

			if(m1.UpdateFillPromComm(htpar))
			{
				this.SetSuccMsgPageBydir("消费积分设置成功！","paraconf/wfmSysParaSet.aspx");
				return;
			}
			else
			{
				this.SetErrorMsgPageBydir("消费积分设置失败！");
				return;
			}
		}

        private void Query()
        {
            Hashtable htapp = (Hashtable)Application["appconf"];
            string strcons = (string)htapp["cons"];
            m1 = new Manager(strcons);
            try
            {
                DataTable dtout = m1.GetPromRatio();
                if (dtout == null)
                {
                    this.SetErrorMsgPageBydir("查询出错，请重试！");
                    return;
                }
                else
                {
                    this.GridView1.DataSource = dtout;
                    this.GridView1.DataBind();
                    Session["PromRatio"] = dtout;
                }
            }
            catch (Exception er)
            {
                this.clog.WriteLine(er);
                this.SetErrorMsgPageBydir("查询错误，请重试！");
                return;
            }
        }

        private void BindGridView()
        {
            DataTable dtOut = (DataTable)Session["PromRatio"];
            if (dtOut != null)
            {
                this.GridView1.DataSource = dtOut;
                this.GridView1.DataBind();
            }
            else
            {
                Query();
            }
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.GridView1.PageIndex = e.NewPageIndex;
            BindGridView();
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            this.GridView1.EditIndex = -1;
            BindGridView();
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string strOldCommName = ((Label)this.GridView1.Rows[e.RowIndex].Cells[3].Controls[1]).Text;
            string strOldCommCode = ((Label)this.GridView1.Rows[e.RowIndex].Cells[4].Controls[1]).Text;
            Hashtable htapp = (Hashtable)Application["appconf"];
            string strcons = (string)htapp["cons"];
            m1 = new Manager(strcons);
            try
            {
                bool success = m1.DeletePromRatio(strOldCommName, strOldCommCode);
                if (!success)
                {
                    this.SetErrorMsgPageBydir("充值赠款规则删除出错，请重试！");
                    return;
                }
                else
                {
                    this.Query();
                }
            }
            catch (Exception er)
            {
                this.clog.WriteLine(er);
                this.SetErrorMsgPageBydir("充值赠款规则删除错误，请重试！");
                return;
            }
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            this.GridView1.EditIndex = e.NewEditIndex;
            BindGridView();
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string strCommName1 = ((TextBox)this.GridView1.Rows[e.RowIndex].Cells[0].Controls[1]).Text;
            string strCommName2 = ((TextBox)this.GridView1.Rows[e.RowIndex].Cells[1].Controls[1]).Text;
            string strCommName = "";
            if (string.IsNullOrEmpty(strCommName2))
            {
                strCommName = strCommName1;
            }
            else
            {
                strCommName = strCommName1 + "-" + strCommName2;
            }
            string strCommCode = ((TextBox)this.GridView1.Rows[e.RowIndex].Cells[2].Controls[1]).Text;
            
            string strOldCommName = ((Label)this.GridView1.Rows[e.RowIndex].Cells[3].Controls[1]).Text;
            string strOldCommCode = ((Label)this.GridView1.Rows[e.RowIndex].Cells[4].Controls[1]).Text;

            Hashtable htapp = (Hashtable)Application["appconf"];
            string strcons = (string)htapp["cons"];
            m1 = new Manager(strcons);
            CMSMStruct.LoginStruct ls1 = (CMSMStruct.LoginStruct)Session["Login"];
            try
            {
                bool exist = m1.ExistPromRatio(strCommName);
                if (strCommName != strOldCommName && exist)
                {
                    this.Popup("充值赠款规则已存在");
                    return;
                }
                bool success = m1.UpdatePromRatio(strOldCommName, strCommName, strOldCommCode, strCommCode);
                if (!success)
                {
                    this.SetErrorMsgPageBydir("充值赠款规则修改出错，请重试！");
                    return;
                }
                else
                {
                    this.GridView1.EditIndex = -1;
                    this.Query();
                }
            }
            catch (Exception er)
            {
                this.clog.WriteLine(er);
                this.SetErrorMsgPageBydir("充值赠款规则修改错误，请重试！");
                return;
            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string strCommName = "";
            string strCommName1 = "";
            string strCommName2 = "";
            string strCommCode = "";
            
            if (e.CommandName == "EmptyInsert")
            {
                strCommName1 = ((TextBox)GridView1.Controls[0].Controls[0].Controls[0].FindControl("txtBeginValue")).Text;
                strCommName2 = ((TextBox)GridView1.Controls[0].Controls[0].Controls[0].FindControl("txtEndValue")).Text;
                strCommCode = ((TextBox)GridView1.Controls[0].Controls[0].Controls[0].FindControl("txtPromRatio")).Text;                
            }
            else if (e.CommandName == "Insert")
            {
                strCommName1 = ((TextBox)GridView1.FooterRow.Cells[0].Controls[1]).Text;
                strCommName2 = ((TextBox)GridView1.FooterRow.Cells[1].Controls[1]).Text;
                strCommCode = ((TextBox)GridView1.FooterRow.Cells[2].Controls[1]).Text;                
            }
            else
            {
                return;
            }
            if (string.IsNullOrEmpty(strCommName2))
            {
                strCommName = strCommName1;
            }
            else
            {
                strCommName = strCommName1 + "-" + strCommName2;
            }
            if (string.IsNullOrEmpty(strCommName)||string.IsNullOrEmpty(strCommCode))
            {
                this.Popup("充值赠款规则不能为空");
                return;
            }
            Hashtable htapp = (Hashtable)Application["appconf"];
            string strcons = (string)htapp["cons"];
            m1 = new Manager(strcons);
            CMSMStruct.LoginStruct ls1 = (CMSMStruct.LoginStruct)Session["Login"];
            try
            {
                bool exist = m1.ExistPromRatio(strCommName);
                if (exist)
                {
                    this.Popup("充值赠款规则已存在");
                    return;
                }
                bool success = m1.InsertPromRatio(strCommName, strCommCode);
                if (!success)
                {
                    this.SetErrorMsgPageBydir("充值赠款规则添加出错，请重试！");
                    return;
                }
                else
                {
                    this.GridView1.EditIndex = -1;
                    this.Query();
                }
            }
            catch (Exception er)
            {
                this.clog.WriteLine(er);
                this.SetErrorMsgPageBydir("充值赠款规则添加错误，请重试！");
                return;
            }
        }
	}
}
