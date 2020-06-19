using DataAccess.EF;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class CategoryDAO
    {
        EShopDbContext db = null;
        public CategoryDAO()
        {
            db = new EShopDbContext();
            db.Configuration.ProxyCreationEnabled = false;
        }

        public async Task<List<Category>> LoadData()
        {
            return await db.Categories.AsNoTracking().ToListAsync();
        }

        public async Task<Category> LoadByID(int id)
        {
            return await db.Categories.FindAsync(id);
        }

        public async Task<Category> LoadByURL(string url)
        {
            return await db.Categories
                .AsNoTracking()
                .Where(x => x.CategoryURL.Equals(url))
                .SingleOrDefaultAsync();
        }

        public async Task<int> CreateCategory(Category cate)
        {
            try
            {
                db.Categories.Add(cate);
                await db.SaveChangesAsync();
                return cate.CategoryID;
            }
            catch
            {
                return 0;
            }
        }

        public async Task<bool> DeleteCategory(int ID)
        {
            try
            {
                var prod = await db.Categories.Where(x => x.CategoryID == ID).SingleOrDefaultAsync();
                db.Categories.Remove(prod);
                await db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<int> EditCategory(Category cate, int ID)
        {
            try
            {
                var item = db.Categories.Find(ID);
                if (item == null)
                {
                    return 0;
                }
                item.CategoryName = cate.CategoryName;
                item.CategoryURL= cate.CategoryURL;
                await db.SaveChangesAsync();
                return item.CategoryID;
            }
            catch
            {
                return 0;
            }
        }
    }
}
