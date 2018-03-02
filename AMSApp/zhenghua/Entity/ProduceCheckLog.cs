
/******************************************************************** FR 1.20E *******
* ��Ŀ���ƣ�   AMSApp
* �ļ���:   	ProduceCheckLog.cs
* ����:	     ֣��
* ��������:    2008-10-28
* ��������:    �����̵���ˮ��

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
	/// **�������ƣ������̵���ˮ��ʵ����
	/// </summary>
	[Serializable]
	[TableMapping("tbProduceCheckLog")]
	public class ProduceCheckLog: EntityObjectBase
	{
		#region ���ݱ����ɱ���



		
		
		private decimal _cnnProduceSerialNo;
		private string _cnvcProduceDeptID = String.Empty;
		private string _cnvcOperID = String.Empty;
		private DateTime _cndOperDate;
		private string _cnvcName = String.Empty;
		private string _cnvcCode = String.Empty;
		private string _cnvcUnit = String.Empty;
		private decimal _cnnOrderCount;
		private decimal _cnnAddCount;
		private decimal _cnnReduceCount;
		private decimal _cnnProduceCount;
		private decimal _cnnCheckCount;
		private decimal _cnnAssignCount;
		
		#endregion
		
		#region ���캯��




		public ProduceCheckLog():base()
		{
		}
		
		public ProduceCheckLog(DataRow row):base(row)
		{
		}
		
		public ProduceCheckLog(DataTable table):base(table)
		{
		}
		
		public ProduceCheckLog(string  strXML):base(strXML)
		{
		}
		#endregion
		
		#region ϵͳ��������




				
		/// <summary>
		/// ������ˮ
		/// </summary>
		[ColumnMapping("cnnProduceSerialNo",IsPrimaryKey=true,IsIdentity=false,IsVersionNumber=false)]
		public decimal cnnProduceSerialNo
		{
			get {return _cnnProduceSerialNo;}
			set {_cnnProduceSerialNo = value;}
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

		/// <summary>
		/// ��Ʒ����
		/// </summary>
		[ColumnMapping("cnvcName",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcName
		{
			get {return _cnvcName;}
			set {_cnvcName = value;}
		}

		/// <summary>
		/// ��Ʒ����
		/// </summary>
		[ColumnMapping("cnvcCode",IsPrimaryKey=true,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcCode
		{
			get {return _cnvcCode;}
			set {_cnvcCode = value;}
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
		/// ������
		/// </summary>
		[ColumnMapping("cnnOrderCount",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public decimal cnnOrderCount
		{
			get {return _cnnOrderCount;}
			set {_cnnOrderCount = value;}
		}

		/// <summary>
		/// �ӵ���
		/// </summary>
		[ColumnMapping("cnnAddCount",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public decimal cnnAddCount
		{
			get {return _cnnAddCount;}
			set {_cnnAddCount = value;}
		}

		/// <summary>
		/// ������
		/// </summary>
		[ColumnMapping("cnnReduceCount",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public decimal cnnReduceCount
		{
			get {return _cnnReduceCount;}
			set {_cnnReduceCount = value;}
		}

		/// <summary>
		/// ������
		/// </summary>
		[ColumnMapping("cnnProduceCount",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public decimal cnnProduceCount
		{
			get {return _cnnProduceCount;}
			set {_cnnProduceCount = value;}
		}

		/// <summary>
		/// �̵���
		/// </summary>
		[ColumnMapping("cnnCheckCount",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public decimal cnnCheckCount
		{
			get {return _cnnCheckCount;}
			set {_cnnCheckCount = value;}
		}
		/// <summary>
		/// �̵���
		/// </summary>
		[ColumnMapping("cnnAssignCount",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public decimal cnnAssignCount
		{
			get {return _cnnAssignCount;}
			set {_cnnAssignCount = value;}
		}
		#endregion
	}	
}
