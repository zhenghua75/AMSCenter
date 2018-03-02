
/******************************************************************** FR 1.20E *******
* 项目名称：   AMSApp
* 文件名:   	MakeLog.cs
* 作者:	     郑华
* 创建日期:    2008-10-12
* 功能描述:    制令单流水表

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
	/// **功能名称：制令单流水表实体类
	/// </summary>
	[Serializable]
	[TableMapping("tbMakeLog")]
	public class MakeLog: EntityObjectBase
	{
		#region 数据表生成变量



		
		
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
		
		#region 构造函数




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
		
		#region 系统生成属性




				
		/// <summary>
		/// 生产流水
		/// </summary>
		[ColumnMapping("cnnProduceSerialNo",IsPrimaryKey=true,IsIdentity=false,IsVersionNumber=false)]
		public decimal cnnProduceSerialNo
		{
			get {return _cnnProduceSerialNo;}
			set {_cnnProduceSerialNo = value;}
		}

		/// <summary>
		/// 制令流水
		/// </summary>
		[ColumnMapping("cnnMakeSerialNo",IsPrimaryKey=true,IsIdentity=false,IsVersionNumber=false)]
		public decimal cnnMakeSerialNo
		{
			get {return _cnnMakeSerialNo;}
			set {_cnnMakeSerialNo = value;}
		}

		/// <summary>
		/// 生产组
		/// </summary>
		[ColumnMapping("cnvcGroup",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcGroup
		{
			get {return _cnvcGroup;}
			set {_cnvcGroup = value;}
		}

		/// <summary>
		/// 制令单名称
		/// </summary>
		[ColumnMapping("cnvcMakeName",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcMakeName
		{
			get {return _cnvcMakeName;}
			set {_cnvcMakeName = value;}
		}

		/// <summary>
		/// 制令类型
		/// </summary>
		[ColumnMapping("cnvcMakeType",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcMakeType
		{
			get {return _cnvcMakeType;}
			set {_cnvcMakeType = value;}
		}

		/// <summary>
		/// 班次
		/// </summary>
		[ColumnMapping("cnvcClass",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcClass
		{
			get {return _cnvcClass;}
			set {_cnvcClass = value;}
		}

		/// <summary>
		/// 生控主管
		/// </summary>
		[ColumnMapping("cnvcInChargeOperID",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcInChargeOperID
		{
			get {return _cnvcInChargeOperID;}
			set {_cnvcInChargeOperID = value;}
		}

		/// <summary>
		/// 操作员
		/// </summary>
		[ColumnMapping("cnvcOperID",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcOperID
		{
			get {return _cnvcOperID;}
			set {_cnvcOperID = value;}
		}

		/// <summary>
		/// 操作时间
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
