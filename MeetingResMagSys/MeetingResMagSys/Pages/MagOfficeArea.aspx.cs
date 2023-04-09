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
    public partial class MagOfficeArea : System.Web.UI.Page
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
            }
        }
        private void gridviewbind()
        {
            AllUser loginingUser = (AllUser)Session["loginingUser"];
            string sortExpression = gv.Attributes["SortExpression"] == null ? "officeAreaId" : gv.Attributes["SortExpression"];
            bool isASCDirection = true;
            if (gv.Attributes["SortDirection"] == "DESC")
            {
                isASCDirection = false;
            }
            int totalCounts = (int)SqlHelper.GetCountNumber("OfficeArea", "id", string.Format("organizationId='{0}'", loginingUser.OrganizationId));
            if (totalCounts > 0)
            {
                AspNetPager1.RecordCount = totalCounts;
                gv.DataSource = SqlHelper.GetPagedData("OfficeArea",string.Format("organizationId='{0}'", loginingUser.OrganizationId), sortExpression, isASCDirection,
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
            string sortExpression = gv.Attributes["SortExpression"] == null ? "officeAreaId" : gv.Attributes["SortExpression"];
            bool isASCDirection = true;
            if (gv.Attributes["SortDirection"] == "DESC")
            {
                isASCDirection = false;
            }
            int totalCounts = (int)SqlHelper.GetCountNumber("OfficeArea", "id", string.Format("organizationId='{0}' and {1} like '%{2}%'", loginingUser.OrganizationId,DBFiled,value));
            if (totalCounts > 0)
            {
                AspNetPager1.RecordCount = totalCounts;
                gv.DataSource = SqlHelper.GetPagedData("OfficeArea", string.Format("organizationId='{0}' and {1} like '%{2}%'", loginingUser.OrganizationId,DBFiled,value), sortExpression, isASCDirection,
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
            dt.Columns.Add("officeAreaId");
            dt.Columns.Add("name");
            dt.Columns.Add("address");
            dt.Columns.Add("phone");
            dt.Columns.Add("serviceDirector");
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
            ////取出数据
            //OfficeArea model = OfficeAreaDAL.GetById(id);
            ////隐藏的角色代码
            //hiddenid.Text = model.Id.ToString();
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
        protected void BindDDL()
        {
            //两个模态框中的下拉框一起绑定
            AllUser loginingUser = (AllUser)Session["loginingUser"];
            if (loginingUser!=null)
            {
                DataTable dt = SqlHelper.Select("name,userId", "AllUser", string.Format("organizationId='{0}' and available='可用'", loginingUser.OrganizationId), "userId ASC");
                //AddModal中的
                ddlserviceDirector.DataSource = dt;
                ddlserviceDirector.DataTextField = "name";
                ddlserviceDirector.DataValueField = "userId";
                ddlserviceDirector.DataBind();
                //EditModal中的
                eddlserviceDirector.DataSource = dt;
                eddlserviceDirector.DataTextField = "name";
                eddlserviceDirector.DataValueField = "userId";    
                eddlserviceDirector.DataBind();
            }
            ListItem item = new ListItem("请选择", null);
            ddlserviceDirector.Items.Insert(0, item);
            ddlserviceDirector.SelectedIndex = 0;

            eddlserviceDirector.Items.Insert(0, item);
            eddlserviceDirector.SelectedIndex = 0;
        }
        protected void btnAddCertain_Click(object sender, EventArgs e)
        {
            AllUser loginingUser = (AllUser)Session["loginingUser"];
            if ("".Equals(txtName.Text.Trim()) || ddlserviceDirector.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('信息填写不全！')", true);
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "refrash", "<script>Reload();</script>", false);
                return;
            }
            if ((int)SqlHelper.GetCountNumber("OfficeArea", "id", string.Format("name='{0}' and organizationId='{1}'", txtName.Text.Trim(), loginingUser.OrganizationId)) != 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('区域名重复！')", true);
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "refrash", "<script>Reload();</script>", false);
                return;
            }
            OfficeArea oa = new OfficeArea();
            oa.OfficeAreaId = OfficeAreaDAL.CreateOfficeAreaId();
            oa.Name = txtName.Text.Trim();
            oa.Address = txtAddress.Text.Trim();
            oa.Phone = txtPhone.Text.Trim();
            oa.ServiceDirector = ddlserviceDirector.SelectedValue;
            oa.Introduction = txtIntroduction.Text.Trim();
            oa.OrganizationId = loginingUser.OrganizationId;
            oa.Remark = txtRemark.Text.Trim();
            if ((int)OfficeAreaDAL.Insert(oa)!=0)
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
            int id = int.Parse((sender as LinkButton).CommandArgument);
            OfficeArea oa = OfficeAreaDAL.GetById(id);
            hiddenid.Text = oa.OfficeAreaId;
            HiddenName.Value = oa.Name;
            etxtName.Text = oa.Name;
            etxtAddress.Text = oa.Address;
            etxtPhone.Text = oa.Phone;
            eddlserviceDirector.SelectedValue = oa.ServiceDirector;
            etxtIntroduction.Text = oa.Introduction;
            etxtRemark.Text = oa.Remark;
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "", "<script>showEditModal();</script>", false);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            AllUser loginingUser = (AllUser)Session["loginingUser"];
            if ("".Equals(etxtName.Text.Trim()) || eddlserviceDirector.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('区域名和区域地址不能为空！')", true);
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "refrash", "<script>Reload();</script>", false);
                return;
            }
            if ((int)SqlHelper.GetCountNumber("OfficeArea", "id", string.Format("name = '{0}' and organizationId ='{1}' and name<>'{2}'", etxtName.Text.Trim(), loginingUser.OrganizationId, HiddenName.Value)) != 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('区域名重复！')", true);
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "refrash", "<script>Reload();</script>", false);
                return;
            }
            string oaid = hiddenid.Text;
            OfficeArea oa = OfficeAreaDAL.GetByOAId(oaid);
            oa.Name = etxtName.Text.Trim();
            oa.Address = etxtAddress.Text.Trim();
            oa.Phone = etxtPhone.Text.Trim();
            oa.ServiceDirector = eddlserviceDirector.SelectedValue;
            oa.Introduction = etxtIntroduction.Text.Trim();
            oa.Remark = etxtRemark.Text.Trim();
            if (OfficeAreaDAL.Update(oa)>0)
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
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "toastr.warning('区域名称搜索输入框为空','搜索警告');", true);
                return;
            }
            gridviewbind("name", txtSearchName.Text.Trim());
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "toastr.info('新结果显示');", true);
        }
        protected string GetServiceDirectorName(string serviceDirector)
        {
            AllUser model = AllUserDAL.GetByUserId(serviceDirector);
            if (model == null)
            {
                return null;
            }
            return model.Name;
        }
    }
}