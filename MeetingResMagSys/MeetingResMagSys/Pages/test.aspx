<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="test.aspx.cs" Inherits="MeetingResMagSys.Pages.test" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>测试</title>
    
</head>
<body>
    <form id="form1" runat="server">
        <asp:FileUpload ID="FileUpload1" runat="server" />
     <asp:Button ID="Button1" runat="server" Text="上传" OnClick="Button1_Click" />
     <asp:Label ID="Label1" runat="server" Text="" Style="color: Red"></asp:Label>

        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                     
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
    <script type="text/javascript">
        $(document).ready(function () {
            
        });
        //-----------------bootstrap-datetimepicker-----------------
        //var date = new Date();
        //$('.form_datetime').datetimepicker({
        //    language: 'zh-CN',
        //    format: 'yyyy-mm-dd hh:ii',
        //    minuteStep: 30,
        //    weekStart: 1,
        //    todayBtn: 1,
        //    autoclose: 1,
        //    todayHighlight: 1,
        //    startDate: date,
        //    startView: 2,
        //    forceParse: 0,
        //    showMeridian: 1
        //});
        //$('.form_date').datetimepicker({
        //    language: 'zh-CN',
        //    format: 'yyyy-mm-dd',
        //    startDate: date,
        //    weekStart: 1,
        //    todayBtn: 1,
        //    autoclose: 1,
        //    todayHighlight: 1,
        //    startView: 2,
        //    minView: 2,
        //    forceParse: 0
        //});
        //$('.form_time').datetimepicker({
        //    language: 'zh-CN',
        //    format: 'hh:ii',
        //    startDate: '08:00',
        //    endDate: '20:00',
        //    weekStart: 1,
        //    todayBtn: false,
        //    autoclose: 1,
        //    minuteStep: 30,
        //    todayHighlight: 1,
        //    startView: 1,
        //    minView: 0,
        //    maxView: 1,
        //    forceParse: 0
        //});
        //-----------------fullcalendar-----------------
        //document.addEventListener('DOMContentLoaded', function () {
        //    var calendarEl = document.getElementById('calendar');
        //    var calendar = new FullCalendar.Calendar(calendarEl, {
        //        schedulerLicenseKey: 'CC-Attribution-NonCommercial-NoDerivatives',//试用期免警告密钥
        //        locale: 'zh-cn',
        //        timeZone: 'UTC',
        //        themeSystem: 'bootstrap',
        //        slotMinTime: '08:00',
        //        slotMaxTime: '20:00',
        //        slotDuration: '00:30:00',
        //        timeFormat: 'HH:mm{ - HH:mm}',
        //        initialView: 'dayGridMonth',
        //        headerToolbar: {
        //            //left: 'prevYear,prev,next,nextYear today',
        //            left: 'prev,next today',
        //            center: 'title',
        //            right: 'dayGridMonth,timeGridWeek,timeGridDay,dayGridWeek,resourceTimelineDay,resourceTimelineWeek,resourceTimelineMonth'
        //        },
        //        expandRows: true,
        //        navLinks: true,
        //        editable: false,
        //        selectable: false,
        //        nowIndicator: false,
        //        dayMaxEvents: true,
        //        stickyHeaderDates: true,
        //        showNonCurrentDates: false,
        //        slotEventOverlap: false,
        //        allDaySlot: false,
        //        displayEventEnd: true,
        //        weekNumbers: true,
        //        selectOverlap: false,
        //        eventTimeFormat: {
        //            hour: '2-digit',
        //            minute: '2-digit',
        //            meridiem: false,
        //            hour12: false
        //        },
        //        selectMinDistance: 10,
        //        dateClick: function (info) {
        //            toastr.info('点击了： ' + info.dateStr);
        //        },
        //        select: function (info) {
        //            toastr.info('选取了： ' + info.startStr + ' 至 ' + info.endStr);
        //        },
        //        //selectAllow: function(selectInfo) {
        //        //    toastr.info(selectInfo.start + "---" + selectInfo.end + "---" + selectInfo.resourceId);
        //        //},
        //        resourceAreaHeaderContent: '会议室',
        //        resourceGroupField: 'building',
        //        resources: [
        //            { id: 'a', building: '460 Bryant', title: 'Auditorium A' },
        //            { id: 'b', building: '460 Bryant', title: 'Auditorium B' },
        //            { id: 'c', building: '460 Bryant', title: 'Auditorium C' },
        //            { id: 'd', building: '460 Bryant', title: 'Auditorium D' },
        //            { id: 'e', building: '460 Bryant', title: 'Auditorium E' },
        //            { id: 'f', building: '460 Bryant', title: 'Auditorium F' },
        //            { id: 'g', building: '564 Pacific', title: 'Auditorium G' },
        //            { id: 'h', building: '564 Pacific', title: 'Auditorium H' },
        //            { id: 'i', building: '564 Pacific', title: 'Auditorium I' },
        //            { id: 'j', building: '564 Pacific', title: 'Auditorium J' },
        //            { id: 'k', building: '564 Pacific', title: 'Auditorium K' },
        //            { id: 'l', building: '564 Pacific', title: 'Auditorium L' },
        //            { id: 'm', building: '564 Pacific', title: 'Auditorium M' },
        //            { id: 'n', building: '564 Pacific', title: 'Auditorium N' },
        //            { id: 'o', building: '101 Main St', title: 'Auditorium O' },
        //            { id: 'p', building: '101 Main St', title: 'Auditorium P' },
        //            { id: 'q', building: '101 Main St', title: 'Auditorium Q' },
        //            { id: 'r', building: '101 Main St', title: 'Auditorium R' },
        //            { id: 's', building: '101 Main St', title: 'Auditorium S' },
        //            { id: 't', building: '101 Main St', title: 'Auditorium T' },
        //            { id: 'u', building: '101 Main St', title: 'Auditorium U' },
        //            { id: 'v', building: '101 Main St', title: 'Auditorium V' },
        //            { id: 'w', building: '101 Main St', title: 'Auditorium W' },
        //            { id: 'x', building: '101 Main St', title: 'Auditorium X' },
        //            { id: 'y', building: '101 Main St', title: 'Auditorium Y' },
        //            { id: 'z', building: '101 Main St', title: 'Auditorium Z' }
        //        ],
        //        //eventColor: 'blue',
        //        //eventBackgroundColor: 'blue',
        //        eventClick: function (info) {
        //            toastr.info('Event: ' + info.event.title);
        //            toastr.info('Coordinates: ' + info.jsEvent.pageX + ',' + info.jsEvent.pageY);
        //            toastr.info('View: ' + info.view.type);
        //            // change the border color just for fun
        //            info.el.style.borderColor = 'red';
        //        },
        //        eventSources: [
        //            {
        //                events: [ // put the array in the `events` property
        //                    { title: '测试5', start: '2021-08-11T10:30:00', end: '2021-08-11T15:30:00' },
        //                    { title: '测试6', start: '2021-08-12T10:30:00', end: '2021-08-12T15:30:00' },
        //                    { title: '测试7', start: '2021-08-13T10:30:00', end: '2021-08-13T15:30:00' }
        //                ],
        //                color: 'green',     // an option!
        //                textColor: 'white' // an option!
        //            },
        //            {
        //                events: [ // put the array in the `events` property
        //                    { title: '测试8', start: '2021-08-14T10:30:00', end: '2021-08-14T15:30:00' },
        //                    { title: '测试9', start: '2021-08-15T10:30:00', end: '2021-08-15T15:30:00' },
        //                    { title: '测试10', start: '2021-08-16T10:30:00', end: '2021-08-16T15:30:00' }
        //                ],
        //                color: 'red',     // an option!
        //                textColor: 'white' // an option!
        //            },
        //            {
        //                events: function (fetchInfo, successCallback, failureCallback) {
        //                    $.ajax({
        //                        type: "post",
        //                        url: "../Handler/GetEvents.ashx",
        //                        data: {},
        //                        dataType: "text",
        //                        success: function (data) {
        //                            var e = JSON.parse(data);
        //                            var events = [];
        //                            for (var i = 0; i < e.length; i++) {
        //                                events.push({
        //                                    id: e[i].meetingId,
        //                                    title: e[i].title,
        //                                    start: e[i].startTime,
        //                                    end: e[i].endTime
        //                                });
        //                            };
        //                            successCallback(events);
        //                        }
        //                    });
        //                },
        //                color: 'yellow',   // an option!
        //                textColor: 'black' // an option!
        //            }
        //        ]
        //    });
        //    calendar.addEvent({ title: '测试1', start: '2021-08-03T10:30:00', end: '2021-08-03T15:30:00' });
        //    calendar.addEvent({ title: '测试2', start: '2021-08-03T14:00:00', end: '2021-08-03T16:30:00' });
        //    calendar.addEvent({ title: '测试3', start: '2021-08-05T10:30:00', end: '2021-08-05T15:30:00' });
        //    calendar.addEvent({ title: '测试4', start: '2021-08-09T10:30:00', end: '2021-08-09T15:30:00' });

        //    calendar.render();
        //});

    </script>

</body>
</html>
