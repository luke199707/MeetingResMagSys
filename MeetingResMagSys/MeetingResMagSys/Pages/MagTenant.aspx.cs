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
    public partial class MagTenant : System.Web.UI.Page
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
            AllUser loginingUser = (AllUser)Session["loginingUser"];
            string sortExpression = gv.Attributes["SortExpression"] == null ? "userId" : gv.Attributes["SortExpression"];
            bool isASCDirection = true;
            if (gv.Attributes["SortDirection"] == "DESC")
            {
                isASCDirection = false;
            }
            int totalCounts = (int)SqlHelper.GetCountNumber("AllUser", "id", string.Format("role='{0}'", "租户"));
            if (totalCounts > 0)
            {
                AspNetPager1.RecordCount = totalCounts;
                gv.DataSource = SqlHelper.GetPagedData("AllUser", string.Format("role='{0}'", "租户"), sortExpression, isASCDirection,
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
        /// 重载，用于搜索使用
        /// </summary>
        private void gridviewbind(string DBFiled,string value)
        {
            AllUser loginingUser = (AllUser)Session["loginingUser"];
            string sortExpression = gv.Attributes["SortExpression"] == null ? "userId" : gv.Attributes["SortExpression"];
            bool isASCDirection = true;
            if (gv.Attributes["SortDirection"] == "DESC")
            {
                isASCDirection = false;
            }
            int totalCounts = (int)SqlHelper.GetCountNumber("AllUser", "id", string.Format("role='{0}' and {1} like '%{2}%'", "租户", DBFiled, value));
            if (totalCounts > 0)
            {
                AspNetPager1.RecordCount = totalCounts;
                gv.DataSource = SqlHelper.GetPagedData("AllUser", string.Format("role='{0}' and {1} like '%{2}%'", "租户", DBFiled, value), sortExpression, isASCDirection,
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
            dt.Columns.Add("UserId");
            dt.Columns.Add("name");
            dt.Columns.Add("Email");
            dt.Columns.Add("phone");
            dt.Columns.Add("role");
            dt.Columns.Add("organizationId");
            dt.Columns.Add("available");
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
            for (int i = 0; i < gv.Rows.Count; i++)
            {
                if ((gv.Rows[i].FindControl("LiteralAvailable") as Literal).Text == "禁用")
                {
                    (gv.Rows[i].FindControl("LiteralBan") as Literal).Visible = true;
                }
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
        protected void lbtnEdit_Click(object sender, EventArgs e)
        {
            string userId = (sender as LinkButton).CommandArgument;
            AllUser model = AllUserDAL.GetByUserId(userId);
            hiddenid.Text = model.UserId;
            HiddenName.Value = model.Name;
            txtUserId.Text = model.UserId;
            txtName.Text = model.Name;
            txtEmail.Text = model.Email;
            txtPhone.Text = model.Phone;
            txtRole.Text = model.Role;
            txtOrgId.Text = model.OrganizationId;
            txtOrgName.Text = GetORGName(model.OrganizationId);
            ddlAvailable.SelectedValue = model.Available;
            txtRemark.Text = model.Remark;
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "refrash", "<script>showEditModal();</script>", false);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            AllUser loginingUser = (AllUser)Session["loginingUser"];
            if ("".Equals(txtName.Text.Trim()))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('租户名称不能为空！')", true);
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "refrash", "<script>Reload();</script>", false);
                return;
            }
            int count = (int)SqlHelper.GetCountNumber("AllUser", "id", string.Format("name<>'{0}' and name = '{1}' and role ='{2}'", HiddenName.Value, txtName.Text.Trim(), "租户"));
            if (count != 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('租户名称重复！')", true);
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "refrash", "<script>Reload();</script>", false);
                return;
            }
            string userId = hiddenid.Text;
            AllUser model = AllUserDAL.GetByUserId(userId);
            model.Name = txtName.Text.Trim();
            model.Email = txtEmail.Text.Trim();
            model.Role = txtRole.Text.Trim();
            model.Phone = txtPhone.Text.Trim();
            model.Available = ddlAvailable.SelectedValue;
            model.Remark = txtRemark.Text.Trim();
            if (AllUserDAL.Update(model) > 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "refrash", "<script>Reload();</script>", false);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('保存失败！')", true);
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "refrash", "<script>Reload();</script>", false);
                return;
            }
        }
        protected void lbtnEnter_Click(object sender, EventArgs e)
        {
            string userId = (sender as LinkButton).CommandArgument;
            AllUser user = AllUserDAL.GetByUserId(userId);
            Session["loginingUser"] = user;
            Response.Redirect("../Layout/Redirect.aspx?type=enter");
        }
        protected void lbtnSearchUserId_Click(object sender, EventArgs e)
        {
            if ("".Equals(txtSearchUserId.Text))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "toastr.warning('租户ID搜索输入框为空','搜索警告');", true);
                return;
            }
            txtSearchName.Text = "";
            txtSearchPhone.Text = "";
            gridviewbind("userId", txtSearchUserId.Text.Trim());
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "toastr.info('新结果显示');", true);
        }

        protected void lbtnSearchName_Click(object sender, EventArgs e)
        {
            if ("".Equals(txtSearchName.Text))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "toastr.warning('租户名称搜索输入框为空','搜索警告');", true);
                return;
            }
            txtSearchUserId.Text = "";
            txtSearchPhone.Text = "";
            gridviewbind("name", txtSearchName.Text.Trim());
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "toastr.info('新结果显示');", true);
        }

        protected void lbtnSearchPhone_Click(object sender, EventArgs e)
        {
            if ("".Equals(txtSearchPhone.Text))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "toastr.warning('租户电话搜索输入框为空','搜索警告');", true);
                return;
            }
            txtSearchUserId.Text = "";
            txtSearchName.Text = "";
            gridviewbind("phone", txtSearchPhone.Text.Trim());
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "toastr.info('新结果显示');", true);
        }

        protected void btnShowAll_Click(object sender, EventArgs e)
        {
            txtSearchUserId.Text = "";
            txtSearchName.Text = "";
            txtSearchPhone.Text = "";
            gridviewbind();
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "toastr.info('新结果显示');", true);
        }
        protected string GetORGName(string organizationId)
        {
            Organization model = OrganizationDAL.GetByOrganizationId(organizationId);
            if (model == null)
            {
                return null;
            }
            return model.Name;
        }
    }
}