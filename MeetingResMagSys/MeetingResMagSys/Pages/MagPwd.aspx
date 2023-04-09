<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MagPwd.aspx.cs" Inherits="MeetingResMagSys.Pages.MagPwd" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>密码修改</title>
    <!--js-->
    <script src="../Scripts/jquery.min.js"></script>
    <script src="../Scripts/bootstrap.min.js"></script>
    <script src="../Plugins/toastr/toastr.min.js"></script>
    <script src="../Plugins/validate/jquery.validate.min.js"></script>
    <!--css-->
    <link href="../StyleSheet/bootstrap.css" rel="stylesheet" />
    <link href="../StyleSheet/font-awesome-4.7.0/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../StyleSheet/AdminTheme.css" rel="stylesheet" type="text/css" />
    <link href="../Plugins/toastr/toastr.min.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server" class="form-horizontal">
        <div>
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <!-- Head -->
                    <div class="breadcrumbs" id="breadcrumbs" style="background-color: #ffffff">
                        <ul class="breadcrumb">
                            <li><i class="fa fa-home fa-fw home-icon"></i><a href="#">个人资料管理</a></li>
                            <li class="active">密码修改</li>
                        </ul>
                    </div>
                    <!-- Content -->
                    <div class="space-8"></div>
                    <div class="form-group">
                        <label class="col-sm-2 col-sm-offset-3 control-label"><span style="color: red">*</span>原密码：</label>
                        <div class="col-sm-3">
                            <asp:TextBox ID="txtOldPwd" class="form-control" runat="server" TextMode="Password" name="oldPwd"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-2 col-sm-offset-3 control-label"><span style="color: red">*</span>新密码：</label>
                        <div class="col-sm-3">
                            <asp:TextBox ID="txtNewPwd" class="form-control" runat="server" TextMode="Password" name="newPwd"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-2 col-sm-offset-3 control-label"><span style="color: red">*</span>再次输入新密码：</label>
                        <div class="col-sm-3">
                            <asp:TextBox ID="txtConfirmPwd" class="form-control" runat="server" TextMode="Password" name="confirmPwd"></asp:TextBox>
                        </div>
                    </div>
                    <div class="space-8"></div>
                    <div class="form-group">
                        <label class="col-sm-2 col-sm-offset-3 control-label"></label>
                        <div class="col-sm-3">
                            <div class="text-center">
                                <div class="btn-group">
                                    <asp:Button ID="btnUpdate" runat="server" Text="确认更新" OnClick="btnUpdate_Click" class="btn btn-default" />
                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
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
