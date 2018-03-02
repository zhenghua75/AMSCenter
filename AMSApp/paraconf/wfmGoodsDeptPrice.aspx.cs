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
    public partial class wfmGoodsDeptPrice : wfmBase
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

                    Session.Remove("QUERY");
                    Session.Remove("toExcel");

                    Hashtable htapp = (Hashtable)Application["appconf"];
                    string strcons = (string)htapp["cons"];
                    m1 = new Manager(strcons);
                    try
                    {
                        DataTable dtout = m1.GetGoodsDeptPrice(strGoodsId);
                        if (dtout == null)
                        {
                            this.SetErrorMsgPageBydir("查询出错，请重试！");
                            return;
                        }
                        else
                        {
                            dtout.TableName = "商品门店单价";
                            Session["QUERY"] = dtout;
                        }
                        UcPageView1.MyDataGrid.PageSize = 30;
                        DataView dvOut = new DataView(dtout);
                        this.UcPageView1.MyDataSource = dvOut;
                        this.UcPageView1.BindGrid();
                    }
                    catch (Exception er)
                    {
                        this.clog.WriteLine(er);
                        this.SetErrorMsgPageBydir("查询错误，请重试！");
                        return;
                    }

                }
            }
        }
        protected void btnAdd_Click(object sender, System.EventArgs e)
        {
            //添加商品门店单价
            this.RedirectPage("wfmGoodsDeptPriceDetail.aspx?OperFlag=add&vcGoodsId=" + this.lblGoodsId.Text 
                + "&vcGoodsName=" + this.lblGoodsName.Text + "&nPrice=" + this.lblPrice.Text);
        }

        protected void Button1_Click(object sender, System.EventArgs e)
        {
            this.RedirectPage("wfmGoods.aspx");
        }
    }
}