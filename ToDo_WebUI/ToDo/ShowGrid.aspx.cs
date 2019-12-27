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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadDataForToDoItemGrid();
            }
        }

        protected void ToDoItemGrid_PageSizeChanged(object sender, GridPageSizeChangedEventArgs e)
        {
            LoadDataForToDoItemGrid();
        }

        protected void ToDoItemGrid_PageIndexChanged(object sender, Telerik.Web.UI.GridPageChangedEventArgs e)
        {
            LoadDataForToDoItemGrid();
        }

        protected void ToDoItemGrid_SortCommand(object sender, Telerik.Web.UI.GridSortCommandEventArgs e)
        {
            LoadDataForToDoItemGrid();
        }

        private void LoadDataForToDoItemGrid()
        {
            ToDoItemGrid.DataSource = GetToDoItemsData();
        }

        protected void ToDoItemGrid_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            (sender as RadGrid).DataSource = GetToDoItemsData();
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

        protected void ToDoItemGrid_PreRender(object sender, EventArgs e)
        {
            (sender as RadGrid).MasterTableView.GetColumnSafe("Id").Visible = false;
            (sender as RadGrid).MasterTableView.GetColumnSafe("WasDoneAt").Visible = false;
            (sender as RadGrid).MasterTableView.GetColumnSafe("DueDate").Visible = false;
            (sender as RadGrid).MasterTableView.GetColumnSafe("AddedByUserId").Visible = false;
        }
    }
}