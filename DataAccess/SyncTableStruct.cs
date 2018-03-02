using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Reflection;
using System.IO;

namespace DataAccess
{
    public class SyncTableStruct
    {
        public static void Update(SqlConnection conn)
        {
            string[] scriptStructFileNames = {"tbGoodsDeptPrice.sql",
                                                 "DelSerial.sql",
                                                 "tbMaterial.sql",
                                                 "tbMaterialDel.sql",
                                                 "tbFormulaDel.sql",
                                                 "tbDosageDel.sql",
                                                 "tbOperStandardDel.sql",
                                                 "tbConsItemUndo.sql",
                                                 "tbGoods.sql",
                                                 "tbDeptInfo.sql",
                                                 "tbCommCode.sql"
                                             };
            UpdateDatabase(conn, scriptStructFileNames);
        }
        private static void UpdateDatabase(SqlConnection conn, string[] scriptStructFileNames)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            List<string> lScript = new List<string>();
            foreach (string scriptFileName in scriptStructFileNames)
            {
                string script = GetSql(assembly, scriptFileName);

                if (!string.IsNullOrEmpty(script))
                {
                    lScript.Add(script);
                }
            }
            if (lScript.Count > 0)
            {
                foreach (string script in lScript)
                {
                    using (SqlCommand cmd = new SqlCommand(script, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }
        private static string GetSql(Assembly assembly, string scriptFileName)
        {
            string script = string.Empty;
            Stream stream = assembly.GetManifestResourceStream("DataAccess.SqlServerScript." + scriptFileName);
            StreamReader sr = new StreamReader(stream);
            if (sr != null)
            {
                script = sr.ReadToEnd();
                sr.Close();
                stream.Close();
            }
            return script;
        }
    }
}
