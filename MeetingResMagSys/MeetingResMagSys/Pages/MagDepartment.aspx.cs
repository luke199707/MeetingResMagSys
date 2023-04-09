using MeetingResMagSys.DAL;
using MeetingResMagSys.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MeetingResMagSys.Pages
{
    public partial class MagDepartment : System.Web.UI.Page
    {
        protected string id = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["loginingUser"] == null)
            {
                Response.Redirect("../Layout/Redirect.aspx?type=reLogin");
                return;
            }
            if (!IsPostBack)
            {
                ddlpagesize.SelectedValue = AspNetPager1.PageSize.ToString();
                gridviewbind();
                BindDDL();
            }
        }
        protected void BindDDL()
        {
            //两个模态框中的下拉框一起绑定
            AllUser loginingUser = (AllUser)Session["loginingUser"];
            ListItem item = new ListItem("请选择", "请选择");
            ListItem item2 = new ListItem("无", "无");
            if (loginingUser != null)
            {
                #region 部门主管绑定
                DataTable dtSupervisor = SqlHelper.Select("userId,name", "AllUser", string.Format("organizationId='{0}'", loginingUser.OrganizationId), "userId ASC");
                //AddModal中的
                ddlSupervisor.DataSource = dtSupervisor;
                ddlSupervisor.DataTextField = "name";
                ddlSupervisor.DataValueField = "userId";
                ddlSupervisor.DataBind();
                ddlSupervisor.Items.Insert(0, item);
                ddlSupervisor.SelectedIndex = 0;
                //EditModal中的
                eddlSupervisor.DataSource = dtSupervisor;
                eddlSupervisor.DataTextField = "name";
                eddlSupervisor.DataValueField = "userId";
                eddlSupervisor.DataBind();
                eddlSupervisor.Items.Insert(0, item);
                eddlSupervisor.SelectedIndex = 0;
                #endregion
                #region 上级部门绑定
                //DataTable dtSuperiorDepartment = SqlHelper.Select("departmentId,name", "Department", string.Format("organizationId='{0}'", loginingUser.OrganizationId), "departmentId ASC");
                ////AddModal中的
                //ddlSuperiorDepartment.DataSource = dtSuperiorDepartment;
                //ddlSuperiorDepartment.DataTextField = "name";
                //ddlSuperiorDepartment.DataValueField = "departmentId";
                //ddlSuperiorDepartment.DataBind();
                //ddlSuperiorDepartment.Items.Insert(0, item2);
                //ddlSuperiorDepartment.SelectedIndex = 0;
                ////EditModal中的
                //eddlSuperiorDepartment.DataSource = dtSuperiorDepartment;
                //eddlSuperiorDepartment.DataTextField = "name";
                //eddlSuperiorDepartment.DataValueField = "departmentId";
                //eddlSuperiorDepartment.DataBind();
                //eddlSuperiorDepartment.Items.Insert(0, item2);
                //eddlSuperiorDepartment.SelectedIndex = 0;
                #endregion
            }
        }
        private void gridviewbind()
        {
            AllUser loginingUser = (AllUser)Session["loginingUser"];
            string sortExpression = gv.Attributes["SortExpression"] == null ? "id" : gv.Attributes["SortExpression"];
            bool isASCDirection = true;
            if (gv.Attributes["SortDirection"] == "DESC")
            {
                isASCDirection = false;
            }
            int totalCounts = (int)SqlHelper.GetCountNumber("Department", "id", string.Format("organizationId='{0}'", loginingUser.OrganizationId));
            if (totalCounts > 0)
            {
                AspNetPager1.RecordCount = totalCounts;
                gv.DataSource = SqlHelper.GetPagedData("Department", string.Format("organizationId='{0}'", loginingUser.OrganizationId), sortExpression, isASCDirection,
                                    AspNetPager1.StartRecordIndex, AspNetPager1.EndRecordIndex);
                gv.DataBind();
            }
            else
            {
                ShowGridViewTitle();
            }

            //显示一条数据的详细信息
            if (id != "")
            {
                ShowDetail(Convert.ToInt32(id));
            }
        }
        /// <summary>
        /// 重载，用于搜索
        /// </summary>
        private void gridviewbind(string DBFiled, String value)
        {
            AllUser loginingUser = (AllUser)Session["loginingUser"];
            string sortExpression = gv.Attributes["SortExpression"] == null ? "id" : gv.Attributes["SortExpression"];
            bool isASCDirection = true;
            if (gv.Attributes["SortDirection"] == "DESC")
            {
                isASCDirection = false;
            }
            int totalCounts = (int)SqlHelper.GetCountNumber("Department", "id", string.Format("organizationId='{0}' and {1} like '%{2}%'", loginingUser.OrganizationId, DBFiled, value));
            if (totalCounts > 0)
            {
                AspNetPager1.RecordCount = totalCounts;
                gv.DataSource = SqlHelper.GetPagedData("Department", string.Format("organizationId='{0}' and {1} like '%{2}%'", loginingUser.OrganizationId, DBFiled, value), sortExpression, isASCDirection,
                                    AspNetPager1.StartRecordIndex, AspNetPager1.EndRecordIndex);
                gv.DataBind();
            }
            else
            {
                ShowGridViewTitle();
            }

            //显示一条数据的详细信息
            if (id != "")
            {
                ShowDetail(Convert.ToInt32(id));
            }
        }
        private void ShowGridViewTitle()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("id");
            dt.Columns.Add("departmentId");
            dt.Columns.Add("name");
            dt.Columns.Add("introduction");
            //dt.Columns.Add("superiorDepartment");
            dt.Columns.Add("supervisor");
            dt.Columns.Add("remark");
            if (dt.Rows.Count == 0)
            {
                DataRow dr = dt.NewRow();
                dt.Rows.Add(dr);
            }
            gv.DataSource = dt;
            gv.DataBind();
            gv.Rows[0].Visible = false;
        }

        private void ShowDetail(int id)
        {
            //数据行的交替显示     gv.Rows.Count是数据行的总行数
            for (int i = 0; i < gv.Rows.Count; i++)
            {
                if (i % 2 == 0)
                {
                    gv.Rows[i].BackColor = gv.RowStyle.BackColor;
                }
                else
                {
                    gv.Rows[i].BackColor = gv.AlternatingRowStyle.BackColor;
                }
                Literal literalId = this.gv.Rows[i].FindControl("LiteralId") as Literal;

                //如果是显示行则背景色为黄色
                if (Convert.ToInt32(literalId.Text) == id)
                {
                    gv.Rows[i].BackColor = System.Drawing.Color.LightYellow;
                }
            }

        }
        protected void AspNetPager1_PageChanged(object sender, EventArgs e)
        {
            gridviewbind();
        }

        protected void GridViewDepart_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "btnDetailCommand")
            {
                ShowDetail(Convert.ToInt32(e.CommandArgument));
            }
        }

        protected void GridViewDepart_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //找到该行的按钮
            Button btnDetail = e.Row.FindControl("btnDetail") as Button;

            if (btnDetail != null)
            {
                //把该行的onclick事件绑定成按钮的点击事件
                e.Row.Attributes["onclick"] = String.Format("javascript:document.getElementById('{0}').click()", btnDetail.ClientID);
                //鼠标样子
                e.Row.Attributes["style"] = "cursor:pointer";
            }
        }

        protected void grid_Sorting(object sender, GridViewSortEventArgs e)
        {
            string sortExpression = e.SortExpression.ToString();
            string sortDirection = "ASC";
            if (sortExpression == gv.Attributes["SortExpression"])
            {
                sortDirection = (gv.Attributes["SortDirection"].ToString() == sortDirection ? "DESC" : "ASC");
            }
            gv.Attributes["SortExpression"] = sortExpression;
            gv.Attributes["SortDirection"] = sortDirection;
            gridviewbind();
        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            AspNetPager1.PageSize = Convert.ToInt32(ddlpagesize.SelectedValue);
            gridviewbind();
        }
        protected void btnAddCertain_Click(object sender, EventArgs e)
        {
            AllUser loginingUser = (AllUser)Session["loginingUser"];
            if ("".Equals(txtName.Text.Trim())||"请选择".Equals(ddlSupervisor.SelectedValue)|| "".Equals(ddlSupervisor.SelectedValue))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('信息填写不全！')", true);
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "refrash", "<script>Reload();</script>", false);
                return;
            }
            if ((int)SqlHelper.GetCountNumber("Department", "id", string.Format("name='{0}' and organizationId='{1}'", txtName.Text.Trim(), loginingUser.OrganizationId)) != 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('部门名称重复！')", true);
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "refrash", "<script>Reload();</script>", false);
                return;
            }
            Department model = new Department();
            model.DepartmentId = DepartmentDAL.CreateDepartmentId();
            model.Name = txtName.Text.Trim();
            model.Introduction = txtIntroduction.Text.Trim();
            //model.SuperiorDepartment = ddlSuperiorDepartment.SelectedValue;
            model.SuperiorDepartment = "无";
            model.Supervisor = ddlSupervisor.SelectedValue;
            model.OrganizationId = loginingUser.OrganizationId;
            model.Time = DateTime.Now;
            model.Remark = txtRemark.Text.Trim();
            if ((int)DepartmentDAL.Insert(model) != 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('部门添加成功！')", true);
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "refrash", "<script>Reload();</script>", false);
                return;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('添加失败！')", true);
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "refrash", "<script>Reload();</script>", false);
                return;
            }
        }

        protected void lbtnEdit_Click(object sender, EventArgs e)
        {
            string departmentId = (sender as LinkButton).CommandArgument;
            Department model = DepartmentDAL.GetByDepartmentId(departmentId);
            hiddenid.Text = model.DepartmentId;
            HiddenName.Value = model.Name;
            etxtName.Text = model.Name;
            etxtIntroduction.Text = model.Introduction;
            //eddlSuperiorDepartment.SelectedValue = model.SuperiorDepartment;
            eddlSupervisor.SelectedValue = model.Supervisor;
            etxtRemark.Text = model.Remark;
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "", "<script>showEditModal();</script>", false);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            AllUser loginingUser = (AllUser)Session["loginingUser"];
            if ("".Equals(etxtName.Text.Trim()) || "请选择".Equals(eddlSupervisor.SelectedValue) || "".Equals(eddlSupervisor.SelectedValue))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('信息填写不全！')", true);
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "refrash", "<script>Reload();</script>", false);
                return;
            }
            if ((int)SqlHelper.GetCountNumber("Department", "id", string.Format("name = '{0}' and organizationId ='{1}' and name<>'{2}'", etxtName.Text.Trim(), loginingUser.OrganizationId, HiddenName.Value)) != 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('部门名称重复！')", true);
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "refrash", "<script>Reload();</script>", false);
                return;
            }
            string departmentId = hiddenid.Text;
            Department model = DepartmentDAL.GetByDepartmentId(departmentId);
            model.Name = etxtName.Text.Trim();
            model.Introduction = etxtIntroduction.Text.Trim();
            //model.SuperiorDepartment = eddlSuperiorDepartment.SelectedValue;
            model.Supervisor = eddlSupervisor.SelectedValue;
            model.Remark = etxtRemark.Text.Trim();
            if (DepartmentDAL.Update(model) > 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('保存成功！')", true);
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "refrash", "<script>Reload();</script>", false);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('保存失败！')", true);
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "refrash", "<script>Reload();</script>", false);
                return;
            }
        }
        protected void lbtnSearchName_Click(object sender, EventArgs e)
        {
            if ("".Equals(txtSearchName.Text))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "toastr.warning('部门名称搜索输入框为空','搜索警告');", true);
                return;
            }
            gridviewbind("name", txtSearchName.Text.Trim());
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "toastr.info('新结果显示');", true);
        }
        protected string GetSuperiorDepartmentName(string departmentId)
        {
            Department model = DepartmentDAL.GetByDepartmentId(departmentId);
            if (model == null)
            {
                return "无";
            }
            return model.Name;
        }
        protected string GetSupervisorName(string userId)
        {
            AllUser model = AllUserDAL.GetByUserId(userId);
            if (model == null)
            {
                return null;
            }
            return model.Name;
        }
    }
}