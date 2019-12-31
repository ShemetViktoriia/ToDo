using System.Collections.Generic;
using ToDo_DAL;

namespace ToDo_Repository
{
    public interface IToDoItemRepository
    {
        void Add(ToDoItem entity);
        ToDoItem FindById(int id);
        IEnumerable<ToDoItem> FindAll();
        void Edit(ToDoItem entity);
        void Delete(int id);
    }
}
