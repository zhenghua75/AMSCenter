
/******************************************************************** FR 1.20E *******
* 项目名称：   AMSApp
* 文件名:   	Book.cs
* 作者:	     郑华
* 创建日期:    2010-5-3
* 功能描述:    留言本

*                                                           Copyright(C) 2010 zhenghua
*************************************************************************************/
#region Import NameSpace
using System;
using System.Data;
using AMSApp.zhenghua.EntityBase;
#endregion

namespace AMSApp.zhenghua.Entity
{
	/// <summary>
	/// **功能名称：留言本实体类
	/// </summary>
	[Serializable]
	[TableMapping("tbBook")]
	public class Book: EntityObjectBase
	{
		#region 数据表生成变量



		
		
		private int _cnnSerialNo;
		private string _cnvcBook = String.Empty;
		private string _cmvcPublishID = String.Empty;
		private string _cnvcPublishName = String.Empty;
		private string _cnvcCheckID = String.Empty;
		private string _cnvcCheckName = String.Empty;
		private string _cnvcState = String.Empty;
		private DateTime _cndPublishDate;
		private DateTime _cndcheckDate;
		private string _cnvcCheckDept = String.Empty;
		
		#endregion
		
		#region 构造函数




		public Book():base()
		{
		}
		
		public Book(DataRow row):base(row)
		{
		}
		
		public Book(DataTable table):base(table)
		{
		}
		
		public Book(string  strXML):base(strXML)
		{
		}
		#endregion
		
		#region 系统生成属性




				
		/// <summary>
		/// 
		/// </summary>
		[ColumnMapping("cnnSerialNo",IsPrimaryKey=true,IsIdentity=true,IsVersionNumber=false)]
		public int cnnSerialNo
		{
			get {return _cnnSerialNo;}
			set {_cnnSerialNo = value;}
		}

		/// <summary>
		/// 
		/// </summary>
		[ColumnMapping("cnvcBook",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcBook
		{
			get {return _cnvcBook;}
			set {_cnvcBook = value;}
		}

		/// <summary>
		/// 
		/// </summary>
		[ColumnMapping("cmvcPublishID",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cmvcPublishID
		{
			get {return _cmvcPublishID;}
			set {_cmvcPublishID = value;}
		}

		/// <summary>
		/// 
		/// </summary>
		[ColumnMapping("cnvcPublishName",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcPublishName
		{
			get {return _cnvcPublishName;}
			set {_cnvcPublishName = value;}
		}

		/// <summary>
		/// 
		/// </summary>
		[ColumnMapping("cnvcCheckID",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcCheckID
		{
			get {return _cnvcCheckID;}
			set {_cnvcCheckID = value;}
		}

		/// <summary>
		/// 
		/// </summary>
		[ColumnMapping("cnvcCheckName",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcCheckName
		{
			get {return _cnvcCheckName;}
			set {_cnvcCheckName = value;}
		}

		/// <summary>
		/// 
		/// </summary>
		[ColumnMapping("cnvcState",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcState
		{
			get {return _cnvcState;}
			set {_cnvcState = value;}
		}

		/// <summary>
		/// 
		/// </summary>
		[ColumnMapping("cndPublishDate",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public DateTime cndPublishDate
		{
			get {return _cndPublishDate;}
			set {_cndPublishDate = value;}
		}

		/// <summary>
		/// 
		/// </summary>
		[ColumnMapping("cndcheckDate",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public DateTime cndcheckDate
		{
			get {return _cndcheckDate;}
			set {_cndcheckDate = value;}
		}
		/// <summary>
		/// 
		/// </summary>
		[ColumnMapping("cnvcCheckDept",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcCheckDept
		{
			get {return _cnvcCheckDept;}
			set {_cnvcCheckDept = value;}
		}
		#endregion
	}	
}
