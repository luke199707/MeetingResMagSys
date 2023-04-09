using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MeetingResMagSys.DAL;
using MeetingResMagSys.Model;

namespace MeetingResMagSys.Pages
{
    public partial class MagPersonalData : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["loginingUser"] == null)
            {
                Response.Redirect("../Layout/Redirect.aspx?type=reLogin");
                return;
            }
            if (!IsPostBack)
            {
                DataLoad();
                IsBtnVisible(true, false, false);
                Disabletxt();
            }
        }
        protected void DataLoad()
        {
            AllUser loginingUser = (AllUser)Session["loginingUser"];
            txtUserId.Text = loginingUser.UserId;
            txtName.Text = loginingUser.Name;
            txtOrgId.Text = loginingUser.OrganizationId;
            txtOrgName.Text = OrganizationDAL.GetNameByORGId(loginingUser.OrganizationId);
            if (DepartmentDAL.GetByDepartmentId(loginingUser.DepartmentName)==null)
            {
                txtDepartment.Text = "无";
            }
            else
            {
                txtDepartment.Text = DepartmentDAL.GetByDepartmentId(loginingUser.DepartmentName).Name;
            }
            txtEmail.Text = loginingUser.Email;
            txtPhone.Text = loginingUser.Phone;
            txtRole.Text = loginingUser.Role;
            txtRemark.Text = loginingUser.Remark;
        }
        protected void showInfo()
        {
            AllUser user = (AllUser)Session["loginingUser"];
            AllUser loginingUser = AllUserDAL.GetById(user.Id);
            txtUserId.Text = loginingUser.UserId;
            txtName.Text = loginingUser.Name;
            txtOrgId.Text = loginingUser.OrganizationId;
            txtOrgName.Text = OrganizationDAL.GetNameByORGId(loginingUser.OrganizationId);
            if (DepartmentDAL.GetByDepartmentId(loginingUser.DepartmentName) == null)
            {
                txtDepartment.Text = "无";
            }
            else
            {
                txtDepartment.Text = DepartmentDAL.GetByDepartmentId(loginingUser.DepartmentName).Name;
            }
            txtEmail.Text = loginingUser.Email;
            txtPhone.Text = loginingUser.Phone;
            txtRole.Text = loginingUser.Role;
            txtRemark.Text = loginingUser.Remark;
        }
        protected void IsBtnVisible(bool update,bool save,bool cancel)
        {
            btnUpdate.Visible = update;
            btnSave.Visible = save;
            btnUpdateCancel.Visible = cancel;
        }
        protected void Disabletxt()
        {
            txtUserId.Attributes.Add("disabled", "");
            txtName.Attributes.Add("disabled", "");
            txtOrgId.Attributes.Add("disabled", "");
            txtOrgName.Attributes.Add("disabled", "");
            txtDepartment.Attributes.Add("disabled", ""); 
            txtEmail.Attributes.Add("disabled", "");
            txtPhone.Attributes.Add("disabled", "");
            txtRole.Attributes.Add("disabled", "");
            txtRemark.Attributes.Add("disabled", "");
        }
        protected void Enabledtxt()
        {
            txtName.Attributes.Remove("disabled");
            txtEmail.Attributes.Remove("disabled");
            txtPhone.Attributes.Remove("disabled");
            txtRemark.Attributes.Remove("disabled");
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            Enabledtxt();
            IsBtnVisible(false,true,true);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            AllUser loginingUser = (AllUser)Session["loginingUser"];
            if ("".Equals(txtName.Text)||"".Equals(txtEmail.Text))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "toastr.warning('信息填写不全！');", true);
                return;
            }
            int count1 = (int)SqlHelper.GetCountNumber("AllUser", "id", string.Format("name<>'{0}' and name = '{1}' and role ='{2}'", loginingUser.Name, txtName.Text.Trim(), "租户"));
            int count2 = (int)SqlHelper.GetCountNumber("AllUser", "id", string.Format("name='{0}' and name<>'{1}' and organizationId='{2}'", txtName.Text.Trim(), loginingUser.Name, loginingUser.OrganizationId));
            if (count1 != 0|| count2!=0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "toastr.error('名称重复');", true);
                return;
            }
            AllUser user = AllUserDAL.GetByUserId(txtUserId.Text);
            user.Name = txtName.Text.Trim();
            user.Email = txtEmail.Text.Trim();
            user.Phone = txtPhone.Text.Trim();
            user.Remark = txtRemark.Text.Trim();
            if (AllUserDAL.Update(user)!=0)
            {
                //更新会话里的用户信息
                Session["loginingUser"] = user;
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "toastr.success('信息更新成功');", true);
                showInfo();
                Disabletxt();
                IsBtnVisible(true, false, false);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "toastr.error('信息更新失败');", true);
                return;
            }
        }

        protected void btnUpdateCancel_Click(object sender, EventArgs e)
        {
            showInfo();
            Disabletxt();
            IsBtnVisible(true, false, false);
        }
    }
}