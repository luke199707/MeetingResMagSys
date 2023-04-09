using MeetingResMagSys.DAL;
using MeetingResMagSys.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MeetingResMagSys.Handler
{
    /// <summary>
    /// SignUp 的摘要说明
    /// </summary>
    public class SignUp : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string username = context.Request["username"].Trim();
            string pwd = context.Request["pwd"].Trim();
            string email = context.Request["email"].Trim();
            if (AllUserDAL.GetByName(username) != null)
            {
                context.Response.Write("username_repeat");
                return;
            }
            else if (AllUserDAL.GetByEmail(email) != null)
            {
                context.Response.Write("email_repeat");
                return;
            }
            AllUser user = new AllUser();
            user.UserId = AllUserDAL.CreateUserId();
            user.Name = username;
            user.Pwd = pwd;
            user.Email = email;
            user.DepartmentName = "无";
            user.Role = "新用户";
            user.Available = "可用";
            object o = AllUserDAL.Insert(user);
            if (o != null)
            {
                context.Response.Write("success");
                return;
            }
            else
            {
                context.Response.Write("fail");
                return;
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