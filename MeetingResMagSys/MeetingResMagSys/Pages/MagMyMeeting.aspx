<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MagMyMeeting.aspx.cs" Inherits="MeetingResMagSys.Pages.MagMyMeeting" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>我的会议</title>
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
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <!-- Head -->
                <div class="breadcrumbs" id="breadcrumbs" style="background-color: #ffffff">
                    <ul class="breadcrumb">
                        <li><i class="fa fa-home fa-fw home-icon"></i><a href="#">我的会议</a></li>
                        <li class="active">我的会议</li>
                    </ul>
                    <ul class="nav navbar-nav navbar-right" style="margin-right: 15px">
                        <li>
                            <asp:Button ID="btnChangeView" runat="server" Text="切换为列表展示" CssClass="btn btn-primary" OnClick="btnChangeView_Click"/>
                        </li>
                    </ul>
                </div>
                <!-- Content -->
                <div class="space-8"></div>
                <div class="container">
                    <div class="row">
                        <div class="col-xs-4"></div>
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
                                            <asp:HiddenField ID="HiddenMeetingRoomValue" runat="server" />
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
                                            <asp:TextBox ID="DtxtIntro" class="form-control" runat="server" placeholder="" ReadOnly="true"  TextMode="MultiLine"></asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                            <label><strong>预订人</strong></label>
                                            <asp:TextBox ID="DtxtBooker" class="form-control" runat="server" placeholder="" ReadOnly="true"></asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                            <label><strong>备注</strong></label>
                                            <asp:TextBox ID="DtxtRemark" class="form-control" runat="server" placeholder="" ReadOnly="true"  TextMode="MultiLine"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="tab-pane fade" id="DisplayModalright">
                                        <asp:CheckBoxList ID="DcblMeetingMember" runat="server" RepeatDirection="Vertical"></asp:CheckBoxList>
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer" style="background-color: white">
                                <button type="button" class="btn btn-default" data-dismiss="modal">关闭</button>
                                <button id="btnEdit" type="button" class="btn btn-warning" onclick="Edit()">编辑</button>
                                <button id="btnDelete" type="button" class="btn btn-danger" onclick="Delete()">取消预订</button>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- EditModal -->
                <div class="modal fade" id="EditModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
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
                                            <asp:HiddenField ID="HiddenMeetingId" runat="server" /><%--编辑保存时使用--%>
                                            <label><span style="color: red">*</span>会议ID</label>
                                            <asp:TextBox ID="EtxtMeetingId" class="form-control" runat="server" placeholder="" ReadOnly="true"></asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                            <label><span style="color: red">*</span>会议主题</label>
                                            <asp:TextBox ID="EtxtTitle" class="form-control" runat="server" placeholder=""></asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                            <label><span style="color: red">*</span>会议室</label>
                                            <asp:DropDownList ID="EddlMeetingRoom" runat="server" CssClass="form-control"></asp:DropDownList>
                                        </div>
                                        <div class="form-group">
                                            <label><span style="color: red">*</span>会议日期</label>
                                            <div class="input-group">
                                                <asp:TextBox ID="EtxtDate" class="form-control" runat="server" placeholder="" autocomplete="off"></asp:TextBox>
                                                <span class="input-group-addon"><span class="fa fa-calendar"></span></span>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label><span style="color: red">*</span>开始时间</label>
                                            <div class="input-group">
                                                <asp:TextBox ID="EtxtStartTime" class="form-control" runat="server" placeholder="" autocomplete="off"></asp:TextBox>
                                                <span class="input-group-addon"><span class="fa fa-clock-o"></span></span>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label><span style="color: red">*</span>结束时间</label>
                                            <div class="input-group">
                                                <asp:TextBox ID="EtxtEndTime" class="form-control" runat="server" placeholder="" autocomplete="off"></asp:TextBox>
                                                <span class="input-group-addon"><span class="fa fa-clock-o"></span></span>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label>会议简介</label>
                                            <asp:TextBox ID="EtxtIntro" class="form-control" runat="server" placeholder="" TextMode="MultiLine"></asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                            <label>备注</label>
                                            <asp:TextBox ID="EtxtRemark" class="form-control" runat="server" placeholder="" TextMode="MultiLine"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="tab-pane fade" id="right">
                                        <asp:CheckBoxList ID="EcblMeetingMember" runat="server" RepeatDirection="Vertical"></asp:CheckBoxList>
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer" style="background-color: white">
                                <button type="button" class="btn btn-default" data-dismiss="modal">关闭</button>
                                <asp:Button ID="btnSave" OnClick="btnSave_Click" runat="server" Text="保存" CssClass="btn btn-primary"/>
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
            $('#EtxtDate').datetimepicker({
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
            $('#EtxtStartTime').datetimepicker({
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
                    $("#EtxtEndTime").datetimepicker("setStartDate", reseStart);
                    $("#EtxtEndTime").datetimepicker("setEndDate", reseEnd);
                } else {
                    $("#EtxtEndTime").datetimepicker("setStartDate", this.value);
                    $("#EtxtEndTime").datetimepicker("setEndDate", reseEnd);
                }
            });
            $('#EtxtEndTime').datetimepicker({
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
                    $("#EtxtStartTime").datetimepicker("setStartDate", reseStart);
                    $("#EtxtStartTime").datetimepicker("setEndDate", reseEnd);
                } else {
                    $("#EtxtStartTime").datetimepicker("setStartDate", reseStart);
                    $("#EtxtStartTime").datetimepicker("setEndDate", this.value);
                }
            });
        };

        function Edit() {
            $("#EtxtMeetingId").val($("#DtxtMeetingId").val());
            $("#EtxtTitle").val($("#DtxtTitle").val());
            $("#EddlMeetingRoom").val($("#HiddenMeetingRoomValue").val());
            $("#EtxtDate").val($("#DtxtDate").val());
            $("#EtxtStartTime").val($("#DtxtStartTime").val());
            $("#EtxtEndTime").val($("#DtxtEndTime").val());
            $("#EtxtIntro").val($("#DtxtIntro").val());
            $("#EtxtRemark").val($("#DtxtRemark").val());
            //设置隐藏的会议ID，用于后台更新操作
            $("#HiddenMeetingId").val($("#DtxtMeetingId").val());
            $('#DisplayModal').modal('toggle');
            $('#EditModal').modal('toggle');
        };

        function Delete() {
            var meetingId = $("#HiddenMeetingId").val();
            if (confirm('确认取消此预订？')) {
                $.post("../Handler/MeetingResDel.ashx", { "meetingId": meetingId}, function (data) {
                    if (data == "ok") {
                        location.href = "MagMyMeeting.aspx";
                    }
                });
            } else {
                return false;
            }
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

        function hideEditModal() {
            $('#EditModal').modal('hide');
        };

        function showDisplayModal() {
            $('#DisplayModal').modal('show');
        };

        $('#DisplayModal').on('shown.bs.modal', function (e) {
            $("#HiddenMeetingId").val($("#DtxtMeetingId").val());//设置隐藏的会议ID，用于删除操作
            var meetingDateTime = $("#DtxtDate").val() + "T" + $("#DtxtEndTime").val();
            var now = new Date();
            var year = now.getFullYear(); 
            var month = now.getMonth();
            var date = now.getDate();
            var hour = now.getHours();
            var min = now.getMinutes();
            month = month + 1;
            if (month < 10) month = "0" + month;
            if (date < 10) date = "0" + date;
            if (hour < 10) hour = "0" + hour;
            if (min < 10) min = "0" + min;
            var nowDateTime = year + "-" + month + "-" + date + "T" + hour + ":" + min;
            var userName = "<%=Session["userName"]%>";//判断会议是否为登陆用户预订，否则不可编辑
            var booker = $("#DtxtBooker").val().toString();
            if (Date.parse(meetingDateTime) < Date.parse(nowDateTime) || userName!=booker) {
                $("#btnEdit").hide();
                $("#btnDelete").hide();
            } else {
                $("#btnEdit").show();
                $("#btnDelete").show();
            };
        });
        //fullcalendar插件js
        function CreateCal() {
            var reseStart;
            var reseEnd;
            var timeUnit;
            $.ajax({
                type: "post",
                async: false,//该AJAX请求嵌套在AJAX中，必须设置为同步，否则执行会空值
                url: "../Handler/GetOrg.ashx",
                data: {},
                dataType: "text",
                success: function (data) {
                    var e = JSON.parse(data);
                    for (var i = 0; i < e.length; i++) {
                        reseStart = e[i].reseStart;
                        reseEnd = e[i].reseEnd;
                        timeUnit = "00:" + e[i].timeUnit;
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
                eventClick: function (info) {
                    $.ajax({
                        type: "post",
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
                                $("#HiddenMeetingRoomValue").val(e[i].meetingRoom);//用于编辑模态框加载会议室名称
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
                    //处理展示模态框中的复选框
                    for (var i = 0; i < document.getElementById("DcblMeetingMember").getElementsByTagName("input").length; i++) {
                        var objCheck = document.getElementById("DcblMeetingMember_" + i);
                        if (attendUserId.indexOf(objCheck.value) != -1) {
                            objCheck.checked = true;
                        }
                        else {
                            objCheck.checked = false;
                        }
                    };
                    //处理编辑模态框中的复选框
                    for (var i = 0; i < document.getElementById("EcblMeetingMember").getElementsByTagName("input").length; i++) {
                        var objCheck = document.getElementById("EcblMeetingMember_" + i);
                        if (attendUserId.indexOf(objCheck.value) != -1) {
                            objCheck.checked = true;
                        }
                        else {
                            objCheck.checked = false;
                        }
                    };
                    setTimePicker();
                    showDisplayModal();
                },
                eventSources: [
                    //加载本人预订的会议
                    {
                        events: function (fetchInfo, successCallback, failureCallback) {
                            $.ajax({
                                type: "post",
                                url: "../Handler/GetAllMyOrderEvent.ashx",
                                data: {},
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
                    //加载本人参与的会议
                    {
                        events: function (fetchInfo, successCallback, failureCallback) {
                            $.ajax({
                                type: "post",
                                url: "../Handler/GetAllMyAttendEvent.ashx",
                                data: {},
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
                    }
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
