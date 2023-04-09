using MeetingResMagSys.Model;
using MeetingResMagSys.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Net.Mail;
using System.Net;

namespace MeetingResMagSys
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtLoginUsername.Text.Trim();
            string pwd = txtLoginPwd.Text.Trim();
            AllUser user = AllUserDAL.GetByNamePwd(username, pwd);
            if ("".Equals(username)||"".Equals(pwd))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "toastr.warning('用户名和密码都不能为空','登陆警告');", true);
                return;
            }
            else if(user==null)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "toastr.error('用户名或密码错误','登陆错误');", true);
                return;
            }
            else if (user.Available!="可用")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "toastr.warning('该账户被禁用','登陆警告');", true);
                return;
            }
            else if ("新用户".Equals(user.Role))
            {
                Session["loginingUser"] = user;
                Response.Redirect("Pages/CreateOrJoinOrg.aspx");
            }
            else
            {
                Session["loginingUser"] = user;
                Response.Redirect("Layout/Default.html");
            }
        }
    }
}
