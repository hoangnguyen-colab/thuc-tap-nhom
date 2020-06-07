using DataAccess.EF;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class AdminDAO
    {
        EShopDbContext db = null;
        public AdminDAO()
        {
            db = new EShopDbContext();
            db.Configuration.ProxyCreationEnabled = false;
        }
        public async Task<bool> LoginAsync(string username, string password)
        {
            string encrypt = DataAccess.DAO.CustomerDAO.MD5Hash(password);
            var result = await db.Admins.AsNoTracking().CountAsync(x => x.AdminUsername.Equals(username) && x.AdminPassword.Equals(encrypt));
            if (result > 0)
                return true;
            else
                return false;
        }

        public async Task<bool> CheckUser(string username)
        {
            return await db.Customers.AsNoTracking().AnyAsync(x => x.CustomerUsername == username);
        }

    }
}
