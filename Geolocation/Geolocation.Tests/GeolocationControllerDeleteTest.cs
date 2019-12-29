using Geolocation.API.Controllers;
using Geolocation.ControllerModels;
using Geolocation.Model;
using Geolocation.Services.Interfaces;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Results;

namespace Geolocation.Tests
{
    [TestFixture]
    public class GeolocationControllerDeleteTest
    {
        [Test]
        public void Can_Delete_Geolocation_Details_By_IP()
        {
            // Arrange
            Mock<IGeolocationDetailsManager> mockManger = new Mock<IGeolocationDetailsManager>();

            Mock<ILocationValidator> mockLocationValidator = new Mock<ILocationValidator>();
            mockLocationValidator.Setup(x => x.IsValidIpAddress(It.IsAny<string>())).Returns(true);

            var controller = new GeolocationController(mockManger.Object, mockLocationValidator.Object);

            // Act
            var actionResult = controller.Delete("test") as StatusCodeResult;

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.AreEqual(HttpStatusCode.NoContent, actionResult.StatusCode);
        }

        [Test]
        public void Can_Delete_Geolocation_Details_By_URL()
        {
            // Arrange
            Mock<IGeolocationDetailsManager> mockManger = new Mock<IGeolocationDetailsManager>();

            Mock<ILocationValidator> mockLocationValidator = new Mock<ILocationValidator>();
            mockLocationValidator.Setup(x => x.IsValidUrl(It.IsAny<string>())).Returns(true);

            var controller = new GeolocationController(mockManger.Object, mockLocationValidator.Object);

            // Act
            var actionResult = controller.Delete("test") as StatusCodeResult;

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.AreEqual(HttpStatusCode.NoContent, actionResult.StatusCode);
        }

        [Test]
        public void Can_Reject_Delete_Geolocation_Details_When_Invalid_IP_Or_URL_Provided()
        {
            // Arrange
            Mock<IGeolocationDetailsManager> mockManger = new Mock<IGeolocationDetailsManager>();

            Mock<ILocationValidator> mockLocationValidator = new Mock<ILocationValidator>();
            mockLocationValidator.Setup(x => x.IsValidIpAddress(It.IsAny<string>())).Returns(false);
            mockLocationValidator.Setup(x => x.IsValidUrl(It.IsAny<string>())).Returns(false);

            var controller = new GeolocationController(mockManger.Object, mockLocationValidator.Object);

            // Act
            var actionResult = controller.Delete("test") as BadRequestErrorMessageResult;

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Message);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void Can_Reject_Delete_Geolocation_Details_When_No_IP_Or_URL_Provided(string ipOrUrl)
        {
            // Arrange
            Mock<IGeolocationDetailsManager> mockManger = new Mock<IGeolocationDetailsManager>();

            Mock<ILocationValidator> mockLocationValidator = new Mock<ILocationValidator>();
            mockLocationValidator.Setup(x => x.IsValidIpAddress(It.IsAny<string>())).Returns(false);
            mockLocationValidator.Setup(x => x.IsValidUrl(It.IsAny<string>())).Returns(false);

            var controller = new GeolocationController(mockManger.Object, mockLocationValidator.Object);

            // Act
            var actionResult = controller.Delete(ipOrUrl) as BadRequestErrorMessageResult;

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Message);
        }
    }
}
