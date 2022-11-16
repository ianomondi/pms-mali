using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Domain.Models
{
    public class Expense:BaseEntity
    {
        [Required]
        [StringLength(255)]
        [Display(Name = "Expense Name")]
        public string Name { get; set; }
        [Required]
        [StringLength(255)]
        [Display(Name = "Expense Description")]
        public string Description { get; set; }
        [Required]
        [StringLength(10)]
        [Display(Name = "Total Amount")]
        public int TotalAmount { get; set; }
    }
}
