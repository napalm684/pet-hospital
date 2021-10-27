using System;
using BluePaw.Router.Controllers;
using BluePaw.Router.Services;
using BluePaw.Shared;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using TestUtilities.Attributes;
using Xunit;

namespace BluePaw.Router.Tests
{
    public class PatientRequestsControllerTests
    {
        [Theory]
        [AutoFakeItEasyData]
        public void Create(IMessagePublisherService messagePublisherService, 
            Envelope envelope)
        {
            // Arrange
            var sut = new PatientRequestsController(messagePublisherService);

            // Act
            var result = sut.Create(envelope);

            // Assert
            Assert.IsType<NoContentResult>(result);
            A.CallTo(() => messagePublisherService.PublishPatientRequest(A<Envelope>._))
                .MustHaveHappenedOnceExactly();
        }
        
        [Theory]
        [AutoFakeItEasyData]
        public void Create_Exception(IMessagePublisherService messagePublisherService, 
            Envelope envelope)
        {
            // Arrange
            A.CallTo(() => messagePublisherService.PublishPatientRequest(A<Envelope>._))
                .Throws<Exception>();
            var sut = new PatientRequestsController(messagePublisherService);

            // Act
            var result = sut.Create(envelope);

            // Assert
            Assert.IsType<StatusCodeResult>(result);
            A.CallTo(() => messagePublisherService.PublishPatientRequest(A<Envelope>._))
                .MustHaveHappenedOnceExactly();
        }
    }
}