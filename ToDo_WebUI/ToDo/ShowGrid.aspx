<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ShowGrid.aspx.cs" Inherits="ToDo_WebUI.ToDo.ShowGrid" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <hgroup>
        <h1>Grid View.</h1>
        <h2 class="text-danger">There should be a list of tasks.</h2>
    </hgroup>
    <telerik:RadGrid ID="ToDoItemGrid" runat="server" RenderMode="Lightweight" AllowPaging="True" AllowSorting="true"
        OnNeedDataSource="ToDoItemGrid_NeedDataSource"  PageSize="5" OnPreRender="ToDoItemGrid_PreRender">
        <PagerStyle PageSizeControlType="RadComboBox"></PagerStyle>
        <MasterTableView PagerStyle-AlwaysVisible="true">
            <PagerStyle Mode="NextPrevAndNumeric" PageSizeLabelText="Page Size: " PageSizes="5,10,25" />
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>
