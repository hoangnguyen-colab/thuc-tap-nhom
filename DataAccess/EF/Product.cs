namespace DataAccess.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Product")]
    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            OderDetails = new HashSet<OrderDetail>();
        }

        public int ProductID { get; set; }

        [Required]
        [StringLength(250)]
        public string ProductName { get; set; }

        public decimal ProductPrice { get; set; }

        public string ProductDescription { get; set; }

        public string ProductImage { get; set; }

        public int? ProductStock { get; set; }

        public bool? ProductStatus { get; set; }

        [StringLength(250)]
        public string ProductURL { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public int? CategoryID { get; set; }

        public virtual Category Category { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetail> OderDetails { get; set; }
    }
}
