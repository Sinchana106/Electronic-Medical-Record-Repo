using InsuranceApi.Controllers;
using Insurance.ApplicationCore.DTOs;
using Insurance.ApplicationCore.Interfaces;
using Insurance.ApplicationCore.Models;
using Insurance.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;


public class InsuranceControllerTests
{
    private readonly Mock<ILogger<InsuranceController>> _loggerMock;
    private readonly Mock<IInsuranceRepository> _repoMock;
    private readonly Mock<IPatientService> _patientServiceMock;
    private readonly InsuranceController _controller;

    public InsuranceControllerTests()
    {
        _loggerMock = new Mock<ILogger<InsuranceController>>();
        _repoMock = new Mock<IInsuranceRepository>();
        _patientServiceMock = new Mock<IPatientService>();

        //_controller = new InsuranceController(_loggerMock.Object, _repoMock.Object, _patientServiceMock.Object);
    }


    [Fact]
    public async Task Create_ReturnsCreatedResult()
    {
        var insurer = new Insurer { Id = "1", Name = "Test" };
        _repoMock.Setup(r => r.Create(insurer)).ReturnsAsync(insurer);

        var result = await _controller.Create(insurer);

        var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        Assert.Equal(insurer, createdResult.Value);
    }

    [Fact]
    public async Task GetById_ReturnsInsurer_WhenFound()
    {
        var insurer = new Insurer { Id = "1", Name = "Test" };
        _repoMock.Setup(r => r.GetById("1")).ReturnsAsync(insurer);

        var result = await _controller.GetById("1");

        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        Assert.Equal(insurer, okResult.Value);
    }

    [Fact]
    public async Task GetById_ReturnsNotFound_WhenNotFound()
    {
        _repoMock.Setup(r => r.GetById("1")).ReturnsAsync((Insurer)null);

        var result = await _controller.GetById("1");

        Assert.IsType<NotFoundObjectResult>(result.Result);
    }

    
    [Fact]
    public async Task GetInsuranceNames_ReturnsNames()
    {
        var names = new string[] { "Insurer A", "Insurer B" };
        _repoMock.Setup(r => r.GetListOfInsuranceName()).ReturnsAsync(names);

        var result = await _controller.GetInsuranceNames();

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(names, okResult.Value);
    }

    [Fact]
    public async Task Get_ReturnsAllInsurers()
    {
        var insurers = new List<Insurer> { new Insurer { Id = "1", Name = "Test" } };
        _repoMock.Setup(r => r.GetAll()).ReturnsAsync(insurers);

        var result = await _controller.Get();

        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        Assert.Equal(insurers, okResult.Value);
    }

    [Fact]
    public async Task Update_ReturnsUpdatedInsurer_WhenFound()
    {
        var insurer = new Insurer { Id = "1", Name = "Updated" };
        _repoMock.Setup(r => r.Update("1", insurer)).ReturnsAsync(true);

        var result = await _controller.Update("1", insurer);

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(true, okResult.Value);
    }

   

    [Fact]
    public async Task Delete_ReturnsNoContent_WhenDeleted()
    {
        _repoMock.Setup(r => r.Delete("1")).ReturnsAsync(true);

        var result = await _controller.Delete("1");

        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task Delete_ReturnsNotFound_WhenNotFound()
    {
        _repoMock.Setup(r => r.Delete("1")).ReturnsAsync(false);

        var result = await _controller.Delete("1");

        Assert.IsType<NotFoundObjectResult>(result);
    }

    [Fact]
    public async Task GetNameByInsuranceCode_ReturnsName_WhenFound()
    {
        _repoMock.Setup(r => r.GetInsuranceNameByInsuranceCode("ABC")).ReturnsAsync("Insurer ABC");

        var result = await _controller.GetNameByInsuranceCode("ABC");

        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        Assert.Equal("Insurer ABC", okResult.Value);
    }

    [Fact]
    public async Task GetNameByInsuranceCode_ReturnsNotFound_WhenNotFound()
    {
        _repoMock.Setup(r => r.GetInsuranceNameByInsuranceCode("XYZ")).ReturnsAsync((string)null);

        var result = await _controller.GetNameByInsuranceCode("XYZ");

        Assert.IsType<NotFoundObjectResult>(result.Result);
    }
}
