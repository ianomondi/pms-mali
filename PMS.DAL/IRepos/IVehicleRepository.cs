using PMS.Domain.Models;
using PMS.Domain.Models.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.DAL.IRepos
{
    public interface IVehicleRepository
    {
        Task<Vehicle> GetByIdAsync(Guid id);
        Task AddAsync(Vehicle vehicle);
        Task<QueryResult<Vehicle>> ListAsync(VehiclesQuery query);
        void Update(Vehicle vehicle);
        void Remove(Vehicle vehicle);
    }
}
