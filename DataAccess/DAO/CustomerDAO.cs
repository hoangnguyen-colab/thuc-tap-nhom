using DataAccess.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace DataAccess.DAO
{
    public class CustomerDAO
    {
        EShopDbContext db = null;
        public CustomerDAO()
        {
            db = new EShopDbContext();
            db.Configuration.ProxyCreationEnabled = false;
        }

        public async Task<List<Customer>> LoadCustomer()
        {
            return await db.Customers.AsNoTracking().ToListAsync();
        }

        public async Task<bool> DeleteCustomer(int ID)
        {
            try
            {
                var cus = await db.Customers.Where(x => x.CustomerID== ID).SingleOrDefaultAsync();
                db.Customers.Remove(cus);
                await db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> CheckUser(string username)
        {
            return await db.Customers.AsNoTracking().AnyAsync(x => x.CustomerUsername == username);
        }

        public async Task<Customer> LoadByID(int id)
        {
            return await db.Customers.AsNoTracking().Where(x => x.CustomerID == id).SingleOrDefaultAsync();
        }

        public async Task<Customer> LoadByUsername(string username)
        {
            return await db.Customers.AsNoTracking().Where(x => x.CustomerUsername.Equals(username)).SingleOrDefaultAsync();
        }

        public async Task<bool> LoginAsync(string username, string password)
        {
            string encrypt = MD5Hash(password);
            var result = await db.Customers.AsNoTracking().CountAsync(x => x.CustomerUsername.Equals(username)  && x.CustomerPassword.Equals(encrypt));
            if (result > 0)
                return true;
            else
                return false;
        }

        public bool Login(string username, string password)
        {
            string encrypt = MD5Hash(password);
            var result = db.Customers.AsNoTracking().Count(x => x.CustomerUsername.Equals(username) && x.CustomerPassword.Equals(encrypt));
            if (result > 0)
                return true;
            else
                return false;
        }

        public async Task<int> Register(Customer cus)
        {
            cus.CustomerPassword = MD5Hash(cus.CustomerPassword);
            db.Customers.Add(cus);
            await db.SaveChangesAsync();
            return cus.CustomerID;
        }

        public static string MD5Hash(string input)
        {
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(input));

            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();
        }
    }
}
