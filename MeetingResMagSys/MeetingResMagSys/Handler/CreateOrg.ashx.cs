using MeetingResMagSys.DAL;
using MeetingResMagSys.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace MeetingResMagSys.Handler
{
    /// <summary>
    /// CreateOrg 的摘要说明
    /// </summary>
    public class CreateOrg : IHttpHandler, IRequiresSessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            AllUser loginingUser = (AllUser)context.Session["loginingUser"];
            string name = context.Request["orgName"].Trim();
            string introduction = context.Request["orgIntro"].Trim(); 
            if (OrganizationDAL.GetByName(name) != null)
            {
                context.Response.Write("nr");
                return;
            }
            Organization org = new Organization();
            org.OrganizationId = OrganizationDAL.CreateOrgId();
            org.Name = name;
            org.Introduction = introduction;
            org.ReseStart = "08:00";
            org.ReseEnd = "23:00";
            org.TimeUnit = "30";
            org.Time = DateTime.Now;
            if (OrganizationDAL.Insert(org) != null)
            {
                //处理创建人
                loginingUser.OrganizationId = org.OrganizationId;
                loginingUser.Role = "租户";
                AllUserDAL.Update(loginingUser);
                //将创建人加入租户表
                Tenant tenant = new Tenant();
                tenant.UserId = loginingUser.UserId;
                tenant.OrganizationId = loginingUser.OrganizationId;
                tenant.Remark = loginingUser.Remark;
                TenantDAL.Insert(tenant);
                //向扩展信息表添加默认数据
                MeetingRoomTypeExtInfoDAL.AddDefaultData(org.OrganizationId);
                //向可扩展表的逻辑列表添加默认数据
                MeetingRoomTypeColumnDAL.AddDefaultData(org.OrganizationId);

                context.Response.Write("success");
            }
            else
            {
                context.Response.Write("fail");
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