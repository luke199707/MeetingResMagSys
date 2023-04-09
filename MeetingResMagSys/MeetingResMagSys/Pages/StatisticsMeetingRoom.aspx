<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StatisticsMeetingRoom.aspx.cs" Inherits="MeetingResMagSys.Pages.StatisticsMeetingRoom" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>预订统计</title>
    <script src="../Scripts/jquery.min.js"></script>
    <script src="../Scripts/bootstrap.min.js"></script>
    <link href="../StyleSheet/bootstrap.css" rel="stylesheet" />
    <link href="../StyleSheet/font-awesome-4.7.0/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../StyleSheet/AdminTheme.css" rel="stylesheet" type="text/css" />
    <link href="../Plugins/toastr/toastr.min.css" rel="stylesheet" />
    <script src="../Plugins/toastr/toastr.min.js"></script>
    <script src="../Plugins/echarts-5.2.1/echarts.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <!-- Head -->
                <div class="breadcrumbs" id="breadcrumbs" style="background-color: #ffffff">
                    <ul class="breadcrumb">
                        <li><i class="fa fa-home fa-fw home-icon"></i><a href="#">预订统计</a></li>
                        <li class="active">按会议室统计</li>
                    </ul>
                </div>
                <!-- Content -->
                <div class="space-16"></div>
                <div id="main" style="width: 1000px; height: 600px;" class="center-block"></div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
    <script type="text/javascript">
        toastr.options = {
            closeButton: true,
            debug: false,
            progressBar: true,
            positionClass: "toast-top-right",
            timeOut: "3000",
            showMethod: "slideDown",
        };
        $(document).ready(function () {
            //获取所有会议室名称
            var roomName;
            $.ajax({
                type: "post",
                async: false,
                url: "../Handler/StatisticsMeetingRoom.ashx",
                data: { "operation": "getRoom" },
                dataType: "json",
                success: function (data) {
                    roomName = data;
                }
            });
            //获取会议室本月被预定次数
            var mouthCount;
            $.ajax({
                type: "post",
                async: false,
                url: "../Handler/StatisticsMeetingRoom.ashx",
                data: { "operation": "getMouthCount" },
                dataType: "json",
                success: function (data) {
                    mouthCount = data;
                }
            });
            //获取会议室本年度被预定次数
            var yearCount;
            $.ajax({
                type: "post",
                async: false,
                url: "../Handler/StatisticsMeetingRoom.ashx",
                data: { "operation": "getYearCount" },
                dataType: "json",
                success: function (data) {
                    yearCount = data;
                }
            });
            //获取会议室被预订的总次数
            var totalCount;
            $.ajax({
                type: "post",
                async: false,
                url: "../Handler/StatisticsMeetingRoom.ashx",
                data: { "operation": "getTotalCount" },
                dataType: "json",
                success: function (data) {
                    totalCount = data;
                }
            });
            // 基于准备好的dom，初始化echarts实例
            var myChart = echarts.init(document.getElementById('main'));
            var option = {
                title: {
                    text: '会议室被预订次数统计'
                },
                tooltip: {},
                legend: {
                    data: ['本月次数', '本年度次数', '总次数']
                },
                xAxis: {
                    data: roomName
                },
                yAxis: {},
                series: [{
                    name: '本月次数',
                    type: 'bar',
                    data: mouthCount
                },
                {
                    name: '本年度次数',
                    type: 'bar',
                    data: yearCount
                },
                {
                    name: '总次数',
                    type: 'bar',
                    data: totalCount
                }]
            };
            // 使用刚指定的配置项和数据显示图表。
            myChart.setOption(option);
        });
    </script>
</body>
</html>
