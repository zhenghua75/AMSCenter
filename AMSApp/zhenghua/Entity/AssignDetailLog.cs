
/******************************************************************** FR 1.20E *******
* ��Ŀ���ƣ�   AMSApp
* �ļ���:   	AssignDetailLog.cs
* ����:	     ֣��
* ��������:    2008-10-17
* ��������:    �ֻ���ˮϸ����־��

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
	/// **�������ƣ��ֻ���ˮϸ����־��ʵ����
	/// </summary>
	[Serializable]
	[TableMapping("tbAssignDetailLog")]
	public class AssignDetailLog: EntityObjectBase
	{
		#region ���ݱ����ɱ���



		
		
		private decimal _cnnSerialNo;
		private decimal _cnnAssignSerialNo;
		private decimal _cnnOrderSerialNo;
		private string _cnvcProductCode = String.Empty;
		private string _cnvcProductName = String.Empty;
		private string _cnvcUnit = String.Empty;
		private decimal _cnnPrice;
		private decimal _cnnOrderCount;
		private decimal _cnnCount;
		private decimal _cnnSum;
		private string _cnvcOperID = String.Empty;
		private DateTime _cndOperDate;
		
		#endregion
		
		#region ���캯��




		public AssignDetailLog():base()
		{
		}
		
		public AssignDetailLog(DataRow row):base(row)
		{
		}
		
		public AssignDetailLog(DataTable table):base(table)
		{
		}
		
		public AssignDetailLog(string  strXML):base(strXML)
		{
		}
		#endregion
		
		#region ϵͳ��������




				
		/// <summary>
		/// ������ˮ
		/// </summary>
		[ColumnMapping("cnnSerialNo",IsPrimaryKey=true,IsIdentity=false,IsVersionNumber=false)]
		public decimal cnnSerialNo
		{
			get {return _cnnSerialNo;}
			set {_cnnSerialNo = value;}
		}

		/// <summary>
		/// �ֻ���ˮ
		/// </summary>
		[ColumnMapping("cnnAssignSerialNo",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public decimal cnnAssignSerialNo
		{
			get {return _cnnAssignSerialNo;}
			set {_cnnAssignSerialNo = value;}
		}

		/// <summary>
		/// ������ˮ
		/// </summary>
		[ColumnMapping("cnnOrderSerialNo",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public decimal cnnOrderSerialNo
		{
			get {return _cnnOrderSerialNo;}
			set {_cnnOrderSerialNo = value;}
		}

		/// <summary>
		/// ��Ʒ����
		/// </summary>
		[ColumnMapping("cnvcProductCode",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcProductCode
		{
			get {return _cnvcProductCode;}
			set {_cnvcProductCode = value;}
		}

		/// <summary>
		/// ��Ʒ����
		/// </summary>
		[ColumnMapping("cnvcProductName",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcProductName
		{
			get {return _cnvcProductName;}
			set {_cnvcProductName = value;}
		}

		/// <summary>
		/// ��λ
		/// </summary>
		[ColumnMapping("cnvcUnit",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcUnit
		{
			get {return _cnvcUnit;}
			set {_cnvcUnit = value;}
		}

		/// <summary>
		/// ����
		/// </summary>
		[ColumnMapping("cnnPrice",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public decimal cnnPrice
		{
			get {return _cnnPrice;}
			set {_cnnPrice = value;}
		}

		/// <summary>
		/// �л�����
		/// </summary>
		[ColumnMapping("cnnOrderCount",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public decimal cnnOrderCount
		{
			get {return _cnnOrderCount;}
			set {_cnnOrderCount = value;}
		}

		/// <summary>
		/// ʵ������
		/// </summary>
		[ColumnMapping("cnnCount",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public decimal cnnCount
		{
			get {return _cnnCount;}
			set {_cnnCount = value;}
		}

		/// <summary>
		/// ���
		/// </summary>
		[ColumnMapping("cnnSum",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public decimal cnnSum
		{
			get {return _cnnSum;}
			set {_cnnSum = value;}
		}

		/// <summary>
		/// ����Ա
		/// </summary>
		[ColumnMapping("cnvcOperID",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcOperID
		{
			get {return _cnvcOperID;}
			set {_cnvcOperID = value;}
		}

		/// <summary>
		/// ����ʱ��
		/// </summary>
		[ColumnMapping("cndOperDate",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public DateTime cndOperDate
		{
			get {return _cndOperDate;}
			set {_cndOperDate = value;}
		}
		#endregion
	}	
}
