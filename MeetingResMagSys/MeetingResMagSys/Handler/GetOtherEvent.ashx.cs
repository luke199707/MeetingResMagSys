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
    /// GetOtherEvent 的摘要说明
    /// </summary>
    public class GetOtherEvent : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            AllUser loginingUser = (AllUser)context.Session["loginingUser"];
            string room = context.Request["room"];
            string sql = string.Format("select meetingId,title,startTime,endTime from MeetingReservation where booker<>'{0}' and organizationId='{1}' and meetingRoom='{2}' and state='正常' and meetingId not in (select meetingId from MeetingMember where userId='{3}')",
                loginingUser.UserId, loginingUser.OrganizationId,room, loginingUser.UserId);
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