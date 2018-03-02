using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CommCenter;
using System.Collections;
using System.Data;

namespace AMSApp.BusiQuery
{
    public class KPIOfGoods
    {
        public string Desc { get; set; }
        public string vcGoodsName { get; set; }
        public string NewDate { get; set; }
        public string Quantity { get; set; }
        public string Amount { get; set; }
        public string Unit { get; set; }
    }
    public class KPIOfDept
    {
        public string Serial { get; set; }
        public string vcDeptName { get; set; }
        public string Kpi { get; set; }
        public string Amount { get; set; }
        public string Sum { get; set; }
        public string Ratio { get; set; }
        public string MonthRatio { get; set; }
    }
    public partial class wfmPerformanceOfDay : wfmBase
    {
        BusiComm.BusiQuery busiq;
        public List<KPIOfGoods> lKPIOfGoods { get; set; }
        public List<KPIOfDept> lKPIOfDept { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Login"] != null)
            {
                if (!IsPostBack)
                {                    
                    Session.Remove("QUERY");
                    Session.Remove("toExcel");
                    Session.Remove("page_view");
                    txtDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                }
            }
            else
            {
                Response.Redirect("../Exit.aspx");
            }
        }
        private void GetKPI(bool IsUpdate)
        {
            //查询数据
            if (string.IsNullOrEmpty(txtDate.Text))
            {
                this.Popup("请输入日期");
                return;
            }
            DateTime dtDate = DateTime.Parse(txtDate.Text);
            Hashtable htapp = (Hashtable)Application["appconf"];
            string strcons = (string)htapp["cons"];
            busiq = new BusiComm.BusiQuery(strcons);

            DataSet ds = busiq.GetKPI(dtDate, txtWeather.Text, txtException.Text, IsUpdate);
            lblWeek.Text = "......";
            txtWeather.Text = "";
            txtException.Text = "";
            lblMonthAmount.Text = "......";
            lblLastYearMonthAmount.Text = "......";
            lblAmount.Text = "......";
            lblLastYearDateAmount.Text = "......";
            lblDifLastYearDateAmount.Text = "......";
            lblSum.Text = "......";
            lblRatio.Text = "......";
            lblMonthRatio.Text = "......";
            lblQuantity.Text = "......";
            lblPrice.Text = "......";
            //去年同月业绩            
            if (ds.Tables.Contains("LastYearKPIOfDept"))
            {
                DataTable dt1 = ds.Tables["LastYearKPIOfDept"];
                object obj = dt1.Compute("Sum([Sum])", "");
                if (obj != null)
                {
                    lblLastYearMonthAmount.Text = obj.ToString();
                }
            }
            //去年同一天
            decimal lastYearDateAmount = 0;
            if (ds.Tables.Contains("LastYearDateKPIOfDept"))
            {
                DataTable dt3 = ds.Tables["LastYearDateKPIOfDept"];
                object obj = dt3.Compute("Sum([Amount])", "");
                if (obj != null)
                {
                    string strLastYearDateAmount = obj.ToString();
                    lastYearDateAmount = Convert.ToDecimal(strLastYearDateAmount);
                    lblLastYearDateAmount.Text = strLastYearDateAmount;

                }
            }
            //业绩 
            decimal amount = 0;
            decimal sum = 0;
            if (ds.Tables.Contains("KPIOfDay"))
            {
                DataTable dt4 = ds.Tables["KPIOfDay"];
                DataRow dr = dt4.Rows[0];

                string strAmount = dr["Amount"].ToString();
                if (!string.IsNullOrEmpty(strAmount))
                {
                    lblAmount.Text = strAmount;
                    amount = Convert.ToDecimal(strAmount);
                }

                string strSum = dr["Sum"].ToString();
                if (!string.IsNullOrEmpty(strSum))
                {
                    sum = Convert.ToDecimal(strSum);
                    lblSum.Text = strSum;
                }
                lblDifLastYearDateAmount.Text = (amount - lastYearDateAmount).ToString();

                lblQuantity.Text = dr["Quantity"].ToString();
                lblPrice.Text = dr["Price"].ToString();
                txtException.Text = dr["Exception"].ToString();
                txtWeather.Text = dr["Weather"].ToString();
                string strWeek = dr["Week"].ToString();
                if (!string.IsNullOrEmpty(strWeek))
                {
                    int iWeek = Convert.ToInt32(strWeek);
                    switch (iWeek)
                    {
                        case 0:
                            lblWeek.Text = "日";
                            break;
                        case 1:
                            lblWeek.Text = "一";
                            break;
                        case 2:
                            lblWeek.Text = "二";
                            break;
                        case 3:
                            lblWeek.Text = "三";
                            break;
                        case 4:
                            lblWeek.Text = "四";
                            break;
                        case 5:
                            lblWeek.Text = "五";
                            break;
                        case 6:
                            lblWeek.Text = "六";
                            break;
                    }
                }
            }
            //门店业绩            
            if (ds.Tables.Contains("KPIOfDept"))
            {
                DataTable dt2 = ds.Tables["KPIOfDept"];
                //指标
                decimal kpi = 0;
                if (ds.Tables.Contains("KPIOfMonth"))
                {
                    DataTable dt6 = ds.Tables["KPIOfMonth"];
                    //object obj = dt6.Compute("Sum([Amount])", "");
                    //if (obj != null)
                    //{
                    //    string strKpi = obj.ToString();
                    //    lblMonthAmount.Text = strKpi;

                    //    kpi = Convert.ToDecimal(strKpi);
                    //    lblRatio.Text = (Math.Round(sum / (kpi == 0 ? 1 : kpi),2) * 100).ToString() + "%";
                    //    lblMonthRatio.Text = (Math.Round(sum / dtDate.Day * DateTime.DaysInMonth(dtDate.Year, dtDate.Month) / (kpi == 0 ? 1 : kpi), 2) * 100).ToString() + "%";
                    //}

                    this.DataTableConvert(dt2, "vcDeptId", "tbCommCode", "vcCommSign ='MD'");
                    lKPIOfDept = new List<KPIOfDept>();
                    int serial = 0;
                    decimal deptKpiSum = 0;
                    decimal deptAmountSum = 0;
                    decimal deptSumSum = 0;

                    foreach (DataRow dr in dt2.Rows)
                    {
                        KPIOfDept dept = new KPIOfDept();
                        serial += 1;
                        dept.Serial = serial.ToString();
                        dept.vcDeptName = dr["vcDeptIdComments"].ToString();
                        string strDeptId = dr["vcDeptId"].ToString();
                        DataRow[] drs = dt6.Select("vcDeptId='" + strDeptId + "'");
                        if (drs.Length > 0)
                        {
                            dept.Kpi = drs[0]["Amount"].ToString();
                        }
                        dept.Amount = dr["Amount"].ToString();
                        dept.Sum = dr["Sum"].ToString();
                        decimal deptkpi = 0;
                        decimal deptSum = 0;
                        decimal deptAmount = 0;
                        if (!string.IsNullOrEmpty(dept.Kpi))
                        {
                            deptkpi = Convert.ToDecimal(dept.Kpi);
                            deptKpiSum += deptkpi;
                        }
                        if (!string.IsNullOrEmpty(dept.Sum))
                        {
                            deptSum = Convert.ToDecimal(dept.Sum);
                            deptSumSum += deptSum;
                        }
                        if (!string.IsNullOrEmpty(dept.Amount))
                        {
                            deptAmount = Convert.ToDecimal(dept.Amount);
                            deptAmountSum += deptAmount;
                        }
                        dept.Ratio = (Math.Round(deptSum / (deptkpi == 0 ? 1 : deptkpi), 2) * 100).ToString() + "%";
                        dept.MonthRatio = (Math.Round(deptSum / dtDate.Day * DateTime.DaysInMonth(dtDate.Year, dtDate.Month) / (deptkpi == 0 ? 1 : deptkpi), 2) * 100).ToString() + "%";
                        lKPIOfDept.Add(dept);
                    }
                    KPIOfDept deptTotal = new KPIOfDept();
                    deptTotal.Serial = "合计";
                    deptTotal.Kpi = deptKpiSum.ToString();
                    deptTotal.Amount = deptAmountSum.ToString();
                    deptTotal.Sum = deptSumSum.ToString();
                    deptTotal.Ratio = (Math.Round(deptSumSum / (deptKpiSum == 0 ? 1 : deptKpiSum), 2) * 100).ToString() + "%";
                    deptTotal.MonthRatio = (Math.Round(deptSumSum / dtDate.Day * DateTime.DaysInMonth(dtDate.Year, dtDate.Month) / (deptKpiSum == 0 ? 1 : deptKpiSum), 2) * 100).ToString() + "%";
                    lKPIOfDept.Add(deptTotal);

                    lblMonthAmount.Text = deptTotal.Kpi;
                    lblRatio.Text = deptTotal.Ratio;
                    lblMonthRatio.Text = deptTotal.MonthRatio;
                }

            }


            //新品及重点产品业绩
            if (ds.Tables.Contains("KPIOfGoods"))
            {
                DataTable dt5 = ds.Tables["KPIOfGoods"];
                if (ds.Tables.Contains("tbGoods"))
                {
                    DataTable dt8 = ds.Tables["tbGoods"];
                    int inew = 0;
                    int ikey = 0;
                    lKPIOfGoods = new List<KPIOfGoods>();
                    foreach (DataRow dr in dt8.Rows)
                    {
                        KPIOfGoods goods = new KPIOfGoods();
                        string strNew = dr["IsNew"].ToString();
                        bool bNew = false;
                        if (!string.IsNullOrEmpty(strNew))
                        {
                            bNew = Convert.ToBoolean(strNew);
                        }
                        string strKey = dr["IsKey"].ToString();
                        bool bKey = false;
                        if (!string.IsNullOrEmpty(strKey))
                        {
                            bKey = Convert.ToBoolean(strKey);
                        }
                        if (bNew)
                        {
                            inew += 1;
                            goods.Desc = "新品名称" + inew.ToString();
                        }
                        if (bKey)
                        {
                            ikey += 1;
                            goods.Desc = "重点产品" + ikey.ToString();
                        }
                        goods.vcGoodsName = dr["vcGoodsName"].ToString();
                        string strNewDate = dr["NewDate"].ToString();
                        if (!string.IsNullOrEmpty(strNewDate))
                        {
                            goods.NewDate = DateTime.Parse(strNewDate).ToString("yyyy-MM-dd");
                        }
                        goods.Unit = dr["Unit"].ToString();
                        string strGoodsId = dr["vcGoodsId"].ToString();
                        DataRow[] drs = dt5.Select("vcGoodsId='" + strGoodsId + "'");
                        if (drs.Length > 0)
                        {
                            goods.Quantity = drs[0]["Quantity"].ToString();
                            goods.Amount = drs[0]["Amount"].ToString();
                        }
                        lKPIOfGoods.Add(goods);
                    }
                }
            }

            //前一天
            if (ds.Tables.Contains("BeforeDateKPIOfDept"))
            {
                DataTable dt7 = ds.Tables["BeforeDateKPIOfDept"];
                object obj = dt7.Compute("Sum([Amount])", "");
                if (obj != null)
                {
                    string strLastDateAmount = obj.ToString();
                    decimal lastDateAmount = Convert.ToDecimal(strLastDateAmount);
                    lblDifLastDateAmount.Text = (amount - lastDateAmount).ToString();
                }
            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            //查询
            GetKPI(false);
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            //指标
            this.RedirectPage("wfmKPI.aspx");
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            //更新
            GetKPI(true);
        }
    }
}