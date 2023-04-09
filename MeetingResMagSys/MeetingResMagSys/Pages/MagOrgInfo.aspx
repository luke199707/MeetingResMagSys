<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MagOrgInfo.aspx.cs" Inherits="MeetingResMagSys.Pages.MagOrgInfo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>组织信息管理</title>
    <script src="../Scripts/jquery.min.js"></script>
    <script src="../Scripts/bootstrap.min.js"></script>
    <script src="../Plugins/toastr/toastr.min.js"></script>
    <link href="../StyleSheet/bootstrap.css" rel="stylesheet" />
    <link href="../StyleSheet/font-awesome-4.7.0/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../StyleSheet/AdminTheme.css" rel="stylesheet" type="text/css" />
    <link href="../Plugins/toastr/toastr.min.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server" class="form-horizontal">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <!-- Head -->
                <div class="breadcrumbs" id="breadcrumbs" style="background-color: #ffffff">
                    <ul class="breadcrumb">
                        <li><i class="fa fa-home fa-fw home-icon"></i><a href="#">组织信息管理</a></li>
                        <li class="active">组织信息管理</li>
                    </ul>
                </div>
                <!-- Head End -->
                <!-- Content -->
                <div class="space-8"></div>
                <div class="form-group">
                    <label class="col-sm-2 col-sm-offset-3 control-label">组织ID</label>
                    <div class="col-sm-3">
                        <asp:TextBox ID="txtOrgId" class="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-2 col-sm-offset-3 control-label"><span style="color: red">*</span>组织名称</label>
                    <div class="col-sm-3">
                        <asp:TextBox ID="txtName" class="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-2 col-sm-offset-3 control-label">组织简介</label>
                    <div class="col-sm-3">
                        <asp:TextBox ID="txtIntroduction" class="form-control" runat="server" TextMode="MultiLine" Rows="20"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-2 col-sm-offset-3 control-label">会议预订起始时间</label>
                    <div class="col-sm-3">
                        <asp:DropDownList ID="ddlReseStart" class="form-control" runat="server">
                            <asp:ListItem>01:00</asp:ListItem>
                            <asp:ListItem>02:00</asp:ListItem>
                            <asp:ListItem>03:00</asp:ListItem>
                            <asp:ListItem>04:00</asp:ListItem>
                            <asp:ListItem>05:00</asp:ListItem>
                            <asp:ListItem>06:00</asp:ListItem>
                            <asp:ListItem>07:00</asp:ListItem>
                            <asp:ListItem>08:00</asp:ListItem>
                            <asp:ListItem>09:00</asp:ListItem>
                            <asp:ListItem>10:00</asp:ListItem>
                            <asp:ListItem>11:00</asp:ListItem>
                            <asp:ListItem>12:00</asp:ListItem>
                            <asp:ListItem>13:00</asp:ListItem>
                            <asp:ListItem>14:00</asp:ListItem>
                            <asp:ListItem>15:00</asp:ListItem>
                            <asp:ListItem>16:00</asp:ListItem>
                            <asp:ListItem>17:00</asp:ListItem>
                            <asp:ListItem>18:00</asp:ListItem>
                            <asp:ListItem>19:00</asp:ListItem>
                            <asp:ListItem>20:00</asp:ListItem>
                            <asp:ListItem>21:00</asp:ListItem>
                            <asp:ListItem>22:00</asp:ListItem>
                            <asp:ListItem>23:00</asp:ListItem>
                            <asp:ListItem>24:00</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-2 col-sm-offset-3 control-label">会议预订结束时间</label>
                    <div class="col-sm-3">
                        <asp:DropDownList ID="ddlReseEnd" class="form-control" runat="server">
                            <asp:ListItem>01:00</asp:ListItem>
                            <asp:ListItem>02:00</asp:ListItem>
                            <asp:ListItem>03:00</asp:ListItem>
                            <asp:ListItem>04:00</asp:ListItem>
                            <asp:ListItem>05:00</asp:ListItem>
                            <asp:ListItem>06:00</asp:ListItem>
                            <asp:ListItem>07:00</asp:ListItem>
                            <asp:ListItem>08:00</asp:ListItem>
                            <asp:ListItem>09:00</asp:ListItem>
                            <asp:ListItem>10:00</asp:ListItem>
                            <asp:ListItem>11:00</asp:ListItem>
                            <asp:ListItem>12:00</asp:ListItem>
                            <asp:ListItem>13:00</asp:ListItem>
                            <asp:ListItem>14:00</asp:ListItem>
                            <asp:ListItem>15:00</asp:ListItem>
                            <asp:ListItem>16:00</asp:ListItem>
                            <asp:ListItem>17:00</asp:ListItem>
                            <asp:ListItem>18:00</asp:ListItem>
                            <asp:ListItem>19:00</asp:ListItem>
                            <asp:ListItem>20:00</asp:ListItem>
                            <asp:ListItem>21:00</asp:ListItem>
                            <asp:ListItem>22:00</asp:ListItem>
                            <asp:ListItem>23:00</asp:ListItem>
                            <asp:ListItem>24:00</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-2 col-sm-offset-3 control-label">会议预订时间间隔(分钟)</label>
                    <div class="col-sm-3">
                        <asp:DropDownList ID="ddlTimeUnit" class="form-control" runat="server"></asp:DropDownList>
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
                <!-- Content End -->
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
