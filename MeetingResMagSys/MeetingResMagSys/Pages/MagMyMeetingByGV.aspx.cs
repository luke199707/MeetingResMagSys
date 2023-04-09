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
    public partial class MagMyMeetingByGV : System.Web.UI.Page
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
                BindMeetingRoom();
            }
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
        protected void BindEditModalCheckBoxList()
        {
            AllUser loginingUser = (AllUser)Session["loginingUser"];
            #region 人员绑定
            //编辑模态框中的复选框组绑定
            DataTable dtUser2 = SqlHelper.Select("userId,name", "AllUser", string.Format("organizationId='{0}' and userId<>'{1}'", loginingUser.OrganizationId, loginingUser.UserId), "userId ASC");
            EcblMeetingMember.DataSource = dtUser2;
            EcblMeetingMember.DataTextField = "name";
            EcblMeetingMember.DataValueField = "userId";
            EcblMeetingMember.DataBind();
            #endregion
        }
        protected void BindMeetingRoom()
        {
            AllUser loginingUser = (AllUser)Session["loginingUser"];
            ListItem item = new ListItem("请选择", "请选择");
            if (loginingUser != null)
            {
                DataTable dtRoom = SqlHelper.Select("name,roomId", "MeetingRoom",
                    string.Format("organizationId='{0}' and available='可用' and roomId not in (select roomId from MeetingRoomBanDep where departmentId = '{1}')",
                    loginingUser.OrganizationId, loginingUser.DepartmentName), "name ASC");
                //会议预订模态框中绑定
                EddlMeetingRoom.DataSource = dtRoom;
                EddlMeetingRoom.DataTextField = "name";
                EddlMeetingRoom.DataValueField = "roomId";
                EddlMeetingRoom.DataBind();
                EddlMeetingRoom.Items.Insert(0, item);
                EddlMeetingRoom.SelectedIndex = 0;
            }
        }
        private void gridviewbind()
        {
            AllUser loginingUser = (AllUser)Session["loginingUser"];
            string sortExpression = gv.Attributes["SortExpression"] == null ? "time" : gv.Attributes["SortExpression"];
            bool isASCDirection = false;
            if (gv.Attributes["SortDirection"] == "ASC")
            {
                isASCDirection = true;
            }
            int totalCounts = (int)SqlHelper.GetCountNumber("MeetingReservation", "id", string.Format("organizationId='{0}' and state='正常' and meetingId in (select meetingId from MeetingMember where userId='{1}')", loginingUser.OrganizationId, loginingUser.UserId));
            if (totalCounts > 0)
            {
                AspNetPager1.RecordCount = totalCounts;
                gv.DataSource = SqlHelper.GetPagedData("MeetingReservation", string.Format("organizationId='{0}' and state='正常' and meetingId in (select meetingId from MeetingMember where userId='{1}')", loginingUser.OrganizationId, loginingUser.UserId), sortExpression, isASCDirection,
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
            string sortExpression = gv.Attributes["SortExpression"] == null ? "time" : gv.Attributes["SortExpression"];
            bool isASCDirection = false;
            if (gv.Attributes["SortDirection"] == "ASC")
            {
                isASCDirection = true;
            }
            int totalCounts = (int)SqlHelper.GetCountNumber("MeetingReservation", "id", string.Format("organizationId='{0}' and state='正常' and {1} like '%{2}%' and meetingId in (select meetingId from MeetingMember where userId='{3}')", loginingUser.OrganizationId, DBFiled, value, loginingUser.UserId));
            if (totalCounts > 0)
            {
                AspNetPager1.RecordCount = totalCounts;
                gv.DataSource = SqlHelper.GetPagedData("MeetingReservation", string.Format("organizationId='{0}' and {1} like '%{2}%' and state='正常' and meetingId in (select meetingId from MeetingMember where userId='{3}')", loginingUser.OrganizationId, DBFiled, value, loginingUser.UserId), sortExpression, isASCDirection,
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
            //会议时间在当前时间之前的，以及会议非登陆用户预订的，隐藏编辑和取消按钮
            AllUser loginingUser = (AllUser)Session["loginingUser"];
            for (int i = 0; i < gv.Rows.Count; i++)//第一行为表头，i从1开始，避免因为表中无数据导致model空值异常
            {
                Button btnEdit = gv.Rows[i].FindControl("btnEdit") as Button;
                Button btnDelete = gv.Rows[i].FindControl("btnDelete") as Button;
                Literal lmeetingId = gv.Rows[i].FindControl("lMeetingId") as Literal;
                Literal lbooker = gv.Rows[i].FindControl("lBooker") as Literal;
                string meetingId = lmeetingId.Text;
                string booker = lbooker.Text;
                MeetingReservation model = MeetingReservationDAL.GetByMeetingId(meetingId);
                if (model!=null)
                {
                    //隐藏编辑和取消按钮
                    if (DateTime.Now.CompareTo(DateTime.Parse(model.EndTime)) >= 0 || booker != loginingUser.Name)
                    {
                        btnEdit.Visible = false;
                        btnDelete.Visible = false;
                    }
                    //处理会议状态
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
            
            for (int i = 0; i < gv.Rows.Count; i++)
            {
                
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
            DtxtRemark.Text = model.Remark;
            //处理展示模态框中的复选框组
            BindDisplayModalCheckBoxList(meetingId);
            for (int i = 0; i < DcblMeetingMember.Items.Count; i++)
            {
                DcblMeetingMember.Items[i].Selected = true;
            }
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "refrash", "<script>showDisModal();</script>", false);
        }
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            string meetingId = (sender as Button).CommandArgument;
            MeetingReservation model = MeetingReservationDAL.GetByMeetingId(meetingId);
            EtxtMeetingId.Text = model.MeetingId;
            EtxtTitle.Text = model.Title;
            if (MeetingRoomDAL.GetByRoomId(model.MeetingRoom) != null)
            {
                EddlMeetingRoom.SelectedValue = model.MeetingRoom;
            }
            else
            {
                EddlMeetingRoom.SelectedValue = "请选择";
            }
            EtxtIntro.Text = model.Introduction;
            EtxtDate.Text = model.Time;
            EtxtStartTime.Text = model.StartTime.Substring(model.StartTime.Length - 5);
            EtxtEndTime.Text = model.EndTime.Substring(model.EndTime.Length - 5);
            EtxtRemark.Text = model.Remark;
            HiddenMeetingId.Value = meetingId;//编辑保存时使用
            //处理编辑模态框中的复选框组
            BindEditModalCheckBoxList();
            string sql = string.Format("select userId from MeetingMember where meetingId='{0}'", meetingId);
            DataTable dt = SqlHelper.ExecuteDataTable(sql, CommandType.Text);
            List<string> attendUserId = new List<string>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                attendUserId.Add(dt.Rows[i][0].ToString());
            }
            for (int i = 0; i < EcblMeetingMember.Items.Count; i++)
            {
                if (attendUserId.Contains(EcblMeetingMember.Items[i].Value))
                {
                    EcblMeetingMember.Items[i].Selected = true;
                }
            }
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "refrash", "<script>showEditModal();</script>", false);
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            AllUser loginingUser = (AllUser)Session["loginingUser"];
            string meetingId = HiddenMeetingId.Value;
            if ("".Equals(EtxtTitle.Text.Trim()) || "请选择".Equals(EddlMeetingRoom.SelectedValue) || "".Equals(EddlMeetingRoom.SelectedValue) || "".Equals(EtxtDate.Text.Trim()) || "".Equals(EtxtStartTime.Text.Trim()) || "".Equals(EtxtEndTime.Text.Trim()))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('信息填写不全!')", true);
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "refrash", "<script>Reload();</script>", false);
                return;
            }
            MeetingReservation model = MeetingReservationDAL.GetByMeetingId(meetingId);
            model.Title = EtxtTitle.Text.Trim();
            model.MeetingRoom = EddlMeetingRoom.SelectedValue;
            model.Introduction = EtxtIntro.Text.Trim();
            model.Time = EtxtDate.Text.Trim();
            string StartTime = EtxtDate.Text.Trim() + "T" + EtxtStartTime.Text.Trim();
            string EndTime = EtxtDate.Text.Trim() + "T" + EtxtEndTime.Text.Trim();
            List<MeetingReservation> list = MeetingReservationDAL.GetAllByDateAndRoom(EddlMeetingRoom.SelectedValue, EtxtDate.Text.Trim(), meetingId, loginingUser.OrganizationId);
            if (MeetingReservationDAL.compareTime(list, StartTime, EndTime) == false)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('所选会议时间与其他会议冲突！')", true);
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "refrash", "<script>Reload();</script>", false);
                return;
            }
            if (DateTime.Parse(EtxtStartTime.Text.Trim()).CompareTo(DateTime.Parse(EtxtEndTime.Text.Trim())) > 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('会议开始时间不能晚于结束时间！')", true);
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "refrash", "<script>Reload();</script>", false);
                return;
            }
            if (DateTime.Parse(StartTime).CompareTo(DateTime.Now) < 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('开始时间不能在现在时间之前！')", true);
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "refrash", "<script>Reload();</script>", false);
                return;
            }
            model.StartTime = StartTime;
            model.EndTime = EndTime;
            string tip = "预订修改成功！";
            MeetingRoom room = MeetingRoomDAL.GetByRoomId(EddlMeetingRoom.SelectedValue);
            if (room.IsCheck == "是")
            {
                if (loginingUser.UserId == room.Director)
                {
                    model.State = "正常";
                }
                else
                {
                    model.State = "待审核";
                    model.Reviewer = room.Director;
                    tip = "预订申请已提交，等待审核！";
                }
            }
            else
            {
                model.State = "正常";
            }
            model.Remark = EtxtRemark.Text.Trim();
            //处理参会人员,先清除，再添加
            MeetingMemberDAL.DeleteByMeetingId(meetingId);
            for (int i = 0; i < EcblMeetingMember.Items.Count; i++)
            {
                if (EcblMeetingMember.Items[i].Selected)
                {
                    MeetingMember mm = new MeetingMember();
                    mm.MeetingId = model.MeetingId;
                    mm.UserId = EcblMeetingMember.Items[i].Value;
                    MeetingMemberDAL.Insert(mm);
                }
            }
            //将会议预订人加入参与人员表
            MeetingMember mm2 = new MeetingMember();
            mm2.MeetingId = model.MeetingId;
            mm2.UserId = loginingUser.UserId;
            MeetingMemberDAL.Insert(mm2);

            MeetingReservationDAL.Update(model);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('" + tip + "！')", true);
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "refrash", "<script>Reload();</script>", false);
        }
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string meetingId = (sender as Button).CommandArgument;
            if (MeetingReservationDAL.DeleteByMeetingId(meetingId) > 0)
            {
                //取消会议时清除会议成员表中的相关数据
                MeetingMemberDAL.DeleteByMeetingId(meetingId);
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "refrash", "<script>toastr.success('预订成功取消！');</script>", false);
            }
            gridviewbind();
        }
        protected void btnChangeView_Click(object sender, EventArgs e)
        {
            Response.Redirect("MagMyMeeting.aspx");
        }
    }
}