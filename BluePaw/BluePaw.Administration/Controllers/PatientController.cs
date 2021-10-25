using BluePaw.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BluePaw.Administration.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PatientController : ControllerBase
    {
        private readonly ILogger<PatientController> _logger;

        public PatientController(ILogger<PatientController> logger)
        {
            _logger = logger;
        }
        
        [HttpPost]
        public IActionResult Create(CreatePatientRequest request)
        {
            _logger.LogInformation($"Creating new patient {request.Name} for owner {request.OwnerName}");
            
            //TODO

            return Ok(1); //TODO return actual ID
        }
    }
}