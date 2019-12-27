using ToDo_BLL.DTO;
using ToDo_WebUI.Models;

namespace ToDo_WebUI.Mapping
{
    public static class Mapper
    {
        public static ToDoItemViewModel Map_ToDo_DTO_ViewModel(this ToDoItemDTO item)
        {
            var toDoViewModel = new ToDoItemViewModel
            {
                Id = item.Id,
                Description = item.Description,
                AddedByUserId = item.AddedByUserId,
                AddedByUserName = item.AddedByUserName,
                AddedAt = item.AddedAt,
                WasDone = item.WasDone,
                DueDate = item.DueDate,
                WasDoneAt = item.WasDoneAt
            };
            return toDoViewModel;
        }

        public static ToDoItemDTO Map_ToDo_ViewModel_DTO(this ToDoItemViewModel item)
        {
            var toDoDTO = new ToDoItemDTO
            {
                Id = item.Id,
                Description = item.Description,
                AddedByUserId = item.AddedByUserId,
                AddedByUserName = item.AddedByUserName,
                AddedAt = item.AddedAt,
                WasDone = item.WasDone,
                DueDate = item.DueDate,
                WasDoneAt = item.WasDoneAt
            };
            return toDoDTO;
        }
    }
}