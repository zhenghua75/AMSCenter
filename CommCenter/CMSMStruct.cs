using System;
using System.Data;

namespace CommCenter
{
	/// <summary>
	/// Summary description for CMSMStruct.
	/// </summary>
	public class CMSMStruct
	{
		public CMSMStruct()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public class AssociatorStruct
		{
			public string strAssID;
			public string strCardID;
			public string strAssName;
			public string strSpell;
			public string strAssNbr;
			public string strLinkPhone;
			public string strLinkAddress;
			public string strEmail;
			public string strAssType;
			public string strAssState;
			public double dCharge;
			public int    iIgValue;
			public string strCardFlag;
			public string strComments;
			public string strCreateDate;
			public string strOperDate;
			public string strDeptID;
		}

		public class OperStruct
		{
			public string strOperType;
			public string strOperID;
			public string strDeptID;
			public string strMacAddress;
			public string strComments;
		}
		public class LoginStruct
		{
			public string strLoginID;
			public string strOperName;
			public string strLimit;
			public string strPwd;
			public string strDeptID;
			public string strNewDeptID;
		}

		public class GoodsStruct
		{
			public string strGoodsID;
			public string strGoodsName;
			public string strSpell;
			public double dPrice;
			public double dRate;
			public int    iIgValue;
			public string strNewFlag;
			public string strComments;
			public bool bPackage;
            public bool bNew;
            public bool bKey;
            public string Unit;
            public string NewDate;
            public bool bDeptPrice;
		}
		
		public class ConsItemStruct
		{
			public string strAssID;
			public string strCardID;
			public string strOperDate;
			public string strOperName;
			public DataTable dtItem;
			public double dChargeLast;
			public double dTolCharge;
			public double dTRate;
			public double dPay;
			public double dBalance;
			public int    iIgLast;
			public int    iIgValue;
			public string strConsType;
			public string strIgType;
			public string strDeptID;
		}

		public class ConsDownStruct
		{
			public string strSerial;
			public string strGoodsID;
			public string strAssID;
			public string strCardID;
			public double dPrice;
			public int    iCount;
			public double dTRate;
			public double dFee;
			public string strComments;
			public string strFlag;
			public string strConsDate;
			public string strOperName;
			public string strDeptID;
		}

		public class CommStruct
		{
			public string strCommName;
			public string strCommCode;
			public string strCommSign;
			public string strComments;
		}

		public class FillFeeStruct
		{
			public string strSerial;
			public string strAssID;
			public string strCardID;
			public double dFillFee;
			public double dFillProm;
			public double dFeeLast;
			public double dFeeCur;
			public string strFillDate;
			public string strComments;
			public string strOperName;
			public string strDeptID;
		}

		public class CardHardStruct
		{
			public string strCardID;
			public double dCurCharge;
			public int    iCurIg;
		}

		public class AssChangeStruct
		{
			public string strCardID;
			public string strChangeField;
			public string strChangeValue;
			public string strOperDate;
		}

		public class BillStruct
		{
			public string strSerial;
			public string strAssID;
			public string strCardID;
			public double dTRate;
			public double dFee;
			public double dPay;
			public double dBalance;
			public int    iIgValue;
			public string strConsType;
			public string strOperName;
			public string strConsDate;
			public string strDeptID;
		}

		public class IntegralStruct
		{
			public string strSerial;
			public string strAssID;
			public string strCardID;
			public string strIgType;
			public int    iIgLast;
			public int    iIgGet;
			public int    iIgArrival;
			public int    iLinkCons;
			public string strIgDate;
			public string strOperName;
			public string strComments;
			public string strDeptID;
		}

		public class BusiLogStruct
		{
			public string strSerial;
			public string strAssID;
			public string strCardID;
			public string strOperType;
			public string strOperName;
			public string strOperDate;
			public string strComments;
			public string strDeptID;
		}

		public class DeptToCenterLogStruct
		{
			public string strFileName;
			public int FileSize;
			public string strCreatingDate;
			public string strCreatedDate;
			public string strUpingDate;
			public string strUpedDate;
			public string strUpdatingDate;
			public string strUpdatedDate;
			public string strCreateFinish;
			public string strUpFinish;
			public string strUpdateFinish;
		}

		public class EmployeeStruct
		{
			public string strCardID;
			public string strEmpName;
			public string strSex;
			public string strEmpNbr;
			public string strInDate;
			public string strDegree;
			public string strLinkPhone;
			public string strAddress;
			public string strPwd;
			public string strOfficer;
			public string strDeptID;
			public string strFlag;
			public string strComments;
			public string strOperDate;
		}

		public class EmpSignStruct
		{
			public string strCardID;
			public string strEmpName;
			public string strSignDate;
			public string strClass;
			public string strSignFlag;
			public string strComments;
		}

		public class NoticeStruct
		{
			public string strid;
			public string strComments;
			public string strCreateDate;
			public string strActiveFlag;
			public string strDeptFlag;
		}

		public class SignIOTimeStruct
		{
			public string strSIOTID;
			public string strOfficer;
			public string strClassName;
			public string strClassId;
			public string strInTime;
			public string strOutTime;
		}

		public class DeptSchStruct
		{
			public string strSIOTID;
			public string strDeptName;
			public string strManager;
			public string strEmpNameGroup;
			public string strEmpOF;
			public string strClass;
			public string strCheckIn;
			public string strCheckOut;
		}

		public class SignAtomStruct
		{
			public string strSignFlag;
			public DateTime dtSignDate;
			public string strComments;
		}

		public class SignListStruct
		{
			public string strSignDate;
			public string strCardID;
			public string strClass;
			public string strEmpName;
			public string strDept;
			public string strOfficer;
			public string strSignIn;
			public string strSignOut;
			public string strSignState;
			public string strSignResult;
			public string strComments;
		}

		public class EmpSchLogStruct
		{
			public string strSchID;
			public string strDeptName;
			public string strManager;
			public string strCardID;
			public string strEmpName;
			public string strEmpOF;
			public string strClass;
			public string strCheckIn;
			public string strCheckOut;
		}

		public class MenuStruct
		{
			public string strFuncName;
			public string strFuncAddress;
		}

		public class OrderFormStruct
		{
			public string strOrderID;
			public string strOrderState;
			public string strDeptID;
			public string strOrderName;
			public string strDesDate;
			public string strOperName1;
			public string strOperDate1;
			public string strComments1;
			public string strOperName2;
			public string strOperDate2;
			public string strComments2;
			public string strOperName3;
			public string strOperDate3;
			public string strComments3;
			public string strOperName4;
			public string strOperDate4;
			public string strComments4;
			public string strOperName5;
			public string strOperDate5;
			public string strComments5;
			public string strOperDateString;
		}

		public class ProviderStruct
		{
			public string strProviderCode;
			public string strProviderName;
			public string strProductCode;
			public string strProductName;
			public string strProviderPrice;
			public string strProviderUnit;
			public string strProviderTime;
			public string strProviderQuality;
			public string strProviderValue;
			public string strLinkName;
			public string strLinkPhone;
			public string strLinkAddress;
		}

		public class ClientOperStruct
		{
			public string strOperID;
			public string strOperName;
			public string strLimit;
			public string strPwd;
			public string strDeptID;
			public string strActiveFlag;
			public string strPwdBeginFlag;
		}
	}

	public class PackagesStruct
	{
		public string strPackageId;
		public string strPackageName;
		public double dPackagePrice;
		public string strGoodsId;
		public string strGoodsName;
		public double dGoodsPrice;
		public string strComments;
	}
}
