<%@ Page Title="内置角色设置页" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="SetDefaultRoleRight.aspx.cs" Inherits="MeetingResMagSys.Pages.SetDefaultRoleRight" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Scripts/fancybox2.1.5/source/jquery.fancybox.css" rel="stylesheet" />
    <script src="../Scripts/jquery-3.4.1.min.js"></script>
    <script src="../Scripts/fancybox2.1.5/source/jquery.fancybox.js"></script>
    <script src="../Scripts/fancybox2.1.5/source/jquery.fancybox.pack.js"></script>
    <script src="../Scripts/fancybox2.1.5/source/jquery.mousewheel-3.0.6.pack.js"></script>
    <link href="../Plugins/toastr/toastr.min.css" rel="stylesheet" />
    <script src="../Plugins/toastr/toastr.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="breadcrumbs" id="breadcrumbs" style="background-color: #ffffff">
                <ul class="breadcrumb">
                    <li><i class="fa fa-home fa-fw home-icon"></i><a href="#">系统管理</a> </li>
                    <li class="active">默认角色设置</li>
                </ul>
            </div>
            <div class="space-8"></div>
            <div class="container">
                <div class="row">
                    <div class="row form-horizontal">
                        <div class="col-xs-2 text-right">
                            <label class="control-label">角色ID：</label>
                        </div>
                        <div class="col-xs-2" style="padding-left: 0px;">
                            <asp:TextBox ID="txtRoleId" runat="server" class="form-control input-sm"></asp:TextBox>
                        </div>
                        <div class="col-xs-1 text-right">
                            <label class="control-label">角色名称：</label>
                        </div>
                        <div class="col-xs-2" style="padding-left: 0px;">
                            <asp:TextBox ID="txtRoleName" runat="server" class="form-control input-sm"></asp:TextBox>
                        </div>
                        <div class="col-xs-1 text-right">
                            <label class="control-label">备注：</label>
                        </div>
                        <div class="col-xs-2" style="padding-left: 0px;">
                            <asp:TextBox ID="txtRemark" runat="server" class="form-control input-sm"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="space-8"></div>
                <div class="row">
                    <div class="row no-margin">
                        <div class="col-xs-4 col-xs-offset-4 text-center">
                            <div class="btn-group">
                                <asp:Button ID="btnAdd" runat="server" Text="添加" OnClick="btnAdd_Click" class="btn btn-default btn-sm" />
                                <asp:Button ID="btnCertain" runat="server" Text="确定" OnClick="btnCertain_Click" class="btn btn-default btn-sm" />
                                <asp:Button ID="btnAddCancel" runat="server" Text="取消" OnClick="btnAddCancel_Click" class="btn btn-default btn-sm" />
                                <asp:Button ID="btnUpdate" runat="server" Text="修改" OnClick="btnUpdate_Click" class="btn btn-default btn-sm" />
                                <asp:Button ID="btnSave" runat="server" Text="保存" OnClick="btnSave_Click" class="btn btn-default btn-sm" />
                                <asp:Button ID="btnUpdateCancel" runat="server" Text="取消" OnClick="btnUpdateCancel_Click" class="btn btn-default btn-sm" />
                                <asp:Button ID="btnDel" runat="server" Text="删除" OnClick="btnDel_Click" OnClientClick="javascript:return confirm('是否删除该项？');" class="btn btn-default btn-sm" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="space-8"></div>
            <div id="gridview" style="padding-top: 0px; margin: 10px; padding-left: 0px">
                <div class="row no-margin">
                    <asp:Literal ID="hiddenid" runat="server" Visible="false"></asp:Literal>
                    <asp:GridView ID="gvRole" runat="server" AutoGenerateColumns="false" AllowSorting="true"
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
                                    <asp:Literal ID="LiteralCode" runat="server" Text='<%#Eval("roleId") %>' Visible="false"></asp:Literal>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="角色ID" SortExpression="roleId">
                                <ItemTemplate>
                                    <%#Eval("roleId")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="角色名称" SortExpression="roleName">
                                <ItemTemplate>
                                    <%#Eval("roleName")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="备注" SortExpression="remark">
                                <ItemTemplate>
                                    <%#Eval("remark")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="操作">
                                <ItemTemplate>
                                    <a href="javascript:void(0)" onclick="selectRight('<%#Eval("roleId") %>')">设置权限</a>
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
        </ContentTemplate>
    </asp:UpdatePanel>
    <script type="text/javascript">
        toastr.options = {
            closeButton: true,
            debug: false,
            progressBar: true,
            positionClass: "toast-top-right",
            timeOut: "5000",
            showMethod: "slideDown",
        };
        function selectRight(roleId) {
            $.fancybox.open({
                href: 'RightSetBox.aspx?roleId=' + roleId,
                type: 'iframe',
                padding: 5,
                width: '50%',
                height: '80%',
            })
        };
        function AfterSelectProject() {
            $.fancybox.close();
            //window.parent.location.href = '../Layout/Default.html';
        };
    </script>
</asp:Content>
