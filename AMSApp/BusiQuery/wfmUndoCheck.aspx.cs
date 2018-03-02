using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CommCenter;
using System.Data;
using System.Collections;

namespace AMSApp.BusiQuery
{
    public partial class wfmUndoCheck : wfmBase
    {
        BusiComm.BusiQuery busiq;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Login"] != null)
            {
                CMSMStruct.LoginStruct ls1 = (CMSMStruct.LoginStruct)Session["Login"];
                if (!IsPostBack)
                {
                    this.FillDropDownList("tbCommCode", ddlDept, "vcCommSign ='MD' and vcCommCode<>'FYZX1'", "全部");
                    this.FillDropDownList("AllREGION", ddlRegion, "", "全部");
                    if (ls1.strLimit != "CL001")
                    {
                        ddlDept.Items.FindByValue(ls1.strDeptID).Selected = true;
                        ddlDept.Enabled = false;
                        ddlRegion.Enabled = false;
                    }
                    this.txtBegin.Text = DateTime.Now.ToString("yyyy-MM-dd");
                    this.txtEnd.Text = this.txtBegin.Text;
                    string strDept = ddlDept.SelectedValue;
                    if (strDept != "全部")
                    {
                        this.FillDropDownList("tbLocalLogin", ddlOper, "vcDeptId='" + strDept + "'", "全部");
                    }
                    else
                    {
                        this.FillDropDownList("tbLocalLogin", ddlOper, "", "全部");
                    }

                    Session.Remove("UndoCheck");
                }
            }
            else
            {
                Response.Redirect("../Exit.aspx");
            }
        }
        private void BindGridView()
        {
            DataTable dtOut = (DataTable)Session["UndoCheck"];
            if (dtOut != null)
            {
                this.GridView1.DataSource = dtOut;
                this.GridView1.DataBind();
            }
            else
            {
                btQuery_Click(null, null);
            }
        }
        protected void btQuery_Click(object sender, EventArgs e)
        {
            if (this.txtBegin.Text == "" || this.txtEnd.Text == "" )
            {
                this.SetErrorMsgPageBydir("时间不能为空，请重新选择时间！");
                return;
            }

            string strBeginDate = Convert.ToDateTime(this.txtBegin.Text).ToString("yyyy-MM-dd");
            string strEndDate = Convert.ToDateTime(this.txtEnd.Text).AddDays(1).ToString("yyyy-MM-dd");
            

            Hashtable htapp = (Hashtable)Application["appconf"];
            string strcons = (string)htapp["cons"];
            busiq = new BusiComm.BusiQuery(strcons);

            string strSerial = txtSerial.Text.Trim();
            string strOperName = ddlOper.SelectedValue;
            string strDeptID = ddlDept.SelectedValue;
            string strFlag = this.DropDownList1.SelectedValue;
            try
            {
                DataTable dtout = busiq.ConsItemUndoQuery(strSerial, strBeginDate, strEndDate, strOperName, strDeptID, "是",strFlag);
                if (dtout == null)
                {
                    this.SetErrorMsgPageBydir("查询出错，请重试！");
                    return;
                }
                else
                {
                    this.GridView1.DataSource = dtout;
                    this.GridView1.DataBind();
                    Session["UndoCheck"] = dtout;
                }
            }
            catch (Exception er)
            {
                this.clog.WriteLine(er);
                this.SetErrorMsgPageBydir("查询错误，请重试！");
                return;
            }
        }

        protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            this.GridView1.EditIndex = e.NewEditIndex;
            BindGridView();
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            this.GridView1.EditIndex = -1;
            BindGridView();
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string strSerial = this.GridView1.Rows[e.RowIndex].Cells[0].Text;
            string strDeptId = this.GridView1.Rows[e.RowIndex].Cells[11].Text;
            string strFlag = this.GridView1.Rows[e.RowIndex].Cells[8].Text;
            if (strFlag != "正常消费")
            {
                this.Popup("撤销已审核");
                return;
            }
            Hashtable htapp = (Hashtable)Application["appconf"];
            string strcons = (string)htapp["cons"];
            busiq = new BusiComm.BusiQuery(strcons);
            CMSMStruct.LoginStruct ls1 = (CMSMStruct.LoginStruct)Session["Login"];
            try
            {
                busiq.CheckConsItemUndo(strSerial, strDeptId, ls1.strLoginID);
                this.btQuery_Click(null, null);
            }
            catch (Exception er)
            {
                this.clog.WriteLine(er);
                this.SetErrorMsgPageBydir("审核出错，请重试！");
                return;
            }
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.GridView1.PageIndex = e.NewPageIndex;
            BindGridView();
        }

        protected void ddlRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.BindRegion(this.ddlRegion.SelectedValue, this.ddlDept);
        }

        int iCount = 0;
        double dFee = 0;
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRow dr = ((DataRowView)e.Row.DataItem).Row;
                iCount += Convert.ToInt32(dr["iCount"]);
                dFee += Convert.ToDouble(dr["nFee"]);
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[4].Text = iCount.ToString();
                e.Row.Cells[6].Text = dFee.ToString("F2");
            }
        }
    }
}