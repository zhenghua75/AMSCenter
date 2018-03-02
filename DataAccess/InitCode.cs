using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Collections;
using System.Collections.Specialized;
using System.Web;
using System.Web.SessionState;
using CommCenter;

namespace DataAccess
{
	/// <summary>
	/// Summary description for InitCode.
	/// </summary>
	public class InitCode
	{
		public InitCode()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public DataSet LoadCodeTable(string strCon)
		{
			//初始化输出包
			DataSet		dsOut = new DataSet();

			//连接数据库
			using (SqlConnection conn=new SqlConnection(strCon))
			{
				//打开数据库连接
				conn.Open();
			
				try
				{
                    string sql = "select * from tbCommCode where vcCommSign in('AS','AT','ES','GT','IGT','MD','MD1','OP','PT','DE','OF','EC','CLT','SFlag','PPS','LM') and vcCommCode<>'CEN00'";
					DataTable dt = SqlHelper.ExecuteDataTable(conn,CommandType.Text,sql);
					dt.TableName="tbCommCode";
					dsOut.Tables.Add(dt);

					sql= "select * from tbCommCode where vcCommSign='MD' order by vcCommName  desc";
					dt = SqlHelper.ExecuteDataTable(conn,CommandType.Text,sql);
					dt.TableName="AllMD";
					dsOut.Tables.Add(dt);

                    sql = "select distinct vcComments from tbCommCode where vcCommSign='MD'";
                    dt = SqlHelper.ExecuteDataTable(conn, CommandType.Text, sql);
                    
                    dt.Columns.Add("vcCommCode");
                    dt.Columns.Add("vcCommName");
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            string strComment = dr["vcComments"].ToString();
                            string[] strComments = strComment.Split('|');
                            if (strComments.Length > 1)
                            {
                                dr["vcCommCode"] = strComments[1];
                                dr["vcCommName"] = strComments[1];
                            }
                        }
                        DataView dataView = dt.DefaultView;
                        DataTable dtDistinct = dataView.ToTable(true, "vcCommCode", "vcCommName");
                        dtDistinct.TableName = "AllREGION";
                        dsOut.Tables.Add(dtDistinct);
                    }                    

                    sql = @"select * from tbCommCode where vcCommSign='MD' 
and vcCommCode in (select vcCommName from tbCommCode where vcCommSign='MDP' and vcCommCode='true')
order by vcCommName  desc";
                    dt = SqlHelper.ExecuteDataTable(conn, CommandType.Text, sql);
                    dt.TableName = "AllMDP";
                    dsOut.Tables.Add(dt);

					sql= "select * from tbCommCode where vcCommSign='Month' order by vcCommName desc";
					dt = SqlHelper.ExecuteDataTable(conn,CommandType.Text,sql);
					dt.TableName="AcctMonth";
					dsOut.Tables.Add(dt);

					sql= "select * from tbCommCode where vcCommSign='OPS'";
					dt = SqlHelper.ExecuteDataTable(conn,CommandType.Text,sql);
					dt.TableName="OPState";
					dsOut.Tables.Add(dt);

					sql= "select vcMacAddress from tbMacAddress";
					dt = SqlHelper.ExecuteDataTable(conn,CommandType.Text,sql);
					dt.TableName="MAC";
					dsOut.Tables.Add(dt);

					sql= "select * from tbOperFunc order by vcOperID,vcFuncName";
					dt = SqlHelper.ExecuteDataTable(conn,CommandType.Text,sql);
					dt.TableName="OperFunc";
					dsOut.Tables.Add(dt);

					sql= "select a.vcCommName as iotName,substring(a.vcCommName,1,5) as Officer,b.vcCommName as vcClassName,b.vcCommCode as vcClassId,a.vcCommCode as InTime,a.vcCommSign as OutTime ";
					sql+=" from tbCommCode a,tbCommCode b where a.vcComments='IOTime' and b.vcCommSign='EC' and substring(a.vcCommName,6,1)=b.vcCommCode order by Officer,vcClassId";
					dt = SqlHelper.ExecuteDataTable(conn,CommandType.Text,sql);
					dt.TableName="IOTime";
					dsOut.Tables.Add(dt);

					sql= "select vcGoodsID as vcCommCode,vcGoodsName as vcCommName from tbGoods";
					dt = SqlHelper.ExecuteDataTable(conn,CommandType.Text,sql);
					dt.TableName="Goods";
					dsOut.Tables.Add(dt);

					sql= "select cnvcProductClassCode as vcCommCode,cnvcProductClassName as vcCommName,cnvcProductType as vcCommSign from tbProductClass order by cnvcProductType,cnvcProductClassCode";
					dt = SqlHelper.ExecuteDataTable(conn,CommandType.Text,sql);
					dt.TableName="PClass";
					dsOut.Tables.Add(dt);

					sql= "select distinct cnvcMaterialCode as vcCommCode,cnvcMaterialName as vcCommName,cnvcProductType,cnvcUnit,cnvcProductClass,cnvcStandardUnit,cnnStatdardCount from tbMaterial order by cnvcMaterialName";
					dt = SqlHelper.ExecuteDataTable(conn,CommandType.Text,sql);
					dt.TableName="AllMaterial";
					dsOut.Tables.Add(dt);

					sql= "select distinct cnvcProviderCode as vcCommCode,cnvcProviderName as vcCommName from tbProvider order by cnvcProviderName";
					dt = SqlHelper.ExecuteDataTable(conn,CommandType.Text,sql);
					dt.TableName="Provider";
					dsOut.Tables.Add(dt);

					sql= "select cnvcDeptName as vcCommName,cnvcDeptID as vcCommCode,cnvcDeptType as vcCommSign from tbDept where cnvcDeptType in('SalesRoom','Factory') order by cnvcDeptName";
					dt = SqlHelper.ExecuteDataTable(conn,CommandType.Text,sql);
					dt.TableName="NewDept";
					dsOut.Tables.Add(dt);

					sql= "select cnvcName as vcCommName,cnvcCode as vcCommCode,cnvcType as vcCommSign from tbNameCode";
					dt = SqlHelper.ExecuteDataTable(conn,CommandType.Text,sql);
					dt.TableName="tbNameCodeToStorage";
					dsOut.Tables.Add(dt);

					sql= "select cnvcProductName as vcCommName,cnvcProductCode as vcCommCode,cnvcProductType,cnvcUnit,cnvcProductClass from tbFormula";
					dt = SqlHelper.ExecuteDataTable(conn,CommandType.Text,sql);
					dt.TableName="tbFormula";
					dsOut.Tables.Add(dt);

					sql= "select distinct cnvcOldDeptID,cnvcNewDeptID from tbDeptMapInfo";
					dt = SqlHelper.ExecuteDataTable(conn,CommandType.Text,sql);
					dt.TableName="DeptMapInfo";
					dsOut.Tables.Add(dt);

                    sql = "select vcLoginId as vcCommCode,vcOperName as vcCommName,vcLimit,vcDeptId from tbLogin";
                    dt = SqlHelper.ExecuteDataTable(conn, CommandType.Text, sql);
                    dt.TableName = "tbLocalLogin";
                    dsOut.Tables.Add(dt);

                    sql = "select * from  tbCommCode";
                    dt = SqlHelper.ExecuteDataTable(conn, CommandType.Text, sql);
                    dt.TableName = "tbCommCodeBDEPT";
                    dsOut.Tables.Add(dt);
				}
				catch(Exception ex)
				{
					AMSLog clog=new AMSLog();
					clog.WriteLine(ex);
					dsOut = null;
				}
				finally			 
				{
					conn.Close();
				}
			}

			//返回数据
			return dsOut;
		}
	}
}
