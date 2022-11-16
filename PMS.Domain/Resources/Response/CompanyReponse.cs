using PMS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Domain.Resources.Response
{
    public class CompanyReponse : BaseResponse<Company>
    {
        public CompanyReponse(Company resource) : base(resource)
        {
        }

        public CompanyReponse(string message) : base(message)
        {
        }

        public CompanyReponse(Company resource, string message) : base(resource, message)
        {
        }
    }
    public class CompanyListReponse : BaseResponse<List<Company>>
    {
        public CompanyListReponse(List<Company> resource) : base(resource)
        {
        }

        public CompanyListReponse(string message) : base(message)
        {
        }

        public CompanyListReponse(List<Company> resource, string message) : base(resource, message)
        {
        }
    }
    public class UserResponse : BaseResponse<User>
    {
        public UserResponse(User resource) : base(resource)
        {
        }

        public UserResponse(string message) : base(message)
        {
        }

        public UserResponse(User resource, string message) : base(resource, message)
        {
        }
    }
    
    public class UserListResponse : BaseResponse<List<User>>
    {
        public UserListResponse(List<User> resource) : base(resource)
        {
        }

        public UserListResponse(string message) : base(message)
        {
        }

        public UserListResponse(List<User> resource, string message) : base(resource, message)
        {
        }
    }
    public class VehicleResponse : BaseResponse<Vehicle>
    {
        public VehicleResponse(Vehicle resource) : base(resource)
        {
        }

        public VehicleResponse(string message) : base(message)
        {
        }

        public VehicleResponse(Vehicle resource, string message) : base(resource, message)
        {
        }
    }
    public class TripResponse : BaseResponse<Trip>
    {
        public TripResponse(Trip resource) : base(resource)
        {
        }

        public TripResponse(string message) : base(message)
        {
        }

        public TripResponse(Trip resource, string message) : base(resource, message)
        {
        }
    }

    public class ExpenseResponse : BaseResponse<Trip>
    {
        public ExpenseResponse(Trip resource) : base(resource)
        {
        }

        public ExpenseResponse(string message) : base(message)
        {
        }

        public ExpenseResponse(Trip resource, string message) : base(resource, message)
        {
        }
    }
}
