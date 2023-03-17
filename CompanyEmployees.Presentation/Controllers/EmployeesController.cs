using Microsoft.AspNetCore.Mvc;
using Service.Contracts;

namespace CompanyEmployees.Presentation.Controllers
{
    [Route("api/companies/{companyId}/employees")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        public EmployeesController(IServiceManager service) => _serviceManager = service;

        [HttpGet]
        public IActionResult GetEmployeesForCompany(Guid companyId)
        {
            var employees = _serviceManager.EmployeeService.GetEmployees(companyId, trackChanges: false);
            return Ok(employees);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult GetEmployeeForCompany(Guid companyId, Guid id)
        {
            var employee = _serviceManager.EmployeeService.GetEmployee(companyId, id, trackChanges: false);
            return Ok(employee);
        }
    }
}
