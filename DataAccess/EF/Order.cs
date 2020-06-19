namespace DataAccess.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Order")]
    public partial class Order
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int OrderID { get; set; }

        public decimal? Total { get; set; }

        public DateTime? OrderDate { get; set; }

        [Required]
        [StringLength(255)]
        public string CustomerName { get; set; }

        [Required]
        [StringLength(20)]
        public string CustomerPhone { get; set; }

        [Required]
        [StringLength(255)]
        public string CustomerAddress { get; set; }

        public int? OrderStatusID { get; set; }

        public DateTime? StatusChangeDate { get; set; }

        public int? CustomerID { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual OrderStatu OrderStatu { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
