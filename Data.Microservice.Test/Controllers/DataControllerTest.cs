using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Microservice.API.Controllers;
using Data.Microservice.App;
using Data.Microservice.Domain;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using Customer.Identity.Microservice.API.Services;

namespace Data.Microservice.Test.Controllers
{
    public class DataControllerTest
    {
        private readonly Mock<IDataServices> _mockService;
        private readonly RabbitMQProducer _rabbitMQProducer;
        private readonly DataController _controller;

        public DataControllerTest()
        {
            _mockService = new Mock<IDataServices>();
            _rabbitMQProducer = new RabbitMQProducer(); 
            _controller = new DataController(_mockService.Object);
        }

        [Fact]
        public void AllData_ReturnsOkResult_WithListOfCData()
        {
            
            var expectedData = new List<CData>
            {
                new CData { CUSTOMERS_ID = 1, FIRSTNAME = "John", SECONDNAME = "A", LASTNAME = "Doe", HOME_ADDRESS = "123 Street" },
                new CData { CUSTOMERS_ID = 2, FIRSTNAME = "Jane", SECONDNAME = "B", LASTNAME = "Smith", HOME_ADDRESS = "456 Avenue" }
            };

            _mockService.Setup(s => s.GetAllData()).Returns(expectedData);

            
            var result = _controller.AllData();

            
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var actualData = Assert.IsType<List<CData>>(okResult.Value);
            Assert.Equal(expectedData.Count, actualData.Count);
        }

        [Fact]
        public void AllDataByID_ReturnsData_WhenIdExists()
        {
            
            int testId = 1;
            var expectedData = new List<CData> { new CData { CUSTOMERS_ID = testId, FIRSTNAME = "John", LASTNAME = "Doe", HOME_ADDRESS = "123 Street" } };

            _mockService.Setup(s => s.GetAllDataByCustomerID(testId)).Returns(expectedData);

            
            var result = _controller.AllDataByID(testId);

            
            var actualData = Assert.IsType<List<CData>>(result.Value);
            Assert.Single(actualData);
            Assert.Equal(testId, actualData[0].CUSTOMERS_ID);
        }

        [Fact]
        public async Task NewAccountData_ReturnsOk_WhenSuccess()
        {
            
            var newCData = new CData { CUSTOMERS_ID = 3, FIRSTNAME = "Alice", LASTNAME = "Brown", HOME_ADDRESS = "789 Road" };
            string expectedResponse = "Account Created";

            _mockService.Setup(s => s.NewCustomerData(newCData)).ReturnsAsync(expectedResponse);

            
            var result = await _controller.NewAccountData(newCData);

            
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(expectedResponse, okResult.Value);
        }

        [Fact]
        public async Task RemoveAnAccount_ReturnsOk_WhenSuccess()
        {
            
            int testId = 2;
            string expectedResponse = "Account Deleted";

            _mockService.Setup(s => s.DeleteCustomerData(testId)).ReturnsAsync(expectedResponse);

            
            var result = await _controller.RemoveAnAccount(testId);

            
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(expectedResponse, okResult.Value);
        }

        [Fact]
        public async Task UpdateAnAccount_ReturnsOk_WhenSuccess()
        {
            
            var updatedCData = new CData { CUSTOMERS_ID = 1, FIRSTNAME = "Updated", LASTNAME = "User", HOME_ADDRESS = "Updated Address" };
            string expectedResponse = "Account Updated";

            _mockService.Setup(s => s.UpDateCustomerData(updatedCData)).ReturnsAsync(expectedResponse);

            
            var result = await _controller.UpdateAnAccount(updatedCData);

            
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(expectedResponse, okResult.Value);
        }
    }
}
