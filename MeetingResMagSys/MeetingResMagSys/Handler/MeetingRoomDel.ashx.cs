using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MeetingResMagSys.DAL;
namespace MeetingResMagSys.Handler
{
    /// <summary>
    /// MeetingRoomDel 的摘要说明
    /// </summary>
    public class MeetingRoomDel : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string roomId = context.Request["roomId"];
            if (MeetingRoomDAL.DeleteByRoomId(roomId) > 0)
            {
                MeetingRoomFacilityDAL.DeleteByRoomId(roomId);//删除会议室的同时 ，清除该会议室设施
                MeetingRoomBanDepDAL.DeleteByRoomId(roomId);//删除会议室的同时 ，清除该会议室禁止预订部门表
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