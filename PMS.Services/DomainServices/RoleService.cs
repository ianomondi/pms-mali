using PMS.DAL.IRepos;
using PMS.Domain.IServices;
using PMS.Domain.Models;
using PMS.Domain.Models.Queries;
using PMS.Domain.Resources.Request.Roles;
using PMS.Domain.Resources.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Services.DomainServices
{
    public class RoleService : IRoleService
    {
        private readonly IRolesRepo _roleRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RoleService(IRolesRepo companyRepository, IUnitOfWork unitOfWork)
        {
            _roleRepository = companyRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<Role>> AddAsync(Role role)
        {
            try
            {

                await _roleRepository.AddAsync(role);
                await _unitOfWork.CompleteAsync();

                //return response
                BaseResponse<Role> reponse = new BaseResponse<Role>(role, DefaultResponseMessages.SavesSuccess);

                return reponse;
            }
            catch (Exception e)
            {
                return new BaseResponse<Role>(DefaultResponseMessages.SaveException(e));
            }

        }

        public Task<Role> GetByIdAsync(Guid id)
        {
            return _roleRepository.GetByIdAsync(id);
        }

        public async Task<QueryResult<Role>> ListAsync(RolesQuery query)
        {
            QueryResult<Role> roles = await _roleRepository.ListAsync(query);

            return roles;
        }


        public async Task<BaseResponse<Role>> RemoveAsync(Guid id)
        {
            var existingRole = await _roleRepository.GetByIdAsync(id);

            if (existingRole == null)
                return new BaseResponse<Role>("Role not found.");

            try
            {
                _roleRepository.Remove(existingRole);
                await _unitOfWork.CompleteAsync();

                return new BaseResponse<Role>(null, DefaultResponseMessages.DeleteSuccess);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new BaseResponse<Role>(DefaultResponseMessages.DeleteException(ex));
            }
        }

        public async Task<BaseResponse<Role>> UpdateAsync(Guid id, Role role)
        {
            var existingRole = await _roleRepository.GetByIdAsync(id);

            if (existingRole == null)
                return new BaseResponse<Role>("Role not found.");

            //TODO: use Automapper
            existingRole.Name = role.Name;

            try
            {
                _roleRepository.Update(existingRole);


                await _unitOfWork.CompleteAsync();

                return new BaseResponse<Role>(null, DefaultResponseMessages.UpdateSuccess);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new BaseResponse<Role>(DefaultResponseMessages.UpdateException(ex));
            }
        }
    }
}
