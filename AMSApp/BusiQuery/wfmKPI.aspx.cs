using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Data;

namespace AMSApp.BusiQuery
{
    public partial class wfmKPI : wfmBase
    {
        BusiComm.BusiQuery busiq;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Login"] != null)
            {
                if (!IsPostBack)
                {
                    this.FillDropDownList("tbCommCode", ddlDeptId, "vcCommSign ='MD' and vcCommCode<>'FYZX1'", "");
                    MonthList(this.ddlMonth,true);
                }
            }
            else
            {
                Response.Redirect("../Exit.aspx");
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

            //查询
            Hashtable htapp = (Hashtable)Application["appconf"];
            string strcons = (string)htapp["cons"];
            busiq = new BusiComm.BusiQuery(strcons);

            DataTable dtout = busiq.GetKPIOfMonth(ddlMonth.SelectedValue, ddlDeptId.SelectedValue);
            this.DataTableConvert(dtout, "vcDeptId", "tbCommCode", "vcCommSign='MD'");
            this.GridView1.DataSource = dtout;
            this.GridView1.DataBind();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ddlMonth.SelectedValue))
            {
                this.Popup("请选择月份");
                return;
            }
            if (string.IsNullOrEmpty(ddlDeptId.SelectedValue))
            {
                this.Popup("请选择门店");
                return;
            }
            if (string.IsNullOrEmpty(txtAmount.Text))
            {
                this.Popup("请输入指标");
                return;
            }
            if (!this.JudgeIsNum(txtAmount.Text))
            {
                this.Popup("指标请输入数字");
                return;
            }
            //添加
            string month = ddlMonth.SelectedValue;
            string deptId = ddlDeptId.SelectedValue;
            decimal amount = Convert.ToDecimal(txtAmount.Text);

            Hashtable htapp = (Hashtable)Application["appconf"];
            string strcons = (string)htapp["cons"];
            busiq = new BusiComm.BusiQuery(strcons);
            if (busiq.ExistKPI(month, deptId))
            {
                this.Popup("指标已存在，请重新选择！");
                return;
            }
            if (!busiq.InsertKPI(month,deptId,amount))
            {
                this.Popup("添加指标失败，请重试！");
            }
            else
            {
                Button1_Click(null, null);
            }
        }        


        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            this.GridView1.EditIndex = e.NewEditIndex;
            Button1_Click(null, null);
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string month = GridView1.Rows[e.RowIndex].Cells[2].Text;
            string deptId = GridView1.Rows[e.RowIndex].Cells[3].Text;

            Hashtable htapp = (Hashtable)Application["appconf"];
            string strcons = (string)htapp["cons"];
            busiq = new BusiComm.BusiQuery(strcons);

            if (!busiq.DeleteKPI(month, deptId))
            {
                this.Popup("删除指标失败，请重试！");
            }
            else
            {
                Button1_Click(null, null);
            }
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            Button1_Click(null, null);
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string month = GridView1.Rows[e.RowIndex].Cells[2].Text;
            string deptId = GridView1.Rows[e.RowIndex].Cells[3].Text;
            string strAmount = (GridView1.Rows[e.RowIndex].Cells[5].Controls[0] as TextBox).Text;

            if (string.IsNullOrEmpty(strAmount))
            {
                this.Popup("请输入指标");
                return;
            }
            if (!this.JudgeIsNum(strAmount))
            {
                this.Popup("指标请输入数字");
                return;
            }

            decimal amount = Convert.ToDecimal(strAmount);

            Hashtable htapp = (Hashtable)Application["appconf"];
            string strcons = (string)htapp["cons"];
            busiq = new BusiComm.BusiQuery(strcons);

            if (!busiq.UpdateKPI(month, deptId, amount))
            {
                this.Popup("编辑指标失败，请重试！");
            }
            else
            {
                GridView1.EditIndex = -1;
                Button1_Click(null, null);
            }
        }


        protected void Button3_Click(object sender, EventArgs e)
        {
            this.RedirectPage("wfmPerformanceOfDay.aspx");
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            Button1_Click(null, null);
        }
    }
}