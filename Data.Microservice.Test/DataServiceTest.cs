using Xunit;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Microservice.App;
using Data.Microservice.Domain;
using Customer.Notify.Microservice.Domain;
using Customer.Notify.Microservice.APP;
using Data.Microservice.APP;

public class DataServicesTests
{
    private readonly Mock<IDataRepository> _repositoryMock;
    private readonly Mock<INotifyRepository> _notifyRepositoryMock;
    private readonly Mock<INotifyServices> _notifyServiceMock;
    private readonly DataServices _service;

    public DataServicesTests()
    {
        _repositoryMock = new Mock<IDataRepository>();
        _notifyRepositoryMock = new Mock<INotifyRepository>();
        _notifyServiceMock = new Mock<INotifyServices>();
        _service = new DataServices(_repositoryMock.Object, _notifyRepositoryMock.Object, _notifyServiceMock.Object);
    }

    [Fact]
    public async Task DeleteCustomerData_ReturnsSuccessMessage()
    {
        _repositoryMock.Setup(repo => repo.DeleteCustomerData("test@example.com", "Subject", "Message", 1)).ReturnsAsync("Success");
        var result = await _service.DeleteCustomerData("test@example.com", "Subject", "Message", 1);
        Assert.Equal("Customer data deleted successfully and notification sent.", result);
    }

    [Fact]
    public void GetAllData_ReturnsDataList()
    {
        var dataList = new List<CData> { new CData { CUSTOMERS_ID = 1 } };
        _repositoryMock.Setup(repo => repo.GetAllData()).Returns(dataList);
        var result = _service.GetAllData();
        Assert.Equal(dataList, result);
    }

    [Fact]
    public void GetAllDataByCustomerID_ReturnsDataList()
    {
        var dataList = new List<CData> { new CData { CUSTOMERS_ID = 1 } };
        _repositoryMock.Setup(repo => repo.GetAllDataByCustomerID(1)).Returns(dataList);
        var result = _service.GetAllDataByCustomerID(1);
        Assert.Equal(dataList, result);
    }


    [Fact]
    public async Task UpdateCustomerData_ReturnsSuccessMessage()
    {
        var cData = new CData { CUSTOMERS_ID = 1 };
        _repositoryMock.Setup(repo => repo.UpDateCustomerData(cData)).ReturnsAsync("Success");
        var result = await _service.UpdateCustomerData(cData, "test@example.com");
        Assert.Equal("Customer data updated and notification sent.", result);
    }
}
