<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MagTenant.aspx.cs" Inherits="MeetingResMagSys.Pages.MagTenant" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>租户管理</title>
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
                        <li><i class="fa fa-home fa-fw home-icon"></i><a href="#">租户管理</a></li>
                        <li class="active">租户管理</li>
                    </ul>
                    <ul class="nav navbar-nav navbar-right" style="margin-right: 15px">
                        <li>
                            <asp:Button ID="btnShowAll" runat="server" OnClick="btnShowAll_Click" Text="显示所有租户" CssClass="btn btn-primary" />
                        </li>
                    </ul>
                </div>
                <!-- Content -->
                <div class="space-8"></div>
                <div class="container">
                    <div class="row">
                        <div class="col-xs-3">
                        </div>
                        <div class="col-xs-2">
                            <div class="input-group">
                                <asp:TextBox ID="txtSearchUserId" runat="server" class="form-control" placeholder="租户ID"></asp:TextBox>
                                <asp:LinkButton ID="lbtnSearchUserId" runat="server" OnClick="lbtnSearchUserId_Click" CssClass="input-group-addon"><i class="fa fa-search fa-fw"></i></asp:LinkButton>
                            </div>
                        </div>
                        <div class="col-xs-2">
                            <div class="input-group">
                                <asp:TextBox ID="txtSearchName" runat="server" class="form-control" placeholder="租户名称"></asp:TextBox>
                                <asp:LinkButton ID="lbtnSearchName" runat="server" OnClick="lbtnSearchName_Click" CssClass="input-group-addon"><i class="fa fa-search fa-fw"></i></asp:LinkButton>
                            </div>
                        </div>
                        <div class="col-xs-2">
                            <div class="input-group">
                                <asp:TextBox ID="txtSearchPhone" runat="server" class="form-control" placeholder="租户电话"></asp:TextBox>
                                <asp:LinkButton ID="lbtnSearchPhone" runat="server" OnClick="lbtnSearchPhone_Click" CssClass="input-group-addon"><i class="fa fa-search fa-fw"></i></asp:LinkButton>
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
                                        <asp:Literal ID="LiteralCode" runat="server" Text='<%#Eval("userId") %>' Visible="false"></asp:Literal>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="租户ID" SortExpression="userId">
                                    <ItemTemplate>
                                        <%#Eval("userId")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="租户名称" SortExpression="name">
                                    <ItemTemplate>
                                        <asp:Literal ID="LiteralBan" runat="server" Visible="false"><span class="fa fa-ban text-danger"></span></asp:Literal>
                                        <%#Eval("name")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="邮箱" SortExpression="Email">
                                    <ItemTemplate>
                                        <%#Eval("Email")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="电话" SortExpression="phone">
                                    <ItemTemplate>
                                        <%#Eval("phone")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="角色" SortExpression="role">
                                    <ItemTemplate>
                                        <%#Eval("role")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="组织" SortExpression="organizationId">
                                    <ItemTemplate>
                                        <%# GetORGName(Eval("organizationId").ToString())%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="状态" SortExpression="available">
                                    <ItemTemplate>
                                        <asp:Literal ID="LiteralAvailable" runat="server" Text='<%#Eval("available")%>'></asp:Literal>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="备注" SortExpression="remark">
                                    <ItemTemplate>
                                        <%#Eval("remark")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="操作">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtnEdit" runat="server" CommandArgument='<%# Bind("userId") %>' OnClick="lbtnEdit_Click"><i class="fa fa-edit fa-2x"></i></asp:LinkButton>
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:LinkButton ID="lbtnEnter" runat="server" CommandArgument='<%# Bind("userId") %>' OnClick="lbtnEnter_Click"><i class="fa fa-sign-in fa-2x"></i></asp:LinkButton>
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <a href="javascript:void(0)" onclick="del('<%#Eval("userId")%>')"><i class="fa fa-trash-o fa-2x"></i></a>
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
                <!-- EditModal -->
                <div class="modal fade" id="EditModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
                    <div class="modal-dialog modal-lg" role="document">
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                                <h4 class="modal-title" id="myModalLabel">租户信息</h4>
                            </div>
                            <div class="modal-body">
                                <div class="form-group">
                                    <label>租户ID</label>
                                    <asp:TextBox ID="txtUserId" class="form-control" runat="server" placeholder="" ReadOnly="true"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <label><span style="color: red">*</span>租户名称</label>
                                    <asp:HiddenField ID="HiddenName" runat="server" />
                                    <asp:TextBox ID="txtName" class="form-control" runat="server" placeholder=""></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <label>邮箱</label>
                                    <asp:TextBox ID="txtEmail" class="form-control" runat="server" placeholder=""></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <label>电话</label>
                                    <asp:TextBox ID="txtPhone" class="form-control" runat="server" placeholder=""></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <label>角色</label>
                                    <asp:TextBox ID="txtRole" class="form-control" runat="server" placeholder="" ></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <label>组织ID</label>
                                    <asp:TextBox ID="txtOrgId" class="form-control" runat="server" placeholder="" ReadOnly="true"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <label>组织名称</label>
                                    <asp:TextBox ID="txtOrgName" class="form-control" runat="server" placeholder="" ReadOnly="true"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <label>是否可用</label>
                                    <asp:DropDownList ID="ddlAvailable" runat="server" CssClass="form-control">
                                        <asp:ListItem>可用</asp:ListItem>
                                        <asp:ListItem>禁用</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group">
                                    <label>备注</label>
                                    <asp:TextBox ID="txtRemark" class="form-control" runat="server" placeholder="" TextMode="MultiLine"></asp:TextBox>
                                </div>
                            </div>
                            <div class="modal-footer" style="background-color: white">
                                <button type="button" class="btn btn-default" data-dismiss="modal">关闭</button>
                                <asp:Button OnClick="btnSave_Click" ID="btnSave" runat="server" Text="保存" CssClass="btn btn-primary" />
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
        function del(userId) {
            if (confirm('删除租户会连同该组织全部信息一并清除，确认操作？')) {
                $.post("../Handler/TenantDel.ashx", { "userId": userId }, function (data) {
                    if (data == "ok") {
                        location.href = "MagTenant.aspx";
                    }
                    if (data == "no") {
                        toastr.info("暂时不可删除租户！");
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
