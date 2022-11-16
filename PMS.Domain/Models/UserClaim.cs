using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Domain.Models
{
    public class UserClaim : BaseEntity
    {
        [Required]
        [Display(Name = "User")]
        //[ForeignKey]
        public Guid UserId { get; set; }
        [Display(Name = "Claim")]
        public string ClaimType { get; set; }
        [Display(Name = "Claim Value")]
        public string ClaimValue { get; set; }

        public User User { get; set; }
    }
}
