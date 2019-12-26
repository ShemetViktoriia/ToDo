using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ToDo_DAL;

namespace ToDo_Repository
{
    public class ToDoItemRepository: IToDoItemRepository
    {
        protected readonly DbContext _context;
        protected readonly IDbSet<ToDoItem> _dbset;

        public ToDoItemRepository(DbContext context)
        {
            _context = context;
            _dbset = context.Set<ToDoItem>();
        }

        public ToDoItem Add(ToDoItem entity)
        {
            return _dbset.Add(entity);
        }

        public void Edit(ToDoItem entity)
        {
            _dbset.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public IEnumerable<ToDoItem> FindAll()
        {
            return _dbset.AsEnumerable();
        }

        public ToDoItem FindById(int id)
        {
            return _dbset.Find(id);
        }
    }
}
