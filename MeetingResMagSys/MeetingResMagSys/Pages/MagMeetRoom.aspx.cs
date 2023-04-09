using MeetingResMagSys.DAL;
using MeetingResMagSys.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace MeetingResMagSys.Pages
{
    public partial class MagMeetRoom : System.Web.UI.Page
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
                BindAddCheckBoxList();
            }
        }
        private void gridviewbind()
        {
            AllUser loginingUser = (AllUser)Session["loginingUser"];
            string sortExpression = gv.Attributes["SortExpression"] == null ? "roomId" : gv.Attributes["SortExpression"];
            bool isASCDirection = true;
            if (gv.Attributes["SortDirection"] == "DESC")
            {
                isASCDirection = false;
            }
            int totalCounts = (int)SqlHelper.GetCountNumber("MeetingRoom", "id", string.Format("organizationId='{0}'", loginingUser.OrganizationId));
            if (totalCounts > 0)
            {
                AspNetPager1.RecordCount = totalCounts;
                gv.DataSource = SqlHelper.GetPagedData("MeetingRoom", string.Format("organizationId='{0}'", loginingUser.OrganizationId), sortExpression, isASCDirection,
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
        private void gridviewbind(string DBFiled,string value)
        {
            AllUser loginingUser = (AllUser)Session["loginingUser"];
            string sortExpression = gv.Attributes["SortExpression"] == null ? "roomId" : gv.Attributes["SortExpression"];
            bool isASCDirection = true;
            if (gv.Attributes["SortDirection"] == "DESC")
            {
                isASCDirection = false;
            }
            int totalCounts = (int)SqlHelper.GetCountNumber("MeetingRoom", "id", string.Format("organizationId='{0}' and {1} like '%{2}%'", loginingUser.OrganizationId,DBFiled,value));
            if (totalCounts > 0)
            {
                AspNetPager1.RecordCount = totalCounts;
                gv.DataSource = SqlHelper.GetPagedData("MeetingRoom", string.Format("organizationId='{0}' and {1} like '%{2}%'", loginingUser.OrganizationId, DBFiled, value), sortExpression, isASCDirection,
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
            dt.Columns.Add("roomId");
            dt.Columns.Add("name");
            dt.Columns.Add("director");
            dt.Columns.Add("officeArea");
            dt.Columns.Add("position");
            dt.Columns.Add("capacity");
            dt.Columns.Add("type");
            dt.Columns.Add("available");
            dt.Columns.Add("isCheck");
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
                if ((gv.Rows[i].FindControl("LiteralAvailable") as Literal).Text=="禁用")
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
        protected void BindDDL()
        {
            //两个模态框中的下拉框一起绑定
            AllUser loginingUser = (AllUser)Session["loginingUser"];
            ListItem item = new ListItem("请选择", "请选择");
            ListItem item2 = new ListItem("无", "无");
            if (loginingUser != null)
            {
                #region 负责人绑定
                DataTable dtDirector = SqlHelper.Select("userId,name", "AllUser", string.Format("organizationId='{0}'", loginingUser.OrganizationId), "userId ASC");
                //AddModal中的
                ddlDirector.DataSource = dtDirector;
                ddlDirector.DataTextField = "name";
                ddlDirector.DataValueField = "userId";
                ddlDirector.DataBind();
                ddlDirector.Items.Insert(0, item);
                ddlDirector.SelectedIndex = 0;
                //EditModal中的
                eddlDirector.DataSource = dtDirector;
                eddlDirector.DataTextField = "name";
                eddlDirector.DataValueField = "userId";
                eddlDirector.DataBind();
                eddlDirector.Items.Insert(0, item);
                eddlDirector.SelectedIndex = 0;
                #endregion
                #region 部门绑定
                DataTable dtDepartment = SqlHelper.Select("departmentId,name", "Department", string.Format("organizationId='{0}'", loginingUser.OrganizationId), "departmentId ASC");
                //EditModal中的
                ddlDepartment.DataSource = dtDepartment;
                ddlDepartment.DataTextField = "name";
                ddlDepartment.DataValueField = "departmentId";
                ddlDepartment.DataBind();
                ddlDepartment.Items.Insert(0, item2);
                ddlDepartment.SelectedIndex = 0;
                //EditModal中的
                eddlDepartment.DataSource = dtDepartment;
                eddlDepartment.DataTextField = "name";
                eddlDepartment.DataValueField = "departmentId";
                eddlDepartment.DataBind();
                eddlDepartment.Items.Insert(0, item2);
                eddlDepartment.SelectedIndex = 0;
                #endregion
                #region 办公区域绑定
                DataTable dtOfficeArea = SqlHelper.Select("officeAreaId,name", "OfficeArea", string.Format("organizationId='{0}'", loginingUser.OrganizationId), "officeAreaId ASC");
                //AddModal中的
                ddlOfficeArea.DataSource = dtOfficeArea;
                ddlOfficeArea.DataTextField = "name";
                ddlOfficeArea.DataValueField = "officeAreaId";
                ddlOfficeArea.DataBind();
                ddlOfficeArea.Items.Insert(0, item);
                ddlOfficeArea.SelectedIndex = 0;
                //EditModal中的
                eddlOfficeArea.DataSource = dtOfficeArea;
                eddlOfficeArea.DataTextField = "name";
                eddlOfficeArea.DataValueField = "officeAreaId";
                eddlOfficeArea.DataBind();
                eddlOfficeArea.Items.Insert(0, item);
                eddlOfficeArea.SelectedIndex = 0;
                #endregion
                #region 会议室类型绑定
                DataTable dtMeetingRoomType = SqlHelper.Select("RoomTypeId,name", "MeetingRoomType", string.Format("organizationId='{0}'", loginingUser.OrganizationId), "id ASC");
                //AddModal中的
                ddlType.DataSource = dtMeetingRoomType;
                ddlType.DataTextField = "name";
                ddlType.DataValueField = "RoomTypeId";
                ddlType.DataBind();
                ddlType.Items.Insert(0, item);
                ddlType.SelectedIndex = 0;
                //EditModal中的
                eddlType.DataSource = dtMeetingRoomType;
                eddlType.DataTextField = "name";
                eddlType.DataValueField = "RoomTypeId";
                eddlType.DataBind();
                eddlType.Items.Insert(0, item);
                eddlType.SelectedIndex = 0;
                #endregion
            }
        }
        protected void BindAddCheckBoxList()
        {
            AllUser loginingUser = (AllUser)Session["loginingUser"];
            #region 设施绑定
            DataTable dtfacility = SqlHelper.Select("facilityId,name", "RoomFacility", string.Format("organizationId='{0}'", loginingUser.OrganizationId), "facilityId ASC");
            cblFacility.DataSource = dtfacility;
            cblFacility.DataTextField = "name";
            cblFacility.DataValueField = "facilityId";
            cblFacility.DataBind();
            #endregion
            #region 部门绑定
            DataTable dtdepartment = SqlHelper.Select("departmentId,name", "Department", string.Format("organizationId='{0}'", loginingUser.OrganizationId), "departmentId ASC");
            cblBanDep.DataSource = dtdepartment;
            cblBanDep.DataTextField = "name";
            cblBanDep.DataValueField = "departmentId";
            cblBanDep.DataBind();
            #endregion
        }

        protected void btnAddCertain_Click(object sender, EventArgs e)
        {
            AllUser loginingUser = (AllUser)Session["loginingUser"];
            if ("".Equals(txtName.Text.Trim())|| ddlDirector.SelectedIndex == 0 || "".Equals(ddlDirector.SelectedValue)||ddlOfficeArea.SelectedIndex==0||"".Equals(ddlOfficeArea.SelectedValue)||ddlType.SelectedIndex==0 || "".Equals(ddlType.SelectedValue))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('信息填写不全！')", true);
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "refrash", "<script>Reload();</script>", false);
                return;
            }
            if ((int)SqlHelper.GetCountNumber("MeetingRoom", "id", string.Format("name='{0}' and organizationId='{1}'", txtName.Text.Trim(), loginingUser.OrganizationId)) != 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('会议室名称重复！')", true);
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "refrash", "<script>Reload();</script>", false);
                return;
            }
            MeetingRoom mr = new MeetingRoom();
            mr.RoomId = MeetingRoomDAL.CreateRoomId();
            mr.Name = txtName.Text.Trim();
            mr.Director = ddlDirector.SelectedValue;
            mr.OfficeArea = ddlOfficeArea.SelectedValue;
            mr.Position = txtPosition.Text.Trim();
            mr.Capacity = txtCapacity.Text.Trim();
            mr.Type = ddlType.SelectedValue;
            mr.Introduction = txtIntroduction.Text.Trim();
            mr.Attention = txtAttention.Text.Trim();
            mr.ResDepartment = ddlDepartment.SelectedValue;
            mr.IsCheck = ddlIsCheck.SelectedValue;
            mr.Available = "可用";
            mr.OrganizationId = loginingUser.OrganizationId;
            mr.Remark = txtRemark.Text.Trim();
            //处理会议室设施
            for (int i = 0; i < cblFacility.Items.Count; i++)
            {
                if (cblFacility.Items[i].Selected)
                {
                    MeetingRoomFacility model = new MeetingRoomFacility();
                    model.RoomId = mr.RoomId;
                    model.FacilityId = cblFacility.Items[i].Value;
                    MeetingRoomFacilityDAL.Insert(model);
                }
            }
            //处理被禁止部门
            for (int i = 0; i < cblBanDep.Items.Count; i++)
            {
                if (cblBanDep.Items[i].Selected)
                {
                    MeetingRoomBanDep model = new MeetingRoomBanDep();
                    model.RoomId = mr.RoomId;
                    model.DepartmentId = cblBanDep.Items[i].Value;
                    MeetingRoomBanDepDAL.Insert(model);
                }
            }
            if ((int)MeetingRoomDAL.Insert(mr) != 0)
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
        protected void BindEditCheckBoxList(string roomId)
        {
            AllUser loginingUser = (AllUser)Session["loginingUser"];
            #region 设施绑定
            DataTable dtfacility = SqlHelper.Select("facilityId,name", "RoomFacility", string.Format("organizationId='{0}'", loginingUser.OrganizationId), "facilityId ASC");
            ecblFacility.DataSource = dtfacility;
            ecblFacility.DataTextField = "name";
            ecblFacility.DataValueField = "facilityId";
            ecblFacility.DataBind();
            //如果该会议室拥有该设备，将复选框勾上
            for (int i = 0; i < ecblFacility.Items.Count; i++)
            {
                string facilityId = ecblFacility.Items[i].Value;
                if (MeetingRoomFacilityDAL.GetByRoomIdFacilityId(roomId, facilityId) !=null)
                {
                    ecblFacility.Items[i].Selected = true;
                }
            }
            #endregion
            #region 部门绑定
            DataTable dtdepartment = SqlHelper.Select("departmentId,name", "Department", string.Format("organizationId='{0}'", loginingUser.OrganizationId), "departmentId ASC");
            ecblBanDep.DataSource = dtdepartment;
            ecblBanDep.DataTextField = "name";
            ecblBanDep.DataValueField = "departmentId";
            ecblBanDep.DataBind();
            //如果该部门被禁止，将复选框勾上
            for (int i = 0; i < ecblBanDep.Items.Count; i++)
            {
                string departmentId = ecblBanDep.Items[i].Value;
                if (MeetingRoomBanDepDAL.GetByRoomIdDepId(roomId, departmentId) != null)
                {
                    ecblBanDep.Items[i].Selected = true;
                }
            }
            #endregion
        }
        protected void lbtnEdit_Click(object sender, EventArgs e)
        {
            string roomId = (sender as LinkButton).CommandArgument;
            MeetingRoom mr = MeetingRoomDAL.GetByRoomId(roomId);
            hiddenid.Text = mr.RoomId;
            HiddenName.Value = mr.Name;
            etxtName.Text = mr.Name;
            eddlDirector.SelectedValue = mr.Director;
            eddlOfficeArea.SelectedValue = mr.OfficeArea;
            etxtPosition.Text = mr.Position;
            etxtCapacity.Text = mr.Capacity.ToString();
            eddlType.SelectedValue = mr.Type;
            etxtIntroduction.Text = mr.Introduction;
            BindEditCheckBoxList(roomId);
            etxtAttention.Text = mr.Attention;
            eddlDepartment.SelectedValue = mr.ResDepartment;
            eddlIsCheck.SelectedValue = mr.IsCheck;
            eddlAvailable.SelectedValue = mr.Available;
            etxtRemark.Text = mr.Remark;
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "refrash", "<script>showEditModal();</script>", false);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            AllUser loginingUser = (AllUser)Session["loginingUser"];
            if ("".Equals(etxtName.Text.Trim()) || eddlDirector.SelectedIndex == 0 || "".Equals(eddlDirector.SelectedValue) || eddlOfficeArea.SelectedIndex == 0 || "".Equals(eddlOfficeArea.SelectedValue) || eddlType.SelectedIndex == 0 || "".Equals(eddlType.SelectedValue))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('信息填写不全！')", true);
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "refrash", "<script>Reload();</script>", false);
                return;
            }
            int count = (int)SqlHelper.GetCountNumber("MeetingRoom", "id", string.Format("name<>'{0}' and name = '{1}' and organizationId ='{2}'", HiddenName.Value, etxtName.Text.Trim(), loginingUser.OrganizationId));
            if (count != 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('会议室名称重复！')", true);
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "refrash", "<script>Reload();</script>", false);
                return;
            }
            string roomId = hiddenid.Text;
            MeetingRoom mr = MeetingRoomDAL.GetByRoomId(roomId);
            mr.Name = etxtName.Text.Trim();
            mr.Director = eddlDirector.SelectedValue;
            mr.OfficeArea = eddlOfficeArea.SelectedValue;
            mr.Position = etxtPosition.Text.Trim();
            mr.Capacity = etxtCapacity.Text.Trim();
            mr.Type = eddlType.SelectedValue;
            mr.Introduction = etxtIntroduction.Text.Trim();
            mr.Attention = etxtAttention.Text.Trim();
            mr.ResDepartment = eddlDepartment.SelectedValue;
            mr.IsCheck = eddlIsCheck.SelectedValue;
            mr.Remark = etxtRemark.Text.Trim();
            mr.Available = eddlAvailable.SelectedValue;
            //处理会议室设施
            //先清除表中数据，再重新添加
            MeetingRoomFacilityDAL.DeleteByRoomId(roomId);
            for (int i = 0; i < ecblFacility.Items.Count; i++)
            {
                if (ecblFacility.Items[i].Selected)
                {
                    MeetingRoomFacility model = new MeetingRoomFacility();
                    model.RoomId = mr.RoomId;
                    model.FacilityId = ecblFacility.Items[i].Value;
                    MeetingRoomFacilityDAL.Insert(model);
                }
            }
            //处理被禁止部门
            //先清除表中数据，再重新添加
            MeetingRoomBanDepDAL.DeleteByRoomId(roomId);
            for (int i = 0; i < ecblBanDep.Items.Count; i++)
            {
                if (ecblBanDep.Items[i].Selected)
                {
                    MeetingRoomBanDep model = new MeetingRoomBanDep();
                    model.RoomId = mr.RoomId;
                    model.DepartmentId = ecblBanDep.Items[i].Value;
                    MeetingRoomBanDepDAL.Insert(model);
                }
            }
            if (MeetingRoomDAL.Update(mr) > 0)
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
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "toastr.warning('会议室名称搜索输入框为空','搜索警告');", true);
                return;
            }
            gridviewbind("name", txtSearchName.Text.Trim());
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "toastr.info('新结果显示');", true);
        }
        protected string GetTypeName(string type)
        {
            MeetingRoomType model = MeetingRoomTypeDAL.GetByRoomTypeId(type);
            if (model == null)
            {
                return null;
            }
            return model.Name;
        }
        protected string GetOfficeAreaName(string officeArea)
        {
            OfficeArea model = OfficeAreaDAL.GetByOAId(officeArea);
            if (model == null)
            {
                return null;
            }
            return model.Name;
        }
        protected string GetDirectorName(string userId)
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