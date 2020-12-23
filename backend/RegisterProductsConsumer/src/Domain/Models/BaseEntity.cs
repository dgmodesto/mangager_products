using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class BaseEntity
    {
        [Column("ID")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int Id { get; set; }
        [Column("AT_CREATED")]
        public DateTime AtCreated { get; set; }
        [Column("AT_UPDATED")]
        public DateTime AtUpdated { get; set; }
    }
}
