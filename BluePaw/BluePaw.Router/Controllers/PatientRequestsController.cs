using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Steeltoe.Messaging.RabbitMQ.Core;
using BPShared = BluePaw.Shared;

namespace BluePaw.Router.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PatientRequestsController : ControllerBase
    {
        private const string PatientRequestsExchange = "patient_requests";

        private readonly ILogger<PatientRequestsController> _logger;
        private readonly RabbitTemplate _rabbitTemplate;

        public PatientRequestsController(ILogger<PatientRequestsController> logger, RabbitTemplate rabbitTemplate)
        {
            _logger = logger;
            _rabbitTemplate = rabbitTemplate;
        }

        [HttpPost]
        public IActionResult Create(BPShared.Envelope envelope)
        {
            var destination = envelope.ReceivingDepartment;
            var sender = envelope.SendingDepartment;

            _logger.LogInformation($"Patient request received from {sender} bound for {destination}");
            _rabbitTemplate.ConvertAndSend(PatientRequestsExchange, 
                destination, JsonSerializer.Serialize(envelope.Request));

            return NoContent();
        }
    }
}
