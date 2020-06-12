using DataAccess.EF;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class ProductDAO
    {
        EShopDbContext db = null;
        public ProductDAO()
        {
            db = new EShopDbContext();
        }

        public async Task<List<Product>> SelectCondition(string cond, int number)
        {
            if (cond.Equals("top"))
                return await db.Products.OrderBy(x => x.ViewCount).Take(number).ToListAsync();
            else if (cond.Equals("newest"))
                return await db.Products.OrderByDescending(x => x.CreatedDate).Take(number).ToListAsync();
            return null;

        }

        public async Task<int> CreateProduct(Product model)
        {
            try
            {
                db.Products.Add(model);
                await db.SaveChangesAsync();
                return model.ProductID;
            }
            catch
            {
                return 0;
            }
        }

        public async Task<bool> DeleteProduct(int ID)
        {
            try
            {
                var prod = await db.Products.FindAsync(ID);
                if (prod == null)
                {
                    return false;
                }
                db.Products.Remove(prod);
                await db.SaveChangesAsync();
                return true;
            } catch
            {
                return false;
            }
        }

        public async Task<Product> LoadByID(int ID)
        {
            return await db.Products.FindAsync(ID);
        }

        public async Task<List<Product>> LoadProduct()
        {
            return await db.Products.AsNoTracking().ToListAsync();
        }

        public async Task<List<Product>> LoadName(string prefix)
        {
            var context = new EShopDbContext();
            context.Configuration.ProxyCreationEnabled = false;
            return await context.Products.AsNoTracking().Where(x => x.ProductName.Contains(prefix)).ToListAsync();
        }

        public async Task<Product> LoadNameByID(int id)
        {
            return await db.Products.AsNoTracking().Where(x => x.ProductID == id).FirstOrDefaultAsync();
        }

        public async Task<List<Product>> LoadProduct(int? cateid, string searchString, string sort, int pagesize, int pageindex)
        {
            // get list
            var list = (from s in db.Products select s).AsNoTracking();

            if (!String.IsNullOrEmpty(cateid.ToString()))
            {
                list = list.Where(x => x.CategoryID == cateid);
            }

            //filter
            if (!String.IsNullOrEmpty(searchString))
            {
                list = list.Where(x => x.ProductName.Contains(searchString));
            }
            switch (sort)
            {
                case "name_desc":
                    list = list.OrderByDescending(s => s.ProductName);
                    break;
                case "name_asc":
                    list = list.OrderBy(s => s.ProductName);
                    break;
                case "price_desc":
                    list = list.OrderByDescending(s => s.ProductPrice);
                    break;
                case "price_asc":
                    list = list.OrderBy(s => s.ProductPrice);
                    break;
                default:
                    list = list.OrderBy(s => s.ProductID);
                    break;
            }
            //return
            return await list.Skip(pageindex * pagesize).Take(pagesize).ToListAsync();
        }

        public async Task<Product> LoadByURL(string url)
        {
            return await db.Products.AsNoTracking().Where(x => x.ProductURL == url).FirstOrDefaultAsync();
        }
    }
}
