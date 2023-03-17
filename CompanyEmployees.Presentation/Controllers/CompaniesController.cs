using Microsoft.AspNetCore.Mvc;
using Service.Contracts;

namespace CompanyEmployees.Presentation.Controllers
{
    [ApiController]
    [Route("/api/Companies")]
    public class CompaniesController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public CompaniesController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }
        [HttpGet]
        public IActionResult GetCompanies()
        {
            var companies = _serviceManager.CompanyService.GetAllCompanies(trackChanges: false);
            return Ok(companies);
        }
        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult GetCompany(Guid id)
        {
            var company = _serviceManager.CompanyService.GetCompany(id, trackChanges: false);
            return Ok(company);
        }
    }
}
