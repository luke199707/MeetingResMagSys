<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Redirect.aspx.cs" Inherits="MeetingResMagSys.Layout.Redirect" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="breadcrumbs" id="breadcrumbs">
        <ul class="breadcrumb">
            <li><i class="fa fa-home fa-fw home-icon"></i><a href="#">主页</a> </li>
            <li class="active">跳转页面</li>
        </ul>
    </div>
    <!-- .breadcrumb -->
    <div class="page-content">
        <div class="page-header">
            <h1>
                跳转页面 <small><i class="fa fa-angle-double-right fa-fw"></i>由于用户的操作提示跳转页面</small>
            </h1>
        </div>
        <div class="row">
            <div class="col-xs-12">
                <!-- PAGE CONTENT BEGINS -->
                    <div id="divInfo" runat="server" style=" font-size:14px; margin-top:10px; margin-left:20px;">
                    </div>
                <!-- PAGE CONTENT ENDS -->
            </div>
        </div>
    </div>
</asp:Content>
