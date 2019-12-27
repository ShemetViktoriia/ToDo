using System.Collections.Generic;
using ToDo_BLL.DTO;

namespace ToDo_BLL.Services
{
    public interface IToDoItemService
    {
        IEnumerable<ToDoItemDTO> GetAllToDoItems();
    }
}
