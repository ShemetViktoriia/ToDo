using Autofac.Integration.Web.Forms;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using ToDo_BLL.Services;
using ToDo_WebUI.Mapping;
using ToDo_WebUI.Models;

namespace ToDo_WebUI.ToDo
{
    [InjectProperties]
    public partial class ShowGrid : System.Web.UI.Page
    {
        public IToDoItemService ToDoservice { get; set; }

        private IEnumerable<ToDoItemViewModel> ToDoItems
        {
            get
            {
                return GetToDoItemsData();
            }
        }

        private IEnumerable<ToDoItemViewModel> GetToDoItemsData()
        {
            var toDoViewModelList = new List<ToDoItemViewModel>();
            var toDoDTOList = ToDoservice.GetAllItems().ToList();
            toDoDTOList.ForEach(item => toDoViewModelList.Add(item.Map_ToDo_DTO_ViewModel()));

            return toDoViewModelList;
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ToDoItemGrid_PreRender(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ToDoItemGrid.MasterTableView.Rebind();
            }
        }

        protected void ToDoItemGrid_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            ToDoItemGrid.DataSource = this.ToDoItems;
        }

        protected void ToDoItemGrid_UpdateCommand(object source, GridCommandEventArgs e)
        {
            var toDoItemViewModel = getModelFromUserControl(e);
            GridEditableItem editedItem = e.Item as GridEditableItem;
            var id = (int)editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["Id"];
            var toDoModel = ToDoservice.FindById(id).Map_ToDo_DTO_ViewModel();

            // set the rest of the properties that are not in control, from DB
            toDoItemViewModel.Id = id;
            toDoItemViewModel.AddedAt = toDoModel.AddedAt;
            toDoItemViewModel.AddedByUserId = toDoModel.AddedByUserId;
            toDoItemViewModel.WasDoneAt = toDoModel.WasDoneAt;

            ToDoservice.AddOrEditItem(toDoItemViewModel.Map_ToDo_ViewModel_DTO());
        }

        protected void ToDoItemGrid_InsertCommand(object source, GridCommandEventArgs e)
        {
            var toDoItemViewModel = getModelFromUserControl(e);

            // set the rest of the properties that are not in control
            toDoItemViewModel.AddedAt = DateTime.Now;
            toDoItemViewModel.AddedByUserId = User.Identity.GetUserId<int>();
            toDoItemViewModel.WasDoneAt = null;

            ToDoservice.AddOrEditItem(toDoItemViewModel.Map_ToDo_ViewModel_DTO());
        }

        protected void ToDoItemGrid_DeleteCommand(object source, GridCommandEventArgs e)
        {
            var toDoId = (int)((GridDataItem)e.Item).GetDataKeyValue("Id");
            ToDoservice.DeleteItem(toDoId);
        }

        private ToDoItemViewModel getModelFromUserControl(GridCommandEventArgs e)
        {
            var userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            var toDoItemViewModel = new ToDoItemViewModel
            {
                Description = (userControl.FindControl("DescriptionTextBox") as TextBox).Text,
                WasDone = (userControl.FindControl("WasDoneCheckBox") as RadCheckBox).Checked ?? false,
                DueDate = (userControl.FindControl("DueDateTimePicker") as RadDateTimePicker).SelectedDate
            };
            return toDoItemViewModel;
        }
    }
}