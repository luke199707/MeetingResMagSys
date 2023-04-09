using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MeetingResMagSys.DAL;
using MeetingResMagSys.Model;
using System.Web.SessionState;

namespace MeetingResMagSys.Handler
{
    /// <summary>
    /// JoinOrg 的摘要说明
    /// </summary>
    public class JoinOrg : IHttpHandler, IRequiresSessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            AllUser loginingUser = (AllUser)context.Session["loginingUser"];
            string id = context.Request["orgId"].Trim();
            string name = context.Request["orgName"].Trim();
            Organization org = OrganizationDAL.GetByIdName(id, name);
            if (org == null)
            {
                context.Response.Write("org_null");
                return;
            }
            loginingUser.OrganizationId = org.OrganizationId;
            loginingUser.Role = "普通用户";
            AllUserDAL.Update(loginingUser);
            context.Response.Write("success");
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