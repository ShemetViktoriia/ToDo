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

        public void AddOrEditItem(ToDoItemDTO item)
        {
            var toDoEntity = item.Map_ToDo_DTO_Entity();
            _toDoRepo.AddOrEdit(toDoEntity);
        }

        public void DeleteItem(int id)
        {
            _toDoRepo.Delete(id);
        }

        public ToDoItemDTO FindById(int id)
        {
            return _toDoRepo.FindById(id).Map_ToDo_Entity_DTO();
        }

        public IEnumerable<ToDoItemDTO> GetAllItems()
        {
            var toDoItemsDTOList = new List<ToDoItemDTO>();
            var toDoItemsList = _toDoRepo.FindAll().ToList();
            toDoItemsList.ForEach(item => toDoItemsDTOList.Add(item.Map_ToDo_Entity_DTO()));
            return toDoItemsDTOList;
        }
    }
}
