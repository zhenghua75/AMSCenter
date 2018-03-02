using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CommCenter;
using System.Data;
using System.Collections;

namespace AMSApp.paraconf
{
    public partial class wfmDeptInfo : wfmBase
    {
        BusiComm.BusiQuery busiq;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Login"] != null)
            {
                CMSMStruct.LoginStruct ls1 = (CMSMStruct.LoginStruct)Session["Login"];
                if (!IsPostBack)
                {                    
                    Session.Remove("DeptInfo");
                    Query();
                }
            }
            else
            {
                Response.Redirect("../Exit.aspx");
            }
        }
        private void Query()
        {            
            Hashtable htapp = (Hashtable)Application["appconf"];
            string strcons = (string)htapp["cons"];
            busiq = new BusiComm.BusiQuery(strcons);

            try
            {
                DataTable dtout = busiq.DeptInfoQuery();
                if (dtout == null)
                {
                    this.SetErrorMsgPageBydir("查询出错，请重试！");
                    return;
                }
                else
                {
                    this.GridView1.DataSource = dtout;
                    this.GridView1.DataBind();
                    Session["DeptInfo"] = dtout;
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
            DataTable dtOut = (DataTable)Session["DeptInfo"];
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
            string strOldDeptName = ((Label)this.GridView1.Rows[e.RowIndex].Cells[9].Controls[1]).Text;
            Hashtable htapp = (Hashtable)Application["appconf"];
            string strcons = (string)htapp["cons"];
            busiq = new BusiComm.BusiQuery(strcons);
            try
            {
                bool success = busiq.DeleteDeptInfo(strOldDeptName);
                if (!success)
                {
                    this.SetErrorMsgPageBydir("门店信息删除出错，请重试！");
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
                this.SetErrorMsgPageBydir("门店信息删除错误，请重试！");
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
            string strDeptName = ((TextBox)this.GridView1.Rows[e.RowIndex].Cells[0].Controls[1]).Text;
            string strAddress = ((TextBox)this.GridView1.Rows[e.RowIndex].Cells[1].Controls[1]).Text;
            string strTel = ((TextBox)this.GridView1.Rows[e.RowIndex].Cells[2].Controls[1]).Text;
            string strManager = ((TextBox)this.GridView1.Rows[e.RowIndex].Cells[3].Controls[1]).Text;
            string strManagerPhone = ((TextBox)this.GridView1.Rows[e.RowIndex].Cells[4].Controls[1]).Text;
            string strAdsl = ((TextBox)this.GridView1.Rows[e.RowIndex].Cells[5].Controls[1]).Text;
            string strAdslPwd = ((TextBox)this.GridView1.Rows[e.RowIndex].Cells[6].Controls[1]).Text;
            string strVpn = ((TextBox)this.GridView1.Rows[e.RowIndex].Cells[7].Controls[1]).Text;
            string strVpnPwd = ((TextBox)this.GridView1.Rows[e.RowIndex].Cells[8].Controls[1]).Text;
            string strOldDeptName = ((Label)this.GridView1.Rows[e.RowIndex].Cells[9].Controls[1]).Text;
            
            Hashtable htapp = (Hashtable)Application["appconf"];
            string strcons = (string)htapp["cons"];
            busiq = new BusiComm.BusiQuery(strcons);
            CMSMStruct.LoginStruct ls1 = (CMSMStruct.LoginStruct)Session["Login"];
            try
            {
                bool exist = busiq.ExistDeptInfo(strDeptName);
                if (strDeptName!=strOldDeptName && exist)
                {
                    this.Popup("门店已存在");
                    return;
                }
                bool success = busiq.UpdateDeptInfo(strOldDeptName, strDeptName, strAddress, strTel, strManager,strManagerPhone, strAdsl,strAdslPwd, strVpn,strVpnPwd);
                if (!success)
                {
                    this.SetErrorMsgPageBydir("门店信息修改出错，请重试！");
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
                this.SetErrorMsgPageBydir("门店信息修改错误，请重试！");
                return;
            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string strDeptName = "";
            string strAddress = "";
            string strTel = "";
            string strManager = "";
            string strManagerPhone = "";
            string strAdsl = "";
            string strAdslPwd = "";
            string strVpn = "";
            string strVpnPwd = "";
            if (e.CommandName == "EmptyInsert")
            {
                strDeptName = ((TextBox)GridView1.Controls[0].Controls[0].Controls[0].FindControl("txtDeptName")).Text;
                strAddress = ((TextBox)GridView1.Controls[0].Controls[0].Controls[0].FindControl("txtAddress")).Text;
                strTel = ((TextBox)GridView1.Controls[0].Controls[0].Controls[0].FindControl("txtTel")).Text;
                strManager = ((TextBox)GridView1.Controls[0].Controls[0].Controls[0].FindControl("txtManager")).Text;
                strManagerPhone = ((TextBox)GridView1.Controls[0].Controls[0].Controls[0].FindControl("txtManagerPhone")).Text;
                strAdsl = ((TextBox)GridView1.Controls[0].Controls[0].Controls[0].FindControl("txtAdsl")).Text;
                strAdslPwd = ((TextBox)GridView1.Controls[0].Controls[0].Controls[0].FindControl("txtAdslPwd")).Text;
                strVpn = ((TextBox)GridView1.Controls[0].Controls[0].Controls[0].FindControl("txtVpn")).Text;
                strVpnPwd = ((TextBox)GridView1.Controls[0].Controls[0].Controls[0].FindControl("txtVpnPwd")).Text;
            }
            else if (e.CommandName == "Insert")
            {
                strDeptName = ((TextBox)GridView1.FooterRow.Cells[0].Controls[1]).Text;
                strAddress = ((TextBox)GridView1.FooterRow.Cells[1].Controls[1]).Text;
                strTel = ((TextBox)GridView1.FooterRow.Cells[2].Controls[1]).Text;
                strManager = ((TextBox)GridView1.FooterRow.Cells[3].Controls[1]).Text;
                strManagerPhone = ((TextBox)GridView1.FooterRow.Cells[4].Controls[1]).Text;
                strAdsl = ((TextBox)GridView1.FooterRow.Cells[5].Controls[1]).Text;
                strAdslPwd = ((TextBox)GridView1.FooterRow.Cells[6].Controls[1]).Text;
                strVpn = ((TextBox)GridView1.FooterRow.Cells[7].Controls[1]).Text;
                strVpnPwd = ((TextBox)GridView1.FooterRow.Cells[8].Controls[1]).Text;
            }
            else
            {
                return;
            }
            if (string.IsNullOrEmpty(strDeptName))
            {
                this.Popup("门店不能为空");
                return;
            }
            Hashtable htapp = (Hashtable)Application["appconf"];
            string strcons = (string)htapp["cons"];
            busiq = new BusiComm.BusiQuery(strcons);
            CMSMStruct.LoginStruct ls1 = (CMSMStruct.LoginStruct)Session["Login"];
            try
            {
                bool exist = busiq.ExistDeptInfo(strDeptName);
                if (exist)
                {
                    this.Popup("门店已存在");
                    return;
                }
                bool success = busiq.InsertDeptInfo(strDeptName, strAddress, strTel, strManager,strManagerPhone, strAdsl,strAdslPwd, strVpn,strVpnPwd);
                if (!success)
                {
                    this.SetErrorMsgPageBydir("门店信息添加出错，请重试！");
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
                this.SetErrorMsgPageBydir("门店信息添加错误，请重试！");
                return;
            }
        }
    }
}