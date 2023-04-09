using MeetingResMagSys.DAL;
using MeetingResMagSys.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MeetingResMagSys.Layout
{
    public partial class Top : System.Web.UI.Page
    {
        public string loginingUser { get; set; }
        public string OrgName { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lbSystemTime.InnerText = DateTime.Now.Year + "年" + DateTime.Now.Month + "月" + DateTime.Now.Day + "日";
                AllUser user = (AllUser)Session["loginingUser"];
                loginingUser = user.Name;
                Organization org = OrganizationDAL.GetByOrganizationId(user.OrganizationId);
                if (user.Role== "服务提供商")
                {
                    OrgName = "面向多租户SaaS的";
                }
                else
                {
                    OrgName = org.Name + "-";
                }
            }
        }
        protected void LbLogout_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Write("<script>parent.location.assign('../Login.aspx');</script>");
        }
    }
}