<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MagDataDic.aspx.cs" Inherits="MeetingResMagSys.Pages.MagDataDic" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="../Scripts/jquery-3.4.1.min.js"></script>
    <script src="../Scripts/bootstrap.min.js"></script>
    <link href="../StyleSheet/bootstrap.css" rel="stylesheet" />
    <link href="../Plugins/toastr/toastr.min.css" rel="stylesheet" />
    <script src="../Plugins/toastr/toastr.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel runat="server" ID="UpdatePanel1">
            <ContentTemplate>
                <div class="breadcrumbs" id="breadcrumbs">
                    <ul class="breadcrumb">
                        <li><i class="fa fa-home fa-fw home-icon"></i><a href="#">系统管理</a> </li>
                        <li class="active">数据字典维护</li>
                    </ul>
                </div>
                <!-- .breadcrumb -->
                <div class="page-content">
                    <div class="row">
                        <div class="col-xs-12">
                            <!-- PAGE CONTENT BEGINS -->
                            <div class="row">
                                <div class="col-xs-3 col-xs-offset-2">
                                    <div class="panel panel-default" style="min-height: 400px;">
                                        <div class="panel-body">
                                            <asp:Panel ID="panelUser" runat="server" ScrollBars="Auto">
                                                <asp:TreeView ID="TreeViewDictionary" runat="server" OnSelectedNodeChanged="TreeView1_SelectedNodeChanged"
                                                    NodeWrap="True" ShowLines="True" ExpandDepth="1" EnableClientScript="False" onclick="SetScrollTop(0,'panelUser','PanelScroll');">
                                                    <NodeStyle NodeSpacing="0px" VerticalPadding="2px" />
                                                    <ParentNodeStyle />
                                                    <SelectedNodeStyle BackColor="#B5B5B5" Font-Underline="False" VerticalPadding="2px" />
                                                </asp:TreeView>
                                            </asp:Panel>
                                        </div>
                                        <asp:HiddenField ID="PanelScroll" runat="server" />
                                    </div>
                                </div>
                                <div class="col-xs-5 form-horizontal">
                                    <asp:TextBox runat="server" class="form-control input-sm" ID="txtParentCode" Visible="false"></asp:TextBox>
                                    <div class="row form-group">
                                        <label runat="server" id="label2" class="col-xs-3 col-xs-offset-1 control-label"
                                            for="txtParentName">
                                            上级节点名称：</label>
                                        <div class="col-xs-6" style="padding-left: 0px;">
                                            <asp:TextBox runat="server" class="form-control input-sm" ID="txtParentName" disabled></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row form-group">
                                        <label runat="server" id="labelParentNode" class="col-xs-3 col-xs-offset-1 control-label"
                                            for="txtParentNode">
                                            当前节点编码：</label>
                                        <div class="col-xs-6" style="padding-left: 0px;">
                                            <asp:TextBox runat="server" class="form-control input-sm" ID="txtCode"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row form-group">
                                        <label runat="server" id="labelNode" class="col-xs-3 col-xs-offset-1 control-label"
                                            for="txtName">
                                            当前节点名称：</label>
                                        <div class="col-xs-6" style="padding-left: 0px;">
                                            <asp:TextBox runat="server" class="form-control input-sm" ID="txtName"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row form-group">
                                        <label runat="server" id="label1" class="col-xs-3 col-xs-offset-1 control-label"
                                            for="txtRemark">
                                            备注：</label>
                                        <div class="col-xs-6" style="padding-left: 0px;">
                                            <asp:TextBox runat="server" class="form-control input-sm" ID="txtRemark"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row" style="margin-bottom: 5px; margin-top: 5px;">
                                        <div class="col-xs-6 col-xs-offset-3 text-center">
                                            <br />
                                            <div class="btn-group">
                                                <asp:Button ID="btnAdd" runat="server" Text=" 添加" OnClick="btnAdd_Click" class="btn btn-default btn-sm" />
                                                <asp:Button ID="btnCertain" runat="server" Text="确定" OnClick="btnCertain_Click" class="btn btn-default btn-sm" />
                                                <asp:Button ID="btnAddCancel" runat="server" Text="取消" OnClick="btnAddCancel_Click" class="btn btn-default btn-sm" />
                                                <asp:Button ID="btnUpdate" runat="server" Text="修改" OnClick="btnUpdate_Click" class="btn btn-default btn-sm" />
                                                <asp:Button ID="btnSave" runat="server" Text="保存" OnClick="btnSave_Click" class="btn btn-default btn-sm" />
                                                <asp:Button ID="btnUpdateCancel" runat="server" Text="取消" OnClick="btnUpdateCancel_Click" class="btn btn-default btn-sm" />
                                                <asp:Button ID="btnDel" runat="server" Text="删除" OnClick="btnDel_Click" class="btn btn-default btn-sm"
                                                    OnClientClick="javascript:return confirm('是否删除该项及其下的所有管理菜单？');" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <!-- PAGE CONTENT ENDS -->
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
    <script type="text/javascript">
        function SetScrollTop(type, panelId, hiddenId) {
            if (type.toString() == "0") {
                $("#" + hiddenId).val($("#" + panelId).scrollTop());
            }
            else {
                $("#" + panelId).scrollTop($("#" + hiddenId).val());
                $("#" + panelId).scrollTop($("#" + hiddenId).val());
            }
        }
    </script>
</body>
</html>
