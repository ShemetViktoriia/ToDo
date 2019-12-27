using System;

namespace ToDo_BLL.DTO
{
    public class ToDoItemDTO
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public System.DateTime AddedAt { get; set; }
        public int AddedByUserId { get; set; }
        public string AddedByUserName { get; set; }
        public bool WasDone { get; set; }
        public Nullable<System.DateTime> WasDoneAt { get; set; }
        public Nullable<System.DateTime> DueDate { get; set; }
    }
}
