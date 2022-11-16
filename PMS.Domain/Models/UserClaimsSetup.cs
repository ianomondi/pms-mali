using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Domain.Models
{
    public class UserClaimsSetup : BaseEntity
    {
        [Display(Name = "Claim/Permission Type")]
        public string ClaimType { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }
    }
    
    public class RolePermissionsSetup : BaseEntity
    {
        [Display(Name = "Permission")]
        public string Permission { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}
