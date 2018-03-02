using System;
using System.Data;
using System.Data.SqlClient;
using AMSApp.zhenghua.DataAccess;
using AMSApp.zhenghua.Common;
using AMSApp.zhenghua.QueryArgs;
using AMSApp.zhenghua.Entity;

namespace AMSApp.zhenghua.Business
{
	/// <summary>
	/// MaterialFacade 的摘要说明。
	/// </summary>
    public class MaterialFacade
    {
        public MaterialFacade()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        //		public DataTable GetMaterials(string strMaterialCode,string strMaterialName)
        //		{
        //			SqlConnection conn = ConnectionPool.BorrowConnection();
        //			DataTable dtRet = null;
        //			try
        //			{
        //				dtRet = MaterialAccess.GetAllMaterial(conn,strMaterialCode,strMaterialName);
        //			}
        //			catch(Exception ex)
        //			{
        //				LogAdapter.WriteFeaturesException(ex);	
        //			}
        //			finally
        //			{
        //				ConnectionPool.ReturnConnection(conn);
        //			}
        //			return dtRet;
        //		}

        public void AddMaterial(Material material, OperLog operLog)//,BusiLog busiLog)
        {
            using (SqlConnection conn = ConnectionPool.BorrowConnection())
            {
                //conn.Open();	

                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    material.IsUse = true;
                    string strSysTime = SqlHelper.ExecuteScalar(trans, CommandType.Text, "select getdate()").ToString();
                    DateTime dtSysTime = DateTime.Parse(strSysTime);

                    operLog.cndOperDate = dtSysTime;
                    operLog.cnvcComments = "原料材料编码：" + material.cnvcMaterialCode;
                    EntityMapping.Create(operLog, trans);

                    EntityMapping.Create(material, trans);
                    //EntityMapping.Create(busiLog, trans);					
                    trans.Commit();
                }
                catch (SqlException sex)
                {
                    trans.Rollback();
                    LogAdapter.WriteDatabaseException(sex);
                    throw sex;
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    LogAdapter.WriteFeaturesException(ex);
                    throw ex;
                }
                finally
                {
                    ConnectionPool.ReturnConnection(conn);
                }
            }
        }

        public void UpdateMaterial(Material mat, string strOldMaterialCode, OperLog operLog)//,BusiLog busiLog)
        {
            using (SqlConnection conn = ConnectionPool.BorrowConnection())
            {
                //conn.Open();	

                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    string strSysTime = SqlHelper.ExecuteScalar(trans, CommandType.Text, "select getdate()").ToString();
                    DateTime dtSysTime = DateTime.Parse(strSysTime);

                    operLog.cndOperDate = dtSysTime;
                    operLog.cnvcComments = "原料材料编码：" + mat.cnvcMaterialCode;
                    EntityMapping.Create(operLog, trans);

                    MaterialAccess.UpdateMaterial(trans, mat, strOldMaterialCode);
                    SqlHelper.ExecuteNonQuery(trans, CommandType.Text, "Update tbProvider set cnvcProductName='" + mat.cnvcMaterialName + "' where cnvcProductCode='" + mat.cnvcMaterialCode + "' and cnvcProductName<>'" + mat.cnvcMaterialName + "'");
                    trans.Commit();
                }
                catch (SqlException sex)
                {
                    trans.Rollback();
                    LogAdapter.WriteDatabaseException(sex);
                    throw sex;
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    LogAdapter.WriteFeaturesException(ex);
                    throw ex;
                }
                finally
                {
                    ConnectionPool.ReturnConnection(conn);
                }
            }
        }
        public void AddFormula(Entity.Formula formula, DataTable dtDosage, DataTable dtPacking, DataTable dtOperStandard, OperLog operLog)
        {
            using (SqlConnection conn = ConnectionPool.BorrowConnection())
            {
                //conn.Open();	

                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    formula.cnvcPortionUnit = "份";
                    EntityMapping.Create(formula, trans);
                    DataTable dtGoods =
                        SqlHelper.ExecuteDataTable(trans, CommandType.Text,
                                                   "select * from tbGoods where vcGoodsID='" + formula.cnvcProductCode + "'");
                    if (dtGoods.Rows.Count == 0 && formula.cnvcProductType == "FINALPRODUCT")
                    {
                        Goods goods = new Goods();
                        goods.vcGoodsID = formula.cnvcProductCode;
                        goods.vcGoodsName = formula.cnvcProductName;
                        goods.vcSpell = Helper.GetChineseSpell(formula.cnvcProductName);
                        goods.iIgValue = -1;
                        goods.vcComments = formula.IsUse ? "1" : "0";
                        EntityMapping.Create(goods, trans);
                    }
                    foreach (DataRow drDosage in dtDosage.Rows)
                    {
                        Dosage dosage = new Dosage(drDosage);
                        EntityMapping.Create(dosage, trans);
                    }
                    foreach (DataRow drPacking in dtPacking.Rows)
                    {
                        Dosage dosage = new Dosage(drPacking);
                        EntityMapping.Create(dosage, trans);
                    }
                    foreach (DataRow drOperStandard in dtOperStandard.Rows)
                    {
                        OperStandard operStandard = new OperStandard(drOperStandard);
                        EntityMapping.Create(operStandard, trans);
                    }

                    string strSysTime = SqlHelper.ExecuteScalar(trans, CommandType.Text, "select getdate()").ToString();
                    DateTime dtSysTime = DateTime.Parse(strSysTime);

                    operLog.cndOperDate = dtSysTime;
                    operLog.cnvcComments = "配方编码：" + formula.cnvcProductCode;
                    EntityMapping.Create(operLog, trans);

                    trans.Commit();
                }
                catch (SqlException sex)
                {
                    trans.Rollback();
                    LogAdapter.WriteDatabaseException(sex);
                    throw sex;
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    LogAdapter.WriteFeaturesException(ex);
                    throw ex;
                }
                finally
                {
                    ConnectionPool.ReturnConnection(conn);
                }
            }
        }
        public void UpdateFormula(Entity.Formula formula, DataTable dtDosage, DataTable dtPacking, DataTable dtOperStandard, OperLog operLog)
        {
            using (SqlConnection conn = ConnectionPool.BorrowConnection())
            {
                //conn.Open();	

                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    Entity.Formula fold = EntityMapping.Get(formula, trans) as Entity.Formula;
                    if (fold == null) throw new Exception("未找到配方");
                    fold.SynchronizeModifyValue(formula);
                    fold.IsUse = formula.IsUse;
                    EntityMapping.Update(fold, trans);

                    DataTable dtGoods =
                        SqlHelper.ExecuteDataTable(trans, CommandType.Text,
                        "select * from tbGoods where vcGoodsID='" + formula.cnvcProductCode + "'");
                    if (dtGoods.Rows.Count == 1 && formula.cnvcProductType == "FINALPRODUCT")
                    {
                        Goods goods = new Goods(dtGoods.Rows[0]);

                        //goods.vcGoodsID = formula.cnvcProductCode;
                        if (goods.vcGoodsName != formula.cnvcProductName)
                        {
                            goods.vcSpell = Helper.GetChineseSpell(formula.cnvcProductName);
                        }
                        goods.vcGoodsName = formula.cnvcProductName;
                        goods.vcComments = formula.IsUse ? "1" : "0";
                        EntityMapping.Update(goods, trans);
                    }

                    DataTable dtHavedDosage = SqlHelper.ExecuteDataTable(trans, CommandType.Text, "select * from tbDosage where cnvcProductCode='" + formula.cnvcProductCode + "'");
                    DataTable dtHavedOperStandard =
                        SqlHelper.ExecuteDataTable(trans, CommandType.Text,
                                                   "select * from tbOperStandard where cnvcProductCode='" + formula.cnvcProductCode + "'");
                    //有的更新，没的添加
                    foreach (DataRow drDosage in dtDosage.Rows)
                    {
                        Dosage newdosage = new Dosage(drDosage);
                        DataRow[] dr = dtHavedDosage.Select("cnvcCode='" + newdosage.cnvcCode + "'");
                        if (dr.Length > 0)
                        {
                            Dosage dosage = new Dosage(dr[0]);
                            dosage.cnnCount = newdosage.cnnCount;
                            dosage.cnnSum = newdosage.cnnSum;
                            EntityMapping.Update(dosage, trans);
                        }
                        else
                        {
                            EntityMapping.Create(newdosage, trans);
                        }

                    }

                    foreach (DataRow drPacking in dtPacking.Rows)
                    {
                        Dosage newdosage = new Dosage(drPacking);
                        DataRow[] dr = dtHavedDosage.Select("cnvcCode='" + newdosage.cnvcCode + "'");
                        if (dr.Length > 0)
                        {
                            Dosage dosage = new Dosage(dr[0]);
                            dosage.cnnCount = newdosage.cnnCount;
                            dosage.cnnSum = newdosage.cnnSum;
                            EntityMapping.Update(dosage, trans);
                        }
                        else
                        {
                            EntityMapping.Create(newdosage, trans);
                        }
                    }
                    foreach (DataRow drDosage in dtHavedDosage.Rows)
                    {
                        Dosage dosage = new Dosage(drDosage);
                        if (dosage.cnvcProductType == "Pack")
                        {
                            DataRow[] dr = dtPacking.Select("cnvcCode='" + dosage.cnvcCode + "'");
                            if (dr.Length == 0)
                                EntityMapping.Delete(dosage, trans);
                        }
                        else
                        {
                            DataRow[] dr = dtDosage.Select("cnvcCode='" + dosage.cnvcCode + "'");
                            if (dr.Length == 0)
                                EntityMapping.Delete(dosage, trans);
                        }

                    }
                    foreach (DataRow drOperStandard in dtOperStandard.Rows)
                    {
                        OperStandard newoperStandard = new OperStandard(drOperStandard);
                        DataRow[] dr = dtHavedOperStandard.Select("cnnSort=" + newoperStandard.cnnSort.ToString());
                        if (dr.Length > 0)
                        {
                            OperStandard operStandard = new OperStandard(dr[0]);
                            operStandard.cnvcKey = newoperStandard.cnvcKey;
                            operStandard.cnvcStandard = newoperStandard.cnvcStandard;
                            EntityMapping.Update(operStandard, trans);
                        }
                        else
                        {
                            EntityMapping.Create(newoperStandard, trans);
                        }

                    }
                    foreach (DataRow drOperStandard in dtHavedOperStandard.Rows)
                    {
                        OperStandard operStandard = new OperStandard(drOperStandard);
                        DataRow[] dr = dtOperStandard.Select("cnnSort=" + operStandard.cnnSort.ToString());
                        if (dr.Length == 0)
                            EntityMapping.Delete(operStandard, trans);
                    }
                    string strSysTime = SqlHelper.ExecuteScalar(trans, CommandType.Text, "select getdate()").ToString();
                    DateTime dtSysTime = DateTime.Parse(strSysTime);

                    operLog.cndOperDate = dtSysTime;
                    operLog.cnvcComments = "配方编码：" + formula.cnvcProductCode;
                    EntityMapping.Create(operLog, trans);

                    trans.Commit();
                }
                catch (SqlException sex)
                {
                    trans.Rollback();
                    LogAdapter.WriteDatabaseException(sex);
                    throw sex;
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    LogAdapter.WriteFeaturesException(ex);
                    throw ex;
                }
                finally
                {
                    ConnectionPool.ReturnConnection(conn);
                }
            }
        }

        public void UpdateCost(OperLog operLog)//,BusiLog busiLog)
        {
            using (SqlConnection conn = ConnectionPool.BorrowConnection())
            {
                //conn.Open();	

                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    //tbMaterial
                    DataTable dtMaterial = SingleTableQuery.ExcuteQuery("tbMaterial", trans);
                    DataTable dtDosage = SingleTableQuery.ExcuteQuery("tbDosage", trans);
                    DataTable dtFormula = SqlHelper.ExecuteDataTable(trans, CommandType.Text, "select cnvcProductType,cnvcProductClass,cnvcProductCode,cnnMaterialCostSum,cnnPackingCostSum,cnnCostSum,cnnPortionCount from tbFormula");
                    //DataRow[] drDosage_Raw_Pack = dtDosage.Select("cnvcProductType<>'SEMIPRODUCT'");
                    //DataRow[] drDosage_Semi = dtDosage.Select("cnvcProductType='SEMIPRODUCT'");
                    //更新原料材料价格
                    foreach (DataRow drDosage in dtDosage.Rows)
                    {
                        Dosage dosage = new Dosage(drDosage);
                        DataRow[] drMaterial = dtMaterial.Select("cnvcMaterialCode='" + dosage.cnvcCode + "'");
                        if (drMaterial.Length > 0)
                        {
                            Material material = new Material(drMaterial[0]);
                            if (dosage.cnnPrice != material.cnnPrice)
                            {
                                dosage.cnnPrice = material.cnnPrice;
                                dosage.cnnSum = Math.Round(dosage.cnnPrice * dosage.cnnCount, 4);
                                EntityMapping.Update(dosage, trans);
                                drDosage["cnnPrice"] = dosage.cnnPrice;
                                drDosage["cnnSum"] = dosage.cnnSum;
                            }
                        }
                    }
                    foreach (DataRow drFormula in dtFormula.Rows)
                    {
                        Entity.Formula formula = new AMSApp.zhenghua.Entity.Formula(drFormula);
                        if (formula.cnvcProductClass == "")
                            continue;
                        UpdatePrice(trans, formula, ref dtDosage, ref dtFormula);
                    }

                    string strSysTime = SqlHelper.ExecuteScalar(trans, CommandType.Text, "select getdate()").ToString();
                    DateTime dtSysTime = DateTime.Parse(strSysTime);

                    operLog.cndOperDate = dtSysTime;
                    EntityMapping.Create(operLog, trans);
                    //EntityMapping.Create(busiLog, trans);					
                    trans.Commit();
                }
                catch (SqlException sex)
                {
                    trans.Rollback();
                    LogAdapter.WriteDatabaseException(sex);
                    throw sex;
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    LogAdapter.WriteFeaturesException(ex);
                    throw ex;
                }
                finally
                {
                    ConnectionPool.ReturnConnection(conn);
                }
            }
        }

        private void UpdatePrice(SqlTransaction trans, Entity.Formula formula, ref DataTable dtDosage, ref DataTable dtFormula)
        {
            DataRow[] drIsMaterial =
                dtDosage.Select("cnvcProductCode='" + formula.cnvcProductCode +
                                "' and cnvcProductType in ('SEMIPRODUCT','FINALPRODUCT')");
            //有半成品

            foreach (DataRow drMaterial in drIsMaterial)
            {
                Dosage dosage = new Dosage(drMaterial);
                DataRow[] drFormula = dtFormula.Select("cnvcProductCode='" + dosage.cnvcCode + "'");
                Entity.Formula formula2 = new AMSApp.zhenghua.Entity.Formula(drFormula[0]);
                UpdatePrice(trans, formula2, ref dtDosage, ref dtFormula);
            }

            //配料全为原材料
            DataRow[] drDosages = dtDosage.Select("cnvcProductCode='" + formula.cnvcProductCode + "'");
            if (drDosages.Length > 0)
            {
                decimal dMaterialCostSum = 0;
                decimal dPackingCostSum = 0;
                foreach (DataRow drDosage in drDosages)
                {
                    Dosage dosage = new Dosage(drDosage);
                    if (dosage.cnvcProductType == "Pack")
                    {
                        dPackingCostSum += dosage.cnnSum;
                    }
                    else
                    {
                        dMaterialCostSum += dosage.cnnSum;
                    }
                    //dSum += dosage.cnnSum;
                }
                if (dMaterialCostSum != formula.cnnMaterialCostSum || dPackingCostSum != formula.cnnPackingCostSum)
                {
                    formula.cnnMaterialCostSum = dMaterialCostSum;
                    formula.cnnPackingCostSum = dPackingCostSum;
                    formula.cnnCostSum = formula.cnnMaterialCostSum + formula.cnnPackingCostSum;
                    SqlHelper.ExecuteNonQuery(trans, CommandType.Text,
                                              "update tbFormula set cnnMaterialCostSum=" + formula.cnnMaterialCostSum +
                                              ",cnnPackingCostSum=" + formula.cnnPackingCostSum + ",cnnCostSum=" +
                                              formula.cnnCostSum + " where cnvcProductCode='" +
                                              formula.cnvcProductCode + "'");
                    DataRow[] drFormula = dtFormula.Select("cnvcProductCode='" + formula.cnvcProductCode + "'");
                    drFormula[0]["cnnMaterialCostSum"] = formula.cnnMaterialCostSum;
                    drFormula[0]["cnnPackingCostSum"] = formula.cnnPackingCostSum;
                    drFormula[0]["cnnCostSum"] = formula.cnnCostSum;
                }
                DataRow[] drDosage_Semi = dtDosage.Select("cnvcCode='" + formula.cnvcProductCode + "'");
                foreach (DataRow dr in drDosage_Semi)
                {
                    Dosage dosage = new Dosage(dr);
                    decimal dPrice = 0;
                    if (formula.cnnPortionCount == 0)
                        dPrice = formula.cnnCostSum;
                    else
                        dPrice = Math.Round(formula.cnnCostSum / formula.cnnPortionCount, 4);
                    if (dosage.cnnPrice != dPrice)
                    {
                        dosage.cnnPrice = dPrice;
                        dosage.cnnSum = Math.Round(dosage.cnnPrice * dosage.cnnCount, 4);
                        EntityMapping.Update(dosage, trans);
                        dr["cnnPrice"] = dPrice;//dosage.cnnPrice;
                        dr["cnnSum"] = dosage.cnnSum;
                    }
                }
            }
        }



        public void UpdateDifference(Material material, OperLog operLog)//,BusiLog busiLog)
        {
            using (SqlConnection conn = ConnectionPool.BorrowConnection())
            {
                //conn.Open();	

                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    string strSysTime = SqlHelper.ExecuteScalar(trans, CommandType.Text, "select getdate()").ToString();
                    DateTime dtSysTime = DateTime.Parse(strSysTime);

                    operLog.cndOperDate = dtSysTime;
                    operLog.cnvcComments = "原料材料编码：" + material.cnvcMaterialCode;
                    EntityMapping.Create(operLog, trans);

                    //EntityMapping.Create(material, trans);
                    //EntityMapping.Create(busiLog, trans);					
                    trans.Commit();
                }
                catch (SqlException sex)
                {
                    trans.Rollback();
                    LogAdapter.WriteDatabaseException(sex);
                    throw sex;
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    LogAdapter.WriteFeaturesException(ex);
                    throw ex;
                }
                finally
                {
                    ConnectionPool.ReturnConnection(conn);
                }
            }
        }

        public void BatchDifference(DataTable dt, OperLog operLog)//,BusiLog busiLog)
        {
            using (SqlConnection conn = ConnectionPool.BorrowConnection())
            {
                //conn.Open();	

                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    //					string strSysTime = SqlHelper.ExecuteScalar(trans, CommandType.Text, "select getdate()").ToString();
                    //					DateTime dtSysTime = DateTime.Parse(strSysTime);
                    //
                    //					operLog.cndOperDate = dtSysTime;
                    //					operLog.cnvcComments = "原料材料编码："+material.cnvcMaterialCode;
                    //					EntityMapping.Create(operLog, trans);		

                    //EntityMapping.Create(material, trans);
                    //EntityMapping.Create(busiLog, trans);			

                    //string strsql = "Get_Dept_Dif '"+strBeginDate+"','"+strEndDate+"'";
                    string strsql = "";
                    foreach (DataRow dr in dt.Rows)
                    {
                        string strDeptID = dr["cnvcDeptID"].ToString();
                        string strOperName = dr["cnvcOperName"].ToString();
                        string strBusinessDate = dr["cndBusinessDate"].ToString();
                        string strType = dr["cnvcType"].ToString();

                        string strPayable_Sum = dr["cnnPayable_Sum"].ToString();
                        string strPayable_Sale = dr["cnnPayable_Sale"].ToString();
                        string strPayable_Retail = dr["cnnPayable_Retail"].ToString();
                        string strPayable_Member = dr["cnnPayable_Member"].ToString();
                        string strPayable_Card = dr["cnnPayable_Card"].ToString();

                        strsql = "update tbDifference set cnnPayable_Sum=" + strPayable_Sum
                            + ",cnnPayable_Sale=" + strPayable_Sale
                            + ",cnnPayable_Retail=" + strPayable_Retail
                            + ",cnnPayable_Member=" + strPayable_Member
                            + ",cnnPayable_Card=" + strPayable_Card
                            + " where cnvcDeptID='" + strDeptID + "' and cnvcOperName='" + strOperName + "' and cndBusinessDate='" + strBusinessDate + "' and cnvcType='" + strType + "'";
                        int icount = SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strsql);
                        if (icount == 0)
                        {
                            strsql = "insert into tbDifference(cnvcDeptID,cnvcOperName,cnnFact_Cash ,cnnFact_Pos,cnnFact_Replace,cnnFact_Check,cnnFact_Sum,"
+ "cnnPayable_Sum ,cnnPayable_Sale,cnnPayable_Retail,cnnPayable_Member,cnnPayable_Card ,"
+ "cnnDif_Sum,cnnDif_More,cnnDif_Add,cnnDif_Dif,"
+ "cndBusinessDate ,cnvcType) values ("
                                + "'" + dr["cnvcDeptID"].ToString() + "'" + ","
                                + "'" + dr["cnvcOperName"].ToString() + "'" + ","
                                + dr["cnnFact_Cash"].ToString() + ","
                                + dr["cnnFact_Pos"].ToString() + ","
                                + dr["cnnFact_Replace"].ToString() + ","
                                + dr["cnnFact_Check"].ToString() + ","
                                + dr["cnnFact_Sum"].ToString() + ","
                                + dr["cnnPayable_Sum"].ToString() + ","
                                + dr["cnnPayable_Sale"].ToString() + ","
                                + dr["cnnPayable_Retail"].ToString() + ","
                                + dr["cnnPayable_Member"].ToString() + ","
                                + dr["cnnPayable_Card"].ToString() + ","
                                + dr["cnnDif_Sum"].ToString() + ","
                                + dr["cnnDif_More"].ToString() + ","
                                + dr["cnnDif_Add"].ToString() + ","
                                + dr["cnnDif_Dif"].ToString() + ","
                                + "'" + dr["cndBusinessDate"].ToString() + "'" + ","
                                + "'" + dr["cnvcType"].ToString() + "')";
                            SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strsql);
                        }
                    }
                    trans.Commit();
                }
                catch (SqlException sex)
                {
                    trans.Rollback();
                    LogAdapter.WriteDatabaseException(sex);
                    throw sex;
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    LogAdapter.WriteFeaturesException(ex);
                    throw ex;
                }
                finally
                {
                    ConnectionPool.ReturnConnection(conn);
                }
            }
        }

        public void DelMaterial(OperLog operLog)
        {
            using (SqlConnection conn = ConnectionPool.BorrowConnection())
            {
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    string sql1 = "INSERT INTO DelSerial (Fill) VALUES ('0');SELECT scope_identity()";
                    SqlCommand cmd = new SqlCommand(sql1, conn, trans);
                    string strSerial = cmd.ExecuteScalar().ToString();

                    string strSysTime = SqlHelper.ExecuteScalar(trans, CommandType.Text, "select getdate()").ToString();
                    DateTime dtSysTime = DateTime.Parse(strSysTime);

                    operLog.cndOperDate = dtSysTime;
                    operLog.cnvcComments = "删除序号：" + strSerial;
                    EntityMapping.Create(operLog, trans);

                    string sql2 = "insert into [tbMaterialDel]([DelId],[cnvcMaterialCode]";
                    sql2 += ",[cnvcMaterialName]";
                    sql2 += ",[cnvcLeastUnit]";
                    sql2 += ",[cnnConversion]";
                    sql2 += ",[cnvcUnit]";
                    sql2 += ",[cnvcStandardUnit]";
                    sql2 += ",[cnnStatdardCount]";
                    sql2 += ",[cnnPrice]";
                    sql2 += ",[cnvcProductType]";
                    sql2 += ",[cnvcProductClass]";
                    sql2 += ",[IsUse])";
                    sql2 += "SELECT " + strSerial + ",[cnvcMaterialCode]";
                    sql2 += ",[cnvcMaterialName]";
                    sql2 += ",[cnvcLeastUnit]";
                    sql2 += ",[cnnConversion]";
                    sql2 += ",[cnvcUnit]";
                    sql2 += ",[cnvcStandardUnit]";
                    sql2 += ",[cnnStatdardCount]";
                    sql2 += ",[cnnPrice]";
                    sql2 += ",[cnvcProductType]";
                    sql2 += ",[cnvcProductClass]";
                    sql2 += ",[IsUse]";
                    sql2 += "FROM [tbMaterial] WHERE IsUse=0";

                    cmd = new SqlCommand(sql2, conn, trans);
                    cmd.ExecuteNonQuery();

                    string sql3 = "DELETE FROM [tbMaterial] WHERE IsUse=0";

                    cmd = new SqlCommand(sql3, conn, trans);
                    cmd.ExecuteNonQuery();
                    trans.Commit();
                }
                catch (SqlException sex)
                {
                    trans.Rollback();
                    LogAdapter.WriteDatabaseException(sex);
                    throw sex;
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    LogAdapter.WriteFeaturesException(ex);
                    throw ex;
                }
                finally
                {
                    ConnectionPool.ReturnConnection(conn);
                }
            }
        }

        public void DelFormula(OperLog operLog)
        {
            using (SqlConnection conn = ConnectionPool.BorrowConnection())
            {
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    string sql1 = "INSERT INTO DelSerial (Fill) VALUES ('1');SELECT scope_identity()";
                    SqlCommand cmd = new SqlCommand(sql1, conn, trans);
                    string strSerial = cmd.ExecuteScalar().ToString();

                    string strSysTime = SqlHelper.ExecuteScalar(trans, CommandType.Text, "select getdate()").ToString();
                    DateTime dtSysTime = DateTime.Parse(strSysTime);

                    operLog.cndOperDate = dtSysTime;
                    operLog.cnvcComments = "删除序号：" + strSerial;
                    EntityMapping.Create(operLog, trans);

                    string sql2 = "insert into tbFormulaDel([DelId],[cnvcProductCode]";
                    sql2 += ",[cnvcProductName]";
                    sql2 += ",[cnvcProductType]";
                    sql2 += ",[cnvcProductClass]";
                    sql2 += ",[cnbProductImage]";
                    sql2 += ",[cnnMaterialCostSum]";
                    sql2 += ",[cnnPackingCostSum]";
                    sql2 += ",[cnnCostSum]";
                    sql2 += ",[cnvcUnit]";
                    sql2 += ",[cnnPortionCount]";
                    sql2 += ",[cnvcPortionUnit]";
                    sql2 += ",[cnvcFeel]";
                    sql2 += ",[cnvcOrganise]";
                    sql2 += ",[cnvcColor]";
                    sql2 += ",[cnvcTaste]";
                    sql2 += ",[IsUse])";
                    sql2 += "SELECT " + strSerial + ",[cnvcProductCode]";
                    sql2 += ",[cnvcProductName]";
                    sql2 += ",[cnvcProductType]";
                    sql2 += ",[cnvcProductClass]";
                    sql2 += ",[cnbProductImage]";
                    sql2 += ",[cnnMaterialCostSum]";
                    sql2 += ",[cnnPackingCostSum]";
                    sql2 += ",[cnnCostSum]";
                    sql2 += ",[cnvcUnit]";
                    sql2 += ",[cnnPortionCount]";
                    sql2 += ",[cnvcPortionUnit]";
                    sql2 += ",[cnvcFeel]";
                    sql2 += ",[cnvcOrganise]";
                    sql2 += ",[cnvcColor]";
                    sql2 += ",[cnvcTaste]";
                    sql2 += ",[IsUse]";
                    sql2 += " FROM [tbFormula] where IsUse=0";
                    cmd = new SqlCommand(sql2, conn, trans);
                    cmd.ExecuteNonQuery();

                    string sql4 = "insert into tbDosageDel([DelId],[cnvcProductCode]";
                    sql4 += ",[cnvcProductType]";
                    sql4 += ",[cnvcCode]";
                    sql4 += ",[cnvcName]";
                    sql4 += ",[cnvcUnit]";
                    sql4 += ",[cnnCount]";
                    sql4 += ",[cnnPrice]";
                    sql4 += ",[cnnSum])";
                    sql4 += "SELECT " + strSerial + ",[cnvcProductCode]";
                    sql4 += ",[cnvcProductType]";
                    sql4 += ",[cnvcCode]";
                    sql4 += ",[cnvcName]";
                    sql4 += ",[cnvcUnit]";
                    sql4 += ",[cnnCount]";
                    sql4 += ",[cnnPrice]";
                    sql4 += ",[cnnSum]";
                    sql4 += " FROM [tbDosage] where [cnvcProductCode] in (select [cnvcProductCode] from tbFormula where IsUse=0)";
                    cmd = new SqlCommand(sql4, conn, trans);
                    cmd.ExecuteNonQuery();

                    string sql5 = "DELETE FROM [tbDosage] where [cnvcProductCode] in (select [cnvcProductCode] from tbFormula where IsUse=0)";
                    cmd = new SqlCommand(sql5, conn, trans);
                    cmd.ExecuteNonQuery();

                    string sql6 = "insert into tbOperStandardDel([DelId],[cnvcProductCode]";
                    sql6 += ",[cnnSort]";
                    sql6 += ",[cnvcStandard]";
                    sql6 += ",[cnvcKey])";
                    sql6 += "SELECT " + strSerial + ",[cnvcProductCode]";
                    sql6 += ",[cnnSort]";
                    sql6 += ",[cnvcStandard]";
                    sql6 += ",[cnvcKey]";
                    sql6 += " FROM [tbOperStandard] where [cnvcProductCode] in (select [cnvcProductCode] from tbFormula where IsUse=0)";
                    cmd = new SqlCommand(sql6, conn, trans);
                    cmd.ExecuteNonQuery();

                    string sql7 = "DELETE FROM [tbOperStandard] where [cnvcProductCode] in (select [cnvcProductCode] from tbFormula where IsUse=0)";
                    cmd = new SqlCommand(sql7, conn, trans);
                    cmd.ExecuteNonQuery();

                    string sql3 = "DELETE FROM [tbFormula] WHERE IsUse=0";
                    cmd = new SqlCommand(sql3, conn, trans);
                    cmd.ExecuteNonQuery();

                    trans.Commit();
                }
                catch (SqlException sex)
                {
                    trans.Rollback();
                    LogAdapter.WriteDatabaseException(sex);
                    throw sex;
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    LogAdapter.WriteFeaturesException(ex);
                    throw ex;
                }
                finally
                {
                    ConnectionPool.ReturnConnection(conn);
                }
            }
        }
    }
}
