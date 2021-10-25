using BluePaw.Ui.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
            //TODO: Temp testing
            await _administrationService.CreatePatient(new CreatePatientRequest
            {
                OwnerName = "Shawn Vause",
                OwnerPhone = "111-111-1111",
                Species = "Canine",
                Name = "Doc"
            });
            return View();
        }

        public IActionResult Laboratory()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
