using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Domain.Resources.Request.Roles
{
    public class RolesRequest
    {
        public class CreateRole
        {
            [Required]
            public string Name { get; set; }
        }
        public class UpdateRole
        {
            [Required]
            public Guid Id { get; set; }    

            [Required]
            public string Name { get; set; }
        }
        public class DeleteRole
        {
            [Required]
            public Guid Id { get; set; }    
        }
        public class RoleListRequest : QueryRequest
        {
            public string? Name { get; set; }
        }
    }
}
