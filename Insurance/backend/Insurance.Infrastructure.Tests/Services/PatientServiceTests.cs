using Insurance.ApplicationCore.DTOs;
using Insurance.Infrastructure.Services;
using Moq;
using Moq.Protected;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Newtonsoft.Json;

public class PatientServiceTests
{
    [Fact]
    public async Task GetAllPatients_ReturnsPatientList_WhenResponseIsSuccessful()
    {
        // Arrange
        var expectedPatients = new List<Patient>
        {
            new Patient { Id = "1", Name = "John Doe" },
            new Patient { Id = "2", Name = "Jane Smith" }
        };

        var mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(JsonConvert.SerializeObject(expectedPatients))
        };

        var handlerMock = new Mock<HttpMessageHandler>();
        handlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(mockResponse);

        var httpClient = new HttpClient(handlerMock.Object);
        var service = new PatientService(httpClient);

        // Act
        var result = await service.GetAllPatients();

        // Assert
        Assert.Equal(2, result.Count);
        Assert.Equal("John Doe", result[0].Name);
    }

    [Fact]
    public async Task GetAllPatients_ReturnsEmptyList_WhenResponseIsNotSuccessful()
    {
        // Arrange
        var mockResponse = new HttpResponseMessage(HttpStatusCode.InternalServerError);

        var handlerMock = new Mock<HttpMessageHandler>();
        handlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(mockResponse);

        var httpClient = new HttpClient(handlerMock.Object);
        var service = new PatientService(httpClient);

        // Act
        var result = await service.GetAllPatients();

        // Assert
        Assert.Empty(result);
    }
}
