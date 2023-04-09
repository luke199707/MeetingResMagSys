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
    public partial class DisplayUnusualMeeting : System.Web.UI.Page
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
                AllUser loginingUser = (AllUser)Session["loginingUser"];
                ddlpagesize.SelectedValue = AspNetPager1.PageSize.ToString();
                gridviewbind();
            }
        }
        private void gridviewbind()
        {
            AllUser loginingUser = (AllUser)Session["loginingUser"];
            string sortExpression = gv.Attributes["SortExpression"] == null ? "meetingId" : gv.Attributes["SortExpression"];
            bool isASCDirection = false;
            if (gv.Attributes["SortDirection"] == "ASC")
            {
                isASCDirection = true;
            }
            int totalCounts = (int)SqlHelper.GetCountNumber("MeetingReservation", "id", string.Format("organizationId='{0}' and state<>'正常' and booker='{1}'", loginingUser.OrganizationId, loginingUser.UserId));
            if (totalCounts > 0)
            {
                AspNetPager1.RecordCount = totalCounts;
                gv.DataSource = SqlHelper.GetPagedData("MeetingReservation", string.Format("organizationId='{0}' and state<>'正常' and booker='{1}'", loginingUser.OrganizationId, loginingUser.UserId), sortExpression, isASCDirection,
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
        private void gridviewbind(string DBFiled, string value)
        {
            AllUser loginingUser = (AllUser)Session["loginingUser"];
            string sortExpression = gv.Attributes["SortExpression"] == null ? "meetingId" : gv.Attributes["SortExpression"];
            bool isASCDirection = false;
            if (gv.Attributes["SortDirection"] == "ASC")
            {
                isASCDirection = true;
            }
            int totalCounts = (int)SqlHelper.GetCountNumber("MeetingReservation", "id", string.Format("organizationId='{0}' and state<>'正常' and {1} like '%{2}%' and booker='{3}'", loginingUser.OrganizationId, DBFiled, value, loginingUser.UserId));
            if (totalCounts > 0)
            {
                AspNetPager1.RecordCount = totalCounts;
                gv.DataSource = SqlHelper.GetPagedData("MeetingReservation", string.Format("organizationId='{0}' and {1} like '%{2}%' and state<>'正常' and booker='{3}'", loginingUser.OrganizationId, DBFiled, value, loginingUser.UserId), sortExpression, isASCDirection,
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
            dt.Columns.Add("meetingId");
            dt.Columns.Add("title");
            dt.Columns.Add("meetingRoom");
            dt.Columns.Add("booker");
            dt.Columns.Add("time");
            dt.Columns.Add("state");
            dt.Columns.Add("reviewer");
            dt.Columns.Add("refuseReason");
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

        public string GetRoomName(string roomId)
        {
            MeetingRoom model = MeetingRoomDAL.GetByRoomId(roomId);
            if (model == null)
            {
                return null;
            }
            return model.Name;
        }
        protected string GetBookerName(string userId)
        {
            AllUser model = AllUserDAL.GetByUserId(userId);
            if (model == null)
            {
                return null;
            }
            return model.Name;
        }

        protected void lbtnSearchTitle_Click(object sender, EventArgs e)
        {
            if ("".Equals(txtSearchTitle.Text))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "toastr.warning('搜索输入框为空','搜索警告');", true);
                return;
            }
            gridviewbind("title", txtSearchTitle.Text.Trim());
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "toastr.info('新结果显示');", true);
        }

        protected void btnDis_Click(object sender, EventArgs e)
        {
            string meetingId = (sender as Button).CommandArgument;
            MeetingReservation model = MeetingReservationDAL.GetByMeetingId(meetingId);
            DtxtMeetingId.Text = model.MeetingId;
            DtxtTitle.Text = model.Title;
            if (MeetingRoomDAL.GetByRoomId(model.MeetingRoom) != null)
            {
                DtxtMeetingRoom.Text = MeetingRoomDAL.GetByRoomId(model.MeetingRoom).Name;
            }
            else
            {
                DtxtMeetingRoom.Text = "会议室已失效！！！";
            }
            DtxtIntro.Text = model.Introduction;
            DtxtDate.Text = model.Time;
            DtxtStartTime.Text = model.StartTime.Substring(model.StartTime.Length - 5);
            DtxtEndTime.Text = model.EndTime.Substring(model.EndTime.Length - 5);
            DtxtBooker.Text = AllUserDAL.GetByUserId(model.Booker).Name;
            DtxtState.Text = model.State;
            DtxtReviewer.Text= AllUserDAL.GetByUserId(model.Reviewer).Name;
            DtxtRefuseReason.Text = model.RefuseReason;
            DtxtRemark.Text = model.Remark;
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "refrash", "<script>showDisModal();</script>", false);
        }
    }
}