using DataAccess.EF;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class OrderStatusDAO
    {
        EShopDbContext db = null;
        public OrderStatusDAO()
        {
            db = new EShopDbContext();
            db.Configuration.ProxyCreationEnabled = false;
        }

        public async Task<List<OrderStatu>> LoadStatus()
        {
            return await db.OrderStatus.AsNoTracking().ToListAsync();
        }
    }
}
