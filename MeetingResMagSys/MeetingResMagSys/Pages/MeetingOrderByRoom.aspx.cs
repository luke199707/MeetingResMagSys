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
    public partial class MeetingOrderByRoom : System.Web.UI.Page
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
                BindAddCheckList();
                BindDisplayCheckList();
                BindOfficeArea();
                BindMeetingRoom();
            }
        }
        protected void BindAddCheckList()
        {
            AllUser loginingUser = (AllUser)Session["loginingUser"];
            #region 人员绑定
            DataTable dtUser = SqlHelper.Select("userId,name", "AllUser", string.Format("organizationId='{0}' and userId<>'{1}'", loginingUser.OrganizationId,loginingUser.UserId), "userId ASC");
            AcblMeetingMember.DataSource = dtUser;
            AcblMeetingMember.DataTextField = "name";
            AcblMeetingMember.DataValueField = "userId";
            AcblMeetingMember.DataBind();
            #endregion
        }
        protected void BindDisplayCheckList()
        {
            AllUser loginingUser = (AllUser)Session["loginingUser"];
            #region 参会人员绑定
            DataTable dtUser = SqlHelper.Select("userId,name", "AllUser",string.Format("organizationId='{0}'",loginingUser.OrganizationId), "userId ASC");
            DcblMeetingMember.DataSource = dtUser;
            DcblMeetingMember.DataTextField = "name";
            DcblMeetingMember.DataValueField = "userId";
            DcblMeetingMember.DataBind();
            #endregion
        }
        protected void BindOfficeArea()
        {
            AllUser loginingUser = (AllUser)Session["loginingUser"];
            ListItem item = new ListItem("全部办工区域", "全部办工区域");
            if (loginingUser != null)
            {
                DataTable dtOfficeArea = SqlHelper.Select("name,officeAreaId", "OfficeArea", string.Format("organizationId='{0}'", loginingUser.OrganizationId), "officeAreaId ASC");
                ddlOfficeArea.DataSource = dtOfficeArea;
                ddlOfficeArea.DataTextField = "name";
                ddlOfficeArea.DataValueField = "officeAreaId";
                ddlOfficeArea.DataBind();
                ddlOfficeArea.Items.Insert(0, item);
                ddlOfficeArea.SelectedIndex = 0;
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
                    loginingUser.OrganizationId,loginingUser.DepartmentName), "name ASC");
                //顶部下拉框绑定
                ddlMeetingRoom.DataSource = dtRoom;
                ddlMeetingRoom.DataTextField = "name";
                ddlMeetingRoom.DataValueField = "roomId";
                ddlMeetingRoom.DataBind();
                ddlMeetingRoom.Items.Insert(0, item);
                ddlMeetingRoom.SelectedIndex = 0;
                //会议预订模态框中绑定
                AddlMeetingRoom.DataSource = dtRoom;
                AddlMeetingRoom.DataTextField = "name";
                AddlMeetingRoom.DataValueField = "roomId";
                AddlMeetingRoom.DataBind();
                AddlMeetingRoom.Items.Insert(0, item);
                AddlMeetingRoom.SelectedIndex = 0;
            }
        }

        protected void ddlOfficeArea_SelectedIndexChanged(object sender, EventArgs e)
        {
            string oa = ddlOfficeArea.SelectedValue;
            AllUser loginingUser = (AllUser)Session["loginingUser"];
            ListItem item = new ListItem("请选择", "请选择");
            DataTable dtRoom;
            if (loginingUser != null)
            {
                if (oa.Equals("全部办工区域"))
                {
                     dtRoom = SqlHelper.Select("name,roomId", "MeetingRoom", string.Format("organizationId='{0}' and available='可用'", loginingUser.OrganizationId), "name ASC");
                }
                else
                {

                     dtRoom = SqlHelper.Select("name,roomId", "MeetingRoom", string.Format("organizationId='{0}' and available='可用' and officeArea='{1}'", loginingUser.OrganizationId, oa), "name ASC");
                }
                ddlMeetingRoom.DataSource = dtRoom;
                ddlMeetingRoom.DataTextField = "name";
                ddlMeetingRoom.DataValueField = "roomId";
                ddlMeetingRoom.DataBind();
                ddlMeetingRoom.Items.Insert(0, item);
                ddlMeetingRoom.SelectedIndex = 0;
            }
        }

        protected void ddlMeetingRoom_SelectedIndexChanged(object sender, EventArgs e)
        {
            AllUser loginingUser = (AllUser)Session["loginingUser"];
            MeetingRoom model = MeetingRoomDAL.GetByRoomId(ddlMeetingRoom.SelectedValue);
            if (model!=null)
            {
                if (model.IsCheck == "是")
                {
                    if (loginingUser.UserId != model.Director)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "<script>Tips();</script>", false);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "<script>CreateCal();</script>", false);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "<script>CreateCal();</script>", false);
                }
            }
        }
        protected void btnAddCertain_Click(object sender, EventArgs e)
        {
            if ("".Equals(AtxtTitle.Text)|| "请选择".Equals(AddlMeetingRoom.SelectedValue) || "".Equals(AtxtDate.Text)||"".Equals(AtxtStartTime.Text)||"".Equals(AtxtEndTime.Text))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('信息填写不全！')", true);
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "refrash", "<script>Reload();</script>", false);
                return;
            }
            AllUser loginingUser = (AllUser)Session["loginingUser"];
            MeetingReservation model = new MeetingReservation();
            model.MeetingId = MeetingReservationDAL.CreateMeetingId();
            model.Title = AtxtTitle.Text.Trim();
            model.MeetingRoom = AddlMeetingRoom.SelectedValue;
            model.Introduction = AtxtIntro.Text.Trim();
            model.Time = AtxtDate.Text.Trim();
            string StartTime = AtxtDate.Text.Trim() + "T" + AtxtStartTime.Text.Trim();
            string EndTime = AtxtDate.Text.Trim() + "T" + AtxtEndTime.Text.Trim();
            List<MeetingReservation> list = MeetingReservationDAL.GetAllByDateAndRoom(AddlMeetingRoom.SelectedValue, AtxtDate.Text.Trim(), loginingUser.OrganizationId);
            if (MeetingReservationDAL.compareTime(list, StartTime,EndTime)==false)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('所选会议时间与其他会议冲突！')", true);
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "refrash", "<script>Reload();</script>", false);
                return;
            }
            if (DateTime.Parse(AtxtStartTime.Text.Trim()).CompareTo(DateTime.Parse(AtxtEndTime.Text.Trim()))>0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('开始时间不能晚于结束时间！')", true);
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
            model.Booker = loginingUser.UserId;
            string tip = "预订成功";
            MeetingRoom room = MeetingRoomDAL.GetByRoomId(AddlMeetingRoom.SelectedValue);
            if (room.IsCheck=="是")
            {
                if (loginingUser.UserId==room.Director)
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
            model.OrganizationId = loginingUser.OrganizationId;
            model.OrderTime = DateTime.Now.ToString();
            model.Remark = AtxtRemark.Text.Trim();
            //处理参会人员
            for (int i = 0; i < AcblMeetingMember.Items.Count; i++)
            {
                if (AcblMeetingMember.Items[i].Selected)
                {
                    MeetingMember mm = new MeetingMember();
                    mm.MeetingId = model.MeetingId;
                    mm.UserId = AcblMeetingMember.Items[i].Value;
                    MeetingMemberDAL.Insert(mm);
                }
            }
            //将会议预订人加入参与人员表
            MeetingMember mm2 = new MeetingMember();
            mm2.MeetingId = model.MeetingId;
            mm2.UserId = loginingUser.UserId;
            MeetingMemberDAL.Insert(mm2);

            if ((int)MeetingReservationDAL.Insert(model) != 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('"+ tip + "！')", true);
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "refrash", "<script>Reload();</script>", false);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('预订失败！')", true);
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "refrash", "<script>Reload();</script>", false);
            }
        }
    }
}