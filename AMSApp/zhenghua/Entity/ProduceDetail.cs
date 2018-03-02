
/******************************************************************** FR 1.20E *******
* ��Ŀ���ƣ�   AMSApp
* �ļ���:   	ProduceDetail.cs
* ����:	     ֣��
* ��������:    2008-10-10
* ��������:    �����ƻ���Ʒϸ�ڱ�

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
	/// **�������ƣ������ƻ���Ʒϸ�ڱ�ʵ����
	/// </summary>
	[Serializable]
	[TableMapping("tbProduceDetail")]
	public class ProduceDetail: EntityObjectBase
	{
		#region ���ݱ����ɱ���



		
		
		private decimal _cnnProduceSerialNo;
		private string _cnvcCode = String.Empty;
		private string _cnvcName = String.Empty;
		private string _cnvcUnit = String.Empty;
		private decimal _cnnCount;
		
		#endregion
		
		#region ���캯��




		public ProduceDetail():base()
		{
		}
		
		public ProduceDetail(DataRow row):base(row)
		{
		}
		
		public ProduceDetail(DataTable table):base(table)
		{
		}
		
		public ProduceDetail(string  strXML):base(strXML)
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
		/// ��Ʒ����
		/// </summary>
		[ColumnMapping("cnvcCode",IsPrimaryKey=true,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcCode
		{
			get {return _cnvcCode;}
			set {_cnvcCode = value;}
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
		[ColumnMapping("cnnCount",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public decimal cnnCount
		{
			get {return _cnnCount;}
			set {_cnnCount = value;}
		}
		#endregion
	}	
}
