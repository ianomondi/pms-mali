using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PMS.Domain.Resources.Request
{
    public class TripRequest
    {
        public class CreateTrip
        {
            [Required]
            public Guid? Vehicle { get; set; }
            public string TotalPassangers { get; set; }
            public int TotalAmount { get; set; }

        }
        public class TripListRequest:QueryRequest
        {
            public Guid? Vehicle { get; set; }
            public string? TotalPassangers { get; set; }
            public int? TotalAmount { get; set; }
        }
        public class UpdateTrip
        {
            [Required]
            public Guid Id { get; set; }
            [Required]
            public Guid Vehicle { get; set; }
            [StringLength(3)]
            public string? TotalPassangers { get; set; }
            public int? TotalAmount { get; set; }
        }

        public class DeleteTrip
        {
            [Required]
            public Guid Id { get; set; }
        }
    }
}
