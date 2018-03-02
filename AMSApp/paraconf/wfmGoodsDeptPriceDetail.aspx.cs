using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusiComm;
using System.Collections;

namespace AMSApp.paraconf
{
    public partial class wfmGoodsDeptPriceDetail : wfmBase
    {
        Manager m1;
        private void SetManager()
        {
            Hashtable htapp = (Hashtable)Application["appconf"];
            string strcons = (string)htapp["cons"];
            m1 = new Manager(strcons);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                //this.FillDropDownList("AllMDP", ddlMD, "vcCommSign='MD' and vcCommCode not in('CEN00','FYZX1')");
                this.FillDropDownList("tbCommCode", ddlMD, "vcCommSign='MD' and vcCommCode not in('CEN00','FYZX1')", "");
                this.txtPrice.Enabled = false;
                this.txtGoodsId.Enabled = false;
                this.txtGoodsName.Enabled = false;

                string strOperFlag = Request["OperFlag"];
                string strGoodsId = Request["vcGoodsId"];
                string strGoodsName = Request["vcGoodsName"];
                string strPrice = Request["nPrice"];
                switch (strOperFlag)
                {
                    case "modify":
                        this.ddlMD.Enabled = false;
                        string strDeptPrice = Request["nDeptPrice"];
                        string strDeptId = Request["vcDeptId"];

                        this.txtPrice.Text = strPrice;

                        this.txtGoodsId.Text = strGoodsId;
                        this.txtGoodsName.Text = strGoodsName;
                        this.txtDeptPrice.Text = strDeptPrice;
                        this.Button1.Visible = false;
                        this.SetDropDownListSelectedIndex(this.ddlMD, strDeptId);
                        break;
                    case "add":
                        this.txtGoodsId.Text = strGoodsId;
                        this.txtGoodsName.Text = strGoodsName;
                        this.txtPrice.Text = strPrice;
                        this.Button2.Visible = false;
                        this.Button3.Visible = false;
                        break;
                }
            }
        }
        
        private void Validpage()
        {
            if (this.txtGoodsId.Text.Trim().Length == 0)
                throw new Exception("请输入商品编号");
            if (this.txtGoodsName.Text.Trim().Length == 0)
                throw new Exception("请输入商品名称");
            if (this.txtDeptPrice.Text.Trim().Length == 0)
                throw new Exception("请输入商品门店单价");
            try
            {
                Convert.ToDouble(this.txtDeptPrice.Text);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void Button1_Click(object sender, System.EventArgs e)
        {
            //添加套餐
            try
            {
                Validpage();
                this.SetManager();
                string strDeptId = this.ddlMD.SelectedValue;
                string strDeptName = this.ddlMD.SelectedItem.Text;
                string strGoodsId = this.txtGoodsId.Text;
                string strGoodsName = this.txtGoodsName.Text;
                string nDeptPrice = this.txtDeptPrice.Text;
                string nPrice = this.txtPrice.Text;
                m1.AddGoodsDeptPrice(strDeptId,strGoodsId,nDeptPrice);
                this.SetSuccMsgPageBydir("添加成功", "paraconf/wfmGoodsDeptPrice.aspx?"
                    + "vcGoodsId="+strGoodsId
                    + "&vcGoodsName="+strGoodsName
                    + "&nPrice=" + nPrice);
            }
            catch (Exception ex)
            {
                this.clog.WriteLine(ex);
                this.SetErrorMsgPageBydirHistory(ex.Message);
                return;
            }
        }

        protected void Button2_Click(object sender, System.EventArgs e)
        {
            //修改套餐
            try
            {
                Validpage();                
                this.SetManager();
                string strDeptId = this.ddlMD.SelectedValue;
                string strGoodsId = this.txtGoodsId.Text;
                string strGoodsName = this.txtGoodsName.Text;
                string nDeptPrice = this.txtDeptPrice.Text;
                string nPrice = this.txtPrice.Text;
                m1.UpdateGoodsDeptPrice(strDeptId,strGoodsId,nDeptPrice);
                this.SetSuccMsgPageBydir("修改成功", "paraconf/wfmGoodsDeptPrice.aspx?"
                    + "vcGoodsId=" + strGoodsId
                    + "&vcGoodsName=" + strGoodsName
                    + "&nPrice=" + nPrice);
            }
            catch (Exception ex)
            {
                this.clog.WriteLine(ex);
                this.SetErrorMsgPageBydir(ex.Message);
                return;
            }
        }

        protected void Button3_Click(object sender, System.EventArgs e)
        {
            //删除套餐
            try
            {
                Validpage();
                this.SetManager();
                string strDeptId = this.ddlMD.SelectedValue;
                string strGoodsId = this.txtGoodsId.Text;
                string strGoodsName = this.txtGoodsName.Text;
                string nPrice = this.txtPrice.Text;
                m1.DeleteGoodsDeptPrice(strDeptId,strGoodsId);
                this.SetSuccMsgPageBydir("删除成功", "paraconf/wfmGoodsDeptPrice.aspx?"
                    + "vcGoodsId=" + strGoodsId
                    + "&vcGoodsName=" + strGoodsName
                    + "&nPrice=" + nPrice);
            }
            catch (Exception ex)
            {
                this.clog.WriteLine(ex);
                this.SetErrorMsgPageBydir(ex.Message);
                return;
            }
        }

        protected void Button4_Click(object sender, System.EventArgs e)
        {
            //返回商品门店单价管理
            string strGoodsId = this.txtGoodsId.Text;
            string strGoodsName = this.txtGoodsName.Text;
            string nPrice = this.txtPrice.Text;
            this.RedirectPage("wfmGoodsDeptPrice.aspx?"
                + "vcGoodsId=" + strGoodsId
                    + "&vcGoodsName=" + strGoodsName
                    + "&nPrice=" + nPrice);
        }
    }
}