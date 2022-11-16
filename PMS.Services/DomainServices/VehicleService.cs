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

namespace PMS.Services.DomainServices
{
    public class VehicleService : IVehicleService
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IUnitOfWork _unitOfWork;

        public VehicleService(IVehicleRepository vehicleRepository, IUnitOfWork unitOfWork)
        {
            _vehicleRepository = vehicleRepository;
            _unitOfWork = unitOfWork;

        }
        public async Task<VehicleResponse> AddAsync(Vehicle vehicle)
        {
            try
            {
                //if exists with same number plate
                var vehicleQuery = new VehiclesQuery(null, vehicle.NumberPlate,0, DateTime.Now, 0.0, 1, 1);
                var vehicles = await _vehicleRepository.ListAsync(vehicleQuery);

                if (vehicles != null && vehicles.TotalItems > 0)
                {
                    return new VehicleResponse("This Number Plate is Already Registered");
                }
                await _vehicleRepository.AddAsync(vehicle);
                await _unitOfWork.CompleteAsync();

                VehicleResponse vehicleResponse = new VehicleResponse(vehicle, DefaultResponseMessages.SavesSuccess);

                //TODO: send email/sms

                return vehicleResponse;
            }
            catch (Exception e)
            {
                return new VehicleResponse(DefaultResponseMessages.SaveException(e));
            }
        }

        public Task<Vehicle> GetByIdAsync(Guid id)
        {
            return _vehicleRepository.GetByIdAsync(id);
        }

        public async Task<QueryResult<Vehicle>> ListAsync(VehiclesQuery query)
        {
            return await _vehicleRepository.ListAsync(query);
        }

        public async Task<VehicleResponse> RemoveAsync(Guid id)
        {
            var existingVehicle = await _vehicleRepository.GetByIdAsync(id);

            if (existingVehicle == null)
                return new VehicleResponse("Vehicle not found.");

            try
            {
                _vehicleRepository.Remove(existingVehicle);
                await _unitOfWork.CompleteAsync();

                return new VehicleResponse(existingVehicle);
            }
            catch (Exception ex)
            {
                return new VehicleResponse(DefaultResponseMessages.DeleteException(ex));
            }
        }

        public async Task<VehicleResponse> UpdateAsync(Guid id, Vehicle vehicle)
        {
            var existingVehicle = await _vehicleRepository.GetByIdAsync(id);
            if (existingVehicle == null)
                return new VehicleResponse("Vehicle not found.");

            try
            {


                if (!string.IsNullOrWhiteSpace(vehicle.NumberPlate) )
                {
                    VehiclesQuery vehiclesQuery = null;
                    QueryResult<Vehicle> vehicles = null;
                    //if exists with same number plate
                    if (!string.IsNullOrWhiteSpace(vehicle.NumberPlate))
                    {
                        vehiclesQuery = new VehiclesQuery(null, vehicle.NumberPlate,0, DateTime.Now, 0.0, 1, 1);
                        vehicles = await _vehicleRepository.ListAsync(vehiclesQuery);

                        if (vehicles != null && vehicles.TotalItems > 0)
                        {
                            return new VehicleResponse("This Number Plate is already regisetered");
                        }
                    }
                    

                }

                existingVehicle.Owner = vehicle.Owner ?? existingVehicle.Owner;
                existingVehicle.NumberPlate = vehicle.NumberPlate ?? existingVehicle.NumberPlate;
                existingVehicle.NumberOfPasengers = vehicle.NumberOfPasengers ?? existingVehicle.NumberOfPasengers;
                existingVehicle.PurchaseDate = vehicle.PurchaseDate;
                existingVehicle.LifeSpan = vehicle.LifeSpan ;

                _vehicleRepository.Update(existingVehicle);
                await _unitOfWork.CompleteAsync();

                return new VehicleResponse(existingVehicle, DefaultResponseMessages.UpdateSuccess);

            }
            catch (Exception e)
            {
                return new VehicleResponse(DefaultResponseMessages.UpdateException(e));
            }
        }
    }
}
