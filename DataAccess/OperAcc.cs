using System;
using System.Data;
using System.Data.SqlClient;
using CommCenter;
using System.Collections;

namespace DataAccess
{
	/// <summary>
	/// Summary description for OperAcc.
	/// </summary>
	public class OperAcc
	{

		SqlDataReader drr;
		SqlConnection con;
		AMSLog clog=new AMSLog();

		public OperAcc(string strcons)
		{
			//
			// TODO: Add constructor logic here
			//
			con=new SqlConnection(strcons);
		}

		public DataTable GetLoginInfo(string strLoginid)
		{
			string strsql="select * from tbLogin where vcLoginID='" + strLoginid + "'";
			DataTable dtout=SqlHelper.ExecuteDataTable(con,CommandType.Text,strsql);
			if(con.State==ConnectionState.Open)
			{
				con.Close();
			}
			
			return dtout;
		}

		public DataTable GetGoods(string strGoodsID,string strGoodsName)
		{
			DataTable dtGoods=new DataTable();
			try
			{
				string strCondition="";
				if(strGoodsID!=""&&strGoodsID!="*")
				{
					strCondition=" vcGoodsID like '" + strGoodsID + "%'";
				}
				if(strGoodsName!=""&&strGoodsName!="*")
				{
					if(strCondition=="")
					{
						strCondition=" vcGoodsName like '%" + strGoodsName + "%'";
					}
					else
					{
						strCondition=strCondition + " and vcGoodsName='" + strGoodsName + "'";
					}
				}
				string sql1= @"select vcGoodsID,vcGoodsName,vcSpell,nPrice,iIgValue,vcComments,
case when IsPackage=0 then '否' else '是' end 是否套餐,
case when IsNew=1 then '是' else '否' end 是否新品,
case when IsKey=1 then '是' else '否' end 是否重点产品,Unit,
convert(char(10),NewDate,121) 上市时间,
case when IsDeptPrice=1 then '是' else '否' end 是否门店单价 from tbGoods";
				if(strCondition!="")
				{
					sql1=sql1 + " where vcComments<>'0' and   " + strCondition + " order by vcGoodsID";
				}
				else
				{
					sql1=sql1 + " where  vcComments<>'0' ";
				}
				dtGoods=SqlHelper.ExecuteDataTable(con,CommandType.Text,sql1);
			}
			catch (Exception e) 
			{
				clog.WriteLine(e);
				return null;
			}
			finally
			{
				if(con.State==ConnectionState.Open)
				{
					con.Close();
				}
			}
			return dtGoods;
		}

		public DataTable GetGoodsInfo(string strGoodsID)
		{
			DataTable dtGoods=new DataTable();
			try
			{
                string sql1 = "select vcGoodsID,vcGoodsName,vcSpell,nPrice,iIgValue,vcComments,IsPackage,isnull(IsNew,0) as IsNew,isnull(IsKey,0) as IsKey,Unit,convert(char(10),NewDate,121) as NewDate,IsDeptPrice from tbGoods where vcGoodsID='" + strGoodsID + "'";
				dtGoods=SqlHelper.ExecuteDataTable(con,CommandType.Text,sql1);
			}
			catch (Exception e) 
			{
				clog.WriteLine(e);
				return null;
			}
			finally
			{
				if(con.State==ConnectionState.Open)
				{
					con.Close();
				}
			}
			return dtGoods;
		}
		
		public string getGoodsID(string strGoodsID)
		{
			string strsql="select count(*) from tbGoods where vcGoodsID='" + strGoodsID + "'";
			drr=SqlHelper.ExecuteReader(con,CommandType.Text,strsql);
			drr.Read();
			string strid=drr[0].ToString();
			drr.Close();
			if(con.State==ConnectionState.Open)
			{
				con.Close();
			}
			
			return strid;
		}

		public string getMaxGoodsID(string mask)
		{
			string strsql="select isnull(max(cast(vcGoodsID as numeric(10,0))),0) from tbGoods where vcGoodsID like '"+mask+"%'";
			drr=SqlHelper.ExecuteReader(con,CommandType.Text,strsql);
			drr.Read();
			string strid=drr[0].ToString();
			drr.Close();
			if(con.State==ConnectionState.Open)
			{
				con.Close();
			}
			
			return strid;
		}

		public string getGoodsName(string strGoodsName)
		{
			string strsql="select count(*) from tbGoods where vcGoodsName='" + strGoodsName + "'";
			drr=SqlHelper.ExecuteReader(con,CommandType.Text,strsql);
			drr.Read();
			string strname=drr[0].ToString();
			drr.Close();
			if(con.State==ConnectionState.Open)
			{
				con.Close();
			}
			
			return strname;
		}

		public string getGoodsNamebyID(string strGoodsName,string strGoodsID)
		{
			string strsql="select count(*) from tbGoods where vcGoodsName='" + strGoodsName + "' and vcGoodsID<>'" + strGoodsID + "'";
			drr=SqlHelper.ExecuteReader(con,CommandType.Text,strsql);
			drr.Read();
			string strname=drr[0].ToString();
			drr.Close();
			if(con.State==ConnectionState.Open)
			{
				con.Close();
			}
			
			return strname;
		}

		public int InsertGoods(CMSMStruct.GoodsStruct gs)
		{
			int recount=0;
			string strGoodsID="";
			string strsql="select isnull(max(cast(vcGoodsID as numeric(10,0))),0) as maxid from tbGoods where vcGoodsID like '8%'";
			drr=SqlHelper.ExecuteReader(con,CommandType.Text,strsql);
			drr.Read();
			int strmaxid=int.Parse(drr[0].ToString());
			drr.Close();
			if(strmaxid==0)
			{
				strGoodsID="8001";
			}
			else if(strmaxid>=8999)
			{
				return 0;
			}
			else
			{
				strGoodsID=(strmaxid+1).ToString();
				string strPackage = "0";
				if(gs.bPackage) strPackage="1";

                string fields = "vcGoodsId,vcGoodsName,vcSpell,nPrice,nRate,iIgValue,cNewFlag,vcComments,IsPackage";
                
                string values = "'" + strGoodsID + "','" + gs.strGoodsName + "','" + gs.strSpell + "'," + gs.dPrice.ToString() + ",0," + gs.iIgValue.ToString() + ",'0','" + gs.strComments + "'," + strPackage;

                if (gs.bNew)
                {
                    fields += ",IsNew";
                    values += ",1";
                }
                if (gs.bKey)
                {
                    fields += ",IsKey";
                    values += ",1";
                }
                if (!string.IsNullOrEmpty(gs.Unit))
                {
                    fields += ",Unit";
                    values += ",'" + gs.Unit + "'";
                }
                if (!string.IsNullOrEmpty(gs.NewDate))
                {
                    fields += ",NewDate";
                    values += ",'" + gs.NewDate + "'";
                }
                if (gs.bDeptPrice)
                {
                    fields += ",IsDeptPrice";
                    values += ",1" ;
                }
                string sql1="insert into tbGoods({0}) values({1})";
                recount = SqlHelper.ExecuteNonQuery(con, CommandType.Text, string.Format(sql1, fields, values));
			}

			if(con.State==ConnectionState.Open)
			{
				con.Close();
			}
			return recount;
		}

		public int UpdateGoods(string strGoodsID,string strsqlset)
		{
			string sql1="update tbGoods set " + strsqlset + " where vcGoodsID='" + strGoodsID + "'";
			int recount=SqlHelper.ExecuteNonQuery(con,CommandType.Text,sql1);
			if(con.State==ConnectionState.Open)
			{
				con.Close();
			}
			return recount;
		}

		public string getGoodsSerial(string strCreateDate)
		{
			string strsql="select isnull(max(cast(substring(vcFileName,14,2) as int)),0) from tbDataSoftUpdateLog where Type='GOODS' and vcFileName like 'CEN00" + strCreateDate + "%'";
			drr=SqlHelper.ExecuteReader(con,CommandType.Text,strsql);
			drr.Read();
			string strserial=drr[0].ToString();
			drr.Close();
			if(con.State==ConnectionState.Open)
			{
				con.Close();
			}
			
			return strserial;
		}

		public string getNotiSerial(string strCreateDate)
		{
			string strsql="select isnull(max(cast(substring(vcFileName,14,2) as int)),0) from tbDataSoftUpdateLog where Type='NOTI' and vcFileName like 'CEN00" + strCreateDate + "%'";
			drr=SqlHelper.ExecuteReader(con,CommandType.Text,strsql);
			drr.Read();
			string strserial=drr[0].ToString();
			drr.Close();
			if(con.State==ConnectionState.Open)
			{
				con.Close();
			}
			
			return strserial;
		}

		public string getAssSerial(string strCreateDate)
		{
			string strsql="select isnull(max(cast(substring(vcFileName,14,2) as int)),0) from tbDataSoftUpdateLog where Type='CEN' and vcFileName like '%CEN00" + strCreateDate + "%'";
			drr=SqlHelper.ExecuteReader(con,CommandType.Text,strsql);
			drr.Read();
			string strserial=drr[0].ToString();
			drr.Close();
			if(con.State==ConnectionState.Open)
			{
				con.Close();
			}
			
			return strserial;
		}

		public int InsertDataLog(string strsqlset)
		{
			string sql1="insert into tbDataSoftUpdateLog values(" + strsqlset + ")";
			int recount=SqlHelper.ExecuteNonQuery(con,CommandType.Text,sql1);
			if(con.State==ConnectionState.Open)
			{
				con.Close();
			}
			return recount;
		}

		public ArrayList DownSysPara()
		{
			DataTable dtPara=new DataTable();
			ArrayList alPara=new ArrayList();
			string sql1="select * from tbCommCode where vcCommSign<>'LOCAL' order by vcCommSign,vcCommCode";
			dtPara=SqlHelper.ExecuteDataTable(con,CommandType.Text,sql1);
			for(int i=0;i<dtPara.Rows.Count;i++)
			{
				CMSMStruct.CommStruct paratmp=new CMSMStruct.CommStruct();
				paratmp.strCommName=dtPara.Rows[i]["vcCommName"].ToString();
				paratmp.strCommCode=dtPara.Rows[i]["vcCommCode"].ToString();
				paratmp.strCommSign=dtPara.Rows[i]["vcCommSign"].ToString();
				paratmp.strComments=dtPara.Rows[i]["vcComments"].ToString();
				alPara.Add(paratmp);
			}
			if(con.State==ConnectionState.Open)
			{
				con.Close();
			}

			return alPara;
		}

		public ArrayList DownGoodsData()
		{
			DataTable dtAss=new DataTable();
			ArrayList alAss=new ArrayList();
			string sql1="select * from tbGoods order by vcGoodsID";
			dtAss=SqlHelper.ExecuteDataTable(con,CommandType.Text,sql1);

			for(int i=0;i<dtAss.Rows.Count;i++)
			{
				CMSMStruct.GoodsStruct asstmp=new CMSMStruct.GoodsStruct();
				asstmp.strGoodsID=dtAss.Rows[i]["vcGoodsID"].ToString();
				asstmp.strGoodsName=dtAss.Rows[i]["vcGoodsName"].ToString();
				asstmp.strSpell=dtAss.Rows[i]["vcSpell"].ToString();
				asstmp.dPrice=double.Parse(dtAss.Rows[i]["nPrice"].ToString());
				asstmp.dRate=double.Parse(dtAss.Rows[i]["nRate"].ToString());
				asstmp.iIgValue=int.Parse(dtAss.Rows[i]["iIgValue"].ToString());
				asstmp.strNewFlag=dtAss.Rows[i]["cNewFlag"].ToString();
				asstmp.strComments=dtAss.Rows[i]["vcComments"].ToString();
				alAss.Add(asstmp);
			}

			return alAss;
		}

		public ArrayList DownNotice(string strid)
		{
			DataTable dtnoti=new DataTable();
			ArrayList alnoti=new ArrayList();
			string sql1="select * from tbNotice where id='"+strid+"'";
			dtnoti=SqlHelper.ExecuteDataTable(con,CommandType.Text,sql1);

			for(int i=0;i<dtnoti.Rows.Count;i++)
			{
				CMSMStruct.NoticeStruct notitmp=new CMSMStruct.NoticeStruct();
				notitmp.strid=dtnoti.Rows[i]["id"].ToString();
				notitmp.strComments=dtnoti.Rows[i]["vcComments"].ToString();
				notitmp.strCreateDate=dtnoti.Rows[i]["dtCreateDate"].ToString();
				notitmp.strActiveFlag=dtnoti.Rows[i]["vcActiveFlag"].ToString();
				notitmp.strDeptFlag=dtnoti.Rows[i]["vcDeptFlag"].ToString();
				alnoti.Add(notitmp);
			}

			return alnoti;
		}

		public ArrayList DownAssData(string strBeginDate)
		{
			DataTable dtAss=new DataTable();
			ArrayList alAss=new ArrayList();
			try
			{
				string sql1="select * from tbAssociator where vcCardID<>'V9999' and vcAssType<>'AT999' and dtOperDate>='" + strBeginDate + "' order by iAssID,vcCardID";
				dtAss=SqlHelper.ExecuteDataTable(con,CommandType.Text,sql1);

				for(int i=0;i<dtAss.Rows.Count;i++)
				{
					CMSMStruct.AssociatorStruct asstmp=new CMSMStruct.AssociatorStruct();
					asstmp.strAssID=dtAss.Rows[i]["iAssID"].ToString();
					asstmp.strCardID=dtAss.Rows[i]["vcCardID"].ToString();
					asstmp.strAssName=dtAss.Rows[i]["vcAssName"].ToString();
					asstmp.strSpell=dtAss.Rows[i]["vcSpell"].ToString();
					asstmp.strAssNbr=dtAss.Rows[i]["vcAssNbr"].ToString();
					asstmp.strLinkPhone=dtAss.Rows[i]["vcLinkPhone"].ToString();
					asstmp.strLinkAddress=dtAss.Rows[i]["vcLinkAddress"].ToString();
					asstmp.strEmail=dtAss.Rows[i]["vcEmail"].ToString();
					asstmp.strAssType=dtAss.Rows[i]["vcAssType"].ToString();
					asstmp.strAssState=dtAss.Rows[i]["vcAssState"].ToString();
					asstmp.dCharge=Double.Parse(dtAss.Rows[i]["nCharge"].ToString());
					asstmp.iIgValue=int.Parse(dtAss.Rows[i]["iIgValue"].ToString());
					asstmp.strCardFlag=dtAss.Rows[i]["vcCardFlag"].ToString();
					asstmp.strComments=dtAss.Rows[i]["vcComments"].ToString();
					asstmp.strCreateDate=dtAss.Rows[i]["dtCreateDate"].ToString();
					asstmp.strOperDate=dtAss.Rows[i]["dtOperDate"].ToString();
					asstmp.strDeptID=dtAss.Rows[i]["vcDeptID"].ToString();
					alAss.Add(asstmp);
				}
			}
			catch(Exception e)
			{
				clog.WriteLine(e);
				return null;
			}
			finally
			{
				con.Close();
			}
			return alAss;
		}

		public DataTable GetLoginOper(Hashtable htPara)
		{
			DataTable dtLogin=new DataTable();
			try
			{
				string strCondition="";
				if(htPara["strLoginName"].ToString()!=""&&htPara["strLoginName"].ToString()!="*")
				{
					strCondition=" vcOperName like '%" + htPara["strLoginName"].ToString() + "%'";
				}
				if(htPara["strDeptID"].ToString()!=""&&htPara["strDeptID"].ToString()!="*")
				{
					if(strCondition=="")
					{
						strCondition=" vcDeptID like '" + htPara["strDeptID"].ToString() + "'";
					}
					else
					{
						strCondition=strCondition + " and vcDeptID='" + htPara["strDeptID"].ToString() + "'";
					}
				}
				string sql1="SELECT [vcLoginID], [vcOperName],[vcLimit],[vcDeptID] FROM [tbLogin]";
				if(strCondition!="")
				{
					sql1=sql1 + " where " + strCondition + " order by vcDeptID,vcOperName";
				}
				dtLogin=SqlHelper.ExecuteDataTable(con,CommandType.Text,sql1);
			}
			catch (Exception e) 
			{
				clog.WriteLine(e);
				return null;
			}
			finally
			{
				if(con.State==ConnectionState.Open)
				{
					con.Close();
				}
			}
			return dtLogin;
		}

		public string getLoginID(string strLoginID)
		{
			string strsql="select count(*) from tbLogin where vcLoginID='" + strLoginID + "'";
			drr=SqlHelper.ExecuteReader(con,CommandType.Text,strsql);
			drr.Read();
			string strid=drr[0].ToString();
			drr.Close();
			if(con.State==ConnectionState.Open)
			{
				con.Close();
			}
			
			return strid;
		}

		public string getOperName(string strOperName)
		{
			string strsql="select count(*) from tbLogin where vcOperName='" + strOperName + "'";
			drr=SqlHelper.ExecuteReader(con,CommandType.Text,strsql);
			drr.Read();
			string strname=drr[0].ToString();
			drr.Close();
			if(con.State==ConnectionState.Open)
			{
				con.Close();
			}
			
			return strname;
		}

		public int InsertLogin(CMSMStruct.LoginStruct lsnew)
		{
			string sql1="insert into tbLogin values('" + lsnew.strLoginID+"','"+lsnew.strOperName+"','"+lsnew.strLimit+"','123456','"+lsnew.strDeptID+"')";
			int recount=SqlHelper.ExecuteNonQuery(con,CommandType.Text,sql1);
			if(con.State==ConnectionState.Open)
			{
				con.Close();
			}
			return recount;
		}

		public int UpdateLogin(string strLoginID,string strsqlset)
		{
			string sql1="update tbLogin set " + strsqlset + " where vcLoginID='" + strLoginID + "'";
			int recount=SqlHelper.ExecuteNonQuery(con,CommandType.Text,sql1);
			if(con.State==ConnectionState.Open)
			{
				con.Close();
			}
			return recount;
		}
		
		public int DeleteLogin(string strLoginID)
		{
			string sql1="delete from tbLogin where vcLoginID='" + strLoginID + "'";
			int recount=SqlHelper.ExecuteNonQuery(con,CommandType.Text,sql1);
			if(con.State==ConnectionState.Open)
			{
				con.Close();
			}
			return recount;
		}

		public DataTable GetNotice(Hashtable htPara)
		{
			DataTable dtNotice=new DataTable();
			try
			{
				string strCondition="";
				if(htPara["strDeptID"].ToString()!=""&&htPara["strDeptID"].ToString()!="*")
				{
					strCondition=" vcDeptFlag in('" + htPara["strDeptID"].ToString() + "')";
				}
				if(htPara["strContent"].ToString()!=""&&htPara["strContent"].ToString()!="*")
				{
					if(strCondition=="")
					{
						strCondition=" vcComments like '%" + htPara["strContent"].ToString() + "%'";
					}
					else
					{
						strCondition=strCondition + " and vcComments='" + htPara["strContent"].ToString() + "'";
					}
				}
				if(strCondition!="")
				{
					strCondition=strCondition+" and ";
				}
				string sql1="select id,vcComments,dtCreateDate,(case vcDeptFlag when 'all' then '全部门店' else vcDeptFlag end) as vcDeptFlag from tbNotice where "+strCondition+" dtCreateDate between '"+htPara["strBegin"].ToString()+"' and '"+htPara["strEnd"].ToString()+" 23:59:59' order by id desc";
				dtNotice=SqlHelper.ExecuteDataTable(con,CommandType.Text,sql1);
			}
			catch (Exception e) 
			{
				clog.WriteLine(e);
				return null;
			}
			finally
			{
				if(con.State==ConnectionState.Open)
				{
					con.Close();
				}
			}
			return dtNotice;
		}

		public int InsertNotice(string strDept,string strContent)
		{
			string sql1="insert into tbNotice values('" + strContent+"',getdate(),'0','"+strDept+"')";
			int recount=SqlHelper.ExecuteNonQuery(con,CommandType.Text,sql1);
			if(con.State==ConnectionState.Open)
			{
				con.Close();
			}
			return recount;
		}

		public int UpdateNotice(string strid)
		{
			string sql1="update tbNotice set vcActiveFlag='2' where id='"+strid+"'";
			int recount=SqlHelper.ExecuteNonQuery(con,CommandType.Text,sql1);
			if(con.State==ConnectionState.Open)
			{
				con.Close();
			}
			return recount;
		}

		public string getNoticeActiveFlag(string strid)
		{
			string strsql="select vcActiveFlag from tbNotice where id='" + strid + "'";
			drr=SqlHelper.ExecuteReader(con,CommandType.Text,strsql);
			drr.Read();
			string strreturnid=drr[0].ToString();
			drr.Close();
			if(con.State==ConnectionState.Open)
			{
				con.Close();
			}
			
			return strreturnid;
		}

		public int UpdateOperPwd(string strid,string strpwd)
		{
			string sql1="update tbLogin set vcPwd='"+strpwd+"' where vcLoginID='"+strid+"'";
			int recount=SqlHelper.ExecuteNonQuery(con,CommandType.Text,sql1);
			if(con.State==ConnectionState.Open)
			{
				con.Close();
			}
			return recount;
		}

		public DataTable GetAllGoodsName()
		{
			DataTable dtGoods=new DataTable();
			try
			{
				string sql1="select vcGoodsName,cNewFlag from tbGoods";
				dtGoods=SqlHelper.ExecuteDataTable(con,CommandType.Text,sql1);
			}
			catch (Exception e) 
			{
				clog.WriteLine(e);
				return null;
			}
			finally
			{
				if(con.State==ConnectionState.Open)
				{
					con.Close();
				}
			}
			return dtGoods;
		}

		public DataTable GetIgPara()
		{
			DataTable dtIg=new DataTable();
			try
			{
				string sql1="select vcCommName,vcCommCode from tbCommCode where vcCommSign='IG'";
				dtIg=SqlHelper.ExecuteDataTable(con,CommandType.Text,sql1);
			}
			catch (Exception e) 
			{
				clog.WriteLine(e);
				return null;
			}
			finally
			{
				if(con.State==ConnectionState.Open)
				{
					con.Close();
				}
			}
			return dtIg;
		}

		public DataTable GetPromRate()
		{
			DataTable dtProm=new DataTable();
			try
			{
                string sql1 = "select * from tbCommCode where vcComments='充值赠款' and vcCommSign in ('FP1','FP2','FP3')";
				dtProm=SqlHelper.ExecuteDataTable(con,CommandType.Text,sql1);
			}
			catch (Exception e) 
			{
				clog.WriteLine(e);
				return null;
			}
			finally
			{
				if(con.State==ConnectionState.Open)
				{
					con.Close();
				}
			}
			return dtProm;
		}
        public int ExistPromRatio(string strCommName)
        {
            string sql = "select count(1) from tbCommCode where vcCommName='{0}' and vcCommSign='FP'";
            object obj = SqlHelper.ExecuteScalar(con, CommandType.Text, string.Format(sql, strCommName));
            int recount = 0;
            if (obj != null)
            {
                recount = Convert.ToInt32(obj);
            }
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            return recount;
        }
        public int DeletePromRatio(string strCommName, string strCommCode)
        {
            string sql = "delete from tbCommCode where vcCommName='{0}' and vcCommCode='{1}'and vcCommSign='FP'";
            int recount = SqlHelper.ExecuteNonQuery(con, CommandType.Text, string.Format(sql, strCommName,strCommCode));
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            return recount;
        }

        public int UpdatePromRatio(string strOldCommName, string strCommName, string strOldCommCode, string strCommCode)
        {
            string sql = "update tbCommCode set vcCommName='{1}',vcCommCode='{3}' where vcCommName='{0}' and vcCommCode='{2}' and vcCommSign='FP'";
            int recount = SqlHelper.ExecuteNonQuery(con, CommandType.Text, string.Format(sql, strOldCommName, strCommName, strOldCommCode, strCommCode));
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            return recount;
        }
        public int InsertPromRatio(string strCommName, string strCommCode)
        {
            string sql = "insert into tbCommCode(vcCommName,vcCommCode,vcCommSign,vcComments)values('{0}','{1}','FP','充值赠款')";
            int recount = SqlHelper.ExecuteNonQuery(con, CommandType.Text, string.Format(sql, strCommName,strCommCode));
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            return recount;
        }
        public DataTable GetPromRatio()
        {
            DataTable dtProm = new DataTable();
            try
            {
                string sql1 = "select * from tbCommCode where vcCommSign='FP'";
                dtProm = SqlHelper.ExecuteDataTable(con, CommandType.Text, sql1);
            }
            catch (Exception e)
            {
                clog.WriteLine(e);
                return null;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return dtProm;
        }
		public DataTable GetIOTime()
		{
			DataTable dttime=new DataTable();
			try
			{
				string sql1="select substring(vcCommName,1,5) as vcOfficer,substring(vcCommName,6,1) as vcClass,vcCommCode,vcCommSign from tbCommCode where vcComments='IOTime' order by vcOfficer,vcClass";
				dttime=SqlHelper.ExecuteDataTable(con,CommandType.Text,sql1);
			}
			catch (Exception e) 
			{
				clog.WriteLine(e);
				return null;
			}
			finally
			{
				if(con.State==ConnectionState.Open)
				{
					con.Close();
				}
			}
			return dttime;
		}

		public DataSet GetFuncList(string strLogionID,string strFuncType)
		{
			DataTable dttmp=new DataTable();
			DataSet dsout=new DataSet();
			try
			{
				string sql1 = "select cnvcFuncName,cnvcFuncAddress from tbFunc where cnvcFuncType='"+strFuncType+"' order by cnvcFuncParentName,cnvcFuncName";				
				dttmp=SqlHelper.ExecuteDataTable(con,CommandType.Text,sql1);
				dttmp.TableName="funclist";
				dsout.Tables.Add(dttmp);

				dttmp=null;
				//string sql2="select vcFuncName from tbOperFunc where vcOperID='"+strLogionID+"' order by vcFuncName";
				string sql2 = "SELECT a.vcFuncName FROM tbOperFunc a "
					+" LEFT JOIN tbFunc b ON a.vcfuncname = b.cnvcfuncname AND a.vcfuncaddress=b.cnvcfuncaddress "
					+" WHERE vcOperID='"+strLogionID+"' AND cnvcFuncType='"+strFuncType+"' "
					+" ORDER BY a.vcFuncName ";
				dttmp=SqlHelper.ExecuteDataTable(con,CommandType.Text,sql2);
				dttmp.TableName="operfunc";
				dsout.Tables.Add(dttmp);
			}
			catch (Exception e) 
			{
				clog.WriteLine(e);
				return null;
			}
			finally
			{
				if(con.State==ConnectionState.Open)
				{
					con.Close();
				}
			}
			return dsout;
		}

		public int UpdateGoodsNewFlag(ArrayList al)
		{
			string sqlset="";
			con.Open();
			using(SqlTransaction tran=con.BeginTransaction())
			{
				try
				{
					if(al.Count==0)
					{
						string sql1="update tbGoods set cNewFlag='0' where cNewFlag<>'0'";
						SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql1);

						tran.Commit();
					}
					else
					{
						for(int i=0;i<al.Count;i++)
						{
							sqlset+="'" + al[i].ToString() + "',";
						}
						sqlset=sqlset.Substring(0,sqlset.Length-1);

						string sql2="update tbGoods set cNewFlag='0' where cNewFlag<>'0'";
						SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql2);

						string sql3="update tbGoods set cNewFlag='1' where vcGoodsName in(" + sqlset + ")";
						SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql3);

						tran.Commit();
					}
				}
				catch (Exception e) 
				{
					tran.Rollback();
					clog.WriteLine(e);
					return 0;
				}
				finally
				{
					if(con.State==ConnectionState.Open)
					{
						con.Close();
					}
				}

				return 1;
			}
		}

		public int UpdateIgComm(CMSMStruct.CommStruct cos)
		{
			con.Open();
			using(SqlTransaction tran=con.BeginTransaction())
			{
				try
				{
					string sql1="select count(*) from tbCommCode where vcCommSign='IG'";
					SqlDataReader dr=SqlHelper.ExecuteReader(con,tran,CommandType.Text,sql1);
					dr.Read();
					string strCount=dr[0].ToString();
					dr.Close();

					string sql2="";
					if(strCount=="0")
					{
						sql2="insert into tbCommCode values('" + cos.strCommName + "','" + cos.strCommCode + "','" + cos.strCommSign + "','" + cos.strComments + "')";
					}
					else
					{
						sql2="update tbCommCode set vcCommName='" + cos.strCommName + "',vcCommCode='" + cos.strCommCode + "' where vcCommSign='IG'";
					}
					SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql2);

					tran.Commit();
				}
				catch (Exception e) 
				{
					tran.Rollback();
					clog.WriteLine(e);
					return 0;
				}
				finally
				{
					if(con.State==ConnectionState.Open)
					{
						con.Close();
					}
				}

				return 1;
			}
		}

		public int UpdateFillPromComm(Hashtable htp)
		{
			con.Open();
			using(SqlTransaction tran=con.BeginTransaction())
			{
				try
				{
					string sql1="";
					string sql2="";
					for(int i=1;i<=htp.Count;i++)
					{
						CMSMStruct.CommStruct cos=(CMSMStruct.CommStruct)htp["FP"+i];
						sql1="select count(*) from tbCommCode where vcCommSign='" + cos.strCommSign + "'";
						SqlDataReader drr=SqlHelper.ExecuteReader(con,tran,CommandType.Text,sql1);
						drr.Read();
						string strCount=drr[0].ToString();
						drr.Close();

						if(strCount=="0")
						{
							sql2="insert into tbCommCode values('" + cos.strCommName + "','" + cos.strCommCode + "','" + cos.strCommSign + "','" + cos.strComments + "')";
						}
						else
						{
							sql2="update tbCommCode set vcCommCode='" + cos.strCommCode + "' where vcCommSign='" + cos.strCommSign + "'";
						}
						SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql2);
					}
					
					tran.Commit();
				}
				catch (Exception e) 
				{
					tran.Rollback();
					clog.WriteLine(e);
					return 0;
				}
				finally
				{
					if(con.State==ConnectionState.Open)
					{
						con.Close();
					}
				}

				return 1;
			}
		}

		public int UpdateIOTimeComm(CMSMStruct.CommStruct cos)
		{
			con.Open();
			using(SqlTransaction tran=con.BeginTransaction())
			{
				try
				{
					string sql1="select count(*) from tbCommCode where vcCommName='" + cos.strCommName + "'";
					SqlDataReader drr=SqlHelper.ExecuteReader(con,tran,CommandType.Text,sql1);
					drr.Read();
					string strCount=drr[0].ToString();
					drr.Close();

					string sql2="";
					if(strCount=="0")
					{
						sql2="insert into tbCommCode values('" + cos.strCommName + "','" + cos.strCommCode + "','" + cos.strCommSign + "','" + cos.strComments + "')";
					}
					else
					{
						sql2="update tbCommCode set vcCommCode='" + cos.strCommCode + "',vcCommSign='" + cos.strCommSign + "' where vcCommName='" + cos.strCommName + "'";
					}
					SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql2);
					
					tran.Commit();
				}
				catch (Exception e) 
				{
					tran.Rollback();
					clog.WriteLine(e);
					return 0;
				}
				finally
				{
					if(con.State==ConnectionState.Open)
					{
						con.Close();
					}
				}

				return 1;
			}
		}

		public int UpdateOperPurview(string strOperID,ArrayList alfunc,string strFuncType)
		{
			con.Open();
			using(SqlTransaction tran=con.BeginTransaction())
			{
				try
				{
					string sql1="delete from tbOperFunc where vcOperID='"+strOperID+"'";
					sql1 += " and vcFuncAddress IN(SELECT cnvcFuncAddress FROM tbFunc WHERE cnvcFuncType = '"+strFuncType+"')";																 
					SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql1);

					string sql2="";
					for(int i=0;i<alfunc.Count;i++)
					{
						CMSMStruct.MenuStruct ms=(CMSMStruct.MenuStruct)alfunc[i];
						sql2="insert into tbOperFunc values('"+strOperID+"','"+ms.strFuncName+"','"+ms.strFuncAddress+"')";
						SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql2);
					}

					tran.Commit();
				}
				catch (Exception e) 
				{
					tran.Rollback();
					clog.WriteLine(e);
					return 0;
				}
				finally
				{
					if(con.State==ConnectionState.Open)
					{
						con.Close();
					}
				}

				return 1;
			}
		}

		public DataTable GetDeptManageInfo()
		{
			DataTable dttmp=new DataTable();
			try
			{
                string sql1 = "select a.vcCommName,a.vcCommCode,a.vcComments,c.cnvcDeptName,c.cnvcDeptID,c.cnnPriority from tbCommCode a,tbDeptMapInfo b,tbDept c where a.vcCommSign='MD' and a.vcCommCode=b.cnvcOldDeptID and b.cnvcNewDeptID=c.cnvcDeptID order by c.cnnPriority";
				dttmp=SqlHelper.ExecuteDataTable(con,CommandType.Text,sql1);
			}
			catch (Exception e) 
			{
				clog.WriteLine(e);
				return null;
			}
			finally
			{
				if(con.State==ConnectionState.Open)
				{
					con.Close();
				}
			}
			return dttmp;
		}

		public int InsertDeptManageInfo(Hashtable htpara)
		{
			con.Open();
			using(SqlTransaction tran=con.BeginTransaction())
			{
				try
				{
					string sql1="insert into tbCommCode(vcCommName,vcCommCode,vcCommSign,vcComments) values('"+htpara["strClientName"].ToString()+"','"+htpara["strClientID"].ToString()+"','MD','门店')";
					SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql1);

					string sql2="insert into tbDept(cnvcDeptName,cnvcDeptID,cnvcDeptType,cnvcParentDeptID,cnvcComments,cnnPriority) values('"+htpara["strNewName"].ToString()+"','"+htpara["strNewID"].ToString()+"','SalesRoom','BreadWorkShop','门店',"+htpara["strSortNum"].ToString()+")";
					SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql2);

					string sql3="insert into tbDeptMapInfo(cnvcOldDeptID,cnvcNewDeptID) values('"+htpara["strClientID"].ToString()+"','"+htpara["strNewID"].ToString()+"')";
					SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql3);

					tran.Commit();
				}
				catch (Exception e) 
				{
					tran.Rollback();
					clog.WriteLine(e);
					return 0;
				}
				finally
				{
					if(con.State==ConnectionState.Open)
					{
						con.Close();
					}
				}

				return 1;
			}
		}

		public int UpdateDeptManageInfo(Hashtable htpara)
		{
			con.Open();
			using(SqlTransaction tran=con.BeginTransaction())
			{
				try
				{
                    string strRegion = htpara["strRegion"].ToString();
                    string strTel = htpara["strTel"].ToString();
                    //if (string.IsNullOrEmpty(strRegion))
                    //{
                    //    strRegion = "门店";
                    //}
                    //if (!strRegion.StartsWith("门店"))
                    //{
                    //    strRegion = "门店" + strRegion;
                    //}

                    string strComment = "门店|"+strRegion+"|"+strTel;
					string sql1="update tbCommCode set vcCommName='"+htpara["strClientName"].ToString()+"',vcComments='"+strComment+"' where vcCommSign='MD' and vcCommCode='"+htpara["strClientID"].ToString()+"'";
					SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql1);

					string sql2="update tbDept set cnvcDeptName='"+htpara["strNewName"].ToString()+"',cnnPriority="+htpara["strSortNum"].ToString()+" where cnvcDeptID='"+htpara["strNewID"].ToString()+"'";
					SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql2);

					tran.Commit();
				}
				catch (Exception e) 
				{
					tran.Rollback();
					clog.WriteLine(e);
					return 0;
				}
				finally
				{
					if(con.State==ConnectionState.Open)
					{
						con.Close();
					}
				}

				return 1;
			}
		}

		public int IsExsitDeptInfo(string strClientID,string strNewID)
		{
			int deptFlag=2;
			try
			{
				string strsql="select count(*) from tbCommCode where vcCommSign='MD' and vcCommCode='" + strClientID + "'";
				drr=SqlHelper.ExecuteReader(con,CommandType.Text,strsql);
				drr.Read();
				int ClientDeptCount=int.Parse(drr[0].ToString());
				drr.Close();

				strsql="select count(*) from tbDept where cnvcDeptID='" + strNewID + "'";
				drr=SqlHelper.ExecuteReader(con,CommandType.Text,strsql);
				drr.Read();
				int NewDeptCount=int.Parse(drr[0].ToString());
				drr.Close();

				if(ClientDeptCount==0&&NewDeptCount==0)
				{
					deptFlag=0;
				}
				else if(ClientDeptCount!=0&&NewDeptCount!=0)
				{
					deptFlag=2;
				}
				else
				{
					deptFlag=1;
				}
			}
			catch(Exception ex)
			{
				clog.WriteLine(ex);
				return deptFlag;
			}
			finally
			{
				if(con.State==ConnectionState.Open)
				{
					con.Close();
				}
			}
			
			return deptFlag;
		}

		public DataTable GetClientOper(Hashtable htPara)
		{
			DataTable dtOper=new DataTable();
			try
			{
				string strCondition="";
				if(htPara["strOperName"].ToString()!=""&&htPara["strOperName"].ToString()!="*")
				{
					strCondition=" vcOperName like '%" + htPara["strOperName"].ToString() + "%'";
				}
				if(htPara["strDeptID"].ToString()!=""&&htPara["strDeptID"].ToString()!="*")
				{
					if(strCondition=="")
					{
						strCondition=" vcDeptID like '" + htPara["strDeptID"].ToString() + "'";
					}
					else
					{
						strCondition=strCondition + " and vcDeptID='" + htPara["strDeptID"].ToString() + "'";
					}
				}
				if(htPara["strState"].ToString()!=""&&htPara["strState"].ToString()!="*")
				{
					if(strCondition=="")
					{
						strCondition=" vcActiveFlag like '" + htPara["strState"].ToString() + "'";
					}
					else
					{
						strCondition=strCondition + " and vcActiveFlag='" + htPara["strState"].ToString() + "'";
					}
				}
				string sql1="SELECT vcOperID,vcOperName,vcLimit,vcDeptID,(case vcActiveFlag when '1' then '正常' else '冻结' end) as vcActiveFlag,(case vcPwdBeginFlag when '1' then '待初始化' else '正常' end) as vcPwdBeginFlag FROM tbOper";
				if(htPara["strLoginID"].ToString()!="admin")
				{
					if(strCondition!="")
						sql1=sql1 + " where " + strCondition + " and vcOperID<>'admin'";
					else
						sql1=sql1 + " where vcOperID<>'admin'";
				}
				else
				{
					if(strCondition!="")
						sql1=sql1 + " where " + strCondition;
				}
				sql1+=" order by vcDeptID,vcOperName";
				dtOper=SqlHelper.ExecuteDataTable(con,CommandType.Text,sql1);
			}
			catch (Exception e) 
			{
				clog.WriteLine(e);
				return null;
			}
			finally
			{
				if(con.State==ConnectionState.Open)
				{
					con.Close();
				}
			}
			return dtOper;
		}

		public DataTable GetClientOperInfo(string strOperid)
		{
			string strsql="select * from tbOper where vcOperID='" + strOperid + "'";
			DataTable dtout=SqlHelper.ExecuteDataTable(con,CommandType.Text,strsql);
			if(con.State==ConnectionState.Open)
			{
				con.Close();
			}
			
			return dtout;
		}

		public string ChkClientOperIDDup(string strOperID)
		{
			string strsql="select count(*) from tbOper where vcOperID='" + strOperID + "'";
			drr=SqlHelper.ExecuteReader(con,CommandType.Text,strsql);
			drr.Read();
			string strid=drr[0].ToString();
			drr.Close();
			if(con.State==ConnectionState.Open)
			{
				con.Close();
			}
			
			return strid;
		}

		public string ChkClientOperNameDup(string strOperName,string strDeptID)
		{
			string strsql="select count(*) from tbOper where vcOperName='" + strOperName + "' and vcDeptID='"+strDeptID+"'";
			drr=SqlHelper.ExecuteReader(con,CommandType.Text,strsql);
			drr.Read();
			string strname=drr[0].ToString();
			drr.Close();
			if(con.State==ConnectionState.Open)
			{
				con.Close();
			}
			
			return strname;
		}

		public int InsertClientOper(CMSMStruct.ClientOperStruct copernew)
		{
			string sql1="insert into tbOper values('" + copernew.strOperID+"','"+copernew.strOperName+"','"+copernew.strLimit+"','000000','"+copernew.strDeptID+"','1','0')";
			int recount=SqlHelper.ExecuteNonQuery(con,CommandType.Text,sql1);
			if(con.State==ConnectionState.Open)
			{
				con.Close();
			}
			return recount;
		}
		public int InsertOperLog(CMSMStruct.OperStruct OperNew)
		{
			string sql1="insert into tbOperLog values('登录中心','" + OperNew.strOperID+"','"+OperNew.strDeptID+"',getdate(),'"+OperNew.strMacAddress+"')";
			int recount=SqlHelper.ExecuteNonQuery(con,CommandType.Text,sql1);
			if(con.State==ConnectionState.Open)
			{
				con.Close();
			}
			return recount;
		}

		public int UpdateClientOper(string strOperID,string strsqlset)
		{
			string sql1="update tbOper set " + strsqlset + " where vcOperID='" + strOperID + "'";
			int recount=SqlHelper.ExecuteNonQuery(con,CommandType.Text,sql1);
			if(con.State==ConnectionState.Open)
			{
				con.Close();
			}
			return recount;
		}

		public int UpdateClientOperPwdBegin(string strOperID)
		{
			string sql1="update tbOper set vcPwd='000000',vcPwdBeginFlag='1' where vcOperID='" + strOperID + "'";
			int recount=SqlHelper.ExecuteNonQuery(con,CommandType.Text,sql1);
			if(con.State==ConnectionState.Open)
			{
				con.Close();
			}
			return recount;
		}

		public int UpdateClientOperFreeze(string strOperID,string strState)
		{
			int recount;
			string sql1="";
			if (strState=="1")
			{
				sql1="update tbOper set vcActiveFlag='0' where vcOperID='" + strOperID + "'";
			}
			else 
			{
				sql1="update tbOper set vcActiveFlag='1' where vcOperID='" + strOperID + "'";
			}
			recount=SqlHelper.ExecuteNonQuery(con,CommandType.Text,sql1);
			if(con.State==ConnectionState.Open)
			{
				con.Close();
			}
			return recount;
		}

		public void ConfirmConsItem(ArrayList al)
		{
			if(con.State==ConnectionState.Closed)
			{
				con.Open();
			}
			SqlTransaction trans=con.BeginTransaction();
			int recount=0;
			string sql1="";
			try
			{
				foreach(string str in al)
				{
					string[] strs = str.Split(',');
					sql1 = "update tbConsItemOther set vcComments='已确认' where iserial="+strs[0]+" and vcDeptId='"+strs[1]+"' and vcComments !='已确认'";
					recount+=SqlHelper.ExecuteNonQuery(con,trans,CommandType.Text,sql1);
				}
				trans.Commit();
			}
			catch(Exception ex)
			{
				trans.Rollback();
				clog.WriteLine(ex);
				throw ex;
			}
			finally
			{
				trans.Dispose();
				con.Close();
			}
				
		}

		public DataTable GetPackages(string strPackageId)
		{
			DataTable dtGoods=new DataTable();
			try
			{
				string sql1="select vcPackageId,b.vcGoodsName as vcPackageName,b.nPrice as nPackagePrice,a.vcGoodsId,c.vcGoodsName as vcGoodsName,a.nPrice as nGoodsPrice,a.vcComments from tbPackages a";
				sql1+=" left join tbGoods b on a.vcPackageId=b.vcGoodsID ";
				sql1+=" left join tbGoods c on a.vcGoodsId = c.vcGoodsID ";
				sql1+=" where a.vcPackageId='"+strPackageId+"'";
				dtGoods=SqlHelper.ExecuteDataTable(con,CommandType.Text,sql1);
			}
			catch (Exception e) 
			{
				clog.WriteLine(e);
				return null;
			}
			finally
			{
				if(con.State==ConnectionState.Open)
				{
					con.Close();
				}
			}
			return dtGoods;
		}
		public DataTable GetPackageInfo(string strId,string strName)
		{
			DataTable dtGoods=new DataTable();
			try
			{
				string sql1="select vcPackageId,b.vcGoodsName as vcPackageName,b.nPrice as nPackagePrice,a.vcGoodsId,c.vcGoodsName as vcGoodsName,a.nPrice as nGoodsPrice,a.vcComments from tbPackages a";
				sql1+=" left join tbGoods b on a.vcPackageId=b.vcGoodsID ";
				sql1+=" left join tbGoods c on a.vcGoodsId = c.vcGoodsID ";
				sql1+=" where 1=1 ";
				if(strId!="")
				{
					sql1+=" and a.vcPackageId='"+strId+"' or a.vcGoodsId='"+strId+"'";
				}
				if(strName!="")
				{
					sql1+=" and b.vcGoodsName like '%"+strName+"%' or c.vcGoodsName like '%"+strName+"%'";
				}
				dtGoods=SqlHelper.ExecuteDataTable(con,CommandType.Text,sql1);
			}
			catch (Exception e) 
			{
				clog.WriteLine(e);
				return null;
			}
			finally
			{
				if(con.State==ConnectionState.Open)
				{
					con.Close();
				}
			}
			return dtGoods;
		}

		public void AddPackage(PackagesStruct ps)
		{
			if(con.State==ConnectionState.Closed)
			{
				con.Open();
			}
			SqlTransaction trans=con.BeginTransaction();
			string sql1="";
			try
			{
				sql1 = "select count(*) from tbPackages where vcPackageId='"+ps.strPackageId+"' and vcGoodsId='"+ps.strGoodsId+"'";
				object objCount = SqlHelper.ExecuteScalar(con,trans,CommandType.Text,sql1);
				int icount = Convert.ToInt32(objCount);
				if(icount>0)
					throw new Exception("【"+ps.strGoodsId+ps.strGoodsName+"】在套餐【"+ps.strPackageId+ps.strPackageName+"】中已存在");

				sql1 = "select count(*) from tbGoods where vcGoodsId = '"+ps.strGoodsId+"'";
				objCount = SqlHelper.ExecuteScalar(con,trans,CommandType.Text,sql1);
				icount = Convert.ToInt32(objCount);
				if(icount==0)
					throw new Exception("【"+ps.strGoodsId+ps.strGoodsName+"】无此商品");

				sql1 = "insert into tbPackages(vcPackageId,vcGoodsId,nPrice,vcComments,IsOptional,OptionalGroup)values('"+ps.strPackageId+"','"+ps.strGoodsId+"',"+ps.dGoodsPrice+",'"+ps.strComments+"',0,'')";
				SqlHelper.ExecuteNonQuery(con,trans,CommandType.Text,sql1);

				trans.Commit();
			}
			catch(Exception ex)
			{
				trans.Rollback();
				clog.WriteLine(ex);
				throw ex;
			}
			finally
			{
				trans.Dispose();
				con.Close();
			}
				
		}

		public void UpdatePackage(PackagesStruct ps)
		{
			if(con.State==ConnectionState.Closed)
			{
				con.Open();
			}
			SqlTransaction trans=con.BeginTransaction();
			string sql1="";
			try
			{
				sql1 = "update tbPackages set nPrice="+ps.dGoodsPrice+",vcComments='"+ps.strComments+"' where vcPackageId='"+ps.strPackageId+"' and vcGoodsId='"+ps.strGoodsId+"'";
				SqlHelper.ExecuteNonQuery(con,trans,CommandType.Text,sql1);
				trans.Commit();
			}
			catch(Exception ex)
			{
				trans.Rollback();
				clog.WriteLine(ex);
				throw ex;
			}
			finally
			{
				trans.Dispose();
				con.Close();
			}
				
		}
		public void DeletePackage(PackagesStruct ps)
		{
			if(con.State==ConnectionState.Closed)
			{
				con.Open();
			}
			SqlTransaction trans=con.BeginTransaction();
			string sql1="";
			try
			{
				sql1 = "delete from tbPackages where vcPackageId='"+ps.strPackageId+"' and vcGoodsId='"+ps.strGoodsId+"'";
				SqlHelper.ExecuteNonQuery(con,trans,CommandType.Text,sql1);
				trans.Commit();
			}
			catch(Exception ex)
			{
				trans.Rollback();
				clog.WriteLine(ex);
				throw ex;
			}
			finally
			{
				trans.Dispose();
				con.Close();
			}
				
		}

		public DataTable GetPackagesGoods(string strPackageId,string strGoodsId,string strGoodsName)
		{
			DataTable dtGoods=new DataTable();
			try
			{
				string sql1="select vcGoodsID,vcGoodsName,nPrice,0 nPackagesPrice,'' vcComments from tbGoods "
+ " where {1} {2} vcGoodsID !='{0}' "
+ " and vcGoodsID not in (select vcGoodsID from tbPackages where vcPackageId='{0}') ";
				string str1 = " vcGoodsID like '%{0}%' and ";
				string str2 = " vcGoodsName like '%{0}%' and ";
				string str3 = "";
				if(strGoodsId!="")
				{
					str3 = string.Format(str1,strGoodsId);
				}
				string str4 = "";
				if(strGoodsName!="")
				{
					str4 = string.Format(str2,strGoodsName);
				}
				dtGoods=SqlHelper.ExecuteDataTable(con,CommandType.Text,string.Format(sql1,strPackageId,str3,str4));
			}
			catch (Exception e) 
			{
				clog.WriteLine(e);
				return null;
			}
			finally
			{
				if(con.State==ConnectionState.Open)
				{
					con.Close();
				}
			}
			return dtGoods;
		}

        public DataTable GetGoodsDeptPrice(string strId, string strName,string strDeptId)
        {
            DataTable dtGoods = new DataTable();
            try
            {
                string sql1 = "select a.vcDeptID,c.vcDeptName,a.vcGoodsID,b.vcGoodsName,a.nPrice as nDeptPrice,b.nPrice from tbGoodsDeptPrice a";
                sql1 += " left join tbGoods b on a.vcGoodsID=b.vcGoodsID ";
                sql1 += " left join (select vcCommCode as vcDeptID,vcCommName as vcDeptName from tbcommcode where vccommsign='MD') c on a.vcDeptID=c.vcDeptID ";
                sql1 += " where 1=1 ";
                if (strId != "")
                {
                    sql1 += " and a.vcGoodsId='" + strId + "'";
                }
                if (strName != "")
                {
                    sql1 += " and b.vcGoodsName like '%" + strName + "%'";
                }
                if (strDeptId != "")
                {
                    sql1 += " and a.vcDeptId='"+strDeptId+"'";
                }
                dtGoods = SqlHelper.ExecuteDataTable(con, CommandType.Text, sql1);
            }
            catch (Exception e)
            {
                clog.WriteLine(e);
                return null;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return dtGoods;
        }
        public DataTable GetGoodsDeptPrice(string strGoodsId)
        {
            DataTable dtGoods = new DataTable();
            try
            {
                string sql1 = "select a.vcDeptID,c.vcDeptName,a.vcGoodsID,b.vcGoodsName,a.nPrice as nDeptPrice,b.nPrice from tbGoodsDeptPrice a "
+ " left join tbGoods b on a.vcGoodsID=b.vcGoodsID "
+ " left join (select vcCommCode as vcDeptID,vcCommName as vcDeptName from tbcommcode where vccommsign='MD') c on a.vcDeptID=c.vcDeptID "
+ " where a.vcGoodsID='"+strGoodsId+"' ";
                dtGoods = SqlHelper.ExecuteDataTable(con, CommandType.Text, sql1);
            }
            catch (Exception e)
            {
                clog.WriteLine(e);
                return null;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return dtGoods;
        }

        public void AddGoodsDeptPrice(string strDeptId, string strGoodsId, string nPrice)
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            SqlTransaction trans = con.BeginTransaction();
            string sql1 = "";
            try
            {
                sql1 = "select count(*) from tbGoodsDeptPrice where vcDeptId='" 
                    + strDeptId + "' and vcGoodsId='" +strGoodsId + "'";
                object objCount = SqlHelper.ExecuteScalar(con, trans, CommandType.Text, sql1);
                int icount = Convert.ToInt32(objCount);
                if (icount > 0)
                    throw new Exception("门店单价已存在");

                sql1 = "select count(*) from tbGoods where vcGoodsId = '" + strGoodsId + "'";
                objCount = SqlHelper.ExecuteScalar(con, trans, CommandType.Text, sql1);
                icount = Convert.ToInt32(objCount);
                if (icount == 0)
                    throw new Exception("无此商品");

                sql1 = "insert into tbGoodsDeptPrice(vcDeptId,vcGoodsId,nPrice)values('" + strDeptId + "','" + strGoodsId + "'," + nPrice + ")";
                SqlHelper.ExecuteNonQuery(con, trans, CommandType.Text, sql1);

                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                clog.WriteLine(ex);
                throw ex;
            }
            finally
            {
                trans.Dispose();
                con.Close();
            }

        }

        public void UpdateGoodsDeptPrice(string strDeptId, string strGoodsId, string nPrice)
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            SqlTransaction trans = con.BeginTransaction();
            string sql1 = "";
            try
            {
                sql1 = "update tbGoodsDeptPrice set nPrice=" + nPrice + " where vcDeptId='" + strDeptId + "' and vcGoodsId='" + strGoodsId + "'";
                SqlHelper.ExecuteNonQuery(con, trans, CommandType.Text, sql1);
                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                clog.WriteLine(ex);
                throw ex;
            }
            finally
            {
                trans.Dispose();
                con.Close();
            }

        }
        public void DeleteGoodsDeptPrice(string strDeptId, string strGoodsId)
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            SqlTransaction trans = con.BeginTransaction();
            string sql1 = "";
            try
            {
                sql1 = "delete from tbGoodsDeptPrice where vcDeptId='" + strDeptId + "' and vcGoodsId='" + strGoodsId + "'";
                SqlHelper.ExecuteNonQuery(con, trans, CommandType.Text, sql1);
                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                clog.WriteLine(ex);
                throw ex;
            }
            finally
            {
                trans.Dispose();
                con.Close();
            }

        }

        public DataTable GetGoodsDept(string strGoodsId,string strGoodsName,string strPrice)
        {
            DataTable dtGoods = new DataTable();
            try
            {
                string sql1 = "select cast(case when b.nPrice is null then 0 else 1 end  as bit) as IsSelect,a.vcDeptId,a.vcDeptName,"
+ "case when c.vcGoodsId is null then '" + strGoodsId + "' else c.vcGoodsId end vcGoodsId,"
+ "case when c.vcGoodsName is null then '" + strGoodsName + "' else c.vcGoodsName end vcGoodsName,"
+ "case when b.nPrice is null then " + strPrice + " else b.nPrice end nPrice"
+" from "
+ "(select vcCommCode as vcDeptId,vcCommName as vcDeptName from tbCommCode where vcCommSign='MD' and vcCommCode not in('CEN00','FYZX1')) a "
+ "left join (select * from tbGoodsDeptPrice where vcGoodsId='"+strGoodsId+"') b on a.vcDeptId=b.vcDeptId "
+ "left join (select * from tbGoods where vcGoodsId='" + strGoodsId + "')  c on b.vcGoodsId=c.vcGoodsId";
                dtGoods = SqlHelper.ExecuteDataTable(con, CommandType.Text, sql1);
            }
            catch (Exception e)
            {
                clog.WriteLine(e);
                return null;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return dtGoods;
        }
        public void AddGoodsDept(string strDeptId, string strGoodsId, string nPrice)
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            SqlTransaction trans = con.BeginTransaction();
            string sql1 = "";
            try
            {
                sql1 = "select count(*) from tbGoodsDeptPrice where vcDeptId='"
                    + strDeptId + "' and vcGoodsId='" + strGoodsId + "'";
                object objCount = SqlHelper.ExecuteScalar(con, trans, CommandType.Text, sql1);
                int icount1 = Convert.ToInt32(objCount);
                //if (icount > 0)
                //    throw new Exception("门店单价已存在");

                sql1 = "select count(*) from tbGoods where vcGoodsId = '" + strGoodsId + "'";
                objCount = SqlHelper.ExecuteScalar(con, trans, CommandType.Text, sql1);
                int icount2 = Convert.ToInt32(objCount);
                //if (icount == 0)
                //    throw new Exception("无此商品");
                if (icount1 == 0 && icount2 > 0)
                {
                    sql1 = "insert into tbGoodsDeptPrice(vcDeptId,vcGoodsId,nPrice)values('" + strDeptId + "','" + strGoodsId + "'," + nPrice + ")";
                    SqlHelper.ExecuteNonQuery(con, trans, CommandType.Text, sql1);
                }
                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                clog.WriteLine(ex);
                throw ex;
            }
            finally
            {
                trans.Dispose();
                con.Close();
            }

        }
        public void DeleteGoodsDept(string strDeptId, string strGoodsId)
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            SqlTransaction trans = con.BeginTransaction();
            string sql1 = "";
            try
            {
                sql1 = "delete from tbGoodsDeptPrice where vcDeptId='" + strDeptId + "' and vcGoodsId='" + strGoodsId + "'";
                SqlHelper.ExecuteNonQuery(con, trans, CommandType.Text, sql1);
                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                clog.WriteLine(ex);
                throw ex;
            }
            finally
            {
                trans.Dispose();
                con.Close();
            }

        }

        public void LossCard(string strCardId,string strAssId,string strOperName,string strDeptId)
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            SqlTransaction trans = con.BeginTransaction();
            string sql1 = "";
            try
            {
                

                sql1 = "update tbAssociator set vcAssState='1' where vcCardId='"+strCardId+"' and vcAssState='0'";
                int iCount = SqlHelper.ExecuteNonQuery(con, trans, CommandType.Text, sql1);
                if (iCount > 0)
                {
                    sql1 = "insert into tbBusiLog values(" + strAssId + ",'" + strCardId + "','OP004','" + strOperName + "',getdate(),'','" + strDeptId + "')";
                    SqlHelper.ExecuteNonQuery(con, trans, CommandType.Text, sql1);

                    sql1 = "insert into tbAssociatorSync SELECT [vcCardID], [vcAssName], [vcSpell], [vcAssNbr], [vcLinkPhone], [vcLinkAddress], [vcEmail], [vcAssType], '1', [nCharge], [iIgValue], [vcCardFlag], [vcComments], [dtCreateDate], getdate(), [vcDeptID], [vcCardSerial],0 FROM [tbAssociator] where vcCardID='" + strCardId + "' and iAssID=" + strAssId + "";
                    SqlHelper.ExecuteNonQuery(con, trans, CommandType.Text, sql1);
                }
                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                clog.WriteLine(ex);
                throw ex;
            }
            finally
            {
                trans.Dispose();
                con.Close();
            }

        }

        public void CancelLossCard(string strCardId, string strAssId, string strOperName, string strDeptId)
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            SqlTransaction trans = con.BeginTransaction();
            string sql1 = "";
            try
            {


                sql1 = "update tbAssociator set vcAssState='0' where vcCardId='" + strCardId + "' and vcAssState='1'";
                int iCount = SqlHelper.ExecuteNonQuery(con, trans, CommandType.Text, sql1);
                if (iCount > 0)
                {
                    sql1 = "insert into tbBusiLog values(" + strAssId + ",'" + strCardId + "','OP016','" + strOperName + "',getdate(),'','" + strDeptId + "')";
                    SqlHelper.ExecuteNonQuery(con, trans, CommandType.Text, sql1);

                    sql1 = "insert into tbAssociatorSync SELECT [vcCardID], [vcAssName], [vcSpell], [vcAssNbr], [vcLinkPhone], [vcLinkAddress], [vcEmail], [vcAssType], '0', [nCharge], [iIgValue], [vcCardFlag], [vcComments], [dtCreateDate], getdate(), [vcDeptID], [vcCardSerial],0 FROM [tbAssociator] where vcCardID='" + strCardId + "' and iAssID=" + strAssId + "";
                    SqlHelper.ExecuteNonQuery(con, trans, CommandType.Text, sql1);
                }
                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                clog.WriteLine(ex);
                throw ex;
            }
            finally
            {
                trans.Dispose();
                con.Close();
            }

        }
	}
}
