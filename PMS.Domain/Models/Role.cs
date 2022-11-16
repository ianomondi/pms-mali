using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Domain.Models
{
    public class Role : BaseEntity
    {
        [Required]
        [Display(Name = "Role Name")]
        [StringLength(255)]
        public string Name { get; set; }

        //public ICollection<UserRole> UserRoles { get; set; }
        public ICollection<User> Users { get; set; }
        public ICollection<RolePermission> RolePermissions { get; set; }

        /*[NotMapped]
        public ICollection<User> Users
        {
            get
            {
                return this.UserRoles.Select(m => m.User).ToList();
            }
        }*/
    }
}
