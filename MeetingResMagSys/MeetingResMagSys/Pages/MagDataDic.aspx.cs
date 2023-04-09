using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MeetingResMagSys.DAL;
using MeetingResMagSys.Model;
namespace MeetingResMagSys.Pages
{
    public partial class MagDataDic : System.Web.UI.Page
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
                BindTree();
                TreeViewDictionary.ExpandDepth = 1;
                TreeViewDictionary.Nodes[0].Select();
                gridviewbind();
            }
        }

        private void gridviewbind()
        {
            EmptyTxt();
            if (TreeViewDictionary.SelectedNode != null)
            {
                ShowDetail(Convert.ToInt32(TreeViewDictionary.SelectedValue));
            }
            //显示一条数据的详细信息
            IsBtnVisible(true, false, false, true, false, false, true);
            DisabledTxt();
        }

        private void ShowDetail(int id)
        {
            DataDictionary model = DataDictionaryDAL.GetById(id);
            if (TreeViewDictionary.SelectedNode.Depth == 0)
            {
                txtParentCode.Text = "";
                txtParentName.Text = "无";
            }
            if (TreeViewDictionary.SelectedNode.Depth == 1)
            {
                txtParentCode.Text = "1";
                txtParentName.Text = "数据字典根节点";
            }
            if (TreeViewDictionary.SelectedNode.Depth == 2)
            {
                DataDictionary dinfo = DataDictionaryDAL.GetById(Convert.ToInt32(model.ParentId));
                txtParentCode.Text = model.ParentId;
                txtParentName.Text = dinfo.Name;
            }
            txtName.Text = model.Name;
            txtCode.Text = model.Code;
            txtRemark.Text = model.Remark;
        }
        private void BindTree()
        {
            TreeViewDictionary.Nodes.Clear();
            List<DataDictionary> DataDicList = DataDictionaryDAL.GetListById(1);
            LoadDataToTree(TreeViewDictionary.Nodes, DataDicList);
            IsBtnVisible(true, false, false, true, false, false, true);
        }

        private void LoadDataToTree(TreeNodeCollection treeNodeCollection, List<DataDictionary> DataDicList)
        {
            if (DataDicList != null)
            {
                foreach (DataDictionary item in DataDicList)
                {
                    TreeNode node = new TreeNode();
                    node.Text = item.Name;
                    node.Value = item.Id.ToString();
                    node.ToolTip = item.Name;
                    treeNodeCollection.Add(node);
                    List<DataDictionary> childrenlist = DataDictionaryDAL.GetListByParentId(item.Id.ToString());
                    if (childrenlist != null)
                    {
                        LoadDataToTree(node.ChildNodes, childrenlist);
                    }
                }
            }
        }

        private void IsBtnVisible(bool Add, bool Certain, bool AddCancel, bool Update, bool Save, bool UpdateCancel, bool Del)
        {
            btnAdd.Visible = Add;
            btnCertain.Visible = Certain;
            btnAddCancel.Visible = AddCancel;
            btnUpdate.Visible = Update;
            btnSave.Visible = Save;
            btnUpdateCancel.Visible = UpdateCancel;
            btnDel.Visible = Del;
        }
        private void txtClear()
        {
            txtName.Text = "";
            txtRemark.Text = "";
        }
        //添加
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (TreeViewDictionary.SelectedNode == null)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "onekey", "alert('请选择节点！')", true);
                return;
            }
            if (TreeViewDictionary.SelectedNode.Depth == 2)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "onekey", "alert('此节点下不能再添加子节点！')", true);
                return;
            }
            if (TreeViewDictionary.SelectedNode.Depth == 0)
            {
                //说明添加的是一级节点
                txtParentName.Text = "数据字典根节点";
                txtCode.Text = GetFirstLevelCode();
            }
            if (TreeViewDictionary.SelectedNode.Depth == 1)
            {
                //说明添加的是二级节点
                txtParentName.Text = txtName.Text;
                txtCode.Text = GetSecondLevelCode();
            }
            txtClear();
            EnabledTxt();
            IsBtnVisible(false, true, true, false, false, false, false);
        }

        //确定添加
        protected void btnCertain_Click(object sender, EventArgs e)
        {
            if (TreeViewDictionary.SelectedNode == null)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "onekey", "alert('请选择节点！')", true);
                return;
            }
            string parentid = TreeViewDictionary.SelectedValue;
            if (txtName.Text == "")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "onekey", "alert('名称不能为空！')", true);
                return;
            }
            if ((int)SqlHelper.GetCountNumber("DataDictionary", "id", string.Format("name='{0}' and parentId='1'", txtName.Text.Trim())) != 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "onekey", "alert('该名称已经存在！')", true);
                return;
            }
            DataDictionary model = new DataDictionary();
            model.ParentId = parentid;
            model.Name = txtName.Text.Trim();
            model.Code = txtCode.Text.Trim();
            model.Remark = txtRemark.Text.Trim();
            int outId = Convert.ToInt32(DataDictionaryDAL.Insert(model));
            if (outId > 0)
            {
                TreeNode node = new TreeNode();
                node.Text = model.Name;
                node.Value = outId.ToString();
                node.ToolTip = model.Name;
                TreeViewDictionary.SelectedNode.ChildNodes.Add(node);
            }
            gridviewbind();
        }
        //生成一级编码
        private string GetFirstLevelCode()
        {
            DataDictionary model = DataDictionaryDAL.GetByParentId("1");
            if (model == null)
            {
                return "001";
            }
            string code = (Convert.ToInt32(model.Code) + 1).ToString();
            if (code.Length == 1)
            {
                code = "00" + code;
            }
            else if (code.Length == 2)
            {
                code = "0" + code;
            }
            return code;
        }
        //生成二级编号
        private string GetSecondLevelCode()
        {
            DataDictionary model = DataDictionaryDAL.GetByCode(txtCode.Text.Trim());
            if (model.Code.Length == 3)
            {
                return model.Code + "001";
            }
            string code = (Convert.ToInt32(model.Code.Substring(3)) + 1).ToString();
            if (code.Length == 1)
            {
                code = "00" + code;
            }
            else if (code.Length == 2)
            {
                code = "0" + code;
            }
            return txtCode.Text.Trim() + code;
        }
        //取消添加
        protected void btnAddCancel_Click(object sender, EventArgs e)
        {
            gridviewbind();
        }
        //修改按钮
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (TreeViewDictionary.SelectedNode.Depth == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "onekey", "alert('根节点不能修改！')", true);
                return;
            }

            if (TreeViewDictionary.SelectedNode == null)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "onekey", "alert('请选择要修改的节点！')", true);
                return;
            }
            IsBtnVisible(false, false, false, false, true, true, false);
            EnabledTxt();
        }

        //保存修改
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (TreeViewDictionary.SelectedNode == null)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "onekey", "alert('请选择要修改的节点！')", true);
                return;
            }
            int id = Convert.ToInt32(TreeViewDictionary.SelectedValue);
            if (txtName.Text == "")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "onekey", "alert('名称不能为空！')", true);
                return;
            }
            if ((int)SqlHelper.GetCountNumber("DataDictionary", "id", string.Format("Name='{0}' and ID<>'{1}'  and parentId='1'", txtName.Text, id)) != 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "onekey", "alert('该名称已经存在！')", true);
                return;
            }
            DataDictionary model = DataDictionaryDAL.GetById(id);
            model.Name = txtName.Text.Trim();
            DataDictionaryDAL.Update(model);
            TreeViewDictionary.SelectedNode.Text = txtName.Text;
            gridviewbind();
        }
        //更新的取消按钮
        protected void btnUpdateCancel_Click(object sender, EventArgs e)
        {
            gridviewbind();
        }
        //删除按钮
        protected void btnDel_Click(object sender, EventArgs e)
        {
            if (TreeViewDictionary.SelectedNode.Depth == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "onekey", "alert('不能删除根节点！')", true);
                return;
            }
            if (TreeViewDictionary.SelectedNode == null)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "onekey", "alert('请选择要删除的节点！')", true);
                return;
            }
            DataDictionary model = DataDictionaryDAL.GetById(Convert.ToInt32(TreeViewDictionary.SelectedValue));
            if (DataDictionaryDAL.DeleteByCode(model.Code) > 0)
            {
                //数据库中删除成功后然后在树中删除节点
                DeleteNodes(TreeViewDictionary.SelectedNode);
            }
        }

        private void DeleteNodes(TreeNode node)
        {
            //递归删除集合时不能用foreach，因为集合发生了改变
            for (int i = 0; i < node.ChildNodes.Count; i++)
            {
                DeleteNodes(node.ChildNodes[i]);
            }
            node.Parent.ChildNodes.Remove(node);
        }

        protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
        {
            gridviewbind();
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "scroll2", "SetScrollTop(1,'panelUser','PanelScroll');", true);
        }
        private void EmptyTxt()
        {
            txtParentCode.Text = "";
            txtCode.Text = "";
            txtName.Text = "";
        }

        private void EnabledTxt()
        {
            txtName.Attributes.Remove("disabled");
            txtRemark.Attributes.Remove("disabled");
        }

        private void DisabledTxt()
        {
            txtCode.Attributes.Add("disabled", "disabled");
            txtName.Attributes.Add("disabled", "disabled");
            txtRemark.Attributes.Add("disabled", "disabled");
        }
    }
}