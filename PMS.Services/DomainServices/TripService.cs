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
    public class TripService : ITripService
    {
        private readonly ITripRepository _tripRepository;
        private readonly IUnitOfWork _unitOfWork;

        public TripService(ITripRepository tripRepository, IUnitOfWork unitOfWork)
        {
            _tripRepository = tripRepository;
            _unitOfWork = unitOfWork;

        }
        public async Task<TripResponse> AddAsync(Trip trip)
        {
            try
            {

            await _tripRepository.AddAsync(trip);
            await _unitOfWork.CompleteAsync();

            TripResponse tripResponse = new TripResponse(trip, DefaultResponseMessages.SavesSuccess);

            //TODO: send email/sms

            return tripResponse;
            }
            catch (Exception e)
            {

                return new TripResponse(DefaultResponseMessages.SaveException(e));
            }
        }

        public Task<Trip> GetByIdAsync(Guid id)
        {
            return _tripRepository.GetByIdAsync(id);
        }

        public async Task<QueryResult<Trip>> ListAsync(TripsQuery query)
        {
            return await _tripRepository.ListAsync(query);
        }

        public async Task<TripResponse> RemoveAsync(Guid id)
        {
            var existingTrip = await _tripRepository.GetByIdAsync(id);

            if (existingTrip == null)
                return new TripResponse("Trip not found.");

            try
            {
                _tripRepository.Remove(existingTrip);
                await _unitOfWork.CompleteAsync();

                return new TripResponse(existingTrip);
            }
            catch (Exception ex)
            {
                return new TripResponse(DefaultResponseMessages.DeleteException(ex));
            }
        }

        public async Task<BaseResponse<Trip>> UpdateAsync(Guid id, Trip trip)
        {
           var existingTrip = await _tripRepository.GetByIdAsync(id);
            if (existingTrip==null)
            {
                return new BaseResponse<Trip>("Trip not found.");
            }

            existingTrip.Vehicle = trip.Vehicle;
            existingTrip.TotalAmount = trip.TotalAmount;
            existingTrip.TotalPassangers = trip.TotalPassangers;
            try
            {
                _tripRepository.Update(existingTrip);
                await _unitOfWork.CompleteAsync();

                return new BaseResponse<Trip>(null, DefaultResponseMessages.UpdateSuccess);
            }
            catch (Exception ex)
            {
                return new BaseResponse<Trip>(DefaultResponseMessages.UpdateException(ex));
            }
        }
    }
}
