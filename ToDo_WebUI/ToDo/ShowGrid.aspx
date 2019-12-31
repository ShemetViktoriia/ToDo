<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ShowGrid.aspx.cs" Inherits="ToDo_WebUI.ToDo.ShowGrid" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <hgroup>
        <h1>A list of tasks.</h1>
    </hgroup>

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
                    <telerik:AjaxUpdatedControl ControlID="ToDoItemGrid" LoadingPanelID="RadAjaxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="RadWindowManager" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>

    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel" runat="server" />
    <telerik:RadFormDecorator RenderMode="Lightweight" ID="RadFormDecorator" runat="server" DecorationZoneID="demo" DecoratedControls="All" EnableRoundedCorners="false" />

    <div id="demo" class="demo-container no-bg">

        <telerik:RadGrid RenderMode="Lightweight" ID="ToDoItemGrid" runat="server" AllowPaging="True" ShowFooter="True"
            AllowSorting="True" AutoGenerateColumns="False" ShowStatusBar="True"
            OnPreRender="ToDoItemGrid_PreRender" OnNeedDataSource="ToDoItemGrid_NeedDataSource" OnUpdateCommand="ToDoItemGrid_UpdateCommand"
            OnInsertCommand="ToDoItemGrid_InsertCommand" OnDeleteCommand="ToDoItemGrid_DeleteCommand" PageSize="5" Skin="Outlook">

            <MasterTableView EditMode="PopUp" Width="100%" CommandItemDisplay="Top" DataKeyNames="Id"
                EditFormSettings-PopUpSettings-KeepInScreenBounds="true" PagerStyle-AlwaysVisible="true">
                <Columns>
                    <telerik:GridEditCommandColumn UniqueName="EditCommandColumn">
                    </telerik:GridEditCommandColumn>
                    <telerik:GridBoundColumn UniqueName="Description" HeaderText="Description" DataField="Description">
                        <ColumnValidationSettings EnableRequiredFieldValidation="true">
                            <RequiredFieldValidator ErrorMessage="This field is required!" Display="Dynamic"></RequiredFieldValidator>
                        </ColumnValidationSettings>
                    </telerik:GridBoundColumn>
                    <telerik:GridCheckBoxColumn UniqueName="WasDone" HeaderText="Was Done" DataField="WasDone">
                    </telerik:GridCheckBoxColumn>
                    <telerik:GridBoundColumn UniqueName="AddedAt" HeaderText="Added At" DataField="AddedAt">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn UniqueName="AddedByUserName" HeaderText="Added By User" DataField="AddedByUserName">
                    </telerik:GridBoundColumn>
                    <telerik:GridButtonColumn ConfirmText="Delete this item?" ConfirmDialogType="RadWindow"
                        ConfirmTitle="Delete" ButtonType="FontIconButton" CommandName="Delete" />
                </Columns>

                <EditFormSettings UserControlName="ToDoItemDetails.ascx" EditFormType="WebUserControl">
                    <EditColumn UniqueName="EditCommandColumn">
                    </EditColumn>
                    <PopUpSettings KeepInScreenBounds="True"></PopUpSettings>
                </EditFormSettings>

                <PagerStyle AlwaysVisible="True"></PagerStyle>
            </MasterTableView>

            <SortingSettings SortedBackColor="Yellow" />

            <ClientSettings>
                <ClientEvents OnRowDblClick="RowDblClick" OnPopUpShowing="onPopUpShowing" />
            </ClientSettings>

        </telerik:RadGrid>
    </div>
    <telerik:RadWindowManager RenderMode="Lightweight" ID="RadWindowManager" runat="server" />
</asp:Content>
