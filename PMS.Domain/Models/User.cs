using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Domain.Models
{
    public class User : BaseEntity
    {
        [Required]
        [StringLength(255)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [StringLength(255)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required]
        [StringLength(255)]
        [Display(Name = "Email Address")]
        public string Email { get; set; }
        [Required]
        [StringLength(15)]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        
        [Required]
        [StringLength(255)]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [ScaffoldColumn(false)]
        [NotMapped]
        public string DisplayName { get { return $"{this.FirstName} {this.LastName}"; } }

        public ICollection<Role> Roles { get; set; }

        public ICollection<UserClaim> UserClaims { get; set; }
        public ICollection<Vehicle> Vehicles { get; set; }
        /*public virtual ICollection<UserRole> UserRoles { get; set; }

        [NotMapped]
        public ICollection<Role> Roles
        {
            get
            {
                return this.UserRoles.Select(m => m.Role).ToList();
            }
        }*/
    }
}
