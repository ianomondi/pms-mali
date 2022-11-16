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
    public class CompanyService : ICompanyServices
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CompanyService(ICompanyRepository companyRepository, IUnitOfWork unitOfWork)
        {
            _companyRepository = companyRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<CompanyReponse> AddAsync(Company company)
        {
            try
            {

                await _companyRepository.AddAsync(company);
                await _unitOfWork.CompleteAsync();

                //return response
                CompanyReponse companyReponse = new CompanyReponse(company, DefaultResponseMessages.SavesSuccess);

                return companyReponse;
            }
            catch (Exception e)
            {
                return new CompanyReponse(DefaultResponseMessages.SaveException(e));
            }

        }

        public Task<Company> GetByIdAsync(Guid id)
        {
            return _companyRepository.GetByIdAsync(id);
        }

        public async Task<QueryResult<Company>> ListAsync(CompaniesQuery query)
        {
            QueryResult<Company> companies = await _companyRepository.ListAsync(query);
            
            return companies;
        }
        public async Task<CompanyReponse> RemoveAsync(Guid id)
        {
            var existingCompany = await _companyRepository.GetByIdAsync(id);

            if (existingCompany == null)
                return new CompanyReponse("Company not found.");

            try
            {
                _companyRepository.Remove(existingCompany);
                await _unitOfWork.CompleteAsync();

                return new CompanyReponse(existingCompany);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new CompanyReponse(DefaultResponseMessages.DeleteException(ex));
            }
        }

        public async Task<CompanyReponse> UpdateAsync(Guid id, Company company)
        {
            var existingCompany = await _companyRepository.GetByIdAsync(id);

            if (existingCompany == null)
                return new CompanyReponse("Product not found.");

            //TODO: use Automapper
            existingCompany.Name = company.Name;
            existingCompany.Address = company.Address;
            existingCompany.PhoneNumber = company.PhoneNumber;

            try
            {
                _companyRepository.Update(existingCompany);


                await _unitOfWork.CompleteAsync();

                return new CompanyReponse(existingCompany, DefaultResponseMessages.UpdateSuccess);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new CompanyReponse(DefaultResponseMessages.UpdateException(ex));
            }
        }
    }
}
