using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Domain.Resources.Request
{
    public class VehicleRequest
    {
        public class CreateVehicle
        {
            [Required]
            public Guid? Owner { get; set; }
            [Required]
            [StringLength(7)]
            public string? NumberPlate { get; set; }
            //[StringLength(3)]
            public int NumberOfPasengers { get; set; } = 0;
            [Required]
            public DateTime PurchaseDate { get; set; }
            public double LifeSpan { get; set; }
        }

        public class VehicleListRequest:QueryRequest
        {
            public Guid? Owner { get; set; }
            public string? NumberPlate { get; set; }
            public int? NumberOfPasengers { get; set; }
            public DateTime PurchaseDate { get; set; }
            public double LifeSpan { get; set; }
        }
        public class UpdateVehicle
        {
            [Required]
            public Guid? Id { get; set; }
            [Required]
            public Guid? Owner { get; set; }
            [StringLength(7)]
            public string? NumberPlate { get; set; }
            public int? NumberOfPasengers { get; set; }
            public DateTime PurchaseDate { get; set; }
            public double LifeSpan { get; set; }
        }
        public class DeleteVehicle
        {
            [Required]
            public Guid Id { get; set; }
        }
    }
}
