using Microsoft.Extensions.Logging;
using Steeltoe.Messaging.RabbitMQ.Attributes;

namespace BluePaw.Administration.Listeners
{
    public class PatientRequestsListener
    {
        private const string AdminQueue = "administration_requests_queue";

        private readonly ILogger<PatientRequestsListener> _logger;

        public PatientRequestsListener(ILogger<PatientRequestsListener> logger)
        {
            _logger = logger;
        }

        [RabbitListener(AdminQueue)]
        public void RecievePatientRequest(string request)
        {
            _logger.LogInformation($"Received: {request}");
        }
    }
}
