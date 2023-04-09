<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MagMeetRoom.aspx.cs" Inherits="MeetingResMagSys.Pages.MagMeetRoom" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>会议室管理</title>
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
                        <li><i class="fa fa-home fa-fw home-icon"></i><a href="#">会议室管理</a></li>
                        <li class="active">会议室管理</li>
                    </ul>
                    <ul class="nav navbar-nav navbar-right" style="margin-right: 15px">
                        <li>
                            <asp:Button data-toggle="modal" data-target="#AddModal" ID="btnAdd" runat="server" Text="添加会议室" CssClass="btn btn-primary" />
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
                                <asp:TextBox ID="txtSearchName" runat="server" class="form-control" placeholder="会议室名称"></asp:TextBox>
                                <asp:LinkButton ID="lbtnSearchName" runat="server" OnClick="lbtnSearchName_Click" CssClass="input-group-addon"><i class="fa fa-search fa-fw"></i></asp:LinkButton>
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
                                        <asp:Literal ID="LiteralCode" runat="server" Text='<%#Eval("roomId") %>' Visible="false"></asp:Literal>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="会议室名称" SortExpression="name">
                                    <ItemTemplate>
                                        <asp:Literal ID="LiteralBan" runat="server" Visible="false"><span class="fa fa-ban text-danger"></span></asp:Literal>
                                        <%#Eval("name")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="负责人" SortExpression="director">
                                    <ItemTemplate>
                                        <%# GetDirectorName(Eval("director").ToString())%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="办公区域" SortExpression="officeArea">
                                    <ItemTemplate>
                                        <%# GetOfficeAreaName(Eval("officeArea").ToString())%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="位置" SortExpression="position">
                                    <ItemTemplate>
                                        <%#Eval("position")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="容纳人数" SortExpression="capacity">
                                    <ItemTemplate>
                                        <%#Eval("capacity")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="类型" SortExpression="type">
                                    <ItemTemplate>
                                        <%# GetTypeName(Eval("type").ToString())%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="状态" SortExpression="available">
                                    <ItemTemplate>
                                        <asp:Literal ID="LiteralAvailable" runat="server" Text='<%#Eval("available")%>'></asp:Literal>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="预订是否需要审核" SortExpression="isCheck">
                                    <ItemTemplate>
                                        <asp:Literal ID="LiteralIsCheck" runat="server" Text='<%#Eval("isCheck")%>'></asp:Literal>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="备注" SortExpression="remark">
                                    <ItemTemplate>
                                        <%#Eval("remark")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="操作">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtnEdit" runat="server" CommandArgument='<%# Bind("roomId") %>' OnClick="lbtnEdit_Click"><i class="fa fa-edit fa-2x"></i></asp:LinkButton>
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <a href="javascript:void(0)" onclick="del('<%#Eval("roomId")%>')"><i class="fa fa-trash-o fa-2x"></i></a>
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
                <!-- AddModal -->
                <div class="modal fade" id="AddModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
                    <div class="modal-dialog modal-lg" role="document">
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                                <h4 class="modal-title" id="myModalLabel">会议室信息</h4>
                            </div>
                            <div class="modal-body">
                                <ul id="myTab" class="nav nav-tabs">
                                    <li class="active"><a href="#info" data-toggle="tab">基本信息</a></li>
                                    <li><a href="#right" data-toggle="tab">权限设置</a></li>
                                </ul>
                                <div id="myTabContent" class="tab-content">
                                    <div class="tab-pane fade in active" id="info">
                                        <div class="form-group">
                                            <label><span style="color: red">*</span>会议室名称</label>
                                            <asp:TextBox ID="txtName" class="form-control" runat="server" placeholder=""></asp:TextBox>
                                            <asp:HiddenField ID="HiddenName" runat="server" />
                                        </div>
                                        <div class="form-group">
                                            <label><span style="color: red">*</span>负责人</label>
                                            <asp:DropDownList ID="ddlDirector" runat="server" CssClass="form-control"></asp:DropDownList>
                                        </div>
                                        <div class="form-group">
                                            <label><span style="color: red">*</span>办公区域</label>
                                            <asp:DropDownList ID="ddlOfficeArea" runat="server" CssClass="form-control"></asp:DropDownList>
                                        </div>
                                        <div class="form-group">
                                            <label>位置</label>
                                            <asp:TextBox ID="txtPosition" class="form-control" runat="server" placeholder=""></asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                            <label>容纳人数</label>
                                            <asp:TextBox ID="txtCapacity" class="form-control" runat="server" placeholder="" TextMode="Number"></asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                            <label><span style="color: red">*</span>类型</label>
                                            <asp:DropDownList ID="ddlType" runat="server" CssClass="form-control"></asp:DropDownList>
                                        </div>
                                        <div class="form-group">
                                            <label>简介</label>
                                            <asp:TextBox ID="txtIntroduction" class="form-control" runat="server" placeholder="" TextMode="MultiLine"></asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                            <div><label>设施</label></div>
                                            <asp:CheckBoxList ID="cblFacility" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow"></asp:CheckBoxList>
                                        </div>
                                        <div class="form-group">
                                            <label>注意事项</label>
                                            <asp:TextBox ID="txtAttention" class="form-control" runat="server" placeholder="" TextMode="MultiLine"></asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                            <label>备注</label>
                                            <asp:TextBox ID="txtRemark" class="form-control" runat="server" placeholder="" TextMode="MultiLine"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="tab-pane fade" id="right">
                                        <div class="form-group">
                                            <label>负责部门</label>
                                            <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="form-control"></asp:DropDownList>
                                        </div>
                                        <div class="form-group">
                                            <div><label>禁止预订的部门</label></div>
                                            <asp:CheckBoxList ID="cblBanDep" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow"></asp:CheckBoxList>
                                        </div>
                                        <div class="form-group">
                                            <label>预订是否需要审核</label>
                                            <asp:DropDownList ID="ddlIsCheck" runat="server" CssClass="form-control">
                                                <asp:ListItem>是</asp:ListItem>
                                                <asp:ListItem Selected="True">否</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer" style="background-color: white">
                                <button type="button" class="btn btn-default" data-dismiss="modal">关闭</button>
                                <asp:Button OnClick="btnAddCertain_Click" ID="btnAddCertain" runat="server" Text="添加" CssClass="btn btn-primary" />
                            </div>
                        </div>
                    </div>
                </div>
                <!-- EditModal -->
                <div class="modal fade" id="EditModal" tabindex="-1" role="dialog" aria-labelledby="EditModalLabel">
                    <div class="modal-dialog modal-lg" role="document">
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                                <h4 class="modal-title" id="EditModalLabel">会议室信息</h4>
                            </div>
                            <div class="modal-body">
                                <ul id="myTab2" class="nav nav-tabs">
                                    <li class="active"><a href="#info2" data-toggle="tab">基本信息</a></li>
                                    <li><a href="#right2" data-toggle="tab">权限设置</a></li>
                                </ul>
                                <div id="myTabContent2" class="tab-content">
                                    <div class="tab-pane fade in active" id="info2">
                                        <div class="form-group">
                                            <label><span style="color: red">*</span>会议室名称</label>
                                            <asp:TextBox ID="etxtName" class="form-control" runat="server" placeholder=""></asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                            <label><span style="color: red">*</span>负责人</label>
                                            <asp:DropDownList ID="eddlDirector" runat="server" CssClass="form-control"></asp:DropDownList>
                                        </div>
                                        <div class="form-group">
                                            <label><span style="color: red">*</span>办公区域</label>
                                            <asp:DropDownList ID="eddlOfficeArea" runat="server" CssClass="form-control">
                                                <asp:ListItem>信息馆4楼</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="form-group">
                                            <label>位置</label>
                                            <asp:TextBox ID="etxtPosition" class="form-control" runat="server" placeholder=""></asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                            <label>容纳人数</label>
                                            <asp:TextBox ID="etxtCapacity" class="form-control" runat="server" placeholder="" TextMode="Number"></asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                            <label><span style="color: red">*</span>类型</label>
                                            <asp:DropDownList ID="eddlType" runat="server" CssClass="form-control"></asp:DropDownList>
                                        </div>
                                        <div class="form-group">
                                            <label>简介</label>
                                            <asp:TextBox ID="etxtIntroduction" class="form-control" runat="server" placeholder="" TextMode="MultiLine"></asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                            <div><label>设施</label></div>
                                            <asp:CheckBoxList ID="ecblFacility" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow"></asp:CheckBoxList>
                                        </div>
                                        <div class="form-group">
                                            <label>注意事项</label>
                                            <asp:TextBox ID="etxtAttention" class="form-control" runat="server" placeholder="" TextMode="MultiLine"></asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                            <label>备注</label>
                                            <asp:TextBox ID="etxtRemark" class="form-control" runat="server" placeholder="" TextMode="MultiLine"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="tab-pane fade" id="right2">
                                        <div class="form-group">
                                            <label>负责部门</label>
                                            <asp:DropDownList ID="eddlDepartment" runat="server" CssClass="form-control"></asp:DropDownList>
                                        </div>
                                        <div class="form-group">
                                            <div><label>禁止预订的部门</label></div>
                                            <asp:CheckBoxList ID="ecblBanDep" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow"></asp:CheckBoxList>
                                        </div>
                                        <div class="form-group">
                                            <label>预订是否需要审核</label>
                                            <asp:DropDownList ID="eddlIsCheck" runat="server" CssClass="form-control">
                                                <asp:ListItem>是</asp:ListItem>
                                                <asp:ListItem>否</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="form-group">
                                            <label>会议室是否可用</label>
                                            <asp:DropDownList ID="eddlAvailable" runat="server" CssClass="form-control">
                                                <asp:ListItem>可用</asp:ListItem>
                                                <asp:ListItem>禁用</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer" style="background-color: white">
                                <button type="button" class="btn btn-default" data-dismiss="modal">关闭</button>
                                <asp:Button ID="btnSave" runat="server" Text="保存" OnClick="btnSave_Click" CssClass="btn btn-primary" />
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
        $(document).ready(function () {
            //清空Modal,Modal隐藏时触发
            $('#AddModal').on('hidden.bs.modal', function () {
                $("#txtName").val("");
                $("#txtPosition").val("");
                $("#txtCapacity").val("");
                $("#txtIntroduction").val("");
                $("#txtFacility").val("");
                $("#txtAttention").val("");
                $("#txtRemark").val("");
                $("#txtResDep").val("");
                $("#txtUseRole").val("");
                $("#txtUseDep").val("");
                $("#txtNotDep").val("");
            });
            $('#EditModal').on('hidden.bs.modal', function () {
                $("#etxtName").val("");
                $("#etxtPosition").val("");
                $("#etxtCapacity").val("");
                $("#etxtIntroduction").val("");
                $("#etxtFacility").val("");
                $("#etxtAttention").val("");
                $("#etxtRemark").val("");
                $("#etxtResDep").val("");
                $("#etxtUseRole").val("");
                $("#etxtUseDep").val("");
                $("#etxtNotDep").val("");
            });
        });
        function del(roomId) {
            if (confirm('确认要删除此项？')) {
                $.post("../Handler/MeetingRoomDel.ashx", { "roomId": roomId }, function (data) {
                    if (data == "ok") {
                        location.href = "MagMeetRoom.aspx";
                    }
                });
            } else {
                return false;
            }
        }
        function showEditModal() {
            $('#EditModal').modal('show');
        }
        function Reload() {
            window.location.reload();
        }
    </script>
</body>
</html>
