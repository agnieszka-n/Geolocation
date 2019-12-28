using Geolocation.API.Controllers;
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
    public class GeolocationControllerGetTest
    {
        [Test]
        public void Can_Get_Geolocation_Details_By_IP()
        {
            // Arrange
            GeolocationDetails sampleModel = new GeolocationDetails()
            {
                IP = "sample IP"
            };
            GetGeolocationDetailsByIpReturnModel sampleViewModel = new GetGeolocationDetailsByIpReturnModel(sampleModel);

            Mock<IGeolocationDetailsManager> mockManager = new Mock<IGeolocationDetailsManager>();
            mockManager.Setup(x => x.GetByIp(It.IsAny<string>())).Returns(sampleViewModel);

            Mock<ILocationValidator> mockLocationValidator = new Mock<ILocationValidator>();
            mockLocationValidator.Setup(x => x.IsValidIpAddress(It.IsAny<string>())).Returns(true);

            var controller = new GeolocationController(mockManager.Object, mockLocationValidator.Object);

            // Act
            var actionResult = controller.Get("ip", null) as OkNegotiatedContentResult<GetGeolocationDetailsByIpReturnModel>;

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.AreEqual("sample IP", actionResult.Content.IP);
        }

        [Test]
        public void Can_Get_Geolocation_Details_By_URL()
        {
            // Arrange
            GeolocationDetails sampleModel = new GeolocationDetails()
            {
                URL = "sample URL"
            };
            GetGeolocationDetailsByUrlReturnModel sampleViewModel = new GetGeolocationDetailsByUrlReturnModel(sampleModel);

            Mock<IGeolocationDetailsManager> mockManager = new Mock<IGeolocationDetailsManager>();
            mockManager.Setup(x => x.GetByUrl(It.IsAny<string>())).Returns(sampleViewModel);

            Mock<ILocationValidator> mockLocationValidator = new Mock<ILocationValidator>();
            mockLocationValidator.Setup(x => x.IsValidUrl(It.IsAny<string>())).Returns(true);

            var controller = new GeolocationController(mockManager.Object, mockLocationValidator.Object);

            // Act
            var actionResult = controller.Get(null, "url") as OkNegotiatedContentResult<GetGeolocationDetailsByUrlReturnModel>;

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.AreEqual("sample URL", actionResult.Content.URL);
        }

        [Test]
        public void Can_Return_BadRequest_When_Invalid_IP_Provided()
        {
            // Arrange
            Mock<IGeolocationDetailsManager> mockManager = new Mock<IGeolocationDetailsManager>();

            Mock<ILocationValidator> mockLocationValidator = new Mock<ILocationValidator>();
            mockLocationValidator.Setup(x => x.IsValidIpAddress(It.IsAny<string>())).Returns(false);

            var controller = new GeolocationController(mockManager.Object, mockLocationValidator.Object);

            // Act
            var actionResult = controller.Get("ip", null) as BadRequestErrorMessageResult;

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Message);
        }

        [Test]
        public void Can_Return_BadRequest_When_Invalid_URL_Provided()
        {
            // Arrange
            Mock<IGeolocationDetailsManager> mockManager = new Mock<IGeolocationDetailsManager>();

            Mock<ILocationValidator> mockLocationValidator = new Mock<ILocationValidator>();
            mockLocationValidator.Setup(x => x.IsValidUrl(It.IsAny<string>())).Returns(false);

            var controller = new GeolocationController(mockManager.Object, mockLocationValidator.Object);

            // Act
            var actionResult = controller.Get(null, "url") as BadRequestErrorMessageResult;

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Message);
        }

        [Test]
        public void Can_Return_NotFound_When_IP_Nonexistent()
        {
            // Arrange
            Mock<IGeolocationDetailsManager> mockManager = new Mock<IGeolocationDetailsManager>();
            mockManager.Setup(x => x.GetByIp(It.IsAny<string>())).Returns<GetGeolocationDetailsByIpReturnModel>(null);

            Mock<ILocationValidator> mockLocationValidator = new Mock<ILocationValidator>();
            mockLocationValidator.Setup(x => x.IsValidIpAddress(It.IsAny<string>())).Returns(true);

            var controller = new GeolocationController(mockManager.Object, mockLocationValidator.Object);

            // Act
            var actionResult = controller.Get("ip", null) as NotFoundResult;

            // Assert
            Assert.IsNotNull(actionResult);
        }

        [Test]
        public void Can_Return_NotFound_When_URL_Nonexistent()
        {
            // Arrange
            Mock<IGeolocationDetailsManager> mockManager = new Mock<IGeolocationDetailsManager>();
            mockManager.Setup(x => x.GetByUrl(It.IsAny<string>())).Returns<GetGeolocationDetailsByUrlReturnModel>(null);

            Mock<ILocationValidator> mockLocationValidator = new Mock<ILocationValidator>();
            mockLocationValidator.Setup(x => x.IsValidUrl(It.IsAny<string>())).Returns(true);

            var controller = new GeolocationController(mockManager.Object, mockLocationValidator.Object);

            // Act
            var actionResult = controller.Get(null, "url") as NotFoundResult;

            // Assert
            Assert.IsNotNull(actionResult);
        }

        [Test]
        public void Can_Get_Geolocation_Details_When_Both_IP_URL_Provided()
        {
            // Arrange
            Mock<IGeolocationDetailsManager> mockManager = new Mock<IGeolocationDetailsManager>();
            Mock<ILocationValidator> mockLocationValidator = new Mock<ILocationValidator>();

            var controller = new GeolocationController(mockManager.Object, mockLocationValidator.Object);

            // Act
            var actionResult = controller.Get("ip", "url") as BadRequestErrorMessageResult;

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Message);
        }

        [Test]
        public void Can_Return_BadRequest_When_No_IP_URL_Provided()
        {
            // Arrange
            Mock<IGeolocationDetailsManager> mockManager = new Mock<IGeolocationDetailsManager>();
            Mock<ILocationValidator> mockLocationValidator = new Mock<ILocationValidator>();

            var controller = new GeolocationController(mockManager.Object, mockLocationValidator.Object);

            // Act
            var actionResult = controller.Get(null, null) as BadRequestErrorMessageResult;

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Message);
        }
    }
}
