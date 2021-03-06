﻿using Geolocation.API.Controllers;
using Geolocation.ControllerModels;
using Geolocation.Model;
using Geolocation.Services.Interfaces;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Results;

namespace Geolocation.Tests
{
    [TestFixture]
    public class GeolocationControllerCreateTest
    {
        [Test]
        public void Can_Create_Geolocation_Details_With_IP()
        {
            // Arrange
            GeolocationDetails sampleDetails = new GeolocationDetails()
            {
                IP = "IP"
            };
            CreateGeolocationDetailsWithIpReturnModel sampleControllerModel = new CreateGeolocationDetailsWithIpReturnModel(sampleDetails);
            
            Mock<IGeolocationDetailsManager> mockManger = new Mock<IGeolocationDetailsManager>();
            mockManger.Setup(x => x.CreateWithIpAsync(It.IsAny<string>())).Returns(Task.FromResult(sampleControllerModel));

            Mock<ILocationValidator> mockLocationValidator = new Mock<ILocationValidator>();
            mockLocationValidator.Setup(x => x.IsValidIpAddress(It.IsAny<string>())).Returns(true);

            var controller = new GeolocationController(mockManger.Object, mockLocationValidator.Object);

            // Act
            var actionResult = controller.PostAsync("test").Result as CreatedNegotiatedContentResult<CreateGeolocationDetailsWithIpReturnModel>;

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.AreEqual("IP", actionResult.Content.IP);
        }

        [Test]
        public void Can_Create_Geolocation_Details_With_URL()
        {
            // Arrange
            GeolocationDetails sampleDetails = new GeolocationDetails()
            {
                URL = "URL"
            };
            CreateGeolocationDetailsWithUrlReturnModel sampleControllerModel = new CreateGeolocationDetailsWithUrlReturnModel(sampleDetails);

            Mock<IGeolocationDetailsManager> mockManger = new Mock<IGeolocationDetailsManager>();
            mockManger.Setup(x => x.CreateWithUrlAsync(It.IsAny<string>())).Returns(Task.FromResult(sampleControllerModel));

            Mock<ILocationValidator> mockLocationValidator = new Mock<ILocationValidator>();
            mockLocationValidator.Setup(x => x.IsValidUrl(It.IsAny<string>())).Returns(true);

            var controller = new GeolocationController(mockManger.Object, mockLocationValidator.Object);

            // Act
            var actionResult = controller.PostAsync("test").Result as CreatedNegotiatedContentResult<CreateGeolocationDetailsWithUrlReturnModel>;

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.AreEqual("URL", actionResult.Content.URL);
        }

        [Test]
        public void Can_Reject_Create_Geolocation_Details_When_Invalid_IP_Or_URL_Provided()
        {
            // Arrange
            Mock<IGeolocationDetailsManager> mockManger = new Mock<IGeolocationDetailsManager>();
            Mock<ILocationValidator> mockLocationValidator = new Mock<ILocationValidator>();
            mockLocationValidator.Setup(x => x.IsValidIpAddress(It.IsAny<string>())).Returns(false);
            mockLocationValidator.Setup(x => x.IsValidUrl(It.IsAny<string>())).Returns(false);

            var controller = new GeolocationController(mockManger.Object, mockLocationValidator.Object);

            // Act
            var actionResult = controller.PostAsync("test").Result as BadRequestErrorMessageResult;

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Message);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void Can_Reject_Create_Geolocation_Details_When_No_IP_Or_URL_Provided(string ipOrUrl)
        {
            // Arrange
            Mock<IGeolocationDetailsManager> mockManger = new Mock<IGeolocationDetailsManager>();
            Mock<ILocationValidator> mockLocationValidator = new Mock<ILocationValidator>();

            var controller = new GeolocationController(mockManger.Object, mockLocationValidator.Object);

            // Act
            var actionResult = controller.PostAsync(ipOrUrl).Result as BadRequestErrorMessageResult;

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Message);
        }
    }
}
