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
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        //private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<UserResponse> AddAsync(User user)
        {
            try
            {
                string password = user.Password;

                if (string.IsNullOrWhiteSpace(password))
                {
                    password = "12345";
                }

                //user.Two


                //if exists with same username
                var userQuery = new UsersQuery(user.UserName, null, user.Email, null, 1, 1);
                var users = await _userRepository.ListAsync(userQuery);
                if (users != null && users.TotalItems > 0)
                {
                    return new UserResponse("This username is already taken");
                }

                //if exists with same email
                userQuery = new UsersQuery(null, null, user.Email, null, 1, 1);
                users = await _userRepository.ListAsync(userQuery);

                if (users != null && users.TotalItems > 0)
                {
                    return new UserResponse("This email is already taken");
                }

                //Phone number
                userQuery = new UsersQuery(null, null, null, user.PhoneNumber, 1, 1);
                users = await _userRepository.ListAsync(userQuery);
                if (users != null && users.TotalItems > 0)
                {
                    return new UserResponse("This phone number is already taken");
                }

                await _userRepository.AddAsync(user);
                await _unitOfWork.CompleteAsync();

                UserResponse userResponse = new UserResponse(user, DefaultResponseMessages.SavesSuccess);

                //TODO: send email/sms

                return userResponse;
            }
            catch (Exception e)
            {
                return new UserResponse(DefaultResponseMessages.SaveException(e));
            }
        }

        public Task<User> GetByIdAsync(Guid id)
        {
            return _userRepository.GetByIdAsync(id);
        }

        public Task<User> GetLoginUserAsync(string username, string password)
        {
            return _userRepository.GetLoginUserAsync(username, password);
        }


        public async Task<UserResponse> RemoveAsync(Guid id)
        {
            var existingUser = await _userRepository.GetByIdAsync(id);

            if (existingUser == null)
                return new UserResponse("User not found.");

            try
            {
                _userRepository.Remove(existingUser);
                await _unitOfWork.CompleteAsync();

                return new UserResponse(existingUser);
            }
            catch (Exception ex)
            {
                return new UserResponse(DefaultResponseMessages.DeleteException(ex));
            }
        }

        public async Task<UserResponse> UpdateAsync(Guid id, User user)
        {
            var existingUser = await _userRepository.GetByIdAsync(id);
            if (existingUser == null)
                return new UserResponse("User not found.");

            try
            {


                if (!string.IsNullOrWhiteSpace(user.Email) || !string.IsNullOrWhiteSpace(user.PhoneNumber))
                {
                    UsersQuery userQuery = null;
                    QueryResult<User> users = null;
                    //if exists with same email
                    if (!string.IsNullOrWhiteSpace(user.Email))
                    {
                        userQuery = new UsersQuery(null, null, user.Email, null, 1, 1);
                        users = await _userRepository.ListAsync(userQuery);

                        if (users != null && users.TotalItems > 0 && !users.Items.Any(x => x.Id == user.Id))
                        {
                            return new UserResponse("This email is already in use");
                        }
                    }
                    if (!string.IsNullOrWhiteSpace(user.PhoneNumber))
                    {
                        //Phone number
                        userQuery = new UsersQuery(null, null, null, user.PhoneNumber, 1, 1);
                        users = await _userRepository.ListAsync(userQuery);
                        if (users != null && users.TotalItems > 0 && !users.Items.Any(x => x.Id == user.Id))
                        {
                            return new UserResponse("This phone number is already in use");
                        }
                    }

                }


                existingUser.FirstName = user.FirstName ?? existingUser.FirstName;
                existingUser.LastName = user.LastName ?? existingUser.LastName;
                existingUser.Email = user.Email ?? existingUser.Email;
                existingUser.PhoneNumber = user.PhoneNumber ?? existingUser.PhoneNumber;

                _userRepository.Update(existingUser);
                await _unitOfWork.CompleteAsync();

                return new UserResponse(existingUser, DefaultResponseMessages.UpdateSuccess);

            }
            catch (Exception e)
            {
                return new UserResponse(DefaultResponseMessages.UpdateException(e));
            }
        }

        public async Task<QueryResult<User>> ListAsync(UsersQuery query)
        {
            return await _userRepository.ListAsync(query);
        }
    }
}
