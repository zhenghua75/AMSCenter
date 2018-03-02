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
	/// BookFacade 的摘要说明。
	/// </summary>
	public class BookFacade
	{
		public BookFacade()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		public void AddBook(Book book,OperLog operLog)
		{
			using (SqlConnection	conn  =  ConnectionPool.BorrowConnection())
			{
				//conn.Open();	
				
				SqlTransaction trans = conn.BeginTransaction();
				try
				{
					string strSysTime = SqlHelper.ExecuteScalar(trans, CommandType.Text, "select getdate()").ToString();
					DateTime dtSysTime = DateTime.Parse(strSysTime);

					book.cndPublishDate = dtSysTime;
					long ibook = EntityMapping.Create(book,trans);

					operLog.cndOperDate = dtSysTime;
					operLog.cnvcComments = "留言本流水号："+ibook.ToString();
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

		public void UpdateBook(Book book,OperLog operLog)
		{
			using (SqlConnection	conn  =  ConnectionPool.BorrowConnection())
			{
				//conn.Open();	
				
				SqlTransaction trans = conn.BeginTransaction();
				try
				{
					string strSysTime = SqlHelper.ExecuteScalar(trans, CommandType.Text, "select getdate()").ToString();
					DateTime dtSysTime = DateTime.Parse(strSysTime);

					Entity.Book oldbook = new Book();
					oldbook.cnnSerialNo = book.cnnSerialNo;
					oldbook = EntityMapping.Get(oldbook,trans) as Book;
					if(oldbook == null) throw new Exception("未找到留言内容！");
					if(oldbook.cnvcState != "0" ) throw new Exception("已确认的留言不能修改！");
					oldbook.cnvcBook = book.cnvcBook;
					oldbook.cmvcPublishID = book.cmvcPublishID;
					oldbook.cnvcPublishName = book.cnvcPublishName;
					oldbook.cndPublishDate = dtSysTime;
					oldbook.cnvcCheckDept = book.cnvcCheckDept;

					oldbook.cnvcCheckID = string.Empty;
					oldbook.cnvcCheckName = string.Empty;
					oldbook.cndcheckDate = DateTime.MinValue;

					EntityMapping.Update(oldbook,trans);

					operLog.cndOperDate = dtSysTime;
					operLog.cnvcComments = "留言本流水号："+oldbook.cnnSerialNo.ToString();
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

		public void CheckBook(Book book,OperLog operLog)
		{
			using (SqlConnection	conn  =  ConnectionPool.BorrowConnection())
			{
				//conn.Open();	
				
				SqlTransaction trans = conn.BeginTransaction();
				try
				{
					string strSysTime = SqlHelper.ExecuteScalar(trans, CommandType.Text, "select getdate()").ToString();
					DateTime dtSysTime = DateTime.Parse(strSysTime);

					Entity.Book oldbook = new Book();
					oldbook.cnnSerialNo = book.cnnSerialNo;
					oldbook = EntityMapping.Get(oldbook,trans) as Book;
					if(oldbook == null) throw new Exception("未找到留言内容！");
					//if(oldbook.cnvcState != 0 ) throw new Exception("已确认的留言不能再次确认！");
					oldbook.cnvcState = book.cnvcState;
					oldbook.cnvcCheckID = book.cnvcCheckID;
					oldbook.cnvcCheckName = book.cnvcCheckName;
					oldbook.cndcheckDate = dtSysTime;

					EntityMapping.Update(oldbook,trans);

					operLog.cndOperDate = dtSysTime;
					operLog.cnvcComments = "留言本流水号："+oldbook.cnnSerialNo.ToString();
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

		public void BookReturn(BookReturn br,OperLog operLog)
		{
			using (SqlConnection	conn  =  ConnectionPool.BorrowConnection())
			{
				//conn.Open();	
				
				SqlTransaction trans = conn.BeginTransaction();
				try
				{
					string strSysTime = SqlHelper.ExecuteScalar(trans, CommandType.Text, "select getdate()").ToString();
					DateTime dtSysTime = DateTime.Parse(strSysTime);

					br.cndReturnDate = dtSysTime;
					long ibr = EntityMapping.Create(br,trans);

					operLog.cndOperDate = dtSysTime;
					operLog.cnvcComments = "回复流水号："+ibr.ToString();
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
