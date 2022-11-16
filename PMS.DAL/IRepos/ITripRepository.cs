using PMS.Domain.Models;
using PMS.Domain.Models.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.DAL.IRepos
{
    public interface ITripRepository
    {
        Task<Trip> GetByIdAsync(Guid id);
        Task AddAsync(Trip trip);
        Task<QueryResult<Trip>> ListAsync(TripsQuery query);
        void Update(Trip trip);
        void Remove(Trip trip);
    }
}
