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
    public partial class MagMyMeeting : System.Web.UI.Page
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
                AllUser loginingUser = (AllUser)Session["loginingUser"];
                string userName = loginingUser.Name;
                Session["userName"] = userName;//用于前台判定
                BindMeetingRoom();
                BindCheckList();
            }
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
        protected void BindCheckList()
        {
            AllUser loginingUser = (AllUser)Session["loginingUser"];
            #region 人员绑定
            //展示模态框中的复选框组绑定
            DataTable dtUser = SqlHelper.Select("userId,name", "AllUser", string.Format("organizationId='{0}'", loginingUser.OrganizationId), "userId ASC");
            DcblMeetingMember.DataSource = dtUser;
            DcblMeetingMember.DataTextField = "name";
            DcblMeetingMember.DataValueField = "userId";
            DcblMeetingMember.DataBind();
            //编辑模态框中的复选框组绑定
            DataTable dtUser2 = SqlHelper.Select("userId,name", "AllUser", string.Format("organizationId='{0}' and userId<>'{1}'", loginingUser.OrganizationId, loginingUser.UserId), "userId ASC");
            EcblMeetingMember.DataSource = dtUser2;
            EcblMeetingMember.DataTextField = "name";
            EcblMeetingMember.DataValueField = "userId";
            EcblMeetingMember.DataBind();
            #endregion
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if ("".Equals(EtxtTitle.Text.Trim())|| "请选择".Equals(EddlMeetingRoom.SelectedValue)||"".Equals(EddlMeetingRoom.SelectedValue)||"".Equals(EtxtDate.Text.Trim())|| "".Equals(EtxtStartTime.Text.Trim()) || "".Equals(EtxtEndTime.Text.Trim()))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('信息填写不全！')", true);
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "refrash", "<script>Reload();</script>", false);
                return;
            }
            string meetingId = HiddenMeetingId.Value;
            AllUser loginingUser = (AllUser)Session["loginingUser"];
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
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('"+ tip + "！')", true);
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "refrash", "<script>Reload();</script>", false);
        }

        protected void btnChangeView_Click(object sender, EventArgs e)
        {
            Response.Redirect("MagMyMeetingByGV.aspx");
        }
    }
}