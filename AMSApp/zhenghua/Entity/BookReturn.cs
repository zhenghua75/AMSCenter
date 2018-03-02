
/******************************************************************** FR 1.20E *******
* 项目名称：   AMSApp
* 文件名:   	BookReturn.cs
* 作者:	     郑华
* 创建日期:    2010-5-5
* 功能描述:    留言本回复

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
	/// **功能名称：留言本回复实体类
	/// </summary>
	[Serializable]
	[TableMapping("tbBookReturn")]
	public class BookReturn: EntityObjectBase
	{
		#region 数据表生成变量



		
		
		private int _cnnBookID;
		private int _cnnSerialNo;
		private string _cnvcReturn = String.Empty;
		private DateTime _cndReturnDate;
		private string _cnvcReturnID = String.Empty;
		private string _cnvcReturnName = String.Empty;
		
		#endregion
		
		#region 构造函数




		public BookReturn():base()
		{
		}
		
		public BookReturn(DataRow row):base(row)
		{
		}
		
		public BookReturn(DataTable table):base(table)
		{
		}
		
		public BookReturn(string  strXML):base(strXML)
		{
		}
		#endregion
		
		#region 系统生成属性




				
		/// <summary>
		/// 
		/// </summary>
		[ColumnMapping("cnnBookID",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public int cnnBookID
		{
			get {return _cnnBookID;}
			set {_cnnBookID = value;}
		}

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
		[ColumnMapping("cnvcReturn",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcReturn
		{
			get {return _cnvcReturn;}
			set {_cnvcReturn = value;}
		}

		/// <summary>
		/// 
		/// </summary>
		[ColumnMapping("cndReturnDate",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public DateTime cndReturnDate
		{
			get {return _cndReturnDate;}
			set {_cndReturnDate = value;}
		}

		/// <summary>
		/// 
		/// </summary>
		[ColumnMapping("cnvcReturnID",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcReturnID
		{
			get {return _cnvcReturnID;}
			set {_cnvcReturnID = value;}
		}

		/// <summary>
		/// 
		/// </summary>
		[ColumnMapping("cnvcReturnName",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcReturnName
		{
			get {return _cnvcReturnName;}
			set {_cnvcReturnName = value;}
		}
		#endregion
	}	
}
