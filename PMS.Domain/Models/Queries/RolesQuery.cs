using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Domain.Models.Queries
{
    public class RolesQuery : Query
    {
        public string? Name { get; set; }

        public RolesQuery(string name, int page, int itemsPerPage) : base(page, itemsPerPage)
        {
            Name = name;
        }
    }
}
