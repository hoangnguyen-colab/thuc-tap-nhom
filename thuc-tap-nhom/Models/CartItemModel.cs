using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccess.DAO;
using DataAccess.EF;

namespace cong_nghe_web.Models
{
    public class CartItemModel
    {
        public Product product { get; set; }
        public int quantity { get; set; }

        public CartItemModel(Product product)
        { }

        public CartItemModel(Product product, int quantity)
        {
            this.product = product;
            this.quantity = quantity;
        }
    }
    public class CartSession
    {
        public int ProductID { get; set; }
        public int Quantity { get; set; }

        public CartSession()
        { }

        public CartSession(int ProductID, int Quantity)
        {
            this.ProductID = ProductID;
            this.Quantity = Quantity;
        }
    }
}