<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Top.aspx.cs" Inherits="MeetingResMagSys.Layout.Top" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>顶部样式</title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <meta name="author" content="Luke" />
    <link href="../StyleSheet/bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="../StyleSheet/font-awesome-4.7.0/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../StyleSheet/AdminTheme.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.11.2.min.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="navbar navbar-default" id="navbar" style="background-color:#3c5b9a">
            <div class="navbar-container no-padding-left no-padding-right" id="navbar-container">
                <div class="navbar-header pull-left" style="height: 45px">
                    <a class="navbar-brand"><i class=" " style="font-style: normal;"> <%=OrgName%>会议室预订管理系统 </i> </a>
                </div>
                <div class="navbar-header pull-right">
                    <ul class="nav ace-nav">
                        <li class="light-blue"><a href="#" style="background-color:#3c5b9a"><span><i class="fa fa-user fa-fw"></i>欢迎您，<%=loginingUser%> </span></a></li>
                        <li class="light-blue"><a href="#" style="background-color:#3c5b9a"><span><i class="fa fa-calendar fa-fw"></i>&nbsp;<label id="lbSystemTime" runat="server"></label></span></a></li>
                        <li class="light-blue">
                            <asp:LinkButton ID="lbLogout" runat="server" OnClick="LbLogout_Click" style="background-color:#3c5b9a"><span><i class="fa fa-power-off fa-fw "></i> 退出</span></asp:LinkButton>&nbsp;&nbsp;
                        </li>
                    </ul>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
