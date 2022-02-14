namespace PSIsystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Purchase")]
    public partial class Purchase
    {
        [Required]
        [StringLength(10)]
        public string ID { get; set; }

        [StringLength(3)]
        public string ProductID { get; set; }

        public int? Count { get; set; }

        public DateTime? Date { get; set; }

        public decimal? Total { get; set; }

        [StringLength(50)]
        public string Creator { get; set; }

        public DateTime? CreateTime { get; set; }

        public bool? Isdelete { get; set; }

        [Key]
        public int SerialNumber { get; set; }

        public virtual Product Product { get; set; }
    }
}
