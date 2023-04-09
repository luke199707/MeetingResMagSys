<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DataExtension.aspx.cs" Inherits="MeetingResMagSys.Pages.DataExtension" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>数据扩展管理</title>
    <script src="../Scripts/jquery.min.js"></script>
    <script src="../Scripts/bootstrap.min.js"></script>
    <script src="../Plugins/toastr/toastr.min.js"></script>
    <link href="../StyleSheet/bootstrap.css" rel="stylesheet" />
    <link href="../StyleSheet/font-awesome-4.7.0/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../StyleSheet/AdminTheme.css" rel="stylesheet" type="text/css" />
    <link href="../Plugins/toastr/toastr.min.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server" class="form-horizontal">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <!-- Head -->
                <div class="breadcrumbs" id="breadcrumbs" style="background-color: #ffffff">
                    <ul class="breadcrumb">
                        <li><i class="fa fa-home fa-fw home-icon"></i><a href="#">数据扩展管理</a></li>
                        <li class="active">数据扩展管理</li>
                    </ul>
                </div>
                <!-- Head End -->
                <!-- Content -->
                <div class="space-8"></div>
                <div class="form-group">
                    <label class="col-sm-2 col-sm-offset-3 control-label">要扩展的表</label>
                    <div class="col-sm-3">
                        <asp:DropDownList ID="ddlExtTable" class="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlExtTable_SelectedIndexChanged">
                            <asp:ListItem Value="MeetingRoomType">会议室类型表</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="space-8"></div>
                <!-- gridview -->
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
                                        <asp:Literal ID="LiteralIndex" runat="server" Text="<%# Container.DataItemIndex+1 +(AspNetPager1.CurrentPageIndex-1)*AspNetPager1.PageSize %>"></asp:Literal>
                                        <asp:Literal ID="LiteralId" runat="server" Text='<%#Eval("id") %>' Visible="false"></asp:Literal>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="属性字段名" SortExpression="cname">
                                    <ItemTemplate>
                                        <%#Eval("cname")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="意义" SortExpression="lable">
                                    <ItemTemplate>
                                        <%#Eval("lable")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="是否为扩展字段" SortExpression="isExtension">
                                    <ItemTemplate>
                                        <%#Eval("isExtension")%>
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
                                <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged">
                                    <asp:ListItem>10</asp:ListItem>
                                    <asp:ListItem>20</asp:ListItem>
                                    <asp:ListItem>50</asp:ListItem>
                                </asp:DropDownList>
                            条
                        </div>
                    </div>
                </div>
                <!-- gridview End -->
                <div class="space-8"></div>
                <div class="form-group">
                    <label class="col-sm-2 col-sm-offset-3 control-label"><span style="color: red">*</span>字段名称</label>
                    <div class="col-sm-3">
                        <asp:TextBox ID="txtCname" class="form-control" runat="server"></asp:TextBox>
                        <!-- 表单校验 -->
                        <asp:RequiredFieldValidator ID="rvfCname" runat="server" Style="color: red" Display="Dynamic"
                            ControlToValidate="txtCname"
                            ErrorMessage="字段名称不能为空">
                        </asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="revCname" runat="server" Style="color: red" Display="Dynamic"
                            ControlToValidate="txtCname"
                            ValidationExpression="^[a-zA-Z0-9_]+$"
                            ErrorMessage="请输入只包含字母、数字和下划线的字符串">
                        </asp:RegularExpressionValidator>
                        <!-- 表单校验 End -->
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-2 col-sm-offset-3 control-label"><span style="color: red">*</span>字段意义</label>
                    <div class="col-sm-3">
                        <asp:TextBox ID="txtLable" class="form-control" runat="server"></asp:TextBox>
                        <!-- 表单校验 -->
                        <asp:RequiredFieldValidator ID="rfvLable" runat="server" Style="color: red" Display="Dynamic"
                            ControlToValidate="txtLable"
                            ErrorMessage="字段意义不能为空">
                        </asp:RequiredFieldValidator>
                        <!-- 表单校验 End -->
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-2 col-sm-offset-3 control-label"><span style="color: red">*</span>字段类型</label>
                    <div class="col-sm-3">
                        <asp:DropDownList ID="ddlType" class="form-control" runat="server">
                            <asp:ListItem>varchar</asp:ListItem>
                            <asp:ListItem>char</asp:ListItem>
                            <asp:ListItem>int</asp:ListItem>
                            <asp:ListItem>date</asp:ListItem>
                            <asp:ListItem>datetime</asp:ListItem>
                            <asp:ListItem>float</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-2 col-sm-offset-3 control-label">字段长度</label>
                    <div class="col-sm-3">
                        <asp:TextBox ID="txtLength" class="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-sm-3 col-sm-offset-5">
                        <asp:Button ID="btnSave" runat="server" Text="确认扩展" class="btn btn-primary btn-sm" OnClick="btnSave_Click" />
                    </div>
                </div>
                <!-- Content End -->
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
    <script type="text/javascript">
        toastr.options = {
            closeButton: true,
            debug: false,
            progressBar: true,
            positionClass: "toast-top-right",
            timeOut: "5000",
            showMethod: "slideDown",
        };
        function Reload() {
            window.location.reload();
        }
    </script>
</body>
</html>
