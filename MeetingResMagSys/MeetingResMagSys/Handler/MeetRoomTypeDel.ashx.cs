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
    /// MeetRoomTypeDel 的摘要说明
    /// </summary>
    public class MeetRoomTypeDel : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string RoomTypeId = context.Request["RoomTypeId"];
            AllUser loginingUser = (AllUser)context.Session["loginingUser"];
            if ((int)SqlHelper.GetCountNumber("MeetingRoom", "id", string.Format("type='{0}' and organizationId='{1}'", RoomTypeId, loginingUser.OrganizationId)) != 0)
            {
                context.Response.Write("MeetingRoom_notnull");
                return;
            }
            if (MeetingRoomTypeDAL.DeleteByRoomTypeId(RoomTypeId) > 0)
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