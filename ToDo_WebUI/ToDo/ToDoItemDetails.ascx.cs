using System;

namespace ToDo_WebUI
{
    public partial class ToDoItemDetails : System.Web.UI.UserControl
    {
        private object _dataItem = null;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public object DataItem
        {
            get
            {
                return _dataItem;
            }
            set
            {
                _dataItem = value;
            }
        }
    }
}