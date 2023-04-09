using MeetingResMagSys.DAL;
using MeetingResMagSys.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace MeetingResMagSys.Handler
{
    /// <summary>
    /// ResetPwd 的摘要说明
    /// </summary>
    public class ResetPwd : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string email = context.Request["email"].Trim();
            AllUser UpdatingUser = AllUserDAL.GetByEmail(email);
            if (UpdatingUser == null)
            {
                context.Response.Write("fail");
                return;
            }
            string str = Guid.NewGuid().ToString().Substring(0, 6);
            //发送新生成的密码到邮箱
            MailMessage mailmsg = new MailMessage();
            mailmsg.From = new MailAddress("zjh18296137364@163.com", "会议室预订管理系统");
            mailmsg.To.Add(new MailAddress(email, ""));
            mailmsg.Subject = "尊敬的用户您好";
            mailmsg.SubjectEncoding = System.Text.Encoding.UTF8;
            mailmsg.Body = DateTime.Now + ",您的密码重置为" + str;
            mailmsg.BodyEncoding = System.Text.Encoding.UTF8;
            SmtpClient client = new SmtpClient("smtp.163.com");
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            //client.Port = 587;//部署在服务器上使用，否则无法发送邮件
            client.Credentials = new NetworkCredential("zjh18296137364@163.com", "IHCNVOHWTLMFYIZT");//第二参数为邮箱smtp服务授权码
            
            try
            {
                client.Send(mailmsg);
                //更新密码
                UpdatingUser.Pwd = str;
                if (AllUserDAL.Update(UpdatingUser) > 0)
                {
                    context.Response.Write("success");
                }
            }
            catch (Exception e)
            {
                context.Response.Write("failSend");
                throw e;
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