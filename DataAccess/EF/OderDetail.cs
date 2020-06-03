namespace DataAccess.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("OderDetail")]
    public partial class OderDetail
    {
        [Key]
        public int DetailID { get; set; }

        public int Quantity { get; set; }

        public int? OrderID { get; set; }

        public int? ProductID { get; set; }

        public virtual Order Order { get; set; }

        public virtual Product Product { get; set; }
    }
}
