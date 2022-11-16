using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Domain.Resources.Request.Companies
{
    public class CompanyListRequest : QueryRequest
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Status { get; set; }
    }

    public class CreateRequest
    {
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        [StringLength(255)]
        public string Email { get; set; }
        [Required]
        [StringLength(15)]
        public string PhoneNumber { get; set; }
        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string? Status { get; set; } = "Approved";
    }

    public class EditRequest
    {
        [Required]
        public Guid Id { get; set; }
        [StringLength(255)]
        public string Name { get; set; }
        public string Description { get; set; }
        [StringLength(255)]
        public string Email { get; set; }
        [StringLength(15)]
        public string PhoneNumber { get; set; }
        public bool IsActive { get; set; }
        public string Address { get; set; }
        public string? Status { get; set; } = "Approved";
    }

    public class DeleteRequest
    {
        [Required]
        public Guid Id { get; set; }
       
    }
}
