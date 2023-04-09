//============================================================
// author:yangyiliang
//============================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using System.Collections;

namespace MeetingResMagSys.DAL
{
    public static class SqlHelper
    {
        public static readonly string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;

        public static int ExecuteNonQuery(string sql, CommandType type, params SqlParameter[] parameters)
        {
            using (SqlConnection conn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.CommandType = type;
                    if (parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }
                    conn.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        public static object ExecuteScalar(string sql, CommandType type, params SqlParameter[] parameters)
        {
            using (SqlConnection conn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.CommandType = type;
                    if (parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }
                    conn.Open();
                    return cmd.ExecuteScalar();
                }
            }
        }

        public static DataTable ExecuteDataTable(string sql, CommandType type, params SqlParameter[] parameters)
        {
            using (SqlConnection conn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.CommandType = type;
                    if (parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }
                    conn.Open();
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        return dt;
                    }
                }
            }
        }

        public static SqlDataReader ExecuteDataReader(string sql, CommandType type, params SqlParameter[] parameters)
        {
            SqlConnection conn = new SqlConnection(constr);

            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.CommandType = type;
                if (parameters != null)
                {
                    cmd.Parameters.AddRange(parameters);
                }
                conn.Open();
                return cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
        }

        //-----------------------------------------------手动添加的-----------------------------------------------
        public static DataTable GetPagedDataTable(string tableName, string condition, string orderByColumn, bool isASC, int startIndex, int endIndex)
        {
            return ExecuteDataTable("Pr_GetPagedData", CommandType.StoredProcedure,
                          new SqlParameter("@tableName", tableName),
                          new SqlParameter("@condition", condition),
                          new SqlParameter("@orderByColumn", orderByColumn),
                          new SqlParameter("@isASC", isASC),
                          new SqlParameter("@startIndex", startIndex),
                          new SqlParameter("@endIndex", endIndex));
        }
        public static object GetCountNumber(string tableName, string primaryKey, string condition)
        {
            return ExecuteScalar(CommandType.StoredProcedure, "Pr_GetCountNumber",
                          new SqlParameter("@tableName", tableName),
                          new SqlParameter("@primaryKey", primaryKey),
                          new SqlParameter("@condition", condition));
        }
        public static object ExecuteScalar(CommandType cmdType, string cmdText, params SqlParameter[] cmdParms)
        {
            SqlCommand cmd = new SqlCommand();
            using (SqlConnection conn = new SqlConnection(constr))
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms);
                object val = cmd.ExecuteScalar();
                cmd.Parameters.Clear();
                return val;
            }
        }
        private static void PrepareCommand(SqlCommand cmd, SqlConnection conn, SqlTransaction trans, CommandType cmdType, string cmdText, SqlParameter[] cmdParms)
        {
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            if (trans != null)
            {
                cmd.Transaction = trans;
            }
            cmd.CommandType = cmdType;
            if (cmdParms != null)
            {
                foreach (SqlParameter parm in cmdParms)
                {
                    cmd.Parameters.Add(parm);
                }
            }
        }
        public static DataTable Select(string columns, string tableName, string condition, string order)
        {
            SqlConnection Connection = new SqlConnection(constr);
            Connection.Open();
            string sql = "SELECT " + columns + " FROM " + tableName + " WHERE " + condition + " ORDER BY " + order;
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(sql, Connection);//从数据库中查询
            da.Fill(dt);
            Connection.Close();
            return dt;
        }
        public static SqlDataReader ExecuteReader(CommandType cmdType, string cmdText, params SqlParameter[] cmdParms)
        {
            SqlCommand cmd = new SqlCommand();
            SqlConnection conn = new SqlConnection(constr);
            try
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms);
                SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                return rdr;
            }
            catch
            {
                conn.Close();
                throw;
            }
        }
        public static SqlDataReader GetPagedData(string tableName, string condition, string orderByColumn, bool isASC, int startIndex, int endIndex)
        {
            return ExecuteReader(CommandType.StoredProcedure, "Pr_GetPagedData",
                          new SqlParameter("@tableName", tableName),
                          new SqlParameter("@condition", condition),
                          new SqlParameter("@orderByColumn", orderByColumn),
                          new SqlParameter("@isASC", isASC),
                          new SqlParameter("@startIndex", startIndex),
                          new SqlParameter("@endIndex", endIndex));

        }
        public static DataTable GetPagedGroupDataTable(string tableName, string columns, string groupcolumns, string condition, string orderByColumn, bool isASC, int startIndex, int endIndex)
        {
            return ExecuteDataTable("Pr_GetPagedGroupData", CommandType.StoredProcedure,
                          new SqlParameter("@tableName", tableName),
                          new SqlParameter("@columns", columns),
                          new SqlParameter("@groupcolumns", groupcolumns),
                          new SqlParameter("@condition", condition),
                          new SqlParameter("@orderByColumn", orderByColumn),
                          new SqlParameter("@isASC", isASC),
                          new SqlParameter("@startIndex", startIndex),
                          new SqlParameter("@endIndex", endIndex));
        }
        public static DataTable GetGroupData(string tableName, string columns, string groupcolumns, string condition, string orderByColumn, bool isASC)
        {
            return ExecuteDataTable("Pr_GetGroupData", CommandType.StoredProcedure,
                          new SqlParameter("@tableName", tableName),
                          new SqlParameter("@columns", columns),
                          new SqlParameter("@groupcolumns", groupcolumns),
                          new SqlParameter("@condition", condition),
                          new SqlParameter("@orderByColumn", orderByColumn),
                          new SqlParameter("@isASC", isASC));
        }
        public static string DataTableToJsonWithJsonNet(DataTable table)
        {
            string JsonString = string.Empty;
            JsonString = JsonConvert.SerializeObject(table);
            return JsonString;
        }
        public static string ListToJsonWithJsonNet(IList IL)
        {
            string JsonString = string.Empty;
            JsonString = JsonConvert.SerializeObject(IL);
            return JsonString;
        }
    }
}
