using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MeetingResMagSys.DAL;
using MeetingResMagSys.Model;
namespace MeetingResMagSys.Pages
{
    public partial class MagOrgInfo : System.Web.UI.Page
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
                BindDDL();
                IsBtnVisible(true, false, false);
                Disabletxt();
            }
        }
        protected void BindDDL()
        {
            AllUser loginingUser = (AllUser)Session["loginingUser"];
            ListItem item = new ListItem("请选择", "请选择");
            if (loginingUser != null)
            {
                #region 绑定会议预订时间间隔
                DataDictionary mode = DataDictionaryDAL.GetByName("会议预订时间间隔");
                if (mode == null)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('会议预订时间间隔为空！')", true);
                    return;
                }
                DataTable dt = SqlHelper.Select("name", "DataDictionary", "parentId=" + mode.Id.ToString(), "ID ASC");
                ddlTimeUnit.DataSource = dt;
                ddlTimeUnit.DataTextField = "name";
                ddlTimeUnit.DataValueField = "name";
                ddlTimeUnit.DataBind();
                ddlTimeUnit.SelectedIndex = 0;
                #endregion
            }
        }
        protected void DataLoad()
        {
            AllUser loginingUser = (AllUser)Session["loginingUser"];
            Organization model = OrganizationDAL.GetByOrganizationId(loginingUser.OrganizationId);
            txtOrgId.Text = model.OrganizationId;
            txtName.Text = model.Name;
            txtIntroduction.Text = model.Introduction;
            ddlReseStart.SelectedValue = model.ReseStart;
            ddlReseEnd.SelectedValue = model.ReseEnd;
            ddlTimeUnit.SelectedValue = model.TimeUnit;
            txtRemark.Text = model.Remark;
        }
        protected void IsBtnVisible(bool update, bool save, bool cancel)
        {
            btnUpdate.Visible = update;
            btnSave.Visible = save;
            btnUpdateCancel.Visible = cancel;
        }
        protected void Disabletxt()
        {
            txtOrgId.Attributes.Add("disabled", "");
            txtName.Attributes.Add("disabled", "");
            txtIntroduction.Attributes.Add("disabled", "");
            ddlReseStart.Attributes.Add("disabled", "");
            ddlReseEnd.Attributes.Add("disabled", "");
            ddlTimeUnit.Attributes.Add("disabled", "");
            txtRemark.Attributes.Add("disabled", "");
        }
        protected void Enabledtxt()
        {
            txtName.Attributes.Remove("disabled");
            txtIntroduction.Attributes.Remove("disabled");
            ddlReseStart.Attributes.Remove("disabled");
            ddlReseEnd.Attributes.Remove("disabled");
            ddlTimeUnit.Attributes.Remove("disabled");
            txtRemark.Attributes.Remove("disabled");
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            Enabledtxt();
            IsBtnVisible(false, true, true);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            AllUser loginingUser = (AllUser)Session["loginingUser"];
            Organization ORG = OrganizationDAL.GetByOrganizationId(loginingUser.OrganizationId);
            if ("".Equals(txtName.Text))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "toastr.warning('信息填写不全！');", true);
                return;
            }
            int count = (int)SqlHelper.GetCountNumber("Organization", "id", string.Format("name='{0}' and name<>'{1}'", txtName.Text.Trim(), ORG.Name));
            if (count != 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "toastr.error('组织名称重复');", true);
                return;
            }
            if (DateTime.Parse(ddlReseStart.SelectedValue).CompareTo(DateTime.Parse(ddlReseEnd.SelectedValue)) > 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('预订起始时间不能晚于预订结束时间！')", true);
                //ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "refrash", "<script>Reload();</script>", false);
                return;
            }
            ORG.Name = txtName.Text.Trim();
            ORG.Introduction = txtIntroduction.Text.Trim();
            ORG.ReseStart = ddlReseStart.SelectedValue;
            ORG.ReseEnd = ddlReseEnd.SelectedValue;
            ORG.TimeUnit = ddlTimeUnit.SelectedValue;
            ORG.Remark = txtRemark.Text.Trim();
            if (OrganizationDAL.Update(ORG) != 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "toastr.success('信息更新成功');", true);
                DataLoad();
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
            DataLoad();
            Disabletxt();
            IsBtnVisible(true, false, false);
        }
    }
}