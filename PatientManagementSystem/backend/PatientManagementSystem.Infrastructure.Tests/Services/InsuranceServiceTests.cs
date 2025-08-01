using Moq;
using Moq.Protected;
using Newtonsoft.Json;

using PatientManagementSystem.ApplicationCore.DTOs;
using PatientManagementSystem.ApplicationCore.Interfaces;
using PatientManagementSystem.Infrastructure.Services;

public class InsuranceServiceTests
{
    private HttpClient CreateMockHttpClient(HttpResponseMessage response)
    {
        var handlerMock = new Mock<HttpMessageHandler>();

        handlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(response);

        return new HttpClient(handlerMock.Object)
        {
            BaseAddress = new System.Uri("https://localhost:7180/")
        };
    }

    [Fact]
    public async Task GetAllInsurers_ReturnsList_WhenSuccessful()
    {
        var insurers = new List<Insurer>
        {
            new Insurer { Id = "1", Name = "Insurer A" },
            new Insurer{Id="2", Name= "Insurer B" }
        };

    var response = new HttpResponseMessage(HttpStatusCode.OK)
    {
        Content = new StringContent(JsonConvert.SerializeObject(insurers))
    };

    var httpClient = CreateMockHttpClient(response);
        IInsuranceService service = new InsuranceService(httpClient);
        var result = await service.GetAllInsurers();

        Assert.NotNull(result);
        Assert.Equal(2, ((List<Insurer>) result).Count);
    }

[Fact]
public async Task GetInsuranceNameAsync_ReturnsName_WhenSuccessful()
{
    var response = new HttpResponseMessage(HttpStatusCode.OK)
    {
        Content = new StringContent("HealthPlus")
    };

    var httpClient = CreateMockHttpClient(response);
    var service = new InsuranceService(httpClient);

    var result = await service.GetInsuranceNameAsync("S101");

    Assert.Equal("HealthPlus", result);
}

[Fact]
public async Task GetInsuranceNameAsync_ReturnsUnknown_WhenFailed()
{
    var response = new HttpResponseMessage(HttpStatusCode.NotFound);

    var httpClient = CreateMockHttpClient(response);
    var service = new InsuranceService(httpClient);

    var result = await service.GetInsuranceNameAsync("S999");

    Assert.Equal("Unknown Insurance", result);
}

[Fact]
public async Task GetListOfPatientInsurances_ReturnsList_WhenSuccessful()
{
    var names = new[] { "HealthPlus", "MediCare" };

    var response = new HttpResponseMessage(HttpStatusCode.OK)
    {
        Content = new StringContent(JsonConvert.SerializeObject(names))
    };

    var httpClient = CreateMockHttpClient(response); 
     IInsuranceService service = new InsuranceService(httpClient);
        var result = await service.GetListOfPatientInsurances();
    Assert.Contains("HealthPlus", result);
    Assert.Contains("MediCare", result);
}

[Fact]
public async Task GetListOfInsurers_ReturnsObject_WhenSuccessful()
{
    var names = new[] { "Insurer A", "Insurer B" };

    var response = new HttpResponseMessage(HttpStatusCode.OK)
    {
        Content = new StringContent(JsonConvert.SerializeObject(names))
    };

    var httpClient = CreateMockHttpClient(response);
    var service = new InsuranceService(httpClient);

    var result = await service.GetListOfInsurers();

    Assert.NotNull(result);
}
}
