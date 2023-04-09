using MeetingResMagSys.DAL;
using MeetingResMagSys.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace MeetingResMagSys.Layout
{
    public partial class Left : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["loginingUser"] == null)
            {
                Response.Write("<script>window.parent.frames.location.href='Redirect.aspx?type=reLogin'</script>");
            }
            else
            {
                if (!IsPostBack)
                {
                    AllUser user = (AllUser)Session["loginingUser"];
                    LoadDefaultUserRight(user.Name,user.OrganizationId);
                }
            }
        }
        /// <summary>
        /// 根据用户名和组织ID加载菜单
        /// </summary>
        /// <param name="name"></param>
        /// <param name="OrganizationId"></param>
        private void LoadDefaultUserRight(string name,string OrganizationId)
        {
            AllUser user = (AllUser)Session["loginingUser"];
            //获取一级菜单
            List<FunctionModel> list = new List<FunctionModel>();
            if (user.Role == "服务提供商"||user.Role=="租户"||user.Role=="普通用户")
            {
                //服务提供商、租户、普通用户都为系统内置角色，不受租户管理
                list = FunctionModelDAL.GetFirstLevel(name);
            }
            else
            {
                list = FunctionModelDAL.GetFirstLevel(name, OrganizationId);
            }
            HtmlGenericControl control = new HtmlGenericControl();
            if (list != null)
            {
                control.InnerHtml += "<ul class='nav nav-list'>";
                control.InnerHtml += "<li class='active'><a href='../Pages/Index.aspx' target='right'><i class='fa fa-dashboard fa-fw'></i><span class='menu-text'> 首页 </span></a></li>";
                for (int i = 0; i < list.Count; i++)
                {
                    control.InnerHtml += string.Format("<li><a href='#' class='dropdown-toggle'><i class='{0}'></i><span  class='menu-text'>{1} </span><b class='arrow fa fa-angle-down fa-fw'></b></a>", list[i].Css, list[i].ModelName);
                    control.InnerHtml += "<ul class='submenu'>";
                    //二级菜单
                    List<RoleRight> secendList = new List<RoleRight>();
                    if (user.Role == "服务提供商" || user.Role == "租户" || user.Role == "普通用户")
                    {
                        secendList = RoleRightDAL.GetChildNode(list[i].CurrentId, user.Role);
                    }
                    else
                    {
                        secendList = RoleRightDAL.GetChildNode(list[i].CurrentId, user.Role, user.OrganizationId);
                    }
                    if (secendList != null)
                    {
                        for (int j = 0; j < secendList.Count; j++)
                        {
                            FunctionModel mo = FunctionModelDAL.GetByCurrentID(secendList[j].RightCode);
                            control.InnerHtml += string.Format("<li><a href='{0}' target='right'>{1}</a></li>", mo.Url, mo.ModelName);
                        }
                    }
                    control.InnerHtml += "</ul>";
                    control.InnerHtml += "</li>";
                }
            }
            control.InnerHtml += "</ul>";
            sidebar.Controls.Add(control);
        }
    }
}