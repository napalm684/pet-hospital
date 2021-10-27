using System.Collections.Generic;
using System.Linq;
using BluePaw.Administration.Data;
using BluePaw.Shared;
using Microsoft.AspNetCore.Mvc;

namespace BluePaw.Administration.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RequestController : ControllerBase
    {
        private readonly BluePawDbContext _dbContext;

        public RequestController(BluePawDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        [HttpGet]
        [Route("all")]
        public IEnumerable<PatientRequest> Requests()
        {
            //Note: In a production application we should validate input.
            
            return _dbContext.AdministrationRequests.Select(r =>
                new PatientRequest
                {
                    PatientId = r.PatientId,
                    Request = r.Request
                });
        }
    }
}