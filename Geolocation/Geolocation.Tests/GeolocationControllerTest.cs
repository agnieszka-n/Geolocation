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
    public class GeolocationControllerTest
    {
        [Test]
        public void Can_Create_Geolocation_Details()
        {
            // Arrange
            GeolocationDetails sampleDetails = new GeolocationDetails()
            {
                ID = 123
            };
            Mock<IGeolocationDetailsManager> mockManger = new Mock<IGeolocationDetailsManager>();
            mockManger.Setup(x => x.Create(It.IsAny<string>())).Returns(sampleDetails);

            var controller = new GeolocationController(mockManger.Object);

            // Act
            var actionResult = controller.Post("test IP") as CreatedNegotiatedContentResult<GeolocationDetails>;

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.AreEqual(123, actionResult.Content.ID);
        }
    }
}
