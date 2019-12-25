using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ToDo_DAL.Identity
{
    public partial class UserStore :
       IQueryableUserStore<AspNetUser, int>, IUserPasswordStore<AspNetUser, int>, IUserLoginStore<AspNetUser, int>,
       IUserClaimStore<AspNetUser, int>, IUserRoleStore<AspNetUser, int>, IUserSecurityStampStore<AspNetUser, int>,
       IUserEmailStore<AspNetUser, int>, IUserPhoneNumberStore<AspNetUser, int>, IUserTwoFactorStore<AspNetUser, int>,
       IUserLockoutStore<AspNetUser, int>
    {
        private readonly ToDoContext db;

        public UserStore(ToDoContext db)
        {
            if (db == null)
            {
                throw new ArgumentNullException("db");
            }

            this.db = db;
        }

        //// IQueryableUserStore<User, int>

        public IQueryable<AspNetUser> Users
        {
            get { return this.db.AspNetUsers; }
        }

        //// IUserStore<User, Key>

        public Task CreateAsync(AspNetUser user)
        {
            this.db.AspNetUsers.Add(user);
            return this.db.SaveChangesAsync();
        }

        public Task DeleteAsync(AspNetUser user)
        {
            this.db.AspNetUsers.Remove(user);
            return this.db.SaveChangesAsync();
        }

        public Task<AspNetUser> FindByIdAsync(int userId)
        {
            return this.db.AspNetUsers
                .Include(u => u.AspNetUserLogins).Include(u => u.AspNetRoles).Include(u => u.AspNetUserClaims)
                .FirstOrDefaultAsync(u => u.Id.Equals(userId));
        }

        public Task<AspNetUser> FindByNameAsync(string userName)
        {
            return this.db.AspNetUsers
                .Include(u => u.AspNetUserLogins).Include(u => u.AspNetRoles).Include(u => u.AspNetUserClaims)
                .FirstOrDefaultAsync(u => u.UserName == userName);
        }

        public Task UpdateAsync(AspNetUser user)
        {
            this.db.Entry<AspNetUser>(user).State = EntityState.Modified;
            return this.db.SaveChangesAsync();
        }

        //// IUserPasswordStore<User, Key>

        public Task<string> GetPasswordHashAsync(AspNetUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            return Task.FromResult(user.PasswordHash);
        }

        public Task<bool> HasPasswordAsync(AspNetUser user)
        {
            return Task.FromResult(user.PasswordHash != null);
        }

        public Task SetPasswordHashAsync(AspNetUser user, string passwordHash)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            user.PasswordHash = passwordHash;
            return Task.FromResult(0);
        }

        //// IUserLoginStore<User, Key>

        public Task AddLoginAsync(AspNetUser user, UserLoginInfo login)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            if (login == null)
            {
                throw new ArgumentNullException("login");
            }

            var userLogin = Activator.CreateInstance<AspNetUserLogin>();
            userLogin.UserId = user.Id;
            userLogin.LoginProvider = login.LoginProvider;
            userLogin.ProviderKey = login.ProviderKey;
            user.AspNetUserLogins.Add(userLogin);
            return Task.FromResult(0);
        }

        public async Task<AspNetUser> FindAsync(UserLoginInfo login)
        {
            if (login == null)
            {
                throw new ArgumentNullException("login");
            }

            var provider = login.LoginProvider;
            var key = login.ProviderKey;

            var userLogin = await this.db.AspNetUserLogins.FirstOrDefaultAsync(l => l.LoginProvider == provider && l.ProviderKey == key);

            if (userLogin == null)
            {
                return default(AspNetUser);
            }

            return await this.db.AspNetUsers
                .Include(u => u.AspNetUserLogins).Include(u => u.AspNetRoles).Include(u => u.AspNetUserClaims)
                .FirstOrDefaultAsync(u => u.Id.Equals(userLogin.UserId));
        }

        public Task<IList<UserLoginInfo>> GetLoginsAsync(AspNetUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            return Task.FromResult<IList<UserLoginInfo>>(user.AspNetUserLogins.Select(l => new UserLoginInfo(l.LoginProvider, l.ProviderKey)).ToList());
        }

        public Task RemoveLoginAsync(AspNetUser user, UserLoginInfo login)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            if (login == null)
            {
                throw new ArgumentNullException("login");
            }

            var provider = login.LoginProvider;
            var key = login.ProviderKey;

            var item = user.AspNetUserLogins.SingleOrDefault(l => l.LoginProvider == provider && l.ProviderKey == key);

            if (item != null)
            {
                user.AspNetUserLogins.Remove(item);
            }

            return Task.FromResult(0);
        }

        //// IUserClaimStore<User, int>

        public Task AddClaimAsync(AspNetUser user, Claim claim)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            if (claim == null)
            {
                throw new ArgumentNullException("claim");
            }

            var item = Activator.CreateInstance<AspNetUserClaim>();
            item.UserId = user.Id;
            item.ClaimType = claim.Type;
            item.ClaimValue = claim.Value;
            user.AspNetUserClaims.Add(item);
            return Task.FromResult(0);
        }

        public Task<IList<Claim>> GetClaimsAsync(AspNetUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            return Task.FromResult<IList<Claim>>(user.AspNetUserClaims.Select(c => new Claim(c.ClaimType, c.ClaimValue)).ToList());
        }

        public Task RemoveClaimAsync(AspNetUser user, Claim claim)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            if (claim == null)
            {
                throw new ArgumentNullException("claim");
            }

            foreach (var item in user.AspNetUserClaims.Where(uc => uc.ClaimValue == claim.Value && uc.ClaimType == claim.Type).ToList())
            {
                user.AspNetUserClaims.Remove(item);
            }

            foreach (var item in this.db.AspNetUserClaims.Where(uc => uc.UserId.Equals(user.Id) && uc.ClaimValue == claim.Value && uc.ClaimType == claim.Type).ToList())
            {
                this.db.AspNetUserClaims.Remove(item);
            }

            return Task.FromResult(0);
        }

        //// IUserRoleStore<User, int>

        public Task AddToRoleAsync(AspNetUser user, string roleName)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            if (string.IsNullOrWhiteSpace(roleName))
            {
                throw new ArgumentException(Resources.ValueCannotBeNullOrEmpty, "roleName");
            }

            var userRole = this.db.AspNetRoles.SingleOrDefault(r => r.Name == roleName);

            if (userRole == null)
            {
                throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.RoleNotFound, new object[] { roleName }));
            }

            user.AspNetRoles.Add(userRole);
            return Task.FromResult(0);
        }

        public Task<IList<string>> GetRolesAsync(AspNetUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            return Task.FromResult<IList<string>>(user.AspNetRoles.Join(this.db.AspNetRoles, ur => ur.Id, r => r.Id, (ur, r) => r.Name).ToList());
        }

        public Task<bool> IsInRoleAsync(AspNetUser user, string roleName)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            if (string.IsNullOrWhiteSpace(roleName))
            {
                throw new ArgumentException(Resources.ValueCannotBeNullOrEmpty, "roleName");
            }

            return
                Task.FromResult<bool>(
                    this.db.AspNetRoles.Any(r => r.Name == roleName && r.AspNetUsers.Any(u => u.Id.Equals(user.Id))));
        }

        public Task RemoveFromRoleAsync(AspNetUser user, string roleName)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            if (string.IsNullOrWhiteSpace(roleName))
            {
                throw new ArgumentException(Resources.ValueCannotBeNullOrEmpty, "roleName");
            }

            var userRole = user.AspNetRoles.SingleOrDefault(r => r.Name == roleName);

            if (userRole != null)
            {
                user.AspNetRoles.Remove(userRole);
            }

            return Task.FromResult(0);
        }

        //// IUserSecurityStampStore<User, int>

        public Task<string> GetSecurityStampAsync(AspNetUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            return Task.FromResult(user.SecurityStamp);
        }

        public Task SetSecurityStampAsync(AspNetUser user, string stamp)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            user.SecurityStamp = stamp;
            return Task.FromResult(0);
        }

        //// IUserEmailStore<User, int>

        public Task<AspNetUser> FindByEmailAsync(string email)
        {
            return this.db.AspNetUsers
                .Include(u => u.AspNetUserLogins).Include(u => u.AspNetRoles).Include(u => u.AspNetUserClaims)
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public Task<string> GetEmailAsync(AspNetUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            return Task.FromResult(user.Email);
        }

        public Task<bool> GetEmailConfirmedAsync(AspNetUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            return Task.FromResult(user.EmailConfirmed);
        }

        public Task SetEmailAsync(AspNetUser user, string email)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            user.Email = email;
            return Task.FromResult(0);
        }

        public Task SetEmailConfirmedAsync(AspNetUser user, bool confirmed)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            user.EmailConfirmed = confirmed;
            return Task.FromResult(0);
        }

        //// IUserPhoneNumberStore<User, int>

        public Task<string> GetPhoneNumberAsync(AspNetUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            return Task.FromResult(user.PhoneNumber);
        }

        public Task<bool> GetPhoneNumberConfirmedAsync(AspNetUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            return Task.FromResult(user.PhoneNumberConfirmed);
        }

        public Task SetPhoneNumberAsync(AspNetUser user, string phoneNumber)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            user.PhoneNumber = phoneNumber;
            return Task.FromResult(0);
        }

        public Task SetPhoneNumberConfirmedAsync(AspNetUser user, bool confirmed)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            user.PhoneNumberConfirmed = confirmed;
            return Task.FromResult(0);
        }

        //// IUserTwoFactorStore<User, int>

        public Task<bool> GetTwoFactorEnabledAsync(AspNetUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            return Task.FromResult(user.TwoFactorEnabled);
        }

        public Task SetTwoFactorEnabledAsync(AspNetUser user, bool enabled)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            user.TwoFactorEnabled = enabled;
            return Task.FromResult(0);
        }

        //// IUserLockoutStore<User, int>

        public Task<int> GetAccessFailedCountAsync(AspNetUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            return Task.FromResult(user.AccessFailedCount);
        }

        public Task<bool> GetLockoutEnabledAsync(AspNetUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            return Task.FromResult(user.LockoutEnabled);
        }

        public Task<DateTimeOffset> GetLockoutEndDateAsync(AspNetUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            return Task.FromResult(
                user.LockoutEndDateUtc.HasValue ?
                    new DateTimeOffset(DateTime.SpecifyKind(user.LockoutEndDateUtc.Value, DateTimeKind.Utc)) :
                    new DateTimeOffset());
        }

        public Task<int> IncrementAccessFailedCountAsync(AspNetUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            user.AccessFailedCount++;
            return Task.FromResult(user.AccessFailedCount);
        }

        public Task ResetAccessFailedCountAsync(AspNetUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            user.AccessFailedCount = 0;
            return Task.FromResult(0);
        }

        public Task SetLockoutEnabledAsync(AspNetUser user, bool enabled)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            user.LockoutEnabled = enabled;
            return Task.FromResult(0);
        }

        public Task SetLockoutEndDateAsync(AspNetUser user, DateTimeOffset lockoutEnd)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            user.LockoutEndDateUtc = lockoutEnd == DateTimeOffset.MinValue ? null : new DateTime?(lockoutEnd.UtcDateTime);
            return Task.FromResult(0);
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
