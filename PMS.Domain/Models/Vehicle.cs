using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Domain.Models
{
    public class Vehicle:BaseEntity
    {
        [Required]
        [StringLength(255)]
        [Display(Name ="Owner")]
        [ForeignKey("UserOwner")]
        public Guid? Owner { get; set; }
        [Required]
        [StringLength(7)]
        [Display(Name = "Number Plate")]
        public string? NumberPlate { get; set; }

        [Required]
        public int? NumberOfPasengers { get; set; }
        [Required]

        [Display(Name = "Vehicle Purchase Date")]
        public DateTime? PurchaseDate { get; set; }

        [Display(Name = "Vehicle Life Span")]
        public double LifeSpan { get; set; }   
        
        public User? UserOwner { get; set; }
        public ICollection<Trip>? Trips { get; set; }
    }
}
