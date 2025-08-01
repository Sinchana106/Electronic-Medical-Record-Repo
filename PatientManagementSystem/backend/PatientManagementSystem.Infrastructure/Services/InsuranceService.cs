using MongoDB.Bson.IO;
using Newtonsoft.Json;
using PatientManagementSystem.ApplicationCore.DTOs;
using PatientManagementSystem.ApplicationCore.Interfaces;
namespace PatientManagementSystem.Infrastructure.Services
{
    public class InsuranceService
    {
        private readonly HttpClient _httpClient;

        public InsuranceService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        //To get list of Insurer
        public async Task<IEnumerable<Insurer>> GetAllInsurers()
        {
            var response = await _httpClient.GetAsync("https://localhost:7180/Insurance/");
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var insurers = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Insurer>>(responseContent);
                return insurers;
            }
            return new List<Insurer>();
        }

        public async Task<string> GetInsuranceNameAsync(string insurerId)
        {
            var response = await _httpClient.GetAsync($"https://localhost:7180/Insurance/insuranceCode/{insurerId}");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }

            return "Unknown Insurance";
        }
        public async Task<IEnumerable<string>> GetListOfPatientInsurances()
        {
            var response = await _httpClient.GetAsync("https://localhost:7180/Insurance/list");

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var names = Newtonsoft.Json.JsonConvert.DeserializeObject<string[]>(responseContent);
                return names;
            }

            return new string[] { "Empty" };
        }

        public async Task<Object> GetListOfInsurers()
        {
            var response = await _httpClient.GetAsync("https://localhost:7180/Insurance/");

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var names = Newtonsoft.Json.JsonConvert.DeserializeObject<string[]>(responseContent);
                return names;
            }

            return null;
        }

        

       
    }

}
