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
            throw new Exception("Exception");
            //try
            //{
            var companies = _serviceManager.CompanyService.GetAllCompanies(trackChanges: false);
                return Ok(companies);
            //}
            //catch
            //{
            //    return StatusCode(500, "Internal server error");
            //}
        }
    }
}
