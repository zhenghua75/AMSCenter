using System;
using DataAccess;
using System.Data;
using CommCenter;
using System.Collections;

namespace BusiComm
{
	/// <summary>
	/// Summary description for Manager.
	/// </summary>
	public class BusiQuery
	{
		string strcon="";
		QueryAcc Qa=null;
		public BusiQuery(string strcons)
		{
			//
			// TODO: Add constructor logic here
			//
			strcon=strcons;
			Qa=new QueryAcc(strcon);
		}

		public DataTable GetConsQuery(Hashtable htpara)
		{
			DataTable dtout=Qa.GetConsQuery(htpara);
			if(dtout!=null)
			{
				dtout.Columns["iSerial"].ColumnName="流水";
				dtout.Columns["vcAssName"].ColumnName="会员名称";
				dtout.Columns["vcAssType"].ColumnName="会员类型";
				dtout.Columns["vcCardID"].ColumnName="会员卡号";
				dtout.Columns["vcGoodsName"].ColumnName="商品名称";
//				dtout.Columns["vcGoodsID"].ColumnName="商品ID";
				dtout.Columns["nPrice"].ColumnName="单价";
				dtout.Columns["iCount"].ColumnName="数量";
				dtout.Columns["nFee"].ColumnName="合计";
				dtout.Columns["vcConsType"].ColumnName="付款类型";
				dtout.Columns["vcComments"].ColumnName="备注";
				dtout.Columns["cFlag"].ColumnName="有效状态";
				dtout.Columns["dtConsDate"].ColumnName="消费日期";
				dtout.Columns["vcOperName"].ColumnName="操作员";
				dtout.Columns["vcDeptID"].ColumnName="门店";
			}
			return dtout;
		}

		public DataTable GetFillQuery(Hashtable htpara)
		{
			DataTable dtout=Qa.GetFillQuery(htpara);
			if(dtout!=null)
			{
				dtout.Columns["iSerial"].ColumnName="流水";
				dtout.Columns["vcAssName"].ColumnName="会员名称";
				dtout.Columns["vcAssType"].ColumnName="会员类型";
				dtout.Columns["vcCardID"].ColumnName="会员卡号";
				dtout.Columns["nFillFee"].ColumnName="充值金额";
				dtout.Columns["nFillProm"].ColumnName="赠款金额";
				dtout.Columns["nFeeLast"].ColumnName="上次余额";
				dtout.Columns["nFeeCur"].ColumnName="当前余额";
				dtout.Columns["vcComments"].ColumnName="备注";
				dtout.Columns["dtFillDate"].ColumnName="充值日期";
				dtout.Columns["vcOperName"].ColumnName="操作员";
				dtout.Columns["vcDeptID"].ColumnName="操作员门店";
			}
			return dtout;
		}

		public DataTable GetConsKindQuery(Hashtable htpara,string flag,bool bDate,bool bAssType,bool bGoodsType,bool bDept)
		{
			DataTable dtout=Qa.GetConsKindQuery(htpara,flag,bDate,bAssType,bGoodsType,bDept);
			if(dtout!=null)
			{
				if(dtout.Columns.Contains("vcDeptID"))
				{
					dtout.Columns["vcDeptID"].ColumnName="门店";
				}
				if(dtout.Columns.Contains("vcAssType"))
				{
					dtout.Columns["vcAssType"].ColumnName="会员类型";
				}
				if(dtout.Columns.Contains("vcGoodsType"))
				{
					dtout.Columns["vcGoodsType"].ColumnName="商品类型";
				}
				dtout.Columns["vcGoodsName"].ColumnName="商品名称";
				dtout.Columns["tolcount"].ColumnName="数量合计";
				dtout.Columns["tolfee"].ColumnName="金额合计";
			}
			return dtout;
		}

		public DataTable GetBusiLogQuery(Hashtable htpara)
		{
			DataTable dtout=Qa.GetBusiLogQuery(htpara);
			if(dtout!=null)
			{
				dtout.Columns["iSerial"].ColumnName="流水";
				dtout.Columns["vcAssName"].ColumnName="会员名称";
				dtout.Columns["vcAssType"].ColumnName="会员类型";
				dtout.Columns["vcCardID"].ColumnName="会员卡号";
				dtout.Columns["vcCommName"].ColumnName="操作类型";
				dtout.Columns["vcOperName"].ColumnName="操作员";
				dtout.Columns["dtOperDate"].ColumnName="操作日期";
				dtout.Columns["vcDeptID"].ColumnName="操作员门店";
				dtout.Columns["vcComments"].ColumnName="备注";
			}
			return dtout;
		}

		public DataTable BusiIncomeReport(Hashtable htpara,string strDeptName)
		{
			DataSet dsout=Qa.BusiIncomeReport(htpara);
			DataTable dtIncome=null;
			if(dsout!=null)
			{
				dtIncome=ReportConvert(dsout,strDeptName);
			}
			return dtIncome;
		}

		private DataTable ReportConvert(DataSet dsIn,string strname)
		{
			string strtmp;
			DataTable dtdis=new DataTable();

			#region 总体情况，如果查询门店为全部，则只有此总情况，没有下面的本店与他店情况
			strtmp="";
			dtdis.Columns.Add("type");
			dtdis.Columns.Add("col1");
			dtdis.Columns.Add("col2");
			dtdis.Columns.Add("col3");
			dtdis.Columns.Add("col4");
			dtdis.Columns.Add("col5");
			dtdis.Columns.Add("col6");
			dtdis.Columns.Add("col7");

			DataRow dr;
			foreach (DataRow drr in dsIn.Tables["AllIncome"].Rows)
			{
				switch(drr["Type"].ToString())
				{
					case "OldState":
						dr=dtdis.NewRow();
						dr["type"]="原状态";
						dr["col1"]=drr["REP1"].ToString();
						dr["col2"]=drr["REP2"].ToString();
						dr["col3"]="......";
						dr["col4"]=drr["REP4"].ToString();
						dr["col5"]="......";
						dr["col6"]="......";
						dr["col7"]="......";
						dtdis.Rows.Add(dr);
						break;
					case "NewState":
						dr=dtdis.NewRow();
						dr["type"]="现状态";
						dr["col1"]=drr["REP1"].ToString();
						dr["col2"]=drr["REP2"].ToString();
						dr["col3"]="......";
						dr["col4"]=drr["REP4"].ToString();
						dr["col5"]="......";
						dr["col6"]="......";
						dr["col7"]="......";
						dtdis.Rows.Add(dr);
						break;
					case "NewAss":
						dr=dtdis.NewRow();
						dr["type"]="新入会员";
						dr["col1"]=drr["REP1"].ToString();
						dr["col2"]=drr["REP2"].ToString();
						dr["col3"]="......";
						dr["col4"]=drr["REP4"].ToString();
						dr["col5"]="......";
						dr["col6"]="......";
						dr["col7"]="......";
						dtdis.Rows.Add(dr);
						break;
					case "LostAss":
						dr=dtdis.NewRow();
						dr["type"]="挂失会员";
						dr["col1"]=drr["REP1"].ToString();
						dr["col2"]="......";
						dr["col3"]="......";
						dr["col4"]="......";
						dr["col5"]="......";
						dr["col6"]="......";
						dr["col7"]="......";
						dtdis.Rows.Add(dr);
						break;
					case "FillFee":
						dr=dtdis.NewRow();
						dr["type"]="充值";
						dr["col1"]=drr["REP1"].ToString();
						dr["col2"]="......";
						dr["col3"]="......";
						dr["col4"]=drr["REP4"].ToString();
						dr["col5"]=drr["REP5"].ToString();
						strtmp=drr["REP6"].ToString();
						if(strtmp=="0")
						{
							dr["col6"]="0";
						}
						else
						{
							dr["col6"]=strtmp.Substring(0,strtmp.IndexOf(".",0));
						}
						dr["col7"]="......";
						dtdis.Rows.Add(dr);
						break;
					case "AssCons":
						dr=dtdis.NewRow();
						dr["type"]="普通会员消费";
						dr["col1"]=drr["REP1"].ToString();
						dr["col2"]="......";
						dr["col3"]="......";
						dr["col4"]=drr["REP4"].ToString();
						strtmp=drr["REP5"].ToString();
						if(strtmp=="0")
						{
							dr["col5"]="0";
						}
						else
						{
							dr["col5"]=strtmp.Substring(0,strtmp.IndexOf(".",0));
						}
						strtmp=drr["REP6"].ToString();
						if(strtmp=="0")
						{
							dr["col6"]="0";
						}
						else
						{
							dr["col6"]=strtmp.Substring(0,strtmp.IndexOf(".",0));
						}
						dr["col7"]=drr["REP7"].ToString();
						dtdis.Rows.Add(dr);
						break;
					case "PromCons":
						dr=dtdis.NewRow();
						dr["type"]="赠卡会员消费";
						dr["col1"]=drr["REP1"].ToString();
						dr["col2"]="......";
						dr["col3"]="......";
						dr["col4"]=drr["REP4"].ToString();
						strtmp=drr["REP5"].ToString();
						if(strtmp=="0")
						{
							dr["col5"]="0";
						}
						else
						{
							dr["col5"]=strtmp.Substring(0,strtmp.IndexOf(".",0));
						}
						strtmp=drr["REP6"].ToString();
						if(strtmp=="0")
						{
							dr["col6"]="0";
						}
						else
						{
							dr["col6"]=strtmp.Substring(0,strtmp.IndexOf(".",0));
						}
						dr["col7"]=drr["REP7"].ToString();
						dtdis.Rows.Add(dr);
						break;
					case "Retail":
						dr=dtdis.NewRow();
						dr["type"]="零售";
						dr["col1"]="......";
						dr["col2"]="......";
						dr["col3"]="......";
						dr["col4"]=drr["REP4"].ToString();
						dr["col5"]="......";
						strtmp=drr["REP6"].ToString();
						if(strtmp=="0")
						{
							dr["col6"]="0";
						}
						else
						{
							dr["col6"]=strtmp.Substring(0,strtmp.IndexOf(".",0));
						}
						dr["col7"]=drr["REP7"].ToString();
						dtdis.Rows.Add(dr);
						break;
					case "Larg":
						dr=dtdis.NewRow();
						dr["type"]="会员赠送";
						dr["col1"]="......";
						dr["col2"]="......";
						dr["col3"]="......";
						dr["col4"]="......";
						dr["col5"]="......";
						strtmp=drr["REP6"].ToString();
						if(strtmp=="0")
						{
							dr["col6"]="0";
						}
						else
						{
							dr["col6"]=strtmp.Substring(0,strtmp.IndexOf(".",0));
						}
						dr["col7"]=drr["REP7"].ToString();
						dtdis.Rows.Add(dr);
						break;
					case "IgChange":
						dr=dtdis.NewRow();
						dr["type"]="积分兑换";
						dr["col1"]=drr["REP1"].ToString();
						dr["col2"]="......";
						strtmp=drr["REP3"].ToString();
						if(strtmp=="0")
						{
							dr["col3"]="0";
						}
						else
						{
							dr["col3"]=strtmp.Substring(0,strtmp.IndexOf(".",0));
						}
						dr["col4"]="......";
						dr["col5"]="......";
						strtmp=drr["REP6"].ToString();
						if(strtmp=="0")
						{
							dr["col6"]="0";
						}
						else
						{
							dr["col6"]=strtmp.Substring(0,strtmp.IndexOf(".",0));
						}
						dr["col7"]=drr["REP7"].ToString();
						dtdis.Rows.Add(dr);
						break;
					case "WasteSpec":
						dr=dtdis.NewRow();
						dr["type"]="报损消耗";
						dr["col1"]="......";
						dr["col2"]="......";
						dr["col3"]="......";
						dr["col4"]="......";
						dr["col5"]="......";
						strtmp=drr["REP6"].ToString();
						if(strtmp=="0")
						{
							dr["col6"]="0";
						}
						else
						{
							dr["col6"]=strtmp.Substring(0,strtmp.IndexOf(".",0));
						}
						dr["col7"]=drr["REP7"].ToString();
						dtdis.Rows.Add(dr);
						break;
					case "TasteSpec":
						dr=dtdis.NewRow();
						dr["type"]="品尝消耗";
						dr["col1"]="......";
						dr["col2"]="......";
						dr["col3"]="......";
						dr["col4"]="......";
						dr["col5"]="......";
						strtmp=drr["REP6"].ToString();
						if(strtmp=="0")
						{
							dr["col6"]="0";
						}
						else
						{
							dr["col6"]=strtmp.Substring(0,strtmp.IndexOf(".",0));
						}
						dr["col7"]=drr["REP7"].ToString();
						dtdis.Rows.Add(dr);
						break;
					case "BackGoodsSpec":
						dr=dtdis.NewRow();
						dr["type"]="退货消耗";
						dr["col1"]="......";
						dr["col2"]="......";
						dr["col3"]="......";
						dr["col4"]="......";
						dr["col5"]="......";
						strtmp=drr["REP6"].ToString();
						if(strtmp=="0")
						{
							dr["col6"]="0";
						}
						else
						{
							dr["col6"]=strtmp.Substring(0,strtmp.IndexOf(".",0));
						}
						dr["col7"]=drr["REP7"].ToString();
						dtdis.Rows.Add(dr);
						break;
				}
			}

			dr=dtdis.NewRow();
			dr["type"]="";
			dr["col1"]="会员增加总数";
			dr["col2"]="积分增加总额";
			dr["col3"]="余额增加总额";
			dr["col4"]="现金增加总额";
			dr["col5"]="赠款增加总额";
			dr["col6"]="会员消费总额";
			dr["col7"]="商品销售总数";
			dtdis.Rows.Add(dr);

			dr=dtdis.NewRow();
			dr["type"]="总计";
			dr["col1"]=dsIn.Tables["AllIncome"].Rows[13]["REP1"].ToString();
			dr["col2"]=dsIn.Tables["AllIncome"].Rows[13]["REP2"].ToString();
			dr["col3"]=dsIn.Tables["AllIncome"].Rows[13]["REP3"].ToString();
			dr["col4"]=dsIn.Tables["AllIncome"].Rows[13]["REP4"].ToString();
			dr["col5"]=dsIn.Tables["AllIncome"].Rows[13]["REP5"].ToString();
			dr["col6"]=dsIn.Tables["AllIncome"].Rows[13]["REP6"].ToString();
			dr["col7"]=dsIn.Tables["AllIncome"].Rows[13]["REP7"].ToString();
			dtdis.Rows.Add(dr);

//			if(strname=="全部")
//			{
//				dtdis.Columns["type"].ColumnName="所有门店";
//			}
//			else
//			{
//				dtdis.Columns["type"].ColumnName=strname;
//			}			
//			dtdis.Columns["col1"].ColumnName="会员数";
//			dtdis.Columns["col2"].ColumnName="可用积分";
//			dtdis.Columns["col3"].ColumnName="使用积分";
//			dtdis.Columns["col4"].ColumnName="金额";
//			dtdis.Columns["col5"].ColumnName="附赠情况";
//			dtdis.Columns["col6"].ColumnName="次数";
//			dtdis.Columns["col7"].ColumnName="商品数";
//
//			dsret.Tables.Add(dtdis);
			#endregion

			if(strname!="全部")
			{
				#region 本店会员在本店充值和消费情况
				strtmp="";
				dr=dtdis.NewRow();
				dr["type"]="";
				dr["col1"]="";
				dr["col2"]="";
				dr["col3"]="";
				dr["col4"]="";
				dr["col5"]="";
				dr["col6"]="";
				dr["col7"]="";
				dtdis.Rows.Add(dr);

				dr=dtdis.NewRow();
				dr["type"]="本店会员在本店";
				dr["col1"]="";
				dr["col2"]="";
				dr["col3"]="";
				dr["col4"]="";
				dr["col5"]="";
				dr["col6"]="";
				dr["col7"]="";
				dtdis.Rows.Add(dr);

				foreach (DataRow drr in dsIn.Tables["LocalIncome"].Rows)
				{
					switch(drr["Type"].ToString())
					{
						case "Local-FillFee":
							dr=dtdis.NewRow();
							dr["type"]="充值";
							dr["col1"]=drr["REP1"].ToString();
							dr["col2"]="......";
							dr["col3"]="......";
							dr["col4"]=drr["REP4"].ToString();
							dr["col5"]=drr["REP5"].ToString();
							strtmp=drr["REP6"].ToString();
							if(strtmp=="0")
							{
								dr["col6"]="0";
							}
							else
							{
								dr["col6"]=strtmp.Substring(0,strtmp.IndexOf(".",0));
							}
							dr["col7"]="......";
							dtdis.Rows.Add(dr);
							break;
						case "Local-AssCons":
							dr=dtdis.NewRow();
							dr["type"]="普通会员消费";
							dr["col1"]=drr["REP1"].ToString();
							dr["col2"]="......";
							dr["col3"]="......";
							dr["col4"]=drr["REP4"].ToString();
							strtmp=drr["REP5"].ToString();
							if(strtmp=="0")
							{
								dr["col5"]="0";
							}
							else
							{
								dr["col5"]=strtmp.Substring(0,strtmp.IndexOf(".",0));
							}
							strtmp=drr["REP6"].ToString();
							if(strtmp=="0")
							{
								dr["col6"]="0";
							}
							else
							{
								dr["col6"]=strtmp.Substring(0,strtmp.IndexOf(".",0));
							}
							dr["col7"]=drr["REP7"].ToString();
							dtdis.Rows.Add(dr);
							break;
						case "Local-PromCons":
							dr=dtdis.NewRow();
							dr["type"]="赠卡会员消费";
							dr["col1"]=drr["REP1"].ToString();
							dr["col2"]="......";
							dr["col3"]="......";
							dr["col4"]=drr["REP4"].ToString();
							strtmp=drr["REP5"].ToString();
							if(strtmp=="0")
							{
								dr["col5"]="0";
							}
							else
							{
								dr["col5"]=strtmp.Substring(0,strtmp.IndexOf(".",0));
							}
							strtmp=drr["REP6"].ToString();
							if(strtmp=="0")
							{
								dr["col6"]="0";
							}
							else
							{
								dr["col6"]=strtmp.Substring(0,strtmp.IndexOf(".",0));
							}
							dr["col7"]=drr["REP7"].ToString();
							dtdis.Rows.Add(dr);
							break;
						case "Local-Larg":
							dr=dtdis.NewRow();
							dr["type"]="会员赠送";
							dr["col1"]="......";
							dr["col2"]="......";
							dr["col3"]="......";
							dr["col4"]="......";
							dr["col5"]="......";
							strtmp=drr["REP6"].ToString();
							if(strtmp=="0")
							{
								dr["col6"]="0";
							}
							else
							{
								dr["col6"]=strtmp.Substring(0,strtmp.IndexOf(".",0));
							}
							dr["col7"]=drr["REP7"].ToString();
							dtdis.Rows.Add(dr);
							break;
						case "Local-IgChange":
							dr=dtdis.NewRow();
							dr["type"]="积分兑换";
							dr["col1"]=drr["REP1"].ToString();
							dr["col2"]="......";
							strtmp=drr["REP3"].ToString();
							if(strtmp=="0")
							{
								dr["col3"]="0";
							}
							else
							{
								dr["col3"]=strtmp.Substring(0,strtmp.IndexOf(".",0));
							}
							dr["col4"]="......";
							dr["col5"]="......";
							strtmp=drr["REP6"].ToString();
							if(strtmp=="0")
							{
								dr["col6"]="0";
							}
							else
							{
								dr["col6"]=strtmp.Substring(0,strtmp.IndexOf(".",0));
							}
							dr["col7"]=drr["REP7"].ToString();
							dtdis.Rows.Add(dr);
							break;
					}
				}

//				dr=dtdis.NewRow();
//				dr["type"]="";
//				dr["col1"]="会员增加总数";
//				dr["col2"]="积分增加总额";
//				dr["col3"]="余额增加总额";
//				dr["col4"]="现金增加总额";
//				dr["col5"]="赠款增加总额";
//				dr["col6"]="会员消费总额";
//				dr["col7"]="商品销售总数";
//				dtdis.Rows.Add(dr);
//
//				dr=dtdis.NewRow();
//				dr["type"]="总计";
//				dr["col1"]=dtreg.Rows[10]["REP1"].ToString();
//				dr["col2"]=dtreg.Rows[10]["REP2"].ToString();
//				dr["col3"]=dtreg.Rows[10]["REP3"].ToString();
//				dr["col4"]=dtreg.Rows[10]["REP4"].ToString();
//				dr["col5"]=dtreg.Rows[10]["REP5"].ToString();
//				dr["col6"]=dtreg.Rows[10]["REP6"].ToString();
//				dr["col7"]=dtreg.Rows[10]["REP7"].ToString();
//				dtdis.Rows.Add(dr);
				#endregion

				#region 他店会员在本店充值和消费情况
				strtmp="";
				dr=dtdis.NewRow();
				dr["type"]="";
				dr["col1"]="";
				dr["col2"]="";
				dr["col3"]="";
				dr["col4"]="";
				dr["col5"]="";
				dr["col6"]="";
				dr["col7"]="";
				dtdis.Rows.Add(dr);

				dr=dtdis.NewRow();
				dr["type"]="他店会员在本店";
				dr["col1"]="";
				dr["col2"]="";
				dr["col3"]="";
				dr["col4"]="";
				dr["col5"]="";
				dr["col6"]="";
				dr["col7"]="";
				dtdis.Rows.Add(dr);

				foreach (DataRow drr in dsIn.Tables["OtherIncome"].Rows)
				{
					switch(drr["Type"].ToString())
					{
						case "Other-FillFee":
							dr=dtdis.NewRow();
							dr["type"]="充值";
							dr["col1"]=drr["REP1"].ToString();
							dr["col2"]="......";
							dr["col3"]="......";
							dr["col4"]=drr["REP4"].ToString();
							dr["col5"]=drr["REP5"].ToString();
							strtmp=drr["REP6"].ToString();
							if(strtmp=="0")
							{
								dr["col6"]="0";
							}
							else
							{
								dr["col6"]=strtmp.Substring(0,strtmp.IndexOf(".",0));
							}
							dr["col7"]="......";
							dtdis.Rows.Add(dr);
							break;
						case "Other-AssCons":
							dr=dtdis.NewRow();
							dr["type"]="普通会员消费";
							dr["col1"]=drr["REP1"].ToString();
							dr["col2"]="......";
							dr["col3"]="......";
							dr["col4"]=drr["REP4"].ToString();
							strtmp=drr["REP5"].ToString();
							if(strtmp=="0")
							{
								dr["col5"]="0";
							}
							else
							{
								dr["col5"]=strtmp.Substring(0,strtmp.IndexOf(".",0));
							}
							strtmp=drr["REP6"].ToString();
							if(strtmp=="0")
							{
								dr["col6"]="0";
							}
							else
							{
								dr["col6"]=strtmp.Substring(0,strtmp.IndexOf(".",0));
							}
							dr["col7"]=drr["REP7"].ToString();
							dtdis.Rows.Add(dr);
							break;
						case "Other-PromCons":
							dr=dtdis.NewRow();
							dr["type"]="赠卡会员消费";
							dr["col1"]=drr["REP1"].ToString();
							dr["col2"]="......";
							dr["col3"]="......";
							dr["col4"]=drr["REP4"].ToString();
							strtmp=drr["REP5"].ToString();
							if(strtmp=="0")
							{
								dr["col5"]="0";
							}
							else
							{
								dr["col5"]=strtmp.Substring(0,strtmp.IndexOf(".",0));
							}
							strtmp=drr["REP6"].ToString();
							if(strtmp=="0")
							{
								dr["col6"]="0";
							}
							else
							{
								dr["col6"]=strtmp.Substring(0,strtmp.IndexOf(".",0));
							}
							dr["col7"]=drr["REP7"].ToString();
							dtdis.Rows.Add(dr);
							break;
						case "Other-Larg":
							dr=dtdis.NewRow();
							dr["type"]="会员赠送";
							dr["col1"]="......";
							dr["col2"]="......";
							dr["col3"]="......";
							dr["col4"]="......";
							dr["col5"]="......";
							strtmp=drr["REP6"].ToString();
							if(strtmp=="0")
							{
								dr["col6"]="0";
							}
							else
							{
								dr["col6"]=strtmp.Substring(0,strtmp.IndexOf(".",0));
							}
							dr["col7"]=drr["REP7"].ToString();
							dtdis.Rows.Add(dr);
							break;
						case "Other-IgChange":
							dr=dtdis.NewRow();
							dr["type"]="积分兑换";
							dr["col1"]=drr["REP1"].ToString();
							dr["col2"]="......";
							strtmp=drr["REP3"].ToString();
							if(strtmp=="0")
							{
								dr["col3"]="0";
							}
							else
							{
								dr["col3"]=strtmp.Substring(0,strtmp.IndexOf(".",0));
							}
							dr["col4"]="......";
							dr["col5"]="......";
							strtmp=drr["REP6"].ToString();
							if(strtmp=="0")
							{
								dr["col6"]="0";
							}
							else
							{
								dr["col6"]=strtmp.Substring(0,strtmp.IndexOf(".",0));
							}
							dr["col7"]=drr["REP7"].ToString();
							dtdis.Rows.Add(dr);
							break;
					}
				}
				#endregion

				#region 本店会员在他店充值和消费情况
				strtmp="";
				dr=dtdis.NewRow();
				dr["type"]="";
				dr["col1"]="";
				dr["col2"]="";
				dr["col3"]="";
				dr["col4"]="";
				dr["col5"]="";
				dr["col6"]="";
				dr["col7"]="";
				dtdis.Rows.Add(dr);

				dr=dtdis.NewRow();
				dr["type"]="本店会员在他店";
				dr["col1"]="";
				dr["col2"]="";
				dr["col3"]="";
				dr["col4"]="";
				dr["col5"]="";
				dr["col6"]="";
				dr["col7"]="";
				dtdis.Rows.Add(dr);

				foreach (DataRow drr in dsIn.Tables["LocalToOtherIncome"].Rows)
				{
					switch(drr["Type"].ToString())
					{
						case "LocalToOtherFillFee":
							dr=dtdis.NewRow();
							dr["type"]="充值";
							dr["col1"]=drr["REP1"].ToString();
							dr["col2"]="......";
							dr["col3"]="......";
							dr["col4"]=drr["REP4"].ToString();
							dr["col5"]=drr["REP5"].ToString();
							strtmp=drr["REP6"].ToString();
							if(strtmp=="0")
							{
								dr["col6"]="0";
							}
							else
							{
								dr["col6"]=strtmp.Substring(0,strtmp.IndexOf(".",0));
							}
							dr["col7"]="......";
							dtdis.Rows.Add(dr);
							break;
						case "LocalToOtherAssCons":
							dr=dtdis.NewRow();
							dr["type"]="普通会员消费";
							dr["col1"]=drr["REP1"].ToString();
							dr["col2"]="......";
							dr["col3"]="......";
							dr["col4"]=drr["REP4"].ToString();
							strtmp=drr["REP5"].ToString();
							if(strtmp=="0")
							{
								dr["col5"]="0";
							}
							else
							{
								dr["col5"]=strtmp.Substring(0,strtmp.IndexOf(".",0));
							}
							strtmp=drr["REP6"].ToString();
							if(strtmp=="0")
							{
								dr["col6"]="0";
							}
							else
							{
								dr["col6"]=strtmp.Substring(0,strtmp.IndexOf(".",0));
							}
							dr["col7"]=drr["REP7"].ToString();
							dtdis.Rows.Add(dr);
							break;
						case "LocalToOtherPromCons":
							dr=dtdis.NewRow();
							dr["type"]="赠卡会员消费";
							dr["col1"]=drr["REP1"].ToString();
							dr["col2"]="......";
							dr["col3"]="......";
							dr["col4"]=drr["REP4"].ToString();
							strtmp=drr["REP5"].ToString();
							if(strtmp=="0")
							{
								dr["col5"]="0";
							}
							else
							{
								dr["col5"]=strtmp.Substring(0,strtmp.IndexOf(".",0));
							}
							strtmp=drr["REP6"].ToString();
							if(strtmp=="0")
							{
								dr["col6"]="0";
							}
							else
							{
								dr["col6"]=strtmp.Substring(0,strtmp.IndexOf(".",0));
							}
							dr["col7"]=drr["REP7"].ToString();
							dtdis.Rows.Add(dr);
							break;
						case "LocalToOtherLarg":
							dr=dtdis.NewRow();
							dr["type"]="会员赠送";
							dr["col1"]="......";
							dr["col2"]="......";
							dr["col3"]="......";
							dr["col4"]="......";
							dr["col5"]="......";
							strtmp=drr["REP6"].ToString();
							if(strtmp=="0")
							{
								dr["col6"]="0";
							}
							else
							{
								dr["col6"]=strtmp.Substring(0,strtmp.IndexOf(".",0));
							}
							dr["col7"]=drr["REP7"].ToString();
							dtdis.Rows.Add(dr);
							break;
						case "LocalToOtherIgChange":
							dr=dtdis.NewRow();
							dr["type"]="积分兑换";
							dr["col1"]=drr["REP1"].ToString();
							dr["col2"]="......";
							strtmp=drr["REP3"].ToString();
							if(strtmp=="0")
							{
								dr["col3"]="0";
							}
							else
							{
								dr["col3"]=strtmp.Substring(0,strtmp.IndexOf(".",0));
							}
							dr["col4"]="......";
							dr["col5"]="......";
							strtmp=drr["REP6"].ToString();
							if(strtmp=="0")
							{
								dr["col6"]="0";
							}
							else
							{
								dr["col6"]=strtmp.Substring(0,strtmp.IndexOf(".",0));
							}
							dr["col7"]=drr["REP7"].ToString();
							dtdis.Rows.Add(dr);
							break;
					}
				}
				#endregion
			}

			if(strname=="全部")
			{
				dtdis.Columns["type"].ColumnName="所有门店";
			}
			else
			{
				dtdis.Columns["type"].ColumnName=strname;
			}			
			dtdis.Columns["col1"].ColumnName="会员数";
			dtdis.Columns["col2"].ColumnName="可用积分";
			dtdis.Columns["col3"].ColumnName="使用积分";
			dtdis.Columns["col4"].ColumnName="金额";
			dtdis.Columns["col5"].ColumnName="附赠情况";
			dtdis.Columns["col6"].ColumnName="次数";
			dtdis.Columns["col7"].ColumnName="商品数";

			return dtdis;
		}

		public DataTable GetTopQuery(Hashtable htpara,string strtype)
		{
			DataTable dtout=Qa.GetTopQuery(htpara,strtype);
			if(dtout!=null)
			{
				if(strtype=="0")
				{
					dtout.Columns["vcGoodsName"].ColumnName="商品名称";
					dtout.Columns["salecount"].ColumnName="销售数量";
				}
				else
				{
					dtout.Columns["vcCardID"].ColumnName="会员卡号";
					dtout.Columns["vcAssName"].ColumnName="会员名称";
					dtout.Columns["salefee"].ColumnName="消费额";
				}
			}
			return dtout;
		}

		public DataTable GetAssInfo(Hashtable htpara)
		{
			DataTable dtout=Qa.GetAssInfo(htpara);
			if(dtout!=null)
			{
				dtout.Columns["vcCardID"].ColumnName="会员卡号";
				dtout.Columns["vcAssName"].ColumnName="会员姓名";
				dtout.Columns["vcLinkPhone"].ColumnName="联系电话";
				dtout.Columns["vcAssState"].ColumnName="会员状态";
				dtout.Columns["nCharge"].ColumnName="当前余额";
				dtout.Columns["vcDeptID"].ColumnName="门店";
				dtout.Columns["dtCreateDate"].ColumnName="创建日期";
				dtout.Columns["vcLinkAddress"].ColumnName="联系地址";
				dtout.Columns["dtOperDate"].ColumnName="操作日期";
                dtout.Columns["vcComments"].ColumnName = "备注";
			}
			return dtout;
		}
        public DataTable GetAssInfo(string strCardId,string strAssState)
        {
            DataTable dtout = Qa.GetAssInfo(strCardId,strAssState);
            return dtout;
        }
		public DataTable GetConsOperList(string strDeptID,string strbegin,string strend)
		{
			DataTable dtout=Qa.GetConsOperList(strDeptID, strbegin, strend);
			return dtout;
		}

		public DataTable GetUpDownQuery(Hashtable htpara)
		{
			DataTable dtout=Qa.GetUpDownQuery(htpara);
			if(dtout!=null)
			{
				dtout.Columns["Dept"].ColumnName="数据来源";
				dtout.Columns["vcFileName"].ColumnName="文件名称";
				dtout.Columns["FileSize"].ColumnName="文件大小";
				dtout.Columns["dtStartDate"].ColumnName="开始处理时间";
				dtout.Columns["dtFinDate"].ColumnName="处理完成时间";
				dtout.Columns["Type"].ColumnName="上下传类型";
			}
			return dtout;
		}

		
		public DataTable GetDailyCashQuery(Hashtable htpara)
		{
			DataTable dtDaily=new DataTable();
			DataTable dtout=Qa.GetDailyCashQuery(htpara);
			if(dtout.Rows.Count>0)
			{
				dtDaily.Columns.Add("统计项");
				string stroper=dtout.Rows[0]["vcOperName"].ToString();
				dtDaily.Columns.Add(stroper);
				for(int i=0;i<dtout.Rows.Count;i++)
				{
					if(stroper!=dtout.Rows[i]["vcOperName"].ToString())
					{
						stroper=dtout.Rows[i]["vcOperName"].ToString();
						dtDaily.Columns.Add(stroper);
					}
				}
				DataRow drFillCount=dtDaily.NewRow();
				drFillCount["统计项"]="现金充值次数";
				DataRow drFillFee=dtDaily.NewRow();
				drFillFee["统计项"]="现金充值金额";
				DataRow drBankFillCount=dtDaily.NewRow();
				drBankFillCount["统计项"]="银行卡充值次数";
				DataRow drBankFillFee=dtDaily.NewRow();
				drBankFillFee["统计项"]="银行卡充值金额";
				DataRow drConsCount=dtDaily.NewRow();
				drConsCount["统计项"]="消费次数";
				DataRow drRetailCons=dtDaily.NewRow();
				drRetailCons["统计项"]="现金零售金额";
				DataRow drBankRetailCons=dtDaily.NewRow();
				drBankRetailCons["统计项"]="银行卡零售金额";
				DataRow drAssCons=dtDaily.NewRow();
				drAssCons["统计项"]="会员消费金额";
				DataRow drReCycleCount=dtDaily.NewRow();
				drReCycleCount["统计项"]="回收卡数";
				DataRow drReCycleFee=dtDaily.NewRow();
				drReCycleFee["统计项"]="回收退款金额";
				DataRow drLargCount=dtDaily.NewRow();
				drLargCount["统计项"]="赠送次数";
				DataRow drCashFee=dtDaily.NewRow();
				drCashFee["统计项"]="现金总额";
				for(int k=1;k<dtDaily.Columns.Count;k++)
				{
					drFillCount[k]=0;
					drFillFee[k]=0;
					drBankFillCount[k]=0;
					drBankFillFee[k]=0;
					drConsCount[k]=0;
					drRetailCons[k]=0;
					drBankRetailCons[k]=0;
					drAssCons[k]=0;
					drReCycleCount[k]=0;
					drReCycleFee[k]=0;
					drLargCount[k]=0;
					drCashFee[k]=0;
				}
				for(int i=0;i<dtout.Rows.Count;i++)
				{
					switch(dtout.Rows[i]["vcConsType"].ToString())
					{
						case "PT001":
							drConsCount[dtout.Rows[i]["vcOperName"].ToString()]=int.Parse(drConsCount[dtout.Rows[i]["vcOperName"].ToString()].ToString())+int.Parse(dtout.Rows[i]["ConsCount"].ToString());
							drAssCons[dtout.Rows[i]["vcOperName"].ToString()]=dtout.Rows[i]["ConsFee"].ToString();
							break;
						case "PT002":
							drConsCount[dtout.Rows[i]["vcOperName"].ToString()]=int.Parse(drConsCount[dtout.Rows[i]["vcOperName"].ToString()].ToString())+int.Parse(dtout.Rows[i]["ConsCount"].ToString());
							drCashFee[dtout.Rows[i]["vcOperName"].ToString()]=double.Parse(drCashFee[dtout.Rows[i]["vcOperName"].ToString()].ToString())+double.Parse(dtout.Rows[i]["ConsFee"].ToString());
							drRetailCons[dtout.Rows[i]["vcOperName"].ToString()]=dtout.Rows[i]["ConsFee"].ToString();
							break;
						case "PT004":
							drLargCount[dtout.Rows[i]["vcOperName"].ToString()]=int.Parse(drLargCount[dtout.Rows[i]["vcOperName"].ToString()].ToString())+int.Parse(dtout.Rows[i]["ConsCount"].ToString());
							break;
						case "PT005":
							drConsCount[dtout.Rows[i]["vcOperName"].ToString()]=int.Parse(drConsCount[dtout.Rows[i]["vcOperName"].ToString()].ToString())+int.Parse(dtout.Rows[i]["ConsCount"].ToString());
							drCashFee[dtout.Rows[i]["vcOperName"].ToString()]=double.Parse(drCashFee[dtout.Rows[i]["vcOperName"].ToString()].ToString())+double.Parse(dtout.Rows[i]["ConsFee"].ToString());
							drBankRetailCons[dtout.Rows[i]["vcOperName"].ToString()]=dtout.Rows[i]["ConsFee"].ToString();
							break;
						case "Fill":
							drFillCount[dtout.Rows[i]["vcOperName"].ToString()]=dtout.Rows[i]["ConsCount"].ToString();
							drFillFee[dtout.Rows[i]["vcOperName"].ToString()]=dtout.Rows[i]["ConsFee"].ToString();
							drCashFee[dtout.Rows[i]["vcOperName"].ToString()]=double.Parse(drCashFee[dtout.Rows[i]["vcOperName"].ToString()].ToString())+double.Parse(dtout.Rows[i]["ConsFee"].ToString());
							break;
						case "FillBank":
							drBankFillCount[dtout.Rows[i]["vcOperName"].ToString()]=dtout.Rows[i]["ConsCount"].ToString();
							drBankFillFee[dtout.Rows[i]["vcOperName"].ToString()]=dtout.Rows[i]["ConsFee"].ToString();
							drCashFee[dtout.Rows[i]["vcOperName"].ToString()]=double.Parse(drCashFee[dtout.Rows[i]["vcOperName"].ToString()].ToString())+double.Parse(dtout.Rows[i]["ConsFee"].ToString());
							break;
						case "CradRoll":
							drReCycleCount[dtout.Rows[i]["vcOperName"].ToString()]=dtout.Rows[i]["ConsCount"].ToString();
							drReCycleFee[dtout.Rows[i]["vcOperName"].ToString()]=dtout.Rows[i]["ConsFee"].ToString();
							drCashFee[dtout.Rows[i]["vcOperName"].ToString()]=double.Parse(drCashFee[dtout.Rows[i]["vcOperName"].ToString()].ToString())+double.Parse(dtout.Rows[i]["ConsFee"].ToString());
							break;
					}
				}

				dtDaily.Rows.Add(drFillCount);
				dtDaily.Rows.Add(drFillFee);
				dtDaily.Rows.Add(drBankFillCount);
				dtDaily.Rows.Add(drBankFillFee);
				dtDaily.Rows.Add(drConsCount);
				dtDaily.Rows.Add(drRetailCons);
				dtDaily.Rows.Add(drBankRetailCons);
				dtDaily.Rows.Add(drAssCons);
				dtDaily.Rows.Add(drReCycleCount);
				dtDaily.Rows.Add(drReCycleFee);
				dtDaily.Rows.Add(drLargCount);
				dtDaily.Rows.Add(drCashFee);
			}
			else
			{
				dtDaily.Columns.Add("无任何记录！");
			}
			return dtDaily;
		}

		public DataTable GetSalesSum(string year,string month,string nextmonth)
		{
			try
			{
				DataTable dt=new DataTable();
				dt=Qa.GetSalesSum(year,month,nextmonth);
				return dt;
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}
		public DataTable GetTimeSales(string strDeptId,string strBeginDate,string strEndDate)
		{
			try
			{
				DataTable dt=new DataTable();
				dt=Qa.GetTimeSales(strDeptId,strBeginDate,strEndDate);
				return dt;
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}

        public DataTable GetSaleRatio(string months,string goodsType)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = Qa.GetSaleRatio(months, goodsType);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet GetKPI(DateTime dtDate, string weather, string exception,bool IsUpdate)
        {
            try
            {
                DataSet ds = Qa.GetKPI(dtDate, weather, exception, IsUpdate);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetKPIOfMonth(string month, string deptId)
        {
            try
            {
                DataTable dt = Qa.GetKPIOfMonth(month, deptId);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool InsertKPI(string month,string deptId,decimal amount)
        {
            int recount = Qa.InsertKPI(month,deptId,amount);
            if (recount <= 0)
            {
                return false;
            }

            return true;
        }
        public bool ExistKPI(string month, string deptId)
        {
            int recount = Qa.ExistKPI(month, deptId);
            if (recount <= 0)
            {
                return false;
            }
            return true;
        }
        public bool UpdateKPI(string month, string deptId, decimal amount)
        {
            int recount = Qa.UpdateKPI(month, deptId, amount);
            if (recount <= 0)
            {
                return false;
            }

            return true;
        }
        public bool DeleteKPI(string month, string deptId)
        {
            int recount = Qa.DeleteKPI(month, deptId);
            if (recount <= 0)
            {
                return false;
            }
            return true;
        }

        public DataTable ConsItemUndoQuery(string strSerial,string strBeginDate,string strEndDate,
            string strOperName, string strDeptId, string strIsUndo, string strFlag)
        {
            DataTable dtout = Qa.ConsItemUndoQuery(strSerial,strBeginDate,strEndDate,strOperName,strDeptId,strIsUndo,strFlag);
            return dtout;
        }
        public bool InsertConsItemUndo(string strSerial, string strOperName, string strDeptId)
        {
            int recount = Qa.InsertConsItemUndo(strSerial, strOperName, strDeptId);
            if (recount <= 0)
            {
                return false;
            }

            return true;
        }
        public bool ExistConsItemUndo(string strSerial, string strDeptId)
        {
            int recount = Qa.ExistConsItemUndo(strSerial, strDeptId);
            if (recount <= 0)
            {
                return false;
            }
            return true;
        }
        public bool DeleteConsItemUndo(string strSerial, string strDeptId)
        {
            int recount = Qa.DeleteConsItemUndo(strSerial, strDeptId);
            if (recount <= 0)
            {
                return false;
            }
            return true;
        }

        public void CheckConsItemUndo(string strSerial, string strDeptId, string strOperId)
        {
            Qa.CheckConsItemUndo(strSerial, strDeptId, strOperId);
        }

        public bool ExistDeptInfo(string strDeptName)
        {
            int recount = Qa.ExistDeptInfo(strDeptName);
            if (recount <= 0)
            {
                return false;
            }
            return true;
        }
        public bool DeleteDeptInfo(string strDeptName)
        {
            int recount = Qa.DeleteDeptInfo(strDeptName);
            if (recount <= 0)
            {
                return false;
            }
            return true;
        }
        public bool UpdateDeptInfo(string strOldDeptName, string strDeptName, string strAddress, string strTel, string strManager,string strManagerPhone, string strAdsl,string strAdslPwd, string strVpn,string strVpnPwd)
        {
            int recount = Qa.UpdateDeptInfo(strOldDeptName, strDeptName, strAddress, strTel, strManager,strManagerPhone, strAdsl,strAdslPwd, strVpn,strVpnPwd);
            if (recount <= 0)
            {
                return false;
            }

            return true;
        }
        public DataTable DeptInfoQuery()
        {
            DataTable dtout = Qa.DeptInfoQuery();
            return dtout;
        }
        public bool InsertDeptInfo(string strDeptName, string strAddress, string strTel, string strManager,string strManagerPhone, string strAdsl,string strAdslPwd, string strVpn,string strVpnPwd)
        {
            int recount = Qa.InsertDeptInfo(strDeptName, strAddress, strTel, strManager,strManagerPhone, strAdsl,strAdslPwd, strVpn,strVpnPwd);
            if (recount <= 0)
            {
                return false;
            }

            return true;
        }
	}
}
