using MeetingResMagSys.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using MeetingResMagSys.Model;

namespace MeetingResMagSys.Handler
{
    /// <summary>
    /// RoomFacilityDel 的摘要说明
    /// </summary>
    public class RoomFacilityDel : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string facilityId = context.Request["facilityId"];
            AllUser loginingUser = (AllUser)context.Session["loginingUser"];
            if ((int)SqlHelper.GetCountNumber("MeetingRoomFacility", "id", string.Format("facilityId='{0}'", facilityId)) != 0)
            {
                context.Response.Write("MeetingRoomFacility_notnull");
                return;
            }
            if (RoomFacilityDAL.DeleteByFacilityId(facilityId) > 0)
            {
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
                return true;
            }
        }
    }
}