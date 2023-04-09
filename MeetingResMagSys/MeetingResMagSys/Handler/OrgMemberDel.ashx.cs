using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MeetingResMagSys.DAL;
using MeetingResMagSys.Model;

namespace MeetingResMagSys.Handler
{
    /// <summary>
    /// OrgMemberDel 的摘要说明
    /// </summary>
    public class OrgMemberDel : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string userId = context.Request["userId"];
            //将该成员清除出组织
            AllUser user = AllUserDAL.GetByUserId(userId);
            user.OrganizationId = "";
            user.DepartmentName = "";
            user.Role = "新用户";
            if (AllUserDAL.Update(user) > 0)
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
                return false;
            }
        }
    }
}