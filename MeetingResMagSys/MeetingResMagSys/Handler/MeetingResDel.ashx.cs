using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MeetingResMagSys.DAL;

namespace MeetingResMagSys.Handler
{
    /// <summary>
    /// MeetingResDel 的摘要说明
    /// </summary>
    public class MeetingResDel : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string meetingId = context.Request["meetingId"];
            if (MeetingReservationDAL.DeleteByMeetingId(meetingId) > 0)
            {
                //取消会议时清除会议成员表中的相关数据
                MeetingMemberDAL.DeleteByMeetingId(meetingId);
                context.Response.Write("ok");
            }
            else
            {
                context.Response.Write("no");
            }
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