using DataAccess.EF;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class OrderDAO
    {
        EShopDbContext db = null;
        public OrderDAO()
        {
            db = new EShopDbContext();
        }

        public async Task<int> AddOrder(int CustomerID, decimal total)
        {
            var order = new Order
            {
                Total = total,
                OrderDate = DateTime.Now,
                OrderStatusID = 1,
                CustomerID = CustomerID
            };
            db.Orders.Add(order);
            await db.SaveChangesAsync();
            return order.OrderID;
        }

        public async Task<Order> LoadByID(int OrderID)
        {
            return await db.Orders.AsNoTracking().Where(x => x.OrderID == OrderID).FirstOrDefaultAsync();
        }

        public async Task<List<Order>> LoadOrder()
        {
            return await db.Orders.AsNoTracking().ToListAsync();
        }

        public async Task<List<Order>> LoadOrder(int CustomerID)
        {
            return await db.Orders.AsNoTracking().Where(x => x.CustomerID == CustomerID).ToListAsync();
        }

        public async Task<List<T>> LoadOrder<T>(int CustomerID)
        {
            var Param = new SqlParameter("@CustomerID", CustomerID);

            return await db.Database
                 .SqlQuery<T>("SelectOrder @CustomerID", Param)
                 .ToListAsync();
        }

        public async Task<List<OrderDetail>> LoadProductOrder(int OrderID)
        {
            return await db.OrderDetails.AsNoTracking().Where(x => x.OrderID == OrderID).ToListAsync();
        }

        public async Task<List<T>> LoadProductOrder<T>(int OrderID)
        {
            var Param = new SqlParameter("@OrderID", OrderID);

            return await db.Database
                 .SqlQuery<T>("SelectOrderProduct @OrderID", Param)
                 .ToListAsync();
        }

        public async Task<int> ChangeOrder(int OrderID, int StatusID)
        {
            try
            {
                var order = await db.Orders.FindAsync(OrderID);
                if (order == null)
                {
                    return 0;
                }
                var status = await db.OrderStatus.FindAsync(StatusID);
                if (status == null)
                {
                    return 0;
                }
                if (order.OrderStatusID == StatusID)
                {
                    return 0;
                }
                order.OrderStatusID = StatusID;
                await db.SaveChangesAsync();
                return order.OrderID;
            }
            catch
            {
                return 0;
            }
        }
    }
}
