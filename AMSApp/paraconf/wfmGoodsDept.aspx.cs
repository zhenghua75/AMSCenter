using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusiComm;
using System.Collections;
using System.Data;

namespace AMSApp.paraconf
{
    public partial class wfmGoodsDept : wfmBase
    {
        Manager m1;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (Request["vcGoodsId"] != "")
                {
                    string strGoodsId = Request["vcGoodsId"];
                    string strGoodsName = Request["vcGoodsName"];
                    string strPrice = Request["nPrice"];

                    this.lblGoodsId.Text = strGoodsId;
                    this.lblGoodsName.Text = strGoodsName;
                    this.lblPrice.Text = strPrice;

                    Hashtable htapp = (Hashtable)Application["appconf"];
                    string strcons = (string)htapp["cons"];
                    m1 = new Manager(strcons);

                    BindGridView(strGoodsId, strGoodsName, strPrice);
                }
            }
        }
        private void BindGridView(string strGoodsId,string strGoodsName,string strPrice)
        {            
            try
            {
                DataTable dtout = m1.GetGoodsDept(strGoodsId, strGoodsName, strPrice);
                if (dtout == null)
                {
                    this.SetErrorMsgPageBydir("查询出错，请重试！");
                    return;
                }

                this.GridView1.DataSource = dtout;
                this.GridView1.DataBind();
            }
            catch (Exception er)
            {
                this.clog.WriteLine(er);
                this.SetErrorMsgPageBydir("查询错误，请重试！");
                return;
            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            this.RedirectPage("wfmGoods.aspx");
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            Hashtable htapp = (Hashtable)Application["appconf"];
            string strcons = (string)htapp["cons"];
            m1 = new Manager(strcons);
            foreach (GridViewRow gvr in this.GridView1.Rows)
            {
                CheckBox cb = (CheckBox)gvr.FindControl("CheckBox1");
                string strDeptId = gvr.Cells[1].Text;
                string strGoodsId = gvr.Cells[3].Text;
                string strPrice = gvr.Cells[5].Text;
                if (cb.Checked)
                {
                    m1.AddGoodsDept(strDeptId, strGoodsId, strPrice);
                }
                else
                {
                    m1.DeleteGoodsDept(strDeptId, strGoodsId);
                }
            }
            this.Popup("设置成功");
            BindGridView(this.lblGoodsId.Text, this.lblGoodsName.Text, this.lblPrice.Text);
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {


            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                CheckBox headerchk = (CheckBox)this.GridView1.HeaderRow.FindControl("chkAll");

                CheckBox childchk = (CheckBox)e.Row.FindControl("CheckBox1");

                childchk.Attributes.Add("onclick", "javascript:Selectchildcheckboxes('" + headerchk.ClientID + "')");

            }
        }
    }
}