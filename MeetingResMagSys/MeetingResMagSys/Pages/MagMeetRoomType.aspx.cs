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
    public partial class MagMeetRoomType : System.Web.UI.Page
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
            string sortExpression = gv.Attributes["SortExpression"] == null ? "id" : gv.Attributes["SortExpression"];
            bool isASCDirection = true;
            if (gv.Attributes["SortDirection"] == "DESC")
            {
                isASCDirection = false;
            }
            int totalCounts = (int)SqlHelper.GetCountNumber("MeetingRoomType", "id", string.Format("organizationId='{0}'", loginingUser.OrganizationId));
            if (totalCounts > 0)
            {
                AspNetPager1.RecordCount = totalCounts;
                gv.DataSource = SqlHelper.GetPagedData("MeetingRoomType", string.Format("organizationId='{0}'", loginingUser.OrganizationId), sortExpression, isASCDirection,
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
        private void gridviewbind(string DBFiled,String value)
        {
            AllUser loginingUser = (AllUser)Session["loginingUser"];
            string sortExpression = gv.Attributes["SortExpression"] == null ? "id" : gv.Attributes["SortExpression"];
            bool isASCDirection = true;
            if (gv.Attributes["SortDirection"] == "DESC")
            {
                isASCDirection = false;
            }
            int totalCounts = (int)SqlHelper.GetCountNumber("MeetingRoomType", "id", string.Format("organizationId='{0}' and {1} like '%{2}%'", loginingUser.OrganizationId,DBFiled,value));
            if (totalCounts > 0)
            {
                AspNetPager1.RecordCount = totalCounts;
                gv.DataSource = SqlHelper.GetPagedData("MeetingRoomType", string.Format("organizationId='{0}' and {1} like '%{2}%'", loginingUser.OrganizationId, DBFiled, value), sortExpression, isASCDirection,
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
            dt.Columns.Add("RoomTypeId");
            dt.Columns.Add("name");
            dt.Columns.Add("introduction");
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
            if ("".Equals(txtName.Text.Trim()))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('类型名称不能为空！')", true);
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "refrash", "<script>Reload();</script>", false);
                return;
            }
            if ((int)SqlHelper.GetCountNumber("MeetingRoomType", "id", string.Format("name='{0}' and organizationId='{1}'", txtName.Text.Trim(), loginingUser.OrganizationId)) != 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('类型名称重复！')", true);
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "refrash", "<script>Reload();</script>", false);
                return;
            }
            MeetingRoomType  model = new MeetingRoomType();
            model.RoomTypeId = MeetingRoomTypeDAL.CreateRoomTypeId();
            model.Name = txtName.Text.Trim();
            model.Introduction = txtIntroduction.Text.Trim();
            model.OrganizationId = loginingUser.OrganizationId;
            model.Remark = txtRemark.Text.Trim();
            if ((int)MeetingRoomTypeDAL.Insert(model) != 0)
            {
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
            string RoomTypeId = (sender as LinkButton).CommandArgument;
            MeetingRoomType mrt = MeetingRoomTypeDAL.GetByRoomTypeId(RoomTypeId);
            hiddenid.Text = mrt.RoomTypeId;
            HiddenName.Value = mrt.Name;
            etxtName.Text = mrt.Name;
            etxtIntroduction.Text = mrt.Introduction;
            etxtRemark.Text = mrt.Remark;
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "", "<script>showEditModal();</script>", false);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            AllUser loginingUser = (AllUser)Session["loginingUser"];
            if ("".Equals(etxtName.Text.Trim()))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('类型名称不能为空！')", true);
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "refrash", "<script>Reload();</script>", false);
                return;
            }
            if ((int)SqlHelper.GetCountNumber("MeetingRoomType", "id", string.Format("name = '{0}' and organizationId ='{1}' and name<>'{2}'", etxtName.Text.Trim(), loginingUser.OrganizationId, HiddenName.Value)) != 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('类型名称重复！')", true);
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "refrash", "<script>Reload();</script>", false);
                return;
            }
            string RoomTypeId = hiddenid.Text;
            MeetingRoomType mrt = MeetingRoomTypeDAL.GetByRoomTypeId(RoomTypeId);
            mrt.Name = etxtName.Text.Trim();
            mrt.Introduction = etxtIntroduction.Text.Trim();
            mrt.Remark = etxtRemark.Text.Trim();
            if (MeetingRoomTypeDAL.Update(mrt) > 0)
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
        protected void lbtnSearchName_Click(object sender, EventArgs e)
        {
            if ("".Equals(txtSearchName.Text))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "toastr.warning('类型名称搜索输入框为空','搜索警告');", true);
                return;
            }
            gridviewbind("name", txtSearchName.Text.Trim());
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "toastr.info('新结果显示');", true);
        }
    }
}