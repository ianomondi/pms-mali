using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Domain.Resources.Request
{
    public class UsersRequest
    {
        public class CreateUser
        {
            [Required]
            [StringLength(255)]
            public string FirstName { get; set; }
            [Required]
            [StringLength(255)]
            public string LastName { get; set; }
            [Required]
            [StringLength(255)]
            public string Email { get; set; }
            [Required]
            [StringLength(15)]
            public string PhoneNumber { get; set; }

            [Required]
            [StringLength(255)]
            public string UserName { get; set; }

            [Required]
            [StringLength(255)]
            public string Password { get; set; }
        }

        public class UpdateUser
        {
            [Required]
            public Guid Id { get; set; }

            [StringLength(255)]
            public string? FirstName { get; set; }
            [StringLength(255)]
            public string? LastName { get; set; }
            [StringLength(255)]
            public string? Email { get; set; }
            [StringLength(15)]
            public string? PhoneNumber { get; set; }
        }
        public class DeleteUser
        {
            [Required]
            public Guid Id { get; set; }
        }

        public class UserListRequest : QueryRequest
        {
            public string? UserName { get; set; }
            public string? Name { get; set; }
            public string? Email { get; set; }
            public string? PhoneNumber { get; set; }
        }
    }
}
