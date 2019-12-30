<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ShowGrid.aspx.cs" Inherits="ToDo_WebUI.ToDo.ShowGrid" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <hgroup>
        <h1>A list of tasks.</h1>
    </hgroup>

    <%--<telerik:RadScriptManager runat="server" ID="RadScriptManager" />--%> 
    <telerik:RadSkinManager ID="RadSkinManager" runat="server" ShowChooser="true" />
    <telerik:RadCodeBlock ID="RadCodeBlock" runat="server">
        <script type="text/javascript">
            function RowDblClick(sender, eventArgs) {
                sender.get_masterTableView().editItem(eventArgs.get_itemIndexHierarchical());
            }

            function onPopUpShowing(sender, args) {
                args.get_popUp().className += " popUpEditForm";
            }
        </script>
    </telerik:RadCodeBlock>

    <telerik:RadAjaxManager ID="RadAjaxManager" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="ToDoItemGrid">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="ToDoItemGrid" LoadingPanelID="RadAjaxLoadingPanel"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ConfiguratorPanel">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="ToDoItemGrid" LoadingPanelID="RadAjaxLoadingPanel"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>

    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel" runat="server">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadFormDecorator RenderMode="Lightweight" ID="RadFormDecorator" runat="server" DecorationZoneID="demo" DecoratedControls="All" EnableRoundedCorners="false" />


    <telerik:RadGrid RenderMode="Lightweight" ID="ToDoItemGrid" runat="server" AllowPaging="True" ShowFooter="true"
        AllowSorting="True" AutoGenerateColumns="False" ShowStatusBar="true"
        OnPreRender="ToDoItemGrid_PreRender" OnNeedDataSource="ToDoItemGrid_NeedDataSource" OnUpdateCommand="ToDoItemGrid_UpdateCommand"
        OnInsertCommand="ToDoItemGrid_InsertCommand" OnDeleteCommand="ToDoItemGrid_DeleteCommand" PageSize="5">

        <MasterTableView Width="100%" CommandItemDisplay="Top" DataKeyNames="Id" EditFormSettings-PopUpSettings-KeepInScreenBounds="true" PagerStyle-AlwaysVisible="true">
            <Columns>
                <telerik:GridEditCommandColumn UniqueName="EditCommandColumn">
                </telerik:GridEditCommandColumn>
                <telerik:GridBoundColumn UniqueName="Description" HeaderText="Description" DataField="Description">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn UniqueName="WasDone" HeaderText="Was Done" DataField="WasDone">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn UniqueName="AddedAt" HeaderText="Added At" DataField="AddedAt">
                </telerik:GridBoundColumn>
                 <telerik:GridBoundColumn UniqueName="AddedByUserName" HeaderText="Added By User" DataField="AddedByUserName">
                </telerik:GridBoundColumn>
                <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete">
                </telerik:GridButtonColumn>
            </Columns>

            <EditFormSettings UserControlName="ToDoItemDetails.ascx" EditFormType="WebUserControl">
                <EditColumn UniqueName="EditCommandColumn">
                </EditColumn>
            </EditFormSettings>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>
