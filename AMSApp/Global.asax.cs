using System;
using System.Collections;
using System.Data;
using DataAccess;
using CommCenter;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Web.Configuration;
using System.Configuration;
using System.Linq;

namespace AMSApp 
{
	/// <summary>
	/// Summary description for Global.
	/// </summary>
	public class Global : System.Web.HttpApplication
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
        private readonly string SqlVersion = "1.7";
		public Global()
		{
			InitializeComponent();
		}	
		
		protected void Application_Start(Object sender, EventArgs e)
		{
			Hashtable htapp=new Hashtable();
            string strCon = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"];
            if (JudgeSqlUpdate())
            {
                UpdateServer(strCon);
                UpdateSqlVersionConfig();
            }
            htapp.Add("cons", strCon);
			Application["appconf"]=htapp;
			ParaInit(Application);
			AMSApp.zhenghua.Business.Helper.LoadInitCode(Application);
		}
        private void UpdateServer(string strCon)
        {
//            List<string> lScript = new List<string>();
//            string AddTable_tbGoodsDeptPrice = @"IF  not EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbGoodsDeptPrice]') AND type in (N'U'))
//begin
//create table dbo.tbGoodsDeptPrice
//(
//    vcDeptID varchar(10) not null,
//    vcGoodsID varchar(10) not null,    
//    nPrice numeric(8,2) not null,
//    vcComments varchar(50) null,
//    CONSTRAINT [PK_TBGOODSDEPTPRICE] PRIMARY KEY CLUSTERED
//    (
//       vcGoodsID,
//       vcDeptID
//    ) 
//)
//end";
//            lScript.Add(AddTable_tbGoodsDeptPrice);
//            if (lScript.Count > 0)
//            {
                using (SqlConnection conn = new SqlConnection(strCon))
                {
                    if (conn.State != System.Data.ConnectionState.Open)
                        conn.Open();
                    //foreach (string script in lScript)
                    //{
                    //    using (SqlCommand cmd = new SqlCommand(script, conn))
                    //    {
                    //        cmd.ExecuteNonQuery();
                    //    }
                    //}
                    DataAccess.SyncTableStruct.Update(conn);
                }
            //}
        }
        private bool JudgeSqlUpdate()
        {
            bool update = true;
            if (WebConfigurationManager.AppSettings.AllKeys.Contains("SqlVersion"))
            {
                string sqlversion = WebConfigurationManager.AppSettings["SqlVersion"];
                if (sqlversion == SqlVersion)
                {
                    update = false;
                }
            }
            return update;
        }
        private void UpdateSqlVersionConfig()
        {
            System.Configuration.Configuration config =
              WebConfigurationManager.OpenWebConfiguration("~");
            if (config.AppSettings.Settings.AllKeys.Contains("SqlVersion"))
            {
                config.AppSettings.Settings["SqlVersion"].Value = SqlVersion;
            }
            else
            {
                config.AppSettings.Settings.Add("SqlVersion", SqlVersion);
            }
            config.Save(ConfigurationSaveMode.Modified, false);
            ConfigurationManager.RefreshSection("appSettings");
        }
		protected void Session_Start(Object sender, EventArgs e)
		{

		}

		public void ParaInit(System.Web.HttpApplicationState app)
		{
			try
			{
				DataSet dsIn  = new DataSet();

				InitCode inc=new InitCode();
				Hashtable htapp=(Hashtable)Application["appconf"];
				string strcons=(string)htapp["cons"];
				DataSet dsOut = inc.LoadCodeTable(strcons);			
			
				//错误返回表

				//返回结果存放到Application
				app["tbCommCode"] = dsOut.Tables["tbCommCode"];
				app["AllMD"] = dsOut.Tables["AllMD"];
                app["AllREGION"] = dsOut.Tables["AllREGION"];
                app["AllMDP"] = dsOut.Tables["AllMDP"];
				app["MAC"] = dsOut.Tables["MAC"];
				app["Goods"] = dsOut.Tables["Goods"];
				app["PClass"] = dsOut.Tables["PClass"];
				app["AllMaterial"] = dsOut.Tables["AllMaterial"];
				app["Provider"] = dsOut.Tables["Provider"];
				app["NewDept"] = dsOut.Tables["NewDept"];
				app["tbNameCodeToStorage"] = dsOut.Tables["tbNameCodeToStorage"];
				app["tbFormula"] = dsOut.Tables["tbFormula"];
				app["DeptMapInfo"] = dsOut.Tables["DeptMapInfo"];
				app["AcctMonth"] = dsOut.Tables["AcctMonth"];
                app["tbLocalLogin"] = dsOut.Tables["tbLocalLogin"];

                app["tbCommCodeBDEPT"] = dsOut.Tables["tbCommCodeBDEPT"];
				Hashtable htOperFunc=new Hashtable();
				DataTable dttmp=dsOut.Tables["OperFunc"];
				if(dttmp.Rows.Count>0)
				{
					string strOperID="";
					ArrayList alFuncList=null;
					for(int i=0;i<dttmp.Rows.Count;i++)
					{
						CMSMStruct.MenuStruct menu1=new CMSMStruct.MenuStruct();
						menu1.strFuncName=dttmp.Rows[i]["vcFuncName"].ToString();
						menu1.strFuncAddress=dttmp.Rows[i]["vcFuncAddress"].ToString();
						if(strOperID==dttmp.Rows[i]["vcOperID"].ToString())
						{
							alFuncList.Add(menu1);
							if(i==dttmp.Rows.Count-1)
							{
								htOperFunc.Add(strOperID,alFuncList);
							}
						}
						else
						{
							if(strOperID!=""&&alFuncList.Count>0)
							{
								htOperFunc.Add(strOperID,alFuncList);
							}

							alFuncList=new ArrayList();
							alFuncList.Add(menu1);
							strOperID=dttmp.Rows[i]["vcOperID"].ToString();
							if(i==dttmp.Rows.Count-1)
							{
								htOperFunc.Add(strOperID,alFuncList);
							}
						}
					}
				}
				app["OperFunc"]=htOperFunc;

				Hashtable htIOTime=new Hashtable();
				dttmp=null;
				dttmp=dsOut.Tables["IOTime"];
				if(dttmp.Rows.Count>0)
				{
					string strOfficer="";
					ArrayList altmp=null;
					for(int i=0;i<dttmp.Rows.Count;i++)
					{
						CMSMStruct.SignIOTimeStruct sio1=new CommCenter.CMSMStruct.SignIOTimeStruct();
						sio1.strSIOTID=dttmp.Rows[i]["iotName"].ToString();
						sio1.strOfficer=dttmp.Rows[i]["Officer"].ToString();
						sio1.strClassName=dttmp.Rows[i]["vcClassName"].ToString();
						sio1.strClassId=dttmp.Rows[i]["vcClassId"].ToString();
						sio1.strInTime=dttmp.Rows[i]["InTime"].ToString();
						sio1.strOutTime=dttmp.Rows[i]["OutTime"].ToString();
						if(strOfficer==sio1.strOfficer)
						{
							altmp.Add(sio1);
							if(i==dttmp.Rows.Count-1)
							{
								htIOTime.Add(strOfficer,altmp);
							}
						}
						else
						{
							if(strOfficer!=""&&altmp.Count>0)
							{
								htIOTime.Add(strOfficer,altmp);
							}

							altmp=new ArrayList();
							altmp.Add(sio1);
							strOfficer=sio1.strOfficer;
							if(i==dttmp.Rows.Count-1)
							{
								htIOTime.Add(strOfficer,altmp);
							}
						}
					}
				}
				app["IOTime"]=htIOTime;

                
				app.UnLock();
			}
			catch(Exception e)
			{
				AMSLog clog=new AMSLog();
				clog.WriteLine(e);
			}
			
		}

		protected void Application_BeginRequest(Object sender, EventArgs e)
		{

		}

		protected void Application_EndRequest(Object sender, EventArgs e)
		{

		}

		protected void Application_AuthenticateRequest(Object sender, EventArgs e)
		{

		}

		protected void Application_Error(Object sender, EventArgs e)
		{

		}

		protected void Session_End(Object sender, EventArgs e)
		{

		}

		protected void Application_End(Object sender, EventArgs e)
		{

		}
			
		#region Web Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.components = new System.ComponentModel.Container();
		}
		#endregion
	}
}

