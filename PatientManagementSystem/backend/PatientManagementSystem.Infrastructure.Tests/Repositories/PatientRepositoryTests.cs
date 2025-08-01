using Humanizer;
using Moq;
using PatientManagementSystem.ApplicationCore.Interfaces;

public class PatientRepositoryTests
{
    private readonly Mock<IPatientRepository> _mockRepo;

    public PatientRepositoryTests()
    {
        _mockRepo = new Mock<IPatientRepository>();
    }

    [Fact]
    public async Task Create_ReturnsCreatedPatient()
    {
        var patient = new Patient { Id = "P001", Name = "John Doe" };
        _mockRepo.Setup(r => r.Create(patient)).ReturnsAsync(patient);

        var result = await _mockRepo.Object.Create(patient);

        Assert.NotNull(result);
        Assert.Equal("John Doe", result.Name);
    }

    [Fact]
    public async Task GetAll_ReturnsListOfPatients()
    {
        var patients = new List<Patient>
        {
            new Patient { Id = "1", Name = "Alice" },
            new Patient { Id = "2", Name = "Bob" }
        };

        _mockRepo.Setup(r => r.GetAll()).ReturnsAsync(patients);

        var result = await _mockRepo.Object.GetAll();

        Assert.NotNull(result);
        Assert.Equal(2, ((List<Patient>)result).Count);
    }

    [Fact]
    public async Task GetById_ReturnsCorrectPatient()
    {
        var patient = new Patient { Id = "P123", Name = "Charlie" };
        _mockRepo.Setup(r => r.GetById("P123")).ReturnsAsync(patient);

        var result = await _mockRepo.Object.GetById("P123");

        Assert.NotNull(result);
        Assert.Equal("Charlie", result.Name);
    }

    [Fact]
    public async Task Update_ReturnsTrue_WhenSuccessful()
    {
        var patient = new Patient { Id = "P456", Name = "Updated Name" };
        _mockRepo.Setup(r => r.Update("P456", patient)).ReturnsAsync(true);

        var result = await _mockRepo.Object.Update("P456", patient);

        Assert.True(result);
    }

    [Fact]
    public async Task Delete_ReturnsTrue_WhenSuccessful()
    {
        _mockRepo.Setup(r => r.Delete("P789")).ReturnsAsync(true);

        var result = await _mockRepo.Object.Delete("P789");

        Assert.True(result);
    }
}

