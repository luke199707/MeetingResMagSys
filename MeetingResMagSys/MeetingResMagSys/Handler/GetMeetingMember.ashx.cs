using MeetingResMagSys.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace MeetingResMagSys.Handler
{
    /// <summary>
    /// GetMeetingMember 的摘要说明
    /// </summary>
    public class GetMeetingMember : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string meetingId = context.Request["meetingId"];
            string sql = string.Format("select userId from MeetingMember where meetingId='{0}'", meetingId);
            DataTable dt = SqlHelper.ExecuteDataTable(sql, CommandType.Text);
            string userIds = SqlHelper.DataTableToJsonWithJsonNet(dt);
            context.Response.Write(userIds);
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