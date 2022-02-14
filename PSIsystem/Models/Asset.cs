namespace PSIsystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Asset
    {
        public int ID { get; set; }

        [Column("Asset")]
        public decimal? Asset1 { get; set; }
    }
}
