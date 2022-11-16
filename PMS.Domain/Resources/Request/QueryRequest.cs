using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Domain.Resources.Request
{
    public class QueryRequest
    {
        public int Page { get; set; }
        public int ItemsPerPage { get; set; }
    }
}
