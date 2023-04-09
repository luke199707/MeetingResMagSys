using MeetingResMagSys.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MeetingResMagSys.Handler
{
    /// <summary>
    /// GetXXByXX 的摘要说明
    /// </summary>
    public class GetXXByXX : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string selectField = context.Request["selectField"];
            string tableName = context.Request["tableName"];
            string conditionField = context.Request["conditionField"];
            string conditionValue = context.Request["conditionValue"];
            string sql = string.Format("SELECT {0} FROM {1} WHERE {2}=@Value", selectField,tableName,conditionField);
            SqlParameter[] para = new SqlParameter[]
            {
                new SqlParameter("@Value", conditionValue)
            };
            string data = (string)SqlHelper.ExecuteScalar(CommandType.Text, sql, para);
            context.Response.Write(data);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}