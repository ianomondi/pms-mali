using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Domain.Resources.Request
{
    public class ExpenseRequest
    {
        public class CreateExpense
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public int TotalAmount { get; set; }

        }
        public class ExpenseListRequest : QueryRequest
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public int TotalAmount { get; set; }
        }
        public class UpdateTrip
        {
            [Required]
            [StringLength(255)]
            public string Name { get; set; }
            [Required]
            [StringLength(255)]
            public string Description { get; set; }
            [Required]
            [StringLength(10)]
            public int TotalAmount { get; set; }
        }

        public class DeleteTrip
        {
            [Required]
            public Guid Id { get; set; }
        }
    }
}
