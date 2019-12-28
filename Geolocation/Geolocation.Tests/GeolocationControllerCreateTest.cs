using Geolocation.API.Controllers;
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
                ID = 123
            };
            Mock<IGeolocationDetailsManager> mockManger = new Mock<IGeolocationDetailsManager>();
            mockManger.Setup(x => x.CreateWithIp(It.IsAny<string>())).Returns(sampleDetails);

            Mock<ILocationValidator> mockLocationValidator = new Mock<ILocationValidator>();
            mockLocationValidator.Setup(x => x.IsValidIpAddress(It.IsAny<string>())).Returns(true);

            var controller = new GeolocationController(mockManger.Object, mockLocationValidator.Object);

            // Act
            var actionResult = controller.Post("test") as CreatedNegotiatedContentResult<GeolocationDetails>;

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.AreEqual(123, actionResult.Content.ID);
        }

        [Test]
        public void Can_Create_Geolocation_Details_With_URL()
        {
            // Arrange
            GeolocationDetails sampleDetails = new GeolocationDetails()
            {
                ID = 123
            };
            Mock<IGeolocationDetailsManager> mockManger = new Mock<IGeolocationDetailsManager>();
            mockManger.Setup(x => x.CreateWithUrl(It.IsAny<string>())).Returns(sampleDetails);

            Mock<ILocationValidator> mockLocationValidator = new Mock<ILocationValidator>();
            mockLocationValidator.Setup(x => x.IsValidUrl(It.IsAny<string>())).Returns(true);

            var controller = new GeolocationController(mockManger.Object, mockLocationValidator.Object);

            // Act
            var actionResult = controller.Post("test") as CreatedNegotiatedContentResult<GeolocationDetails>;

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.AreEqual(123, actionResult.Content.ID);
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
            var actionResult = controller.Post("test") as BadRequestErrorMessageResult;

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
            var actionResult = controller.Post(ipOrUrl) as BadRequestErrorMessageResult;

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Message);
        }
    }
}
