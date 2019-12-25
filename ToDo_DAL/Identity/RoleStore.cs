using Microsoft.AspNet.Identity;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace ToDo_DAL.Identity
{
    public class RoleStore : IQueryableRoleStore<AspNetRole, int>
    {
        private readonly ToDoContext db;

        public RoleStore(ToDoContext db)
        {
            this.db = db;
        }

        //// IQueryableRoleStore<UserRole, TKey>

        public IQueryable<AspNetRole> Roles
        {
            get { return this.db.AspNetRoles; }
        }

        //// IRoleStore<UserRole, TKey>

        public virtual Task CreateAsync(AspNetRole role)
        {
            if (role == null)
            {
                throw new ArgumentNullException("role");
            }

            this.db.AspNetRoles.Add(role);
            return this.db.SaveChangesAsync();
        }

        public Task DeleteAsync(AspNetRole role)
        {
            if (role == null)
            {
                throw new ArgumentNullException("role");
            }

            this.db.AspNetRoles.Remove(role);
            return this.db.SaveChangesAsync();
        }

        public Task<AspNetRole> FindByIdAsync(int roleId)
        {
            return this.db.AspNetRoles.FindAsync(new[] { roleId });
        }

        public Task<AspNetRole> FindByNameAsync(string roleName)
        {
            return this.db.AspNetRoles.FirstOrDefaultAsync(r => r.Name == roleName);
        }

        public Task UpdateAsync(AspNetRole role)
        {
            if (role == null)
            {
                throw new ArgumentNullException("role");
            }

            this.db.Entry(role).State = EntityState.Modified;
            return this.db.SaveChangesAsync();
        }

        //// IDisposable

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing && this.db != null)
            {
                this.db.Dispose();
            }
        }
    }
}
