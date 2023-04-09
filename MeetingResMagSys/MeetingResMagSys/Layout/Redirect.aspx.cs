using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MeetingResMagSys.Layout
{
    public partial class Redirect : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["type"] != null)
            {
                string operation = Request.QueryString["type"].ToString();
                switch (operation)
                {
                    case "reLogin":
                        {
                            divInfo.InnerHtml = "对不起，由于您长时间未操作，需要重新登录本系统！<br />" +
                           "系统3秒后将自动跳到<a href='../Login.aspx' target='_parent'>会议室预订管理系统</a>..." +
                           "还剩<span id='time' style='font-weight:bold;color:Red;'>3</span>秒！<br />" +
                           "系统如果没有自动跳转，请点击<a href='../Login.aspx' target='_parent'>会议室预订管理系统</a>...";
                            Response.Write("<script>var i = 3;window.onload=function page_cg(){ document.getElementById('time').innerHTML = i;i--;if(i==0){window.parent.location.assign('../Login.aspx');}setTimeout(page_cg,1000);}</script>");
                            break;
                        }
                    case "updatepwd":
                        {
                            divInfo.InnerHtml = "您修改密码成功，需要重新登录本系统！<br />" +
                           "系统3秒后将自动跳到<a href='../Login.aspx' target='_parent'>会议室预订管理系统</a>..." +
                           "还剩<span id='time' style='font-weight:bold;color:Red;'>3</span>秒！<br />" +
                           "系统如果没有自动跳转，请点击<a href='../Login.aspx' target='_parent'>会议室预订管理系统</a>...";
                            Response.Write("<script>var i = 3;window.onload=function page_cg(){ document.getElementById('time').innerHTML = i;i--;if(i==0){window.parent.location.assign('../Login.aspx');}setTimeout(page_cg,1000);}</script>");
                            break;
                        }
                    case "enter":
                        {
                            Response.Write("<script>window.parent.location.href='Default.html';</script>");
                            break;
                        }
                    default:
                        {
                            Response.Write("<script>window.parent.location.href='../Login.aspx';</script>");
                            break;
                        }
                }
            }
        }
    }
}