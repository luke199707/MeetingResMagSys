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
    public partial class SetDefaultRoleRight : System.Web.UI.Page
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
            }
        }
        private void gridviewbind()
        {
            EmptyTxt();
            string sortExpression = gvRole.Attributes["SortExpression"] == null ? "roleId" : gvRole.Attributes["SortExpression"];
            bool isASCDirection = true;
            if (gvRole.Attributes["SortDirection"] == "DESC")
            {
                isASCDirection = false;
            }
            int totalCounts = (int)SqlHelper.GetCountNumber("Role", "id", "defaultRole='是'");
            if (totalCounts > 0)
            {
                AspNetPager1.RecordCount = totalCounts;
                gvRole.DataSource = SqlHelper.GetPagedData("Role", "defaultRole='是'", sortExpression, isASCDirection,
                                    AspNetPager1.StartRecordIndex, AspNetPager1.EndRecordIndex);
                gvRole.DataBind();
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
            //按钮的变化
            IsBtnVisible(true, false, false, true, false, false, true);
            Disabletxt();
        }
        private void IsBtnVisible(bool Add, bool Certain, bool AddCancel, bool Update, bool Save, bool UpdateCancel, bool Del)
        {
            btnAdd.Visible = Add;
            btnCertain.Visible = Certain;
            btnAddCancel.Visible = AddCancel;
            btnUpdate.Visible = Update;
            btnSave.Visible = Save;
            btnUpdateCancel.Visible = UpdateCancel;
            btnDel.Visible = Del;
        }
        //txt失效
        private void Disabletxt()
        {
            txtRoleId.Attributes.Add("disabled", "");
            txtRoleName.Attributes.Add("disabled", "");
            txtRemark.Attributes.Add("disabled", "");
        }
        //txt有效
        private void Enabledtxt()
        {
            txtRemark.Attributes.Remove("disabled");
        }
        private void EmptyTxt()
        {
            hiddenid.Text = "";
            txtRoleId.Text = "";
            txtRoleName.Text = "";
            txtRemark.Text = "";
        }
        private void ShowGridViewTitle()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("id");
            dt.Columns.Add("roleId");
            dt.Columns.Add("roleName");
            dt.Columns.Add("remark");

            if (dt.Rows.Count == 0)
            {
                DataRow dr = dt.NewRow();
                dt.Rows.Add(dr);
            }
            gvRole.DataSource = dt;
            gvRole.DataBind();
            gvRole.Rows[0].Visible = false;
        }

        private void ShowDetail(int id)
        {
            //取出数据
            Role model = RoleDAL.GetById(id);
            //赋值
            txtRoleId.Text = model.RoleId;
            txtRoleName.Text = model.RoleName;
            txtRemark.Text = model.Remark;
            //隐藏的角色代码
            hiddenid.Text = model.Id.ToString();

            Disabletxt();
            IsBtnVisible(true, false, false, true, false, false, true);

            //数据行的交替显示     gvRole.Rows.Count是数据行的总行数
            for (int i = 0; i < gvRole.Rows.Count; i++)
            {
                if (i % 2 == 0)
                {
                    gvRole.Rows[i].BackColor = gvRole.RowStyle.BackColor;
                }
                else
                {
                    gvRole.Rows[i].BackColor = gvRole.AlternatingRowStyle.BackColor;
                }
                Literal literalId = this.gvRole.Rows[i].FindControl("LiteralId") as Literal;

                //如果是显示行则背景色为黄色
                if (Convert.ToInt32(literalId.Text) == id)
                {
                    gvRole.Rows[i].BackColor = System.Drawing.Color.LightYellow;
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
            if (sortExpression == gvRole.Attributes["SortExpression"])
            {
                sortDirection = (gvRole.Attributes["SortDirection"].ToString() == sortDirection ? "DESC" : "ASC");
            }
            gvRole.Attributes["SortExpression"] = sortExpression;
            gvRole.Attributes["SortDirection"] = sortDirection;
            gridviewbind();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            //清空所有的信息
            EmptyTxt();
            //改变显示
            //去除选中颜色
            for (int i = 0; i < gvRole.Rows.Count; i++)
            {
                if (i % 2 == 0)
                {
                    gvRole.Rows[i].BackColor = gvRole.RowStyle.BackColor;
                }
                else
                {
                    gvRole.Rows[i].BackColor = gvRole.AlternatingRowStyle.BackColor;
                }
            }
            txtRoleId.Text = RoleDAL.CreateRoleId();
            txtRemark.Text = "内置角色";
            txtRoleName.Attributes.Remove("disabled");
            txtRemark.Attributes.Remove("disabled");
            IsBtnVisible(false, true, true, false, false, false, false);
        }

        protected void btnCertain_Click(object sender, EventArgs e)
        {
            if ("".Equals(txtRoleName.Text))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "toastr.warning('角色名称不能为空');", true);
                return;
            }
            if ((int)SqlHelper.GetCountNumber("Role", "id", string.Format("roleName='{0}' and defaultRole='是'", txtRoleName.Text)) != 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "toastr.warning('角色名称重复');", true);
                return;
            }
            Role model = new Role
            {
                RoleId = txtRoleId.Text.Trim(),
                RoleName = txtRoleName.Text.Trim(),
                DefaultRole = "是",
                Remark = txtRemark.Text
            };
            RoleDAL.Insert(model);
            //重新绑定刷新
            gridviewbind();
        }

        protected void btnAddCancel_Click(object sender, EventArgs e)
        {
            gridviewbind();
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (hiddenid.Text == "")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "toastr.warning('请选择要修改的角色');", true);
                return;
            }
            //改变显示
            Enabledtxt();
            IsBtnVisible(false, false, false, false, true, true, false);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if ("".Equals(txtRoleName.Text))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "toastr.warning('角色名称不能为空');", true);
                return;
            }
            if ((int)SqlHelper.GetCountNumber("Role", "id", string.Format("roleName='{0}'", txtRoleName.Text)) != 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "toastr.warning('角色名称重复');", true);
                return;
            }
            Role model = RoleDAL.GetById(Convert.ToInt32(hiddenid.Text));
            model.RoleName = txtRoleName.Text.Trim();
            model.Remark = txtRemark.Text;
            RoleDAL.Update(model);
            gridviewbind();
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "toastr.success('修改成功');", true);
        }

        protected void btnUpdateCancel_Click(object sender, EventArgs e)
        {
            gridviewbind();
        }

        protected void btnDel_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "toastr.warning('内置角色，不可删除！');", true);
            return;
        }

        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            AspNetPager1.PageSize = Convert.ToInt32(ddlpagesize.SelectedValue);
            gridviewbind();
        }
    }
}