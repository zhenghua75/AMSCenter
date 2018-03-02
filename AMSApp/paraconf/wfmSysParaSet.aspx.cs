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
//					this.SetErrorMsgPageBydir("�Բ�����û��Ȩ��ʹ�ô˹��ܣ�");
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

                    //�µ��������
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
				this.SetErrorMsgPageBydir("û����Ʒ��Ϣ������¼����Ʒ��Ϣ��");
				return;
			}
			if(lbtNew.Items.Count>=10)
			{
				this.SetErrorMsgPageBydir("�Ƽ���Ʒֻ������10����");
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
				this.SetErrorMsgPageBydir("��ȷ��ѡ��ĳ����Ʒ��");
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
				this.SetErrorMsgPageBydir("Ŀǰ��û��Ҫ�Ƽ�����Ʒ��");
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
				this.SetSuccMsgPageBydir("�Ƽ���Ʒ���óɹ���","paraconf/wfmSysParaSet.aspx");
				return;
			}
			else
			{
				this.SetErrorMsgPageBydir("�Ƽ���Ʒ����ʧ�ܣ�");
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
				this.SetErrorMsgPageBydir("���ѽ���С��0��");
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
				this.SetErrorMsgPageBydir("���ͻ��ַ�ֵ����С��0��");
				return;
			}
			else
			{
				cos.strCommCode=txtIg.Text.Trim();
			}
			cos.strCommSign="IG";
			cos.strComments="��������";

			Hashtable htapp=(Hashtable)Application["appconf"];
			string strcons=(string)htapp["cons"];
			m1=new Manager(strcons);

			if(m1.UpdateIgComm(cos))
			{
				this.SetSuccMsgPageBydir("���ѻ������óɹ���","paraconf/wfmSysParaSet.aspx");
				return;
			}
			else
			{
				this.SetErrorMsgPageBydir("���ѻ�������ʧ�ܣ�");
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
				this.SetErrorMsgPageBydir("��ֵ�����������С��0��");
				return;
			}
			else
			{
				cos.strCommCode=txtPromRate1.Text.Trim();
			}
			cos.strCommName="��ֵ����100-500";
			cos.strCommSign="FP1";
			cos.strComments="��ֵ����";
			htpar.Add("FP1",cos);

			CMSMStruct.CommStruct cos2=new CMSMStruct.CommStruct();
			if(txtPromRate2.Text.Trim()=="")
			{
				cos2.strCommCode="0";
			}
			else if(double.Parse(txtPromRate2.Text.Trim())<0)
			{
				this.SetErrorMsgPageBydir("��ֵ�����������С��0��");
				return;
			}
			else
			{
				cos2.strCommCode=txtPromRate2.Text.Trim();
			}
			cos2.strCommName="��ֵ����500-1000";
			cos2.strCommSign="FP2";
			cos2.strComments="��ֵ����";
			htpar.Add("FP2",cos2);

			CMSMStruct.CommStruct cos3=new CMSMStruct.CommStruct();
			if(txtPromRate3.Text.Trim()=="")
			{
				cos3.strCommCode="0";
			}
			else if(double.Parse(txtPromRate3.Text.Trim())<0)
			{
				this.SetErrorMsgPageBydir("��ֵ�����������С��0��");
				return;
			}
			else
			{
				cos3.strCommCode=txtPromRate3.Text.Trim();
			}
			cos3.strCommName="��ֵ����1000����";
			cos3.strCommSign="FP3";
			cos3.strComments="��ֵ����";
			htpar.Add("FP3",cos3);

			Hashtable htapp=(Hashtable)Application["appconf"];
			string strcons=(string)htapp["cons"];
			m1=new Manager(strcons);

			if(m1.UpdateFillPromComm(htpar))
			{
				this.SetSuccMsgPageBydir("���ѻ������óɹ���","paraconf/wfmSysParaSet.aspx");
				return;
			}
			else
			{
				this.SetErrorMsgPageBydir("���ѻ�������ʧ�ܣ�");
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
                    this.SetErrorMsgPageBydir("��ѯ���������ԣ�");
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
                this.SetErrorMsgPageBydir("��ѯ���������ԣ�");
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
                    this.SetErrorMsgPageBydir("��ֵ�������ɾ�����������ԣ�");
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
                this.SetErrorMsgPageBydir("��ֵ�������ɾ�����������ԣ�");
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
                    this.Popup("��ֵ��������Ѵ���");
                    return;
                }
                bool success = m1.UpdatePromRatio(strOldCommName, strCommName, strOldCommCode, strCommCode);
                if (!success)
                {
                    this.SetErrorMsgPageBydir("��ֵ��������޸ĳ��������ԣ�");
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
                this.SetErrorMsgPageBydir("��ֵ��������޸Ĵ��������ԣ�");
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
                this.Popup("��ֵ���������Ϊ��");
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
                    this.Popup("��ֵ��������Ѵ���");
                    return;
                }
                bool success = m1.InsertPromRatio(strCommName, strCommCode);
                if (!success)
                {
                    this.SetErrorMsgPageBydir("��ֵ���������ӳ��������ԣ�");
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
                this.SetErrorMsgPageBydir("��ֵ���������Ӵ��������ԣ�");
                return;
            }
        }
	}
}
