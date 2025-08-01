using Insurance.ApplicationCore.DTOs;
using Insurance.ApplicationCore.Interfaces;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace Insurance.Infrastructure.Services
{
    public class PatientService : IPatientService
    {
        private readonly HttpClient _httpClient;

        public PatientService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Patient>> GetAllPatients()
        {
            var response = await _httpClient.GetAsync("https://localhost:7057/api/Patient");
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var patients = JsonConvert.DeserializeObject<List<Patient>>(responseContent);
                return patients;
            }
            return new List<Patient>();
        }
    }
}
