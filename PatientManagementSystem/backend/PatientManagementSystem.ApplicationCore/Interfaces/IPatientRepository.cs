using MongoDB.Bson;


namespace PatientManagementSystem.ApplicationCore.Interfaces
{
    public interface IPatientRepository
    {
        Task <Patient> Create (Patient patient);
        Task <IEnumerable<Patient>> GetAll ();
        Task <Patient> GetById (String id);

        Task <bool> Update (String id,Patient patient);

        Task <bool> Delete (String id);
    }
}
