<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MeetingOrderByRoom.aspx.cs" Inherits="MeetingResMagSys.Pages.MeetingOrderByRoom" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>按会议室找时间</title>
    <script src="../Scripts/jquery-3.5.1.min.js"></script>
    <script src="../Plugins/bootstrap-datetimepicker/bootstrap/js/bootstrap.min.js"></script>
    <link href="../Plugins/bootstrap-datetimepicker/bootstrap/css/bootstrap.css" rel="stylesheet" />
    <link href="../StyleSheet/font-awesome-4.7.0/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../StyleSheet/AdminTheme.css" rel="stylesheet" type="text/css" />
    <link href="../Plugins/toastr/toastr.min.css" rel="stylesheet" />
    <script src="../Plugins/toastr/toastr.min.js"></script>
    <link href="../Plugins/fullcalendar-scheduler-5.9.0/lib/main.min.css" rel="stylesheet" />
    <script src="../Plugins/fullcalendar-scheduler-5.9.0/lib/main.min.js"></script>
    <script src="../Plugins/fullcalendar-scheduler-5.9.0/lib/locales-all.min.js"></script>
    <script src="../Plugins/bootstrap-datetimepicker/js/bootstrap-datetimepicker.js"></script>
    <script src="../Plugins/bootstrap-datetimepicker/js/locales/bootstrap-datetimepicker.zh-CN.js"></script>
    <link href="../Plugins/bootstrap-datetimepicker/css/bootstrap-datetimepicker.min.css" rel="stylesheet" />
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
                        <li><i class="fa fa-home fa-fw home-icon"></i><a href="#">会议室预订</a></li>
                        <li class="active">按会议室找时间</li>
                    </ul>
                    <ul class="nav navbar-nav navbar-right" style="margin-right: 15px">
                        <li>办公区域：
                            <asp:DropDownList ID="ddlOfficeArea" runat="server" CssClass="btn btn-primary" OnSelectedIndexChanged="ddlOfficeArea_SelectedIndexChanged" AutoPostBack="True">
                            </asp:DropDownList>
                        </li>
                        <li style="margin: 0px 15px">会议室：
                            <asp:DropDownList ID="ddlMeetingRoom" runat="server" CssClass="btn btn-primary" OnSelectedIndexChanged="ddlMeetingRoom_SelectedIndexChanged" AutoPostBack="True">
                            </asp:DropDownList>
                        </li>
                        <li>
                            <asp:Button data-toggle="modal" data-target="#AddModal" ID="btnOrder" runat="server" Text="预订会议室" CssClass="btn btn-primary" OnClientClick="setTimePicker()" />
                        </li>
                    </ul>
                </div>
                <!-- Content -->
                <div class="space-8"></div>
                <div class="container">
                    <div class="row">
                        <div class="col-xs-2"></div>
                        <div class="col-xs-1">
                            <button class="btn btn-block" style="background-color: blue" disabled="disabled"></button>
                        </div>
                        <div class="col-xs-2">
                            <p>:其他人的</p>
                        </div>
                        <div class="col-xs-1">
                            <button class="btn btn-block" style="background-color: green" disabled="disabled"></button>
                        </div>
                        <div class="col-xs-2">
                            <p>:我发起的</p>
                        </div>
                        <div class="col-xs-1">
                            <button class="btn btn-block" style="background-color: yellow" disabled="disabled"></button>
                        </div>
                        <div class="col-xs-2">
                            <p>:我参与的</p>
                        </div>
                    </div>
                    <div class="row">
                        <div id="calendar"></div>
                    </div>
                </div>
                <!-- AddModal -->
                <div class="modal fade" id="AddModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
                    <div class="modal-dialog modal-lg" role="document">
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                                <h4 class="modal-title" id="myModalLabel">预订信息</h4>
                            </div>
                            <div class="modal-body">
                                <ul id="myTab" class="nav nav-tabs">
                                    <li class="active"><a href="#info" data-toggle="tab">基本信息</a></li>
                                    <li><a href="#right" data-toggle="tab">选择参会人员</a></li>
                                </ul>
                                <div id="myTabContent" class="tab-content">
                                    <div class="tab-pane fade in active" id="info">
                                        <div class="form-group">
                                            <label><span style="color: red">*</span>会议主题</label>
                                            <asp:TextBox ID="AtxtTitle" class="form-control" runat="server" placeholder=""></asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                            <label><span style="color: red">*</span>会议室</label>
                                            <asp:DropDownList ID="AddlMeetingRoom" runat="server" CssClass="form-control"></asp:DropDownList>
                                        </div>
                                        <div class="form-group">
                                            <label><span style="color: red">*</span>会议日期</label>
                                            <div class="input-group">
                                                <asp:TextBox ID="AtxtDate" class="form-control" runat="server" placeholder="" autocomplete="off"></asp:TextBox>
                                                <span class="input-group-addon"><span class="fa fa-calendar"></span></span>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label><span style="color: red">*</span>开始时间</label>
                                            <div class="input-group">
                                                <asp:TextBox ID="AtxtStartTime" class="form-control" runat="server" placeholder="" autocomplete="off"></asp:TextBox>
                                                <span class="input-group-addon"><span class="fa fa-clock-o"></span></span>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label><span style="color: red">*</span>结束时间</label>
                                            <div class="input-group">
                                                <asp:TextBox ID="AtxtEndTime" class="form-control" runat="server" placeholder="" autocomplete="off"></asp:TextBox>
                                                <span class="input-group-addon"><span class="fa fa-clock-o"></span></span>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label>会议简介</label>
                                            <asp:TextBox ID="AtxtIntro" class="form-control" runat="server" placeholder="" TextMode="MultiLine"></asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                            <label>备注</label>
                                            <asp:TextBox ID="AtxtRemark" class="form-control" runat="server" placeholder="" TextMode="MultiLine"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="tab-pane fade" id="right">
                                        <asp:CheckBoxList ID="AcblMeetingMember" runat="server" RepeatDirection="Vertical"></asp:CheckBoxList>
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer" style="background-color: white">
                                <button type="button" class="btn btn-default" data-dismiss="modal">关闭</button>
                                <asp:Button OnClick="btnAddCertain_Click" ID="btnAddCertain" runat="server" Text="预订会议" CssClass="btn btn-primary" />
                            </div>
                        </div>
                    </div>
                </div>
                <!-- DisplayModal -->
                <div class="modal fade" id="DisplayModal" tabindex="-1" role="dialog" aria-labelledby="DisplayModalLabel">
                    <div class="modal-dialog modal-lg" role="document">
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                                <h4 class="modal-title" id="DisplayModalLabel"><strong>预订信息</strong></h4>
                            </div>
                            <div class="modal-body">
                                <ul id="DisplayModalTab" class="nav nav-tabs">
                                    <li class="active"><a href="#DisplayModalinfo" data-toggle="tab"><strong>基本信息</strong></a></li>
                                    <li><a href="#DisplayModalright" data-toggle="tab"><strong>参会人员</strong></a></li>
                                </ul>
                                <div id="DisplayModalTabContent" class="tab-content">
                                    <div class="tab-pane fade in active" id="DisplayModalinfo">
                                        <div class="form-group">
                                            <label><strong>会议ID</strong></label>
                                            <asp:TextBox ID="DtxtMeetingId" class="form-control" runat="server" placeholder="" ReadOnly="true"></asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                            <label><strong>会议主题</strong></label>
                                            <asp:TextBox ID="DtxtTitle" class="form-control" runat="server" placeholder="" ReadOnly="true"></asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                            <label><strong>会议室</strong></label>
                                            <asp:TextBox ID="DtxtMeetingRoom" class="form-control" runat="server" placeholder="" ReadOnly="true"></asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                            <label><strong>会议日期</strong></label>
                                            <div class="input-group">
                                                <asp:TextBox ID="DtxtDate" class="form-control" runat="server" placeholder="" ReadOnly="true"></asp:TextBox>
                                                <span class="input-group-addon"><span class="fa fa-calendar"></span></span>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label><strong>开始时间</strong></label>
                                            <div class="input-group">
                                                <asp:TextBox ID="DtxtStartTime" class="form-control" runat="server" placeholder="" ReadOnly="true"></asp:TextBox>
                                                <span class="input-group-addon"><span class="fa fa-clock-o"></span></span>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label><strong>结束时间</strong></label>
                                            <div class="input-group">
                                                <asp:TextBox ID="DtxtEndTime" class="form-control" runat="server" placeholder="" ReadOnly="true"></asp:TextBox>
                                                <span class="input-group-addon"><span class="fa fa-clock-o"></span></span>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label><strong>会议简介</strong></label>
                                            <asp:TextBox ID="DtxtIntro" class="form-control" runat="server" placeholder="" ReadOnly="true" TextMode="MultiLine"></asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                            <label><strong>预订人</strong></label>
                                            <asp:TextBox ID="DtxtBooker" class="form-control" runat="server" placeholder="" ReadOnly="true"></asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                            <label><strong>备注</strong></label>
                                            <asp:TextBox ID="DtxtRemark" class="form-control" runat="server" placeholder="" ReadOnly="true" TextMode="MultiLine"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="tab-pane fade" id="DisplayModalright">
                                        <asp:CheckBoxList ID="DcblMeetingMember" runat="server" RepeatDirection="Vertical"></asp:CheckBoxList>
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer" style="background-color: white">
                                <button type="button" class="btn btn-default" data-dismiss="modal">关闭</button>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
    <script type="text/javascript">
        //消息提示器js
        toastr.options = {
            closeButton: true,
            debug: false,
            progressBar: true,
            positionClass: "toast-top-right",
            timeOut: "3000",
            showMethod: "slideDown",
        };
        //时间选择器js
        function setTimePicker() {
            var reseStart;
            var reseEnd;
            var timeUnit;
            $.ajax({
                type: "post",
                async: false,//必须设置为同步，否则执行会空值
                url: "../Handler/GetOrg.ashx",
                data: {},
                dataType: "text",
                success: function (data) {
                    var e = JSON.parse(data);
                    for (var i = 0; i < e.length; i++) {
                        reseStart = e[i].reseStart;
                        reseEnd = e[i].reseEnd;
                        timeUnit = parseInt(e[i].timeUnit);
                    };
                }
            });
            //时间选择器js
            $('#AtxtDate').datetimepicker({
                language: 'zh-CN',
                format: 'yyyy-mm-dd',
                startDate: new Date(),
                weekStart: 1,
                todayBtn: 1,
                autoclose: 1,
                todayHighlight: 1,
                startView: 2,
                minView: 2,
                forceParse: 0
            });
            $('#AtxtStartTime').datetimepicker({
                language: 'zh-CN',
                format: 'hh:ii',
                startDate: reseStart,
                endDate: reseEnd,
                weekStart: 1,
                todayBtn: false,
                autoclose: 1,
                minuteStep: timeUnit,
                todayHighlight: 1,
                startView: 1,
                minView: 0,
                maxView: 1,
                forceParse: 0
            }).on("changeDate", function () {
                if (!this.value) {
                    $("#AtxtEndTime").datetimepicker("setStartDate", reseStart);
                    $("#AtxtEndTime").datetimepicker("setEndDate", reseEnd);
                } else {
                    $("#AtxtEndTime").datetimepicker("setStartDate", this.value);
                    $("#AtxtEndTime").datetimepicker("setEndDate", reseEnd);
                }
            });
            $('#AtxtEndTime').datetimepicker({
                language: 'zh-CN',
                format: 'hh:ii',
                startDate: reseStart,
                endDate: reseEnd,
                weekStart: 1,
                todayBtn: false,
                autoclose: 1,
                minuteStep: timeUnit,
                todayHighlight: 1,
                startView: 1,
                minView: 0,
                maxView: 1,
                forceParse: 0
            }).on("changeDate", function () {
                if (!this.value) {
                    $("#AtxtStartTime").datetimepicker("setStartDate", reseStart);
                    $("#AtxtStartTime").datetimepicker("setEndDate", reseEnd);
                } else {
                    $("#AtxtStartTime").datetimepicker("setStartDate", reseStart);
                    $("#AtxtStartTime").datetimepicker("setEndDate", this.value);
                }
            });
        };

        function Tips() {
            alert("请注意：该会议室预约需要审核！");
            CreateCal();
        }; 

        function GetRoomName(roomId) {
            var name;
            $.ajax({
                type: "post",
                async: false,//该AJAX请求嵌套在AJAX中，必须设置为同步，否则执行会空值
                url: "../Handler/GetXXByXX.ashx",
                data: { "selectField": "name", "tableName": "MeetingRoom", "conditionField": "roomId", "conditionValue": roomId },
                dataType: "text",
                success: function (data) {
                    name = data;
                }
            });
            return name;
        };

        function GetBookerName(userId) {
            var name;
            $.ajax({
                type: "post",
                async: false,//该AJAX请求嵌套在AJAX中，必须设置为同步，否则执行会空值
                url: "../Handler/GetXXByXX.ashx",
                data: { "selectField": "name", "tableName": "AllUser", "conditionField": "userId", "conditionValue": userId },
                dataType: "text",
                success: function (data) {
                    name = data;
                }
            });
            return name;
        };

        function showAddModal() {
            $('#AddModal').modal('show');
        };

        function hideAddModal() {
            $('#AddModal').modal('toggle');
        };

        function showDisplayModal() {
            $('#DisplayModal').modal('show');
        };
        //清空Modal,Modal隐藏时触发
        $('#AddModal').on('hidden.bs.modal', function () {
            $("#AtxtTitle").val("");
            $("#AddlMeetingRoom").val("");
            $("#AtxtDate").val("");
            $("#AtxtStartTime").val("");
            $("#AtxtEndTime").val("");
            $("#AtxtIntro").val("");
            $("#AtxtRemark").val("");
        });
        //fullcalendar插件js
        function CreateCal() {
            var room = $("#ddlMeetingRoom").val();
            var reseStart;
            var reseEnd;
            var timeUnit;
            $.ajax({
                type: "post",
                async: false,//必须设置为同步，否则执行会空值
                url: "../Handler/GetOrg.ashx",
                data: {},
                dataType: "text",
                success: function (data) {
                    var e = JSON.parse(data);
                    for (var i = 0; i < e.length; i++) {
                        reseStart = e[i].reseStart;
                        reseEnd = e[i].reseEnd;
                        timeUnit = "00:"+ e[i].timeUnit;
                    };
                }
            });
            var calendarEl = document.getElementById('calendar');
            var calendar = new FullCalendar.Calendar(calendarEl, {
                schedulerLicenseKey: 'CC-Attribution-NonCommercial-NoDerivatives',//试用期免警告密钥
                locale: 'zh-cn',
                timeZone: 'UTC',
                themeSystem: '',
                //slotMinTime: '08:00',
                //slotMaxTime: '23:00',
                //slotDuration: '00:30',
                slotMinTime: reseStart,
                slotMaxTime: reseEnd,
                slotDuration: timeUnit,
                slotLabelFormat: {
                    hour12: false,
                    hour: '2-digit',
                    minute: '2-digit'
                },
                timeFormat: 'HH:mm{ - HH:mm}',
                initialView: 'dayGridMonth',
                headerToolbar: {
                    left: 'prev,next today',
                    center: 'title',
                    right: 'dayGridMonth,timeGridWeek,timeGridDay'
                },
                eventTimeFormat: {
                    hour: '2-digit',
                    minute: '2-digit',
                    meridiem: false,
                    hour12: false
                },
                expandRows: true,
                navLinks: true,
                editable: false,
                selectable: false,
                nowIndicator: false,
                dayMaxEvents: true,
                stickyHeaderDates: true,
                showNonCurrentDates: false,
                slotEventOverlap: false,
                allDaySlot: false,
                displayEventEnd: true,
                weekNumbers: false,
                selectOverlap: false,
                selectMinDistance: 10,
                dateClick: function (info) {
                    //重新绑定时间选择器，否则会因为下拉框回发服务器导致失效
                    setTimePicker();
                    $("#AtxtDate").val((info.dateStr).slice(0, 10));
                    showAddModal();
                },
                eventClick: function (info) {
                    //加载显示模态框内容
                    $.ajax({
                        type: "post",
                        //async: false,
                        url: "../Handler/GetOneEvent.ashx",
                        data: { "meetingId": info.event.id },
                        dataType: "text",
                        success: function (data) {
                            var e = JSON.parse(data);
                            //单个结果也需用循环读取
                            for (var i = 0; i < e.length; i++) {
                                $("#DtxtMeetingId").val(e[i].meetingId);
                                $("#DtxtTitle").val(e[i].title);
                                $("#DtxtMeetingRoom").val(GetRoomName(e[i].meetingRoom));
                                $("#DtxtDate").val(e[i].time);
                                $("#DtxtStartTime").val((e[i].startTime).slice(-5));
                                $("#DtxtEndTime").val((e[i].endTime).slice(-5));
                                $("#DtxtIntro").val(e[i].introduction);
                                $("#DtxtBooker").val(GetBookerName(e[i].booker));
                                $("#DtxtRemark").val(e[i].remark);
                            };
                        }
                    });
                    //处理参会人员复选框
                    var attendUserId = [];
                    $.ajax({
                        type: "post",
                        async: false,//必须设置为同步，否则会空值
                        url: "../Handler/GetMeetingMember.ashx",
                        data: { "meetingId": info.event.id },
                        dataType: "text",
                        success: function (data) {
                            var e = JSON.parse(data);
                            for (var i = 0; i < e.length; i++) {
                                attendUserId.push(e[i].userId);
                            };
                        }
                    });
                    for (var i = 0; i < document.getElementById("DcblMeetingMember").getElementsByTagName("input").length; i++) {
                        var objCheck = document.getElementById("DcblMeetingMember_" + i);
                        if (attendUserId.indexOf(objCheck.value) != -1) {
                            objCheck.checked = true;
                        }
                        else {
                            objCheck.checked = false;
                        }
                    };
                    //显示展示模态框
                    showDisplayModal();
                },
                eventSources: [
                    //加载本人预订的会议
                    {
                        events: function (fetchInfo, successCallback, failureCallback) {
                            $.ajax({
                                type: "post",
                                url: "../Handler/GetMyOrderEvent.ashx",
                                data: { "room": room },
                                dataType: "text",
                                success: function (data) {
                                    var e = JSON.parse(data);
                                    var events = [];
                                    for (var i = 0; i < e.length; i++) {
                                        events.push({
                                            id: e[i].meetingId,
                                            title: e[i].title,
                                            start: e[i].startTime,
                                            end: e[i].endTime
                                        });
                                    };
                                    successCallback(events);
                                }
                            });
                        },
                        color: 'green',
                        textColor: 'white'
                    },
                    //加载其他人预订的会议
                    {
                        events: function (fetchInfo, successCallback, failureCallback) {
                            $.ajax({
                                type: "post",
                                url: "../Handler/GetOtherEvent.ashx",
                                data: { "room": room },
                                dataType: "text",
                                success: function (data) {
                                    var e = JSON.parse(data);
                                    var events = [];
                                    for (var i = 0; i < e.length; i++) {
                                        events.push({
                                            id: e[i].meetingId,
                                            title: e[i].title,
                                            start: e[i].startTime,
                                            end: e[i].endTime
                                        });
                                    };
                                    successCallback(events);
                                }
                            });
                        },
                        color: 'blue',
                        textColor: 'white'
                    },
                    //加载其本人参与的会议
                    {
                        events: function (fetchInfo, successCallback, failureCallback) {
                            $.ajax({
                                type: "post",
                                url: "../Handler/GetMyAttendEvent.ashx",
                                data: { "room": room },
                                dataType: "text",
                                success: function (data) {
                                    var e = JSON.parse(data);
                                    var events = [];
                                    for (var i = 0; i < e.length; i++) {
                                        events.push({
                                            id: e[i].meetingId,
                                            title: e[i].title,
                                            start: e[i].startTime,
                                            end: e[i].endTime
                                        });
                                    };
                                    successCallback(events);
                                }
                            });
                        },
                        color: 'yellow',
                        textColor: 'black'
                    },
                ]
            });
            calendar.render();
        };

        $(document).ready(function () {
            CreateCal();
        });
        function Reload() {
            window.location.reload();
        }
    </script>
</body>
</html>
