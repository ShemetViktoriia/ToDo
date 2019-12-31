using System.Collections.Generic;
using ToDo_DAL;

namespace ToDo_Repository
{
    public interface IToDoItemRepository
    {
        void AddOrEdit(ToDoItem entity);
        ToDoItem FindById(int id);
        IEnumerable<ToDoItem> FindAll();
        void Delete(int id);
    }
}
