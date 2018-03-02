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
	/// BookFacade ��ժҪ˵����
	/// </summary>
	public class BookFacade
	{
		public BookFacade()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
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
					operLog.cnvcComments = "���Ա���ˮ�ţ�"+ibook.ToString();
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
					if(oldbook == null) throw new Exception("δ�ҵ��������ݣ�");
					if(oldbook.cnvcState != "0" ) throw new Exception("��ȷ�ϵ����Բ����޸ģ�");
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
					operLog.cnvcComments = "���Ա���ˮ�ţ�"+oldbook.cnnSerialNo.ToString();
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
					if(oldbook == null) throw new Exception("δ�ҵ��������ݣ�");
					//if(oldbook.cnvcState != 0 ) throw new Exception("��ȷ�ϵ����Բ����ٴ�ȷ�ϣ�");
					oldbook.cnvcState = book.cnvcState;
					oldbook.cnvcCheckID = book.cnvcCheckID;
					oldbook.cnvcCheckName = book.cnvcCheckName;
					oldbook.cndcheckDate = dtSysTime;

					EntityMapping.Update(oldbook,trans);

					operLog.cndOperDate = dtSysTime;
					operLog.cnvcComments = "���Ա���ˮ�ţ�"+oldbook.cnnSerialNo.ToString();
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
					operLog.cnvcComments = "�ظ���ˮ�ţ�"+ibr.ToString();
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
