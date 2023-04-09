using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MeetingResMagSys.Model;
using MeetingResMagSys.DAL;
using System.Data;

namespace MeetingResMagSys.Pages
{
    public partial class Index : System.Web.UI.Page
    {
        protected string id = "";
        public string CountWeek { get; set; }
        public string CountMouth { get; set; }
        public string CountYear { get; set; }
        public string CountAll { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            CountWeek = GetCountWeek();
            CountMouth = GetCountMouth();
            CountYear = GetCountYear();
            CountAll = GetCountAll();
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
        protected string GetCountWeek()
        {
            AllUser loginingUser = (AllUser)Session["loginingUser"];
            string str = MeetingReservationDAL.getWeekMeetingCountByBooker(loginingUser.UserId, loginingUser.OrganizationId);
            return str;
        }
        protected string GetCountMouth()
        {
            AllUser loginingUser = (AllUser)Session["loginingUser"];
            string str = MeetingReservationDAL.getMouthMeetingCountByBooker(loginingUser.UserId, loginingUser.OrganizationId); ;
            return str;
        }
        protected string GetCountYear()
        {
            AllUser loginingUser = (AllUser)Session["loginingUser"];
            string str = MeetingReservationDAL.getYearMeetingCountByBooker(loginingUser.UserId, loginingUser.OrganizationId); ;
            return str;
        }
        protected string GetCountAll()
        {
            AllUser loginingUser = (AllUser)Session["loginingUser"];
            string str = MeetingReservationDAL.getTotalMeetingCountByBooker(loginingUser.UserId, loginingUser.OrganizationId); ;
            return str;
        }
        
        protected void BindDisplayModalCheckBoxList(string meetingId)
        {
            AllUser loginingUser = (AllUser)Session["loginingUser"];
            #region 人员绑定
            //展示模态框中的复选框组绑定
            DataTable dtUser = SqlHelper.Select("a.userId,name", "AllUser as a join MeetingMember as m on a.userId=m.userId", string.Format("a.organizationId='{0}' and meetingId='{1}'", loginingUser.OrganizationId, meetingId), "userId ASC");
            DcblMeetingMember.DataSource = dtUser;
            DcblMeetingMember.DataTextField = "name";
            DcblMeetingMember.DataValueField = "userId";
            DcblMeetingMember.DataBind();
            #endregion
        }
        private void gridviewbind()
        {
            AllUser loginingUser = (AllUser)Session["loginingUser"];
            string sortExpression = gv.Attributes["SortExpression"] == null ? "time" : gv.Attributes["SortExpression"];
            bool isASCDirection = true;
            if (gv.Attributes["SortDirection"] == "DESC")
            {
                isASCDirection = false;
            }
            int totalCounts = (int)SqlHelper.GetCountNumber("MeetingReservation", "id", string.Format("organizationId='{0}' and state='正常' and datediff(DAY,time,getdate())<1 and meetingId in (select meetingId from MeetingMember where userId='{1}')", loginingUser.OrganizationId, loginingUser.UserId));
            if (totalCounts > 0)
            {
                AspNetPager1.RecordCount = totalCounts;
                gv.DataSource = SqlHelper.GetPagedData("MeetingReservation", string.Format("organizationId='{0}' and state='正常' and datediff(DAY,time,getdate())<1 and meetingId in (select meetingId from MeetingMember where userId='{1}')", loginingUser.OrganizationId, loginingUser.UserId), sortExpression, isASCDirection,
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
            dt.Columns.Add("meetingState");
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

        protected void gv_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "btnDetailCommand")
            {
                ShowDetail(Convert.ToInt32(e.CommandArgument));
            }
        }

        protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
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
            //处理会议状态
            for (int i = 0; i < gv.Rows.Count; i++)
            {
                string meetingId = (gv.Rows[i].FindControl("LiteralCode") as Literal).Text;
                MeetingReservation model = MeetingReservationDAL.GetByMeetingId(meetingId);
                if (model!=null)
                {
                    if (DateTime.Now.CompareTo(DateTime.Parse(model.StartTime)) < 0)
                    {
                        (gv.Rows[i].FindControl("lMeetingState") as Literal).Text = "未开始";
                    }
                    if (DateTime.Now.CompareTo(DateTime.Parse(model.StartTime)) > 0 && DateTime.Now.CompareTo(DateTime.Parse(model.EndTime)) < 0)
                    {
                        (gv.Rows[i].FindControl("lMeetingState") as Literal).Text = "进行中";
                    }
                    if (DateTime.Now.CompareTo(DateTime.Parse(model.EndTime)) > 0)
                    {
                        (gv.Rows[i].FindControl("lMeetingState") as Literal).Text = "已结束";
                    }
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
            DtxtRemark.Text = model.Remark;
            //处理展示模态框中的复选框组
            BindDisplayModalCheckBoxList(meetingId);
            for (int i = 0; i < DcblMeetingMember.Items.Count; i++)
            {
                DcblMeetingMember.Items[i].Selected = true;
            }
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "refrash", "<script>showDisModal();</script>", false);
        }
    }
}