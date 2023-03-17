using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace Service
{
    internal sealed class EmployeeService : IEmployeeService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly ILoggerManager _loggerManager;
        private readonly IMapper _mapper;

        public EmployeeService(IRepositoryManager repositoryManager, ILoggerManager loggerManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _loggerManager = loggerManager;
            _mapper = mapper;
        }

        public IEnumerable<EmployeeDto> GetEmployees(Guid companyId, bool trackChanges)
        {
            var company = _repositoryManager.CompanyRepository.GetCompany(companyId, trackChanges);
            if (company is null)
                throw new CompanyNotFoundException(companyId);
            var employeesFromDb = _repositoryManager.EmployeeRepository.GetEmployees(companyId, trackChanges);
            return _mapper.Map<IEnumerable<EmployeeDto>>(employeesFromDb);

        }

        public EmployeeDto GetEmployee(Guid companyId, Guid id, bool trackChanges)
        {
            var company = _repositoryManager.CompanyRepository.GetCompany(companyId, trackChanges);
            if (company is null) 
                throw new CompanyNotFoundException(companyId);
            var employeeDb = _repositoryManager.EmployeeRepository.GetEmployee(companyId, id, trackChanges);
            if (employeeDb is null) 
                throw new EmployeeNotFoundException(id);
            var employee = _mapper.Map<EmployeeDto>(employeeDb); 
            return employee;
        }
    }
}
