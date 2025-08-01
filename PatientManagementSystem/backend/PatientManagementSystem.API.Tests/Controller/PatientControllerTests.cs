using Xunit;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PatientManagementSystem.Api.Controllers;
using PatientManagementSystem.ApplicationCore.DTOs;
using PatientManagementSystem.ApplicationCore.Interfaces;

namespace PatientManagementSystem.API.Tests
{
    public class PatientControllerTests
    {
        private readonly Mock<IPatientRepository> _mockRepo;
        private readonly Mock<IInsuranceService> _mockInsuranceService;
        private readonly PatientController _controller;

        public PatientControllerTests()
        {
            _mockRepo = new Mock<IPatientRepository>();
            _mockInsuranceService = new Mock<IInsuranceService>();
            _controller = new PatientController(_mockInsuranceService.Object, _mockRepo.Object);
        }

        [Fact]
        public async Task Get_ReturnsOk_WithPatients()
        {
            var patients = new List<Patient> { new Patient { Id = "1", Name = "John" } };
            _mockRepo.Setup(r => r.GetAll()).ReturnsAsync(patients);

            var result = await _controller.Get();

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnPatients = Assert.IsAssignableFrom<IEnumerable<Patient>>(okResult.Value);
            Assert.Single(returnPatients);
        }

        [Fact]
        public async Task Get_ById_ReturnsNotFound_WhenPatientMissing()
        {
            _mockRepo.Setup(r => r.GetById("123")).ReturnsAsync((Patient)null);

            var result = await _controller.Get("123");

            Assert.IsType<NotFoundObjectResult>(result.Result);
        }

        [Fact]
        public async Task Post_ReturnsCreatedAtAction_WhenValid()
        {
            var patient = new Patient { Id = "1", Name = "Jane" };

            var result = await _controller.Post(patient);

            var created = Assert.IsType<CreatedAtActionResult>(result.Result);
            var returnPatient = Assert.IsType<Patient>(created.Value);
            Assert.Equal("Jane", returnPatient.Name);
        }

        [Fact]
        public async Task Put_ReturnsNoContent_WhenPatientExists()
        {
            var patient = new Patient { Id = "1", Name = "Updated" };
            _mockRepo.Setup(r => r.GetById("1")).ReturnsAsync(patient);

            var result = await _controller.Put("1", patient);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Put_ReturnsNotFound_WhenPatientMissing()
        {
            var patient = new Patient { Id = "1", Name = "Updated" };
            _mockRepo.Setup(r => r.GetById("1")).ReturnsAsync((Patient)null);

            var result = await _controller.Put("1", patient);

            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task Delete_ReturnsNoContent_WhenPatientExists()
        {
            var patient = new Patient { Id = "1", Name = "John" };
            _mockRepo.Setup(r => r.GetById("1")).ReturnsAsync(patient);

            var result = await _controller.Delete("1");

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Delete_ReturnsNotFound_WhenPatientMissing()
        {
            _mockRepo.Setup(r => r.GetById("1")).ReturnsAsync((Patient)null);

            var result = await _controller.Delete("1");

            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task GetAllInsurers_ReturnsOk_WhenInsurersExist()
        {
            var insurers = new List<Insurer>
            {
                new Insurer { Id = "1", Name = "Insurer A" },
                new Insurer { Id = "2", Name = "Insurer B" }
            };
            _mockInsuranceService.Setup(s => s.GetAllInsurers()).ReturnsAsync(insurers);

            var result = await _controller.GetAllInsurers();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnInsurers = Assert.IsAssignableFrom<IEnumerable<Insurer>>(okResult.Value);
            Assert.Equal(2, ((List<Insurer>)returnInsurers).Count);
        }

        [Fact]
        public async Task GetPatientInsurance_ReturnsOk_WhenInsuranceExists()
        {
            string insurerId = "S101";
            string insuranceName = "HealthPlus";
            _mockInsuranceService.Setup(s => s.GetInsuranceNameAsync(insurerId)).ReturnsAsync(insuranceName);

            var result = await _controller.GetPatientInsurance(insurerId);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var value = Assert.IsType<Dictionary<string, string>>(okResult.Value);
            Assert.Equal("HealthPlus", value["InsuranceName"]);
        }

        [Fact]
        public async Task GetPatientInsurance_ReturnsNotFound_WhenInsuranceMissing()
        {
            string insurerId = "S999";
            _mockInsuranceService.Setup(s => s.GetInsuranceNameAsync(insurerId)).ReturnsAsync(string.Empty);

            var result = await _controller.GetPatientInsurance(insurerId);

            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task GetListOfPatientInsurances_ReturnsOk_WithNames()
        {
            var insuranceNames = new List<string> { "HealthPlus", "MediCare" };
            _mockInsuranceService.Setup(s => s.GetListOfPatientInsurances()).ReturnsAsync(insuranceNames);

            var result = await _controller.GetListOfPatientInsurances();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var value = Assert.IsAssignableFrom<IEnumerable<string>>(okResult.Value);
            Assert.Equal(2, ((List<string>)value).Count);
        }
    }
}
