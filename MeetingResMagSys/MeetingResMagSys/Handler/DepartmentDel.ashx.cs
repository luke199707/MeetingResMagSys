using MeetingResMagSys.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MeetingResMagSys.Handler
{
    /// <summary>
    /// DepartmentDel 的摘要说明
    /// </summary>
    public class DepartmentDel : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string departmentId = context.Request["departmentId"];
            if ((int)SqlHelper.GetCountNumber("AllUser", "id", string.Format("departmentName='{0}'", departmentId)) != 0)
            {
                context.Response.Write("AllUser_notNull");
                return;
            }
            if ((int)SqlHelper.GetCountNumber("MeetingRoom", "id", string.Format("resDepartment='{0}'", departmentId)) != 0)
            {
                context.Response.Write("MeetingRoom_notNull");
                return;
            }
            if (DepartmentDAL.DeleteByDepartmentIdId(departmentId) > 0)
            {
                MeetingRoomBanDepDAL.DeleteByDepId(departmentId);//清除会议室禁止预订部门表
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