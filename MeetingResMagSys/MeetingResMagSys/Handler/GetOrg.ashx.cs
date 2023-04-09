using MeetingResMagSys.DAL;
using MeetingResMagSys.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.SessionState;
namespace MeetingResMagSys.Handler
{
    /// <summary>
    /// GetOrg 的摘要说明
    /// </summary>
    public class GetOrg : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            AllUser loginingUser = (AllUser)context.Session["loginingUser"];
            string sql = string.Format("select reseStart,reseEnd,timeUnit from Organization where organizationId='{0}'", loginingUser.OrganizationId);
            DataTable dt = SqlHelper.ExecuteDataTable(sql, CommandType.Text);
            string org = SqlHelper.DataTableToJsonWithJsonNet(dt);
            context.Response.Write(org);
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