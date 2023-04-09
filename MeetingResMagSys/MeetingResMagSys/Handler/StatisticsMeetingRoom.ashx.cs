using MeetingResMagSys.DAL;
using MeetingResMagSys.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace MeetingResMagSys.Handler
{
    /// <summary>
    /// StatisticsMeetingRoom 的摘要说明
    /// </summary>
    public class StatisticsMeetingRoom : IHttpHandler, IRequiresSessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            AllUser loginingUser = (AllUser)context.Session["loginingUser"];
            if (loginingUser != null)
            {
                //按操作返回
                string operation = context.Request["operation"];
                switch (operation)
                {
                    case "getRoom":
                        {
                            //会议室名称列表获取
                            string sql1 = string.Format("select name from MeetingRoom where organizationId='{0}' order by name", loginingUser.OrganizationId);
                            SqlDataReader reader1 = SqlHelper.ExecuteDataReader(sql1, CommandType.Text);
                            List<string> lsRoomName = new List<string>();
                            while (reader1.Read())
                            {
                                lsRoomName.Add(reader1[0].ToString());
                            }
                            string roomName = SqlHelper.ListToJsonWithJsonNet(lsRoomName);
                            context.Response.Write(roomName);
                            break;
                        }
                    case "getMouthCount":
                        {
                            //会议室本月预订次数获取
                            string sql1 = string.Format("select name from MeetingRoom where organizationId='{0}' order by name", loginingUser.OrganizationId);
                            SqlDataReader reader1 = SqlHelper.ExecuteDataReader(sql1, CommandType.Text);
                            List<string> lsRoomName = new List<string>();
                            while (reader1.Read())
                            {
                                lsRoomName.Add(reader1[0].ToString());
                            }
                            List<string> lsCount = new List<string>();
                            for (int i = 0; i < lsRoomName.Count; i++)
                            {
                                string sum = MeetingReservationDAL.getMouthMeetingCountByRoom(lsRoomName[i], loginingUser.OrganizationId);
                                lsCount.Add(sum);
                            }
                            string count = SqlHelper.ListToJsonWithJsonNet(lsCount);
                            context.Response.Write(count);
                            break;
                        }
                    case "getYearCount":
                        {
                            //会议室本年度预订次数获取
                            string sql1 = string.Format("select name from MeetingRoom where organizationId='{0}' order by name", loginingUser.OrganizationId);
                            SqlDataReader reader1 = SqlHelper.ExecuteDataReader(sql1, CommandType.Text);
                            List<string> lsRoomName = new List<string>();
                            while (reader1.Read())
                            {
                                lsRoomName.Add(reader1[0].ToString());
                            }
                            List<string> lsCount = new List<string>();
                            for (int i = 0; i < lsRoomName.Count; i++)
                            {
                                string sum = MeetingReservationDAL.getYearMeetingCountByRoom(lsRoomName[i], loginingUser.OrganizationId);
                                lsCount.Add(sum);
                            }
                            string count = SqlHelper.ListToJsonWithJsonNet(lsCount);
                            context.Response.Write(count);
                            break;
                        }
                    case "getTotalCount":
                        {
                            //会议室总预订次数获取
                            string sql1 = string.Format("select name from MeetingRoom where organizationId='{0}' order by name", loginingUser.OrganizationId);
                            SqlDataReader reader1 = SqlHelper.ExecuteDataReader(sql1, CommandType.Text);
                            List<string> lsRoomName = new List<string>();
                            while (reader1.Read())
                            {
                                lsRoomName.Add(reader1[0].ToString());
                            }
                            List<string> lsCount = new List<string>();
                            for (int i = 0; i < lsRoomName.Count; i++)
                            {
                                string sum = MeetingReservationDAL.getTotalMeetingCountByRoom(lsRoomName[i], loginingUser.OrganizationId);
                                lsCount.Add(sum);
                            }
                            string count = SqlHelper.ListToJsonWithJsonNet(lsCount);
                            context.Response.Write(count);
                            break;
                        }
                    default:
                        {
                            context.Response.Write("fail");
                            break;
                        }
                }
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