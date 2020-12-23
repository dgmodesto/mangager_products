using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Models
{
    [Table("Products")]
    public class Product : BaseEntity
    {
        [Column("DESCRIPTION")]
        [Required]
        public string Description { get; set; }

        [Column("PRICE")]
        [Required]
        public double Price { get; set; }

    }
}
