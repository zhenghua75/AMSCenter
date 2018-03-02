
/******************************************************************** FR 1.20E *******
* ��Ŀ���ƣ�   AMSApp
* �ļ���:   	OrderBook.cs
* ����:	     ֣��
* ��������:    2008-10-4
* ��������:    ������

*                                                           Copyright(C) 2008 zhenghua
*************************************************************************************/
#region Import NameSpace
using System;
using System.Data;
using AMSApp.zhenghua.EntityBase;
#endregion

namespace AMSApp.zhenghua.Entity
{
	/// <summary>
	/// **�������ƣ�������ʵ����
	/// </summary>
	[Serializable]
	[TableMapping("tbOrderBook")]
	public class OrderBook: EntityObjectBase
	{
		#region ���ݱ����ɱ���



		
		
		private decimal _cnnOrderSerialNo;
		private string _cnvcOrderDeptID = String.Empty;
		private string _cnvcProduceDeptID = String.Empty;
		private string _cnvcOrderType = String.Empty;
		private string _cnvcOrderComments = String.Empty;
		private string _cnvcOrderOperID = String.Empty;
		private DateTime _cndOrderDate;
		private DateTime _cndShipDate;
		private string _cnvcCustomName = String.Empty;
		private string _cnvcShipAddress = String.Empty;
		private string _cnvcLinkPhone = String.Empty;
		private DateTime _cndArrivedDate;
		private string _cnvcOrderState = String.Empty;
		
		#endregion
		
		#region ���캯��




		public OrderBook():base()
		{
		}
		
		public OrderBook(DataRow row):base(row)
		{
		}
		
		public OrderBook(DataTable table):base(table)
		{
		}
		
		public OrderBook(string  strXML):base(strXML)
		{
		}
		#endregion
		
		#region ϵͳ��������




				
		/// <summary>
		/// ������ˮ
		/// </summary>
		[ColumnMapping("cnnOrderSerialNo",IsPrimaryKey=true,IsIdentity=false,IsVersionNumber=false)]
		public decimal cnnOrderSerialNo
		{
			get {return _cnnOrderSerialNo;}
			set {_cnnOrderSerialNo = value;}
		}

		/// <summary>
		/// ��������
		/// </summary>
		[ColumnMapping("cnvcOrderDeptID",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcOrderDeptID
		{
			get {return _cnvcOrderDeptID;}
			set {_cnvcOrderDeptID = value;}
		}

		/// <summary>
		/// ������λ
		/// </summary>
		[ColumnMapping("cnvcProduceDeptID",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcProduceDeptID
		{
			get {return _cnvcProduceDeptID;}
			set {_cnvcProduceDeptID = value;}
		}

		/// <summary>
		/// ��������
		/// </summary>
		[ColumnMapping("cnvcOrderType",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcOrderType
		{
			get {return _cnvcOrderType;}
			set {_cnvcOrderType = value;}
		}

		/// <summary>
		/// ����˵��
		/// </summary>
		[ColumnMapping("cnvcOrderComments",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcOrderComments
		{
			get {return _cnvcOrderComments;}
			set {_cnvcOrderComments = value;}
		}

		/// <summary>
		/// ��������Ա
		/// </summary>
		[ColumnMapping("cnvcOrderOperID",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcOrderOperID
		{
			get {return _cnvcOrderOperID;}
			set {_cnvcOrderOperID = value;}
		}

		/// <summary>
		/// ����ʱ��
		/// </summary>
		[ColumnMapping("cndOrderDate",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public DateTime cndOrderDate
		{
			get {return _cndOrderDate;}
			set {_cndOrderDate = value;}
		}

		/// <summary>
		/// ��������
		/// </summary>
		[ColumnMapping("cndShipDate",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public DateTime cndShipDate
		{
			get {return _cndShipDate;}
			set {_cndShipDate = value;}
		}

		/// <summary>
		/// �ͻ�������λ
		/// </summary>
		[ColumnMapping("cnvcCustomName",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcCustomName
		{
			get {return _cnvcCustomName;}
			set {_cnvcCustomName = value;}
		}

		/// <summary>
		/// �ͻ���ַ
		/// </summary>
		[ColumnMapping("cnvcShipAddress",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcShipAddress
		{
			get {return _cnvcShipAddress;}
			set {_cnvcShipAddress = value;}
		}

		/// <summary>
		/// ��ϵ�绰
		/// </summary>
		[ColumnMapping("cnvcLinkPhone",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcLinkPhone
		{
			get {return _cnvcLinkPhone;}
			set {_cnvcLinkPhone = value;}
		}

		/// <summary>
		/// ����Ҫ�󵽻�ʱ��
		/// </summary>
		[ColumnMapping("cndArrivedDate",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public DateTime cndArrivedDate
		{
			get {return _cndArrivedDate;}
			set {_cndArrivedDate = value;}
		}

		/// <summary>
		/// ����״̬
		/// </summary>
		[ColumnMapping("cnvcOrderState",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcOrderState
		{
			get {return _cnvcOrderState;}
			set {_cnvcOrderState = value;}
		}
		#endregion
	}	
}
