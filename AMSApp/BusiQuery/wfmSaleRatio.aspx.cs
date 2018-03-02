using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Data;
using CommCenter;

namespace AMSApp.BusiQuery
{
    public partial class wfmSaleRatio : wfmBase
    {
        BusiComm.BusiQuery busiq;
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Button2.Attributes.Add("onclick", "javascript:window.open('../DataGridToExcel.aspx', 'Sample', 'toolbar=no,location=no,directories=no,status=yes,menubar=yes,scrollbars=no,resizable=yes,copyhistory=yes,width=790,height=520,left=0,top=0')");
            if (Session["Login"] == null)
            {
                Response.Redirect("../Exit.aspx");
            }
            if (!IsPostBack)
            {
                Session.Remove("QUERY");
                Session.Remove("toExcel");
                Session.Remove("page_view");
                MonthList(ddlMonths,false);
            }
        }
        

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                //查询
                Session.Remove("QUERY");
                Session.Remove("toExcel");

                Hashtable htapp = (Hashtable)Application["appconf"];
                string strcons = (string)htapp["cons"];
                busiq = new BusiComm.BusiQuery(strcons);

                DataTable tbCommCode = (DataTable)Application["tbCommCode"];
                DataRow[] drs = tbCommCode.Select("vcCommSign ='GT'");
                string strGoodsType = "[累计业绩],";
                foreach (DataRow dr in drs)
                {
                    strGoodsType += "[" + dr["vcCommName"].ToString() + "],[" + dr["vcCommName"].ToString() + "占比],";
                }
                strGoodsType = strGoodsType.TrimEnd(',');

                DataTable dtout = busiq.GetSaleRatio(ddlMonths.SelectedValue, strGoodsType);
                foreach (DataRow dr in dtout.Rows)
                {
                    decimal sum = 0;
                    foreach (DataRow dr1 in drs)
                    {
                        string value = dr[dr1["vcCommName"].ToString()].ToString();
                        if (!string.IsNullOrEmpty(value))
                        {
                            sum += decimal.Parse(value);
                        }
                    }
                    dr["累计业绩"] = sum;
                    if (sum > 0)
                    {
                        foreach (DataRow dr1 in drs)
                        {
                            string value = dr[dr1["vcCommName"].ToString()].ToString();
                            if (!string.IsNullOrEmpty(value))
                            {
                                dr[dr1["vcCommName"].ToString() + "占比"] = Math.Round(decimal.Parse(value) / sum*100,2);
                            }
                        }
                    }
                }

                dtout.TableName = "月份各类产品销售占比表";
                DataTable dtexcel = dtout.Copy();
                Session["QUERY"] = dtout;
                Session["toExcel"] = dtexcel;
                CMSMStruct.LoginStruct ls1 = (CMSMStruct.LoginStruct)Session["Login"];
                if (dtout.Rows.Count <= 0)
                {
                    Button2.Enabled = false;
                }
                else
                {
                    if (ls1.strLimit == "CL001")
                    {
                        Button2.Enabled = true;
                    }
                }
                UcPageView1.MyDataGrid.PageSize = 30;
                DataView dvOut = new DataView(dtout);
                this.UcPageView1.MyDataSource = dvOut;
                this.UcPageView1.BindGrid();
            }
            catch (Exception ex)
            {
                this.clog.WriteLine(ex);
                this.SetErrorMsgPageBydir("查询错误，请重试！");
                return;
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {

        }
    }
}