using System;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using Steeltoe.Messaging.RabbitMQ.Core;
using BPShared = BluePaw.Shared;

namespace BluePaw.Router.Services
{
    public class MessagePublisherService : IMessagePublisherService
    {
        private const string PatientRequestsExchange = "patient_requests";
        
        private readonly ILogger<MessagePublisherService> _logger;
        private readonly IRabbitTemplate _rabbitTemplate;

        public MessagePublisherService(ILogger<MessagePublisherService> logger, 
            IRabbitTemplate rabbitTemplate)
        {
            _logger = logger;
            _rabbitTemplate = rabbitTemplate;
        }

        public void PublishPatientRequest(BPShared.Envelope envelope)
        {
            var destination = envelope.ReceivingDepartment;
            var sender = envelope.SendingDepartment;

            try
            {
                _logger.LogInformation($"Patient request received from {sender} bound for {destination}");
                _rabbitTemplate.ConvertAndSend(PatientRequestsExchange,
                    destination, JsonSerializer.Serialize(envelope.Request));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unable to publish patient request to RabbitMQ!");
            }
        }
    }
}