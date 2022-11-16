using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Domain.Models
{
    public class RolePermission : BaseEntity
    {
        [Required]
        [Display(Name = "Role")]
        //[ForeignKey]
        public Guid RoleId { get; set; }
        [Display(Name = "Permission")]
        public string Permission { get; set; }
        
        [Display(Name = "Description")]
        public string Description { get; set; }
        public Role Role { get; set; }
    }
}
