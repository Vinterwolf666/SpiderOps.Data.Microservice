using Xunit;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Customer.Notify.Microservice.Domain;
using Data.Microservice.Service;
using Data.Microservice.APP;

namespace Data.Microservice.Tests
{
    public class NotifyServiceTests
    {
        private readonly Mock<INotifyRepository> _repositoryMock;
        private readonly NotifyService _service;

        public NotifyServiceTests()
        {
            _repositoryMock = new Mock<INotifyRepository>();
            _service = new NotifyService(_repositoryMock.Object);
        }

        [Fact]
        public async Task SendNotification_ReturnsSuccessMessage()
        {
            var email = "test@example.com";
            var subject = "Test Subject";
            var message = "Test Message";
            int customerId = 1;

            _repositoryMock.Setup(r => r.AddNotificationAsync(It.IsAny<Notification>())).Returns(Task.CompletedTask);

            var result = await _service.SendNotification(email, subject, message, customerId);

            Assert.Equal("Notification sent successfully", result);
            _repositoryMock.Verify(r => r.AddNotificationAsync(It.IsAny<Notification>()), Times.Once);
        }

        [Fact]
        public void SendEmail_ReturnsSuccessMessage()
        {
            var result = _service.SendEmail("test@example.com", "Test Subject", "Test Body", "Success");

            Assert.Contains("Success", result);
        }

        [Fact]
        public async Task AllNotificationsAsync_ReturnsList()
        {
            var notifications = new List<Notification> { new Notification { EMAIL = "test@example.com" } };
            _repositoryMock.Setup(r => r.AllNotificationsAsync()).ReturnsAsync(notifications);

            var result = await _service.AllNotificationsAsync();

            Assert.Equal(notifications, result);
        }

        [Fact]
        public async Task AllNotificationsByIDAsync_ReturnsList()
        {
            int id = 1;
            var notifications = new List<Notification> { new Notification { CUSTOMER_ID = id } };
            _repositoryMock.Setup(r => r.AllNotificationsByIDAsync(id)).ReturnsAsync(notifications);

            var result = await _service.AllNotificationsByIDAsync(id);

            Assert.Equal(notifications, result);
        }

        [Fact]
        public async Task DeleteByIDAsync_ReturnsTrue()
        {
            int id = 1;
            _repositoryMock.Setup(r => r.DeleteByIDAsync(id)).ReturnsAsync(true);

            var result = await _service.DeleteByIDAsync(id);

            Assert.True(result);
            _repositoryMock.Verify(r => r.DeleteByIDAsync(id), Times.Once);
        }
    }
}
