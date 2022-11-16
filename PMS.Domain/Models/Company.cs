using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Domain.Models
{
    public class Company : BaseEntity
    {
        [Required]
        [StringLength(255)]
        [Display(Name = "Company Name")]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        [StringLength(255)]
        [Display(Name = "Email Address")]
        public string Email { get; set; }
        [Required]
        [StringLength(15)]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }
        [Required]
        [Display(Name = "Company Address")]
        public string Address { get; set; }
        [Required]
        public string Status { get; set; }
    }
}
