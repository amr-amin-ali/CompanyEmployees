using Contracts;

namespace Repository
{
    public sealed class RepositoryManager : IRepositoryManager
    {
        private readonly Lazy<ICompanyRepository> _companyRepository;
        private readonly Lazy<IEmployeeRepository> _employeeRepository;
        private readonly RepositoryContext _repositoryContext;

        public ICompanyRepository CompanyRepository => _companyRepository.Value;
        public IEmployeeRepository EmployeeRepository => _employeeRepository.Value;

        public RepositoryManager(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
            _companyRepository = new Lazy<ICompanyRepository>(() => new CompanyRepository(_repositoryContext));
            _employeeRepository = new Lazy<IEmployeeRepository>(() => new EmployeeRepository(_repositoryContext));
        }

        public void SaveChanges() => _repositoryContext.SaveChanges();

    }
}
