using System;
using System.Text.Json;
using BluePaw.Router.Services;
using FakeItEasy;
using BPShared = BluePaw.Shared;
using Microsoft.Extensions.Logging;
using Steeltoe.Messaging.RabbitMQ.Core;
using TestUtilities;
using TestUtilities.Attributes;
using Xunit;

namespace BluePaw.Router.Tests
{
    public class MessagePublisherTests
    {
        private const string PatientRequestsExchange = "patient_requests";
        
        [Theory]
        [AutoFakeItEasyData]
        public void PublishPatientRequest(ILogger<MessagePublisherService> logger, IRabbitTemplate rabbitTemplate,
            BPShared.Envelope envelope)
        {
            // Arrange
            var sut = new MessagePublisherService(logger, rabbitTemplate);
            
            // Act
            sut.PublishPatientRequest(envelope);
            
            // Assert
            A.CallTo(() => rabbitTemplate.ConvertAndSend(PatientRequestsExchange, envelope.ReceivingDepartment,
                    JsonSerializer.Serialize(envelope.Request, null)))
                .MustHaveHappenedOnceExactly();
        }

        [Theory]
        [AutoFakeItEasyData]
        public void PublishPatientRequest_Exception(IRabbitTemplate rabbitTemplate,
            BPShared.Envelope envelope)
        {
            // Arrange
            var dummyLogger = new DummyLogger<MessagePublisherService>();
            var exception = new Exception("something went wrong");
            A.CallTo(() => rabbitTemplate.ConvertAndSend(PatientRequestsExchange, A<string>._,
                A<string>._)).Throws(exception);
            var sut = new MessagePublisherService(dummyLogger, rabbitTemplate);

            // Act
            sut.PublishPatientRequest(envelope);

            // Assert
            Assert.True(dummyLogger.WasCalled);
        }
    }
}