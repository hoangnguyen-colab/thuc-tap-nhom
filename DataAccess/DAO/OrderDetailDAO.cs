using DataAccess.EF;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class OrderDetailDAO
    {
        EShopDbContext db = null;
        public OrderDetailDAO()
        {
            db = new EShopDbContext();
            db.Configuration.ProxyCreationEnabled = false;
        }

        public async Task<int> AddOrderDetail(int OrderID, int ProductID, int Quanity)
        {
            try {
                var order = new OrderDetail()
                {
                    OrderID = OrderID,
                    ProductID = ProductID,
                    Quantity = Quanity
                };
                db.OrderDetails.Add(order);
                await db.SaveChangesAsync();
                return order.DetailID;
            }
            catch
            {
                return 0;
            }
        }

        public async Task<List<OrderDetail>> LoadOrderDetail(int OrderID)
        {
            return await db.OrderDetails.AsNoTracking().Where(x => x.OrderID == OrderID).ToListAsync();
        }
    }
}
