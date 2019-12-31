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

        public void Add(ToDoItem entity)
        {
            _dbset.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            ToDoItem ent = _dbset.Find(id);
            _dbset.Remove(ent);
            _context.SaveChanges();
        }

        public void Edit(ToDoItem entity)
        {
            _dbset.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
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
