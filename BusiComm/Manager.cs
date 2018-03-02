using System;
using DataAccess;
using System.Data;
using CommCenter;
using System.Collections;

namespace BusiComm
{
	/// <summary>
	/// Summary description for Manager.
	/// </summary>
	public class Manager
	{
		string strcon="";
		OperAcc opa=null;
		public Manager(string strcons)
		{
			//
			// TODO: Add constructor logic here
			//
			strcon=strcons;
			opa=new OperAcc(strcon);
		}

		public CMSMStruct.LoginStruct GetLoginInfo(string strLoginid)
		{
			DataTable dtout=opa.GetLoginInfo(strLoginid);
			CMSMStruct.LoginStruct ls1=new CommCenter.CMSMStruct.LoginStruct();
			if(dtout!=null && dtout.Rows.Count>0)
			{
				ls1.strLoginID=dtout.Rows[0]["vcLoginID"].ToString();
				ls1.strOperName=dtout.Rows[0]["vcOperName"].ToString();
				ls1.strLimit=dtout.Rows[0]["vcLimit"].ToString();
				ls1.strPwd=dtout.Rows[0]["vcPwd"].ToString();
				ls1.strDeptID=dtout.Rows[0]["vcDeptID"].ToString();
			}
			else
			{
				ls1=null;
			}
			return ls1;
		}

		public DataTable GetGoods(string strGoodsid,string strGoodsName)
		{
			DataTable dtout=opa.GetGoods(strGoodsid,strGoodsName);
			if(dtout!=null)
			{
				dtout.Columns["vcGoodsID"].ColumnName="��Ʒ���";
				dtout.Columns["vcGoodsName"].ColumnName="��Ʒ����";
				dtout.Columns["vcSpell"].ColumnName="ƴ����д";
				dtout.Columns["nPrice"].ColumnName="����";
				dtout.Columns["iIgValue"].ColumnName="�һ���ֵ";
				dtout.Columns["vcComments"].ColumnName="��ע";
                dtout.Columns["Unit"].ColumnName = "��λ";
				dtout.Columns.Add("����");
                dtout.Columns.Add("��Ʒ�ŵ굥��");
                dtout.Columns.Add("��Ʒ�ŵ�");
				for(int i=0;i<dtout.Rows.Count;i++)
				{
					dtout.Rows[i]["����"]="<a href='wfmGoodsDetail.aspx?id=" + dtout.Rows[i]["��Ʒ���"].ToString() + "'>�༭</a>";
                    dtout.Rows[i]["��Ʒ�ŵ굥��"] = "<a href='wfmGoodsDeptPrice.aspx?"
                            + "vcGoodsId=" + dtout.Rows[i]["��Ʒ���"].ToString()
                            + "&vcGoodsName=" + dtout.Rows[i]["��Ʒ����"].ToString()
                            + "&nPrice=" + dtout.Rows[i]["����"].ToString()
                            + "'>�༭��Ʒ�ŵ굥��</a>";
                    dtout.Rows[i]["��Ʒ�ŵ�"] = "<a href='wfmGoodsDept.aspx?"
                            + "vcGoodsId=" + dtout.Rows[i]["��Ʒ���"].ToString()
                            + "&vcGoodsName=" + dtout.Rows[i]["��Ʒ����"].ToString()
                            + "&nPrice=" + dtout.Rows[i]["����"].ToString()
                            + "'>�༭��Ʒ�ŵ�</a>";
				}
			}
			return dtout;
		}
		
		public bool ChkGoodsIDDup(string strGoodsID)
		{
			string strid=opa.getGoodsID(strGoodsID);
			if(strid!="0")
			{
				return false;
			}

			return true;
		}

		public string GetGoodsMaxID(string strmask)
		{
			string strid=opa.getMaxGoodsID(strmask);

			return strid;
		}

		public bool ChkGoodsNameDup(string strGoodsName)
		{
			string strname=opa.getGoodsName(strGoodsName);
			if(strname!="0")
			{
				return false;
			}

			return true;
		}

		public bool ChkNewGoodsNameDup(string strnewGoodsName,string strGoodsID)
		{
			string strname=opa.getGoodsNamebyID(strnewGoodsName,strGoodsID);
			if(strname!="0")
			{
				return false;
			}

			return true;
		}

		public bool InsertGoods(CMSMStruct.GoodsStruct gs)
		{
			int recount=opa.InsertGoods(gs);
			if(recount<=0)
			{
				return false;
			}

			return true;
		}

		public CMSMStruct.GoodsStruct GetGoodsInfo(string strGoodsid)
		{
			DataTable dtout=opa.GetGoodsInfo(strGoodsid);
			CMSMStruct.GoodsStruct gs=new CommCenter.CMSMStruct.GoodsStruct();
			if(dtout!=null)
			{
				gs.strGoodsID=dtout.Rows[0]["vcGoodsID"].ToString();
				gs.strGoodsName=dtout.Rows[0]["vcGoodsName"].ToString();
				gs.strSpell=dtout.Rows[0]["vcSpell"].ToString();
				gs.dPrice=double.Parse(dtout.Rows[0]["nPrice"].ToString());
				gs.iIgValue=int.Parse(dtout.Rows[0]["iIgValue"].ToString());
				gs.strComments=dtout.Rows[0]["vcComments"].ToString();
				gs.bPackage = Convert.ToBoolean(dtout.Rows[0]["IsPackage"]);
                gs.bNew = Convert.ToBoolean(dtout.Rows[0]["IsNew"].ToString());
                gs.bKey = Convert.ToBoolean(dtout.Rows[0]["IsKey"].ToString());
                gs.Unit = dtout.Rows[0]["Unit"].ToString();
                gs.NewDate = dtout.Rows[0]["NewDate"].ToString();
                gs.bDeptPrice = Convert.ToBoolean(dtout.Rows[0]["IsDeptPrice"].ToString());
			}
			return gs;
		}

		public bool UpdateGoods(CMSMStruct.GoodsStruct gsnew,CMSMStruct.GoodsStruct gsold)
		{
			string sqlset="";
			if(gsnew.strGoodsName!=gsold.strGoodsName)
			{
				sqlset+="vcGoodsName='" + gsnew.strGoodsName + "',";
			}
			if(gsnew.strSpell!=gsold.strSpell)
			{
				sqlset+="vcSpell='" + gsnew.strSpell + "',";
			}
			if(gsnew.dPrice!=gsold.dPrice)
			{
				sqlset+="nPrice=" + gsnew.dPrice.ToString() + ",";
			}
			if(gsnew.iIgValue!=gsold.iIgValue)
			{
				sqlset+="iIgValue=" + gsnew.iIgValue.ToString() + ",";
			}
			if(gsnew.strComments!=gsold.strComments)
			{
				sqlset+="vcComments='" + gsnew.strComments + "',";
			}
			if(gsnew.bPackage != gsold.bPackage)
			{
				if(gsnew.bPackage)
				{
					sqlset+="IsPackage=1,";
				}
				else
				{
					sqlset+="IsPackage=0,";
				}
			}
            sqlset += gsnew.bNew ? "IsNew=1," : "IsNew=0,";
            //if (!string.IsNullOrEmpty(gsnew.bNew))
            //{
            //    sqlset += "IsNew=1,";
            //}
            sqlset += gsnew.bKey ? "IsKey=1," : "IsKey=0,";
            //if (!string.IsNullOrEmpty(gsnew.bKey))
            //{
            //    sqlset += "IsKey=1,";
            //}
            if (!string.IsNullOrEmpty(gsnew.Unit))
            {
                sqlset += "Unit='" + gsnew.Unit + "',";
            }
            if (!string.IsNullOrEmpty(gsnew.NewDate))
            {
                sqlset += "NewDate='" + gsnew.NewDate + "',";
            }
            sqlset += gsnew.bDeptPrice ? "IsDeptPrice=1," : "IsDeptPrice=0,";
            //if (!string.IsNullOrEmpty(gsnew.bDeptPrice))
            //{
            //    sqlset += "IsDeptPrice=1,";
            //}
			if(sqlset!="")
			{
				sqlset=sqlset.Substring(0,sqlset.Length-1);
				int recount=opa.UpdateGoods(gsnew.strGoodsID,sqlset);
				if(recount<=0)
				{
					return false;
				}
			}
			
			return true;
		}

		public string getGoodsSerial(string strCreateDate)
		{
			string strserial=opa.getGoodsSerial(strCreateDate);
			return strserial;
		}

		public string getNotiSerial(string strCreateDate)
		{
			string strserial=opa.getNotiSerial(strCreateDate);
			return strserial;
		}

		public string getAssSerial(string strCreateDate)
		{
			string strserial=opa.getAssSerial(strCreateDate);
			return strserial;
		}

		public bool writeDataLog(string strsqlset)
		{
			int recount=opa.InsertDataLog(strsqlset);
			if(recount<=0)
			{
				return false;
			}

			return true;
		}

		public ArrayList DownSysPara()
		{
			return opa.DownSysPara();
		}

		public ArrayList DownGoodsData()
		{
			return opa.DownGoodsData();
		}

		public ArrayList DownNotice(string strid)
		{
			return opa.DownNotice(strid);
		}

		public ArrayList DownAssData(string strBeginDate)
		{
			return opa.DownAssData(strBeginDate);
		}

		public DataTable GetLoginOper(Hashtable htpara)
		{
			DataTable dtout=opa.GetLoginOper(htpara);
			if(dtout!=null)
			{
				dtout.Columns["vcLoginID"].ColumnName="��¼ID";
				dtout.Columns["vcOperName"].ColumnName="����Ա����";
				dtout.Columns["vcLimit"].ColumnName="�鿴Ȩ��";
				dtout.Columns["vcDeptID"].ColumnName="�ŵ�";
				dtout.Columns.Add("����Ȩ��");
				for(int i=0;i<dtout.Rows.Count;i++)
				{
					dtout.Rows[i]["����Ȩ��"]="<a href='wfmOperPurview.aspx?FuncType=BS&id=" + dtout.Rows[i]["��¼ID"].ToString() + "&name="+dtout.Rows[i]["����Ա����"].ToString()+"'>�޸�Ȩ��</a>";
				}
				dtout.Columns.Add("����");
				for(int i=0;i<dtout.Rows.Count;i++)
				{
					dtout.Rows[i]["����"]="<a href='wfmOperDetail.aspx?id=" + dtout.Rows[i]["��¼ID"].ToString() + "'>�༭</a>";
				}
			}
			return dtout;
		}

		public bool ChkLoginIDDup(string strLoginID)
		{
			string strid=opa.getLoginID(strLoginID);
			if(strid!="0")
			{
				return false;
			}

			return true;
		}

		public int getLoginIDcount(string strLoginID)
		{
			string strid=opa.getLoginID(strLoginID);
			return int.Parse(strid);
		}

		public bool ChkOperNameDup(string strOperName)
		{
			string strname=opa.getOperName(strOperName);
			if(strname!="0")
			{
				return false;
			}

			return true;
		}

		public bool InsertLogin(CMSMStruct.LoginStruct lsnew)
		{
			int recount=opa.InsertLogin(lsnew);
			if(recount<=0)
			{
				return false;
			}

			return true;
		}
		
		public bool UpdateLogin(CMSMStruct.LoginStruct lsnew,CMSMStruct.LoginStruct lsold)
		{
			string sqlset="";
			if(lsnew.strOperName!=lsold.strOperName)
			{
				sqlset+="vcOperName='" + lsnew.strOperName + "',";
			}
			if(lsnew.strLimit!=lsold.strLimit)
			{
				sqlset+="vcLimit='" + lsnew.strLimit + "',";
			}
			if(lsnew.strDeptID!=lsold.strDeptID)
			{
				sqlset+="vcDeptID='" + lsnew.strDeptID + "',";
			}

			if(sqlset!="")
			{
				sqlset=sqlset.Substring(0,sqlset.Length-1);
				int recount=opa.UpdateLogin(lsold.strLoginID,sqlset);
				if(recount<=0)
				{
					return false;
				}
			}
			
			return true;
		}

		
		public bool DeleteLogin(string strLoginID)
		{
			int recount=opa.DeleteLogin(strLoginID);
			if(recount<=0)
			{
				return false;
			}

			return true;
		}

		public DataTable GetNotice(Hashtable htpara)
		{
			DataTable dtout=opa.GetNotice(htpara);
			if(dtout!=null)
			{
				dtout.Columns["id"].ColumnName="֪ͨ���";
				dtout.Columns["vcComments"].ColumnName="֪ͨ����";
				dtout.Columns["dtCreateDate"].ColumnName="����ʱ��";
//				dtout.Columns["vcActiveFlag"].ColumnName="���ͱ�־";
				dtout.Columns["vcDeptFlag"].ColumnName="�����ŵ�";
//				dtout.Columns.Add("����");
//				for(int i=0;i<dtout.Rows.Count;i++)
//				{
//					if(dtout.Rows[i]["���ͱ�־"].ToString()=="0")
//					{
//						dtout.Rows[i]["����"]="<a href='wfmNoticeDetail.aspx?id=" + dtout.Rows[i]["֪ͨ���"].ToString() + "'>����</a>";
//					}
//					else
//					{
//						dtout.Rows[i]["����"]="";
//					}
//				}
			}
			return dtout;
		}

		public bool InsertNotice(string strDept,string strContent)
		{
			int recount=opa.InsertNotice(strDept,strContent);
			if(recount<=0)
			{
				return false;
			}

			return true;
		}

		public bool UpdateNotice(string strid)
		{
			int recount=opa.UpdateNotice(strid);
			if(recount<=0)
			{
				return false;
			}

			return true;
		}

		public string getNoticeActiveFlag(string strid)
		{
			string strreturnid=opa.getNoticeActiveFlag(strid);
			return strreturnid;
		}

		public bool UpdateOperPwd(string strid,string strpwd)
		{
			int recount=opa.UpdateOperPwd(strid,strpwd);
			if(recount<=0)
			{
				return false;
			}

			return true;
		}

		public DataTable GetAllGoodsName()
		{
			DataTable dtout=opa.GetAllGoodsName();
			return dtout;
		}

		public DataTable GetIgPara()
		{
			DataTable dtout=opa.GetIgPara();
			return dtout;
		}

		public Hashtable GetPromRate()
		{
			DataTable dtout=opa.GetPromRate();
			Hashtable htPromRate=new Hashtable();
			if(dtout!=null)
			{
				for(int i=0;i<dtout.Rows.Count;i++)
				{
					htPromRate.Add(dtout.Rows[i]["vcCommSign"].ToString(),dtout.Rows[i]["vcCommCode"].ToString());
				}
			}
			return htPromRate;
		}
        public DataTable GetPromRatio()
        {
            DataTable dtout = opa.GetPromRatio();
            DataTable dt = new DataTable();
            dt.Columns.Add("vcBeginValue");
            dt.Columns.Add("vcEndValue");
            dt.Columns.Add("vcPromRatio");
            dt.Columns.Add("vcCommName");
            dt.Columns.Add("vcCommCode");
            foreach (DataRow dr in dtout.Rows)
            {
                string strValue = dr["vcCommName"].ToString();
                string strPromRatio = dr["vcCommCode"].ToString();
                string[] strValues = strValue.Split('-');
                if (strValues.Length == 2)
                {
                    DataRow drnew = dt.NewRow();
                    drnew["vcBeginValue"] = strValues[0];
                    drnew["vcEndValue"] = strValues[1];
                    drnew["vcPromRatio"] = strPromRatio;
                    drnew["vcCommName"] = strValue;
                    drnew["vcCommCode"] = strPromRatio;
                    dt.Rows.Add(drnew);
                }
                if (strValues.Length == 1)
                {
                    DataRow drnew = dt.NewRow();
                    drnew["vcBeginValue"] = strValue;
                    drnew["vcPromRatio"] = strPromRatio;
                    drnew["vcCommName"] = strValue;
                    drnew["vcCommCode"] = strPromRatio;
                    dt.Rows.Add(drnew);
                }
            }
            return dt;
        }
        public bool ExistPromRatio(string strCommName)
        {
            int recount = opa.ExistPromRatio(strCommName);
            if (recount <= 0)
            {
                return false;
            }
            return true;
        }
        public bool DeletePromRatio(string strCommName, string strCommCode)
        {
            int recount = opa.DeletePromRatio(strCommName,strCommCode);
            if (recount <= 0)
            {
                return false;
            }
            return true;
        }
        public bool UpdatePromRatio(string strOldCommName, string strCommName, string strOldCommCode, string strCommCode)
        {
            int recount = opa.UpdatePromRatio(strOldCommName, strCommName, strOldCommCode, strCommCode);
            if (recount <= 0)
            {
                return false;
            }

            return true;
        }
        public bool InsertPromRatio(string strCommName, string strCommCode)
        {
            int recount = opa.InsertPromRatio(strCommName, strCommCode);
            if (recount <= 0)
            {
                return false;
            }

            return true;
        }
		public DataTable GetIOTime()
		{
			DataTable dtout=opa.GetIOTime();
			if(dtout!=null)
			{
				dtout.Columns["vcOfficer"].ColumnName="ְ������";
				dtout.Columns["vcClass"].ColumnName="���";
				dtout.Columns["vcCommCode"].ColumnName="�ϰ�ʱ��";
				dtout.Columns["vcCommSign"].ColumnName="�°�ʱ��";
			}
			return dtout;
		}

		public DataSet GetFuncList(string strLogionID,string strFuncType)
		{
			DataSet dsout=opa.GetFuncList(strLogionID,strFuncType);
			return dsout;
		}

		public bool UpdateGoodsNewFlag(ArrayList al)
		{
			int recount=opa.UpdateGoodsNewFlag(al);
			if(recount<=0)
			{
				return false;
			}
			else
			{
				return true;
			}
		}

		public bool UpdateIgComm(CMSMStruct.CommStruct cos)
		{
			int recount=opa.UpdateIgComm(cos);
			if(recount<=0)
			{
				return false;
			}
			else
			{
				return true;
			}
		}

		public bool UpdateFillPromComm(Hashtable htfp)
		{
			int recount=opa.UpdateFillPromComm(htfp);
			if(recount<=0)
			{
				return false;
			}
			else
			{
				return true;
			}
		}

		public bool UpdateIOTimeComm(CMSMStruct.CommStruct cos)
		{
			int recount=opa.UpdateIOTimeComm(cos);
			if(recount<=0)
			{
				return false;
			}
			else
			{
				return true;
			}
		}

		public bool UpdateOperPurview(string strOperID,ArrayList alfunc,string strFuncType)
		{
			int recount=opa.UpdateOperPurview(strOperID,alfunc,strFuncType);
			if(recount<=0)
			{
				return false;
			}
			else
			{
				return true;
			}
		}

		public DataTable GetDeptManageInfo()
		{
			DataTable dtout=opa.GetDeptManageInfo();
            if (dtout != null && dtout.Rows.Count > 0)
            {
                dtout.Columns.Add("vcRegion");
                dtout.Columns.Add("vcTel");
                foreach (DataRow dr in dtout.Rows)
                {
                    string strComment = dr["vcComments"].ToString();
                    string[] strComments = strComment.Split('|');
                    if (strComments.Length > 1)
                    {
                        dr["vcRegion"] = strComments[1];
                    }
                    if (strComments.Length > 2)
                    {
                        dr["vcTel"] = strComments[2];
                    }
                }
            }
			return dtout;
		}

		public bool InsertDeptManageInfo(Hashtable htpara)
		{
			int recount=opa.InsertDeptManageInfo(htpara);
			if(recount<=0)
			{
				return false;
			}
			else
			{
				return true;
			}
		}

		public bool UpdateDeptManageInfo(Hashtable htpara)
		{
			int recount=opa.UpdateDeptManageInfo(htpara);
			if(recount<=0)
			{
				return false;
			}
			else
			{
				return true;
			}
		}

		public int IsExsitDeptInfo(string strClientID,string strNewID)
		{
			int DeptCount=opa.IsExsitDeptInfo(strClientID,strNewID);
			return DeptCount;
		}

		public DataTable GetClientOper(Hashtable htpara)
		{
			DataTable dtout=opa.GetClientOper(htpara);
			if(dtout!=null)
			{
				dtout.Columns["vcOperID"].ColumnName="�ͻ��˲���Ա���";
				dtout.Columns["vcOperName"].ColumnName="�ͻ��˲���Ա����";
				dtout.Columns["vcLimit"].ColumnName="Ȩ��";
				dtout.Columns["vcDeptID"].ColumnName="�ŵ�";
				dtout.Columns["vcActiveFlag"].ColumnName="״̬";
				dtout.Columns["vcPwdBeginFlag"].ColumnName="�����ʼ����־";
				dtout.Columns.Add("����Ȩ��");
				for(int i=0;i<dtout.Rows.Count;i++)
				{
					dtout.Rows[i]["����Ȩ��"]="<a href='wfmOperPurview.aspx?FuncType=CS&id=" + dtout.Rows[i]["�ͻ��˲���Ա���"].ToString() + "&name="+dtout.Rows[i]["�ͻ��˲���Ա����"].ToString()+"'>�޸�Ȩ��</a>";
				}
				dtout.Columns.Add("����");
				for(int i=0;i<dtout.Rows.Count;i++)
				{
					dtout.Rows[i]["����"]="<a href='wfmClientOperDetail.aspx?id=" + dtout.Rows[i]["�ͻ��˲���Ա���"].ToString() + "'>�༭</a>";
				}
			}
			return dtout;
		}

		public CMSMStruct.ClientOperStruct GetClientOperInfo(string strOperid)
		{
			DataTable dtout=opa.GetClientOperInfo(strOperid);
			CMSMStruct.ClientOperStruct coper1=new CommCenter.CMSMStruct.ClientOperStruct();
			if(dtout!=null)
			{
				coper1.strOperID=dtout.Rows[0]["vcOperID"].ToString();
				coper1.strOperName=dtout.Rows[0]["vcOperName"].ToString();
				coper1.strLimit=dtout.Rows[0]["vcLimit"].ToString();
				coper1.strPwd=dtout.Rows[0]["vcPwd"].ToString();
				coper1.strDeptID=dtout.Rows[0]["vcDeptID"].ToString();
				coper1.strActiveFlag=dtout.Rows[0]["vcActiveFlag"].ToString();
				coper1.strPwdBeginFlag=dtout.Rows[0]["vcPwdBeginFlag"].ToString();
			}
			else
			{
				coper1=null;
			}
			return coper1;
		}

		public bool ChkClientOperIDDup(string strOperID)
		{
			string strid=opa.ChkClientOperIDDup(strOperID);
			if(strid!="0")
			{
				return false;
			}

			return true;
		}

		public bool ChkClientOperNameDup(string strOperName,string strDeptID)
		{
			string strname=opa.ChkClientOperNameDup(strOperName,strDeptID);
			if(strname!="0")
			{
				return false;
			}

			return true;
		}

		public bool InsertClientOper(CMSMStruct.ClientOperStruct copernew)
		{
			int recount=opa.InsertClientOper(copernew);
			if(recount<=0)
			{
				return false;
			}

			return true;
		}
		public bool InsertOperLog(CMSMStruct.OperStruct OperNew)
		{
			int recount=opa.InsertOperLog(OperNew);
			if(recount<=0)
			{
				return false;
			}

			return true;
		}

		public bool UpdateClientOper(CMSMStruct.ClientOperStruct copernew,CMSMStruct.ClientOperStruct coperold)
		{
			string sqlset="";
			if(copernew.strOperName!=coperold.strOperName)
			{
				sqlset+="vcOperName='" + copernew.strOperName + "',";
			}
			if(copernew.strLimit!=coperold.strLimit)
			{
				sqlset+="vcLimit='" + copernew.strLimit + "',";
			}
			if(copernew.strDeptID!=coperold.strDeptID)
			{
				sqlset+="vcDeptID='" + copernew.strDeptID + "',";
			}

			if(sqlset!="")
			{
				sqlset=sqlset.Substring(0,sqlset.Length-1);
				int recount=opa.UpdateClientOper(coperold.strOperID,sqlset);
				if(recount<=0)
				{
					return false;
				}
			}
			
			return true;
		}

		public bool UpdateClientOperPwdBegin(string strOperID)
		{
			int recount=opa.UpdateClientOperPwdBegin(strOperID);
			if(recount<=0)
			{
				return false;
			}

			return true;
		}

		public bool UpdateClientOperFreeze(string strOperID,string strState)
		{
			int recount=opa.UpdateClientOperFreeze(strOperID,strState);
			if(recount<=0)
			{
				return false;
			}

			return true;
		}
		public void ConfirmConsItem(ArrayList al)
		{
			try
			{
				opa.ConfirmConsItem(al);
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}

		public DataTable GetPackages(string strPackageId)
		{
			DataTable dtout=opa.GetPackages(strPackageId);
			if(dtout!=null)
			{
				dtout.Columns["vcPackageId"].ColumnName="�ײͱ��";
				dtout.Columns["vcPackageName"].ColumnName="�ײ�����";
				dtout.Columns["nPackagePrice"].ColumnName="�ײ͵���";		
				dtout.Columns["vcGoodsID"].ColumnName="��Ʒ���";
				dtout.Columns["vcGoodsName"].ColumnName="��Ʒ����";				
				dtout.Columns["nGoodsPrice"].ColumnName="��Ʒ����";				
				dtout.Columns["vcComments"].ColumnName="����";
				dtout.Columns.Add("����");
				for(int i=0;i<dtout.Rows.Count;i++)
				{
					dtout.Rows[i]["����"]="<a href='wfmPackagesDetail.aspx?OperFlag=modify&vcPackageId="+dtout.Rows[i]["�ײͱ��"].ToString()
						+"&vcPackageName=" + dtout.Rows[i]["�ײ�����"].ToString() 
						+"&nPackagePrice=" + dtout.Rows[i]["�ײ͵���"].ToString() 
						+"&vcGoodsId=" + dtout.Rows[i]["��Ʒ���"].ToString() 
						+"&vcGoodsName=" + dtout.Rows[i]["��Ʒ����"].ToString() 
						+"&nGoodsPrice=" + dtout.Rows[i]["��Ʒ����"].ToString() 
						+"&vcComments=" + dtout.Rows[i]["����"].ToString() 
						+ "'>�༭</a>";
				}
			}
			return dtout;
		}
		
		public DataTable GetPackageInfo(string strId,string strName)
		{
			DataTable dtout=opa.GetPackageInfo(strId,strName);
			if(dtout!=null)
			{
				dtout.Columns["vcPackageId"].ColumnName="�ײͱ��";
				dtout.Columns["vcPackageName"].ColumnName="�ײ�����";
				dtout.Columns["nPackagePrice"].ColumnName="�ײ͵���";		
				dtout.Columns["vcGoodsID"].ColumnName="��Ʒ���";
				dtout.Columns["vcGoodsName"].ColumnName="��Ʒ����";				
				dtout.Columns["nGoodsPrice"].ColumnName="��Ʒ����";				
				dtout.Columns["vcComments"].ColumnName="����";
				dtout.Columns.Add("����");
				for(int i=0;i<dtout.Rows.Count;i++)
				{
					dtout.Rows[i]["����"]="<a href='wfmPackagesDetail.aspx?OperFlag=modify&vcPackageId="+dtout.Rows[i]["�ײͱ��"].ToString()
						+"&vcPackageName=" + dtout.Rows[i]["�ײ�����"].ToString() 
						+"&nPackagePrice=" + dtout.Rows[i]["�ײ͵���"].ToString() 
						+"&vcGoodsId=" + dtout.Rows[i]["��Ʒ���"].ToString() 
						+"&vcGoodsName=" + dtout.Rows[i]["��Ʒ����"].ToString() 
						+"&nGoodsPrice=" + dtout.Rows[i]["��Ʒ����"].ToString() 
						+"&vcComments=" + dtout.Rows[i]["����"].ToString() 
						+ "'>�༭</a>";
				}
			}
			return dtout;
		}

		public void AddPackage(PackagesStruct ps)
		{
			try
			{
				opa.AddPackage(ps);
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}
		public void UpdatePackage(PackagesStruct ps)
		{
			try
			{
				opa.UpdatePackage(ps);
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}
		public void DeletePackage(PackagesStruct ps)
		{
			try
			{
				opa.DeletePackage(ps);
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}

		public DataTable GetPackagesGoods(string strPackageId,string strGoodsId,string strGoodsName)
		{
			try
			{
				DataTable dtOut=opa.GetPackagesGoods(strPackageId,strGoodsId,strGoodsName);
				return dtOut;
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}

        public DataTable GetGoodsDeptPrice(string strId, string strName,string strDeptId)
        {
            DataTable dtout = opa.GetGoodsDeptPrice(strId, strName,strDeptId);
            if (dtout != null)
            {
                dtout.Columns["vcDeptId"].ColumnName = "�ŵ���";
                dtout.Columns["vcDeptName"].ColumnName = "�ŵ�����";
                dtout.Columns["vcGoodsID"].ColumnName = "��Ʒ���";
                dtout.Columns["vcGoodsName"].ColumnName = "��Ʒ����";
                dtout.Columns["nPrice"].ColumnName = "��Ʒ����";
                dtout.Columns["nDeptPrice"].ColumnName = "��Ʒ�ŵ굥��";
                dtout.Columns.Add("����");
                for (int i = 0; i < dtout.Rows.Count; i++)
                {
                    dtout.Rows[i]["����"] = "<a href='wfmGoodsDeptPriceDetail.aspx?OperFlag=modify&vcDeptId="
                        + dtout.Rows[i]["�ŵ���"].ToString()
                        + "&vcDeptName=" + dtout.Rows[i]["�ŵ�����"].ToString()
                        + "&vcGoodsId=" + dtout.Rows[i]["��Ʒ���"].ToString()
                        + "&vcGoodsName=" + dtout.Rows[i]["��Ʒ����"].ToString()
                        + "&nDeptPrice=" + dtout.Rows[i]["��Ʒ�ŵ굥��"].ToString()
                        + "&nPrice=" + dtout.Rows[i]["��Ʒ����"].ToString()
                        + "'>�༭</a>";
                }
            }
            return dtout;
        }
        public DataTable GetGoodsDeptPrice(string strGoodsId)
        {
            DataTable dtout = opa.GetGoodsDeptPrice(strGoodsId);
            if (dtout != null)
            {
                dtout.Columns["vcDeptId"].ColumnName = "�ŵ���";
                dtout.Columns["vcDeptName"].ColumnName = "�ŵ�����";
                dtout.Columns["vcGoodsID"].ColumnName = "��Ʒ���";
                dtout.Columns["vcGoodsName"].ColumnName = "��Ʒ����";
                dtout.Columns["nDeptPrice"].ColumnName = "��Ʒ�ŵ굥��";
                dtout.Columns["nPrice"].ColumnName = "��Ʒ����";
                dtout.Columns.Add("����");
                for (int i = 0; i < dtout.Rows.Count; i++)
                {
                    dtout.Rows[i]["����"] = "<a href='wfmGoodsDeptPriceDetail.aspx?OperFlag=modify&vcDeptId=" 
                        + dtout.Rows[i]["�ŵ���"].ToString()
                        + "&vcGoodsId=" + dtout.Rows[i]["��Ʒ���"].ToString()
                        + "&vcGoodsName=" + dtout.Rows[i]["��Ʒ����"].ToString()
                        + "&nPrice=" + dtout.Rows[i]["��Ʒ����"].ToString()
                        + "&nDeptPrice=" + dtout.Rows[i]["��Ʒ�ŵ굥��"].ToString()
                        + "'>�༭</a>";
                }
            }
            return dtout;
        }
        public void AddGoodsDeptPrice(string strDeptId,string strGoodsId,string nPrice)
        {
            try
            {
                opa.AddGoodsDeptPrice(strDeptId,strGoodsId,nPrice);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void UpdateGoodsDeptPrice(string strDeptId, string strGoodsId, string nPrice)
        {
            try
            {
                opa.UpdateGoodsDeptPrice(strDeptId,strGoodsId,nPrice);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void DeleteGoodsDeptPrice(string strDeptId, string strGoodsId)
        {
            try
            {
                opa.DeleteGoodsDeptPrice(strDeptId,strGoodsId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetGoodsDept(string strGoodsId, string strGoodsName, string strPrice)
        {
            DataTable dtout = opa.GetGoodsDept(strGoodsId,strGoodsName,strPrice);
            return dtout;
        }
        public void AddGoodsDept(string strDeptId, string strGoodsId, string nPrice)
        {
            try
            {
                opa.AddGoodsDept(strDeptId, strGoodsId, nPrice);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void DeleteGoodsDept(string strDeptId, string strGoodsId)
        {
            try
            {
                opa.DeleteGoodsDept(strDeptId, strGoodsId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void LossCard(string strCardId, string strAssId, string strOperName, string strDeptId)
        {
            try
            {
                opa.LossCard(strCardId,strAssId,strOperName,strDeptId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void CancelLossCard(string strCardId, string strAssId, string strOperName, string strDeptId)
        {
            try
            {
                opa.CancelLossCard(strCardId, strAssId, strOperName, strDeptId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
	}
}
