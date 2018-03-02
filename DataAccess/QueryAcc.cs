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
	public class QueryAcc
	{

//		SqlDataReader drr;
		SqlConnection con;
		AMSLog clog=new AMSLog();

		public QueryAcc(string strcons)
		{
			//
			// TODO: Add constructor logic here
			//
			con=new SqlConnection(strcons);
		}
		
		public DataTable GetConsQuery(Hashtable htPara)
		{
			DataTable dtCons=new DataTable();
			try
			{
				string strCondition="";
				if(htPara["strCardID"].ToString()!=""&&htPara["strCardID"].ToString()!="*")
				{
					strCondition=" a.vcCardID='" + htPara["strCardID"].ToString() + "'";
				}
				if(htPara["strAssName"].ToString()!=""&&htPara["strAssName"].ToString()!="*")
				{
					if(strCondition=="")
					{
						strCondition=" c.vcAssName  like  '%" + htPara["strAssName"].ToString() + "%'";
					}
					else
					{
						strCondition=strCondition + " and c.vcAssName like  '%" + htPara["strAssName"].ToString() + "%'";
					}
				}
				if(htPara["strSerial"].ToString()!=""&&htPara["strSerial"].ToString()!="*")
				{
					if(strCondition=="")
					{
						strCondition=" a.iSerial like  '%" + htPara["strSerial"].ToString() + "%'";
					}
					else
					{
						strCondition=strCondition + " and a.iSerial like '%" + htPara["strSerial"].ToString() + "%'";
					}
				}
				if(htPara["strAssType"].ToString()!="")
				{
					if(strCondition=="")
					{
						strCondition=" c.vcAssType='" + htPara["strAssType"].ToString() + "'";
					}
					else
					{
						strCondition=strCondition + " and c.vcAssType = '" + htPara["strAssType"].ToString() + "'";
					}
				}
				if(htPara["strOperName"].ToString()!="")
				{
					if(strCondition=="")
					{
						strCondition=" a.vcOperName='" + htPara["strOperName"].ToString() + "'";
					}
					else
					{
						strCondition=strCondition + " and a.vcOperName = '" + htPara["strOperName"].ToString() + "'";
					}
				}
				if(htPara["strDeptID"].ToString()!="")
				{
					if(strCondition=="")
					{
						strCondition=" a.vcDeptID='" + htPara["strDeptID"].ToString() + "'";
					}
					else
					{
						strCondition=strCondition + " and a.vcDeptID = '" + htPara["strDeptID"].ToString() + "'";
					}
				}
				if(htPara["strBillType"].ToString()!="")
				{
					if(strCondition=="") 
					{
						strCondition=" b.vcConsType='" + htPara["strBillType"].ToString() + "'";
					}
					else
					{
						strCondition=strCondition + " and b.vcConsType = '" + htPara["strBillType"].ToString() + "'";
					}
				}
				if(htPara["strConfirm"].ToString()!="")
				{
					if(htPara["strConfirm"].ToString()=="已确认")
					{
						if(strCondition=="") 
						{
							strCondition=" a.vcComments='已确认'";
						}
						else
						{
							strCondition=strCondition + " and a.vcComments = '已确认'";
						}
					}
					else
					{
						if(strCondition=="") 
						{
							strCondition=" a.vcComments!='已确认'";
						}
						else
						{
							strCondition=strCondition + " and a.vcComments != '已确认'";
						}
					}
				}
				if(htPara["strPackage"].ToString()!="")
				{
					if(htPara["strPackage"].ToString()=="是")
					{
						if(strCondition=="") 
						{
							strCondition=" a.vcPackageId is not null and vcPackageId!=''";
						}
						else
						{
							strCondition=strCondition + " and a.vcPackageId is not null and vcPackageId!=''";
						}
					}
					else
					{
						if(strCondition=="") 
						{
							strCondition=" (a.vcPackageId is null or vcPackageId='')";
						}
						else
						{
							strCondition=strCondition + " and (a.vcPackageId is null or vcPackageId='')";
						}
					}
				}
				
				string sql1="select a.iSerial,c.vcAssName,c.vcAssType,a.vcCardID,";
				
				if(htPara.ContainsKey("bPackage"))
				{
					sql1+= " f.vcGoodsName 套餐名称,e.nPrice 套餐单价,d.vcGoodsName,a.nPrice,a.iCount,a.nFee,b.vcConsType,a.vcComments,(case a.cFlag when '0' then '正常消费' when '9' then '已撤消' else a.cFlag end) as cFlag,a.dtConsDate,a.vcOperName,a.vcDeptID";
					sql1+=" from tbConsItemPackage a,vwBill b,tbAssociator c,tbGoods d,(select * from vwConsItem where vcPackageId is not null and vcPackageId !='') e,tbGoods f";
				}
				else
				{
					sql1+= " case when a.vcPackageId is not null and vcPackageId!='' then '是' else '否' end  是否套餐,d.vcGoodsName,a.nPrice,a.iCount,a.nFee,b.vcConsType,a.vcComments,(case a.cFlag when '0' then '正常消费' when '9' then '已撤消' else a.cFlag end) as cFlag,a.dtConsDate,a.vcOperName,a.vcDeptID";
					sql1+=" from vwConsItem a,vwBill b,tbAssociator c,tbGoods d";
				}
				//sql1+=" from vwConsItem a ";
				//sql1+=" left join vwBill b on a.iSerial=b.iSerial and a.vcCardID=b.vcCardID and a.vcDeptID=b.vcDeptID ";
				//sql1+=" left join tbAssociator c on a.vcCardID=c.vcCardID ";
				//sql1+=" left join tbGoods d on a.vcGoodsID=d.vcGoodsID ";
				//sql1+=" left join tbGoods e on a.vcPackageId = e.vcGoodsId ";
				if (htPara["strAssState"].ToString()=="Roll")
				{
					sql1+=" where a.dtConsDate between c.dtCreateDate and case when c.vcAssState=3 then  dtOperDate else GETDATE() end and a.iSerial=b.iSerial and a.vcCardID=b.vcCardID and a.vcDeptID=b.vcDeptID and a.vcCardID=c.vcCardID and a.vcGoodsID=d.vcGoodsID and a.cFlag='"+htPara["strConsFlag"].ToString()+"' and a.dtConsDate between '" + htPara["strBegin"].ToString() + "' and '" + htPara["strEnd"].ToString() + " 23:59:59' ";
				}
				else
				{
					sql1+=" where a.dtConsDate between c.dtCreateDate and case when c.vcAssState=3 then  dtOperDate else GETDATE() end and c.vcAssState='" + htPara["strAssState"].ToString() + "' and a.iSerial=b.iSerial and a.vcCardID=b.vcCardID and a.vcDeptID=b.vcDeptID and a.vcCardID=c.vcCardID and a.vcGoodsID=d.vcGoodsID and a.cFlag='"+htPara["strConsFlag"].ToString()+"' and a.dtConsDate between '" + htPara["strBegin"].ToString() + "' and '" + htPara["strEnd"].ToString() + " 23:59:59' ";
				}
				if(htPara.ContainsKey("bPackage"))
				{
					sql1+= " and a.iSerial=e.iSerial and a.vcCardID=e.vcCardID and a.vcDeptID=e.vcDeptID and a.vcPackageId=e.vcGoodsId and e.vcGoodsId=f.vcGoodsId ";
				}
				//sql1+=" where a.cFlag='"+htPara["strConsFlag"].ToString()+"' and a.dtConsDate between '" + htPara["strBegin"].ToString() + "' and '" + htPara["strEnd"].ToString() + " 23:59:59' ";
				if(strCondition!="")
				{
					sql1+=" and " + strCondition;
				}
				sql1+=" order by a.dtConsDate";
				dtCons=SqlHelper.ExecuteDataTable(con,CommandType.Text,sql1);
				return dtCons;
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
			
		}

		public DataTable GetConsOperList(string strDept,string strbegin,string strend)
		{
			DataTable dtoperlist=new DataTable();
			try
			{
				string sql1="";
				if(strDept==""||strDept=="全部")
				{
					sql1="select distinct vcOperName from tbBusiLogOther where dtOperDate between '" + strbegin+"' and '"+strend+"  23:59:59' order by vcOperName";
				}
				else
				{
					sql1="select distinct vcOperName from tbBusiLogOther where vcDeptID='"+ strDept+"' and dtOperDate between '" + strbegin+"' and '"+strend+" 23:59:59' order by vcOperName";
				}
				dtoperlist=SqlHelper.ExecuteDataTable(con,CommandType.Text,sql1);
				return dtoperlist;
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
			
		}

		public DataTable GetFillQuery(Hashtable htPara)
		{
			DataTable dtCons=new DataTable();
			try
			{
				string strCondition="";
				if(htPara["strCardID"].ToString()!=""&&htPara["strCardID"].ToString()!="*")
				{
					strCondition=" a.vcCardID='" + htPara["strCardID"].ToString() + "'";
				}
				if(htPara["strAssName"].ToString()!=""&&htPara["strAssName"].ToString()!="*")
				{
					if(strCondition=="")
					{
						strCondition=" b.vcAssName like '%" + htPara["strAssName"].ToString() + "%'";
					}
					else
					{
						strCondition=strCondition + " and b.vcAssName = '%" + htPara["strAssName"].ToString() + "%'";
					}
				}
				if(htPara["strAssType"].ToString()!="")
				{
					if(strCondition=="")
					{
						strCondition=" b.vcAssType='" + htPara["strAssType"].ToString() + "'";
					}
					else
					{
						strCondition=strCondition + " and b.vcAssType = '" + htPara["strAssType"].ToString() + "'";
					}
				}
				if(htPara["strOperName"].ToString()!="")
				{
					if(strCondition=="")
					{
						strCondition=" a.vcOperName='" + htPara["strOperName"].ToString() + "'";
					}
					else
					{
						strCondition=strCondition + " and a.vcOperName = '" + htPara["strOperName"].ToString() + "'";
					}
				}
				if(htPara["strDeptID"].ToString()!="")
				{
					if(strCondition=="")
					{
						strCondition=" a.vcDeptID='" + htPara["strDeptID"].ToString() + "'";
					}
					else
					{
						strCondition=strCondition + " and a.vcDeptID = '" + htPara["strDeptID"].ToString() + "'";
					}
				}
				if(htPara["strFillType"].ToString()!="")
				{
					if(strCondition=="")
					{
						if(htPara["strFillType"].ToString()=="Norm")
						{
							strCondition=" (a.vcComments=''or a.vcComments like '发卡充值%')";
						}
						else
						{
							strCondition=" a.vcComments like '" + htPara["strFillType"].ToString() + "'";
						}
					}
					else
					{
						if(htPara["strFillType"].ToString()=="Norm")
						{
							strCondition=strCondition + " and (a.vcComments='' or a.vcComments like '发卡充值%')";
						}
						else
						{
							strCondition=strCondition + " and a.vcComments like '" + htPara["strFillType"].ToString() + "'";
						}
					}
				}
				string sql1="";
				if (htPara["strAssState"].ToString()=="Roll")
				{
					//b.vcAssState<>'3' and
					sql1="select a.iSerial,b.vcAssName,b.vcAssType,a.vcCardID,a.nFillFee,a.nFillProm,a.nFeeLast,a.nFeeCur,a.vcComments,a.dtFillDate,a.vcOperName,a.vcDeptID from vwFillFee a,tbAssociator b where  a.vcCardID=b.vcCardID  and a.dtFillDate between '" + htPara["strBegin"].ToString() + "' and '" + htPara["strEnd"].ToString() + " 23:59:59' ";
					sql1+=" and a.dtFillDate between b.dtCreateDate and case when b.vcAssState=3 then  b.dtOperDate else GETDATE() end ";
				}
				else
				{
					sql1="select a.iSerial,b.vcAssName,b.vcAssType,a.vcCardID,a.nFillFee,a.nFillProm,a.nFeeLast,a.nFeeCur,a.vcComments,a.dtFillDate,a.vcOperName,a.vcDeptID from vwFillFee a,tbAssociator b where b.vcAssState='" + htPara["strAssState"].ToString() + "' and a.vcCardID=b.vcCardID  and a.dtFillDate between '" + htPara["strBegin"].ToString() + "' and '" + htPara["strEnd"].ToString() + " 23:59:59' ";
					sql1+=" and a.dtFillDate between b.dtCreateDate and case when b.vcAssState=3 then  b.dtOperDate else GETDATE() end ";
				}
				
				if(strCondition!="")
				{
					sql1+=" and " + strCondition;
				}
				if(htPara["strFillType"].ToString()=="Norm")
				{
					sql1+=" and a.nFillFee>0 order by a.dtFillDate desc";
				}
				else
				{
					sql1+=" order by a.dtFillDate,a.vcCardID";
				}
				dtCons=SqlHelper.ExecuteDataTable(con,CommandType.Text,sql1);
				return dtCons;
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
		}

		public DataTable GetConsKindQuery(Hashtable htPara,string querytype,bool bDate,bool bAssType,bool bGoodsType,bool bDept)
		{
			DataTable dtCons=new DataTable();
			try
			{
				string strsql="select ";
				if(bDate)
				{
					strsql +="convert(varchar(10),a.dtConsDate,121) as 消费日期,";
				}
				if(bAssType)
				{
					strsql +="b.vcAssType,";
				}
				if(bGoodsType)
				{
					strsql +="d.vcCommName as 商品类型,";
				}
				if(bDept)
				{
					strsql +="a.vcDeptID,";
				}
				strsql +="c.vcGoodsName,sum(a.iCount) as tolcount,sum(a.nFee) as tolfee";
				//strsql="select a.vcDeptID,b.vcAssType,d.vcCommName as 商品类型,c.vcGoodsName,sum(a.iCount) as tolcount,sum(a.nFee) as tolfee";
				strsql+=" from vwConsItem a,tbAssociator b,tbGoods c,tbCommCode d";
				strsql+=" where a.dtConsDate between b.dtCreateDate and case when b.vcAssState=3 then  b.dtOperDate else GETDATE() end and a.cFlag='0' and d.vcCommSign='GT' and c.vcGoodsName like '%"+htPara["strGoodsName"].ToString()+"%' and a.vcCardID=b.vcCardID and a.vcGoodsID=c.vcGoodsID and substring(a.vcGoodsID,1,2) between substring(d.vcCommCode,1,2) and substring(d.vcCommCode,4,2) and a.dtConsDate between '" + htPara["strBegin"].ToString() + "' and '" + htPara["strEnd"].ToString() + " 23:59:59' ";
				if(htPara["strDeptID"].ToString()!="")
				{
					strsql+=" and a.vcDeptID='"+htPara["strDeptID"].ToString()+"'";
				}
				if(htPara["strAssType"].ToString()!="")
				{
					strsql+=" and b.vcAssType='"+htPara["strAssType"].ToString()+"'";
				}
				if(htPara["strGoodsType"].ToString()!="")
				{
					strsql+=" and d.vcCommCode='"+htPara["strGoodsType"].ToString()+"'";
				}
				strsql+=" group by c.vcGoodsName ";

				if(bDate)
				{
					strsql+=",convert(varchar(10),a.dtConsDate,121)";
				}
				if(bAssType)
				{
					strsql +=",b.vcAssType";
				}
				if(bGoodsType)
				{
					strsql +=",d.vcCommName";
				}
				if(bDept)
				{
					strsql +=",a.vcDeptID";
				}
				strsql+=" order by c.vcGoodsName";
				if(bDate)
				{
					strsql+=",convert(varchar(10),a.dtConsDate,121)";
				}
				if(bAssType)
				{
					strsql +=",b.vcAssType";
				}
				if(bGoodsType)
				{
					strsql +=",d.vcCommName";
				}
				if(bDept)
				{
					strsql +=",a.vcDeptID";
				}
//				switch(querytype)
//				{
//					case "1":
//						strsql="select a.vcDeptID,b.vcAssType,d.vcCommName as 商品类型,c.vcGoodsName,sum(a.iCount) as tolcount,sum(a.nFee) as tolfee";
//						strsql+=" from vwConsItem a,tbAssociator b,tbGoods c,tbCommCode d";
//						strsql+=" where a.cFlag='0' and d.vcCommSign='GT' and c.vcGoodsName like '%"+htPara["strGoodsName"].ToString()+"%' and a.vcCardID=b.vcCardID and a.vcGoodsID=c.vcGoodsID and substring(a.vcGoodsID,1,2) between substring(d.vcCommCode,1,2) and substring(d.vcCommCode,4,2) and a.dtConsDate between '" + htPara["strBegin"].ToString() + "' and '" + htPara["strEnd"].ToString() + " 23:59:59' ";
//						if(htPara["strDeptID"].ToString()!="")
//						{
//							strsql+=" and a.vcDeptID='"+htPara["strDeptID"].ToString()+"'";
//						}
//						if(htPara["strAssType"].ToString()!="")
//						{
//							strsql+=" and b.vcAssType='"+htPara["strAssType"].ToString()+"'";
//						}
//						if(htPara["strGoodsType"].ToString()!="")
//						{
//							strsql+=" and d.vcCommCode='"+htPara["strGoodsType"].ToString()+"'";
//						}
//						strsql+=" group by a.vcDeptID,b.vcAssType,d.vcCommName,c.vcGoodsName order by a.vcDeptID,b.vcAssType,d.vcCommName,c.vcGoodsName";
//						break;
//					case "2":
//						strsql="select d.vcCommName as 商品类型,c.vcGoodsName,sum(a.iCount) as tolcount,sum(a.nFee) as tolfee";
//						strsql+=" from vwConsItem a,tbAssociator b,tbGoods c,tbCommCode d";
//						strsql+=" where a.cFlag='0' and d.vcCommSign='GT' and c.vcGoodsName like '%"+htPara["strGoodsName"].ToString()+"%' and a.vcCardID=b.vcCardID and a.vcGoodsID=c.vcGoodsID and substring(a.vcGoodsID,1,2) between substring(d.vcCommCode,1,2) and substring(d.vcCommCode,4,2) and a.dtConsDate between '" + htPara["strBegin"].ToString() + "' and '" + htPara["strEnd"].ToString() + " 23:59:59' ";
//						if(htPara["strGoodsType"].ToString()!="")
//						{
//							strsql+=" and d.vcCommCode='"+htPara["strGoodsType"].ToString()+"'";
//						}
//						strsql+=" group by a.vcDeptID,b.vcAssType,d.vcCommName,c.vcGoodsName order by a.vcDeptID,b.vcAssType,d.vcCommName,c.vcGoodsName";
//						break;
//					case "3":
//						strsql="select d.vcCommName as 商品类型,c.vcGoodsName,sum(a.iCount) as tolcount,sum(a.nFee) as tolfee";
//						strsql+=" from vwConsItem a,tbAssociator b,tbGoods c,tbCommCode d";
//						strsql+=" where a.cFlag='0' and d.vcCommSign='GT' and c.vcGoodsName like '%"+htPara["strGoodsName"].ToString()+"%' and a.vcCardID=b.vcCardID and a.vcGoodsID=c.vcGoodsID and substring(a.vcGoodsID,1,2) between substring(d.vcCommCode,1,2) and substring(d.vcCommCode,4,2) and a.dtConsDate between '" + htPara["strBegin"].ToString() + "' and '" + htPara["strEnd"].ToString() + " 23:59:59' ";
//						if(htPara["strDeptID"].ToString()!="")
//						{
//							strsql+=" and a.vcDeptID='"+htPara["strDeptID"].ToString()+"'";
//						}
//						if(htPara["strAssType"].ToString()!="")
//						{
//							strsql+=" and b.vcAssType='"+htPara["strAssType"].ToString()+"'";
//						}
//						if(htPara["strGoodsType"].ToString()!="")
//						{
//							strsql+=" and d.vcCommCode='"+htPara["strGoodsType"].ToString()+"'";
//						}
//						strsql+=" group by d.vcCommName,c.vcGoodsName order by d.vcCommName,c.vcGoodsName";
//						break;
//					case "4":
//						strsql="select a.vcDeptID,c.vcGoodsName,sum(a.iCount) as tolcount,sum(a.nFee) as tolfee";
//						strsql+=" from vwConsItem a,tbAssociator b,tbGoods c,tbCommCode d";
//						strsql+=" where a.cFlag='0' and d.vcCommSign='GT' and c.vcGoodsName like '%"+htPara["strGoodsName"].ToString()+"%' and a.vcCardID=b.vcCardID and a.vcGoodsID=c.vcGoodsID and substring(a.vcGoodsID,1,2) between substring(d.vcCommCode,1,2) and substring(d.vcCommCode,4,2) and a.dtConsDate between '" + htPara["strBegin"].ToString() + "' and '" + htPara["strEnd"].ToString() + " 23:59:59' ";
//						if(htPara["strDeptID"].ToString()!="")
//						{
//							strsql+=" and a.vcDeptID='"+htPara["strDeptID"].ToString()+"'";
//						}
//						if(htPara["strAssType"].ToString()!="")
//						{
//							strsql+=" and b.vcAssType='"+htPara["strAssType"].ToString()+"'";
//						}
//						if(htPara["strGoodsType"].ToString()!="")
//						{
//							strsql+=" and d.vcCommCode='"+htPara["strGoodsType"].ToString()+"'";
//						}
//						strsql+=" group by a.vcDeptID,c.vcGoodsName order by a.vcDeptID,c.vcGoodsName";
//						break;
//					case "5":
//						strsql="select b.vcAssType,d.vcCommName as 商品类型,c.vcGoodsName,sum(a.iCount) as tolcount,sum(a.nFee) as tolfee";
//						strsql+=" from vwConsItem a,tbAssociator b,tbGoods c,tbCommCode d";
//						strsql+=" where a.cFlag='0' and d.vcCommSign='GT' and c.vcGoodsName like '%"+htPara["strGoodsName"].ToString()+"%' and a.vcCardID=b.vcCardID and a.vcGoodsID=c.vcGoodsID and substring(a.vcGoodsID,1,2) between substring(d.vcCommCode,1,2) and substring(d.vcCommCode,4,2) and a.dtConsDate between '" + htPara["strBegin"].ToString() + "' and '" + htPara["strEnd"].ToString() + " 23:59:59' ";
//						if(htPara["strDeptID"].ToString()!="")
//						{
//							strsql+=" and a.vcDeptID='"+htPara["strDeptID"].ToString()+"'";
//						}
//						if(htPara["strAssType"].ToString()!="")
//						{
//							strsql+=" and b.vcAssType='"+htPara["strAssType"].ToString()+"'";
//						}
//						if(htPara["strGoodsType"].ToString()!="")
//						{
//							strsql+=" and d.vcCommCode='"+htPara["strGoodsType"].ToString()+"'";
//						}
//						strsql+=" group by b.vcAssType,d.vcCommName,c.vcGoodsName order by b.vcAssType,d.vcCommName,c.vcGoodsName";
//						break;
//					case "6":
//						strsql="select a.vcDeptID,b.vcAssType,c.vcGoodsName,sum(a.iCount) as tolcount,sum(a.nFee) as tolfee";
//						strsql+=" from vwConsItem a,tbAssociator b,tbGoods c,tbCommCode d";
//						strsql+=" where a.cFlag='0' and d.vcCommSign='GT' and c.vcGoodsName like '%"+htPara["strGoodsName"].ToString()+"%' and a.vcCardID=b.vcCardID and a.vcGoodsID=c.vcGoodsID and substring(a.vcGoodsID,1,2) between substring(d.vcCommCode,1,2) and substring(d.vcCommCode,4,2) and a.dtConsDate between '" + htPara["strBegin"].ToString() + "' and '" + htPara["strEnd"].ToString() + " 23:59:59' ";
//						if(htPara["strDeptID"].ToString()!="")
//						{
//							strsql+=" and a.vcDeptID='"+htPara["strDeptID"].ToString()+"'";
//						}
//						if(htPara["strAssType"].ToString()!="")
//						{
//							strsql+=" and b.vcAssType='"+htPara["strAssType"].ToString()+"'";
//						}
//						if(htPara["strGoodsType"].ToString()!="")
//						{
//							strsql+=" and d.vcCommCode='"+htPara["strGoodsType"].ToString()+"'";
//						}
//						strsql+=" group by a.vcDeptID,b.vcAssType,c.vcGoodsName order by  a.vcDeptID,b.vcAssType,c.vcGoodsName";
//						break;
//					case "7":
//						strsql="select a.vcDeptID,d.vcCommName as 商品类型,c.vcGoodsName,sum(a.iCount) as tolcount,sum(a.nFee) as tolfee";
//						strsql+=" from vwConsItem a,tbAssociator b,tbGoods c,tbCommCode d";
//						strsql+=" where a.cFlag='0' and d.vcCommSign='GT' and c.vcGoodsName like '%"+htPara["strGoodsName"].ToString()+"%' and a.vcCardID=b.vcCardID and a.vcGoodsID=c.vcGoodsID and substring(a.vcGoodsID,1,2) between substring(d.vcCommCode,1,2) and substring(d.vcCommCode,4,2) and a.dtConsDate between '" + htPara["strBegin"].ToString() + "' and '" + htPara["strEnd"].ToString() + " 23:59:59' ";
//						if(htPara["strDeptID"].ToString()!="")
//						{
//							strsql+=" and a.vcDeptID='"+htPara["strDeptID"].ToString()+"'";
//						}
//						if(htPara["strAssType"].ToString()!="")
//						{
//							strsql+=" and b.vcAssType='"+htPara["strAssType"].ToString()+"'";
//						}
//						if(htPara["strGoodsType"].ToString()!="")
//						{
//							strsql+=" and d.vcCommCode='"+htPara["strGoodsType"].ToString()+"'";
//						}
//						strsql+=" group by a.vcDeptID,d.vcCommName,c.vcGoodsName order by a.vcDeptID,d.vcCommName,c.vcGoodsName";
//						break;
//				}
				if(querytype=="")
				{
					return dtCons;
				}
				else
				{
					dtCons=SqlHelper.ExecuteDataTable(con,CommandType.Text,strsql);
				}
				
				return dtCons;
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
			
		}

		public DataTable GetBusiLogQuery(Hashtable htPara)
		{
			DataTable dtBusiLog=new DataTable();
			try
			{
				string strCondition="";
				if(htPara["strOperName"].ToString()!=""&&htPara["strOperName"].ToString()!="*")
				{
					strCondition=" a.vcOperName='" + htPara["strOperName"].ToString() + "'";
				}
				if(htPara["strDeptID"].ToString()!="")
				{
					if(strCondition=="")
					{
						strCondition=" a.vcDeptID='" + htPara["strDeptID"].ToString() + "'";
					}
					else
					{
						strCondition=strCondition + " and a.vcDeptID = '" + htPara["strDeptID"].ToString() + "'";
					}
				}
				if(htPara["strCardID"].ToString()!="")
				{
					if(strCondition=="")
					{
						strCondition=" b.vcCardID='" + htPara["strCardID"].ToString() + "'";
					}
					else
					{
						strCondition=strCondition + " and b.vcCardID = '" + htPara["strCardID"].ToString() + "'";
					}
				}
				string sql1="select a.iSerial,b.vcAssName,b.vcAssType,a.vcCardID,c.vcCommName,a.vcOperName,a.dtOperDate,a.vcDeptID,a.vcComments from vwBusiLog a,tbAssociator b,tbCommCode c where a.vcCardID=b.vcCardID and a.vcOperType=c.vcCommCode and c.vcCommSign='OP' and a.dtOperDate between '" +  htPara["strBegin"].ToString() + "' and '" +  htPara["strEnd"].ToString() + " 23:59:59' ";
				if(strCondition!="")
				{
					sql1+="and a.dtOperDate between b.dtCreateDate and case when b.vcAssState=3 then  b.dtOperDate else GETDATE() end ";
					sql1+=" and " + strCondition;
				}
				sql1+=" order by a.dtOperDate";
				dtBusiLog=SqlHelper.ExecuteDataTable(con,CommandType.Text,sql1);
				return dtBusiLog;
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
		}

		public DataTable GetTopQuery(Hashtable htPara,string strtype)
		{
			DataTable dttop=new DataTable();
			try
			{
				string sql1="";
				if(strtype=="0")
				{
					if(htPara["strDeptID"].ToString()!="")
					{
						sql1="select b.vcGoodsName,sum(iCount) as salecount,sum(nFee) 营业额 ,cast (sum(iCount)/case when sum(nFee)=0 then 1 else sum(nFee) end as numeric(18,2))  千元用量 from vwConsItem a,tbGoods b where a.vcGoodsID=b.vcGoodsID and a.dtConsDate between '" + htPara["strBegin"].ToString() +"' and '" + htPara["strEnd"].ToString() +" 23:59:59' and a.vcDeptID like '" + htPara["strDeptID"].ToString() + "' group by vcGoodsName order by sum(iCount) desc";
					}
					else
					{
						sql1="select b.vcGoodsName,sum(iCount) as salecount,sum(nFee) 营业额 ,cast (sum(iCount)/case when sum(nFee)=0 then 1 else sum(nFee) end as numeric(18,2))  千元用量  from vwConsItem a,tbGoods b where a.vcGoodsID=b.vcGoodsID and a.dtConsDate between '" + htPara["strBegin"].ToString() +"' and '" + htPara["strEnd"].ToString() +" 23:59:59' group by vcGoodsName order by sum(iCount) desc";
					}
					
				}
				else
				{
					if(htPara["strDeptID"].ToString()!="")
					{
						sql1="select a.vcCardID,vcAssName,vcLinkPhone as 会员联系电话,sum(nFee) as salefee from vwConsItem a,tbAssociator b where b.vcAssType<>'AT002' and a.vcCardID=b.vcCardID and a.dtConsDate between '" + htPara["strBegin"].ToString() +"' and '" + htPara["strEnd"].ToString() +" 23:59:59' and a.vcDeptID like '" + htPara["strDeptID"].ToString() + "' and a.vcCardID<>'V9999' group by a.vcCardID,vcAssName,vcLinkPhone order by sum(nFee) desc";
					}
					else
					{
						sql1="select a.vcCardID,vcAssName,vcLinkPhone as 会员联系电话,sum(nFee) as salefee from vwConsItem a,tbAssociator b where b.vcAssType<>'AT002' and a.vcCardID=b.vcCardID and a.dtConsDate between '" + htPara["strBegin"].ToString() +"' and '" + htPara["strEnd"].ToString() +" 23:59:59' and a.vcCardID<>'V9999' group by a.vcCardID,vcAssName,vcLinkPhone order by sum(nFee) desc";
					}
					
				}
				dttop=SqlHelper.ExecuteDataTable(con,CommandType.Text,sql1);
				return dttop;
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
		}

		public DataSet BusiIncomeReport(Hashtable htPara)
		{
			DataSet dsincome=new DataSet();
			try
			{
				string sql1="exec sp_BusiIncomeReport '" + htPara["strDeptID"].ToString() + "','" + htPara["strBegin"].ToString() + "','" + htPara["strEnd"].ToString() + "','" + htPara["strYestoday"].ToString() + "'";
				SqlHelper.ExecuteNonQuery(con,CommandType.Text,sql1);

				string sql2="select Type,REP1,REP2,REP3,REP4,REP5,REP6,REP7 from tbBusiIncomeReport where vcDateZoom='" + htPara["strBegin"].ToString()+ htPara["strEnd"].ToString() +"' and vcDeptID='"+htPara["strDeptID"].ToString()+"' and Type not like 'Local%' and Type not like 'Other%' order by ReNo";
				DataTable dtTmp=SqlHelper.ExecuteDataTable(con,CommandType.Text,sql2);
				dtTmp.TableName="AllIncome";
				dsincome.Tables.Add(dtTmp);

				dtTmp=new DataTable();
				string sql3="select Type,REP1,REP2,REP3,REP4,REP5,REP6,REP7 from tbBusiIncomeReport where vcDateZoom='" + htPara["strBegin"].ToString()+htPara["strEnd"].ToString() +"' and vcDeptID='"+htPara["strDeptID"].ToString()+"' and Type like 'Local-%' order by ReNo";
				dtTmp=SqlHelper.ExecuteDataTable(con,CommandType.Text,sql3);
				dtTmp.TableName="LocalIncome";
				dsincome.Tables.Add(dtTmp);

				dtTmp=new DataTable();
				string sql4="select Type,REP1,REP2,REP3,REP4,REP5,REP6,REP7 from tbBusiIncomeReport where vcDateZoom='" + htPara["strBegin"].ToString()+htPara["strEnd"].ToString() +"' and vcDeptID='"+htPara["strDeptID"].ToString()+"' and Type like 'Other-%' order by ReNo";
				dtTmp=SqlHelper.ExecuteDataTable(con,CommandType.Text,sql4);
				dtTmp.TableName="OtherIncome";
				dsincome.Tables.Add(dtTmp);

				dtTmp=new DataTable();
				string sql5="select Type,REP1,REP2,REP3,REP4,REP5,REP6,REP7 from tbBusiIncomeReport where vcDateZoom='" + htPara["strBegin"].ToString()+htPara["strEnd"].ToString() +"' and vcDeptID='"+htPara["strDeptID"].ToString()+"' and Type like 'LocalToOther%' order by ReNo";
				dtTmp=SqlHelper.ExecuteDataTable(con,CommandType.Text,sql5);
				dtTmp.TableName="LocalToOtherIncome";
				dsincome.Tables.Add(dtTmp);

				return dsincome;
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
		}

		public DataTable GetAssInfo(Hashtable htPara)
		{
			DataTable dtAss=new DataTable();
			try
			{
				string strCondition="";
				if(htPara["strCardID"].ToString()!=""&&htPara["strCardID"].ToString()!="*")
				{
					strCondition=" vcCardID like '" + htPara["strCardID"].ToString() + "%'";
				}
				if(htPara["strAssName"].ToString()!=""&&htPara["strAssName"].ToString()!="*")
				{
					if(strCondition=="")
					{
						strCondition=" vcAssName like '%" + htPara["strAssName"].ToString() + "%'";
					}
					else
					{
						strCondition=strCondition + " and vcAssName like '%" + htPara["strAssName"].ToString() + "%'";
					}
				}
				if(htPara["strAssType"].ToString()!=""&&htPara["strAssType"].ToString()!="*")
				{
					if(strCondition=="")
					{
						strCondition=" vcAssType ='" + htPara["strAssType"].ToString() + "'";
					}
					else
					{
						strCondition=strCondition + " and vcAssType = '" + htPara["strAssType"].ToString() + "'";
					}
				}
				if(htPara["strAssState"].ToString()!=""&&htPara["strAssState"].ToString()!="*")
				{
					if(strCondition=="")
					{
						strCondition=" vcAssState ='" + htPara["strAssState"].ToString() + "'";
					}
					else
					{
						strCondition=strCondition + " and vcAssState = '" + htPara["strAssState"].ToString() + "'";
					}
				}
				if(htPara["strDeptID"].ToString()!=""&&htPara["strDeptID"].ToString()!="*")
				{
					if(strCondition=="")
					{
						strCondition=" vcDeptID ='" + htPara["strDeptID"].ToString() + "'";
					}
					else
					{
						strCondition=strCondition + " and vcDeptID = '" + htPara["strDeptID"].ToString() + "'";
					}
				}

				if(htPara["strLinkPhone"].ToString()!=""&&htPara["strLinkPhone"].ToString()!="*")
				{
					if(strCondition=="")
					{
						strCondition=" vcLinkPhone like '%" + htPara["strLinkPhone"].ToString() + "%'";
					}
					else
					{
						strCondition=strCondition + " and vcLinkPhone like '%" + htPara["strLinkPhone"].ToString() + "%'";
					}
				}

//				string sql1="select a.[vcCardID], [vcAssName], [vcLinkPhone], [vcAssState], [nCharge], [vcDeptID], [dtCreateDate],b.会员消费额, [vcLinkAddress], [dtOperDate] from tbAssociator a ";
//				sql1+=" left join (select vcCardId,sum(nFee) as 会员消费额  from vwConsItem group by vcCardId) b on a.vcCardId=b.vcCardId ";
//				sql1+=" where a.vcCardID<>'V9999' and vcAssType<>'AT999'";
//				sql1+=" and dtCreateDate between '"+htPara["strBeginDate"].ToString()+"' and '"+htPara["strEndDate"].ToString()+" 23:59:59'";

				string sql1="select [vcCardID], [vcAssName], [vcLinkPhone], [vcAssState], [nCharge], [vcDeptID], [dtCreateDate],[vcLinkAddress], [dtOperDate],vcComments from tbAssociator ";
				sql1+=" where vcCardID<>'V9999' and vcAssType<>'AT999'";
				sql1+=" and dtCreateDate between '"+htPara["strBeginDate"].ToString()+"' and '"+htPara["strEndDate"].ToString()+" 23:59:59'";
				if(strCondition!="")
				{
					sql1+=" and " + strCondition;
				}
				sql1+=" order by dtCreateDate desc";
				dtAss=SqlHelper.ExecuteDataTable(con,CommandType.Text,sql1);

				return dtAss;
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
		}
        public DataTable GetAssInfo(string strCardId,string strAssState)
        {
            DataTable dtAss = new DataTable();
            try
            {
                string sql1 = "select a.vcCardId,a.iAssId,a.vcAssName,a.vcLinkPhone,a.vcLinkAddress,b.vcAssTypeName as vcAssType,c.vcAssStateName as vcAssState, "
+" a.nCharge,a.dtCreateDate,a.dtOperDate,d.vcDeptName from tbAssociator a "
+" left join (select vcCommName as vcAssTypeName,vcCommCode as vcAssTypeCode from tbCommCode where vcCommSign='AT') b on a.vcAssType=b.vcAssTypeCode "
+" left join (select vcCommName as vcAssStatename,vcCommCode as vcAssStateCode from tbCommCode where vcCommSign='AS') c on a.vcAssState=c.vcAssStateCode "
+" left join (select vcCommName as vcDeptName,vcCommCode as vcDeptId from tbCommCode where vcCommSign='MD') d on a.vcDeptId=d.vcDeptId "
+" where a.vcCardID<>'V9999' and a.vcAssType<>'AT999' and a.vcCardId='"+strCardId+"' and a.vcAssState='"+strAssState+"'";                
                dtAss = SqlHelper.ExecuteDataTable(con, CommandType.Text, sql1);

                return dtAss;
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
        }
		public DataTable GetUpDownQuery(Hashtable htPara)
		{
			DataTable dtuplog=new DataTable();
			try
			{
				string sql1="";
				switch(htPara["strquerytype"].ToString())
				{
					case "0":
						sql1="select substring(vcFileName,1,5) as Dept,(case Type when 'CEN' then '会员数据-中心至分店' when 'MD' then '业务数据-分店至中心' when 'PARA' then '基本参数-中心至分店' when 'GOODS' then '商品数据-中心至分店' else Type end) as Type,vcFileName,FileSize,dtStartDate,dtFinDate from tbDataSoftUpdateLog where dtStartDate between '"+htPara["strBegin"].ToString()+"' and '"+htPara["strEnd"].ToString()+" 23:59:59' order by Type,dtStartDate desc";
						break;
					case "1":
						sql1="select substring(vcFileName,1,5) as Dept,'业务数据-分店至中心' as Type,vcFileName,FileSize,dtStartDate,dtFinDate from tbDataSoftUpdateLog where Type='MD' and vcFileName like '%"+ htPara["strDept"].ToString() +"%' and dtStartDate between '"+htPara["strBegin"].ToString()+"' and '"+htPara["strEnd"].ToString()+" 23:59:59' order by dtStartDate desc,Type";
						break;
					case "2":
						sql1="select substring(vcFileName,1,5) as Dept,'会员数据-中心至分店' as Type,vcFileName,FileSize,dtStartDate,dtFinDate from tbDataSoftUpdateLog where Type='CEN' and dtStartDate between '"+htPara["strBegin"].ToString()+"' and '"+htPara["strEnd"].ToString()+" 23:59:59' order by dtStartDate desc";
						break;
					case "3":
						sql1="select substring(vcFileName,1,5) as Dept,'基本参数-中心至分店' as Type,vcFileName,FileSize,dtStartDate,dtFinDate from tbDataSoftUpdateLog where Type='PARA' and dtStartDate between '"+htPara["strBegin"].ToString()+"' and '"+htPara["strEnd"].ToString()+" 23:59:59' order by dtStartDate desc";
						break;
					case "4":
						sql1="select substring(vcFileName,1,5) as Dept,'商品数据-中心至分店' as Type,vcFileName,FileSize,dtStartDate,dtFinDate from tbDataSoftUpdateLog where Type='GOODS' and dtStartDate between '"+htPara["strBegin"].ToString()+"' and '"+htPara["strEnd"].ToString()+" 23:59:59' order by dtStartDate desc";
						break;
				}
				dtuplog=SqlHelper.ExecuteDataTable(con,CommandType.Text,sql1);
				return dtuplog;
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
		}

		public DataTable GetDailyCashQuery(Hashtable htPara)
		{
			DataTable dtCons=new DataTable();
			try
			{
				string strCondition="";
				if(htPara["strDeptID"].ToString()!=""&&htPara["strDeptID"].ToString()!="*")
				{
					strCondition=" and vcDeptID='" + htPara["strDeptID"].ToString() + "'";
				}
				if(htPara["strOperName"].ToString()!=""&&htPara["strOperName"].ToString()!="*")
				{
					strCondition=strCondition + " and vcOperName = '" + htPara["strOperName"].ToString() + "'";
				}

				string sql1="select vcOperName,vcConsType,count(*) as ConsCount,isnull(sum(nFee),0) as ConsFee from vwBill where dtConsDate between '"+htPara["strBegin"].ToString()+"' and '"+htPara["strEnd"].ToString()+" 23:59:59' "+strCondition+" and iSerial not in(select distinct iSerial from vwConsItem where cFlag='9' and dtConsDate between '"+htPara["strBegin"].ToString()+"' and '"+htPara["strEnd"].ToString()+" 23:59:59' "+strCondition+") group by vcOperName,vcConsType";
				sql1+= " union all select vcOperName,'Fill' as vcConsType,count(*) as ConsCount,isnull(sum(nFillFee),0) as ConsFee from vwFillFee where nFillFee>0 and dtFillDate between '"+htPara["strBegin"].ToString()+"' and '"+htPara["strEnd"].ToString()+" 23:59:59' "+strCondition+" and vcComments<>'银行卡' and vcComments not like '%补卡%' and vcComments not like '%补充值%' and vcComments not like '%消费撤消%' and vcComments not like '回收卡%' and vcComments not like '合并%'and vcComments not like '充值撤消%'  group by vcOperName";
				sql1+= " union all select vcOperName,'FillBank' as vcConsType,count(*) as ConsCount,isnull(sum(nFillFee),0) as ConsFee from vwFillFee where dtFillDate between '"+htPara["strBegin"].ToString()+"' and '"+htPara["strEnd"].ToString()+" 23:59:59' "+strCondition+" and nFillFee>0 and vcComments='银行卡' and vcComments not like '%补卡%' and vcComments not like '%补充值%' and vcComments not like '%消费撤消%' and vcComments not like '回收卡%' and vcComments not like '合并%' and vcComments not like '充值撤消%' group by vcOperName";
				sql1+= " union all select vcOperName,'CradRoll',count(1) as ConsCount,isnull(sum(nFillFee),0)as ConsFee from vwFillFee where dtFillDate between '"+htPara["strBegin"].ToString()+"' and '"+htPara["strEnd"].ToString()+" 23:59:59' "+strCondition+" and vcComments  like '回收卡%' group by vcOperName";
				sql1+=" order by vcOperName,vcConsType";
	
				dtCons=SqlHelper.ExecuteDataTable(con,CommandType.Text,sql1);
				return dtCons;
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
		}
//		private int IsFirstDayOfMonth(string strdatetime)
//		{
//			DateTime datetime = Convert.ToDateTime(strdatetime);
//			return datetime == datetime.AddDays(1 - datetime.Day);
//		}
		private int LastDayOfMonth(string strdatetime)
		{
			DateTime datetime = Convert.ToDateTime(strdatetime);
			return datetime.AddDays(1 - datetime.Day).AddMonths(1).AddDays(-1).Day;
		}
		public DataTable GetSalesSum(string year,string month,string nextmonth)
		{

			DataTable dt=new DataTable();
			try
			{
			string strsql = "";
			string strdays = "";
			string strsumdays = "";
			//int lastDay = LastDayOfMonth(year+"-"+month+"-1");
			for(int i=1;i<=31;i++)
			{
				strdays +="["+i.ToString()+"],";
				strsumdays +="sum(["+i.ToString()+"]) as ["+i.ToString()+"],";
			}
			strdays = strdays.TrimEnd(',');
			strsumdays = strsumdays.TrimEnd(',');


			strsql = "select cnvcDeptId as 门店,{0} from ";
strsql+="( ";
strsql+=" select cnvcDeptId,{1} from ";
strsql+=" (select cnvcDeptID,day(cndBusinessDate) as dtConsDate,cast(SUM(cnnPayable_Sale-cnnDIf_More) as numeric(18,2)) as nFee from tbDifference where YEAR(cndBusinessDate)={3}  and cnvcType='部门'";
				if(month!="")
				{
					strsql+= " and MONTH(cndBusinessDate)>={4} ";
				}
				if(nextmonth!="")
				{
					strsql+= " and MONTH(cndBusinessDate)<={5} ";
				}
strsql+=" group by cnvcDeptId,day(cndBusinessDate)) a ";
strsql+=" pivot ";
strsql+=" (sum(nFee) for dtConsDate in ({2}) )as pvt ";
strsql+=" ) b ";
strsql+=" group by rollup(cnvcDeptId)";
			strsql = string.Format(strsql,strsumdays,strdays,strdays,year,month,nextmonth);

				dt=SqlHelper.ExecuteDataTable(con,CommandType.Text,strsql);
				return dt;
			}
			catch (Exception ex) 
			{
				clog.WriteLine(ex);
				throw ex;
			}
			finally
			{
				if(con.State==ConnectionState.Open)
				{
					con.Close();
				}
			}
		}

		public DataTable GetTimeSales(string strDeptId,string strBeginDate,string strEndDate)
		{
			DataTable dt=new DataTable();
			try
			{
				string deptCond = "";
				if(strDeptId!="")
				{
					deptCond = " vcDeptId='"+strDeptId+"' and ";
				}
				string strsql1 = @"select '销售' as 时段,[7],[8],[9],[10],[11],[12],[13],[14],[15],[16],[17],[18],[19],[20],[21],[22] from ";
strsql1+=" (select DATEPART(HH,dtConsDate) as dtConsDate,SUM(nFee) as nFee from vwConsItem where {0}Convert(varchar(10),dtConsDate,121)>='{1}' and Convert(varchar(10),dtConsDate,121)<='{2}' ";
strsql1+=" group by DATEPART(HH,dtConsDate)) a ";
strsql1+=" pivot ";
strsql1+=" (sum(nFee) for dtConsDate in ([7],[8],[9],[10],[11],[12],[13],[14],[15],[16],[17],[18],[19],[20],[21],[22]) )as pvt";

				string strsql2 = @"select '来客次数' as 时段,[7],[8],[9],[10],[11],[12],[13],[14],[15],[16],[17],[18],[19],[20],[21],[22] from ";
strsql2 += " (select DATEPART(HH,dtConsDate) as dtConsDate,COUNT(1) as nFee from vwConsItem where {0}Convert(varchar(10),dtConsDate,121)>='{1}' and Convert(varchar(10),dtConsDate,121)<='{2}' ";
strsql2 += " group by DATEPART(HH,dtConsDate)) a ";
strsql2 += " pivot ";
strsql2 += " (sum(nFee) for dtConsDate in ([7],[8],[9],[10],[11],[12],[13],[14],[15],[16],[17],[18],[19],[20],[21],[22]) )as pvt";

				DataTable dt1 = SqlHelper.ExecuteDataTable(con,CommandType.Text,string.Format(strsql1,deptCond,strBeginDate,strEndDate));
				DataTable dt2 = SqlHelper.ExecuteDataTable(con,CommandType.Text,string.Format(strsql2,deptCond,strBeginDate,strEndDate));
				
				dt = dt1.Copy();
				if(dt2.Rows.Count>0)
				{
					dt.Rows.Add(dt2.Rows[0].ItemArray);
				

					DataRow dr = dt.NewRow();
					dr["时段"] = "客单价";
					for(int i=7;i<23;i++)
					{
						string str1 = dt1.Rows[0][i.ToString()].ToString();
						if(str1=="")
						{
							str1="0";
						}
						string str2 = dt2.Rows[0][i.ToString()].ToString();
						if(str2 == "")
						{
							str2="1";
						}
						dr[i.ToString()] = Math.Round(Convert.ToDecimal(str1)/Convert.ToDecimal(str2),2);
					}
					dt.Rows.Add(dr);
				}
				return dt;
			}
			catch (Exception ex) 
			{
				clog.WriteLine(ex);
				throw ex;
			}
			finally
			{
				if(con.State==ConnectionState.Open)
				{
					con.Close();
				}
			}
		}

        public DataTable GetSaleRatio(string months,string goodsType)
        {

            DataTable dt = new DataTable();
            try
            {
                DateTime BeginDate = DateTime.Parse(months + "-01");
                DateTime EndDate = BeginDate.AddMonths(1);
                string strBeginDate = BeginDate.ToString("yyyy-MM-dd");
                string strEndDate = EndDate.ToString("yyyy-MM-dd");
                string strsql = @"
select vcDeptName as [门店],{0}
from (select b.vcCommName as vcDeptName,c.vcCommName as vcGoodsType,sum(nFee) as nFee from tbConsItemOther a
left join (select * from tbCommCode where vcCommSign='MD') b on a.vcDeptId=b.vcCommCode
left join (select * from tbCommCode where vcCommSign='GT') c on substring(a.vcGoodsID,1,2) between substring(c.vcCommCode,1,2) and substring(c.vcCommCode,4,2)
where dtConsDate between '{1}' and '{2}' and a.cFlag='0'
group by b.vcCommName,c.vcCommName) d
pivot
(
Sum(nFee) for d.vcGoodsType in ({0}
)) as pvt";
                strsql = string.Format(strsql, goodsType,strBeginDate,strEndDate);

                dt = SqlHelper.ExecuteDataTable(con, CommandType.Text, strsql);
                return dt;
            }
            catch (Exception ex)
            {
                clog.WriteLine(ex);
                throw ex;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }

        public DataSet GetKPI(DateTime dtDate, string weather, string exception,bool IsUpdate)
        {

            DataSet ds = new DataSet();
            try
            {                
                DayOfWeek dwWeek = dtDate.DayOfWeek;
                int week = (int)dwWeek;
                string strDate = dtDate.ToString("yyyy-MM-dd");
                if (IsUpdate)
                {
                    int count = SqlHelper.ExecuteNonQuery(con, CommandType.Text, string.Format("delete from KPIOfDay where [Date]='{0}'", strDate));
                    count = SqlHelper.ExecuteNonQuery(con, CommandType.Text, string.Format("delete from KPIOfDept where [Date]='{0}'", strDate));
                    count = SqlHelper.ExecuteNonQuery(con, CommandType.Text, string.Format("delete from KPIOfGoods where [Date]='{0}'", strDate));
                }
                string strDate1 = dtDate.AddDays(1).ToString("yyyy-MM-dd");
                string strBeforeDate = dtDate.AddDays(-1).ToString("yyyy-MM-dd");
                DateTime dtFirstDate = new DateTime(dtDate.Year, dtDate.Month, 1);
                DateTime dtLastDate = new DateTime(dtDate.Year, dtDate.Month, DateTime.DaysInMonth(dtDate.Year, dtDate.Month));
                string strFirstDate = dtFirstDate.ToString("yyyy-MM-dd");
                string strLastDate = dtLastDate.ToString("yyyy-MM-dd");

                string strConstSql1 = @"insert into KPIOfDept
select '{0}' [Date],a.vcDeptId,a.Amount,b.[Sum] from 
(select vcDeptId,sum(nFee) Amount  from tbConsItemOther
where dtConsDate between '{0}' and '{1}' and cFlag='0'
group by vcDeptId) a
join
(select vcDeptId,sum(nFee) [Sum]  from tbConsItemOther
where dtConsDate between '{2}' and '{0}' and cFlag='0'
group by vcDeptId) b on a.vcDeptId=b.vcDeptId";

                string strConstSql2 = "select * from KPIOfDept where [Date]='{0}'";

                string strConstSql3 = "select * from KPIOfDay where [Date]='{0}'";

                string strConstSql4 = "select Count(1) Quantity from  vwConsItem where cFlag='0' and dtConsDate between '{0}' and '{1}'";

                string strConstSql5 = "insert into KPIOfDay([Date],[Week],[Weather],[Amount],[Sum],[Quantity],[Price],[Exception]) values('{0}',{1},'{2}',{3},{4},{5},{6},'{7}')";

                string strConstSql6 = @"insert into KPIOfGoods
select '{0}' [Date],a.vcGoodsId,a.Amount,b.[Sum],a.Quantity,b.Total from 
(select vcGoodsId,sum(iCount) Quantity,sum(nFee) Amount from tbConsItemOther 
where dtConsDate between '{0}' and '{1}' and cFlag='0'
and vcGoodsId in (select vcGoodsId from tbGoods where IsNew=1 or IsKey=1)
group by vcGoodsId) a
join
(select vcGoodsId,sum(iCount) Total,sum(nFee) [Sum] from tbConsItemOther 
where dtConsDate between '{2}' and '{0}' and cFlag='0' 
and vcGoodsId in (select vcGoodsId from tbGoods where IsNew=1 or IsKey=1)
group by vcGoodsId) b on a.vcGoodsId=b.vcGoodsId";

                string strConstSql7 = "select * from KPIOfGoods where [Date]='{0}'";

                string strConstSql8 = "select * from KPIOfMonth where Month='{0}'";

                string strConstSql9 = "select * from tbGoods where IsNew=1 or IsKey=1";
                //去年同月业绩                
                DateTime dtLastYearDate = dtDate.AddYears(-1);
                string strLastYearDate = dtLastYearDate.ToString("yyyy-MM-dd");
                string strLastYearDate1 = dtLastYearDate.AddDays(1).ToString("yyyy-MM-dd");

                DateTime dtLastYearFirstDate = new DateTime(dtLastYearDate.Year, dtLastYearDate.Month, 1);
                string strLastYearFirstDate = dtLastYearFirstDate.ToString("yyyy-MM-dd");

                DateTime dtLastYearLastDate = new DateTime(dtLastYearDate.Year, dtLastYearDate.Month, DateTime.DaysInMonth(dtLastYearDate.Year, dtLastYearDate.Month));
                string strLastYearLastDate = dtLastYearLastDate.ToString("yyyy-MM-dd");

                string strsql1 = string.Format(strConstSql2, dtLastYearLastDate);
                DataTable dt1 = SqlHelper.ExecuteDataTable(con, CommandType.Text, strsql1);
                if (dt1 == null || dt1.Rows.Count==0)
                {
                    string strLastYearLastDate1 = dtLastYearLastDate.AddDays(1).ToString("yyyy-MM-dd");                    
                    string strsql2 = string.Format(strConstSql1, strLastYearLastDate, strLastYearLastDate1, strLastYearFirstDate);
                    int count = SqlHelper.ExecuteNonQuery(con, CommandType.Text, strsql2);
                    dt1 = SqlHelper.ExecuteDataTable(con, CommandType.Text, strsql1);
                }
                if (dt1 != null && dt1.Rows.Count > 0)
                {
                    dt1.TableName = "LastYearKPIOfDept";
                    ds.Tables.Add(dt1);
                }
                
                //门店业绩
                decimal sum = 0;
                decimal amount = 0;
                string strsql3 = string.Format(strConstSql2, strDate);
                DataTable dt2 = SqlHelper.ExecuteDataTable(con, CommandType.Text, strsql3);
                if (dt2 == null || dt2.Rows.Count == 0)
                {
                    string strsql4 = string.Format(strConstSql1, strDate, strDate1, strFirstDate);
                    int count = SqlHelper.ExecuteNonQuery(con, CommandType.Text, strsql4);
                    dt2 = SqlHelper.ExecuteDataTable(con, CommandType.Text, strsql3);
                }
                if (dt2 != null && dt2.Rows.Count > 0)
                {
                    dt2.TableName = "KPIOfDept";
                    ds.Tables.Add(dt2);
                    object obj1 = dt2.Compute("Sum([Sum])", "");
                    if (obj1 != null && obj1.ToString() != "")
                    {
                        sum = Convert.ToDecimal(obj1);
                    }
                    object obj2 = dt2.Compute("Sum([Amount])", "");
                    if (obj2 != null && obj2.ToString() != "")
                    {
                        amount = Convert.ToDecimal(obj2);
                    }
                }
                
                //去年同一天
                string strsql5 = string.Format(strConstSql2, strLastYearDate);
                DataTable dt3 = SqlHelper.ExecuteDataTable(con, CommandType.Text, strsql5);
                if (dt3 == null || dt3.Rows.Count == 0)
                {                    
                    string strsql6 = string.Format(strConstSql1, strLastYearDate, strLastYearDate1, strLastYearFirstDate);
                    int count = SqlHelper.ExecuteNonQuery(con, CommandType.Text, strsql6);
                    dt3 = SqlHelper.ExecuteDataTable(con, CommandType.Text, strsql5);
                    
                }
                if (dt3 != null && dt3.Rows.Count > 0)
                {
                    dt3.TableName = "LastYearDateKPIOfDept";
                    ds.Tables.Add(dt3);
                }
                //业绩
                string strsql7 = string.Format(strConstSql3, strDate);
                DataTable dt4 = SqlHelper.ExecuteDataTable(con, CommandType.Text, strsql7);
                if (dt4 == null || dt4.Rows.Count == 0)
                {
                    string strsql8 = string.Format(strConstSql4, strDate, strDate1);
                    object obj = SqlHelper.ExecuteScalar(con, CommandType.Text, strsql8);
                    int quantity = 0;
                    if (obj != null && obj.ToString() != "")
                    {
                        quantity = Convert.ToInt32(obj);
                    }
                    //if (quantity == 0) quantity = 1;
                    string strsql9 = string.Format(strConstSql5, strDate, week.ToString(),
                        weather, amount.ToString(), sum.ToString(),quantity.ToString(), (Math.Round(amount / (quantity==0?1:quantity), 2)).ToString(), exception);
                    int count = SqlHelper.ExecuteNonQuery(con, CommandType.Text, strsql9);
                    dt4 = SqlHelper.ExecuteDataTable(con, CommandType.Text, strsql7);
                }
                if (dt4 != null && dt4.Rows.Count > 0)
                {
                    dt4.TableName = "KPIOfDay";
                    ds.Tables.Add(dt4);
                }
                //新品及重点产品业绩
                string strsql10 = string.Format(strConstSql7, strDate);
                DataTable dt5 = SqlHelper.ExecuteDataTable(con, CommandType.Text, strsql10);
                if (dt5 == null || dt5.Rows.Count == 0)
                {
                    string strsql11 = string.Format(strConstSql6, strDate, strDate1, strFirstDate);
                    int count = SqlHelper.ExecuteNonQuery(con, CommandType.Text, strsql11);
                    dt5 = SqlHelper.ExecuteDataTable(con, CommandType.Text, strsql10);
                }
                if (dt5 != null && dt5.Rows.Count > 0)
                {
                    dt5.TableName = "KPIOfGoods";
                    ds.Tables.Add(dt5);
                }
                //指标
                string strMonth = dtDate.ToString("yyyy-MM");
                string strsql12 = string.Format(strConstSql8, strMonth);
                DataTable dt6 = SqlHelper.ExecuteDataTable(con, CommandType.Text, strsql12);
                if (dt6 != null && dt6.Rows.Count > 0)
                {
                    dt6.TableName = "KPIOfMonth";
                    ds.Tables.Add(dt6);
                }
                //前一天
                string strsql13 = string.Format(strConstSql2, strBeforeDate);
                DataTable dt7 = SqlHelper.ExecuteDataTable(con, CommandType.Text, strsql13);
                if (dt7 == null || dt7.Rows.Count == 0)
                {
                    string strsql14 = string.Format(strConstSql1, strBeforeDate, strDate, strFirstDate);
                    int count = SqlHelper.ExecuteNonQuery(con, CommandType.Text, strsql14);
                    dt7 = SqlHelper.ExecuteDataTable(con, CommandType.Text, strsql13);
                }
                if (dt7 != null && dt7.Rows.Count > 0)
                {
                    dt7.TableName = "BeforeDateKPIOfDept";
                    ds.Tables.Add(dt7);
                }

                DataTable dt8 = SqlHelper.ExecuteDataTable(con, CommandType.Text, strConstSql9);
                if (dt8 != null && dt8.Rows.Count > 0)
                {
                    dt8.TableName = "tbGoods";
                    ds.Tables.Add(dt8);
                }
                return ds;
            }
            catch (Exception ex)
            {
                clog.WriteLine(ex);
                throw ex;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }

        public DataTable GetKPIOfMonth(string month, string deptId)
        {
            DataTable dt = new DataTable();
            try
            {
                string sql = "select * from KPIOfMonth where 1=1 ";
                if (!string.IsNullOrEmpty(month))
                {
                    sql += " and [Month]='"+month+"'";
                }
                if (!string.IsNullOrEmpty(deptId))
                {
                    sql += " and vcDeptId='"+deptId+"'";
                }
                dt = SqlHelper.ExecuteDataTable(con, CommandType.Text, sql);                
                return dt;
            }
            catch (Exception ex)
            {
                clog.WriteLine(ex);
                throw ex;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }

        public int InsertKPI(string month, string deptId, decimal amount)
        {
            string sql = "insert into KPIOfMonth([Month],vcDeptId,Amount)values('{0}','{1}',{2})";
            int recount = SqlHelper.ExecuteNonQuery(con, CommandType.Text, string.Format(sql, month, deptId,amount));
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            return recount;
        }
        public int ExistKPI(string month, string deptId)
        {
            string sql = "select count(1) from KPIOfMonth where [Month]='{0}' and vcDeptId='{1}'";
            object obj = SqlHelper.ExecuteScalar(con, CommandType.Text, string.Format(sql, month, deptId));
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
        public int UpdateKPI(string month, string deptId, decimal amount)
        {
            string sql = "update KPIOfMonth set Amount={2} where [Month]='{0}' and vcDeptId='{1}'";
            int recount = SqlHelper.ExecuteNonQuery(con, CommandType.Text, string.Format(sql, month, deptId, amount));
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            return recount;
        }

        public int DeleteKPI(string month, string deptId)
        {
            string sql = "delete from KPIOfMonth where [Month]='{0}' and vcDeptId='{1}'";
            int recount = SqlHelper.ExecuteNonQuery(con, CommandType.Text, string.Format(sql, month, deptId));
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            return recount;
        }

        public DataTable ConsItemUndoQuery(string strSerial,string strBeginDate,string strEndDate,
            string strOperName,string strDeptId,string strIsUndo,string strFlag)
        {
            DataTable dtCons = new DataTable();
            try
            {
                string sql1 = @"select a.iSerial,a.vcGoodsId,b.vcGoodsName,--a.vcCardId,c.vcAssName,
a.nPrice,a.iCount,a.nTRate,a.nFee,a.vcComments,case a.cFlag when '0' then '正常消费' when '9' then '已撤消' else a.cFlag end as cFlag,a.dtConsDate,a.vcOperName,a.vcDeptId,d.vcDeptName,
case when e.iSerial is null then '否' else '是' end as IsUndo
from vwConsItem a
left join tbGoods b on a.vcGoodsId = b.vcGoodsId
--left join tbAssociator c on a.vcCardId=c.vcCardId
left join (select vcCommName as vcDeptName,vcCommCode as vcDeptId from tbCommCode where vcCommSign='MD') d on a.vcDeptId=d.vcDeptId
left join tbConsItemUndo e on a.iSerial=e.iSerial and a.vcDeptId=e.vcDeptId
where a.vcCardId='V9999'";
                if (!string.IsNullOrEmpty(strSerial))
                {
                    sql1 += " and a.iSerial="+strSerial;
                }
                if (!string.IsNullOrEmpty(strBeginDate))
                {
                    sql1 += " and a.dtConsDate>='"+strBeginDate+"'";
                }
                if (!string.IsNullOrEmpty(strEndDate))
                {
                    sql1 += " and a.dtConsDate<='"+strEndDate+"'";
                }
                if (strOperName != "全部")
                {
                    sql1 += " and a.vcOperName='"+strOperName+"'";
                }
                if (strDeptId != "全部")
                {
                    sql1 += " and a.vcDeptId='"+strDeptId+"'";
                }
                if (strIsUndo == "是")
                {
                    sql1 += " and e.iSerial is not null";
                }
                if (strIsUndo == "否")
                {
                    sql1 += " and e.iSerial is null";
                }
                if (!string.IsNullOrEmpty(strFlag))
                {
                    sql1 += "  and a.cFlag='"+strFlag+"'";
                }
                sql1 += " order by a.iSerial";
                dtCons = SqlHelper.ExecuteDataTable(con, CommandType.Text, sql1);
                return dtCons;
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

        }

        public int InsertConsItemUndo(string strSerial, string strOperName, string strDeptId)
        {
            string sql = "insert into tbConsItemUndo(iSerial,CreateDate,vcOperName,vcDeptId)values({0},getdate(),'{1}','{2}')";
            int recount = SqlHelper.ExecuteNonQuery(con, CommandType.Text, string.Format(sql, strSerial, strOperName, strDeptId));
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            return recount;
        }
        public int DeleteConsItemUndo(string strSerial, string strDeptId)
        {
            string sql = "delete from tbConsItemUndo where iSerial='{0}' and vcDeptId='{1}'";
            int recount = SqlHelper.ExecuteNonQuery(con, CommandType.Text, string.Format(sql, strSerial, strDeptId));
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            return recount;
        }
        public int ExistConsItemUndo(string strSerial, string strDeptId)
        {
            string sql = "select count(1) from tbConsItemUndo where iSerial='{0}' and vcDeptId='{1}'";
            object obj = SqlHelper.ExecuteScalar(con, CommandType.Text, string.Format(sql, strSerial, strDeptId));
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

        public void CheckConsItemUndo(string strSerial, string strDeptId,string strOperId)
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            SqlTransaction trans = con.BeginTransaction();
            string sql1 = "";
            try
            {
                sql1 = "update tbConsItemOther set cFlag='9' where iSerial="+strSerial+" and vcDeptId='"+strDeptId+"'";
                int icount = SqlHelper.ExecuteNonQuery(con, trans, CommandType.Text, sql1);
                if (icount == 0)
                    throw new Exception("审核失败");

                sql1 = "insert into tbOperLog values('撤销审核','" + strOperId + "','" + strDeptId + "',getdate(),'流水："+strSerial+"部门："+strDeptId+"')";
                SqlHelper.ExecuteNonQuery(con,trans, CommandType.Text, sql1);

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

        public int ExistDeptInfo(string strDeptName)
        {
            string sql = "select count(1) from tbDeptInfo where vcDeptName='{0}'";
            object obj = SqlHelper.ExecuteScalar(con, CommandType.Text, string.Format(sql, strDeptName));
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
        public int DeleteDeptInfo(string strDeptName)
        {
            string sql = "delete from tbDeptInfo where vcDeptName='{0}'";
            int recount = SqlHelper.ExecuteNonQuery(con, CommandType.Text, string.Format(sql, strDeptName));
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            return recount;
        }

        public int UpdateDeptInfo(string strOldDeptName, string strDeptName, string strAddress, string strTel, string strManager,string strManagerPhone, string strAdsl,string strAdslPwd, string strVpn,string strVpnPwd)
        {
            string sql = "update tbDeptInfo set vcDeptName='{1}',vcAddress='{2}',vcTel='{3}',vcManager='{4}',vcManagerPhone='{5}',vcAdsl='{6}',vcAdslPwd='{7}',vcVpn='{8}',vcVpnPwd='{9}' where vcDeptName='{0}'";
            int recount = SqlHelper.ExecuteNonQuery(con, CommandType.Text, string.Format(sql, strOldDeptName,strDeptName,strAddress,strTel,strManager,strManagerPhone,strAdsl,strAdslPwd,strVpn,strVpnPwd));
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            return recount;
        }
        public int InsertDeptInfo(string strDeptName, string strAddress, string strTel, string strManager,string strManagerPhone, string strAdsl,string strAdslPwd, string strVpn,string strVpnPwd)
        {
            string sql = "insert into tbDeptInfo(vcDeptName,vcAddress,vcTel,vcManager,vcManagerPhone,vcAdsl,vcAdslPwd,vcVpn,vcVpnPwd)values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}')";
            int recount = SqlHelper.ExecuteNonQuery(con, CommandType.Text, string.Format(sql, strDeptName, strAddress, strTel, strManager,strManagerPhone, strAdsl,strAdslPwd, strVpn,strVpnPwd));
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            return recount;
        }
        public DataTable DeptInfoQuery()
        {
            DataTable dtCons = new DataTable();
            try
            {
                string sql1 = "select * from tbDeptInfo";
                
                dtCons = SqlHelper.ExecuteDataTable(con, CommandType.Text, sql1);
                return dtCons;
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

        }
	}
}
