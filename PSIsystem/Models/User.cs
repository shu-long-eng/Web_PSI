namespace PSIsystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("User")]
    public partial class User
    {
        public int ID { get; set; }

        [StringLength(10)]
        public string Account { get; set; }

        [StringLength(10)]
        public string PWD { get; set; }

        [StringLength(10)]
        public string Name { get; set; }

        public int? Level { get; set; }
    }
}
