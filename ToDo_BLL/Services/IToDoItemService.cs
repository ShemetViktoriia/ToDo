using System.Collections.Generic;
using ToDo_BLL.DTO;

namespace ToDo_BLL.Services
{
    public interface IToDoItemService
    {
        ToDoItemDTO FindById(int id);
        IEnumerable<ToDoItemDTO> GetAllItems();
        void DeleteItem(int id);
        void AddOrEditItem(ToDoItemDTO item);
    }
}
