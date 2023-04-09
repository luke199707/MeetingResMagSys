using MeetingResMagSys.DAL;
using MeetingResMagSys.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MeetingResMagSys.Pages
{
    public partial class ModelSet : System.Web.UI.Page
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
                TreeView1.Nodes.Clear();
                LoadToTree("0", TreeView1.Nodes);
                btnIsAble(true, true, true, false, false, false, false);
                TreeView1.Nodes[0].Select();
                txtDisabled();
            }
        }
        private void LoadToTree(string parentId, TreeNodeCollection treeNodeCollection)
        {
            List<FunctionModel> list = new List<FunctionModel>();
            string sql = "select parentId, modelName,currentId from FunctionModel where parentId=@parentId";
            using (SqlDataReader reader = SqlHelper.ExecuteDataReader(sql, System.Data.CommandType.Text, new SqlParameter("@parentId", parentId)))
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        FunctionModel model = new FunctionModel();
                        model.ParentId = reader.GetString(0);
                        model.ModelName = reader.GetString(1);
                        model.CurrentId = reader.GetString(2);
                        list.Add(model);
                    }
                }
            }
            foreach (var item in list)
            {
                //递归加载子节点,加载一个父节点然后递归加载子节点
                TreeNode node = new TreeNode();
                node.Text = item.ModelName;
                node.Value = item.CurrentId;
                treeNodeCollection.Add(node);
                LoadToTree(item.CurrentId, node.ChildNodes);
            }

        }
        /// <summary>
        /// 移除FunctionModel中的数据
        /// </summary>
        /// <param name="parentId"></param>
        private void DelTreeNode(string parentId)
        {
            List<FunctionModel> list = FunctionModelDAL.GetByParentID(parentId);
            if (list.Count != 0)
            {
                foreach (var item in list)
                {
                    DelTreeNode(item.CurrentId);
                }
            }
            FunctionModelDAL.DeleteByCurrentId(parentId);
        }
        protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
        {
            Gridviewbind();
        }
        private void Gridviewbind()
        {
            string code = TreeView1.SelectedValue;
            if (code != null)
            {
                ShowDetail(code);
            }
            txtDisabled();
        }
        private void ShowDetail(string id)
        {
            FunctionModel model = FunctionModelDAL.GetByCurrentID(id);
            if (TreeView1.SelectedNode.Depth == 0)
            {
                txtParentId.Text = "无";
                txtCode.Text = model.CurrentId;
                txtname.Text = model.ModelName;
                txturl.Text = model.Url;
                txtcss.Text = model.Css;
            }
            if (TreeView1.SelectedNode.Depth == 1)
            {
                txtParentId.Text = TreeView1.SelectedNode.Parent.Text.Split(' ').Last();
                txtCode.Text = model.CurrentId;
                txtname.Text = model.ModelName;
                txturl.Text = model.Url;
                txtcss.Text = model.Css;
            }
            if (TreeView1.SelectedNode.Depth == 2)
            {
                txtParentId.Text = TreeView1.SelectedNode.Parent.Text.Split(' ').Last();
                txtCode.Text = model.CurrentId;
                txtname.Text = model.ModelName;
                txturl.Text = model.Url;
                txtcss.Text = model.Css;
            }

        }
        private void txtDisabled()
        {
            txtParentId.Attributes.Add("disabled", "disabled");
            txtCode.Attributes.Add("disabled", "disabled");
            txtname.Attributes.Add("disabled", "disabled");
            txturl.Attributes.Add("disabled", "disabled");
            txtcss.Attributes.Add("disabled", "disabled");
        }
        private void txtAble()
        {
            txtname.Attributes.Remove("disabled");
            txturl.Attributes.Remove("disabled");
            txtcss.Attributes.Remove("disabled");
        }
        private void btnIsAble(bool btnAdd, bool btnUpdate, bool btnDel, bool btnAddSubmit, bool btnAddCancel, bool btnUpSubmit, bool btnUpCancel)
        {
            this.btnAdd.Visible = btnAdd;
            this.btnUpdate.Visible = btnUpdate;
            this.btnDel.Visible = btnDel;
            this.btnAddSubmit.Visible = btnAddSubmit;
            this.btnAddCancel.Visible = btnAddCancel;
            this.btnUpSubmit.Visible = btnUpSubmit;
            this.btnUpCancel.Visible = btnUpCancel;
        }
        private void clearTxt()
        {
            txtname.Text = "";
            txturl.Text = "";
            txtcss.Text = "";
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (TreeView1.SelectedValue == "")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('未选中任何节点！');", true);
                return;
            }
            if (TreeView1.SelectedNode.Depth == 2)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('不能添加下级节点！');", true);
                return;
            }
            string parentId = TreeView1.SelectedValue;
            string max = "00";
            string strCode;
            List<FunctionModel> list = FunctionModelDAL.GetByParentID(parentId);
            if (list.Count != 0)
            {
                foreach (var item in list)
                {
                    if (string.Compare(item.CurrentId, max) > 0)//两字符串相等返回0，不等返回1
                    {
                        max = item.CurrentId;
                    }
                }
                //截取后两位字符串
                string str = max.Substring(max.Length - 2, 2);
                //截取前几位
                strCode = max.Substring(0, max.Length - 2);
                int num = int.Parse(str);
                if (num < 9)
                {//前面多加一个0
                    num += 1;
                    strCode = strCode + "0" + num;
                }
                else
                {
                    num += 1;
                    strCode = strCode + num;
                }
            }
            else
            {
                strCode = "0" + parentId + "001";
            }
            txtParentId.Text = TreeView1.SelectedNode.Text;
            txtCode.Text = strCode;
            clearTxt();
            txtAble();
            btnIsAble(false, false, false, true, true, false, false);
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            txtAble();
            btnIsAble(false, false, false, false, false, true, true);
        }
        protected void btnDel_Click(object sender, EventArgs e)
        {
            if (TreeView1.SelectedNode.Depth == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('不能删除根节点！');", true);
                return;
            }
            else
            {
                string currentId = TreeView1.SelectedValue;
                //移除FunctionModel中的数据
                DelTreeNode(currentId);
                //删除模块节点的时候，roleRight中对应的项也删除
                RoleRightDAL.DeleteByRightCode(currentId);
                //移除treeview中的节点
                DeleteNodes(TreeView1.SelectedNode);
                clearTxt();
                txtParentId.Text = "";
                txtCode.Text = "";
                btnIsAble(true, true, true, false, false, false, false);
                txtDisabled();
            }
        }
        /// <summary>
        /// 递归移除节点
        /// </summary>
        /// <param name="node"></param>
        private void DeleteNodes(TreeNode node)
        {
            //递归删除集合时不能用foreach，因为集合发生了改变
            for (int i = 0; i < node.ChildNodes.Count; i++)
            {
                DeleteNodes(node.ChildNodes[i]);
            }
            node.Parent.ChildNodes.Remove(node);
        }
        //点击添加按钮后的确定按钮
        protected void btnAddSubmit_Click(object sender, EventArgs e)
        {
            string parentId = TreeView1.SelectedValue;
            string currentId = txtCode.Text.Trim();
            string name = txtname.Text.Trim();
            string url = txturl.Text.Trim();
            string css = txtcss.Text.Trim();
            string code = "0" + currentId;
            FunctionModel model = new FunctionModel();
            model.ParentId = parentId;
            model.CurrentId = currentId;
            model.ModelName = name;
            model.Url = url;
            model.Css = css;
            model.Target = "right";
            model.ChildId = code;
            model.Time = DateTime.Now;
            if ((int)FunctionModelDAL.Insert(model) > 0)
            {
                btnIsAble(true, true, true, false, false, false, false);
                txtDisabled();
                TreeNode node = new TreeNode();
                node.Text = model.ModelName;
                node.Value = model.CurrentId;
                if (TreeView1.SelectedNode != null)
                {
                    TreeView1.SelectedNode.ChildNodes.Add(node);
                }
                Gridviewbind();
                // 将新添加的模块加入超级管理员角色的权限之中
                RoleRight rr = new RoleRight();
                rr.RoleId = "R202106280001";
                rr.RoleName = "服务提供商";
                if (currentId.Length==2)
                {
                    rr.RightCode = "0" + currentId;
                }
                else
                {
                    rr.RightCode = currentId;
                }
                rr.OrganizationId = "0";
                rr.OrganizationName = "0";
                RoleRightDAL.Insert(rr);
            }
        }
        //点击添加按钮后的取消按钮
        protected void btnAddCancel_Click(object sender, EventArgs e)
        {
            btnIsAble(true, true, true, false, false, false, false);
            txtDisabled();
        }
        //点击修改后的保存按钮
        protected void btnUpSubmit_Click(object sender, EventArgs e)
        {
            string CurrentId = TreeView1.SelectedValue;
            FunctionModel model = FunctionModelDAL.GetByCurrentID(CurrentId);
            if (model != null)
            {
                model.ModelName = txtname.Text.Trim();
                model.Url = txturl.Text.Trim();
                model.Css = txtcss.Text.Trim();
                model.Time = DateTime.Now;
                FunctionModelDAL.Update(model);
                TreeView1.SelectedNode.Text = model.ModelName;
                TreeView1.SelectedNode.Value = model.CurrentId;
                btnIsAble(true, true, true, false, false, false, false);
                txtDisabled();
                Gridviewbind();
            }
        }
        //点击修改后的取消按钮
        protected void btnUpCancel_Click(object sender, EventArgs e)
        {
            btnIsAble(true, true, true, false, false, false, false);
            txtDisabled();
        }
    }
}