using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MeetingResMagSys.Model;
using MeetingResMagSys.DAL;

namespace MeetingResMagSys.Pages
{
    public partial class MagPwd : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["loginingUser"] == null)
            {
                Response.Redirect("../Layout/Redirect.aspx?type=reLogin");
                return;
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            AllUser user = (AllUser)Session["loginingUser"];
            if ("".Equals(txtOldPwd.Text))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "toastr.warning('原密码不能为空'); ", true);
                return;
            }
            else if ("".Equals(txtNewPwd.Text))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "toastr.warning('新密码不能为空'); ", true);
                return;
            }
            else if ("".Equals(txtConfirmPwd.Text))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "toastr.warning('确认新密码不能为空'); ", true);
                return;
            }
            else if (!txtNewPwd.Text.Equals(txtConfirmPwd.Text))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "toastr.error('两次密码输入不一致'); ", true);
                return;
            }
            else if (user.Pwd != txtOldPwd.Text.Trim())
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "toastr.error('原密码错误'); ", true);
                return;
            }
            user.Pwd = txtNewPwd.Text;
            if (AllUserDAL.Update(user)>0)
            {
                Response.Redirect("../Layout/Redirect.aspx?type=updatepwd");
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "toastr.error('密码更新失败'); ", true);
            }
            

        }
    }
}