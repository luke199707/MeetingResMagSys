<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DisplayUnusualMeeting.aspx.cs" Inherits="MeetingResMagSys.Pages.DisplayUnusualMeeting" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>待审核会议</title>
    <script src="../Scripts/jquery.min.js"></script>
    <script src="../Scripts/bootstrap.min.js"></script>
    <link href="../StyleSheet/bootstrap.css" rel="stylesheet" />
    <link href="../StyleSheet/font-awesome-4.7.0/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../StyleSheet/AdminTheme.css" rel="stylesheet" type="text/css" />
    <link href="../Plugins/toastr/toastr.min.css" rel="stylesheet" />
    <script src="../Plugins/toastr/toastr.min.js"></script>
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
                        <li class="active">待审核会议</li>
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
                            class="table table-bordered table-hover" OnRowCommand="GridViewDepart_RowCommand"
                            OnRowDataBound="GridViewDepart_RowDataBound" Width="100%"
                            OnSorting="grid_Sorting">
                            <RowStyle BackColor="#f9f9f9" HorizontalAlign="Center" VerticalAlign="Top" />
                            <AlternatingRowStyle BackColor="White" HorizontalAlign="Center" VerticalAlign="Top" />
                            <Columns>
                                <asp:TemplateField HeaderText="序号" SortExpression="id">
                                    <ItemTemplate>
                                        <asp:Button ID="btnDetail" CommandName="btnDetailCommand" CommandArgument='<%#Eval("id") %>' runat="server" Text="详细信息" Style="display: none;" />
                                        <asp:Literal ID="LiteralIndex" runat="server" Text="<%# Container.DataItemIndex+1 +(AspNetPager1.CurrentPageIndex-1)*AspNetPager1.PageSize %>"></asp:Literal>
                                        <asp:Literal ID="LiteralId" runat="server" Text='<%#Eval("id") %>' Visible="false"></asp:Literal>
                                        <asp:Literal ID="LiteralCode" runat="server" Text='<%#Eval("meetingId") %>' Visible="false"></asp:Literal>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="会议ID" SortExpression="meetingId">
                                    <ItemTemplate>
                                        <%#Eval("meetingId")%>
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
                                        <%#GetBookerName(Eval("booker").ToString())%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="会议日期" SortExpression="time">
                                    <ItemTemplate>
                                        <%#Eval("time")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="预订状态" SortExpression="state">
                                    <ItemTemplate>
                                        <%#Eval("state")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="审核人" SortExpression="reviewer">
                                    <ItemTemplate>
                                        <%#GetBookerName(Eval("reviewer").ToString())%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="拒绝理由" SortExpression="refuseReason">
                                    <ItemTemplate>
                                        <%#Eval("refuseReason")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="操作">
                                    <ItemTemplate>
                                        <asp:Button ID="btnDis" runat="server" CommandArgument='<%# Bind("meetingId") %>' OnClick="btnDis_Click" CssClass="btn btn-info" Text="详情" />
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
                                <h4 class="modal-title" id="DisplayModalLabel">预订信息</h4>
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
                                            <label><strong>日期</strong></label>
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
                                            <label><strong>会议状态</strong></label>
                                            <asp:TextBox ID="DtxtState" class="form-control" runat="server" placeholder="" ReadOnly="true"></asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                            <label><strong>审核人</strong></label>
                                            <asp:TextBox ID="DtxtReviewer" class="form-control" runat="server" placeholder="" ReadOnly="true"></asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                            <label><strong>拒绝理由</strong></label>
                                            <asp:TextBox ID="DtxtRefuseReason" class="form-control" runat="server" placeholder="" ReadOnly="true" TextMode="MultiLine"></asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                            <label><strong>备注</strong></label>
                                            <asp:TextBox ID="DtxtRemark" class="form-control" runat="server" placeholder="" ReadOnly="true" TextMode="MultiLine"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="tab-pane fade" id="DisplayModalright">
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
        toastr.options = {
            closeButton: true,
            debug: false,
            progressBar: true,
            positionClass: "toast-top-right",
            timeOut: "3000",
            showMethod: "slideDown",
        };
        function showDisModal() {
            $('#DisplayModal').modal('toggle');
        };
        function Reload() {
            window.location.reload();
        }
    </script>
</body>
</html>
