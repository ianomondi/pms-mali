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
    public interface ITripService
    {
        Task<TripResponse> AddAsync(Trip trip);
        Task<QueryResult<Trip>> ListAsync(TripsQuery query);
        Task<TripResponse> RemoveAsync(Guid id);
        Task<BaseResponse<Trip>> UpdateAsync(Guid id, Trip trip);
        Task<Trip> GetByIdAsync(Guid id);
    }
}
