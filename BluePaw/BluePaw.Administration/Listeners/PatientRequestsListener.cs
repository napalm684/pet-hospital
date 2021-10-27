using System;
using System.Text.Json;
using BluePaw.Administration.Data;
using BluePaw.Shared;
using Microsoft.Extensions.Logging;
using Steeltoe.Messaging.RabbitMQ.Attributes;

namespace BluePaw.Administration.Listeners
{
    public class PatientRequestsListener
    {
        private const string AdminQueue = "administration_requests_queue";

        private readonly ILogger<PatientRequestsListener> _logger;
        private readonly Func<BluePawDbContext> _dbContextFactory;

        public PatientRequestsListener(ILogger<PatientRequestsListener> logger, Func<BluePawDbContext> dbContextFactory)
        {
            _logger = logger;
            _dbContextFactory = dbContextFactory;
        }

        [RabbitListener(AdminQueue)]
        public void ReceivePatientRequest(string request)
        {
            _logger.LogInformation($"Received: {request}");
            var patientRequest = JsonSerializer.Deserialize<PatientRequest>(request);

            if (patientRequest != null)
            {
                using var dbContext = _dbContextFactory();
                dbContext.AdministrationRequests.Add(new AdministrationRequest
                {
                    PatientId = patientRequest.PatientId,
                    Request = patientRequest?.Request,
                    CreateTime = DateTime.Now,
                    Completed = false
                });

                dbContext.SaveChanges();
            }
        }
    }
}
