using Insurance.ApplicationCore.DTOs;
using Insurance.ApplicationCore.Interfaces;
using Insurance.ApplicationCore.Models;
using Insurance.Infrastructure.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace InsuranceApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [EnableCors("AllowAllOrigins")]
    public class InsuranceController : ControllerBase
    {
        private readonly ILogger<InsuranceController> _logger;
        private readonly IInsuranceRepository _insuranceRepository;
        private readonly PatientService _patientService;


        public InsuranceController(ILogger<InsuranceController> logger, IInsuranceRepository insuranceRepository, PatientService patientService)
        {
            _logger = logger;
            _insuranceRepository = insuranceRepository;
            _patientService = patientService;
        }


        [HttpPost]
        public async Task<ActionResult<Insurer>> Create(Insurer insurer)
        {
            try
            {
                var insurerDetails = await _insuranceRepository.Create(insurer);
                return CreatedAtAction(nameof(GetById), new { id = insurerDetails.Id }, insurerDetails);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating insurer");
                return StatusCode(500, new { message = "An error occurred while creating the insurer." });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Insurer>> GetById(string id)
        {
            var insurer = await _insuranceRepository.GetById(id);
            if (insurer == null)
                return NotFound(new { message = $"Insurer with ID {id} not found." });

            return Ok(insurer);
        }

        [HttpGet("patients")]
        public async Task<ActionResult<IEnumerable<Patient>>> GetAllPatients()
        {
            try
            {
                var patients = await _patientService.GetAllPatients();
                return Ok(patients);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching patients");
                return StatusCode(500, new { message = "An error occurred while fetching patients." });
            }
        }

        [HttpGet("insuranceCode/{insuranceCode}")]
        public async Task<ActionResult<string?>> GetNameByInsuranceCode(string insuranceCode)
        {
            var name = await _insuranceRepository.GetInsuranceNameByInsuranceCode(insuranceCode);
            if (string.IsNullOrEmpty(name))
                return NotFound(new { message = $"No insurer found with code {insuranceCode}." });

            return Ok(name);
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetInsuranceNames()
        {
            try
            {
                var names = await _insuranceRepository.GetListOfInsuranceName();
                return Ok(names);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching insurance names");
                return StatusCode(500, new { message = "An error occurred while fetching insurance names." });
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Insurer>>> Get()
        {
            try
            {
                var result = await _insuranceRepository.GetAll();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching insurers");
                return StatusCode(500, new { message = "An error occurred while fetching insurers." });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, Insurer insurer)
        {
            try
            {
                var updated = await _insuranceRepository.Update(id, insurer);
                if (updated == null)
                    return NotFound(new { message = $"Insurer with ID {id} not found." });

                return Ok(updated);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating insurer");
                return StatusCode(500, new { message = "An error occurred while updating the insurer." });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var deleted = await _insuranceRepository.Delete(id);
                if (!deleted)
                    return NotFound(new { message = $"Insurer with ID {id} not found." });

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting insurer");
                return StatusCode(500, new { message = "An error occurred while deleting the insurer." });
            }
        }
    }
}
