using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PatientManagementSystem.ApplicationCore.Interfaces;
using PatientManagementSystem.Infrastructure.Services;


namespace PatientManagementSystem.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatientController : ControllerBase
    {
        private readonly IPatientRepository _patientRepository;
        private readonly InsuranceService _insuranceService;

        public PatientController(InsuranceService insuranceService, IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
            _insuranceService = insuranceService;
        }

        // GET: api/Patient
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Patient>>> Get()
        {
            try
            {
                var patients = await _patientRepository.GetAll();
                return Ok(patients);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving patients.", error = ex.Message });
            }
        }

        // GET api/Patient/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Patient>> Get(string id)
        {
            try
            {
                var patient = await _patientRepository.GetById(id);
                if (patient == null)
                    return NotFound(new { message = $"Patient with ID {id} not found." });

                return Ok(patient);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving the patient.", error = ex.Message });
            }
        }

        // POST api/Patient
        [HttpPost]
        public async Task<ActionResult<Patient>> Post([FromBody] Patient patient)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _patientRepository.Create(patient);
                return CreatedAtAction(nameof(Get), new { id = patient.Id }, patient);
            }
            catch (DbUpdateException dbEx)
            {
                return StatusCode(500, new { message = "Database error occurred.", error = dbEx.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while creating the patient.", error = ex.Message });
            }
        }

        // PUT api/Patient/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] Patient patient)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var existing = await _patientRepository.GetById(id);
                if (existing == null)
                    return NotFound(new { message = $"Patient with ID {id} not found." });

                await _patientRepository.Update(id, patient);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while updating the patient.", error = ex.Message });
            }
        }

        // DELETE api/Patient/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var existing = await _patientRepository.GetById(id);
                if (existing == null)
                    return NotFound(new { message = $"Patient with ID {id} not found." });

                await _patientRepository.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while deleting the patient.", error = ex.Message });
            }
        }

        // GET api/Patient/insurance/
        [HttpGet("insurance")]
        public async Task<IActionResult> GetAllInsurers()
        {
            try
            {
                var insurances = await _insuranceService.GetAllInsurers();
                if (insurances == null || !insurances.Any())
                    return NotFound(new { message = "No insurers found." });

                return Ok(insurances);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving insurers.", error = ex.Message });
            }
        }

        // GET api/Patient/insurance/S101
        [HttpGet("insurance/{insurerId}")]
        public async Task<IActionResult> GetPatientInsurance(string insurerId)
        {
            try
            {
                var insuranceName = await _insuranceService.GetInsuranceNameAsync(insurerId);
                if (string.IsNullOrEmpty(insuranceName))
                    return NotFound(new { message = $"No insurance found for ID {insurerId}." });

                return Ok(new Dictionary<string, string> { { "InsuranceName", insuranceName } });

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving insurance name.", error = ex.Message });
            }
        }

        // GET api/Patient/insurance/names
        [HttpGet("insurance/names")]
        public async Task<IActionResult> GetListOfPatientInsurances()
        {
            try
            {
                var insuranceNames = await _insuranceService.GetListOfPatientInsurances();
                return Ok(insuranceNames);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving insurance names.", error = ex.Message });
            }
        }
    }
}
