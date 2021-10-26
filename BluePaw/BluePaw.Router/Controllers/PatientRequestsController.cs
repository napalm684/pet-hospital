using System;
using BluePaw.Router.Services;
using Microsoft.AspNetCore.Mvc;
using BPShared = BluePaw.Shared;

namespace BluePaw.Router.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PatientRequestsController : ControllerBase
    {
        private readonly IMessagePublisherService _messagePublisherService;

        public PatientRequestsController(IMessagePublisherService messagePublisherService)
        {
            _messagePublisherService = messagePublisherService;
        }

        [HttpPost]
        public IActionResult Create(BPShared.Envelope envelope)
        {
            try
            {
                _messagePublisherService.PublishPatientRequest(envelope);
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
    }
}
