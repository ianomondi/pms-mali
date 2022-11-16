using AutoMapper;
using PMS.Domain.Models;
using PMS.Domain.Models.Queries;
using PMS.Domain.Resources.Request;
using PMS.Domain.Resources.Request.Companies;
using PMS.Domain.Resources.Request.Roles;

namespace PMS.API.Profiles
{
    public class RequestToModelProfile : Profile
    {
        public RequestToModelProfile()
        {
            CreateMap<CompanyListRequest, CompaniesQuery>();
            CreateMap<CreateRequest, Company>();
            CreateMap<EditRequest, Company>();

            CreateMap<RolesRequest.CreateRole, Role>();
            CreateMap<RolesRequest.UpdateRole, Role>();
            CreateMap<RolesRequest.RoleListRequest, RolesQuery>();

            CreateMap<UsersRequest.CreateUser, User>();
            CreateMap<UsersRequest.UpdateUser, User>();
            CreateMap<UsersRequest.UserListRequest, UsersQuery>();

            CreateMap<VehicleRequest.CreateVehicle, Vehicle>();
            CreateMap<VehicleRequest.UpdateVehicle, Vehicle>();
            CreateMap<VehicleRequest.VehicleListRequest, Vehicle>();
            CreateMap<VehicleRequest.VehicleListRequest, VehiclesQuery>();

            CreateMap<TripRequest.CreateTrip, Trip>();
            CreateMap<TripRequest.UpdateTrip, Trip>();
            CreateMap<TripRequest.TripListRequest, TripsQuery>();
        }
    }
}
