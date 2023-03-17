using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace Service
{
    internal sealed class CompanyService : ICompanyService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly ILoggerManager _loggerManager;
        private readonly IMapper _mapper;

        public CompanyService(IRepositoryManager repositoryManager, ILoggerManager loggerManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _loggerManager = loggerManager;
            _mapper = mapper;
        }

        public IEnumerable<CompanyDto> GetAllCompanies(bool trackChanges)
        {
                var companies = _repositoryManager.CompanyRepository.GetAllCompanies(trackChanges);
                var companiesDto = _mapper.Map<IEnumerable<CompanyDto>>(companies);
                return companiesDto;
        }

        public CompanyDto GetCompany(Guid companyId, bool trackChanges)
        {
            var company=_repositoryManager.CompanyRepository.GetCompany(companyId, trackChanges);
            if (company == null)
            {
                throw new CompanyNotFoundException(companyId);
            }
            return _mapper.Map<CompanyDto>(company);
        }
    }
}
