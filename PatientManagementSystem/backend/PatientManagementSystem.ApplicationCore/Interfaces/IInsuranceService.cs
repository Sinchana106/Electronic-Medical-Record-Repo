using System.Collections.Generic;
using System.Threading.Tasks;
using PatientManagementSystem.ApplicationCore.DTOs;

namespace PatientManagementSystem.ApplicationCore.Interfaces
{
    public interface IInsuranceService
    {

        Task<IEnumerable<Insurer>> GetAllInsurers();
        Task<string> GetInsuranceNameAsync(string insurerId);
        Task<IEnumerable<string>> GetListOfPatientInsurances();
        Task<object> GetListOfInsurers();

    }
}
