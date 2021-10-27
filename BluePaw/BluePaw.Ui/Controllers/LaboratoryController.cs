using System;
using System.Threading.Tasks;
using BluePaw.Shared;
using BluePaw.Ui.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BluePaw.Ui.Controllers
{
    public class LaboratoryController : Controller
    {
        private readonly ILogger<LaboratoryController> _logger;
        private readonly IAdministrationService _administrationService;
        private readonly IRequestRouter _router;

        public LaboratoryController(ILogger<LaboratoryController> logger, 
            IAdministrationService administrationService, IRequestRouter router)
        {
            _logger = logger;
            _administrationService = administrationService;
            _router = router;
        }

        public IActionResult Index()
        {
            return View();
        }
        
        public async Task<ActionResult> AdministrationRequest()
        {
            ViewData["patients"] = await _administrationService.RetrievePatients();
            return View("AdminRequest");
        }

        [HttpPost]
        public async Task<IActionResult> AdministrationRequest(PatientRequest patientRequest)
        {
            _logger.LogInformation($"Administrative request for patient id {patientRequest.PatientId} created");

            //Note: In a production application we should validate input.
            
            try
            {
                await _router.CreateRequest(new Envelope
                {
                    SendingDepartment = "laboratory",
                    ReceivingDepartment = "administration",
                    Request = patientRequest,
                    SentOn = DateTime.Now
                });

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unable to create administrative request at this time!");
                return RedirectToAction("Error", "Home");
            }
        }
    }
}