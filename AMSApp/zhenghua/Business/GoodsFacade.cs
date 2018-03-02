using System;
using System.Data;
using System.Data.SqlClient;
using AMSApp.zhenghua.DataAccess;
using AMSApp.zhenghua.Common;
using AMSApp.zhenghua.QueryArgs;
using AMSApp.zhenghua.Entity;
using System.Collections;
using System.IO;
namespace AMSApp.zhenghua.Business
{
	/// <summary>
	/// GoodsFacade 的摘要说明。
	/// </summary>
	public class GoodsFacade
	{
		public GoodsFacade()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}


		//入库
		public void ProduceCheck(DataTable dtProduce,OperLog operLog,string strProduceDeptID,string strProduceSerialNo)
		{
			using (SqlConnection	conn  =  ConnectionPool.BorrowConnection())
			{
				//conn.Open();	
				
				SqlTransaction trans = conn.BeginTransaction();
				try
				{
					string strSysTime = SqlHelper.ExecuteScalar(trans, CommandType.Text, "select getdate()").ToString();
					DateTime dtSysTime = DateTime.Parse(strSysTime);

					foreach(DataRow drProduce in dtProduce.Rows)
					{
						ProduceCheckLog check = new ProduceCheckLog(drProduce);
						check.cnvcOperID = operLog.cnvcOperID;
						check.cnvcProduceDeptID = strProduceDeptID;
						check.cndOperDate = dtSysTime;
						ProduceCheckLog check2 = new ProduceCheckLog();
						check2.cnnProduceSerialNo = check.cnnProduceSerialNo;
						check2.cnvcCode = check.cnvcCode;
						check2 = EntityMapping.Get(check2,trans) as ProduceCheckLog;
						if(check2 == null)
							EntityMapping.Create(check, trans);
						else
						{
							check2.cnnCheckCount = check.cnnCheckCount;
							check2.cnvcOperID = operLog.cnvcOperID;
							check2.cndOperDate = dtSysTime;
							EntityMapping.Update(check2,trans);

						}
					}
					ProduceLog pl = new ProduceLog();
					pl.cnnProduceSerialNo = decimal.Parse(strProduceSerialNo);
					pl = EntityMapping.Get(pl, trans) as ProduceLog;
					if(pl == null)
						throw new Exception("生产数据异常");
					pl.cnvcProduceState = "3";
					pl.cnvcOperID = operLog.cnvcOperID;
					pl.cndOperDate = dtSysTime;
					EntityMapping.Update(pl, trans);

					operLog.cndOperDate = dtSysTime;
					operLog.cnvcComments = "生产流水："+strProduceSerialNo;
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

		//更新
		public void UpdateProduceCheck(ProduceCheckLog check,OperLog operLog)
		{
			using (SqlConnection	conn  =  ConnectionPool.BorrowConnection())
			{
				//conn.Open();	
				
				SqlTransaction trans = conn.BeginTransaction();
				try
				{
					string strSysTime = SqlHelper.ExecuteScalar(trans, CommandType.Text, "select getdate()").ToString();
					DateTime dtSysTime = DateTime.Parse(strSysTime);

					ProduceCheckLog oldCheck = new ProduceCheckLog();
					oldCheck.cnnProduceSerialNo = check.cnnProduceSerialNo;
					oldCheck.cnvcCode = check.cnvcCode;
					oldCheck = EntityMapping.Get(oldCheck, trans) as ProduceCheckLog;
					if(oldCheck == null)
						throw new Exception("未找到指定产品生产库存");
					oldCheck.cnnCheckCount = check.cnnCheckCount;
					oldCheck.cnvcOperID = check.cnvcOperID;
					oldCheck.cndOperDate = dtSysTime;
					EntityMapping.Update(oldCheck, trans);

					operLog.cndOperDate = dtSysTime;
					operLog.cnvcComments = "生产流水："+check.cnnProduceSerialNo.ToString()+"，产品编码："+check.cnvcCode;
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

		public void BatchUpdateProduceCheck(ArrayList alCheck,string strProduceSerialNo,OperLog operLog)
		{
			using (SqlConnection	conn  =  ConnectionPool.BorrowConnection())
			{
				//conn.Open();	
				
				SqlTransaction trans = conn.BeginTransaction();
				try
				{
					string strSysTime = SqlHelper.ExecuteScalar(trans, CommandType.Text, "select getdate()").ToString();
					DateTime dtSysTime = DateTime.Parse(strSysTime);

					for(int i=0;i<alCheck.Count;i++)
					{
						ProduceCheckLog check = alCheck[i] as ProduceCheckLog;
						ProduceCheckLog oldCheck = new ProduceCheckLog();
						oldCheck.cnnProduceSerialNo = check.cnnProduceSerialNo;
						oldCheck.cnvcCode = check.cnvcCode;
						oldCheck = EntityMapping.Get(oldCheck, trans) as ProduceCheckLog;
						if(oldCheck == null)
							throw new Exception("未找到指定产品生产库存");
						oldCheck.cnnCheckCount = check.cnnCheckCount;
						oldCheck.cnvcOperID = check.cnvcOperID;
						oldCheck.cndOperDate = dtSysTime;
						EntityMapping.Update(oldCheck, trans);
					}

					operLog.cndOperDate = dtSysTime;
					operLog.cnvcComments = "生产流水："+strProduceSerialNo;
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


		public void AddAssignLog(ProduceLog produceLog,OperLog operLog)//,BusiLog busiLog)
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
						throw new Exception("生产计划不存在");
					}
//					if(oldLog.cnvcProduceState != "3")
//					{
//						throw new Exception("未盘点");
//					}
					oldLog.cnvcOperID = produceLog.cnvcOperID;
					oldLog.cndOperDate = dtSysTime;
					AssignLog assign = new AssignLog();
					assign.cndOperDate = dtSysTime;
					assign.cnnAssignSerialNo = serialNo.cnnSerialNo;
					assign.cnnProduceSerialNo = oldLog.cnnProduceSerialNo;
					assign.cnvcShipOperID = produceLog.cnvcOperID;
					assign.cnvcShipDeptID = oldLog.cnvcProduceDeptID;
					assign.cnvcOperID = produceLog.cnvcOperID;
					assign.cndShipDate = oldLog.cndProduceDate;

					#region
					//订单流水
					//收货单位

					//订单、加单、减单清单
//					string strOrderSql = "select b.cnvcOrderDeptID,b.cndShipDate,b.cnvcOrderType,e.cnnPriority,c.* from tbProduceOrderLog a "
//					                     + " join tbOrderBook b on a.cnnOrderSerialNo=b.cnnOrderSerialNo "
//					                     + " left outer join tbOrderBookDetail c on b.cnnOrderSerialNo=c.cnnOrderSerialNo "
//					                     + " left outer join tbDept e on b.cnvcOrderDeptID=e.cnvcDeptID "
//					                     + " where a.cnnProduceSerialNo = " + oldLog.cnnProduceSerialNo.ToString()
//					                     + " and a.cnvcType='0' ";//and b.cnvcOrderState='1'";
//					string strOrderAddSql = "select b.* from tbProduceOrderLog a "
//					                        + " left outer join tbOrderAddLog b on a.cnnOrderSerialNo=b.cnnAddSerialNo "
//					                        + " where a.cnnProduceSerialNo=" + oldLog.cnnProduceSerialNo.ToString()
//					                        + " and a.cnvcType='1' ";//and b.cnvcAddState='1'";
//					string strOrderReduceSql = "select b.* from tbProduceOrderLog a "
//					                           + " left outer join tbOrderReduceLog b on a.cnnOrderSerialNo=b.cnnReduceSerialNo "
//					                           + " where a.cnnProduceSerialNo=" + oldLog.cnnProduceSerialNo.ToString()
//					                           + " and a.cnvcType='2' ";//and b.cnvcReduceState='1'";
//					DataTable dtOrder = SqlHelper.ExecuteDataTable(trans, CommandType.Text, strOrderSql);
//					DataTable dtOrderAdd = SqlHelper.ExecuteDataTable(trans, CommandType.Text, strOrderAddSql);
//					DataTable dtOrderReduce = SqlHelper.ExecuteDataTable(trans, CommandType.Text, strOrderReduceSql);
//					//合计订单
//					foreach(DataRow drAdd in dtOrderAdd.Rows)
//					{
//						string strOrderSerialNo = drAdd["cnnOrderSerialNo"].ToString();
//						string strProductCode = drAdd["cnvcProductCode"].ToString();
//						DataRow[] drOrders = dtOrder.Select("cnvcProductCode='" + strProductCode + "'");
//						if(drOrders.Length == 0)
//						{
//							DataRow[] drOrderSerialNo = dtOrder.Select("cnnOrderSerialNo=" + strOrderSerialNo);
//							if(drOrderSerialNo.Length > 0)
//							{
//								DataRow drOrder = dtOrder.NewRow();
//								drOrder["cnvcOrderDeptID"] = drOrderSerialNo[0]["cnvcOrderDeptID"];
//								drOrder["cndShipDate"] = drOrderSerialNo[0]["cndShipDate"];
//								drOrder["cnvcOrderType"] = drOrderSerialNo[0]["cnvcOrderType"];
//								drOrder["cnnPriority"] = drOrderSerialNo[0]["cnnPriority"];
//
//								drOrder["cnnOrderSerialNo"] = drAdd["cnvcProductCode"];
//								drOrder["cnvcProductCode"] = drAdd["cnvcProductCode"];
//								drOrder["cnvcProductName"] = drAdd["cnvcProductName"];
//								drOrder["cnnOrderCount"] = drAdd["cnnAddCount"];
//								drOrder["cnvcUnit"] = drAdd["cnvcUnit"];
//								drOrder["cnnPrice"] = drAdd["cnnPrice"];
//								drOrder["cnnSum"] = drAdd["cnnSum"];
//								dtOrder.Rows.Add(drOrder);
//							}
//						}
//
//					}
//					
//					foreach(DataRow drOrder in dtOrder.Rows)
//					{
//						//有的加减数量，无的加产品
//						string strProductCode = drOrder["cnvcProductCode"].ToString();
//						//订单量
//						decimal dCount = decimal.Parse(drOrder["cnnOrderCount"].ToString());
//						decimal dAddCount = 0;
//						DataRow[] drOrderAdds = dtOrderAdd.Select("cnvcProductCode='" + strProductCode + "'");
//						if(drOrderAdds.Length > 0)
//						{
//							//加单量
//							string strOrderAddCount = drOrderAdds[0]["cnnAddCount"].ToString();
//							dAddCount = decimal.Parse(strOrderAddCount);
//							//dAddCount = decimal.Parse(strCount) + decimal.Parse(strOrderAddCount);
//							drOrder["cnnOrderCount"] = dCount + dAddCount;
//						}
//						DataRow[] drOrderReduces = dtOrderReduce.Select("cnvcProductCode='" + strProductCode + "'");
//						if(drOrderReduces.Length > 0)
//						{
//							//减单量
//							decimal dReduceCount = decimal.Parse(drOrderReduces[0]["cnnReduceCount"].ToString());
//							//decimal dReduceCount = decimal.Parse(strCount) - decimal.Parse(strOrderReduceCount);
//							drOrder["cnnOrderCount"] = dCount + dAddCount - dReduceCount;
//						}
//					}
					#endregion
					#region 注释
					//生产、加单、减单
//					string strProduceSql = "select * from tbProduceDetail where cnnProduceSerialNo="+oldLog.cnnProduceSerialNo.ToString();
//					string strProduceAddSql = "select * from tbProduceDetailAdd where cnvcState='2' and cnnProduceSerialNo="+oldLog.cnnProduceSerialNo.ToString();
//					string strProduceReduceSql = "select * from tbProduceDetailReduce where cnvcState='2' and cnnProduceSerialNo="+oldLog.cnnProduceSerialNo;
//					DataTable dtProduce = SqlHelper.ExecuteDataTable(trans, CommandType.Text, strProduceSql);
//					DataTable dtProduceAdd = SqlHelper.ExecuteDataTable(trans, CommandType.Text, strProduceAddSql);
//					DataTable dtProduceReduce = SqlHelper.ExecuteDataTable(trans, CommandType.Text, strProduceReduceSql);
//
//					foreach(DataRow drProduce in dtProduce.Rows)
//					{
//						//有的加减数量，无的加产品
//						string strProductCode = drProduce["cnvcCode"].ToString();
//						decimal dCount = decimal.Parse(drProduce["cnnCount"].ToString());
//						decimal dAddCount = 0;
//						DataRow[] drProduceAdds = dtProduceAdd.Select("cnvcCode='" + strProductCode + "'");
//						if(drProduceAdds.Length > 0)
//						{
//							dAddCount = decimal.Parse(drProduceAdds[0]["cnnCount"].ToString());
//							//decimal dAddCount = decimal.Parse(strCount) + decimal.Parse(strOrderAddCount);
//							drProduce["cnnCount"] = dCount + dAddCount;//dAddCount.ToString();
//						}
//						DataRow[] drProduceReduces = dtProduceReduce.Select("cnvcCode='" + strProductCode + "'");
//						if(drProduceReduces.Length > 0)
//						{
//							decimal dReduceCount = decimal.Parse(drProduceReduces[0]["cnnCount"].ToString());
//							//decimal dReduceCount = decimal.Parse(strCount) - decimal.Parse(strOrderReduceCount);
//							drProduce["cnnCount"] = dCount + dAddCount - dReduceCount;//dReduceCount.ToString();
//						}
//					}
//					foreach(DataRow drAdd in dtProduceAdd.Rows)
//					{
//						string strProductCode = drAdd["cnvcCode"].ToString();
//						DataRow[] drProduces = dtProduce.Select("cnvcCode='" + strProductCode + "'");
//						if(drProduces.Length == 0)
//						{
//							DataRow drProduce = dtOrder.NewRow();
//							drProduce["cnnProduceSerialNo"] = drAdd["cnnProduceSerialNo"];
//							drProduce["cnvcCode"] = drAdd["cnvcCode"];
//							drProduce["cnvcName"] = drAdd["cnvcName"];
//							drProduce["cnnCount"] = drAdd["cnnCount"];
//							drProduce["cnvcUnit"] = drAdd["cnvcUnit"];
//							//drProduce["cnvcPrice"] = drAdd["cnvcPrice"];
//							//drProduce["cnnSum"] = drAdd["cnnSum"];
//							dtProduceAdd.Rows.Add(drProduce);
//						}
//
//					}
					#endregion
					//订单数据
					string strOrderSql = "select b.cnvcOrderDeptID,b.cndShipDate,b.cnvcOrderType,f.cnnPriority,"
										+" c.cnnOrderSerialNo,c.cnvcProductCode,c.cnvcProductName, "
										+" (c.cnnOrderCount+isnull(d.cnnAddCount,0)-isnull(e.cnnReduceCount,0)) as cnnOrderCount,c.cnvcUnit,c.cnnPrice, "
										+" (c.cnnSum+isnull(d.cnnAddSum,0)-isnull(e.cnnReduceSum,0)) as cnnSum,c.cnnAssignCount "
										+"  from  tbOrderBookDetail c "
										+" left outer join "
										+" (select cnnOrderSerialNo,cnvcProductCode,sum(cnnAddCount) as cnnAddCount,sum(cnnSum) as cnnAddSum from "
										+" tbOrderAddLog "
										+" group by cnnOrderSerialNo,cnvcProductCode) "
										+"  d on c.cnnOrderSerialNo=d.cnnOrderSerialNo and d.cnvcProductCode=c.cnvcProductCode "
										+" left outer join  "
										+" (select cnnOrderSerialNo,cnvcProductCode,sum(cnnReduceCount) as cnnReduceCount,sum(cnnSum) as cnnReduceSum from  "
										+" tbOrderReduceLog "
										+" group by cnnOrderSerialNo,cnvcProductCode) "
										+" e on c.cnnOrderSerialNo=e.cnnOrderSerialNo and e.cnvcProductCode=c.cnvcProductCode "
										+" left outer join tbOrderBook b on c.cnnOrderSerialNo=b.cnnOrderSerialNo "
										+" left outer join tbDept f on b.cnvcOrderDeptID=f.cnvcDeptID "
										+" where c.cnnOrderSerialNo in (select cnnOrderSerialNo from tbProduceOrderLog where cnnProduceSerialNo="+oldLog.cnnProduceSerialNo.ToString()+") "
										+" and c.cnnOrderCount+isnull(d.cnnAddCount,0)-isnull(e.cnnReduceCount,0)>c.cnnAssignCount ";
					DataTable dtOrder = SqlHelper.ExecuteDataTable(trans,CommandType.Text,strOrderSql);
					//生产盘点数据
					string strCheckSql = "select * from tbProduceCheckLog where cnnProduceSerialNo=" +
					                     oldLog.cnnProduceSerialNo.ToString()+" and cnnCheckCount>0";
					DataTable dtProduce = SqlHelper.ExecuteDataTable(trans, CommandType.Text, strCheckSql);
					//订单按产品分类汇总
					//分配 外订订单有限分配
					DataView dvOrder = new DataView(dtOrder);
					dvOrder.Sort = "cnvcOrderType desc,cnnPriority asc,cnnOrderSerialNo asc";
					Hashtable hOrderSerialNo = new Hashtable();
					foreach(DataRowView dv in dvOrder)
					{
						string strOrderSerialNo = dv["cnnOrderSerialNo"].ToString();
						string strProductCode = dv["cnvcProductCode"].ToString();
						string strProductName = dv["cnvcProductName"].ToString();
						string strOrderCount = dv["cnnOrderCount"].ToString();
						string strAssignCount = dv["cnnAssignCount"].ToString();
						decimal dOrderCount = decimal.Parse(strOrderCount);
						decimal dAssignCount = Convert.ToDecimal(strAssignCount);
						string strUnit = dv["cnvcUnit"].ToString();
						string strPrice = dv["cnnPrice"].ToString();
						string strOrderType = dv["cnvcOrderType"].ToString();
						decimal dPrice = decimal.Parse(strPrice);

						DataRow[] drProduces = dtProduce.Select("cnvcCode='"+strProductCode+"'");
						if(drProduces.Length > 0)
						{
							string strSumCount = drProduces[0]["cnnCheckCount"].ToString();
							decimal dSumCount = decimal.Parse(strSumCount);
							string strSumAssign = drProduces[0]["cnnAssignCount"].ToString();
							decimal dSumAssign = decimal.Parse(strSumAssign);
							if(dSumCount > 0)
							{
								AssignDetail assignDetail = new AssignDetail();
								assignDetail.cnnAssignSerialNo = assign.cnnAssignSerialNo;
								assignDetail.cnnProduceSerialNo = assign.cnnProduceSerialNo;
								assignDetail.cnnOrderSerialNo = decimal.Parse(strOrderSerialNo);
								assignDetail.cnvcProductCode = strProductCode;
								assignDetail.cnvcProductName = strProductName;
								assignDetail.cnvcUnit = strUnit;
								if(dSumCount >= dOrderCount-dAssignCount)
								{									
									assignDetail.cnnOrderCount = dOrderCount;
									assignDetail.cnnCount = dOrderCount-dAssignCount;
									drProduces[0]["cnnCheckCount"] = dSumCount - dOrderCount+dAssignCount;
									drProduces[0]["cnnAssignCount"] = dSumAssign+dOrderCount-dAssignCount;
								}
								else
								{
									if(strOrderType == "WDO")
									{
										throw new Exception("订单流水为"+dv["cnnOrderSerialNo"].ToString()+"的外订定单"+strProductName+"不能满足分配");
									}
									assignDetail.cnnOrderCount = dOrderCount;
									assignDetail.cnnCount = dSumCount;
									drProduces[0]["cnnCheckCount"] = 0;
									drProduces[0]["cnnAssignCount"] = dSumAssign+dSumCount;
								}
								assignDetail.cnnPrice = dPrice;
								assignDetail.cnnSum = assignDetail.cnnCount*dPrice;
								EntityMapping.Create(assignDetail, trans);
								if(!hOrderSerialNo.Contains(dv["cnnOrderSerialNo"].ToString()))
								{
									hOrderSerialNo.Add(dv["cnnOrderSerialNo"].ToString(), dv["cnnOrderSerialNo"].ToString());
									assign.cnnOrderSerialNo = decimal.Parse(dv["cnnOrderSerialNo"].ToString());
									assign.cnvcReceiveDeptID = dv["cnvcOrderDeptID"].ToString();
									assign.cndShipDate = DateTime.Parse(dv["cndShipDate"].ToString());
									EntityMapping.Create(assign, trans);
								}
								string strOrderBookDetail = "update tbOrderBookDetail set cnnAssignCount="+Convert.ToString(assignDetail.cnnCount+dAssignCount)+" where cnnOrderSerialNo="+strOrderSerialNo+" and cnvcProductCode='"+strProductCode+"'";
								SqlHelper.ExecuteNonQuery(trans,CommandType.Text,strOrderBookDetail);
							}
							
							
							
						}
						
					}
					foreach(DataRow drProduce in dtProduce.Rows)
					{
						ProduceCheckLog check = new ProduceCheckLog(drProduce);
						//check.cnnCheckCount = 0;
						//EntityMapping.Update(check);
						string strSql = "update tbProduceCheckLog set cnnCheckCount="+check.cnnCheckCount.ToString()+",cnnAssignCount="+check.cnnAssignCount.ToString()+" where cnnProduceSerialNo="+check.cnnProduceSerialNo.ToString()+" and cnvcCode='"+check.cnvcCode+"'";
						SqlHelper.ExecuteNonQuery(trans,CommandType.Text,strSql);
					}
					//更新订单生产计划状态
					string strUpdateOrder = "update tbOrderBook set cnvcOrderState='2' "
					                        +
					                        " where cnnOrderSerialNo in (select cnnOrderSerialNo from tbProduceOrderLog where cnvcType='0' and cnnProduceSerialNo="+oldLog.cnnProduceSerialNo+") ";
					string strUpdateOrderAdd = "update tbOrderAddLog set cnvcAddState='2' "
						+
						" where cnnOrderSerialNo in (select cnnOrderSerialNo from tbProduceOrderLog where cnvcType='1' and cnnProduceSerialNo="+oldLog.cnnProduceSerialNo+") ";
					string strUpdateOrderReduce = "update tbOrderReduceLog set cnvcReduceState='2' "
						+
						" where cnnOrderSerialNo in (select cnnOrderSerialNo from tbProduceOrderLog where cnvcType='2' and cnnProduceSerialNo="+oldLog.cnnProduceSerialNo+") ";
					SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strUpdateOrder);
					SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strUpdateOrderAdd);
					SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strUpdateOrderReduce);
					//更新生产计划状态 
					
					string strUpdteProduceAdd =
						"update tbProduceDetailAdd set cnvcState='4' where cnvcState='3' and cnnProduceSerialNo=" +
						oldLog.cnnProduceSerialNo;
					string strUpdteProduceReduce =
						"update tbProduceDetailReduce set cnvcState='4' where cnvcState='3' and cnnProduceSerialNo=" +
						oldLog.cnnProduceSerialNo;
					SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strUpdteProduceAdd);
					SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strUpdteProduceReduce);
					oldLog.cnvcProduceState = "4";
					EntityMapping.Update(oldLog, trans);
					
					operLog.cndOperDate = dtSysTime;
					operLog.cnvcComments = "分货流水："+produceLog.cnnProduceSerialNo.ToString();
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

		public void UpdateAssignLog(AssignDetail detail,OperLog operLog)
		{
			using (SqlConnection	conn  =  ConnectionPool.BorrowConnection())
			{
				//conn.Open();	
				
				SqlTransaction trans = conn.BeginTransaction();
				try
				{
//					
//					AssignDetail detail = new AssignDetail();
//					detail.cnnAssignSerialNo = detailLog.cnnAssignSerialNo;
//					detail.cnnOrderSerialNo = detailLog.cnnOrderSerialNo;
//					detail.cnvcProductCode = detailLog.cnvcProductCode;
					AssignDetail detailOld = EntityMapping.Get(detail, trans) as AssignDetail;
					if(detail.cnnCount != detailOld.cnnCount)
					{
						string strSysTime = SqlHelper.ExecuteScalar(trans, CommandType.Text, "select getdate()").ToString();
						DateTime dtSysTime = DateTime.Parse(strSysTime);

						OrderSerialNo serialNo = new OrderSerialNo();
						serialNo.cnvcFill = "0";
						serialNo.cnnSerialNo = Convert.ToDecimal(EntityMapping.Create(serialNo, trans));

						detailOld.cnnCount = detail.cnnCount;
						detailOld.cnnSum = Math.Round(detail.cnnCount*detailOld.cnnPrice, 2);
						EntityMapping.Update(detailOld, trans);

						AssignDetailLog detailLog = new AssignDetailLog(detailOld.ToTable());
						detailLog.cnvcOperID = operLog.cnvcOperID;
						detailLog.cndOperDate = dtSysTime;
						detailLog.cnnSerialNo = serialNo.cnnSerialNo;
						EntityMapping.Create(detailLog, trans);

						operLog.cndOperDate = dtSysTime;
						operLog.cnvcComments = "生产流水："+detail.cnnAssignSerialNo.ToString()+"，日志流水："+serialNo.cnnSerialNo.ToString();
						EntityMapping.Create(operLog, trans);		
					}

							
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


		public void BatchUpdateAssignLog(ArrayList alDetail,OperLog operLog,string strProduceSerialNo)
		{
			using (SqlConnection	conn  =  ConnectionPool.BorrowConnection())
			{
				//conn.Open();	
				
				SqlTransaction trans = conn.BeginTransaction();
				try
				{
					//					
					//					AssignDetail detail = new AssignDetail();
					//					detail.cnnAssignSerialNo = detailLog.cnnAssignSerialNo;
					//					detail.cnnOrderSerialNo = detailLog.cnnOrderSerialNo;
					//					detail.cnvcProductCode = detailLog.cnvcProductCode;

					OrderSerialNo serialNo = new OrderSerialNo();
					serialNo.cnvcFill = "0";

					string strSysTime = SqlHelper.ExecuteScalar(trans, CommandType.Text, "select getdate()").ToString();
					DateTime dtSysTime = DateTime.Parse(strSysTime);

					for(int i=0 ;i<alDetail.Count;i++)
					{
						AssignDetail detail = alDetail[i] as AssignDetail;
						AssignDetail detailOld = EntityMapping.Get(detail, trans) as AssignDetail;
						if(detail.cnnCount != detailOld.cnnCount)
						{
							

							//OrderSerialNo serialNo = new OrderSerialNo();
							//serialNo.cnvcFill = "0";
							//serialNo.cnnSerialNo = Convert.ToDecimal(EntityMapping.Create(serialNo, trans));

							detailOld.cnnCount = detail.cnnCount;
							detailOld.cnnSum = Math.Round(detail.cnnCount*detailOld.cnnPrice, 2);
							EntityMapping.Update(detailOld, trans);

							AssignDetailLog detailLog = new AssignDetailLog(detailOld.ToTable());
							detailLog.cnvcOperID = operLog.cnvcOperID;
							detailLog.cndOperDate = dtSysTime;
							detailLog.cnnSerialNo = Convert.ToDecimal(EntityMapping.Create(serialNo, trans));//serialNo.cnnSerialNo;
							EntityMapping.Create(detailLog, trans);

							
						}
					}
					//operLog.cnnOperSerialNo = Convert.ToDecimal(EntityMapping.Create(serialNo, trans));
					operLog.cndOperDate = dtSysTime;
					operLog.cnvcComments = "生产流水："+strProduceSerialNo;//+"，日志流水："+serialNo.cnnSerialNo.ToString();
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

		public void UpdatePriority(Dept dept,OperLog operLog)
		{
			using (SqlConnection	conn  =  ConnectionPool.BorrowConnection())
			{
				//conn.Open();	
				
				SqlTransaction trans = conn.BeginTransaction();
				try
				{
					Dept oldDept = new Dept();
					oldDept.cnvcDeptID = dept.cnvcDeptID;
					oldDept = EntityMapping.Get(oldDept, trans) as Dept;
					if(oldDept == null)
						throw new Exception("未找到相应的部门");
					oldDept.cnnPriority = dept.cnnPriority;
					EntityMapping.Update(oldDept, trans);

					string strSysTime = SqlHelper.ExecuteScalar(trans, CommandType.Text, "select getdate()").ToString();
					DateTime dtSysTime = DateTime.Parse(strSysTime);

					operLog.cndOperDate = dtSysTime;
					operLog.cnvcComments = "部门ID："+dept.cnvcDeptID;
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
	}
}
