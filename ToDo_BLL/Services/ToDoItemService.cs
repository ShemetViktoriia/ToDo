using System.Collections.Generic;
using System.Linq;
using ToDo_BLL.DTO;
using ToDo_BLL.Mapping;
using ToDo_Repository;

namespace ToDo_BLL.Services
{
    public class ToDoItemService: IToDoItemService
    {
        readonly IToDoItemRepository _toDoRepo;

        public ToDoItemService(IToDoItemRepository toDoRepo)
        {
            _toDoRepo = toDoRepo;
        }

        public IEnumerable<ToDoItemDTO> GetAllToDoItems()
        {
            var toDoItemsDTOList = new List<ToDoItemDTO>();
            var toDoItemsList = _toDoRepo.FindAll().ToList();
            toDoItemsList.ForEach(item => toDoItemsDTOList.Add(item.Map_ToDo_Entity_DTO()));
            return toDoItemsDTOList;
        }
    }
}
