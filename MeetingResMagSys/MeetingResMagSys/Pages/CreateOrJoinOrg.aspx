<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreateOrJoinOrg.aspx.cs" Inherits="MeetingResMagSys.Pages.CreateOrJoinOrg" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>创建或加入组织页</title>
    <link href="../StyleSheet/zjhCreate/CreateOrJoinOrg.css" rel="stylesheet" />
    <script src="../Scripts/jquery-3.4.1.js"></script>
    <script src="../Plugins/toastr/toastr.min.js"></script>
    <link href="../Plugins/toastr/toastr.min.css" rel="stylesheet" />
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
                        <div id="divHead" style="display: none;">
                            <h2>你好新用户:<strong><%= username%></strong>，你需要创建或者加入一个组织</h2>
                        </div>
                        <h3></h3>
                    </div>
                    <%--加入组织div--%>
                    <div id="divJoin" class="signup" style="height:350px;">
                        <h4>加入组织</h4>
                        <div class="field">
                            <asp:TextBox CssClass="ZSinput" ID="txtOrgID" runat="server" placeholder="组织ID"></asp:TextBox>
                        </div>
                        <div class="field">
                            <asp:TextBox CssClass="ZSinput" ID="txtJoinOrgName" runat="server" placeholder="组织名"></asp:TextBox>
                        </div>
                        <div class="field">
                            <input type="button" class="ZSbutton" id="btnJoin" value="加入" onclick="Join()" style="background-color: #3c5b9a; color: white; height: 45px" />
                        </div>
                        <div class="field">
                            <a href="javascript:void(0)" onclick="CreateOrg()" style="color:#3c5b9a">想要创建自己的组织？</a>
                        </div>
                    </div>
                    <%--加入组织div结束--%>
                    <%--创建组织div--%>
                    <div id="divCreate" class="signup" style="display: none">
                        <h4>创建组织</h4>
                        <div class="field">
                            <asp:TextBox CssClass="ZSinput" ID="txtCreateOrgName" runat="server" placeholder="组织名称"></asp:TextBox>
                        </div>
                        <div class="field">
                            <asp:TextBox CssClass="ZSinput" ID="txtOrgIntroduction" runat="server" TextMode="MultiLine" placeholder="简介"></asp:TextBox>
                        </div>
                        <div class="field">
                            <input type="button" class="ZSbutton" id="btnCreate" value="创建" onclick="Create()" style="background-color: #3c5b9a; color: white; height: 45px" />
                        <div class="field">
                            <a href="javascript:void(0)" onclick="JoinOrg()" style="color:#3c5b9a">已找到组织准备加入？</a>
                        </div>
                    </div>
                    <%--创建组织div结束--%>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#divHead").show(1000);
        });
        toastr.options = {
            closeButton: true,
            debug: false,
            progressBar: true,
            positionClass: "toast-top-center",
            timeOut: "5000",
            showMethod: "slideDown",
        };
        function CreateOrg() {
            $("#divJoin").hide(500);
            $("#divCreate").show(1000);
        }

        function JoinOrg() {
            $("#divCreate").hide(500);
            $("#divJoin").show(1000);
        }
        function Join() {
            var orgId = $("#txtOrgID").val();
            var orgName = $("#txtJoinOrgName").val();
            if (orgName == "" || orgId == "") {
                toastr.warning('信息填写不全！', '加入组织警告');
                return;
            }
            $.post("../Handler/JoinOrg.ashx", { "orgId": orgId, "orgName": orgName }, function (data) {
                if (data == "org_null") {
                    toastr.warning('组织不存在！', '加入组织警告');
                }
                else if (data == "success") {
                    alert("成功加入组织！");
                    window.parent.frames.location.href = "../Layout/Default.html";
                }
            });
        }
        function Create() {
            var orgName = $("#txtCreateOrgName").val();
            var orgIntro = $("#txtOrgIntroduction").val();
            if (orgName == "") {
                toastr.warning('组织名不能为空！', '创建组织警告');
                return;
            } 
            $.post("../Handler/CreateOrg.ashx", { "orgName": orgName, "orgIntro": orgIntro }, function (data) {
                if (data == "nr") {
                    toastr.warning('组织名重复！', '创建组织警告');
                }
                else if (data == "fail") {
                    toastr.danger('组织创建失败！');
                }
                else {
                    $("#txtCreateOrgName").val("");
                    $("#txtOrgIntroduction").val("");
                    alert("组织创建成功！");
                    window.parent.frames.location.href = "../Layout/Default.html";
                }
            });
        }
    </script>
</html>
