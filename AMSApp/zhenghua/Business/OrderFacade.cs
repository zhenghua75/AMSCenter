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
	/// OrderFacade ��ժҪ˵����
	/// </summary>
	public class OrderFacade
	{
		public OrderFacade()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		public void AddOrder(OrderBook orderBook,DataTable dtOrderBookDetail,OperLog operLog)//,BusiLog busiLog)
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
					
					orderBook.cnnOrderSerialNo = serialNo.cnnSerialNo;
					orderBook.cndOrderDate = dtSysTime;
					orderBook.cnvcOrderOperID = operLog.cnvcOperID;
					EntityMapping.Create(orderBook, trans);
					foreach(DataRow drOrderBookDetail in dtOrderBookDetail.Rows)
					{
						OrderBookDetail detail = new OrderBookDetail(drOrderBookDetail);
						detail.cnnOrderSerialNo = serialNo.cnnSerialNo;
						detail.cndOperDate = dtSysTime;
						detail.cnvcOperID = operLog.cnvcOperID;
						EntityMapping.Create(detail, trans);
					}
					operLog.cndOperDate = dtSysTime;
					operLog.cnvcComments = "������ˮ��"+orderBook.cnnOrderSerialNo;
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

		public void UpdateOrder(OrderBook order,OperLog operLog)//,BusiLog busiLog)
		{
			using (SqlConnection	conn  =  ConnectionPool.BorrowConnection())
			{
				//conn.Open();	
				
				SqlTransaction trans = conn.BeginTransaction();
				try
				{
					string strSysTime = SqlHelper.ExecuteScalar(trans, CommandType.Text, "select getdate()").ToString();
					DateTime dtSysTime = DateTime.Parse(strSysTime);

					OrderBook oldOrder = new OrderBook();
					oldOrder.cnnOrderSerialNo = order.cnnOrderSerialNo;
					oldOrder = EntityMapping.Get(oldOrder, trans) as OrderBook;
					if(oldOrder == null)
					{
						throw new Exception("����δ�ҵ�");
					}
					oldOrder.cnvcOrderOperID = order.cnvcOrderOperID;
					oldOrder.cnvcOrderType = order.cnvcOrderType;
					oldOrder.cnvcProduceDeptID = order.cnvcProduceDeptID;
					oldOrder.cnvcOrderDeptID = order.cnvcOrderDeptID;

					if(oldOrder.cnvcOrderType == "WDO")
					{
						oldOrder.cndArrivedDate = order.cndArrivedDate;
						oldOrder.cnvcLinkPhone = order.cnvcLinkPhone;
						oldOrder.cnvcShipAddress = order.cnvcShipAddress;
						oldOrder.cnvcCustomName = order.cnvcCustomName;
				
					}
					//order.cnnOrderSerialNo = decimal.Parse(txtOrderSerialNo.Text);
					oldOrder.cndShipDate = order.cndShipDate;
					EntityMapping.Update(oldOrder, trans);

					operLog.cndOperDate = dtSysTime;
					operLog.cnvcComments = "������ˮ��"+oldOrder.cnnOrderSerialNo;
					EntityMapping.Create(operLog, trans);	

					//EntityMapping.Create(busiLog, trans);					
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

		//�ӵ�
		public void OrderAdd(string strOrderSerialNo,string strAddType,string strAddComments,DataTable dtOrderAdd,OperLog operLog)//,BusiLog busiLog)
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
					
					foreach(DataRow drOrderAdd in dtOrderAdd.Rows)
					{
						OrderAddLog orderAdd = new OrderAddLog(drOrderAdd);
						orderAdd.cnnOrderSerialNo = decimal.Parse(strOrderSerialNo);
						orderAdd.cnnAddSerialNo = serialNo.cnnSerialNo;
						orderAdd.cnvcAddType = strAddType;
						orderAdd.cnvcAddComments = strAddComments;
						orderAdd.cndOperDate = dtSysTime;
						orderAdd.cnvcOperID = operLog.cnvcOperID;
						orderAdd.cnvcAddState = "0";
						orderAdd.cnnAddCount = decimal.Parse(drOrderAdd["cnnOrderCount"].ToString());
						EntityMapping.Create(orderAdd, trans);
					}
					operLog.cndOperDate = dtSysTime;
					operLog.cnvcComments = "������ˮ��"+strOrderSerialNo;
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

		//����
		public void OrderReduce(string strOrderSerialNo,string strReduceType,string strAddComments,DataTable dtOrderReduce,OperLog operLog)//,BusiLog busiLog)
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
					
					foreach(DataRow drOrderReduce in dtOrderReduce.Rows)
					{
						OrderReduceLog orderReduce = new OrderReduceLog(drOrderReduce);
						orderReduce.cnnOrderSerialNo = decimal.Parse(strOrderSerialNo);
						orderReduce.cnnReduceSerialNo = serialNo.cnnSerialNo;
						orderReduce.cnvcReduceType = strReduceType;
						orderReduce.cnvcReduceComments = strAddComments;
						orderReduce.cndOperDate = dtSysTime;
						orderReduce.cnvcOperID = operLog.cnvcOperID;
						orderReduce.cnvcReduceState = "0";
						orderReduce.cnnReduceCount = decimal.Parse(drOrderReduce["cnnOrderCount"].ToString());
						EntityMapping.Create(orderReduce, trans);
					}
					
					operLog.cndOperDate = dtSysTime;
					operLog.cnvcComments = "������ˮ��"+strOrderSerialNo;
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

		//������Ӳ�Ʒ
		public void AddDetail(string strOrderSerialNo,OperLog operLog,DataTable dtDetail)//,BusiLog busiLog)
		{
			using (SqlConnection	conn  =  ConnectionPool.BorrowConnection())
			{
				//conn.Open();	
				
				SqlTransaction trans = conn.BeginTransaction();
				try
				{
					string strSysTime = SqlHelper.ExecuteScalar(trans, CommandType.Text, "select getdate()").ToString();
					DateTime dtSysTime = DateTime.Parse(strSysTime);
					foreach(DataRow drDetail in dtDetail.Rows)
					{
						OrderBookDetail detail = new OrderBookDetail(drDetail);
						detail.cnnOrderSerialNo = decimal.Parse(strOrderSerialNo);
						detail.cnvcOperID = operLog.cnvcOperID;
						detail.cndOperDate = dtSysTime;
						EntityMapping.Create(detail, trans);
					}
					
					operLog.cndOperDate = dtSysTime;
					operLog.cnvcComments = "������ˮ��"+strOrderSerialNo;
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

		//�������²�Ʒ
		public void UpdateDetail(OrderBookDetail detail,OperLog operLog)//,BusiLog busiLog)
		{
			using (SqlConnection	conn  =  ConnectionPool.BorrowConnection())
			{
				//conn.Open();	
				
				SqlTransaction trans = conn.BeginTransaction();
				try
				{
					string strSysTime = SqlHelper.ExecuteScalar(trans, CommandType.Text, "select getdate()").ToString();
					DateTime dtSysTime = DateTime.Parse(strSysTime);

					OrderBookDetail oldDetail = new OrderBookDetail();
					oldDetail.cnnOrderSerialNo = detail.cnnOrderSerialNo;
					oldDetail.cnvcProductCode = detail.cnvcProductCode;
					oldDetail = EntityMapping.Get(oldDetail, trans) as OrderBookDetail;

					oldDetail.cnnOrderCount = detail.cnnOrderCount;
					oldDetail.cnnSum = detail.cnnOrderCount*oldDetail.cnnPrice;
					oldDetail.cnvcOperID = detail.cnvcOperID;
					oldDetail.cndOperDate = dtSysTime;
					EntityMapping.Update(oldDetail, trans);
					
					operLog.cndOperDate = dtSysTime;
					operLog.cnvcComments = "������ˮ��"+detail.cnnOrderSerialNo.ToString();
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

		//����ɾ����Ʒ
		public void DeleteDetail(OrderBookDetail detail,OperLog operLog)//,BusiLog busiLog)
		{
			using (SqlConnection	conn  =  ConnectionPool.BorrowConnection())
			{
				//conn.Open();	
				
				SqlTransaction trans = conn.BeginTransaction();
				try
				{
					
					EntityMapping.Delete(detail, trans);

					string strSysTime = SqlHelper.ExecuteScalar(trans, CommandType.Text, "select getdate()").ToString();
					DateTime dtSysTime = DateTime.Parse(strSysTime);

					operLog.cndOperDate = dtSysTime;
					operLog.cnvcComments = "������ˮ��"+detail.cnnOrderSerialNo.ToString()+"����Ʒ���룺"+detail.cnvcProductCode;
					EntityMapping.Create(operLog, trans);		
					//EntityMapping.Create(busiLog, trans);					
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
