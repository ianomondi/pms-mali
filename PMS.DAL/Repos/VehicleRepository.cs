using Microsoft.EntityFrameworkCore;
using PMS.DAL.IRepos;
using PMS.Domain.IServices;
using PMS.Domain.Models;
using PMS.Domain.Models.Queries;
using PMS.Domain.Resources.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.DAL.Repos
{
    public class VehicleRepository : BaseRepository, IVehicleRepository
    {
        public VehicleRepository(PMSDbContext context) : base(context)
        {

        }
        public async Task AddAsync(Vehicle vehicle)
        {
            var existingRole = await _context.Vehicles.FirstOrDefaultAsync(m => m.NumberPlate == vehicle.NumberPlate);

            if (existingRole != null) throw new Exception("Vehicle already exists");

            await _context.Vehicles.AddAsync(vehicle);
        }

        public async Task<Vehicle> GetByIdAsync(Guid id)
        {
            Vehicle vehicle = await _context.Vehicles.FindAsync(id);
            return vehicle;
        }

        public Task<User> GetLoginUserAsync(string username, string password)
        {
            throw new NotImplementedException();
        }

        public async Task<QueryResult<Vehicle>> ListAsync(VehiclesQuery query)
        {
            IQueryable<Vehicle> queryable = _context.Vehicles.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(query.Owner))
            {
                queryable = queryable.Where(x => x.Owner.HasValue && x.UserOwner != null 
                 && (x.UserOwner.FirstName.Contains(query.Owner) || x.UserOwner.LastName.Contains(query.Owner) || x.UserOwner.Email.Contains(query.Owner)));
            }
            if (!string.IsNullOrWhiteSpace(query.NumberPlate))
            {
                queryable = queryable.Where(x => x.NumberPlate != null && x.NumberPlate.Contains(query.NumberPlate));
            }
            if (query.NumberOfPasengers.HasValue)
            {
                queryable = queryable.Where(x => x.NumberOfPasengers == (query.NumberOfPasengers.Value));
            }
            if (query.PurchaseDate.HasValue && query.PurchaseDate.Value!=DateTime.MinValue)
            {
                queryable = queryable.Where(x => x.PurchaseDate == query.PurchaseDate.Value);
            }
            if ((query.LifeSpan != null))
            {
                queryable = queryable.Where(x => x.LifeSpan == query.LifeSpan);
            }

            int totalItems = await queryable.CountAsync();

            List<Vehicle> vehicles = await queryable.Skip((query.Page - 1) * query.ItemsPerPage)
                                                    .Take(query.ItemsPerPage).Include(x=>x.UserOwner)
                                                    .ToListAsync();

            return new QueryResult<Vehicle>
            {
                Items = vehicles,
                TotalItems = totalItems,
            };

        }

        public void Remove(Vehicle vehicle)
        {
            _context.Vehicles.Remove(vehicle);
        }

        public void Update(Vehicle vehicle)
        {
            var existingVehicle = _context.Vehicles.FirstOrDefault(m => m.NumberPlate != vehicle.NumberPlate);
            if (existingVehicle != null) throw new Exception("Vehicle already exists");
            _context.Vehicles.Update(vehicle);
        }
    }
}
