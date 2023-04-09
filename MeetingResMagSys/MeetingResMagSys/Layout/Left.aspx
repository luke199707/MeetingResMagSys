<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Left.aspx.cs" Inherits="MeetingResMagSys.Layout.Left" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>左侧菜单栏</title>
    <script src="../Scripts/jquery-3.4.1.min.js"></script>
    <link href="../StyleSheet/bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="../StyleSheet/font-awesome-4.7.0/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../StyleSheet/AdminTheme.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/ace-extra.min.js"></script>
    <script src="../Scripts/bootstrap.min.js" type="text/javascript"></script>
    <script src="../Scripts/holder.min.js" type="text/javascript"></script>
    <script src="../Scripts/ace-elements.min.js" type="text/javascript"></script>
    <script src="../Scripts/ace.min.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
         <div class="sidebar" id="sidebar" runat="server" >
               <div id="sidebar-collapse" class="sidebar-collapse">
                   <i class="fa  fa-chevron-circle-left " data-icon2="fa fa-chevron-circle-right" data-icon1="fa fa-chevron-circle-left"></i>
              </div>
         </div>
    </form>
</body>
</html>
