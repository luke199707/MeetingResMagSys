using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MeetingResMagSys.DAL;
namespace MeetingResMagSys.Handler
{
    /// <summary>
    /// UserDel 的摘要说明
    /// </summary>
    public class UserDel : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string userId = context.Request["userId"];
            if (AllUserDAL.DeleteByUserId(userId)>0)
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