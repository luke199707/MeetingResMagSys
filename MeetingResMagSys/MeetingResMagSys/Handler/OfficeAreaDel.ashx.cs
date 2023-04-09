using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using MeetingResMagSys.DAL;
using MeetingResMagSys.Model;

namespace MeetingResMagSys.Handler
{
    /// <summary>
    /// OfficeAreaDel 的摘要说明
    /// </summary>
    public class OfficeAreaDel : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            AllUser loginingUser = (AllUser)context.Session["loginingUser"];
            string OAid = context.Request["officeAreaId"];
            if ((int)SqlHelper.GetCountNumber("MeetingRoom", "id", string.Format("officeArea='{0}' and organizationId='{1}'", OAid, loginingUser.OrganizationId)) != 0)
            {
                context.Response.Write("MeetingRoom_notnull");
                return;
            }
            if (OfficeAreaDAL.DeleteByOAId(OAid)>0)
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