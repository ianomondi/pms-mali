using Microsoft.EntityFrameworkCore;
using PMS.DAL.IRepos;
using PMS.Domain.Models;
using PMS.Domain.Models.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.DAL.Repos
{
    public class TripRepository: BaseRepository, ITripRepository
    {

        public TripRepository(PMSDbContext context) : base(context)
        {

        }
        public async Task AddAsync(Trip trip)
        {

            await _context.Trips.AddAsync(trip);
        }

        public async Task<Trip> GetByIdAsync(Guid id)
        {
            return await _context.Trips.FindAsync(id);
        }

        public Task<User> GetLoginUserAsync(string username, string password)
        {
            throw new NotImplementedException();
        }

        public async Task<QueryResult<Trip>> ListAsync(TripsQuery query)
        {
            IQueryable<Trip> queryable = _context.Trips.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(query.Vehicle))
            {
                queryable = queryable.Where(x => x.Vehicle.HasValue && x.VehicleTrip!=null);
            }
            if (!string.IsNullOrWhiteSpace(query.TotalPassangers))
            {
                queryable = queryable.Where(x => x.TotalPassangers.Contains(query.TotalPassangers));
            }
            if ((query.TotalAmount != null))
            {
                queryable = queryable.Where(x => x.TotalAmount == (query.TotalAmount));
            }

            int totalItems = await queryable.CountAsync();

            List<Trip> trips = await queryable.Skip((query.Page - 1) * query.ItemsPerPage)
                                                    .Take(query.ItemsPerPage)
                                                    .ToListAsync();

            return new QueryResult<Trip>
            {
                Items = trips,
                TotalItems = totalItems,
            };

        }

        public void Remove(Trip trip)
        {
            _context.Trips.Remove(trip);
        }

        public void Update(Trip trip)
        {
            _context.Trips.Update(trip);
        }
    }
}
