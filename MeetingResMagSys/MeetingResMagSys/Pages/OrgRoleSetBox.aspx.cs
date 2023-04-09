using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MeetingResMagSys.DAL;
using MeetingResMagSys.Model;

namespace MeetingResMagSys.Pages
{
    public partial class OrgRoleSetBox : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "scroll1", "SetScrollTop(1,'panelUser','PanelScroll');", true);
            if (!IsPostBack)
            {
                BindTree();
            }
            TreeView1.Attributes.Add("onclick", "postBackByObject()");
        }
        //绑定树
        private void BindTree()
        {
            TreeView1.Nodes.Clear();
            LoadToTree("0", TreeView1.Nodes, Request["roleId"]);
        }
        private void LoadToTree(string code, TreeNodeCollection treeNodeCollection, string roleId)
        {
            List<FunctionModel> list = new List<FunctionModel>();
            string sql = "select parentId, modelName,currentId,childId from FunctionModel where parentId=@parentId and modelName<>'租户管理' and modelName<>'系统管理'";
            using (SqlDataReader reader = SqlHelper.ExecuteDataReader(sql, System.Data.CommandType.Text, new SqlParameter("@parentId", code)))
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        FunctionModel model = new FunctionModel();
                        model.ParentId = reader.GetString(0);
                        model.ModelName = reader.GetString(1);
                        model.CurrentId = reader.GetString(2);
                        model.ChildId = reader.GetString(3);
                        list.Add(model);
                    }
                }
            }
            foreach (var item in list)
            {
                //获取角色的所拥有的权限
                List<RoleRight> roleList = RoleRightDAL.GetByRoleId(roleId);
                TreeNode node = new TreeNode();
                node.Text = item.ModelName;
                node.Value = item.CurrentId;
                treeNodeCollection.Add(node);
                //如果角色有该权限，绑定的时候复选框是选中的状态
                foreach (RoleRight roleright in roleList)
                {
                    if (node.Value == roleright.RightCode)
                    {
                        node.Checked = true;
                    }
                    if (node.Checked)
                    {
                        node.Parent.Checked = true;
                        node.Parent.Parent.Checked = true;
                    }
                }
                LoadToTree(item.CurrentId, node.ChildNodes, roleId);
            }
        }
        //将角色权限全部删除，然后再将所选权限全部插入
        protected void btnSave_Click(object sender, EventArgs e)
        {
            AllUser loginingUser = (AllUser)Session["loginingUser"];
            string roleId = Request.QueryString["roleId"];
            string roleName = RoleDAL.GetByRoleId(roleId).RoleName;
            RoleRightDAL.DeleteByRoleId(roleId);
            foreach (TreeNode node in TreeView1.CheckedNodes)
            {
                RoleRight mod = new RoleRight();
                mod.RoleId = roleId;
                if (node.Value == "1")
                {
                    continue;
                }
                if (node.Value.Length == 2)
                {
                    mod.RightCode = "0" + node.Value;
                }
                else
                {
                    mod.RightCode = node.Value;
                }
                mod.RoleName = roleName;
                mod.OrganizationId = loginingUser.OrganizationId;
                RoleRightDAL.Insert(mod);
            }
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "select", "window.parent.AfterSelectProject();", true);
        }

        //复选框发生变化事件
        protected void TreeView1_TreeNodeCheckChanged(object sender, TreeNodeEventArgs e)
        {
            ParentChecked(e.Node);
            ChildsNodesCheckChange(e.Node);
        }

        //迭代遍历如果一个节点是选中那个它的父节点也选中
        private void ParentChecked(TreeNode node)
        {
            if (node.Checked == true)
            {
                if (node.Parent != null)
                {
                    node.Parent.Checked = true;
                    ParentChecked(node.Parent);
                }
            }
            if (node.Checked == false)
            {
                int flag = 0;
                if (node.Parent != null)
                {
                    foreach (TreeNode item in node.Parent.ChildNodes)
                    {
                        if (item.Checked == true)
                        {
                            //代表兄弟节点还有选中的
                            flag = 1;
                            break;
                        }
                    }
                }
                //如果flag == 0，代表兄弟节点都没有选中,父节点也取消选中，继续递归，如果没有取消父节点选中就不需要再递归。
                if (flag == 0)
                {
                    if (node.Parent != null)
                    {
                        node.Parent.Checked = false;
                        ParentChecked(node.Parent);
                    }
                }
            }
        }
        //迭代遍历，如果有子节点，子节点的选中状态和改变节点状态一致
        private void ChildsNodesCheckChange(TreeNode node)
        {
            if (node.Checked == true)
            {
                if (node.ChildNodes.Count > 0)
                {
                    foreach (TreeNode item in node.ChildNodes)
                    {
                        item.Checked = true;
                        ChildsNodesCheckChange(item);
                    }
                }
            }
            else if (node.Checked == false)
            {
                if (node.ChildNodes.Count > 0)
                {
                    foreach (TreeNode item in node.ChildNodes)
                    {
                        item.Checked = false;
                        ChildsNodesCheckChange(item);
                    }
                }
            }
        }
    }
}