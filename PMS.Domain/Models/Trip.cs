using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Domain.Models
{
    public class Trip:BaseEntity
    {
        [Display(Name = "Vehicle")]
        [StringLength(255)]
        [ForeignKey("VehicleTrip")]
        public Guid? Vehicle { get; set; }
        [Required]
        [StringLength(3)]
        [Display(Name = "Number of Passangers")]
        public string? TotalPassangers { get; set;}
        [Required]
        [Display(Name = "Total Amount")]
        public int TotalAmount { get; set;}
        public Vehicle? VehicleTrip { get; set; }
    }
}
