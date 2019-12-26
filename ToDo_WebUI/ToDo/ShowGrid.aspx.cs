using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using ToDo_DAL;
using ToDo_Repository;

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

        private IEnumerable<ToDoItem> GetToDoItemsData()
        {
            ToDoItemRepository repo = new ToDoItemRepository(new ToDoContext());
            return repo.FindAll().ToList();
        }

        protected void ToDoItemGrid_PreRender(object sender, EventArgs e)
        {
            (sender as RadGrid).MasterTableView.GetColumnSafe("Id").Visible=false;
            (sender as RadGrid).MasterTableView.GetColumnSafe("WasDoneAt").Visible = false;
            (sender as RadGrid).MasterTableView.GetColumnSafe("DueDate").Visible = false;
        }
    }
}