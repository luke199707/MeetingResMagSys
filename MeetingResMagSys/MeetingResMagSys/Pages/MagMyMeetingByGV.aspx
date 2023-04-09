<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MagMyMeetingByGV.aspx.cs" Inherits="MeetingResMagSys.Pages.MagMyMeetingByGV" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>我的会议</title>
    <script src="../Scripts/jquery-3.5.1.min.js"></script>
    <script src="../Scripts/bootstrap.min.js"></script>
    <link href="../StyleSheet/bootstrap.css" rel="stylesheet" />
    <%--<script src="../Plugins/bootstrap-datetimepicker/bootstrap/js/bootstrap.min.js"></script>
    <link href="../Plugins/bootstrap-datetimepicker/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../Plugins/bootstrap-datetimepicker/bootstrap/css/bootstrap-theme.min.css" rel="stylesheet" />--%>

    <link href="../StyleSheet/font-awesome-4.7.0/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../StyleSheet/AdminTheme.css" rel="stylesheet" type="text/css" />
    <link href="../Plugins/toastr/toastr.min.css" rel="stylesheet" />
    <script src="../Plugins/toastr/toastr.min.js"></script>
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
                            <asp:Button ID="btnChangeView" runat="server" Text="切换为日历展示" CssClass="btn btn-primary" OnClick="btnChangeView_Click"/>
                        </li>
                    </ul>
                </div>
                <!-- Content -->
                <div class="space-8"></div>
                <div class="container">
                    <div class="row">
                        <div class="col-xs-4">
                        </div>
                        <div class="col-xs-4">
                            <div class="input-group">
                                <asp:TextBox ID="txtSearchTitle" runat="server" class="form-control" placeholder="会议主题"></asp:TextBox>
                                <asp:LinkButton ID="lbtnSearchTitle" runat="server" OnClick="lbtnSearchTitle_Click" CssClass="input-group-addon"><i class="fa fa-search fa-fw"></i></asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="space-8"></div>
                <div id="gridview" style="padding-top: 0px; margin: 10px; padding-left: 0px">
                    <div class="row no-margin">
                        <asp:Literal ID="hiddenid" runat="server" Visible="false"></asp:Literal>
                        <asp:GridView ID="gv" runat="server" AutoGenerateColumns="false" AllowSorting="true"
                            class="table table-bordered table-hover" OnRowCommand="gv_RowCommand"
                            OnRowDataBound="gv_RowDataBound" Width="100%" 
                            OnSorting="grid_Sorting">
                            <RowStyle BackColor="#f9f9f9" HorizontalAlign="Center" VerticalAlign="Top" />
                            <AlternatingRowStyle BackColor="White" HorizontalAlign="Center" VerticalAlign="Top" />
                            <Columns>
                                <asp:TemplateField HeaderText="序号" SortExpression="id">
                                    <ItemTemplate>
                                        <asp:Button ID="btnDetail" CommandName="btnDetailCommand" CommandArgument='<%#Eval("id") %>' runat="server" Text="详细信息" Style="display: none;" />
                                        <asp:Literal ID="LiteralIndex" runat="server" Text="<%# Container.DataItemIndex+1 +(AspNetPager1.CurrentPageIndex-1)*AspNetPager1.PageSize %>"></asp:Literal>
                                        <asp:Literal ID="LiteralId" runat="server" Text='<%#Eval("id") %>' Visible="false"></asp:Literal>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="会议ID" SortExpression="meetingId">
                                    <ItemTemplate>
                                        <asp:Literal ID="lMeetingId" runat="server" Text='<%#Eval("meetingId")%>'></asp:Literal>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="会议主题" SortExpression="title">
                                    <ItemTemplate>
                                        <%#Eval("title")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="会议室" SortExpression="meetingRoom">
                                    <ItemTemplate>
                                        <%#GetRoomName(Eval("meetingRoom").ToString())%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="预订人" SortExpression="booker">
                                    <ItemTemplate>
                                        <asp:Literal ID="lBooker" runat="server" Text='<%#GetBookerName(Eval("booker").ToString())%>'></asp:Literal>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="会议日期" SortExpression="time">
                                    <ItemTemplate>
                                        <%#Eval("time")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="会议状态">
                                    <ItemTemplate>
                                        <asp:Literal ID="lMeetingState" runat="server" Text=''></asp:Literal>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="操作">
                                    <ItemTemplate>
                                        <asp:Button ID="btnDis" runat="server" CommandArgument='<%# Bind("meetingId") %>' OnClick="btnDis_Click" CssClass="btn btn-info" Text="详情" />
                                        <asp:Button ID="btnEdit" runat="server" CommandArgument='<%# Bind("meetingId") %>' OnClick="btnEdit_Click" CssClass="btn btn-warning" Text="编辑" />
                                        <asp:Button ID="btnDelete" runat="server" CommandArgument='<%# Bind("meetingId") %>' OnClientClick="javascript:return confirm('确定取消预订吗？')" OnClick="btnDelete_Click" CssClass="btn btn-danger" Text="取消预订" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <div class="row no-margin dataTables_wrapper_row">
                        <div class="col-xs-9 text-left">
                            <webdiyer:AspNetPager ID="AspNetPager1" runat="server" Width="100%" CssClass="pages"
                                FirstPageText="首页" LastPageText="尾页" PrevPageText="上页" NextPageText="下页" CurrentPageButtonClass="cpb"
                                AlwaysShow="true" ShowCustomInfoSection="Left" ShowPageIndexBox="always" PageIndexBoxType="DropDownList"
                                CustomInfoHTML="第<font color='red'><b>%currentPageIndex%</b></font>页，共%PageCount%页，共<font color='red'><b>%RecordCount%</b></font>条记录"
                                OnPageChanged="AspNetPager1_PageChanged" PageSize="10">
                            </webdiyer:AspNetPager>
                        </div>
                        <div class="col-xs-3 text-right">
                            每页显示记录数：
                                <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged">
                                    <asp:ListItem>10</asp:ListItem>
                                    <asp:ListItem>20</asp:ListItem>
                                    <asp:ListItem>50</asp:ListItem>
                                    <asp:ListItem>100</asp:ListItem>
                                    <asp:ListItem>200</asp:ListItem>
                                </asp:DropDownList>
                            条
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
                                            <asp:HiddenField ID="HiddenMeetingId" runat="server" />
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
        toastr.options = {
            closeButton: true,
            debug: false,
            progressBar: true,
            positionClass: "toast-top-right",
            timeOut: "3000",
            showMethod: "slideDown",
        };

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
            //$('#EtxtStartTime').datetimepicker({
            //    language: 'zh-CN',
            //    format: 'hh:ii',
            //    startDate: reseStart,
            //    endDate: reseEnd,
            //    weekStart: 1,
            //    todayBtn: false,
            //    autoclose: 1,
            //    minuteStep: timeUnit,
            //    todayHighlight: 1,
            //    startView: 1,
            //    minView: 0,
            //    maxView: 1,
            //    forceParse: 0
            //}).on("changeDate", function () {
            //    if (!this.value) {
            //        $("#EtxtEndTime").datetimepicker("setStartDate", reseStart);
            //        $("#EtxtEndTime").datetimepicker("setEndDate", reseEnd);
            //    } else {
            //        $("#EtxtEndTime").datetimepicker("setStartDate", this.value);
            //        $("#EtxtEndTime").datetimepicker("setEndDate", reseEnd);
            //    }
            //});
            //$('#EtxtEndTime').datetimepicker({
            //    language: 'zh-CN',
            //    format: 'hh:ii',
            //    startDate: reseStart,
            //    endDate: reseEnd,
            //    weekStart: 1,
            //    todayBtn: false,
            //    autoclose: 1,
            //    minuteStep: timeUnit,
            //    todayHighlight: 1,
            //    startView: 1,
            //    minView: 0,
            //    maxView: 1,
            //    forceParse: 0
            //}).on("changeDate", function () {
            //    if (!this.value) {
            //        $("#EtxtStartTime").datetimepicker("setStartDate", reseStart);
            //        $("#EtxtStartTime").datetimepicker("setEndDate", reseEnd);
            //    } else {
            //        $("#EtxtStartTime").datetimepicker("setStartDate", reseStart);
            //        $("#EtxtStartTime").datetimepicker("setEndDate", this.value);
            //    }
            //});
        };
        
        function showDisModal() {
            $('#DisplayModal').modal('toggle');
        };

        function showEditModal() {
            setTimePicker();
            $('#EditModal').modal('toggle');
        };

        $(document).ready(function () {
            
        });

        function Reload() {
            window.location.reload();
        };
        
    </script>
</body>
</html>
