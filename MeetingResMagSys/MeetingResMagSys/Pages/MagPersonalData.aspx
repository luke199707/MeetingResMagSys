<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MagPersonalData.aspx.cs" Inherits="MeetingResMagSys.Pages.MagPersonalData" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>个人资料管理</title>
    <script src="../Scripts/jquery.min.js"></script>
    <script src="../Scripts/bootstrap.min.js"></script>
    <script src="../Plugins/toastr/toastr.min.js"></script>
    <link href="../StyleSheet/bootstrap.css" rel="stylesheet" />
    <link href="../StyleSheet/font-awesome-4.7.0/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../StyleSheet/AdminTheme.css" rel="stylesheet" type="text/css" />
    <link href="../Plugins/toastr/toastr.min.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server" class="form-horizontal" >
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <!-- Head -->
                <div class="breadcrumbs" id="breadcrumbs" style="background-color: #ffffff">
                    <ul class="breadcrumb">
                        <li><i class="fa fa-home fa-fw home-icon"></i><a href="#">个人资料管理</a></li>
                        <li class="active">个人资料管理</li>
                    </ul>
                </div>
                <!-- Content -->
                <div class="space-8"></div>
                <div class="form-group">
                    <label class="col-sm-2 col-sm-offset-3 control-label">用户ID</label>
                    <div class="col-sm-3">
                        <asp:TextBox ID="txtUserId" class="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-2 col-sm-offset-3 control-label"><span style="color: red">*</span>用户名</label>
                    <div class="col-sm-3">
                        <asp:TextBox ID="txtName" class="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-2 col-sm-offset-3 control-label">组织ID</label>
                    <div class="col-sm-3">
                        <asp:TextBox ID="txtOrgId" class="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-2 col-sm-offset-3 control-label">组织名</label>
                    <div class="col-sm-3">
                        <asp:TextBox ID="txtOrgName" class="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-2 col-sm-offset-3 control-label">所属部门</label>
                    <div class="col-sm-3">
                        <asp:TextBox ID="txtDepartment" class="form-control" runat="server" ReadOnly="true"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-2 col-sm-offset-3 control-label"><span style="color: red">*</span>邮箱</label>
                    <div class="col-sm-3">
                        <asp:TextBox ID="txtEmail" class="form-control" runat="server" TextMode="Email"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-2 col-sm-offset-3 control-label">电话</label>
                    <div class="col-sm-3">
                        <asp:TextBox ID="txtPhone" class="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-2 col-sm-offset-3 control-label">用户角色</label>
                    <div class="col-sm-3">
                        <asp:TextBox ID="txtRole" class="form-control" runat="server" ReadOnly="true"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-2 col-sm-offset-3 control-label">备注</label>
                    <div class="col-sm-3">
                        <asp:TextBox ID="txtRemark" class="form-control" runat="server" TextMode="MultiLine"></asp:TextBox>
                    </div>
                </div>
                <div class="space-8"></div>
                <div class="form-group">
                    <label class="col-sm-2 col-sm-offset-3 control-label"></label>
                    <div class="col-sm-3">
                        <div class="text-center">
                            <div class="btn-group">
                                <asp:Button ID="btnUpdate" runat="server" Text="修改" OnClick="btnUpdate_Click" class="btn btn-default btn-sm" />
                                <asp:Button ID="btnSave" runat="server" Text="保存" OnClick="btnSave_Click" class="btn btn-default btn-sm" />
                                <asp:Button ID="btnUpdateCancel" runat="server" Text="取消" OnClick="btnUpdateCancel_Click" class="btn btn-default btn-sm" />
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
    <script type="text/javascript"> 
        toastr.options = {
            closeButton: true,
            debug: false,
            progressBar: true,
            positionClass: "toast-top-right",
            timeOut: "5000",
            showMethod: "slideDown",
        };
    </script>
</body>
</html>
