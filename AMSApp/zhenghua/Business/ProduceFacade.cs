using System;
using System.Data;
using System.Data.SqlClient;
using AMSApp.zhenghua.DataAccess;
using AMSApp.zhenghua.Common;
using AMSApp.zhenghua.QueryArgs;
using AMSApp.zhenghua.Entity;
namespace AMSApp.zhenghua.Business
{
	/// <summary>
	/// ProduceFacade 的摘要说明。
	/// </summary>
	public class ProduceFacade
	{
		public ProduceFacade()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		public void AddProduceLog(ProduceLog produceLog,OperLog operLog)//,BusiLog busiLog)
		{
			using (SqlConnection	conn  =  ConnectionPool.BorrowConnection())
			{
				//conn.Open();	
				
				SqlTransaction trans = conn.BeginTransaction();
				try
				{
					string strSysTime = SqlHelper.ExecuteScalar(trans, CommandType.Text, "select getdate()").ToString();
					DateTime dtSysTime = DateTime.Parse(strSysTime);

					OrderSerialNo serialNo = new OrderSerialNo();
					serialNo.cnvcFill = "0";
					serialNo.cnnSerialNo = Convert.ToDecimal(EntityMapping.Create(serialNo, trans));
					
					produceLog.cndOperDate = dtSysTime;
					produceLog.cnnProduceSerialNo = serialNo.cnnSerialNo;
					EntityMapping.Create(produceLog, trans);
					
					operLog.cndOperDate = dtSysTime;
					operLog.cnvcComments = "生产流水："+produceLog.cnnProduceSerialNo.ToString();

					string strOrderBookSql = "select count(*) from tbOrderBook where cnvcOrderState='0' and cnvcOrderState='0' and cndShipDate>='"+produceLog.cndShipBeginDate+"' and cndShipDate<='"+produceLog.cndShipEndDate+"' and cnvcProduceDeptID='"+produceLog.cnvcProduceDeptID+"'";
					object oCount = SqlHelper.ExecuteScalar(trans, CommandType.Text, strOrderBookSql);
					int iCount = int.Parse(oCount.ToString());
					if(iCount<1)
						throw new Exception("没有订单，不允许做计划！");
					EntityMapping.Create(operLog, trans);		

					trans.Commit();
				}
				catch(SqlException sex)
				{
					trans.Rollback();
					LogAdapter.WriteDatabaseException(sex);
					throw sex;
				}
				catch(Exception ex)
				{
					trans.Rollback();
					LogAdapter.WriteFeaturesException(ex);
					throw ex;
				}
				finally
				{
					ConnectionPool.ReturnConnection(conn);
				}
			}
		}

		public void UpdateProduceLog(ProduceLog produceLog,OperLog operLog)//,BusiLog busiLog)
		{
			using (SqlConnection	conn  =  ConnectionPool.BorrowConnection())
			{
				//conn.Open();	
				
				SqlTransaction trans = conn.BeginTransaction();
				try
				{
					string strSysTime = SqlHelper.ExecuteScalar(trans, CommandType.Text, "select getdate()").ToString();
					DateTime dtSysTime = DateTime.Parse(strSysTime);

//					OrderSerialNo serialNo = new OrderSerialNo();
//					serialNo.cnvcFill = "0";
//					serialNo.cnnSerialNo = Convert.ToDecimal(EntityMapping.Create(serialNo, trans));
					
					ProduceLog oldLog = new ProduceLog();
					oldLog.cnnProduceSerialNo = produceLog.cnnProduceSerialNo;
					oldLog = EntityMapping.Get(oldLog, trans) as ProduceLog;
					if(oldLog == null)
					{
						throw new Exception("生产计划为空");
					}
					oldLog.cndOperDate = dtSysTime;
					oldLog.cndProduceDate = produceLog.cndProduceDate;
					oldLog.cndShipBeginDate = produceLog.cndShipEndDate;
					oldLog.cndShipEndDate = produceLog.cndShipEndDate;
					oldLog.cnvcOperID = produceLog.cnvcOperID;
					oldLog.cnvcProduceDeptID = produceLog.cnvcProduceDeptID;

					
					EntityMapping.Update(oldLog, trans);
					
					operLog.cndOperDate = dtSysTime;
					operLog.cnvcComments = "生产流水："+produceLog.cnnProduceSerialNo.ToString();
					EntityMapping.Create(operLog, trans);	

					trans.Commit();
				}
				catch(SqlException sex)
				{
					trans.Rollback();
					LogAdapter.WriteDatabaseException(sex);
					throw sex;
				}
				catch(Exception ex)
				{
					trans.Rollback();
					LogAdapter.WriteFeaturesException(ex);
					throw ex;
				}
				finally
				{
					ConnectionPool.ReturnConnection(conn);
				}
			}
		}

		public void LindOrder(ProduceLog produceLog,OperLog operLog)//,BusiLog busiLog)
		{
			using (SqlConnection	conn  =  ConnectionPool.BorrowConnection())
			{
				//conn.Open();	
				
				SqlTransaction trans = conn.BeginTransaction();
				try
				{
					string strSysTime = SqlHelper.ExecuteScalar(trans, CommandType.Text, "select getdate()").ToString();
					DateTime dtSysTime = DateTime.Parse(strSysTime);

					//					OrderSerialNo serialNo = new OrderSerialNo();
					//					serialNo.cnvcFill = "0";
					//					serialNo.cnnSerialNo = Convert.ToDecimal(EntityMapping.Create(serialNo, trans));
					
					ProduceLog oldLog = new ProduceLog();
					oldLog.cnnProduceSerialNo = produceLog.cnnProduceSerialNo;
					oldLog = EntityMapping.Get(oldLog, trans) as ProduceLog;
					if(oldLog == null)
					{
						throw new Exception("生产计划为空");
					}
//					if(oldLog.cnvcProduceState != "0")
//					{
//						throw new Exception("生产计划状态不能关联订单");
//					}
					//锁定订单表
					string strOrderBookSql = "update tbOrderBook set cnvcOrderState='0' where cnvcOrderState='0' and cndShipDate>='"+oldLog.cndShipBeginDate+"' and cndShipDate<='"+oldLog.cndShipEndDate+"' and cnvcProduceDeptID='"+oldLog.cnvcProduceDeptID+"'";
					SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strOrderBookSql);

					
					//入生产计划产品细节表
					string strProduceDetailSql = " insert into tbProduceDetail(cnnProduceSerialNo,cnvcCode,cnvcName,cnvcUnit,cnnCount)  "
						+ "select '"+oldLog.cnnProduceSerialNo.ToString()+"' as cnnProduceSerialNo,a.cnvcProductCode,a.cnvcProductName,cnvcUnit,sum(a.cnnOrderCount) as cnnOrderCount from tbOrderBookDetail a "
						//+ " where a.cnnOrderSerialNo in (select cnnOrderSerialNo from tbOrderBook where cnvcOrderState='0' and cnvcOrderType <>'SELFPRODUCE' and cndShipDate>='"+oldLog.cndShipBeginDate+"' and cndShipDate<='"+oldLog.cndShipEndDate+"' and cnvcProduceDeptID='"+oldLog.cnvcProduceDeptID+"')  "
						+ " where a.cnnOrderSerialNo in (select cnnOrderSerialNo from tbOrderBook where cnvcOrderState='0' and cnvcOrderType in ('MDO','WDO') and cndShipDate>='"+oldLog.cndShipBeginDate+"' and cndShipDate<='"+oldLog.cndShipEndDate+"' and cnvcProduceDeptID='"+oldLog.cnvcProduceDeptID+"')  "
						+ " group by a.cnvcProductCode,a.cnvcProductName,a.cnvcUnit";
					SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strProduceDetailSql);

					//入生产流水订单流水关联表
					string strProduceOrderSql = "insert into tbProduceOrderLog(cnnProduceSerialNo,cnnOrderSerialNo,cnvcType) "
						+ " select " + oldLog.cnnProduceSerialNo + ",cnnOrderSerialNo,'0' from tbOrderBook"
						+ " where cnvcOrderState='0' and cnvcOrderType in ('MDO','WDO') and cndShipDate>='"+oldLog.cndShipBeginDate+"' and cndShipDate<='"+oldLog.cndShipEndDate+"' and cnvcProduceDeptID='"+oldLog.cnvcProduceDeptID+"'";
					SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strProduceOrderSql);

					//更新订单状态
					string strOrderSql = "update tbOrderBook set cnvcOrderState='1' where cnvcOrderState='0' and cnvcOrderType in ('MDO','WDO') and cndShipDate>='"+oldLog.cndShipBeginDate+"' and cndShipDate<='"+oldLog.cndShipEndDate+"' and cnvcProduceDeptID='"+oldLog.cnvcProduceDeptID+"'";
					SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strOrderSql);


					//更新计划状态
					if(oldLog.cnvcProduceState == "0")
					{
						oldLog.cnvcProduceState = "1";
						oldLog.cnvcOperID = produceLog.cnvcOperID;
						oldLog.cndOperDate = dtSysTime;

						EntityMapping.Update(oldLog, trans);
					}
					
					operLog.cndOperDate = dtSysTime;
					operLog.cnvcComments = "生产流水："+produceLog.cnnProduceSerialNo.ToString();
					EntityMapping.Create(operLog, trans);	

					trans.Commit();
				}
				catch(SqlException sex)
				{
					trans.Rollback();
					LogAdapter.WriteDatabaseException(sex);
					throw sex;
				}
				catch(Exception ex)
				{
					trans.Rollback();
					LogAdapter.WriteFeaturesException(ex);
					throw ex;
				}
				finally
				{
					ConnectionPool.ReturnConnection(conn);
				}
			}
		}

		//关联加单表
		public void LindOrderAdd(ProduceLog produceLog,OperLog operLog)//,BusiLog busiLog)
		{
			using (SqlConnection	conn  =  ConnectionPool.BorrowConnection())
			{
				//conn.Open();	
				
				SqlTransaction trans = conn.BeginTransaction();
				try
				{
					string strSysTime = SqlHelper.ExecuteScalar(trans, CommandType.Text, "select getdate()").ToString();
					DateTime dtSysTime = DateTime.Parse(strSysTime);

					OrderSerialNo serialNo = new OrderSerialNo();
					serialNo.cnvcFill = "0";
					serialNo.cnnSerialNo = Convert.ToDecimal(EntityMapping.Create(serialNo, trans));
					
					ProduceLog oldLog = new ProduceLog();
					oldLog.cnnProduceSerialNo = produceLog.cnnProduceSerialNo;
					oldLog = EntityMapping.Get(oldLog, trans) as ProduceLog;
					if(oldLog == null)
					{
						throw new Exception("生产计划为空");
					}
//					if(oldLog.cnvcProduceState != "1")
//					{
//						throw new Exception("生产计划状态不能关联加单");
//					}
					//锁定加单表
					string strOrderBookSql =
						"update tbOrderAddLog set cnvcAddState='0' where cnvcAddState='0' and cnnOrderSerialNo in (select cnnOrderSerialNo from tbOrderBook where cnvcOrderState='1' and cndShipDate>='" +
						oldLog.cndShipBeginDate + "' and cndShipDate<='" + oldLog.cndShipEndDate + "' and cnvcProduceDeptID='" +
						oldLog.cnvcProduceDeptID + "')";
					SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strOrderBookSql);

					
					//入生产计划产品细节加单表
					string strProduceDetailSql = " insert into tbProduceDetailAdd(cnnProduceSerialNo,cnnAddSerialNo,cnvcCode,cnvcName,cnvcUnit,cnnCount,cnvcState)  "
					                             + "select '" + oldLog.cnnProduceSerialNo.ToString() +
					                             "' as cnnProduceSerialNo,"+serialNo.cnnSerialNo.ToString()+",a.cnvcProductCode,a.cnvcProductName,a.cnvcUnit,sum(a.cnnAddCount) as cnnOrderCount,'1' from tbOrderAddLog a "
					                             +
					                             " where cnvcAddState='0' and cnnOrderSerialNo in (select cnnOrderSerialNo from tbOrderBook where cnvcOrderState='1' and cndShipDate>='" +
					                             oldLog.cndShipBeginDate + "' and cndShipDate<='" + oldLog.cndShipEndDate +
					                             "' and cnvcProduceDeptID='" + oldLog.cnvcProduceDeptID + "')  "
					                             + " group by a.cnvcProductCode,a.cnvcProductName,a.cnvcUnit";
					SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strProduceDetailSql);

					//入生产流水加单流水关联表
					string strProduceOrderSql = "insert into tbProduceOrderLog(cnnProduceSerialNo,cnnOrderSerialNo,cnvcType) "
					                            + " select distinct " + oldLog.cnnProduceSerialNo + ",cnnAddSerialNo,'1' from tbOrderAddLog"
					                            +
					                            " where cnvcAddState='0' and cnnOrderSerialNo in (select cnnOrderSerialNo from tbOrderBook where cnvcOrderState='1' and cndShipDate>='" +
					                            oldLog.cndShipBeginDate + "' and cndShipDate<='" + oldLog.cndShipEndDate +
					                            "' and cnvcProduceDeptID='" + oldLog.cnvcProduceDeptID + "')";
					SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strProduceOrderSql);

					//更新加单状态
					string strOrderSql =
						"update tbOrderAddLog set cnvcAddState='1' where cnvcAddState='0' and cnnOrderSerialNo in (select cnnOrderSerialNo from tbOrderBook where cnvcOrderState='1' and cndShipDate>='" +
						oldLog.cndShipBeginDate + "' and cndShipDate<='" + oldLog.cndShipEndDate + "' and cnvcProduceDeptID='" +
						oldLog.cnvcProduceDeptID + "')";
					SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strOrderSql);					

					
					operLog.cndOperDate = dtSysTime;
					operLog.cnvcComments = "生产流水："+produceLog.cnnProduceSerialNo.ToString();
					EntityMapping.Create(operLog, trans);	

					trans.Commit();
				}
				catch(SqlException sex)
				{
					trans.Rollback();
					LogAdapter.WriteDatabaseException(sex);
					throw sex;
				}
				catch(Exception ex)
				{
					trans.Rollback();
					LogAdapter.WriteFeaturesException(ex);
					throw ex;
				}
				finally
				{
					ConnectionPool.ReturnConnection(conn);
				}
			}
		}

		//关联减单表
		public void LindOrderReduce(ProduceLog produceLog,OperLog operLog)//,BusiLog busiLog)
		{
			using (SqlConnection	conn  =  ConnectionPool.BorrowConnection())
			{
				//conn.Open();	
				
				SqlTransaction trans = conn.BeginTransaction();
				try
				{
					string strSysTime = SqlHelper.ExecuteScalar(trans, CommandType.Text, "select getdate()").ToString();
					DateTime dtSysTime = DateTime.Parse(strSysTime);

					OrderSerialNo serialNo = new OrderSerialNo();
					serialNo.cnvcFill = "0";
					serialNo.cnnSerialNo = Convert.ToDecimal(EntityMapping.Create(serialNo, trans));
					
					ProduceLog oldLog = new ProduceLog();
					oldLog.cnnProduceSerialNo = produceLog.cnnProduceSerialNo;
					oldLog = EntityMapping.Get(oldLog, trans) as ProduceLog;
					if(oldLog == null)
					{
						throw new Exception("生产计划为空");
					}
//					if(oldLog.cnvcProduceState != "1")
//					{
//						throw new Exception("生产计划状态不能关联减单");
//					}
					//锁定加单表
					string strOrderBookSql =
						"update tbOrderReduceLog set cnvcReduceState='0' where cnvcReduceState='0' and cnnOrderSerialNo in (select cnnOrderSerialNo from tbOrderBook where cnvcOrderState='1' and cndShipDate>='" +
						oldLog.cndShipBeginDate + "' and cndShipDate<='" + oldLog.cndShipEndDate + "' and cnvcProduceDeptID='" +
						oldLog.cnvcProduceDeptID + "')";
					SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strOrderBookSql);

					
					//入生产计划产品细节减单表
					string strProduceDetailSql = " insert into tbProduceDetailReduce(cnnProduceSerialNo,cnnReduceSerialNo,cnvcCode,cnvcName,cnvcUnit,cnnCount,cnvcState)  "
					                             + "select '" + oldLog.cnnProduceSerialNo.ToString() +
					                             "' as cnnProduceSerialNo,"+serialNo.cnnSerialNo.ToString()+",a.cnvcProductCode,a.cnvcProductName,a.cnvcUnit,sum(a.cnnReduceCount) as cnnOrderCount,'1' from tbOrderReduceLog a "
					                             +
					                             " where cnvcReduceState='0' and cnnOrderSerialNo in (select cnnOrderSerialNo from tbOrderBook where cnvcOrderState='1' and cndShipDate>='" +
					                             oldLog.cndShipBeginDate + "' and cndShipDate<='" + oldLog.cndShipEndDate +
					                             "' and cnvcProduceDeptID='" + oldLog.cnvcProduceDeptID + "')  "
					                             + " group by a.cnvcProductCode,a.cnvcProductName,a.cnvcUnit";
					SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strProduceDetailSql);

					//入生产流水减单流水关联表
					string strProduceOrderSql = "insert into tbProduceOrderLog(cnnProduceSerialNo,cnnOrderSerialNo,cnvcType) "
					                            + " select distinct " + oldLog.cnnProduceSerialNo + ",cnnReduceSerialNo,'2' from tbOrderReduceLog"
					                            +
					                            " where cnvcReduceState='0' and cnnOrderSerialNo in (select cnnOrderSerialNo from tbOrderBook where cnvcOrderState='1' and cndShipDate>='" +
					                            oldLog.cndShipBeginDate + "' and cndShipDate<='" + oldLog.cndShipEndDate +
					                            "' and cnvcProduceDeptID='" + oldLog.cnvcProduceDeptID + "')";
					SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strProduceOrderSql);

					//更新加单状态
					string strOrderSql =
						"update tbOrderReduceLog set cnvcReduceState='1' where cnvcReduceState='0' and cnnOrderSerialNo in (select cnnOrderSerialNo from tbOrderBook where cnvcOrderState='1' and cndShipDate>='" +
						oldLog.cndShipBeginDate + "' and cndShipDate<='" + oldLog.cndShipEndDate + "' and cnvcProduceDeptID='" +
						oldLog.cnvcProduceDeptID + "')";
					SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strOrderSql);					

					
					operLog.cndOperDate = dtSysTime;
					operLog.cnvcComments = "生产流水："+produceLog.cnnProduceSerialNo.ToString();
					EntityMapping.Create(operLog, trans);	

					trans.Commit();
				}
				catch(SqlException sex)
				{
					trans.Rollback();
					LogAdapter.WriteDatabaseException(sex);
					throw sex;
				}
				catch(Exception ex)
				{
					trans.Rollback();
					LogAdapter.WriteFeaturesException(ex);
					throw ex;
				}
				finally
				{
					ConnectionPool.ReturnConnection(conn);
				}
			}
		}


		//制令单
		public void AddMakeLog(ProduceLog produceLog,string strMakeType,OperLog operLog)//,BusiLog busiLog)
		{
			using (SqlConnection	conn  =  ConnectionPool.BorrowConnection())
			{
				//conn.Open();	
				
				SqlTransaction trans = conn.BeginTransaction();
				try
				{
					string strSysTime = SqlHelper.ExecuteScalar(trans, CommandType.Text, "select getdate()").ToString();
					DateTime dtSysTime = DateTime.Parse(strSysTime);

					OrderSerialNo serialNo = new OrderSerialNo();
					serialNo.cnvcFill = "0";

					ProduceLog oldLog = new ProduceLog();
					oldLog.cnnProduceSerialNo = produceLog.cnnProduceSerialNo;
					oldLog = EntityMapping.Get(oldLog, trans) as ProduceLog;
					
					if(oldLog == null)
					{
						throw new Exception("生产计划不存在");
					}
					if(oldLog.cnvcProduceState != "1" && strMakeType == "0")
					{
						throw new Exception("生产计划不在关联订单状态");
					}
					//制令产品					
					string strProduceDetail = "";
					switch(strMakeType)
					{
						case "0":
							strProduceDetail = "select a.cnnProduceSerialNo,b.cnvcProductTypeCode as cnvcProductType,b.cnvcProductClassCode as cnvcProductClass,b.cnvcProductClassCode as cnvcParentProductClass,a.cnvcCode,a.cnvcName,a.cnvcUnit,a.cnnCount from tbProduceDetail a "
								+ " left outer join vwProduct b on a.cnvcCode=b.cnvcProductCode "
								+ " where a.cnnProduceSerialNo=" +
								produceLog.cnnProduceSerialNo.ToString();
							break;
						case "1":
							strProduceDetail = "select a.cnnProduceSerialNo,b.cnvcProductTypeCode as cnvcProductType,b.cnvcProductClassCode as cnvcProductClass,b.cnvcProductClassCode as cnvcParentProductClass,a.cnvcCode,a.cnvcName,a.cnvcUnit,sum(a.cnnCount) as cnnCount from tbProduceDetailAdd a "
								+ " left outer join vwProduct b on a.cnvcCode=b.cnvcProductCode "
								+ " where a.cnvcState='1' and a.cnnProduceSerialNo=" +
								produceLog.cnnProduceSerialNo.ToString();
							strProduceDetail +=
								" group by a.cnnProduceSerialNo,b.cnvcProductTypeCode,b.cnvcProductClassCode,a.cnvcCode,a.cnvcName,a.cnvcUnit";
							break;
						case "2":
							strProduceDetail = "select a.cnnProduceSerialNo,b.cnvcProductTypeCode as cnvcProductType,b.cnvcProductClassCode as cnvcProductClass,b.cnvcProductClassCode as cnvcParentProductClass,a.cnvcCode,a.cnvcName,a.cnvcUnit,sum(a.cnnCount) as cnnCount from tbProduceDetailReduce a "
								+ " left outer join vwProduct b on a.cnvcCode=b.cnvcProductCode "
								+ " where a.cnvcState='1' and a.cnnProduceSerialNo=" +
								produceLog.cnnProduceSerialNo.ToString();
							strProduceDetail +=
								" group by a.cnnProduceSerialNo,b.cnvcProductTypeCode,b.cnvcProductClassCode,a.cnvcCode,a.cnvcName,a.cnvcUnit";
							break;
					}
					

					DataTable dtProduceDetail2 = SqlHelper.ExecuteDataTable(trans, CommandType.Text, strProduceDetail);
					
					//原材料
					string strMaterial = "select * from tbMaterial";
					DataTable dtMaterial = SqlHelper.ExecuteDataTable(trans, CommandType.Text, strMaterial);
					//配方表	
					string strFormula = "select cnvcProductCode,cnvcProductName,cnvcProductType,cnvcUnit,cnvcPortionUnit,cnnPortionCount from tbFormula";
					DataTable dtFormula = SqlHelper.ExecuteDataTable(trans, CommandType.Text, strFormula);
					//配料表		
					string strDosage = "select '" + produceLog.cnnProduceSerialNo.ToString() +
					                   "' as cnnProduceSerialNo,b.cnvcProductTypeCode as cnvcProductType,b.cnvcProductClassCode as cnvcProductClass,a.cnvcProductCode,a.cnvcCode,a.cnvcName,a.cnvcUnit,a.cnnCount from tbDosage a "
					                   + " left outer join vwProduct b on a.cnvcCode=b.cnvcProductCode ";
					//string strDosage = "select * from tbDosage";
					DataTable dtDosage = SqlHelper.ExecuteDataTable(trans, CommandType.Text, strDosage);
					//生产组与商品类型对应表
					DataTable dtGroupGoods = SingleTableQuery.ExcuteQuery("tbGroupGoods", trans);
					DataTable dtGroupMake = SqlHelper.ExecuteDataTable(trans,CommandType.Text,"select * from tbGroupMake where cnvcMakeType='"+strMakeType+"'");//SingleTableQuery.ExcuteQuery("tbGroupMake", trans);
					//生产组
					string strGroups = "select * from tbNameCode where cnvcType='GROUP'";
					DataTable dtGroup = SqlHelper.ExecuteDataTable(trans, CommandType.Text, strGroups);
					DataTable dtProduceDetail = dtProduceDetail2.Copy();
					foreach(DataRow drProduceDetail in dtProduceDetail2.Rows)
					{
						string strProductCode = drProduceDetail["cnvcCode"].ToString();
						string strCount = drProduceDetail["cnnCount"].ToString();
						string strParentProductClass = drProduceDetail["cnvcParentProductClass"].ToString();
						AnalyzeFormula(dtDosage,dtFormula, strProductCode, strCount,strParentProductClass, ref dtProduceDetail);					
					}
					//份产单位转化
					//原料材料出仓单位转化
					dtProduceDetail.Columns.Add("cnvcPortionUnit",typeof(string));
					dtProduceDetail.Columns.Add("cnnPortionCount",typeof(decimal));

					dtProduceDetail.Columns.Add("cnvcKGUnit",typeof(string));
					dtProduceDetail.Columns.Add("cnnKGCount",typeof(decimal));
					

					foreach(DataRow drMakeDetail in dtProduceDetail.Rows)
					{
						string strProductCode = drMakeDetail["cnvcCode"].ToString();
						string strCount = drMakeDetail["cnnCount"].ToString();
						//成品半成品
						DataRow[] drFormula = dtFormula.Select("cnvcProductCode='" + strProductCode + "'");
						if(drFormula.Length > 0)
						{
							//string strUnit = drFormula[0]["cnvcUnit"].ToString();
							string strPortionUnit = drFormula[0]["cnvcPortionUnit"].ToString();
							string strPortionCount = drFormula[0]["cnnPortionCount"].ToString();
							if(strPortionCount == "")
								throw new Exception("配方不全，产品编码："+drFormula[0]["cnvcProductCode"].ToString()+"，产品名称："+drFormula[0]["cnvcProductName"].ToString());
							//string strProductType = drFormula[0]["cnvcProductType"].ToString();
							//份单位
							drMakeDetail["cnvcPortionUnit"] = strPortionUnit;
							if(decimal.Parse(strPortionCount) <=0)
								throw new Exception("配方有误，份产数量为零，产品编码："+drFormula[0]["cnvcProductCode"].ToString()+"，产品名称："+drFormula[0]["cnvcProductName"].ToString());
							drMakeDetail["cnnPortionCount"] = Math.Round(decimal.Parse(strCount)/decimal.Parse(strPortionCount),4);
//							drMakeDetail["cnvcKGUnit"] = "KG";
//							drMakeDetail["cnvcKGCount"] = Math.Round(decimal.Parse(strCount)/1000, 2);
							
							
						}
						//原料材料
						DataRow[] drMaterial = dtMaterial.Select("cnvcMaterialCode='" + strProductCode + "'");
						if(drMaterial.Length > 0)
						{
							Material ml = new Material(drMaterial[0]);
							drMakeDetail["cnvcPortionUnit"] = ml.cnvcStandardUnit;
							drMakeDetail["cnnPortionCount"] = Math.Round(decimal.Parse(strCount)/ml.cnnConversion/ml.cnnStatdardCount,4);//ml.cnnStatdardCount;

							drMakeDetail["cnvcKGUnit"] = ml.cnvcUnit;
							drMakeDetail["cnnKGCount"] = Math.Round(decimal.Parse(strCount)/ml.cnnConversion, 4);

						}
					}
					//馅料
					DataRow[] drXL = dtProduceDetail.Select("cnvcProductClass='X001~X999'");
					DataTable dtXL = dtProduceDetail.Clone();
					foreach(DataRow dr in drXL)
					{
						string strProductCode = dr["cnvcProductType"].ToString();
						string strCount = dr["cnnCount"].ToString();
						DataRow[] drDosages = dtDosage.Select("cnvcProductCode='" + strProductCode + "'");
						foreach(DataRow drDosage in drDosages)
						{
							//string strDosageType = drDosage["cnvcProductTypeCode"].ToString();
							string strDosageCode = drDosage["cnvcCode"].ToString();
							string strDosageCount = drDosage["cnnCount"].ToString();
							
							decimal dCount = Math.Round(decimal.Parse(strCount)*decimal.Parse(strDosageCount),4);
							DataRow[] drMakeDetails = dtXL.Select("cnvcCode='" + strDosageCode + "'");	
							if(drMakeDetails.Length > 0)
							{
								drMakeDetails[0]["cnnCount"] = dCount+decimal.Parse(drMakeDetails[0]["cnnCount"].ToString());
							}
							else
							{
								DataRow dr2 = dtXL.NewRow();
								dr2["cnnProduceSerialNo"] = drDosage["cnnProduceSerialNo"];
								dr2["cnvcProductType"] = drDosage["cnvcProductType"];
								dr2["cnvcProductClass"] = drDosage["cnvcProductClass"];
								dr2["cnvcCode"] = strDosageCode;//drDosage["cnvcCode"];
								dr2["cnvcName"] = drDosage["cnvcName"];
								dr2["cnvcUnit"] = drDosage["cnvcUnit"];
								dr2["cnnCount"] = dCount.ToString();					
								dtXL.Rows.Add(dr);
							}
						}
					}				

					MakeLog makeLog =  new MakeLog();
					makeLog.cnnProduceSerialNo = produceLog.cnnProduceSerialNo;								
					makeLog.cndOperDate = dtSysTime;
					makeLog.cnvcOperID = produceLog.cnvcOperID;					
					makeLog.cnvcMakeType = strMakeType;//"0";
					foreach(DataRow drGroup in dtGroup.Rows)
					{
						NameCode nameCode = new NameCode(drGroup);
						//生产组制令关系
						DataRow[] drGroupMakes = dtGroupMake.Select("cnvcGroupCode='" + nameCode.cnvcCode + "'");
						if(drGroupMakes.Length == 0)
							continue;
						foreach(DataRow drGroupMake in drGroupMakes)
						{
							GroupMake gm = new GroupMake(drGroupMake);							
							makeLog.cnvcGroup = gm.cnvcGroupCode;
							makeLog.cnvcMakeName = gm.cnvcMakeName;
							//生产组产品关系
							if(gm.cnvcGroupCode=="XL"&&gm.cnvcProductType=="Raw")
							{
								//馅料组备料明细
								if(dtXL.Rows.Count > 0)
								{
									makeLog.cnnMakeSerialNo = Convert.ToDecimal(EntityMapping.Create(serialNo, trans));
									EntityMapping.Create(makeLog, trans);

									foreach(DataRow drMakeDetail in dtXL.Rows)
									{
										MakeDetail md = new MakeDetail(drMakeDetail);
										md.cnnMakeSerialNo = makeLog.cnnMakeSerialNo;
										switch(drMakeDetail["cnvcProductType"].ToString())
										{
											case "Raw":
											case "Pack":
												md.cnvcUnit = drMakeDetail["cnvcKGUnit"].ToString();
												md.cnnCount = decimal.Parse(drMakeDetail["cnnKGCount"].ToString());
												break;
											case "SEMIPRODUCT":
												md.cnvcUnit = drMakeDetail["cnvcUnit"].ToString();
												md.cnnCount = decimal.Parse(drMakeDetail["cnnCount"].ToString());
												break;												
										}														
										EntityMapping.Create(md, trans);
									}
									
								}
							}
							else if(gm.cnvcProductType == "Raw" && gm.cnvcGroupCode!="XL" && gm.cnvcGroupCode!="PL")
							{
								//饼干 蛋糕 面包组领料单
								DataRow[] drGroupGoodses = dtGroupGoods.Select("cnvcGroupCode='" + nameCode.cnvcCode + "' and cnvcProductType='FINALPRODUCT'");
								if(drGroupGoodses.Length == 0)
								{			
									continue;
								}
								int iDetail = 0;	
								makeLog.cnnMakeSerialNo = Convert.ToDecimal(EntityMapping.Create(serialNo, trans));
								foreach(DataRow drGroupGoods in drGroupGoodses)
								{
									GroupGoods gg = new GroupGoods(drGroupGoods);
									DataRow[] drMakeDetails = dtProduceDetail.Select("cnvcProductType='Raw' and cnvcParentProductClass='" + gg.cnvcProductClass + "'");
									if(drMakeDetails.Length == 0)
									{
										continue;
									}
									iDetail += drMakeDetails.Length;
									foreach(DataRow drMakeDetail in drMakeDetails)
									{
										MakeDetail md = new MakeDetail(drMakeDetail);
										md.cnnMakeSerialNo = makeLog.cnnMakeSerialNo;

										md.cnvcUnit = drMakeDetail["cnvcPortionUnit"].ToString();
										md.cnnCount = decimal.Parse(drMakeDetail["cnnPortionCount"].ToString());
										EntityMapping.Create(md, trans);
									}
								}
								if(iDetail > 0)
								{									
									EntityMapping.Create(makeLog, trans);
								}
							
							}
						
							else
							{
								DataRow[] drGroupGoodses = dtGroupGoods.Select("cnvcGroupCode='" + nameCode.cnvcCode + "' and cnvcProductType='"+gm.cnvcProductType+"'");
								if(drGroupGoodses.Length == 0)
								{			
									continue;
								}
								int iDetail = 0;	
								makeLog.cnnMakeSerialNo = Convert.ToDecimal(EntityMapping.Create(serialNo, trans));
								foreach(DataRow drGroupGoods in drGroupGoodses)
								{
									GroupGoods gg = new GroupGoods(drGroupGoods);
									DataRow[] drMakeDetails = dtProduceDetail.Select("cnvcProductClass='" + gg.cnvcProductClass + "'");
									if(drMakeDetails.Length == 0)
									{
										continue;
									}
									iDetail += drMakeDetails.Length;
									foreach(DataRow drMakeDetail in drMakeDetails)
									{
										MakeDetail md = new MakeDetail(drMakeDetail);
										md.cnnMakeSerialNo = makeLog.cnnMakeSerialNo;
										switch(gg.cnvcProductType)
										{
											case "FINALPRODUCT":
												md.cnvcUnit = drMakeDetail["cnvcUnit"].ToString();
												md.cnnCount = decimal.Parse(drMakeDetail["cnnCount"].ToString());
												break;												
											case "SEMIPRODUCT":
											case "Raw":											
											case "Pack":
												md.cnvcUnit = drMakeDetail["cnvcPortionUnit"].ToString();
												md.cnnCount = decimal.Parse(drMakeDetail["cnnPortionCount"].ToString());
												break;
										}										
										
										EntityMapping.Create(md, trans);
									}
								}
								if(iDetail > 0)
								{									
									EntityMapping.Create(makeLog, trans);
								}
							}

						}

					}
					switch(strMakeType)
					{
						case "0":
							oldLog.cnvcOperID = produceLog.cnvcOperID;
							oldLog.cndOperDate = dtSysTime;
							oldLog.cnvcProduceState = "2";
							EntityMapping.Update(oldLog, trans);
							break;
						case "1":
							string strAddSql = "update tbProduceDetailAdd set cnvcState='2' where cnvcState='1' and cnnProduceSerialNo=" +
							                   oldLog.cnnProduceSerialNo;
							SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strAddSql);
							break;
						case "2":
							string strReduceSql = "update tbProduceDetailReduce set cnvcState='2' where cnvcState='1' and cnnProduceSerialNo=" +
								oldLog.cnnProduceSerialNo;
							SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strReduceSql);
							break;

					}
					
					operLog.cndOperDate = dtSysTime;
					operLog.cnvcComments = "生产流水："+produceLog.cnnProduceSerialNo.ToString();
					EntityMapping.Create(operLog, trans);	
			
					trans.Commit();
				}
				catch(SqlException sex)
				{
					trans.Rollback();
					LogAdapter.WriteDatabaseException(sex);
					throw sex;
				}
				catch(Exception ex)
				{
					trans.Rollback();
					LogAdapter.WriteFeaturesException(ex);
					throw ex;
				}
				finally
				{
					ConnectionPool.ReturnConnection(conn);
				}
			}
		}
		private void AnalyzeFormula(DataTable dtDosage,DataTable dtFormula,string strProductCode,string strCount,string strParentProductClass,ref DataTable dtMakeDetail)
		{
			DataRow[] drDosages = dtDosage.Select("cnvcProductCode='" + strProductCode + "'");
			DataRow[] drFormula = dtFormula.Select("cnvcProductCode='" + strProductCode + "'");
			if(drFormula.Length == 0)
				throw new Exception("无此配方，产品编码："+strProductCode);
			string strPortionCount = drFormula[0]["cnnPortionCount"].ToString();
			string strProductType = drFormula[0]["cnvcProductType"].ToString();
			decimal dPortionCount = 0;
			if(strPortionCount == "")
			{
				dPortionCount = 1;
			}
			else
			{
				dPortionCount = decimal.Parse(strPortionCount);
			}
			if(dPortionCount == 0)
			{
				dPortionCount = 1;
			}
			if(strProductType == "FINALPRODUCT")
			{
				dPortionCount = 1;
			}
			foreach(DataRow drDosage in drDosages)
			{
				string strDosageType = drDosage["cnvcProductType"].ToString();
				string strDosageCode = drDosage["cnvcCode"].ToString();
				string strDosageCount = drDosage["cnnCount"].ToString();
				//string strDosageUnit = drDosage["cnvcUnit"].ToString();

				decimal dCount = Math.Round(decimal.Parse(strCount)*decimal.Parse(strDosageCount)/dPortionCount,4);
				DataRow[] drMakeDetails = dtMakeDetail.Select("cnvcCode='" + strDosageCode + "'");	
				if(drMakeDetails.Length > 0)
				{
					drMakeDetails[0]["cnnCount"] = dCount+decimal.Parse(drMakeDetails[0]["cnnCount"].ToString());
				}
				else
				{
					DataRow dr = dtMakeDetail.NewRow();
					dr["cnnProduceSerialNo"] = drDosage["cnnProduceSerialNo"];
					dr["cnvcProductType"] = drDosage["cnvcProductType"];
					dr["cnvcProductClass"] = drDosage["cnvcProductClass"];
					dr["cnvcParentProductClass"] = strParentProductClass;
					dr["cnvcCode"] = strDosageCode;//drDosage["cnvcCode"];
					dr["cnvcName"] = drDosage["cnvcName"];
					dr["cnvcUnit"] = drDosage["cnvcUnit"];
					dr["cnnCount"] = dCount.ToString();					
					dtMakeDetail.Rows.Add(dr);
				}
				if(strDosageType == "SEMIPRODUCT" || strDosageType == "FINALPRODUCT")
				{
					AnalyzeFormula(dtDosage,dtFormula, strDosageCode, dCount.ToString(),strParentProductClass, ref dtMakeDetail);
				}
			}
		}		
		private void MakeDetail(SqlTransaction trans,ProduceLog produceLog,
			NameCode nameCode,DataTable dtGroupGoods,DataTable dtMakeDetail,
			string strMakeName1,string strMakeName2,string strMakeType
			)
		{
			string strSysTime = SqlHelper.ExecuteScalar(trans, CommandType.Text, "select getdate()").ToString();
			DateTime dtSysTime = DateTime.Parse(strSysTime);

			OrderSerialNo serialNo = new OrderSerialNo();
			serialNo.cnvcFill = "0";
			//serialNo.cnnSerialNo = Convert.ToDecimal(EntityMapping.Create(serialNo, trans));

			MakeLog makeLog = new MakeLog();
			makeLog.cnnProduceSerialNo = produceLog.cnnProduceSerialNo;								
			makeLog.cndOperDate = dtSysTime;
			makeLog.cnvcOperID = produceLog.cnvcOperID;
			makeLog.cnvcGroup = nameCode.cnvcCode;
			makeLog.cnvcMakeType = strMakeType;//"0";

			makeLog.cnnMakeSerialNo = Convert.ToDecimal(EntityMapping.Create(serialNo, trans));
			makeLog.cnvcMakeName = strMakeName1;
			EntityMapping.Create(makeLog,trans);
								
			DataRow[] drGroupGoods = dtGroupGoods.Select("cnvcGroupCode='" + nameCode.cnvcCode + "'");
			string strFilter = "";
			foreach(DataRow drGroupGood in drGroupGoods)
			{
				strFilter += "'" + drGroupGood["cnvcGoodsCode"].ToString() + "',";
			}
			strFilter = strFilter.Substring(0, strFilter.Length - 1);
			DataRow[] drMakeDetails = dtMakeDetail.Select("cnvcProductClassCode in ("+strFilter+") ");

			foreach(DataRow drMakeDetail in drMakeDetails)
			{
				MakeDetail md = new MakeDetail(drMakeDetail);
				md.cnnMakeSerialNo = makeLog.cnnMakeSerialNo;
				md.cnvcUnit = drMakeDetail["cnvcPortionUnit"].ToString();
				md.cnnCount = decimal.Parse(drMakeDetail["cnnPortionCount"].ToString());
				EntityMapping.Create(md, trans);
			}


			makeLog.cnnMakeSerialNo = Convert.ToDecimal(EntityMapping.Create(serialNo, trans));
			makeLog.cnvcMakeName = strMakeName2;
			EntityMapping.Create(makeLog,trans);

			foreach(DataRow drMakeDetail in drMakeDetails)
			{
				MakeDetail md = new MakeDetail(drMakeDetail);
				md.cnnMakeSerialNo = makeLog.cnnMakeSerialNo;
				md.cnvcUnit = drMakeDetail["cnvcBigPortionUnit"].ToString();
				md.cnnCount = decimal.Parse(drMakeDetail["cnnBigPortionCount"].ToString());
				EntityMapping.Create(md, trans);
			}
		}

		//门市生产
		public void SelfProduce(string strOrderSerialNo,string strDeptID,OperLog operLog)//,BusiLog busiLog)
		{
			using (SqlConnection	conn  =  ConnectionPool.BorrowConnection())
			{
				//conn.Open();	
				
				SqlTransaction trans = conn.BeginTransaction();
				try
				{
					string strSysTime = SqlHelper.ExecuteScalar(trans, CommandType.Text, "select getdate()").ToString();
					DateTime dtSysTime = DateTime.Parse(strSysTime);

					OrderSerialNo serialNo = new OrderSerialNo();
					serialNo.cnvcFill = "0";
					serialNo.cnnSerialNo = Convert.ToDecimal(EntityMapping.Create(serialNo, trans));
					
					OrderBook ob = new OrderBook();
					ob.cnnOrderSerialNo = decimal.Parse(strOrderSerialNo);
					ob = EntityMapping.Get(ob, trans) as OrderBook;
					if(ob == null)
					{
						throw new Exception("未找到订单");
					}
					if(ob.cnvcOrderState != "0")
					{
						throw new Exception("订单已作处理");
					}
					DataTable dtOrderBookDetail =
						SqlHelper.ExecuteDataTable(trans, CommandType.Text,
						                           "select * from tbOrderBookDetail where cnnOrderSerialNo=" + strOrderSerialNo);
					DataTable dtDosage = SqlHelper.ExecuteDataTable(trans, CommandType.Text, "select * from tbDosage");
					//配方表	
					string strFormula = "select cnvcProductCode,cnvcProductType,cnvcUnit,cnvcPortionUnit,cnnPortionCount from tbFormula";
					DataTable dtFormula = SqlHelper.ExecuteDataTable(trans, CommandType.Text, strFormula);
					string strMaterial = "select * from tbMaterial";
					DataTable dtMaterial = SqlHelper.ExecuteDataTable(trans, CommandType.Text, strMaterial);
					DataTable dtStorage =
						SqlHelper.ExecuteDataTable(trans, CommandType.Text,
						                           "select * from tbStorage where cnvcStorageDeptID='" + strDeptID + "'");
					DataTable dtOrder = dtStorage.Clone();
					foreach(DataRow dr in dtOrderBookDetail.Rows)
					{
						OrderBookDetail obd = new OrderBookDetail(dr);
						DataRow[] drFormulas = dtFormula.Select("cnvcProductCode='" + obd.cnvcProductCode + "'");
						if(drFormulas.Length > 0)
						{
							Entity.Formula formula = new AMSApp.zhenghua.Entity.Formula(drFormulas[0]);
							decimal dPortionCount = 1;
							if(formula.cnvcProductType == "SEMIPRODUCT")
							{
								dPortionCount = formula.cnnPortionCount;
							}
							DataRow[] drDosages = dtDosage.Select("cnvcProductCode='" + obd.cnvcProductCode + "'");
							if(drDosages.Length > 0)
							{
								foreach(DataRow drDosage in drDosages)
								{
									Dosage dosage = new Dosage(drDosage);
									DataRow drOrder = dtOrder.NewRow();
									drOrder["cnvcStorageDeptID"] = strDeptID;
									drOrder["cnvcProductCode"] = dosage.cnvcCode;
									drOrder["cnvcProductName"] = dosage.cnvcName;
									if(dosage.cnvcProductType == "Raw" || dosage.cnvcProductType == "Pack")
									{
										DataRow[] drMaterials = dtMaterial.Select("cnvcMaterialCode='" + dosage.cnvcCode + "'");
										if(drMaterials.Length == 0)
										{
											throw new Exception("未找到相应原材料，材料编码为："+dosage.cnvcCode);
										}
										Material material = new Material(drMaterials[0]);
										drOrder["cnvcUnit"] = material.cnvcUnit;
										drOrder["cnnCount"] = Math.Round(dosage.cnnCount*obd.cnnOrderCount/material.cnnConversion, 4);
									}
									else
									{
										drOrder["cnvcUnit"] = dosage.cnvcUnit;
										drOrder["cnnCount"] = Math.Round(dosage.cnnCount*obd.cnnOrderCount/dPortionCount, 4);
									}
									dtOrder.Rows.Add(drOrder);
								}
								
							}
						}
					}
					//判断库存是否支持生产
					foreach(DataRow drOrder in dtOrder.Rows)
					{
						Entity.Storage storage = new AMSApp.zhenghua.Entity.Storage(drOrder);
						DataRow[] drStorages = dtStorage.Select("cnvcProductCode='" + storage.cnvcProductCode + "'");
						if(drStorages.Length == 0)
						{
							throw new Exception(storage.cnvcProductName+"无库存");
						}
						Entity.Storage oldStorage = new AMSApp.zhenghua.Entity.Storage(drStorages[0]);
						if(storage.cnnCount > oldStorage.cnnCount)
						{
							throw new Exception(storage.cnvcProductName + "库存量不足");
						}
						drOrder["cnnSafeCount"] = oldStorage.cnnSafeCount;
						drOrder["cnnSafeUpCount"] = oldStorage.cnnSafeUpCount;
					}
					//生产
					//1去料
					foreach(DataRow drOrder in dtOrder.Rows)
					{
						Entity.Storage storage = new AMSApp.zhenghua.Entity.Storage(drOrder);
						StorageLog storageLog = new StorageLog(drOrder);
						storageLog.cnvcOperID = operLog.cnvcOperID;
						storageLog.cndOperDate = dtSysTime;
						storageLog.cnvcStorageDeptID = strDeptID;
						DataRow[] drFormula = dtFormula.Select("cnvcProductCode='" + storage.cnvcProductCode + "'");
						if(drFormula.Length == 0)
						{
							storageLog.cnvcOperType = "DA03";//原材料出
						}
						else
						{
							storageLog.cnvcOperType = "DB04";//半成品出
						}
						DataRow[] drStorages = dtStorage.Select("cnvcProductCode='" + storage.cnvcProductCode + "'");
						if(drStorages.Length == 0)
						{
							throw new Exception(storage.cnvcProductName+"无库存");
						}
						Entity.Storage oldStorage = new AMSApp.zhenghua.Entity.Storage(drStorages[0]);
						if(storage.cnnCount > oldStorage.cnnCount)
						{
							throw new Exception(storage.cnvcProductName + "库存量不足");
						}
						oldStorage.cnnCount = oldStorage.cnnCount - storage.cnnCount;
						EntityMapping.Update(oldStorage, trans);
						EntityMapping.Create(storageLog, trans);
						//写日志
					}
					foreach(DataRow dr in dtOrderBookDetail.Rows)
					{
						OrderBookDetail obd = new OrderBookDetail(dr);
						StorageLog storageLog = new StorageLog(dr);
						storageLog.cnvcOperID = operLog.cnvcOperID;
						storageLog.cnvcStorageDeptID = strDeptID;
						storageLog.cndOperDate = dtSysTime;
						storageLog.cnvcOperType = "DC03";//成品入
						
						DataRow[] drStorages = dtStorage.Select("cnvcProductCode='" + obd.cnvcProductCode + "'");
						if(drStorages.Length == 0)
						{
							//添加
							Entity.Storage storage = new AMSApp.zhenghua.Entity.Storage(dr);
							storage.cnnCount = obd.cnnOrderCount;
							storage.cnvcStorageDeptID = strDeptID;
							EntityMapping.Create(storage, trans);
						}
						else
						{
							//更新量
							Entity.Storage storage = new AMSApp.zhenghua.Entity.Storage(drStorages[0]);
							storage.cnnCount = storage.cnnCount + obd.cnnOrderCount;
							EntityMapping.Update(storage, trans);
							storageLog.cnnSafeCount = storage.cnnSafeCount;
							storageLog.cnnSafeUpCount = storage.cnnSafeUpCount;
						}
						storageLog.cnnCount = obd.cnnOrderCount;
						EntityMapping.Create(storageLog, trans);
						
					}
					//更新订单状态
					ob.cnvcOrderState = "3";
					EntityMapping.Update(ob, trans);

					operLog.cndOperDate = dtSysTime;
					operLog.cnvcComments = "订单流水："+strOrderSerialNo;
					EntityMapping.Create(operLog, trans);		

					trans.Commit();
				}
				catch(SqlException sex)
				{
					trans.Rollback();
					LogAdapter.WriteDatabaseException(sex);
					throw sex;
				}
				catch(Exception ex)
				{
					trans.Rollback();
					LogAdapter.WriteFeaturesException(ex);
					throw ex;
				}
				finally
				{
					ConnectionPool.ReturnConnection(conn);
				}
			}
		}


		public void AssignPrint(string strAssignSerialNo,OperLog operLog)//,BusiLog busiLog)
		{
			using (SqlConnection	conn  =  ConnectionPool.BorrowConnection())
			{
				//conn.Open();	
				
				SqlTransaction trans = conn.BeginTransaction();
				try
				{
					string strSysTime = SqlHelper.ExecuteScalar(trans, CommandType.Text, "select getdate()").ToString();
					DateTime dtSysTime = DateTime.Parse(strSysTime);
										
					string strUpdateSql = "update tbAssignLog set cnnPrintFlag=cnnPrintFlag+1 where cnnAssignSerialNo="+strAssignSerialNo;
					SqlHelper.ExecuteNonQuery(trans,CommandType.Text,strUpdateSql);

					OrderSerialNo serialNo = new OrderSerialNo();
					serialNo.cnvcFill = "0";
					serialNo.cnnSerialNo = Convert.ToDecimal(EntityMapping.Create(serialNo, trans));

					operLog.cndOperDate = dtSysTime;
					operLog.cnnOperSerialNo = serialNo.cnnSerialNo;
					EntityMapping.Create(operLog,trans);

					trans.Commit();
				}
				catch(SqlException sex)
				{
					trans.Rollback();
					LogAdapter.WriteDatabaseException(sex);
					throw sex;
				}
				catch(Exception ex)
				{
					trans.Rollback();
					LogAdapter.WriteFeaturesException(ex);
					throw ex;
				}
				finally
				{
					ConnectionPool.ReturnConnection(conn);
				}
			}
		}


	}
}
