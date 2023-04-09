<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="MeetingResMagSys.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>登陆注册页面</title>
    <link href="StyleSheet/zjhCreate/login.css" rel="stylesheet" />
    <script src="Scripts/jquery-3.4.1.js"></script>
    <script src="Plugins/toastr/toastr.min.js"></script>
    <link href="Plugins/toastr/toastr.min.css" rel="stylesheet" />
</head>
<body style="background-color:#3c5b9a">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="main">
                    <div class="top">
                        <div class="header">
                            <h1 class="logo"><a href="#"><img src="#" alt="" "></a></h1>
                        </div>
                        <h2>欢迎使用会议室预定管理系统</h2>
                        <h3>一个简单高效的会议室预订软件</h3>
                    </div>
                    <%--登陆div--%>
                    <div id="divlogin" class="signup" style="height:350px;">
                        <h4>登陆</h4>
                        <div class="field">
                            <asp:TextBox CssClass="ZSinput" ID="txtLoginUsername" runat="server" placeholder="用户名"></asp:TextBox>
                        </div>
                        <div class="field">
                            <asp:TextBox CssClass="ZSinput" ID="txtLoginPwd" runat="server" TextMode="Password" placeholder="密码"></asp:TextBox>
                        </div>
                        <div class="field">
                            <asp:Button CssClass="ZSbutton" ID="btnLogin" runat="server" OnClick="btnLogin_Click" Text="登陆" style="background-color:#3c5b9a;color:white;height:45px" />
                        </div>
                        <div class="field">
                            <a href="javascript:void(0)" onclick="ForgetPwd()" style="color:#3c5b9a">忘记密码？</a>
                            <div style="float:right;">
                                <a href="javascript:void(0)" onclick="showSignUp()" style="color:#3c5b9a">注册账号？</a>
                            </div>
                        </div>
                    </div>
                    <%--登陆div结束--%>
                    <%--注册div--%>
                    <div id="divSignup" class="signup" style="display: none;height:450px;">
                        <h4>注册</h4>
                        <div class="field">
                            <asp:TextBox CssClass="ZSinput" ID="txtSignupUsername" runat="server" placeholder="用户名"></asp:TextBox>
                        </div>
                        <div class="field">
                            <asp:TextBox CssClass="ZSinput" ID="txtSignupEmail" runat="server" TextMode="Email" placeholder="邮箱"></asp:TextBox>
                        </div>
                        <div class="field">
                            <asp:TextBox CssClass="ZSinput" ID="txtSignupPwd" runat="server" TextMode="Password" placeholder="密码"></asp:TextBox>
                        </div>
                        <div class="field">
                            <asp:TextBox CssClass="ZSinput" ID="txtRepeatPwd" runat="server" TextMode="Password" placeholder="再次输入密码"></asp:TextBox>
                        </div>
                        <div class="field">
                            <input type="button" class="ZSbutton" id="btnSignup" value="确认注册" onclick="SignUp()" style="background-color: #3c5b9a; color: white; height: 45px" />
                        </div>
                        <div class="field">
                            <a href="javascript:void(0)" onclick="ReturnSignup2()" style="color:#3c5b9a">返回登陆</a>
                        </div>
                    </div>
                    <%--注册div结束--%>
                    <%--密码找回div--%>
                    <div id="divResetPwd" class="signup" style="display: none;height:300px;">
                        <h4>密码重置</h4>
                        <div class="field">
                            <asp:TextBox Class="ZSinput" ID="txtResetPwdEmail" runat="server" TextMode="Email" placeholder="邮箱"></asp:TextBox>
                        </div>
                        <div class="field">
                            <input type="button" class="ZSbutton" id="btnResetPwd" value="确认重置" onclick="ResetPwd()" style="background-color: #3c5b9a; color: white; height: 45px" />
                        </div>
                        <div class="field">
                            <a href="javascript:void(0)" onclick="ReturnSignup()" style="color:#3c5b9a">返回登陆</a>
                        </div>
                    </div>
                    <%--密码找回div结束--%>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
    <script type="text/javascript">
        toastr.options = {
            closeButton: true,
            debug: false,
            progressBar: true,
            positionClass: "toast-top-center",
            timeOut: "5000",
            showMethod: "slideDown",
        };
        function ForgetPwd() {
            $("#divlogin").hide(500);
            $("#divResetPwd").show(1000);
        }
        function showSignUp() {
            $("#divlogin").hide(500);
            $("#divSignup").show(1000);
        }
        function ReturnSignup() {
            $("#divResetPwd").hide(500);
            $("#divlogin").show(1000);
        }
        function ReturnSignup2() {
            $("#divSignup").hide(500);
            $("#divlogin").show(1000);
        }

        function SignUp() {
            var username = $("#txtSignupUsername").val();
            var pwd = $("#txtSignupPwd").val();
            var repPwd = $("#txtRepeatPwd").val();
            var email = $("#txtSignupEmail").val();
            if (username == "" || pwd == "" || repPwd == "" || email == "") {
                toastr.warning('信息填写不全！', '注册警告');
                return;
            }
            if (repPwd != pwd) {
                toastr.warning('两次密码输入不一致！', '注册警告');
                return;
            }
            $.post("Handler/SignUp.ashx", { "username": username, "pwd": pwd, "email": email }, function (data) {
                if (data == "username_repeat") {
                    toastr.warning('用户名重复！', '注册警告');
                }
                else if (data == "email_repeat") {
                    toastr.warning('此邮箱已注册！', '注册警告');
                }
                else if (data == "fail") {
                    toastr.danger('注册失败！', '注册错误');
                }
                else if (data == "success"){
                    toastr.success('注册成功，请前往登陆！');
                    ReturnSignup2();
                }
            });
        }

        function ResetPwd() {
            var email = $("#txtResetPwdEmail").val();
            if (email == "") {
                toastr.warning('邮箱不能为空！', '密码重置警告');
                return;
            }
            $.post("Handler/ResetPwd.ashx", { "email": email }, function (data) {
                if (data == "fail") {
                    toastr.warning('此邮箱不存在！');
                }
                if (data == "failSend") {
                    toastr.warning('重置失败！');
                }
                if (data == "success") {
                    $("#txtResetPwdEmail").val("");
                    toastr.success("密码重置成功，请前往邮箱查收！");
                }
            });
        }
    </script>
</html>
