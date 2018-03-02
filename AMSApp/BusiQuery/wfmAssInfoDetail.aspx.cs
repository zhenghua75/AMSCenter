using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;
using CommCenter;
using BusiComm;

namespace AMSApp.BusiQuery
{
    public partial class wfmAssInfoDetail : wfmBase
    {
        BusiComm.BusiQuery busiq;
        BusiComm.Manager m1;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string strCardId = Request["vcCardId"];
                this.lblCardId.Text = strCardId;
                string strAssState = Request["OperFlag"];
                Hashtable htapp = (Hashtable)Application["appconf"];
                string strcons = (string)htapp["cons"];
                busiq = new BusiComm.BusiQuery(strcons);

                DataTable dt = busiq.GetAssInfo(strCardId,strAssState);

                if (dt == null || dt.Rows.Count == 0)
                {
                    switch (strAssState)
                    {
                        case "0":
                            this.Popup("未找到会员资料或者此会员不在正常在用状态");
                            break;
                        case "1":
                            this.Popup("未找到会员资料或者此会员不在挂失状态");
                            break;
                    }
                    this.RedirectPage("wfmAssInfo.aspx");
                    return;
                }
                this.lblAssName.Text = dt.Rows[0]["vcAssName"].ToString();
                this.lblLinkPhone.Text = dt.Rows[0]["vcLinkPhone"].ToString();
                this.lblLinkAddress.Text = dt.Rows[0]["vcLinkAddress"].ToString();
                this.lblAssState.Text = dt.Rows[0]["vcAssState"].ToString();
                this.lblCharge.Text = dt.Rows[0]["nCharge"].ToString();
                this.lblCreateDate.Text = dt.Rows[0]["dtCreateDate"].ToString();
                this.lblOperDate.Text = dt.Rows[0]["dtOperDate"].ToString();
                this.lblDept.Text = dt.Rows[0]["vcDeptName"].ToString();
                this.hfAssId.Value = dt.Rows[0]["iAssId"].ToString();
                this.hfAssState.Value = strAssState;

                switch (strAssState)
                {
                    case "0":
                        this.Button1.Text = "挂失";
                        break;
                    case "1":
                        this.Button1.Text = "解挂";
                        break;
                }
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            this.RedirectPage("wfmAssInfo.aspx");
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                CMSMStruct.LoginStruct ls1 = (CMSMStruct.LoginStruct)Session["Login"];
                Hashtable htapp = (Hashtable)Application["appconf"];
                string strcons = (string)htapp["cons"];
                m1 = new Manager(strcons);
                switch (this.hfAssState.Value)
                {
                    case "0":
                        m1.LossCard(this.lblCardId.Text, this.hfAssId.Value, ls1.strOperName, ls1.strDeptID);
                        this.SetSuccMsgPageBydir("挂失成功", "BusiQuery/wfmAssInfo.aspx");
                        break;
                    case "1":
                        m1.CancelLossCard(this.lblCardId.Text, this.hfAssId.Value, ls1.strOperName, ls1.strDeptID);
                        this.SetSuccMsgPageBydir("解挂成功", "BusiQuery/wfmAssInfo.aspx");
                        break;
                }
            }
            catch (Exception ex)
            {
                this.clog.WriteLine(ex);
                this.SetErrorMsgPageBydirHistory(ex.Message);
                return;
            }
        }
    }
}