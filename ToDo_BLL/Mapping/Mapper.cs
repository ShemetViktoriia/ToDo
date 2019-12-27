using ToDo_BLL.DTO;
using ToDo_DAL;

namespace ToDo_BLL.Mapping
{
    public static class Mapper
    {
        public static ToDoItemDTO Map_ToDo_Entity_DTO(this ToDoItem item)
        {
            var toDoDTO = new ToDoItemDTO
            {
                Id = item.Id,
                Description = item.Description,
                AddedByUserId = item.AddedBy,
                AddedByUserName = item.AspNetUser.UserName,
                AddedAt = item.AddedAt,
                WasDone = item.WasDone,
                DueDate = item.DueDate,
                WasDoneAt = item.WasDoneAt
            };
            return toDoDTO;
        }

        public static ToDoItem Map_ToDo_DTO_Entity(this ToDoItemDTO item)
        {
            var toDoDTO = new ToDoItem
            {
                Id = item.Id,
                Description = item.Description,
                AddedBy = item.AddedByUserId,
                AddedAt = item.AddedAt,
                WasDone = item.WasDone,
                DueDate = item.DueDate,
                WasDoneAt = item.WasDoneAt
            };
            return toDoDTO;
        }
    }
}
