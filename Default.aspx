<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="AngularJsMvcDemo._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container" ng-controller="courseController">
        <h1>Courses Management</h1>

        <div ng-view></div>
       
        <%--<div ng-include="'Templates/course_list.html'"></div>--%>
    </div>
</asp:Content>
