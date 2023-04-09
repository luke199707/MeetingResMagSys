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
    /// GetOneEvent 的摘要说明
    /// </summary>
    public class GetOneEvent : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            AllUser loginingUser = (AllUser)context.Session["loginingUser"];
            string meetingId = context.Request["meetingId"];
            string sql = string.Format("select * from MeetingReservation where meetingId='{0}'", meetingId);
            DataTable dt = SqlHelper.ExecuteDataTable(sql, CommandType.Text);
            string events = SqlHelper.DataTableToJsonWithJsonNet(dt);
            context.Response.Write(events);
        }

        public bool IsReusable
        {
            get
            {
                return true;
            }
        }
    }
}