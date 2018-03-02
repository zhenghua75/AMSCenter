
/******************************************************************** FR 1.20E *******
* ��Ŀ���ƣ�   AMSApp
* �ļ���:   	MakeDetail.cs
* ����:	     ֣��
* ��������:    2008-10-10
* ��������:    ���������Ʒϸ�ڱ�

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
	/// **�������ƣ����������Ʒϸ�ڱ�ʵ����
	/// </summary>
	[Serializable]
	[TableMapping("tbMakeDetail")]
	public class MakeDetail: EntityObjectBase
	{
		#region ���ݱ����ɱ���



		
		
		private decimal _cnnMakeSerialNo;
		private string _cnvcCode = String.Empty;
		private string _cnvcName = String.Empty;
		private string _cnvcUnit = String.Empty;
		private decimal _cnnCount;
		
		#endregion
		
		#region ���캯��




		public MakeDetail():base()
		{
		}
		
		public MakeDetail(DataRow row):base(row)
		{
		}
		
		public MakeDetail(DataTable table):base(table)
		{
		}
		
		public MakeDetail(string  strXML):base(strXML)
		{
		}
		#endregion
		
		#region ϵͳ��������




				
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
