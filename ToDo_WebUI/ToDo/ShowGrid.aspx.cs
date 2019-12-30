using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Web.UI;
using ToDo_BLL.Services;
using ToDo_DAL;
using ToDo_Repository;
using ToDo_WebUI.Mapping;
using ToDo_WebUI.Models;

namespace ToDo_WebUI.ToDo
{
    public partial class ShowGrid : System.Web.UI.Page
    {
        private IEnumerable<ToDoItemViewModel> ToDoItems
        {
            get
            {
                return GetToDoItemsData();
            }
        }

        private IEnumerable<ToDoItemViewModel> GetToDoItemsData()
        {
            #region TODO This block to change later
            IToDoItemRepository repo = new ToDoItemRepository(new ToDoContext());
            IToDoItemService service = new ToDoItemService(repo);
            #endregion

            var toDoViewModelList = new List<ToDoItemViewModel>();
            var toDoDTOList = service.GetAllToDoItems().ToList();
            toDoDTOList.ForEach(item => toDoViewModelList.Add(item.Map_ToDo_DTO_ViewModel()));

            return toDoViewModelList;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ToDoItemGrid.MasterTableView.EditMode = GridEditMode.PopUp;
            }
        }

        protected void ToDoItemGrid_PreRender(object sender, EventArgs e)
        {
            if (!IsPostBack && ToDoItemGrid.MasterTableView.Items.Count>1)
            {
                ToDoItemGrid.MasterTableView.Items[1].Edit = true;
                ToDoItemGrid.MasterTableView.Rebind();
                //(sender as RadGrid).MasterTableView.GetColumnSafe("Id").Visible = false;
                //(sender as RadGrid).MasterTableView.GetColumnSafe("WasDoneAt").Visible = false;
                //(sender as RadGrid).MasterTableView.GetColumnSafe("DueDate").Visible = false;
                //(sender as RadGrid).MasterTableView.GetColumnSafe("AddedByUserId").Visible = false;
            }
        }

        protected void ToDoItemGrid_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            ToDoItemGrid.DataSource = this.ToDoItems;
        }

        protected void ToDoItemGrid_UpdateCommand(object source, GridCommandEventArgs e)
        {
            // TODO implementation
        }

        protected void ToDoItemGrid_InsertCommand(object source, GridCommandEventArgs e)
        {
            // TODO implementation
        }

        protected void ToDoItemGrid_DeleteCommand(object source, GridCommandEventArgs e)
        {
            // TODO implementation
        }
    }
}