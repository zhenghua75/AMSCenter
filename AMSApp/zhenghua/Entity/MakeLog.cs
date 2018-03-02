
/******************************************************************** FR 1.20E *******
* ��Ŀ���ƣ�   AMSApp
* �ļ���:   	MakeLog.cs
* ����:	     ֣��
* ��������:    2008-10-12
* ��������:    �����ˮ��

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
	/// **�������ƣ������ˮ��ʵ����
	/// </summary>
	[Serializable]
	[TableMapping("tbMakeLog")]
	public class MakeLog: EntityObjectBase
	{
		#region ���ݱ����ɱ���



		
		
		private decimal _cnnProduceSerialNo;
		private decimal _cnnMakeSerialNo;
		private string _cnvcGroup = String.Empty;
		private string _cnvcMakeName = String.Empty;
		private string _cnvcMakeType = String.Empty;
		private string _cnvcClass = String.Empty;
		private string _cnvcInChargeOperID = String.Empty;
		private string _cnvcOperID = String.Empty;
		private DateTime _cndOperDate;
		
		#endregion
		
		#region ���캯��




		public MakeLog():base()
		{
		}
		
		public MakeLog(DataRow row):base(row)
		{
		}
		
		public MakeLog(DataTable table):base(table)
		{
		}
		
		public MakeLog(string  strXML):base(strXML)
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
		/// ������ˮ
		/// </summary>
		[ColumnMapping("cnnMakeSerialNo",IsPrimaryKey=true,IsIdentity=false,IsVersionNumber=false)]
		public decimal cnnMakeSerialNo
		{
			get {return _cnnMakeSerialNo;}
			set {_cnnMakeSerialNo = value;}
		}

		/// <summary>
		/// ������
		/// </summary>
		[ColumnMapping("cnvcGroup",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcGroup
		{
			get {return _cnvcGroup;}
			set {_cnvcGroup = value;}
		}

		/// <summary>
		/// �������
		/// </summary>
		[ColumnMapping("cnvcMakeName",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcMakeName
		{
			get {return _cnvcMakeName;}
			set {_cnvcMakeName = value;}
		}

		/// <summary>
		/// ��������
		/// </summary>
		[ColumnMapping("cnvcMakeType",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcMakeType
		{
			get {return _cnvcMakeType;}
			set {_cnvcMakeType = value;}
		}

		/// <summary>
		/// ���
		/// </summary>
		[ColumnMapping("cnvcClass",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcClass
		{
			get {return _cnvcClass;}
			set {_cnvcClass = value;}
		}

		/// <summary>
		/// ��������
		/// </summary>
		[ColumnMapping("cnvcInChargeOperID",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcInChargeOperID
		{
			get {return _cnvcInChargeOperID;}
			set {_cnvcInChargeOperID = value;}
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
