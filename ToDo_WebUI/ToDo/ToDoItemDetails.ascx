<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ToDoItemDetails.ascx.cs" Inherits="ToDo_WebUI.ToDoItemDetails" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<table id="Table1" border="0" style="border-collapse: collapse;">
    <tr class="EditFormHeader">
        <td colspan="2">
            <b>ToDoItem Details</b>
        </td>
    </tr>
    <tr>
        <td>
            <table id="Table2" border="0" class="module">
<%--                <asp:Label ID="ToDoIdLabel" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Id") %>'
                    Hidden ="true">
                </asp:Label>--%>
                <tr>
                    <td>Description:
                    </td>
                    <td>
                        <asp:TextBox ID="DescriptionTextBox" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Description") %>'
                            TextMode="MultiLine" Rows="4" Columns="30">
                        </asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>Was Done:
                    </td>
                    <td>
                        <telerik:RadCheckBox runat="server" ID="WasDoneCheckBox" Checked='<%# DataBinder.Eval(Container, "DataItem.WasDone") %>'>
                        </telerik:RadCheckBox>
                    </td>
                </tr>
                <tr>
                    <td>Due Date:
                    </td>
                    <td>
                        <telerik:RadDateTimePicker RenderMode="Lightweight" ID="DueDateTimePicker" Width="100%" runat="server"
                            MinDate="1/1/2000" DbSelectedDate='<%# DataBinder.Eval(Container, "DataItem.DueDate") %>' TabIndex="2">
                        </telerik:RadDateTimePicker>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td colspan="2"></td>
    </tr>
    <tr>
        <td></td>
        <td></td>
    </tr>
    <tr>
        <td colspan="2">
            <asp:Button ID="btnUpdate" Text='<%# (Container is GridEditFormInsertItem) ? "Insert" : "Update" %>'
                runat="server" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>'></asp:Button>&nbsp;
                                    <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                                        CommandName="Cancel"></asp:Button>
        </td>
    </tr>
</table>
