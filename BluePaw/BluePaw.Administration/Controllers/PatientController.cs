using System;
using System.Collections.Generic;
using System.Linq;
using BluePaw.Administration.Data;
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
        private readonly BluePawDbContext _dbContext;

        public PatientController(ILogger<PatientController> logger, BluePawDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        [HttpGet]
        [Route("{id:int}")]
        public PatientData Get(int id)
        {
            var patient = _dbContext.Patients.Find(id);
            
            return patient == null ? null : new PatientData
            {
                Id = patient.Id,
                Name = patient.Name,
                OwnerName = patient.OwnerName,
                OwnerPhone = patient.OwnerPhone,
                Species = patient.Species
            };
        }

        [HttpGet]
        [Route("all")]
        public IEnumerable<PatientData> GetAll()
        {
            return _dbContext.Patients.Select(p =>
                new PatientData
                {
                    Id = p.Id,
                    Name = p.Name,
                    OwnerName = p.OwnerName,
                    OwnerPhone = p.OwnerPhone,
                    Species = p.Species
                });
        }

        [HttpPost]
        public IActionResult Create(CreatePatientRequest request)
        {
            _logger.LogInformation($"Creating new patient {request.Name} for owner {request.OwnerName}");
            var patient = new Patient
            {
                Name = request.Name,
                OwnerName = request.OwnerName,
                OwnerPhone = request.OwnerPhone,
                Species = request.Species
            };
            
            try
            {
                _dbContext.Patients.Add(patient);
                _dbContext.SaveChanges();
                return Ok(patient.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unable to create patient!");
                return StatusCode(500);
            }
        }
    }
}