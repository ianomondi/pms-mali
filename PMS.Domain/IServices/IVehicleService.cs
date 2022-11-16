using PMS.Domain.Models;
using PMS.Domain.Models.Queries;
using PMS.Domain.Resources.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Domain.IServices
{

    public interface IVehicleService
    {
        Task<VehicleResponse> AddAsync(Vehicle vehicle);
        Task<QueryResult<Vehicle>> ListAsync(VehiclesQuery query);
        Task<VehicleResponse> RemoveAsync(Guid id);
        Task<VehicleResponse> UpdateAsync(Guid id, Vehicle vehicle);
        Task<Vehicle> GetByIdAsync(Guid id);
    }
}
