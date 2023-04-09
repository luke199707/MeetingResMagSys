using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Timers;
using System.Diagnostics;
using System.Net.Mail;
using System.Net;
using System.Data;
using MeetingResMagSys.DAL;

namespace MeetingResMagSys
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            // 在应用程序启动时运行的代码//定义一个线程
            System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ThreadStart(SetTimer));
            thread.Start();
        }
        /// <summary>
        /// 定义一个定时器，并开启和配置相关属性
        /// </summary>
        protected void SetTimer()
        {
            System.Timers.Timer objTimer = new System.Timers.Timer(1000);//执行任务的周期
            objTimer.Elapsed += new System.Timers.ElapsedEventHandler(objTimer_Elapsed);
            objTimer.Enabled = true;
            objTimer.AutoReset = true;
        }
        /// <summary>
        /// 这个方法内实现你想做的事情。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void objTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            // 得到 hour minute second　如果等于某个值就开始执行某个程序。
            int eventHour = e.SignalTime.Hour;
            int eventMinute = e.SignalTime.Minute;
            int eventSecond = e.SignalTime.Second;
            // 定制时间； 比如 在特定的时候执行某个函数
            int Hour = 8;
            int Minute = 00;
            int Second = 00;
            // 设置　 每秒钟的开始执行一次
            if (eventSecond == Second)
            {
                SendEmail();
                //Debug.WriteLine("每秒钟的开始执行一次！");
            }
            //// 设置　每个小时的３０分钟开始执行
            //if (eventMinute == Minute && eventSecond == Second)
            //{
            //    Debug.WriteLine("每个小时的３０分钟开始执行一次！");
            //}
            //// 设置　每天的08:00:00开始执行程序
            //if (eventHour == Hour && eventMinute == Minute && eventSecond == Second)
            //{
            //    Debug.WriteLine("在每天08:00:00开始执行！");
            //}
        }
        /// <summary>
        /// 具体业务
        /// </summary>
        protected void SendEmail()
        {
            //查询30分钟后召开的会议以及人员名单
            string sql = "select name,title,REPLACE(startTime,'T',' ') as startTime,Email " +
            "from(MeetingReservation as a join MeetingMember as b on a.meetingId = b.meetingId) join AllUser as c on b.userId = c.userId "+
            "where DATEDIFF(DD, GETDATE(), time)= 0 and DATEDIFF(MINUTE, GETDATE(), REPLACE(startTime,'T',' ')) = 30" +
            "and DATEDIFF(MINUTE, GETDATE(), REPLACE(startTime,'T',' '))> 0"; 
            using (DataTable dt = SqlHelper.ExecuteDataTable(sql, CommandType.Text))
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //发送会议提醒到邮箱
                    MailMessage mailmsg = new MailMessage();
                    mailmsg.From = new MailAddress("zjh18296137364@163.com", "会议室预订管理系统");
                    mailmsg.To.Add(new MailAddress(dt.Rows[i][3].ToString(), ""));
                    mailmsg.Subject = "尊敬的用户:"+ dt.Rows[i][0].ToString() + ",您好!";
                    mailmsg.SubjectEncoding = System.Text.Encoding.UTF8;
                    mailmsg.Body = string.Format("会议：{0}，将于半小时后，即{1}开始，请您准时参加!", dt.Rows[i][1].ToString(), dt.Rows[i][2].ToString());
                    mailmsg.BodyEncoding = System.Text.Encoding.UTF8;
                    SmtpClient client = new SmtpClient("smtp.163.com");
                    //client.Port = 587;//部署在服务器上使用，否则无法发送邮件
                    client.Credentials = new NetworkCredential("zjh18296137364@163.com", "IHCNVOHWTLMFYIZT");//第二参数为邮箱smtp服务授权码                
                    client.Send(mailmsg);
                }
            }
        }
        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 只要在使用 Timer，就必须保留对它的引用。对于任何托管对象，如果没有对 Timer 的引用，计时器会被垃圾回收。
        /// 即使 Timer 仍处在活动状态，也会被回收。下面的代码是关键，可解决IIS应用程序池自动回收的问题
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Application_End(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(1000);
            string RequestURL = "Pages/Index.aspx";//这里设置你的web地址，可以随便指向你的任意一个aspx页面甚至不存在的页面，目的是要激发Application_Start
            System.Net.HttpWebRequest __HttpWebRequest = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(RequestURL);
            System.Net.HttpWebResponse __HttpWebResponse = (System.Net.HttpWebResponse)__HttpWebRequest.GetResponse();
            System.IO.Stream __rStream = __HttpWebResponse.GetResponseStream();//得到回写的字节流  
            //当不再需要计时器时，请使用 Dispose 方法释放计时器持有的资源。
            __rStream.Close();
            __rStream.Dispose();
        }
    }
}