using BluePaw.Ui.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using BluePaw.Shared;
using BluePaw.Ui.Services;

namespace BluePaw.Ui.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAdministrationService _administrationService;

        public HomeController(ILogger<HomeController> logger, IAdministrationService administrationService)
        {
            _logger = logger;
            _administrationService = administrationService;
        }

        public async Task<IActionResult> Index()
        {
            var requests = await _administrationService.RetrieveRequests();
            return View(requests);
        }

        public IActionResult Create()
        {
            ViewData["species"] = new[]
            {
                "Dog",
                "Cat",
                "Other"
            };
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePatientRequest createRequest)
        {
            _logger.LogInformation($"Creating patient in administration service: {createRequest.Name}");
            
            try
            {
                await _administrationService.CreatePatient(createRequest);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unable to create patient!");
                return RedirectToAction("Error");
            }
        }
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
