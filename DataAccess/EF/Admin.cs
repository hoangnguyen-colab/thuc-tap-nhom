namespace DataAccess.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Admin")]
    public partial class Admin
    {
        public int AdminID { get; set; }

        [Required]
        [StringLength(250)]
        public string AdminUsername { get; set; }

        [Required]
        [StringLength(250)]
        public string AdminPassword { get; set; }

        [StringLength(250)]
        public string AdminName { get; set; }

        public DateTime? CreatedDate { get; set; }
    }
}
