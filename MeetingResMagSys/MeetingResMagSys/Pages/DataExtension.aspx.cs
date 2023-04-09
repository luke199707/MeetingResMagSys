using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MeetingResMagSys.DAL;
using MeetingResMagSys.Model;

namespace MeetingResMagSys.Pages
{
    public partial class DataExtension : System.Web.UI.Page
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
                ShowGridViewTitle();
                gridviewbind(ddlExtTable.SelectedValue+"Column");
            }
        }
        private void gridviewbind(String tableName)
        {
            AllUser loginingUser = (AllUser)Session["loginingUser"];
            string sortExpression = gv.Attributes["SortExpression"] == null ? "id" : gv.Attributes["SortExpression"];
            bool isASCDirection = true;
            if (gv.Attributes["SortDirection"] == "DESC")
            {
                isASCDirection = false;
            }
            int totalCounts = (int)SqlHelper.GetCountNumber(tableName, "id", string.Format("organizationId='{0}'", loginingUser.OrganizationId));
            if (totalCounts > 0)
            {
                AspNetPager1.RecordCount = totalCounts;
                gv.DataSource = SqlHelper.GetPagedData(tableName, string.Format("organizationId='{0}'", loginingUser.OrganizationId), sortExpression, isASCDirection,
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
            dt.Columns.Add("cname");
            dt.Columns.Add("lable");
            dt.Columns.Add("isExtension");
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
            gridviewbind(ddlExtTable.SelectedValue + "Column");
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
            gridviewbind(ddlExtTable.SelectedValue + "Column");
        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            AspNetPager1.PageSize = Convert.ToInt32(ddlpagesize.SelectedValue);
            gridviewbind(ddlExtTable.SelectedValue + "Column");
        }

        protected void ddlExtTable_SelectedIndexChanged(object sender, EventArgs e)
        {
            gridviewbind(ddlExtTable.SelectedValue + "Column");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            AllUser loginingUser = (AllUser)Session["loginingUser"];
            if ((int)SqlHelper.GetCountNumber("MeetingRoomTypeColumn", "id", string.Format("cname='{0}' and organizationId='{1}'", txtCname.Text.Trim(), loginingUser.OrganizationId)) != 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('扩展字段名称重复！')", true);
                return;
            }
            String tableName = ddlExtTable.SelectedValue;
            String cname = txtCname.Text.Trim();
            String lable = txtLable.Text.Trim();
            String type = ddlType.SelectedValue;
            String length = txtLength.Text.Trim();
            //检测租户扩展列是否用完
            string sql = string.Format("SELECT UseEC FROM {0} WHERE organizationId='{1}'", tableName + "ExtInfo", loginingUser.OrganizationId);
            int useEC = (int)SqlHelper.ExecuteScalar(sql, CommandType.Text);
            if (useEC < 2)//扩展列未用尽，使用扩展列实现数据扩展
            {
                //将扩展数据元数据存入扩展元数据表
                MeetingRoomTypeEMT mrtEMT = new MeetingRoomTypeEMT();
                mrtEMT.OrganizationId = loginingUser.OrganizationId;
                mrtEMT.Cname = "c" + (useEC + 1);
                mrtEMT.Lable = lable;
                mrtEMT.Type = type + "(" + length + ")";
                MeetingRoomTypeEMTDAL.Insert(mrtEMT);
                //更新扩展信息表
                MeetingRoomTypeExtInfo mrtExtInfo = MeetingRoomTypeExtInfoDAL.GetByOrgId(loginingUser.OrganizationId);
                mrtExtInfo.UseEC+=1;
                MeetingRoomTypeExtInfoDAL.Update(mrtExtInfo);
                //更新被扩展表的租户逻辑列表
                MeetingRoomTypeColumn mrtColumn = new MeetingRoomTypeColumn();
                mrtColumn.OrganizationId = loginingUser.OrganizationId;
                mrtColumn.Cname = "c" + (useEC + 1);
                mrtColumn.Lable = lable;
                mrtColumn.IsExtension = "Y";
                MeetingRoomTypeColumnDAL.Insert(mrtColumn);
                //刷新页面
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "refrash", "<script>Reload();</script>", false);
            }
            else//扩展列用尽，使用扩展表实现数据扩展
            {
                //将扩展数据元数据存入扩展元数据表
                MeetingRoomTypeEMT mrtEMT = new MeetingRoomTypeEMT();
                mrtEMT.OrganizationId = loginingUser.OrganizationId;
                mrtEMT.Cname = cname;
                mrtEMT.Lable = lable;
                mrtEMT.Type = type + "(" + length + ")";
                MeetingRoomTypeEMTDAL.Insert(mrtEMT);
                //更新扩展信息表
                MeetingRoomTypeExtInfo mrtExtInfo = MeetingRoomTypeExtInfoDAL.GetByOrgId(loginingUser.OrganizationId);
                mrtExtInfo.IsUseET = "Y";
                MeetingRoomTypeExtInfoDAL.Update(mrtExtInfo);
                //更新被扩展表的租户逻辑列表
                MeetingRoomTypeColumn mrtColumn = new MeetingRoomTypeColumn();
                mrtColumn.OrganizationId = loginingUser.OrganizationId;
                mrtColumn.Cname = cname;
                mrtColumn.Lable = lable;
                mrtColumn.IsExtension = "Y";
                MeetingRoomTypeColumnDAL.Insert(mrtColumn);
                //刷新页面
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "refrash", "<script>Reload();</script>", false);
            }
        }
    }
}